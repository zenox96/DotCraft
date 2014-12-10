using System;

namespace DotCraftCore.World.Biome
{

	using Block = DotCraftCore.block.Block;
	using EntityWolf = DotCraftCore.entity.passive.EntityWolf;
	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.World.World;
	using WorldGenAbstractTree = DotCraftCore.World.Gen.Feature.WorldGenAbstractTree;
	using WorldGenBlockBlob = DotCraftCore.World.Gen.Feature.WorldGenBlockBlob;
	using WorldGenMegaPineTree = DotCraftCore.World.Gen.Feature.WorldGenMegaPineTree;
	using WorldGenTaiga1 = DotCraftCore.World.Gen.Feature.WorldGenTaiga1;
	using WorldGenTaiga2 = DotCraftCore.World.Gen.Feature.WorldGenTaiga2;
	using WorldGenTallGrass = DotCraftCore.World.Gen.Feature.WorldGenTallGrass;
	using WorldGenerator = DotCraftCore.World.Gen.Feature.WorldGenerator;

	public class BiomeGenTaiga : BiomeGenBase
	{
		private static readonly WorldGenTaiga1 field_150639_aC = new WorldGenTaiga1();
		private static readonly WorldGenTaiga2 field_150640_aD = new WorldGenTaiga2(false);
		private static readonly WorldGenMegaPineTree field_150641_aE = new WorldGenMegaPineTree(false, false);
		private static readonly WorldGenMegaPineTree field_150642_aF = new WorldGenMegaPineTree(false, true);
		private static readonly WorldGenBlockBlob field_150643_aG = new WorldGenBlockBlob(Blocks.mossy_cobblestone, 0);
		private int field_150644_aH;
		private const string __OBFID = "CL_00000186";

		public BiomeGenTaiga(int p_i45385_1_, int p_i45385_2_) : base(p_i45385_1_)
		{
			this.field_150644_aH = p_i45385_2_;
			this.spawnableCreatureList.add(new BiomeGenBase.SpawnListEntry(typeof(EntityWolf), 8, 4, 4));
			this.theBiomeDecorator.treesPerChunk = 10;

			if (p_i45385_2_ != 1 && p_i45385_2_ != 2)
			{
				this.theBiomeDecorator.grassPerChunk = 1;
				this.theBiomeDecorator.mushroomsPerChunk = 1;
			}
			else
			{
				this.theBiomeDecorator.grassPerChunk = 7;
				this.theBiomeDecorator.deadBushPerChunk = 1;
				this.theBiomeDecorator.mushroomsPerChunk = 3;
			}
		}

		public virtual WorldGenAbstractTree func_150567_a(Random p_150567_1_)
		{
			return (WorldGenAbstractTree)((this.field_150644_aH == 1 || this.field_150644_aH == 2) && p_150567_1_.Next(3) == 0 ? (this.field_150644_aH != 2 && p_150567_1_.Next(13) != 0 ? field_150641_aE : field_150642_aF) : (p_150567_1_.Next(3) == 0 ? field_150639_aC : field_150640_aD));
		}

///    
///     <summary> * Gets a WorldGen appropriate for this biome. </summary>
///     
		public virtual WorldGenerator getRandomWorldGenForGrass(Random p_76730_1_)
		{
			return p_76730_1_.Next(5) > 0 ? new WorldGenTallGrass(Blocks.tallgrass, 2) : new WorldGenTallGrass(Blocks.tallgrass, 1);
		}

		public virtual void decorate(World p_76728_1_, Random p_76728_2_, int p_76728_3_, int p_76728_4_)
		{
			int var5;
			int var6;
			int var7;
			int var8;

			if (this.field_150644_aH == 1 || this.field_150644_aH == 2)
			{
				var5 = p_76728_2_.Next(3);

				for (var6 = 0; var6 < var5; ++var6)
				{
					var7 = p_76728_3_ + p_76728_2_.Next(16) + 8;
					var8 = p_76728_4_ + p_76728_2_.Next(16) + 8;
					int var9 = p_76728_1_.getHeightValue(var7, var8);
					field_150643_aG.generate(p_76728_1_, p_76728_2_, var7, var9, var8);
				}
			}

			field_150610_ae.func_150548_a(3);

			for (var5 = 0; var5 < 7; ++var5)
			{
				var6 = p_76728_3_ + p_76728_2_.Next(16) + 8;
				var7 = p_76728_4_ + p_76728_2_.Next(16) + 8;
				var8 = p_76728_2_.Next(p_76728_1_.getHeightValue(var6, var7) + 32);
				field_150610_ae.generate(p_76728_1_, p_76728_2_, var6, var8, var7);
			}

			base.decorate(p_76728_1_, p_76728_2_, p_76728_3_, p_76728_4_);
		}

		public virtual void func_150573_a(World p_150573_1_, Random p_150573_2_, Block[] p_150573_3_, sbyte[] p_150573_4_, int p_150573_5_, int p_150573_6_, double p_150573_7_)
		{
			if (this.field_150644_aH == 1 || this.field_150644_aH == 2)
			{
				this.topBlock = Blocks.grass;
				this.field_150604_aj = 0;
				this.fillerBlock = Blocks.dirt;

				if (p_150573_7_ > 1.75D)
				{
					this.topBlock = Blocks.dirt;
					this.field_150604_aj = 1;
				}
				else if (p_150573_7_ > -0.95D)
				{
					this.topBlock = Blocks.dirt;
					this.field_150604_aj = 2;
				}
			}

			this.func_150560_b(p_150573_1_, p_150573_2_, p_150573_3_, p_150573_4_, p_150573_5_, p_150573_6_, p_150573_7_);
		}

		protected internal virtual BiomeGenBase func_150566_k()
		{
			return this.biomeID == BiomeGenBase.field_150578_U.biomeID ? (new BiomeGenTaiga(this.biomeID + 128, 2)).func_150557_a(5858897, true).setBiomeName("Mega Spruce Taiga").func_76733_a(5159473).setTemperatureRainfall(0.25F, 0.8F).func_150570_a(new BiomeGenBase.Height(this.minHeight, this.maxHeight)) : base.func_150566_k();
		}
	}

}