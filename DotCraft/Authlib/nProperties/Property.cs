using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authlib.nProperties
{
    public class Property
    {
        private readonly String _name;
        private readonly String _value;
        private readonly String _signature;

        public String Name
        {
            get { return _name; }
        }

        public String Value
        {
            get { return _value; }
        }

        public String Signature
        {
            get { return _signature; }
        }

        public Property(String value, String name)
            : this(value, name, (string)null)
        {
        }

        public Property(String name, String value, String signature) {
            this._name = name;
            this._value = value;
            this._signature = signature;
        }

        public Boolean HasSignature
        {
            get { return this._signature != null; }
        }

        public Boolean isSignatureValid(PublicKey publicKey) {
            try {
                Signature e = Signature.getInstance("SHA1withRSA");
                e.initVerify(publicKey);
                e.update(this._value.getBytes());
                return e.verify(Base64.decodeBase64(this._signature));
            } catch (NoSuchAlgorithmException var3) {
                var3.printStackTrace();
            } catch (InvalidKeyException var4) {
                var4.printStackTrace();
            } catch (SignatureException var5) {
                var5.printStackTrace();
            }

            return false;
        }
    }
}
