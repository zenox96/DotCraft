using System;

namespace DotCraftCore.nServer.nManagement
{

	using JsonObject = com.google.gson.JsonObject;
	using GameProfile = com.mojang.authlib.GameProfile;

	public class UserListOpsEntry : UserListEntry
	{
		private readonly int field_152645_a;
		

		public UserListOpsEntry(GameProfile p_i1149_1_, int p_i1149_2_) : base(p_i1149_1_)
		{
			this.field_152645_a = p_i1149_2_;
		}

		public UserListOpsEntry(JsonObject p_i1150_1_) : base(func_152643_b(p_i1150_1_), p_i1150_1_)
		{
			this.field_152645_a = p_i1150_1_.has("level") ? p_i1150_1_.get("level").AsInt : 0;
		}

		public virtual int func_152644_a()
		{
			return this.field_152645_a;
		}

		protected internal override void func_152641_a(JsonObject p_152641_1_)
		{
			if(this.func_152640_f() != null)
			{
				p_152641_1_.addProperty("uuid", ((GameProfile)this.func_152640_f()).Id == null ? "" : ((GameProfile)this.func_152640_f()).Id.ToString());
				p_152641_1_.addProperty("name", ((GameProfile)this.func_152640_f()).Name);
				base.func_152641_a(p_152641_1_);
				p_152641_1_.addProperty("level", Convert.ToInt32(this.field_152645_a));
			}
		}

		private static GameProfile func_152643_b(JsonObject p_152643_0_)
		{
			if(p_152643_0_.has("uuid") && p_152643_0_.has("name"))
			{
				string var1 = p_152643_0_.get("uuid").AsString;
				UUID var2;

				try
				{
					var2 = UUID.fromString(var1);
				}
				catch (Exception var4)
				{
					return null;
				}

				return new GameProfile(var2, p_152643_0_.get("name").AsString);
			}
			else
			{
				return null;
			}
		}
	}

}