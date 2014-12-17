namespace DotCraftCore.nBlock
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;
	using IIcon = DotCraftCore.nUtil.IIcon;

	public class BlockHay : BlockRotatedPillar
	{
		

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