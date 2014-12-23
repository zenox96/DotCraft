using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockColored : Block
	{
		public BlockColored(Material p_i45398_1_) : base(p_i45398_1_)
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

		public static int func_150032_b(int p_150032_0_)
		{
			return func_150031_c(p_150032_0_);
		}

		public static int func_150031_c(int p_150031_0_)
		{
			return ~p_150031_0_ & 15;
		}

		public override void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			for (int var4 = 0; var4 < 16; ++var4)
			{
				p_149666_3_.Add(new ItemStack(p_149666_1_, 1, var4));
			}
		}

		public override MapColor getMapColor(int p_149728_1_)
		{
			return MapColor.func_151644_a(p_149728_1_);
		}
	}

}