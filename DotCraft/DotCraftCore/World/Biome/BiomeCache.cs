using System.Collections;

namespace DotCraftCore.World.Biome
{

	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using LongHashMap = DotCraftCore.Util.LongHashMap;

	public class BiomeCache
	{
	/// <summary> Reference to the WorldChunkManager  </summary>
		private readonly WorldChunkManager chunkManager;

	/// <summary> The last time this BiomeCache was cleaned, in milliseconds.  </summary>
		private long lastCleanupTime;

///    
///     <summary> * The map of keys to BiomeCacheBlocks. Keys are based on the chunk x, z coordinates as (x | z << 32). </summary>
///     
		private LongHashMap cacheMap = new LongHashMap();

	/// <summary> The list of cached BiomeCacheBlocks  </summary>
		private IList cache = new ArrayList();
		private const string __OBFID = "CL_00000162";

		public BiomeCache(WorldChunkManager p_i1973_1_)
		{
			this.chunkManager = p_i1973_1_;
		}

///    
///     <summary> * Returns a biome cache block at location specified. </summary>
///     
		public virtual BiomeCache.Block getBiomeCacheBlock(int p_76840_1_, int p_76840_2_)
		{
			p_76840_1_ >>= 4;
			p_76840_2_ >>= 4;
			long var3 = (long)p_76840_1_ & 4294967295L | ((long)p_76840_2_ & 4294967295L) << 32;
			BiomeCache.Block var5 = (BiomeCache.Block)this.cacheMap.getValueByKey(var3);

			if (var5 == null)
			{
				var5 = new BiomeCache.Block(p_76840_1_, p_76840_2_);
				this.cacheMap.add(var3, var5);
				this.cache.Add(var5);
			}

			var5.lastAccessTime = MinecraftServer.SystemTimeMillis;
			return var5;
		}

///    
///     <summary> * Returns the BiomeGenBase related to the x, z position from the cache. </summary>
///     
		public virtual BiomeGenBase getBiomeGenAt(int p_76837_1_, int p_76837_2_)
		{
			return this.getBiomeCacheBlock(p_76837_1_, p_76837_2_).getBiomeGenAt(p_76837_1_, p_76837_2_);
		}

///    
///     <summary> * Removes BiomeCacheBlocks from this cache that haven't been accessed in at least 30 seconds. </summary>
///     
		public virtual void cleanupCache()
		{
			long var1 = MinecraftServer.SystemTimeMillis;
			long var3 = var1 - this.lastCleanupTime;

			if (var3 > 7500L || var3 < 0L)
			{
				this.lastCleanupTime = var1;

				for (int var5 = 0; var5 < this.cache.Count; ++var5)
				{
					BiomeCache.Block var6 = (BiomeCache.Block)this.cache.get(var5);
					long var7 = var1 - var6.lastAccessTime;

					if (var7 > 30000L || var7 < 0L)
					{
						this.cache.Remove(var5--);
						long var9 = (long)var6.xPosition & 4294967295L | ((long)var6.zPosition & 4294967295L) << 32;
						this.cacheMap.remove(var9);
					}
				}
			}
		}

///    
///     <summary> * Returns the array of cached biome types in the BiomeCacheBlock at the given location. </summary>
///     
		public virtual BiomeGenBase[] getCachedBiomes(int p_76839_1_, int p_76839_2_)
		{
			return this.getBiomeCacheBlock(p_76839_1_, p_76839_2_).biomes;
		}

		public class Block
		{
			public float[] rainfallValues = new float[256];
			public BiomeGenBase[] biomes = new BiomeGenBase[256];
			public int xPosition;
			public int zPosition;
			public long lastAccessTime;
			private const string __OBFID = "CL_00000163";

			public Block(int p_i1972_2_, int p_i1972_3_)
			{
				this.xPosition = p_i1972_2_;
				this.zPosition = p_i1972_3_;
				BiomeCache.chunkManager.getRainfall(this.rainfallValues, p_i1972_2_ << 4, p_i1972_3_ << 4, 16, 16);
				BiomeCache.chunkManager.getBiomeGenAt(this.biomes, p_i1972_2_ << 4, p_i1972_3_ << 4, 16, 16, false);
			}

			public virtual BiomeGenBase getBiomeGenAt(int p_76885_1_, int p_76885_2_)
			{
				return this.biomes[p_76885_1_ & 15 | (p_76885_2_ & 15) << 4];
			}
		}
	}

}