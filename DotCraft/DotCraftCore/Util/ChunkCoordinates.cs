namespace DotCraftCore.Util
{

	public class ChunkCoordinates : Comparable
	{
		public int posX;

	/// <summary> the y coordinate  </summary>
		public int posY;

	/// <summary> the z coordinate  </summary>
		public int posZ;
		

		public ChunkCoordinates()
		{
		}

		public ChunkCoordinates(int p_i1354_1_, int p_i1354_2_, int p_i1354_3_)
		{
			this.posX = p_i1354_1_;
			this.posY = p_i1354_2_;
			this.posZ = p_i1354_3_;
		}

		public ChunkCoordinates(ChunkCoordinates p_i1355_1_)
		{
			this.posX = p_i1355_1_.posX;
			this.posY = p_i1355_1_.posY;
			this.posZ = p_i1355_1_.posZ;
		}

		public override bool Equals(object p_equals_1_)
		{
			if(!(p_equals_1_ is ChunkCoordinates))
			{
				return false;
			}
			else
			{
				ChunkCoordinates var2 = (ChunkCoordinates)p_equals_1_;
				return this.posX == var2.posX && this.posY == var2.posY && this.posZ == var2.posZ;
			}
		}

		public override int GetHashCode()
		{
			return this.posX + this.posZ << 8 + this.posY << 16;
		}

		public virtual int compareTo(ChunkCoordinates p_compareTo_1_)
		{
			return this.posY == p_compareTo_1_.posY ? (this.posZ == p_compareTo_1_.posZ ? this.posX - p_compareTo_1_.posX : this.posZ - p_compareTo_1_.posZ) : this.posY - p_compareTo_1_.posY;
		}

		public virtual void set(int p_71571_1_, int p_71571_2_, int p_71571_3_)
		{
			this.posX = p_71571_1_;
			this.posY = p_71571_2_;
			this.posZ = p_71571_3_;
		}

///    
///     <summary> * Returns the squared distance between this coordinates and the coordinates given as argument. </summary>
///     
		public virtual float getDistanceSquared(int p_71569_1_, int p_71569_2_, int p_71569_3_)
		{
			float var4 = (float)(this.posX - p_71569_1_);
			float var5 = (float)(this.posY - p_71569_2_);
			float var6 = (float)(this.posZ - p_71569_3_);
			return var4 * var4 + var5 * var5 + var6 * var6;
		}

///    
///     <summary> * Return the squared distance between this coordinates and the ChunkCoordinates given as argument. </summary>
///     
		public virtual float getDistanceSquaredToChunkCoordinates(ChunkCoordinates p_82371_1_)
		{
			return this.getDistanceSquared(p_82371_1_.posX, p_82371_1_.posY, p_82371_1_.posZ);
		}

		public override string ToString()
		{
			return "Pos{x=" + this.posX + ", y=" + this.posY + ", z=" + this.posZ + '}';
		}

		public virtual int compareTo(object p_compareTo_1_)
		{
			return this.CompareTo((ChunkCoordinates)p_compareTo_1_);
		}
	}

}