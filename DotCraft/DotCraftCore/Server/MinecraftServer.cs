using System;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Threading;

namespace DotCraftCore.Server
{

	using Charsets = com.google.common.base.Charsets;
	using GameProfile = com.mojang.authlib.GameProfile;
	using GameProfileRepository = com.mojang.authlib.GameProfileRepository;
	using MinecraftSessionService = com.mojang.authlib.minecraft.MinecraftSessionService;
	using YggdrasilAuthenticationService = com.mojang.authlib.yggdrasil.YggdrasilAuthenticationService;
	using ByteBuf = io.netty.buffer.ByteBuf;
	using ByteBufOutputStream = io.netty.buffer.ByteBufOutputStream;
	using Unpooled = io.netty.buffer.Unpooled;
	using Base64 = io.netty.handler.codec.base64.Base64;
	using ImageIO = javax.imageio.ImageIO;
	using CommandBase = DotCraftCore.command.CommandBase;
	using ICommandManager = DotCraftCore.command.ICommandManager;
	using ICommandSender = DotCraftCore.command.ICommandSender;
	using ServerCommandManager = DotCraftCore.command.ServerCommandManager;
	using CrashReport = DotCraftCore.crash.CrashReport;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using EntityPlayerMP = DotCraftCore.entity.player.EntityPlayerMP;
	using NetworkSystem = DotCraftCore.network.NetworkSystem;
	using ServerStatusResponse = DotCraftCore.network.ServerStatusResponse;
	using S03PacketTimeUpdate = DotCraftCore.network.play.server.S03PacketTimeUpdate;
	using IPlayerUsage = DotCraftCore.profiler.IPlayerUsage;
	using PlayerUsageSnooper = DotCraftCore.profiler.PlayerUsageSnooper;
	using Profiler = DotCraftCore.profiler.Profiler;
	using IUpdatePlayerListBox = DotCraftCore.Server.GUI.IUpdatePlayerListBox;
	using PlayerProfileCache = DotCraftCore.Server.Management.PlayerProfileCache;
	using ServerConfigurationManager = DotCraftCore.Server.Management.ServerConfigurationManager;
	using ChatComponentText = DotCraftCore.Util.ChatComponentText;
	using ChunkCoordinates = DotCraftCore.Util.ChunkCoordinates;
	using IChatComponent = DotCraftCore.Util.IChatComponent;
	using IProgressUpdate = DotCraftCore.Util.IProgressUpdate;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using ReportedException = DotCraftCore.Util.ReportedException;
	using EnumDifficulty = DotCraftCore.World.EnumDifficulty;
	using MinecraftException = DotCraftCore.World.MinecraftException;
	using World = DotCraftCore.World.World;
	using WorldManager = DotCraftCore.World.WorldManager;
	using WorldServer = DotCraftCore.World.WorldServer;
	using WorldServerMulti = DotCraftCore.World.WorldServerMulti;
	using WorldSettings = DotCraftCore.World.WorldSettings;
	using WorldType = DotCraftCore.World.WorldType;
	using AnvilSaveConverter = DotCraftCore.World.Chunk.Storage.AnvilSaveConverter;
	using DemoWorldServer = DotCraftCore.World.Demo.DemoWorldServer;
	using ISaveFormat = DotCraftCore.World.Storage.ISaveFormat;
	using ISaveHandler = DotCraftCore.World.Storage.ISaveHandler;
	using WorldInfo = DotCraftCore.World.Storage.WorldInfo;
	using Validate = org.apache.commons.lang3.Validate;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public abstract class MinecraftServer : ICommandSender, Runnable, IPlayerUsage
	{
		private static readonly Logger logger = LogManager.Logger;
		public static readonly File field_152367_a = new File("usercache.json");

	/// <summary> Instance of Minecraft Server.  </summary>
		private static MinecraftServer mcServer;
		private readonly ISaveFormat anvilConverterForAnvilFile;

	/// <summary> The PlayerUsageSnooper instance.  </summary>
		private readonly PlayerUsageSnooper usageSnooper = new PlayerUsageSnooper("server", this, SystemTimeMillis);
		private readonly File anvilFile;

///    
///     <summary> * Collection of objects to update every tick. Type: List<IUpdatePlayerListBox> </summary>
///     
		private readonly IList tickables = new ArrayList();
		private readonly ICommandManager commandManager;
		public readonly Profiler theProfiler = new Profiler();
		private readonly NetworkSystem field_147144_o;
		private readonly ServerStatusResponse field_147147_p = new ServerStatusResponse();
		private readonly Random field_147146_q = new Random();

	/// <summary> The server's port.  </summary>
		private int serverPort = -1;

	/// <summary> The server world instances.  </summary>
		public WorldServer[] worldServers;

	/// <summary> The ServerConfigurationManager instance.  </summary>
		private ServerConfigurationManager serverConfigManager;

///    
///     <summary> * Indicates whether the server is running or not. Set to false to initiate a shutdown. </summary>
///     
		private bool serverRunning = true;

	/// <summary> Indicates to other classes that the server is safely stopped.  </summary>
		private bool serverStopped;

	/// <summary> Incremented every tick.  </summary>
		private int tickCounter;
		protected internal readonly Proxy serverProxy;

///    
///     <summary> * The task the server is currently working on(and will output on outputPercentRemaining). </summary>
///     
		public string currentTask;

	/// <summary> The percentage of the current task finished so far.  </summary>
		public int percentDone;

	/// <summary> True if the server is in online mode.  </summary>
		private bool onlineMode;

	/// <summary> True if the server has animals turned on.  </summary>
		private bool canSpawnAnimals;
		private bool canSpawnNPCs;

	/// <summary> Indicates whether PvP is active on the server or not.  </summary>
		private bool pvpEnabled;

	/// <summary> Determines if flight is allowed or not.  </summary>
		private bool allowFlight;

	/// <summary> The server MOTD string.  </summary>
		private string motd;

	/// <summary> Maximum build height.  </summary>
		private int buildLimit;
		private int field_143008_E = 0;
		public readonly long[] tickTimeArray = new long[100];

	/// <summary> Stats are [dimension][tick%100] system.nanoTime is stored.  </summary>
		public long[][] timeOfLastDimensionTick;
		private KeyPair serverKeyPair;

	/// <summary> Username of the server owner (for integrated servers)  </summary>
		private string serverOwner;
		private string folderName;
		private string worldName;
		private bool isDemo;
		private bool enableBonusChest;

///    
///     <summary> * If true, there is no need to save chunks or stop the server, because that is already being done. </summary>
///     
		private bool worldIsBeingDeleted;
		private string field_147141_M = "";
		private bool serverIsRunning;

///    
///     <summary> * Set when warned for "Can't keep up", which triggers again after 15 seconds. </summary>
///     
		private long timeOfLastWarning;
		private string userMessage;
		private bool startProfiling;
		private bool isGamemodeForced;
		private readonly YggdrasilAuthenticationService field_152364_T;
		private readonly MinecraftSessionService field_147143_S;
		private long field_147142_T = 0L;
		private readonly GameProfileRepository field_152365_W;
		private readonly PlayerProfileCache field_152366_X;
		private const string __OBFID = "CL_00001462";

		public MinecraftServer(File p_i45281_1_, Proxy p_i45281_2_)
		{
			this.field_152366_X = new PlayerProfileCache(this, field_152367_a);
			mcServer = this;
			this.serverProxy = p_i45281_2_;
			this.anvilFile = p_i45281_1_;
			this.field_147144_o = new NetworkSystem(this);
			this.commandManager = new ServerCommandManager();
			this.anvilConverterForAnvilFile = new AnvilSaveConverter(p_i45281_1_);
			this.field_152364_T = new YggdrasilAuthenticationService(p_i45281_2_, UUID.randomUUID().ToString());
			this.field_147143_S = this.field_152364_T.createMinecraftSessionService();
			this.field_152365_W = this.field_152364_T.createProfileRepository();
		}

///    
///     <summary> * Initialises the server and starts it. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected abstract boolean startServer() throws IOException;
		protected internal abstract bool startServer();

		protected internal virtual void convertMapIfNeeded(string p_71237_1_)
		{
			if(this.ActiveAnvilConverter.isOldMapFormat(p_71237_1_))
			{
				logger.info("Converting map!");
				this.UserMessage = "menu.convertingLevel";
				this.ActiveAnvilConverter.convertMapFormat(p_71237_1_, new IProgressUpdate() { private long field_96245_b = System.currentTimeMillis(); private static final string __OBFID = "CL_00001417"; public void displayProgressMessage(string p_73720_1_) {} public void resetProgressAndMessage(string p_73721_1_) {} public void setLoadingProgress(int p_73718_1_) { if(System.currentTimeMillis() - this.field_96245_b >= 1000L) { this.field_96245_b = System.currentTimeMillis(); MinecraftServer.logger.info("Converting... " + p_73718_1_ + "%"); } } public void func_146586_a() {} public void resetProgresAndWorkingMessage(string p_73719_1_) {} });
			}
		}

///    
///     <summary> * Typically "menu.convertingLevel", "menu.loadingLevel" or others. </summary>
///     
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected internal virtual string UserMessage
		{
			set
			{
				this.userMessage = value;
			}
			get
			{
				return this.userMessage;
			}
		}

		[MethodImpl(MethodImplOptions.Synchronized)]

		protected internal virtual void loadAllWorlds(string p_71247_1_, string p_71247_2_, long p_71247_3_, WorldType p_71247_5_, string p_71247_6_)
		{
			this.convertMapIfNeeded(p_71247_1_);
			this.UserMessage = "menu.loadingLevel";
			this.worldServers = new WorldServer[3];
//ORIGINAL LINE: this.timeOfLastDimensionTick = new long[this.worldServers.Length][100];
//JAVA TO VB & C# CONVERTER NOTE: The following call to the 'RectangularArrays' helper class reproduces the rectangular array initialization that is automatic in Java:
			this.timeOfLastDimensionTick = RectangularArrays.ReturnRectangularLongArray(this.worldServers.Length, 100);
			ISaveHandler var7 = this.anvilConverterForAnvilFile.getSaveLoader(p_71247_1_, true);
			WorldInfo var9 = var7.loadWorldInfo();
			WorldSettings var8;

			if(var9 == null)
			{
				var8 = new WorldSettings(p_71247_3_, this.GameType, this.canStructuresSpawn(), this.Hardcore, p_71247_5_);
				var8.func_82750_a(p_71247_6_);
			}
			else
			{
				var8 = new WorldSettings(var9);
			}

			if(this.enableBonusChest)
			{
				var8.enableBonusChest();
			}

			for(int var10 = 0; var10 < this.worldServers.Length; ++var10)
			{
				sbyte var11 = 0;

				if(var10 == 1)
				{
					var11 = -1;
				}

				if(var10 == 2)
				{
					var11 = 1;
				}

				if(var10 == 0)
				{
					if(this.Demo)
					{
						this.worldServers[var10] = new DemoWorldServer(this, var7, p_71247_2_, var11, this.theProfiler);
					}
					else
					{
						this.worldServers[var10] = new WorldServer(this, var7, p_71247_2_, var11, var8, this.theProfiler);
					}
				}
				else
				{
					this.worldServers[var10] = new WorldServerMulti(this, var7, p_71247_2_, var11, var8, this.worldServers[0], this.theProfiler);
				}

				this.worldServers[var10].addWorldAccess(new WorldManager(this, this.worldServers[var10]));

				if(!this.SinglePlayer)
				{
					this.worldServers[var10].WorldInfo.GameType = this.GameType;
				}

				this.serverConfigManager.PlayerManager = this.worldServers;
			}

			this.func_147139_a(this.func_147135_j());
			this.initialWorldChunkLoad();
		}

		protected internal virtual void initialWorldChunkLoad()
		{
			bool var1 = true;
			bool var2 = true;
			bool var3 = true;
			bool var4 = true;
			int var5 = 0;
			this.UserMessage = "menu.generatingTerrain";
			sbyte var6 = 0;
			logger.info("Preparing start region for level " + var6);
			WorldServer var7 = this.worldServers[var6];
			ChunkCoordinates var8 = var7.SpawnPoint;
			long var9 = SystemTimeMillis;

			for(int var11 = -192; var11 <= 192 && this.ServerRunning; var11 += 16)
			{
				for(int var12 = -192; var12 <= 192 && this.ServerRunning; var12 += 16)
				{
					long var13 = SystemTimeMillis;

					if(var13 - var9 > 1000L)
					{
						this.outputPercentRemaining("Preparing spawn area", var5 * 100 / 625);
						var9 = var13;
					}

					++var5;
					var7.theChunkProviderServer.loadChunk(var8.posX + var11 >> 4, var8.posZ + var12 >> 4);
				}
			}

			this.clearCurrentTask();
		}

		public abstract bool canStructuresSpawn();

		public abstract WorldSettings.GameType GameType {get;}

		public abstract EnumDifficulty func_147135_j();

///    
///     <summary> * Defaults to false. </summary>
///     
		public abstract bool isHardcore() {get;}

		public abstract int func_110455_j();

		public abstract bool func_152363_m();

///    
///     <summary> * Used to display a percent remaining given text and the percentage. </summary>
///     
		protected internal virtual void outputPercentRemaining(string p_71216_1_, int p_71216_2_)
		{
			this.currentTask = p_71216_1_;
			this.percentDone = p_71216_2_;
			logger.info(p_71216_1_ + ": " + p_71216_2_ + "%");
		}

///    
///     <summary> * Set current task to null and set its percentage to 0. </summary>
///     
		protected internal virtual void clearCurrentTask()
		{
			this.currentTask = null;
			this.percentDone = 0;
		}

///    
///     <summary> * par1 indicates if a log message should be output. </summary>
///     
		protected internal virtual void saveAllWorlds(bool p_71267_1_)
		{
			if(!this.worldIsBeingDeleted)
			{
				WorldServer[] var2 = this.worldServers;
				int var3 = var2.Length;

				for(int var4 = 0; var4 < var3; ++var4)
				{
					WorldServer var5 = var2[var4];

					if(var5 != null)
					{
						if(!p_71267_1_)
						{
							logger.info("Saving chunks for level \'" + var5.WorldInfo.WorldName + "\'/" + var5.provider.DimensionName);
						}

						try
						{
							var5.saveAllChunks(true, (IProgressUpdate)null);
						}
						catch (MinecraftException var7)
						{
							logger.warn(var7.Message);
						}
					}
				}
			}
		}

///    
///     <summary> * Saves all necessary data as preparation for stopping the server. </summary>
///     
		public virtual void stopServer()
		{
			if(!this.worldIsBeingDeleted)
			{
				logger.info("Stopping server");

				if(this.func_147137_ag() != null)
				{
					this.func_147137_ag().terminateEndpoints();
				}

				if(this.serverConfigManager != null)
				{
					logger.info("Saving players");
					this.serverConfigManager.saveAllPlayerData();
					this.serverConfigManager.removeAllPlayers();
				}

				if(this.worldServers != null)
				{
					logger.info("Saving worlds");
					this.saveAllWorlds(false);

					for(int var1 = 0; var1 < this.worldServers.Length; ++var1)
					{
						WorldServer var2 = this.worldServers[var1];
						var2.flush();
					}
				}

				if(this.usageSnooper.SnooperRunning)
				{
					this.usageSnooper.stopSnooper();
				}
			}
		}

		public virtual bool isServerRunning()
		{
			get
			{
				return this.serverRunning;
			}
		}

///    
///     <summary> * Sets the serverRunning variable to false, in order to get the server to shut down. </summary>
///     
		public virtual void initiateShutdown()
		{
			this.serverRunning = false;
		}

		public virtual void run()
		{
			try
			{
				if(this.startServer())
				{
					long var1 = SystemTimeMillis;
					long var50 = 0L;
					this.field_147147_p.func_151315_a(new ChatComponentText(this.motd));
					this.field_147147_p.func_151321_a(new ServerStatusResponse.MinecraftProtocolVersionIdentifier("1.7.10", 5));
					this.func_147138_a(this.field_147147_p);

					while(this.serverRunning)
					{
						long var5 = SystemTimeMillis;
						long var7 = var5 - var1;

						if(var7 > 2000L && var1 - this.timeOfLastWarning >= 15000L)
						{
							logger.warn("Can\'t keep up! Did the system time change, or is the server overloaded? Running {}ms behind, skipping {} tick(s)", new object[] {Convert.ToInt64(var7), Convert.ToInt64(var7 / 50L)});
							var7 = 2000L;
							this.timeOfLastWarning = var1;
						}

						if(var7 < 0L)
						{
							logger.warn("Time ran backwards! Did the system time change?");
							var7 = 0L;
						}

						var50 += var7;
						var1 = var5;

						if(this.worldServers[0].areAllPlayersAsleep())
						{
							this.tick();
							var50 = 0L;
						}
						else
						{
							while(var50 > 50L)
							{
								var50 -= 50L;
								this.tick();
							}
						}

						Thread.Sleep(Math.Max(1L, 50L - var50));
						this.serverIsRunning = true;
					}
				}
				else
				{
					this.finalTick((CrashReport)null);
				}
			}
			catch (Exception var48)
			{
				logger.error("Encountered an unexpected exception", var48);
				CrashReport var2 = null;

				if(var48 is ReportedException)
				{
					var2 = this.addServerInfoToCrashReport(((ReportedException)var48).CrashReport);
				}
				else
				{
					var2 = this.addServerInfoToCrashReport(new CrashReport("Exception in server tick loop", var48));
				}

				File var3 = new File(new File(this.DataDirectory, "crash-reports"), "crash-" + (new SimpleDateFormat("yyyy-MM-dd_HH.mm.ss")).format(DateTime.Now) + "-server.txt");

				if(var2.saveToFile(var3))
				{
					logger.error("This crash report has been saved to: " + var3.AbsolutePath);
				}
				else
				{
					logger.error("We were unable to save this crash report to disk.");
				}

				this.finalTick(var2);
			}
			finally
			{
				try
				{
					this.stopServer();
					this.serverStopped = true;
				}
				catch (Exception var46)
				{
					logger.error("Exception stopping the server", var46);
				}
				finally
				{
					this.systemExitNow();
				}
			}
		}

		private void func_147138_a(ServerStatusResponse p_147138_1_)
		{
			File var2 = this.getFile("server-icon.png");

			if(var2.File)
			{
				ByteBuf var3 = Unpooled.buffer();

				try
				{
					BufferedImage var4 = ImageIO.read(var2);
					Validate.validState(var4.Width == 64, "Must be 64 pixels wide", new object[0]);
					Validate.validState(var4.Height == 64, "Must be 64 pixels high", new object[0]);
					ImageIO.write(var4, "PNG", new ByteBufOutputStream(var3));
					ByteBuf var5 = Base64.encode(var3);
					p_147138_1_.func_151320_a("data:image/png;base64," + var5.ToString(Charsets.UTF_8));
				}
				catch (Exception var9)
				{
					logger.error("Couldn\'t load server icon", var9);
				}
				finally
				{
					var3.release();
				}
			}
		}

		protected internal virtual File DataDirectory
		{
			get
			{
				return new File(".");
			}
		}

///    
///     <summary> * Called on exit from the main run() loop. </summary>
///     
		protected internal virtual void finalTick(CrashReport p_71228_1_)
		{
		}

///    
///     <summary> * Directly calls System.exit(0), instantly killing the program. </summary>
///     
		protected internal virtual void systemExitNow()
		{
		}

///    
///     <summary> * Main function called by run() every loop. </summary>
///     
		public virtual void tick()
		{
			long var1 = System.nanoTime();
			++this.tickCounter;

			if(this.startProfiling)
			{
				this.startProfiling = false;
				this.theProfiler.profilingEnabled = true;
				this.theProfiler.clearProfiling();
			}

			this.theProfiler.startSection("root");
			this.updateTimeLightAndEntities();

			if(var1 - this.field_147142_T >= 5000000000L)
			{
				this.field_147142_T = var1;
				this.field_147147_p.func_151319_a(new ServerStatusResponse.PlayerCountData(this.MaxPlayers, this.CurrentPlayerCount));
				GameProfile[] var3 = new GameProfile[Math.Min(this.CurrentPlayerCount, 12)];
				int var4 = MathHelper.getRandomIntegerInRange(this.field_147146_q, 0, this.CurrentPlayerCount - var3.Length);

				for(int var5 = 0; var5 < var3.Length; ++var5)
				{
					var3[var5] = ((EntityPlayerMP)this.serverConfigManager.playerEntityList[var4 + var5]).GameProfile;
				}

				Collections.shuffle(var3);
				this.field_147147_p.func_151318_b().func_151330_a(var3);
			}

			if(this.tickCounter % 900 == 0)
			{
				this.theProfiler.startSection("save");
				this.serverConfigManager.saveAllPlayerData();
				this.saveAllWorlds(true);
				this.theProfiler.endSection();
			}

			this.theProfiler.startSection("tallying");
			this.tickTimeArray[this.tickCounter % 100] = System.nanoTime() - var1;
			this.theProfiler.endSection();
			this.theProfiler.startSection("snooper");

			if(!this.usageSnooper.SnooperRunning && this.tickCounter > 100)
			{
				this.usageSnooper.startSnooper();
			}

			if(this.tickCounter % 6000 == 0)
			{
				this.usageSnooper.addMemoryStatsToSnooper();
			}

			this.theProfiler.endSection();
			this.theProfiler.endSection();
		}

		public virtual void updateTimeLightAndEntities()
		{
			this.theProfiler.startSection("levels");
			int var1;

			for(var1 = 0; var1 < this.worldServers.Length; ++var1)
			{
				long var2 = System.nanoTime();

				if(var1 == 0 || this.AllowNether)
				{
					WorldServer var4 = this.worldServers[var1];
					this.theProfiler.startSection(var4.WorldInfo.WorldName);
					this.theProfiler.startSection("pools");
					this.theProfiler.endSection();

					if(this.tickCounter % 20 == 0)
					{
						this.theProfiler.startSection("timeSync");
						this.serverConfigManager.func_148537_a(new S03PacketTimeUpdate(var4.TotalWorldTime, var4.WorldTime, var4.GameRules.getGameRuleBooleanValue("doDaylightCycle")), var4.provider.dimensionId);
						this.theProfiler.endSection();
					}

					this.theProfiler.startSection("tick");
					CrashReport var6;

					try
					{
						var4.tick();
					}
					catch (Exception var8)
					{
						var6 = CrashReport.makeCrashReport(var8, "Exception ticking world");
						var4.addWorldInfoToCrashReport(var6);
						throw new ReportedException(var6);
					}

					try
					{
						var4.updateEntities();
					}
					catch (Exception var7)
					{
						var6 = CrashReport.makeCrashReport(var7, "Exception ticking world entities");
						var4.addWorldInfoToCrashReport(var6);
						throw new ReportedException(var6);
					}

					this.theProfiler.endSection();
					this.theProfiler.startSection("tracker");
					var4.EntityTracker.updateTrackedEntities();
					this.theProfiler.endSection();
					this.theProfiler.endSection();
				}

				this.timeOfLastDimensionTick[var1][this.tickCounter % 100] = System.nanoTime() - var2;
			}

			this.theProfiler.endStartSection("connection");
			this.func_147137_ag().networkTick();
			this.theProfiler.endStartSection("players");
			this.serverConfigManager.sendPlayerInfoToAllPlayers();
			this.theProfiler.endStartSection("tickables");

			for(var1 = 0; var1 < this.tickables.Count; ++var1)
			{
				((IUpdatePlayerListBox)this.tickables.get(var1)).update();
			}

			this.theProfiler.endSection();
		}

		public virtual bool AllowNether
		{
			get
			{
				return true;
			}
		}

		public virtual void startServerThread()
		{
			(new Thread("Server thread") { private static final string __OBFID = "CL_00001418"; public void run() { MinecraftServer.run(); } }).start();
		}

///    
///     <summary> * Returns a File object from the specified string. </summary>
///     
		public virtual File getFile(string p_71209_1_)
		{
			return new File(this.DataDirectory, p_71209_1_);
		}

///    
///     <summary> * Logs the message with a level of WARN. </summary>
///     
		public virtual void logWarning(string p_71236_1_)
		{
			logger.warn(p_71236_1_);
		}

///    
///     <summary> * Gets the worldServer by the given dimension. </summary>
///     
		public virtual WorldServer worldServerForDimension(int p_71218_1_)
		{
			return p_71218_1_ == -1 ? this.worldServers[1] : (p_71218_1_ == 1 ? this.worldServers[2] : this.worldServers[0]);
		}

///    
///     <summary> * Returns the server's Minecraft version as string. </summary>
///     
		public virtual string MinecraftVersion
		{
			get
			{
				return "1.7.10";
			}
		}

///    
///     <summary> * Returns the number of players currently on the server. </summary>
///     
		public virtual int CurrentPlayerCount
		{
			get
			{
				return this.serverConfigManager.CurrentPlayerCount;
			}
		}

///    
///     <summary> * Returns the maximum number of players allowed on the server. </summary>
///     
		public virtual int MaxPlayers
		{
			get
			{
				return this.serverConfigManager.MaxPlayers;
			}
		}

///    
///     <summary> * Returns an array of the usernames of all the connected players. </summary>
///     
		public virtual string[] AllUsernames
		{
			get
			{
				return this.serverConfigManager.AllUsernames;
			}
		}

		public virtual GameProfile[] func_152357_F()
		{
			return this.serverConfigManager.func_152600_g();
		}

		public virtual string ServerModName
		{
			get
			{
				return "vanilla";
			}
		}

///    
///     <summary> * Adds the server info, including from theWorldServer, to the crash report. </summary>
///     
		public virtual CrashReport addServerInfoToCrashReport(CrashReport p_71230_1_)
		{
			p_71230_1_.Category.addCrashSectionCallable("Profiler Position", new Callable() { private static final string __OBFID = "CL_00001419"; public string call() { return MinecraftServer.theProfiler.profilingEnabled ? MinecraftServer.theProfiler.NameOfLastSection : "N/A (disabled)"; } });

			if(this.worldServers != null && this.worldServers.Length > 0 && this.worldServers[0] != null)
			{
				p_71230_1_.Category.addCrashSectionCallable("Vec3 Pool Size", new Callable() { private static final string __OBFID = "CL_00001420"; public string call() { sbyte var1 = 0; int var2 = 56 * var1; int var3 = var2 / 1024 / 1024; sbyte var4 = 0; int var5 = 56 * var4; int var6 = var5 / 1024 / 1024; return var1 + " (" + var2 + " bytes; " + var3 + " MB) allocated, " + var4 + " (" + var5 + " bytes; " + var6 + " MB) used"; } });
			}

			if(this.serverConfigManager != null)
			{
				p_71230_1_.Category.addCrashSectionCallable("Player Count", new Callable() { private static final string __OBFID = "CL_00001780"; public string call() { return MinecraftServer.serverConfigManager.CurrentPlayerCount + " / " + MinecraftServer.serverConfigManager.MaxPlayers + "; " + MinecraftServer.serverConfigManager.playerEntityList; } });
			}

			return p_71230_1_;
		}

///    
///     <summary> * If par2Str begins with /, then it searches for commands, otherwise it returns players. </summary>
///     
		public virtual IList getPossibleCompletions(ICommandSender p_71248_1_, string p_71248_2_)
		{
			ArrayList var3 = new ArrayList();

			if(p_71248_2_.StartsWith("/"))
			{
				p_71248_2_ = p_71248_2_.Substring(1);
				bool var10 = !p_71248_2_.Contains(" ");
				IList var11 = this.commandManager.getPossibleCommands(p_71248_1_, p_71248_2_);

				if(var11 != null)
				{
					IEnumerator var12 = var11.GetEnumerator();

					while(var12.MoveNext())
					{
						string var13 = (string)var12.Current;

						if(var10)
						{
							var3.Add("/" + var13);
						}
						else
						{
							var3.Add(var13);
						}
					}
				}

				return var3;
			}
			else
			{
				string[] var4 = StringHelperClass.StringSplit(p_71248_2_, " ", false);
				string var5 = var4[var4.Length - 1];
				string[] var6 = this.serverConfigManager.AllUsernames;
				int var7 = var6.Length;

				for(int var8 = 0; var8 < var7; ++var8)
				{
					string var9 = var6[var8];

					if(CommandBase.doesStringStartWith(var5, var9))
					{
						var3.Add(var9);
					}
				}

				return var3;
			}
		}

///    
///     <summary> * Gets mcServer. </summary>
///     
		public static MinecraftServer Server
		{
			get
			{
				return mcServer;
			}
		}

///    
///     <summary> * Gets the name of this command sender (usually username, but possibly "Rcon") </summary>
///     
		public virtual string CommandSenderName
		{
			get
			{
				return "Server";
			}
		}

///    
///     <summary> * Notifies this sender of some sort of information.  This is for messages intended to display to the user.  Used
///     * for typical output (like "you asked for whether or not this game rule is set, so here's your answer"), warnings
///     * (like "I fetched this block for you by ID, but I'd like you to know that every time you do this, I die a little
///     * inside"), and errors (like "it's not called iron_pixacke, silly"). </summary>
///     
		public virtual void addChatMessage(IChatComponent p_145747_1_)
		{
			logger.info(p_145747_1_.UnformattedText);
		}

///    
///     <summary> * Returns true if the command sender is allowed to use the given command. </summary>
///     
		public virtual bool canCommandSenderUseCommand(int p_70003_1_, string p_70003_2_)
		{
			return true;
		}

		public virtual ICommandManager CommandManager
		{
			get
			{
				return this.commandManager;
			}
		}

///    
///     <summary> * Gets KeyPair instanced in MinecraftServer. </summary>
///     
		public virtual KeyPair KeyPair
		{
			get
			{
				return this.serverKeyPair;
			}
			set
			{
				this.serverKeyPair = value;
			}
		}

///    
///     <summary> * Returns the username of the server owner (for integrated servers) </summary>
///     
		public virtual string ServerOwner
		{
			get
			{
				return this.serverOwner;
			}
			set
			{
				this.serverOwner = value;
			}
		}

///    
///     <summary> * Sets the username of the owner of this server (in the case of an integrated server) </summary>
///     

		public virtual bool isSinglePlayer()
		{
			get
			{
				return this.serverOwner != null;
			}
		}

		public virtual string FolderName
		{
			get
			{
				return this.folderName;
			}
			set
			{
				this.folderName = value;
			}
		}


		public virtual string WorldName
		{
			set
			{
				this.worldName = value;
			}
			get
			{
				return this.worldName;
			}
		}



		public virtual void func_147139_a(EnumDifficulty p_147139_1_)
		{
			for(int var2 = 0; var2 < this.worldServers.Length; ++var2)
			{
				WorldServer var3 = this.worldServers[var2];

				if(var3 != null)
				{
					if(var3.WorldInfo.HardcoreModeEnabled)
					{
						var3.difficultySetting = EnumDifficulty.HARD;
						var3.setAllowedSpawnTypes(true, true);
					}
					else if(this.SinglePlayer)
					{
						var3.difficultySetting = p_147139_1_;
						var3.setAllowedSpawnTypes(var3.difficultySetting != EnumDifficulty.PEACEFUL, true);
					}
					else
					{
						var3.difficultySetting = p_147139_1_;
						var3.setAllowedSpawnTypes(this.allowSpawnMonsters(), this.canSpawnAnimals);
					}
				}
			}
		}

		protected internal virtual bool allowSpawnMonsters()
		{
			return true;
		}

///    
///     <summary> * Gets whether this is a demo or not. </summary>
///     
		public virtual bool isDemo()
		{
			get
			{
				return this.isDemo;
			}
			set
			{
				this.isDemo = value;
			}
		}

///    
///     <summary> * Sets whether this is a demo or not. </summary>
///     

		public virtual void canCreateBonusChest(bool p_71194_1_)
		{
			this.enableBonusChest = p_71194_1_;
		}

		public virtual ISaveFormat ActiveAnvilConverter
		{
			get
			{
				return this.anvilConverterForAnvilFile;
			}
		}

///    
///     <summary> * WARNING : directly calls
///     * getActiveAnvilConverter().deleteWorldDirectory(theWorldServer[0].getSaveHandler().getWorldDirectoryName()); </summary>
///     
		public virtual void deleteWorldAndStopServer()
		{
			this.worldIsBeingDeleted = true;
			this.ActiveAnvilConverter.flushCache();

			for(int var1 = 0; var1 < this.worldServers.Length; ++var1)
			{
				WorldServer var2 = this.worldServers[var1];

				if(var2 != null)
				{
					var2.flush();
				}
			}

			this.ActiveAnvilConverter.deleteWorldDirectory(this.worldServers[0].SaveHandler.WorldDirectoryName);
			this.initiateShutdown();
		}

		public virtual string func_147133_T()
		{
			return this.field_147141_M;
		}

		public virtual void addServerStatsToSnooper(PlayerUsageSnooper p_70000_1_)
		{
			p_70000_1_.func_152768_a("whitelist_enabled", Convert.ToBoolean(false));
			p_70000_1_.func_152768_a("whitelist_count", Convert.ToInt32(0));
			p_70000_1_.func_152768_a("players_current", Convert.ToInt32(this.CurrentPlayerCount));
			p_70000_1_.func_152768_a("players_max", Convert.ToInt32(this.MaxPlayers));
			p_70000_1_.func_152768_a("players_seen", Convert.ToInt32(this.serverConfigManager.AvailablePlayerDat.Length));
			p_70000_1_.func_152768_a("uses_auth", Convert.ToBoolean(this.onlineMode));
			p_70000_1_.func_152768_a("gui_state", this.GuiEnabled ? "enabled" : "disabled");
			p_70000_1_.func_152768_a("run_time", Convert.ToInt64((SystemTimeMillis - p_70000_1_.MinecraftStartTimeMillis) / 60L * 1000L));
			p_70000_1_.func_152768_a("avg_tick_ms", Convert.ToInt32((int)(MathHelper.average(this.tickTimeArray) * 1.0E-6D)));
			int var2 = 0;

			for(int var3 = 0; var3 < this.worldServers.Length; ++var3)
			{
				if(this.worldServers[var3] != null)
				{
					WorldServer var4 = this.worldServers[var3];
					WorldInfo var5 = var4.WorldInfo;
					p_70000_1_.func_152768_a("world[" + var2 + "][dimension]", Convert.ToInt32(var4.provider.dimensionId));
					p_70000_1_.func_152768_a("world[" + var2 + "][mode]", var5.GameType);
					p_70000_1_.func_152768_a("world[" + var2 + "][difficulty]", var4.difficultySetting);
					p_70000_1_.func_152768_a("world[" + var2 + "][hardcore]", Convert.ToBoolean(var5.HardcoreModeEnabled));
					p_70000_1_.func_152768_a("world[" + var2 + "][generator_name]", var5.TerrainType.WorldTypeName);
					p_70000_1_.func_152768_a("world[" + var2 + "][generator_version]", Convert.ToInt32(var5.TerrainType.GeneratorVersion));
					p_70000_1_.func_152768_a("world[" + var2 + "][height]", Convert.ToInt32(this.buildLimit));
					p_70000_1_.func_152768_a("world[" + var2 + "][chunks_loaded]", Convert.ToInt32(var4.ChunkProvider.LoadedChunkCount));
					++var2;
				}
			}

			p_70000_1_.func_152768_a("worlds", Convert.ToInt32(var2));
		}

		public virtual void addServerTypeToSnooper(PlayerUsageSnooper p_70001_1_)
		{
			p_70001_1_.func_152767_b("singleplayer", Convert.ToBoolean(this.SinglePlayer));
			p_70001_1_.func_152767_b("server_brand", this.ServerModName);
			p_70001_1_.func_152767_b("gui_supported", GraphicsEnvironment.Headless ? "headless" : "supported");
			p_70001_1_.func_152767_b("dedicated", Convert.ToBoolean(this.DedicatedServer));
		}

///    
///     <summary> * Returns whether snooping is enabled or not. </summary>
///     
		public virtual bool isSnooperEnabled()
		{
			get
			{
				return true;
			}
		}

		public abstract bool isDedicatedServer() {get;}

		public virtual bool isServerInOnlineMode()
		{
			get
			{
				return this.onlineMode;
			}
		}

		public virtual bool OnlineMode
		{
			set
			{
				this.onlineMode = value;
			}
		}

		public virtual bool CanSpawnAnimals
		{
			get
			{
				return this.canSpawnAnimals;
			}
			set
			{
				this.canSpawnAnimals = value;
			}
		}


		public virtual bool CanSpawnNPCs
		{
			get
			{
				return this.canSpawnNPCs;
			}
			set
			{
				this.canSpawnNPCs = value;
			}
		}


		public virtual bool isPVPEnabled()
		{
			get
			{
				return this.pvpEnabled;
			}
		}

		public virtual bool AllowPvp
		{
			set
			{
				this.pvpEnabled = value;
			}
		}

		public virtual bool isFlightAllowed()
		{
			get
			{
				return this.allowFlight;
			}
		}

		public virtual bool AllowFlight
		{
			set
			{
				this.allowFlight = value;
			}
		}

///    
///     <summary> * Return whether command blocks are enabled. </summary>
///     
		public abstract bool isCommandBlockEnabled() {get;}

		public virtual string MOTD
		{
			get
			{
				return this.motd;
			}
			set
			{
				this.motd = value;
			}
		}


		public virtual int BuildLimit
		{
			get
			{
				return this.buildLimit;
			}
			set
			{
				this.buildLimit = value;
			}
		}


		public virtual ServerConfigurationManager ConfigurationManager
		{
			get
			{
				return this.serverConfigManager;
			}
		}

		public virtual void func_152361_a(ServerConfigurationManager p_152361_1_)
		{
			this.serverConfigManager = p_152361_1_;
		}

///    
///     <summary> * Sets the game type for all worlds. </summary>
///     
		public virtual WorldSettings.GameType GameType
		{
			set
			{
				for(int var2 = 0; var2 < this.worldServers.Length; ++var2)
				{
					Server.worldServers[var2].WorldInfo.GameType = value;
				}
			}
		}

		public virtual NetworkSystem func_147137_ag()
		{
			return this.field_147144_o;
		}

		public virtual bool serverIsInRunLoop()
		{
			return this.serverIsRunning;
		}

		public virtual bool GuiEnabled
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * On dedicated does nothing. On integrated, sets commandsAllowedForAll, gameType and allows external connections. </summary>
///     
		public abstract string shareToLAN(WorldSettings.GameType p_71206_1_, bool p_71206_2_);

		public virtual int TickCounter
		{
			get
			{
				return this.tickCounter;
			}
		}

		public virtual void enableProfiling()
		{
			this.startProfiling = true;
		}

		public virtual PlayerUsageSnooper PlayerUsageSnooper
		{
			get
			{
				return this.usageSnooper;
			}
		}

///    
///     <summary> * Return the position for this command sender. </summary>
///     
		public virtual ChunkCoordinates PlayerCoordinates
		{
			get
			{
				return new ChunkCoordinates(0, 0, 0);
			}
		}

		public virtual World EntityWorld
		{
			get
			{
				return this.worldServers[0];
			}
		}

///    
///     <summary> * Return the spawn protection area's size. </summary>
///     
		public virtual int SpawnProtectionSize
		{
			get
			{
				return 16;
			}
		}

///    
///     <summary> * Returns true if a player does not have permission to edit the block at the given coordinates. </summary>
///     
		public virtual bool isBlockProtected(World p_96290_1_, int p_96290_2_, int p_96290_3_, int p_96290_4_, EntityPlayer p_96290_5_)
		{
			return false;
		}

		public virtual bool ForceGamemode
		{
			get
			{
				return this.isGamemodeForced;
			}
		}

		public virtual Proxy ServerProxy
		{
			get
			{
				return this.serverProxy;
			}
		}

///    
///     <summary> * returns the difference, measured in milliseconds, between the current system time and midnight, January 1, 1970
///     * UTC. </summary>
///     
		public static long SystemTimeMillis
		{
			get
			{
				return System.currentTimeMillis();
			}
		}

		public virtual int func_143007_ar()
		{
			return this.field_143008_E;
		}

		public virtual void func_143006_e(int p_143006_1_)
		{
			this.field_143008_E = p_143006_1_;
		}

		public virtual IChatComponent func_145748_c_()
		{
			return new ChatComponentText(this.CommandSenderName);
		}

		public virtual bool func_147136_ar()
		{
			return true;
		}

		public virtual MinecraftSessionService func_147130_as()
		{
			return this.field_147143_S;
		}

		public virtual GameProfileRepository func_152359_aw()
		{
			return this.field_152365_W;
		}

		public virtual PlayerProfileCache func_152358_ax()
		{
			return this.field_152366_X;
		}

		public virtual ServerStatusResponse func_147134_at()
		{
			return this.field_147147_p;
		}

		public virtual void func_147132_au()
		{
			this.field_147142_T = 0L;
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
    internal static long[][] ReturnRectangularLongArray(int Size1, int Size2)
    {
        long[][] Array = new long[Size1][];
        for (int Array1 = 0; Array1 < Size1; Array1++)
        {
            Array[Array1] = new long[Size2];
        }
        return Array;
    }
}