namespace DotCraftCore.Enchantment
{

	using Entity = DotCraftCore.Entity.Entity;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EnumCreatureAttribute = DotCraftCore.Entity.EnumCreatureAttribute;
	using ItemAxe = DotCraftCore.Item.ItemAxe;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using Potion = DotCraftCore.Potion.Potion;
	using PotionEffect = DotCraftCore.Potion.PotionEffect;

	public class EnchantmentDamage : Enchantment
	{
	/// <summary> Holds the name to be translated of each protection type.  </summary>
		private static readonly string[] protectionName = new string[] {"all", "undead", "arthropods"};

///    
///     <summary> * Holds the base factor of enchantability needed to be able to use the enchant. </summary>
///     
		private static readonly int[] baseEnchantability = new int[] {1, 5, 5};

///    
///     <summary> * Holds how much each level increased the enchantability factor to be able to use this enchant. </summary>
///     
		private static readonly int[] levelEnchantability = new int[] {11, 8, 8};

///    
///     <summary> * Used on the formula of base enchantability, this is the 'window' factor of values to be able to use thing
///     * enchant. </summary>
///     
		private static readonly int[] thresholdEnchantability = new int[] {20, 20, 20};

///    
///     <summary> * Defines the type of damage of the enchantment, 0 = all, 1 = undead, 3 = arthropods </summary>
///     
		public readonly int damageType;
		

		public EnchantmentDamage(int p_i1923_1_, int p_i1923_2_, int p_i1923_3_) : base(p_i1923_1_, p_i1923_2_, EnumEnchantmentType.weapon)
		{
			this.damageType = p_i1923_3_;
		}

///    
///     <summary> * Returns the minimal value of enchantability needed on the enchantment level passed. </summary>
///     
		public override int getMinEnchantability(int p_77321_1_)
		{
			return baseEnchantability[this.damageType] + (p_77321_1_ - 1) * levelEnchantability[this.damageType];
		}

///    
///     <summary> * Returns the maximum value of enchantability nedded on the enchantment level passed. </summary>
///     
		public override int getMaxEnchantability(int p_77317_1_)
		{
			return this.getMinEnchantability(p_77317_1_) + thresholdEnchantability[this.damageType];
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

		public override float func_152376_a(int p_152376_1_, EnumCreatureAttribute p_152376_2_)
		{
			return this.damageType == 0 ? (float)p_152376_1_ * 1.25F : (this.damageType == 1 && p_152376_2_ == EnumCreatureAttribute.UNDEAD ? (float)p_152376_1_ * 2.5F : (this.damageType == 2 && p_152376_2_ == EnumCreatureAttribute.ARTHROPOD ? (float)p_152376_1_ * 2.5F : 0.0F));
		}

///    
///     <summary> * Return the name of key in translation table of this enchantment. </summary>
///     
		public override string Name
		{
			get
			{
				return "enchantment.damage." + protectionName[this.damageType];
			}
		}

///    
///     <summary> * Determines if the enchantment passed can be applyied together with this enchantment. </summary>
///     
		public override bool canApplyTogether(Enchantment p_77326_1_)
		{
			return !(p_77326_1_ is EnchantmentDamage);
		}

		public override bool canApply(ItemStack p_92089_1_)
		{
			return p_92089_1_.Item is ItemAxe ? true : base.canApply(p_92089_1_);
		}

		public override void func_151368_a(EntityLivingBase p_151368_1_, Entity p_151368_2_, int p_151368_3_)
		{
			if (p_151368_2_ is EntityLivingBase)
			{
				EntityLivingBase var4 = (EntityLivingBase)p_151368_2_;

				if (this.damageType == 2 && var4.CreatureAttribute == EnumCreatureAttribute.ARTHROPOD)
				{
					int var5 = 20 + p_151368_1_.RNG.Next(10 * p_151368_3_);
					var4.addPotionEffect(new PotionEffect(Potion.moveSlowdown.id, var5, 3));
				}
			}
		}
	}

}