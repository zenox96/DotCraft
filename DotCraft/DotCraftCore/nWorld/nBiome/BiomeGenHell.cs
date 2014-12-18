namespace DotCraftCore.nWorld.nBiome
{

	using EntityGhast = DotCraftCore.entity.monster.EntityGhast;
	using EntityMagmaCube = DotCraftCore.entity.monster.EntityMagmaCube;
	using EntityPigZombie = DotCraftCore.entity.monster.EntityPigZombie;

	public class BiomeGenHell : BiomeGenBase
	{
		

		public BiomeGenHell(int p_i1981_1_) : base(p_i1981_1_)
		{
			this.spawnableMonsterList.clear();
			this.spawnableCreatureList.clear();
			this.spawnableWaterCreatureList.clear();
			this.spawnableCaveCreatureList.clear();
			this.spawnableMonsterList.add(new BiomeGenBase.SpawnListEntry(typeof(EntityGhast), 50, 4, 4));
			this.spawnableMonsterList.add(new BiomeGenBase.SpawnListEntry(typeof(EntityPigZombie), 100, 4, 4));
			this.spawnableMonsterList.add(new BiomeGenBase.SpawnListEntry(typeof(EntityMagmaCube), 1, 4, 4));
		}
	}

}