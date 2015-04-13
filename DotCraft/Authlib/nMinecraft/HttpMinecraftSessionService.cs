using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authlib.nMinecraft
{
    public abstract class HttpMinecraftSessionService : BaseMinecraftSessionService
    {
        protected HttpMinecraftSessionService(HttpAuthenticationService authenticationService)
            : base(authenticationService)
        {
        }

        public HttpAuthenticationService getAuthenticationService()
        {
            return (HttpAuthenticationService)base.getAuthenticationService();
        }
    }
}
