using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nUtil;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockGlowstone : Block
	{
		public BlockGlowstone(Material p_i45409_1_) : base(p_i45409_1_)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Returns the usual quantity dropped by the block plus a bonus of 1 to 'i' (inclusive). </summary>
///     
		public override int quantityDroppedWithBonus(int p_149679_1_, Random p_149679_2_)
		{
			return MathHelper.clamp_int(this.quantityDropped(p_149679_2_) + p_149679_2_.Next(p_149679_1_ + 1), 1, 4);
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public override int quantityDropped(Random p_149745_1_)
		{
			return 2 + p_149745_1_.Next(3);
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.glowstone_dust;
		}

		public override MapColor getMapColor(int p_149728_1_)
		{
			return MapColor.field_151658_d;
		}
	}

}