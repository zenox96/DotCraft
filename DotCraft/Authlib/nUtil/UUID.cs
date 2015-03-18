using System;
using System.Text;

namespace Authlib.nUtil
{
    /***
     * 
     * Helper Class for interop with the java UUIDs
     * 
     ***/
    class UUID
    {
        private static Random rand = new Random();

        public UUID(long mostSigBits, long leastSigBits)
        {
            MostSignificantBits = (ulong)mostSigBits;
            LeastSignificantBits = (ulong)leastSigBits;
        }

        public UUID(ulong mostSigBits, ulong leastSigBits)
        {
            MostSignificantBits = mostSigBits;
            LeastSignificantBits = leastSigBits;
        }

        private UUID(byte[] data) {
            ulong msb = 0;
            ulong lsb = 0;
            for (int i=0; i<8; i++)
                msb = (msb << 8) | (data[i] & 0xffu);
            for (int i=8; i<16; i++)
                lsb = (lsb << 8) | (data[i] & 0xffu);
            this.MostSignificantBits = msb;
            this.LeastSignificantBits = lsb;
        }

        public static UUID RandomUUID()
        {
            UUID tmp = new UUID(rand.NextLong(), rand.NextLong());
            tmp.MostSignificantBits = (tmp.MostSignificantBits | 0x000000000000F000UL) & 0x0000000000004000UL;
            tmp.LeastSignificantBits = (tmp.LeastSignificantBits | 0xC000000000000000UL) & 0x8000000000000000UL;
            return tmp;
        }

        public static UUID NameUUIDFromBytes(byte[] name)
        {
            throw new NotImplementedException("NameUUIDFromBytes isnt implemented yet");
            /* MD5 md = MD5.Create( );
            byte[] md5Bytes = md.digest(name);
            md5Bytes[6]  &= 0x0f;  // clear version
            md5Bytes[6]  |= 0x30;  // set to version 3
            md5Bytes[8]  &= 0x3f;  // clear variant
            md5Bytes[8]  |= 0x80;  // set to IETF variant
            return new UUID(md5Bytes); */
        }

        public static UUID FromString(String name)
        {
            var tmp = name.Replace("-","");

            return new UUID(long.Parse(tmp.Substring(0, 15), System.Globalization.NumberStyles.AllowHexSpecifier), long.Parse(tmp.Substring(16), System.Globalization.NumberStyles.AllowHexSpecifier));
        }

        public ulong LeastSignificantBits
        {
            get;
            private set;
        }

        public ulong MostSignificantBits
        {
            get;
            private set;
        }

        public int Version
        {
            get
            {
                return (int)(MostSignificantBits >> 12 & 0xFL);
            }
        }

        public int Variant
        {
            get
            {
                return (int)(LeastSignificantBits >> 62 & 0x3L);
            }
        }

        public long GetTimestamp( )
        {
            if (Version != 1) throw new NotSupportedException("GetTimestamp() is only avalible for type 1 UUID (time-based)");

            return (long)(this.MostSignificantBits >> 32 & 0xFFFFFFFFL);
        }

        public long GetClockSequence( )
        {
            if (Version != 1)
                throw new NotSupportedException("GetClockSequence() is only avalible for type 1 UUID (time-based)");

            return (long)(LeastSignificantBits >> 48 & 0x3FFFL);
        }

        public long GetNode( )
        {
            if (Version != 1)
                throw new NotSupportedException("GetNode() is only avalible for type 1 UUID (time-based)");

            return (long)(this.LeastSignificantBits & 0xFFFFFFFFFFFFL);
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("{0,8:8}-", this.MostSignificantBits >> 32 & 0xFFFFFFFFL);
            sb.AppendFormat("{0,4:X4}-", this.MostSignificantBits >> 16 & 0xFFFFL);
            sb.AppendFormat("{0,4:X4}-", this.MostSignificantBits & 0xFFFFL);

            sb.AppendFormat("{0,4:X4}-", this.LeastSignificantBits >> 48 & 0xFFFFL);
            sb.AppendFormat("{0,12:X12}", this.LeastSignificantBits & 0xFFFFFFFFFFFFL);

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is UUID){
                var tmpObj = (UUID)obj;
                if (tmpObj.LeastSignificantBits == this.LeastSignificantBits & tmpObj.MostSignificantBits == this.MostSignificantBits)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
