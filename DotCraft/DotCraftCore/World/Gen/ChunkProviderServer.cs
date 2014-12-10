using System;
using System.Collections;

namespace DotCraftCore.World.Gen
{

	using Lists = com.google.common.collect.Lists;
	using CrashReport = DotCraftCore.crash.CrashReport;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using EnumCreatureType = DotCraftCore.entity.EnumCreatureType;
	using ChunkCoordinates = DotCraftCore.Util.ChunkCoordinates;
	using IProgressUpdate = DotCraftCore.Util.IProgressUpdate;
	using LongHashMap = DotCraftCore.Util.LongHashMap;
	using ReportedException = DotCraftCore.Util.ReportedException;
	using ChunkCoordIntPair = DotCraftCore.World.ChunkCoordIntPair;
	using ChunkPosition = DotCraftCore.World.ChunkPosition;
	using MinecraftException = DotCraftCore.World.MinecraftException;
	using World = DotCraftCore.World.World;
	using WorldServer = DotCraftCore.World.WorldServer;
	using Chunk = DotCraftCore.World.Chunk.Chunk;
	using EmptyChunk = DotCraftCore.World.Chunk.EmptyChunk;
	using IChunkProvider = DotCraftCore.World.Chunk.IChunkProvider;
	using IChunkLoader = DotCraftCore.World.Chunk.Storage.IChunkLoader;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class ChunkProviderServer : IChunkProvider
	{
		private static readonly Logger logger = LogManager.Logger;

///    
///     <summary> * used by unload100OldestChunks to iterate the loadedChunkHashMap for unload (underlying assumption, first in,
///     * first out) </summary>
///     
		private Set chunksToUnload = Collections.newSetFromMap(new ConcurrentHashMap());
		private Chunk defaultEmptyChunk;
		private IChunkProvider currentChunkProvider;
		private IChunkLoader currentChunkLoader;

///    
///     <summary> * if this is false, the defaultEmptyChunk will be returned by the provider </summary>
///     
		public bool loadChunkOnProvideRequest = true;
		private LongHashMap loadedChunkHashMap = new LongHashMap();
		private IList loadedChunks = new ArrayList();
		private WorldServer worldObj;
		private const string __OBFID = "CL_00001436";

		public ChunkProviderServer(WorldServer p_i1520_1_, IChunkLoader p_i1520_2_, IChunkProvider p_i1520_3_)
		{
			this.defaultEmptyChunk = new EmptyChunk(p_i1520_1_, 0, 0);
			this.worldObj = p_i1520_1_;
			this.currentChunkLoader = p_i1520_2_;
			this.currentChunkProvider = p_i1520_3_;
		}

///    
///     <summary> * Checks to see if a chunk exists at x, y </summary>
///     
		public virtual bool chunkExists(int p_73149_1_, int p_73149_2_)
		{
			return this.loadedChunkHashMap.containsItem(ChunkCoordIntPair.chunkXZ2Int(p_73149_1_, p_73149_2_));
		}

		public virtual IList func_152380_a()
		{
			return this.loadedChunks;
		}

///    
///     <summary> * marks chunk for unload by "unload100OldestChunks"  if there is no spawn point, or if the center of the chunk is
///     * outside 200 blocks (x or z) of the spawn </summary>
///     
		public virtual void unloadChunksIfNotNearSpawn(int p_73241_1_, int p_73241_2_)
		{
			if (this.worldObj.provider.canRespawnHere())
			{
				ChunkCoordinates var3 = this.worldObj.SpawnPoint;
				int var4 = p_73241_1_ * 16 + 8 - var3.posX;
				int var5 = p_73241_2_ * 16 + 8 - var3.posZ;
				short var6 = 128;

				if (var4 < -var6 || var4 > var6 || var5 < -var6 || var5 > var6)
				{
					this.chunksToUnload.add(Convert.ToInt64(ChunkCoordIntPair.chunkXZ2Int(p_73241_1_, p_73241_2_)));
				}
			}
			else
			{
				this.chunksToUnload.add(Convert.ToInt64(ChunkCoordIntPair.chunkXZ2Int(p_73241_1_, p_73241_2_)));
			}
		}

///    
///     <summary> * marks all chunks for unload, ignoring those near the spawn </summary>
///     
		public virtual void unloadAllChunks()
		{
			IEnumerator var1 = this.loadedChunks.GetEnumerator();

			while (var1.MoveNext())
			{
				Chunk var2 = (Chunk)var1.Current;
				this.unloadChunksIfNotNearSpawn(var2.xPosition, var2.zPosition);
			}
		}

///    
///     <summary> * loads or generates the chunk at the chunk location specified </summary>
///     
		public virtual Chunk loadChunk(int p_73158_1_, int p_73158_2_)
		{
			long var3 = ChunkCoordIntPair.chunkXZ2Int(p_73158_1_, p_73158_2_);
			this.chunksToUnload.remove(Convert.ToInt64(var3));
			Chunk var5 = (Chunk)this.loadedChunkHashMap.getValueByKey(var3);

			if (var5 == null)
			{
				var5 = this.safeLoadChunk(p_73158_1_, p_73158_2_);

				if (var5 == null)
				{
					if (this.currentChunkProvider == null)
					{
						var5 = this.defaultEmptyChunk;
					}
					else
					{
						try
						{
							var5 = this.currentChunkProvider.provideChunk(p_73158_1_, p_73158_2_);
						}
						catch (Exception var9)
						{
							CrashReport var7 = CrashReport.makeCrashReport(var9, "Exception generating new chunk");
							CrashReportCategory var8 = var7.makeCategory("Chunk to be generated");
							var8.addCrashSection("Location", string.Format("{0:D},{1:D}", new object[] {Convert.ToInt32(p_73158_1_), Convert.ToInt32(p_73158_2_)}));
							var8.addCrashSection("Position hash", Convert.ToInt64(var3));
							var8.addCrashSection("Generator", this.currentChunkProvider.makeString());
							throw new ReportedException(var7);
						}
					}
				}

				this.loadedChunkHashMap.add(var3, var5);
				this.loadedChunks.Add(var5);
				var5.onChunkLoad();
				var5.populateChunk(this, this, p_73158_1_, p_73158_2_);
			}

			return var5;
		}

///    
///     <summary> * Will return back a chunk, if it doesn't exist and its not a MP client it will generates all the blocks for the
///     * specified chunk from the map seed and chunk seed </summary>
///     
		public virtual Chunk provideChunk(int p_73154_1_, int p_73154_2_)
		{
			Chunk var3 = (Chunk)this.loadedChunkHashMap.getValueByKey(ChunkCoordIntPair.chunkXZ2Int(p_73154_1_, p_73154_2_));
			return var3 == null ? (!this.worldObj.findingSpawnPoint && !this.loadChunkOnProvideRequest ? this.defaultEmptyChunk : this.loadChunk(p_73154_1_, p_73154_2_)) : var3;
		}

///    
///     <summary> * used by loadChunk, but catches any exceptions if the load fails. </summary>
///     
		private Chunk safeLoadChunk(int p_73239_1_, int p_73239_2_)
		{
			if (this.currentChunkLoader == null)
			{
				return null;
			}
			else
			{
				try
				{
					Chunk var3 = this.currentChunkLoader.loadChunk(this.worldObj, p_73239_1_, p_73239_2_);

					if (var3 != null)
					{
						var3.lastSaveTime = this.worldObj.TotalWorldTime;

						if (this.currentChunkProvider != null)
						{
							this.currentChunkProvider.recreateStructures(p_73239_1_, p_73239_2_);
						}
					}

					return var3;
				}
				catch (Exception var4)
				{
					logger.error("Couldn\'t load chunk", var4);
					return null;
				}
			}
		}

///    
///     <summary> * used by saveChunks, but catches any exceptions if the save fails. </summary>
///     
		private void safeSaveExtraChunkData(Chunk p_73243_1_)
		{
			if (this.currentChunkLoader != null)
			{
				try
				{
					this.currentChunkLoader.saveExtraChunkData(this.worldObj, p_73243_1_);
				}
				catch (Exception var3)
				{
					logger.error("Couldn\'t save entities", var3);
				}
			}
		}

///    
///     <summary> * used by saveChunks, but catches any exceptions if the save fails. </summary>
///     
		private void safeSaveChunk(Chunk p_73242_1_)
		{
			if (this.currentChunkLoader != null)
			{
				try
				{
					p_73242_1_.lastSaveTime = this.worldObj.TotalWorldTime;
					this.currentChunkLoader.saveChunk(this.worldObj, p_73242_1_);
				}
				catch (IOException var3)
				{
					logger.error("Couldn\'t save chunk", var3);
				}
				catch (MinecraftException var4)
				{
					logger.error("Couldn\'t save chunk; already in use by another instance of Minecraft?", var4);
				}
			}
		}

///    
///     <summary> * Populates chunk with ores etc etc </summary>
///     
		public virtual void populate(IChunkProvider p_73153_1_, int p_73153_2_, int p_73153_3_)
		{
			Chunk var4 = this.provideChunk(p_73153_2_, p_73153_3_);

			if (!var4.isTerrainPopulated)
			{
				var4.func_150809_p();

				if (this.currentChunkProvider != null)
				{
					this.currentChunkProvider.populate(p_73153_1_, p_73153_2_, p_73153_3_);
					var4.setChunkModified();
				}
			}
		}

///    
///     <summary> * Two modes of operation: if passed true, save all Chunks in one go.  If passed false, save up to two chunks.
///     * Return true if all chunks have been saved. </summary>
///     
		public virtual bool saveChunks(bool p_73151_1_, IProgressUpdate p_73151_2_)
		{
			int var3 = 0;
			ArrayList var4 = Lists.newArrayList(this.loadedChunks);

			for (int var5 = 0; var5 < var4.Count; ++var5)
			{
				Chunk var6 = (Chunk)var4[var5];

				if (p_73151_1_)
				{
					this.safeSaveExtraChunkData(var6);
				}

				if (var6.needsSaving(p_73151_1_))
				{
					this.safeSaveChunk(var6);
					var6.isModified = false;
					++var3;

					if (var3 == 24 && !p_73151_1_)
					{
						return false;
					}
				}
			}

			return true;
		}

///    
///     <summary> * Save extra data not associated with any Chunk.  Not saved during autosave, only during world unload.  Currently
///     * unimplemented. </summary>
///     
		public virtual void saveExtraData()
		{
			if (this.currentChunkLoader != null)
			{
				this.currentChunkLoader.saveExtraData();
			}
		}

///    
///     <summary> * Unloads chunks that are marked to be unloaded. This is not guaranteed to unload every such chunk. </summary>
///     
		public virtual bool unloadQueuedChunks()
		{
			if (!this.worldObj.levelSaving)
			{
				for (int var1 = 0; var1 < 100; ++var1)
				{
					if (!this.chunksToUnload.Empty)
					{
						long? var2 = (long?)this.chunksToUnload.GetEnumerator().next();
						Chunk var3 = (Chunk)this.loadedChunkHashMap.getValueByKey((long)var2);

						if (var3 != null)
						{
							var3.onChunkUnload();
							this.safeSaveChunk(var3);
							this.safeSaveExtraChunkData(var3);
							this.loadedChunks.Remove(var3);
						}

						this.chunksToUnload.remove(var2);
						this.loadedChunkHashMap.remove((long)var2);
					}
				}

				if (this.currentChunkLoader != null)
				{
					this.currentChunkLoader.chunkTick();
				}
			}

			return this.currentChunkProvider.unloadQueuedChunks();
		}

///    
///     <summary> * Returns if the IChunkProvider supports saving. </summary>
///     
		public virtual bool canSave()
		{
			return !this.worldObj.levelSaving;
		}

///    
///     <summary> * Converts the instance data to a readable string. </summary>
///     
		public virtual string makeString()
		{
			return "ServerChunkCache: " + this.loadedChunkHashMap.NumHashElements + " Drop: " + this.chunksToUnload.size();
		}

///    
///     <summary> * Returns a list of creatures of the specified type that can spawn at the given location. </summary>
///     
		public virtual IList getPossibleCreatures(EnumCreatureType p_73155_1_, int p_73155_2_, int p_73155_3_, int p_73155_4_)
		{
			return this.currentChunkProvider.getPossibleCreatures(p_73155_1_, p_73155_2_, p_73155_3_, p_73155_4_);
		}

		public virtual ChunkPosition func_147416_a(World p_147416_1_, string p_147416_2_, int p_147416_3_, int p_147416_4_, int p_147416_5_)
		{
			return this.currentChunkProvider.func_147416_a(p_147416_1_, p_147416_2_, p_147416_3_, p_147416_4_, p_147416_5_);
		}

		public virtual int LoadedChunkCount
		{
			get
			{
				return this.loadedChunkHashMap.NumHashElements;
			}
		}

		public virtual void recreateStructures(int p_82695_1_, int p_82695_2_)
		{
		}
	}

}