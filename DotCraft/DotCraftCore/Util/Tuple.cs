namespace DotCraftCore.Util
{

	public class Tuple
	{
	/// <summary> First Object in the Tuple  </summary>
		private object first;

	/// <summary> Second Object in the Tuple  </summary>
		private object second;
		

		public Tuple(object p_i1555_1_, object p_i1555_2_)
		{
			this.first = p_i1555_1_;
			this.second = p_i1555_2_;
		}

///    
///     <summary> * Get the first Object in the Tuple </summary>
///     
		public virtual object First
		{
			get
			{
				return this.first;
			}
		}

///    
///     <summary> * Get the second Object in the Tuple </summary>
///     
		public virtual object Second
		{
			get
			{
				return this.second;
			}
		}
	}

}