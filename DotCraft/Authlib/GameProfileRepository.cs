using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authlib
{
    interface GameProfileRepository
    {
        void findProfilesByNames(String[] var1, Agent var2, ProfileLookupCallback var3);
    }
}
