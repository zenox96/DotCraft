using System;
using System.Collections;
using System.Threading;

namespace DotCraftCore.nWorld.nChunk
{

	using Block = DotCraftCore.nBlock.Block;
	using ITileEntityProvider = DotCraftCore.nBlock.ITileEntityProvider;
	using Material = DotCraftCore.nBlock.nMaterial.Material;
	using IEntitySelector = DotCraftCore.nCommand.IEntitySelector;
	using CrashReport = DotCraftCore.crash.CrashReport;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using Entity = DotCraftCore.entity.Entity;
	using Blocks = DotCraftCore.init.Blocks;
	using TileEntity = DotCraftCore.nTileEntity.TileEntity;
	using AxisAlignedBB = DotCraftCore.nUtil.AxisAlignedBB;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using ReportedException = DotCraftCore.nUtil.ReportedException;
	using ChunkCoordIntPair = DotCraftCore.nWorld.ChunkCoordIntPair;
	using ChunkPosition = DotCraftCore.nWorld.ChunkPosition;
	using EnumSkyBlock = DotCraftCore.nWorld.EnumSkyBlock;
	using World = DotCraftCore.nWorld.World;
	using BiomeGenBase = DotCraftCore.nWorld.nBiome.BiomeGenBase;
	using WorldChunkManager = DotCraftCore.nWorld.nBiome.WorldChunkManager;
	using ExtendedBlockStorage = DotCraftCore.nWorld.nChunk.nStorage.ExtendedBlockStorage;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class Chunk
	{
		private static readonly Logger logger = LogManager.Logger;

///    
///     <summary> * Determines if the chunk is lit or not at a light value greater than 0. </summary>
///     
		public static bool isLit;

///    
///     <summary> * Used to store block IDs, block MSBs, Sky-light maps, Block-light maps, and metadata. Each entry corresponds to a
///     * logical segment of 16x16x16 blocks, stacked vertically. </summary>
///     
		private ExtendedBlockStorage[] storageArrays;

///    
///     <summary> * Contains a 16x16 mapping on the X/Z plane of the biome ID to which each colum belongs. </summary>
///     
		private sbyte[] blockBiomeArray;

///    
///     <summary> * A map, similar to heightMap, that tracks how far down precipitation can fall. </summary>
///     
		public int[] precipitationHeightMap;

	/// <summary> Which columns need their skylightMaps updated.  </summary>
		public bool[] updateSkylightColumns;

	/// <summary> Whether or not this Chunk is currently loaded into the World  </summary>
		public bool isChunkLoaded;

	/// <summary> Reference to the World object.  </summary>
		public World worldObj;
		public int[] heightMap;

	/// <summary> The x coordinate of the chunk.  </summary>
		public readonly int xPosition;

	/// <summary> The z coordinate of the chunk.  </summary>
		public readonly int zPosition;
		private bool isGapLightingUpdated;

	/// <summary> A Map of ChunkPositions to TileEntities in this chunk  </summary>
		public IDictionary chunkTileEntityMap;

///    
///     <summary> * Array of Lists containing the entities in this Chunk. Each List represents a 16 block subchunk. </summary>
///     
		public IList[] entityLists;

	/// <summary> Boolean value indicating if the terrain is populated.  </summary>
		public bool isTerrainPopulated;
		public bool isLightPopulated;
		public bool field_150815_m;

///    
///     <summary> * Set to true if the chunk has been modified and needs to be updated internally. </summary>
///     
		public bool isModified;

///    
///     <summary> * Whether this Chunk has any Entities and thus requires saving on every tick </summary>
///     
		public bool hasEntities;

	/// <summary> The time according to World.worldTime when this chunk was last saved  </summary>
		public long lastSaveTime;

///    
///     <summary> * Updates to this chunk will not be sent to clients if this is false. This field is set to true the first time the
///     * chunk is sent to a client, and never set to false. </summary>
///     
		public bool sendUpdates;

	/// <summary> Lowest value in the heightmap.  </summary>
		public int heightMapMinimum;

	/// <summary> the cumulative number of ticks players have been in this chunk  </summary>
		public long inhabitedTime;

///    
///     <summary> * Contains the current round-robin relight check index, and is implied as the relight check location as well. </summary>
///     
		private int queuedLightChecks;
		

		public Chunk(World p_i1995_1_, int p_i1995_2_, int p_i1995_3_)
		{
			this.storageArrays = new ExtendedBlockStorage[16];
			this.blockBiomeArray = new sbyte[256];
			this.precipitationHeightMap = new int[256];
			this.updateSkylightColumns = new bool[256];
			this.chunkTileEntityMap = new Hashtable();
			this.queuedLightChecks = 4096;
			this.entityLists = new IList[16];
			this.worldObj = p_i1995_1_;
			this.xPosition = p_i1995_2_;
			this.zPosition = p_i1995_3_;
			this.heightMap = new int[256];

			for (int var4 = 0; var4 < this.entityLists.Length; ++var4)
			{
				this.entityLists[var4] = new ArrayList();
			}

			Arrays.fill(this.precipitationHeightMap, -999);
			Arrays.fill(this.blockBiomeArray, (sbyte) - 1);
		}

		public Chunk(World p_i45446_1_, Block[] p_i45446_2_, int p_i45446_3_, int p_i45446_4_) : this(p_i45446_1_, p_i45446_3_, p_i45446_4_)
		{
			int var5 = p_i45446_2_.Length / 256;
			bool var6 = !p_i45446_1_.provider.hasNoSky;

			for (int var7 = 0; var7 < 16; ++var7)
			{
				for (int var8 = 0; var8 < 16; ++var8)
				{
					for (int var9 = 0; var9 < var5; ++var9)
					{
						Block var10 = p_i45446_2_[var7 << 11 | var8 << 7 | var9];

						if (var10 != null && var10.Material != Material.air)
						{
							int var11 = var9 >> 4;

							if (this.storageArrays[var11] == null)
							{
								this.storageArrays[var11] = new ExtendedBlockStorage(var11 << 4, var6);
							}

							this.storageArrays[var11].func_150818_a(var7, var9 & 15, var8, var10);
						}
					}
				}
			}
		}

		public Chunk(World p_i45447_1_, Block[] p_i45447_2_, sbyte[] p_i45447_3_, int p_i45447_4_, int p_i45447_5_) : this(p_i45447_1_, p_i45447_4_, p_i45447_5_)
		{
			int var6 = p_i45447_2_.Length / 256;
			bool var7 = !p_i45447_1_.provider.hasNoSky;

			for (int var8 = 0; var8 < 16; ++var8)
			{
				for (int var9 = 0; var9 < 16; ++var9)
				{
					for (int var10 = 0; var10 < var6; ++var10)
					{
						int var11 = var8 * var6 * 16 | var9 * var6 | var10;
						Block var12 = p_i45447_2_[var11];

						if (var12 != null && var12 != Blocks.air)
						{
							int var13 = var10 >> 4;

							if (this.storageArrays[var13] == null)
							{
								this.storageArrays[var13] = new ExtendedBlockStorage(var13 << 4, var7);
							}

							this.storageArrays[var13].func_150818_a(var8, var10 & 15, var9, var12);
							this.storageArrays[var13].setExtBlockMetadata(var8, var10 & 15, var9, p_i45447_3_[var11]);
						}
					}
				}
			}
		}

///    
///     <summary> * Checks whether the chunk is at the X/Z location specified </summary>
///     
		public virtual bool isAtLocation(int p_76600_1_, int p_76600_2_)
		{
			return p_76600_1_ == this.xPosition && p_76600_2_ == this.zPosition;
		}

///    
///     <summary> * Returns the value in the height map at this x, z coordinate in the chunk </summary>
///     
		public virtual int getHeightValue(int p_76611_1_, int p_76611_2_)
		{
			return this.heightMap[p_76611_2_ << 4 | p_76611_1_];
		}

///    
///     <summary> * Returns the topmost ExtendedBlockStorage instance for this Chunk that actually contains a block. </summary>
///     
		public virtual int TopFilledSegment
		{
			get
			{
				for (int var1 = this.storageArrays.Length - 1; var1 >= 0; --var1)
				{
					if (this.storageArrays[var1] != null)
					{
						return this.storageArrays[var1].YLocation;
					}
				}
	
				return 0;
			}
		}

///    
///     <summary> * Returns the ExtendedBlockStorage array for this Chunk. </summary>
///     
		public virtual ExtendedBlockStorage[] BlockStorageArray
		{
			get
			{
				return this.storageArrays;
			}
		}

///    
///     <summary> * Generates the height map for a chunk from scratch </summary>
///     
		public virtual void generateHeightMap()
		{
			int var1 = this.TopFilledSegment;
			this.heightMapMinimum = int.MaxValue;

			for (int var2 = 0; var2 < 16; ++var2)
			{
				int var3 = 0;

				while (var3 < 16)
				{
					this.precipitationHeightMap[var2 + (var3 << 4)] = -999;
					int var4 = var1 + 16 - 1;

					while (true)
					{
						if (var4 > 0)
						{
							Block var5 = this.func_150810_a(var2, var4 - 1, var3);

							if (var5.LightOpacity == 0)
							{
								--var4;
								continue;
							}

							this.heightMap[var3 << 4 | var2] = var4;

							if (var4 < this.heightMapMinimum)
							{
								this.heightMapMinimum = var4;
							}
						}

						++var3;
						break;
					}
				}
			}

			this.isModified = true;
		}

///    
///     <summary> * Generates the initial skylight map for the chunk upon generation or load. </summary>
///     
		public virtual void generateSkylightMap()
		{
			int var1 = this.TopFilledSegment;
			this.heightMapMinimum = int.MaxValue;

			for (int var2 = 0; var2 < 16; ++var2)
			{
				int var3 = 0;

				while (var3 < 16)
				{
					this.precipitationHeightMap[var2 + (var3 << 4)] = -999;
					int var4 = var1 + 16 - 1;

					while (true)
					{
						if (var4 > 0)
						{
							if (this.func_150808_b(var2, var4 - 1, var3) == 0)
							{
								--var4;
								continue;
							}

							this.heightMap[var3 << 4 | var2] = var4;

							if (var4 < this.heightMapMinimum)
							{
								this.heightMapMinimum = var4;
							}
						}

						if (!this.worldObj.provider.hasNoSky)
						{
							var4 = 15;
							int var5 = var1 + 16 - 1;

							do
							{
								int var6 = this.func_150808_b(var2, var5, var3);

								if (var6 == 0 && var4 != 15)
								{
									var6 = 1;
								}

								var4 -= var6;

								if (var4 > 0)
								{
									ExtendedBlockStorage var7 = this.storageArrays[var5 >> 4];

									if (var7 != null)
									{
										var7.setExtSkylightValue(var2, var5 & 15, var3, var4);
										this.worldObj.func_147479_m((this.xPosition << 4) + var2, var5, (this.zPosition << 4) + var3);
									}
								}

								--var5;
							}
							while (var5 > 0 && var4 > 0);
						}

						++var3;
						break;
					}
				}
			}

			this.isModified = true;
		}

///    
///     <summary> * Propagates a given sky-visible block's light value downward and upward to neighboring blocks as necessary. </summary>
///     
		private void propagateSkylightOcclusion(int p_76595_1_, int p_76595_2_)
		{
			this.updateSkylightColumns[p_76595_1_ + p_76595_2_ * 16] = true;
			this.isGapLightingUpdated = true;
		}

		private void recheckGaps(bool p_150803_1_)
		{
			this.worldObj.theProfiler.startSection("recheckGaps");

			if (this.worldObj.doChunksNearChunkExist(this.xPosition * 16 + 8, 0, this.zPosition * 16 + 8, 16))
			{
				for (int var2 = 0; var2 < 16; ++var2)
				{
					for (int var3 = 0; var3 < 16; ++var3)
					{
						if (this.updateSkylightColumns[var2 + var3 * 16])
						{
							this.updateSkylightColumns[var2 + var3 * 16] = false;
							int var4 = this.getHeightValue(var2, var3);
							int var5 = this.xPosition * 16 + var2;
							int var6 = this.zPosition * 16 + var3;
							int var7 = this.worldObj.getChunkHeightMapMinimum(var5 - 1, var6);
							int var8 = this.worldObj.getChunkHeightMapMinimum(var5 + 1, var6);
							int var9 = this.worldObj.getChunkHeightMapMinimum(var5, var6 - 1);
							int var10 = this.worldObj.getChunkHeightMapMinimum(var5, var6 + 1);

							if (var8 < var7)
							{
								var7 = var8;
							}

							if (var9 < var7)
							{
								var7 = var9;
							}

							if (var10 < var7)
							{
								var7 = var10;
							}

							this.checkSkylightNeighborHeight(var5, var6, var7);
							this.checkSkylightNeighborHeight(var5 - 1, var6, var4);
							this.checkSkylightNeighborHeight(var5 + 1, var6, var4);
							this.checkSkylightNeighborHeight(var5, var6 - 1, var4);
							this.checkSkylightNeighborHeight(var5, var6 + 1, var4);

							if (p_150803_1_)
							{
								this.worldObj.theProfiler.endSection();
								return;
							}
						}
					}
				}

				this.isGapLightingUpdated = false;
			}

			this.worldObj.theProfiler.endSection();
		}

///    
///     <summary> * Checks the height of a block next to a sky-visible block and schedules a lighting update as necessary. </summary>
///     
		private void checkSkylightNeighborHeight(int p_76599_1_, int p_76599_2_, int p_76599_3_)
		{
			int var4 = this.worldObj.getHeightValue(p_76599_1_, p_76599_2_);

			if (var4 > p_76599_3_)
			{
				this.updateSkylightNeighborHeight(p_76599_1_, p_76599_2_, p_76599_3_, var4 + 1);
			}
			else if (var4 < p_76599_3_)
			{
				this.updateSkylightNeighborHeight(p_76599_1_, p_76599_2_, var4, p_76599_3_ + 1);
			}
		}

		private void updateSkylightNeighborHeight(int p_76609_1_, int p_76609_2_, int p_76609_3_, int p_76609_4_)
		{
			if (p_76609_4_ > p_76609_3_ && this.worldObj.doChunksNearChunkExist(p_76609_1_, 0, p_76609_2_, 16))
			{
				for (int var5 = p_76609_3_; var5 < p_76609_4_; ++var5)
				{
					this.worldObj.updateLightByType(EnumSkyBlock.Sky, p_76609_1_, var5, p_76609_2_);
				}

				this.isModified = true;
			}
		}

///    
///     <summary> * Initiates the recalculation of both the block-light and sky-light for a given block inside a chunk. </summary>
///     
		private void relightBlock(int p_76615_1_, int p_76615_2_, int p_76615_3_)
		{
			int var4 = this.heightMap[p_76615_3_ << 4 | p_76615_1_] & 255;
			int var5 = var4;

			if (p_76615_2_ > var4)
			{
				var5 = p_76615_2_;
			}

			while (var5 > 0 && this.func_150808_b(p_76615_1_, var5 - 1, p_76615_3_) == 0)
			{
				--var5;
			}

			if (var5 != var4)
			{
				this.worldObj.markBlocksDirtyVertical(p_76615_1_ + this.xPosition * 16, p_76615_3_ + this.zPosition * 16, var5, var4);
				this.heightMap[p_76615_3_ << 4 | p_76615_1_] = var5;
				int var6 = this.xPosition * 16 + p_76615_1_;
				int var7 = this.zPosition * 16 + p_76615_3_;
				int var8;
				int var12;

				if (!this.worldObj.provider.hasNoSky)
				{
					ExtendedBlockStorage var9;

					if (var5 < var4)
					{
						for (var8 = var5; var8 < var4; ++var8)
						{
							var9 = this.storageArrays[var8 >> 4];

							if (var9 != null)
							{
								var9.setExtSkylightValue(p_76615_1_, var8 & 15, p_76615_3_, 15);
								this.worldObj.func_147479_m((this.xPosition << 4) + p_76615_1_, var8, (this.zPosition << 4) + p_76615_3_);
							}
						}
					}
					else
					{
						for (var8 = var4; var8 < var5; ++var8)
						{
							var9 = this.storageArrays[var8 >> 4];

							if (var9 != null)
							{
								var9.setExtSkylightValue(p_76615_1_, var8 & 15, p_76615_3_, 0);
								this.worldObj.func_147479_m((this.xPosition << 4) + p_76615_1_, var8, (this.zPosition << 4) + p_76615_3_);
							}
						}
					}

					var8 = 15;

					while (var5 > 0 && var8 > 0)
					{
						--var5;
						var12 = this.func_150808_b(p_76615_1_, var5, p_76615_3_);

						if (var12 == 0)
						{
							var12 = 1;
						}

						var8 -= var12;

						if (var8 < 0)
						{
							var8 = 0;
						}

						ExtendedBlockStorage var10 = this.storageArrays[var5 >> 4];

						if (var10 != null)
						{
							var10.setExtSkylightValue(p_76615_1_, var5 & 15, p_76615_3_, var8);
						}
					}
				}

				var8 = this.heightMap[p_76615_3_ << 4 | p_76615_1_];
				var12 = var4;
				int var13 = var8;

				if (var8 < var4)
				{
					var12 = var8;
					var13 = var4;
				}

				if (var8 < this.heightMapMinimum)
				{
					this.heightMapMinimum = var8;
				}

				if (!this.worldObj.provider.hasNoSky)
				{
					this.updateSkylightNeighborHeight(var6 - 1, var7, var12, var13);
					this.updateSkylightNeighborHeight(var6 + 1, var7, var12, var13);
					this.updateSkylightNeighborHeight(var6, var7 - 1, var12, var13);
					this.updateSkylightNeighborHeight(var6, var7 + 1, var12, var13);
					this.updateSkylightNeighborHeight(var6, var7, var12, var13);
				}

				this.isModified = true;
			}
		}

		public virtual int func_150808_b(int p_150808_1_, int p_150808_2_, int p_150808_3_)
		{
			return this.func_150810_a(p_150808_1_, p_150808_2_, p_150808_3_).LightOpacity;
		}

//JAVA TO VB & C# CONVERTER WARNING: 'final' parameters are not allowed in .NET:
//ORIGINAL LINE: public Block func_150810_a(final int p_150810_1_, final int p_150810_2_, final int p_150810_3_)
		public virtual Block func_150810_a(int p_150810_1_, int p_150810_2_, int p_150810_3_)
		{
			Block var4 = Blocks.air;

			if (p_150810_2_ >> 4 < this.storageArrays.Length)
			{
				ExtendedBlockStorage var5 = this.storageArrays[p_150810_2_ >> 4];

				if (var5 != null)
				{
					try
					{
						var4 = var5.func_150819_a(p_150810_1_, p_150810_2_ & 15, p_150810_3_);
					}
					catch (Exception var9)
					{
						CrashReport var7 = CrashReport.makeCrashReport(var9, "Getting block");
						CrashReportCategory var8 = var7.makeCategory("Block being got");
						var8.addCrashSectionCallable("Location", new Callable() {  public string call() { return CrashReportCategory.getLocationInfo(p_150810_1_, p_150810_2_, p_150810_3_); } });
						throw new ReportedException(var7);
					}
				}
			}

			return var4;
		}

///    
///     <summary> * Return the metadata corresponding to the given coordinates inside a chunk. </summary>
///     
		public virtual int getBlockMetadata(int p_76628_1_, int p_76628_2_, int p_76628_3_)
		{
			if (p_76628_2_ >> 4 >= this.storageArrays.Length)
			{
				return 0;
			}
			else
			{
				ExtendedBlockStorage var4 = this.storageArrays[p_76628_2_ >> 4];
				return var4 != null ? var4.getExtBlockMetadata(p_76628_1_, p_76628_2_ & 15, p_76628_3_) : 0;
			}
		}

		public virtual bool func_150807_a(int p_150807_1_, int p_150807_2_, int p_150807_3_, Block p_150807_4_, int p_150807_5_)
		{
			int var6 = p_150807_3_ << 4 | p_150807_1_;

			if (p_150807_2_ >= this.precipitationHeightMap[var6] - 1)
			{
				this.precipitationHeightMap[var6] = -999;
			}

			int var7 = this.heightMap[var6];
			Block var8 = this.func_150810_a(p_150807_1_, p_150807_2_, p_150807_3_);
			int var9 = this.getBlockMetadata(p_150807_1_, p_150807_2_, p_150807_3_);

			if (var8 == p_150807_4_ && var9 == p_150807_5_)
			{
				return false;
			}
			else
			{
				ExtendedBlockStorage var10 = this.storageArrays[p_150807_2_ >> 4];
				bool var11 = false;

				if (var10 == null)
				{
					if (p_150807_4_ == Blocks.air)
					{
						return false;
					}

					var10 = this.storageArrays[p_150807_2_ >> 4] = new ExtendedBlockStorage(p_150807_2_ >> 4 << 4, !this.worldObj.provider.hasNoSky);
					var11 = p_150807_2_ >= var7;
				}

				int var12 = this.xPosition * 16 + p_150807_1_;
				int var13 = this.zPosition * 16 + p_150807_3_;

				if (!this.worldObj.isClient)
				{
					var8.onBlockPreDestroy(this.worldObj, var12, p_150807_2_, var13, var9);
				}

				var10.func_150818_a(p_150807_1_, p_150807_2_ & 15, p_150807_3_, p_150807_4_);

				if (!this.worldObj.isClient)
				{
					var8.breakBlock(this.worldObj, var12, p_150807_2_, var13, var8, var9);
				}
				else if (var8 is ITileEntityProvider && var8 != p_150807_4_)
				{
					this.worldObj.removeTileEntity(var12, p_150807_2_, var13);
				}

				if (var10.func_150819_a(p_150807_1_, p_150807_2_ & 15, p_150807_3_) != p_150807_4_)
				{
					return false;
				}
				else
				{
					var10.setExtBlockMetadata(p_150807_1_, p_150807_2_ & 15, p_150807_3_, p_150807_5_);

					if (var11)
					{
						this.generateSkylightMap();
					}
					else
					{
						int var14 = p_150807_4_.LightOpacity;
						int var15 = var8.LightOpacity;

						if (var14 > 0)
						{
							if (p_150807_2_ >= var7)
							{
								this.relightBlock(p_150807_1_, p_150807_2_ + 1, p_150807_3_);
							}
						}
						else if (p_150807_2_ == var7 - 1)
						{
							this.relightBlock(p_150807_1_, p_150807_2_, p_150807_3_);
						}

						if (var14 != var15 && (var14 < var15 || this.getSavedLightValue(EnumSkyBlock.Sky, p_150807_1_, p_150807_2_, p_150807_3_) > 0 || this.getSavedLightValue(EnumSkyBlock.Block, p_150807_1_, p_150807_2_, p_150807_3_) > 0))
						{
							this.propagateSkylightOcclusion(p_150807_1_, p_150807_3_);
						}
					}

					TileEntity var16;

					if (var8 is ITileEntityProvider)
					{
						var16 = this.func_150806_e(p_150807_1_, p_150807_2_, p_150807_3_);

						if (var16 != null)
						{
							var16.updateContainingBlockInfo();
						}
					}

					if (!this.worldObj.isClient)
					{
						p_150807_4_.onBlockAdded(this.worldObj, var12, p_150807_2_, var13);
					}

					if (p_150807_4_ is ITileEntityProvider)
					{
						var16 = this.func_150806_e(p_150807_1_, p_150807_2_, p_150807_3_);

						if (var16 == null)
						{
							var16 = ((ITileEntityProvider)p_150807_4_).createNewTileEntity(this.worldObj, p_150807_5_);
							this.worldObj.setTileEntity(var12, p_150807_2_, var13, var16);
						}

						if (var16 != null)
						{
							var16.updateContainingBlockInfo();
						}
					}

					this.isModified = true;
					return true;
				}
			}
		}

///    
///     <summary> * Set the metadata of a block in the chunk </summary>
///     
		public virtual bool setBlockMetadata(int p_76589_1_, int p_76589_2_, int p_76589_3_, int p_76589_4_)
		{
			ExtendedBlockStorage var5 = this.storageArrays[p_76589_2_ >> 4];

			if (var5 == null)
			{
				return false;
			}
			else
			{
				int var6 = var5.getExtBlockMetadata(p_76589_1_, p_76589_2_ & 15, p_76589_3_);

				if (var6 == p_76589_4_)
				{
					return false;
				}
				else
				{
					this.isModified = true;
					var5.setExtBlockMetadata(p_76589_1_, p_76589_2_ & 15, p_76589_3_, p_76589_4_);

					if (var5.func_150819_a(p_76589_1_, p_76589_2_ & 15, p_76589_3_) is ITileEntityProvider)
					{
						TileEntity var7 = this.func_150806_e(p_76589_1_, p_76589_2_, p_76589_3_);

						if (var7 != null)
						{
							var7.updateContainingBlockInfo();
							var7.blockMetadata = p_76589_4_;
						}
					}

					return true;
				}
			}
		}

///    
///     <summary> * Gets the amount of light saved in this block (doesn't adjust for daylight) </summary>
///     
		public virtual int getSavedLightValue(EnumSkyBlock p_76614_1_, int p_76614_2_, int p_76614_3_, int p_76614_4_)
		{
			ExtendedBlockStorage var5 = this.storageArrays[p_76614_3_ >> 4];
			return var5 == null ? (this.canBlockSeeTheSky(p_76614_2_, p_76614_3_, p_76614_4_) ? p_76614_1_.defaultLightValue : 0) : (p_76614_1_ == EnumSkyBlock.Sky ? (this.worldObj.provider.hasNoSky ? 0 : var5.getExtSkylightValue(p_76614_2_, p_76614_3_ & 15, p_76614_4_)) : (p_76614_1_ == EnumSkyBlock.Block ? var5.getExtBlocklightValue(p_76614_2_, p_76614_3_ & 15, p_76614_4_) : p_76614_1_.defaultLightValue));
		}

///    
///     <summary> * Sets the light value at the coordinate. If enumskyblock is set to sky it sets it in the skylightmap and if its a
///     * block then into the blocklightmap. Args enumSkyBlock, x, y, z, lightValue </summary>
///     
		public virtual void setLightValue(EnumSkyBlock p_76633_1_, int p_76633_2_, int p_76633_3_, int p_76633_4_, int p_76633_5_)
		{
			ExtendedBlockStorage var6 = this.storageArrays[p_76633_3_ >> 4];

			if (var6 == null)
			{
				var6 = this.storageArrays[p_76633_3_ >> 4] = new ExtendedBlockStorage(p_76633_3_ >> 4 << 4, !this.worldObj.provider.hasNoSky);
				this.generateSkylightMap();
			}

			this.isModified = true;

			if (p_76633_1_ == EnumSkyBlock.Sky)
			{
				if (!this.worldObj.provider.hasNoSky)
				{
					var6.setExtSkylightValue(p_76633_2_, p_76633_3_ & 15, p_76633_4_, p_76633_5_);
				}
			}
			else if (p_76633_1_ == EnumSkyBlock.Block)
			{
				var6.setExtBlocklightValue(p_76633_2_, p_76633_3_ & 15, p_76633_4_, p_76633_5_);
			}
		}

///    
///     <summary> * Gets the amount of light on a block taking into account sunlight </summary>
///     
		public virtual int getBlockLightValue(int p_76629_1_, int p_76629_2_, int p_76629_3_, int p_76629_4_)
		{
			ExtendedBlockStorage var5 = this.storageArrays[p_76629_2_ >> 4];

			if (var5 == null)
			{
				return !this.worldObj.provider.hasNoSky && p_76629_4_ < EnumSkyBlock.Sky.defaultLightValue ? EnumSkyBlock.Sky.defaultLightValue - p_76629_4_ : 0;
			}
			else
			{
				int var6 = this.worldObj.provider.hasNoSky ? 0 : var5.getExtSkylightValue(p_76629_1_, p_76629_2_ & 15, p_76629_3_);

				if (var6 > 0)
				{
					isLit = true;
				}

				var6 -= p_76629_4_;
				int var7 = var5.getExtBlocklightValue(p_76629_1_, p_76629_2_ & 15, p_76629_3_);

				if (var7 > var6)
				{
					var6 = var7;
				}

				return var6;
			}
		}

///    
///     <summary> * Adds an entity to the chunk. Args: entity </summary>
///     
		public virtual void addEntity(Entity p_76612_1_)
		{
			this.hasEntities = true;
			int var2 = MathHelper.floor_double(p_76612_1_.posX / 16.0D);
			int var3 = MathHelper.floor_double(p_76612_1_.posZ / 16.0D);

			if (var2 != this.xPosition || var3 != this.zPosition)
			{
				logger.warn("Wrong location! " + p_76612_1_ + " (at " + var2 + ", " + var3 + " instead of " + this.xPosition + ", " + this.zPosition + ")");
				Thread.dumpStack();
			}

			int var4 = MathHelper.floor_double(p_76612_1_.posY / 16.0D);

			if (var4 < 0)
			{
				var4 = 0;
			}

			if (var4 >= this.entityLists.Length)
			{
				var4 = this.entityLists.Length - 1;
			}

			p_76612_1_.addedToChunk = true;
			p_76612_1_.chunkCoordX = this.xPosition;
			p_76612_1_.chunkCoordY = var4;
			p_76612_1_.chunkCoordZ = this.zPosition;
			this.entityLists[var4].Add(p_76612_1_);
		}

///    
///     <summary> * removes entity using its y chunk coordinate as its index </summary>
///     
		public virtual void removeEntity(Entity p_76622_1_)
		{
			this.removeEntityAtIndex(p_76622_1_, p_76622_1_.chunkCoordY);
		}

///    
///     <summary> * Removes entity at the specified index from the entity array. </summary>
///     
		public virtual void removeEntityAtIndex(Entity p_76608_1_, int p_76608_2_)
		{
			if (p_76608_2_ < 0)
			{
				p_76608_2_ = 0;
			}

			if (p_76608_2_ >= this.entityLists.Length)
			{
				p_76608_2_ = this.entityLists.Length - 1;
			}

			this.entityLists[p_76608_2_].Remove(p_76608_1_);
		}

///    
///     <summary> * Returns whether is not a block above this one blocking sight to the sky (done via checking against the heightmap) </summary>
///     
		public virtual bool canBlockSeeTheSky(int p_76619_1_, int p_76619_2_, int p_76619_3_)
		{
			return p_76619_2_ >= this.heightMap[p_76619_3_ << 4 | p_76619_1_];
		}

		public virtual TileEntity func_150806_e(int p_150806_1_, int p_150806_2_, int p_150806_3_)
		{
			ChunkPosition var4 = new ChunkPosition(p_150806_1_, p_150806_2_, p_150806_3_);
			TileEntity var5 = (TileEntity)this.chunkTileEntityMap.get(var4);

			if (var5 == null)
			{
				Block var6 = this.func_150810_a(p_150806_1_, p_150806_2_, p_150806_3_);

				if (!var6.hasTileEntity())
				{
					return null;
				}

				var5 = ((ITileEntityProvider)var6).createNewTileEntity(this.worldObj, this.getBlockMetadata(p_150806_1_, p_150806_2_, p_150806_3_));
				this.worldObj.setTileEntity(this.xPosition * 16 + p_150806_1_, p_150806_2_, this.zPosition * 16 + p_150806_3_, var5);
			}

			if (var5 != null && var5.Invalid)
			{
				this.chunkTileEntityMap.Remove(var4);
				return null;
			}
			else
			{
				return var5;
			}
		}

		public virtual void addTileEntity(TileEntity p_150813_1_)
		{
			int var2 = p_150813_1_.field_145851_c - this.xPosition * 16;
			int var3 = p_150813_1_.field_145848_d;
			int var4 = p_150813_1_.field_145849_e - this.zPosition * 16;
			this.func_150812_a(var2, var3, var4, p_150813_1_);

			if (this.isChunkLoaded)
			{
				this.worldObj.field_147482_g.Add(p_150813_1_);
			}
		}

		public virtual void func_150812_a(int p_150812_1_, int p_150812_2_, int p_150812_3_, TileEntity p_150812_4_)
		{
			ChunkPosition var5 = new ChunkPosition(p_150812_1_, p_150812_2_, p_150812_3_);
			p_150812_4_.WorldObj = this.worldObj;
			p_150812_4_.field_145851_c = this.xPosition * 16 + p_150812_1_;
			p_150812_4_.field_145848_d = p_150812_2_;
			p_150812_4_.field_145849_e = this.zPosition * 16 + p_150812_3_;

			if (this.func_150810_a(p_150812_1_, p_150812_2_, p_150812_3_) is ITileEntityProvider)
			{
				if (this.chunkTileEntityMap.ContainsKey(var5))
				{
					((TileEntity)this.chunkTileEntityMap.get(var5)).invalidate();
				}

				p_150812_4_.validate();
				this.chunkTileEntityMap.Add(var5, p_150812_4_);
			}
		}

		public virtual void removeTileEntity(int p_150805_1_, int p_150805_2_, int p_150805_3_)
		{
			ChunkPosition var4 = new ChunkPosition(p_150805_1_, p_150805_2_, p_150805_3_);

			if (this.isChunkLoaded)
			{
				TileEntity var5 = (TileEntity)this.chunkTileEntityMap.remove(var4);

				if (var5 != null)
				{
					var5.invalidate();
				}
			}
		}

///    
///     <summary> * Called when this Chunk is loaded by the ChunkProvider </summary>
///     
		public virtual void onChunkLoad()
		{
			this.isChunkLoaded = true;
			this.worldObj.func_147448_a(this.chunkTileEntityMap.Values);

			for (int var1 = 0; var1 < this.entityLists.Length; ++var1)
			{
				IEnumerator var2 = this.entityLists[var1].GetEnumerator();

				while (var2.MoveNext())
				{
					Entity var3 = (Entity)var2.Current;
					var3.onChunkLoad();
				}

				this.worldObj.addLoadedEntities(this.entityLists[var1]);
			}
		}

///    
///     <summary> * Called when this Chunk is unloaded by the ChunkProvider </summary>
///     
		public virtual void onChunkUnload()
		{
			this.isChunkLoaded = false;
			IEnumerator var1 = this.chunkTileEntityMap.Values.GetEnumerator();

			while (var1.MoveNext())
			{
				TileEntity var2 = (TileEntity)var1.Current;
				this.worldObj.func_147457_a(var2);
			}

			for (int var3 = 0; var3 < this.entityLists.Length; ++var3)
			{
				this.worldObj.unloadEntities(this.entityLists[var3]);
			}
		}

///    
///     <summary> * Sets the isModified flag for this Chunk </summary>
///     
		public virtual void setChunkModified()
		{
			this.isModified = true;
		}

///    
///     <summary> * Fills the given list of all entities that intersect within the given bounding box that aren't the passed entity
///     * Args: entity, aabb, listToFill </summary>
///     
		public virtual void getEntitiesWithinAABBForEntity(Entity p_76588_1_, AxisAlignedBB p_76588_2_, IList p_76588_3_, IEntitySelector p_76588_4_)
		{
			int var5 = MathHelper.floor_double((p_76588_2_.minY - 2.0D) / 16.0D);
			int var6 = MathHelper.floor_double((p_76588_2_.maxY + 2.0D) / 16.0D);
			var5 = MathHelper.clamp_int(var5, 0, this.entityLists.Length - 1);
			var6 = MathHelper.clamp_int(var6, 0, this.entityLists.Length - 1);

			for (int var7 = var5; var7 <= var6; ++var7)
			{
				IList var8 = this.entityLists[var7];

				for (int var9 = 0; var9 < var8.Count; ++var9)
				{
					Entity var10 = (Entity)var8[var9];

					if (var10 != p_76588_1_ && var10.boundingBox.intersectsWith(p_76588_2_) && (p_76588_4_ == null || p_76588_4_.isEntityApplicable(var10)))
					{
						p_76588_3_.Add(var10);
						Entity[] var11 = var10.Parts;

						if (var11 != null)
						{
							for (int var12 = 0; var12 < var11.Length; ++var12)
							{
								var10 = var11[var12];

								if (var10 != p_76588_1_ && var10.boundingBox.intersectsWith(p_76588_2_) && (p_76588_4_ == null || p_76588_4_.isEntityApplicable(var10)))
								{
									p_76588_3_.Add(var10);
								}
							}
						}
					}
				}
			}
		}

///    
///     <summary> * Gets all entities that can be assigned to the specified class. Args: entityClass, aabb, listToFill </summary>
///     
		public virtual void getEntitiesOfTypeWithinAAAB(Type p_76618_1_, AxisAlignedBB p_76618_2_, IList p_76618_3_, IEntitySelector p_76618_4_)
		{
			int var5 = MathHelper.floor_double((p_76618_2_.minY - 2.0D) / 16.0D);
			int var6 = MathHelper.floor_double((p_76618_2_.maxY + 2.0D) / 16.0D);
			var5 = MathHelper.clamp_int(var5, 0, this.entityLists.Length - 1);
			var6 = MathHelper.clamp_int(var6, 0, this.entityLists.Length - 1);

			for (int var7 = var5; var7 <= var6; ++var7)
			{
				IList var8 = this.entityLists[var7];

				for (int var9 = 0; var9 < var8.Count; ++var9)
				{
					Entity var10 = (Entity)var8[var9];

					if (p_76618_1_.isAssignableFrom(var10.GetType()) && var10.boundingBox.intersectsWith(p_76618_2_) && (p_76618_4_ == null || p_76618_4_.isEntityApplicable(var10)))
					{
						p_76618_3_.Add(var10);
					}
				}
			}
		}

///    
///     <summary> * Returns true if this Chunk needs to be saved </summary>
///     
		public virtual bool needsSaving(bool p_76601_1_)
		{
			if (p_76601_1_)
			{
				if (this.hasEntities && this.worldObj.TotalWorldTime != this.lastSaveTime || this.isModified)
				{
					return true;
				}
			}
			else if (this.hasEntities && this.worldObj.TotalWorldTime >= this.lastSaveTime + 600L)
			{
				return true;
			}

			return this.isModified;
		}

		public virtual Random getRandomWithSeed(long p_76617_1_)
		{
			return new Random(this.worldObj.Seed + (long)(this.xPosition * this.xPosition * 4987142) + (long)(this.xPosition * 5947611) + (long)(this.zPosition * this.zPosition) * 4392871L + (long)(this.zPosition * 389711) ^ p_76617_1_);
		}

		public virtual bool isEmpty()
		{
			get
			{
				return false;
			}
		}

		public virtual void populateChunk(IChunkProvider p_76624_1_, IChunkProvider p_76624_2_, int p_76624_3_, int p_76624_4_)
		{
			if (!this.isTerrainPopulated && p_76624_1_.chunkExists(p_76624_3_ + 1, p_76624_4_ + 1) && p_76624_1_.chunkExists(p_76624_3_, p_76624_4_ + 1) && p_76624_1_.chunkExists(p_76624_3_ + 1, p_76624_4_))
			{
				p_76624_1_.populate(p_76624_2_, p_76624_3_, p_76624_4_);
			}

			if (p_76624_1_.chunkExists(p_76624_3_ - 1, p_76624_4_) && !p_76624_1_.provideChunk(p_76624_3_ - 1, p_76624_4_).isTerrainPopulated && p_76624_1_.chunkExists(p_76624_3_ - 1, p_76624_4_ + 1) && p_76624_1_.chunkExists(p_76624_3_, p_76624_4_ + 1) && p_76624_1_.chunkExists(p_76624_3_ - 1, p_76624_4_ + 1))
			{
				p_76624_1_.populate(p_76624_2_, p_76624_3_ - 1, p_76624_4_);
			}

			if (p_76624_1_.chunkExists(p_76624_3_, p_76624_4_ - 1) && !p_76624_1_.provideChunk(p_76624_3_, p_76624_4_ - 1).isTerrainPopulated && p_76624_1_.chunkExists(p_76624_3_ + 1, p_76624_4_ - 1) && p_76624_1_.chunkExists(p_76624_3_ + 1, p_76624_4_ - 1) && p_76624_1_.chunkExists(p_76624_3_ + 1, p_76624_4_))
			{
				p_76624_1_.populate(p_76624_2_, p_76624_3_, p_76624_4_ - 1);
			}

			if (p_76624_1_.chunkExists(p_76624_3_ - 1, p_76624_4_ - 1) && !p_76624_1_.provideChunk(p_76624_3_ - 1, p_76624_4_ - 1).isTerrainPopulated && p_76624_1_.chunkExists(p_76624_3_, p_76624_4_ - 1) && p_76624_1_.chunkExists(p_76624_3_ - 1, p_76624_4_))
			{
				p_76624_1_.populate(p_76624_2_, p_76624_3_ - 1, p_76624_4_ - 1);
			}
		}

///    
///     <summary> * Gets the height to which rain/snow will fall. Calculates it if not already stored. </summary>
///     
		public virtual int getPrecipitationHeight(int p_76626_1_, int p_76626_2_)
		{
			int var3 = p_76626_1_ | p_76626_2_ << 4;
			int var4 = this.precipitationHeightMap[var3];

			if (var4 == -999)
			{
				int var5 = this.TopFilledSegment + 15;
				var4 = -1;

				while (var5 > 0 && var4 == -1)
				{
					Block var6 = this.func_150810_a(p_76626_1_, var5, p_76626_2_);
					Material var7 = var6.Material;

					if (!var7.blocksMovement() && !var7.Liquid)
					{
						--var5;
					}
					else
					{
						var4 = var5 + 1;
					}
				}

				this.precipitationHeightMap[var3] = var4;
			}

			return var4;
		}

		public virtual void func_150804_b(bool p_150804_1_)
		{
			if (this.isGapLightingUpdated && !this.worldObj.provider.hasNoSky && !p_150804_1_)
			{
				this.recheckGaps(this.worldObj.isClient);
			}

			this.field_150815_m = true;

			if (!this.isLightPopulated && this.isTerrainPopulated)
			{
				this.func_150809_p();
			}
		}

		public virtual bool func_150802_k()
		{
			return this.field_150815_m && this.isTerrainPopulated && this.isLightPopulated;
		}

///    
///     <summary> * Gets a ChunkCoordIntPair representing the Chunk's position. </summary>
///     
		public virtual ChunkCoordIntPair ChunkCoordIntPair
		{
			get
			{
				return new ChunkCoordIntPair(this.xPosition, this.zPosition);
			}
		}

///    
///     <summary> * Returns whether the ExtendedBlockStorages containing levels (in blocks) from arg 1 to arg 2 are fully empty
///     * (true) or not (false). </summary>
///     
		public virtual bool getAreLevelsEmpty(int p_76606_1_, int p_76606_2_)
		{
			if (p_76606_1_ < 0)
			{
				p_76606_1_ = 0;
			}

			if (p_76606_2_ >= 256)
			{
				p_76606_2_ = 255;
			}

			for (int var3 = p_76606_1_; var3 <= p_76606_2_; var3 += 16)
			{
				ExtendedBlockStorage var4 = this.storageArrays[var3 >> 4];

				if (var4 != null && !var4.Empty)
				{
					return false;
				}
			}

			return true;
		}

		public virtual ExtendedBlockStorage[] StorageArrays
		{
			set
			{
				this.storageArrays = value;
			}
		}

///    
///     <summary> * Initialise this chunk with new binary data </summary>
///     
		public virtual void fillChunk(sbyte[] p_76607_1_, int p_76607_2_, int p_76607_3_, bool p_76607_4_)
		{
			int var5 = 0;
			bool var6 = !this.worldObj.provider.hasNoSky;
			int var7;

			for (var7 = 0; var7 < this.storageArrays.Length; ++var7)
			{
				if ((p_76607_2_ & 1 << var7) != 0)
				{
					if (this.storageArrays[var7] == null)
					{
						this.storageArrays[var7] = new ExtendedBlockStorage(var7 << 4, var6);
					}

					sbyte[] var8 = this.storageArrays[var7].BlockLSBArray;
					Array.Copy(p_76607_1_, var5, var8, 0, var8.Length);
					var5 += var8.Length;
				}
				else if (p_76607_4_ && this.storageArrays[var7] != null)
				{
					this.storageArrays[var7] = null;
				}
			}

			NibbleArray var10;

			for (var7 = 0; var7 < this.storageArrays.Length; ++var7)
			{
				if ((p_76607_2_ & 1 << var7) != 0 && this.storageArrays[var7] != null)
				{
					var10 = this.storageArrays[var7].MetadataArray;
					Array.Copy(p_76607_1_, var5, var10.data, 0, var10.data.Length);
					var5 += var10.data.Length;
				}
			}

			for (var7 = 0; var7 < this.storageArrays.Length; ++var7)
			{
				if ((p_76607_2_ & 1 << var7) != 0 && this.storageArrays[var7] != null)
				{
					var10 = this.storageArrays[var7].BlocklightArray;
					Array.Copy(p_76607_1_, var5, var10.data, 0, var10.data.Length);
					var5 += var10.data.Length;
				}
			}

			if (var6)
			{
				for (var7 = 0; var7 < this.storageArrays.Length; ++var7)
				{
					if ((p_76607_2_ & 1 << var7) != 0 && this.storageArrays[var7] != null)
					{
						var10 = this.storageArrays[var7].SkylightArray;
						Array.Copy(p_76607_1_, var5, var10.data, 0, var10.data.Length);
						var5 += var10.data.Length;
					}
				}
			}

			for (var7 = 0; var7 < this.storageArrays.Length; ++var7)
			{
				if ((p_76607_3_ & 1 << var7) != 0)
				{
					if (this.storageArrays[var7] == null)
					{
						var5 += 2048;
					}
					else
					{
						var10 = this.storageArrays[var7].BlockMSBArray;

						if (var10 == null)
						{
							var10 = this.storageArrays[var7].createBlockMSBArray();
						}

						Array.Copy(p_76607_1_, var5, var10.data, 0, var10.data.Length);
						var5 += var10.data.Length;
					}
				}
				else if (p_76607_4_ && this.storageArrays[var7] != null && this.storageArrays[var7].BlockMSBArray != null)
				{
					this.storageArrays[var7].clearMSBArray();
				}
			}

			if (p_76607_4_)
			{
				Array.Copy(p_76607_1_, var5, this.blockBiomeArray, 0, this.blockBiomeArray.Length);
				int var10000 = var5 + this.blockBiomeArray.Length;
			}

			for (var7 = 0; var7 < this.storageArrays.Length; ++var7)
			{
				if (this.storageArrays[var7] != null && (p_76607_2_ & 1 << var7) != 0)
				{
					this.storageArrays[var7].removeInvalidBlocks();
				}
			}

			this.isLightPopulated = true;
			this.isTerrainPopulated = true;
			this.generateHeightMap();
			IEnumerator var9 = this.chunkTileEntityMap.Values.GetEnumerator();

			while (var9.MoveNext())
			{
				TileEntity var11 = (TileEntity)var9.Current;
				var11.updateContainingBlockInfo();
			}
		}

///    
///     <summary> * This method retrieves the biome at a set of coordinates </summary>
///     
		public virtual BiomeGenBase getBiomeGenForWorldCoords(int p_76591_1_, int p_76591_2_, WorldChunkManager p_76591_3_)
		{
			int var4 = this.blockBiomeArray[p_76591_2_ << 4 | p_76591_1_] & 255;

			if (var4 == 255)
			{
				BiomeGenBase var5 = p_76591_3_.getBiomeGenAt((this.xPosition << 4) + p_76591_1_, (this.zPosition << 4) + p_76591_2_);
				var4 = var5.biomeID;
				this.blockBiomeArray[p_76591_2_ << 4 | p_76591_1_] = (sbyte)(var4 & 255);
			}

			return BiomeGenBase.func_150568_d(var4) == null ? BiomeGenBase.plains : BiomeGenBase.func_150568_d(var4);
		}

///    
///     <summary> * Returns an array containing a 16x16 mapping on the X/Z of block positions in this Chunk to biome IDs. </summary>
///     
		public virtual sbyte[] BiomeArray
		{
			get
			{
				return this.blockBiomeArray;
			}
			set
			{
				this.blockBiomeArray = value;
			}
		}

///    
///     <summary> * Accepts a 256-entry array that contains a 16x16 mapping on the X/Z plane of block positions in this Chunk to
///     * biome IDs. </summary>
///     

///    
///     <summary> * Resets the relight check index to 0 for this Chunk. </summary>
///     
		public virtual void resetRelightChecks()
		{
			this.queuedLightChecks = 0;
		}

///    
///     <summary> * Called once-per-chunk-per-tick, and advances the round-robin relight check index per-storage-block by up to 8
///     * blocks at a time. In a worst-case scenario, can potentially take up to 1.6 seconds, calculated via
///     * (4096/(8*16))/20, to re-check all blocks in a chunk, which could explain both lagging light updates in certain
///     * cases as well as Nether relight </summary>
///     
		public virtual void enqueueRelightChecks()
		{
			for (int var1 = 0; var1 < 8; ++var1)
			{
				if (this.queuedLightChecks >= 4096)
				{
					return;
				}

				int var2 = this.queuedLightChecks % 16;
				int var3 = this.queuedLightChecks / 16 % 16;
				int var4 = this.queuedLightChecks / 256;
				++this.queuedLightChecks;
				int var5 = (this.xPosition << 4) + var3;
				int var6 = (this.zPosition << 4) + var4;

				for (int var7 = 0; var7 < 16; ++var7)
				{
					int var8 = (var2 << 4) + var7;

					if (this.storageArrays[var2] == null && (var7 == 0 || var7 == 15 || var3 == 0 || var3 == 15 || var4 == 0 || var4 == 15) || this.storageArrays[var2] != null && this.storageArrays[var2].func_150819_a(var3, var7, var4).Material == Material.air)
					{
						if (this.worldObj.getBlock(var5, var8 - 1, var6).LightValue > 0)
						{
							this.worldObj.func_147451_t(var5, var8 - 1, var6);
						}

						if (this.worldObj.getBlock(var5, var8 + 1, var6).LightValue > 0)
						{
							this.worldObj.func_147451_t(var5, var8 + 1, var6);
						}

						if (this.worldObj.getBlock(var5 - 1, var8, var6).LightValue > 0)
						{
							this.worldObj.func_147451_t(var5 - 1, var8, var6);
						}

						if (this.worldObj.getBlock(var5 + 1, var8, var6).LightValue > 0)
						{
							this.worldObj.func_147451_t(var5 + 1, var8, var6);
						}

						if (this.worldObj.getBlock(var5, var8, var6 - 1).LightValue > 0)
						{
							this.worldObj.func_147451_t(var5, var8, var6 - 1);
						}

						if (this.worldObj.getBlock(var5, var8, var6 + 1).LightValue > 0)
						{
							this.worldObj.func_147451_t(var5, var8, var6 + 1);
						}

						this.worldObj.func_147451_t(var5, var8, var6);
					}
				}
			}
		}

		public virtual void func_150809_p()
		{
			this.isTerrainPopulated = true;
			this.isLightPopulated = true;

			if (!this.worldObj.provider.hasNoSky)
			{
				if (this.worldObj.checkChunksExist(this.xPosition * 16 - 1, 0, this.zPosition * 16 - 1, this.xPosition * 16 + 1, 63, this.zPosition * 16 + 1))
				{
					for (int var1 = 0; var1 < 16; ++var1)
					{
						for (int var2 = 0; var2 < 16; ++var2)
						{
							if (!this.func_150811_f(var1, var2))
							{
								this.isLightPopulated = false;
								break;
							}
						}
					}

					if (this.isLightPopulated)
					{
						Chunk var3 = this.worldObj.getChunkFromBlockCoords(this.xPosition * 16 - 1, this.zPosition * 16);
						var3.func_150801_a(3);
						var3 = this.worldObj.getChunkFromBlockCoords(this.xPosition * 16 + 16, this.zPosition * 16);
						var3.func_150801_a(1);
						var3 = this.worldObj.getChunkFromBlockCoords(this.xPosition * 16, this.zPosition * 16 - 1);
						var3.func_150801_a(0);
						var3 = this.worldObj.getChunkFromBlockCoords(this.xPosition * 16, this.zPosition * 16 + 16);
						var3.func_150801_a(2);
					}
				}
				else
				{
					this.isLightPopulated = false;
				}
			}
		}

		private void func_150801_a(int p_150801_1_)
		{
			if (this.isTerrainPopulated)
			{
				int var2;

				if (p_150801_1_ == 3)
				{
					for (var2 = 0; var2 < 16; ++var2)
					{
						this.func_150811_f(15, var2);
					}
				}
				else if (p_150801_1_ == 1)
				{
					for (var2 = 0; var2 < 16; ++var2)
					{
						this.func_150811_f(0, var2);
					}
				}
				else if (p_150801_1_ == 0)
				{
					for (var2 = 0; var2 < 16; ++var2)
					{
						this.func_150811_f(var2, 15);
					}
				}
				else if (p_150801_1_ == 2)
				{
					for (var2 = 0; var2 < 16; ++var2)
					{
						this.func_150811_f(var2, 0);
					}
				}
			}
		}

		private bool func_150811_f(int p_150811_1_, int p_150811_2_)
		{
			int var3 = this.TopFilledSegment;
			bool var4 = false;
			bool var5 = false;
			int var6;

			for (var6 = var3 + 16 - 1; var6 > 63 || var6 > 0 && !var5; --var6)
			{
				int var7 = this.func_150808_b(p_150811_1_, var6, p_150811_2_);

				if (var7 == 255 && var6 < 63)
				{
					var5 = true;
				}

				if (!var4 && var7 > 0)
				{
					var4 = true;
				}
				else if (var4 && var7 == 0 && !this.worldObj.func_147451_t(this.xPosition * 16 + p_150811_1_, var6, this.zPosition * 16 + p_150811_2_))
				{
					return false;
				}
			}

			for (; var6 > 0; --var6)
			{
				if (this.func_150810_a(p_150811_1_, var6, p_150811_2_).LightValue > 0)
				{
					this.worldObj.func_147451_t(this.xPosition * 16 + p_150811_1_, var6, this.zPosition * 16 + p_150811_2_);
				}
			}

			return true;
		}
	}

}