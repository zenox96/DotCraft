using System;

namespace DotCraftCore.nWorld.nGen.nFeature
{

	using Block = DotCraftCore.nBlock.Block;
	using Material = DotCraftCore.nBlock.nMaterial.Material;
	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.nWorld.World;

	public class WorldGenSand : WorldGenerator
	{
		private Block field_150517_a;

	/// <summary> The maximum radius used when generating a patch of blocks.  </summary>
		private int radius;
		

		public WorldGenSand(Block p_i45462_1_, int p_i45462_2_)
		{
			this.field_150517_a = p_i45462_1_;
			this.radius = p_i45462_2_;
		}

		public override bool generate(World p_76484_1_, Random p_76484_2_, int p_76484_3_, int p_76484_4_, int p_76484_5_)
		{
			if (p_76484_1_.getBlock(p_76484_3_, p_76484_4_, p_76484_5_).Material != Material.water)
			{
				return false;
			}
			else
			{
				int var6 = p_76484_2_.Next(this.radius - 2) + 2;
				sbyte var7 = 2;

				for (int var8 = p_76484_3_ - var6; var8 <= p_76484_3_ + var6; ++var8)
				{
					for (int var9 = p_76484_5_ - var6; var9 <= p_76484_5_ + var6; ++var9)
					{
						int var10 = var8 - p_76484_3_;
						int var11 = var9 - p_76484_5_;

						if (var10 * var10 + var11 * var11 <= var6 * var6)
						{
							for (int var12 = p_76484_4_ - var7; var12 <= p_76484_4_ + var7; ++var12)
							{
								Block var13 = p_76484_1_.getBlock(var8, var12, var9);

								if (var13 == Blocks.dirt || var13 == Blocks.grass)
								{
									p_76484_1_.setBlock(var8, var12, var9, this.field_150517_a, 0, 2);
								}
							}
						}
					}
				}

				return true;
			}
		}
	}

}