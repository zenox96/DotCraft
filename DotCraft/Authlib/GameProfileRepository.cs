using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authlib
{
    public interface GameProfileRepository
    {
        void FindProfilesByNames(String[] var1, Agent var2, ProfileLookupCallback var3);
    }
}
