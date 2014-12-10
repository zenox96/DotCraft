namespace DotCraftCore.Block
{

	
	using IBlockAccess = DotCraftCore.world.IBlockAccess;

	public class BlockLeavesBase : Block
	{
		protected internal bool field_150121_P;
		private const string __OBFID = "CL_00000326";

		protected internal BlockLeavesBase(Material p_i45433_1_, bool p_i45433_2_) : base(p_i45433_1_)
		{
			this.field_150121_P = p_i45433_2_;
		}

		public virtual bool isOpaqueCube()
		{
			get
			{
				return false;
			}
		}

		public virtual bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
		{
			Block var6 = p_149646_1_.getBlock(p_149646_2_, p_149646_3_, p_149646_4_);
			return !this.field_150121_P && var6 == this ? false : base.shouldSideBeRendered(p_149646_1_, p_149646_2_, p_149646_3_, p_149646_4_, p_149646_5_);
		}
	}

}