using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInit;
using DotCraftCore.nItem;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockStone : Block
	{
		public BlockStone() : base(Material.rock)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Item.getItemFromBlock(Blocks.cobblestone);
		}
	}

}
