﻿using DotCraftUtil;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authlib
{
    public class GameProfile
    {
        private readonly UUID _id;
        private readonly String _name;
        private readonly Dictionary<String, Property> _properties = new Dictionary<String, Property>( );

        public UUID Id
        {
            get { return _id; }
        }

        public String Name
        {
            get { return _name; }
        }

        public Dictionary<String, Property> Properties
        {
            get { return _properties; }
        }

        public Boolean Legacy
        {
            get;
            private set;
        }

        public GameProfile(UUID id, String name)
        {
            if (id == null && name.Trim()=="")
            {
                throw new ArgumentException("Name and ID cannot both be blank");
            }
            else
            {
                this._id = id;
                this._name = name;
            }
        }

        public Boolean isComplete( )
        {
            return this.Id != null && Name.Trim()!="";
        }

        public Boolean equals(Object o)
        {
            if (this == o)
            {
                return true;
            }
            else if (o != null && this.GetType( ) == o.GetType( ))
            {
                GameProfile that = (GameProfile)o;
                if (this.Id != null)
                {
                    if (!this.Id.Equals(that.Id))
                    {
                        return false;
                    }
                }
                else if (that.Id != null)
                {
                    return false;
                }

                if (this.Name != null)
                {
                    if (this.Name.Equals(that.Name))
                    {
                        return true;
                    }
                }
                else if (that.Name == null)
                {
                    return true;
                }

                return false;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode( )
        {
            int result = this.Id != null ? this.Id.GetHashCode( ) : 0;
            result = 31 * result + (this.Name != null ? this.Name.GetJavaHashCode( ) : 0);
            return result;
        }

        public override String ToString( )
        {
            var sb = new StringBuilder();
            sb.Append("id=");
            sb.Append(this.Id);
            sb.Append(",name=");
            sb.Append(this.Name);
            sb.Append(",properties=");
            sb.Append(this.Properties);
            sb.Append(",legacy=");
            sb.Append(this.Legacy);
            sb.Append(",");
            return sb.ToString();
        }
    }
}
