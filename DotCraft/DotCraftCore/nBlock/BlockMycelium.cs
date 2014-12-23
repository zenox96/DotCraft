using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nWorld;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockMycelium : Block
	{
		protected internal BlockMycelium() : base(Material.grass)
		{
			this.TickRandomly = true;
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public override void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			if (!p_149674_1_.isClient)
			{
				if (p_149674_1_.getBlockLightValue(p_149674_2_, p_149674_3_ + 1, p_149674_4_) < 4 && p_149674_1_.getBlock(p_149674_2_, p_149674_3_ + 1, p_149674_4_).LightOpacity > 2)
				{
					p_149674_1_.setBlock(p_149674_2_, p_149674_3_, p_149674_4_, Blocks.dirt);
				}
				else if (p_149674_1_.getBlockLightValue(p_149674_2_, p_149674_3_ + 1, p_149674_4_) >= 9)
				{
					for (int var6 = 0; var6 < 4; ++var6)
					{
						int var7 = p_149674_2_ + p_149674_5_.Next(3) - 1;
						int var8 = p_149674_3_ + p_149674_5_.Next(5) - 3;
						int var9 = p_149674_4_ + p_149674_5_.Next(3) - 1;
						Block var10 = p_149674_1_.getBlock(var7, var8 + 1, var9);

						if (p_149674_1_.getBlock(var7, var8, var9) == Blocks.dirt && p_149674_1_.getBlockMetadata(var7, var8, var9) == 0 && p_149674_1_.getBlockLightValue(var7, var8 + 1, var9) >= 4 && var10.LightOpacity <= 2)
						{
							p_149674_1_.setBlock(var7, var8, var9, this);
						}
					}
				}
			}
		}

///    
///     <summary> * A randomly called display update to be able to add particles or other items for display </summary>
///     
		public override void randomDisplayTick(World p_149734_1_, int p_149734_2_, int p_149734_3_, int p_149734_4_, Random p_149734_5_)
		{
			base.randomDisplayTick(p_149734_1_, p_149734_2_, p_149734_3_, p_149734_4_, p_149734_5_);

			if (p_149734_5_.Next(10) == 0)
			{
				p_149734_1_.spawnParticle("townaura", (double)p_149734_2_ + p_149734_5_.NextDouble(), (double)p_149734_3_ + 1.1F, (double)p_149734_4_ + p_149734_5_.NextDouble(), 0.0D, 0.0D, 0.0D);
			}
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Blocks.dirt.getItemDropped(0, p_149650_2_, p_149650_3_);
		}
	}

}