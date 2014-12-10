namespace DotCraftCore.World.Chunk.Storage
{

	public class NibbleArrayReader
	{
		public readonly sbyte[] data;
		private readonly int depthBits;
		private readonly int depthBitsPlusFour;
		private const string __OBFID = "CL_00000376";

		public NibbleArrayReader(sbyte[] p_i1998_1_, int p_i1998_2_)
		{
			this.data = p_i1998_1_;
			this.depthBits = p_i1998_2_;
			this.depthBitsPlusFour = p_i1998_2_ + 4;
		}

		public virtual int get(int p_76686_1_, int p_76686_2_, int p_76686_3_)
		{
			int var4 = p_76686_1_ << this.depthBitsPlusFour | p_76686_3_ << this.depthBits | p_76686_2_;
			int var5 = var4 >> 1;
			int var6 = var4 & 1;
			return var6 == 0 ? this.data[var5] & 15 : this.data[var5] >> 4 & 15;
		}
	}

}