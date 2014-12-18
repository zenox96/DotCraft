namespace DotCraftCore.nEnchantment
{

	public class EnchantmentArrowDamage : Enchantment
	{
		

		public EnchantmentArrowDamage(int p_i1919_1_, int p_i1919_2_) : base(p_i1919_1_, p_i1919_2_, EnumEnchantmentType.bow)
		{
			this.Name = "arrowDamage";
		}

///    
///     <summary> * Returns the minimal value of enchantability needed on the enchantment level passed. </summary>
///     
		public override int getMinEnchantability(int p_77321_1_)
		{
			return 1 + (p_77321_1_ - 1) * 10;
		}

///    
///     <summary> * Returns the maximum value of enchantability nedded on the enchantment level passed. </summary>
///     
		public override int getMaxEnchantability(int p_77317_1_)
		{
			return this.getMinEnchantability(p_77317_1_) + 15;
		}

///    
///     <summary> * Returns the maximum level that the enchantment can have. </summary>
///     
		public override int MaxLevel
		{
			get
			{
				return 5;
			}
		}
	}

}