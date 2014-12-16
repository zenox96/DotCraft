namespace DotCraftCore.Server.Management
{

	using JsonObject = com.google.gson.JsonObject;

	public class UserListEntry
	{
		private readonly object field_152642_a;
		

		public UserListEntry(object p_i1146_1_)
		{
			this.field_152642_a = p_i1146_1_;
		}

		protected internal UserListEntry(object p_i1147_1_, JsonObject p_i1147_2_)
		{
			this.field_152642_a = p_i1147_1_;
		}

		internal virtual object func_152640_f()
		{
			return this.field_152642_a;
		}

		internal virtual bool hasBanExpired()
		{
			return false;
		}

		protected internal virtual void func_152641_a(JsonObject p_152641_1_)
		{
		}
	}

}