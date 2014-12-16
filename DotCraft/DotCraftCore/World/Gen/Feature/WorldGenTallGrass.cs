using System;

namespace DotCraftCore.World.Gen.Feature
{

	using Block = DotCraftCore.block.Block;
	using Material = DotCraftCore.block.material.Material;
	using World = DotCraftCore.World.World;

	public class WorldGenTallGrass : WorldGenerator
	{
		private Block field_150522_a;
		private int tallGrassMetadata;
		

		public WorldGenTallGrass(Block p_i45466_1_, int p_i45466_2_)
		{
			this.field_150522_a = p_i45466_1_;
			this.tallGrassMetadata = p_i45466_2_;
		}

		public override bool generate(World p_76484_1_, Random p_76484_2_, int p_76484_3_, int p_76484_4_, int p_76484_5_)
		{
			Block var6;

			while (((var6 = p_76484_1_.getBlock(p_76484_3_, p_76484_4_, p_76484_5_)).Material == Material.air || var6.Material == Material.leaves) && p_76484_4_ > 0)
			{
				--p_76484_4_;
			}

			for (int var7 = 0; var7 < 128; ++var7)
			{
				int var8 = p_76484_3_ + p_76484_2_.Next(8) - p_76484_2_.Next(8);
				int var9 = p_76484_4_ + p_76484_2_.Next(4) - p_76484_2_.Next(4);
				int var10 = p_76484_5_ + p_76484_2_.Next(8) - p_76484_2_.Next(8);

				if (p_76484_1_.isAirBlock(var8, var9, var10) && this.field_150522_a.canBlockStay(p_76484_1_, var8, var9, var10))
				{
					p_76484_1_.setBlock(var8, var9, var10, this.field_150522_a, this.tallGrassMetadata, 2);
				}
			}

			return true;
		}
	}

}