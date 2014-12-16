namespace DotCraftCore.Pathfinding
{

	using Entity = DotCraftCore.entity.Entity;
	using Vec3 = DotCraftCore.Util.Vec3;

	public class PathEntity
	{
	/// <summary> The actual points in the path  </summary>
		private readonly PathPoint[] points;

	/// <summary> PathEntity Array Index the Entity is currently targeting  </summary>
		private int currentPathIndex;

	/// <summary> The total length of the path  </summary>
		private int pathLength;
		

		public PathEntity(PathPoint[] p_i2136_1_)
		{
			this.points = p_i2136_1_;
			this.pathLength = p_i2136_1_.Length;
		}

///    
///     <summary> * Directs this path to the next point in its array </summary>
///     
		public virtual void incrementPathIndex()
		{
			++this.currentPathIndex;
		}

///    
///     <summary> * Returns true if this path has reached the end </summary>
///     
		public virtual bool isFinished()
		{
			get
			{
				return this.currentPathIndex >= this.pathLength;
			}
		}

///    
///     <summary> * returns the last PathPoint of the Array </summary>
///     
		public virtual PathPoint FinalPathPoint
		{
			get
			{
				return this.pathLength > 0 ? this.points[this.pathLength - 1] : null;
			}
		}

///    
///     <summary> * return the PathPoint located at the specified PathIndex, usually the current one </summary>
///     
		public virtual PathPoint getPathPointFromIndex(int p_75877_1_)
		{
			return this.points[p_75877_1_];
		}

		public virtual int CurrentPathLength
		{
			get
			{
				return this.pathLength;
			}
			set
			{
				this.pathLength = value;
			}
		}


		public virtual int CurrentPathIndex
		{
			get
			{
				return this.currentPathIndex;
			}
			set
			{
				this.currentPathIndex = value;
			}
		}


///    
///     <summary> * Gets the vector of the PathPoint associated with the given index. </summary>
///     
		public virtual Vec3 getVectorFromIndex(Entity p_75881_1_, int p_75881_2_)
		{
			double var3 = (double)this.points[p_75881_2_].xCoord + (double)((int)(p_75881_1_.width + 1.0F)) * 0.5D;
			double var5 = (double)this.points[p_75881_2_].yCoord;
			double var7 = (double)this.points[p_75881_2_].zCoord + (double)((int)(p_75881_1_.width + 1.0F)) * 0.5D;
			return Vec3.createVectorHelper(var3, var5, var7);
		}

///    
///     <summary> * returns the current PathEntity target node as Vec3D </summary>
///     
		public virtual Vec3 getPosition(Entity p_75878_1_)
		{
			return this.getVectorFromIndex(p_75878_1_, this.currentPathIndex);
		}

///    
///     <summary> * Returns true if the EntityPath are the same. Non instance related equals. </summary>
///     
		public virtual bool isSamePath(PathEntity p_75876_1_)
		{
			if (p_75876_1_ == null)
			{
				return false;
			}
			else if (p_75876_1_.points.Length != this.points.Length)
			{
				return false;
			}
			else
			{
				for (int var2 = 0; var2 < this.points.Length; ++var2)
				{
					if (this.points[var2].xCoord != p_75876_1_.points[var2].xCoord || this.points[var2].yCoord != p_75876_1_.points[var2].yCoord || this.points[var2].zCoord != p_75876_1_.points[var2].zCoord)
					{
						return false;
					}
				}

				return true;
			}
		}

///    
///     <summary> * Returns true if the final PathPoint in the PathEntity is equal to Vec3D coords. </summary>
///     
		public virtual bool isDestinationSame(Vec3 p_75880_1_)
		{
			PathPoint var2 = this.FinalPathPoint;
			return var2 == null ? false : var2.xCoord == (int)p_75880_1_.xCoord && var2.zCoord == (int)p_75880_1_.zCoord;
		}
	}

}