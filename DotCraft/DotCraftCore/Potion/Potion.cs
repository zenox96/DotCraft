using System;
using System.Collections;

namespace DotCraftCore.Potion
{

	using Maps = com.google.common.collect.Maps;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using SharedMonsterAttributes = DotCraftCore.entity.SharedMonsterAttributes;
	using AttributeModifier = DotCraftCore.entity.ai.attributes.AttributeModifier;
	using BaseAttributeMap = DotCraftCore.entity.ai.attributes.BaseAttributeMap;
	using IAttribute = DotCraftCore.entity.ai.attributes.IAttribute;
	using IAttributeInstance = DotCraftCore.entity.ai.attributes.IAttributeInstance;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using StringUtils = DotCraftCore.Util.StringUtils;

	public class Potion
	{
	/// <summary> The array of potion types.  </summary>
		public static readonly Potion[] potionTypes = new Potion[32];
		public static readonly Potion field_76423_b = null;
		public static readonly Potion moveSpeed = (new Potion(1, false, 8171462)).setPotionName("potion.moveSpeed").setIconIndex(0, 0).func_111184_a(SharedMonsterAttributes.movementSpeed, "91AEAA56-376B-4498-935B-2F7F68070635", 0.20000000298023224D, 2);
		public static readonly Potion moveSlowdown = (new Potion(2, true, 5926017)).setPotionName("potion.moveSlowdown").setIconIndex(1, 0).func_111184_a(SharedMonsterAttributes.movementSpeed, "7107DE5E-7CE8-4030-940E-514C1F160890", -0.15000000596046448D, 2);
		public static readonly Potion digSpeed = (new Potion(3, false, 14270531)).setPotionName("potion.digSpeed").setIconIndex(2, 0).setEffectiveness(1.5D);
		public static readonly Potion digSlowdown = (new Potion(4, true, 4866583)).setPotionName("potion.digSlowDown").setIconIndex(3, 0);
		public static readonly Potion damageBoost = (new PotionAttackDamage(5, false, 9643043)).setPotionName("potion.damageBoost").setIconIndex(4, 0).func_111184_a(SharedMonsterAttributes.attackDamage, "648D7064-6A60-4F59-8ABE-C2C23A6DD7A9", 3.0D, 2);
		public static readonly Potion heal = (new PotionHealth(6, false, 16262179)).PotionName = "potion.heal";
		public static readonly Potion harm = (new PotionHealth(7, true, 4393481)).PotionName = "potion.harm";
		public static readonly Potion jump = (new Potion(8, false, 7889559)).setPotionName("potion.jump").setIconIndex(2, 1);
		public static readonly Potion confusion = (new Potion(9, true, 5578058)).setPotionName("potion.confusion").setIconIndex(3, 1).setEffectiveness(0.25D);

	/// <summary> The regeneration Potion object.  </summary>
		public static readonly Potion regeneration = (new Potion(10, false, 13458603)).setPotionName("potion.regeneration").setIconIndex(7, 0).setEffectiveness(0.25D);
		public static readonly Potion resistance = (new Potion(11, false, 10044730)).setPotionName("potion.resistance").setIconIndex(6, 1);

	/// <summary> The fire resistance Potion object.  </summary>
		public static readonly Potion fireResistance = (new Potion(12, false, 14981690)).setPotionName("potion.fireResistance").setIconIndex(7, 1);

	/// <summary> The water breathing Potion object.  </summary>
		public static readonly Potion waterBreathing = (new Potion(13, false, 3035801)).setPotionName("potion.waterBreathing").setIconIndex(0, 2);

	/// <summary> The invisibility Potion object.  </summary>
		public static readonly Potion invisibility = (new Potion(14, false, 8356754)).setPotionName("potion.invisibility").setIconIndex(0, 1);

	/// <summary> The blindness Potion object.  </summary>
		public static readonly Potion blindness = (new Potion(15, true, 2039587)).setPotionName("potion.blindness").setIconIndex(5, 1).setEffectiveness(0.25D);

	/// <summary> The night vision Potion object.  </summary>
		public static readonly Potion nightVision = (new Potion(16, false, 2039713)).setPotionName("potion.nightVision").setIconIndex(4, 1);

	/// <summary> The hunger Potion object.  </summary>
		public static readonly Potion hunger = (new Potion(17, true, 5797459)).setPotionName("potion.hunger").setIconIndex(1, 1);

	/// <summary> The weakness Potion object.  </summary>
		public static readonly Potion weakness = (new PotionAttackDamage(18, true, 4738376)).setPotionName("potion.weakness").setIconIndex(5, 0).func_111184_a(SharedMonsterAttributes.attackDamage, "22653B89-116E-49DC-9B6B-9971489B5BE5", 2.0D, 0);

	/// <summary> The poison Potion object.  </summary>
		public static readonly Potion poison = (new Potion(19, true, 5149489)).setPotionName("potion.poison").setIconIndex(6, 0).setEffectiveness(0.25D);

	/// <summary> The wither Potion object.  </summary>
		public static readonly Potion wither = (new Potion(20, true, 3484199)).setPotionName("potion.wither").setIconIndex(1, 2).setEffectiveness(0.25D);
		public static readonly Potion field_76434_w = (new PotionHealthBoost(21, false, 16284963)).setPotionName("potion.healthBoost").setIconIndex(2, 2).func_111184_a(SharedMonsterAttributes.maxHealth, "5D6F0BA2-1186-46AC-B896-C61C5CEE99CC", 4.0D, 0);
		public static readonly Potion field_76444_x = (new PotionAbsoption(22, false, 2445989)).setPotionName("potion.absorption").setIconIndex(2, 2);
		public static readonly Potion field_76443_y = (new PotionHealth(23, false, 16262179)).PotionName = "potion.saturation";
		public static readonly Potion field_76442_z = null;
		public static readonly Potion field_76409_A = null;
		public static readonly Potion field_76410_B = null;
		public static readonly Potion field_76411_C = null;
		public static readonly Potion field_76405_D = null;
		public static readonly Potion field_76406_E = null;
		public static readonly Potion field_76407_F = null;
		public static readonly Potion field_76408_G = null;

	/// <summary> The Id of a Potion object.  </summary>
		public readonly int id;
		private readonly IDictionary field_111188_I = Maps.newHashMap();

///    
///     <summary> * This field indicated if the effect is 'bad' - negative - for the entity. </summary>
///     
		private readonly bool isBadEffect;

	/// <summary> Is the color of the liquid for this potion.  </summary>
		private readonly int liquidColor;

	/// <summary> The name of the Potion.  </summary>
		private string name = "";

	/// <summary> The index for the icon displayed when the potion effect is active.  </summary>
		private int statusIconIndex = -1;
		private double effectiveness;
		private bool usable;
		private const string __OBFID = "CL_00001528";

		protected internal Potion(int p_i1573_1_, bool p_i1573_2_, int p_i1573_3_)
		{
			this.id = p_i1573_1_;
			potionTypes[p_i1573_1_] = this;
			this.isBadEffect = p_i1573_2_;

			if (p_i1573_2_)
			{
				this.effectiveness = 0.5D;
			}
			else
			{
				this.effectiveness = 1.0D;
			}

			this.liquidColor = p_i1573_3_;
		}

///    
///     <summary> * Sets the index for the icon displayed in the player's inventory when the status is active. </summary>
///     
		protected internal virtual Potion setIconIndex(int p_76399_1_, int p_76399_2_)
		{
			this.statusIconIndex = p_76399_1_ + p_76399_2_ * 8;
			return this;
		}

///    
///     <summary> * returns the ID of the potion </summary>
///     
		public virtual int Id
		{
			get
			{
				return this.id;
			}
		}

		public virtual void performEffect(EntityLivingBase p_76394_1_, int p_76394_2_)
		{
			if (this.id == regeneration.id)
			{
				if (p_76394_1_.Health < p_76394_1_.MaxHealth)
				{
					p_76394_1_.heal(1.0F);
				}
			}
			else if (this.id == poison.id)
			{
				if (p_76394_1_.Health > 1.0F)
				{
					p_76394_1_.attackEntityFrom(DamageSource.magic, 1.0F);
				}
			}
			else if (this.id == wither.id)
			{
				p_76394_1_.attackEntityFrom(DamageSource.wither, 1.0F);
			}
			else if (this.id == hunger.id && p_76394_1_ is EntityPlayer)
			{
				((EntityPlayer)p_76394_1_).addExhaustion(0.025F * (float)(p_76394_2_ + 1));
			}
			else if (this.id == field_76443_y.id && p_76394_1_ is EntityPlayer)
			{
				if (!p_76394_1_.worldObj.isClient)
				{
					((EntityPlayer)p_76394_1_).FoodStats.addStats(p_76394_2_ + 1, 1.0F);
				}
			}
			else if ((this.id != heal.id || p_76394_1_.EntityUndead) && (this.id != harm.id || !p_76394_1_.EntityUndead))
			{
				if (this.id == harm.id && !p_76394_1_.EntityUndead || this.id == heal.id && p_76394_1_.EntityUndead)
				{
					p_76394_1_.attackEntityFrom(DamageSource.magic, (float)(6 << p_76394_2_));
				}
			}
			else
			{
				p_76394_1_.heal((float)Math.Max(4 << p_76394_2_, 0));
			}
		}

///    
///     <summary> * Hits the provided entity with this potion's instant effect. </summary>
///     
		public virtual void affectEntity(EntityLivingBase p_76402_1_, EntityLivingBase p_76402_2_, int p_76402_3_, double p_76402_4_)
		{
			int var6;

			if ((this.id != heal.id || p_76402_2_.EntityUndead) && (this.id != harm.id || !p_76402_2_.EntityUndead))
			{
				if (this.id == harm.id && !p_76402_2_.EntityUndead || this.id == heal.id && p_76402_2_.EntityUndead)
				{
					var6 = (int)(p_76402_4_ * (double)(6 << p_76402_3_) + 0.5D);

					if (p_76402_1_ == null)
					{
						p_76402_2_.attackEntityFrom(DamageSource.magic, (float)var6);
					}
					else
					{
						p_76402_2_.attackEntityFrom(DamageSource.causeIndirectMagicDamage(p_76402_2_, p_76402_1_), (float)var6);
					}
				}
			}
			else
			{
				var6 = (int)(p_76402_4_ * (double)(4 << p_76402_3_) + 0.5D);
				p_76402_2_.heal((float)var6);
			}
		}

///    
///     <summary> * Returns true if the potion has an instant effect instead of a continuous one (eg Harming) </summary>
///     
		public virtual bool isInstant()
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * checks if Potion effect is ready to be applied this tick. </summary>
///     
		public virtual bool isReady(int p_76397_1_, int p_76397_2_)
		{
			int var3;

			if (this.id == regeneration.id)
			{
				var3 = 50 >> p_76397_2_;
				return var3 > 0 ? p_76397_1_ % var3 == 0 : true;
			}
			else if (this.id == poison.id)
			{
				var3 = 25 >> p_76397_2_;
				return var3 > 0 ? p_76397_1_ % var3 == 0 : true;
			}
			else if (this.id == wither.id)
			{
				var3 = 40 >> p_76397_2_;
				return var3 > 0 ? p_76397_1_ % var3 == 0 : true;
			}
			else
			{
				return this.id == hunger.id;
			}
		}

///    
///     <summary> * Set the potion name. </summary>
///     
		public virtual Potion PotionName
		{
			set
			{
				this.name = value;
				return this;
			}
		}

///    
///     <summary> * returns the name of the potion </summary>
///     
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

///    
///     <summary> * Returns true if the potion has a associated status icon to display in then inventory when active. </summary>
///     
		public virtual bool hasStatusIcon()
		{
			return this.statusIconIndex >= 0;
		}

///    
///     <summary> * Returns the index for the icon to display when the potion is active. </summary>
///     
		public virtual int StatusIconIndex
		{
			get
			{
				return this.statusIconIndex;
			}
		}

///    
///     <summary> * This method returns true if the potion effect is bad - negative - for the entity. </summary>
///     
		public virtual bool isBadEffect()
		{
			get
			{
				return this.isBadEffect;
			}
		}

		public static string getDurationString(PotionEffect p_76389_0_)
		{
			if (p_76389_0_.IsPotionDurationMax)
			{
				return "**:**";
			}
			else
			{
				int var1 = p_76389_0_.Duration;
				return StringUtils.ticksToElapsedTime(var1);
			}
		}

		protected internal virtual Potion Effectiveness
		{
			set
			{
				this.effectiveness = value;
				return this;
			}
			get
			{
				return this.effectiveness;
			}
		}


		public virtual bool isUsable()
		{
			get
			{
				return this.usable;
			}
		}

///    
///     <summary> * Returns the color of the potion liquid. </summary>
///     
		public virtual int LiquidColor
		{
			get
			{
				return this.liquidColor;
			}
		}

		public virtual Potion func_111184_a(IAttribute p_111184_1_, string p_111184_2_, double p_111184_3_, int p_111184_5_)
		{
			AttributeModifier var6 = new AttributeModifier(UUID.fromString(p_111184_2_), this.Name, p_111184_3_, p_111184_5_);
			this.field_111188_I.Add(p_111184_1_, var6);
			return this;
		}

		public virtual IDictionary func_111186_k()
		{
			return this.field_111188_I;
		}

		public virtual void removeAttributesModifiersFromEntity(EntityLivingBase p_111187_1_, BaseAttributeMap p_111187_2_, int p_111187_3_)
		{
			IEnumerator var4 = this.field_111188_I.GetEnumerator();

			while (var4.MoveNext())
			{
				Entry var5 = (Entry)var4.Current;
				IAttributeInstance var6 = p_111187_2_.getAttributeInstance((IAttribute)var5.Key);

				if (var6 != null)
				{
					var6.removeModifier((AttributeModifier)var5.Value);
				}
			}
		}

		public virtual void applyAttributesModifiersToEntity(EntityLivingBase p_111185_1_, BaseAttributeMap p_111185_2_, int p_111185_3_)
		{
			IEnumerator var4 = this.field_111188_I.GetEnumerator();

			while (var4.MoveNext())
			{
				Entry var5 = (Entry)var4.Current;
				IAttributeInstance var6 = p_111185_2_.getAttributeInstance((IAttribute)var5.Key);

				if (var6 != null)
				{
					AttributeModifier var7 = (AttributeModifier)var5.Value;
					var6.removeModifier(var7);
					var6.applyModifier(new AttributeModifier(var7.ID, this.Name + " " + p_111185_3_, this.func_111183_a(p_111185_3_, var7), var7.Operation));
				}
			}
		}

		public virtual double func_111183_a(int p_111183_1_, AttributeModifier p_111183_2_)
		{
			return p_111183_2_.Amount * (double)(p_111183_1_ + 1);
		}
	}

}