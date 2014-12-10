using System;

namespace DotCraftCore.World.Gen.Feature
{

	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.World.World;

	public class WorldGenDoublePlant : WorldGenerator
	{
		private int field_150549_a;
		private const string __OBFID = "CL_00000408";

		public virtual void func_150548_a(int p_150548_1_)
		{
			this.field_150549_a = p_150548_1_;
		}

		public override bool generate(World p_76484_1_, Random p_76484_2_, int p_76484_3_, int p_76484_4_, int p_76484_5_)
		{
			bool var6 = false;

			for (int var7 = 0; var7 < 64; ++var7)
			{
				int var8 = p_76484_3_ + p_76484_2_.Next(8) - p_76484_2_.Next(8);
				int var9 = p_76484_4_ + p_76484_2_.Next(4) - p_76484_2_.Next(4);
				int var10 = p_76484_5_ + p_76484_2_.Next(8) - p_76484_2_.Next(8);

				if (p_76484_1_.isAirBlock(var8, var9, var10) && (!p_76484_1_.provider.hasNoSky || var9 < 254) && Blocks.double_plant.canPlaceBlockAt(p_76484_1_, var8, var9, var10))
				{
					Blocks.double_plant.func_149889_c(p_76484_1_, var8, var9, var10, this.field_150549_a, 2);
					var6 = true;
				}
			}

			return var6;
		}
	}

}