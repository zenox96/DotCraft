using System;
using System.Collections;

namespace DotCraftCore.World
{

	using Lists = com.google.common.collect.Lists;
	using Block = DotCraftCore.block.Block;
	using BlockEventData = DotCraftCore.block.BlockEventData;
	using Material = DotCraftCore.block.material.Material;
	using CrashReport = DotCraftCore.crash.CrashReport;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using Entity = DotCraftCore.entity.Entity;
	using EntityTracker = DotCraftCore.entity.EntityTracker;
	using EnumCreatureType = DotCraftCore.entity.EnumCreatureType;
	using INpc = DotCraftCore.entity.INpc;
	using EntityLightningBolt = DotCraftCore.entity.effect.EntityLightningBolt;
	using EntityAnimal = DotCraftCore.entity.passive.EntityAnimal;
	using EntityWaterMob = DotCraftCore.entity.passive.EntityWaterMob;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using EntityPlayerMP = DotCraftCore.entity.player.EntityPlayerMP;
	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using Item = DotCraftCore.item.Item;
	using S19PacketEntityStatus = DotCraftCore.network.play.server.S19PacketEntityStatus;
	using S24PacketBlockAction = DotCraftCore.network.play.server.S24PacketBlockAction;
	using S27PacketExplosion = DotCraftCore.network.play.server.S27PacketExplosion;
	using S2APacketParticles = DotCraftCore.network.play.server.S2APacketParticles;
	using S2BPacketChangeGameState = DotCraftCore.network.play.server.S2BPacketChangeGameState;
	using S2CPacketSpawnGlobalEntity = DotCraftCore.network.play.server.S2CPacketSpawnGlobalEntity;
	using Profiler = DotCraftCore.profiler.Profiler;
	using ScoreboardSaveData = DotCraftCore.Scoreboard.ScoreboardSaveData;
	using ServerScoreboard = DotCraftCore.Scoreboard.ServerScoreboard;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using PlayerManager = DotCraftCore.Server.Management.PlayerManager;
	using TileEntity = DotCraftCore.TileEntity.TileEntity;
	using ChunkCoordinates = DotCraftCore.Util.ChunkCoordinates;
	using IProgressUpdate = DotCraftCore.Util.IProgressUpdate;
	using IntHashMap = DotCraftCore.Util.IntHashMap;
	using ReportedException = DotCraftCore.Util.ReportedException;
	using Vec3 = DotCraftCore.Util.Vec3;
	using WeightedRandom = DotCraftCore.Util.WeightedRandom;
	using WeightedRandomChestContent = DotCraftCore.Util.WeightedRandomChestContent;
	using BiomeGenBase = DotCraftCore.World.Biome.BiomeGenBase;
	using WorldChunkManager = DotCraftCore.World.Biome.WorldChunkManager;
	using Chunk = DotCraftCore.World.Chunk.Chunk;
	using IChunkProvider = DotCraftCore.World.Chunk.IChunkProvider;
	using ExtendedBlockStorage = DotCraftCore.World.Chunk.Storage.ExtendedBlockStorage;
	using IChunkLoader = DotCraftCore.World.Chunk.Storage.IChunkLoader;
	using ChunkProviderServer = DotCraftCore.World.Gen.ChunkProviderServer;
	using WorldGeneratorBonusChest = DotCraftCore.World.Gen.Feature.WorldGeneratorBonusChest;
	using ISaveHandler = DotCraftCore.World.Storage.ISaveHandler;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class WorldServer : World
	{
		private static readonly Logger logger = LogManager.Logger;
		private readonly MinecraftServer mcServer;
		private readonly EntityTracker theEntityTracker;
		private readonly PlayerManager thePlayerManager;
		private Set pendingTickListEntriesHashSet;

	/// <summary> All work to do in future ticks.  </summary>
		private TreeSet pendingTickListEntriesTreeSet;
		public ChunkProviderServer theChunkProviderServer;

	/// <summary> Whether or not level saving is enabled  </summary>
		public bool levelSaving;

	/// <summary> is false if there are no players  </summary>
		private bool allPlayersSleeping;
		private int updateEntityTick;

///    
///     <summary> * the teleporter to use when the entity is being transferred into the dimension </summary>
///     
		private readonly Teleporter worldTeleporter;
		private readonly SpawnerAnimals animalSpawner = new SpawnerAnimals();
		private WorldServer.ServerBlockEventList[] field_147490_S = new WorldServer.ServerBlockEventList[] {new WorldServer.ServerBlockEventList(null), new WorldServer.ServerBlockEventList(null)};
		private int field_147489_T;
		private static readonly WeightedRandomChestContent[] bonusChestContent = new WeightedRandomChestContent[] {new WeightedRandomChestContent(Items.stick, 0, 1, 3, 10), new WeightedRandomChestContent(Item.getItemFromBlock(Blocks.planks), 0, 1, 3, 10), new WeightedRandomChestContent(Item.getItemFromBlock(Blocks.log), 0, 1, 3, 10), new WeightedRandomChestContent(Items.stone_axe, 0, 1, 1, 3), new WeightedRandomChestContent(Items.wooden_axe, 0, 1, 1, 5), new WeightedRandomChestContent(Items.stone_pickaxe, 0, 1, 1, 3), new WeightedRandomChestContent(Items.wooden_pickaxe, 0, 1, 1, 5), new WeightedRandomChestContent(Items.apple, 0, 2, 3, 5), new WeightedRandomChestContent(Items.bread, 0, 2, 3, 3), new WeightedRandomChestContent(Item.getItemFromBlock(Blocks.log2), 0, 1, 3, 10)};
		private IList pendingTickListEntriesThisTick = new ArrayList();

	/// <summary> An IntHashMap of entity IDs (integers) to their Entity objects.  </summary>
		private IntHashMap entityIdMap;
		

		public WorldServer(MinecraftServer p_i45284_1_, ISaveHandler p_i45284_2_, string p_i45284_3_, int p_i45284_4_, WorldSettings p_i45284_5_, Profiler p_i45284_6_) : base(p_i45284_2_, p_i45284_3_, p_i45284_5_, WorldProvider.getProviderForDimension(p_i45284_4_), p_i45284_6_)
		{
			this.mcServer = p_i45284_1_;
			this.theEntityTracker = new EntityTracker(this);
			this.thePlayerManager = new PlayerManager(this);

			if (this.entityIdMap == null)
			{
				this.entityIdMap = new IntHashMap();
			}

			if (this.pendingTickListEntriesHashSet == null)
			{
				this.pendingTickListEntriesHashSet = new HashSet();
			}

			if (this.pendingTickListEntriesTreeSet == null)
			{
				this.pendingTickListEntriesTreeSet = new TreeSet();
			}

			this.worldTeleporter = new Teleporter(this);
			this.worldScoreboard = new ServerScoreboard(p_i45284_1_);
			ScoreboardSaveData var7 = (ScoreboardSaveData)this.mapStorage.loadData(typeof(ScoreboardSaveData), "scoreboard");

			if (var7 == null)
			{
				var7 = new ScoreboardSaveData();
				this.mapStorage.setData("scoreboard", var7);
			}

			var7.func_96499_a(this.worldScoreboard);
			((ServerScoreboard)this.worldScoreboard).func_96547_a(var7);
		}

///    
///     <summary> * Runs a single tick for the world </summary>
///     
		public override void tick()
		{
			base.tick();

			if (this.WorldInfo.HardcoreModeEnabled && this.difficultySetting != EnumDifficulty.HARD)
			{
				this.difficultySetting = EnumDifficulty.HARD;
			}

			this.provider.worldChunkMgr.cleanupCache();

			if (this.areAllPlayersAsleep())
			{
				if (this.GameRules.getGameRuleBooleanValue("doDaylightCycle"))
				{
					long var1 = this.worldInfo.WorldTime + 24000L;
					this.worldInfo.WorldTime = var1 - var1 % 24000L;
				}

				this.wakeAllPlayers();
			}

			this.theProfiler.startSection("mobSpawner");

			if (this.GameRules.getGameRuleBooleanValue("doMobSpawning"))
			{
				this.animalSpawner.findChunksForSpawning(this, this.spawnHostileMobs, this.spawnPeacefulMobs, this.worldInfo.WorldTotalTime % 400L == 0L);
			}

			this.theProfiler.endStartSection("chunkSource");
			this.chunkProvider.unloadQueuedChunks();
			int var3 = this.calculateSkylightSubtracted(1.0F);

			if (var3 != this.skylightSubtracted)
			{
				this.skylightSubtracted = var3;
			}

			this.worldInfo.incrementTotalWorldTime(this.worldInfo.WorldTotalTime + 1L);

			if (this.GameRules.getGameRuleBooleanValue("doDaylightCycle"))
			{
				this.worldInfo.WorldTime = this.worldInfo.WorldTime + 1L;
			}

			this.theProfiler.endStartSection("tickPending");
			this.tickUpdates(false);
			this.theProfiler.endStartSection("tickBlocks");
			this.func_147456_g();
			this.theProfiler.endStartSection("chunkMap");
			this.thePlayerManager.updatePlayerInstances();
			this.theProfiler.endStartSection("village");
			this.villageCollectionObj.tick();
			this.villageSiegeObj.tick();
			this.theProfiler.endStartSection("portalForcer");
			this.worldTeleporter.removeStalePortalLocations(this.TotalWorldTime);
			this.theProfiler.endSection();
			this.func_147488_Z();
		}

///    
///     <summary> * only spawns creatures allowed by the chunkProvider </summary>
///     
		public virtual BiomeGenBase.SpawnListEntry spawnRandomCreature(EnumCreatureType p_73057_1_, int p_73057_2_, int p_73057_3_, int p_73057_4_)
		{
			IList var5 = this.ChunkProvider.getPossibleCreatures(p_73057_1_, p_73057_2_, p_73057_3_, p_73057_4_);
			return var5 != null && !var5.Count == 0 ? (BiomeGenBase.SpawnListEntry)WeightedRandom.getRandomItem(this.rand, var5) : null;
		}

///    
///     <summary> * Updates the flag that indicates whether or not all players in the world are sleeping. </summary>
///     
		public override void updateAllPlayersSleepingFlag()
		{
			this.allPlayersSleeping = !this.playerEntities.Count == 0;
			IEnumerator var1 = this.playerEntities.GetEnumerator();

			while (var1.MoveNext())
			{
				EntityPlayer var2 = (EntityPlayer)var1.Current;

				if (!var2.PlayerSleeping)
				{
					this.allPlayersSleeping = false;
					break;
				}
			}
		}

		protected internal virtual void wakeAllPlayers()
		{
			this.allPlayersSleeping = false;
			IEnumerator var1 = this.playerEntities.GetEnumerator();

			while (var1.MoveNext())
			{
				EntityPlayer var2 = (EntityPlayer)var1.Current;

				if (var2.PlayerSleeping)
				{
					var2.wakeUpPlayer(false, false, true);
				}
			}

			this.resetRainAndThunder();
		}

		private void resetRainAndThunder()
		{
			this.worldInfo.RainTime = 0;
			this.worldInfo.Raining = false;
			this.worldInfo.ThunderTime = 0;
			this.worldInfo.Thundering = false;
		}

		public virtual bool areAllPlayersAsleep()
		{
			if (this.allPlayersSleeping && !this.isClient)
			{
				IEnumerator var1 = this.playerEntities.GetEnumerator();
				EntityPlayer var2;

				do
				{
					if (!var1.MoveNext())
					{
						return true;
					}

					var2 = (EntityPlayer)var1.Current;
				}
				while (var2.PlayerFullyAsleep);

				return false;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Sets a new spawn location by finding an uncovered block at a random (x,z) location in the chunk. </summary>
///     
		public override void setSpawnLocation()
		{
			if (this.worldInfo.SpawnY <= 0)
			{
				this.worldInfo.SpawnY = 64;
			}

			int var1 = this.worldInfo.SpawnX;
			int var2 = this.worldInfo.SpawnZ;
			int var3 = 0;

			while (this.getTopBlock(var1, var2).Material == Material.air)
			{
				var1 += this.rand.Next(8) - this.rand.Next(8);
				var2 += this.rand.Next(8) - this.rand.Next(8);
				++var3;

				if (var3 == 10000)
				{
					break;
				}
			}

			this.worldInfo.SpawnX = var1;
			this.worldInfo.SpawnZ = var2;
		}

		protected internal override void func_147456_g()
		{
			base.func_147456_g();
			int var1 = 0;
			int var2 = 0;
			IEnumerator var3 = this.activeChunkSet.GetEnumerator();

			while (var3.MoveNext())
			{
				ChunkCoordIntPair var4 = (ChunkCoordIntPair)var3.Current;
				int var5 = var4.chunkXPos * 16;
				int var6 = var4.chunkZPos * 16;
				this.theProfiler.startSection("getChunk");
				Chunk var7 = this.getChunkFromChunkCoords(var4.chunkXPos, var4.chunkZPos);
				this.func_147467_a(var5, var6, var7);
				this.theProfiler.endStartSection("tickChunk");
				var7.func_150804_b(false);
				this.theProfiler.endStartSection("thunder");
				int var8;
				int var9;
				int var10;
				int var11;

				if (this.rand.Next(100000) == 0 && this.Raining && this.Thundering)
				{
					this.updateLCG = this.updateLCG * 3 + 1013904223;
					var8 = this.updateLCG >> 2;
					var9 = var5 + (var8 & 15);
					var10 = var6 + (var8 >> 8 & 15);
					var11 = this.getPrecipitationHeight(var9, var10);

					if (this.canLightningStrikeAt(var9, var11, var10))
					{
						this.addWeatherEffect(new EntityLightningBolt(this, (double)var9, (double)var11, (double)var10));
					}
				}

				this.theProfiler.endStartSection("iceandsnow");

				if (this.rand.Next(16) == 0)
				{
					this.updateLCG = this.updateLCG * 3 + 1013904223;
					var8 = this.updateLCG >> 2;
					var9 = var8 & 15;
					var10 = var8 >> 8 & 15;
					var11 = this.getPrecipitationHeight(var9 + var5, var10 + var6);

					if (this.isBlockFreezableNaturally(var9 + var5, var11 - 1, var10 + var6))
					{
						this.setBlock(var9 + var5, var11 - 1, var10 + var6, Blocks.ice);
					}

					if (this.Raining && this.func_147478_e(var9 + var5, var11, var10 + var6, true))
					{
						this.setBlock(var9 + var5, var11, var10 + var6, Blocks.snow_layer);
					}

					if (this.Raining)
					{
						BiomeGenBase var12 = this.getBiomeGenForCoords(var9 + var5, var10 + var6);

						if (var12.canSpawnLightningBolt())
						{
							this.getBlock(var9 + var5, var11 - 1, var10 + var6).fillWithRain(this, var9 + var5, var11 - 1, var10 + var6);
						}
					}
				}

				this.theProfiler.endStartSection("tickBlocks");
				ExtendedBlockStorage[] var18 = var7.BlockStorageArray;
				var9 = var18.Length;

				for (var10 = 0; var10 < var9; ++var10)
				{
					ExtendedBlockStorage var19 = var18[var10];

					if (var19 != null && var19.NeedsRandomTick)
					{
						for (int var20 = 0; var20 < 3; ++var20)
						{
							this.updateLCG = this.updateLCG * 3 + 1013904223;
							int var13 = this.updateLCG >> 2;
							int var14 = var13 & 15;
							int var15 = var13 >> 8 & 15;
							int var16 = var13 >> 16 & 15;
							++var2;
							Block var17 = var19.func_150819_a(var14, var16, var15);

							if (var17.TickRandomly)
							{
								++var1;
								var17.updateTick(this, var14 + var5, var16 + var19.YLocation, var15 + var6, this.rand);
							}
						}
					}
				}

				this.theProfiler.endSection();
			}
		}

		public override bool func_147477_a(int p_147477_1_, int p_147477_2_, int p_147477_3_, Block p_147477_4_)
		{
			NextTickListEntry var5 = new NextTickListEntry(p_147477_1_, p_147477_2_, p_147477_3_, p_147477_4_);
			return this.pendingTickListEntriesThisTick.Contains(var5);
		}

///    
///     <summary> * Schedules a tick to a block with a delay (Most commonly the tick rate) </summary>
///     
		public override void scheduleBlockUpdate(int p_147464_1_, int p_147464_2_, int p_147464_3_, Block p_147464_4_, int p_147464_5_)
		{
			this.func_147454_a(p_147464_1_, p_147464_2_, p_147464_3_, p_147464_4_, p_147464_5_, 0);
		}

		public override void func_147454_a(int p_147454_1_, int p_147454_2_, int p_147454_3_, Block p_147454_4_, int p_147454_5_, int p_147454_6_)
		{
			NextTickListEntry var7 = new NextTickListEntry(p_147454_1_, p_147454_2_, p_147454_3_, p_147454_4_);
			sbyte var8 = 0;

			if (this.scheduledUpdatesAreImmediate && p_147454_4_.Material != Material.air)
			{
				if (p_147454_4_.func_149698_L())
				{
					var8 = 8;

					if (this.checkChunksExist(var7.xCoord - var8, var7.yCoord - var8, var7.zCoord - var8, var7.xCoord + var8, var7.yCoord + var8, var7.zCoord + var8))
					{
						Block var9 = this.getBlock(var7.xCoord, var7.yCoord, var7.zCoord);

						if (var9.Material != Material.air && var9 == var7.func_151351_a())
						{
							var9.updateTick(this, var7.xCoord, var7.yCoord, var7.zCoord, this.rand);
						}
					}

					return;
				}

				p_147454_5_ = 1;
			}

			if (this.checkChunksExist(p_147454_1_ - var8, p_147454_2_ - var8, p_147454_3_ - var8, p_147454_1_ + var8, p_147454_2_ + var8, p_147454_3_ + var8))
			{
				if (p_147454_4_.Material != Material.air)
				{
					var7.ScheduledTime = (long)p_147454_5_ + this.worldInfo.WorldTotalTime;
					var7.Priority = p_147454_6_;
				}

				if (!this.pendingTickListEntriesHashSet.contains(var7))
				{
					this.pendingTickListEntriesHashSet.add(var7);
					this.pendingTickListEntriesTreeSet.add(var7);
				}
			}
		}

		public override void func_147446_b(int p_147446_1_, int p_147446_2_, int p_147446_3_, Block p_147446_4_, int p_147446_5_, int p_147446_6_)
		{
			NextTickListEntry var7 = new NextTickListEntry(p_147446_1_, p_147446_2_, p_147446_3_, p_147446_4_);
			var7.Priority = p_147446_6_;

			if (p_147446_4_.Material != Material.air)
			{
				var7.ScheduledTime = (long)p_147446_5_ + this.worldInfo.WorldTotalTime;
			}

			if (!this.pendingTickListEntriesHashSet.contains(var7))
			{
				this.pendingTickListEntriesHashSet.add(var7);
				this.pendingTickListEntriesTreeSet.add(var7);
			}
		}

///    
///     <summary> * Updates (and cleans up) entities and tile entities </summary>
///     
		public override void updateEntities()
		{
			if (this.playerEntities.Count == 0)
			{
				if (this.updateEntityTick++ >= 1200)
				{
					return;
				}
			}
			else
			{
				this.resetUpdateEntityTick();
			}

			base.updateEntities();
		}

///    
///     <summary> * Resets the updateEntityTick field to 0 </summary>
///     
		public virtual void resetUpdateEntityTick()
		{
			this.updateEntityTick = 0;
		}

///    
///     <summary> * Runs through the list of updates to run and ticks them </summary>
///     
		public override bool tickUpdates(bool p_72955_1_)
		{
			int var2 = this.pendingTickListEntriesTreeSet.size();

			if (var2 != this.pendingTickListEntriesHashSet.size())
			{
				throw new IllegalStateException("TickNextTick list out of synch");
			}
			else
			{
				if (var2 > 1000)
				{
					var2 = 1000;
				}

				this.theProfiler.startSection("cleaning");
				NextTickListEntry var4;

				for (int var3 = 0; var3 < var2; ++var3)
				{
					var4 = (NextTickListEntry)this.pendingTickListEntriesTreeSet.first();

					if (!p_72955_1_ && var4.scheduledTime > this.worldInfo.WorldTotalTime)
					{
						break;
					}

					this.pendingTickListEntriesTreeSet.remove(var4);
					this.pendingTickListEntriesHashSet.remove(var4);
					this.pendingTickListEntriesThisTick.Add(var4);
				}

				this.theProfiler.endSection();
				this.theProfiler.startSection("ticking");
				IEnumerator var14 = this.pendingTickListEntriesThisTick.GetEnumerator();

				while (var14.MoveNext())
				{
					var4 = (NextTickListEntry)var14.Current;
					var14.remove();
					sbyte var5 = 0;

					if (this.checkChunksExist(var4.xCoord - var5, var4.yCoord - var5, var4.zCoord - var5, var4.xCoord + var5, var4.yCoord + var5, var4.zCoord + var5))
					{
						Block var6 = this.getBlock(var4.xCoord, var4.yCoord, var4.zCoord);

						if (var6.Material != Material.air && Block.isEqualTo(var6, var4.func_151351_a()))
						{
							try
							{
								var6.updateTick(this, var4.xCoord, var4.yCoord, var4.zCoord, this.rand);
							}
							catch (Exception var13)
							{
								CrashReport var8 = CrashReport.makeCrashReport(var13, "Exception while ticking a block");
								CrashReportCategory var9 = var8.makeCategory("Block being ticked");
								int var10;

								try
								{
									var10 = this.getBlockMetadata(var4.xCoord, var4.yCoord, var4.zCoord);
								}
								catch (Exception var12)
								{
									var10 = -1;
								}

								CrashReportCategory.func_147153_a(var9, var4.xCoord, var4.yCoord, var4.zCoord, var6, var10);
								throw new ReportedException(var8);
							}
						}
					}
					else
					{
						this.scheduleBlockUpdate(var4.xCoord, var4.yCoord, var4.zCoord, var4.func_151351_a(), 0);
					}
				}

				this.theProfiler.endSection();
				this.pendingTickListEntriesThisTick.Clear();
				return !this.pendingTickListEntriesTreeSet.Empty;
			}
		}

		public override IList getPendingBlockUpdates(Chunk p_72920_1_, bool p_72920_2_)
		{
			ArrayList var3 = null;
			ChunkCoordIntPair var4 = p_72920_1_.ChunkCoordIntPair;
			int var5 = (var4.chunkXPos << 4) - 2;
			int var6 = var5 + 16 + 2;
			int var7 = (var4.chunkZPos << 4) - 2;
			int var8 = var7 + 16 + 2;

			for (int var9 = 0; var9 < 2; ++var9)
			{
				IEnumerator var10;

				if (var9 == 0)
				{
					var10 = this.pendingTickListEntriesTreeSet.GetEnumerator();
				}
				else
				{
					var10 = this.pendingTickListEntriesThisTick.GetEnumerator();

					if (!this.pendingTickListEntriesThisTick.Count == 0)
					{
						logger.debug("toBeTicked = " + this.pendingTickListEntriesThisTick.Count);
					}
				}

				while (var10.MoveNext())
				{
					NextTickListEntry var11 = (NextTickListEntry)var10.Current;

					if (var11.xCoord >= var5 && var11.xCoord < var6 && var11.zCoord >= var7 && var11.zCoord < var8)
					{
						if (p_72920_2_)
						{
							this.pendingTickListEntriesHashSet.remove(var11);
							var10.remove();
						}

						if (var3 == null)
						{
							var3 = new ArrayList();
						}

						var3.Add(var11);
					}
				}
			}

			return var3;
		}

///    
///     <summary> * Will update the entity in the world if the chunk the entity is in is currently loaded or its forced to update.
///     * Args: entity, forceUpdate </summary>
///     
		public override void updateEntityWithOptionalForce(Entity p_72866_1_, bool p_72866_2_)
		{
			if (!this.mcServer.CanSpawnAnimals && (p_72866_1_ is EntityAnimal || p_72866_1_ is EntityWaterMob))
			{
				p_72866_1_.setDead();
			}

			if (!this.mcServer.CanSpawnNPCs && p_72866_1_ is INpc)
			{
				p_72866_1_.setDead();
			}

			base.updateEntityWithOptionalForce(p_72866_1_, p_72866_2_);
		}

///    
///     <summary> * Creates the chunk provider for this world. Called in the constructor. Retrieves provider from worldProvider? </summary>
///     
		protected internal override IChunkProvider createChunkProvider()
		{
			IChunkLoader var1 = this.saveHandler.getChunkLoader(this.provider);
			this.theChunkProviderServer = new ChunkProviderServer(this, var1, this.provider.createChunkGenerator());
			return this.theChunkProviderServer;
		}

		public virtual IList func_147486_a(int p_147486_1_, int p_147486_2_, int p_147486_3_, int p_147486_4_, int p_147486_5_, int p_147486_6_)
		{
			ArrayList var7 = new ArrayList();

			for (int var8 = 0; var8 < this.field_147482_g.Count; ++var8)
			{
				TileEntity var9 = (TileEntity)this.field_147482_g.get(var8);

				if (var9.field_145851_c >= p_147486_1_ && var9.field_145848_d >= p_147486_2_ && var9.field_145849_e >= p_147486_3_ && var9.field_145851_c < p_147486_4_ && var9.field_145848_d < p_147486_5_ && var9.field_145849_e < p_147486_6_)
				{
					var7.Add(var9);
				}
			}

			return var7;
		}

///    
///     <summary> * Called when checking if a certain block can be mined or not. The 'spawn safe zone' check is located here. </summary>
///     
		public override bool canMineBlock(EntityPlayer p_72962_1_, int p_72962_2_, int p_72962_3_, int p_72962_4_)
		{
			return !this.mcServer.isBlockProtected(this, p_72962_2_, p_72962_3_, p_72962_4_, p_72962_1_);
		}

		protected internal override void initialize(WorldSettings p_72963_1_)
		{
			if (this.entityIdMap == null)
			{
				this.entityIdMap = new IntHashMap();
			}

			if (this.pendingTickListEntriesHashSet == null)
			{
				this.pendingTickListEntriesHashSet = new HashSet();
			}

			if (this.pendingTickListEntriesTreeSet == null)
			{
				this.pendingTickListEntriesTreeSet = new TreeSet();
			}

			this.createSpawnPosition(p_72963_1_);
			base.initialize(p_72963_1_);
		}

///    
///     <summary> * creates a spawn position at random within 256 blocks of 0,0 </summary>
///     
		protected internal virtual void createSpawnPosition(WorldSettings p_73052_1_)
		{
			if (!this.provider.canRespawnHere())
			{
				this.worldInfo.setSpawnPosition(0, this.provider.AverageGroundLevel, 0);
			}
			else
			{
				this.findingSpawnPoint = true;
				WorldChunkManager var2 = this.provider.worldChunkMgr;
				IList var3 = var2.BiomesToSpawnIn;
				Random var4 = new Random(this.Seed);
				ChunkPosition var5 = var2.func_150795_a(0, 0, 256, var3, var4);
				int var6 = 0;
				int var7 = this.provider.AverageGroundLevel;
				int var8 = 0;

				if (var5 != null)
				{
					var6 = var5.field_151329_a;
					var8 = var5.field_151328_c;
				}
				else
				{
					logger.warn("Unable to find spawn biome");
				}

				int var9 = 0;

				while (!this.provider.canCoordinateBeSpawn(var6, var8))
				{
					var6 += var4.Next(64) - var4.Next(64);
					var8 += var4.Next(64) - var4.Next(64);
					++var9;

					if (var9 == 1000)
					{
						break;
					}
				}

				this.worldInfo.setSpawnPosition(var6, var7, var8);
				this.findingSpawnPoint = false;

				if (p_73052_1_.BonusChestEnabled)
				{
					this.createBonusChest();
				}
			}
		}

///    
///     <summary> * Creates the bonus chest in the world. </summary>
///     
		protected internal virtual void createBonusChest()
		{
			WorldGeneratorBonusChest var1 = new WorldGeneratorBonusChest(bonusChestContent, 10);

			for (int var2 = 0; var2 < 10; ++var2)
			{
				int var3 = this.worldInfo.SpawnX + this.rand.Next(6) - this.rand.Next(6);
				int var4 = this.worldInfo.SpawnZ + this.rand.Next(6) - this.rand.Next(6);
				int var5 = this.getTopSolidOrLiquidBlock(var3, var4) + 1;

				if (var1.generate(this, this.rand, var3, var5, var4))
				{
					break;
				}
			}
		}

///    
///     <summary> * Gets the hard-coded portal location to use when entering this dimension. </summary>
///     
		public virtual ChunkCoordinates EntrancePortalLocation
		{
			get
			{
				return this.provider.EntrancePortalLocation;
			}
		}

///    
///     <summary> * Saves all chunks to disk while updating progress bar. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void saveAllChunks(boolean p_73044_1_, IProgressUpdate p_73044_2_) throws MinecraftException
		public virtual void saveAllChunks(bool p_73044_1_, IProgressUpdate p_73044_2_)
		{
			if (this.chunkProvider.canSave())
			{
				if (p_73044_2_ != null)
				{
					p_73044_2_.displayProgressMessage("Saving level");
				}

				this.saveLevel();

				if (p_73044_2_ != null)
				{
					p_73044_2_.resetProgresAndWorkingMessage("Saving chunks");
				}

				this.chunkProvider.saveChunks(p_73044_1_, p_73044_2_);
				ArrayList var3 = Lists.newArrayList(this.theChunkProviderServer.func_152380_a());
				IEnumerator var4 = var3.GetEnumerator();

				while (var4.MoveNext())
				{
					Chunk var5 = (Chunk)var4.Current;

					if (var5 != null && !this.thePlayerManager.func_152621_a(var5.xPosition, var5.zPosition))
					{
						this.theChunkProviderServer.unloadChunksIfNotNearSpawn(var5.xPosition, var5.zPosition);
					}
				}
			}
		}

///    
///     <summary> * saves chunk data - currently only called during execution of the Save All command </summary>
///     
		public virtual void saveChunkData()
		{
			if (this.chunkProvider.canSave())
			{
				this.chunkProvider.saveExtraData();
			}
		}

///    
///     <summary> * Saves the chunks to disk. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void saveLevel() throws MinecraftException
		protected internal virtual void saveLevel()
		{
			this.checkSessionLock();
			this.saveHandler.saveWorldInfoWithPlayer(this.worldInfo, this.mcServer.ConfigurationManager.HostPlayerData);
			this.mapStorage.saveAllData();
		}

		protected internal override void onEntityAdded(Entity p_72923_1_)
		{
			base.onEntityAdded(p_72923_1_);
			this.entityIdMap.addKey(p_72923_1_.EntityId, p_72923_1_);
			Entity[] var2 = p_72923_1_.Parts;

			if (var2 != null)
			{
				for (int var3 = 0; var3 < var2.Length; ++var3)
				{
					this.entityIdMap.addKey(var2[var3].EntityId, var2[var3]);
				}
			}
		}

		protected internal override void onEntityRemoved(Entity p_72847_1_)
		{
			base.onEntityRemoved(p_72847_1_);
			this.entityIdMap.removeObject(p_72847_1_.EntityId);
			Entity[] var2 = p_72847_1_.Parts;

			if (var2 != null)
			{
				for (int var3 = 0; var3 < var2.Length; ++var3)
				{
					this.entityIdMap.removeObject(var2[var3].EntityId);
				}
			}
		}

///    
///     <summary> * Returns the Entity with the given ID, or null if it doesn't exist in this World. </summary>
///     
		public override Entity getEntityByID(int p_73045_1_)
		{
			return (Entity)this.entityIdMap.lookup(p_73045_1_);
		}

///    
///     <summary> * adds a lightning bolt to the list of lightning bolts in this world. </summary>
///     
		public override bool addWeatherEffect(Entity p_72942_1_)
		{
			if (base.addWeatherEffect(p_72942_1_))
			{
				this.mcServer.ConfigurationManager.func_148541_a(p_72942_1_.posX, p_72942_1_.posY, p_72942_1_.posZ, 512.0D, this.provider.dimensionId, new S2CPacketSpawnGlobalEntity(p_72942_1_));
				return true;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * sends a Packet 38 (Entity Status) to all tracked players of that entity </summary>
///     
		public override void setEntityState(Entity p_72960_1_, sbyte p_72960_2_)
		{
			this.EntityTracker.func_151248_b(p_72960_1_, new S19PacketEntityStatus(p_72960_1_, p_72960_2_));
		}

///    
///     <summary> * returns a new explosion. Does initiation (at time of writing Explosion is not finished) </summary>
///     
		public override Explosion newExplosion(Entity p_72885_1_, double p_72885_2_, double p_72885_4_, double p_72885_6_, float p_72885_8_, bool p_72885_9_, bool p_72885_10_)
		{
			Explosion var11 = new Explosion(this, p_72885_1_, p_72885_2_, p_72885_4_, p_72885_6_, p_72885_8_);
			var11.isFlaming = p_72885_9_;
			var11.isSmoking = p_72885_10_;
			var11.doExplosionA();
			var11.doExplosionB(false);

			if (!p_72885_10_)
			{
				var11.affectedBlockPositions.Clear();
			}

			IEnumerator var12 = this.playerEntities.GetEnumerator();

			while (var12.MoveNext())
			{
				EntityPlayer var13 = (EntityPlayer)var12.Current;

				if (var13.getDistanceSq(p_72885_2_, p_72885_4_, p_72885_6_) < 4096.0D)
				{
					((EntityPlayerMP)var13).playerNetServerHandler.sendPacket(new S27PacketExplosion(p_72885_2_, p_72885_4_, p_72885_6_, p_72885_8_, var11.affectedBlockPositions, (Vec3)var11.func_77277_b()[var13]));
				}
			}

			return var11;
		}

		public override void func_147452_c(int p_147452_1_, int p_147452_2_, int p_147452_3_, Block p_147452_4_, int p_147452_5_, int p_147452_6_)
		{
			BlockEventData var7 = new BlockEventData(p_147452_1_, p_147452_2_, p_147452_3_, p_147452_4_, p_147452_5_, p_147452_6_);
			IEnumerator var8 = this.field_147490_S[this.field_147489_T].GetEnumerator();
			BlockEventData var9;

			do
			{
				if (!var8.MoveNext())
				{
					this.field_147490_S[this.field_147489_T].add(var7);
					return;
				}

				var9 = (BlockEventData)var8.Current;
			}
			while (!var9.Equals(var7));
		}

		private void func_147488_Z()
		{
			while (!this.field_147490_S[this.field_147489_T].Empty)
			{
				int var1 = this.field_147489_T;
				this.field_147489_T ^= 1;
				IEnumerator var2 = this.field_147490_S[var1].GetEnumerator();

				while (var2.MoveNext())
				{
					BlockEventData var3 = (BlockEventData)var2.Current;

					if (this.func_147485_a(var3))
					{
						this.mcServer.ConfigurationManager.func_148541_a((double)var3.func_151340_a(), (double)var3.func_151342_b(), (double)var3.func_151341_c(), 64.0D, this.provider.dimensionId, new S24PacketBlockAction(var3.func_151340_a(), var3.func_151342_b(), var3.func_151341_c(), var3.Block, var3.EventID, var3.EventParameter));
					}
				}

				this.field_147490_S[var1].clear();
			}
		}

		private bool func_147485_a(BlockEventData p_147485_1_)
		{
			Block var2 = this.getBlock(p_147485_1_.func_151340_a(), p_147485_1_.func_151342_b(), p_147485_1_.func_151341_c());
			return var2 == p_147485_1_.Block ? var2.onBlockEventReceived(this, p_147485_1_.func_151340_a(), p_147485_1_.func_151342_b(), p_147485_1_.func_151341_c(), p_147485_1_.EventID, p_147485_1_.EventParameter) : false;
		}

///    
///     <summary> * Syncs all changes to disk and wait for completion. </summary>
///     
		public virtual void flush()
		{
			this.saveHandler.flush();
		}

///    
///     <summary> * Updates all weather states. </summary>
///     
		protected internal override void updateWeather()
		{
			bool var1 = this.Raining;
			base.updateWeather();

			if (this.prevRainingStrength != this.rainingStrength)
			{
				this.mcServer.ConfigurationManager.func_148537_a(new S2BPacketChangeGameState(7, this.rainingStrength), this.provider.dimensionId);
			}

			if (this.prevThunderingStrength != this.thunderingStrength)
			{
				this.mcServer.ConfigurationManager.func_148537_a(new S2BPacketChangeGameState(8, this.thunderingStrength), this.provider.dimensionId);
			}

			if (var1 != this.Raining)
			{
				if (var1)
				{
					this.mcServer.ConfigurationManager.func_148540_a(new S2BPacketChangeGameState(2, 0.0F));
				}
				else
				{
					this.mcServer.ConfigurationManager.func_148540_a(new S2BPacketChangeGameState(1, 0.0F));
				}

				this.mcServer.ConfigurationManager.func_148540_a(new S2BPacketChangeGameState(7, this.rainingStrength));
				this.mcServer.ConfigurationManager.func_148540_a(new S2BPacketChangeGameState(8, this.thunderingStrength));
			}
		}

		protected internal override int func_152379_p()
		{
			return this.mcServer.ConfigurationManager.ViewDistance;
		}

		public virtual MinecraftServer func_73046_m()
		{
			return this.mcServer;
		}

///    
///     <summary> * Gets the EntityTracker </summary>
///     
		public virtual EntityTracker EntityTracker
		{
			get
			{
				return this.theEntityTracker;
			}
		}

		public virtual PlayerManager PlayerManager
		{
			get
			{
				return this.thePlayerManager;
			}
		}

		public virtual Teleporter DefaultTeleporter
		{
			get
			{
				return this.worldTeleporter;
			}
		}

		public virtual void func_147487_a(string p_147487_1_, double p_147487_2_, double p_147487_4_, double p_147487_6_, int p_147487_8_, double p_147487_9_, double p_147487_11_, double p_147487_13_, double p_147487_15_)
		{
			S2APacketParticles var17 = new S2APacketParticles(p_147487_1_, (float)p_147487_2_, (float)p_147487_4_, (float)p_147487_6_, (float)p_147487_9_, (float)p_147487_11_, (float)p_147487_13_, (float)p_147487_15_, p_147487_8_);

			for (int var18 = 0; var18 < this.playerEntities.Count; ++var18)
			{
				EntityPlayerMP var19 = (EntityPlayerMP)this.playerEntities.get(var18);
				ChunkCoordinates var20 = var19.PlayerCoordinates;
				double var21 = p_147487_2_ - (double)var20.posX;
				double var23 = p_147487_4_ - (double)var20.posY;
				double var25 = p_147487_6_ - (double)var20.posZ;
				double var27 = var21 * var21 + var23 * var23 + var25 * var25;

				if (var27 <= 256.0D)
				{
					var19.playerNetServerHandler.sendPacket(var17);
				}
			}
		}

		internal class ServerBlockEventList : ArrayList
		{
			

			private ServerBlockEventList()
			{
			}

			internal ServerBlockEventList(object p_i1521_1_) : this()
			{
			}
		}
	}

}