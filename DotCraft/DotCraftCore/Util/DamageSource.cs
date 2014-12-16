namespace DotCraftCore.Util
{

	using Entity = DotCraftCore.entity.Entity;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using EntityArrow = DotCraftCore.entity.projectile.EntityArrow;
	using EntityFireball = DotCraftCore.entity.projectile.EntityFireball;
	using Explosion = DotCraftCore.World.Explosion;

	public class DamageSource
	{
		public static DamageSource inFire = (new DamageSource("inFire")).setFireDamage();
		public static DamageSource onFire = (new DamageSource("onFire")).setDamageBypassesArmor().setFireDamage();
		public static DamageSource lava = (new DamageSource("lava")).setFireDamage();
		public static DamageSource inWall = (new DamageSource("inWall")).setDamageBypassesArmor();
		public static DamageSource drown = (new DamageSource("drown")).setDamageBypassesArmor();
		public static DamageSource starve = (new DamageSource("starve")).setDamageBypassesArmor().setDamageIsAbsolute();
		public static DamageSource cactus = new DamageSource("cactus");
		public static DamageSource fall = (new DamageSource("fall")).setDamageBypassesArmor();
		public static DamageSource outOfWorld = (new DamageSource("outOfWorld")).setDamageBypassesArmor().setDamageAllowedInCreativeMode();
		public static DamageSource generic = (new DamageSource("generic")).setDamageBypassesArmor();
		public static DamageSource magic = (new DamageSource("magic")).setDamageBypassesArmor().setMagicDamage();
		public static DamageSource wither = (new DamageSource("wither")).setDamageBypassesArmor();
		public static DamageSource anvil = new DamageSource("anvil");
		public static DamageSource fallingBlock = new DamageSource("fallingBlock");

	/// <summary> This kind of damage can be blocked or not.  </summary>
		private bool isUnblockable;
		private bool isDamageAllowedInCreativeMode;

///    
///     <summary> * Whether or not the damage ignores modification by potion effects or enchantments. </summary>
///     
		private bool damageIsAbsolute;
		private float hungerDamage = 0.3F;

	/// <summary> This kind of damage is based on fire or not.  </summary>
		private bool fireDamage;

	/// <summary> This kind of damage is based on a projectile or not.  </summary>
		private bool projectile;

///    
///     <summary> * Whether this damage source will have its damage amount scaled based on the current difficulty. </summary>
///     
		private bool difficultyScaled;
		private bool magicDamage;
		private bool explosion;
		public string damageType;
		

		public static DamageSource causeMobDamage(EntityLivingBase p_76358_0_)
		{
			return new EntityDamageSource("mob", p_76358_0_);
		}

///    
///     <summary> * returns an EntityDamageSource of type player </summary>
///     
		public static DamageSource causePlayerDamage(EntityPlayer p_76365_0_)
		{
			return new EntityDamageSource("player", p_76365_0_);
		}

///    
///     <summary> * returns EntityDamageSourceIndirect of an arrow </summary>
///     
		public static DamageSource causeArrowDamage(EntityArrow p_76353_0_, Entity p_76353_1_)
		{
			return(new EntityDamageSourceIndirect("arrow", p_76353_0_, p_76353_1_)).setProjectile();
		}

///    
///     <summary> * returns EntityDamageSourceIndirect of a fireball </summary>
///     
		public static DamageSource causeFireballDamage(EntityFireball p_76362_0_, Entity p_76362_1_)
		{
			return p_76362_1_ == null ? (new EntityDamageSourceIndirect("onFire", p_76362_0_, p_76362_0_)).setFireDamage().setProjectile() : (new EntityDamageSourceIndirect("fireball", p_76362_0_, p_76362_1_)).setFireDamage().setProjectile();
		}

		public static DamageSource causeThrownDamage(Entity p_76356_0_, Entity p_76356_1_)
		{
			return(new EntityDamageSourceIndirect("thrown", p_76356_0_, p_76356_1_)).setProjectile();
		}

		public static DamageSource causeIndirectMagicDamage(Entity p_76354_0_, Entity p_76354_1_)
		{
			return(new EntityDamageSourceIndirect("indirectMagic", p_76354_0_, p_76354_1_)).setDamageBypassesArmor().setMagicDamage();
		}

///    
///     <summary> * Returns the EntityDamageSource of the Thorns enchantment </summary>
///     
		public static DamageSource causeThornsDamage(Entity p_92087_0_)
		{
			return(new EntityDamageSource("thorns", p_92087_0_)).setMagicDamage();
		}

		public static DamageSource ExplosionSource
		{
			set
			{
				return value != null && value.ExplosivePlacedBy != null ? (new EntityDamageSource("explosion.player", value.ExplosivePlacedBy)).setDifficultyScaled().setExplosion() : (new DamageSource("explosion")).setDifficultyScaled().setExplosion();
			}
		}

///    
///     <summary> * Returns true if the damage is projectile based. </summary>
///     
		public virtual bool isProjectile()
		{
			get
			{
				return this.projectile;
			}
		}

///    
///     <summary> * Define the damage type as projectile based. </summary>
///     
		public virtual DamageSource setProjectile()
		{
			this.projectile = true;
			return this;
		}

		public virtual bool isExplosion()
		{
			get
			{
				return this.explosion;
			}
		}

		public virtual DamageSource setExplosion()
		{
			this.explosion = true;
			return this;
		}

		public virtual bool isUnblockable()
		{
			get
			{
				return this.isUnblockable;
			}
		}

///    
///     <summary> * How much satiate(food) is consumed by this DamageSource </summary>
///     
		public virtual float HungerDamage
		{
			get
			{
				return this.hungerDamage;
			}
		}

		public virtual bool canHarmInCreative()
		{
			return this.isDamageAllowedInCreativeMode;
		}

///    
///     <summary> * Whether or not the damage ignores modification by potion effects or enchantments. </summary>
///     
		public virtual bool isDamageAbsolute()
		{
			get
			{
				return this.damageIsAbsolute;
			}
		}

		protected internal DamageSource(string p_i1566_1_)
		{
			this.damageType = p_i1566_1_;
		}

		public virtual Entity SourceOfDamage
		{
			get
			{
				return this.Entity;
			}
		}

		public virtual Entity Entity
		{
			get
			{
				return null;
			}
		}

		protected internal virtual DamageSource setDamageBypassesArmor()
		{
			this.isUnblockable = true;
			this.hungerDamage = 0.0F;
			return this;
		}

		protected internal virtual DamageSource setDamageAllowedInCreativeMode()
		{
			this.isDamageAllowedInCreativeMode = true;
			return this;
		}

///    
///     <summary> * Sets a value indicating whether the damage is absolute (ignores modification by potion effects or enchantments),
///     * and also clears out hunger damage. </summary>
///     
		protected internal virtual DamageSource setDamageIsAbsolute()
		{
			this.damageIsAbsolute = true;
			this.hungerDamage = 0.0F;
			return this;
		}

///    
///     <summary> * Define the damage type as fire based. </summary>
///     
		protected internal virtual DamageSource setFireDamage()
		{
			this.fireDamage = true;
			return this;
		}

		public virtual IChatComponent func_151519_b(EntityLivingBase p_151519_1_)
		{
			EntityLivingBase var2 = p_151519_1_.func_94060_bK();
			string var3 = "death.attack." + this.damageType;
			string var4 = var3 + ".player";
			return var2 != null && StatCollector.canTranslate(var4) ? new ChatComponentTranslation(var4, new object[] {p_151519_1_.func_145748_c_(), var2.func_145748_c_()}): new ChatComponentTranslation(var3, new object[] {p_151519_1_.func_145748_c_()});
		}

///    
///     <summary> * Returns true if the damage is fire based. </summary>
///     
		public virtual bool isFireDamage()
		{
			get
			{
				return this.fireDamage;
			}
		}

///    
///     <summary> * Return the name of damage type. </summary>
///     
		public virtual string DamageType
		{
			get
			{
				return this.damageType;
			}
		}

///    
///     <summary> * Set whether this damage source will have its damage amount scaled based on the current difficulty. </summary>
///     
		public virtual DamageSource setDifficultyScaled()
		{
			this.difficultyScaled = true;
			return this;
		}

///    
///     <summary> * Return whether this damage source will have its damage amount scaled based on the current difficulty. </summary>
///     
		public virtual bool isDifficultyScaled()
		{
			get
			{
				return this.difficultyScaled;
			}
		}

///    
///     <summary> * Returns true if the damage is magic based. </summary>
///     
		public virtual bool isMagicDamage()
		{
			get
			{
				return this.magicDamage;
			}
		}

///    
///     <summary> * Define the damage type as magic based. </summary>
///     
		public virtual DamageSource setMagicDamage()
		{
			this.magicDamage = true;
			return this;
		}
	}

}