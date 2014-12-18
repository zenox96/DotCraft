namespace DotCraftCore.nServer.nManagement
{

	using JsonObject = com.google.gson.JsonObject;

	public class BanList : UserList
	{
		

		public BanList(File p_i1490_1_) : base(p_i1490_1_)
		{
		}

		protected internal override UserListEntry func_152682_a(JsonObject p_152682_1_)
		{
			return new IPBanEntry(p_152682_1_);
		}

		public virtual bool func_152708_a(SocketAddress p_152708_1_)
		{
			string var2 = this.func_152707_c(p_152708_1_);
			return this.func_152692_d(var2);
		}

		public virtual IPBanEntry func_152709_b(SocketAddress p_152709_1_)
		{
			string var2 = this.func_152707_c(p_152709_1_);
			return(IPBanEntry)this.func_152683_b(var2);
		}

		private string func_152707_c(SocketAddress p_152707_1_)
		{
			string var2 = p_152707_1_.ToString();

			if(var2.Contains("/"))
			{
				var2 = var2.Substring(var2.IndexOf(47) + 1);
			}

			if(var2.Contains(":"))
			{
				var2 = var2.Substring(0, var2.IndexOf(58));
			}

			return var2;
		}
	}

}