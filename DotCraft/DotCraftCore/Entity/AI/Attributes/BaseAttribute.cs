namespace DotCraftCore.Entity.AI.Attributes
{

	public abstract class BaseAttribute : IAttribute
	{
		private readonly string unlocalizedName;
		private readonly double defaultValue;
		private bool shouldWatch;
		private const string __OBFID = "CL_00001565";

		protected internal BaseAttribute(string p_i1607_1_, double p_i1607_2_)
		{
			this.unlocalizedName = p_i1607_1_;
			this.defaultValue = p_i1607_2_;

			if (p_i1607_1_ == null)
			{
				throw new System.ArgumentException("Name cannot be null!");
			}
		}

		public virtual string AttributeUnlocalizedName
		{
			get
			{
				return this.unlocalizedName;
			}
		}

		public virtual double DefaultValue
		{
			get
			{
				return this.defaultValue;
			}
		}

		public virtual bool ShouldWatch
		{
			get
			{
				return this.shouldWatch;
			}
			set
			{
				this.shouldWatch = value;
				return this;
			}
		}


		public override int GetHashCode()
		{
			return this.unlocalizedName.GetHashCode();
		}
	}

}