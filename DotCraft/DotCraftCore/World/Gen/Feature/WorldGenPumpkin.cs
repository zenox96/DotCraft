using System;

namespace DotCraftCore.World.Gen.Feature
{

	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.World.World;

	public class WorldGenPumpkin : WorldGenerator
	{
		

		public override bool generate(World p_76484_1_, Random p_76484_2_, int p_76484_3_, int p_76484_4_, int p_76484_5_)
		{
			for (int var6 = 0; var6 < 64; ++var6)
			{
				int var7 = p_76484_3_ + p_76484_2_.Next(8) - p_76484_2_.Next(8);
				int var8 = p_76484_4_ + p_76484_2_.Next(4) - p_76484_2_.Next(4);
				int var9 = p_76484_5_ + p_76484_2_.Next(8) - p_76484_2_.Next(8);

				if (p_76484_1_.isAirBlock(var7, var8, var9) && p_76484_1_.getBlock(var7, var8 - 1, var9) == Blocks.grass && Blocks.pumpkin.canPlaceBlockAt(p_76484_1_, var7, var8, var9))
				{
					p_76484_1_.setBlock(var7, var8, var9, Blocks.pumpkin, p_76484_2_.Next(4), 2);
				}
			}

			return true;
		}
	}

}