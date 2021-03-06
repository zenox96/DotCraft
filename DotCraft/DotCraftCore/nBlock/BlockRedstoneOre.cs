using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nInit;
using DotCraftCore.nItem;
using DotCraftCore.nWorld;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockRedstoneOre : Block
	{
		private bool field_150187_a;
		

		public BlockRedstoneOre(bool p_i45420_1_) : base(Material.rock)
		{

			if (p_i45420_1_)
			{
				this.TickRandomly = true;
			}

			this.field_150187_a = p_i45420_1_;
		}

		public override int func_149738_a(World p_149738_1_)
		{
			return 30;
		}

///    
///     <summary> * Called when a player hits the block. Args: world, x, y, z, player </summary>
///     
		public override void onBlockClicked(World p_149699_1_, int p_149699_2_, int p_149699_3_, int p_149699_4_, EntityPlayer p_149699_5_)
		{
			this.func_150185_e(p_149699_1_, p_149699_2_, p_149699_3_, p_149699_4_);
			base.onBlockClicked(p_149699_1_, p_149699_2_, p_149699_3_, p_149699_4_, p_149699_5_);
		}

		public override void onEntityWalking(World p_149724_1_, int p_149724_2_, int p_149724_3_, int p_149724_4_, Entity p_149724_5_)
		{
			this.func_150185_e(p_149724_1_, p_149724_2_, p_149724_3_, p_149724_4_);
			base.onEntityWalking(p_149724_1_, p_149724_2_, p_149724_3_, p_149724_4_, p_149724_5_);
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public override bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			this.func_150185_e(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_);
			return base.onBlockActivated(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_, p_149727_5_, p_149727_6_, p_149727_7_, p_149727_8_, p_149727_9_);
		}

		private void func_150185_e(World p_150185_1_, int p_150185_2_, int p_150185_3_, int p_150185_4_)
		{
			this.func_150186_m(p_150185_1_, p_150185_2_, p_150185_3_, p_150185_4_);

			if (this == Blocks.redstone_ore)
			{
				p_150185_1_.setBlock(p_150185_2_, p_150185_3_, p_150185_4_, Blocks.lit_redstone_ore);
			}
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public override void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			if (this == Blocks.lit_redstone_ore)
			{
				p_149674_1_.setBlock(p_149674_2_, p_149674_3_, p_149674_4_, Blocks.redstone_ore);
			}
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Items.redstone;
		}

///    
///     <summary> * Returns the usual quantity dropped by the block plus a bonus of 1 to 'i' (inclusive). </summary>
///     
		public override int quantityDroppedWithBonus(int p_149679_1_, Random p_149679_2_)
		{
			return this.quantityDropped(p_149679_2_) + p_149679_2_.Next(p_149679_1_ + 1);
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public override int quantityDropped(Random p_149745_1_)
		{
			return 4 + p_149745_1_.Next(2);
		}

///    
///     <summary> * Drops the block items with a specified chance of dropping the specified items </summary>
///     
		public override void dropBlockAsItemWithChance(World p_149690_1_, int p_149690_2_, int p_149690_3_, int p_149690_4_, int p_149690_5_, float p_149690_6_, int p_149690_7_)
		{
			base.dropBlockAsItemWithChance(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, p_149690_5_, p_149690_6_, p_149690_7_);

			if (this.getItemDropped(p_149690_5_, p_149690_1_.rand, p_149690_7_) != Item.getItemFromBlock(this))
			{
				int var8 = 1 + p_149690_1_.rand.Next(5);
				this.dropXpOnBlockBreak(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, var8);
			}
		}

///    
///     <summary> * A randomly called display update to be able to add particles or other items for display </summary>
///     
		public override void randomDisplayTick(World p_149734_1_, int p_149734_2_, int p_149734_3_, int p_149734_4_, Random p_149734_5_)
		{
			if (this.field_150187_a)
			{
				this.func_150186_m(p_149734_1_, p_149734_2_, p_149734_3_, p_149734_4_);
			}
		}

		private void func_150186_m(World p_150186_1_, int p_150186_2_, int p_150186_3_, int p_150186_4_)
		{
			Random var5 = p_150186_1_.rand;
			double var6 = 0.0625D;

			for (int var8 = 0; var8 < 6; ++var8)
			{
				double var9 = ((double)p_150186_2_ + var5.NextDouble());
				double var11 = ((double)p_150186_3_ + var5.NextDouble());
				double var13 = ((double)p_150186_4_ + var5.NextDouble());

				if (var8 == 0 && !p_150186_1_.getBlock(p_150186_2_, p_150186_3_ + 1, p_150186_4_).OpaqueCube)
				{
					var11 = (double)(p_150186_3_ + 1) + var6;
				}

				if (var8 == 1 && !p_150186_1_.getBlock(p_150186_2_, p_150186_3_ - 1, p_150186_4_).OpaqueCube)
				{
					var11 = (double)(p_150186_3_ + 0) - var6;
				}

				if (var8 == 2 && !p_150186_1_.getBlock(p_150186_2_, p_150186_3_, p_150186_4_ + 1).OpaqueCube)
				{
					var13 = (double)(p_150186_4_ + 1) + var6;
				}

				if (var8 == 3 && !p_150186_1_.getBlock(p_150186_2_, p_150186_3_, p_150186_4_ - 1).OpaqueCube)
				{
					var13 = (double)(p_150186_4_ + 0) - var6;
				}

				if (var8 == 4 && !p_150186_1_.getBlock(p_150186_2_ + 1, p_150186_3_, p_150186_4_).OpaqueCube)
				{
					var9 = (double)(p_150186_2_ + 1) + var6;
				}

				if (var8 == 5 && !p_150186_1_.getBlock(p_150186_2_ - 1, p_150186_3_, p_150186_4_).OpaqueCube)
				{
					var9 = (double)(p_150186_2_ + 0) - var6;
				}

				if (var9 < (double)p_150186_2_ || var9 > (double)(p_150186_2_ + 1) || var11 < 0.0D || var11 > (double)(p_150186_3_ + 1) || var13 < (double)p_150186_4_ || var13 > (double)(p_150186_4_ + 1))
				{
					p_150186_1_.spawnParticle("reddust", var9, var11, var13, 0.0D, 0.0D, 0.0D);
				}
			}
		}

///    
///     <summary> * Returns an item stack containing a single instance of the current block type. 'i' is the block's subtype/damage
///     * and is ignored for blocks which do not support subtypes. Blocks which cannot be harvested should return null. </summary>
///     
		protected internal override ItemStack createStackedBlock(int p_149644_1_)
		{
			return new ItemStack(Blocks.redstone_ore);
		}
	}

}