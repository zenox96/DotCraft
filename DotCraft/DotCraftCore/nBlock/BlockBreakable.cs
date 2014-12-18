namespace DotCraftCore.nBlock
{
    using DotCraftCore.nBlock.nMaterial;
    using DotCraftCore.nInit;
    using DotCraftCore.nUtil;
    using DotCraftCore.nWorld;

	public class BlockBreakable : Block
	{
		private bool field_149996_a;
		private string field_149995_b;
		

		protected internal BlockBreakable(string p_i45411_1_, Material p_i45411_2_, bool p_i45411_3_) : base(p_i45411_2_)
		{
			this.field_149996_a = p_i45411_3_;
			this.field_149995_b = p_i45411_1_;
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

			if (this == Blocks.glass || this == Blocks.stained_glass)
			{
				if (p_149646_1_.getBlockMetadata(p_149646_2_, p_149646_3_, p_149646_4_) != p_149646_1_.getBlockMetadata(p_149646_2_ - Facing.offsetsXForSide[p_149646_5_], p_149646_3_ - Facing.offsetsYForSide[p_149646_5_], p_149646_4_ - Facing.offsetsZForSide[p_149646_5_]))
				{
					return true;
				}

				if (var6 == this)
				{
					return false;
				}
			}

			return !this.field_149996_a && var6 == this ? false : base.shouldSideBeRendered(p_149646_1_, p_149646_2_, p_149646_3_, p_149646_4_, p_149646_5_);
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.blockIcon = p_149651_1_.registerIcon(this.field_149995_b);
		}
	}

}