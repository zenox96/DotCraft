using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using System;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockStoneSlab : BlockSlab
	{
		public static readonly string[] field_150006_b = new string[] {"stone", "sand", "wood", "cobble", "brick", "smoothStoneBrick", "netherBrick", "quartz"};

		public BlockStoneSlab(bool p_i45431_1_) : base(p_i45431_1_, Material.rock)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Item.getItemFromBlock(Blocks.stone_slab);
		}

///    
///     <summary> * Returns an item stack containing a single instance of the current block type. 'i' is the block's subtype/damage
///     * and is ignored for blocks which do not support subtypes. Blocks which cannot be harvested should return null. </summary>
///     
		protected internal override ItemStack createStackedBlock(int p_149644_1_)
		{
			return new ItemStack(Item.getItemFromBlock(Blocks.stone_slab), 2, p_149644_1_ & 7);
		}

		public override string func_150002_b(int p_150002_1_)
		{
			if (p_150002_1_ < 0 || p_150002_1_ >= field_150006_b.Length)
			{
				p_150002_1_ = 0;
			}

			return base.UnlocalizedName + "." + field_150006_b[p_150002_1_];
		}

		public override void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			if (p_149666_1_ != Item.getItemFromBlock(Blocks.double_stone_slab))
			{
				for (int var4 = 0; var4 <= 7; ++var4)
				{
					if (var4 != 2)
					{
						p_149666_3_.Add(new ItemStack(p_149666_1_, 1, var4));
					}
				}
			}
		}
	}

}