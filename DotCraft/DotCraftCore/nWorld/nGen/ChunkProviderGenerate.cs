using System;
using System.Collections;

namespace DotCraftCore.nWorld.nGen
{

	using Block = DotCraftCore.nBlock.Block;
	using BlockFalling = DotCraftCore.nBlock.BlockFalling;
	using EnumCreatureType = DotCraftCore.entity.EnumCreatureType;
	using Blocks = DotCraftCore.init.Blocks;
	using IProgressUpdate = DotCraftCore.nUtil.IProgressUpdate;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using ChunkPosition = DotCraftCore.nWorld.ChunkPosition;
	using SpawnerAnimals = DotCraftCore.nWorld.SpawnerAnimals;
	using World = DotCraftCore.nWorld.World;
	using WorldType = DotCraftCore.nWorld.WorldType;
	using BiomeGenBase = DotCraftCore.nWorld.nBiome.BiomeGenBase;
	using Chunk = DotCraftCore.nWorld.nChunk.Chunk;
	using IChunkProvider = DotCraftCore.nWorld.nChunk.IChunkProvider;
	using WorldGenDungeons = DotCraftCore.nWorld.nGen.nFeature.WorldGenDungeons;
	using WorldGenLakes = DotCraftCore.nWorld.nGen.nFeature.WorldGenLakes;
	using MapGenMineshaft = DotCraftCore.nWorld.nGen.nStructure.MapGenMineshaft;
	using MapGenScatteredFeature = DotCraftCore.nWorld.nGen.nStructure.MapGenScatteredFeature;
	using MapGenStronghold = DotCraftCore.nWorld.nGen.nStructure.MapGenStronghold;
	using MapGenVillage = DotCraftCore.nWorld.nGen.nStructure.MapGenVillage;

	public class ChunkProviderGenerate : IChunkProvider
	{
	/// <summary> RNG.  </summary>
		private Random rand;
		private NoiseGeneratorOctaves field_147431_j;
		private NoiseGeneratorOctaves field_147432_k;
		private NoiseGeneratorOctaves field_147429_l;
		private NoiseGeneratorPerlin field_147430_m;

	/// <summary> A NoiseGeneratorOctaves used in generating terrain  </summary>
		public NoiseGeneratorOctaves noiseGen5;

	/// <summary> A NoiseGeneratorOctaves used in generating terrain  </summary>
		public NoiseGeneratorOctaves noiseGen6;
		public NoiseGeneratorOctaves mobSpawnerNoise;

	/// <summary> Reference to the World object.  </summary>
		private World worldObj;

	/// <summary> are map structures going to be generated (e.g. strongholds)  </summary>
		private readonly bool mapFeaturesEnabled;
		private WorldType field_147435_p;
		private readonly double[] field_147434_q;
		private readonly float[] parabolicField;
		private double[] stoneNoise = new double[256];
		private MapGenBase caveGenerator = new MapGenCaves();

	/// <summary> Holds Stronghold Generator  </summary>
		private MapGenStronghold strongholdGenerator = new MapGenStronghold();

	/// <summary> Holds Village Generator  </summary>
		private MapGenVillage villageGenerator = new MapGenVillage();

	/// <summary> Holds Mineshaft Generator  </summary>
		private MapGenMineshaft mineshaftGenerator = new MapGenMineshaft();
		private MapGenScatteredFeature scatteredFeatureGenerator = new MapGenScatteredFeature();

	/// <summary> Holds ravine generator  </summary>
		private MapGenBase ravineGenerator = new MapGenRavine();

	/// <summary> The biomes that are used to generate the chunk  </summary>
		private BiomeGenBase[] biomesForGeneration;
		internal double[] field_147427_d;
		internal double[] field_147428_e;
		internal double[] field_147425_f;
		internal double[] field_147426_g;
//ORIGINAL LINE: internal int[][] field_73219_j = new int[32][32];
//JAVA TO VB & C# CONVERTER NOTE: The following call to the 'RectangularArrays' helper class reproduces the rectangular array initialization that is automatic in Java:
		internal int[][] field_73219_j = RectangularArrays.ReturnRectangularIntArray(32, 32);
		

		public ChunkProviderGenerate(World p_i2006_1_, long p_i2006_2_, bool p_i2006_4_)
		{
			this.worldObj = p_i2006_1_;
			this.mapFeaturesEnabled = p_i2006_4_;
			this.field_147435_p = p_i2006_1_.WorldInfo.TerrainType;
			this.rand = new Random(p_i2006_2_);
			this.field_147431_j = new NoiseGeneratorOctaves(this.rand, 16);
			this.field_147432_k = new NoiseGeneratorOctaves(this.rand, 16);
			this.field_147429_l = new NoiseGeneratorOctaves(this.rand, 8);
			this.field_147430_m = new NoiseGeneratorPerlin(this.rand, 4);
			this.noiseGen5 = new NoiseGeneratorOctaves(this.rand, 10);
			this.noiseGen6 = new NoiseGeneratorOctaves(this.rand, 16);
			this.mobSpawnerNoise = new NoiseGeneratorOctaves(this.rand, 8);
			this.field_147434_q = new double[825];
			this.parabolicField = new float[25];

			for (int var5 = -2; var5 <= 2; ++var5)
			{
				for (int var6 = -2; var6 <= 2; ++var6)
				{
					float var7 = 10.0F / MathHelper.sqrt_float((float)(var5 * var5 + var6 * var6) + 0.2F);
					this.parabolicField[var5 + 2 + (var6 + 2) * 5] = var7;
				}
			}
		}

		public virtual void func_147424_a(int p_147424_1_, int p_147424_2_, Block[] p_147424_3_)
		{
			sbyte var4 = 63;
			this.biomesForGeneration = this.worldObj.WorldChunkManager.getBiomesForGeneration(this.biomesForGeneration, p_147424_1_ * 4 - 2, p_147424_2_ * 4 - 2, 10, 10);
			this.func_147423_a(p_147424_1_ * 4, 0, p_147424_2_ * 4);

			for (int var5 = 0; var5 < 4; ++var5)
			{
				int var6 = var5 * 5;
				int var7 = (var5 + 1) * 5;

				for (int var8 = 0; var8 < 4; ++var8)
				{
					int var9 = (var6 + var8) * 33;
					int var10 = (var6 + var8 + 1) * 33;
					int var11 = (var7 + var8) * 33;
					int var12 = (var7 + var8 + 1) * 33;

					for (int var13 = 0; var13 < 32; ++var13)
					{
						double var14 = 0.125D;
						double var16 = this.field_147434_q[var9 + var13];
						double var18 = this.field_147434_q[var10 + var13];
						double var20 = this.field_147434_q[var11 + var13];
						double var22 = this.field_147434_q[var12 + var13];
						double var24 = (this.field_147434_q[var9 + var13 + 1] - var16) * var14;
						double var26 = (this.field_147434_q[var10 + var13 + 1] - var18) * var14;
						double var28 = (this.field_147434_q[var11 + var13 + 1] - var20) * var14;
						double var30 = (this.field_147434_q[var12 + var13 + 1] - var22) * var14;

						for (int var32 = 0; var32 < 8; ++var32)
						{
							double var33 = 0.25D;
							double var35 = var16;
							double var37 = var18;
							double var39 = (var20 - var16) * var33;
							double var41 = (var22 - var18) * var33;

							for (int var43 = 0; var43 < 4; ++var43)
							{
								int var44 = var43 + var5 * 4 << 12 | 0 + var8 * 4 << 8 | var13 * 8 + var32;
								short var45 = 256;
								var44 -= var45;
								double var46 = 0.25D;
								double var50 = (var37 - var35) * var46;
								double var48 = var35 - var50;

								for (int var52 = 0; var52 < 4; ++var52)
								{
									if ((var48 += var50) > 0.0D)
									{
										p_147424_3_[var44 += var45] = Blocks.stone;
									}
									else if (var13 * 8 + var32 < var4)
									{
										p_147424_3_[var44 += var45] = Blocks.water;
									}
									else
									{
										p_147424_3_[var44 += var45] = null;
									}
								}

								var35 += var39;
								var37 += var41;
							}

							var16 += var24;
							var18 += var26;
							var20 += var28;
							var22 += var30;
						}
					}
				}
			}
		}

		public virtual void func_147422_a(int p_147422_1_, int p_147422_2_, Block[] p_147422_3_, sbyte[] p_147422_4_, BiomeGenBase[] p_147422_5_)
		{
			double var6 = 0.03125D;
			this.stoneNoise = this.field_147430_m.func_151599_a(this.stoneNoise, (double)(p_147422_1_ * 16), (double)(p_147422_2_ * 16), 16, 16, var6 * 2.0D, var6 * 2.0D, 1.0D);

			for (int var8 = 0; var8 < 16; ++var8)
			{
				for (int var9 = 0; var9 < 16; ++var9)
				{
					BiomeGenBase var10 = p_147422_5_[var9 + var8 * 16];
					var10.func_150573_a(this.worldObj, this.rand, p_147422_3_, p_147422_4_, p_147422_1_ * 16 + var8, p_147422_2_ * 16 + var9, this.stoneNoise[var9 + var8 * 16]);
				}
			}
		}

///    
///     <summary> * loads or generates the chunk at the chunk location specified </summary>
///     
		public virtual Chunk loadChunk(int p_73158_1_, int p_73158_2_)
		{
			return this.provideChunk(p_73158_1_, p_73158_2_);
		}

///    
///     <summary> * Will return back a chunk, if it doesn't exist and its not a MP client it will generates all the blocks for the
///     * specified chunk from the map seed and chunk seed </summary>
///     
		public virtual Chunk provideChunk(int p_73154_1_, int p_73154_2_)
		{
			this.rand.Seed = (long)p_73154_1_ * 341873128712L + (long)p_73154_2_ * 132897987541L;
			Block[] var3 = new Block[65536];
			sbyte[] var4 = new sbyte[65536];
			this.func_147424_a(p_73154_1_, p_73154_2_, var3);
			this.biomesForGeneration = this.worldObj.WorldChunkManager.loadBlockGeneratorData(this.biomesForGeneration, p_73154_1_ * 16, p_73154_2_ * 16, 16, 16);
			this.func_147422_a(p_73154_1_, p_73154_2_, var3, var4, this.biomesForGeneration);
			this.caveGenerator.func_151539_a(this, this.worldObj, p_73154_1_, p_73154_2_, var3);
			this.ravineGenerator.func_151539_a(this, this.worldObj, p_73154_1_, p_73154_2_, var3);

			if (this.mapFeaturesEnabled)
			{
				this.mineshaftGenerator.func_151539_a(this, this.worldObj, p_73154_1_, p_73154_2_, var3);
				this.villageGenerator.func_151539_a(this, this.worldObj, p_73154_1_, p_73154_2_, var3);
				this.strongholdGenerator.func_151539_a(this, this.worldObj, p_73154_1_, p_73154_2_, var3);
				this.scatteredFeatureGenerator.func_151539_a(this, this.worldObj, p_73154_1_, p_73154_2_, var3);
			}

			Chunk var5 = new Chunk(this.worldObj, var3, var4, p_73154_1_, p_73154_2_);
			sbyte[] var6 = var5.BiomeArray;

			for (int var7 = 0; var7 < var6.Length; ++var7)
			{
				var6[var7] = (sbyte)this.biomesForGeneration[var7].biomeID;
			}

			var5.generateSkylightMap();
			return var5;
		}

		private void func_147423_a(int p_147423_1_, int p_147423_2_, int p_147423_3_)
		{
			double var4 = 684.412D;
			double var6 = 684.412D;
			double var8 = 512.0D;
			double var10 = 512.0D;
			this.field_147426_g = this.noiseGen6.generateNoiseOctaves(this.field_147426_g, p_147423_1_, p_147423_3_, 5, 5, 200.0D, 200.0D, 0.5D);
			this.field_147427_d = this.field_147429_l.generateNoiseOctaves(this.field_147427_d, p_147423_1_, p_147423_2_, p_147423_3_, 5, 33, 5, 8.555150000000001D, 4.277575000000001D, 8.555150000000001D);
			this.field_147428_e = this.field_147431_j.generateNoiseOctaves(this.field_147428_e, p_147423_1_, p_147423_2_, p_147423_3_, 5, 33, 5, 684.412D, 684.412D, 684.412D);
			this.field_147425_f = this.field_147432_k.generateNoiseOctaves(this.field_147425_f, p_147423_1_, p_147423_2_, p_147423_3_, 5, 33, 5, 684.412D, 684.412D, 684.412D);
			bool var45 = false;
			bool var44 = false;
			int var12 = 0;
			int var13 = 0;
			double var14 = 8.5D;

			for (int var16 = 0; var16 < 5; ++var16)
			{
				for (int var17 = 0; var17 < 5; ++var17)
				{
					float var18 = 0.0F;
					float var19 = 0.0F;
					float var20 = 0.0F;
					sbyte var21 = 2;
					BiomeGenBase var22 = this.biomesForGeneration[var16 + 2 + (var17 + 2) * 10];

					for (int var23 = -var21; var23 <= var21; ++var23)
					{
						for (int var24 = -var21; var24 <= var21; ++var24)
						{
							BiomeGenBase var25 = this.biomesForGeneration[var16 + var23 + 2 + (var17 + var24 + 2) * 10];
							float var26 = var25.minHeight;
							float var27 = var25.maxHeight;

							if (this.field_147435_p == WorldType.field_151360_e && var26 > 0.0F)
							{
								var26 = 1.0F + var26 * 2.0F;
								var27 = 1.0F + var27 * 4.0F;
							}

							float var28 = this.parabolicField[var23 + 2 + (var24 + 2) * 5] / (var26 + 2.0F);

							if (var25.minHeight > var22.minHeight)
							{
								var28 /= 2.0F;
							}

							var18 += var27 * var28;
							var19 += var26 * var28;
							var20 += var28;
						}
					}

					var18 /= var20;
					var19 /= var20;
					var18 = var18 * 0.9F + 0.1F;
					var19 = (var19 * 4.0F - 1.0F) / 8.0F;
					double var46 = this.field_147426_g[var13] / 8000.0D;

					if (var46 < 0.0D)
					{
						var46 = -var46 * 0.3D;
					}

					var46 = var46 * 3.0D - 2.0D;

					if (var46 < 0.0D)
					{
						var46 /= 2.0D;

						if (var46 < -1.0D)
						{
							var46 = -1.0D;
						}

						var46 /= 1.4D;
						var46 /= 2.0D;
					}
					else
					{
						if (var46 > 1.0D)
						{
							var46 = 1.0D;
						}

						var46 /= 8.0D;
					}

					++var13;
					double var47 = (double)var19;
					double var48 = (double)var18;
					var47 += var46 * 0.2D;
					var47 = var47 * 8.5D / 8.0D;
					double var29 = 8.5D + var47 * 4.0D;

					for (int var31 = 0; var31 < 33; ++var31)
					{
						double var32 = ((double)var31 - var29) * 12.0D * 128.0D / 256.0D / var48;

						if (var32 < 0.0D)
						{
							var32 *= 4.0D;
						}

						double var34 = this.field_147428_e[var12] / 512.0D;
						double var36 = this.field_147425_f[var12] / 512.0D;
						double var38 = (this.field_147427_d[var12] / 10.0D + 1.0D) / 2.0D;
						double var40 = MathHelper.denormalizeClamp(var34, var36, var38) - var32;

						if (var31 > 29)
						{
							double var42 = (double)((float)(var31 - 29) / 3.0F);
							var40 = var40 * (1.0D - var42) + -10.0D * var42;
						}

						this.field_147434_q[var12] = var40;
						++var12;
					}
				}
			}
		}

///    
///     <summary> * Checks to see if a chunk exists at x, y </summary>
///     
		public virtual bool chunkExists(int p_73149_1_, int p_73149_2_)
		{
			return true;
		}

///    
///     <summary> * Populates chunk with ores etc etc </summary>
///     
		public virtual void populate(IChunkProvider p_73153_1_, int p_73153_2_, int p_73153_3_)
		{
			BlockFalling.field_149832_M = true;
			int var4 = p_73153_2_ * 16;
			int var5 = p_73153_3_ * 16;
			BiomeGenBase var6 = this.worldObj.getBiomeGenForCoords(var4 + 16, var5 + 16);
			this.rand.Seed = this.worldObj.Seed;
			long var7 = this.rand.nextLong() / 2L * 2L + 1L;
			long var9 = this.rand.nextLong() / 2L * 2L + 1L;
			this.rand.Seed = (long)p_73153_2_ * var7 + (long)p_73153_3_ * var9 ^ this.worldObj.Seed;
			bool var11 = false;

			if (this.mapFeaturesEnabled)
			{
				this.mineshaftGenerator.generateStructuresInChunk(this.worldObj, this.rand, p_73153_2_, p_73153_3_);
				var11 = this.villageGenerator.generateStructuresInChunk(this.worldObj, this.rand, p_73153_2_, p_73153_3_);
				this.strongholdGenerator.generateStructuresInChunk(this.worldObj, this.rand, p_73153_2_, p_73153_3_);
				this.scatteredFeatureGenerator.generateStructuresInChunk(this.worldObj, this.rand, p_73153_2_, p_73153_3_);
			}

			int var12;
			int var13;
			int var14;

			if (var6 != BiomeGenBase.desert && var6 != BiomeGenBase.desertHills && !var11 && this.rand.Next(4) == 0)
			{
				var12 = var4 + this.rand.Next(16) + 8;
				var13 = this.rand.Next(256);
				var14 = var5 + this.rand.Next(16) + 8;
				(new WorldGenLakes(Blocks.water)).generate(this.worldObj, this.rand, var12, var13, var14);
			}

			if (!var11 && this.rand.Next(8) == 0)
			{
				var12 = var4 + this.rand.Next(16) + 8;
				var13 = this.rand.Next(this.rand.Next(248) + 8);
				var14 = var5 + this.rand.Next(16) + 8;

				if (var13 < 63 || this.rand.Next(10) == 0)
				{
					(new WorldGenLakes(Blocks.lava)).generate(this.worldObj, this.rand, var12, var13, var14);
				}
			}

			for (var12 = 0; var12 < 8; ++var12)
			{
				var13 = var4 + this.rand.Next(16) + 8;
				var14 = this.rand.Next(256);
				int var15 = var5 + this.rand.Next(16) + 8;
				(new WorldGenDungeons()).generate(this.worldObj, this.rand, var13, var14, var15);
			}

			var6.decorate(this.worldObj, this.rand, var4, var5);
			SpawnerAnimals.performWorldGenSpawning(this.worldObj, var6, var4 + 8, var5 + 8, 16, 16, this.rand);
			var4 += 8;
			var5 += 8;

			for (var12 = 0; var12 < 16; ++var12)
			{
				for (var13 = 0; var13 < 16; ++var13)
				{
					var14 = this.worldObj.getPrecipitationHeight(var4 + var12, var5 + var13);

					if (this.worldObj.isBlockFreezable(var12 + var4, var14 - 1, var13 + var5))
					{
						this.worldObj.setBlock(var12 + var4, var14 - 1, var13 + var5, Blocks.ice, 0, 2);
					}

					if (this.worldObj.func_147478_e(var12 + var4, var14, var13 + var5, true))
					{
						this.worldObj.setBlock(var12 + var4, var14, var13 + var5, Blocks.snow_layer, 0, 2);
					}
				}
			}

			BlockFalling.field_149832_M = false;
		}

///    
///     <summary> * Two modes of operation: if passed true, save all Chunks in one go.  If passed false, save up to two chunks.
///     * Return true if all chunks have been saved. </summary>
///     
		public virtual bool saveChunks(bool p_73151_1_, IProgressUpdate p_73151_2_)
		{
			return true;
		}

///    
///     <summary> * Save extra data not associated with any Chunk.  Not saved during autosave, only during world unload.  Currently
///     * unimplemented. </summary>
///     
		public virtual void saveExtraData()
		{
		}

///    
///     <summary> * Unloads chunks that are marked to be unloaded. This is not guaranteed to unload every such chunk. </summary>
///     
		public virtual bool unloadQueuedChunks()
		{
			return false;
		}

///    
///     <summary> * Returns if the IChunkProvider supports saving. </summary>
///     
		public virtual bool canSave()
		{
			return true;
		}

///    
///     <summary> * Converts the instance data to a readable string. </summary>
///     
		public virtual string makeString()
		{
			return "RandomLevelSource";
		}

///    
///     <summary> * Returns a list of creatures of the specified type that can spawn at the given location. </summary>
///     
		public virtual IList getPossibleCreatures(EnumCreatureType p_73155_1_, int p_73155_2_, int p_73155_3_, int p_73155_4_)
		{
			BiomeGenBase var5 = this.worldObj.getBiomeGenForCoords(p_73155_2_, p_73155_4_);
			return p_73155_1_ == EnumCreatureType.monster && this.scatteredFeatureGenerator.func_143030_a(p_73155_2_, p_73155_3_, p_73155_4_) ? this.scatteredFeatureGenerator.ScatteredFeatureSpawnList : var5.getSpawnableList(p_73155_1_);
		}

		public virtual ChunkPosition func_147416_a(World p_147416_1_, string p_147416_2_, int p_147416_3_, int p_147416_4_, int p_147416_5_)
		{
			return "Stronghold".Equals(p_147416_2_) && this.strongholdGenerator != null ? this.strongholdGenerator.func_151545_a(p_147416_1_, p_147416_3_, p_147416_4_, p_147416_5_) : null;
		}

		public virtual int LoadedChunkCount
		{
			get
			{
				return 0;
			}
		}

		public virtual void recreateStructures(int p_82695_1_, int p_82695_2_)
		{
			if (this.mapFeaturesEnabled)
			{
				this.mineshaftGenerator.func_151539_a(this, this.worldObj, p_82695_1_, p_82695_2_, (Block[])null);
				this.villageGenerator.func_151539_a(this, this.worldObj, p_82695_1_, p_82695_2_, (Block[])null);
				this.strongholdGenerator.func_151539_a(this, this.worldObj, p_82695_1_, p_82695_2_, (Block[])null);
				this.scatteredFeatureGenerator.func_151539_a(this, this.worldObj, p_82695_1_, p_82695_2_, (Block[])null);
			}
		}
	}

}

//----------------------------------------------------------------------------------------
//	Copyright © 2008 - 2010 Tangible Software Solutions Inc.
//	This class can be used by anyone provided that the copyright notice remains intact.
//
//	This class provides the logic to simulate Java rectangular arrays, which are jagged
//	arrays with inner arrays of the same length.
//----------------------------------------------------------------------------------------
internal static partial class RectangularArrays
{
    internal static int[][] ReturnRectangularIntArray(int Size1, int Size2)
    {
        int[][] Array = new int[Size1][];
        for (int Array1 = 0; Array1 < Size1; Array1++)
        {
            Array[Array1] = new int[Size2];
        }
        return Array;
    }
}