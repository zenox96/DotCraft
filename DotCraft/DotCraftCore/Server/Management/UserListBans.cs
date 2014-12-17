using System.Collections;

namespace DotCraftCore.nServer.nManagement
{

	using JsonObject = com.google.gson.JsonObject;
	using GameProfile = com.mojang.authlib.GameProfile;

	public class UserListBans : UserList
	{
		

		public UserListBans(File p_i1138_1_) : base(p_i1138_1_)
		{
		}

		protected internal override UserListEntry func_152682_a(JsonObject p_152682_1_)
		{
			return new UserListBansEntry(p_152682_1_);
		}

		public virtual bool func_152702_a(GameProfile p_152702_1_)
		{
			return this.func_152692_d(p_152702_1_);
		}

		public override string[] func_152685_a()
		{
			string[] var1 = new string[this.func_152688_e().Count];
			int var2 = 0;
			UserListBansEntry var4;

			for(IEnumerator var3 = this.func_152688_e().Values.GetEnumerator(); var3.MoveNext(); var1[var2++] = ((GameProfile)var4.func_152640_f()).Name)
			{
				var4 = (UserListBansEntry)var3.Current;
			}

			return var1;
		}

		protected internal virtual string func_152701_b(GameProfile p_152701_1_)
		{
			return p_152701_1_.Id.ToString();
		}

		public virtual GameProfile func_152703_a(string p_152703_1_)
		{
			IEnumerator var2 = this.func_152688_e().Values.GetEnumerator();
			UserListBansEntry var3;

			do
			{
				if(!var2.MoveNext())
				{
					return null;
				}

				var3 = (UserListBansEntry)var2.Current;
			}
			while(!p_152703_1_.equalsIgnoreCase(((GameProfile)var3.func_152640_f()).Name));

			return(GameProfile)var3.func_152640_f();
		}

		protected internal override string func_152681_a(object p_152681_1_)
		{
			return this.func_152701_b((GameProfile)p_152681_1_);
		}
	}

}