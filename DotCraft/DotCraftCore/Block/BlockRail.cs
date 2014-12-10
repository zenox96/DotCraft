namespace DotCraftCore.Block
{

	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using IIcon = DotCraftCore.Util.IIcon;
	using World = DotCraftCore.World.World;

	public class BlockRail : BlockRailBase
	{
		private IIcon field_150056_b;
		private const string __OBFID = "CL_00000293";

		protected internal BlockRail() : base(false)
		{
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return p_149691_2_ >= 6 ? this.field_150056_b : this.blockIcon;
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			base.registerBlockIcons(p_149651_1_);
			this.field_150056_b = p_149651_1_.registerIcon(this.TextureName + "_turned");
		}

		protected internal override void func_150048_a(World p_150048_1_, int p_150048_2_, int p_150048_3_, int p_150048_4_, int p_150048_5_, int p_150048_6_, Block p_150048_7_)
		{
			if (p_150048_7_.canProvidePower() && (new BlockRailBase.Rail(p_150048_1_, p_150048_2_, p_150048_3_, p_150048_4_)).func_150650_a() == 3)
			{
				this.func_150052_a(p_150048_1_, p_150048_2_, p_150048_3_, p_150048_4_, false);
			}
		}
	}

}