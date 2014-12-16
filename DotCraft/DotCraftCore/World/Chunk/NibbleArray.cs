namespace DotCraftCore.World.Chunk
{

	public class NibbleArray
	{
///    
///     <summary> * Byte array of data stored in this holder. Possibly a light map or some chunk data. Data is accessed in 4-bit
///     * pieces. </summary>
///     
		public readonly sbyte[] data;

///    
///     <summary> * Log base 2 of the chunk height (128); applied as a shift on Z coordinate </summary>
///     
		private readonly int depthBits;

///    
///     <summary> * Log base 2 of the chunk height (128) * width (16); applied as a shift on X coordinate </summary>
///     
		private readonly int depthBitsPlusFour;
		

		public NibbleArray(int p_i1992_1_, int p_i1992_2_)
		{
			this.data = new sbyte[p_i1992_1_ >> 1];
			this.depthBits = p_i1992_2_;
			this.depthBitsPlusFour = p_i1992_2_ + 4;
		}

		public NibbleArray(sbyte[] p_i1993_1_, int p_i1993_2_)
		{
			this.data = p_i1993_1_;
			this.depthBits = p_i1993_2_;
			this.depthBitsPlusFour = p_i1993_2_ + 4;
		}

///    
///     <summary> * Returns the nibble of data corresponding to the passed in x, y, z. y is at most 6 bits, z is at most 4. </summary>
///     
		public virtual int get(int p_76582_1_, int p_76582_2_, int p_76582_3_)
		{
			int var4 = p_76582_2_ << this.depthBitsPlusFour | p_76582_3_ << this.depthBits | p_76582_1_;
			int var5 = var4 >> 1;
			int var6 = var4 & 1;
			return var6 == 0 ? this.data[var5] & 15 : this.data[var5] >> 4 & 15;
		}

///    
///     <summary> * Arguments are x, y, z, val. Sets the nibble of data at x << 11 | z << 7 | y to val. </summary>
///     
		public virtual void set(int p_76581_1_, int p_76581_2_, int p_76581_3_, int p_76581_4_)
		{
			int var5 = p_76581_2_ << this.depthBitsPlusFour | p_76581_3_ << this.depthBits | p_76581_1_;
			int var6 = var5 >> 1;
			int var7 = var5 & 1;

			if (var7 == 0)
			{
				this.data[var6] = (sbyte)(this.data[var6] & 240 | p_76581_4_ & 15);
			}
			else
			{
				this.data[var6] = (sbyte)(this.data[var6] & 15 | (p_76581_4_ & 15) << 4);
			}
		}
	}

}