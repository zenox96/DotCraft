using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authlib
{
    interface ProfileLookupCallback
    {
        void onProfileLookupSucceeded(GameProfile var1);

        void onProfileLookupFailed(GameProfile var1, Exception var2);
    }
}
