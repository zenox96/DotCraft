namespace DotCraftCore.Enchantment
{

	using Entity = DotCraftCore.Entity.Entity;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using MathHelper = DotCraftCore.Util.MathHelper;

	public class EnchantmentProtection : Enchantment
	{
	/// <summary> Holds the name to be translated of each protection type.  </summary>
		private static readonly string[] protectionName = new string[] {"all", "fire", "fall", "explosion", "projectile"};

///    
///     <summary> * Holds the base factor of enchantability needed to be able to use the enchant. </summary>
///     
		private static readonly int[] baseEnchantability = new int[] {1, 10, 5, 5, 3};

///    
///     <summary> * Holds how much each level increased the enchantability factor to be able to use this enchant. </summary>
///     
		private static readonly int[] levelEnchantability = new int[] {11, 8, 6, 8, 6};

///    
///     <summary> * Used on the formula of base enchantability, this is the 'window' factor of values to be able to use thing
///     * enchant. </summary>
///     
		private static readonly int[] thresholdEnchantability = new int[] {20, 12, 10, 12, 15};

///    
///     <summary> * Defines the type of protection of the enchantment, 0 = all, 1 = fire, 2 = fall (feather fall), 3 = explosion and
///     * 4 = projectile. </summary>
///     
		public readonly int protectionType;
		private const string __OBFID = "CL_00000121";

		public EnchantmentProtection(int p_i1936_1_, int p_i1936_2_, int p_i1936_3_) : base(p_i1936_1_, p_i1936_2_, EnumEnchantmentType.armor)
		{
			this.protectionType = p_i1936_3_;

			if (p_i1936_3_ == 2)
			{
				this.type = EnumEnchantmentType.armor_feet;
			}
		}

///    
///     <summary> * Returns the minimal value of enchantability needed on the enchantment level passed. </summary>
///     
		public override int getMinEnchantability(int p_77321_1_)
		{
			return baseEnchantability[this.protectionType] + (p_77321_1_ - 1) * levelEnchantability[this.protectionType];
		}

///    
///     <summary> * Returns the maximum value of enchantability nedded on the enchantment level passed. </summary>
///     
		public override int getMaxEnchantability(int p_77317_1_)
		{
			return this.getMinEnchantability(p_77317_1_) + thresholdEnchantability[this.protectionType];
		}

///    
///     <summary> * Returns the maximum level that the enchantment can have. </summary>
///     
		public override int MaxLevel
		{
			get
			{
				return 4;
			}
		}

///    
///     <summary> * Calculates de damage protection of the enchantment based on level and damage source passed. </summary>
///     
		public override int calcModifierDamage(int p_77318_1_, DamageSource p_77318_2_)
		{
			if (p_77318_2_.canHarmInCreative())
			{
				return 0;
			}
			else
			{
				float var3 = (float)(6 + p_77318_1_ * p_77318_1_) / 3.0F;
				return this.protectionType == 0 ? MathHelper.floor_float(var3 * 0.75F) : (this.protectionType == 1 && p_77318_2_.FireDamage ? MathHelper.floor_float(var3 * 1.25F) : (this.protectionType == 2 && p_77318_2_ == DamageSource.fall ? MathHelper.floor_float(var3 * 2.5F) : (this.protectionType == 3 && p_77318_2_.Explosion ? MathHelper.floor_float(var3 * 1.5F) : (this.protectionType == 4 && p_77318_2_.Projectile ? MathHelper.floor_float(var3 * 1.5F) : 0))));
			}
		}

///    
///     <summary> * Return the name of key in translation table of this enchantment. </summary>
///     
		public override string Name
		{
			get
			{
				return "enchantment.protect." + protectionName[this.protectionType];
			}
		}

///    
///     <summary> * Determines if the enchantment passed can be applyied together with this enchantment. </summary>
///     
		public override bool canApplyTogether(Enchantment p_77326_1_)
		{
			if (p_77326_1_ is EnchantmentProtection)
			{
				EnchantmentProtection var2 = (EnchantmentProtection)p_77326_1_;
				return var2.protectionType == this.protectionType ? false : this.protectionType == 2 || var2.protectionType == 2;
			}
			else
			{
				return base.canApplyTogether(p_77326_1_);
			}
		}

///    
///     <summary> * Gets the amount of ticks an entity should be set fire, adjusted for fire protection. </summary>
///     
		public static int getFireTimeForEntity(Entity p_92093_0_, int p_92093_1_)
		{
			int var2 = EnchantmentHelper.getMaxEnchantmentLevel(Enchantment.fireProtection.effectId, p_92093_0_.LastActiveItems);

			if (var2 > 0)
			{
				p_92093_1_ -= MathHelper.floor_float((float)p_92093_1_ * (float)var2 * 0.15F);
			}

			return p_92093_1_;
		}

		public static double func_92092_a(Entity p_92092_0_, double p_92092_1_)
		{
			int var3 = EnchantmentHelper.getMaxEnchantmentLevel(Enchantment.blastProtection.effectId, p_92092_0_.LastActiveItems);

			if (var3 > 0)
			{
				p_92092_1_ -= (double)MathHelper.floor_double(p_92092_1_ * (double)((float)var3 * 0.15F));
			}

			return p_92092_1_;
		}
	}

}