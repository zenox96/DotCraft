using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotCraftUtil
{
    public class UUIDTypeAdapter : TypeAdapter<UUID>
    {
        public UUIDTypeAdapter() {
        }

        public void write(JsonWriter outputput, UUID value)
        {
            output.value(fromUUID(value));
        }

        public UUID read(JsonReader intput)
        {
            return fromString(intput.nextString());
        }

        public static String fromUUID(UUID value)
        {
            return value.ToString().Replace("-", "");
        }

        public static UUID fromString(String input)
        {
            StringBuilder sb = new StringBuilder(input.Substring(0,8));
            sb.Append(input.Substring(7,4));
            sb.Append(input.Substring(11,4));
            sb.Append(input.Substring(15,4));
            sb.Append(input.Substring(19));
            return UUID.FromString(sb.ToString());
        }
    }

    public abstract class TypeAdapter<T>
    {
        public TypeAdapter() {
        }

        public abstract void write(JsonWriter var1, T var2);

        public sealed void toJson(Writer output, T value)
        {
            JsonWriter writer = new JsonWriter(output);
            this.write(writer, value);
        }

        /*public sealed TypeAdapter<T> nullSafe()
        {
            return new TypeAdapter()
            {
                public void write(JsonWriter output, T value)
                {
                    if(value == null) {
                        output.nullValue();
                    } else {
                        TypeAdapter.this.write(output, value);
                    }

                }

                public T read(JsonReader reader)
                {
                    if(reader.peek() == JsonToken.NULL) {
                        reader.nextNull();
                        return null;
                    } else {
                        return TypeAdapter.this.read(reader);
                    }
                }
            };
        }*/

        public sealed String toJson(T value)
        {
            StringWriter stringWriter = new StringWriter();
            this.toJson(stringWriter, value);
            return stringWriter.toString();
        }

        public sealed JsonElement toJsonTree(T value)
        {
            try
            {
                JsonTreeWriter e = new JsonTreeWriter();
                this.write(e, value);
                return e.get();
            } catch (IOException var3) {
                throw new JsonIOException(var3);
            }
        }

        public abstract T read(JsonReader var1);

        public sealed T fromJson(Reader input)
        {
            JsonReader reader = new JsonReader(input);
            return this.read(reader);
        }

        public sealed T fromJson(String json)
        {
            return this.fromJson((Reader)(new StringReader(json)));
        }

        public sealed T fromJsonTree(JsonElement jsonTree)
        {
            try
            {
                JsonTreeReader e = new JsonTreeReader(jsonTree);
                return this.read(e);
            } catch (IOException var3) {
                throw new JsonIOException(var3);
            }
        }
    }

}
