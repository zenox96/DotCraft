using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authlib.nMinecraft
{
    public abstract class BaseMinecraftSessionService : MinecraftSessionService
    {
        private readonly AuthenticationService authenticationService;

        protected BaseMinecraftSessionService(AuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        public AuthenticationService getAuthenticationService()
        {
            return this.authenticationService;
        }
    }
}
