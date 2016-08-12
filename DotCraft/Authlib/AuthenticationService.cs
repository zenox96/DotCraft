using Authlib.nMinecraft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authlib
{
    public interface AuthenticationService
    {
        UserAuthentication CreateUserAuthentication(Agent var1);

        MinecraftSessionService CreateMinecraftSessionService( );

        GameProfileRepository CreateProfileRepository( );
    }
}
