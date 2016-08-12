using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authlib
{
    public enum UserType
    {
        NULL = 0,
        LEGACY = 1,
        MOJANG = 2
    }

    public static class UserTypeExtension
    {
        private static readonly Dictionary<String, UserType> BY_NAME;
        private static readonly String name;

        public static UserType ByName(String name)
        {
            return (UserType)BY_NAME[name.ToLower()];
        }

        public static String GetName() {
            return name;
        }

        static UserTypeExtension() {
            BY_NAME = new Dictionary<String, UserType>();
            UserType[] enums = Enum.GetValues(typeof(UserType)).Cast<UserType>().ToArray();
            int len = enums.Length;

            for(int i = 0; i < len; ++i) {
                UserType type = enums[i];
                BY_NAME[type.ToString().ToLower()] = type;
            }

        }
    }
}
