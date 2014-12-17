namespace DotCraftCore.nDispenser
{

	public class PositionImpl : IPosition
	{
		protected internal readonly double x;
		protected internal readonly double y;
		protected internal readonly double z;
		

		public PositionImpl(double p_i1368_1_, double p_i1368_3_, double p_i1368_5_)
		{
			this.x = p_i1368_1_;
			this.y = p_i1368_3_;
			this.z = p_i1368_5_;
		}

		public virtual double X
		{
			get
			{
				return this.x;
			}
		}

		public virtual double Y
		{
			get
			{
				return this.y;
			}
		}

		public virtual double Z
		{
			get
			{
				return this.z;
			}
		}
	}

}