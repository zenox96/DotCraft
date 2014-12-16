namespace DotCraftCore.Pathfinding
{

	using MathHelper = DotCraftCore.Util.MathHelper;

	public class PathPoint
	{
	/// <summary> The x coordinate of this point  </summary>
		public readonly int xCoord;

	/// <summary> The y coordinate of this point  </summary>
		public readonly int yCoord;

	/// <summary> The z coordinate of this point  </summary>
		public readonly int zCoord;

	/// <summary> A hash of the coordinates used to identify this point  </summary>
		private readonly int hash;

	/// <summary> The index of this point in its assigned path  </summary>
		internal int index = -1;

	/// <summary> The distance along the path to this point  </summary>
		internal float totalPathDistance;

	/// <summary> The linear distance to the next point  </summary>
		internal float distanceToNext;

	/// <summary> The distance to the target  </summary>
		internal float distanceToTarget;

	/// <summary> The point preceding this in its assigned path  </summary>
		internal PathPoint previous;

	/// <summary> Indicates this is the origin  </summary>
		public bool isFirst;
		

		public PathPoint(int p_i2135_1_, int p_i2135_2_, int p_i2135_3_)
		{
			this.xCoord = p_i2135_1_;
			this.yCoord = p_i2135_2_;
			this.zCoord = p_i2135_3_;
			this.hash = makeHash(p_i2135_1_, p_i2135_2_, p_i2135_3_);
		}

		public static int makeHash(int p_75830_0_, int p_75830_1_, int p_75830_2_)
		{
			return p_75830_1_ & 255 | (p_75830_0_ & 32767) << 8 | (p_75830_2_ & 32767) << 24 | (p_75830_0_ < 0 ? int.MinValue : 0) | (p_75830_2_ < 0 ? 32768 : 0);
		}

///    
///     <summary> * Returns the linear distance to another path point </summary>
///     
		public virtual float distanceTo(PathPoint p_75829_1_)
		{
			float var2 = (float)(p_75829_1_.xCoord - this.xCoord);
			float var3 = (float)(p_75829_1_.yCoord - this.yCoord);
			float var4 = (float)(p_75829_1_.zCoord - this.zCoord);
			return MathHelper.sqrt_float(var2 * var2 + var3 * var3 + var4 * var4);
		}

///    
///     <summary> * Returns the squared distance to another path point </summary>
///     
		public virtual float distanceToSquared(PathPoint p_75832_1_)
		{
			float var2 = (float)(p_75832_1_.xCoord - this.xCoord);
			float var3 = (float)(p_75832_1_.yCoord - this.yCoord);
			float var4 = (float)(p_75832_1_.zCoord - this.zCoord);
			return var2 * var2 + var3 * var3 + var4 * var4;
		}

		public override bool Equals(object p_equals_1_)
		{
			if (!(p_equals_1_ is PathPoint))
			{
				return false;
			}
			else
			{
				PathPoint var2 = (PathPoint)p_equals_1_;
				return this.hash == var2.hash && this.xCoord == var2.xCoord && this.yCoord == var2.yCoord && this.zCoord == var2.zCoord;
			}
		}

		public override int GetHashCode()
		{
			return this.hash;
		}

///    
///     <summary> * Returns true if this point has already been assigned to a path </summary>
///     
		public virtual bool isAssigned()
		{
			get
			{
				return this.index >= 0;
			}
		}

		public override string ToString()
		{
			return this.xCoord + ", " + this.yCoord + ", " + this.zCoord;
		}
	}

}