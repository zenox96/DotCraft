using System;
using System.Collections;

namespace DotCraftCore.World.Gen
{

	using Block = DotCraftCore.block.Block;
	using EnumCreatureType = DotCraftCore.entity.EnumCreatureType;
	using Blocks = DotCraftCore.init.Blocks;
	using IProgressUpdate = DotCraftCore.Util.IProgressUpdate;
	using ChunkPosition = DotCraftCore.World.ChunkPosition;
	using World = DotCraftCore.World.World;
	using BiomeGenBase = DotCraftCore.World.Biome.BiomeGenBase;
	using Chunk = DotCraftCore.World.Chunk.Chunk;
	using IChunkProvider = DotCraftCore.World.Chunk.IChunkProvider;
	using ExtendedBlockStorage = DotCraftCore.World.Chunk.Storage.ExtendedBlockStorage;
	using WorldGenDungeons = DotCraftCore.World.Gen.Feature.WorldGenDungeons;
	using WorldGenLakes = DotCraftCore.World.Gen.Feature.WorldGenLakes;
	using MapGenMineshaft = DotCraftCore.World.Gen.Structure.MapGenMineshaft;
	using MapGenScatteredFeature = DotCraftCore.World.Gen.Structure.MapGenScatteredFeature;
	using MapGenStronghold = DotCraftCore.World.Gen.Structure.MapGenStronghold;
	using MapGenStructure = DotCraftCore.World.Gen.Structure.MapGenStructure;
	using MapGenVillage = DotCraftCore.World.Gen.Structure.MapGenVillage;

	public class ChunkProviderFlat : IChunkProvider
	{
		private World worldObj;
		private Random random;
		private readonly Block[] cachedBlockIDs = new Block[256];
		private readonly sbyte[] cachedBlockMetadata = new sbyte[256];
		private readonly FlatGeneratorInfo flatWorldGenInfo;
		private readonly IList structureGenerators = new ArrayList();
		private readonly bool hasDecoration;
		private readonly bool hasDungeons;
		private WorldGenLakes waterLakeGenerator;
		private WorldGenLakes lavaLakeGenerator;
		private const string __OBFID = "CL_00000391";

		public ChunkProviderFlat(World p_i2004_1_, long p_i2004_2_, bool p_i2004_4_, string p_i2004_5_)
		{
			this.worldObj = p_i2004_1_;
			this.random = new Random(p_i2004_2_);
			this.flatWorldGenInfo = FlatGeneratorInfo.createFlatGeneratorFromString(p_i2004_5_);

			if (p_i2004_4_)
			{
				IDictionary var6 = this.flatWorldGenInfo.WorldFeatures;

				if (var6.ContainsKey("village"))
				{
					IDictionary var7 = (IDictionary)var6["village"];

					if (!var7.ContainsKey("size"))
					{
						var7.Add("size", "1");
					}

					this.structureGenerators.Add(new MapGenVillage(var7));
				}

				if (var6.ContainsKey("biome_1"))
				{
					this.structureGenerators.Add(new MapGenScatteredFeature((IDictionary)var6["biome_1"]));
				}

				if (var6.ContainsKey("mineshaft"))
				{
					this.structureGenerators.Add(new MapGenMineshaft((IDictionary)var6["mineshaft"]));
				}

				if (var6.ContainsKey("stronghold"))
				{
					this.structureGenerators.Add(new MapGenStronghold((IDictionary)var6["stronghold"]));
				}
			}

			this.hasDecoration = this.flatWorldGenInfo.WorldFeatures.ContainsKey("decoration");

			if (this.flatWorldGenInfo.WorldFeatures.ContainsKey("lake"))
			{
				this.waterLakeGenerator = new WorldGenLakes(Blocks.water);
			}

			if (this.flatWorldGenInfo.WorldFeatures.ContainsKey("lava_lake"))
			{
				this.lavaLakeGenerator = new WorldGenLakes(Blocks.lava);
			}

			this.hasDungeons = this.flatWorldGenInfo.WorldFeatures.ContainsKey("dungeon");
			IEnumerator var9 = this.flatWorldGenInfo.FlatLayers.GetEnumerator();

			while (var9.MoveNext())
			{
				FlatLayerInfo var10 = (FlatLayerInfo)var9.Current;

				for (int var8 = var10.MinY; var8 < var10.MinY + var10.LayerCount; ++var8)
				{
					this.cachedBlockIDs[var8] = var10.func_151536_b();
					this.cachedBlockMetadata[var8] = (sbyte)var10.FillBlockMeta;
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
			Chunk var3 = new Chunk(this.worldObj, p_73154_1_, p_73154_2_);
			int var6;

			for (int var4 = 0; var4 < this.cachedBlockIDs.Length; ++var4)
			{
				Block var5 = this.cachedBlockIDs[var4];

				if (var5 != null)
				{
					var6 = var4 >> 4;
					ExtendedBlockStorage var7 = var3.BlockStorageArray[var6];

					if (var7 == null)
					{
						var7 = new ExtendedBlockStorage(var4, !this.worldObj.provider.hasNoSky);
						var3.BlockStorageArray[var6] = var7;
					}

					for (int var8 = 0; var8 < 16; ++var8)
					{
						for (int var9 = 0; var9 < 16; ++var9)
						{
							var7.func_150818_a(var8, var4 & 15, var9, var5);
							var7.setExtBlockMetadata(var8, var4 & 15, var9, this.cachedBlockMetadata[var4]);
						}
					}
				}
			}

			var3.generateSkylightMap();
			BiomeGenBase[] var10 = this.worldObj.WorldChunkManager.loadBlockGeneratorData((BiomeGenBase[])null, p_73154_1_ * 16, p_73154_2_ * 16, 16, 16);
			sbyte[] var11 = var3.BiomeArray;

			for (var6 = 0; var6 < var11.Length; ++var6)
			{
				var11[var6] = (sbyte)var10[var6].biomeID;
			}

			IEnumerator var12 = this.structureGenerators.GetEnumerator();

			while (var12.MoveNext())
			{
				MapGenBase var13 = (MapGenBase)var12.Current;
				var13.func_151539_a(this, this.worldObj, p_73154_1_, p_73154_2_, (Block[])null);
			}

			var3.generateSkylightMap();
			return var3;
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
			int var4 = p_73153_2_ * 16;
			int var5 = p_73153_3_ * 16;
			BiomeGenBase var6 = this.worldObj.getBiomeGenForCoords(var4 + 16, var5 + 16);
			bool var7 = false;
			this.random.Seed = this.worldObj.Seed;
			long var8 = this.random.nextLong() / 2L * 2L + 1L;
			long var10 = this.random.nextLong() / 2L * 2L + 1L;
			this.random.Seed = (long)p_73153_2_ * var8 + (long)p_73153_3_ * var10 ^ this.worldObj.Seed;
			IEnumerator var12 = this.structureGenerators.GetEnumerator();

			while (var12.MoveNext())
			{
				MapGenStructure var13 = (MapGenStructure)var12.Current;
				bool var14 = var13.generateStructuresInChunk(this.worldObj, this.random, p_73153_2_, p_73153_3_);

				if (var13 is MapGenVillage)
				{
					var7 |= var14;
				}
			}

			int var16;
			int var17;
			int var18;

			if (this.waterLakeGenerator != null && !var7 && this.random.Next(4) == 0)
			{
				var16 = var4 + this.random.Next(16) + 8;
				var17 = this.random.Next(256);
				var18 = var5 + this.random.Next(16) + 8;
				this.waterLakeGenerator.generate(this.worldObj, this.random, var16, var17, var18);
			}

			if (this.lavaLakeGenerator != null && !var7 && this.random.Next(8) == 0)
			{
				var16 = var4 + this.random.Next(16) + 8;
				var17 = this.random.Next(this.random.Next(248) + 8);
				var18 = var5 + this.random.Next(16) + 8;

				if (var17 < 63 || this.random.Next(10) == 0)
				{
					this.lavaLakeGenerator.generate(this.worldObj, this.random, var16, var17, var18);
				}
			}

			if (this.hasDungeons)
			{
				for (var16 = 0; var16 < 8; ++var16)
				{
					var17 = var4 + this.random.Next(16) + 8;
					var18 = this.random.Next(256);
					int var15 = var5 + this.random.Next(16) + 8;
					(new WorldGenDungeons()).generate(this.worldObj, this.random, var17, var18, var15);
				}
			}

			if (this.hasDecoration)
			{
				var6.decorate(this.worldObj, this.random, var4, var5);
			}
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
			return "FlatLevelSource";
		}

///    
///     <summary> * Returns a list of creatures of the specified type that can spawn at the given location. </summary>
///     
		public virtual IList getPossibleCreatures(EnumCreatureType p_73155_1_, int p_73155_2_, int p_73155_3_, int p_73155_4_)
		{
			BiomeGenBase var5 = this.worldObj.getBiomeGenForCoords(p_73155_2_, p_73155_4_);
			return var5.getSpawnableList(p_73155_1_);
		}

		public virtual ChunkPosition func_147416_a(World p_147416_1_, string p_147416_2_, int p_147416_3_, int p_147416_4_, int p_147416_5_)
		{
			if ("Stronghold".Equals(p_147416_2_))
			{
				IEnumerator var6 = this.structureGenerators.GetEnumerator();

				while (var6.MoveNext())
				{
					MapGenStructure var7 = (MapGenStructure)var6.Current;

					if (var7 is MapGenStronghold)
					{
						return var7.func_151545_a(p_147416_1_, p_147416_3_, p_147416_4_, p_147416_5_);
					}
				}
			}

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
			IEnumerator var3 = this.structureGenerators.GetEnumerator();

			while (var3.MoveNext())
			{
				MapGenStructure var4 = (MapGenStructure)var3.Current;
				var4.func_151539_a(this, this.worldObj, p_82695_1_, p_82695_2_, (Block[])null);
			}
		}
	}

}