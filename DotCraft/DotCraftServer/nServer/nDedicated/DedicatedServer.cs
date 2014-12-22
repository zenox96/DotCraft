using DotCraftCore.nServer;
using System;
using System.Collections;
using System.Threading;

namespace DotCraftServer.nServer.nDedicated
{
	public class DedicatedServer : MinecraftServer, IServer
	{
		private static readonly Logger field_155771_h = LogManager.Logger;
		private readonly IList pendingCommandList = Collections.synchronizedList(new ArrayList());
		private RConThreadQuery theRConThreadQuery;
		private RConThreadMain theRConThreadMain;
		private PropertyManager settings;
		private ServerEula field_154332_n;
		private bool canSpawnStructures;
		private WorldSettings.GameType gameType;
		private bool guiIsEnabled;
		private const string __OBFID = "CL_00001784";

		public DedicatedServer(File p_i1508_1_) : base(p_i1508_1_, Proxy.NO_PROXY)
		{
			Thread var10001 = new ThreadAnonymousInnerClassHelper(this);
		}

		private class ThreadAnonymousInnerClassHelper : System.Threading.Thread
		{
			private readonly DedicatedServer outerInstance;

			public ThreadAnonymousInnerClassHelper(DedicatedServer outerInstance) : base("Server Infinisleeper")
			{
				this.outerInstance = outerInstance;
				__OBFID = "CL_00001787";
			}

			private static readonly string __OBFID;
//JAVA TO C# CONVERTER TODO TASK: Initialization blocks declared within anonymous inner classes are not converted:
	//		{
	//			this.setDaemon(true);
	//			this.start();
	//		}
			public virtual void run()
			{
				while (true)
				{
					try
					{
						while (true)
						{
							Thread.Sleep(2147483647L);
						}
					}
					catch (InterruptedException)
					{
						;
					}
				}
			}
		}

		/// <summary>
		/// Initialises the server and starts it.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected boolean startServer() throws java.io.IOException
		protected internal virtual bool startServer()
		{
			Thread var1 = new ThreadAnonymousInnerClassHelper2(this);
			var1.Daemon = true;
			var1.Start();
			field_155771_h.info("Starting minecraft server version 1.7.10");

			if (Runtime.Runtime.maxMemory() / 1024L / 1024L < 512L)
			{
				field_155771_h.warn("To start the server with more ram, launch it as \"java -Xmx1024M -Xms1024M -jar minecraft_server.jar\"");
			}

			field_155771_h.info("Loading properties");
			this.settings = new PropertyManager(new File("server.properties"));
			this.field_154332_n = new ServerEula(new File("eula.txt"));

			if (!this.field_154332_n.func_154346_a())
			{
				field_155771_h.info("You need to agree to the EULA in order to run the server. Go to eula.txt for more info.");
				this.field_154332_n.func_154348_b();
				return false;
			}
			else
			{
				if (this.SinglePlayer)
				{
					this.Hostname = "127.0.0.1";
				}
				else
				{
					this.OnlineMode = this.settings.getBooleanProperty("online-mode", true);
					this.Hostname = this.settings.getStringProperty("server-ip", "");
				}

				this.CanSpawnAnimals = this.settings.getBooleanProperty("spawn-animals", true);
				this.CanSpawnNPCs = this.settings.getBooleanProperty("spawn-npcs", true);
				this.AllowPvp = this.settings.getBooleanProperty("pvp", true);
				this.AllowFlight = this.settings.getBooleanProperty("allow-flight", false);
				this.func_155759m(this.settings.getStringProperty("resource-pack", ""));
				this.MOTD = this.settings.getStringProperty("motd", "A Minecraft Server");
				this.ForceGamemode = this.settings.getBooleanProperty("force-gamemode", false);
				this.func_143006_e(this.settings.getIntProperty("player-idle-timeout", 0));

				if (this.settings.getIntProperty("difficulty", 1) < 0)
				{
					this.settings.setProperty("difficulty", Convert.ToInt32(0));
				}
				else if (this.settings.getIntProperty("difficulty", 1) > 3)
				{
					this.settings.setProperty("difficulty", Convert.ToInt32(3));
				}

				this.canSpawnStructures = this.settings.getBooleanProperty("generate-structures", true);
				int var2 = this.settings.getIntProperty("gamemode", WorldSettings.GameType.SURVIVAL.ID);
				this.gameType = WorldSettings.getGameTypeById(var2);
				field_155771_h.info("Default game type: " + this.gameType);
				InetAddress var3 = null;

				if (this.ServerHostname.length() > 0)
				{
					var3 = InetAddress.getByName(this.ServerHostname);
				}

				if (this.ServerPort < 0)
				{
					this.ServerPort = this.settings.getIntProperty("server-port", 25565);
				}

				field_155771_h.info("Generating keypair");
				this.KeyPair = CryptManager.generateKeyPair();
				field_155771_h.info("Starting Minecraft server on " + (this.ServerHostname.length() == 0 ? "*" : this.ServerHostname) + ":" + this.ServerPort);

				try
				{
					this.func_147137_ag().addLanEndpoint(var3, this.ServerPort);
				}
				catch (IOException var16)
				{
					field_155771_h.warn("**** FAILED TO BIND TO PORT!");
					field_155771_h.warn("The exception was: {}", new object[] {var16.ToString()});
					field_155771_h.warn("Perhaps a server is already running on that port?");
					return false;
				}

				if (!this.ServerInOnlineMode)
				{
					field_155771_h.warn("**** SERVER IS RUNNING IN OFFLINE/INSECURE MODE!");
					field_155771_h.warn("The server will make no attempt to authenticate usernames. Beware.");
					field_155771_h.warn("While this makes the game possible to play without internet access, it also opens up the ability for hackers to connect with any username they choose.");
					field_155771_h.warn("To change this, set \"online-mode\" to \"true\" in the server.properties file.");
				}

				if (this.func_152368_aE())
				{
					this.func_152358_ax().func_152658_c();
				}

				if (!PreYggdrasilConverter.func_152714_a(this.settings))
				{
					return false;
				}
				else
				{
					this.func_152361_a(new DedicatedPlayerList(this));
					long var4 = System.nanoTime();

					if (this.FolderName == null)
					{
						this.FolderName = this.settings.getStringProperty("level-name", "world");
					}

					string var6 = this.settings.getStringProperty("level-seed", "");
					string var7 = this.settings.getStringProperty("level-type", "DEFAULT");
					string var8 = this.settings.getStringProperty("generator-settings", "");
					long var9 = (new Random()).nextLong();

					if (var6.Length > 0)
					{
						try
						{
							long var11 = long.Parse(var6);

							if (var11 != 0L)
							{
								var9 = var11;
							}
						}
						catch (System.FormatException)
						{
							var9 = (long)var6.GetHashCode();
						}
					}

					WorldType var17 = WorldType.parseWorldType(var7);

					if (var17 == null)
					{
						var17 = WorldType.DEFAULT;
					}

					this.func_147136_ar();
					this.CommandBlockEnabled;
					this.OpPermissionLevel;
					this.SnooperEnabled;
					this.BuildLimit = this.settings.getIntProperty("max-build-height", 256);
					this.BuildLimit = (this.BuildLimit + 8) / 16 * 16;
					this.BuildLimit = MathHelper.clamp_int(this.BuildLimit, 64, 256);
					this.settings.setProperty("max-build-height", Convert.ToInt32(this.BuildLimit));
					field_155771_h.info("Preparing level \"" + this.FolderName + "\"");
					this.loadAllWorlds(this.FolderName, this.FolderName, var9, var17, var8);
					long var12 = System.nanoTime() - var4;
					string var14 = string.Format("{0:F3}s", new object[] {Convert.ToDouble((double)var12 / 1.0E9D)});
					field_155771_h.info("Done (" + var14 + ")! For help, type \"help\" or \"?\"");

					if (this.settings.getBooleanProperty("enable-query", false))
					{
						field_155771_h.info("Starting GS4 status listener");
						this.theRConThreadQuery = new RConThreadQuery(this);
						this.theRConThreadQuery.startThread();
					}

					if (this.settings.getBooleanProperty("enable-rcon", false))
					{
						field_155771_h.info("Starting remote control listener");
						this.theRConThreadMain = new RConThreadMain(this);
						this.theRConThreadMain.startThread();
					}

					return true;
				}
			}
		}

		private class ThreadAnonymousInnerClassHelper2 : System.Threading.Thread
		{
			private readonly DedicatedServer outerInstance;

			public ThreadAnonymousInnerClassHelper2(DedicatedServer outerInstance) : base("Server console handler")
			{
				this.outerInstance = outerInstance;
				__OBFID = "CL_00001786";
			}

			private static readonly string __OBFID;
			public virtual void run()
			{
				System.IO.StreamReader var1 = new System.IO.StreamReader(System.in);
				string var2;

				try
				{
					while (!outerInstance.ServerStopped && outerInstance.ServerRunning && (var2 = var1.ReadLine()) != null)
					{
						outerInstance.addPendingCommand(var2, outerInstance);
					}
				}
				catch (IOException var4)
				{
					DedicatedServer.field_155771_h.error("Exception handling console input", var4);
				}
			}
		}

		public virtual bool canStructuresSpawn()
		{
			return this.canSpawnStructures;
		}

		public virtual WorldSettings.GameType GameType
		{
			get
			{
				return this.gameType;
			}
		}

		public virtual EnumDifficulty func_147135_j()
		{
			return EnumDifficulty.func_151523_a(this.settings.getIntProperty("difficulty", 1));
		}

		/// <summary>
		/// Defaults to false.
		/// </summary>
		public virtual bool Hardcore
		{
			get
			{
				return this.settings.getBooleanProperty("hardcore", false);
			}
		}

		/// <summary>
		/// Called on exit from the main run() loop.
		/// </summary>
		protected internal virtual void finalTick(CrashReport p_712281_)
		{
		}

		/// <summary>
		/// Adds the server info, including from theWorldServer, to the crash report.
		/// </summary>
		public virtual CrashReport addServerInfoToCrashReport(CrashReport p_712301_)
		{
			p_712301_ = base.addServerInfoToCrashReport(p_712301_);
			p_712301_.Category.addCrashSectionCallable("Is Modded", new CallableAnonymousInnerClassHelper(this));
			p_712301_.Category.addCrashSectionCallable("Type", new CallableAnonymousInnerClassHelper2(this));
			return p_712301_;
		}

		private class CallableAnonymousInnerClassHelper : Callable
		{
			private readonly DedicatedServer outerInstance;

			public CallableAnonymousInnerClassHelper(DedicatedServer outerInstance)
			{
				this.outerInstance = outerInstance;
				__OBFID = "CL_00001785";
			}

			private static readonly string __OBFID;
			public virtual string call()
			{
				string var1 = outerInstance.ServerModName;
				return !var1.Equals("vanilla") ? "Definitely; Server brand changed to \'" + var1 + "\'" : "Unknown (can\'t tell)";
			}
		}

		private class CallableAnonymousInnerClassHelper2 : Callable
		{
			private readonly DedicatedServer outerInstance;

			public CallableAnonymousInnerClassHelper2(DedicatedServer outerInstance)
			{
				this.outerInstance = outerInstance;
				__OBFID = "CL_00001788";
			}

			private static readonly string __OBFID;
			public virtual string call()
			{
				return "Dedicated Server (map_server.txt)";
			}
		}

		/// <summary>
		/// Directly calls System.exit(0), instantly killing the program.
		/// </summary>
		protected internal virtual void systemExitNow()
		{
			Environment.Exit(0);
		}

		public virtual void updateTimeLightAndEntities()
		{
			base.updateTimeLightAndEntities();
			this.executePendingCommands();
		}

		public virtual bool AllowNether
		{
			get
			{
				return this.settings.getBooleanProperty("allow-nether", true);
			}
		}

		public virtual bool allowSpawnMonsters()
		{
			return this.settings.getBooleanProperty("spawn-monsters", true);
		}

		public virtual void addServerStatsToSnooper(PlayerUsageSnooper p_700001_)
		{
			p_700001_.func_152768_a("whitelist_enabled", Convert.ToBoolean(this.ConfigurationManager.WhiteListEnabled));
			p_700001_.func_152768_a("whitelist_count", Convert.ToInt32(this.ConfigurationManager.func_152598l().length));
			base.addServerStatsToSnooper(p_700001_);
		}

		/// <summary>
		/// Returns whether snooping is enabled or not.
		/// </summary>
		public virtual bool SnooperEnabled
		{
			get
			{
				return this.settings.getBooleanProperty("snooper-enabled", true);
			}
		}

		public virtual void addPendingCommand(string p_713311_, ICommandSender p_713312_)
		{
			this.pendingCommandList.Add(new ServerCommand(p_713311_, p_713312_));
		}

		public virtual void executePendingCommands()
		{
			while (this.pendingCommandList.Count > 0)
			{
				ServerCommand var1 = (ServerCommand)this.pendingCommandList.Remove(0);
				this.CommandManager.executeCommand(var1.sender, var1.command);
			}
		}

		public virtual bool isDedicatedServer()
		{
			return true;
		}

		public virtual DedicatedPlayerList ConfigurationManager
		{
			get
			{
				return (DedicatedPlayerList)base.ConfigurationManager;
			}
		}

		/// <summary>
		/// Gets an integer property. If it does not exist, set it to the specified value.
		/// </summary>
		public virtual int getIntProperty(string p_713271_, int p_713272_)
		{
			return this.settings.getIntProperty(p_713271_, p_713272_);
		}

		/// <summary>
		/// Gets a string property. If it does not exist, set it to the specified value.
		/// </summary>
		public virtual string getStringProperty(string p_713301_, string p_713302_)
		{
			return this.settings.getStringProperty(p_713301_, p_713302_);
		}

		/// <summary>
		/// Gets a boolean property. If it does not exist, set it to the specified value.
		/// </summary>
		public virtual bool getBooleanProperty(string p_713321_, bool p_713322_)
		{
			return this.settings.getBooleanProperty(p_713321_, p_713322_);
		}

		/// <summary>
		/// Saves an Object with the given property name.
		/// </summary>
		public virtual void setProperty(string p_713281_, object p_713282_)
		{
			this.settings.setProperty(p_713281_, p_713282_);
		}

		/// <summary>
		/// Saves all of the server properties to the properties file.
		/// </summary>
		public virtual void saveProperties()
		{
			this.settings.saveProperties();
		}

		/// <summary>
		/// Returns the filename where server properties are stored
		/// </summary>
		public virtual string SettingsFilename
		{
			get
			{
				File var1 = this.settings.PropertiesFile;
				return var1 != null ? var1.AbsolutePath : "No settings file";
			}
		}

		public virtual void setGuiEnabled()
		{
			MinecraftServerGui.createServerGui(this);
			this.guiIsEnabled = true;
		}

		public virtual bool GuiEnabled
		{
			get
			{
				return this.guiIsEnabled;
			}
		}

		/// <summary>
		/// On dedicated does nothing. On integrated, sets commandsAllowedForAll, gameType and allows external connections.
		/// </summary>
		public virtual string shareToLAN(WorldSettings.GameType p_712061_, bool p_712062_)
		{
			return "";
		}

		/// <summary>
		/// Return whether command blocks are enabled.
		/// </summary>
		public virtual bool CommandBlockEnabled
		{
			get
			{
				return this.settings.getBooleanProperty("enable-command-block", false);
			}
		}

		/// <summary>
		/// Return the spawn protection area's size.
		/// </summary>
		public virtual int SpawnProtectionSize
		{
			get
			{
				return this.settings.getIntProperty("spawn-protection", base.SpawnProtectionSize);
			}
		}

		/// <summary>
		/// Returns true if a player does not have permission to edit the block at the given coordinates.
		/// </summary>
		public virtual bool isBlockProtected(World p_962901_, int p_962902_, int p_962903_, int p_962904_, EntityPlayer p_962905_)
		{
			if (p_962901_.provider.dimensionId != 0)
			{
				return false;
			}
			else if (this.ConfigurationManager.func_152603m().func_152690d())
			{
				return false;
			}
			else if (this.ConfigurationManager.func_152596_g(p_962905_.GameProfile))
			{
				return false;
			}
			else if (this.SpawnProtectionSize <= 0)
			{
				return false;
			}
			else
			{
				ChunkCoordinates var6 = p_962901_.SpawnPoint;
				int var7 = MathHelper.abs_int(p_962902_ - var6.posX);
				int var8 = MathHelper.abs_int(p_962904_ - var6.posZ);
				int var9 = Math.Max(var7, var8);
				return var9 <= this.SpawnProtectionSize;
			}
		}

		public virtual int OpPermissionLevel
		{
			get
			{
				return this.settings.getIntProperty("op-permission-level", 4);
			}
		}

		public virtual void func_143006_e(int p_1430061_)
		{
			base.func_143006_e(p_1430061_);
			this.settings.setProperty("player-idle-timeout", Convert.ToInt32(p_1430061_));
			this.saveProperties();
		}

		public virtual bool func_152363m()
		{
			return this.settings.getBooleanProperty("broadcast-rcon-to-ops", true);
		}

		public virtual bool func_147136_ar()
		{
			return this.settings.getBooleanProperty("announce-player-achievements", true);
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected boolean func_152368_aE() throws java.io.IOException
		protected internal virtual bool func_152368_aE()
		{
			bool var2 = false;
			int var1;

			for (var1 = 0; !var2 && var1 <= 2; ++var1)
			{
				if (var1 > 0)
				{
					field_155771_h.warn("Encountered a problem while converting the user banlist, retrying in a few seconds");
					this.func_152369_aG();
				}

				var2 = PreYggdrasilConverter.func_152724_a(this);
			}

			bool var3 = false;

			for (var1 = 0; !var3 && var1 <= 2; ++var1)
			{
				if (var1 > 0)
				{
					field_155771_h.warn("Encountered a problem while converting the ip banlist, retrying in a few seconds");
					this.func_152369_aG();
				}

				var3 = PreYggdrasilConverter.func_152722_b(this);
			}

			bool var4 = false;

			for (var1 = 0; !var4 && var1 <= 2; ++var1)
			{
				if (var1 > 0)
				{
					field_155771_h.warn("Encountered a problem while converting the op list, retrying in a few seconds");
					this.func_152369_aG();
				}

				var4 = PreYggdrasilConverter.func_152718_c(this);
			}

			bool var5 = false;

			for (var1 = 0; !var5 && var1 <= 2; ++var1)
			{
				if (var1 > 0)
				{
					field_155771_h.warn("Encountered a problem while converting the whitelist, retrying in a few seconds");
					this.func_152369_aG();
				}

				var5 = PreYggdrasilConverter.func_152710d(this);
			}

			bool var6 = false;

			for (var1 = 0; !var6 && var1 <= 2; ++var1)
			{
				if (var1 > 0)
				{
					field_155771_h.warn("Encountered a problem while converting the player save files, retrying in a few seconds");
					this.func_152369_aG();
				}

				var6 = PreYggdrasilConverter.func_152723_a(this, this.settings);
			}

			return var2 || var3 || var4 || var5 || var6;
		}

		private void func_152369_aG()
		{
			try
			{
				Thread.Sleep(5000L);
			}
			catch (InterruptedException)
			{
				;
			}
		}
	}

}