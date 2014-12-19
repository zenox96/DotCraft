using System;

namespace DotCraftCore.nWorld.nGen.nFeature
{

	using Block = DotCraftCore.nBlock.Block;
	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.nWorld.World;

	public class WorldGenIcePath : WorldGenerator
	{
		private Block field_150555_a;
		private int field_150554_b;
		

		public WorldGenIcePath(int p_i45454_1_)
		{
			this.field_150555_a = Blocks.packed_ice;
			this.field_150554_b = p_i45454_1_;
		}

		public override bool generate(World p_76484_1_, Random p_76484_2_, int p_76484_3_, int p_76484_4_, int p_76484_5_)
		{
			while (p_76484_1_.isAirBlock(p_76484_3_, p_76484_4_, p_76484_5_) && p_76484_4_ > 2)
			{
				--p_76484_4_;
			}

			if (p_76484_1_.getBlock(p_76484_3_, p_76484_4_, p_76484_5_) != Blocks.snow)
			{
				return false;
			}
			else
			{
				int var6 = p_76484_2_.Next(this.field_150554_b - 2) + 2;
				sbyte var7 = 1;

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

								if (var13 == Blocks.dirt || var13 == Blocks.snow || var13 == Blocks.ice)
								{
									p_76484_1_.setBlock(var8, var12, var9, this.field_150555_a, 0, 2);
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