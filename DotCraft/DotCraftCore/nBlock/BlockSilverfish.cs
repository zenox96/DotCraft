using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity.nMonster;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nWorld;
using System;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockSilverfish : Block
	{
		public static readonly string[] field_150198_a = new string[] {"stone", "cobble", "brick", "mossybrick", "crackedbrick", "chiseledbrick"};
		

		public BlockSilverfish() : base(Material.field_151571_B)
		{
			this.Hardness = 0.0F;
			this.CreativeTab = CreativeTabs.tabDecorations;
		}

		public override void onBlockDestroyedByPlayer(World p_149664_1_, int p_149664_2_, int p_149664_3_, int p_149664_4_, int p_149664_5_)
		{
			if (!p_149664_1_.isClient)
			{
				EntitySilverfish var6 = new EntitySilverfish(p_149664_1_);
				var6.setLocationAndAngles((double)p_149664_2_ + 0.5D, (double)p_149664_3_, (double)p_149664_4_ + 0.5D, 0.0F, 0.0F);
				p_149664_1_.spawnEntityInWorld(var6);
				var6.spawnExplosionParticle();
			}

			base.onBlockDestroyedByPlayer(p_149664_1_, p_149664_2_, p_149664_3_, p_149664_4_, p_149664_5_);
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public override int quantityDropped(Random p_149745_1_)
		{
			return 0;
		}

		public static bool func_150196_a(Block p_150196_0_)
		{
			return p_150196_0_ == Blocks.stone || p_150196_0_ == Blocks.cobblestone || p_150196_0_ == Blocks.stonebrick;
		}

		public static int func_150195_a(Block p_150195_0_, int p_150195_1_)
		{
			if (p_150195_1_ == 0)
			{
				if (p_150195_0_ == Blocks.cobblestone)
				{
					return 1;
				}

				if (p_150195_0_ == Blocks.stonebrick)
				{
					return 2;
				}
			}
			else if (p_150195_0_ == Blocks.stonebrick)
			{
				switch (p_150195_1_)
				{
					case 1:
						return 3;

					case 2:
						return 4;

					case 3:
						return 5;
				}
			}

			return 0;
		}

		public static Tuple<Block, int> func_150197_b(int p_150197_0_)
		{
			switch (p_150197_0_)
			{
				case 1:
                    return new Tuple<Block, int>(Blocks.cobblestone, 0);

				case 2:
                    return new Tuple<Block, int>(Blocks.stonebrick, 0);

				case 3:
                    return new Tuple<Block, int>(Blocks.stonebrick, 1);

				case 4:
                    return new Tuple<Block, int>(Blocks.stonebrick, 2);

				case 5:
                    return new Tuple<Block, int>(Blocks.stonebrick, 3);

				default:
                    return new Tuple<Block, int>(Blocks.stone, 0);
			}
		}

///    
///     <summary> * Returns an item stack containing a single instance of the current block type. 'i' is the block's subtype/damage
///     * and is ignored for blocks which do not support subtypes. Blocks which cannot be harvested should return null. </summary>
///     
		protected internal override ItemStack createStackedBlock(int p_149644_1_)
		{
			switch (p_149644_1_)
			{
				case 1:
					return new ItemStack(Blocks.cobblestone);

				case 2:
					return new ItemStack(Blocks.stonebrick);

				case 3:
					return new ItemStack(Blocks.stonebrick, 1, 1);

				case 4:
					return new ItemStack(Blocks.stonebrick, 1, 2);

				case 5:
					return new ItemStack(Blocks.stonebrick, 1, 3);

				default:
					return new ItemStack(Blocks.stone);
			}
		}

///    
///     <summary> * Drops the block items with a specified chance of dropping the specified items </summary>
///     
		public override void dropBlockAsItemWithChance(World p_149690_1_, int p_149690_2_, int p_149690_3_, int p_149690_4_, int p_149690_5_, float p_149690_6_, int p_149690_7_)
		{
			if (!p_149690_1_.isClient)
			{
				EntitySilverfish var8 = new EntitySilverfish(p_149690_1_);
				var8.setLocationAndAngles((double)p_149690_2_ + 0.5D, (double)p_149690_3_, (double)p_149690_4_ + 0.5D, 0.0F, 0.0F);
				p_149690_1_.spawnEntityInWorld(var8);
				var8.spawnExplosionParticle();
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
			for (int var4 = 0; var4 < field_150198_a.Length; ++var4)
			{
				p_149666_3_.Add(new ItemStack(p_149666_1_, 1, var4));
			}
		}
	}

}