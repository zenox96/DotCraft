namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using IIcon = DotCraftCore.Util.IIcon;

	public class BlockHay : BlockRotatedPillar
	{
		private const string __OBFID = "CL_00000256";

		public BlockHay() : base(Material.grass)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		protected internal override IIcon func_150163_b(int p_150163_1_)
		{
			return this.blockIcon;
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.field_150164_N = p_149651_1_.registerIcon(this.TextureName + "_top");
			this.blockIcon = p_149651_1_.registerIcon(this.TextureName + "_side");
		}
	}

}