using System.Collections;

namespace DotCraftCore.Server.Management
{

	using JsonObject = com.google.gson.JsonObject;
	using GameProfile = com.mojang.authlib.GameProfile;

	public class UserListWhitelist : UserList
	{
		private const string __OBFID = "CL_00001871";

		public UserListWhitelist(File p_i1132_1_) : base(p_i1132_1_)
		{
		}

		protected internal override UserListEntry func_152682_a(JsonObject p_152682_1_)
		{
			return new UserListWhitelistEntry(p_152682_1_);
		}

		public override string[] func_152685_a()
		{
			string[] var1 = new string[this.func_152688_e().Count];
			int var2 = 0;
			UserListWhitelistEntry var4;

			for(IEnumerator var3 = this.func_152688_e().Values.GetEnumerator(); var3.MoveNext(); var1[var2++] = ((GameProfile)var4.func_152640_f()).Name)
			{
				var4 = (UserListWhitelistEntry)var3.Current;
			}

			return var1;
		}

		protected internal virtual string func_152704_b(GameProfile p_152704_1_)
		{
			return p_152704_1_.Id.ToString();
		}

		public virtual GameProfile func_152706_a(string p_152706_1_)
		{
			IEnumerator var2 = this.func_152688_e().Values.GetEnumerator();
			UserListWhitelistEntry var3;

			do
			{
				if(!var2.MoveNext())
				{
					return null;
				}

				var3 = (UserListWhitelistEntry)var2.Current;
			}
			while(!p_152706_1_.equalsIgnoreCase(((GameProfile)var3.func_152640_f()).Name));

			return(GameProfile)var3.func_152640_f();
		}

		protected internal override string func_152681_a(object p_152681_1_)
		{
			return this.func_152704_b((GameProfile)p_152681_1_);
		}
	}

}