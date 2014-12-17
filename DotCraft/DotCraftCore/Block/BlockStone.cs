using System;

namespace DotCraftCore.nBlock
{
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;
	using Blocks = DotCraftCore.nInit.Blocks;
	using Item = DotCraftCore.nItem.Item;

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
