using System;

namespace DotCraftCore.World
{

	using ChunkCoordinates = DotCraftCore.Util.ChunkCoordinates;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using Vec3 = DotCraftCore.Util.Vec3;
	using BiomeGenBase = DotCraftCore.World.Biome.BiomeGenBase;
	using WorldChunkManagerHell = DotCraftCore.World.Biome.WorldChunkManagerHell;
	using IChunkProvider = DotCraftCore.World.Chunk.IChunkProvider;
	using ChunkProviderEnd = DotCraftCore.World.Gen.ChunkProviderEnd;

	public class WorldProviderEnd : WorldProvider
	{
		private const string __OBFID = "CL_00000389";

///    
///     <summary> * creates a new world chunk manager for WorldProvider </summary>
///     
		public override void registerWorldChunkManager()
		{
			this.worldChunkMgr = new WorldChunkManagerHell(BiomeGenBase.sky, 0.0F);
			this.dimensionId = 1;
			this.hasNoSky = true;
		}

///    
///     <summary> * Returns a new chunk provider which generates chunks for this world </summary>
///     
		public override IChunkProvider createChunkGenerator()
		{
			return new ChunkProviderEnd(this.worldObj, this.worldObj.Seed);
		}

///    
///     <summary> * Calculates the angle of sun and moon in the sky relative to a specified time (usually worldTime) </summary>
///     
		public override float calculateCelestialAngle(long p_76563_1_, float p_76563_3_)
		{
			return 0.0F;
		}

///    
///     <summary> * Returns array with sunrise/sunset colors </summary>
///     
		public override float[] calcSunriseSunsetColors(float p_76560_1_, float p_76560_2_)
		{
			return null;
		}

///    
///     <summary> * Return Vec3D with biome specific fog color </summary>
///     
		public override Vec3 getFogColor(float p_76562_1_, float p_76562_2_)
		{
			int var3 = 10518688;
			float var4 = MathHelper.cos(p_76562_1_ * (float)Math.PI * 2.0F) * 2.0F + 0.5F;

			if (var4 < 0.0F)
			{
				var4 = 0.0F;
			}

			if (var4 > 1.0F)
			{
				var4 = 1.0F;
			}

			float var5 = (float)(var3 >> 16 & 255) / 255.0F;
			float var6 = (float)(var3 >> 8 & 255) / 255.0F;
			float var7 = (float)(var3 & 255) / 255.0F;
			var5 *= var4 * 0.0F + 0.15F;
			var6 *= var4 * 0.0F + 0.15F;
			var7 *= var4 * 0.0F + 0.15F;
			return Vec3.createVectorHelper((double)var5, (double)var6, (double)var7);
		}

		public override bool isSkyColored()
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * True if the player can respawn in this dimension (true = overworld, false = nether). </summary>
///     
		public override bool canRespawnHere()
		{
			return false;
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
///     <summary> * the y level at which clouds are rendered. </summary>
///     
		public override float CloudHeight
		{
			get
			{
				return 8.0F;
			}
		}

///    
///     <summary> * Will check if the x, z position specified is alright to be set as the map spawn point </summary>
///     
		public override bool canCoordinateBeSpawn(int p_76566_1_, int p_76566_2_)
		{
			return this.worldObj.getTopBlock(p_76566_1_, p_76566_2_).Material.blocksMovement();
		}

///    
///     <summary> * Gets the hard-coded portal location to use when entering this dimension. </summary>
///     
		public override ChunkCoordinates EntrancePortalLocation
		{
			get
			{
				return new ChunkCoordinates(100, 50, 0);
			}
		}

		public override int AverageGroundLevel
		{
			get
			{
				return 50;
			}
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
				return "The End";
			}
		}
	}

}