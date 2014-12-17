namespace DotCraftCore.nEnchantment
{

	public class EnchantmentFireAspect : Enchantment
	{
		

		protected internal EnchantmentFireAspect(int p_i1932_1_, int p_i1932_2_) : base(p_i1932_1_, p_i1932_2_, EnumEnchantmentType.weapon)
		{
			this.Name = "fire";
		}

///    
///     <summary> * Returns the minimal value of enchantability needed on the enchantment level passed. </summary>
///     
		public override int getMinEnchantability(int p_77321_1_)
		{
			return 10 + 20 * (p_77321_1_ - 1);
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