using System;

namespace DotCraftCore.World.Gen.Feature
{

	using Block = DotCraftCore.block.Block;
	using World = DotCraftCore.World.World;

	public class WorldGenFlowers : WorldGenerator
	{
		private Block field_150552_a;
		private int field_150551_b;
		private const string __OBFID = "CL_00000410";

		public WorldGenFlowers(Block p_i45452_1_)
		{
			this.field_150552_a = p_i45452_1_;
		}

		public virtual void func_150550_a(Block p_150550_1_, int p_150550_2_)
		{
			this.field_150552_a = p_150550_1_;
			this.field_150551_b = p_150550_2_;
		}

		public override bool generate(World p_76484_1_, Random p_76484_2_, int p_76484_3_, int p_76484_4_, int p_76484_5_)
		{
			for (int var6 = 0; var6 < 64; ++var6)
			{
				int var7 = p_76484_3_ + p_76484_2_.Next(8) - p_76484_2_.Next(8);
				int var8 = p_76484_4_ + p_76484_2_.Next(4) - p_76484_2_.Next(4);
				int var9 = p_76484_5_ + p_76484_2_.Next(8) - p_76484_2_.Next(8);

				if (p_76484_1_.isAirBlock(var7, var8, var9) && (!p_76484_1_.provider.hasNoSky || var8 < 255) && this.field_150552_a.canBlockStay(p_76484_1_, var7, var8, var9))
				{
					p_76484_1_.setBlock(var7, var8, var9, this.field_150552_a, this.field_150551_b, 2);
				}
			}

			return true;
		}
	}

}