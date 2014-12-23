using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity.nItem;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nWorld;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockFalling : Block
	{
		public static bool field_149832_M;

		public BlockFalling() : base(Material.sand)
		{
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public BlockFalling(Material p_i45405_1_) : base(p_i45405_1_)
		{
		}

		public override void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
			p_149726_1_.scheduleBlockUpdate(p_149726_2_, p_149726_3_, p_149726_4_, this, this.func_149738_a(p_149726_1_));
		}

		public override void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			p_149695_1_.scheduleBlockUpdate(p_149695_2_, p_149695_3_, p_149695_4_, this, this.func_149738_a(p_149695_1_));
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public override void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			if (!p_149674_1_.isClient)
			{
				this.func_149830_m(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_);
			}
		}

		private void func_149830_m(World p_149830_1_, int p_149830_2_, int p_149830_3_, int p_149830_4_)
		{
			if (func_149831_e(p_149830_1_, p_149830_2_, p_149830_3_ - 1, p_149830_4_) && p_149830_3_ >= 0)
			{
				sbyte var8 = 32;

				if (!field_149832_M && p_149830_1_.checkChunksExist(p_149830_2_ - var8, p_149830_3_ - var8, p_149830_4_ - var8, p_149830_2_ + var8, p_149830_3_ + var8, p_149830_4_ + var8))
				{
					if (!p_149830_1_.isClient)
					{
						EntityFallingBlock var9 = new EntityFallingBlock(p_149830_1_, (double)p_149830_2_ + 0.5D, (double)p_149830_3_ + 0.5D, (double)p_149830_4_ + 0.5D, this, p_149830_1_.getBlockMetadata(p_149830_2_, p_149830_3_, p_149830_4_));
						this.func_149829_a(var9);
						p_149830_1_.spawnEntityInWorld(var9);
					}
				}
				else
				{
					p_149830_1_.setBlockToAir(p_149830_2_, p_149830_3_, p_149830_4_);

					while (func_149831_e(p_149830_1_, p_149830_2_, p_149830_3_ - 1, p_149830_4_) && p_149830_3_ > 0)
					{
						--p_149830_3_;
					}

					if (p_149830_3_ > 0)
					{
						p_149830_1_.setBlock(p_149830_2_, p_149830_3_, p_149830_4_, this);
					}
				}
			}
		}

		protected internal override void func_149829_a(EntityFallingBlock p_149829_1_)
		{
		}

		public override int func_149738_a(World p_149738_1_)
		{
			return 2;
		}

		public static bool func_149831_e(World p_149831_0_, int p_149831_1_, int p_149831_2_, int p_149831_3_)
		{
			Block var4 = p_149831_0_.getBlock(p_149831_1_, p_149831_2_, p_149831_3_);

			if (var4.BlockMaterial == Material.air)
			{
				return true;
			}
			else if (var4 == Blocks.fire)
			{
				return true;
			}
			else
			{
				Material var5 = var4.BlockMaterial;
				return var5 == Material.water ? true : var5 == Material.lava;
			}
		}

		public override void func_149828_a(World p_149828_1_, int p_149828_2_, int p_149828_3_, int p_149828_4_, int p_149828_5_)
		{
		}
	}

}
