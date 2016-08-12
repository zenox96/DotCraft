using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Authlib.nMinecraft;

namespace Authlib
{
    public abstract class BaseAuthenticationService : AuthenticationService
    {
        public BaseAuthenticationService()
        {
        }

        public abstract MinecraftSessionService CreateMinecraftSessionService();

        public abstract GameProfileRepository CreateProfileRepository();

        public abstract UserAuthentication CreateUserAuthentication(Agent var1);
    }
}
