using System;

namespace DotCraftCore.Block
{

	
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using StatList = DotCraftCore.stats.StatList;
	using IIcon = DotCraftCore.util.IIcon;
	using ColorizerFoliage = DotCraftCore.world.ColorizerFoliage;
	using IBlockAccess = DotCraftCore.world.IBlockAccess;
	using World = DotCraftCore.world.World;

	public abstract class BlockLeaves : BlockLeavesBase
	{
		internal int[] field_150128_a;
		protected internal int field_150127_b;
		protected internal IIcon[][] field_150129_M = new IIcon[2][];
		private const string __OBFID = "CL_00000263";

		public BlockLeaves() : base(Material.leaves, false)
		{
			this.TickRandomly = true;
			this.CreativeTab = CreativeTabs.tabDecorations;
			this.Hardness = 0.2F;
			this.LightOpacity = 1;
			this.StepSound = soundTypeGrass;
		}

		public virtual int BlockColor
		{
			get
			{
				double var1 = 0.5D;
				double var3 = 1.0D;
				return ColorizerFoliage.getFoliageColor(var1, var3);
			}
		}

///    
///     <summary> * Returns the color this block should be rendered. Used by leaves. </summary>
///     
		public virtual int getRenderColor(int p_149741_1_)
		{
			return ColorizerFoliage.FoliageColorBasic;
		}

///    
///     <summary> * Returns a integer with hex for 0xrrggbb with this color multiplied against the blocks color. Note only called
///     * when first determining what to render. </summary>
///     
		public virtual int colorMultiplier(IBlockAccess p_149720_1_, int p_149720_2_, int p_149720_3_, int p_149720_4_)
		{
			int var5 = 0;
			int var6 = 0;
			int var7 = 0;

			for (int var8 = -1; var8 <= 1; ++var8)
			{
				for (int var9 = -1; var9 <= 1; ++var9)
				{
					int var10 = p_149720_1_.getBiomeGenForCoords(p_149720_2_ + var9, p_149720_4_ + var8).getBiomeFoliageColor(p_149720_2_ + var9, p_149720_3_, p_149720_4_ + var8);
					var5 += (var10 & 16711680) >> 16;
					var6 += (var10 & 65280) >> 8;
					var7 += var10 & 255;
				}
			}

			return (var5 / 9 & 255) << 16 | (var6 / 9 & 255) << 8 | var7 / 9 & 255;
		}

		public virtual void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			sbyte var7 = 1;
			int var8 = var7 + 1;

			if (p_149749_1_.checkChunksExist(p_149749_2_ - var8, p_149749_3_ - var8, p_149749_4_ - var8, p_149749_2_ + var8, p_149749_3_ + var8, p_149749_4_ + var8))
			{
				for (int var9 = -var7; var9 <= var7; ++var9)
				{
					for (int var10 = -var7; var10 <= var7; ++var10)
					{
						for (int var11 = -var7; var11 <= var7; ++var11)
						{
							if (p_149749_1_.getBlock(p_149749_2_ + var9, p_149749_3_ + var10, p_149749_4_ + var11).Material == Material.leaves)
							{
								int var12 = p_149749_1_.getBlockMetadata(p_149749_2_ + var9, p_149749_3_ + var10, p_149749_4_ + var11);
								p_149749_1_.setBlockMetadataWithNotify(p_149749_2_ + var9, p_149749_3_ + var10, p_149749_4_ + var11, var12 | 8, 4);
							}
						}
					}
				}
			}
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public virtual void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			if (!p_149674_1_.isClient)
			{
				int var6 = p_149674_1_.getBlockMetadata(p_149674_2_, p_149674_3_, p_149674_4_);

				if ((var6 & 8) != 0 && (var6 & 4) == 0)
				{
					sbyte var7 = 4;
					int var8 = var7 + 1;
					sbyte var9 = 32;
					int var10 = var9 * var9;
					int var11 = var9 / 2;

					if (this.field_150128_a == null)
					{
						this.field_150128_a = new int[var9 * var9 * var9];
					}

					int var12;

					if (p_149674_1_.checkChunksExist(p_149674_2_ - var8, p_149674_3_ - var8, p_149674_4_ - var8, p_149674_2_ + var8, p_149674_3_ + var8, p_149674_4_ + var8))
					{
						int var13;
						int var14;

						for (var12 = -var7; var12 <= var7; ++var12)
						{
							for (var13 = -var7; var13 <= var7; ++var13)
							{
								for (var14 = -var7; var14 <= var7; ++var14)
								{
									Block var15 = p_149674_1_.getBlock(p_149674_2_ + var12, p_149674_3_ + var13, p_149674_4_ + var14);

									if (var15 != Blocks.log && var15 != Blocks.log2)
									{
										if (var15.Material == Material.leaves)
										{
											this.field_150128_a[(var12 + var11) * var10 + (var13 + var11) * var9 + var14 + var11] = -2;
										}
										else
										{
											this.field_150128_a[(var12 + var11) * var10 + (var13 + var11) * var9 + var14 + var11] = -1;
										}
									}
									else
									{
										this.field_150128_a[(var12 + var11) * var10 + (var13 + var11) * var9 + var14 + var11] = 0;
									}
								}
							}
						}

						for (var12 = 1; var12 <= 4; ++var12)
						{
							for (var13 = -var7; var13 <= var7; ++var13)
							{
								for (var14 = -var7; var14 <= var7; ++var14)
								{
									for (int var16 = -var7; var16 <= var7; ++var16)
									{
										if (this.field_150128_a[(var13 + var11) * var10 + (var14 + var11) * var9 + var16 + var11] == var12 - 1)
										{
											if (this.field_150128_a[(var13 + var11 - 1) * var10 + (var14 + var11) * var9 + var16 + var11] == -2)
											{
												this.field_150128_a[(var13 + var11 - 1) * var10 + (var14 + var11) * var9 + var16 + var11] = var12;
											}

											if (this.field_150128_a[(var13 + var11 + 1) * var10 + (var14 + var11) * var9 + var16 + var11] == -2)
											{
												this.field_150128_a[(var13 + var11 + 1) * var10 + (var14 + var11) * var9 + var16 + var11] = var12;
											}

											if (this.field_150128_a[(var13 + var11) * var10 + (var14 + var11 - 1) * var9 + var16 + var11] == -2)
											{
												this.field_150128_a[(var13 + var11) * var10 + (var14 + var11 - 1) * var9 + var16 + var11] = var12;
											}

											if (this.field_150128_a[(var13 + var11) * var10 + (var14 + var11 + 1) * var9 + var16 + var11] == -2)
											{
												this.field_150128_a[(var13 + var11) * var10 + (var14 + var11 + 1) * var9 + var16 + var11] = var12;
											}

											if (this.field_150128_a[(var13 + var11) * var10 + (var14 + var11) * var9 + (var16 + var11 - 1)] == -2)
											{
												this.field_150128_a[(var13 + var11) * var10 + (var14 + var11) * var9 + (var16 + var11 - 1)] = var12;
											}

											if (this.field_150128_a[(var13 + var11) * var10 + (var14 + var11) * var9 + var16 + var11 + 1] == -2)
											{
												this.field_150128_a[(var13 + var11) * var10 + (var14 + var11) * var9 + var16 + var11 + 1] = var12;
											}
										}
									}
								}
							}
						}
					}

					var12 = this.field_150128_a[var11 * var10 + var11 * var9 + var11];

					if (var12 >= 0)
					{
						p_149674_1_.setBlockMetadataWithNotify(p_149674_2_, p_149674_3_, p_149674_4_, var6 & -9, 4);
					}
					else
					{
						this.func_150126_e(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_);
					}
				}
			}
		}

///    
///     <summary> * A randomly called display update to be able to add particles or other items for display </summary>
///     
		public virtual void randomDisplayTick(World p_149734_1_, int p_149734_2_, int p_149734_3_, int p_149734_4_, Random p_149734_5_)
		{
			if (p_149734_1_.canLightningStrikeAt(p_149734_2_, p_149734_3_ + 1, p_149734_4_) && !World.doesBlockHaveSolidTopSurface(p_149734_1_, p_149734_2_, p_149734_3_ - 1, p_149734_4_) && p_149734_5_.Next(15) == 1)
			{
				double var6 = (double)((float)p_149734_2_ + p_149734_5_.nextFloat());
				double var8 = (double)p_149734_3_ - 0.05D;
				double var10 = (double)((float)p_149734_4_ + p_149734_5_.nextFloat());
				p_149734_1_.spawnParticle("dripWater", var6, var8, var10, 0.0D, 0.0D, 0.0D);
			}
		}

		private void func_150126_e(World p_150126_1_, int p_150126_2_, int p_150126_3_, int p_150126_4_)
		{
			this.dropBlockAsItem(p_150126_1_, p_150126_2_, p_150126_3_, p_150126_4_, p_150126_1_.getBlockMetadata(p_150126_2_, p_150126_3_, p_150126_4_), 0);
			p_150126_1_.setBlockToAir(p_150126_2_, p_150126_3_, p_150126_4_);
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return p_149745_1_.Next(20) == 0 ? 1 : 0;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Item.getItemFromBlock(Blocks.sapling);
		}

///    
///     <summary> * Drops the block items with a specified chance of dropping the specified items </summary>
///     
		public virtual void dropBlockAsItemWithChance(World p_149690_1_, int p_149690_2_, int p_149690_3_, int p_149690_4_, int p_149690_5_, float p_149690_6_, int p_149690_7_)
		{
			if (!p_149690_1_.isClient)
			{
				int var8 = this.func_150123_b(p_149690_5_);

				if (p_149690_7_ > 0)
				{
					var8 -= 2 << p_149690_7_;

					if (var8 < 10)
					{
						var8 = 10;
					}
				}

				if (p_149690_1_.rand.Next(var8) == 0)
				{
					Item var9 = this.getItemDropped(p_149690_5_, p_149690_1_.rand, p_149690_7_);
					this.dropBlockAsItem_do(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, new ItemStack(var9, 1, this.damageDropped(p_149690_5_)));
				}

				var8 = 200;

				if (p_149690_7_ > 0)
				{
					var8 -= 10 << p_149690_7_;

					if (var8 < 40)
					{
						var8 = 40;
					}
				}

				this.func_150124_c(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, p_149690_5_, var8);
			}
		}

		protected internal virtual void func_150124_c(World p_150124_1_, int p_150124_2_, int p_150124_3_, int p_150124_4_, int p_150124_5_, int p_150124_6_)
		{
		}

		protected internal virtual int func_150123_b(int p_150123_1_)
		{
			return 20;
		}

		public virtual void harvestBlock(World p_149636_1_, EntityPlayer p_149636_2_, int p_149636_3_, int p_149636_4_, int p_149636_5_, int p_149636_6_)
		{
			if (!p_149636_1_.isClient && p_149636_2_.CurrentEquippedItem != null && p_149636_2_.CurrentEquippedItem.Item == Items.shears)
			{
				p_149636_2_.addStat(StatList.mineBlockStatArray[Block.getIdFromBlock(this)], 1);
				this.dropBlockAsItem_do(p_149636_1_, p_149636_3_, p_149636_4_, p_149636_5_, new ItemStack(Item.getItemFromBlock(this), 1, p_149636_6_ & 3));
			}
			else
			{
				base.harvestBlock(p_149636_1_, p_149636_2_, p_149636_3_, p_149636_4_, p_149636_5_, p_149636_6_);
			}
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public virtual int damageDropped(int p_149692_1_)
		{
			return p_149692_1_ & 3;
		}

		public override bool isOpaqueCube()
		{
			get
			{
				return !this.field_150121_P;
			}
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public abstract IIcon getIcon(int p_149691_1_, int p_149691_2_);

		public virtual void func_150122_b(bool p_150122_1_)
		{
			this.field_150121_P = p_150122_1_;
			this.field_150127_b = p_150122_1_ ? 0 : 1;
		}

///    
///     <summary> * Returns an item stack containing a single instance of the current block type. 'i' is the block's subtype/damage
///     * and is ignored for blocks which do not support subtypes. Blocks which cannot be harvested should return null. </summary>
///     
		protected internal virtual ItemStack createStackedBlock(int p_149644_1_)
		{
			return new ItemStack(Item.getItemFromBlock(this), 1, p_149644_1_ & 3);
		}

		public abstract string[] func_150125_e();
	}

}