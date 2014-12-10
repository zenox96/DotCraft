using System;

namespace DotCraftCore.World.Biome
{

	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.World.World;
	using WorldGenDesertWells = DotCraftCore.World.Gen.Feature.WorldGenDesertWells;

	public class BiomeGenDesert : BiomeGenBase
	{
		private const string __OBFID = "CL_00000167";

		public BiomeGenDesert(int p_i1977_1_) : base(p_i1977_1_)
		{
			this.spawnableCreatureList.clear();
			this.topBlock = Blocks.sand;
			this.fillerBlock = Blocks.sand;
			this.theBiomeDecorator.treesPerChunk = -999;
			this.theBiomeDecorator.deadBushPerChunk = 2;
			this.theBiomeDecorator.reedsPerChunk = 50;
			this.theBiomeDecorator.cactiPerChunk = 10;
			this.spawnableCreatureList.clear();
		}

		public virtual void decorate(World p_76728_1_, Random p_76728_2_, int p_76728_3_, int p_76728_4_)
		{
			base.decorate(p_76728_1_, p_76728_2_, p_76728_3_, p_76728_4_);

			if (p_76728_2_.Next(1000) == 0)
			{
				int var5 = p_76728_3_ + p_76728_2_.Next(16) + 8;
				int var6 = p_76728_4_ + p_76728_2_.Next(16) + 8;
				WorldGenDesertWells var7 = new WorldGenDesertWells();
				var7.generate(p_76728_1_, p_76728_2_, var5, p_76728_1_.getHeightValue(var5, var6) + 1, var6);
			}
		}
	}

}