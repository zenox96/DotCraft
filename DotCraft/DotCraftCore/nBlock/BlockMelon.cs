using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nUtil;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockMelon : Block
	{
		protected internal BlockMelon() : base(Material.field_151572_C)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.melon;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 3 + p_149745_1_.Next(5);
		}

///    
///     <summary> * Returns the usual quantity dropped by the block plus a bonus of 1 to 'i' (inclusive). </summary>
///     
		public virtual int quantityDroppedWithBonus(int p_149679_1_, Random p_149679_2_)
		{
			int var3 = this.quantityDropped(p_149679_2_) + p_149679_2_.Next(1 + p_149679_1_);

			if (var3 > 9)
			{
				var3 = 9;
			}

			return var3;
		}
	}
}