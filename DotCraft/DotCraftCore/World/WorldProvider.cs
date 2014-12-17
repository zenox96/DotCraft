using System;

namespace DotCraftCore.nWorld
{

	using Blocks = DotCraftCore.init.Blocks;
	using ChunkCoordinates = DotCraftCore.nUtil.ChunkCoordinates;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using Vec3 = DotCraftCore.nUtil.Vec3;
	using BiomeGenBase = DotCraftCore.nWorld.nBiome.BiomeGenBase;
	using WorldChunkManager = DotCraftCore.nWorld.nBiome.WorldChunkManager;
	using WorldChunkManagerHell = DotCraftCore.nWorld.nBiome.WorldChunkManagerHell;
	using IChunkProvider = DotCraftCore.nWorld.nChunk.IChunkProvider;
	using ChunkProviderFlat = DotCraftCore.nWorld.nGen.ChunkProviderFlat;
	using ChunkProviderGenerate = DotCraftCore.nWorld.nGen.ChunkProviderGenerate;
	using FlatGeneratorInfo = DotCraftCore.nWorld.nGen.FlatGeneratorInfo;

	public abstract class WorldProvider
	{
		public static readonly float[] moonPhaseFactors = new float[] {1.0F, 0.75F, 0.5F, 0.25F, 0.0F, 0.25F, 0.5F, 0.75F};

	/// <summary> world object being used  </summary>
		public World worldObj;
		public WorldType terrainType;
		public string field_82913_c;

	/// <summary> World chunk manager being used to generate chunks  </summary>
		public WorldChunkManager worldChunkMgr;

///    
///     <summary> * States whether the Hell world provider is used(true) or if the normal world provider is used(false) </summary>
///     
		public bool isHellWorld;

///    
///     <summary> * A boolean that tells if a world does not have a sky. Used in calculating weather and skylight </summary>
///     
		public bool hasNoSky;

	/// <summary> Light to brightness conversion table  </summary>
		public float[] lightBrightnessTable = new float[16];

	/// <summary> The id for the dimension (ex. -1: Nether, 0: Overworld, 1: The End)  </summary>
		public int dimensionId;

	/// <summary> Array for sunrise/sunset colors (RGBA)  </summary>
		private float[] colorsSunriseSunset = new float[4];
		

///    
///     <summary> * associate an existing world with a World provider, and setup its lightbrightness table </summary>
///     
		public void registerWorld(World p_76558_1_)
		{
			this.worldObj = p_76558_1_;
			this.terrainType = p_76558_1_.WorldInfo.TerrainType;
			this.field_82913_c = p_76558_1_.WorldInfo.GeneratorOptions;
			this.registerWorldChunkManager();
			this.generateLightBrightnessTable();
		}

///    
///     <summary> * Creates the light to brightness table </summary>
///     
		protected internal virtual void generateLightBrightnessTable()
		{
			float var1 = 0.0F;

			for (int var2 = 0; var2 <= 15; ++var2)
			{
				float var3 = 1.0F - (float)var2 / 15.0F;
				this.lightBrightnessTable[var2] = (1.0F - var3) / (var3 * 3.0F + 1.0F) * (1.0F - var1) + var1;
			}
		}

///    
///     <summary> * creates a new world chunk manager for WorldProvider </summary>
///     
		protected internal virtual void registerWorldChunkManager()
		{
			if (this.worldObj.WorldInfo.TerrainType == WorldType.FLAT)
			{
				FlatGeneratorInfo var1 = FlatGeneratorInfo.createFlatGeneratorFromString(this.worldObj.WorldInfo.GeneratorOptions);
				this.worldChunkMgr = new WorldChunkManagerHell(BiomeGenBase.func_150568_d(var1.Biome), 0.5F);
			}
			else
			{
				this.worldChunkMgr = new WorldChunkManager(this.worldObj);
			}
		}

///    
///     <summary> * Returns a new chunk provider which generates chunks for this world </summary>
///     
		public virtual IChunkProvider createChunkGenerator()
		{
			return (IChunkProvider)(this.terrainType == WorldType.FLAT ? new ChunkProviderFlat(this.worldObj, this.worldObj.Seed, this.worldObj.WorldInfo.MapFeaturesEnabled, this.field_82913_c) : new ChunkProviderGenerate(this.worldObj, this.worldObj.Seed, this.worldObj.WorldInfo.MapFeaturesEnabled));
		}

///    
///     <summary> * Will check if the x, z position specified is alright to be set as the map spawn point </summary>
///     
		public virtual bool canCoordinateBeSpawn(int p_76566_1_, int p_76566_2_)
		{
			return this.worldObj.getTopBlock(p_76566_1_, p_76566_2_) == Blocks.grass;
		}

///    
///     <summary> * Calculates the angle of sun and moon in the sky relative to a specified time (usually worldTime) </summary>
///     
		public virtual float calculateCelestialAngle(long p_76563_1_, float p_76563_3_)
		{
			int var4 = (int)(p_76563_1_ % 24000L);
			float var5 = ((float)var4 + p_76563_3_) / 24000.0F - 0.25F;

			if (var5 < 0.0F)
			{
				++var5;
			}

			if (var5 > 1.0F)
			{
				--var5;
			}

			float var6 = var5;
			var5 = 1.0F - (float)((Math.Cos((double)var5 * Math.PI) + 1.0D) / 2.0D);
			var5 = var6 + (var5 - var6) / 3.0F;
			return var5;
		}

		public virtual int getMoonPhase(long p_76559_1_)
		{
			return (int)(p_76559_1_ / 24000L % 8L + 8L) % 8;
		}

///    
///     <summary> * Returns 'true' if in the "main surface world", but 'false' if in the Nether or End dimensions. </summary>
///     
		public virtual bool isSurfaceWorld()
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * Returns array with sunrise/sunset colors </summary>
///     
		public virtual float[] calcSunriseSunsetColors(float p_76560_1_, float p_76560_2_)
		{
			float var3 = 0.4F;
			float var4 = MathHelper.cos(p_76560_1_ * (float)Math.PI * 2.0F) - 0.0F;
			float var5 = -0.0F;

			if (var4 >= var5 - var3 && var4 <= var5 + var3)
			{
				float var6 = (var4 - var5) / var3 * 0.5F + 0.5F;
				float var7 = 1.0F - (1.0F - MathHelper.sin(var6 * (float)Math.PI)) * 0.99F;
				var7 *= var7;
				this.colorsSunriseSunset[0] = var6 * 0.3F + 0.7F;
				this.colorsSunriseSunset[1] = var6 * var6 * 0.7F + 0.2F;
				this.colorsSunriseSunset[2] = var6 * var6 * 0.0F + 0.2F;
				this.colorsSunriseSunset[3] = var7;
				return this.colorsSunriseSunset;
			}
			else
			{
				return null;
			}
		}

///    
///     <summary> * Return Vec3D with biome specific fog color </summary>
///     
		public virtual Vec3 getFogColor(float p_76562_1_, float p_76562_2_)
		{
			float var3 = MathHelper.cos(p_76562_1_ * (float)Math.PI * 2.0F) * 2.0F + 0.5F;

			if (var3 < 0.0F)
			{
				var3 = 0.0F;
			}

			if (var3 > 1.0F)
			{
				var3 = 1.0F;
			}

			float var4 = 0.7529412F;
			float var5 = 0.84705883F;
			float var6 = 1.0F;
			var4 *= var3 * 0.94F + 0.06F;
			var5 *= var3 * 0.94F + 0.06F;
			var6 *= var3 * 0.91F + 0.09F;
			return Vec3.createVectorHelper((double)var4, (double)var5, (double)var6);
		}

///    
///     <summary> * True if the player can respawn in this dimension (true = overworld, false = nether). </summary>
///     
		public virtual bool canRespawnHere()
		{
			return true;
		}

		public static WorldProvider getProviderForDimension(int p_76570_0_)
		{
			return (WorldProvider)(p_76570_0_ == -1 ? new WorldProviderHell() : (p_76570_0_ == 0 ? new WorldProviderSurface() : (p_76570_0_ == 1 ? new WorldProviderEnd() : null)));
		}

///    
///     <summary> * the y level at which clouds are rendered. </summary>
///     
		public virtual float CloudHeight
		{
			get
			{
				return 128.0F;
			}
		}

		public virtual bool isSkyColored()
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * Gets the hard-coded portal location to use when entering this dimension. </summary>
///     
		public virtual ChunkCoordinates EntrancePortalLocation
		{
			get
			{
				return null;
			}
		}

		public virtual int AverageGroundLevel
		{
			get
			{
				return this.terrainType == WorldType.FLAT ? 4 : 64;
			}
		}

///    
///     <summary> * returns true if this dimension is supposed to display void particles and pull in the far plane based on the
///     * user's Y offset. </summary>
///     
		public virtual bool WorldHasVoidParticles
		{
			get
			{
				return this.terrainType != WorldType.FLAT && !this.hasNoSky;
			}
		}

///    
///     <summary> * Returns a double value representing the Y value relative to the top of the map at which void fog is at its
///     * maximum. The default factor of 0.03125 relative to 256, for example, means the void fog will be at its maximum at
///     * (256*0.03125), or 8. </summary>
///     
		public virtual double VoidFogYFactor
		{
			get
			{
				return this.terrainType == WorldType.FLAT ? 1.0D : 0.03125D;
			}
		}

///    
///     <summary> * Returns true if the given X,Z coordinate should show environmental fog. </summary>
///     
		public virtual bool doesXZShowFog(int p_76568_1_, int p_76568_2_)
		{
			return false;
		}

///    
///     <summary> * Returns the dimension's name, e.g. "The End", "Nether", or "Overworld". </summary>
///     
		public abstract string DimensionName {get;}
	}

}