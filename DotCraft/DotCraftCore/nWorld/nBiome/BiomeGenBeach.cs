namespace DotCraftCore.nWorld.nBiome
{

	using Blocks = DotCraftCore.init.Blocks;

	public class BiomeGenBeach : BiomeGenBase
	{
		

		public BiomeGenBeach(int p_i1969_1_) : base(p_i1969_1_)
		{
			this.spawnableCreatureList.clear();
			this.topBlock = Blocks.sand;
			this.fillerBlock = Blocks.sand;
			this.theBiomeDecorator.treesPerChunk = -999;
			this.theBiomeDecorator.deadBushPerChunk = 0;
			this.theBiomeDecorator.reedsPerChunk = 0;
			this.theBiomeDecorator.cactiPerChunk = 0;
		}
	}

}