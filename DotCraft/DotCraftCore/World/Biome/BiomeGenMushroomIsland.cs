namespace DotCraftCore.World.Biome
{

	using EntityMooshroom = DotCraftCore.entity.passive.EntityMooshroom;
	using Blocks = DotCraftCore.init.Blocks;

	public class BiomeGenMushroomIsland : BiomeGenBase
	{
		private const string __OBFID = "CL_00000177";

		public BiomeGenMushroomIsland(int p_i1984_1_) : base(p_i1984_1_)
		{
			this.theBiomeDecorator.treesPerChunk = -100;
			this.theBiomeDecorator.flowersPerChunk = -100;
			this.theBiomeDecorator.grassPerChunk = -100;
			this.theBiomeDecorator.mushroomsPerChunk = 1;
			this.theBiomeDecorator.bigMushroomsPerChunk = 1;
			this.topBlock = Blocks.mycelium;
			this.spawnableMonsterList.clear();
			this.spawnableCreatureList.clear();
			this.spawnableWaterCreatureList.clear();
			this.spawnableCreatureList.add(new BiomeGenBase.SpawnListEntry(typeof(EntityMooshroom), 8, 4, 8));
		}
	}

}