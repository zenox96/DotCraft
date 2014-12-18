namespace DotCraftCore.nEnchantment
{

	using Items = DotCraftCore.nInit.Items;
	using ItemStack = DotCraftCore.nItem.ItemStack;

	public class EnchantmentDigging : Enchantment
	{
		

		protected internal EnchantmentDigging(int p_i1925_1_, int p_i1925_2_) : base(p_i1925_1_, p_i1925_2_, EnumEnchantmentType.digger)
		{
			this.Name = "digging";
		}

///    
///     <summary> * Returns the minimal value of enchantability needed on the enchantment level passed. </summary>
///     
		public override int getMinEnchantability(int p_77321_1_)
		{
			return 1 + 10 * (p_77321_1_ - 1);
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
				return 5;
			}
		}

		public override bool canApply(ItemStack p_92089_1_)
		{
			return p_92089_1_.Item == Items.shears ? true : base.canApply(p_92089_1_);
		}
	}

}