using DotCraftUtil;
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
        private readonly Dictionary<String, Property> userProperties = new Dictionary<String, Property>();
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
            this.getModifiableUserProperties().Clear();
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
            if(this.isLoggedIn() && this.canPlayOnline() && password.Trim() != "") {
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
                    List<Dictionary<String,String>> profile = (List<Dictionary<String,String>>)credentials["userProperties"];
                    List<Dictionary<String,String>>.Enumerator t = profile.GetEnumerator();

                    while(t.MoveNext())
                    {
                        Dictionary<String,String> i = t.Current;
                        String propertyMap = i["name"];
                        name = i["value"];
                        value = i["signature"];
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
                        List<Dictionary<String, String>> t1 = (List<Dictionary<String, String>>)credentials["profileProperties"];
                        List<Dictionary<String, String>>.Enumerator i = t1.GetEnumerator();

                        while(i.MoveNext()) {
                            Dictionary<String, String> propertyMap1 = i.Current;
                            name = propertyMap1["name"];
                            value = propertyMap1["value"];
                            String signature = propertyMap1["signature"];
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

            if(this.getUserProperties().Count() != 0) {
                List<Dictionary<String,String>> selectedProfile = new List<Dictionary<String,String>>();
                Dictionary<String, Property>.ValueCollection.Enumerator properties = this.getUserProperties().Values.GetEnumerator();

                while(properties.MoveNext()) {
                    Property i = properties.Current;
                    Dictionary<String,String> profileProperty = new Dictionary<String,String>();
                    profileProperty["name"] = i.Name;
                    profileProperty["value"] = i.Value;
                    profileProperty["signature"] = i.Signature;
                    selectedProfile.Add(profileProperty);
                }

                result["userProperties"] = selectedProfile;
            }

            GameProfile selectedProfile1 = this.getSelectedProfile();
            if(selectedProfile1 != null) {
                result["displayName"] = selectedProfile1.Name;
                result["uuid"] = selectedProfile1.Id;
                List<Dictionary<String, String>> properties1 = new List<Dictionary<String, String>>( );
                Dictionary<String, Property>.ValueCollection.Enumerator i = selectedProfile1.Properties.Values.GetEnumerator();

                while(i.MoveNext()) {
                    Property profileProperty1 = i.Current;
                    Dictionary<String,String> property = new Dictionary<String,String>();
                    property["name"] = profileProperty1.Name;
                    property["value"] = profileProperty1.Value;
                    property["signature"] = profileProperty1.Signature;
                    properties1.Add(property);
                }

                if(properties1.Count() != 0) {
                    result["profileProperties"] = properties1;
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

        public Dictionary<String, Property> getUserProperties() {
            if(this.isLoggedIn()) {
                return new Dictionary<String, Property>(this.getModifiableUserProperties());
            } else {
                return new Dictionary<String, Property>();
            }
        }

        protected Dictionary<String, Property> getModifiableUserProperties() {
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

        public void logIn();

        public bool canPlayOnline();

        public GameProfile[] getAvailableProfiles();

        public void selectGameProfile(GameProfile var1);

        public string getAuthenticatedToken();

        Dictionary<String, Property> UserAuthentication.getUserProperties();
    }
}
