namespace DotCraftCore.nStats
{

	using Item = DotCraftCore.item.Item;
	using IChatComponent = DotCraftCore.nUtil.IChatComponent;

	public class StatCrafting : StatBase
	{
		private readonly Item field_150960_a;
		

		public StatCrafting(string p_i45305_1_, IChatComponent p_i45305_2_, Item p_i45305_3_) : base(p_i45305_1_, p_i45305_2_)
		{
			this.field_150960_a = p_i45305_3_;
		}

		public virtual Item func_150959_a()
		{
			return this.field_150960_a;
		}
	}

}