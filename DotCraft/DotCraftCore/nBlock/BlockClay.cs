using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockClay : Block
	{
		public BlockClay() : base(Material.field_151571_B)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.clay_ball;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public override int quantityDropped(Random p_149745_1_)
		{
			return 4;
		}
	}

}