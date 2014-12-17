using System;

namespace DotCraftCore.nWorld.nBiome
{

	using EntityChicken = DotCraftCore.entity.passive.EntityChicken;
	using EntityOcelot = DotCraftCore.entity.passive.EntityOcelot;
	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.nWorld.World;
	using WorldGenAbstractTree = DotCraftCore.nWorld.nGen.nFeature.WorldGenAbstractTree;
	using WorldGenMegaJungle = DotCraftCore.nWorld.nGen.nFeature.WorldGenMegaJungle;
	using WorldGenMelon = DotCraftCore.nWorld.nGen.nFeature.WorldGenMelon;
	using WorldGenShrub = DotCraftCore.nWorld.nGen.nFeature.WorldGenShrub;
	using WorldGenTallGrass = DotCraftCore.nWorld.nGen.nFeature.WorldGenTallGrass;
	using WorldGenTrees = DotCraftCore.nWorld.nGen.nFeature.WorldGenTrees;
	using WorldGenVines = DotCraftCore.nWorld.nGen.nFeature.WorldGenVines;
	using WorldGenerator = DotCraftCore.nWorld.nGen.nFeature.WorldGenerator;

	public class BiomeGenJungle : BiomeGenBase
	{
		private bool field_150614_aC;
		

		public BiomeGenJungle(int p_i45379_1_, bool p_i45379_2_) : base(p_i45379_1_)
		{
			this.field_150614_aC = p_i45379_2_;

			if (p_i45379_2_)
			{
				this.theBiomeDecorator.treesPerChunk = 2;
			}
			else
			{
				this.theBiomeDecorator.treesPerChunk = 50;
			}

			this.theBiomeDecorator.grassPerChunk = 25;
			this.theBiomeDecorator.flowersPerChunk = 4;

			if (!p_i45379_2_)
			{
				this.spawnableMonsterList.add(new BiomeGenBase.SpawnListEntry(typeof(EntityOcelot), 2, 1, 1));
			}

			this.spawnableCreatureList.add(new BiomeGenBase.SpawnListEntry(typeof(EntityChicken), 10, 4, 4));
		}

		public virtual WorldGenAbstractTree func_150567_a(Random p_150567_1_)
		{
			return (WorldGenAbstractTree)(p_150567_1_.Next(10) == 0 ? this.worldGeneratorBigTree : (p_150567_1_.Next(2) == 0 ? new WorldGenShrub(3, 0) : (!this.field_150614_aC && p_150567_1_.Next(3) == 0 ? new WorldGenMegaJungle(false, 10, 20, 3, 3) : new WorldGenTrees(false, 4 + p_150567_1_.Next(7), 3, 3, true))));
		}

///    
///     <summary> * Gets a WorldGen appropriate for this biome. </summary>
///     
		public virtual WorldGenerator getRandomWorldGenForGrass(Random p_76730_1_)
		{
			return p_76730_1_.Next(4) == 0 ? new WorldGenTallGrass(Blocks.tallgrass, 2) : new WorldGenTallGrass(Blocks.tallgrass, 1);
		}

		public virtual void decorate(World p_76728_1_, Random p_76728_2_, int p_76728_3_, int p_76728_4_)
		{
			base.decorate(p_76728_1_, p_76728_2_, p_76728_3_, p_76728_4_);
			int var5 = p_76728_3_ + p_76728_2_.Next(16) + 8;
			int var6 = p_76728_4_ + p_76728_2_.Next(16) + 8;
			int var7 = p_76728_2_.Next(p_76728_1_.getHeightValue(var5, var6) * 2);
			(new WorldGenMelon()).generate(p_76728_1_, p_76728_2_, var5, var7, var6);
			WorldGenVines var10 = new WorldGenVines();

			for (var6 = 0; var6 < 50; ++var6)
			{
				var7 = p_76728_3_ + p_76728_2_.Next(16) + 8;
				short var8 = 128;
				int var9 = p_76728_4_ + p_76728_2_.Next(16) + 8;
				var10.generate(p_76728_1_, p_76728_2_, var7, var8, var9);
			}
		}
	}

}