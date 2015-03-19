using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authlib.nMinecraft
{
    interface MinecraftSessionService
    {
        void joinServer(GameProfile var1, String var2, String var3);

        GameProfile hasJoinedServer(GameProfile var1, String var2);

        Dictionary<Type, MinecraftProfileTexture> getTextures(GameProfile var1, Boolean var2);

        GameProfile fillProfileProperties(GameProfile var1, Boolean var2);
    }
}
