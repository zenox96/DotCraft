namespace DotCraftCore.nBlock
{

	using MapColor = DotCraftCore.nBlock.nMaterial.MapColor;
	
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;

	public class BlockNetherrack : Block
	{
		

		public BlockNetherrack() : base(Material.rock)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public virtual MapColor getMapColor(int p_149728_1_)
		{
			return MapColor.field_151655_K;
		}
	}

}