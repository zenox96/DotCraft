using System;

namespace DotCraftCore.World.Gen.Feature
{

	using Material = DotCraftCore.block.material.Material;
	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.World.World;

	public class WorldGenReed : WorldGenerator
	{
		private const string __OBFID = "CL_00000429";

		public override bool generate(World p_76484_1_, Random p_76484_2_, int p_76484_3_, int p_76484_4_, int p_76484_5_)
		{
			for (int var6 = 0; var6 < 20; ++var6)
			{
				int var7 = p_76484_3_ + p_76484_2_.Next(4) - p_76484_2_.Next(4);
				int var8 = p_76484_4_;
				int var9 = p_76484_5_ + p_76484_2_.Next(4) - p_76484_2_.Next(4);

				if (p_76484_1_.isAirBlock(var7, p_76484_4_, var9) && (p_76484_1_.getBlock(var7 - 1, p_76484_4_ - 1, var9).Material == Material.water || p_76484_1_.getBlock(var7 + 1, p_76484_4_ - 1, var9).Material == Material.water || p_76484_1_.getBlock(var7, p_76484_4_ - 1, var9 - 1).Material == Material.water || p_76484_1_.getBlock(var7, p_76484_4_ - 1, var9 + 1).Material == Material.water))
				{
					int var10 = 2 + p_76484_2_.Next(p_76484_2_.Next(3) + 1);

					for (int var11 = 0; var11 < var10; ++var11)
					{
						if (Blocks.reeds.canBlockStay(p_76484_1_, var7, var8 + var11, var9))
						{
							p_76484_1_.setBlock(var7, var8 + var11, var9, Blocks.reeds, 0, 2);
						}
					}
				}
			}

			return true;
		}
	}

}