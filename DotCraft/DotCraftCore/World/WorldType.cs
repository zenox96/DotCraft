namespace DotCraftCore.World
{

	public class WorldType
	{
	/// <summary> List of world types.  </summary>
		public static readonly WorldType[] worldTypes = new WorldType[16];

	/// <summary> Default world type.  </summary>
		public static readonly WorldType DEFAULT = (new WorldType(0, "default", 1)).setVersioned();

	/// <summary> Flat world type.  </summary>
		public static readonly WorldType FLAT = new WorldType(1, "flat");

	/// <summary> Large Biome world Type.  </summary>
		public static readonly WorldType LARGE_BIOMES = new WorldType(2, "largeBiomes");
		public static readonly WorldType field_151360_e = (new WorldType(3, "amplified")).func_151358_j();

	/// <summary> Default (1.1) world type.  </summary>
		public static readonly WorldType DEFAULT_1_1 = (new WorldType(8, "default_1_1", 0)).CanBeCreated = false;

	/// <summary> ID for this world type.  </summary>
		private readonly int worldTypeId;

	/// <summary> 'default' or 'flat'  </summary>
		private readonly string worldType;

	/// <summary> The int version of the ChunkProvider that generated this world.  </summary>
		private readonly int generatorVersion;

///    
///     <summary> * Whether this world type can be generated. Normally true; set to false for out-of-date generator versions. </summary>
///     
		private bool canBeCreated;

	/// <summary> Whether this WorldType has a version or not.  </summary>
		private bool isWorldTypeVersioned;
		private bool field_151361_l;
		

		private WorldType(int p_i1959_1_, string p_i1959_2_) : this(p_i1959_1_, p_i1959_2_, 0)
		{
		}

		private WorldType(int p_i1960_1_, string p_i1960_2_, int p_i1960_3_)
		{
			this.worldType = p_i1960_2_;
			this.generatorVersion = p_i1960_3_;
			this.canBeCreated = true;
			this.worldTypeId = p_i1960_1_;
			worldTypes[p_i1960_1_] = this;
		}

		public virtual string WorldTypeName
		{
			get
			{
				return this.worldType;
			}
		}

///    
///     <summary> * Gets the translation key for the name of this world type. </summary>
///     
		public virtual string TranslateName
		{
			get
			{
				return "generator." + this.worldType;
			}
		}

		public virtual string func_151359_c()
		{
			return this.TranslateName + ".info";
		}

///    
///     <summary> * Returns generatorVersion. </summary>
///     
		public virtual int GeneratorVersion
		{
			get
			{
				return this.generatorVersion;
			}
		}

		public virtual WorldType getWorldTypeForGeneratorVersion(int p_77132_1_)
		{
			return this == DEFAULT && p_77132_1_ == 0 ? DEFAULT_1_1 : this;
		}

///    
///     <summary> * Sets canBeCreated to the provided value, and returns this. </summary>
///     
		private WorldType CanBeCreated
		{
			set
			{
				this.canBeCreated = value;
				return this;
			}
			get
			{
				return this.canBeCreated;
			}
		}

///    
///     <summary> * Gets whether this WorldType can be used to generate a new world. </summary>
///     

///    
///     <summary> * Flags this world type as having an associated version. </summary>
///     
		private WorldType setVersioned()
		{
			this.isWorldTypeVersioned = true;
			return this;
		}

///    
///     <summary> * Returns true if this world Type has a version associated with it. </summary>
///     
		public virtual bool isVersioned()
		{
			get
			{
				return this.isWorldTypeVersioned;
			}
		}

		public static WorldType parseWorldType(string p_77130_0_)
		{
			for (int var1 = 0; var1 < worldTypes.Length; ++var1)
			{
				if (worldTypes[var1] != null && worldTypes[var1].worldType.equalsIgnoreCase(p_77130_0_))
				{
					return worldTypes[var1];
				}
			}

			return null;
		}

		public virtual int WorldTypeID
		{
			get
			{
				return this.worldTypeId;
			}
		}

		public virtual bool func_151357_h()
		{
			return this.field_151361_l;
		}

		private WorldType func_151358_j()
		{
			this.field_151361_l = true;
			return this;
		}
	}

}