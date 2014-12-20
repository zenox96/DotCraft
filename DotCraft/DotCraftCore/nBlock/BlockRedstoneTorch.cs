using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nWorld;
using System;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockRedstoneTorch : BlockTorch
	{
		private bool field_150113_a;
		private static IDictionary field_150112_b = new Hashtable();

		private bool func_150111_a(World p_150111_1_, int p_150111_2_, int p_150111_3_, int p_150111_4_, bool p_150111_5_)
		{
			if (!field_150112_b.ContainsKey(p_150111_1_))
			{
				field_150112_b.Add(p_150111_1_, new ArrayList());
			}

			IList var6 = (IList)field_150112_b[p_150111_1_];

			if (p_150111_5_)
			{
				var6.Add(new BlockRedstoneTorch.Toggle(p_150111_2_, p_150111_3_, p_150111_4_, p_150111_1_.TotalWorldTime));
			}

			int var7 = 0;

			for (int var8 = 0; var8 < var6.Count; ++var8)
			{
				BlockRedstoneTorch.Toggle var9 = (BlockRedstoneTorch.Toggle)var6[var8];

				if (var9.field_150847_a == p_150111_2_ && var9.field_150845_b == p_150111_3_ && var9.field_150846_c == p_150111_4_)
				{
					++var7;

					if (var7 >= 8)
					{
						return true;
					}
				}
			}

			return false;
		}

		protected internal BlockRedstoneTorch(bool p_i45423_1_)
		{
			this.field_150113_a = p_i45423_1_;
			this.TickRandomly = true;
			this.CreativeTab = (CreativeTabs)null;
		}

		public virtual int func_149738_a(World p_149738_1_)
		{
			return 2;
		}

		public override void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
			if (p_149726_1_.getBlockMetadata(p_149726_2_, p_149726_3_, p_149726_4_) == 0)
			{
				base.onBlockAdded(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);
			}

			if (this.field_150113_a)
			{
				p_149726_1_.notifyBlocksOfNeighborChange(p_149726_2_, p_149726_3_ - 1, p_149726_4_, this);
				p_149726_1_.notifyBlocksOfNeighborChange(p_149726_2_, p_149726_3_ + 1, p_149726_4_, this);
				p_149726_1_.notifyBlocksOfNeighborChange(p_149726_2_ - 1, p_149726_3_, p_149726_4_, this);
				p_149726_1_.notifyBlocksOfNeighborChange(p_149726_2_ + 1, p_149726_3_, p_149726_4_, this);
				p_149726_1_.notifyBlocksOfNeighborChange(p_149726_2_, p_149726_3_, p_149726_4_ - 1, this);
				p_149726_1_.notifyBlocksOfNeighborChange(p_149726_2_, p_149726_3_, p_149726_4_ + 1, this);
			}
		}

		public virtual void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			if (this.field_150113_a)
			{
				p_149749_1_.notifyBlocksOfNeighborChange(p_149749_2_, p_149749_3_ - 1, p_149749_4_, this);
				p_149749_1_.notifyBlocksOfNeighborChange(p_149749_2_, p_149749_3_ + 1, p_149749_4_, this);
				p_149749_1_.notifyBlocksOfNeighborChange(p_149749_2_ - 1, p_149749_3_, p_149749_4_, this);
				p_149749_1_.notifyBlocksOfNeighborChange(p_149749_2_ + 1, p_149749_3_, p_149749_4_, this);
				p_149749_1_.notifyBlocksOfNeighborChange(p_149749_2_, p_149749_3_, p_149749_4_ - 1, this);
				p_149749_1_.notifyBlocksOfNeighborChange(p_149749_2_, p_149749_3_, p_149749_4_ + 1, this);
			}
		}

		public virtual int isProvidingWeakPower(IBlockAccess p_149709_1_, int p_149709_2_, int p_149709_3_, int p_149709_4_, int p_149709_5_)
		{
			if (!this.field_150113_a)
			{
				return 0;
			}
			else
			{
				int var6 = p_149709_1_.getBlockMetadata(p_149709_2_, p_149709_3_, p_149709_4_);
				return var6 == 5 && p_149709_5_ == 1 ? 0 : (var6 == 3 && p_149709_5_ == 3 ? 0 : (var6 == 4 && p_149709_5_ == 2 ? 0 : (var6 == 1 && p_149709_5_ == 5 ? 0 : (var6 == 2 && p_149709_5_ == 4 ? 0 : 15))));
			}
		}

		private bool func_150110_m(World p_150110_1_, int p_150110_2_, int p_150110_3_, int p_150110_4_)
		{
			int var5 = p_150110_1_.getBlockMetadata(p_150110_2_, p_150110_3_, p_150110_4_);
			return var5 == 5 && p_150110_1_.getIndirectPowerOutput(p_150110_2_, p_150110_3_ - 1, p_150110_4_, 0) ? true : (var5 == 3 && p_150110_1_.getIndirectPowerOutput(p_150110_2_, p_150110_3_, p_150110_4_ - 1, 2) ? true : (var5 == 4 && p_150110_1_.getIndirectPowerOutput(p_150110_2_, p_150110_3_, p_150110_4_ + 1, 3) ? true : (var5 == 1 && p_150110_1_.getIndirectPowerOutput(p_150110_2_ - 1, p_150110_3_, p_150110_4_, 4) ? true : var5 == 2 && p_150110_1_.getIndirectPowerOutput(p_150110_2_ + 1, p_150110_3_, p_150110_4_, 5))));
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public override void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			bool var6 = this.func_150110_m(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_);
			IList var7 = (IList)field_150112_b[p_149674_1_];

			while (var7 != null && !var7.Count == 0 && p_149674_1_.TotalWorldTime - ((BlockRedstoneTorch.Toggle)var7[0]).field_150844_d > 60L)
			{
				var7.Remove(0);
			}

			if (this.field_150113_a)
			{
				if (var6)
				{
					p_149674_1_.setBlock(p_149674_2_, p_149674_3_, p_149674_4_, Blocks.unlit_redstone_torch, p_149674_1_.getBlockMetadata(p_149674_2_, p_149674_3_, p_149674_4_), 3);

					if (this.func_150111_a(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_, true))
					{
						p_149674_1_.playSoundEffect((double)((float)p_149674_2_ + 0.5F), (double)((float)p_149674_3_ + 0.5F), (double)((float)p_149674_4_ + 0.5F), "random.fizz", 0.5F, 2.6F + (p_149674_1_.rand.nextFloat() - p_149674_1_.rand.nextFloat()) * 0.8F);

						for (int var8 = 0; var8 < 5; ++var8)
						{
							double var9 = (double)p_149674_2_ + p_149674_5_.NextDouble() * 0.6D + 0.2D;
							double var11 = (double)p_149674_3_ + p_149674_5_.NextDouble() * 0.6D + 0.2D;
							double var13 = (double)p_149674_4_ + p_149674_5_.NextDouble() * 0.6D + 0.2D;
							p_149674_1_.spawnParticle("smoke", var9, var11, var13, 0.0D, 0.0D, 0.0D);
						}
					}
				}
			}
			else if (!var6 && !this.func_150111_a(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_, false))
			{
				p_149674_1_.setBlock(p_149674_2_, p_149674_3_, p_149674_4_, Blocks.redstone_torch, p_149674_1_.getBlockMetadata(p_149674_2_, p_149674_3_, p_149674_4_), 3);
			}
		}

		public override void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			if (!this.func_150108_b(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_, p_149695_5_))
			{
				bool var6 = this.func_150110_m(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_);

				if (this.field_150113_a && var6 || !this.field_150113_a && !var6)
				{
					p_149695_1_.scheduleBlockUpdate(p_149695_2_, p_149695_3_, p_149695_4_, this, this.func_149738_a(p_149695_1_));
				}
			}
		}

		public virtual int isProvidingStrongPower(IBlockAccess p_149748_1_, int p_149748_2_, int p_149748_3_, int p_149748_4_, int p_149748_5_)
		{
			return p_149748_5_ == 0 ? this.isProvidingWeakPower(p_149748_1_, p_149748_2_, p_149748_3_, p_149748_4_, p_149748_5_) : 0;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Item.getItemFromBlock(Blocks.redstone_torch);
		}

///    
///     <summary> * Can this block provide power. Only wire currently seems to have this change based on its state. </summary>
///     
		public virtual bool canProvidePower()
		{
			return true;
		}

///    
///     <summary> * A randomly called display update to be able to add particles or other items for display </summary>
///     
		public override void randomDisplayTick(World p_149734_1_, int p_149734_2_, int p_149734_3_, int p_149734_4_, Random p_149734_5_)
		{
			if (this.field_150113_a)
			{
				int var6 = p_149734_1_.getBlockMetadata(p_149734_2_, p_149734_3_, p_149734_4_);
				double var7 = ((double)p_149734_2_ + 0.5D) + (p_149734_5_.NextDouble() - 0.5D) * 0.2D;
				double var9 = ((double)p_149734_3_ + 0.7D) + (p_149734_5_.NextDouble() - 0.5D) * 0.2D;
				double var11 = ((double)p_149734_4_ + 0.5D) + (p_149734_5_.NextDouble() - 0.5D) * 0.2D;
				double var13 = 0.2199999988079071D;
				double var15 = 0.27000001072883606D;

				if (var6 == 1)
				{
					p_149734_1_.spawnParticle("reddust", var7 - var15, var9 + var13, var11, 0.0D, 0.0D, 0.0D);
				}
				else if (var6 == 2)
				{
					p_149734_1_.spawnParticle("reddust", var7 + var15, var9 + var13, var11, 0.0D, 0.0D, 0.0D);
				}
				else if (var6 == 3)
				{
					p_149734_1_.spawnParticle("reddust", var7, var9 + var13, var11 - var15, 0.0D, 0.0D, 0.0D);
				}
				else if (var6 == 4)
				{
					p_149734_1_.spawnParticle("reddust", var7, var9 + var13, var11 + var15, 0.0D, 0.0D, 0.0D);
				}
				else
				{
					p_149734_1_.spawnParticle("reddust", var7, var9, var11, 0.0D, 0.0D, 0.0D);
				}
			}
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Item.getItemFromBlock(Blocks.redstone_torch);
		}

		public virtual bool func_149667_c(Block p_149667_1_)
		{
			return p_149667_1_ == Blocks.unlit_redstone_torch || p_149667_1_ == Blocks.redstone_torch;
		}

		internal class Toggle
		{
			internal int field_150847_a;
			internal int field_150845_b;
			internal int field_150846_c;
			internal long field_150844_d;
			

			public Toggle(int p_i45422_1_, int p_i45422_2_, int p_i45422_3_, long p_i45422_4_)
			{
				this.field_150847_a = p_i45422_1_;
				this.field_150845_b = p_i45422_2_;
				this.field_150846_c = p_i45422_3_;
				this.field_150844_d = p_i45422_4_;
			}
		}
	}

}