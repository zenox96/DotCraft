using System;

namespace DotCraftCore.World.Storage
{

	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using GameRules = DotCraftCore.World.GameRules;
	using WorldSettings = DotCraftCore.World.WorldSettings;
	using WorldType = DotCraftCore.World.WorldType;

	public class WorldInfo
	{
	/// <summary> Holds the seed of the currently world.  </summary>
		private long randomSeed;
		private WorldType terrainType;
		private string generatorOptions;

	/// <summary> The spawn zone position X coordinate.  </summary>
		private int spawnX;

	/// <summary> The spawn zone position Y coordinate.  </summary>
		private int spawnY;

	/// <summary> The spawn zone position Z coordinate.  </summary>
		private int spawnZ;

	/// <summary> Total time for this world.  </summary>
		private long totalTime;

	/// <summary> The current world time in ticks, ranging from 0 to 23999.  </summary>
		private long worldTime;

	/// <summary> The last time the player was in this world.  </summary>
		private long lastTimePlayed;

	/// <summary> The size of entire save of current world on the disk, isn't exactly.  </summary>
		private long sizeOnDisk;
		private NBTTagCompound playerTag;
		private int dimension;

	/// <summary> The name of the save defined at world creation.  </summary>
		private string levelName;

	/// <summary> Introduced in beta 1.3, is the save version for future control.  </summary>
		private int saveVersion;

	/// <summary> True if it's raining, false otherwise.  </summary>
		private bool raining;

	/// <summary> Number of ticks until next rain.  </summary>
		private int rainTime;

	/// <summary> Is thunderbolts failing now?  </summary>
		private bool thundering;

	/// <summary> Number of ticks untils next thunderbolt.  </summary>
		private int thunderTime;

	/// <summary> The Game Type.  </summary>
		private WorldSettings.GameType theGameType;

///    
///     <summary> * Whether the map features (e.g. strongholds) generation is enabled or disabled. </summary>
///     
		private bool mapFeaturesEnabled;

	/// <summary> Hardcore mode flag  </summary>
		private bool hardcore;
		private bool allowCommands;
		private bool initialized;
		private GameRules theGameRules;
		private const string __OBFID = "CL_00000587";

		protected internal WorldInfo()
		{
			this.terrainType = WorldType.DEFAULT;
			this.generatorOptions = "";
			this.theGameRules = new GameRules();
		}

		public WorldInfo(NBTTagCompound p_i2157_1_)
		{
			this.terrainType = WorldType.DEFAULT;
			this.generatorOptions = "";
			this.theGameRules = new GameRules();
			this.randomSeed = p_i2157_1_.getLong("RandomSeed");

			if (p_i2157_1_.func_150297_b("generatorName", 8))
			{
				string var2 = p_i2157_1_.getString("generatorName");
				this.terrainType = WorldType.parseWorldType(var2);

				if (this.terrainType == null)
				{
					this.terrainType = WorldType.DEFAULT;
				}
				else if (this.terrainType.Versioned)
				{
					int var3 = 0;

					if (p_i2157_1_.func_150297_b("generatorVersion", 99))
					{
						var3 = p_i2157_1_.getInteger("generatorVersion");
					}

					this.terrainType = this.terrainType.getWorldTypeForGeneratorVersion(var3);
				}

				if (p_i2157_1_.func_150297_b("generatorOptions", 8))
				{
					this.generatorOptions = p_i2157_1_.getString("generatorOptions");
				}
			}

			this.theGameType = WorldSettings.GameType.getByID(p_i2157_1_.getInteger("GameType"));

			if (p_i2157_1_.func_150297_b("MapFeatures", 99))
			{
				this.mapFeaturesEnabled = p_i2157_1_.getBoolean("MapFeatures");
			}
			else
			{
				this.mapFeaturesEnabled = true;
			}

			this.spawnX = p_i2157_1_.getInteger("SpawnX");
			this.spawnY = p_i2157_1_.getInteger("SpawnY");
			this.spawnZ = p_i2157_1_.getInteger("SpawnZ");
			this.totalTime = p_i2157_1_.getLong("Time");

			if (p_i2157_1_.func_150297_b("DayTime", 99))
			{
				this.worldTime = p_i2157_1_.getLong("DayTime");
			}
			else
			{
				this.worldTime = this.totalTime;
			}

			this.lastTimePlayed = p_i2157_1_.getLong("LastPlayed");
			this.sizeOnDisk = p_i2157_1_.getLong("SizeOnDisk");
			this.levelName = p_i2157_1_.getString("LevelName");
			this.saveVersion = p_i2157_1_.getInteger("version");
			this.rainTime = p_i2157_1_.getInteger("rainTime");
			this.raining = p_i2157_1_.getBoolean("raining");
			this.thunderTime = p_i2157_1_.getInteger("thunderTime");
			this.thundering = p_i2157_1_.getBoolean("thundering");
			this.hardcore = p_i2157_1_.getBoolean("hardcore");

			if (p_i2157_1_.func_150297_b("initialized", 99))
			{
				this.initialized = p_i2157_1_.getBoolean("initialized");
			}
			else
			{
				this.initialized = true;
			}

			if (p_i2157_1_.func_150297_b("allowCommands", 99))
			{
				this.allowCommands = p_i2157_1_.getBoolean("allowCommands");
			}
			else
			{
				this.allowCommands = this.theGameType == WorldSettings.GameType.CREATIVE;
			}

			if (p_i2157_1_.func_150297_b("Player", 10))
			{
				this.playerTag = p_i2157_1_.getCompoundTag("Player");
				this.dimension = this.playerTag.getInteger("Dimension");
			}

			if (p_i2157_1_.func_150297_b("GameRules", 10))
			{
				this.theGameRules.readGameRulesFromNBT(p_i2157_1_.getCompoundTag("GameRules"));
			}
		}

		public WorldInfo(WorldSettings p_i2158_1_, string p_i2158_2_)
		{
			this.terrainType = WorldType.DEFAULT;
			this.generatorOptions = "";
			this.theGameRules = new GameRules();
			this.randomSeed = p_i2158_1_.Seed;
			this.theGameType = p_i2158_1_.GameType;
			this.mapFeaturesEnabled = p_i2158_1_.MapFeaturesEnabled;
			this.levelName = p_i2158_2_;
			this.hardcore = p_i2158_1_.HardcoreEnabled;
			this.terrainType = p_i2158_1_.TerrainType;
			this.generatorOptions = p_i2158_1_.func_82749_j();
			this.allowCommands = p_i2158_1_.areCommandsAllowed();
			this.initialized = false;
		}

		public WorldInfo(WorldInfo p_i2159_1_)
		{
			this.terrainType = WorldType.DEFAULT;
			this.generatorOptions = "";
			this.theGameRules = new GameRules();
			this.randomSeed = p_i2159_1_.randomSeed;
			this.terrainType = p_i2159_1_.terrainType;
			this.generatorOptions = p_i2159_1_.generatorOptions;
			this.theGameType = p_i2159_1_.theGameType;
			this.mapFeaturesEnabled = p_i2159_1_.mapFeaturesEnabled;
			this.spawnX = p_i2159_1_.spawnX;
			this.spawnY = p_i2159_1_.spawnY;
			this.spawnZ = p_i2159_1_.spawnZ;
			this.totalTime = p_i2159_1_.totalTime;
			this.worldTime = p_i2159_1_.worldTime;
			this.lastTimePlayed = p_i2159_1_.lastTimePlayed;
			this.sizeOnDisk = p_i2159_1_.sizeOnDisk;
			this.playerTag = p_i2159_1_.playerTag;
			this.dimension = p_i2159_1_.dimension;
			this.levelName = p_i2159_1_.levelName;
			this.saveVersion = p_i2159_1_.saveVersion;
			this.rainTime = p_i2159_1_.rainTime;
			this.raining = p_i2159_1_.raining;
			this.thunderTime = p_i2159_1_.thunderTime;
			this.thundering = p_i2159_1_.thundering;
			this.hardcore = p_i2159_1_.hardcore;
			this.allowCommands = p_i2159_1_.allowCommands;
			this.initialized = p_i2159_1_.initialized;
			this.theGameRules = p_i2159_1_.theGameRules;
		}

///    
///     <summary> * Gets the NBTTagCompound for the worldInfo </summary>
///     
		public virtual NBTTagCompound NBTTagCompound
		{
			get
			{
				NBTTagCompound var1 = new NBTTagCompound();
				this.updateTagCompound(var1, this.playerTag);
				return var1;
			}
		}

///    
///     <summary> * Creates a new NBTTagCompound for the world, with the given NBTTag as the "Player" </summary>
///     
		public virtual NBTTagCompound cloneNBTCompound(NBTTagCompound p_76082_1_)
		{
			NBTTagCompound var2 = new NBTTagCompound();
			this.updateTagCompound(var2, p_76082_1_);
			return var2;
		}

		private void updateTagCompound(NBTTagCompound p_76064_1_, NBTTagCompound p_76064_2_)
		{
			p_76064_1_.setLong("RandomSeed", this.randomSeed);
			p_76064_1_.setString("generatorName", this.terrainType.WorldTypeName);
			p_76064_1_.setInteger("generatorVersion", this.terrainType.GeneratorVersion);
			p_76064_1_.setString("generatorOptions", this.generatorOptions);
			p_76064_1_.setInteger("GameType", this.theGameType.ID);
			p_76064_1_.setBoolean("MapFeatures", this.mapFeaturesEnabled);
			p_76064_1_.setInteger("SpawnX", this.spawnX);
			p_76064_1_.setInteger("SpawnY", this.spawnY);
			p_76064_1_.setInteger("SpawnZ", this.spawnZ);
			p_76064_1_.setLong("Time", this.totalTime);
			p_76064_1_.setLong("DayTime", this.worldTime);
			p_76064_1_.setLong("SizeOnDisk", this.sizeOnDisk);
			p_76064_1_.setLong("LastPlayed", MinecraftServer.SystemTimeMillis);
			p_76064_1_.setString("LevelName", this.levelName);
			p_76064_1_.setInteger("version", this.saveVersion);
			p_76064_1_.setInteger("rainTime", this.rainTime);
			p_76064_1_.setBoolean("raining", this.raining);
			p_76064_1_.setInteger("thunderTime", this.thunderTime);
			p_76064_1_.setBoolean("thundering", this.thundering);
			p_76064_1_.setBoolean("hardcore", this.hardcore);
			p_76064_1_.setBoolean("allowCommands", this.allowCommands);
			p_76064_1_.setBoolean("initialized", this.initialized);
			p_76064_1_.setTag("GameRules", this.theGameRules.writeGameRulesToNBT());

			if (p_76064_2_ != null)
			{
				p_76064_1_.setTag("Player", p_76064_2_);
			}
		}

///    
///     <summary> * Returns the seed of current world. </summary>
///     
		public virtual long Seed
		{
			get
			{
				return this.randomSeed;
			}
		}

///    
///     <summary> * Returns the x spawn position </summary>
///     
		public virtual int SpawnX
		{
			get
			{
				return this.spawnX;
			}
			set
			{
				this.spawnX = value;
			}
		}

///    
///     <summary> * Return the Y axis spawning point of the player. </summary>
///     
		public virtual int SpawnY
		{
			get
			{
				return this.spawnY;
			}
			set
			{
				this.spawnY = value;
			}
		}

///    
///     <summary> * Returns the z spawn position </summary>
///     
		public virtual int SpawnZ
		{
			get
			{
				return this.spawnZ;
			}
			set
			{
				this.spawnZ = value;
			}
		}

		public virtual long WorldTotalTime
		{
			get
			{
				return this.totalTime;
			}
		}

///    
///     <summary> * Get current world time </summary>
///     
		public virtual long WorldTime
		{
			get
			{
				return this.worldTime;
			}
			set
			{
				this.worldTime = value;
			}
		}

		public virtual long SizeOnDisk
		{
			get
			{
				return this.sizeOnDisk;
			}
		}

///    
///     <summary> * Returns the player's NBTTagCompound to be loaded </summary>
///     
		public virtual NBTTagCompound PlayerNBTTagCompound
		{
			get
			{
				return this.playerTag;
			}
		}

///    
///     <summary> * Returns vanilla MC dimension (-1,0,1). For custom dimension compatibility, always prefer
///     * WorldProvider.dimensionID accessed from World.provider.dimensionID </summary>
///     
		public virtual int VanillaDimension
		{
			get
			{
				return this.dimension;
			}
		}

///    
///     <summary> * Set the x spawn position to the passed in value </summary>
///     

///    
///     <summary> * Sets the y spawn position </summary>
///     

///    
///     <summary> * Set the z spawn position to the passed in value </summary>
///     

		public virtual void incrementTotalWorldTime(long p_82572_1_)
		{
			this.totalTime = p_82572_1_;
		}

///    
///     <summary> * Set current world time </summary>
///     

///    
///     <summary> * Sets the spawn zone position. Args: x, y, z </summary>
///     
		public virtual void setSpawnPosition(int p_76081_1_, int p_76081_2_, int p_76081_3_)
		{
			this.spawnX = p_76081_1_;
			this.spawnY = p_76081_2_;
			this.spawnZ = p_76081_3_;
		}

///    
///     <summary> * Get current world name </summary>
///     
		public virtual string WorldName
		{
			get
			{
				return this.levelName;
			}
			set
			{
				this.levelName = value;
			}
		}


///    
///     <summary> * Returns the save version of this world </summary>
///     
		public virtual int SaveVersion
		{
			get
			{
				return this.saveVersion;
			}
			set
			{
				this.saveVersion = value;
			}
		}

///    
///     <summary> * Sets the save version of the world </summary>
///     

///    
///     <summary> * Return the last time the player was in this world. </summary>
///     
		public virtual long LastTimePlayed
		{
			get
			{
				return this.lastTimePlayed;
			}
		}

///    
///     <summary> * Returns true if it is thundering, false otherwise. </summary>
///     
		public virtual bool isThundering()
		{
			get
			{
				return this.thundering;
			}
			set
			{
				this.thundering = value;
			}
		}

///    
///     <summary> * Sets whether it is thundering or not. </summary>
///     

///    
///     <summary> * Returns the number of ticks until next thunderbolt. </summary>
///     
		public virtual int ThunderTime
		{
			get
			{
				return this.thunderTime;
			}
			set
			{
				this.thunderTime = value;
			}
		}

///    
///     <summary> * Defines the number of ticks until next thunderbolt. </summary>
///     

///    
///     <summary> * Returns true if it is raining, false otherwise. </summary>
///     
		public virtual bool isRaining()
		{
			get
			{
				return this.raining;
			}
			set
			{
				this.raining = value;
			}
		}

///    
///     <summary> * Sets whether it is raining or not. </summary>
///     

///    
///     <summary> * Return the number of ticks until rain. </summary>
///     
		public virtual int RainTime
		{
			get
			{
				return this.rainTime;
			}
			set
			{
				this.rainTime = value;
			}
		}

///    
///     <summary> * Sets the number of ticks until rain. </summary>
///     

///    
///     <summary> * Gets the GameType. </summary>
///     
		public virtual WorldSettings.GameType GameType
		{
			get
			{
				return this.theGameType;
			}
			set
			{
				this.theGameType = value;
			}
		}

///    
///     <summary> * Get whether the map features (e.g. strongholds) generation is enabled or disabled. </summary>
///     
		public virtual bool isMapFeaturesEnabled()
		{
			get
			{
				return this.mapFeaturesEnabled;
			}
		}

///    
///     <summary> * Sets the GameType. </summary>
///     

///    
///     <summary> * Returns true if hardcore mode is enabled, otherwise false </summary>
///     
		public virtual bool isHardcoreModeEnabled()
		{
			get
			{
				return this.hardcore;
			}
		}

		public virtual WorldType TerrainType
		{
			get
			{
				return this.terrainType;
			}
			set
			{
				this.terrainType = value;
			}
		}


		public virtual string GeneratorOptions
		{
			get
			{
				return this.generatorOptions;
			}
		}

///    
///     <summary> * Returns true if commands are allowed on this World. </summary>
///     
		public virtual bool areCommandsAllowed()
		{
			return this.allowCommands;
		}

///    
///     <summary> * Returns true if the World is initialized. </summary>
///     
		public virtual bool isInitialized()
		{
			get
			{
				return this.initialized;
			}
		}

///    
///     <summary> * Sets the initialization status of the World. </summary>
///     
		public virtual bool ServerInitialized
		{
			set
			{
				this.initialized = value;
			}
		}

///    
///     <summary> * Gets the GameRules class Instance. </summary>
///     
		public virtual GameRules GameRulesInstance
		{
			get
			{
				return this.theGameRules;
			}
		}

///    
///     <summary> * Adds this WorldInfo instance to the crash report. </summary>
///     
		public virtual void addToCrashReport(CrashReportCategory p_85118_1_)
		{
			p_85118_1_.addCrashSectionCallable("Level seed", new Callable() { private static final string __OBFID = "CL_00000588"; public string call() { return Convert.ToString(WorldInfo.Seed); } });
//JAVA TO VB & C# CONVERTER TODO TASK: The following line has a Java format specifier which cannot be directly translated to .NET:
//ORIGINAL LINE: p_85118_1_.addCrashSectionCallable("Level generator", new Callable() { private static final String __OBFID = "CL_00000589"; public String call() { return String.format("ID %02d - %s, ver %d. Features enabled: %b", new Object[] {Integer.valueOf(WorldInfo.terrainType.getWorldTypeID()), WorldInfo.terrainType.getWorldTypeName(), Integer.valueOf(WorldInfo.terrainType.getGeneratorVersion()), Boolean.valueOf(WorldInfo.mapFeaturesEnabled)}); } });
			p_85118_1_.addCrashSectionCallable("Level generator", new Callable() { private static final string __OBFID = "CL_00000589"; public string call() { return string.Format("ID %02d - %s, ver %d. Features enabled: %b", new object[] {Convert.ToInt32(WorldInfo.terrainType.WorldTypeID), WorldInfo.terrainType.WorldTypeName, Convert.ToInt32(WorldInfo.terrainType.GeneratorVersion), Convert.ToBoolean(WorldInfo.mapFeaturesEnabled)}); } });
			p_85118_1_.addCrashSectionCallable("Level generator options", new Callable() { private static final string __OBFID = "CL_00000590"; public string call() { return WorldInfo.generatorOptions; } });
			p_85118_1_.addCrashSectionCallable("Level spawn location", new Callable() { private static final string __OBFID = "CL_00000591"; public string call() { return CrashReportCategory.getLocationInfo(WorldInfo.spawnX, WorldInfo.spawnY, WorldInfo.spawnZ); } });
			p_85118_1_.addCrashSectionCallable("Level time", new Callable() { private static final string __OBFID = "CL_00000592"; public string call() { return string.Format("{0:D} game time, {1:D} day time", new object[] {Convert.ToInt64(WorldInfo.totalTime), Convert.ToInt64(WorldInfo.worldTime)}); } });
			p_85118_1_.addCrashSectionCallable("Level dimension", new Callable() { private static final string __OBFID = "CL_00000593"; public string call() { return Convert.ToString(WorldInfo.dimension); } });
			p_85118_1_.addCrashSectionCallable("Level storage version", new Callable() { private static final string __OBFID = "CL_00000594"; public string call() { string var1 = "Unknown?"; try { switch (WorldInfo.saveVersion) { case 19132: var1 = "McRegion"; break; case 19133: var1 = "Anvil"; } } catch (Exception var3) { ; } return string.Format("0x{0:X5} - {1}", new object[] {Convert.ToInt32(WorldInfo.saveVersion), var1}); } });
//JAVA TO VB & C# CONVERTER TODO TASK: The following line has a Java format specifier which cannot be directly translated to .NET:
//ORIGINAL LINE: p_85118_1_.addCrashSectionCallable("Level weather", new Callable() { private static final String __OBFID = "CL_00000595"; public String call() { return String.format("Rain time: %d (now: %b), thunder time: %d (now: %b)", new Object[] {Integer.valueOf(WorldInfo.rainTime), Boolean.valueOf(WorldInfo.raining), Integer.valueOf(WorldInfo.thunderTime), Boolean.valueOf(WorldInfo.thundering)}); } });
			p_85118_1_.addCrashSectionCallable("Level weather", new Callable() { private static final string __OBFID = "CL_00000595"; public string call() { return string.Format("Rain time: %d (now: %b), thunder time: %d (now: %b)", new object[] {Convert.ToInt32(WorldInfo.rainTime), Convert.ToBoolean(WorldInfo.raining), Convert.ToInt32(WorldInfo.thunderTime), Convert.ToBoolean(WorldInfo.thundering)}); } });
//JAVA TO VB & C# CONVERTER TODO TASK: The following line has a Java format specifier which cannot be directly translated to .NET:
//ORIGINAL LINE: p_85118_1_.addCrashSectionCallable("Level game mode", new Callable() { private static final String __OBFID = "CL_00000597"; public String call() { return String.format("Game mode: %s (ID %d). Hardcore: %b. Cheats: %b", new Object[] {WorldInfo.theGameType.getName(), Integer.valueOf(WorldInfo.theGameType.getID()), Boolean.valueOf(WorldInfo.hardcore), Boolean.valueOf(WorldInfo.allowCommands)}); } });
			p_85118_1_.addCrashSectionCallable("Level game mode", new Callable() { private static final string __OBFID = "CL_00000597"; public string call() { return string.Format("Game mode: %s (ID %d). Hardcore: %b. Cheats: %b", new object[] {WorldInfo.theGameType.Name, Convert.ToInt32(WorldInfo.theGameType.ID), Convert.ToBoolean(WorldInfo.hardcore), Convert.ToBoolean(WorldInfo.allowCommands)}); } });
		}
	}

}