using Authlib.nMinecraft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authlib
{
    public interface AuthenticationService
    {
        UserAuthentication createUserAuthentication(Agent var1);

        MinecraftSessionService createMinecraftSessionService( );

        GameProfileRepository createProfileRepository( );
    }
}
