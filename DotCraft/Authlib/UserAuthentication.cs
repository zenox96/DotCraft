using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authlib
{
    public interface UserAuthentication {
        Boolean canLogIn();

        void logIn();

        void logOut();

        Boolean isLoggedIn();

        Boolean canPlayOnline();

        GameProfile[] getAvailableProfiles();

        GameProfile getSelectedProfile();

        void selectGameProfile(GameProfile var1);

        void loadFromStorage(Dictionary<String, Object> var1);

        Dictionary<String, Object> saveForStorage();

        void setUsername(String var1);

        void setPassword(String var1);

        String getAuthenticatedToken();

        String getUserID();

        Dictionary<String, Property> getUserProperties();

        UserType getUserType();
    }
}
