namespace DotCraftCore.Potion
{

	public class PotionHealth : Potion
	{
		private const string __OBFID = "CL_00001527";

		public PotionHealth(int p_i1572_1_, bool p_i1572_2_, int p_i1572_3_) : base(p_i1572_1_, p_i1572_2_, p_i1572_3_)
		{
		}

///    
///     <summary> * Returns true if the potion has an instant effect instead of a continuous one (eg Harming) </summary>
///     
		public override bool isInstant()
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * checks if Potion effect is ready to be applied this tick. </summary>
///     
		public override bool isReady(int p_76397_1_, int p_76397_2_)
		{
			return p_76397_1_ >= 1;
		}
	}

}