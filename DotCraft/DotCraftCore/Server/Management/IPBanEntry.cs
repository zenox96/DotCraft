using System;

namespace DotCraftCore.Server.Management
{

	using JsonObject = com.google.gson.JsonObject;

	public class IPBanEntry : BanEntry
	{
		

		public IPBanEntry(string p_i1158_1_) : this(p_i1158_1_, (DateTime)null, (string)null, (DateTime)null, (string)null)
		{
		}

		public IPBanEntry(string p_i1159_1_, DateTime p_i1159_2_, string p_i1159_3_, DateTime p_i1159_4_, string p_i1159_5_) : base(p_i1159_1_, p_i1159_2_, p_i1159_3_, p_i1159_4_, p_i1159_5_)
		{
		}

		public IPBanEntry(JsonObject p_i1160_1_) : base(func_152647_b(p_i1160_1_), p_i1160_1_)
		{
		}

		private static string func_152647_b(JsonObject p_152647_0_)
		{
			return p_152647_0_.has("ip") ? p_152647_0_.get("ip").AsString : null;
		}

		protected internal override void func_152641_a(JsonObject p_152641_1_)
		{
			if(this.func_152640_f() != null)
			{
				p_152641_1_.addProperty("ip", (string)this.func_152640_f());
				base.func_152641_a(p_152641_1_);
			}
		}
	}

}