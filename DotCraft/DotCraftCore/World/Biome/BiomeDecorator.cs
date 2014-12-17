using System;

namespace DotCraftCore.nWorld.nBiome
{

	using BlockFlower = DotCraftCore.nBlock.BlockFlower;
	using Material = DotCraftCore.nBlock.nMaterial.Material;
	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.nWorld.World;
	using WorldGenAbstractTree = DotCraftCore.nWorld.nGen.nFeature.WorldGenAbstractTree;
	using WorldGenBigMushroom = DotCraftCore.nWorld.nGen.nFeature.WorldGenBigMushroom;
	using WorldGenCactus = DotCraftCore.nWorld.nGen.nFeature.WorldGenCactus;
	using WorldGenClay = DotCraftCore.nWorld.nGen.nFeature.WorldGenClay;
	using WorldGenDeadBush = DotCraftCore.nWorld.nGen.nFeature.WorldGenDeadBush;
	using WorldGenFlowers = DotCraftCore.nWorld.nGen.nFeature.WorldGenFlowers;
	using WorldGenLiquids = DotCraftCore.nWorld.nGen.nFeature.WorldGenLiquids;
	using WorldGenMinable = DotCraftCore.nWorld.nGen.nFeature.WorldGenMinable;
	using WorldGenPumpkin = DotCraftCore.nWorld.nGen.nFeature.WorldGenPumpkin;
	using WorldGenReed = DotCraftCore.nWorld.nGen.nFeature.WorldGenReed;
	using WorldGenSand = DotCraftCore.nWorld.nGen.nFeature.WorldGenSand;
	using WorldGenWaterlily = DotCraftCore.nWorld.nGen.nFeature.WorldGenWaterlily;
	using WorldGenerator = DotCraftCore.nWorld.nGen.nFeature.WorldGenerator;

	public class BiomeDecorator
	{
	/// <summary> The world the BiomeDecorator is currently decorating  </summary>
		protected internal World currentWorld;

	/// <summary> The Biome Decorator's random number generator.  </summary>
		protected internal Random randomGenerator;

	/// <summary> The X-coordinate of the chunk currently being decorated  </summary>
		protected internal int chunk_X;

	/// <summary> The Z-coordinate of the chunk currently being decorated  </summary>
		protected internal int chunk_Z;

	/// <summary> The clay generator.  </summary>
		protected internal WorldGenerator clayGen = new WorldGenClay(4);

	/// <summary> The sand generator.  </summary>
		protected internal WorldGenerator sandGen;

	/// <summary> The gravel generator.  </summary>
		protected internal WorldGenerator gravelAsSandGen;

	/// <summary> The dirt generator.  </summary>
		protected internal WorldGenerator dirtGen;
		protected internal WorldGenerator gravelGen;
		protected internal WorldGenerator coalGen;
		protected internal WorldGenerator ironGen;

	/// <summary> Field that holds gold WorldGenMinable  </summary>
		protected internal WorldGenerator goldGen;

	/// <summary> Field that holds redstone WorldGenMinable  </summary>
		protected internal WorldGenerator redstoneGen;

	/// <summary> Field that holds diamond WorldGenMinable  </summary>
		protected internal WorldGenerator diamondGen;

	/// <summary> Field that holds Lapis WorldGenMinable  </summary>
		protected internal WorldGenerator lapisGen;
		protected internal WorldGenFlowers field_150514_p;

	/// <summary> Field that holds mushroomBrown WorldGenFlowers  </summary>
		protected internal WorldGenerator mushroomBrownGen;

	/// <summary> Field that holds mushroomRed WorldGenFlowers  </summary>
		protected internal WorldGenerator mushroomRedGen;

	/// <summary> Field that holds big mushroom generator  </summary>
		protected internal WorldGenerator bigMushroomGen;

	/// <summary> Field that holds WorldGenReed  </summary>
		protected internal WorldGenerator reedGen;

	/// <summary> Field that holds WorldGenCactus  </summary>
		protected internal WorldGenerator cactusGen;

	/// <summary> The water lily generation!  </summary>
		protected internal WorldGenerator waterlilyGen;

	/// <summary> Amount of waterlilys per chunk.  </summary>
		protected internal int waterlilyPerChunk;

///    
///     <summary> * The number of trees to attempt to generate per chunk. Up to 10 in forests, none in deserts. </summary>
///     
		protected internal int treesPerChunk;

///    
///     <summary> * The number of yellow flower patches to generate per chunk. The game generates much less than this number, since
///     * it attempts to generate them at a random altitude. </summary>
///     
		protected internal int flowersPerChunk;

	/// <summary> The amount of tall grass to generate per chunk.  </summary>
		protected internal int grassPerChunk;

///    
///     <summary> * The number of dead bushes to generate per chunk. Used in deserts and swamps. </summary>
///     
		protected internal int deadBushPerChunk;

///    
///     <summary> * The number of extra mushroom patches per chunk. It generates 1/4 this number in brown mushroom patches, and 1/8
///     * this number in red mushroom patches. These mushrooms go beyond the default base number of mushrooms. </summary>
///     
		protected internal int mushroomsPerChunk;

///    
///     <summary> * The number of reeds to generate per chunk. Reeds won't generate if the randomly selected placement is unsuitable. </summary>
///     
		protected internal int reedsPerChunk;

///    
///     <summary> * The number of cactus plants to generate per chunk. Cacti only work on sand. </summary>
///     
		protected internal int cactiPerChunk;

///    
///     <summary> * The number of sand patches to generate per chunk. Sand patches only generate when part of it is underwater. </summary>
///     
		protected internal int sandPerChunk;

///    
///     <summary> * The number of sand patches to generate per chunk. Sand patches only generate when part of it is underwater. There
///     * appear to be two separate fields for this. </summary>
///     
		protected internal int sandPerChunk2;

///    
///     <summary> * The number of clay patches to generate per chunk. Only generates when part of it is underwater. </summary>
///     
		protected internal int clayPerChunk;

	/// <summary> Amount of big mushrooms per chunk  </summary>
		protected internal int bigMushroomsPerChunk;

	/// <summary> True if decorator should generate surface lava & water  </summary>
		public bool generateLakes;
		

		public BiomeDecorator()
		{
			this.sandGen = new WorldGenSand(Blocks.sand, 7);
			this.gravelAsSandGen = new WorldGenSand(Blocks.gravel, 6);
			this.dirtGen = new WorldGenMinable(Blocks.dirt, 32);
			this.gravelGen = new WorldGenMinable(Blocks.gravel, 32);
			this.coalGen = new WorldGenMinable(Blocks.coal_ore, 16);
			this.ironGen = new WorldGenMinable(Blocks.iron_ore, 8);
			this.goldGen = new WorldGenMinable(Blocks.gold_ore, 8);
			this.redstoneGen = new WorldGenMinable(Blocks.redstone_ore, 7);
			this.diamondGen = new WorldGenMinable(Blocks.diamond_ore, 7);
			this.lapisGen = new WorldGenMinable(Blocks.lapis_ore, 6);
			this.field_150514_p = new WorldGenFlowers(Blocks.yellow_flower);
			this.mushroomBrownGen = new WorldGenFlowers(Blocks.brown_mushroom);
			this.mushroomRedGen = new WorldGenFlowers(Blocks.red_mushroom);
			this.bigMushroomGen = new WorldGenBigMushroom();
			this.reedGen = new WorldGenReed();
			this.cactusGen = new WorldGenCactus();
			this.waterlilyGen = new WorldGenWaterlily();
			this.flowersPerChunk = 2;
			this.grassPerChunk = 1;
			this.sandPerChunk = 1;
			this.sandPerChunk2 = 3;
			this.clayPerChunk = 1;
			this.generateLakes = true;
		}

		public virtual void func_150512_a(World p_150512_1_, Random p_150512_2_, BiomeGenBase p_150512_3_, int p_150512_4_, int p_150512_5_)
		{
			if (this.currentWorld != null)
			{
				throw new Exception("Already decorating!!");
			}
			else
			{
				this.currentWorld = p_150512_1_;
				this.randomGenerator = p_150512_2_;
				this.chunk_X = p_150512_4_;
				this.chunk_Z = p_150512_5_;
				this.func_150513_a(p_150512_3_);
				this.currentWorld = null;
				this.randomGenerator = null;
			}
		}

		protected internal virtual void func_150513_a(BiomeGenBase p_150513_1_)
		{
			this.generateOres();
			int var2;
			int var3;
			int var4;

			for (var2 = 0; var2 < this.sandPerChunk2; ++var2)
			{
				var3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				var4 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				this.sandGen.generate(this.currentWorld, this.randomGenerator, var3, this.currentWorld.getTopSolidOrLiquidBlock(var3, var4), var4);
			}

			for (var2 = 0; var2 < this.clayPerChunk; ++var2)
			{
				var3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				var4 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				this.clayGen.generate(this.currentWorld, this.randomGenerator, var3, this.currentWorld.getTopSolidOrLiquidBlock(var3, var4), var4);
			}

			for (var2 = 0; var2 < this.sandPerChunk; ++var2)
			{
				var3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				var4 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				this.gravelAsSandGen.generate(this.currentWorld, this.randomGenerator, var3, this.currentWorld.getTopSolidOrLiquidBlock(var3, var4), var4);
			}

			var2 = this.treesPerChunk;

			if (this.randomGenerator.Next(10) == 0)
			{
				++var2;
			}

			int var5;
			int var6;

			for (var3 = 0; var3 < var2; ++var3)
			{
				var4 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				var5 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				var6 = this.currentWorld.getHeightValue(var4, var5);
				WorldGenAbstractTree var7 = p_150513_1_.func_150567_a(this.randomGenerator);
				var7.setScale(1.0D, 1.0D, 1.0D);

				if (var7.generate(this.currentWorld, this.randomGenerator, var4, var6, var5))
				{
					var7.func_150524_b(this.currentWorld, this.randomGenerator, var4, var6, var5);
				}
			}

			for (var3 = 0; var3 < this.bigMushroomsPerChunk; ++var3)
			{
				var4 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				var5 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				this.bigMushroomGen.generate(this.currentWorld, this.randomGenerator, var4, this.currentWorld.getHeightValue(var4, var5), var5);
			}

			for (var3 = 0; var3 < this.flowersPerChunk; ++var3)
			{
				var4 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				var5 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				var6 = this.randomGenerator.Next(this.currentWorld.getHeightValue(var4, var5) + 32);
				string var9 = p_150513_1_.func_150572_a(this.randomGenerator, var4, var6, var5);
				BlockFlower var8 = BlockFlower.func_149857_e(var9);

				if (var8.Material != Material.air)
				{
					this.field_150514_p.func_150550_a(var8, BlockFlower.func_149856_f(var9));
					this.field_150514_p.generate(this.currentWorld, this.randomGenerator, var4, var6, var5);
				}
			}

			for (var3 = 0; var3 < this.grassPerChunk; ++var3)
			{
				var4 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				var5 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				var6 = this.randomGenerator.Next(this.currentWorld.getHeightValue(var4, var5) * 2);
				WorldGenerator var10 = p_150513_1_.getRandomWorldGenForGrass(this.randomGenerator);
				var10.generate(this.currentWorld, this.randomGenerator, var4, var6, var5);
			}

			for (var3 = 0; var3 < this.deadBushPerChunk; ++var3)
			{
				var4 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				var5 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				var6 = this.randomGenerator.Next(this.currentWorld.getHeightValue(var4, var5) * 2);
				(new WorldGenDeadBush(Blocks.deadbush)).generate(this.currentWorld, this.randomGenerator, var4, var6, var5);
			}

			for (var3 = 0; var3 < this.waterlilyPerChunk; ++var3)
			{
				var4 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				var5 = this.chunk_Z + this.randomGenerator.Next(16) + 8;

				for (var6 = this.randomGenerator.Next(this.currentWorld.getHeightValue(var4, var5) * 2); var6 > 0 && this.currentWorld.isAirBlock(var4, var6 - 1, var5); --var6)
				{
					;
				}

				this.waterlilyGen.generate(this.currentWorld, this.randomGenerator, var4, var6, var5);
			}

			for (var3 = 0; var3 < this.mushroomsPerChunk; ++var3)
			{
				if (this.randomGenerator.Next(4) == 0)
				{
					var4 = this.chunk_X + this.randomGenerator.Next(16) + 8;
					var5 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
					var6 = this.currentWorld.getHeightValue(var4, var5);
					this.mushroomBrownGen.generate(this.currentWorld, this.randomGenerator, var4, var6, var5);
				}

				if (this.randomGenerator.Next(8) == 0)
				{
					var4 = this.chunk_X + this.randomGenerator.Next(16) + 8;
					var5 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
					var6 = this.randomGenerator.Next(this.currentWorld.getHeightValue(var4, var5) * 2);
					this.mushroomRedGen.generate(this.currentWorld, this.randomGenerator, var4, var6, var5);
				}
			}

			if (this.randomGenerator.Next(4) == 0)
			{
				var3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				var4 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				var5 = this.randomGenerator.Next(this.currentWorld.getHeightValue(var3, var4) * 2);
				this.mushroomBrownGen.generate(this.currentWorld, this.randomGenerator, var3, var5, var4);
			}

			if (this.randomGenerator.Next(8) == 0)
			{
				var3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				var4 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				var5 = this.randomGenerator.Next(this.currentWorld.getHeightValue(var3, var4) * 2);
				this.mushroomRedGen.generate(this.currentWorld, this.randomGenerator, var3, var5, var4);
			}

			for (var3 = 0; var3 < this.reedsPerChunk; ++var3)
			{
				var4 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				var5 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				var6 = this.randomGenerator.Next(this.currentWorld.getHeightValue(var4, var5) * 2);
				this.reedGen.generate(this.currentWorld, this.randomGenerator, var4, var6, var5);
			}

			for (var3 = 0; var3 < 10; ++var3)
			{
				var4 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				var5 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				var6 = this.randomGenerator.Next(this.currentWorld.getHeightValue(var4, var5) * 2);
				this.reedGen.generate(this.currentWorld, this.randomGenerator, var4, var6, var5);
			}

			if (this.randomGenerator.Next(32) == 0)
			{
				var3 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				var4 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				var5 = this.randomGenerator.Next(this.currentWorld.getHeightValue(var3, var4) * 2);
				(new WorldGenPumpkin()).generate(this.currentWorld, this.randomGenerator, var3, var5, var4);
			}

			for (var3 = 0; var3 < this.cactiPerChunk; ++var3)
			{
				var4 = this.chunk_X + this.randomGenerator.Next(16) + 8;
				var5 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
				var6 = this.randomGenerator.Next(this.currentWorld.getHeightValue(var4, var5) * 2);
				this.cactusGen.generate(this.currentWorld, this.randomGenerator, var4, var6, var5);
			}

			if (this.generateLakes)
			{
				for (var3 = 0; var3 < 50; ++var3)
				{
					var4 = this.chunk_X + this.randomGenerator.Next(16) + 8;
					var5 = this.randomGenerator.Next(this.randomGenerator.Next(248) + 8);
					var6 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
					(new WorldGenLiquids(Blocks.flowing_water)).generate(this.currentWorld, this.randomGenerator, var4, var5, var6);
				}

				for (var3 = 0; var3 < 20; ++var3)
				{
					var4 = this.chunk_X + this.randomGenerator.Next(16) + 8;
					var5 = this.randomGenerator.Next(this.randomGenerator.Next(this.randomGenerator.Next(240) + 8) + 8);
					var6 = this.chunk_Z + this.randomGenerator.Next(16) + 8;
					(new WorldGenLiquids(Blocks.flowing_lava)).generate(this.currentWorld, this.randomGenerator, var4, var5, var6);
				}
			}
		}

///    
///     <summary> * Standard ore generation helper. Generates most ores. </summary>
///     
		protected internal virtual void genStandardOre1(int p_76795_1_, WorldGenerator p_76795_2_, int p_76795_3_, int p_76795_4_)
		{
			for (int var5 = 0; var5 < p_76795_1_; ++var5)
			{
				int var6 = this.chunk_X + this.randomGenerator.Next(16);
				int var7 = this.randomGenerator.Next(p_76795_4_ - p_76795_3_) + p_76795_3_;
				int var8 = this.chunk_Z + this.randomGenerator.Next(16);
				p_76795_2_.generate(this.currentWorld, this.randomGenerator, var6, var7, var8);
			}
		}

///    
///     <summary> * Standard ore generation helper. Generates Lapis Lazuli. </summary>
///     
		protected internal virtual void genStandardOre2(int p_76793_1_, WorldGenerator p_76793_2_, int p_76793_3_, int p_76793_4_)
		{
			for (int var5 = 0; var5 < p_76793_1_; ++var5)
			{
				int var6 = this.chunk_X + this.randomGenerator.Next(16);
				int var7 = this.randomGenerator.Next(p_76793_4_) + this.randomGenerator.Next(p_76793_4_) + (p_76793_3_ - p_76793_4_);
				int var8 = this.chunk_Z + this.randomGenerator.Next(16);
				p_76793_2_.generate(this.currentWorld, this.randomGenerator, var6, var7, var8);
			}
		}

///    
///     <summary> * Generates ores in the current chunk </summary>
///     
		protected internal virtual void generateOres()
		{
			this.genStandardOre1(20, this.dirtGen, 0, 256);
			this.genStandardOre1(10, this.gravelGen, 0, 256);
			this.genStandardOre1(20, this.coalGen, 0, 128);
			this.genStandardOre1(20, this.ironGen, 0, 64);
			this.genStandardOre1(2, this.goldGen, 0, 32);
			this.genStandardOre1(8, this.redstoneGen, 0, 16);
			this.genStandardOre1(1, this.diamondGen, 0, 16);
			this.genStandardOre2(1, this.lapisGen, 16, 16);
		}
	}

}