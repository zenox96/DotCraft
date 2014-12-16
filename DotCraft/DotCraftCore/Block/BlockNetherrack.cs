namespace DotCraftCore.Block
{

	using MapColor = DotCraftCore.Block.material.MapColor;
	
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;

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