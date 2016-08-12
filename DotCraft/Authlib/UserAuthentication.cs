using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authlib
{
    public interface UserAuthentication {
        bool canLogIn();

        void logIn();

        void logOut();

        bool isLoggedIn();

        bool canPlayOnline();

        GameProfile[] getAvailableProfiles();

        GameProfile getSelectedProfile();

        void selectGameProfile(GameProfile var1);

        void loadFromStorage(Dictionary<string, object> var1);

        Dictionary<string, object> saveForStorage();

        void setUsername(string var1);

        void setPassword(string var1);

        string getAuthenticatedToken();

        string getUserID();

        Dictionary<string, Property> getUserProperties();

        UserType getUserType();
    }
}
