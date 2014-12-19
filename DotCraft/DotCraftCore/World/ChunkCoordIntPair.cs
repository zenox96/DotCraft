namespace DotCraftCore.nWorld
{

	public class ChunkCoordIntPair
	{
	/// <summary> The X position of this Chunk Coordinate Pair  </summary>
		public readonly int chunkXPos;

	/// <summary> The Z position of this Chunk Coordinate Pair  </summary>
		public readonly int chunkZPos;
		

		public ChunkCoordIntPair(int p_i1947_1_, int p_i1947_2_)
		{
			this.chunkXPos = p_i1947_1_;
			this.chunkZPos = p_i1947_2_;
		}

///    
///     <summary> * converts a chunk coordinate pair to an integer (suitable for hashing) </summary>
///     
		public static long chunkXZ2Int(int p_77272_0_, int p_77272_1_)
		{
			return (long)p_77272_0_ & 4294967295L | ((long)p_77272_1_ & 4294967295L) << 32;
		}

		public override int GetHashCode()
		{
			int var1 = 1664525 * this.chunkXPos + 1013904223;
			int var2 = 1664525 * (this.chunkZPos ^ -559038737) + 1013904223;
			return var1 ^ var2;
		}

		public override bool Equals(object p_equals_1_)
		{
			if (this == p_equals_1_)
			{
				return true;
			}
			else if (!(p_equals_1_ is ChunkCoordIntPair))
			{
				return false;
			}
			else
			{
				ChunkCoordIntPair var2 = (ChunkCoordIntPair)p_equals_1_;
				return this.chunkXPos == var2.chunkXPos && this.chunkZPos == var2.chunkZPos;
			}
		}

		public virtual int CenterXPos
		{
			get
			{
				return (this.chunkXPos << 4) + 8;
			}
		}

		public virtual int CenterZPosition
		{
			get
			{
				return (this.chunkZPos << 4) + 8;
			}
		}

		public virtual ChunkPosition func_151349_a(int p_151349_1_)
		{
			return new ChunkPosition(this.CenterXPos, p_151349_1_, this.CenterZPosition);
		}

		public override string ToString()
		{
			return "[" + this.chunkXPos + ", " + this.chunkZPos + "]";
		}
	}

}