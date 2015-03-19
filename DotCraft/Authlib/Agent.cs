using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authlib
{
    public class Agent
    {
        public static readonly Agent MINECRAFT = new Agent("Minecraft", 1);
        public static readonly Agent SCROLLS = new Agent("Scrolls", 1);
        private readonly String _name;
        private readonly int _version;

        public String Name
        {
            get { return _name; }
        }

        public int Version
        {
            get { return _version; }
        }

        public Agent(String name, int version) {
            this._name = name;
            this._version = version;
        }

        public override String ToString() {
            return "Agent{name=\'" + this._name + '\'' + ", version=" + this._version + '}';
        }
    }

}
