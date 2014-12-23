using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nUtil;
using System;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockWoodSlab : BlockSlab
	{
		public static readonly string[] field_150005_b = new string[] {"oak", "spruce", "birch", "jungle", "acacia", "big_oak"};

		public BlockWoodSlab(bool p_i45437_1_) : base(p_i45437_1_, Material.wood)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Item.getItemFromBlock(Blocks.wooden_slab);
		}

///    
///     <summary> * Returns an item stack containing a single instance of the current block type. 'i' is the block's subtype/damage
///     * and is ignored for blocks which do not support subtypes. Blocks which cannot be harvested should return null. </summary>
///     
		protected internal override ItemStack createStackedBlock(int p_149644_1_)
		{
			return new ItemStack(Item.getItemFromBlock(Blocks.wooden_slab), 2, p_149644_1_ & 7);
		}

		public override string func_150002_b(int p_150002_1_)
		{
			if (p_150002_1_ < 0 || p_150002_1_ >= field_150005_b.Length)
			{
				p_150002_1_ = 0;
			}

			return base.UnlocalizedName + "." + field_150005_b[p_150002_1_];
		}

		public override void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			if (p_149666_1_ != Item.getItemFromBlock(Blocks.double_wooden_slab))
			{
				for (int var4 = 0; var4 < field_150005_b.Length; ++var4)
				{
					p_149666_3_.Add(new ItemStack(p_149666_1_, 1, var4));
				}
			}
		}
	}
}