namespace DotCraftCore.Block
{

	using MapColor = DotCraftCore.Block.material.MapColor;
	
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;

	public class BlockCompressed : Block
	{
		private readonly MapColor field_150202_a;
		private const string __OBFID = "CL_00000268";

		public BlockCompressed(MapColor p_i45414_1_) : base(Material.iron)
		{
			this.field_150202_a = p_i45414_1_;
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public virtual MapColor getMapColor(int p_149728_1_)
		{
			return this.field_150202_a;
		}
	}

}