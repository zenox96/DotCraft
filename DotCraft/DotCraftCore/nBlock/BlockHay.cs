using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
namespace DotCraftCore.nBlock
{
	public class BlockHay : BlockRotatedPillar
	{
		public BlockHay() : base(Material.grass)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}
	}
}