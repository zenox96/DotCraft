namespace DotCraftCore.nEnchantment
{

	using WeightedRandom = DotCraftCore.nUtil.WeightedRandom;

	public class EnchantmentData : WeightedRandom.Item
	{
	/// <summary> Enchantment object associated with this EnchantmentData  </summary>
		public readonly Enchantment enchantmentobj;

	/// <summary> Enchantment level associated with this EnchantmentData  </summary>
		public readonly int enchantmentLevel;
		

		public EnchantmentData(Enchantment p_i1930_1_, int p_i1930_2_) : base(p_i1930_1_.getWeight())
		{
			this.enchantmentobj = p_i1930_1_;
			this.enchantmentLevel = p_i1930_2_;
		}

		public EnchantmentData(int p_i1931_1_, int p_i1931_2_) : this(Enchantment.enchantmentsList[p_i1931_1_], p_i1931_2_)
		{
		}
	}

}