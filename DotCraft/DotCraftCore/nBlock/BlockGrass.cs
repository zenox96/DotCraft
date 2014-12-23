using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nWorld;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockGrass : Block, IGrowable
	{
		private static readonly Logger logger = LogManager.Logger;

		protected internal BlockGrass() : base(Material.grass)
		{
			this.TickRandomly = true;
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public override int BlockColor
		{
			get
			{
				double var1 = 0.5D;
				double var3 = 1.0D;
				return ColorizerGrass.getGrassColor(var1, var3);
			}
		}

///    
///     <summary> * Returns the color this block should be rendered. Used by leaves. </summary>
///     
		public override int getRenderColor(int p_149741_1_)
		{
			return this.BlockColor;
		}

///    
///     <summary> * Returns a integer with hex for 0xrrggbb with this color multiplied against the blocks color. Note only called
///     * when first determining what to render. </summary>
///     
		public override int colorMultiplier(IBlockAccess p_149720_1_, int p_149720_2_, int p_149720_3_, int p_149720_4_)
		{
			int var5 = 0;
			int var6 = 0;
			int var7 = 0;

			for (int var8 = -1; var8 <= 1; ++var8)
			{
				for (int var9 = -1; var9 <= 1; ++var9)
				{
					int var10 = p_149720_1_.getBiomeGenForCoords(p_149720_2_ + var9, p_149720_4_ + var8).getBiomeGrassColor(p_149720_2_ + var9, p_149720_3_, p_149720_4_ + var8);
					var5 += (var10 & 16711680) >> 16;
					var6 += (var10 & 65280) >> 8;
					var7 += var10 & 255;
				}
			}

			return (var5 / 9 & 255) << 16 | (var6 / 9 & 255) << 8 | var7 / 9 & 255;
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
							p_149674_1_.setBlock(var7, var8, var9, Blocks.grass);
						}
					}
				}
			}
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return Blocks.dirt.getItemDropped(0, p_149650_2_, p_149650_3_);
		}

		public override bool func_149851_a(World p_149851_1_, int p_149851_2_, int p_149851_3_, int p_149851_4_, bool p_149851_5_)
		{
			return true;
		}

		public override bool func_149852_a(World p_149852_1_, Random p_149852_2_, int p_149852_3_, int p_149852_4_, int p_149852_5_)
		{
			return true;
		}

		public override void func_149853_b(World p_149853_1_, Random p_149853_2_, int p_149853_3_, int p_149853_4_, int p_149853_5_)
		{
			int var6 = 0;

			while (var6 < 128)
			{
				int var7 = p_149853_3_;
				int var8 = p_149853_4_ + 1;
				int var9 = p_149853_5_;
				int var10 = 0;

				while (true)
				{
					if (var10 < var6 / 16)
					{
						var7 += p_149853_2_.Next(3) - 1;
						var8 += (p_149853_2_.Next(3) - 1) * p_149853_2_.Next(3) / 2;
						var9 += p_149853_2_.Next(3) - 1;

						if (p_149853_1_.getBlock(var7, var8 - 1, var9) == Blocks.grass && !p_149853_1_.getBlock(var7, var8, var9).isBlockNormalCube())
						{
							++var10;
							continue;
						}
					}
					else if (p_149853_1_.getBlock(var7, var8, var9).BlockMaterial == Material.air)
					{
						if (p_149853_2_.Next(8) != 0)
						{
							if (Blocks.tallgrass.canBlockStay(p_149853_1_, var7, var8, var9))
							{
								p_149853_1_.setBlock(var7, var8, var9, Blocks.tallgrass, 1, 3);
							}
						}
						else
						{
							string var13 = p_149853_1_.getBiomeGenForCoords(var7, var9).func_150572_a(p_149853_2_, var7, var8, var9);
							logger.debug("Flower in " + p_149853_1_.getBiomeGenForCoords(var7, var9).biomeName + ": " + var13);
							BlockFlower var11 = BlockFlower.func_149857_e(var13);

							if (var11 != null && var11.canBlockStay(p_149853_1_, var7, var8, var9))
							{
								int var12 = BlockFlower.func_149856_f(var13);
								p_149853_1_.setBlock(var7, var8, var9, var11, var12, 3);
							}
						}
					}

					++var6;
					break;
				}
			}
		}
	}

}