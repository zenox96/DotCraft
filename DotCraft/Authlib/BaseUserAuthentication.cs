using DotCraftUtil;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authlib
{
    public abstract class BaseUserAuthentication : UserAuthentication
    {
        private static readonly ILog LOGGER = LogManager.GetLogger("Minecraft");
        protected static readonly string STORAGE_KEY_PROFILE_NAME = "displayName";
        protected static readonly string STORAGE_KEY_PROFILE_ID = "uuid";
        protected static readonly string STORAGE_KEY_PROFILE_PROPERTIES = "profileProperties";
        protected static readonly string STORAGE_KEY_USER_NAME = "username";
        protected static readonly string STORAGE_KEY_USER_ID = "userid";
        protected static readonly string STORAGE_KEY_USER_PROPERTIES = "userProperties";
        private readonly AuthenticationService authenticationService;
        private readonly Dictionary<string, Property> userProperties = new Dictionary<string, Property>();
        private string userid;
        private string username;
        private string password;
        private GameProfile selectedProfile;
        private UserType userType;

        protected BaseUserAuthentication(AuthenticationService authenticationService) {
            if (authenticationService == null)
                throw new ArgumentNullException("Authentication Service is null");

            this.authenticationService = authenticationService;
        }

        public bool canLogIn() {
            return !canPlayOnline() && !getUsername().Trim().Equals("") && !getPassword().Trim().Equals("");
        }

        public void logOut() {
            password = null;
            userid = null;
            setSelectedProfile(null);
            getModifiableUserProperties().Clear();
            setUserType(UserType.NULL);
        }

        public bool isLoggedIn() {
            return getSelectedProfile() != null;
        }

        public void setUsername(string username) {
            if(isLoggedIn() && canPlayOnline()) {
                throw new AccessViolationException("Cannot change username whilst logged in & online");
            } else {
                this.username = username;
            }
        }

        public void setPassword(string password) {
            if(isLoggedIn() && canPlayOnline() && password.Trim() != "") {
                throw new AccessViolationException("Cannot set password whilst logged in & online");
            } else {
                this.password = password;
            }
        }

        protected string getUsername() {
            return username;
        }

        protected string getPassword() {
            return password;
        }

        public void loadFromStorage(Dictionary<string, object> credentials) {
            logOut();
            setUsername((string)credentials["username"]);
            if(credentials.ContainsKey("userid")) {
                userid = (string)credentials["userid"];
            } else {
                userid = username;
            }

            string name;
            string value;
            if(credentials.ContainsKey("userProperties")) {
                try {
                    List<Dictionary<string,string>> profile = (List<Dictionary<string,string>>)credentials["userProperties"];
                    List<Dictionary<string,string>>.Enumerator t = profile.GetEnumerator();

                    while(t.MoveNext())
                    {
                        Dictionary<string,string> i = t.Current;
                        string propertyMap = i["name"];
                        name = i["value"];
                        value = i["signature"];
                        if(value == null) {
                            getModifiableUserProperties()[propertyMap] = new Property(propertyMap, name);
                        } else {
                            getModifiableUserProperties()[propertyMap] = new Property(propertyMap, name, value);
                        }
                    }
                } catch (Exception var10) {
                    LOGGER.Warn("Couldn\'t deserialize user properties", var10);
                }
            }

            if(credentials.ContainsKey("displayName") && credentials.ContainsKey("uuid")) {
                GameProfile profile1 = new GameProfile(UUIDTypeAdapter.fromString((string)credentials["uuid"]), (string)credentials["displayName"]);
                if(credentials.ContainsKey("profileProperties")) {
                    try {
                        List<Dictionary<string, string>> t1 = (List<Dictionary<string, string>>)credentials["profileProperties"];
                        List<Dictionary<string, string>>.Enumerator i = t1.GetEnumerator();

                        while(i.MoveNext()) {
                            Dictionary<string, string> propertyMap1 = i.Current;
                            name = propertyMap1["name"];
                            value = propertyMap1["value"];
                            string signature = propertyMap1["signature"];
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

                setSelectedProfile(profile1);
            }

        }

        public Dictionary<string, object> saveForStorage() {
            Dictionary<string, object> result = new Dictionary<string, object>();
            if(getUsername() != null) {
                result["username"] = getUsername();
            }

            if(getUserID() != null) {
                result["userid"] = getUserID();
            } else if(getUsername() != null) {
                result["username"] = getUsername();
            }

            if(getUserProperties().Count() != 0) {
                List<Dictionary<string,string>> selectedProfile = new List<Dictionary<string,string>>();
                Dictionary<string, Property>.ValueCollection.Enumerator properties = getUserProperties().Values.GetEnumerator();

                while(properties.MoveNext()) {
                    Property i = properties.Current;
                    Dictionary<string,string> profileProperty = new Dictionary<string,string>();
                    profileProperty["name"] = i.Name;
                    profileProperty["value"] = i.Value;
                    profileProperty["signature"] = i.Signature;
                    selectedProfile.Add(profileProperty);
                }

                result["userProperties"] = selectedProfile;
            }

            GameProfile selectedProfile1 = getSelectedProfile();
            if(selectedProfile1 != null) {
                result["displayName"] = selectedProfile1.Name;
                result["uuid"] = selectedProfile1.Id;
                List<Dictionary<string, string>> properties1 = new List<Dictionary<string, string>>( );
                Dictionary<string, Property>.ValueCollection.Enumerator i = selectedProfile1.Properties.Values.GetEnumerator();

                while(i.MoveNext()) {
                    Property profileProperty1 = i.Current;
                    Dictionary<string,string> property = new Dictionary<string,string>();
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
            return selectedProfile;
        }

        public override string ToString() {
            StringBuilder result = new StringBuilder();
            result.Append(GetType().FullName);
            result.Append("{");
            if(isLoggedIn()) {
                result.Append("Logged in as ");
                result.Append(getUsername());
                if(getSelectedProfile() != null) {
                    result.Append(" / ");
                    result.Append(getSelectedProfile());
                    result.Append(" - ");
                    if(canPlayOnline()) {
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
            return authenticationService;
        }

        public string getUserID() {
            return userid;
        }

        public Dictionary<string, Property> getUserProperties() {
            if(isLoggedIn()) {
                return new Dictionary<string, Property>(getModifiableUserProperties());
            } else {
                return new Dictionary<string, Property>();
            }
        }

        protected Dictionary<string, Property> getModifiableUserProperties() {
            return userProperties;
        }

        public UserType getUserType() {
            return isLoggedIn() ? (userType == UserType.NULL ? UserType.LEGACY : userType) : UserType.NULL;
        }

        protected void setUserType(UserType userType) {
            this.userType = userType;
        }

        protected void setUserid(string userid) {
            this.userid = userid;
        }

        public abstract void logIn();

        public abstract bool canPlayOnline();

        public abstract GameProfile[] getAvailableProfiles();

        public abstract void selectGameProfile(GameProfile var1);

        public abstract string getAuthenticatedToken();
    }
}
