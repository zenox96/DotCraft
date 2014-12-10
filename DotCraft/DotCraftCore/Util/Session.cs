using System;

namespace DotCraftCore.Util
{

	using Maps = com.google.common.collect.Maps;
	using GameProfile = com.mojang.authlib.GameProfile;
	using UUIDTypeAdapter = com.mojang.util.UUIDTypeAdapter;

	public class Session
	{
		private readonly string username;
		private readonly string playerID;
		private readonly string token;
		private readonly Session.Type field_152429_d;
		private const string __OBFID = "CL_00000659";

		public Session(string p_i1098_1_, string p_i1098_2_, string p_i1098_3_, string p_i1098_4_)
		{
			this.username = p_i1098_1_;
			this.playerID = p_i1098_2_;
			this.token = p_i1098_3_;
			this.field_152429_d = Session.Type.func_152421_a(p_i1098_4_);
		}

		public virtual string SessionID
		{
			get
			{
				return "token:" + this.token + ":" + this.playerID;
			}
		}

		public virtual string PlayerID
		{
			get
			{
				return this.playerID;
			}
		}

		public virtual string Username
		{
			get
			{
				return this.username;
			}
		}

		public virtual string Token
		{
			get
			{
				return this.token;
			}
		}

		public virtual GameProfile func_148256_e()
		{
			try
			{
				UUID var1 = UUIDTypeAdapter.fromString(this.PlayerID);
				return new GameProfile(var1, this.Username);
			}
			catch (System.ArgumentException var2)
			{
				return new GameProfile((UUID)null, this.Username);
			}
		}

		public virtual Session.Type func_152428_f()
		{
			return this.field_152429_d;
		}

		public enum Type
		{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			LEGACY("LEGACY", 0, "legacy"),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			MOJANG("MOJANG", 1, "mojang");
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private static final Map field_152425_c = Maps.newHashMap();
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private final String field_152426_d;

			@private static final Session.Type[] $VALUES = new Session.Type[]{LEGACY, MOJANG
		}
			private const string __OBFID = "CL_00001851";

			private Type(string p_i1096_1_, int p_i1096_2_, string p_i1096_3_)
			{
				this.field_152426_d = p_i1096_3_;
			}

			public static Session.Type func_152421_a(string p_152421_0_)
			{
				return(Session.Type)field_152425_c.get(p_152421_0_.ToLower());
			}

			static Session()
			{
				Session.Type[] var0 = values();
				int var1 = var0.Length;

				for(int var2 = 0; var2 < var1; ++var2)
				{
					Session.Type var3 = var0[var2];
					field_152425_c.put(var3.field_152426_d, var3);
				}
			}
		}
	}

}