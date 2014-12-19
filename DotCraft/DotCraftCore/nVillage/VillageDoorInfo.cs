namespace DotCraftCore.nVillage
{

	public class VillageDoorInfo
	{
		public readonly int posX;
		public readonly int posY;
		public readonly int posZ;
		public readonly int insideDirectionX;
		public readonly int insideDirectionZ;
		public int lastActivityTimestamp;
		public bool isDetachedFromVillageFlag;
		private int doorOpeningRestrictionCounter;
		

		public VillageDoorInfo(int p_i1673_1_, int p_i1673_2_, int p_i1673_3_, int p_i1673_4_, int p_i1673_5_, int p_i1673_6_)
		{
			this.posX = p_i1673_1_;
			this.posY = p_i1673_2_;
			this.posZ = p_i1673_3_;
			this.insideDirectionX = p_i1673_4_;
			this.insideDirectionZ = p_i1673_5_;
			this.lastActivityTimestamp = p_i1673_6_;
		}

///    
///     <summary> * Returns the squared distance between this door and the given coordinate. </summary>
///     
		public virtual int getDistanceSquared(int p_75474_1_, int p_75474_2_, int p_75474_3_)
		{
			int var4 = p_75474_1_ - this.posX;
			int var5 = p_75474_2_ - this.posY;
			int var6 = p_75474_3_ - this.posZ;
			return var4 * var4 + var5 * var5 + var6 * var6;
		}

///    
///     <summary> * Get the square of the distance from a location 2 blocks away from the door considered 'inside' and the given
///     * arguments </summary>
///     
		public virtual int getInsideDistanceSquare(int p_75469_1_, int p_75469_2_, int p_75469_3_)
		{
			int var4 = p_75469_1_ - this.posX - this.insideDirectionX;
			int var5 = p_75469_2_ - this.posY;
			int var6 = p_75469_3_ - this.posZ - this.insideDirectionZ;
			return var4 * var4 + var5 * var5 + var6 * var6;
		}

		public virtual int InsidePosX
		{
			get
			{
				return this.posX + this.insideDirectionX;
			}
		}

		public virtual int InsidePosY
		{
			get
			{
				return this.posY;
			}
		}

		public virtual int InsidePosZ
		{
			get
			{
				return this.posZ + this.insideDirectionZ;
			}
		}

		public virtual bool isInside(int p_75467_1_, int p_75467_2_)
		{
			int var3 = p_75467_1_ - this.posX;
			int var4 = p_75467_2_ - this.posZ;
			return var3 * this.insideDirectionX + var4 * this.insideDirectionZ >= 0;
		}

		public virtual void resetDoorOpeningRestrictionCounter()
		{
			this.doorOpeningRestrictionCounter = 0;
		}

		public virtual void incrementDoorOpeningRestrictionCounter()
		{
			++this.doorOpeningRestrictionCounter;
		}

		public virtual int DoorOpeningRestrictionCounter
		{
			get
			{
				return this.doorOpeningRestrictionCounter;
			}
		}
	}

}