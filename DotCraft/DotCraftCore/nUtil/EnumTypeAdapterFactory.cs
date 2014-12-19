using System;
using System.Collections;

namespace DotCraftCore.nUtil
{

	using Gson = com.google.gson.Gson;
	using TypeAdapter = com.google.gson.TypeAdapter;
	using TypeAdapterFactory = com.google.gson.TypeAdapterFactory;
	using TypeToken = com.google.gson.reflect.TypeToken;
	using JsonReader = com.google.gson.stream.JsonReader;
	using JsonToken = com.google.gson.stream.JsonToken;
	using JsonWriter = com.google.gson.stream.JsonWriter;

	public class EnumTypeAdapterFactory : TypeAdapterFactory
	{
		

		public virtual TypeAdapter create(Gson p_create_1_, TypeToken p_create_2_)
		{
			Type var3 = p_create_2_.RawType;

			if(!var3.Enum)
			{
				return null;
			}
			else
			{
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final HashMap var4 = new HashMap();
				Hashtable var4 = new Hashtable();
				object[] var5 = var3.EnumConstants;
				int var6 = var5.Length;

				for(int var7 = 0; var7 < var6; ++var7)
				{
					object var8 = var5[var7];
					var4.Add(this.func_151232_a(var8), var8);
				}

//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//				return new TypeAdapter()
//			{
//				
//				public void write(JsonWriter p_write_1_, Object p_write_2_) throws IOException
//				{
//					if (p_write_2_ == null)
//					{
//						p_write_1_.nullValue();
//					}
//					else
//					{
//						p_write_1_.value(EnumTypeAdapterFactory.func_151232_a(p_write_2_));
//					}
//				}
//				public Object read(JsonReader p_read_1_) throws IOException
//				{
//					if (p_read_1_.peek() == JsonToken.NULL)
//					{
//						p_read_1_.nextNull();
//						return null;
//					}
//					else
//					{
//						return var4.get(p_read_1_.nextString());
//					}
//				}
//			};
			}
		}

		private string func_151232_a(object p_151232_1_)
		{
			return p_151232_1_ is Enum ? ((Enum)p_151232_1_).name().ToLower(Locale.US) : p_151232_1_.ToString().ToLower(Locale.US);
		}
	}

}