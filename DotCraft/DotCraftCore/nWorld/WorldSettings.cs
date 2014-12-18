namespace DotCraftCore.nWorld
{

	using PlayerCapabilities = DotCraftCore.entity.player.PlayerCapabilities;
	using WorldInfo = DotCraftCore.nWorld.nStorage.WorldInfo;

	public sealed class WorldSettings
	{
	/// <summary> The seed for the map.  </summary>
		private readonly long seed;

	/// <summary> The EnumGameType.  </summary>
		private readonly WorldSettings.GameType theGameType;

///    
///     <summary> * Switch for the map features. 'true' for enabled, 'false' for disabled. </summary>
///     
		private readonly bool mapFeaturesEnabled;

	/// <summary> True if hardcore mode is enabled  </summary>
		private readonly bool hardcoreEnabled;
		private readonly WorldType terrainType;

	/// <summary> True if Commands (cheats) are allowed.  </summary>
		private bool commandsAllowed;

	/// <summary> True if the Bonus Chest is enabled.  </summary>
		private bool bonusChestEnabled;
		private string field_82751_h;
		

		public WorldSettings(long p_i1957_1_, WorldSettings.GameType p_i1957_3_, bool p_i1957_4_, bool p_i1957_5_, WorldType p_i1957_6_)
		{
			this.field_82751_h = "";
			this.seed = p_i1957_1_;
			this.theGameType = p_i1957_3_;
			this.mapFeaturesEnabled = p_i1957_4_;
			this.hardcoreEnabled = p_i1957_5_;
			this.terrainType = p_i1957_6_;
		}

		public WorldSettings(WorldInfo p_i1958_1_) : this(p_i1958_1_.getSeed(), p_i1958_1_.getGameType(), p_i1958_1_.isMapFeaturesEnabled(), p_i1958_1_.isHardcoreModeEnabled(), p_i1958_1_.getTerrainType())
		{
		}

///    
///     <summary> * Enables the bonus chest. </summary>
///     
		public WorldSettings enableBonusChest()
		{
			this.bonusChestEnabled = true;
			return this;
		}

///    
///     <summary> * Enables Commands (cheats). </summary>
///     
		public WorldSettings enableCommands()
		{
			this.commandsAllowed = true;
			return this;
		}

		public WorldSettings func_82750_a(string p_82750_1_)
		{
			this.field_82751_h = p_82750_1_;
			return this;
		}

///    
///     <summary> * Returns true if the Bonus Chest is enabled. </summary>
///     
		public bool isBonusChestEnabled()
		{
			get
			{
				return this.bonusChestEnabled;
			}
		}

///    
///     <summary> * Returns the seed for the world. </summary>
///     
		public long Seed
		{
			get
			{
				return this.seed;
			}
		}

///    
///     <summary> * Gets the game type. </summary>
///     
		public WorldSettings.GameType GameType
		{
			get
			{
				return this.theGameType;
			}
		}

///    
///     <summary> * Returns true if hardcore mode is enabled, otherwise false </summary>
///     
		public bool HardcoreEnabled
		{
			get
			{
				return this.hardcoreEnabled;
			}
		}

///    
///     <summary> * Get whether the map features (e.g. strongholds) generation is enabled or disabled. </summary>
///     
		public bool isMapFeaturesEnabled()
		{
			get
			{
				return this.mapFeaturesEnabled;
			}
		}

		public WorldType TerrainType
		{
			get
			{
				return this.terrainType;
			}
		}

///    
///     <summary> * Returns true if Commands (cheats) are allowed. </summary>
///     
		public bool areCommandsAllowed()
		{
			return this.commandsAllowed;
		}

///    
///     <summary> * Gets the GameType by ID </summary>
///     
		public static WorldSettings.GameType getGameTypeById(int p_77161_0_)
		{
			return WorldSettings.GameType.getByID(p_77161_0_);
		}

		public string func_82749_j()
		{
			return this.field_82751_h;
		}

		public enum GameType
		{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			NOT_SET("NOT_SET", 0, -1, ""),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			SURVIVAL("SURVIVAL", 1, 0, "survival"),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			CREATIVE("CREATIVE", 2, 1, "creative"),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			ADVENTURE("ADVENTURE", 3, 2, "adventure");
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			int id;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			String name;

			@private static final WorldSettings.GameType[] $VALUES = new WorldSettings.GameType[]{NOT_SET, SURVIVAL, CREATIVE, ADVENTURE
		}
			

			private GameType(string p_i1956_1_, int p_i1956_2_, int p_i1956_3_, string p_i1956_4_)
			{
				this.id = p_i1956_3_;
				this.name = p_i1956_4_;
			}

			public int ID
			{
				get
				{
					return this.id;
				}
			}

			public string Name
			{
				get
				{
					return this.name;
				}
			}

			public void configurePlayerCapabilities(PlayerCapabilities p_77147_1_)
			{
				if (this == CREATIVE)
				{
					p_77147_1_.allowFlying = true;
					p_77147_1_.isCreativeMode = true;
					p_77147_1_.disableDamage = true;
				}
				else
				{
					p_77147_1_.allowFlying = false;
					p_77147_1_.isCreativeMode = false;
					p_77147_1_.disableDamage = false;
					p_77147_1_.isFlying = false;
				}

				p_77147_1_.allowEdit = !this.Adventure;
			}

			public bool isAdventure()
			{
				get
				{
					return this == ADVENTURE;
				}
			}

			public bool isCreative()
			{
				get
				{
					return this == CREATIVE;
				}
			}

			public bool isSurvivalOrAdventure()
			{
				get
				{
					return this == SURVIVAL || this == ADVENTURE;
				}
			}

			public static WorldSettings.GameType getByID(int p_77146_0_)
			{
				WorldSettings.GameType[] var1 = values();
				int var2 = var1.Length;

				for (int var3 = 0; var3 < var2; ++var3)
				{
					WorldSettings.GameType var4 = var1[var3];

					if (var4.id == p_77146_0_)
					{
						return var4;
					}
				}

				return SURVIVAL;
			}

			public static WorldSettings.GameType getByName(string p_77142_0_)
			{
				WorldSettings.GameType[] var1 = values();
				int var2 = var1.Length;

				for (int var3 = 0; var3 < var2; ++var3)
				{
					WorldSettings.GameType var4 = var1[var3];

					if (var4.name.Equals(p_77142_0_))
					{
						return var4;
					}
				}

				return SURVIVAL;
			}
		}
	}

}