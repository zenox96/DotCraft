namespace DotCraftCore.Enchantment
{

	public class EnchantmentArrowKnockback : Enchantment
	{
		private const string __OBFID = "CL_00000101";

		public EnchantmentArrowKnockback(int p_i1922_1_, int p_i1922_2_) : base(p_i1922_1_, p_i1922_2_, EnumEnchantmentType.bow)
		{
			this.Name = "arrowKnockback";
		}

///    
///     <summary> * Returns the minimal value of enchantability needed on the enchantment level passed. </summary>
///     
		public override int getMinEnchantability(int p_77321_1_)
		{
			return 12 + (p_77321_1_ - 1) * 20;
		}

///    
///     <summary> * Returns the maximum value of enchantability nedded on the enchantment level passed. </summary>
///     
		public override int getMaxEnchantability(int p_77317_1_)
		{
			return this.getMinEnchantability(p_77317_1_) + 25;
		}

///    
///     <summary> * Returns the maximum level that the enchantment can have. </summary>
///     
		public override int MaxLevel
		{
			get
			{
				return 2;
			}
		}
	}

}