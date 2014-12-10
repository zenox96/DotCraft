namespace DotCraftCore.World
{

	using Vec3 = DotCraftCore.Util.Vec3;
	using BiomeGenBase = DotCraftCore.World.Biome.BiomeGenBase;
	using WorldChunkManagerHell = DotCraftCore.World.Biome.WorldChunkManagerHell;
	using IChunkProvider = DotCraftCore.World.Chunk.IChunkProvider;
	using ChunkProviderHell = DotCraftCore.World.Gen.ChunkProviderHell;

	public class WorldProviderHell : WorldProvider
	{
		private const string __OBFID = "CL_00000387";

///    
///     <summary> * creates a new world chunk manager for WorldProvider </summary>
///     
		public override void registerWorldChunkManager()
		{
			this.worldChunkMgr = new WorldChunkManagerHell(BiomeGenBase.hell, 0.0F);
			this.isHellWorld = true;
			this.hasNoSky = true;
			this.dimensionId = -1;
		}

///    
///     <summary> * Return Vec3D with biome specific fog color </summary>
///     
		public override Vec3 getFogColor(float p_76562_1_, float p_76562_2_)
		{
			return Vec3.createVectorHelper(0.20000000298023224D, 0.029999999329447746D, 0.029999999329447746D);
		}

///    
///     <summary> * Creates the light to brightness table </summary>
///     
		protected internal override void generateLightBrightnessTable()
		{
			float var1 = 0.1F;

			for (int var2 = 0; var2 <= 15; ++var2)
			{
				float var3 = 1.0F - (float)var2 / 15.0F;
				this.lightBrightnessTable[var2] = (1.0F - var3) / (var3 * 3.0F + 1.0F) * (1.0F - var1) + var1;
			}
		}

///    
///     <summary> * Returns a new chunk provider which generates chunks for this world </summary>
///     
		public override IChunkProvider createChunkGenerator()
		{
			return new ChunkProviderHell(this.worldObj, this.worldObj.Seed);
		}

///    
///     <summary> * Returns 'true' if in the "main surface world", but 'false' if in the Nether or End dimensions. </summary>
///     
		public override bool isSurfaceWorld()
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * Will check if the x, z position specified is alright to be set as the map spawn point </summary>
///     
		public override bool canCoordinateBeSpawn(int p_76566_1_, int p_76566_2_)
		{
			return false;
		}

///    
///     <summary> * Calculates the angle of sun and moon in the sky relative to a specified time (usually worldTime) </summary>
///     
		public override float calculateCelestialAngle(long p_76563_1_, float p_76563_3_)
		{
			return 0.5F;
		}

///    
///     <summary> * True if the player can respawn in this dimension (true = overworld, false = nether). </summary>
///     
		public override bool canRespawnHere()
		{
			return false;
		}

///    
///     <summary> * Returns true if the given X,Z coordinate should show environmental fog. </summary>
///     
		public override bool doesXZShowFog(int p_76568_1_, int p_76568_2_)
		{
			return true;
		}

///    
///     <summary> * Returns the dimension's name, e.g. "The End", "Nether", or "Overworld". </summary>
///     
		public override string DimensionName
		{
			get
			{
				return "Nether";
			}
		}
	}

}