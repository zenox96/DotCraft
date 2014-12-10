using System;

namespace DotCraftCore.Server.Management
{

	using JsonObject = com.google.gson.JsonObject;
	using GameProfile = com.mojang.authlib.GameProfile;

	public class UserListBansEntry : BanEntry
	{
		private const string __OBFID = "CL_00001872";

		public UserListBansEntry(GameProfile p_i1134_1_) : this(p_i1134_1_, (DateTime)null, (string)null, (DateTime)null, (string)null)
		{
		}

		public UserListBansEntry(GameProfile p_i1135_1_, DateTime p_i1135_2_, string p_i1135_3_, DateTime p_i1135_4_, string p_i1135_5_) : base(p_i1135_1_, p_i1135_4_, p_i1135_3_, p_i1135_4_, p_i1135_5_)
		{
		}

		public UserListBansEntry(JsonObject p_i1136_1_) : base(func_152648_b(p_i1136_1_), p_i1136_1_)
		{
		}

		protected internal override void func_152641_a(JsonObject p_152641_1_)
		{
			if(this.func_152640_f() != null)
			{
				p_152641_1_.addProperty("uuid", ((GameProfile)this.func_152640_f()).Id == null ? "" : ((GameProfile)this.func_152640_f()).Id.ToString());
				p_152641_1_.addProperty("name", ((GameProfile)this.func_152640_f()).Name);
				base.func_152641_a(p_152641_1_);
			}
		}

		private static GameProfile func_152648_b(JsonObject p_152648_0_)
		{
			if(p_152648_0_.has("uuid") && p_152648_0_.has("name"))
			{
				string var1 = p_152648_0_.get("uuid").AsString;
				UUID var2;

				try
				{
					var2 = UUID.fromString(var1);
				}
				catch (Exception var4)
				{
					return null;
				}

				return new GameProfile(var2, p_152648_0_.get("name").AsString);
			}
			else
			{
				return null;
			}
		}
	}

}