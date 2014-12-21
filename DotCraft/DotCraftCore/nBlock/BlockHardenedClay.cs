using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
namespace DotCraftCore.nBlock
{
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