using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nWorld;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockNewLeaf : BlockLeaves
	{
		public static readonly string[,] field_150132_N = new string[,] { {"leaves_acacia", "leaves_big_oak"}, {"leaves_acacia_opaque", "leaves_big_oak_opaque"}};
		public static readonly string[] field_150133_O = new string[] {"acacia", "big_oak"};
		
		protected internal override void func_150124_c(World p_150124_1_, int p_150124_2_, int p_150124_3_, int p_150124_4_, int p_150124_5_, int p_150124_6_)
		{
			if ((p_150124_5_ & 3) == 1 && p_150124_1_.rand.Next(p_150124_6_) == 0)
			{
				this.dropBlockAsItem_do(p_150124_1_, p_150124_2_, p_150124_3_, p_150124_4_, new ItemStack(Items.apple, 1, 0));
			}
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public override int damageDropped(int p_149692_1_)
		{
			return base.damageDropped(p_149692_1_) + 4;
		}

///    
///     <summary> * Get the block's damage value (for use with pick block). </summary>
///     
		public override int getDamageValue(World p_149643_1_, int p_149643_2_, int p_149643_3_, int p_149643_4_)
		{
			return p_149643_1_.getBlockMetadata(p_149643_2_, p_149643_3_, p_149643_4_) & 3;
		}

		public override void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 0));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 1));
		}

		public override string[] func_150125_e()
		{
			return field_150133_O;
		}
	}
}