namespace DotCraftCore.Enchantment
{

	public class EnchantmentWaterWorker : Enchantment
	{
		

		public EnchantmentWaterWorker(int p_i1939_1_, int p_i1939_2_) : base(p_i1939_1_, p_i1939_2_, EnumEnchantmentType.armor_head)
		{
			this.Name = "waterWorker";
		}

///    
///     <summary> * Returns the minimal value of enchantability needed on the enchantment level passed. </summary>
///     
		public override int getMinEnchantability(int p_77321_1_)
		{
			return 1;
		}

///    
///     <summary> * Returns the maximum value of enchantability nedded on the enchantment level passed. </summary>
///     
		public override int getMaxEnchantability(int p_77317_1_)
		{
			return this.getMinEnchantability(p_77317_1_) + 40;
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