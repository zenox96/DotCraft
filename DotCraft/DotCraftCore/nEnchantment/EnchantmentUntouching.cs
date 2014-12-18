namespace DotCraftCore.nEnchantment
{

	using Items = DotCraftCore.nInit.Items;
	using ItemStack = DotCraftCore.nItem.ItemStack;

	public class EnchantmentUntouching : Enchantment
	{
		

		protected internal EnchantmentUntouching(int p_i1938_1_, int p_i1938_2_) : base(p_i1938_1_, p_i1938_2_, EnumEnchantmentType.digger)
		{
			this.Name = "untouching";
		}

///    
///     <summary> * Returns the minimal value of enchantability needed on the enchantment level passed. </summary>
///     
		public override int getMinEnchantability(int p_77321_1_)
		{
			return 15;
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
				return 1;
			}
		}

///    
///     <summary> * Determines if the enchantment passed can be applyied together with this enchantment. </summary>
///     
		public override bool canApplyTogether(Enchantment p_77326_1_)
		{
			return base.canApplyTogether(p_77326_1_) && p_77326_1_.effectId != fortune.effectId;
		}

		public override bool canApply(ItemStack p_92089_1_)
		{
			return p_92089_1_.Item == Items.shears ? true : base.canApply(p_92089_1_);
		}
	}

}