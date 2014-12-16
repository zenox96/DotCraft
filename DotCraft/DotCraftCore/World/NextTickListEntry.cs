namespace DotCraftCore.World
{

	using Block = DotCraftCore.block.Block;

	public class NextTickListEntry : Comparable
	{
	/// <summary> The id number for the next tick entry  </summary>
		private static long nextTickEntryID;
		private readonly Block field_151352_g;

	/// <summary> X position this tick is occuring at  </summary>
		public int xCoord;

	/// <summary> Y position this tick is occuring at  </summary>
		public int yCoord;

	/// <summary> Z position this tick is occuring at  </summary>
		public int zCoord;

	/// <summary> Time this tick is scheduled to occur at  </summary>
		public long scheduledTime;
		public int priority;

	/// <summary> The id of the tick entry  </summary>
		private long tickEntryID;
		

		public NextTickListEntry(int p_i45370_1_, int p_i45370_2_, int p_i45370_3_, Block p_i45370_4_)
		{
			this.tickEntryID = (long)(nextTickEntryID++);
			this.xCoord = p_i45370_1_;
			this.yCoord = p_i45370_2_;
			this.zCoord = p_i45370_3_;
			this.field_151352_g = p_i45370_4_;
		}

		public override bool Equals(object p_equals_1_)
		{
			if (!(p_equals_1_ is NextTickListEntry))
			{
				return false;
			}
			else
			{
				NextTickListEntry var2 = (NextTickListEntry)p_equals_1_;
				return this.xCoord == var2.xCoord && this.yCoord == var2.yCoord && this.zCoord == var2.zCoord && Block.isEqualTo(this.field_151352_g, var2.field_151352_g);
			}
		}

		public override int GetHashCode()
		{
			return (this.xCoord * 1024 * 1024 + this.zCoord * 1024 + this.yCoord) * 256;
		}

///    
///     <summary> * Sets the scheduled time for this tick entry </summary>
///     
		public virtual NextTickListEntry ScheduledTime
		{
			set
			{
				this.scheduledTime = value;
				return this;
			}
		}

		public virtual int Priority
		{
			set
			{
				this.priority = value;
			}
		}

		public virtual int compareTo(NextTickListEntry p_compareTo_1_)
		{
			return this.scheduledTime < p_compareTo_1_.scheduledTime ? -1 : (this.scheduledTime > p_compareTo_1_.scheduledTime ? 1 : (this.priority != p_compareTo_1_.priority ? this.priority - p_compareTo_1_.priority : (this.tickEntryID < p_compareTo_1_.tickEntryID ? -1 : (this.tickEntryID > p_compareTo_1_.tickEntryID ? 1 : 0))));
		}

		public override string ToString()
		{
			return Block.getIdFromBlock(this.field_151352_g) + ": (" + this.xCoord + ", " + this.yCoord + ", " + this.zCoord + "), " + this.scheduledTime + ", " + this.priority + ", " + this.tickEntryID;
		}

		public virtual Block func_151351_a()
		{
			return this.field_151352_g;
		}

		public virtual int compareTo(object p_compareTo_1_)
		{
			return this.CompareTo((NextTickListEntry)p_compareTo_1_);
		}
	}

}