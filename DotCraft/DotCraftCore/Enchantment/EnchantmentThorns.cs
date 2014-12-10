using System;

namespace DotCraftCore.Enchantment
{

	using Entity = DotCraftCore.Entity.Entity;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using ItemArmor = DotCraftCore.Item.ItemArmor;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using DamageSource = DotCraftCore.Util.DamageSource;

	public class EnchantmentThorns : Enchantment
	{
		private const string __OBFID = "CL_00000122";

		public EnchantmentThorns(int p_i1937_1_, int p_i1937_2_) : base(p_i1937_1_, p_i1937_2_, EnumEnchantmentType.armor_torso)
		{
			this.Name = "thorns";
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
				return 3;
			}
		}

		public override bool canApply(ItemStack p_92089_1_)
		{
			return p_92089_1_.Item is ItemArmor ? true : base.canApply(p_92089_1_);
		}

		public override void func_151367_b(EntityLivingBase p_151367_1_, Entity p_151367_2_, int p_151367_3_)
		{
			Random var4 = p_151367_1_.RNG;
			ItemStack var5 = EnchantmentHelper.func_92099_a(Enchantment.thorns, p_151367_1_);

			if (func_92094_a(p_151367_3_, var4))
			{
				p_151367_2_.attackEntityFrom(DamageSource.causeThornsDamage(p_151367_1_), (float)func_92095_b(p_151367_3_, var4));
				p_151367_2_.playSound("damage.thorns", 0.5F, 1.0F);

				if (var5 != null)
				{
					var5.damageItem(3, p_151367_1_);
				}
			}
			else if (var5 != null)
			{
				var5.damageItem(1, p_151367_1_);
			}
		}

		public static bool func_92094_a(int p_92094_0_, Random p_92094_1_)
		{
			return p_92094_0_ <= 0 ? false : p_92094_1_.nextFloat() < 0.15F * (float)p_92094_0_;
		}

		public static int func_92095_b(int p_92095_0_, Random p_92095_1_)
		{
			return p_92095_0_ > 10 ? p_92095_0_ - 10 : 1 + p_92095_1_.Next(4);
		}
	}

}