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

        public abstract GameProfile FillProfileProperties(GameProfile var1, bool var2);

        public AuthenticationService getAuthenticationService()
        {
            return this.authenticationService;
        }

        public abstract Dictionary<Type, MinecraftProfileTexture> GetTextures(GameProfile var1, bool var2);

        public abstract GameProfile HasJoinedServer(GameProfile var1, string var2);

        public abstract void JoinServer(GameProfile var1, string var2, string var3);
    }
}
