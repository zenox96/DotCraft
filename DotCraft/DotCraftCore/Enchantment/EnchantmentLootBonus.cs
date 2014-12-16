namespace DotCraftCore.Enchantment
{

	public class EnchantmentLootBonus : Enchantment
	{
		

		protected internal EnchantmentLootBonus(int p_i1934_1_, int p_i1934_2_, EnumEnchantmentType p_i1934_3_) : base(p_i1934_1_, p_i1934_2_, p_i1934_3_)
		{

			if (p_i1934_3_ == EnumEnchantmentType.digger)
			{
				this.Name = "lootBonusDigger";
			}
			else if (p_i1934_3_ == EnumEnchantmentType.fishing_rod)
			{
				this.Name = "lootBonusFishing";
			}
			else
			{
				this.Name = "lootBonus";
			}
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

///    
///     <summary> * Determines if the enchantment passed can be applyied together with this enchantment. </summary>
///     
		public override bool canApplyTogether(Enchantment p_77326_1_)
		{
			return base.canApplyTogether(p_77326_1_) && p_77326_1_.effectId != silkTouch.effectId;
		}
	}

}