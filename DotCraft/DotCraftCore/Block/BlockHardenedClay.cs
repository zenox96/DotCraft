namespace DotCraftCore.Block
{

	using MapColor = DotCraftCore.Block.material.MapColor;
	
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;

	public class BlockHardenedClay : Block
	{
		private const string __OBFID = "CL_00000255";

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