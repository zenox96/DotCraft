using System;

namespace DotCraftCore.nServer.nManagement
{

	using JsonObject = com.google.gson.JsonObject;

	public abstract class BanEntry : UserListEntry
	{
		public static readonly SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss Z");
		protected internal readonly DateTime banStartDate;
		protected internal readonly string bannedBy;
		protected internal readonly DateTime banEndDate;
		protected internal readonly string reason;
		

		public BanEntry(object p_i1173_1_, DateTime p_i1173_2_, string p_i1173_3_, DateTime p_i1173_4_, string p_i1173_5_) : base(p_i1173_1_)
		{
			this.banStartDate = p_i1173_2_ == null ? DateTime.Now : p_i1173_2_;
			this.bannedBy = p_i1173_3_ == null ? "(Unknown)" : p_i1173_3_;
			this.banEndDate = p_i1173_4_;
			this.reason = p_i1173_5_ == null ? "Banned by an operator." : p_i1173_5_;
		}

		protected internal BanEntry(object p_i1174_1_, JsonObject p_i1174_2_) : base(p_i1174_1_, p_i1174_2_)
		{
			DateTime var3;

			try
			{
				var3 = p_i1174_2_.has("created") ? dateFormat.parse(p_i1174_2_.get("created").AsString) : DateTime.Now;
			}
			catch (ParseException var7)
			{
				var3 = DateTime.Now;
			}

			this.banStartDate = var3;
			this.bannedBy = p_i1174_2_.has("source") ? p_i1174_2_.get("source").AsString : "(Unknown)";
			DateTime var4;

			try
			{
				var4 = p_i1174_2_.has("expires") ? dateFormat.parse(p_i1174_2_.get("expires").AsString) : null;
			}
			catch (ParseException var6)
			{
				var4 = null;
			}

			this.banEndDate = var4;
			this.reason = p_i1174_2_.has("reason") ? p_i1174_2_.get("reason").AsString : "Banned by an operator.";
		}

		public virtual DateTime BanEndDate
		{
			get
			{
				return this.banEndDate;
			}
		}

		public virtual string BanReason
		{
			get
			{
				return this.reason;
			}
		}

		internal override bool hasBanExpired()
		{
			return this.banEndDate == null ? false : this.banEndDate.before(DateTime.Now);
		}

		protected internal override void func_152641_a(JsonObject p_152641_1_)
		{
			p_152641_1_.addProperty("created", dateFormat.format(this.banStartDate));
			p_152641_1_.addProperty("source", this.bannedBy);
			p_152641_1_.addProperty("expires", this.banEndDate == null ? "forever" : dateFormat.format(this.banEndDate));
			p_152641_1_.addProperty("reason", this.reason);
		}
	}

}