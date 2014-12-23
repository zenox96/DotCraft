using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
namespace DotCraftCore.nBlock
{
	public class BlockNetherrack : Block
	{
		public BlockNetherrack() : base(Material.rock)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public override MapColor getMapColor(int p_149728_1_)
		{
			return MapColor.field_151655_K;
		}
	}

}