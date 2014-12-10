using System;

namespace DotCraftCore.World.Gen
{

	using Block = DotCraftCore.block.Block;
	using World = DotCraftCore.World.World;
	using IChunkProvider = DotCraftCore.World.Chunk.IChunkProvider;

	public class MapGenBase
	{
	/// <summary> The number of Chunks to gen-check in any given direction.  </summary>
		protected internal int range = 8;

	/// <summary> The RNG used by the MapGen classes.  </summary>
		protected internal Random rand = new Random();

	/// <summary> This world object.  </summary>
		protected internal World worldObj;
		private const string __OBFID = "CL_00000394";

		public virtual void func_151539_a(IChunkProvider p_151539_1_, World p_151539_2_, int p_151539_3_, int p_151539_4_, Block[] p_151539_5_)
		{
			int var6 = this.range;
			this.worldObj = p_151539_2_;
			this.rand.Seed = p_151539_2_.Seed;
			long var7 = this.rand.nextLong();
			long var9 = this.rand.nextLong();

			for (int var11 = p_151539_3_ - var6; var11 <= p_151539_3_ + var6; ++var11)
			{
				for (int var12 = p_151539_4_ - var6; var12 <= p_151539_4_ + var6; ++var12)
				{
					long var13 = (long)var11 * var7;
					long var15 = (long)var12 * var9;
					this.rand.Seed = var13 ^ var15 ^ p_151539_2_.Seed;
					this.func_151538_a(p_151539_2_, var11, var12, p_151539_3_, p_151539_4_, p_151539_5_);
				}
			}
		}

		protected internal virtual void func_151538_a(World p_151538_1_, int p_151538_2_, int p_151538_3_, int p_151538_4_, int p_151538_5_, Block[] p_151538_6_)
		{
		}
	}

}