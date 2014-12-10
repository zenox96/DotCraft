using System;
using System.Collections;

namespace DotCraftCore.Server.Management
{

	using Charsets = com.google.common.base.Charsets;
	using Lists = com.google.common.collect.Lists;
	using Maps = com.google.common.collect.Maps;
	using Files = com.google.common.io.Files;
	using Gson = com.google.gson.Gson;
	using GsonBuilder = com.google.gson.GsonBuilder;
	using JsonDeserializationContext = com.google.gson.JsonDeserializationContext;
	using JsonDeserializer = com.google.gson.JsonDeserializer;
	using JsonElement = com.google.gson.JsonElement;
	using JsonObject = com.google.gson.JsonObject;
	using JsonSerializationContext = com.google.gson.JsonSerializationContext;
	using JsonSerializer = com.google.gson.JsonSerializer;
	using IOUtils = org.apache.commons.io.IOUtils;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class UserList
	{
		protected internal static readonly Logger field_152693_a = LogManager.Logger;
		protected internal readonly Gson field_152694_b;
		private readonly File field_152695_c;
		private readonly IDictionary field_152696_d = Maps.newHashMap();
		private bool field_152697_e = true;
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		private static final ParameterizedType field_152698_f = new ParameterizedType()
//	{
//		private static final String __OBFID = "CL_00001875";
//		public Type[] getActualTypeArguments()
//		{
//			return new Type[] {UserListEntry.class};
//		}
//		public Type getRawType()
//		{
//			return List.class;
//		}
//		public Type getOwnerType()
//		{
//			return null;
//		}
//	};
		private const string __OBFID = "CL_00001876";

		public UserList(File p_i1144_1_)
		{
			this.field_152695_c = p_i1144_1_;
			GsonBuilder var2 = (new GsonBuilder()).setPrettyPrinting();
			var2.registerTypeHierarchyAdapter(typeof(UserListEntry), new UserList.Serializer(null));
			this.field_152694_b = var2.create();
		}

		public virtual bool func_152689_b()
		{
			return this.field_152697_e;
		}

		public virtual void func_152686_a(bool p_152686_1_)
		{
			this.field_152697_e = p_152686_1_;
		}

		public virtual void func_152687_a(UserListEntry p_152687_1_)
		{
			this.field_152696_d.Add(this.func_152681_a(p_152687_1_.func_152640_f()), p_152687_1_);

			try
			{
				this.func_152678_f();
			}
			catch (IOException var3)
			{
				field_152693_a.warn("Could not save the list after adding a user.", var3);
			}
		}

		public virtual UserListEntry func_152683_b(object p_152683_1_)
		{
			this.func_152680_h();
			return(UserListEntry)this.field_152696_d.get(this.func_152681_a(p_152683_1_));
		}

		public virtual void func_152684_c(object p_152684_1_)
		{
			this.field_152696_d.Remove(this.func_152681_a(p_152684_1_));

			try
			{
				this.func_152678_f();
			}
			catch (IOException var3)
			{
				field_152693_a.warn("Could not save the list after removing a user.", var3);
			}
		}

		public virtual string[] func_152685_a()
		{
			return(string[])this.field_152696_d.Keys.ToArray(new string[this.field_152696_d.Count]);
		}

		protected internal virtual string func_152681_a(object p_152681_1_)
		{
			return p_152681_1_.ToString();
		}

		protected internal virtual bool func_152692_d(object p_152692_1_)
		{
			return this.field_152696_d.ContainsKey(this.func_152681_a(p_152692_1_));
		}

		private void func_152680_h()
		{
			ArrayList var1 = Lists.newArrayList();
			IEnumerator var2 = this.field_152696_d.Values.GetEnumerator();

			while(var2.MoveNext())
			{
				UserListEntry var3 = (UserListEntry)var2.Current;

				if(var3.hasBanExpired())
				{
					var1.Add(var3.func_152640_f());
				}
			}

			var2 = var1.GetEnumerator();

			while(var2.MoveNext())
			{
				object var4 = var2.Current;
				this.field_152696_d.Remove(var4);
			}
		}

		protected internal virtual UserListEntry func_152682_a(JsonObject p_152682_1_)
		{
			return new UserListEntry((object)null, p_152682_1_);
		}

		protected internal virtual IDictionary func_152688_e()
		{
			return this.field_152696_d;
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void func_152678_f() throws IOException
		public virtual void func_152678_f()
		{
			ICollection var1 = this.field_152696_d.Values;
			string var2 = this.field_152694_b.toJson(var1);
			BufferedWriter var3 = null;

			try
			{
				var3 = Files.newWriter(this.field_152695_c, Charsets.UTF_8);
				var3.write(var2);
			}
			finally
			{
				IOUtils.closeQuietly(var3);
			}
		}

		internal class Serializer : JsonDeserializer, JsonSerializer
		{
			private const string __OBFID = "CL_00001874";

			private Serializer()
			{
			}

			public virtual JsonElement func_152751_a(UserListEntry p_152751_1_, Type p_152751_2_, JsonSerializationContext p_152751_3_)
			{
				JsonObject var4 = new JsonObject();
				p_152751_1_.func_152641_a(var4);
				return var4;
			}

			public virtual UserListEntry func_152750_a(JsonElement p_152750_1_, Type p_152750_2_, JsonDeserializationContext p_152750_3_)
			{
				if(p_152750_1_.JsonObject)
				{
					JsonObject var4 = p_152750_1_.AsJsonObject;
					UserListEntry var5 = UserList.func_152682_a(var4);
					return var5;
				}
				else
				{
					return null;
				}
			}

			public virtual JsonElement serialize(object p_serialize_1_, Type p_serialize_2_, JsonSerializationContext p_serialize_3_)
			{
				return this.func_152751_a((UserListEntry)p_serialize_1_, p_serialize_2_, p_serialize_3_);
			}

			public virtual object deserialize(JsonElement p_deserialize_1_, Type p_deserialize_2_, JsonDeserializationContext p_deserialize_3_)
			{
				return this.func_152750_a(p_deserialize_1_, p_deserialize_2_, p_deserialize_3_);
			}

			internal Serializer(object p_i1141_2_) : this()
			{
			}
		}
	}

}