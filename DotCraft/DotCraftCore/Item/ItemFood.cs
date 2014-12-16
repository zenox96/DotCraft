namespace DotCraftCore.Item
{

	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using PotionEffect = DotCraftCore.Potion.PotionEffect;
	using World = DotCraftCore.World.World;

	public class ItemFood : Item
	{
	/// <summary> Number of ticks to run while 'EnumAction'ing until result.  </summary>
		public readonly int itemUseDuration;

	/// <summary> The amount this food item heals the player.  </summary>
		private readonly int healAmount;
		private readonly float saturationModifier;

	/// <summary> Whether wolves like this food (true for raw and cooked porkchop).  </summary>
		private readonly bool isWolfsFavoriteMeat;

///    
///     <summary> * If this field is true, the food can be consumed even if the player don't need to eat. </summary>
///     
		private bool alwaysEdible;

///    
///     <summary> * represents the potion effect that will occurr upon eating this food. Set by setPotionEffect </summary>
///     
		private int potionId;

	/// <summary> set by setPotionEffect  </summary>
		private int potionDuration;

	/// <summary> set by setPotionEffect  </summary>
		private int potionAmplifier;

	/// <summary> probably of the set potion effect occurring  </summary>
		private float potionEffectProbability;
		

		public ItemFood(int p_i45339_1_, float p_i45339_2_, bool p_i45339_3_)
		{
			this.itemUseDuration = 32;
			this.healAmount = p_i45339_1_;
			this.isWolfsFavoriteMeat = p_i45339_3_;
			this.saturationModifier = p_i45339_2_;
			this.CreativeTab = CreativeTabs.tabFood;
		}

		public ItemFood(int p_i45340_1_, bool p_i45340_2_) : this(p_i45340_1_, 0.6F, p_i45340_2_)
		{
		}

		public virtual ItemStack onEaten(ItemStack p_77654_1_, World p_77654_2_, EntityPlayer p_77654_3_)
		{
			--p_77654_1_.stackSize;
			p_77654_3_.FoodStats.func_151686_a(this, p_77654_1_);
			p_77654_2_.playSoundAtEntity(p_77654_3_, "random.burp", 0.5F, p_77654_2_.rand.nextFloat() * 0.1F + 0.9F);
			this.onFoodEaten(p_77654_1_, p_77654_2_, p_77654_3_);
			return p_77654_1_;
		}

		protected internal virtual void onFoodEaten(ItemStack p_77849_1_, World p_77849_2_, EntityPlayer p_77849_3_)
		{
			if (!p_77849_2_.isClient && this.potionId > 0 && p_77849_2_.rand.nextFloat() < this.potionEffectProbability)
			{
				p_77849_3_.addPotionEffect(new PotionEffect(this.potionId, this.potionDuration * 20, this.potionAmplifier));
			}
		}

///    
///     <summary> * How long it takes to use or consume an item </summary>
///     
		public virtual int getMaxItemUseDuration(ItemStack p_77626_1_)
		{
			return 32;
		}

///    
///     <summary> * returns the action that specifies what animation to play when the items is being used </summary>
///     
		public virtual EnumAction getItemUseAction(ItemStack p_77661_1_)
		{
			return EnumAction.eat;
		}

///    
///     <summary> * Called whenever this item is equipped and the right mouse button is pressed. Args: itemStack, world, entityPlayer </summary>
///     
		public virtual ItemStack onItemRightClick(ItemStack p_77659_1_, World p_77659_2_, EntityPlayer p_77659_3_)
		{
			if (p_77659_3_.canEat(this.alwaysEdible))
			{
				p_77659_3_.setItemInUse(p_77659_1_, this.getMaxItemUseDuration(p_77659_1_));
			}

			return p_77659_1_;
		}

		public virtual int func_150905_g(ItemStack p_150905_1_)
		{
			return this.healAmount;
		}

		public virtual float func_150906_h(ItemStack p_150906_1_)
		{
			return this.saturationModifier;
		}

///    
///     <summary> * Whether wolves like this food (true for raw and cooked porkchop). </summary>
///     
		public virtual bool isWolfsFavoriteMeat()
		{
			get
			{
				return this.isWolfsFavoriteMeat;
			}
		}

///    
///     <summary> * sets a potion effect on the item. Args: int potionId, int duration (will be multiplied by 20), int amplifier,
///     * float probability of effect happening </summary>
///     
		public virtual ItemFood setPotionEffect(int p_77844_1_, int p_77844_2_, int p_77844_3_, float p_77844_4_)
		{
			this.potionId = p_77844_1_;
			this.potionDuration = p_77844_2_;
			this.potionAmplifier = p_77844_3_;
			this.potionEffectProbability = p_77844_4_;
			return this;
		}

///    
///     <summary> * Set the field 'alwaysEdible' to true, and make the food edible even if the player don't need to eat. </summary>
///     
		public virtual ItemFood setAlwaysEdible()
		{
			this.alwaysEdible = true;
			return this;
		}
	}

}