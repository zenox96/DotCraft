namespace DotCraftCore.Enchantment
{

	public class EnchantmentArrowFire : Enchantment
	{
		private const string __OBFID = "CL_00000099";

		public EnchantmentArrowFire(int p_i1920_1_, int p_i1920_2_) : base(p_i1920_1_, p_i1920_2_, EnumEnchantmentType.bow)
		{
			this.Name = "arrowFire";
		}

///    
///     <summary> * Returns the minimal value of enchantability needed on the enchantment level passed. </summary>
///     
		public override int getMinEnchantability(int p_77321_1_)
		{
			return 20;
		}

///    
///     <summary> * Returns the maximum value of enchantability nedded on the enchantment level passed. </summary>
///     
		public override int getMaxEnchantability(int p_77317_1_)
		{
			return 50;
		}

///    
///     <summary> * Returns the maximum level that the enchantment can have. </summary>
///     
		public override int MaxLevel
		{
			get
			{
				return 1;
			}
		}
	}

}