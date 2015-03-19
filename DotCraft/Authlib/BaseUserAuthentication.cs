using Authlib.nProperties;
using log4net;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authlib
{
    public abstract class BaseUserAuthentication : UserAuthentication
    {
        private static readonly ILog LOGGER = LogManager.GetLogger();
        protected static readonly String STORAGE_KEY_PROFILE_NAME = "displayName";
        protected static readonly String STORAGE_KEY_PROFILE_ID = "uuid";
        protected static readonly String STORAGE_KEY_PROFILE_PROPERTIES = "profileProperties";
        protected static readonly String STORAGE_KEY_USER_NAME = "username";
        protected static readonly String STORAGE_KEY_USER_ID = "userid";
        protected static readonly String STORAGE_KEY_USER_PROPERTIES = "userProperties";
        private readonly AuthenticationService authenticationService;
        private readonly PropertyMap userProperties = new PropertyMap();
        private String userid;
        private String username;
        private String password;
        private GameProfile selectedProfile;
        private UserType userType;

        protected BaseUserAuthentication(AuthenticationService authenticationService) {
            if (authenticationService == null)
                throw new ArgumentNullException("Authentication Service is null");

            this.authenticationService = authenticationService;
        }

        public Boolean canLogIn() {
            return !this.canPlayOnline() && !this.getUsername().Trim().Equals("") && !this.getPassword().Trim().Equals("");
        }

        public void logOut() {
            this.password = null;
            this.userid = null;
            this.setSelectedProfile((GameProfile)null);
            this.getModifiableUserProperties().clear();
            this.setUserType(UserType.NULL);
        }

        public Boolean isLoggedIn() {
            return this.getSelectedProfile() != null;
        }

        public void setUsername(String username) {
            if(this.isLoggedIn() && this.canPlayOnline()) {
                throw new AccessViolationException("Cannot change username whilst logged in & online");
            } else {
                this.username = username;
            }
        }

        public void setPassword(String password) {
            if(this.isLoggedIn() && this.canPlayOnline() && StringUtils.isNotBlank(password)) {
                throw new AccessViolationException("Cannot set password whilst logged in & online");
            } else {
                this.password = password;
            }
        }

        protected String getUsername() {
            return this.username;
        }

        protected String getPassword() {
            return this.password;
        }

        public void loadFromStorage(Dictionary<String, Object> credentials) {
            this.logOut();
            this.setUsername((String)credentials["username"]);
            if(credentials.ContainsKey("userid")) {
                this.userid = (String)credentials["userid"];
            } else {
                this.userid = this.username;
            }

            String name;
            String value;
            if(credentials.ContainsKey("userProperties")) {
                try {
                    List<Dictionary<String,Object>> profile = (List<Dictionary<String,Object>>)credentials["userProperties"];
                    List<Dictionary<String,Object>>.Enumerator t = profile.GetEnumerator();

                    while(t.MoveNext())
                    {
                        Dictionary<String,Object> i = t.Current;
                        String propertyMap = (String)i["name"];
                        name = (String)i["value"];
                        value = (String)i["signature"];
                        if(value == null) {
                            this.getModifiableUserProperties()[propertyMap] = new Property(propertyMap, name);
                        } else {
                            this.getModifiableUserProperties()[propertyMap] = new Property(propertyMap, name, value);
                        }
                    }
                } catch (Exception var10) {
                    LOGGER.Warn("Couldn\'t deserialize user properties", var10);
                }
            }

            if(credentials.ContainsKey("displayName") && credentials.ContainsKey("uuid")) {
                GameProfile profile1 = new GameProfile(UUIDTypeAdapter.fromString((String)credentials["uuid"]), (String)credentials["displayName"]);
                if(credentials.ContainsKey("profileProperties")) {
                    try {
                        List<Dictionary<String, Object>> t1 = (List<Dictionary<String, Object>>)credentials["profileProperties"];
                        List<Dictionary<String, Object>>.Enumerator i = t1.GetEnumerator();

                        while(i.MoveNext()) {
                            Dictionary<String, Object> propertyMap1 = (Dictionary<String, Object>)i.Current;
                            name = (String)propertyMap1["name"];
                            value = (String)propertyMap1["value"];
                            String signature = (String)propertyMap1["signature"];
                            if(signature == null) {
                                profile1.Properties[name] = new Property(name, value);
                            } else {
                                profile1.Properties[name] = new Property(name, value, signature);
                            }
                        }
                    } catch (Exception var9) {
                        LOGGER.Warn("Couldn\'t deserialize profile properties", var9);
                    }
                }

                this.setSelectedProfile(profile1);
            }

        }

        public Dictionary<String, Object> saveForStorage() {
            Dictionary<String, Object> result = new Dictionary<String, Object>();
            if(this.getUsername() != null) {
                result["username"] = this.getUsername();
            }

            if(this.getUserID() != null) {
                result["userid"] = this.getUserID();
            } else if(this.getUsername() != null) {
                result["username"] = this.getUsername();
            }

            if(!this.getUserProperties().isEmpty()) {
                ArrayList selectedProfile = new ArrayList();
                List<Property>.Enumerator properties = this.getUserProperties().values().iterator();

                while(properties.MoveNext()) {
                    Property i = properties.Current;
                    Dictionary<String,Object> profileProperty = new Dictionary<String,Object>();
                    profileProperty["name"] = i.Name;
                    profileProperty["value"] = i.Value;
                    profileProperty["signature"] = i.Signature;
                    selectedProfile.add(profileProperty);
                }

                result["userProperties"] = selectedProfile;
            }

            GameProfile selectedProfile1 = this.getSelectedProfile();
            if(selectedProfile1 != null) {
                result.put("displayName", selectedProfile1.getName());
                result.put("uuid", selectedProfile1.getId());
                ArrayList properties1 = new ArrayList();
                Iterator i$1 = selectedProfile1.getProperties().values().iterator();

                while(i$1.hasNext()) {
                    Property profileProperty1 = (Property)i$1.next();
                    HashMap property = new HashMap();
                    property.put("name", profileProperty1.getName());
                    property.put("value", profileProperty1.getValue());
                    property.put("signature", profileProperty1.getSignature());
                    properties1.add(property);
                }

                if(!properties1.isEmpty()) {
                    result.put("profileProperties", properties1);
                }
            }

            return result;
        }

        protected void setSelectedProfile(GameProfile selectedProfile) {
            this.selectedProfile = selectedProfile;
        }

        public GameProfile getSelectedProfile() {
            return this.selectedProfile;
        }

        public override String ToString() {
            StringBuilder result = new StringBuilder();
            result.Append(this.GetType().FullName);
            result.Append("{");
            if(this.isLoggedIn()) {
                result.Append("Logged in as ");
                result.Append(this.getUsername());
                if(this.getSelectedProfile() != null) {
                    result.Append(" / ");
                    result.Append(this.getSelectedProfile());
                    result.Append(" - ");
                    if(this.canPlayOnline()) {
                        result.Append("Online");
                    } else {
                        result.Append("Offline");
                    }
                }
            } else {
                result.Append("Not logged in");
            }

            result.Append("}");
            return result.ToString();
        }

        public AuthenticationService getAuthenticationService() {
            return this.authenticationService;
        }

        public String getUserID() {
            return this.userid;
        }

        public PropertyMap getUserProperties() {
            if(this.isLoggedIn()) {
                PropertyMap result = new PropertyMap();
                result.putAll(this.getModifiableUserProperties());
                return result;
            } else {
                return new PropertyMap();
            }
        }

        protected PropertyMap getModifiableUserProperties() {
            return this.userProperties;
        }

        public UserType getUserType() {
            return this.isLoggedIn() ? (this.userType == UserType.NULL ? UserType.LEGACY : this.userType) : UserType.NULL;
        }

        protected void setUserType(UserType userType) {
            this.userType = userType;
        }

        protected void setUserid(String userid) {
            this.userid = userid;
        }
    }
}
