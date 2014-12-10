namespace DotCraftCore.Enchantment
{

	public class EnchantmentKnockback : Enchantment
	{
		private const string __OBFID = "CL_00000118";

		protected internal EnchantmentKnockback(int p_i1933_1_, int p_i1933_2_) : base(p_i1933_1_, p_i1933_2_, EnumEnchantmentType.weapon)
		{
			this.Name = "knockback";
		}

///    
///     <summary> * Returns the minimal value of enchantability needed on the enchantment level passed. </summary>
///     
		public override int getMinEnchantability(int p_77321_1_)
		{
			return 5 + 20 * (p_77321_1_ - 1);
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
				return 2;
			}
		}
	}

}