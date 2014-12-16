using System;

namespace DotCraftCore.Server.Management
{

	using JsonObject = com.google.gson.JsonObject;
	using GameProfile = com.mojang.authlib.GameProfile;

	public class UserListWhitelistEntry : UserListEntry
	{
		

		public UserListWhitelistEntry(GameProfile p_i1129_1_) : base(p_i1129_1_)
		{
		}

		public UserListWhitelistEntry(JsonObject p_i1130_1_) : base(func_152646_b(p_i1130_1_), p_i1130_1_)
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

		private static GameProfile func_152646_b(JsonObject p_152646_0_)
		{
			if(p_152646_0_.has("uuid") && p_152646_0_.has("name"))
			{
				string var1 = p_152646_0_.get("uuid").AsString;
				UUID var2;

				try
				{
					var2 = UUID.fromString(var1);
				}
				catch (Exception var4)
				{
					return null;
				}

				return new GameProfile(var2, p_152646_0_.get("name").AsString);
			}
			else
			{
				return null;
			}
		}
	}

}