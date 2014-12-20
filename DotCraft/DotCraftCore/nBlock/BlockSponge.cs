using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
namespace DotCraftCore.nBlock
{
	public class BlockSponge : Block
	{
		protected internal BlockSponge() : base(Material.sponge)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}
	}
}