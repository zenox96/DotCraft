using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authlib
{
    public interface ProfileLookupCallback
    {
        void OnProfileLookupSucceeded(GameProfile var1);

        void OnProfileLookupFailed(GameProfile var1, Exception var2);
    }
}
