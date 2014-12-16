using System;
using System.Collections;

namespace DotCraftCore.Entity.Monster
{

	using Material = DotCraftCore.block.material.Material;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using IRangedAttackMob = DotCraftCore.Entity.IRangedAttackMob;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using EntityAIArrowAttack = DotCraftCore.Entity.AI.EntityAIArrowAttack;
	using EntityAIHurtByTarget = DotCraftCore.Entity.AI.EntityAIHurtByTarget;
	using EntityAILookIdle = DotCraftCore.Entity.AI.EntityAILookIdle;
	using EntityAINearestAttackableTarget = DotCraftCore.Entity.AI.EntityAINearestAttackableTarget;
	using EntityAISwimming = DotCraftCore.Entity.AI.EntityAISwimming;
	using EntityAIWander = DotCraftCore.Entity.AI.EntityAIWander;
	using EntityAIWatchClosest = DotCraftCore.Entity.AI.EntityAIWatchClosest;
	using AttributeModifier = DotCraftCore.Entity.AI.Attributes.AttributeModifier;
	using IAttributeInstance = DotCraftCore.Entity.AI.Attributes.IAttributeInstance;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using EntityPotion = DotCraftCore.Entity.Projectile.EntityPotion;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using Potion = DotCraftCore.Potion.Potion;
	using PotionEffect = DotCraftCore.Potion.PotionEffect;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;

	public class EntityWitch : EntityMob, IRangedAttackMob
	{
		private static readonly UUID field_110184_bp = UUID.fromString("5CD17E52-A79A-43D3-A529-90FDE04B181E");
		private static readonly AttributeModifier field_110185_bq = (new AttributeModifier(field_110184_bp, "Drinking speed penalty", -0.25D, 0)).Saved = false;

	/// <summary> List of items a witch should drop on death.  </summary>
		private static readonly Item[] witchDrops = new Item[] {Items.glowstone_dust, Items.sugar, Items.redstone, Items.spider_eye, Items.glass_bottle, Items.gunpowder, Items.stick, Items.stick};

///    
///     <summary> * Timer used as interval for a witch's attack, decremented every tick if aggressive and when reaches zero the witch
///     * will throw a potion at the target entity. </summary>
///     
		private int witchAttackTimer;
		

		public EntityWitch(World p_i1744_1_) : base(p_i1744_1_)
		{
			this.tasks.addTask(1, new EntityAISwimming(this));
			this.tasks.addTask(2, new EntityAIArrowAttack(this, 1.0D, 60, 10.0F));
			this.tasks.addTask(2, new EntityAIWander(this, 1.0D));
			this.tasks.addTask(3, new EntityAIWatchClosest(this, typeof(EntityPlayer), 8.0F));
			this.tasks.addTask(3, new EntityAILookIdle(this));
			this.targetTasks.addTask(1, new EntityAIHurtByTarget(this, false));
			this.targetTasks.addTask(2, new EntityAINearestAttackableTarget(this, typeof(EntityPlayer), 0, true));
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.DataWatcher.addObject(21, Convert.ToByte((sbyte)0));
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return "mob.witch.idle";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.witch.hurt";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.witch.death";
			}
		}

///    
///     <summary> * Set whether this witch is aggressive at an entity. </summary>
///     
		public virtual bool Aggressive
		{
			set
			{
				this.DataWatcher.updateObject(21, Convert.ToByte((sbyte)(value ? 1 : 0)));
			}
			get
			{
				return this.DataWatcher.getWatchableObjectByte(21) == 1;
			}
		}

///    
///     <summary> * Return whether this witch is aggressive at an entity. </summary>
///     

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 26.0D;
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.25D;
		}

///    
///     <summary> * Returns true if the newer Entity AI code should be run </summary>
///     
		public override bool isAIEnabled()
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public override void onLivingUpdate()
		{
			if (!this.worldObj.isClient)
			{
				if (this.Aggressive)
				{
					if (this.witchAttackTimer-- <= 0)
					{
						this.Aggressive = false;
						ItemStack var1 = this.HeldItem;
						this.setCurrentItemOrArmor(0, (ItemStack)null);

						if (var1 != null && var1.Item == Items.potionitem)
						{
							IList var2 = Items.potionitem.getEffects(var1);

							if (var2 != null)
							{
								IEnumerator var3 = var2.GetEnumerator();

								while (var3.MoveNext())
								{
									PotionEffect var4 = (PotionEffect)var3.Current;
									this.addPotionEffect(new PotionEffect(var4));
								}
							}
						}

						this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).removeModifier(field_110185_bq);
					}
				}
				else
				{
					short var5 = -1;

					if (this.rand.nextFloat() < 0.15F && this.isInsideOfMaterial(Material.water) && !this.isPotionActive(Potion.waterBreathing))
					{
						var5 = 8237;
					}
					else if (this.rand.nextFloat() < 0.15F && this.Burning && !this.isPotionActive(Potion.fireResistance))
					{
						var5 = 16307;
					}
					else if (this.rand.nextFloat() < 0.05F && this.Health < this.MaxHealth)
					{
						var5 = 16341;
					}
					else if (this.rand.nextFloat() < 0.25F && this.AttackTarget != null && !this.isPotionActive(Potion.moveSpeed) && this.AttackTarget.getDistanceSqToEntity(this) > 121.0D)
					{
						var5 = 16274;
					}
					else if (this.rand.nextFloat() < 0.25F && this.AttackTarget != null && !this.isPotionActive(Potion.moveSpeed) && this.AttackTarget.getDistanceSqToEntity(this) > 121.0D)
					{
						var5 = 16274;
					}

					if (var5 > -1)
					{
						this.setCurrentItemOrArmor(0, new ItemStack(Items.potionitem, 1, var5));
						this.witchAttackTimer = this.HeldItem.MaxItemUseDuration;
						this.Aggressive = true;
						IAttributeInstance var6 = this.getEntityAttribute(SharedMonsterAttributes.movementSpeed);
						var6.removeModifier(field_110185_bq);
						var6.applyModifier(field_110185_bq);
					}
				}

				if (this.rand.nextFloat() < 7.5E-4F)
				{
					this.worldObj.setEntityState(this, (sbyte)15);
				}
			}

			base.onLivingUpdate();
		}

		public override void handleHealthUpdate(sbyte p_70103_1_)
		{
			if (p_70103_1_ == 15)
			{
				for (int var2 = 0; var2 < this.rand.Next(35) + 10; ++var2)
				{
					this.worldObj.spawnParticle("witchMagic", this.posX + this.rand.nextGaussian() * 0.12999999523162842D, this.boundingBox.maxY + 0.5D + this.rand.nextGaussian() * 0.12999999523162842D, this.posZ + this.rand.nextGaussian() * 0.12999999523162842D, 0.0D, 0.0D, 0.0D);
				}
			}
			else
			{
				base.handleHealthUpdate(p_70103_1_);
			}
		}

///    
///     <summary> * Reduces damage, depending on potions </summary>
///     
		protected internal override float applyPotionDamageCalculations(DamageSource p_70672_1_, float p_70672_2_)
		{
			p_70672_2_ = base.applyPotionDamageCalculations(p_70672_1_, p_70672_2_);

			if (p_70672_1_.Entity == this)
			{
				p_70672_2_ = 0.0F;
			}

			if (p_70672_1_.MagicDamage)
			{
				p_70672_2_ = (float)((double)p_70672_2_ * 0.15D);
			}

			return p_70672_2_;
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			int var3 = this.rand.Next(3) + 1;

			for (int var4 = 0; var4 < var3; ++var4)
			{
				int var5 = this.rand.Next(3);
				Item var6 = witchDrops[this.rand.Next(witchDrops.Length)];

				if (p_70628_2_ > 0)
				{
					var5 += this.rand.Next(p_70628_2_ + 1);
				}

				for (int var7 = 0; var7 < var5; ++var7)
				{
					this.func_145779_a(var6, 1);
				}
			}
		}

///    
///     <summary> * Attack the specified entity using a ranged attack. </summary>
///     
		public virtual void attackEntityWithRangedAttack(EntityLivingBase p_82196_1_, float p_82196_2_)
		{
			if (!this.Aggressive)
			{
				EntityPotion var3 = new EntityPotion(this.worldObj, this, 32732);
				var3.rotationPitch -= -20.0F;
				double var4 = p_82196_1_.posX + p_82196_1_.motionX - this.posX;
				double var6 = p_82196_1_.posY + (double)p_82196_1_.EyeHeight - 1.100000023841858D - this.posY;
				double var8 = p_82196_1_.posZ + p_82196_1_.motionZ - this.posZ;
				float var10 = MathHelper.sqrt_double(var4 * var4 + var8 * var8);

				if (var10 >= 8.0F && !p_82196_1_.isPotionActive(Potion.moveSlowdown))
				{
					var3.PotionDamage = 32698;
				}
				else if (p_82196_1_.Health >= 8.0F && !p_82196_1_.isPotionActive(Potion.poison))
				{
					var3.PotionDamage = 32660;
				}
				else if (var10 <= 3.0F && !p_82196_1_.isPotionActive(Potion.weakness) && this.rand.nextFloat() < 0.25F)
				{
					var3.PotionDamage = 32696;
				}

				var3.setThrowableHeading(var4, var6 + (double)(var10 * 0.2F), var8, 0.75F, 8.0F);
				this.worldObj.spawnEntityInWorld(var3);
			}
		}
	}

}