using System;
using System.Collections;

namespace DotCraftCore.World
{

	using Block = DotCraftCore.block.Block;
	using BlockHopper = DotCraftCore.block.BlockHopper;
	using BlockLiquid = DotCraftCore.block.BlockLiquid;
	using BlockSlab = DotCraftCore.block.BlockSlab;
	using BlockSnow = DotCraftCore.block.BlockSnow;
	using BlockStairs = DotCraftCore.block.BlockStairs;
	using Material = DotCraftCore.block.material.Material;
	using IEntitySelector = DotCraftCore.command.IEntitySelector;
	using CrashReport = DotCraftCore.crash.CrashReport;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using Entity = DotCraftCore.entity.Entity;
	using EntityLiving = DotCraftCore.entity.EntityLiving;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using PathEntity = DotCraftCore.pathfinding.PathEntity;
	using PathFinder = DotCraftCore.pathfinding.PathFinder;
	using Profiler = DotCraftCore.profiler.Profiler;
	using Scoreboard = DotCraftCore.Scoreboard.Scoreboard;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using TileEntity = DotCraftCore.TileEntity.TileEntity;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using ChunkCoordinates = DotCraftCore.Util.ChunkCoordinates;
	using Direction = DotCraftCore.Util.Direction;
	using Facing = DotCraftCore.Util.Facing;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using MovingObjectPosition = DotCraftCore.Util.MovingObjectPosition;
	using ReportedException = DotCraftCore.Util.ReportedException;
	using Vec3 = DotCraftCore.Util.Vec3;
	using VillageCollection = DotCraftCore.Village.VillageCollection;
	using VillageSiege = DotCraftCore.Village.VillageSiege;
	using BiomeGenBase = DotCraftCore.World.Biome.BiomeGenBase;
	using WorldChunkManager = DotCraftCore.World.Biome.WorldChunkManager;
	using Chunk = DotCraftCore.World.Chunk.Chunk;
	using IChunkProvider = DotCraftCore.World.Chunk.IChunkProvider;
	using ISaveHandler = DotCraftCore.World.Storage.ISaveHandler;
	using MapStorage = DotCraftCore.World.Storage.MapStorage;
	using WorldInfo = DotCraftCore.World.Storage.WorldInfo;

	public abstract class World : IBlockAccess
	{
///    
///     <summary> * boolean; if true updates scheduled by scheduleBlockUpdate happen immediately </summary>
///     
		public bool scheduledUpdatesAreImmediate;

	/// <summary> A list of all Entities in all currently-loaded chunks  </summary>
		public IList loadedEntityList = new ArrayList();
		protected internal IList unloadedEntityList = new ArrayList();
		public IList field_147482_g = new ArrayList();
		private IList field_147484_a = new ArrayList();
		private IList field_147483_b = new ArrayList();

	/// <summary> Array list of players in the world.  </summary>
		public IList playerEntities = new ArrayList();

	/// <summary> a list of all the lightning entities  </summary>
		public IList weatherEffects = new ArrayList();
		private long cloudColour = 16777215L;

	/// <summary> How much light is subtracted from full daylight  </summary>
		public int skylightSubtracted;

///    
///     <summary> * Contains the current Linear Congruential Generator seed for block updates. Used with an A value of 3 and a C
///     * value of 0x3c6ef35f, producing a highly planar series of values ill-suited for choosing random blocks in a
///     * 16x128x16 field. </summary>
///     
		protected internal int updateLCG = (new Random()).nextInt();

///    
///     <summary> * magic number used to generate fast random numbers for 3d distribution within a chunk </summary>
///     
		protected internal readonly int DIST_HASH_MAGIC = 1013904223;
		protected internal float prevRainingStrength;
		protected internal float rainingStrength;
		protected internal float prevThunderingStrength;
		protected internal float thunderingStrength;

///    
///     <summary> * Set to 2 whenever a lightning bolt is generated in SSP. Decrements if > 0 in updateWeather(). Value appears to be
///     * unused. </summary>
///     
		public int lastLightningBolt;

	/// <summary> Option > Difficulty setting (0 - 3)  </summary>
		public EnumDifficulty difficultySetting;

	/// <summary> RNG for World.  </summary>
		public Random rand = new Random();

	/// <summary> The WorldProvider instance that World uses.  </summary>
		public readonly WorldProvider provider;
		protected internal IList worldAccesses = new ArrayList();

	/// <summary> Handles chunk operations and caching  </summary>
		protected internal IChunkProvider chunkProvider;
		protected internal readonly ISaveHandler saveHandler;

///    
///     <summary> * holds information about a world (size on disk, time, spawn point, seed, ...) </summary>
///     
		protected internal WorldInfo worldInfo;

	/// <summary> Boolean that is set to true when trying to find a spawn point  </summary>
		public bool findingSpawnPoint;
		public MapStorage mapStorage;
		public readonly VillageCollection villageCollectionObj;
		protected internal readonly VillageSiege villageSiegeObj = new VillageSiege(this);
		public readonly Profiler theProfiler;
		private readonly Calendar theCalendar = Calendar.Instance;
		protected internal Scoreboard worldScoreboard = new Scoreboard();

	/// <summary> This is set to true for client worlds, and false for server worlds.  </summary>
		public bool isClient;

	/// <summary> Positions to update  </summary>
		protected internal Set activeChunkSet = new HashSet();

	/// <summary> number of ticks until the next random ambients play  </summary>
		private int ambientTickCountdown;

	/// <summary> indicates if enemies are spawned or not  </summary>
		protected internal bool spawnHostileMobs;

	/// <summary> A flag indicating whether we should spawn peaceful mobs.  </summary>
		protected internal bool spawnPeacefulMobs;
		private ArrayList collidingBoundingBoxes;
		private bool field_147481_N;

///    
///     <summary> * is a temporary list of blocks and light values used when updating light levels. Holds up to 32x32x32 blocks (the
///     * maximum influence of a light source.) Every element is a packed bit value: 0000000000LLLLzzzzzzyyyyyyxxxxxx. The
///     * 4-bit L is a light level used when darkening blocks. 6-bit numbers x, y and z represent the block's offset from
///     * the original block, plus 32 (i.e. value of 31 would mean a -1 offset </summary>
///     
		internal int[] lightUpdateBlockList;
		private const string __OBFID = "CL_00000140";

///    
///     <summary> * Gets the biome for a given set of x/z coordinates </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: 'final' parameters are not allowed in .NET:
//ORIGINAL LINE: public BiomeGenBase getBiomeGenForCoords(final int p_72807_1_, final int p_72807_2_)
		public virtual BiomeGenBase getBiomeGenForCoords(int p_72807_1_, int p_72807_2_)
		{
			if (this.blockExists(p_72807_1_, 0, p_72807_2_))
			{
				Chunk var3 = this.getChunkFromBlockCoords(p_72807_1_, p_72807_2_);

				try
				{
					return var3.getBiomeGenForWorldCoords(p_72807_1_ & 15, p_72807_2_ & 15, this.provider.worldChunkMgr);
				}
				catch (Exception var7)
				{
					CrashReport var5 = CrashReport.makeCrashReport(var7, "Getting biome");
					CrashReportCategory var6 = var5.makeCategory("Coordinates of biome request");
					var6.addCrashSectionCallable("Location", new Callable() { private static final string __OBFID = "CL_00000141"; public string call() { return CrashReportCategory.getLocationInfo(p_72807_1_, 0, p_72807_2_); } });
					throw new ReportedException(var5);
				}
			}
			else
			{
				return this.provider.worldChunkMgr.getBiomeGenAt(p_72807_1_, p_72807_2_);
			}
		}

		public virtual WorldChunkManager WorldChunkManager
		{
			get
			{
				return this.provider.worldChunkMgr;
			}
		}

		public World(ISaveHandler p_i45368_1_, string p_i45368_2_, WorldProvider p_i45368_3_, WorldSettings p_i45368_4_, Profiler p_i45368_5_)
		{
			this.ambientTickCountdown = this.rand.Next(12000);
			this.spawnHostileMobs = true;
			this.spawnPeacefulMobs = true;
			this.collidingBoundingBoxes = new ArrayList();
			this.lightUpdateBlockList = new int[32768];
			this.saveHandler = p_i45368_1_;
			this.theProfiler = p_i45368_5_;
			this.worldInfo = new WorldInfo(p_i45368_4_, p_i45368_2_);
			this.provider = p_i45368_3_;
			this.mapStorage = new MapStorage(p_i45368_1_);
			VillageCollection var6 = (VillageCollection)this.mapStorage.loadData(typeof(VillageCollection), "villages");

			if (var6 == null)
			{
				this.villageCollectionObj = new VillageCollection(this);
				this.mapStorage.setData("villages", this.villageCollectionObj);
			}
			else
			{
				this.villageCollectionObj = var6;
				this.villageCollectionObj.func_82566_a(this);
			}

			p_i45368_3_.registerWorld(this);
			this.chunkProvider = this.createChunkProvider();
			this.calculateInitialSkylight();
			this.calculateInitialWeather();
		}

		public World(ISaveHandler p_i45369_1_, string p_i45369_2_, WorldSettings p_i45369_3_, WorldProvider p_i45369_4_, Profiler p_i45369_5_)
		{
			this.ambientTickCountdown = this.rand.Next(12000);
			this.spawnHostileMobs = true;
			this.spawnPeacefulMobs = true;
			this.collidingBoundingBoxes = new ArrayList();
			this.lightUpdateBlockList = new int[32768];
			this.saveHandler = p_i45369_1_;
			this.theProfiler = p_i45369_5_;
			this.mapStorage = new MapStorage(p_i45369_1_);
			this.worldInfo = p_i45369_1_.loadWorldInfo();

			if (p_i45369_4_ != null)
			{
				this.provider = p_i45369_4_;
			}
			else if (this.worldInfo != null && this.worldInfo.VanillaDimension != 0)
			{
				this.provider = WorldProvider.getProviderForDimension(this.worldInfo.VanillaDimension);
			}
			else
			{
				this.provider = WorldProvider.getProviderForDimension(0);
			}

			if (this.worldInfo == null)
			{
				this.worldInfo = new WorldInfo(p_i45369_3_, p_i45369_2_);
			}
			else
			{
				this.worldInfo.WorldName = p_i45369_2_;
			}

			this.provider.registerWorld(this);
			this.chunkProvider = this.createChunkProvider();

			if (!this.worldInfo.Initialized)
			{
				try
				{
					this.initialize(p_i45369_3_);
				}
				catch (Exception var10)
				{
					CrashReport var7 = CrashReport.makeCrashReport(var10, "Exception initializing level");

					try
					{
						this.addWorldInfoToCrashReport(var7);
					}
					catch (Exception var9)
					{
						;
					}

					throw new ReportedException(var7);
				}

				this.worldInfo.ServerInitialized = true;
			}

			VillageCollection var6 = (VillageCollection)this.mapStorage.loadData(typeof(VillageCollection), "villages");

			if (var6 == null)
			{
				this.villageCollectionObj = new VillageCollection(this);
				this.mapStorage.setData("villages", this.villageCollectionObj);
			}
			else
			{
				this.villageCollectionObj = var6;
				this.villageCollectionObj.func_82566_a(this);
			}

			this.calculateInitialSkylight();
			this.calculateInitialWeather();
		}

///    
///     <summary> * Creates the chunk provider for this world. Called in the constructor. Retrieves provider from worldProvider? </summary>
///     
		protected internal abstract IChunkProvider createChunkProvider();

		protected internal virtual void initialize(WorldSettings p_72963_1_)
		{
			this.worldInfo.ServerInitialized = true;
		}

///    
///     <summary> * Sets a new spawn location by finding an uncovered block at a random (x,z) location in the chunk. </summary>
///     
		public virtual void setSpawnLocation()
		{
			this.setSpawnLocation(8, 64, 8);
		}

		public virtual Block getTopBlock(int p_147474_1_, int p_147474_2_)
		{
			int var3;

			for (var3 = 63; !this.isAirBlock(p_147474_1_, var3 + 1, p_147474_2_); ++var3)
			{
				;
			}

			return this.getBlock(p_147474_1_, var3, p_147474_2_);
		}

		public virtual Block getBlock(int p_147439_1_, int p_147439_2_, int p_147439_3_)
		{
			if (p_147439_1_ >= -30000000 && p_147439_3_ >= -30000000 && p_147439_1_ < 30000000 && p_147439_3_ < 30000000 && p_147439_2_ >= 0 && p_147439_2_ < 256)
			{
				Chunk var4 = null;

				try
				{
					var4 = this.getChunkFromChunkCoords(p_147439_1_ >> 4, p_147439_3_ >> 4);
					return var4.func_150810_a(p_147439_1_ & 15, p_147439_2_, p_147439_3_ & 15);
				}
				catch (Exception var8)
				{
					CrashReport var6 = CrashReport.makeCrashReport(var8, "Exception getting block type in world");
					CrashReportCategory var7 = var6.makeCategory("Requested block coordinates");
					var7.addCrashSection("Found chunk", Convert.ToBoolean(var4 == null));
					var7.addCrashSection("Location", CrashReportCategory.getLocationInfo(p_147439_1_, p_147439_2_, p_147439_3_));
					throw new ReportedException(var6);
				}
			}
			else
			{
				return Blocks.air;
			}
		}

///    
///     <summary> * Returns true if the block at the specified coordinates is empty </summary>
///     
		public virtual bool isAirBlock(int p_147437_1_, int p_147437_2_, int p_147437_3_)
		{
			return this.getBlock(p_147437_1_, p_147437_2_, p_147437_3_).Material == Material.air;
		}

///    
///     <summary> * Returns whether a block exists at world coordinates x, y, z </summary>
///     
		public virtual bool blockExists(int p_72899_1_, int p_72899_2_, int p_72899_3_)
		{
			return p_72899_2_ >= 0 && p_72899_2_ < 256 ? this.chunkExists(p_72899_1_ >> 4, p_72899_3_ >> 4) : false;
		}

///    
///     <summary> * Checks if any of the chunks within distance (argument 4) blocks of the given block exist </summary>
///     
		public virtual bool doChunksNearChunkExist(int p_72873_1_, int p_72873_2_, int p_72873_3_, int p_72873_4_)
		{
			return this.checkChunksExist(p_72873_1_ - p_72873_4_, p_72873_2_ - p_72873_4_, p_72873_3_ - p_72873_4_, p_72873_1_ + p_72873_4_, p_72873_2_ + p_72873_4_, p_72873_3_ + p_72873_4_);
		}

///    
///     <summary> * Checks between a min and max all the chunks inbetween actually exist. Args: minX, minY, minZ, maxX, maxY, maxZ </summary>
///     
		public virtual bool checkChunksExist(int p_72904_1_, int p_72904_2_, int p_72904_3_, int p_72904_4_, int p_72904_5_, int p_72904_6_)
		{
			if (p_72904_5_ >= 0 && p_72904_2_ < 256)
			{
				p_72904_1_ >>= 4;
				p_72904_3_ >>= 4;
				p_72904_4_ >>= 4;
				p_72904_6_ >>= 4;

				for (int var7 = p_72904_1_; var7 <= p_72904_4_; ++var7)
				{
					for (int var8 = p_72904_3_; var8 <= p_72904_6_; ++var8)
					{
						if (!this.chunkExists(var7, var8))
						{
							return false;
						}
					}
				}

				return true;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Returns whether a chunk exists at chunk coordinates x, y </summary>
///     
		protected internal virtual bool chunkExists(int p_72916_1_, int p_72916_2_)
		{
			return this.chunkProvider.chunkExists(p_72916_1_, p_72916_2_);
		}

///    
///     <summary> * Returns a chunk looked up by block coordinates. Args: x, z </summary>
///     
		public virtual Chunk getChunkFromBlockCoords(int p_72938_1_, int p_72938_2_)
		{
			return this.getChunkFromChunkCoords(p_72938_1_ >> 4, p_72938_2_ >> 4);
		}

///    
///     <summary> * Returns back a chunk looked up by chunk coordinates Args: x, y </summary>
///     
		public virtual Chunk getChunkFromChunkCoords(int p_72964_1_, int p_72964_2_)
		{
			return this.chunkProvider.provideChunk(p_72964_1_, p_72964_2_);
		}

///    
///     <summary> * Sets the block ID and metadata at a given location. Args: X, Y, Z, new block ID, new metadata, flags. Flag 1 will
///     * cause a block update. Flag 2 will send the change to clients (you almost always want this). Flag 4 prevents the
///     * block from being re-rendered, if this is a client world. Flags can be added together. </summary>
///     
		public virtual bool setBlock(int p_147465_1_, int p_147465_2_, int p_147465_3_, Block p_147465_4_, int p_147465_5_, int p_147465_6_)
		{
			if (p_147465_1_ >= -30000000 && p_147465_3_ >= -30000000 && p_147465_1_ < 30000000 && p_147465_3_ < 30000000)
			{
				if (p_147465_2_ < 0)
				{
					return false;
				}
				else if (p_147465_2_ >= 256)
				{
					return false;
				}
				else
				{
					Chunk var7 = this.getChunkFromChunkCoords(p_147465_1_ >> 4, p_147465_3_ >> 4);
					Block var8 = null;

					if ((p_147465_6_ & 1) != 0)
					{
						var8 = var7.func_150810_a(p_147465_1_ & 15, p_147465_2_, p_147465_3_ & 15);
					}

					bool var9 = var7.func_150807_a(p_147465_1_ & 15, p_147465_2_, p_147465_3_ & 15, p_147465_4_, p_147465_5_);
					this.theProfiler.startSection("checkLight");
					this.func_147451_t(p_147465_1_, p_147465_2_, p_147465_3_);
					this.theProfiler.endSection();

					if (var9)
					{
						if ((p_147465_6_ & 2) != 0 && (!this.isClient || (p_147465_6_ & 4) == 0) && var7.func_150802_k())
						{
							this.func_147471_g(p_147465_1_, p_147465_2_, p_147465_3_);
						}

						if (!this.isClient && (p_147465_6_ & 1) != 0)
						{
							this.notifyBlockChange(p_147465_1_, p_147465_2_, p_147465_3_, var8);

							if (p_147465_4_.hasComparatorInputOverride())
							{
								this.func_147453_f(p_147465_1_, p_147465_2_, p_147465_3_, p_147465_4_);
							}
						}
					}

					return var9;
				}
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Returns the block metadata at coords x,y,z </summary>
///     
		public virtual int getBlockMetadata(int p_72805_1_, int p_72805_2_, int p_72805_3_)
		{
			if (p_72805_1_ >= -30000000 && p_72805_3_ >= -30000000 && p_72805_1_ < 30000000 && p_72805_3_ < 30000000)
			{
				if (p_72805_2_ < 0)
				{
					return 0;
				}
				else if (p_72805_2_ >= 256)
				{
					return 0;
				}
				else
				{
					Chunk var4 = this.getChunkFromChunkCoords(p_72805_1_ >> 4, p_72805_3_ >> 4);
					p_72805_1_ &= 15;
					p_72805_3_ &= 15;
					return var4.getBlockMetadata(p_72805_1_, p_72805_2_, p_72805_3_);
				}
			}
			else
			{
				return 0;
			}
		}

///    
///     <summary> * Sets the blocks metadata and if set will then notify blocks that this block changed, depending on the flag. Args:
///     * x, y, z, metadata, flag. See setBlock for flag description </summary>
///     
		public virtual bool setBlockMetadataWithNotify(int p_72921_1_, int p_72921_2_, int p_72921_3_, int p_72921_4_, int p_72921_5_)
		{
			if (p_72921_1_ >= -30000000 && p_72921_3_ >= -30000000 && p_72921_1_ < 30000000 && p_72921_3_ < 30000000)
			{
				if (p_72921_2_ < 0)
				{
					return false;
				}
				else if (p_72921_2_ >= 256)
				{
					return false;
				}
				else
				{
					Chunk var6 = this.getChunkFromChunkCoords(p_72921_1_ >> 4, p_72921_3_ >> 4);
					int var7 = p_72921_1_ & 15;
					int var8 = p_72921_3_ & 15;
					bool var9 = var6.setBlockMetadata(var7, p_72921_2_, var8, p_72921_4_);

					if (var9)
					{
						Block var10 = var6.func_150810_a(var7, p_72921_2_, var8);

						if ((p_72921_5_ & 2) != 0 && (!this.isClient || (p_72921_5_ & 4) == 0) && var6.func_150802_k())
						{
							this.func_147471_g(p_72921_1_, p_72921_2_, p_72921_3_);
						}

						if (!this.isClient && (p_72921_5_ & 1) != 0)
						{
							this.notifyBlockChange(p_72921_1_, p_72921_2_, p_72921_3_, var10);

							if (var10.hasComparatorInputOverride())
							{
								this.func_147453_f(p_72921_1_, p_72921_2_, p_72921_3_, var10);
							}
						}
					}

					return var9;
				}
			}
			else
			{
				return false;
			}
		}

		public virtual bool setBlockToAir(int p_147468_1_, int p_147468_2_, int p_147468_3_)
		{
			return this.setBlock(p_147468_1_, p_147468_2_, p_147468_3_, Blocks.air, 0, 3);
		}

		public virtual bool func_147480_a(int p_147480_1_, int p_147480_2_, int p_147480_3_, bool p_147480_4_)
		{
			Block var5 = this.getBlock(p_147480_1_, p_147480_2_, p_147480_3_);

			if (var5.Material == Material.air)
			{
				return false;
			}
			else
			{
				int var6 = this.getBlockMetadata(p_147480_1_, p_147480_2_, p_147480_3_);
				this.playAuxSFX(2001, p_147480_1_, p_147480_2_, p_147480_3_, Block.getIdFromBlock(var5) + (var6 << 12));

				if (p_147480_4_)
				{
					var5.dropBlockAsItem(this, p_147480_1_, p_147480_2_, p_147480_3_, var6, 0);
				}

				return this.setBlock(p_147480_1_, p_147480_2_, p_147480_3_, Blocks.air, 0, 3);
			}
		}

///    
///     <summary> * Sets a block by a coordinate </summary>
///     
		public virtual bool setBlock(int p_147449_1_, int p_147449_2_, int p_147449_3_, Block p_147449_4_)
		{
			return this.setBlock(p_147449_1_, p_147449_2_, p_147449_3_, p_147449_4_, 0, 3);
		}

		public virtual void func_147471_g(int p_147471_1_, int p_147471_2_, int p_147471_3_)
		{
			for (int var4 = 0; var4 < this.worldAccesses.Count; ++var4)
			{
				((IWorldAccess)this.worldAccesses.get(var4)).markBlockForUpdate(p_147471_1_, p_147471_2_, p_147471_3_);
			}
		}

///    
///     <summary> * The block type change and need to notify other systems  Args: x, y, z, blockID </summary>
///     
		public virtual void notifyBlockChange(int p_147444_1_, int p_147444_2_, int p_147444_3_, Block p_147444_4_)
		{
			this.notifyBlocksOfNeighborChange(p_147444_1_, p_147444_2_, p_147444_3_, p_147444_4_);
		}

///    
///     <summary> * marks a vertical line of blocks as dirty </summary>
///     
		public virtual void markBlocksDirtyVertical(int p_72975_1_, int p_72975_2_, int p_72975_3_, int p_72975_4_)
		{
			int var5;

			if (p_72975_3_ > p_72975_4_)
			{
				var5 = p_72975_4_;
				p_72975_4_ = p_72975_3_;
				p_72975_3_ = var5;
			}

			if (!this.provider.hasNoSky)
			{
				for (var5 = p_72975_3_; var5 <= p_72975_4_; ++var5)
				{
					this.updateLightByType(EnumSkyBlock.Sky, p_72975_1_, var5, p_72975_2_);
				}
			}

			this.markBlockRangeForRenderUpdate(p_72975_1_, p_72975_3_, p_72975_2_, p_72975_1_, p_72975_4_, p_72975_2_);
		}

		public virtual void markBlockRangeForRenderUpdate(int p_147458_1_, int p_147458_2_, int p_147458_3_, int p_147458_4_, int p_147458_5_, int p_147458_6_)
		{
			for (int var7 = 0; var7 < this.worldAccesses.Count; ++var7)
			{
				((IWorldAccess)this.worldAccesses.get(var7)).markBlockRangeForRenderUpdate(p_147458_1_, p_147458_2_, p_147458_3_, p_147458_4_, p_147458_5_, p_147458_6_);
			}
		}

		public virtual void notifyBlocksOfNeighborChange(int p_147459_1_, int p_147459_2_, int p_147459_3_, Block p_147459_4_)
		{
			this.func_147460_e(p_147459_1_ - 1, p_147459_2_, p_147459_3_, p_147459_4_);
			this.func_147460_e(p_147459_1_ + 1, p_147459_2_, p_147459_3_, p_147459_4_);
			this.func_147460_e(p_147459_1_, p_147459_2_ - 1, p_147459_3_, p_147459_4_);
			this.func_147460_e(p_147459_1_, p_147459_2_ + 1, p_147459_3_, p_147459_4_);
			this.func_147460_e(p_147459_1_, p_147459_2_, p_147459_3_ - 1, p_147459_4_);
			this.func_147460_e(p_147459_1_, p_147459_2_, p_147459_3_ + 1, p_147459_4_);
		}

		public virtual void func_147441_b(int p_147441_1_, int p_147441_2_, int p_147441_3_, Block p_147441_4_, int p_147441_5_)
		{
			if (p_147441_5_ != 4)
			{
				this.func_147460_e(p_147441_1_ - 1, p_147441_2_, p_147441_3_, p_147441_4_);
			}

			if (p_147441_5_ != 5)
			{
				this.func_147460_e(p_147441_1_ + 1, p_147441_2_, p_147441_3_, p_147441_4_);
			}

			if (p_147441_5_ != 0)
			{
				this.func_147460_e(p_147441_1_, p_147441_2_ - 1, p_147441_3_, p_147441_4_);
			}

			if (p_147441_5_ != 1)
			{
				this.func_147460_e(p_147441_1_, p_147441_2_ + 1, p_147441_3_, p_147441_4_);
			}

			if (p_147441_5_ != 2)
			{
				this.func_147460_e(p_147441_1_, p_147441_2_, p_147441_3_ - 1, p_147441_4_);
			}

			if (p_147441_5_ != 3)
			{
				this.func_147460_e(p_147441_1_, p_147441_2_, p_147441_3_ + 1, p_147441_4_);
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: 'final' parameters are not allowed in .NET:
//ORIGINAL LINE: public void func_147460_e(int p_147460_1_, int p_147460_2_, int p_147460_3_, final Block p_147460_4_)
		public virtual void func_147460_e(int p_147460_1_, int p_147460_2_, int p_147460_3_, Block p_147460_4_)
		{
			if (!this.isClient)
			{
				Block var5 = this.getBlock(p_147460_1_, p_147460_2_, p_147460_3_);

				try
				{
					var5.onNeighborBlockChange(this, p_147460_1_, p_147460_2_, p_147460_3_, p_147460_4_);
				}
				catch (Exception var12)
				{
					CrashReport var7 = CrashReport.makeCrashReport(var12, "Exception while updating neighbours");
					CrashReportCategory var8 = var7.makeCategory("Block being updated");
					int var9;

					try
					{
						var9 = this.getBlockMetadata(p_147460_1_, p_147460_2_, p_147460_3_);
					}
					catch (Exception var11)
					{
						var9 = -1;
					}

					var8.addCrashSectionCallable("Source block type", new Callable() { private static final string __OBFID = "CL_00000142"; public string call() { try { return string.Format("ID #{0:D} ({1} // {2})", new object[] {Convert.ToInt32(Block.getIdFromBlock(p_147460_4_)), p_147460_4_.UnlocalizedName, p_147460_4_.GetType().CanonicalName}); } catch (Exception var2) { return "ID #" + Block.getIdFromBlock(p_147460_4_); } } });
					CrashReportCategory.func_147153_a(var8, p_147460_1_, p_147460_2_, p_147460_3_, var5, var9);
					throw new ReportedException(var7);
				}
			}
		}

		public virtual bool func_147477_a(int p_147477_1_, int p_147477_2_, int p_147477_3_, Block p_147477_4_)
		{
			return false;
		}

///    
///     <summary> * Checks if the specified block is able to see the sky </summary>
///     
		public virtual bool canBlockSeeTheSky(int p_72937_1_, int p_72937_2_, int p_72937_3_)
		{
			return this.getChunkFromChunkCoords(p_72937_1_ >> 4, p_72937_3_ >> 4).canBlockSeeTheSky(p_72937_1_ & 15, p_72937_2_, p_72937_3_ & 15);
		}

///    
///     <summary> * Does the same as getBlockLightValue_do but without checking if its not a normal block </summary>
///     
		public virtual int getFullBlockLightValue(int p_72883_1_, int p_72883_2_, int p_72883_3_)
		{
			if (p_72883_2_ < 0)
			{
				return 0;
			}
			else
			{
				if (p_72883_2_ >= 256)
				{
					p_72883_2_ = 255;
				}

				return this.getChunkFromChunkCoords(p_72883_1_ >> 4, p_72883_3_ >> 4).getBlockLightValue(p_72883_1_ & 15, p_72883_2_, p_72883_3_ & 15, 0);
			}
		}

///    
///     <summary> * Gets the light value of a block location </summary>
///     
		public virtual int getBlockLightValue(int p_72957_1_, int p_72957_2_, int p_72957_3_)
		{
			return this.getBlockLightValue_do(p_72957_1_, p_72957_2_, p_72957_3_, true);
		}

///    
///     <summary> * Gets the light value of a block location. This is the actual function that gets the value and has a bool flag
///     * that indicates if its a half step block to get the maximum light value of a direct neighboring block (left,
///     * right, forward, back, and up) </summary>
///     
		public virtual int getBlockLightValue_do(int p_72849_1_, int p_72849_2_, int p_72849_3_, bool p_72849_4_)
		{
			if (p_72849_1_ >= -30000000 && p_72849_3_ >= -30000000 && p_72849_1_ < 30000000 && p_72849_3_ < 30000000)
			{
				if (p_72849_4_ && this.getBlock(p_72849_1_, p_72849_2_, p_72849_3_).func_149710_n())
				{
					int var10 = this.getBlockLightValue_do(p_72849_1_, p_72849_2_ + 1, p_72849_3_, false);
					int var6 = this.getBlockLightValue_do(p_72849_1_ + 1, p_72849_2_, p_72849_3_, false);
					int var7 = this.getBlockLightValue_do(p_72849_1_ - 1, p_72849_2_, p_72849_3_, false);
					int var8 = this.getBlockLightValue_do(p_72849_1_, p_72849_2_, p_72849_3_ + 1, false);
					int var9 = this.getBlockLightValue_do(p_72849_1_, p_72849_2_, p_72849_3_ - 1, false);

					if (var6 > var10)
					{
						var10 = var6;
					}

					if (var7 > var10)
					{
						var10 = var7;
					}

					if (var8 > var10)
					{
						var10 = var8;
					}

					if (var9 > var10)
					{
						var10 = var9;
					}

					return var10;
				}
				else if (p_72849_2_ < 0)
				{
					return 0;
				}
				else
				{
					if (p_72849_2_ >= 256)
					{
						p_72849_2_ = 255;
					}

					Chunk var5 = this.getChunkFromChunkCoords(p_72849_1_ >> 4, p_72849_3_ >> 4);
					p_72849_1_ &= 15;
					p_72849_3_ &= 15;
					return var5.getBlockLightValue(p_72849_1_, p_72849_2_, p_72849_3_, this.skylightSubtracted);
				}
			}
			else
			{
				return 15;
			}
		}

///    
///     <summary> * Returns the y coordinate with a block in it at this x, z coordinate </summary>
///     
		public virtual int getHeightValue(int p_72976_1_, int p_72976_2_)
		{
			if (p_72976_1_ >= -30000000 && p_72976_2_ >= -30000000 && p_72976_1_ < 30000000 && p_72976_2_ < 30000000)
			{
				if (!this.chunkExists(p_72976_1_ >> 4, p_72976_2_ >> 4))
				{
					return 0;
				}
				else
				{
					Chunk var3 = this.getChunkFromChunkCoords(p_72976_1_ >> 4, p_72976_2_ >> 4);
					return var3.getHeightValue(p_72976_1_ & 15, p_72976_2_ & 15);
				}
			}
			else
			{
				return 64;
			}
		}

///    
///     <summary> * Gets the heightMapMinimum field of the given chunk, or 0 if the chunk is not loaded. Coords are in blocks. Args:
///     * X, Z </summary>
///     
		public virtual int getChunkHeightMapMinimum(int p_82734_1_, int p_82734_2_)
		{
			if (p_82734_1_ >= -30000000 && p_82734_2_ >= -30000000 && p_82734_1_ < 30000000 && p_82734_2_ < 30000000)
			{
				if (!this.chunkExists(p_82734_1_ >> 4, p_82734_2_ >> 4))
				{
					return 0;
				}
				else
				{
					Chunk var3 = this.getChunkFromChunkCoords(p_82734_1_ >> 4, p_82734_2_ >> 4);
					return var3.heightMapMinimum;
				}
			}
			else
			{
				return 64;
			}
		}

///    
///     <summary> * Brightness for SkyBlock.Sky is clear white and (through color computing it is assumed) DEPENDENT ON DAYTIME.
///     * Brightness for SkyBlock.Block is yellowish and independent. </summary>
///     
		public virtual int getSkyBlockTypeBrightness(EnumSkyBlock p_72925_1_, int p_72925_2_, int p_72925_3_, int p_72925_4_)
		{
			if (this.provider.hasNoSky && p_72925_1_ == EnumSkyBlock.Sky)
			{
				return 0;
			}
			else
			{
				if (p_72925_3_ < 0)
				{
					p_72925_3_ = 0;
				}

				if (p_72925_3_ >= 256)
				{
					return p_72925_1_.defaultLightValue;
				}
				else if (p_72925_2_ >= -30000000 && p_72925_4_ >= -30000000 && p_72925_2_ < 30000000 && p_72925_4_ < 30000000)
				{
					int var5 = p_72925_2_ >> 4;
					int var6 = p_72925_4_ >> 4;

					if (!this.chunkExists(var5, var6))
					{
						return p_72925_1_.defaultLightValue;
					}
					else if (this.getBlock(p_72925_2_, p_72925_3_, p_72925_4_).func_149710_n())
					{
						int var12 = this.getSavedLightValue(p_72925_1_, p_72925_2_, p_72925_3_ + 1, p_72925_4_);
						int var8 = this.getSavedLightValue(p_72925_1_, p_72925_2_ + 1, p_72925_3_, p_72925_4_);
						int var9 = this.getSavedLightValue(p_72925_1_, p_72925_2_ - 1, p_72925_3_, p_72925_4_);
						int var10 = this.getSavedLightValue(p_72925_1_, p_72925_2_, p_72925_3_, p_72925_4_ + 1);
						int var11 = this.getSavedLightValue(p_72925_1_, p_72925_2_, p_72925_3_, p_72925_4_ - 1);

						if (var8 > var12)
						{
							var12 = var8;
						}

						if (var9 > var12)
						{
							var12 = var9;
						}

						if (var10 > var12)
						{
							var12 = var10;
						}

						if (var11 > var12)
						{
							var12 = var11;
						}

						return var12;
					}
					else
					{
						Chunk var7 = this.getChunkFromChunkCoords(var5, var6);
						return var7.getSavedLightValue(p_72925_1_, p_72925_2_ & 15, p_72925_3_, p_72925_4_ & 15);
					}
				}
				else
				{
					return p_72925_1_.defaultLightValue;
				}
			}
		}

///    
///     <summary> * Returns saved light value without taking into account the time of day.  Either looks in the sky light map or
///     * block light map based on the enumSkyBlock arg. </summary>
///     
		public virtual int getSavedLightValue(EnumSkyBlock p_72972_1_, int p_72972_2_, int p_72972_3_, int p_72972_4_)
		{
			if (p_72972_3_ < 0)
			{
				p_72972_3_ = 0;
			}

			if (p_72972_3_ >= 256)
			{
				p_72972_3_ = 255;
			}

			if (p_72972_2_ >= -30000000 && p_72972_4_ >= -30000000 && p_72972_2_ < 30000000 && p_72972_4_ < 30000000)
			{
				int var5 = p_72972_2_ >> 4;
				int var6 = p_72972_4_ >> 4;

				if (!this.chunkExists(var5, var6))
				{
					return p_72972_1_.defaultLightValue;
				}
				else
				{
					Chunk var7 = this.getChunkFromChunkCoords(var5, var6);
					return var7.getSavedLightValue(p_72972_1_, p_72972_2_ & 15, p_72972_3_, p_72972_4_ & 15);
				}
			}
			else
			{
				return p_72972_1_.defaultLightValue;
			}
		}

///    
///     <summary> * Sets the light value either into the sky map or block map depending on if enumSkyBlock is set to sky or block.
///     * Args: enumSkyBlock, x, y, z, lightValue </summary>
///     
		public virtual void setLightValue(EnumSkyBlock p_72915_1_, int p_72915_2_, int p_72915_3_, int p_72915_4_, int p_72915_5_)
		{
			if (p_72915_2_ >= -30000000 && p_72915_4_ >= -30000000 && p_72915_2_ < 30000000 && p_72915_4_ < 30000000)
			{
				if (p_72915_3_ >= 0)
				{
					if (p_72915_3_ < 256)
					{
						if (this.chunkExists(p_72915_2_ >> 4, p_72915_4_ >> 4))
						{
							Chunk var6 = this.getChunkFromChunkCoords(p_72915_2_ >> 4, p_72915_4_ >> 4);
							var6.setLightValue(p_72915_1_, p_72915_2_ & 15, p_72915_3_, p_72915_4_ & 15, p_72915_5_);

							for (int var7 = 0; var7 < this.worldAccesses.Count; ++var7)
							{
								((IWorldAccess)this.worldAccesses.get(var7)).markBlockForRenderUpdate(p_72915_2_, p_72915_3_, p_72915_4_);
							}
						}
					}
				}
			}
		}

		public virtual void func_147479_m(int p_147479_1_, int p_147479_2_, int p_147479_3_)
		{
			for (int var4 = 0; var4 < this.worldAccesses.Count; ++var4)
			{
				((IWorldAccess)this.worldAccesses.get(var4)).markBlockForRenderUpdate(p_147479_1_, p_147479_2_, p_147479_3_);
			}
		}

///    
///     <summary> * Any Light rendered on a 1.8 Block goes through here </summary>
///     
		public virtual int getLightBrightnessForSkyBlocks(int p_72802_1_, int p_72802_2_, int p_72802_3_, int p_72802_4_)
		{
			int var5 = this.getSkyBlockTypeBrightness(EnumSkyBlock.Sky, p_72802_1_, p_72802_2_, p_72802_3_);
			int var6 = this.getSkyBlockTypeBrightness(EnumSkyBlock.Block, p_72802_1_, p_72802_2_, p_72802_3_);

			if (var6 < p_72802_4_)
			{
				var6 = p_72802_4_;
			}

			return var5 << 20 | var6 << 4;
		}

///    
///     <summary> * Returns how bright the block is shown as which is the block's light value looked up in a lookup table (light
///     * values aren't linear for brightness). Args: x, y, z </summary>
///     
		public virtual float getLightBrightness(int p_72801_1_, int p_72801_2_, int p_72801_3_)
		{
			return this.provider.lightBrightnessTable[this.getBlockLightValue(p_72801_1_, p_72801_2_, p_72801_3_)];
		}

///    
///     <summary> * Checks whether its daytime by seeing if the light subtracted from the skylight is less than 4 </summary>
///     
		public virtual bool isDaytime()
		{
			get
			{
				return this.skylightSubtracted < 4;
			}
		}

///    
///     <summary> * Performs a raycast against all blocks in the world except liquids. </summary>
///     
		public virtual MovingObjectPosition rayTraceBlocks(Vec3 p_72933_1_, Vec3 p_72933_2_)
		{
			return this.func_147447_a(p_72933_1_, p_72933_2_, false, false, false);
		}

///    
///     <summary> * Performs a raycast against all blocks in the world, and optionally liquids. </summary>
///     
		public virtual MovingObjectPosition rayTraceBlocks(Vec3 p_72901_1_, Vec3 p_72901_2_, bool p_72901_3_)
		{
			return this.func_147447_a(p_72901_1_, p_72901_2_, p_72901_3_, false, false);
		}

		public virtual MovingObjectPosition func_147447_a(Vec3 p_147447_1_, Vec3 p_147447_2_, bool p_147447_3_, bool p_147447_4_, bool p_147447_5_)
		{
			if (!double.isNaN(p_147447_1_.xCoord) && !double.isNaN(p_147447_1_.yCoord) && !double.isNaN(p_147447_1_.zCoord))
			{
				if (!double.isNaN(p_147447_2_.xCoord) && !double.isNaN(p_147447_2_.yCoord) && !double.isNaN(p_147447_2_.zCoord))
				{
					int var6 = MathHelper.floor_double(p_147447_2_.xCoord);
					int var7 = MathHelper.floor_double(p_147447_2_.yCoord);
					int var8 = MathHelper.floor_double(p_147447_2_.zCoord);
					int var9 = MathHelper.floor_double(p_147447_1_.xCoord);
					int var10 = MathHelper.floor_double(p_147447_1_.yCoord);
					int var11 = MathHelper.floor_double(p_147447_1_.zCoord);
					Block var12 = this.getBlock(var9, var10, var11);
					int var13 = this.getBlockMetadata(var9, var10, var11);

					if ((!p_147447_4_ || var12.getCollisionBoundingBoxFromPool(this, var9, var10, var11) != null) && var12.canCollideCheck(var13, p_147447_3_))
					{
						MovingObjectPosition var14 = var12.collisionRayTrace(this, var9, var10, var11, p_147447_1_, p_147447_2_);

						if (var14 != null)
						{
							return var14;
						}
					}

					MovingObjectPosition var40 = null;
					var13 = 200;

					while (var13-- >= 0)
					{
						if (double.isNaN(p_147447_1_.xCoord) || double.isNaN(p_147447_1_.yCoord) || double.isNaN(p_147447_1_.zCoord))
						{
							return null;
						}

						if (var9 == var6 && var10 == var7 && var11 == var8)
						{
							return p_147447_5_ ? var40 : null;
						}

						bool var41 = true;
						bool var15 = true;
						bool var16 = true;
						double var17 = 999.0D;
						double var19 = 999.0D;
						double var21 = 999.0D;

						if (var6 > var9)
						{
							var17 = (double)var9 + 1.0D;
						}
						else if (var6 < var9)
						{
							var17 = (double)var9 + 0.0D;
						}
						else
						{
							var41 = false;
						}

						if (var7 > var10)
						{
							var19 = (double)var10 + 1.0D;
						}
						else if (var7 < var10)
						{
							var19 = (double)var10 + 0.0D;
						}
						else
						{
							var15 = false;
						}

						if (var8 > var11)
						{
							var21 = (double)var11 + 1.0D;
						}
						else if (var8 < var11)
						{
							var21 = (double)var11 + 0.0D;
						}
						else
						{
							var16 = false;
						}

						double var23 = 999.0D;
						double var25 = 999.0D;
						double var27 = 999.0D;
						double var29 = p_147447_2_.xCoord - p_147447_1_.xCoord;
						double var31 = p_147447_2_.yCoord - p_147447_1_.yCoord;
						double var33 = p_147447_2_.zCoord - p_147447_1_.zCoord;

						if (var41)
						{
							var23 = (var17 - p_147447_1_.xCoord) / var29;
						}

						if (var15)
						{
							var25 = (var19 - p_147447_1_.yCoord) / var31;
						}

						if (var16)
						{
							var27 = (var21 - p_147447_1_.zCoord) / var33;
						}

						bool var35 = false;
						sbyte var42;

						if (var23 < var25 && var23 < var27)
						{
							if (var6 > var9)
							{
								var42 = 4;
							}
							else
							{
								var42 = 5;
							}

							p_147447_1_.xCoord = var17;
							p_147447_1_.yCoord += var31 * var23;
							p_147447_1_.zCoord += var33 * var23;
						}
						else if (var25 < var27)
						{
							if (var7 > var10)
							{
								var42 = 0;
							}
							else
							{
								var42 = 1;
							}

							p_147447_1_.xCoord += var29 * var25;
							p_147447_1_.yCoord = var19;
							p_147447_1_.zCoord += var33 * var25;
						}
						else
						{
							if (var8 > var11)
							{
								var42 = 2;
							}
							else
							{
								var42 = 3;
							}

							p_147447_1_.xCoord += var29 * var27;
							p_147447_1_.yCoord += var31 * var27;
							p_147447_1_.zCoord = var21;
						}

						Vec3 var36 = Vec3.createVectorHelper(p_147447_1_.xCoord, p_147447_1_.yCoord, p_147447_1_.zCoord);
						var9 = (int)(var36.xCoord = (double)MathHelper.floor_double(p_147447_1_.xCoord));

						if (var42 == 5)
						{
							--var9;
							++var36.xCoord;
						}

						var10 = (int)(var36.yCoord = (double)MathHelper.floor_double(p_147447_1_.yCoord));

						if (var42 == 1)
						{
							--var10;
							++var36.yCoord;
						}

						var11 = (int)(var36.zCoord = (double)MathHelper.floor_double(p_147447_1_.zCoord));

						if (var42 == 3)
						{
							--var11;
							++var36.zCoord;
						}

						Block var37 = this.getBlock(var9, var10, var11);
						int var38 = this.getBlockMetadata(var9, var10, var11);

						if (!p_147447_4_ || var37.getCollisionBoundingBoxFromPool(this, var9, var10, var11) != null)
						{
							if (var37.canCollideCheck(var38, p_147447_3_))
							{
								MovingObjectPosition var39 = var37.collisionRayTrace(this, var9, var10, var11, p_147447_1_, p_147447_2_);

								if (var39 != null)
								{
									return var39;
								}
							}
							else
							{
								var40 = new MovingObjectPosition(var9, var10, var11, var42, p_147447_1_, false);
							}
						}
					}

					return p_147447_5_ ? var40 : null;
				}
				else
				{
					return null;
				}
			}
			else
			{
				return null;
			}
		}

///    
///     <summary> * Plays a sound at the entity's position. Args: entity, sound, volume (relative to 1.0), and frequency (or pitch,
///     * also relative to 1.0). </summary>
///     
		public virtual void playSoundAtEntity(Entity p_72956_1_, string p_72956_2_, float p_72956_3_, float p_72956_4_)
		{
			for (int var5 = 0; var5 < this.worldAccesses.Count; ++var5)
			{
				((IWorldAccess)this.worldAccesses.get(var5)).playSound(p_72956_2_, p_72956_1_.posX, p_72956_1_.posY - (double)p_72956_1_.yOffset, p_72956_1_.posZ, p_72956_3_, p_72956_4_);
			}
		}

///    
///     <summary> * Plays sound to all near players except the player reference given </summary>
///     
		public virtual void playSoundToNearExcept(EntityPlayer p_85173_1_, string p_85173_2_, float p_85173_3_, float p_85173_4_)
		{
			for (int var5 = 0; var5 < this.worldAccesses.Count; ++var5)
			{
				((IWorldAccess)this.worldAccesses.get(var5)).playSoundToNearExcept(p_85173_1_, p_85173_2_, p_85173_1_.posX, p_85173_1_.posY - (double)p_85173_1_.yOffset, p_85173_1_.posZ, p_85173_3_, p_85173_4_);
			}
		}

///    
///     <summary> * Play a sound effect. Many many parameters for this function. Not sure what they do, but a classic call is :
///     * (double)i + 0.5D, (double)j + 0.5D, (double)k + 0.5D, 'random.door_open', 1.0F, world.rand.nextFloat() * 0.1F +
///     * 0.9F with i,j,k position of the block. </summary>
///     
		public virtual void playSoundEffect(double p_72908_1_, double p_72908_3_, double p_72908_5_, string p_72908_7_, float p_72908_8_, float p_72908_9_)
		{
			for (int var10 = 0; var10 < this.worldAccesses.Count; ++var10)
			{
				((IWorldAccess)this.worldAccesses.get(var10)).playSound(p_72908_7_, p_72908_1_, p_72908_3_, p_72908_5_, p_72908_8_, p_72908_9_);
			}
		}

///    
///     <summary> * par8 is loudness, all pars passed to minecraftInstance.sndManager.playSound </summary>
///     
		public virtual void playSound(double p_72980_1_, double p_72980_3_, double p_72980_5_, string p_72980_7_, float p_72980_8_, float p_72980_9_, bool p_72980_10_)
		{
		}

///    
///     <summary> * Plays a record at the specified coordinates of the specified name. Args: recordName, x, y, z </summary>
///     
		public virtual void playRecord(string p_72934_1_, int p_72934_2_, int p_72934_3_, int p_72934_4_)
		{
			for (int var5 = 0; var5 < this.worldAccesses.Count; ++var5)
			{
				((IWorldAccess)this.worldAccesses.get(var5)).playRecord(p_72934_1_, p_72934_2_, p_72934_3_, p_72934_4_);
			}
		}

///    
///     <summary> * Spawns a particle.  Args particleName, x, y, z, velX, velY, velZ </summary>
///     
		public virtual void spawnParticle(string p_72869_1_, double p_72869_2_, double p_72869_4_, double p_72869_6_, double p_72869_8_, double p_72869_10_, double p_72869_12_)
		{
			for (int var14 = 0; var14 < this.worldAccesses.Count; ++var14)
			{
				((IWorldAccess)this.worldAccesses.get(var14)).spawnParticle(p_72869_1_, p_72869_2_, p_72869_4_, p_72869_6_, p_72869_8_, p_72869_10_, p_72869_12_);
			}
		}

///    
///     <summary> * adds a lightning bolt to the list of lightning bolts in this world. </summary>
///     
		public virtual bool addWeatherEffect(Entity p_72942_1_)
		{
			this.weatherEffects.Add(p_72942_1_);
			return true;
		}

///    
///     <summary> * Called to place all entities as part of a world </summary>
///     
		public virtual bool spawnEntityInWorld(Entity p_72838_1_)
		{
			int var2 = MathHelper.floor_double(p_72838_1_.posX / 16.0D);
			int var3 = MathHelper.floor_double(p_72838_1_.posZ / 16.0D);
			bool var4 = p_72838_1_.forceSpawn;

			if (p_72838_1_ is EntityPlayer)
			{
				var4 = true;
			}

			if (!var4 && !this.chunkExists(var2, var3))
			{
				return false;
			}
			else
			{
				if (p_72838_1_ is EntityPlayer)
				{
					EntityPlayer var5 = (EntityPlayer)p_72838_1_;
					this.playerEntities.Add(var5);
					this.updateAllPlayersSleepingFlag();
				}

				this.getChunkFromChunkCoords(var2, var3).addEntity(p_72838_1_);
				this.loadedEntityList.Add(p_72838_1_);
				this.onEntityAdded(p_72838_1_);
				return true;
			}
		}

		protected internal virtual void onEntityAdded(Entity p_72923_1_)
		{
			for (int var2 = 0; var2 < this.worldAccesses.Count; ++var2)
			{
				((IWorldAccess)this.worldAccesses.get(var2)).onEntityCreate(p_72923_1_);
			}
		}

		protected internal virtual void onEntityRemoved(Entity p_72847_1_)
		{
			for (int var2 = 0; var2 < this.worldAccesses.Count; ++var2)
			{
				((IWorldAccess)this.worldAccesses.get(var2)).onEntityDestroy(p_72847_1_);
			}
		}

///    
///     <summary> * Schedule the entity for removal during the next tick. Marks the entity dead in anticipation. </summary>
///     
		public virtual void removeEntity(Entity p_72900_1_)
		{
			if (p_72900_1_.riddenByEntity != null)
			{
				p_72900_1_.riddenByEntity.mountEntity((Entity)null);
			}

			if (p_72900_1_.ridingEntity != null)
			{
				p_72900_1_.mountEntity((Entity)null);
			}

			p_72900_1_.setDead();

			if (p_72900_1_ is EntityPlayer)
			{
				this.playerEntities.Remove(p_72900_1_);
				this.updateAllPlayersSleepingFlag();
				this.onEntityRemoved(p_72900_1_);
			}
		}

///    
///     <summary> * Do NOT use this method to remove normal entities- use normal removeEntity </summary>
///     
		public virtual void removePlayerEntityDangerously(Entity p_72973_1_)
		{
			p_72973_1_.setDead();

			if (p_72973_1_ is EntityPlayer)
			{
				this.playerEntities.Remove(p_72973_1_);
				this.updateAllPlayersSleepingFlag();
			}

			int var2 = p_72973_1_.chunkCoordX;
			int var3 = p_72973_1_.chunkCoordZ;

			if (p_72973_1_.addedToChunk && this.chunkExists(var2, var3))
			{
				this.getChunkFromChunkCoords(var2, var3).removeEntity(p_72973_1_);
			}

			this.loadedEntityList.Remove(p_72973_1_);
			this.onEntityRemoved(p_72973_1_);
		}

///    
///     <summary> * Adds a IWorldAccess to the list of worldAccesses </summary>
///     
		public virtual void addWorldAccess(IWorldAccess p_72954_1_)
		{
			this.worldAccesses.Add(p_72954_1_);
		}

///    
///     <summary> * Removes a worldAccess from the worldAccesses object </summary>
///     
		public virtual void removeWorldAccess(IWorldAccess p_72848_1_)
		{
			this.worldAccesses.Remove(p_72848_1_);
		}

///    
///     <summary> * Returns a list of bounding boxes that collide with aabb excluding the passed in entity's collision. Args: entity,
///     * aabb </summary>
///     
		public virtual IList getCollidingBoundingBoxes(Entity p_72945_1_, AxisAlignedBB p_72945_2_)
		{
			this.collidingBoundingBoxes.Clear();
			int var3 = MathHelper.floor_double(p_72945_2_.minX);
			int var4 = MathHelper.floor_double(p_72945_2_.maxX + 1.0D);
			int var5 = MathHelper.floor_double(p_72945_2_.minY);
			int var6 = MathHelper.floor_double(p_72945_2_.maxY + 1.0D);
			int var7 = MathHelper.floor_double(p_72945_2_.minZ);
			int var8 = MathHelper.floor_double(p_72945_2_.maxZ + 1.0D);

			for (int var9 = var3; var9 < var4; ++var9)
			{
				for (int var10 = var7; var10 < var8; ++var10)
				{
					if (this.blockExists(var9, 64, var10))
					{
						for (int var11 = var5 - 1; var11 < var6; ++var11)
						{
							Block var12;

							if (var9 >= -30000000 && var9 < 30000000 && var10 >= -30000000 && var10 < 30000000)
							{
								var12 = this.getBlock(var9, var11, var10);
							}
							else
							{
								var12 = Blocks.stone;
							}

							var12.addCollisionBoxesToList(this, var9, var11, var10, p_72945_2_, this.collidingBoundingBoxes, p_72945_1_);
						}
					}
				}
			}

			double var14 = 0.25D;
			IList var15 = this.getEntitiesWithinAABBExcludingEntity(p_72945_1_, p_72945_2_.expand(var14, var14, var14));

			for (int var16 = 0; var16 < var15.Count; ++var16)
			{
				AxisAlignedBB var13 = ((Entity)var15[var16]).BoundingBox;

				if (var13 != null && var13.intersectsWith(p_72945_2_))
				{
					this.collidingBoundingBoxes.Add(var13);
				}

				var13 = p_72945_1_.getCollisionBox((Entity)var15[var16]);

				if (var13 != null && var13.intersectsWith(p_72945_2_))
				{
					this.collidingBoundingBoxes.Add(var13);
				}
			}

			return this.collidingBoundingBoxes;
		}

		public virtual IList func_147461_a(AxisAlignedBB p_147461_1_)
		{
			this.collidingBoundingBoxes.Clear();
			int var2 = MathHelper.floor_double(p_147461_1_.minX);
			int var3 = MathHelper.floor_double(p_147461_1_.maxX + 1.0D);
			int var4 = MathHelper.floor_double(p_147461_1_.minY);
			int var5 = MathHelper.floor_double(p_147461_1_.maxY + 1.0D);
			int var6 = MathHelper.floor_double(p_147461_1_.minZ);
			int var7 = MathHelper.floor_double(p_147461_1_.maxZ + 1.0D);

			for (int var8 = var2; var8 < var3; ++var8)
			{
				for (int var9 = var6; var9 < var7; ++var9)
				{
					if (this.blockExists(var8, 64, var9))
					{
						for (int var10 = var4 - 1; var10 < var5; ++var10)
						{
							Block var11;

							if (var8 >= -30000000 && var8 < 30000000 && var9 >= -30000000 && var9 < 30000000)
							{
								var11 = this.getBlock(var8, var10, var9);
							}
							else
							{
								var11 = Blocks.bedrock;
							}

							var11.addCollisionBoxesToList(this, var8, var10, var9, p_147461_1_, this.collidingBoundingBoxes, (Entity)null);
						}
					}
				}
			}

			return this.collidingBoundingBoxes;
		}

///    
///     <summary> * Returns the amount of skylight subtracted for the current time </summary>
///     
		public virtual int calculateSkylightSubtracted(float p_72967_1_)
		{
			float var2 = this.getCelestialAngle(p_72967_1_);
			float var3 = 1.0F - (MathHelper.cos(var2 * (float)Math.PI * 2.0F) * 2.0F + 0.5F);

			if (var3 < 0.0F)
			{
				var3 = 0.0F;
			}

			if (var3 > 1.0F)
			{
				var3 = 1.0F;
			}

			var3 = 1.0F - var3;
			var3 = (float)((double)var3 * (1.0D - (double)(this.getRainStrength(p_72967_1_) * 5.0F) / 16.0D));
			var3 = (float)((double)var3 * (1.0D - (double)(this.getWeightedThunderStrength(p_72967_1_) * 5.0F) / 16.0D));
			var3 = 1.0F - var3;
			return (int)(var3 * 11.0F);
		}

///    
///     <summary> * Returns the sun brightness - checks time of day, rain and thunder </summary>
///     
		public virtual float getSunBrightness(float p_72971_1_)
		{
			float var2 = this.getCelestialAngle(p_72971_1_);
			float var3 = 1.0F - (MathHelper.cos(var2 * (float)Math.PI * 2.0F) * 2.0F + 0.2F);

			if (var3 < 0.0F)
			{
				var3 = 0.0F;
			}

			if (var3 > 1.0F)
			{
				var3 = 1.0F;
			}

			var3 = 1.0F - var3;
			var3 = (float)((double)var3 * (1.0D - (double)(this.getRainStrength(p_72971_1_) * 5.0F) / 16.0D));
			var3 = (float)((double)var3 * (1.0D - (double)(this.getWeightedThunderStrength(p_72971_1_) * 5.0F) / 16.0D));
			return var3 * 0.8F + 0.2F;
		}

///    
///     <summary> * Calculates the color for the skybox </summary>
///     
		public virtual Vec3 getSkyColor(Entity p_72833_1_, float p_72833_2_)
		{
			float var3 = this.getCelestialAngle(p_72833_2_);
			float var4 = MathHelper.cos(var3 * (float)Math.PI * 2.0F) * 2.0F + 0.5F;

			if (var4 < 0.0F)
			{
				var4 = 0.0F;
			}

			if (var4 > 1.0F)
			{
				var4 = 1.0F;
			}

			int var5 = MathHelper.floor_double(p_72833_1_.posX);
			int var6 = MathHelper.floor_double(p_72833_1_.posY);
			int var7 = MathHelper.floor_double(p_72833_1_.posZ);
			BiomeGenBase var8 = this.getBiomeGenForCoords(var5, var7);
			float var9 = var8.getFloatTemperature(var5, var6, var7);
			int var10 = var8.getSkyColorByTemp(var9);
			float var11 = (float)(var10 >> 16 & 255) / 255.0F;
			float var12 = (float)(var10 >> 8 & 255) / 255.0F;
			float var13 = (float)(var10 & 255) / 255.0F;
			var11 *= var4;
			var12 *= var4;
			var13 *= var4;
			float var14 = this.getRainStrength(p_72833_2_);
			float var15;
			float var16;

			if (var14 > 0.0F)
			{
				var15 = (var11 * 0.3F + var12 * 0.59F + var13 * 0.11F) * 0.6F;
				var16 = 1.0F - var14 * 0.75F;
				var11 = var11 * var16 + var15 * (1.0F - var16);
				var12 = var12 * var16 + var15 * (1.0F - var16);
				var13 = var13 * var16 + var15 * (1.0F - var16);
			}

			var15 = this.getWeightedThunderStrength(p_72833_2_);

			if (var15 > 0.0F)
			{
				var16 = (var11 * 0.3F + var12 * 0.59F + var13 * 0.11F) * 0.2F;
				float var17 = 1.0F - var15 * 0.75F;
				var11 = var11 * var17 + var16 * (1.0F - var17);
				var12 = var12 * var17 + var16 * (1.0F - var17);
				var13 = var13 * var17 + var16 * (1.0F - var17);
			}

			if (this.lastLightningBolt > 0)
			{
				var16 = (float)this.lastLightningBolt - p_72833_2_;

				if (var16 > 1.0F)
				{
					var16 = 1.0F;
				}

				var16 *= 0.45F;
				var11 = var11 * (1.0F - var16) + 0.8F * var16;
				var12 = var12 * (1.0F - var16) + 0.8F * var16;
				var13 = var13 * (1.0F - var16) + 1.0F * var16;
			}

			return Vec3.createVectorHelper((double)var11, (double)var12, (double)var13);
		}

///    
///     <summary> * calls calculateCelestialAngle </summary>
///     
		public virtual float getCelestialAngle(float p_72826_1_)
		{
			return this.provider.calculateCelestialAngle(this.worldInfo.WorldTime, p_72826_1_);
		}

		public virtual int MoonPhase
		{
			get
			{
				return this.provider.getMoonPhase(this.worldInfo.WorldTime);
			}
		}

///    
///     <summary> * gets the current fullness of the moon expressed as a float between 1.0 and 0.0, in steps of .25 </summary>
///     
		public virtual float CurrentMoonPhaseFactor
		{
			get
			{
				return WorldProvider.moonPhaseFactors[this.provider.getMoonPhase(this.worldInfo.WorldTime)];
			}
		}

///    
///     <summary> * Return getCelestialAngle()*2*PI </summary>
///     
		public virtual float getCelestialAngleRadians(float p_72929_1_)
		{
			float var2 = this.getCelestialAngle(p_72929_1_);
			return var2 * (float)Math.PI * 2.0F;
		}

		public virtual Vec3 getCloudColour(float p_72824_1_)
		{
			float var2 = this.getCelestialAngle(p_72824_1_);
			float var3 = MathHelper.cos(var2 * (float)Math.PI * 2.0F) * 2.0F + 0.5F;

			if (var3 < 0.0F)
			{
				var3 = 0.0F;
			}

			if (var3 > 1.0F)
			{
				var3 = 1.0F;
			}

			float var4 = (float)(this.cloudColour >> 16 & 255L) / 255.0F;
			float var5 = (float)(this.cloudColour >> 8 & 255L) / 255.0F;
			float var6 = (float)(this.cloudColour & 255L) / 255.0F;
			float var7 = this.getRainStrength(p_72824_1_);
			float var8;
			float var9;

			if (var7 > 0.0F)
			{
				var8 = (var4 * 0.3F + var5 * 0.59F + var6 * 0.11F) * 0.6F;
				var9 = 1.0F - var7 * 0.95F;
				var4 = var4 * var9 + var8 * (1.0F - var9);
				var5 = var5 * var9 + var8 * (1.0F - var9);
				var6 = var6 * var9 + var8 * (1.0F - var9);
			}

			var4 *= var3 * 0.9F + 0.1F;
			var5 *= var3 * 0.9F + 0.1F;
			var6 *= var3 * 0.85F + 0.15F;
			var8 = this.getWeightedThunderStrength(p_72824_1_);

			if (var8 > 0.0F)
			{
				var9 = (var4 * 0.3F + var5 * 0.59F + var6 * 0.11F) * 0.2F;
				float var10 = 1.0F - var8 * 0.95F;
				var4 = var4 * var10 + var9 * (1.0F - var10);
				var5 = var5 * var10 + var9 * (1.0F - var10);
				var6 = var6 * var10 + var9 * (1.0F - var10);
			}

			return Vec3.createVectorHelper((double)var4, (double)var5, (double)var6);
		}

///    
///     <summary> * Returns vector(ish) with R/G/B for fog </summary>
///     
		public virtual Vec3 getFogColor(float p_72948_1_)
		{
			float var2 = this.getCelestialAngle(p_72948_1_);
			return this.provider.getFogColor(var2, p_72948_1_);
		}

///    
///     <summary> * Gets the height to which rain/snow will fall. Calculates it if not already stored. </summary>
///     
		public virtual int getPrecipitationHeight(int p_72874_1_, int p_72874_2_)
		{
			return this.getChunkFromBlockCoords(p_72874_1_, p_72874_2_).getPrecipitationHeight(p_72874_1_ & 15, p_72874_2_ & 15);
		}

///    
///     <summary> * Finds the highest block on the x, z coordinate that is solid and returns its y coord. Args x, z </summary>
///     
		public virtual int getTopSolidOrLiquidBlock(int p_72825_1_, int p_72825_2_)
		{
			Chunk var3 = this.getChunkFromBlockCoords(p_72825_1_, p_72825_2_);
			int var4 = var3.TopFilledSegment + 15;
			p_72825_1_ &= 15;

			for (p_72825_2_ &= 15; var4 > 0; --var4)
			{
				Block var5 = var3.func_150810_a(p_72825_1_, var4, p_72825_2_);

				if (var5.Material.blocksMovement() && var5.Material != Material.leaves)
				{
					return var4 + 1;
				}
			}

			return -1;
		}

///    
///     <summary> * How bright are stars in the sky </summary>
///     
		public virtual float getStarBrightness(float p_72880_1_)
		{
			float var2 = this.getCelestialAngle(p_72880_1_);
			float var3 = 1.0F - (MathHelper.cos(var2 * (float)Math.PI * 2.0F) * 2.0F + 0.25F);

			if (var3 < 0.0F)
			{
				var3 = 0.0F;
			}

			if (var3 > 1.0F)
			{
				var3 = 1.0F;
			}

			return var3 * var3 * 0.5F;
		}

///    
///     <summary> * Schedules a tick to a block with a delay (Most commonly the tick rate) </summary>
///     
		public virtual void scheduleBlockUpdate(int p_147464_1_, int p_147464_2_, int p_147464_3_, Block p_147464_4_, int p_147464_5_)
		{
		}

		public virtual void func_147454_a(int p_147454_1_, int p_147454_2_, int p_147454_3_, Block p_147454_4_, int p_147454_5_, int p_147454_6_)
		{
		}

		public virtual void func_147446_b(int p_147446_1_, int p_147446_2_, int p_147446_3_, Block p_147446_4_, int p_147446_5_, int p_147446_6_)
		{
		}

///    
///     <summary> * Updates (and cleans up) entities and tile entities </summary>
///     
		public virtual void updateEntities()
		{
			this.theProfiler.startSection("entities");
			this.theProfiler.startSection("global");
			int var1;
			Entity var2;
			CrashReport var4;
			CrashReportCategory var5;

			for (var1 = 0; var1 < this.weatherEffects.Count; ++var1)
			{
				var2 = (Entity)this.weatherEffects.get(var1);

				try
				{
					++var2.ticksExisted;
					var2.onUpdate();
				}
				catch (Exception var8)
				{
					var4 = CrashReport.makeCrashReport(var8, "Ticking entity");
					var5 = var4.makeCategory("Entity being ticked");

					if (var2 == null)
					{
						var5.addCrashSection("Entity", "~~NULL~~");
					}
					else
					{
						var2.addEntityCrashInfo(var5);
					}

					throw new ReportedException(var4);
				}

				if (var2.isDead)
				{
					this.weatherEffects.Remove(var1--);
				}
			}

			this.theProfiler.endStartSection("remove");
//JAVA TO VB & C# CONVERTER TODO TASK: There is no .NET equivalent to the java.util.Collection 'removeAll' method:
			this.loadedEntityList.removeAll(this.unloadedEntityList);
			int var3;
			int var14;

			for (var1 = 0; var1 < this.unloadedEntityList.Count; ++var1)
			{
				var2 = (Entity)this.unloadedEntityList.get(var1);
				var3 = var2.chunkCoordX;
				var14 = var2.chunkCoordZ;

				if (var2.addedToChunk && this.chunkExists(var3, var14))
				{
					this.getChunkFromChunkCoords(var3, var14).removeEntity(var2);
				}
			}

			for (var1 = 0; var1 < this.unloadedEntityList.Count; ++var1)
			{
				this.onEntityRemoved((Entity)this.unloadedEntityList.get(var1));
			}

			this.unloadedEntityList.Clear();
			this.theProfiler.endStartSection("regular");

			for (var1 = 0; var1 < this.loadedEntityList.Count; ++var1)
			{
				var2 = (Entity)this.loadedEntityList.get(var1);

				if (var2.ridingEntity != null)
				{
					if (!var2.ridingEntity.isDead && var2.ridingEntity.riddenByEntity == var2)
					{
						continue;
					}

					var2.ridingEntity.riddenByEntity = null;
					var2.ridingEntity = null;
				}

				this.theProfiler.startSection("tick");

				if (!var2.isDead)
				{
					try
					{
						this.updateEntity(var2);
					}
					catch (Exception var7)
					{
						var4 = CrashReport.makeCrashReport(var7, "Ticking entity");
						var5 = var4.makeCategory("Entity being ticked");
						var2.addEntityCrashInfo(var5);
						throw new ReportedException(var4);
					}
				}

				this.theProfiler.endSection();
				this.theProfiler.startSection("remove");

				if (var2.isDead)
				{
					var3 = var2.chunkCoordX;
					var14 = var2.chunkCoordZ;

					if (var2.addedToChunk && this.chunkExists(var3, var14))
					{
						this.getChunkFromChunkCoords(var3, var14).removeEntity(var2);
					}

					this.loadedEntityList.Remove(var1--);
					this.onEntityRemoved(var2);
				}

				this.theProfiler.endSection();
			}

			this.theProfiler.endStartSection("blockEntities");
			this.field_147481_N = true;
			IEnumerator var9 = this.field_147482_g.GetEnumerator();

			while (var9.MoveNext())
			{
				TileEntity var10 = (TileEntity)var9.Current;

				if (!var10.Invalid && var10.hasWorldObj() && this.blockExists(var10.field_145851_c, var10.field_145848_d, var10.field_145849_e))
				{
					try
					{
						var10.updateEntity();
					}
					catch (Exception var6)
					{
						var4 = CrashReport.makeCrashReport(var6, "Ticking block entity");
						var5 = var4.makeCategory("Block entity being ticked");
						var10.func_145828_a(var5);
						throw new ReportedException(var4);
					}
				}

				if (var10.Invalid)
				{
					var9.remove();

					if (this.chunkExists(var10.field_145851_c >> 4, var10.field_145849_e >> 4))
					{
						Chunk var12 = this.getChunkFromChunkCoords(var10.field_145851_c >> 4, var10.field_145849_e >> 4);

						if (var12 != null)
						{
							var12.removeTileEntity(var10.field_145851_c & 15, var10.field_145848_d, var10.field_145849_e & 15);
						}
					}
				}
			}

			this.field_147481_N = false;

			if (!this.field_147483_b.Count == 0)
			{
//JAVA TO VB & C# CONVERTER TODO TASK: There is no .NET equivalent to the java.util.Collection 'removeAll' method:
				this.field_147482_g.removeAll(this.field_147483_b);
				this.field_147483_b.Clear();
			}

			this.theProfiler.endStartSection("pendingBlockEntities");

			if (!this.field_147484_a.Count == 0)
			{
				for (int var11 = 0; var11 < this.field_147484_a.Count; ++var11)
				{
					TileEntity var13 = (TileEntity)this.field_147484_a.get(var11);

					if (!var13.Invalid)
					{
						if (!this.field_147482_g.Contains(var13))
						{
							this.field_147482_g.Add(var13);
						}

						if (this.chunkExists(var13.field_145851_c >> 4, var13.field_145849_e >> 4))
						{
							Chunk var15 = this.getChunkFromChunkCoords(var13.field_145851_c >> 4, var13.field_145849_e >> 4);

							if (var15 != null)
							{
								var15.func_150812_a(var13.field_145851_c & 15, var13.field_145848_d, var13.field_145849_e & 15, var13);
							}
						}

						this.func_147471_g(var13.field_145851_c, var13.field_145848_d, var13.field_145849_e);
					}
				}

				this.field_147484_a.Clear();
			}

			this.theProfiler.endSection();
			this.theProfiler.endSection();
		}

		public virtual void func_147448_a(ICollection p_147448_1_)
		{
			if (this.field_147481_N)
			{
				this.field_147484_a.AddRange(p_147448_1_);
			}
			else
			{
				this.field_147482_g.AddRange(p_147448_1_);
			}
		}

///    
///     <summary> * Will update the entity in the world if the chunk the entity is in is currently loaded. Args: entity </summary>
///     
		public virtual void updateEntity(Entity p_72870_1_)
		{
			this.updateEntityWithOptionalForce(p_72870_1_, true);
		}

///    
///     <summary> * Will update the entity in the world if the chunk the entity is in is currently loaded or its forced to update.
///     * Args: entity, forceUpdate </summary>
///     
		public virtual void updateEntityWithOptionalForce(Entity p_72866_1_, bool p_72866_2_)
		{
			int var3 = MathHelper.floor_double(p_72866_1_.posX);
			int var4 = MathHelper.floor_double(p_72866_1_.posZ);
			sbyte var5 = 32;

			if (!p_72866_2_ || this.checkChunksExist(var3 - var5, 0, var4 - var5, var3 + var5, 0, var4 + var5))
			{
				p_72866_1_.lastTickPosX = p_72866_1_.posX;
				p_72866_1_.lastTickPosY = p_72866_1_.posY;
				p_72866_1_.lastTickPosZ = p_72866_1_.posZ;
				p_72866_1_.prevRotationYaw = p_72866_1_.rotationYaw;
				p_72866_1_.prevRotationPitch = p_72866_1_.rotationPitch;

				if (p_72866_2_ && p_72866_1_.addedToChunk)
				{
					++p_72866_1_.ticksExisted;

					if (p_72866_1_.ridingEntity != null)
					{
						p_72866_1_.updateRidden();
					}
					else
					{
						p_72866_1_.onUpdate();
					}
				}

				this.theProfiler.startSection("chunkCheck");

				if (double.isNaN(p_72866_1_.posX) || double.IsInfinity(p_72866_1_.posX))
				{
					p_72866_1_.posX = p_72866_1_.lastTickPosX;
				}

				if (double.isNaN(p_72866_1_.posY) || double.IsInfinity(p_72866_1_.posY))
				{
					p_72866_1_.posY = p_72866_1_.lastTickPosY;
				}

				if (double.isNaN(p_72866_1_.posZ) || double.IsInfinity(p_72866_1_.posZ))
				{
					p_72866_1_.posZ = p_72866_1_.lastTickPosZ;
				}

				if (double.isNaN((double)p_72866_1_.rotationPitch) || double.IsInfinity((double)p_72866_1_.rotationPitch))
				{
					p_72866_1_.rotationPitch = p_72866_1_.prevRotationPitch;
				}

				if (double.isNaN((double)p_72866_1_.rotationYaw) || double.IsInfinity((double)p_72866_1_.rotationYaw))
				{
					p_72866_1_.rotationYaw = p_72866_1_.prevRotationYaw;
				}

				int var6 = MathHelper.floor_double(p_72866_1_.posX / 16.0D);
				int var7 = MathHelper.floor_double(p_72866_1_.posY / 16.0D);
				int var8 = MathHelper.floor_double(p_72866_1_.posZ / 16.0D);

				if (!p_72866_1_.addedToChunk || p_72866_1_.chunkCoordX != var6 || p_72866_1_.chunkCoordY != var7 || p_72866_1_.chunkCoordZ != var8)
				{
					if (p_72866_1_.addedToChunk && this.chunkExists(p_72866_1_.chunkCoordX, p_72866_1_.chunkCoordZ))
					{
						this.getChunkFromChunkCoords(p_72866_1_.chunkCoordX, p_72866_1_.chunkCoordZ).removeEntityAtIndex(p_72866_1_, p_72866_1_.chunkCoordY);
					}

					if (this.chunkExists(var6, var8))
					{
						p_72866_1_.addedToChunk = true;
						this.getChunkFromChunkCoords(var6, var8).addEntity(p_72866_1_);
					}
					else
					{
						p_72866_1_.addedToChunk = false;
					}
				}

				this.theProfiler.endSection();

				if (p_72866_2_ && p_72866_1_.addedToChunk && p_72866_1_.riddenByEntity != null)
				{
					if (!p_72866_1_.riddenByEntity.isDead && p_72866_1_.riddenByEntity.ridingEntity == p_72866_1_)
					{
						this.updateEntity(p_72866_1_.riddenByEntity);
					}
					else
					{
						p_72866_1_.riddenByEntity.ridingEntity = null;
						p_72866_1_.riddenByEntity = null;
					}
				}
			}
		}

///    
///     <summary> * Returns true if there are no solid, live entities in the specified AxisAlignedBB </summary>
///     
		public virtual bool checkNoEntityCollision(AxisAlignedBB p_72855_1_)
		{
			return this.checkNoEntityCollision(p_72855_1_, (Entity)null);
		}

///    
///     <summary> * Returns true if there are no solid, live entities in the specified AxisAlignedBB, excluding the given entity </summary>
///     
		public virtual bool checkNoEntityCollision(AxisAlignedBB p_72917_1_, Entity p_72917_2_)
		{
			IList var3 = this.getEntitiesWithinAABBExcludingEntity((Entity)null, p_72917_1_);

			for (int var4 = 0; var4 < var3.Count; ++var4)
			{
				Entity var5 = (Entity)var3[var4];

				if (!var5.isDead && var5.preventEntitySpawning && var5 != p_72917_2_)
				{
					return false;
				}
			}

			return true;
		}

///    
///     <summary> * Returns true if there are any blocks in the region constrained by an AxisAlignedBB </summary>
///     
		public virtual bool checkBlockCollision(AxisAlignedBB p_72829_1_)
		{
			int var2 = MathHelper.floor_double(p_72829_1_.minX);
			int var3 = MathHelper.floor_double(p_72829_1_.maxX + 1.0D);
			int var4 = MathHelper.floor_double(p_72829_1_.minY);
			int var5 = MathHelper.floor_double(p_72829_1_.maxY + 1.0D);
			int var6 = MathHelper.floor_double(p_72829_1_.minZ);
			int var7 = MathHelper.floor_double(p_72829_1_.maxZ + 1.0D);

			if (p_72829_1_.minX < 0.0D)
			{
				--var2;
			}

			if (p_72829_1_.minY < 0.0D)
			{
				--var4;
			}

			if (p_72829_1_.minZ < 0.0D)
			{
				--var6;
			}

			for (int var8 = var2; var8 < var3; ++var8)
			{
				for (int var9 = var4; var9 < var5; ++var9)
				{
					for (int var10 = var6; var10 < var7; ++var10)
					{
						Block var11 = this.getBlock(var8, var9, var10);

						if (var11.Material != Material.air)
						{
							return true;
						}
					}
				}
			}

			return false;
		}

///    
///     <summary> * Returns if any of the blocks within the aabb are liquids. Args: aabb </summary>
///     
		public virtual bool isAnyLiquid(AxisAlignedBB p_72953_1_)
		{
			int var2 = MathHelper.floor_double(p_72953_1_.minX);
			int var3 = MathHelper.floor_double(p_72953_1_.maxX + 1.0D);
			int var4 = MathHelper.floor_double(p_72953_1_.minY);
			int var5 = MathHelper.floor_double(p_72953_1_.maxY + 1.0D);
			int var6 = MathHelper.floor_double(p_72953_1_.minZ);
			int var7 = MathHelper.floor_double(p_72953_1_.maxZ + 1.0D);

			if (p_72953_1_.minX < 0.0D)
			{
				--var2;
			}

			if (p_72953_1_.minY < 0.0D)
			{
				--var4;
			}

			if (p_72953_1_.minZ < 0.0D)
			{
				--var6;
			}

			for (int var8 = var2; var8 < var3; ++var8)
			{
				for (int var9 = var4; var9 < var5; ++var9)
				{
					for (int var10 = var6; var10 < var7; ++var10)
					{
						Block var11 = this.getBlock(var8, var9, var10);

						if (var11.Material.Liquid)
						{
							return true;
						}
					}
				}
			}

			return false;
		}

		public virtual bool func_147470_e(AxisAlignedBB p_147470_1_)
		{
			int var2 = MathHelper.floor_double(p_147470_1_.minX);
			int var3 = MathHelper.floor_double(p_147470_1_.maxX + 1.0D);
			int var4 = MathHelper.floor_double(p_147470_1_.minY);
			int var5 = MathHelper.floor_double(p_147470_1_.maxY + 1.0D);
			int var6 = MathHelper.floor_double(p_147470_1_.minZ);
			int var7 = MathHelper.floor_double(p_147470_1_.maxZ + 1.0D);

			if (this.checkChunksExist(var2, var4, var6, var3, var5, var7))
			{
				for (int var8 = var2; var8 < var3; ++var8)
				{
					for (int var9 = var4; var9 < var5; ++var9)
					{
						for (int var10 = var6; var10 < var7; ++var10)
						{
							Block var11 = this.getBlock(var8, var9, var10);

							if (var11 == Blocks.fire || var11 == Blocks.flowing_lava || var11 == Blocks.lava)
							{
								return true;
							}
						}
					}
				}
			}

			return false;
		}

///    
///     <summary> * handles the acceleration of an object whilst in water. Not sure if it is used elsewhere. </summary>
///     
		public virtual bool handleMaterialAcceleration(AxisAlignedBB p_72918_1_, Material p_72918_2_, Entity p_72918_3_)
		{
			int var4 = MathHelper.floor_double(p_72918_1_.minX);
			int var5 = MathHelper.floor_double(p_72918_1_.maxX + 1.0D);
			int var6 = MathHelper.floor_double(p_72918_1_.minY);
			int var7 = MathHelper.floor_double(p_72918_1_.maxY + 1.0D);
			int var8 = MathHelper.floor_double(p_72918_1_.minZ);
			int var9 = MathHelper.floor_double(p_72918_1_.maxZ + 1.0D);

			if (!this.checkChunksExist(var4, var6, var8, var5, var7, var9))
			{
				return false;
			}
			else
			{
				bool var10 = false;
				Vec3 var11 = Vec3.createVectorHelper(0.0D, 0.0D, 0.0D);

				for (int var12 = var4; var12 < var5; ++var12)
				{
					for (int var13 = var6; var13 < var7; ++var13)
					{
						for (int var14 = var8; var14 < var9; ++var14)
						{
							Block var15 = this.getBlock(var12, var13, var14);

							if (var15.Material == p_72918_2_)
							{
								double var16 = (double)((float)(var13 + 1) - BlockLiquid.func_149801_b(this.getBlockMetadata(var12, var13, var14)));

								if ((double)var7 >= var16)
								{
									var10 = true;
									var15.velocityToAddToEntity(this, var12, var13, var14, p_72918_3_, var11);
								}
							}
						}
					}
				}

				if (var11.lengthVector() > 0.0D && p_72918_3_.PushedByWater)
				{
					var11 = var11.normalize();
					double var18 = 0.014D;
					p_72918_3_.motionX += var11.xCoord * var18;
					p_72918_3_.motionY += var11.yCoord * var18;
					p_72918_3_.motionZ += var11.zCoord * var18;
				}

				return var10;
			}
		}

///    
///     <summary> * Returns true if the given bounding box contains the given material </summary>
///     
		public virtual bool isMaterialInBB(AxisAlignedBB p_72875_1_, Material p_72875_2_)
		{
			int var3 = MathHelper.floor_double(p_72875_1_.minX);
			int var4 = MathHelper.floor_double(p_72875_1_.maxX + 1.0D);
			int var5 = MathHelper.floor_double(p_72875_1_.minY);
			int var6 = MathHelper.floor_double(p_72875_1_.maxY + 1.0D);
			int var7 = MathHelper.floor_double(p_72875_1_.minZ);
			int var8 = MathHelper.floor_double(p_72875_1_.maxZ + 1.0D);

			for (int var9 = var3; var9 < var4; ++var9)
			{
				for (int var10 = var5; var10 < var6; ++var10)
				{
					for (int var11 = var7; var11 < var8; ++var11)
					{
						if (this.getBlock(var9, var10, var11).Material == p_72875_2_)
						{
							return true;
						}
					}
				}
			}

			return false;
		}

///    
///     <summary> * checks if the given AABB is in the material given. Used while swimming. </summary>
///     
		public virtual bool isAABBInMaterial(AxisAlignedBB p_72830_1_, Material p_72830_2_)
		{
			int var3 = MathHelper.floor_double(p_72830_1_.minX);
			int var4 = MathHelper.floor_double(p_72830_1_.maxX + 1.0D);
			int var5 = MathHelper.floor_double(p_72830_1_.minY);
			int var6 = MathHelper.floor_double(p_72830_1_.maxY + 1.0D);
			int var7 = MathHelper.floor_double(p_72830_1_.minZ);
			int var8 = MathHelper.floor_double(p_72830_1_.maxZ + 1.0D);

			for (int var9 = var3; var9 < var4; ++var9)
			{
				for (int var10 = var5; var10 < var6; ++var10)
				{
					for (int var11 = var7; var11 < var8; ++var11)
					{
						Block var12 = this.getBlock(var9, var10, var11);

						if (var12.Material == p_72830_2_)
						{
							int var13 = this.getBlockMetadata(var9, var10, var11);
							double var14 = (double)(var10 + 1);

							if (var13 < 8)
							{
								var14 = (double)(var10 + 1) - (double)var13 / 8.0D;
							}

							if (var14 >= p_72830_1_.minY)
							{
								return true;
							}
						}
					}
				}
			}

			return false;
		}

///    
///     <summary> * Creates an explosion. Args: entity, x, y, z, strength </summary>
///     
		public virtual Explosion createExplosion(Entity p_72876_1_, double p_72876_2_, double p_72876_4_, double p_72876_6_, float p_72876_8_, bool p_72876_9_)
		{
			return this.newExplosion(p_72876_1_, p_72876_2_, p_72876_4_, p_72876_6_, p_72876_8_, false, p_72876_9_);
		}

///    
///     <summary> * returns a new explosion. Does initiation (at time of writing Explosion is not finished) </summary>
///     
		public virtual Explosion newExplosion(Entity p_72885_1_, double p_72885_2_, double p_72885_4_, double p_72885_6_, float p_72885_8_, bool p_72885_9_, bool p_72885_10_)
		{
			Explosion var11 = new Explosion(this, p_72885_1_, p_72885_2_, p_72885_4_, p_72885_6_, p_72885_8_);
			var11.isFlaming = p_72885_9_;
			var11.isSmoking = p_72885_10_;
			var11.doExplosionA();
			var11.doExplosionB(true);
			return var11;
		}

///    
///     <summary> * Gets the percentage of real blocks within within a bounding box, along a specified vector. </summary>
///     
		public virtual float getBlockDensity(Vec3 p_72842_1_, AxisAlignedBB p_72842_2_)
		{
			double var3 = 1.0D / ((p_72842_2_.maxX - p_72842_2_.minX) * 2.0D + 1.0D);
			double var5 = 1.0D / ((p_72842_2_.maxY - p_72842_2_.minY) * 2.0D + 1.0D);
			double var7 = 1.0D / ((p_72842_2_.maxZ - p_72842_2_.minZ) * 2.0D + 1.0D);

			if (var3 >= 0.0D && var5 >= 0.0D && var7 >= 0.0D)
			{
				int var9 = 0;
				int var10 = 0;

				for (float var11 = 0.0F; var11 <= 1.0F; var11 = (float)((double)var11 + var3))
				{
					for (float var12 = 0.0F; var12 <= 1.0F; var12 = (float)((double)var12 + var5))
					{
						for (float var13 = 0.0F; var13 <= 1.0F; var13 = (float)((double)var13 + var7))
						{
							double var14 = p_72842_2_.minX + (p_72842_2_.maxX - p_72842_2_.minX) * (double)var11;
							double var16 = p_72842_2_.minY + (p_72842_2_.maxY - p_72842_2_.minY) * (double)var12;
							double var18 = p_72842_2_.minZ + (p_72842_2_.maxZ - p_72842_2_.minZ) * (double)var13;

							if (this.rayTraceBlocks(Vec3.createVectorHelper(var14, var16, var18), p_72842_1_) == null)
							{
								++var9;
							}

							++var10;
						}
					}
				}

				return (float)var9 / (float)var10;
			}
			else
			{
				return 0.0F;
			}
		}

///    
///     <summary> * If the block in the given direction of the given coordinate is fire, extinguish it. Args: Player, X,Y,Z,
///     * blockDirection </summary>
///     
		public virtual bool extinguishFire(EntityPlayer p_72886_1_, int p_72886_2_, int p_72886_3_, int p_72886_4_, int p_72886_5_)
		{
			if (p_72886_5_ == 0)
			{
				--p_72886_3_;
			}

			if (p_72886_5_ == 1)
			{
				++p_72886_3_;
			}

			if (p_72886_5_ == 2)
			{
				--p_72886_4_;
			}

			if (p_72886_5_ == 3)
			{
				++p_72886_4_;
			}

			if (p_72886_5_ == 4)
			{
				--p_72886_2_;
			}

			if (p_72886_5_ == 5)
			{
				++p_72886_2_;
			}

			if (this.getBlock(p_72886_2_, p_72886_3_, p_72886_4_) == Blocks.fire)
			{
				this.playAuxSFXAtEntity(p_72886_1_, 1004, p_72886_2_, p_72886_3_, p_72886_4_, 0);
				this.setBlockToAir(p_72886_2_, p_72886_3_, p_72886_4_);
				return true;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * This string is 'All: (number of loaded entities)' Viewable by press ing F3 </summary>
///     
		public virtual string DebugLoadedEntities
		{
			get
			{
				return "All: " + this.loadedEntityList.Count;
			}
		}

///    
///     <summary> * Returns the name of the current chunk provider, by calling chunkprovider.makeString() </summary>
///     
		public virtual string ProviderName
		{
			get
			{
				return this.chunkProvider.makeString();
			}
		}

		public virtual TileEntity getTileEntity(int p_147438_1_, int p_147438_2_, int p_147438_3_)
		{
			if (p_147438_2_ >= 0 && p_147438_2_ < 256)
			{
				TileEntity var4 = null;
				int var5;
				TileEntity var6;

				if (this.field_147481_N)
				{
					for (var5 = 0; var5 < this.field_147484_a.Count; ++var5)
					{
						var6 = (TileEntity)this.field_147484_a.get(var5);

						if (!var6.Invalid && var6.field_145851_c == p_147438_1_ && var6.field_145848_d == p_147438_2_ && var6.field_145849_e == p_147438_3_)
						{
							var4 = var6;
							break;
						}
					}
				}

				if (var4 == null)
				{
					Chunk var7 = this.getChunkFromChunkCoords(p_147438_1_ >> 4, p_147438_3_ >> 4);

					if (var7 != null)
					{
						var4 = var7.func_150806_e(p_147438_1_ & 15, p_147438_2_, p_147438_3_ & 15);
					}
				}

				if (var4 == null)
				{
					for (var5 = 0; var5 < this.field_147484_a.Count; ++var5)
					{
						var6 = (TileEntity)this.field_147484_a.get(var5);

						if (!var6.Invalid && var6.field_145851_c == p_147438_1_ && var6.field_145848_d == p_147438_2_ && var6.field_145849_e == p_147438_3_)
						{
							var4 = var6;
							break;
						}
					}
				}

				return var4;
			}
			else
			{
				return null;
			}
		}

		public virtual void setTileEntity(int p_147455_1_, int p_147455_2_, int p_147455_3_, TileEntity p_147455_4_)
		{
			if (p_147455_4_ != null && !p_147455_4_.Invalid)
			{
				if (this.field_147481_N)
				{
					p_147455_4_.field_145851_c = p_147455_1_;
					p_147455_4_.field_145848_d = p_147455_2_;
					p_147455_4_.field_145849_e = p_147455_3_;
					IEnumerator var5 = this.field_147484_a.GetEnumerator();

					while (var5.MoveNext())
					{
						TileEntity var6 = (TileEntity)var5.Current;

						if (var6.field_145851_c == p_147455_1_ && var6.field_145848_d == p_147455_2_ && var6.field_145849_e == p_147455_3_)
						{
							var6.invalidate();
							var5.remove();
						}
					}

					this.field_147484_a.Add(p_147455_4_);
				}
				else
				{
					this.field_147482_g.Add(p_147455_4_);
					Chunk var7 = this.getChunkFromChunkCoords(p_147455_1_ >> 4, p_147455_3_ >> 4);

					if (var7 != null)
					{
						var7.func_150812_a(p_147455_1_ & 15, p_147455_2_, p_147455_3_ & 15, p_147455_4_);
					}
				}
			}
		}

		public virtual void removeTileEntity(int p_147475_1_, int p_147475_2_, int p_147475_3_)
		{
			TileEntity var4 = this.getTileEntity(p_147475_1_, p_147475_2_, p_147475_3_);

			if (var4 != null && this.field_147481_N)
			{
				var4.invalidate();
				this.field_147484_a.Remove(var4);
			}
			else
			{
				if (var4 != null)
				{
					this.field_147484_a.Remove(var4);
					this.field_147482_g.Remove(var4);
				}

				Chunk var5 = this.getChunkFromChunkCoords(p_147475_1_ >> 4, p_147475_3_ >> 4);

				if (var5 != null)
				{
					var5.removeTileEntity(p_147475_1_ & 15, p_147475_2_, p_147475_3_ & 15);
				}
			}
		}

		public virtual void func_147457_a(TileEntity p_147457_1_)
		{
			this.field_147483_b.Add(p_147457_1_);
		}

		public virtual bool func_147469_q(int p_147469_1_, int p_147469_2_, int p_147469_3_)
		{
			AxisAlignedBB var4 = this.getBlock(p_147469_1_, p_147469_2_, p_147469_3_).getCollisionBoundingBoxFromPool(this, p_147469_1_, p_147469_2_, p_147469_3_);
			return var4 != null && var4.AverageEdgeLength >= 1.0D;
		}

///    
///     <summary> * Returns true if the block at the given coordinate has a solid (buildable) top surface. </summary>
///     
		public static bool doesBlockHaveSolidTopSurface(IBlockAccess p_147466_0_, int p_147466_1_, int p_147466_2_, int p_147466_3_)
		{
			Block var4 = p_147466_0_.getBlock(p_147466_1_, p_147466_2_, p_147466_3_);
			int var5 = p_147466_0_.getBlockMetadata(p_147466_1_, p_147466_2_, p_147466_3_);
			return var4.Material.Opaque && var4.renderAsNormalBlock() ? true : (var4 is BlockStairs ? (var5 & 4) == 4 : (var4 is BlockSlab ? (var5 & 8) == 8 : (var4 is BlockHopper ? true : (var4 is BlockSnow ? (var5 & 7) == 7 : false))));
		}

///    
///     <summary> * Checks if the block is a solid, normal cube. If the chunk does not exist, or is not loaded, it returns the
///     * boolean parameter </summary>
///     
		public virtual bool isBlockNormalCubeDefault(int p_147445_1_, int p_147445_2_, int p_147445_3_, bool p_147445_4_)
		{
			if (p_147445_1_ >= -30000000 && p_147445_3_ >= -30000000 && p_147445_1_ < 30000000 && p_147445_3_ < 30000000)
			{
				Chunk var5 = this.chunkProvider.provideChunk(p_147445_1_ >> 4, p_147445_3_ >> 4);

				if (var5 != null && !var5.Empty)
				{
					Block var6 = this.getBlock(p_147445_1_, p_147445_2_, p_147445_3_);
					return var6.Material.Opaque && var6.renderAsNormalBlock();
				}
				else
				{
					return p_147445_4_;
				}
			}
			else
			{
				return p_147445_4_;
			}
		}

///    
///     <summary> * Called on construction of the World class to setup the initial skylight values </summary>
///     
		public virtual void calculateInitialSkylight()
		{
			int var1 = this.calculateSkylightSubtracted(1.0F);

			if (var1 != this.skylightSubtracted)
			{
				this.skylightSubtracted = var1;
			}
		}

///    
///     <summary> * Set which types of mobs are allowed to spawn (peaceful vs hostile). </summary>
///     
		public virtual void setAllowedSpawnTypes(bool p_72891_1_, bool p_72891_2_)
		{
			this.spawnHostileMobs = p_72891_1_;
			this.spawnPeacefulMobs = p_72891_2_;
		}

///    
///     <summary> * Runs a single tick for the world </summary>
///     
		public virtual void tick()
		{
			this.updateWeather();
		}

///    
///     <summary> * Called from World constructor to set rainingStrength and thunderingStrength </summary>
///     
		private void calculateInitialWeather()
		{
			if (this.worldInfo.Raining)
			{
				this.rainingStrength = 1.0F;

				if (this.worldInfo.Thundering)
				{
					this.thunderingStrength = 1.0F;
				}
			}
		}

///    
///     <summary> * Updates all weather states. </summary>
///     
		protected internal virtual void updateWeather()
		{
			if (!this.provider.hasNoSky)
			{
				if (!this.isClient)
				{
					int var1 = this.worldInfo.ThunderTime;

					if (var1 <= 0)
					{
						if (this.worldInfo.Thundering)
						{
							this.worldInfo.ThunderTime = this.rand.Next(12000) + 3600;
						}
						else
						{
							this.worldInfo.ThunderTime = this.rand.Next(168000) + 12000;
						}
					}
					else
					{
						--var1;
						this.worldInfo.ThunderTime = var1;

						if (var1 <= 0)
						{
							this.worldInfo.Thundering = !this.worldInfo.Thundering;
						}
					}

					this.prevThunderingStrength = this.thunderingStrength;

					if (this.worldInfo.Thundering)
					{
						this.thunderingStrength = (float)((double)this.thunderingStrength + 0.01D);
					}
					else
					{
						this.thunderingStrength = (float)((double)this.thunderingStrength - 0.01D);
					}

					this.thunderingStrength = MathHelper.clamp_float(this.thunderingStrength, 0.0F, 1.0F);
					int var2 = this.worldInfo.RainTime;

					if (var2 <= 0)
					{
						if (this.worldInfo.Raining)
						{
							this.worldInfo.RainTime = this.rand.Next(12000) + 12000;
						}
						else
						{
							this.worldInfo.RainTime = this.rand.Next(168000) + 12000;
						}
					}
					else
					{
						--var2;
						this.worldInfo.RainTime = var2;

						if (var2 <= 0)
						{
							this.worldInfo.Raining = !this.worldInfo.Raining;
						}
					}

					this.prevRainingStrength = this.rainingStrength;

					if (this.worldInfo.Raining)
					{
						this.rainingStrength = (float)((double)this.rainingStrength + 0.01D);
					}
					else
					{
						this.rainingStrength = (float)((double)this.rainingStrength - 0.01D);
					}

					this.rainingStrength = MathHelper.clamp_float(this.rainingStrength, 0.0F, 1.0F);
				}
			}
		}

		protected internal virtual void setActivePlayerChunksAndCheckLight()
		{
			this.activeChunkSet.clear();
			this.theProfiler.startSection("buildList");
			int var1;
			EntityPlayer var2;
			int var3;
			int var4;
			int var5;

			for (var1 = 0; var1 < this.playerEntities.Count; ++var1)
			{
				var2 = (EntityPlayer)this.playerEntities.get(var1);
				var3 = MathHelper.floor_double(var2.posX / 16.0D);
				var4 = MathHelper.floor_double(var2.posZ / 16.0D);
				var5 = this.func_152379_p();

				for (int var6 = -var5; var6 <= var5; ++var6)
				{
					for (int var7 = -var5; var7 <= var5; ++var7)
					{
						this.activeChunkSet.add(new ChunkCoordIntPair(var6 + var3, var7 + var4));
					}
				}
			}

			this.theProfiler.endSection();

			if (this.ambientTickCountdown > 0)
			{
				--this.ambientTickCountdown;
			}

			this.theProfiler.startSection("playerCheckLight");

			if (!this.playerEntities.Count == 0)
			{
				var1 = this.rand.Next(this.playerEntities.Count);
				var2 = (EntityPlayer)this.playerEntities.get(var1);
				var3 = MathHelper.floor_double(var2.posX) + this.rand.Next(11) - 5;
				var4 = MathHelper.floor_double(var2.posY) + this.rand.Next(11) - 5;
				var5 = MathHelper.floor_double(var2.posZ) + this.rand.Next(11) - 5;
				this.func_147451_t(var3, var4, var5);
			}

			this.theProfiler.endSection();
		}

		protected internal abstract int func_152379_p();

		protected internal virtual void func_147467_a(int p_147467_1_, int p_147467_2_, Chunk p_147467_3_)
		{
			this.theProfiler.endStartSection("moodSound");

			if (this.ambientTickCountdown == 0 && !this.isClient)
			{
				this.updateLCG = this.updateLCG * 3 + 1013904223;
				int var4 = this.updateLCG >> 2;
				int var5 = var4 & 15;
				int var6 = var4 >> 8 & 15;
				int var7 = var4 >> 16 & 255;
				Block var8 = p_147467_3_.func_150810_a(var5, var7, var6);
				var5 += p_147467_1_;
				var6 += p_147467_2_;

				if (var8.Material == Material.air && this.getFullBlockLightValue(var5, var7, var6) <= this.rand.Next(8) && this.getSavedLightValue(EnumSkyBlock.Sky, var5, var7, var6) <= 0)
				{
					EntityPlayer var9 = this.getClosestPlayer((double)var5 + 0.5D, (double)var7 + 0.5D, (double)var6 + 0.5D, 8.0D);

					if (var9 != null && var9.getDistanceSq((double)var5 + 0.5D, (double)var7 + 0.5D, (double)var6 + 0.5D) > 4.0D)
					{
						this.playSoundEffect((double)var5 + 0.5D, (double)var7 + 0.5D, (double)var6 + 0.5D, "ambient.cave.cave", 0.7F, 0.8F + this.rand.nextFloat() * 0.2F);
						this.ambientTickCountdown = this.rand.Next(12000) + 6000;
					}
				}
			}

			this.theProfiler.endStartSection("checkLight");
			p_147467_3_.enqueueRelightChecks();
		}

		protected internal virtual void func_147456_g()
		{
			this.setActivePlayerChunksAndCheckLight();
		}

///    
///     <summary> * checks to see if a given block is both water and is cold enough to freeze </summary>
///     
		public virtual bool isBlockFreezable(int p_72884_1_, int p_72884_2_, int p_72884_3_)
		{
			return this.canBlockFreeze(p_72884_1_, p_72884_2_, p_72884_3_, false);
		}

///    
///     <summary> * checks to see if a given block is both water and has at least one immediately adjacent non-water block </summary>
///     
		public virtual bool isBlockFreezableNaturally(int p_72850_1_, int p_72850_2_, int p_72850_3_)
		{
			return this.canBlockFreeze(p_72850_1_, p_72850_2_, p_72850_3_, true);
		}

///    
///     <summary> * checks to see if a given block is both water, and cold enough to freeze - if the par4 boolean is set, this will
///     * only return true if there is a non-water block immediately adjacent to the specified block </summary>
///     
		public virtual bool canBlockFreeze(int p_72834_1_, int p_72834_2_, int p_72834_3_, bool p_72834_4_)
		{
			BiomeGenBase var5 = this.getBiomeGenForCoords(p_72834_1_, p_72834_3_);
			float var6 = var5.getFloatTemperature(p_72834_1_, p_72834_2_, p_72834_3_);

			if (var6 > 0.15F)
			{
				return false;
			}
			else
			{
				if (p_72834_2_ >= 0 && p_72834_2_ < 256 && this.getSavedLightValue(EnumSkyBlock.Block, p_72834_1_, p_72834_2_, p_72834_3_) < 10)
				{
					Block var7 = this.getBlock(p_72834_1_, p_72834_2_, p_72834_3_);

					if ((var7 == Blocks.water || var7 == Blocks.flowing_water) && this.getBlockMetadata(p_72834_1_, p_72834_2_, p_72834_3_) == 0)
					{
						if (!p_72834_4_)
						{
							return true;
						}

						bool var8 = true;

						if (var8 && this.getBlock(p_72834_1_ - 1, p_72834_2_, p_72834_3_).Material != Material.water)
						{
							var8 = false;
						}

						if (var8 && this.getBlock(p_72834_1_ + 1, p_72834_2_, p_72834_3_).Material != Material.water)
						{
							var8 = false;
						}

						if (var8 && this.getBlock(p_72834_1_, p_72834_2_, p_72834_3_ - 1).Material != Material.water)
						{
							var8 = false;
						}

						if (var8 && this.getBlock(p_72834_1_, p_72834_2_, p_72834_3_ + 1).Material != Material.water)
						{
							var8 = false;
						}

						if (!var8)
						{
							return true;
						}
					}
				}

				return false;
			}
		}

		public virtual bool func_147478_e(int p_147478_1_, int p_147478_2_, int p_147478_3_, bool p_147478_4_)
		{
			BiomeGenBase var5 = this.getBiomeGenForCoords(p_147478_1_, p_147478_3_);
			float var6 = var5.getFloatTemperature(p_147478_1_, p_147478_2_, p_147478_3_);

			if (var6 > 0.15F)
			{
				return false;
			}
			else if (!p_147478_4_)
			{
				return true;
			}
			else
			{
				if (p_147478_2_ >= 0 && p_147478_2_ < 256 && this.getSavedLightValue(EnumSkyBlock.Block, p_147478_1_, p_147478_2_, p_147478_3_) < 10)
				{
					Block var7 = this.getBlock(p_147478_1_, p_147478_2_, p_147478_3_);

					if (var7.Material == Material.air && Blocks.snow_layer.canPlaceBlockAt(this, p_147478_1_, p_147478_2_, p_147478_3_))
					{
						return true;
					}
				}

				return false;
			}
		}

		public virtual bool func_147451_t(int p_147451_1_, int p_147451_2_, int p_147451_3_)
		{
			bool var4 = false;

			if (!this.provider.hasNoSky)
			{
				var4 |= this.updateLightByType(EnumSkyBlock.Sky, p_147451_1_, p_147451_2_, p_147451_3_);
			}

			var4 |= this.updateLightByType(EnumSkyBlock.Block, p_147451_1_, p_147451_2_, p_147451_3_);
			return var4;
		}

		private int computeLightValue(int p_98179_1_, int p_98179_2_, int p_98179_3_, EnumSkyBlock p_98179_4_)
		{
			if (p_98179_4_ == EnumSkyBlock.Sky && this.canBlockSeeTheSky(p_98179_1_, p_98179_2_, p_98179_3_))
			{
				return 15;
			}
			else
			{
				Block var5 = this.getBlock(p_98179_1_, p_98179_2_, p_98179_3_);
				int var6 = p_98179_4_ == EnumSkyBlock.Sky ? 0 : var5.LightValue;
				int var7 = var5.LightOpacity;

				if (var7 >= 15 && var5.LightValue > 0)
				{
					var7 = 1;
				}

				if (var7 < 1)
				{
					var7 = 1;
				}

				if (var7 >= 15)
				{
					return 0;
				}
				else if (var6 >= 14)
				{
					return var6;
				}
				else
				{
					for (int var8 = 0; var8 < 6; ++var8)
					{
						int var9 = p_98179_1_ + Facing.offsetsXForSide[var8];
						int var10 = p_98179_2_ + Facing.offsetsYForSide[var8];
						int var11 = p_98179_3_ + Facing.offsetsZForSide[var8];
						int var12 = this.getSavedLightValue(p_98179_4_, var9, var10, var11) - var7;

						if (var12 > var6)
						{
							var6 = var12;
						}

						if (var6 >= 14)
						{
							return var6;
						}
					}

					return var6;
				}
			}
		}

		public virtual bool updateLightByType(EnumSkyBlock p_147463_1_, int p_147463_2_, int p_147463_3_, int p_147463_4_)
		{
			if (!this.doChunksNearChunkExist(p_147463_2_, p_147463_3_, p_147463_4_, 17))
			{
				return false;
			}
			else
			{
				int var5 = 0;
				int var6 = 0;
				this.theProfiler.startSection("getBrightness");
				int var7 = this.getSavedLightValue(p_147463_1_, p_147463_2_, p_147463_3_, p_147463_4_);
				int var8 = this.computeLightValue(p_147463_2_, p_147463_3_, p_147463_4_, p_147463_1_);
				int var9;
				int var10;
				int var11;
				int var12;
				int var13;
				int var14;
				int var15;
				int var16;
				int var17;

				if (var8 > var7)
				{
					this.lightUpdateBlockList[var6++] = 133152;
				}
				else if (var8 < var7)
				{
					this.lightUpdateBlockList[var6++] = 133152 | var7 << 18;

					while (var5 < var6)
					{
						var9 = this.lightUpdateBlockList[var5++];
						var10 = (var9 & 63) - 32 + p_147463_2_;
						var11 = (var9 >> 6 & 63) - 32 + p_147463_3_;
						var12 = (var9 >> 12 & 63) - 32 + p_147463_4_;
						var13 = var9 >> 18 & 15;
						var14 = this.getSavedLightValue(p_147463_1_, var10, var11, var12);

						if (var14 == var13)
						{
							this.setLightValue(p_147463_1_, var10, var11, var12, 0);

							if (var13 > 0)
							{
								var15 = MathHelper.abs_int(var10 - p_147463_2_);
								var16 = MathHelper.abs_int(var11 - p_147463_3_);
								var17 = MathHelper.abs_int(var12 - p_147463_4_);

								if (var15 + var16 + var17 < 17)
								{
									for (int var18 = 0; var18 < 6; ++var18)
									{
										int var19 = var10 + Facing.offsetsXForSide[var18];
										int var20 = var11 + Facing.offsetsYForSide[var18];
										int var21 = var12 + Facing.offsetsZForSide[var18];
										int var22 = Math.Max(1, this.getBlock(var19, var20, var21).LightOpacity);
										var14 = this.getSavedLightValue(p_147463_1_, var19, var20, var21);

										if (var14 == var13 - var22 && var6 < this.lightUpdateBlockList.Length)
										{
											this.lightUpdateBlockList[var6++] = var19 - p_147463_2_ + 32 | var20 - p_147463_3_ + 32 << 6 | var21 - p_147463_4_ + 32 << 12 | var13 - var22 << 18;
										}
									}
								}
							}
						}
					}

					var5 = 0;
				}

				this.theProfiler.endSection();
				this.theProfiler.startSection("checkedPosition < toCheckCount");

				while (var5 < var6)
				{
					var9 = this.lightUpdateBlockList[var5++];
					var10 = (var9 & 63) - 32 + p_147463_2_;
					var11 = (var9 >> 6 & 63) - 32 + p_147463_3_;
					var12 = (var9 >> 12 & 63) - 32 + p_147463_4_;
					var13 = this.getSavedLightValue(p_147463_1_, var10, var11, var12);
					var14 = this.computeLightValue(var10, var11, var12, p_147463_1_);

					if (var14 != var13)
					{
						this.setLightValue(p_147463_1_, var10, var11, var12, var14);

						if (var14 > var13)
						{
							var15 = Math.Abs(var10 - p_147463_2_);
							var16 = Math.Abs(var11 - p_147463_3_);
							var17 = Math.Abs(var12 - p_147463_4_);
							bool var23 = var6 < this.lightUpdateBlockList.Length - 6;

							if (var15 + var16 + var17 < 17 && var23)
							{
								if (this.getSavedLightValue(p_147463_1_, var10 - 1, var11, var12) < var14)
								{
									this.lightUpdateBlockList[var6++] = var10 - 1 - p_147463_2_ + 32 + (var11 - p_147463_3_ + 32 << 6) + (var12 - p_147463_4_ + 32 << 12);
								}

								if (this.getSavedLightValue(p_147463_1_, var10 + 1, var11, var12) < var14)
								{
									this.lightUpdateBlockList[var6++] = var10 + 1 - p_147463_2_ + 32 + (var11 - p_147463_3_ + 32 << 6) + (var12 - p_147463_4_ + 32 << 12);
								}

								if (this.getSavedLightValue(p_147463_1_, var10, var11 - 1, var12) < var14)
								{
									this.lightUpdateBlockList[var6++] = var10 - p_147463_2_ + 32 + (var11 - 1 - p_147463_3_ + 32 << 6) + (var12 - p_147463_4_ + 32 << 12);
								}

								if (this.getSavedLightValue(p_147463_1_, var10, var11 + 1, var12) < var14)
								{
									this.lightUpdateBlockList[var6++] = var10 - p_147463_2_ + 32 + (var11 + 1 - p_147463_3_ + 32 << 6) + (var12 - p_147463_4_ + 32 << 12);
								}

								if (this.getSavedLightValue(p_147463_1_, var10, var11, var12 - 1) < var14)
								{
									this.lightUpdateBlockList[var6++] = var10 - p_147463_2_ + 32 + (var11 - p_147463_3_ + 32 << 6) + (var12 - 1 - p_147463_4_ + 32 << 12);
								}

								if (this.getSavedLightValue(p_147463_1_, var10, var11, var12 + 1) < var14)
								{
									this.lightUpdateBlockList[var6++] = var10 - p_147463_2_ + 32 + (var11 - p_147463_3_ + 32 << 6) + (var12 + 1 - p_147463_4_ + 32 << 12);
								}
							}
						}
					}
				}

				this.theProfiler.endSection();
				return true;
			}
		}

///    
///     <summary> * Runs through the list of updates to run and ticks them </summary>
///     
		public virtual bool tickUpdates(bool p_72955_1_)
		{
			return false;
		}

		public virtual IList getPendingBlockUpdates(Chunk p_72920_1_, bool p_72920_2_)
		{
			return null;
		}

///    
///     <summary> * Will get all entities within the specified AABB excluding the one passed into it. Args: entityToExclude, aabb </summary>
///     
		public virtual IList getEntitiesWithinAABBExcludingEntity(Entity p_72839_1_, AxisAlignedBB p_72839_2_)
		{
			return this.getEntitiesWithinAABBExcludingEntity(p_72839_1_, p_72839_2_, (IEntitySelector)null);
		}

		public virtual IList getEntitiesWithinAABBExcludingEntity(Entity p_94576_1_, AxisAlignedBB p_94576_2_, IEntitySelector p_94576_3_)
		{
			ArrayList var4 = new ArrayList();
			int var5 = MathHelper.floor_double((p_94576_2_.minX - 2.0D) / 16.0D);
			int var6 = MathHelper.floor_double((p_94576_2_.maxX + 2.0D) / 16.0D);
			int var7 = MathHelper.floor_double((p_94576_2_.minZ - 2.0D) / 16.0D);
			int var8 = MathHelper.floor_double((p_94576_2_.maxZ + 2.0D) / 16.0D);

			for (int var9 = var5; var9 <= var6; ++var9)
			{
				for (int var10 = var7; var10 <= var8; ++var10)
				{
					if (this.chunkExists(var9, var10))
					{
						this.getChunkFromChunkCoords(var9, var10).getEntitiesWithinAABBForEntity(p_94576_1_, p_94576_2_, var4, p_94576_3_);
					}
				}
			}

			return var4;
		}

///    
///     <summary> * Returns all entities of the specified class type which intersect with the AABB. Args: entityClass, aabb </summary>
///     
		public virtual IList getEntitiesWithinAABB(Type p_72872_1_, AxisAlignedBB p_72872_2_)
		{
			return this.selectEntitiesWithinAABB(p_72872_1_, p_72872_2_, (IEntitySelector)null);
		}

		public virtual IList selectEntitiesWithinAABB(Type p_82733_1_, AxisAlignedBB p_82733_2_, IEntitySelector p_82733_3_)
		{
			int var4 = MathHelper.floor_double((p_82733_2_.minX - 2.0D) / 16.0D);
			int var5 = MathHelper.floor_double((p_82733_2_.maxX + 2.0D) / 16.0D);
			int var6 = MathHelper.floor_double((p_82733_2_.minZ - 2.0D) / 16.0D);
			int var7 = MathHelper.floor_double((p_82733_2_.maxZ + 2.0D) / 16.0D);
			ArrayList var8 = new ArrayList();

			for (int var9 = var4; var9 <= var5; ++var9)
			{
				for (int var10 = var6; var10 <= var7; ++var10)
				{
					if (this.chunkExists(var9, var10))
					{
						this.getChunkFromChunkCoords(var9, var10).getEntitiesOfTypeWithinAAAB(p_82733_1_, p_82733_2_, var8, p_82733_3_);
					}
				}
			}

			return var8;
		}

		public virtual Entity findNearestEntityWithinAABB(Type p_72857_1_, AxisAlignedBB p_72857_2_, Entity p_72857_3_)
		{
			IList var4 = this.getEntitiesWithinAABB(p_72857_1_, p_72857_2_);
			Entity var5 = null;
			double var6 = double.MaxValue;

			for (int var8 = 0; var8 < var4.Count; ++var8)
			{
				Entity var9 = (Entity)var4[var8];

				if (var9 != p_72857_3_)
				{
					double var10 = p_72857_3_.getDistanceSqToEntity(var9);

					if (var10 <= var6)
					{
						var5 = var9;
						var6 = var10;
					}
				}
			}

			return var5;
		}

///    
///     <summary> * Returns the Entity with the given ID, or null if it doesn't exist in this World. </summary>
///     
		public abstract Entity getEntityByID(int p_73045_1_);

///    
///     <summary> * Accessor for world Loaded Entity List </summary>
///     
		public virtual IList LoadedEntityList
		{
			get
			{
				return this.loadedEntityList;
			}
		}

		public virtual void func_147476_b(int p_147476_1_, int p_147476_2_, int p_147476_3_, TileEntity p_147476_4_)
		{
			if (this.blockExists(p_147476_1_, p_147476_2_, p_147476_3_))
			{
				this.getChunkFromBlockCoords(p_147476_1_, p_147476_3_).setChunkModified();
			}
		}

///    
///     <summary> * Counts how many entities of an entity class exist in the world. Args: entityClass </summary>
///     
		public virtual int countEntities(Type p_72907_1_)
		{
			int var2 = 0;

			for (int var3 = 0; var3 < this.loadedEntityList.Count; ++var3)
			{
				Entity var4 = (Entity)this.loadedEntityList.get(var3);

				if ((!(var4 is EntityLiving) || !((EntityLiving)var4).NoDespawnRequired) && p_72907_1_.isAssignableFrom(var4.GetType()))
				{
					++var2;
				}
			}

			return var2;
		}

///    
///     <summary> * adds entities to the loaded entities list, and loads thier skins. </summary>
///     
		public virtual void addLoadedEntities(IList p_72868_1_)
		{
			this.loadedEntityList.AddRange(p_72868_1_);

			for (int var2 = 0; var2 < p_72868_1_.Count; ++var2)
			{
				this.onEntityAdded((Entity)p_72868_1_[var2]);
			}
		}

///    
///     <summary> * Adds a list of entities to be unloaded on the next pass of World.updateEntities() </summary>
///     
		public virtual void unloadEntities(IList p_72828_1_)
		{
			this.unloadedEntityList.AddRange(p_72828_1_);
		}

		public virtual bool canPlaceEntityOnSide(Block p_147472_1_, int p_147472_2_, int p_147472_3_, int p_147472_4_, bool p_147472_5_, int p_147472_6_, Entity p_147472_7_, ItemStack p_147472_8_)
		{
			Block var9 = this.getBlock(p_147472_2_, p_147472_3_, p_147472_4_);
			AxisAlignedBB var10 = p_147472_5_ ? null : p_147472_1_.getCollisionBoundingBoxFromPool(this, p_147472_2_, p_147472_3_, p_147472_4_);
			return var10 != null && !this.checkNoEntityCollision(var10, p_147472_7_) ? false : (var9.Material == Material.circuits && p_147472_1_ == Blocks.anvil ? true : var9.Material.Replaceable && p_147472_1_.canReplace(this, p_147472_2_, p_147472_3_, p_147472_4_, p_147472_6_, p_147472_8_));
		}

		public virtual PathEntity getPathEntityToEntity(Entity p_72865_1_, Entity p_72865_2_, float p_72865_3_, bool p_72865_4_, bool p_72865_5_, bool p_72865_6_, bool p_72865_7_)
		{
			this.theProfiler.startSection("pathfind");
			int var8 = MathHelper.floor_double(p_72865_1_.posX);
			int var9 = MathHelper.floor_double(p_72865_1_.posY + 1.0D);
			int var10 = MathHelper.floor_double(p_72865_1_.posZ);
			int var11 = (int)(p_72865_3_ + 16.0F);
			int var12 = var8 - var11;
			int var13 = var9 - var11;
			int var14 = var10 - var11;
			int var15 = var8 + var11;
			int var16 = var9 + var11;
			int var17 = var10 + var11;
			ChunkCache var18 = new ChunkCache(this, var12, var13, var14, var15, var16, var17, 0);
			PathEntity var19 = (new PathFinder(var18, p_72865_4_, p_72865_5_, p_72865_6_, p_72865_7_)).createEntityPathTo(p_72865_1_, p_72865_2_, p_72865_3_);
			this.theProfiler.endSection();
			return var19;
		}

		public virtual PathEntity getEntityPathToXYZ(Entity p_72844_1_, int p_72844_2_, int p_72844_3_, int p_72844_4_, float p_72844_5_, bool p_72844_6_, bool p_72844_7_, bool p_72844_8_, bool p_72844_9_)
		{
			this.theProfiler.startSection("pathfind");
			int var10 = MathHelper.floor_double(p_72844_1_.posX);
			int var11 = MathHelper.floor_double(p_72844_1_.posY);
			int var12 = MathHelper.floor_double(p_72844_1_.posZ);
			int var13 = (int)(p_72844_5_ + 8.0F);
			int var14 = var10 - var13;
			int var15 = var11 - var13;
			int var16 = var12 - var13;
			int var17 = var10 + var13;
			int var18 = var11 + var13;
			int var19 = var12 + var13;
			ChunkCache var20 = new ChunkCache(this, var14, var15, var16, var17, var18, var19, 0);
			PathEntity var21 = (new PathFinder(var20, p_72844_6_, p_72844_7_, p_72844_8_, p_72844_9_)).createEntityPathTo(p_72844_1_, p_72844_2_, p_72844_3_, p_72844_4_, p_72844_5_);
			this.theProfiler.endSection();
			return var21;
		}

///    
///     <summary> * Is this block powering in the specified direction Args: x, y, z, direction </summary>
///     
		public virtual int isBlockProvidingPowerTo(int p_72879_1_, int p_72879_2_, int p_72879_3_, int p_72879_4_)
		{
			return this.getBlock(p_72879_1_, p_72879_2_, p_72879_3_).isProvidingStrongPower(this, p_72879_1_, p_72879_2_, p_72879_3_, p_72879_4_);
		}

///    
///     <summary> * Returns the highest redstone signal strength powering the given block. Args: X, Y, Z. </summary>
///     
		public virtual int getBlockPowerInput(int p_94577_1_, int p_94577_2_, int p_94577_3_)
		{
			sbyte var4 = 0;
			int var5 = Math.Max(var4, this.isBlockProvidingPowerTo(p_94577_1_, p_94577_2_ - 1, p_94577_3_, 0));

			if (var5 >= 15)
			{
				return var5;
			}
			else
			{
				var5 = Math.Max(var5, this.isBlockProvidingPowerTo(p_94577_1_, p_94577_2_ + 1, p_94577_3_, 1));

				if (var5 >= 15)
				{
					return var5;
				}
				else
				{
					var5 = Math.Max(var5, this.isBlockProvidingPowerTo(p_94577_1_, p_94577_2_, p_94577_3_ - 1, 2));

					if (var5 >= 15)
					{
						return var5;
					}
					else
					{
						var5 = Math.Max(var5, this.isBlockProvidingPowerTo(p_94577_1_, p_94577_2_, p_94577_3_ + 1, 3));

						if (var5 >= 15)
						{
							return var5;
						}
						else
						{
							var5 = Math.Max(var5, this.isBlockProvidingPowerTo(p_94577_1_ - 1, p_94577_2_, p_94577_3_, 4));

							if (var5 >= 15)
							{
								return var5;
							}
							else
							{
								var5 = Math.Max(var5, this.isBlockProvidingPowerTo(p_94577_1_ + 1, p_94577_2_, p_94577_3_, 5));
								return var5 >= 15 ? var5 : var5;
							}
						}
					}
				}
			}
		}

///    
///     <summary> * Returns the indirect signal strength being outputted by the given block in the *opposite* of the given direction.
///     * Args: X, Y, Z, direction </summary>
///     
		public virtual bool getIndirectPowerOutput(int p_94574_1_, int p_94574_2_, int p_94574_3_, int p_94574_4_)
		{
			return this.getIndirectPowerLevelTo(p_94574_1_, p_94574_2_, p_94574_3_, p_94574_4_) > 0;
		}

///    
///     <summary> * Gets the power level from a certain block face.  Args: x, y, z, direction </summary>
///     
		public virtual int getIndirectPowerLevelTo(int p_72878_1_, int p_72878_2_, int p_72878_3_, int p_72878_4_)
		{
			return this.getBlock(p_72878_1_, p_72878_2_, p_72878_3_).NormalCube ? this.getBlockPowerInput(p_72878_1_, p_72878_2_, p_72878_3_) : this.getBlock(p_72878_1_, p_72878_2_, p_72878_3_).isProvidingWeakPower(this, p_72878_1_, p_72878_2_, p_72878_3_, p_72878_4_);
		}

///    
///     <summary> * Used to see if one of the blocks next to you or your block is getting power from a neighboring block. Used by
///     * items like TNT or Doors so they don't have redstone going straight into them.  Args: x, y, z </summary>
///     
		public virtual bool isBlockIndirectlyGettingPowered(int p_72864_1_, int p_72864_2_, int p_72864_3_)
		{
			return this.getIndirectPowerLevelTo(p_72864_1_, p_72864_2_ - 1, p_72864_3_, 0) > 0 ? true : (this.getIndirectPowerLevelTo(p_72864_1_, p_72864_2_ + 1, p_72864_3_, 1) > 0 ? true : (this.getIndirectPowerLevelTo(p_72864_1_, p_72864_2_, p_72864_3_ - 1, 2) > 0 ? true : (this.getIndirectPowerLevelTo(p_72864_1_, p_72864_2_, p_72864_3_ + 1, 3) > 0 ? true : (this.getIndirectPowerLevelTo(p_72864_1_ - 1, p_72864_2_, p_72864_3_, 4) > 0 ? true : this.getIndirectPowerLevelTo(p_72864_1_ + 1, p_72864_2_, p_72864_3_, 5) > 0))));
		}

		public virtual int getStrongestIndirectPower(int p_94572_1_, int p_94572_2_, int p_94572_3_)
		{
			int var4 = 0;

			for (int var5 = 0; var5 < 6; ++var5)
			{
				int var6 = this.getIndirectPowerLevelTo(p_94572_1_ + Facing.offsetsXForSide[var5], p_94572_2_ + Facing.offsetsYForSide[var5], p_94572_3_ + Facing.offsetsZForSide[var5], var5);

				if (var6 >= 15)
				{
					return 15;
				}

				if (var6 > var4)
				{
					var4 = var6;
				}
			}

			return var4;
		}

///    
///     <summary> * Gets the closest player to the entity within the specified distance (if distance is less than 0 then ignored).
///     * Args: entity, dist </summary>
///     
		public virtual EntityPlayer getClosestPlayerToEntity(Entity p_72890_1_, double p_72890_2_)
		{
			return this.getClosestPlayer(p_72890_1_.posX, p_72890_1_.posY, p_72890_1_.posZ, p_72890_2_);
		}

///    
///     <summary> * Gets the closest player to the point within the specified distance (distance can be set to less than 0 to not
///     * limit the distance). Args: x, y, z, dist </summary>
///     
		public virtual EntityPlayer getClosestPlayer(double p_72977_1_, double p_72977_3_, double p_72977_5_, double p_72977_7_)
		{
			double var9 = -1.0D;
			EntityPlayer var11 = null;

			for (int var12 = 0; var12 < this.playerEntities.Count; ++var12)
			{
				EntityPlayer var13 = (EntityPlayer)this.playerEntities.get(var12);
				double var14 = var13.getDistanceSq(p_72977_1_, p_72977_3_, p_72977_5_);

				if ((p_72977_7_ < 0.0D || var14 < p_72977_7_ * p_72977_7_) && (var9 == -1.0D || var14 < var9))
				{
					var9 = var14;
					var11 = var13;
				}
			}

			return var11;
		}

///    
///     <summary> * Returns the closest vulnerable player to this entity within the given radius, or null if none is found </summary>
///     
		public virtual EntityPlayer getClosestVulnerablePlayerToEntity(Entity p_72856_1_, double p_72856_2_)
		{
			return this.getClosestVulnerablePlayer(p_72856_1_.posX, p_72856_1_.posY, p_72856_1_.posZ, p_72856_2_);
		}

///    
///     <summary> * Returns the closest vulnerable player within the given radius, or null if none is found. </summary>
///     
		public virtual EntityPlayer getClosestVulnerablePlayer(double p_72846_1_, double p_72846_3_, double p_72846_5_, double p_72846_7_)
		{
			double var9 = -1.0D;
			EntityPlayer var11 = null;

			for (int var12 = 0; var12 < this.playerEntities.Count; ++var12)
			{
				EntityPlayer var13 = (EntityPlayer)this.playerEntities.get(var12);

				if (!var13.capabilities.disableDamage && var13.EntityAlive)
				{
					double var14 = var13.getDistanceSq(p_72846_1_, p_72846_3_, p_72846_5_);
					double var16 = p_72846_7_;

					if (var13.Sneaking)
					{
						var16 = p_72846_7_ * 0.800000011920929D;
					}

					if (var13.Invisible)
					{
						float var18 = var13.ArmorVisibility;

						if (var18 < 0.1F)
						{
							var18 = 0.1F;
						}

						var16 *= (double)(0.7F * var18);
					}

					if ((p_72846_7_ < 0.0D || var14 < var16 * var16) && (var9 == -1.0D || var14 < var9))
					{
						var9 = var14;
						var11 = var13;
					}
				}
			}

			return var11;
		}

///    
///     <summary> * Find a player by name in this world. </summary>
///     
		public virtual EntityPlayer getPlayerEntityByName(string p_72924_1_)
		{
			for (int var2 = 0; var2 < this.playerEntities.Count; ++var2)
			{
				EntityPlayer var3 = (EntityPlayer)this.playerEntities.get(var2);

				if (p_72924_1_.Equals(var3.CommandSenderName))
				{
					return var3;
				}
			}

			return null;
		}

		public virtual EntityPlayer func_152378_a(UUID p_152378_1_)
		{
			for (int var2 = 0; var2 < this.playerEntities.Count; ++var2)
			{
				EntityPlayer var3 = (EntityPlayer)this.playerEntities.get(var2);

				if (p_152378_1_.Equals(var3.UniqueID))
				{
					return var3;
				}
			}

			return null;
		}

///    
///     <summary> * If on MP, sends a quitting packet. </summary>
///     
		public virtual void sendQuittingDisconnectingPacket()
		{
		}

///    
///     <summary> * Checks whether the session lock file was modified by another process </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void checkSessionLock() throws MinecraftException
		public virtual void checkSessionLock()
		{
			this.saveHandler.checkSessionLock();
		}

		public virtual void func_82738_a(long p_82738_1_)
		{
			this.worldInfo.incrementTotalWorldTime(p_82738_1_);
		}

///    
///     <summary> * Retrieve the world seed from level.dat </summary>
///     
		public virtual long Seed
		{
			get
			{
				return this.worldInfo.Seed;
			}
		}

		public virtual long TotalWorldTime
		{
			get
			{
				return this.worldInfo.WorldTotalTime;
			}
		}

		public virtual long WorldTime
		{
			get
			{
				return this.worldInfo.WorldTime;
			}
			set
			{
				this.worldInfo.WorldTime = value;
			}
		}

///    
///     <summary> * Sets the world time. </summary>
///     

///    
///     <summary> * Returns the coordinates of the spawn point </summary>
///     
		public virtual ChunkCoordinates SpawnPoint
		{
			get
			{
				return new ChunkCoordinates(this.worldInfo.SpawnX, this.worldInfo.SpawnY, this.worldInfo.SpawnZ);
			}
		}

		public virtual void setSpawnLocation(int p_72950_1_, int p_72950_2_, int p_72950_3_)
		{
			this.worldInfo.setSpawnPosition(p_72950_1_, p_72950_2_, p_72950_3_);
		}

///    
///     <summary> * spwans an entity and loads surrounding chunks </summary>
///     
		public virtual void joinEntityInSurroundings(Entity p_72897_1_)
		{
			int var2 = MathHelper.floor_double(p_72897_1_.posX / 16.0D);
			int var3 = MathHelper.floor_double(p_72897_1_.posZ / 16.0D);
			sbyte var4 = 2;

			for (int var5 = var2 - var4; var5 <= var2 + var4; ++var5)
			{
				for (int var6 = var3 - var4; var6 <= var3 + var4; ++var6)
				{
					this.getChunkFromChunkCoords(var5, var6);
				}
			}

			if (!this.loadedEntityList.Contains(p_72897_1_))
			{
				this.loadedEntityList.Add(p_72897_1_);
			}
		}

///    
///     <summary> * Called when checking if a certain block can be mined or not. The 'spawn safe zone' check is located here. </summary>
///     
		public virtual bool canMineBlock(EntityPlayer p_72962_1_, int p_72962_2_, int p_72962_3_, int p_72962_4_)
		{
			return true;
		}

///    
///     <summary> * sends a Packet 38 (Entity Status) to all tracked players of that entity </summary>
///     
		public virtual void setEntityState(Entity p_72960_1_, sbyte p_72960_2_)
		{
		}

///    
///     <summary> * gets the IChunkProvider this world uses. </summary>
///     
		public virtual IChunkProvider ChunkProvider
		{
			get
			{
				return this.chunkProvider;
			}
		}

		public virtual void func_147452_c(int p_147452_1_, int p_147452_2_, int p_147452_3_, Block p_147452_4_, int p_147452_5_, int p_147452_6_)
		{
			p_147452_4_.onBlockEventReceived(this, p_147452_1_, p_147452_2_, p_147452_3_, p_147452_5_, p_147452_6_);
		}

///    
///     <summary> * Returns this world's current save handler </summary>
///     
		public virtual ISaveHandler SaveHandler
		{
			get
			{
				return this.saveHandler;
			}
		}

///    
///     <summary> * Gets the World's WorldInfo instance </summary>
///     
		public virtual WorldInfo WorldInfo
		{
			get
			{
				return this.worldInfo;
			}
		}

///    
///     <summary> * Gets the GameRules instance. </summary>
///     
		public virtual GameRules GameRules
		{
			get
			{
				return this.worldInfo.GameRulesInstance;
			}
		}

///    
///     <summary> * Updates the flag that indicates whether or not all players in the world are sleeping. </summary>
///     
		public virtual void updateAllPlayersSleepingFlag()
		{
		}

		public virtual float getWeightedThunderStrength(float p_72819_1_)
		{
			return (this.prevThunderingStrength + (this.thunderingStrength - this.prevThunderingStrength) * p_72819_1_) * this.getRainStrength(p_72819_1_);
		}

///    
///     <summary> * Sets the strength of the thunder. </summary>
///     
		public virtual float ThunderStrength
		{
			set
			{
				this.prevThunderingStrength = value;
				this.thunderingStrength = value;
			}
		}

///    
///     <summary> * Not sure about this actually. Reverting this one myself. </summary>
///     
		public virtual float getRainStrength(float p_72867_1_)
		{
			return this.prevRainingStrength + (this.rainingStrength - this.prevRainingStrength) * p_72867_1_;
		}

///    
///     <summary> * Sets the strength of the rain. </summary>
///     
		public virtual float RainStrength
		{
			set
			{
				this.prevRainingStrength = value;
				this.rainingStrength = value;
			}
		}

///    
///     <summary> * Returns true if the current thunder strength (weighted with the rain strength) is greater than 0.9 </summary>
///     
		public virtual bool isThundering()
		{
			get
			{
				return (double)this.getWeightedThunderStrength(1.0F) > 0.9D;
			}
		}

///    
///     <summary> * Returns true if the current rain strength is greater than 0.2 </summary>
///     
		public virtual bool isRaining()
		{
			get
			{
				return (double)this.getRainStrength(1.0F) > 0.2D;
			}
		}

		public virtual bool canLightningStrikeAt(int p_72951_1_, int p_72951_2_, int p_72951_3_)
		{
			if (!this.Raining)
			{
				return false;
			}
			else if (!this.canBlockSeeTheSky(p_72951_1_, p_72951_2_, p_72951_3_))
			{
				return false;
			}
			else if (this.getPrecipitationHeight(p_72951_1_, p_72951_3_) > p_72951_2_)
			{
				return false;
			}
			else
			{
				BiomeGenBase var4 = this.getBiomeGenForCoords(p_72951_1_, p_72951_3_);
				return var4.EnableSnow ? false : (this.func_147478_e(p_72951_1_, p_72951_2_, p_72951_3_, false) ? false : var4.canSpawnLightningBolt());
			}
		}

///    
///     <summary> * Checks to see if the biome rainfall values for a given x,y,z coordinate set are extremely high </summary>
///     
		public virtual bool isBlockHighHumidity(int p_72958_1_, int p_72958_2_, int p_72958_3_)
		{
			BiomeGenBase var4 = this.getBiomeGenForCoords(p_72958_1_, p_72958_3_);
			return var4.HighHumidity;
		}

///    
///     <summary> * Assigns the given String id to the given MapDataBase using the MapStorage, removing any existing ones of the same
///     * id. </summary>
///     
		public virtual void setItemData(string p_72823_1_, WorldSavedData p_72823_2_)
		{
			this.mapStorage.setData(p_72823_1_, p_72823_2_);
		}

///    
///     <summary> * Loads an existing MapDataBase corresponding to the given String id from disk using the MapStorage, instantiating
///     * the given Class, or returns null if none such file exists. args: Class to instantiate, String dataid </summary>
///     
		public virtual WorldSavedData loadItemData(Type p_72943_1_, string p_72943_2_)
		{
			return this.mapStorage.loadData(p_72943_1_, p_72943_2_);
		}

///    
///     <summary> * Returns an unique new data id from the MapStorage for the given prefix and saves the idCounts map to the
///     * 'idcounts' file. </summary>
///     
		public virtual int getUniqueDataId(string p_72841_1_)
		{
			return this.mapStorage.getUniqueDataId(p_72841_1_);
		}

		public virtual void playBroadcastSound(int p_82739_1_, int p_82739_2_, int p_82739_3_, int p_82739_4_, int p_82739_5_)
		{
			for (int var6 = 0; var6 < this.worldAccesses.Count; ++var6)
			{
				((IWorldAccess)this.worldAccesses.get(var6)).broadcastSound(p_82739_1_, p_82739_2_, p_82739_3_, p_82739_4_, p_82739_5_);
			}
		}

///    
///     <summary> * See description for playAuxSFX. </summary>
///     
		public virtual void playAuxSFX(int p_72926_1_, int p_72926_2_, int p_72926_3_, int p_72926_4_, int p_72926_5_)
		{
			this.playAuxSFXAtEntity((EntityPlayer)null, p_72926_1_, p_72926_2_, p_72926_3_, p_72926_4_, p_72926_5_);
		}

///    
///     <summary> * See description for playAuxSFX. </summary>
///     
		public virtual void playAuxSFXAtEntity(EntityPlayer p_72889_1_, int p_72889_2_, int p_72889_3_, int p_72889_4_, int p_72889_5_, int p_72889_6_)
		{
			try
			{
				for (int var7 = 0; var7 < this.worldAccesses.Count; ++var7)
				{
					((IWorldAccess)this.worldAccesses.get(var7)).playAuxSFX(p_72889_1_, p_72889_2_, p_72889_3_, p_72889_4_, p_72889_5_, p_72889_6_);
				}
			}
			catch (Exception var10)
			{
				CrashReport var8 = CrashReport.makeCrashReport(var10, "Playing level event");
				CrashReportCategory var9 = var8.makeCategory("Level event being played");
				var9.addCrashSection("Block coordinates", CrashReportCategory.getLocationInfo(p_72889_3_, p_72889_4_, p_72889_5_));
				var9.addCrashSection("Event source", p_72889_1_);
				var9.addCrashSection("Event type", Convert.ToInt32(p_72889_2_));
				var9.addCrashSection("Event data", Convert.ToInt32(p_72889_6_));
				throw new ReportedException(var8);
			}
		}

///    
///     <summary> * Returns current world height. </summary>
///     
		public virtual int Height
		{
			get
			{
				return 256;
			}
		}

///    
///     <summary> * Returns current world height. </summary>
///     
		public virtual int ActualHeight
		{
			get
			{
				return this.provider.hasNoSky ? 128 : 256;
			}
		}

///    
///     <summary> * puts the World Random seed to a specific state dependant on the inputs </summary>
///     
		public virtual Random setRandomSeed(int p_72843_1_, int p_72843_2_, int p_72843_3_)
		{
			long var4 = (long)p_72843_1_ * 341873128712L + (long)p_72843_2_ * 132897987541L + this.WorldInfo.Seed + (long)p_72843_3_;
			this.rand.Seed = var4;
			return this.rand;
		}

///    
///     <summary> * Returns the location of the closest structure of the specified type. If not found returns null. </summary>
///     
		public virtual ChunkPosition findClosestStructure(string p_147440_1_, int p_147440_2_, int p_147440_3_, int p_147440_4_)
		{
			return this.ChunkProvider.func_147416_a(this, p_147440_1_, p_147440_2_, p_147440_3_, p_147440_4_);
		}

///    
///     <summary> * set by !chunk.getAreLevelsEmpty </summary>
///     
		public virtual bool extendedLevelsInChunkCache()
		{
			return false;
		}

///    
///     <summary> * Returns horizon height for use in rendering the sky. </summary>
///     
		public virtual double Horizon
		{
			get
			{
				return this.worldInfo.TerrainType == WorldType.FLAT ? 0.0D : 63.0D;
			}
		}

///    
///     <summary> * Adds some basic stats of the world to the given crash report. </summary>
///     
		public virtual CrashReportCategory addWorldInfoToCrashReport(CrashReport p_72914_1_)
		{
			CrashReportCategory var2 = p_72914_1_.makeCategoryDepth("Affected level", 1);
			var2.addCrashSection("Level name", this.worldInfo == null ? "????" : this.worldInfo.WorldName);
			var2.addCrashSectionCallable("All players", new Callable() { private static final string __OBFID = "CL_00000143"; public string call() { return World.playerEntities.size() + " total; " + World.playerEntities.ToString(); } });
			var2.addCrashSectionCallable("Chunk stats", new Callable() { private static final string __OBFID = "CL_00000144"; public string call() { return World.chunkProvider.makeString(); } });

			try
			{
				this.worldInfo.addToCrashReport(var2);
			}
			catch (Exception var4)
			{
				var2.addCrashSectionThrowable("Level Data Unobtainable", var4);
			}

			return var2;
		}

///    
///     <summary> * Starts (or continues) destroying a block with given ID at the given coordinates for the given partially destroyed
///     * value. </summary>
///     
		public virtual void destroyBlockInWorldPartially(int p_147443_1_, int p_147443_2_, int p_147443_3_, int p_147443_4_, int p_147443_5_)
		{
			for (int var6 = 0; var6 < this.worldAccesses.Count; ++var6)
			{
				IWorldAccess var7 = (IWorldAccess)this.worldAccesses.get(var6);
				var7.destroyBlockPartially(p_147443_1_, p_147443_2_, p_147443_3_, p_147443_4_, p_147443_5_);
			}
		}

///    
///     <summary> * returns a calendar object containing the current date </summary>
///     
		public virtual Calendar CurrentDate
		{
			get
			{
				if (this.TotalWorldTime % 600L == 0L)
				{
					this.theCalendar.TimeInMillis = MinecraftServer.SystemTimeMillis;
				}
	
				return this.theCalendar;
			}
		}

		public virtual void makeFireworks(double p_92088_1_, double p_92088_3_, double p_92088_5_, double p_92088_7_, double p_92088_9_, double p_92088_11_, NBTTagCompound p_92088_13_)
		{
		}

		public virtual Scoreboard Scoreboard
		{
			get
			{
				return this.worldScoreboard;
			}
		}

		public virtual void func_147453_f(int p_147453_1_, int p_147453_2_, int p_147453_3_, Block p_147453_4_)
		{
			for (int var5 = 0; var5 < 4; ++var5)
			{
				int var6 = p_147453_1_ + Direction.offsetX[var5];
				int var7 = p_147453_3_ + Direction.offsetZ[var5];
				Block var8 = this.getBlock(var6, p_147453_2_, var7);

				if (Blocks.unpowered_comparator.func_149907_e(var8))
				{
					var8.onNeighborBlockChange(this, var6, p_147453_2_, var7, p_147453_4_);
				}
				else if (var8.NormalCube)
				{
					var6 += Direction.offsetX[var5];
					var7 += Direction.offsetZ[var5];
					Block var9 = this.getBlock(var6, p_147453_2_, var7);

					if (Blocks.unpowered_comparator.func_149907_e(var9))
					{
						var9.onNeighborBlockChange(this, var6, p_147453_2_, var7, p_147453_4_);
					}
				}
			}
		}

		public virtual float func_147462_b(double p_147462_1_, double p_147462_3_, double p_147462_5_)
		{
			return this.func_147473_B(MathHelper.floor_double(p_147462_1_), MathHelper.floor_double(p_147462_3_), MathHelper.floor_double(p_147462_5_));
		}

		public virtual float func_147473_B(int p_147473_1_, int p_147473_2_, int p_147473_3_)
		{
			float var4 = 0.0F;
			bool var5 = this.difficultySetting == EnumDifficulty.HARD;

			if (this.blockExists(p_147473_1_, p_147473_2_, p_147473_3_))
			{
				float var6 = this.CurrentMoonPhaseFactor;
				var4 += MathHelper.clamp_float((float)this.getChunkFromBlockCoords(p_147473_1_, p_147473_3_).inhabitedTime / 3600000.0F, 0.0F, 1.0F) * (var5 ? 1.0F : 0.75F);
				var4 += var6 * 0.25F;
			}

			if (this.difficultySetting == EnumDifficulty.EASY || this.difficultySetting == EnumDifficulty.PEACEFUL)
			{
				var4 *= (float)this.difficultySetting.DifficultyId / 2.0F;
			}

			return MathHelper.clamp_float(var4, 0.0F, var5 ? 1.5F : 1.0F);
		}

		public virtual void func_147450_X()
		{
			IEnumerator var1 = this.worldAccesses.GetEnumerator();

			while (var1.MoveNext())
			{
				IWorldAccess var2 = (IWorldAccess)var1.Current;
				var2.onStaticEntitiesChanged();
			}
		}
	}

}