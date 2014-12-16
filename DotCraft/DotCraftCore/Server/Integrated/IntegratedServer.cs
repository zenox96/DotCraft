using System;

namespace DotCraftCore.Server.Integrated
{

	using ClientBrandRetriever = DotCraftCore.client.ClientBrandRetriever;
	using Minecraft = DotCraftCore.client.Minecraft;
	using ThreadLanServerPing = DotCraftCore.client.multiplayer.ThreadLanServerPing;
	using CrashReport = DotCraftCore.crash.CrashReport;
	using PlayerUsageSnooper = DotCraftCore.profiler.PlayerUsageSnooper;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using CryptManager = DotCraftCore.Util.CryptManager;
	using HttpUtil = DotCraftCore.Util.HttpUtil;
	using EnumDifficulty = DotCraftCore.World.EnumDifficulty;
	using WorldManager = DotCraftCore.World.WorldManager;
	using WorldServer = DotCraftCore.World.WorldServer;
	using WorldServerMulti = DotCraftCore.World.WorldServerMulti;
	using WorldSettings = DotCraftCore.World.WorldSettings;
	using WorldType = DotCraftCore.World.WorldType;
	using DemoWorldServer = DotCraftCore.World.Demo.DemoWorldServer;
	using ISaveHandler = DotCraftCore.World.Storage.ISaveHandler;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class IntegratedServer : MinecraftServer
	{
		private static readonly Logger logger = LogManager.Logger;

	/// <summary> The Minecraft instance.  </summary>
		private readonly Minecraft mc;
		private readonly WorldSettings theWorldSettings;
		private bool isGamePaused;
		private bool isPublic;
		private ThreadLanServerPing lanServerPing;
		

		public IntegratedServer(Minecraft p_i1317_1_, string p_i1317_2_, string p_i1317_3_, WorldSettings p_i1317_4_) : base(new File(p_i1317_1_.mcDataDir, "saves"), p_i1317_1_.getProxy())
		{
			this.ServerOwner = p_i1317_1_.Session.Username;
			this.FolderName = p_i1317_2_;
			this.WorldName = p_i1317_3_;
			this.Demo = p_i1317_1_.Demo;
			this.canCreateBonusChest(p_i1317_4_.BonusChestEnabled);
			this.BuildLimit = 256;
			this.func_152361_a(new IntegratedPlayerList(this));
			this.mc = p_i1317_1_;
			this.theWorldSettings = p_i1317_4_;
		}

		protected internal override void loadAllWorlds(string p_71247_1_, string p_71247_2_, long p_71247_3_, WorldType p_71247_5_, string p_71247_6_)
		{
			this.convertMapIfNeeded(p_71247_1_);
			this.worldServers = new WorldServer[3];
//ORIGINAL LINE: this.timeOfLastDimensionTick = new long[this.worldServers.Length][100];
//JAVA TO VB & C# CONVERTER NOTE: The following call to the 'RectangularArrays' helper class reproduces the rectangular array initialization that is automatic in Java:
			this.timeOfLastDimensionTick = RectangularArrays.ReturnRectangularLongArray(this.worldServers.Length, 100);
			ISaveHandler var7 = this.ActiveAnvilConverter.getSaveLoader(p_71247_1_, true);

			for(int var8 = 0; var8 < this.worldServers.Length; ++var8)
			{
				sbyte var9 = 0;

				if(var8 == 1)
				{
					var9 = -1;
				}

				if(var8 == 2)
				{
					var9 = 1;
				}

				if(var8 == 0)
				{
					if(this.Demo)
					{
						this.worldServers[var8] = new DemoWorldServer(this, var7, p_71247_2_, var9, this.theProfiler);
					}
					else
					{
						this.worldServers[var8] = new WorldServer(this, var7, p_71247_2_, var9, this.theWorldSettings, this.theProfiler);
					}
				}
				else
				{
					this.worldServers[var8] = new WorldServerMulti(this, var7, p_71247_2_, var9, this.theWorldSettings, this.worldServers[0], this.theProfiler);
				}

				this.worldServers[var8].addWorldAccess(new WorldManager(this, this.worldServers[var8]));
				this.ConfigurationManager.PlayerManager = this.worldServers;
			}

			this.func_147139_a(this.func_147135_j());
			this.initialWorldChunkLoad();
		}

///    
///     <summary> * Initialises the server and starts it. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected boolean startServer() throws IOException
		protected internal override bool startServer()
		{
			logger.info("Starting integrated minecraft server version 1.7.10");
			this.OnlineMode = true;
			this.CanSpawnAnimals = true;
			this.CanSpawnNPCs = true;
			this.AllowPvp = true;
			this.AllowFlight = true;
			logger.info("Generating keypair");
			this.KeyPair = CryptManager.createNewKeyPair();
			this.loadAllWorlds(this.FolderName, this.WorldName, this.theWorldSettings.Seed, this.theWorldSettings.TerrainType, this.theWorldSettings.func_82749_j());
			this.MOTD = this.ServerOwner + " - " + this.worldServers[0].WorldInfo.WorldName;
			return true;
		}

///    
///     <summary> * Main function called by run() every loop. </summary>
///     
		public override void tick()
		{
			bool var1 = this.isGamePaused;
			this.isGamePaused = Minecraft.Minecraft.NetHandler != null && Minecraft.Minecraft.func_147113_T();

			if(!var1 && this.isGamePaused)
			{
				logger.info("Saving and pausing game...");
				this.ConfigurationManager.saveAllPlayerData();
				this.saveAllWorlds(false);
			}

			if(!this.isGamePaused)
			{
				base.tick();

				if(this.mc.gameSettings.renderDistanceChunks != this.ConfigurationManager.ViewDistance)
				{
					logger.info("Changing view distance to {}, from {}", new object[] {Convert.ToInt32(this.mc.gameSettings.renderDistanceChunks), Convert.ToInt32(this.ConfigurationManager.ViewDistance)});
					this.ConfigurationManager.func_152611_a(this.mc.gameSettings.renderDistanceChunks);
				}
			}
		}

		public override bool canStructuresSpawn()
		{
			return false;
		}

		public override WorldSettings.GameType GameType
		{
			get
			{
				return this.theWorldSettings.GameType;
			}
			set
			{
				this.ConfigurationManager.func_152604_a(value);
			}
		}

		public override EnumDifficulty func_147135_j()
		{
			return this.mc.gameSettings.difficulty;
		}

///    
///     <summary> * Defaults to false. </summary>
///     
		public override bool isHardcore()
		{
			get
			{
				return this.theWorldSettings.HardcoreEnabled;
			}
		}

		public override bool func_152363_m()
		{
			return false;
		}

		protected internal override File DataDirectory
		{
			get
			{
				return this.mc.mcDataDir;
			}
		}

		public override bool isDedicatedServer()
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * Called on exit from the main run() loop. </summary>
///     
		protected internal override void finalTick(CrashReport p_71228_1_)
		{
			this.mc.crashed(p_71228_1_);
		}

///    
///     <summary> * Adds the server info, including from theWorldServer, to the crash report. </summary>
///     
		public override CrashReport addServerInfoToCrashReport(CrashReport p_71230_1_)
		{
			p_71230_1_ = base.addServerInfoToCrashReport(p_71230_1_);
			p_71230_1_.Category.addCrashSectionCallable("Type", new Callable() {  public string call() { return "Integrated Server (map_client.txt)"; } });
			p_71230_1_.Category.addCrashSectionCallable("Is Modded", new Callable() {  public string call() { string var1 = ClientBrandRetriever.ClientModName; if(!var1.Equals("vanilla")) { return "Definitely; Client brand changed to \'" + var1 + "\'"; } else { var1 = IntegratedServer.ServerModName; return !var1.Equals("vanilla") ? "Definitely; Server brand changed to \'" + var1 + "\'" : (typeof(Minecraft).Signers == null ? "Very likely; Jar signature invalidated" : "Probably not. Jar signature remains and both client + server brands are untouched."); } } });
			return p_71230_1_;
		}

		public override void addServerStatsToSnooper(PlayerUsageSnooper p_70000_1_)
		{
			base.addServerStatsToSnooper(p_70000_1_);
			p_70000_1_.func_152768_a("snooper_partner", this.mc.PlayerUsageSnooper.UniqueID);
		}

///    
///     <summary> * Returns whether snooping is enabled or not. </summary>
///     
		public override bool isSnooperEnabled()
		{
			get
			{
				return Minecraft.Minecraft.SnooperEnabled;
			}
		}

///    
///     <summary> * On dedicated does nothing. On integrated, sets commandsAllowedForAll, gameType and allows external connections. </summary>
///     
		public override string shareToLAN(WorldSettings.GameType p_71206_1_, bool p_71206_2_)
		{
			try
			{
				int var3 = -1;

				try
				{
					var3 = HttpUtil.func_76181_a();
				}
				catch (IOException var5)
				{
					;
				}

				if(var3 <= 0)
				{
					var3 = 25564;
				}

				this.func_147137_ag().addLanEndpoint((InetAddress)null, var3);
				logger.info("Started on " + var3);
				this.isPublic = true;
				this.lanServerPing = new ThreadLanServerPing(this.MOTD, var3 + "");
				this.lanServerPing.start();
				this.ConfigurationManager.func_152604_a(p_71206_1_);
				this.ConfigurationManager.CommandsAllowedForAll = p_71206_2_;
				return var3 + "";
			}
			catch (IOException var6)
			{
				return null;
			}
		}

///    
///     <summary> * Saves all necessary data as preparation for stopping the server. </summary>
///     
		public override void stopServer()
		{
			base.stopServer();

			if(this.lanServerPing != null)
			{
				this.lanServerPing.interrupt();
				this.lanServerPing = null;
			}
		}

///    
///     <summary> * Sets the serverRunning variable to false, in order to get the server to shut down. </summary>
///     
		public override void initiateShutdown()
		{
			base.initiateShutdown();

			if(this.lanServerPing != null)
			{
				this.lanServerPing.interrupt();
				this.lanServerPing = null;
			}
		}

///    
///     <summary> * Returns true if this integrated server is open to LAN </summary>
///     
		public virtual bool Public
		{
			get
			{
				return this.isPublic;
			}
		}

///    
///     <summary> * Sets the game type for all worlds. </summary>
///     

///    
///     <summary> * Return whether command blocks are enabled. </summary>
///     
		public override bool isCommandBlockEnabled()
		{
			get
			{
				return true;
			}
		}

		public override int func_110455_j()
		{
			return 4;
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