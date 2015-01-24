using Entity = DotCraftCore.nEntity.Entity;

namespace DotCraftCore.nUtil
{
	public class MovingObjectPosition
	{
	/// <summary> What type of ray trace hit was this? 0 = block, 1 = entity  </summary>
		public MovingObjectPosition.MovingObjectType typeOfHit;

	/// <summary> x coordinate of the block ray traced against  </summary>
		public int blockX;

	/// <summary> y coordinate of the block ray traced against  </summary>
		public int blockY;

	/// <summary> z coordinate of the block ray traced against  </summary>
		public int blockZ;

///    
///     <summary> * Which side was hit. If its -1 then it went the full length of the ray trace. Bottom = 0, Top = 1, East = 2, West
///     * = 3, North = 4, South = 5. </summary>
///     
		public int sideHit;

	/// <summary> The vector position of the hit  </summary>
		public Vec3 hitVec;

	/// <summary> The hit entity  </summary>
		public Entity entityHit;
		

		public MovingObjectPosition(int p_i2303_1_, int p_i2303_2_, int p_i2303_3_, int p_i2303_4_, Vec3 p_i2303_5_) : this(p_i2303_1_, p_i2303_2_, p_i2303_3_, p_i2303_4_, p_i2303_5_, true)
		{
		}

		public MovingObjectPosition(int p_i45481_1_, int p_i45481_2_, int p_i45481_3_, int p_i45481_4_, Vec3 p_i45481_5_, bool p_i45481_6_)
		{
			this.typeOfHit = p_i45481_6_ ? MovingObjectPosition.MovingObjectType.BLOCK : MovingObjectPosition.MovingObjectType.MISS;
			this.blockX = p_i45481_1_;
			this.blockY = p_i45481_2_;
			this.blockZ = p_i45481_3_;
			this.sideHit = p_i45481_4_;
			this.hitVec = Vec3.createVectorHelper(p_i45481_5_.xCoord, p_i45481_5_.yCoord, p_i45481_5_.zCoord);
		}

		public MovingObjectPosition(Entity p_i2304_1_) : this(p_i2304_1_, Vec3.createVectorHelper(p_i2304_1_.posX, p_i2304_1_.posY, p_i2304_1_.posZ))
		{
		}

		public MovingObjectPosition(Entity p_i45482_1_, Vec3 p_i45482_2_)
		{
			this.typeOfHit = MovingObjectPosition.MovingObjectType.ENTITY;
			this.entityHit = p_i45482_1_;
			this.hitVec = p_i45482_2_;
		}

		public override string ToString()
		{
			return "HitResult{type=" + this.typeOfHit + ", x=" + this.blockX + ", y=" + this.blockY + ", z=" + this.blockZ + ", f=" + this.sideHit + ", pos=" + this.hitVec + ", entity=" + this.entityHit + '}';
		}

		public enum MovingObjectType
		{
            MISS = 0,
            BLOCK = 1,
            ENTITY = 2
        }
	}
}