namespace DotCraftCore.World.Biome
{

	using EntityEnderman = DotCraftCore.entity.monster.EntityEnderman;
	using Blocks = DotCraftCore.init.Blocks;

	public class BiomeGenEnd : BiomeGenBase
	{
		private const string __OBFID = "CL_00000187";

		public BiomeGenEnd(int p_i1990_1_) : base(p_i1990_1_)
		{
			this.spawnableMonsterList.clear();
			this.spawnableCreatureList.clear();
			this.spawnableWaterCreatureList.clear();
			this.spawnableCaveCreatureList.clear();
			this.spawnableMonsterList.add(new BiomeGenBase.SpawnListEntry(typeof(EntityEnderman), 10, 4, 4));
			this.topBlock = Blocks.dirt;
			this.fillerBlock = Blocks.dirt;
			this.theBiomeDecorator = new BiomeEndDecorator();
		}

///    
///     <summary> * takes temperature, returns color </summary>
///     
		public virtual int getSkyColorByTemp(float p_76731_1_)
		{
			return 0;
		}
	}

}