using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
namespace DotCraftCore.nBlock
{
	public class BlockCompressed : Block
	{
		private readonly MapColor field_150202_a;
		

		public BlockCompressed(MapColor p_i45414_1_) : base(Material.iron)
		{
			this.field_150202_a = p_i45414_1_;
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public override MapColor getMapColor(int p_149728_1_)
		{
			return this.field_150202_a;
		}
	}

}