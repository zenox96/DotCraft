namespace DotCraftCore.Enchantment
{

	public class EnchantmentFishingSpeed : Enchantment
	{
		private const string __OBFID = "CL_00000117";

		protected internal EnchantmentFishingSpeed(int p_i45361_1_, int p_i45361_2_, EnumEnchantmentType p_i45361_3_) : base(p_i45361_1_, p_i45361_2_, p_i45361_3_)
		{
			this.Name = "fishingSpeed";
		}

///    
///     <summary> * Returns the minimal value of enchantability needed on the enchantment level passed. </summary>
///     
		public override int getMinEnchantability(int p_77321_1_)
		{
			return 15 + (p_77321_1_ - 1) * 9;
		}

///    
///     <summary> * Returns the maximum value of enchantability nedded on the enchantment level passed. </summary>
///     
		public override int getMaxEnchantability(int p_77317_1_)
		{
			return base.getMinEnchantability(p_77317_1_) + 50;
		}

///    
///     <summary> * Returns the maximum level that the enchantment can have. </summary>
///     
		public override int MaxLevel
		{
			get
			{
				return 3;
			}
		}
	}

}