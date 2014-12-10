using System;
using System.Collections;

namespace DotCraftCore.World.Chunk.Storage
{

	using Block = DotCraftCore.block.Block;
	using Entity = DotCraftCore.entity.Entity;
	using EntityList = DotCraftCore.entity.EntityList;
	using CompressedStreamTools = DotCraftCore.nbt.CompressedStreamTools;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using NBTTagList = DotCraftCore.nbt.NBTTagList;
	using TileEntity = DotCraftCore.TileEntity.TileEntity;
	using ChunkCoordIntPair = DotCraftCore.World.ChunkCoordIntPair;
	using MinecraftException = DotCraftCore.World.MinecraftException;
	using NextTickListEntry = DotCraftCore.World.NextTickListEntry;
	using World = DotCraftCore.World.World;
	using Chunk = DotCraftCore.World.Chunk.Chunk;
	using NibbleArray = DotCraftCore.World.Chunk.NibbleArray;
	using IThreadedFileIO = DotCraftCore.World.Storage.IThreadedFileIO;
	using ThreadedFileIOBase = DotCraftCore.World.Storage.ThreadedFileIOBase;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class AnvilChunkLoader : IChunkLoader, IThreadedFileIO
	{
		private static readonly Logger logger = LogManager.Logger;
		private IList chunksToRemove = new ArrayList();
		private Set pendingAnvilChunksCoordinates = new HashSet();
		private object syncLockObject = new object();

	/// <summary> Save directory for chunks using the Anvil format  </summary>
		private readonly File chunkSaveLocation;
		private const string __OBFID = "CL_00000384";

		public AnvilChunkLoader(File p_i2003_1_)
		{
			this.chunkSaveLocation = p_i2003_1_;
		}

///    
///     <summary> * Loads the specified(XZ) chunk into the specified world. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Chunk loadChunk(World p_75815_1_, int p_75815_2_, int p_75815_3_) throws IOException
		public virtual Chunk loadChunk(World p_75815_1_, int p_75815_2_, int p_75815_3_)
		{
			NBTTagCompound var4 = null;
			ChunkCoordIntPair var5 = new ChunkCoordIntPair(p_75815_2_, p_75815_3_);
			object var6 = this.syncLockObject;

			lock (this.syncLockObject)
			{
				if (this.pendingAnvilChunksCoordinates.contains(var5))
				{
					for (int var7 = 0; var7 < this.chunksToRemove.Count; ++var7)
					{
						if (((AnvilChunkLoader.PendingChunk)this.chunksToRemove.get(var7)).chunkCoordinate.Equals(var5))
						{
							var4 = ((AnvilChunkLoader.PendingChunk)this.chunksToRemove.get(var7)).nbtTags;
							break;
						}
					}
				}
			}

			if (var4 == null)
			{
				DataInputStream var10 = RegionFileCache.getChunkInputStream(this.chunkSaveLocation, p_75815_2_, p_75815_3_);

				if (var10 == null)
				{
					return null;
				}

				var4 = CompressedStreamTools.read(var10);
			}

			return this.checkedReadChunkFromNBT(p_75815_1_, p_75815_2_, p_75815_3_, var4);
		}

///    
///     <summary> * Wraps readChunkFromNBT. Checks the coordinates and several NBT tags. </summary>
///     
		protected internal virtual Chunk checkedReadChunkFromNBT(World p_75822_1_, int p_75822_2_, int p_75822_3_, NBTTagCompound p_75822_4_)
		{
			if (!p_75822_4_.func_150297_b("Level", 10))
			{
				logger.error("Chunk file at " + p_75822_2_ + "," + p_75822_3_ + " is missing level data, skipping");
				return null;
			}
			else if (!p_75822_4_.getCompoundTag("Level").func_150297_b("Sections", 9))
			{
				logger.error("Chunk file at " + p_75822_2_ + "," + p_75822_3_ + " is missing block data, skipping");
				return null;
			}
			else
			{
				Chunk var5 = this.readChunkFromNBT(p_75822_1_, p_75822_4_.getCompoundTag("Level"));

				if (!var5.isAtLocation(p_75822_2_, p_75822_3_))
				{
					logger.error("Chunk file at " + p_75822_2_ + "," + p_75822_3_ + " is in the wrong location; relocating. (Expected " + p_75822_2_ + ", " + p_75822_3_ + ", got " + var5.xPosition + ", " + var5.zPosition + ")");
					p_75822_4_.setInteger("xPos", p_75822_2_);
					p_75822_4_.setInteger("zPos", p_75822_3_);
					var5 = this.readChunkFromNBT(p_75822_1_, p_75822_4_.getCompoundTag("Level"));
				}

				return var5;
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void saveChunk(World p_75816_1_, Chunk p_75816_2_) throws MinecraftException, IOException
		public virtual void saveChunk(World p_75816_1_, Chunk p_75816_2_)
		{
			p_75816_1_.checkSessionLock();

			try
			{
				NBTTagCompound var3 = new NBTTagCompound();
				NBTTagCompound var4 = new NBTTagCompound();
				var3.setTag("Level", var4);
				this.writeChunkToNBT(p_75816_2_, p_75816_1_, var4);
				this.addChunkToPending(p_75816_2_.ChunkCoordIntPair, var3);
			}
			catch (Exception var5)
			{
				var5.printStackTrace();
			}
		}

		protected internal virtual void addChunkToPending(ChunkCoordIntPair p_75824_1_, NBTTagCompound p_75824_2_)
		{
			object var3 = this.syncLockObject;

			lock (this.syncLockObject)
			{
				if (this.pendingAnvilChunksCoordinates.contains(p_75824_1_))
				{
					for (int var4 = 0; var4 < this.chunksToRemove.Count; ++var4)
					{
						if (((AnvilChunkLoader.PendingChunk)this.chunksToRemove.get(var4)).chunkCoordinate.Equals(p_75824_1_))
						{
							this.chunksToRemove[var4] = new AnvilChunkLoader.PendingChunk(p_75824_1_, p_75824_2_);
							return;
						}
					}
				}

				this.chunksToRemove.Add(new AnvilChunkLoader.PendingChunk(p_75824_1_, p_75824_2_));
				this.pendingAnvilChunksCoordinates.add(p_75824_1_);
				ThreadedFileIOBase.threadedIOInstance.queueIO(this);
			}
		}

///    
///     <summary> * Returns a boolean stating if the write was unsuccessful. </summary>
///     
		public virtual bool writeNextIO()
		{
			AnvilChunkLoader.PendingChunk var1 = null;
			object var2 = this.syncLockObject;

			lock (this.syncLockObject)
			{
				if (this.chunksToRemove.Count == 0)
				{
					return false;
				}

				var1 = (AnvilChunkLoader.PendingChunk)this.chunksToRemove.remove(0);
				this.pendingAnvilChunksCoordinates.remove(var1.chunkCoordinate);
			}

			if (var1 != null)
			{
				try
				{
					this.writeChunkNBTTags(var1);
				}
				catch (Exception var4)
				{
					var4.printStackTrace();
				}
			}

			return true;
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private void writeChunkNBTTags(AnvilChunkLoader.PendingChunk p_75821_1_) throws IOException
		private void writeChunkNBTTags(AnvilChunkLoader.PendingChunk p_75821_1_)
		{
			DataOutputStream var2 = RegionFileCache.getChunkOutputStream(this.chunkSaveLocation, p_75821_1_.chunkCoordinate.chunkXPos, p_75821_1_.chunkCoordinate.chunkZPos);
			CompressedStreamTools.write(p_75821_1_.nbtTags, var2);
			var2.close();
		}

///    
///     <summary> * Save extra data associated with this Chunk not normally saved during autosave, only during chunk unload.
///     * Currently unused. </summary>
///     
		public virtual void saveExtraChunkData(World p_75819_1_, Chunk p_75819_2_)
		{
		}

///    
///     <summary> * Called every World.tick() </summary>
///     
		public virtual void chunkTick()
		{
		}

///    
///     <summary> * Save extra data not associated with any Chunk.  Not saved during autosave, only during world unload.  Currently
///     * unused. </summary>
///     
		public virtual void saveExtraData()
		{
			while (this.writeNextIO())
			{
				;
			}
		}

///    
///     <summary> * Writes the Chunk passed as an argument to the NBTTagCompound also passed, using the World argument to retrieve
///     * the Chunk's last update time. </summary>
///     
		private void writeChunkToNBT(Chunk p_75820_1_, World p_75820_2_, NBTTagCompound p_75820_3_)
		{
			p_75820_3_.setByte("V", (sbyte)1);
			p_75820_3_.setInteger("xPos", p_75820_1_.xPosition);
			p_75820_3_.setInteger("zPos", p_75820_1_.zPosition);
			p_75820_3_.setLong("LastUpdate", p_75820_2_.TotalWorldTime);
			p_75820_3_.setIntArray("HeightMap", p_75820_1_.heightMap);
			p_75820_3_.setBoolean("TerrainPopulated", p_75820_1_.isTerrainPopulated);
			p_75820_3_.setBoolean("LightPopulated", p_75820_1_.isLightPopulated);
			p_75820_3_.setLong("InhabitedTime", p_75820_1_.inhabitedTime);
			ExtendedBlockStorage[] var4 = p_75820_1_.BlockStorageArray;
			NBTTagList var5 = new NBTTagList();
			bool var6 = !p_75820_2_.provider.hasNoSky;
			ExtendedBlockStorage[] var7 = var4;
			int var8 = var4.Length;
			NBTTagCompound var11;

			for (int var9 = 0; var9 < var8; ++var9)
			{
				ExtendedBlockStorage var10 = var7[var9];

				if (var10 != null)
				{
					var11 = new NBTTagCompound();
					var11.setByte("Y", (sbyte)(var10.YLocation >> 4 & 255));
					var11.setByteArray("Blocks", var10.BlockLSBArray);

					if (var10.BlockMSBArray != null)
					{
						var11.setByteArray("Add", var10.BlockMSBArray.data);
					}

					var11.setByteArray("Data", var10.MetadataArray.data);
					var11.setByteArray("BlockLight", var10.BlocklightArray.data);

					if (var6)
					{
						var11.setByteArray("SkyLight", var10.SkylightArray.data);
					}
					else
					{
						var11.setByteArray("SkyLight", new sbyte[var10.BlocklightArray.data.Length]);
					}

					var5.appendTag(var11);
				}
			}

			p_75820_3_.setTag("Sections", var5);
			p_75820_3_.setByteArray("Biomes", p_75820_1_.BiomeArray);
			p_75820_1_.hasEntities = false;
			NBTTagList var16 = new NBTTagList();
			IEnumerator var18;

			for (var8 = 0; var8 < p_75820_1_.entityLists.Length; ++var8)
			{
				var18 = p_75820_1_.entityLists[var8].GetEnumerator();

				while (var18.MoveNext())
				{
					Entity var20 = (Entity)var18.Current;
					var11 = new NBTTagCompound();

					if (var20.writeToNBTOptional(var11))
					{
						p_75820_1_.hasEntities = true;
						var16.appendTag(var11);
					}
				}
			}

			p_75820_3_.setTag("Entities", var16);
			NBTTagList var17 = new NBTTagList();
			var18 = p_75820_1_.chunkTileEntityMap.Values.GetEnumerator();

			while (var18.MoveNext())
			{
				TileEntity var21 = (TileEntity)var18.Current;
				var11 = new NBTTagCompound();
				var21.writeToNBT(var11);
				var17.appendTag(var11);
			}

			p_75820_3_.setTag("TileEntities", var17);
			IList var19 = p_75820_2_.getPendingBlockUpdates(p_75820_1_, false);

			if (var19 != null)
			{
				long var22 = p_75820_2_.TotalWorldTime;
				NBTTagList var12 = new NBTTagList();
				IEnumerator var13 = var19.GetEnumerator();

				while (var13.MoveNext())
				{
					NextTickListEntry var14 = (NextTickListEntry)var13.Current;
					NBTTagCompound var15 = new NBTTagCompound();
					var15.setInteger("i", Block.getIdFromBlock(var14.func_151351_a()));
					var15.setInteger("x", var14.xCoord);
					var15.setInteger("y", var14.yCoord);
					var15.setInteger("z", var14.zCoord);
					var15.setInteger("t", (int)(var14.scheduledTime - var22));
					var15.setInteger("p", var14.priority);
					var12.appendTag(var15);
				}

				p_75820_3_.setTag("TileTicks", var12);
			}
		}

///    
///     <summary> * Reads the data stored in the passed NBTTagCompound and creates a Chunk with that data in the passed World.
///     * Returns the created Chunk. </summary>
///     
		private Chunk readChunkFromNBT(World p_75823_1_, NBTTagCompound p_75823_2_)
		{
			int var3 = p_75823_2_.getInteger("xPos");
			int var4 = p_75823_2_.getInteger("zPos");
			Chunk var5 = new Chunk(p_75823_1_, var3, var4);
			var5.heightMap = p_75823_2_.getIntArray("HeightMap");
			var5.isTerrainPopulated = p_75823_2_.getBoolean("TerrainPopulated");
			var5.isLightPopulated = p_75823_2_.getBoolean("LightPopulated");
			var5.inhabitedTime = p_75823_2_.getLong("InhabitedTime");
			NBTTagList var6 = p_75823_2_.getTagList("Sections", 10);
			sbyte var7 = 16;
			ExtendedBlockStorage[] var8 = new ExtendedBlockStorage[var7];
			bool var9 = !p_75823_1_.provider.hasNoSky;

			for (int var10 = 0; var10 < var6.tagCount(); ++var10)
			{
				NBTTagCompound var11 = var6.getCompoundTagAt(var10);
				sbyte var12 = var11.getByte("Y");
				ExtendedBlockStorage var13 = new ExtendedBlockStorage(var12 << 4, var9);
				var13.BlockLSBArray = var11.getByteArray("Blocks");

				if (var11.func_150297_b("Add", 7))
				{
					var13.BlockMSBArray = new NibbleArray(var11.getByteArray("Add"), 4);
				}

				var13.BlockMetadataArray = new NibbleArray(var11.getByteArray("Data"), 4);
				var13.BlocklightArray = new NibbleArray(var11.getByteArray("BlockLight"), 4);

				if (var9)
				{
					var13.SkylightArray = new NibbleArray(var11.getByteArray("SkyLight"), 4);
				}

				var13.removeInvalidBlocks();
				var8[var12] = var13;
			}

			var5.StorageArrays = var8;

			if (p_75823_2_.func_150297_b("Biomes", 7))
			{
				var5.BiomeArray = p_75823_2_.getByteArray("Biomes");
			}

			NBTTagList var17 = p_75823_2_.getTagList("Entities", 10);

			if (var17 != null)
			{
				for (int var18 = 0; var18 < var17.tagCount(); ++var18)
				{
					NBTTagCompound var20 = var17.getCompoundTagAt(var18);
					Entity var23 = EntityList.createEntityFromNBT(var20, p_75823_1_);
					var5.hasEntities = true;

					if (var23 != null)
					{
						var5.addEntity(var23);
						Entity var14 = var23;

						for (NBTTagCompound var15 = var20; var15.func_150297_b("Riding", 10); var15 = var15.getCompoundTag("Riding"))
						{
							Entity var16 = EntityList.createEntityFromNBT(var15.getCompoundTag("Riding"), p_75823_1_);

							if (var16 != null)
							{
								var5.addEntity(var16);
								var14.mountEntity(var16);
							}

							var14 = var16;
						}
					}
				}
			}

			NBTTagList var19 = p_75823_2_.getTagList("TileEntities", 10);

			if (var19 != null)
			{
				for (int var21 = 0; var21 < var19.tagCount(); ++var21)
				{
					NBTTagCompound var24 = var19.getCompoundTagAt(var21);
					TileEntity var26 = TileEntity.createAndLoadEntity(var24);

					if (var26 != null)
					{
						var5.addTileEntity(var26);
					}
				}
			}

			if (p_75823_2_.func_150297_b("TileTicks", 9))
			{
				NBTTagList var22 = p_75823_2_.getTagList("TileTicks", 10);

				if (var22 != null)
				{
					for (int var25 = 0; var25 < var22.tagCount(); ++var25)
					{
						NBTTagCompound var27 = var22.getCompoundTagAt(var25);
						p_75823_1_.func_147446_b(var27.getInteger("x"), var27.getInteger("y"), var27.getInteger("z"), Block.getBlockById(var27.getInteger("i")), var27.getInteger("t"), var27.getInteger("p"));
					}
				}
			}

			return var5;
		}

		internal class PendingChunk
		{
			public readonly ChunkCoordIntPair chunkCoordinate;
			public readonly NBTTagCompound nbtTags;
			private const string __OBFID = "CL_00000385";

			public PendingChunk(ChunkCoordIntPair p_i2002_1_, NBTTagCompound p_i2002_2_)
			{
				this.chunkCoordinate = p_i2002_1_;
				this.nbtTags = p_i2002_2_;
			}
		}
	}

}