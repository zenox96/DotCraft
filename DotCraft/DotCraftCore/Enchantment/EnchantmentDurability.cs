using System;

namespace DotCraftCore.Enchantment
{

	using ItemArmor = DotCraftCore.Item.ItemArmor;
	using ItemStack = DotCraftCore.Item.ItemStack;

	public class EnchantmentDurability : Enchantment
	{
		

		protected internal EnchantmentDurability(int p_i1924_1_, int p_i1924_2_) : base(p_i1924_1_, p_i1924_2_, EnumEnchantmentType.breakable)
		{
			this.Name = "durability";
		}

///    
///     <summary> * Returns the minimal value of enchantability needed on the enchantment level passed. </summary>
///     
		public override int getMinEnchantability(int p_77321_1_)
		{
			return 5 + (p_77321_1_ - 1) * 8;
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

		public override bool canApply(ItemStack p_92089_1_)
		{
			return p_92089_1_.ItemStackDamageable ? true : base.canApply(p_92089_1_);
		}

///    
///     <summary> * Used by ItemStack.attemptDamageItem. Randomly determines if a point of damage should be negated using the
///     * enchantment level (par1). If the ItemStack is Armor then there is a flat 60% chance for damage to be negated no
///     * matter the enchantment level, otherwise there is a 1-(par/1) chance for damage to be negated. </summary>
///     
		public static bool negateDamage(ItemStack p_92097_0_, int p_92097_1_, Random p_92097_2_)
		{
			return p_92097_0_.Item is ItemArmor && p_92097_2_.nextFloat() < 0.6F ? false : p_92097_2_.Next(p_92097_1_ + 1) > 0;
		}
	}

}