using Authlib.nProperties;
using Authlib.nUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authlib
{
    public class GameProfile
    {

        private readonly UUID id;
        private readonly String name;
        private readonly PropertyMap properties = new PropertyMap();
        private Boolean legacy;
    }
}
