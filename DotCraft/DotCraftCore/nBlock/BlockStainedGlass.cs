using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using System;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockStainedGlass : BlockBreakable
	{
		public BlockStainedGlass(Material p_i45427_1_) : base("glass", p_i45427_1_, false)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
        public override int damageDropped(int p_149692_1_)
		{
			return p_149692_1_;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
        public override int quantityDropped(Random p_149745_1_)
		{
			return 0;
		}

        protected internal override bool canSilkHarvest( )
		{
			return true;
		}

		public override bool renderAsNormalBlock()
		{
			return false;
		}
	}

}