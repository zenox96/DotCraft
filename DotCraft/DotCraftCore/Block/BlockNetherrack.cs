namespace DotCraftCore.Block
{

	using MapColor = DotCraftCore.Block.material.MapColor;
	
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;

	public class BlockNetherrack : Block
	{
		private const string __OBFID = "CL_00000275";

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