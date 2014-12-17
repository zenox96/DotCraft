namespace DotCraftCore.nBlock
{

	using MapColor = DotCraftCore.nBlock.nMaterial.MapColor;
	
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;

	public class BlockHardenedClay : Block
	{
		

		public BlockHardenedClay() : base(Material.rock)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public virtual MapColor getMapColor(int p_149728_1_)
		{
			return MapColor.field_151676_q;
		}
	}

}