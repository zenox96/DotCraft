using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authlib.nMinecraft
{
    public interface MinecraftSessionService
    {
        void JoinServer(GameProfile var1, String var2, String var3);

        GameProfile HasJoinedServer(GameProfile var1, String var2);

        Dictionary<Type, MinecraftProfileTexture> GetTextures(GameProfile var1, Boolean var2);

        GameProfile FillProfileProperties(GameProfile var1, Boolean var2);
    }
}
