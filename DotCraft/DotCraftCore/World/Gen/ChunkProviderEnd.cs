using System;
using System.Collections;

namespace DotCraftCore.World.Gen
{

	using Block = DotCraftCore.block.Block;
	using BlockFalling = DotCraftCore.block.BlockFalling;
	using Material = DotCraftCore.block.material.Material;
	using EnumCreatureType = DotCraftCore.entity.EnumCreatureType;
	using Blocks = DotCraftCore.init.Blocks;
	using IProgressUpdate = DotCraftCore.Util.IProgressUpdate;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using ChunkPosition = DotCraftCore.World.ChunkPosition;
	using World = DotCraftCore.World.World;
	using BiomeGenBase = DotCraftCore.World.Biome.BiomeGenBase;
	using Chunk = DotCraftCore.World.Chunk.Chunk;
	using IChunkProvider = DotCraftCore.World.Chunk.IChunkProvider;

	public class ChunkProviderEnd : IChunkProvider
	{
		private Random endRNG;
		private NoiseGeneratorOctaves noiseGen1;
		private NoiseGeneratorOctaves noiseGen2;
		private NoiseGeneratorOctaves noiseGen3;
		public NoiseGeneratorOctaves noiseGen4;
		public NoiseGeneratorOctaves noiseGen5;
		private World endWorld;
		private double[] densities;

	/// <summary> The biomes that are used to generate the chunk  </summary>
		private BiomeGenBase[] biomesForGeneration;
		internal double[] noiseData1;
		internal double[] noiseData2;
		internal double[] noiseData3;
		internal double[] noiseData4;
		internal double[] noiseData5;
//ORIGINAL LINE: internal int[][] field_73203_h = new int[32][32];
//JAVA TO VB & C# CONVERTER NOTE: The following call to the 'RectangularArrays' helper class reproduces the rectangular array initialization that is automatic in Java:
		internal int[][] field_73203_h = RectangularArrays.ReturnRectangularIntArray(32, 32);
		

		public ChunkProviderEnd(World p_i2007_1_, long p_i2007_2_)
		{
			this.endWorld = p_i2007_1_;
			this.endRNG = new Random(p_i2007_2_);
			this.noiseGen1 = new NoiseGeneratorOctaves(this.endRNG, 16);
			this.noiseGen2 = new NoiseGeneratorOctaves(this.endRNG, 16);
			this.noiseGen3 = new NoiseGeneratorOctaves(this.endRNG, 8);
			this.noiseGen4 = new NoiseGeneratorOctaves(this.endRNG, 10);
			this.noiseGen5 = new NoiseGeneratorOctaves(this.endRNG, 16);
		}

		public virtual void func_147420_a(int p_147420_1_, int p_147420_2_, Block[] p_147420_3_, BiomeGenBase[] p_147420_4_)
		{
			sbyte var5 = 2;
			int var6 = var5 + 1;
			sbyte var7 = 33;
			int var8 = var5 + 1;
			this.densities = this.initializeNoiseField(this.densities, p_147420_1_ * var5, 0, p_147420_2_ * var5, var6, var7, var8);

			for (int var9 = 0; var9 < var5; ++var9)
			{
				for (int var10 = 0; var10 < var5; ++var10)
				{
					for (int var11 = 0; var11 < 32; ++var11)
					{
						double var12 = 0.25D;
						double var14 = this.densities[((var9 + 0) * var8 + var10 + 0) * var7 + var11 + 0];
						double var16 = this.densities[((var9 + 0) * var8 + var10 + 1) * var7 + var11 + 0];
						double var18 = this.densities[((var9 + 1) * var8 + var10 + 0) * var7 + var11 + 0];
						double var20 = this.densities[((var9 + 1) * var8 + var10 + 1) * var7 + var11 + 0];
						double var22 = (this.densities[((var9 + 0) * var8 + var10 + 0) * var7 + var11 + 1] - var14) * var12;
						double var24 = (this.densities[((var9 + 0) * var8 + var10 + 1) * var7 + var11 + 1] - var16) * var12;
						double var26 = (this.densities[((var9 + 1) * var8 + var10 + 0) * var7 + var11 + 1] - var18) * var12;
						double var28 = (this.densities[((var9 + 1) * var8 + var10 + 1) * var7 + var11 + 1] - var20) * var12;

						for (int var30 = 0; var30 < 4; ++var30)
						{
							double var31 = 0.125D;
							double var33 = var14;
							double var35 = var16;
							double var37 = (var18 - var14) * var31;
							double var39 = (var20 - var16) * var31;

							for (int var41 = 0; var41 < 8; ++var41)
							{
								int var42 = var41 + var9 * 8 << 11 | 0 + var10 * 8 << 7 | var11 * 4 + var30;
								short var43 = 128;
								double var44 = 0.125D;
								double var46 = var33;
								double var48 = (var35 - var33) * var44;

								for (int var50 = 0; var50 < 8; ++var50)
								{
									Block var51 = null;

									if (var46 > 0.0D)
									{
										var51 = Blocks.end_stone;
									}

									p_147420_3_[var42] = var51;
									var42 += var43;
									var46 += var48;
								}

								var33 += var37;
								var35 += var39;
							}

							var14 += var22;
							var16 += var24;
							var18 += var26;
							var20 += var28;
						}
					}
				}
			}
		}

		public virtual void func_147421_b(int p_147421_1_, int p_147421_2_, Block[] p_147421_3_, BiomeGenBase[] p_147421_4_)
		{
			for (int var5 = 0; var5 < 16; ++var5)
			{
				for (int var6 = 0; var6 < 16; ++var6)
				{
					sbyte var7 = 1;
					int var8 = -1;
					Block var9 = Blocks.end_stone;
					Block var10 = Blocks.end_stone;

					for (int var11 = 127; var11 >= 0; --var11)
					{
						int var12 = (var6 * 16 + var5) * 128 + var11;
						Block var13 = p_147421_3_[var12];

						if (var13 != null && var13.Material != Material.air)
						{
							if (var13 == Blocks.stone)
							{
								if (var8 == -1)
								{
									if (var7 <= 0)
									{
										var9 = null;
										var10 = Blocks.end_stone;
									}

									var8 = var7;

									if (var11 >= 0)
									{
										p_147421_3_[var12] = var9;
									}
									else
									{
										p_147421_3_[var12] = var10;
									}
								}
								else if (var8 > 0)
								{
									--var8;
									p_147421_3_[var12] = var10;
								}
							}
						}
						else
						{
							var8 = -1;
						}
					}
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
			this.endRNG.Seed = (long)p_73154_1_ * 341873128712L + (long)p_73154_2_ * 132897987541L;
			Block[] var3 = new Block[32768];
			this.biomesForGeneration = this.endWorld.WorldChunkManager.loadBlockGeneratorData(this.biomesForGeneration, p_73154_1_ * 16, p_73154_2_ * 16, 16, 16);
			this.func_147420_a(p_73154_1_, p_73154_2_, var3, this.biomesForGeneration);
			this.func_147421_b(p_73154_1_, p_73154_2_, var3, this.biomesForGeneration);
			Chunk var4 = new Chunk(this.endWorld, var3, p_73154_1_, p_73154_2_);
			sbyte[] var5 = var4.BiomeArray;

			for (int var6 = 0; var6 < var5.Length; ++var6)
			{
				var5[var6] = (sbyte)this.biomesForGeneration[var6].biomeID;
			}

			var4.generateSkylightMap();
			return var4;
		}

///    
///     <summary> * generates a subset of the level's terrain data. Takes 7 arguments: the [empty] noise array, the position, and the
///     * size. </summary>
///     
		private double[] initializeNoiseField(double[] p_73187_1_, int p_73187_2_, int p_73187_3_, int p_73187_4_, int p_73187_5_, int p_73187_6_, int p_73187_7_)
		{
			if (p_73187_1_ == null)
			{
				p_73187_1_ = new double[p_73187_5_ * p_73187_6_ * p_73187_7_];
			}

			double var8 = 684.412D;
			double var10 = 684.412D;
			this.noiseData4 = this.noiseGen4.generateNoiseOctaves(this.noiseData4, p_73187_2_, p_73187_4_, p_73187_5_, p_73187_7_, 1.121D, 1.121D, 0.5D);
			this.noiseData5 = this.noiseGen5.generateNoiseOctaves(this.noiseData5, p_73187_2_, p_73187_4_, p_73187_5_, p_73187_7_, 200.0D, 200.0D, 0.5D);
			var8 *= 2.0D;
			this.noiseData1 = this.noiseGen3.generateNoiseOctaves(this.noiseData1, p_73187_2_, p_73187_3_, p_73187_4_, p_73187_5_, p_73187_6_, p_73187_7_, var8 / 80.0D, var10 / 160.0D, var8 / 80.0D);
			this.noiseData2 = this.noiseGen1.generateNoiseOctaves(this.noiseData2, p_73187_2_, p_73187_3_, p_73187_4_, p_73187_5_, p_73187_6_, p_73187_7_, var8, var10, var8);
			this.noiseData3 = this.noiseGen2.generateNoiseOctaves(this.noiseData3, p_73187_2_, p_73187_3_, p_73187_4_, p_73187_5_, p_73187_6_, p_73187_7_, var8, var10, var8);
			int var12 = 0;
			int var13 = 0;

			for (int var14 = 0; var14 < p_73187_5_; ++var14)
			{
				for (int var15 = 0; var15 < p_73187_7_; ++var15)
				{
					double var16 = (this.noiseData4[var13] + 256.0D) / 512.0D;

					if (var16 > 1.0D)
					{
						var16 = 1.0D;
					}

					double var18 = this.noiseData5[var13] / 8000.0D;

					if (var18 < 0.0D)
					{
						var18 = -var18 * 0.3D;
					}

					var18 = var18 * 3.0D - 2.0D;
					float var20 = (float)(var14 + p_73187_2_ - 0) / 1.0F;
					float var21 = (float)(var15 + p_73187_4_ - 0) / 1.0F;
					float var22 = 100.0F - MathHelper.sqrt_float(var20 * var20 + var21 * var21) * 8.0F;

					if (var22 > 80.0F)
					{
						var22 = 80.0F;
					}

					if (var22 < -100.0F)
					{
						var22 = -100.0F;
					}

					if (var18 > 1.0D)
					{
						var18 = 1.0D;
					}

					var18 /= 8.0D;
					var18 = 0.0D;

					if (var16 < 0.0D)
					{
						var16 = 0.0D;
					}

					var16 += 0.5D;
					var18 = var18 * (double)p_73187_6_ / 16.0D;
					++var13;
					double var23 = (double)p_73187_6_ / 2.0D;

					for (int var25 = 0; var25 < p_73187_6_; ++var25)
					{
						double var26 = 0.0D;
						double var28 = ((double)var25 - var23) * 8.0D / var16;

						if (var28 < 0.0D)
						{
							var28 *= -1.0D;
						}

						double var30 = this.noiseData2[var12] / 512.0D;
						double var32 = this.noiseData3[var12] / 512.0D;
						double var34 = (this.noiseData1[var12] / 10.0D + 1.0D) / 2.0D;

						if (var34 < 0.0D)
						{
							var26 = var30;
						}
						else if (var34 > 1.0D)
						{
							var26 = var32;
						}
						else
						{
							var26 = var30 + (var32 - var30) * var34;
						}

						var26 -= 8.0D;
						var26 += (double)var22;
						sbyte var36 = 2;
						double var37;

						if (var25 > p_73187_6_ / 2 - var36)
						{
							var37 = (double)((float)(var25 - (p_73187_6_ / 2 - var36)) / 64.0F);

							if (var37 < 0.0D)
							{
								var37 = 0.0D;
							}

							if (var37 > 1.0D)
							{
								var37 = 1.0D;
							}

							var26 = var26 * (1.0D - var37) + -3000.0D * var37;
						}

						var36 = 8;

						if (var25 < var36)
						{
							var37 = (double)((float)(var36 - var25) / ((float)var36 - 1.0F));
							var26 = var26 * (1.0D - var37) + -30.0D * var37;
						}

						p_73187_1_[var12] = var26;
						++var12;
					}
				}
			}

			return p_73187_1_;
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
			BiomeGenBase var6 = this.endWorld.getBiomeGenForCoords(var4 + 16, var5 + 16);
			var6.decorate(this.endWorld, this.endWorld.rand, var4, var5);
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
			BiomeGenBase var5 = this.endWorld.getBiomeGenForCoords(p_73155_2_, p_73155_4_);
			return var5.getSpawnableList(p_73155_1_);
		}

		public virtual ChunkPosition func_147416_a(World p_147416_1_, string p_147416_2_, int p_147416_3_, int p_147416_4_, int p_147416_5_)
		{
			return null;
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