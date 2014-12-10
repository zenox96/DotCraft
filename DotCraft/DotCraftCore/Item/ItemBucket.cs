using System;

namespace DotCraftCore.Item
{

	using Block = DotCraftCore.block.Block;
	using Material = DotCraftCore.block.material.Material;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using MovingObjectPosition = DotCraftCore.Util.MovingObjectPosition;
	using World = DotCraftCore.World.World;

	public class ItemBucket : Item
	{
	/// <summary> field for checking if the bucket has been filled.  </summary>
		private Block isFull;
		private const string __OBFID = "CL_00000000";

		public ItemBucket(Block p_i45331_1_)
		{
			this.maxStackSize = 1;
			this.isFull = p_i45331_1_;
			this.CreativeTab = CreativeTabs.tabMisc;
		}

///    
///     <summary> * Called whenever this item is equipped and the right mouse button is pressed. Args: itemStack, world, entityPlayer </summary>
///     
		public virtual ItemStack onItemRightClick(ItemStack p_77659_1_, World p_77659_2_, EntityPlayer p_77659_3_)
		{
			bool var4 = this.isFull == Blocks.air;
			MovingObjectPosition var5 = this.getMovingObjectPositionFromPlayer(p_77659_2_, p_77659_3_, var4);

			if (var5 == null)
			{
				return p_77659_1_;
			}
			else
			{
				if (var5.typeOfHit == MovingObjectPosition.MovingObjectType.BLOCK)
				{
					int var6 = var5.blockX;
					int var7 = var5.blockY;
					int var8 = var5.blockZ;

					if (!p_77659_2_.canMineBlock(p_77659_3_, var6, var7, var8))
					{
						return p_77659_1_;
					}

					if (var4)
					{
						if (!p_77659_3_.canPlayerEdit(var6, var7, var8, var5.sideHit, p_77659_1_))
						{
							return p_77659_1_;
						}

						Material var9 = p_77659_2_.getBlock(var6, var7, var8).Material;
						int var10 = p_77659_2_.getBlockMetadata(var6, var7, var8);

						if (var9 == Material.water && var10 == 0)
						{
							p_77659_2_.setBlockToAir(var6, var7, var8);
							return this.func_150910_a(p_77659_1_, p_77659_3_, Items.water_bucket);
						}

						if (var9 == Material.lava && var10 == 0)
						{
							p_77659_2_.setBlockToAir(var6, var7, var8);
							return this.func_150910_a(p_77659_1_, p_77659_3_, Items.lava_bucket);
						}
					}
					else
					{
						if (this.isFull == Blocks.air)
						{
							return new ItemStack(Items.bucket);
						}

						if (var5.sideHit == 0)
						{
							--var7;
						}

						if (var5.sideHit == 1)
						{
							++var7;
						}

						if (var5.sideHit == 2)
						{
							--var8;
						}

						if (var5.sideHit == 3)
						{
							++var8;
						}

						if (var5.sideHit == 4)
						{
							--var6;
						}

						if (var5.sideHit == 5)
						{
							++var6;
						}

						if (!p_77659_3_.canPlayerEdit(var6, var7, var8, var5.sideHit, p_77659_1_))
						{
							return p_77659_1_;
						}

						if (this.tryPlaceContainedLiquid(p_77659_2_, var6, var7, var8) && !p_77659_3_.capabilities.isCreativeMode)
						{
							return new ItemStack(Items.bucket);
						}
					}
				}

				return p_77659_1_;
			}
		}

		private ItemStack func_150910_a(ItemStack p_150910_1_, EntityPlayer p_150910_2_, Item p_150910_3_)
		{
			if (p_150910_2_.capabilities.isCreativeMode)
			{
				return p_150910_1_;
			}
			else if (--p_150910_1_.stackSize <= 0)
			{
				return new ItemStack(p_150910_3_);
			}
			else
			{
				if (!p_150910_2_.inventory.addItemStackToInventory(new ItemStack(p_150910_3_)))
				{
					p_150910_2_.dropPlayerItemWithRandomChoice(new ItemStack(p_150910_3_, 1, 0), false);
				}

				return p_150910_1_;
			}
		}

///    
///     <summary> * Attempts to place the liquid contained inside the bucket. </summary>
///     
		public virtual bool tryPlaceContainedLiquid(World p_77875_1_, int p_77875_2_, int p_77875_3_, int p_77875_4_)
		{
			if (this.isFull == Blocks.air)
			{
				return false;
			}
			else
			{
				Material var5 = p_77875_1_.getBlock(p_77875_2_, p_77875_3_, p_77875_4_).Material;
				bool var6 = !var5.Solid;

				if (!p_77875_1_.isAirBlock(p_77875_2_, p_77875_3_, p_77875_4_) && !var6)
				{
					return false;
				}
				else
				{
					if (p_77875_1_.provider.isHellWorld && this.isFull == Blocks.flowing_water)
					{
						p_77875_1_.playSoundEffect((double)((float)p_77875_2_ + 0.5F), (double)((float)p_77875_3_ + 0.5F), (double)((float)p_77875_4_ + 0.5F), "random.fizz", 0.5F, 2.6F + (p_77875_1_.rand.nextFloat() - p_77875_1_.rand.nextFloat()) * 0.8F);

						for (int var7 = 0; var7 < 8; ++var7)
						{
							p_77875_1_.spawnParticle("largesmoke", (double)p_77875_2_ + new Random(1).NextDouble(), (double)p_77875_3_ + new Random(2).NextDouble(), (double)p_77875_4_ + new Random(3).NextDouble(), 0.0D, 0.0D, 0.0D);
						}
					}
					else
					{
						if (!p_77875_1_.isClient && var6 && !var5.Liquid)
						{
							p_77875_1_.func_147480_a(p_77875_2_, p_77875_3_, p_77875_4_, true);
						}

						p_77875_1_.setBlock(p_77875_2_, p_77875_3_, p_77875_4_, this.isFull, 0, 3);
					}

					return true;
				}
			}
		}
	}

}