using System;

namespace DotCraftCore.nBlock
{

	
	using CreativeTabs = DotCraftCore.nInventory.CreativeTabs;
	using Blocks = DotCraftCore.nInit.Blocks;
	using Items = DotCraftCore.nInit.Items;
	using Item = DotCraftCore.nItem.Item;
	using IIcon = DotCraftCore.nUtil.IIcon;

	public class BlockBookshelf : Block
	{
		

		public BlockBookshelf() : base(Material.wood)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return p_149691_1_ != 1 && p_149691_1_ != 0 ? base.getIcon(p_149691_1_, p_149691_2_) : Blocks.planks.getBlockTextureFromSide(p_149691_1_);
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 3;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.book;
		}
	}

}