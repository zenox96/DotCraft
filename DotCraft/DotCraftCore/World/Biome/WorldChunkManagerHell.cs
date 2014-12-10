using System;
using System.Collections;

namespace DotCraftCore.World.Biome
{

	using ChunkPosition = DotCraftCore.World.ChunkPosition;

	public class WorldChunkManagerHell : WorldChunkManager
	{
	/// <summary> The biome generator object.  </summary>
		private BiomeGenBase biomeGenerator;

	/// <summary> The rainfall in the world  </summary>
		private float rainfall;
		private const string __OBFID = "CL_00000169";

		public WorldChunkManagerHell(BiomeGenBase p_i45374_1_, float p_i45374_2_)
		{
			this.biomeGenerator = p_i45374_1_;
			this.rainfall = p_i45374_2_;
		}

///    
///     <summary> * Returns the BiomeGenBase related to the x, z position on the world. </summary>
///     
		public override BiomeGenBase getBiomeGenAt(int p_76935_1_, int p_76935_2_)
		{
			return this.biomeGenerator;
		}

///    
///     <summary> * Returns an array of biomes for the location input. </summary>
///     
		public override BiomeGenBase[] getBiomesForGeneration(BiomeGenBase[] p_76937_1_, int p_76937_2_, int p_76937_3_, int p_76937_4_, int p_76937_5_)
		{
			if (p_76937_1_ == null || p_76937_1_.Length < p_76937_4_ * p_76937_5_)
			{
				p_76937_1_ = new BiomeGenBase[p_76937_4_ * p_76937_5_];
			}

			Arrays.fill(p_76937_1_, 0, p_76937_4_ * p_76937_5_, this.biomeGenerator);
			return p_76937_1_;
		}

///    
///     <summary> * Returns a list of rainfall values for the specified blocks. Args: listToReuse, x, z, width, length. </summary>
///     
		public override float[] getRainfall(float[] p_76936_1_, int p_76936_2_, int p_76936_3_, int p_76936_4_, int p_76936_5_)
		{
			if (p_76936_1_ == null || p_76936_1_.Length < p_76936_4_ * p_76936_5_)
			{
				p_76936_1_ = new float[p_76936_4_ * p_76936_5_];
			}

			Arrays.fill(p_76936_1_, 0, p_76936_4_ * p_76936_5_, this.rainfall);
			return p_76936_1_;
		}

///    
///     <summary> * Returns biomes to use for the blocks and loads the other data like temperature and humidity onto the
///     * WorldChunkManager Args: oldBiomeList, x, z, width, depth </summary>
///     
		public override BiomeGenBase[] loadBlockGeneratorData(BiomeGenBase[] p_76933_1_, int p_76933_2_, int p_76933_3_, int p_76933_4_, int p_76933_5_)
		{
			if (p_76933_1_ == null || p_76933_1_.Length < p_76933_4_ * p_76933_5_)
			{
				p_76933_1_ = new BiomeGenBase[p_76933_4_ * p_76933_5_];
			}

			Arrays.fill(p_76933_1_, 0, p_76933_4_ * p_76933_5_, this.biomeGenerator);
			return p_76933_1_;
		}

///    
///     <summary> * Return a list of biomes for the specified blocks. Args: listToReuse, x, y, width, length, cacheFlag (if false,
///     * don't check biomeCache to avoid infinite loop in BiomeCacheBlock) </summary>
///     
		public override BiomeGenBase[] getBiomeGenAt(BiomeGenBase[] p_76931_1_, int p_76931_2_, int p_76931_3_, int p_76931_4_, int p_76931_5_, bool p_76931_6_)
		{
			return this.loadBlockGeneratorData(p_76931_1_, p_76931_2_, p_76931_3_, p_76931_4_, p_76931_5_);
		}

		public override ChunkPosition func_150795_a(int p_150795_1_, int p_150795_2_, int p_150795_3_, IList p_150795_4_, Random p_150795_5_)
		{
			return p_150795_4_.Contains(this.biomeGenerator) ? new ChunkPosition(p_150795_1_ - p_150795_3_ + p_150795_5_.Next(p_150795_3_ * 2 + 1), 0, p_150795_2_ - p_150795_3_ + p_150795_5_.Next(p_150795_3_ * 2 + 1)) : null;
		}

///    
///     <summary> * checks given Chunk's Biomes against List of allowed ones </summary>
///     
		public override bool areBiomesViable(int p_76940_1_, int p_76940_2_, int p_76940_3_, IList p_76940_4_)
		{
			return p_76940_4_.Contains(this.biomeGenerator);
		}
	}

}