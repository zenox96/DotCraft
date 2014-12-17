using System;

namespace DotCraftCore.nWorld.nGen.nFeature
{

	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.nWorld.World;

	public class WorldGenMelon : WorldGenerator
	{
		

		public override bool generate(World p_76484_1_, Random p_76484_2_, int p_76484_3_, int p_76484_4_, int p_76484_5_)
		{
			for (int var6 = 0; var6 < 64; ++var6)
			{
				int var7 = p_76484_3_ + p_76484_2_.Next(8) - p_76484_2_.Next(8);
				int var8 = p_76484_4_ + p_76484_2_.Next(4) - p_76484_2_.Next(4);
				int var9 = p_76484_5_ + p_76484_2_.Next(8) - p_76484_2_.Next(8);

				if (Blocks.melon_block.canPlaceBlockAt(p_76484_1_, var7, var8, var9) && p_76484_1_.getBlock(var7, var8 - 1, var9) == Blocks.grass)
				{
					p_76484_1_.setBlock(var7, var8, var9, Blocks.melon_block, 0, 2);
				}
			}

			return true;
		}
	}

}