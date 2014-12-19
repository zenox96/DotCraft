namespace DotCraftCore.nWorld.nStorage
{

	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using GameRules = DotCraftCore.nWorld.GameRules;
	using WorldSettings = DotCraftCore.nWorld.WorldSettings;
	using WorldType = DotCraftCore.nWorld.WorldType;

	public class DerivedWorldInfo : WorldInfo
	{
	/// <summary> Instance of WorldInfo.  </summary>
		private readonly WorldInfo theWorldInfo;
		

		public DerivedWorldInfo(WorldInfo p_i2145_1_)
		{
			this.theWorldInfo = p_i2145_1_;
		}

///    
///     <summary> * Gets the NBTTagCompound for the worldInfo </summary>
///     
		public override NBTTagCompound NBTTagCompound
		{
			get
			{
				return this.theWorldInfo.NBTTagCompound;
			}
		}

///    
///     <summary> * Creates a new NBTTagCompound for the world, with the given NBTTag as the "Player" </summary>
///     
		public override NBTTagCompound cloneNBTCompound(NBTTagCompound p_76082_1_)
		{
			return this.theWorldInfo.cloneNBTCompound(p_76082_1_);
		}

///    
///     <summary> * Returns the seed of current world. </summary>
///     
		public override long Seed
		{
			get
			{
				return this.theWorldInfo.Seed;
			}
		}

///    
///     <summary> * Returns the x spawn position </summary>
///     
		public override int SpawnX
		{
			get
			{
				return this.theWorldInfo.SpawnX;
			}
			set
			{
			}
		}

///    
///     <summary> * Return the Y axis spawning point of the player. </summary>
///     
		public override int SpawnY
		{
			get
			{
				return this.theWorldInfo.SpawnY;
			}
			set
			{
			}
		}

///    
///     <summary> * Returns the z spawn position </summary>
///     
		public override int SpawnZ
		{
			get
			{
				return this.theWorldInfo.SpawnZ;
			}
			set
			{
			}
		}

		public override long WorldTotalTime
		{
			get
			{
				return this.theWorldInfo.WorldTotalTime;
			}
		}

///    
///     <summary> * Get current world time </summary>
///     
		public override long WorldTime
		{
			get
			{
				return this.theWorldInfo.WorldTime;
			}
			set
			{
			}
		}

		public override long SizeOnDisk
		{
			get
			{
				return this.theWorldInfo.SizeOnDisk;
			}
		}

///    
///     <summary> * Returns the player's NBTTagCompound to be loaded </summary>
///     
		public override NBTTagCompound PlayerNBTTagCompound
		{
			get
			{
				return this.theWorldInfo.PlayerNBTTagCompound;
			}
		}

///    
///     <summary> * Returns vanilla MC dimension (-1,0,1). For custom dimension compatibility, always prefer
///     * WorldProvider.dimensionID accessed from World.provider.dimensionID </summary>
///     
		public override int VanillaDimension
		{
			get
			{
				return this.theWorldInfo.VanillaDimension;
			}
		}

///    
///     <summary> * Get current world name </summary>
///     
		public override string WorldName
		{
			get
			{
				return this.theWorldInfo.WorldName;
			}
			set
			{
			}
		}

///    
///     <summary> * Returns the save version of this world </summary>
///     
		public override int SaveVersion
		{
			get
			{
				return this.theWorldInfo.SaveVersion;
			}
			set
			{
			}
		}

///    
///     <summary> * Return the last time the player was in this world. </summary>
///     
		public override long LastTimePlayed
		{
			get
			{
				return this.theWorldInfo.LastTimePlayed;
			}
		}

///    
///     <summary> * Returns true if it is thundering, false otherwise. </summary>
///     
		public override bool isThundering()
		{
			get
			{
				return this.theWorldInfo.Thundering;
			}
			set
			{
			}
		}

///    
///     <summary> * Returns the number of ticks until next thunderbolt. </summary>
///     
		public override int ThunderTime
		{
			get
			{
				return this.theWorldInfo.ThunderTime;
			}
			set
			{
			}
		}

///    
///     <summary> * Returns true if it is raining, false otherwise. </summary>
///     
		public override bool isRaining()
		{
			get
			{
				return this.theWorldInfo.Raining;
			}
			set
			{
			}
		}

///    
///     <summary> * Return the number of ticks until rain. </summary>
///     
		public override int RainTime
		{
			get
			{
				return this.theWorldInfo.RainTime;
			}
			set
			{
			}
		}

///    
///     <summary> * Gets the GameType. </summary>
///     
		public override WorldSettings.GameType GameType
		{
			get
			{
				return this.theWorldInfo.GameType;
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

		public override void incrementTotalWorldTime(long p_82572_1_)
		{
		}

///    
///     <summary> * Set current world time </summary>
///     

///    
///     <summary> * Sets the spawn zone position. Args: x, y, z </summary>
///     
		public override void setSpawnPosition(int p_76081_1_, int p_76081_2_, int p_76081_3_)
		{
		}


///    
///     <summary> * Sets the save version of the world </summary>
///     

///    
///     <summary> * Sets whether it is thundering or not. </summary>
///     

///    
///     <summary> * Defines the number of ticks until next thunderbolt. </summary>
///     

///    
///     <summary> * Sets whether it is raining or not. </summary>
///     

///    
///     <summary> * Sets the number of ticks until rain. </summary>
///     

///    
///     <summary> * Get whether the map features (e.g. strongholds) generation is enabled or disabled. </summary>
///     
		public override bool isMapFeaturesEnabled()
		{
			get
			{
				return this.theWorldInfo.MapFeaturesEnabled;
			}
		}

///    
///     <summary> * Returns true if hardcore mode is enabled, otherwise false </summary>
///     
		public override bool isHardcoreModeEnabled()
		{
			get
			{
				return this.theWorldInfo.HardcoreModeEnabled;
			}
		}

		public override WorldType TerrainType
		{
			get
			{
				return this.theWorldInfo.TerrainType;
			}
			set
			{
			}
		}


///    
///     <summary> * Returns true if commands are allowed on this World. </summary>
///     
		public override bool areCommandsAllowed()
		{
			return this.theWorldInfo.areCommandsAllowed();
		}

///    
///     <summary> * Returns true if the World is initialized. </summary>
///     
		public override bool isInitialized()
		{
			get
			{
				return this.theWorldInfo.Initialized;
			}
		}

///    
///     <summary> * Sets the initialization status of the World. </summary>
///     
		public override bool ServerInitialized
		{
			set
			{
			}
		}

///    
///     <summary> * Gets the GameRules class Instance. </summary>
///     
		public override GameRules GameRulesInstance
		{
			get
			{
				return this.theWorldInfo.GameRulesInstance;
			}
		}
	}

}