using System;
using System.Collections;

namespace DotCraftCore.World.Biome
{

	using CrashReport = DotCraftCore.crash.CrashReport;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using ReportedException = DotCraftCore.Util.ReportedException;
	using ChunkPosition = DotCraftCore.World.ChunkPosition;
	using World = DotCraftCore.World.World;
	using WorldType = DotCraftCore.World.WorldType;
	using GenLayer = DotCraftCore.World.Gen.Layer.GenLayer;
	using IntCache = DotCraftCore.World.Gen.Layer.IntCache;

	public class WorldChunkManager
	{
		private GenLayer genBiomes;

	/// <summary> A GenLayer containing the indices into BiomeGenBase.biomeList[]  </summary>
		private GenLayer biomeIndexLayer;

	/// <summary> The BiomeCache object for this world.  </summary>
		private BiomeCache biomeCache;

	/// <summary> A list of biomes that the player can spawn in.  </summary>
		private IList biomesToSpawnIn;
		

		protected internal WorldChunkManager()
		{
			this.biomeCache = new BiomeCache(this);
			this.biomesToSpawnIn = new ArrayList();
			this.biomesToSpawnIn.Add(BiomeGenBase.forest);
			this.biomesToSpawnIn.Add(BiomeGenBase.plains);
			this.biomesToSpawnIn.Add(BiomeGenBase.taiga);
			this.biomesToSpawnIn.Add(BiomeGenBase.taigaHills);
			this.biomesToSpawnIn.Add(BiomeGenBase.forestHills);
			this.biomesToSpawnIn.Add(BiomeGenBase.jungle);
			this.biomesToSpawnIn.Add(BiomeGenBase.jungleHills);
		}

		public WorldChunkManager(long p_i1975_1_, WorldType p_i1975_3_) : this()
		{
			GenLayer[] var4 = GenLayer.initializeAllBiomeGenerators(p_i1975_1_, p_i1975_3_);
			this.genBiomes = var4[0];
			this.biomeIndexLayer = var4[1];
		}

		public WorldChunkManager(World p_i1976_1_) : this(p_i1976_1_.getSeed(), p_i1976_1_.getWorldInfo().getTerrainType())
		{
		}

///    
///     <summary> * Gets the list of valid biomes for the player to spawn in. </summary>
///     
		public virtual IList BiomesToSpawnIn
		{
			get
			{
				return this.biomesToSpawnIn;
			}
		}

///    
///     <summary> * Returns the BiomeGenBase related to the x, z position on the world. </summary>
///     
		public virtual BiomeGenBase getBiomeGenAt(int p_76935_1_, int p_76935_2_)
		{
			return this.biomeCache.getBiomeGenAt(p_76935_1_, p_76935_2_);
		}

///    
///     <summary> * Returns a list of rainfall values for the specified blocks. Args: listToReuse, x, z, width, length. </summary>
///     
		public virtual float[] getRainfall(float[] p_76936_1_, int p_76936_2_, int p_76936_3_, int p_76936_4_, int p_76936_5_)
		{
			IntCache.resetIntCache();

			if (p_76936_1_ == null || p_76936_1_.Length < p_76936_4_ * p_76936_5_)
			{
				p_76936_1_ = new float[p_76936_4_ * p_76936_5_];
			}

			int[] var6 = this.biomeIndexLayer.getInts(p_76936_2_, p_76936_3_, p_76936_4_, p_76936_5_);

			for (int var7 = 0; var7 < p_76936_4_ * p_76936_5_; ++var7)
			{
				try
				{
					float var8 = (float)BiomeGenBase.func_150568_d(var6[var7]).IntRainfall / 65536.0F;

					if (var8 > 1.0F)
					{
						var8 = 1.0F;
					}

					p_76936_1_[var7] = var8;
				}
				catch (Exception var11)
				{
					CrashReport var9 = CrashReport.makeCrashReport(var11, "Invalid Biome id");
					CrashReportCategory var10 = var9.makeCategory("DownfallBlock");
					var10.addCrashSection("biome id", Convert.ToInt32(var7));
					var10.addCrashSection("downfalls[] size", Convert.ToInt32(p_76936_1_.Length));
					var10.addCrashSection("x", Convert.ToInt32(p_76936_2_));
					var10.addCrashSection("z", Convert.ToInt32(p_76936_3_));
					var10.addCrashSection("w", Convert.ToInt32(p_76936_4_));
					var10.addCrashSection("h", Convert.ToInt32(p_76936_5_));
					throw new ReportedException(var9);
				}
			}

			return p_76936_1_;
		}

///    
///     <summary> * Return an adjusted version of a given temperature based on the y height </summary>
///     
		public virtual float getTemperatureAtHeight(float p_76939_1_, int p_76939_2_)
		{
			return p_76939_1_;
		}

///    
///     <summary> * Returns an array of biomes for the location input. </summary>
///     
		public virtual BiomeGenBase[] getBiomesForGeneration(BiomeGenBase[] p_76937_1_, int p_76937_2_, int p_76937_3_, int p_76937_4_, int p_76937_5_)
		{
			IntCache.resetIntCache();

			if (p_76937_1_ == null || p_76937_1_.Length < p_76937_4_ * p_76937_5_)
			{
				p_76937_1_ = new BiomeGenBase[p_76937_4_ * p_76937_5_];
			}

			int[] var6 = this.genBiomes.getInts(p_76937_2_, p_76937_3_, p_76937_4_, p_76937_5_);

			try
			{
				for (int var7 = 0; var7 < p_76937_4_ * p_76937_5_; ++var7)
				{
					p_76937_1_[var7] = BiomeGenBase.func_150568_d(var6[var7]);
				}

				return p_76937_1_;
			}
			catch (Exception var10)
			{
				CrashReport var8 = CrashReport.makeCrashReport(var10, "Invalid Biome id");
				CrashReportCategory var9 = var8.makeCategory("RawBiomeBlock");
				var9.addCrashSection("biomes[] size", Convert.ToInt32(p_76937_1_.Length));
				var9.addCrashSection("x", Convert.ToInt32(p_76937_2_));
				var9.addCrashSection("z", Convert.ToInt32(p_76937_3_));
				var9.addCrashSection("w", Convert.ToInt32(p_76937_4_));
				var9.addCrashSection("h", Convert.ToInt32(p_76937_5_));
				throw new ReportedException(var8);
			}
		}

///    
///     <summary> * Returns biomes to use for the blocks and loads the other data like temperature and humidity onto the
///     * WorldChunkManager Args: oldBiomeList, x, z, width, depth </summary>
///     
		public virtual BiomeGenBase[] loadBlockGeneratorData(BiomeGenBase[] p_76933_1_, int p_76933_2_, int p_76933_3_, int p_76933_4_, int p_76933_5_)
		{
			return this.getBiomeGenAt(p_76933_1_, p_76933_2_, p_76933_3_, p_76933_4_, p_76933_5_, true);
		}

///    
///     <summary> * Return a list of biomes for the specified blocks. Args: listToReuse, x, y, width, length, cacheFlag (if false,
///     * don't check biomeCache to avoid infinite loop in BiomeCacheBlock) </summary>
///     
		public virtual BiomeGenBase[] getBiomeGenAt(BiomeGenBase[] p_76931_1_, int p_76931_2_, int p_76931_3_, int p_76931_4_, int p_76931_5_, bool p_76931_6_)
		{
			IntCache.resetIntCache();

			if (p_76931_1_ == null || p_76931_1_.Length < p_76931_4_ * p_76931_5_)
			{
				p_76931_1_ = new BiomeGenBase[p_76931_4_ * p_76931_5_];
			}

			if (p_76931_6_ && p_76931_4_ == 16 && p_76931_5_ == 16 && (p_76931_2_ & 15) == 0 && (p_76931_3_ & 15) == 0)
			{
				BiomeGenBase[] var9 = this.biomeCache.getCachedBiomes(p_76931_2_, p_76931_3_);
				Array.Copy(var9, 0, p_76931_1_, 0, p_76931_4_ * p_76931_5_);
				return p_76931_1_;
			}
			else
			{
				int[] var7 = this.biomeIndexLayer.getInts(p_76931_2_, p_76931_3_, p_76931_4_, p_76931_5_);

				for (int var8 = 0; var8 < p_76931_4_ * p_76931_5_; ++var8)
				{
					p_76931_1_[var8] = BiomeGenBase.func_150568_d(var7[var8]);
				}

				return p_76931_1_;
			}
		}

///    
///     <summary> * checks given Chunk's Biomes against List of allowed ones </summary>
///     
		public virtual bool areBiomesViable(int p_76940_1_, int p_76940_2_, int p_76940_3_, IList p_76940_4_)
		{
			IntCache.resetIntCache();
			int var5 = p_76940_1_ - p_76940_3_ >> 2;
			int var6 = p_76940_2_ - p_76940_3_ >> 2;
			int var7 = p_76940_1_ + p_76940_3_ >> 2;
			int var8 = p_76940_2_ + p_76940_3_ >> 2;
			int var9 = var7 - var5 + 1;
			int var10 = var8 - var6 + 1;
			int[] var11 = this.genBiomes.getInts(var5, var6, var9, var10);

			try
			{
				for (int var12 = 0; var12 < var9 * var10; ++var12)
				{
					BiomeGenBase var16 = BiomeGenBase.func_150568_d(var11[var12]);

					if (!p_76940_4_.Contains(var16))
					{
						return false;
					}
				}

				return true;
			}
			catch (Exception var15)
			{
				CrashReport var13 = CrashReport.makeCrashReport(var15, "Invalid Biome id");
				CrashReportCategory var14 = var13.makeCategory("Layer");
				var14.addCrashSection("Layer", this.genBiomes.ToString());
				var14.addCrashSection("x", Convert.ToInt32(p_76940_1_));
				var14.addCrashSection("z", Convert.ToInt32(p_76940_2_));
				var14.addCrashSection("radius", Convert.ToInt32(p_76940_3_));
				var14.addCrashSection("allowed", p_76940_4_);
				throw new ReportedException(var13);
			}
		}

		public virtual ChunkPosition func_150795_a(int p_150795_1_, int p_150795_2_, int p_150795_3_, IList p_150795_4_, Random p_150795_5_)
		{
			IntCache.resetIntCache();
			int var6 = p_150795_1_ - p_150795_3_ >> 2;
			int var7 = p_150795_2_ - p_150795_3_ >> 2;
			int var8 = p_150795_1_ + p_150795_3_ >> 2;
			int var9 = p_150795_2_ + p_150795_3_ >> 2;
			int var10 = var8 - var6 + 1;
			int var11 = var9 - var7 + 1;
			int[] var12 = this.genBiomes.getInts(var6, var7, var10, var11);
			ChunkPosition var13 = null;
			int var14 = 0;

			for (int var15 = 0; var15 < var10 * var11; ++var15)
			{
				int var16 = var6 + var15 % var10 << 2;
				int var17 = var7 + var15 / var10 << 2;
				BiomeGenBase var18 = BiomeGenBase.func_150568_d(var12[var15]);

				if (p_150795_4_.Contains(var18) && (var13 == null || p_150795_5_.Next(var14 + 1) == 0))
				{
					var13 = new ChunkPosition(var16, 0, var17);
					++var14;
				}
			}

			return var13;
		}

///    
///     <summary> * Calls the WorldChunkManager's biomeCache.cleanupCache() </summary>
///     
		public virtual void cleanupCache()
		{
			this.biomeCache.cleanupCache();
		}
	}

}