using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nStats;
using DotCraftCore.nWorld;
using System;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockTallGrass : BlockBush, IGrowable
	{
		private static readonly string[] field_149871_a = new string[] {"deadbush", "tallgrass", "fern"};

		protected internal BlockTallGrass() : base(Material.vine)
		{
			float var1 = 0.4F;
			this.setBlockBounds(0.5F - var1, 0.0F, 0.5F - var1, 0.5F + var1, 0.8F, 0.5F + var1);
		}

///    
///     <summary> * Can this block stay at this position.  Similar to canPlaceBlockAt except gets checked often with plants. </summary>
///     
		public override bool canBlockStay(World p_149718_1_, int p_149718_2_, int p_149718_3_, int p_149718_4_)
		{
			return this.func_149854_a(p_149718_1_.getBlock(p_149718_2_, p_149718_3_ - 1, p_149718_4_));
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return p_149650_2_.Next(8) == 0 ? Items.wheat_seeds : null;
		}

///    
///     <summary> * Returns the usual quantity dropped by the block plus a bonus of 1 to 'i' (inclusive). </summary>
///     
		public override int quantityDroppedWithBonus(int p_149679_1_, Random p_149679_2_)
		{
			return 1 + p_149679_2_.Next(p_149679_1_ * 2 + 1);
		}

		public override void harvestBlock(World p_149636_1_, EntityPlayer p_149636_2_, int p_149636_3_, int p_149636_4_, int p_149636_5_, int p_149636_6_)
		{
			if (!p_149636_1_.isClient && p_149636_2_.CurrentEquippedItem != null && p_149636_2_.CurrentEquippedItem.Item == Items.shears)
			{
				p_149636_2_.addStat(StatList.mineBlockStatArray[Block.getIdFromBlock(this)], 1);
				this.dropBlockAsItem_do(p_149636_1_, p_149636_3_, p_149636_4_, p_149636_5_, new ItemStack(Blocks.tallgrass, 1, p_149636_6_));
			}
			else
			{
				base.harvestBlock(p_149636_1_, p_149636_2_, p_149636_3_, p_149636_4_, p_149636_5_, p_149636_6_);
			}
		}

///    
///     <summary> * Get the block's damage value (for use with pick block). </summary>
///     
		public override int getDamageValue(World p_149643_1_, int p_149643_2_, int p_149643_3_, int p_149643_4_)
		{
			return p_149643_1_.getBlockMetadata(p_149643_2_, p_149643_3_, p_149643_4_);
		}

		public override void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			for (int var4 = 1; var4 < 3; ++var4)
			{
				p_149666_3_.Add(new ItemStack(p_149666_1_, 1, var4));
			}
		}

		public override bool func_149851_a(World p_149851_1_, int p_149851_2_, int p_149851_3_, int p_149851_4_, bool p_149851_5_)
		{
			int var6 = p_149851_1_.getBlockMetadata(p_149851_2_, p_149851_3_, p_149851_4_);
			return var6 != 0;
		}

		public override bool func_149852_a(World p_149852_1_, Random p_149852_2_, int p_149852_3_, int p_149852_4_, int p_149852_5_)
		{
			return true;
		}

		public override void func_149853_b(World p_149853_1_, Random p_149853_2_, int p_149853_3_, int p_149853_4_, int p_149853_5_)
		{
			int var6 = p_149853_1_.getBlockMetadata(p_149853_3_, p_149853_4_, p_149853_5_);
			sbyte var7 = 2;

			if (var6 == 2)
			{
				var7 = 3;
			}

			if (Blocks.double_plant.canPlaceBlockAt(p_149853_1_, p_149853_3_, p_149853_4_, p_149853_5_))
			{
				Blocks.double_plant.func_149889_c(p_149853_1_, p_149853_3_, p_149853_4_, p_149853_5_, var7, 2);
			}
		}
	}

}