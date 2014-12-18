using System.Collections;

namespace DotCraftCore.nEnchantment
{

	using Entity = DotCraftCore.nEntity.Entity;
	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using EnumCreatureAttribute = DotCraftCore.nEntity.EnumCreatureAttribute;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using StatCollector = DotCraftCore.nUtil.StatCollector;

	public abstract class Enchantment
	{
		public static readonly Enchantment[] enchantmentsList = new Enchantment[256];

	/// <summary> The list of enchantments applicable by the anvil from a book  </summary>
		public static readonly Enchantment[] enchantmentsBookList;

	/// <summary> Converts environmental damage to armour damage  </summary>
		public static readonly Enchantment protection = new EnchantmentProtection(0, 10, 0);

	/// <summary> Protection against fire  </summary>
		public static readonly Enchantment fireProtection = new EnchantmentProtection(1, 5, 1);

	/// <summary> Less fall damage  </summary>
		public static readonly Enchantment featherFalling = new EnchantmentProtection(2, 5, 2);

	/// <summary> Protection against explosions  </summary>
		public static readonly Enchantment blastProtection = new EnchantmentProtection(3, 2, 3);

	/// <summary> Protection against projectile entities (e.g. arrows)  </summary>
		public static readonly Enchantment projectileProtection = new EnchantmentProtection(4, 5, 4);

///    
///     <summary> * Decreases the rate of air loss underwater; increases time between damage while suffocating </summary>
///     
		public static readonly Enchantment respiration = new EnchantmentOxygen(5, 2);

	/// <summary> Increases underwater mining rate  </summary>
		public static readonly Enchantment aquaAffinity = new EnchantmentWaterWorker(6, 2);
		public static readonly Enchantment thorns = new EnchantmentThorns(7, 1);

	/// <summary> Extra damage to mobs  </summary>
		public static readonly Enchantment sharpness = new EnchantmentDamage(16, 10, 0);

	/// <summary> Extra damage to zombies, zombie pigmen and skeletons  </summary>
		public static readonly Enchantment smite = new EnchantmentDamage(17, 5, 1);

	/// <summary> Extra damage to spiders, cave spiders and silverfish  </summary>
		public static readonly Enchantment baneOfArthropods = new EnchantmentDamage(18, 5, 2);

	/// <summary> Knocks mob and players backwards upon hit  </summary>
		public static readonly Enchantment knockback = new EnchantmentKnockback(19, 5);

	/// <summary> Lights mobs on fire  </summary>
		public static readonly Enchantment fireAspect = new EnchantmentFireAspect(20, 2);

	/// <summary> Mobs have a chance to drop more loot  </summary>
		public static readonly Enchantment looting = new EnchantmentLootBonus(21, 2, EnumEnchantmentType.weapon);

	/// <summary> Faster resource gathering while in use  </summary>
		public static readonly Enchantment efficiency = new EnchantmentDigging(32, 10);

///    
///     <summary> * Blocks mined will drop themselves, even if it should drop something else (e.g. stone will drop stone, not
///     * cobblestone) </summary>
///     
		public static readonly Enchantment silkTouch = new EnchantmentUntouching(33, 1);

///    
///     <summary> * Sometimes, the tool's durability will not be spent when the tool is used </summary>
///     
		public static readonly Enchantment unbreaking = new EnchantmentDurability(34, 5);

	/// <summary> Can multiply the drop rate of items from blocks  </summary>
		public static readonly Enchantment fortune = new EnchantmentLootBonus(35, 2, EnumEnchantmentType.digger);

	/// <summary> Power enchantment for bows, add's extra damage to arrows.  </summary>
		public static readonly Enchantment power = new EnchantmentArrowDamage(48, 10);

///    
///     <summary> * Knockback enchantments for bows, the arrows will knockback the target when hit. </summary>
///     
		public static readonly Enchantment punch = new EnchantmentArrowKnockback(49, 2);

///    
///     <summary> * Flame enchantment for bows. Arrows fired by the bow will be on fire. Any target hit will also set on fire. </summary>
///     
		public static readonly Enchantment flame = new EnchantmentArrowFire(50, 2);

///    
///     <summary> * Infinity enchantment for bows. The bow will not consume arrows anymore, but will still required at least one
///     * arrow on inventory use the bow. </summary>
///     
		public static readonly Enchantment infinity = new EnchantmentArrowInfinite(51, 1);
		public static readonly Enchantment field_151370_z = new EnchantmentLootBonus(61, 2, EnumEnchantmentType.fishing_rod);
		public static readonly Enchantment field_151369_A = new EnchantmentFishingSpeed(62, 2, EnumEnchantmentType.fishing_rod);
		public readonly int effectId;
		private readonly int weight;

	/// <summary> The EnumEnchantmentType given to this Enchantment.  </summary>
		public EnumEnchantmentType type;

	/// <summary> Used in localisation and stats.  </summary>
		protected internal string name;
		

		protected internal Enchantment(int p_i1926_1_, int p_i1926_2_, EnumEnchantmentType p_i1926_3_)
		{
			this.effectId = p_i1926_1_;
			this.weight = p_i1926_2_;
			this.type = p_i1926_3_;

			if (enchantmentsList[p_i1926_1_] != null)
			{
				throw new System.ArgumentException("Duplicate enchantment id!");
			}
			else
			{
				enchantmentsList[p_i1926_1_] = this;
			}
		}

		public virtual int Weight
		{
			get
			{
				return this.weight;
			}
		}

///    
///     <summary> * Returns the minimum level that the enchantment can have. </summary>
///     
		public virtual int MinLevel
		{
			get
			{
				return 1;
			}
		}

///    
///     <summary> * Returns the maximum level that the enchantment can have. </summary>
///     
		public virtual int MaxLevel
		{
			get
			{
				return 1;
			}
		}

///    
///     <summary> * Returns the minimal value of enchantability needed on the enchantment level passed. </summary>
///     
		public virtual int getMinEnchantability(int p_77321_1_)
		{
			return 1 + p_77321_1_ * 10;
		}

///    
///     <summary> * Returns the maximum value of enchantability nedded on the enchantment level passed. </summary>
///     
		public virtual int getMaxEnchantability(int p_77317_1_)
		{
			return this.getMinEnchantability(p_77317_1_) + 5;
		}

///    
///     <summary> * Calculates de damage protection of the enchantment based on level and damage source passed. </summary>
///     
		public virtual int calcModifierDamage(int p_77318_1_, DamageSource p_77318_2_)
		{
			return 0;
		}

		public virtual float func_152376_a(int p_152376_1_, EnumCreatureAttribute p_152376_2_)
		{
			return 0.0F;
		}

///    
///     <summary> * Determines if the enchantment passed can be applyied together with this enchantment. </summary>
///     
		public virtual bool canApplyTogether(Enchantment p_77326_1_)
		{
			return this != p_77326_1_;
		}

///    
///     <summary> * Sets the enchantment name </summary>
///     
		public virtual Enchantment Name
		{
			set
			{
				this.name = value;
				return this;
			}
			get
			{
				return "enchantment." + this.name;
			}
		}

///    
///     <summary> * Return the name of key in translation table of this enchantment. </summary>
///     

///    
///     <summary> * Returns the correct traslated name of the enchantment and the level in roman numbers. </summary>
///     
		public virtual string getTranslatedName(int p_77316_1_)
		{
			string var2 = StatCollector.translateToLocal(this.Name);
			return var2 + " " + StatCollector.translateToLocal("enchantment.level." + p_77316_1_);
		}

		public virtual bool canApply(ItemStack p_92089_1_)
		{
			return this.type.canEnchantItem(p_92089_1_.Item);
		}

		public virtual void func_151368_a(EntityLivingBase p_151368_1_, Entity p_151368_2_, int p_151368_3_)
		{
		}

		public virtual void func_151367_b(EntityLivingBase p_151367_1_, Entity p_151367_2_, int p_151367_3_)
		{
		}

		static Enchantment()
		{
			ArrayList var0 = new ArrayList();
			Enchantment[] var1 = enchantmentsList;
			int var2 = var1.Length;

			for (int var3 = 0; var3 < var2; ++var3)
			{
				Enchantment var4 = var1[var3];

				if (var4 != null)
				{
					var0.Add(var4);
				}
			}

			enchantmentsBookList = (Enchantment[])var0.ToArray();
		}
	}

}