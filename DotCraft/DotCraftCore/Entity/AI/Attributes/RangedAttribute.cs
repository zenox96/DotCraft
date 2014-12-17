namespace DotCraftCore.nEntity.nAI.nAttributes
{

	public class RangedAttribute : BaseAttribute
	{
		private readonly double minimumValue;
		private readonly double maximumValue;
		private string description;
		

		public RangedAttribute(string p_i1609_1_, double p_i1609_2_, double p_i1609_4_, double p_i1609_6_) : base(p_i1609_1_, p_i1609_2_)
		{
			this.minimumValue = p_i1609_4_;
			this.maximumValue = p_i1609_6_;

			if (p_i1609_4_ > p_i1609_6_)
			{
				throw new System.ArgumentException("Minimum value cannot be bigger than maximum value!");
			}
			else if (p_i1609_2_ < p_i1609_4_)
			{
				throw new System.ArgumentException("Default value cannot be lower than minimum value!");
			}
			else if (p_i1609_2_ > p_i1609_6_)
			{
				throw new System.ArgumentException("Default value cannot be bigger than maximum value!");
			}
		}

		public virtual RangedAttribute Description
		{
			set
			{
				this.description = value;
				return this;
			}
			get
			{
				return this.description;
			}
		}


		public virtual double clampValue(double p_111109_1_)
		{
			if (p_111109_1_ < this.minimumValue)
			{
				p_111109_1_ = this.minimumValue;
			}

			if (p_111109_1_ > this.maximumValue)
			{
				p_111109_1_ = this.maximumValue;
			}

			return p_111109_1_;
		}
	}

}