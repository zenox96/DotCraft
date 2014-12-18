using System;
using System.Collections;

namespace DotCraftCore.nEntity
{

	using Block = DotCraftCore.nBlock.Block;
	using Material = DotCraftCore.nBlock.nMaterial.Material;
	using EnchantmentHelper = DotCraftCore.nEnchantment.EnchantmentHelper;
	using AttributeModifier = DotCraftCore.nEntity.nAI.nAttributes.AttributeModifier;
	using BaseAttributeMap = DotCraftCore.nEntity.nAI.nAttributes.BaseAttributeMap;
	using IAttribute = DotCraftCore.nEntity.nAI.nAttributes.IAttribute;
	using IAttributeInstance = DotCraftCore.nEntity.nAI.nAttributes.IAttributeInstance;
	using ServersideAttributeMap = DotCraftCore.nEntity.nAI.nAttributes.ServersideAttributeMap;
	using EntityItem = DotCraftCore.nEntity.nItem.EntityItem;
	using EntityXPOrb = DotCraftCore.nEntity.nItem.EntityXPOrb;
	using EntityZombie = DotCraftCore.nEntity.nMonster.EntityZombie;
	using EntityWolf = DotCraftCore.nEntity.nPassive.EntityWolf;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using EntityPlayerMP = DotCraftCore.nEntity.nPlayer.EntityPlayerMP;
	using EntityArrow = DotCraftCore.nEntity.nProjectile.EntityArrow;
	using Blocks = DotCraftCore.nInit.Blocks;
	using Item = DotCraftCore.nItem.Item;
	using ItemArmor = DotCraftCore.nItem.ItemArmor;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using NBTBase = DotCraftCore.nNBT.NBTBase;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using NBTTagFloat = DotCraftCore.nNBT.NBTTagFloat;
	using NBTTagList = DotCraftCore.nNBT.NBTTagList;
	using NBTTagShort = DotCraftCore.nNBT.NBTTagShort;
	using S04PacketEntityEquipment = DotCraftCore.nNetwork.nPlay.nServer.S04PacketEntityEquipment;
	using S0BPacketAnimation = DotCraftCore.nNetwork.nPlay.nServer.S0BPacketAnimation;
	using S0DPacketCollectItem = DotCraftCore.nNetwork.nPlay.nServer.S0DPacketCollectItem;
	using Potion = DotCraftCore.nPotion.Potion;
	using PotionEffect = DotCraftCore.nPotion.PotionEffect;
	using PotionHelper = DotCraftCore.nPotion.PotionHelper;
	using Team = DotCraftCore.nScoreboard.Team;
	using AxisAlignedBB = DotCraftCore.nUtil.AxisAlignedBB;
	using CombatTracker = DotCraftCore.nUtil.CombatTracker;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using IIcon = DotCraftCore.nUtil.IIcon;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using MovingObjectPosition = DotCraftCore.nUtil.MovingObjectPosition;
	using Vec3 = DotCraftCore.nUtil.Vec3;
	using World = DotCraftCore.nWorld.World;
	using WorldServer = DotCraftCore.nWorld.WorldServer;

	public abstract class EntityLivingBase : Entity
	{
		private static readonly UUID sprintingSpeedBoostModifierUUID = UUID.fromString("662A6B8D-DA3E-4C1C-8813-96EA6097278D");
		private static readonly AttributeModifier sprintingSpeedBoostModifier = (new AttributeModifier(sprintingSpeedBoostModifierUUID, "Sprinting speed boost", 0.30000001192092896D, 2)).Saved = false;
		private BaseAttributeMap attributeMap;
		private readonly CombatTracker _combatTracker = new CombatTracker(this);
		private readonly Hashtable activePotionsMap = new Hashtable();

	/// <summary> The equipment this mob was previously wearing, used for syncing.  </summary>
		private readonly ItemStack[] previousEquipment = new ItemStack[5];

	/// <summary> Whether an arm swing is currently in progress.  </summary>
		public bool isSwingInProgress;
		public int swingProgressInt;
		public int arrowHitTimer;
		public float prevHealth;

///    
///     <summary> * The amount of time remaining this entity should act 'hurt'. (Visual appearance of red tint) </summary>
///     
		public int hurtTime;

	/// <summary> What the hurt time was max set to last.  </summary>
		public int maxHurtTime;

	/// <summary> The yaw at which this entity was last attacked from.  </summary>
		public float attackedAtYaw;

///    
///     <summary> * The amount of time remaining this entity should act 'dead', i.e. have a corpse in the world. </summary>
///     
		public int deathTime;
		public int attackTime;
		public float prevSwingProgress;
		public float swingProgress;
		public float prevLimbSwingAmount;
		public float limbSwingAmount;

///    
///     <summary> * Only relevant when limbYaw is not 0(the entity is moving). Influences where in its swing legs and arms currently
///     * are. </summary>
///     
		public float limbSwing;
		public int maxHurtResistantTime = 20;
		public float prevCameraPitch;
		public float cameraPitch;
		public float field_70769_ao;
		public float field_70770_ap;
		public float renderYawOffset;
		public float prevRenderYawOffset;

	/// <summary> Entity head rotation yaw  </summary>
		public float rotationYawHead;

	/// <summary> Entity head rotation yaw at previous tick  </summary>
		public float prevRotationYawHead;

///    
///     <summary> * A factor used to determine how far this entity will move each tick if it is jumping or falling. </summary>
///     
		public float jumpMovementFactor = 0.02F;

	/// <summary> The most recent player that has attacked this entity  </summary>
		protected internal EntityPlayer attackingPlayer;

///    
///     <summary> * Set to 60 when hit by the player or the player's wolf, then decrements. Used to determine whether the entity
///     * should drop items on death. </summary>
///     
		protected internal int recentlyHit;

///    
///     <summary> * This gets set on entity death, but never used. Looks like a duplicate of isDead </summary>
///     
		protected internal bool dead;

	/// <summary> The age of this EntityLiving (used to determine when it dies)  </summary>
		protected internal int entityAge;
		protected internal float field_70768_au;
		protected internal float field_110154_aX;
		protected internal float field_70764_aw;
		protected internal float field_70763_ax;
		protected internal float field_70741_aB;

	/// <summary> The score value of the Mob, the amount of points the mob is worth.  </summary>
		protected internal int scoreValue;

///    
///     <summary> * Damage taken in the last hit. Mobs are resistant to damage less than this for a short time after taking damage. </summary>
///     
		protected internal float lastDamage;

	/// <summary> used to check whether entity is jumping.  </summary>
		protected internal bool isJumping;
		public float moveStrafing;
		public float moveForward;
		protected internal float randomYawVelocity;

///    
///     <summary> * The number of updates over which the new position and rotation are to be applied to the entity. </summary>
///     
		protected internal int newPosRotationIncrements;

	/// <summary> The new X position to be applied to the entity.  </summary>
		protected internal double newPosX;

	/// <summary> The new Y position to be applied to the entity.  </summary>
		protected internal double newPosY;
		protected internal double newPosZ;

	/// <summary> The new yaw rotation to be applied to the entity.  </summary>
		protected internal double newRotationYaw;

	/// <summary> The new yaw rotation to be applied to the entity.  </summary>
		protected internal double newRotationPitch;

	/// <summary> Whether the DataWatcher needs to be updated with the active potions  </summary>
		private bool potionsNeedUpdate = true;

	/// <summary> is only being set, has no uses as of MC 1.1  </summary>
		private EntityLivingBase entityLivingToAttack;
		private int revengeTimer;
		private EntityLivingBase lastAttacker;

	/// <summary> Holds the value of ticksExisted when setLastAttacker was last called.  </summary>
		private int lastAttackerTime;

///    
///     <summary> * A factor used to determine how far this entity will move each tick if it is walking on land. Adjusted by speed,
///     * and slipperiness of the current block. </summary>
///     
		private float landMovementFactor;

	/// <summary> Number of ticks since last jump  </summary>
		private int jumpTicks;
		private float field_110151_bq;
		

		public EntityLivingBase(World p_i1594_1_) : base(p_i1594_1_)
		{
			this.applyEntityAttributes();
			this.Health = this.MaxHealth;
			this.preventEntitySpawning = true;
			this.field_70770_ap = (float)(new Random(1).NextDouble() + 1.0D) * 0.01F;
			this.setPosition(this.posX, this.posY, this.posZ);
			this.field_70769_ao = (float)new Random(2).NextDouble() * 12398.0F;
			this.rotationYaw = (float)(new Random(3).NextDouble() * Math.PI * 2.0D);
			this.rotationYawHead = this.rotationYaw;
			this.stepHeight = 0.5F;
		}

		protected internal override void entityInit()
		{
			this.dataWatcher.addObject(7, Convert.ToInt32(0));
			this.dataWatcher.addObject(8, Convert.ToByte((sbyte)0));
			this.dataWatcher.addObject(9, Convert.ToByte((sbyte)0));
			this.dataWatcher.addObject(6, Convert.ToSingle(1.0F));
		}

		protected internal virtual void applyEntityAttributes()
		{
			this.AttributeMap.registerAttribute(SharedMonsterAttributes.maxHealth);
			this.AttributeMap.registerAttribute(SharedMonsterAttributes.knockbackResistance);
			this.AttributeMap.registerAttribute(SharedMonsterAttributes.movementSpeed);

			if (!this.AIEnabled)
			{
				this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.10000000149011612D;
			}
		}

///    
///     <summary> * Takes in the distance the entity has fallen this tick and whether its on the ground to update the fall distance
///     * and deal fall damage if landing on the ground.  Args: distanceFallenThisTick, onGround </summary>
///     
		protected internal override void updateFallState(double p_70064_1_, bool p_70064_3_)
		{
			if (!this.InWater)
			{
				this.handleWaterMovement();
			}

			if (p_70064_3_ && this.fallDistance > 0.0F)
			{
				int var4 = MathHelper.floor_double(this.posX);
				int var5 = MathHelper.floor_double(this.posY - 0.20000000298023224D - (double)this.yOffset);
				int var6 = MathHelper.floor_double(this.posZ);
				Block var7 = this.worldObj.getBlock(var4, var5, var6);

				if (var7.Material == Material.air)
				{
					int var8 = this.worldObj.getBlock(var4, var5 - 1, var6).RenderType;

					if (var8 == 11 || var8 == 32 || var8 == 21)
					{
						var7 = this.worldObj.getBlock(var4, var5 - 1, var6);
					}
				}
				else if (!this.worldObj.isClient && this.fallDistance > 3.0F)
				{
					this.worldObj.playAuxSFX(2006, var4, var5, var6, MathHelper.ceiling_float_int(this.fallDistance - 3.0F));
				}

				var7.onFallenUpon(this.worldObj, var4, var5, var6, this, this.fallDistance);
			}

			base.updateFallState(p_70064_1_, p_70064_3_);
		}

		public virtual bool canBreatheUnderwater()
		{
			return false;
		}

///    
///     <summary> * Gets called every tick from main Entity class </summary>
///     
		public override void onEntityUpdate()
		{
			this.prevSwingProgress = this.swingProgress;
			base.onEntityUpdate();
			this.worldObj.theProfiler.startSection("livingEntityBaseTick");

			if (this.EntityAlive && this.EntityInsideOpaqueBlock)
			{
				this.attackEntityFrom(DamageSource.inWall, 1.0F);
			}

			if (this.ImmuneToFire || this.worldObj.isClient)
			{
				this.extinguish();
			}

			bool var1 = this is EntityPlayer && ((EntityPlayer)this).capabilities.disableDamage;

			if (this.EntityAlive && this.isInsideOfMaterial(Material.water))
			{
				if (!this.canBreatheUnderwater() && !this.isPotionActive(Potion.waterBreathing.id) && !var1)
				{
					this.Air = this.decreaseAirSupply(this.Air);

					if (this.Air == -20)
					{
						this.Air = 0;

						for (int var2 = 0; var2 < 8; ++var2)
						{
							float var3 = this.rand.nextFloat() - this.rand.nextFloat();
							float var4 = this.rand.nextFloat() - this.rand.nextFloat();
							float var5 = this.rand.nextFloat() - this.rand.nextFloat();
							this.worldObj.spawnParticle("bubble", this.posX + (double)var3, this.posY + (double)var4, this.posZ + (double)var5, this.motionX, this.motionY, this.motionZ);
						}

						this.attackEntityFrom(DamageSource.drown, 2.0F);
					}
				}

				if (!this.worldObj.isClient && this.Riding && this.ridingEntity is EntityLivingBase)
				{
					this.mountEntity((Entity)null);
				}
			}
			else
			{
				this.Air = 300;
			}

			if (this.EntityAlive && this.Wet)
			{
				this.extinguish();
			}

			this.prevCameraPitch = this.cameraPitch;

			if (this.attackTime > 0)
			{
				--this.attackTime;
			}

			if (this.hurtTime > 0)
			{
				--this.hurtTime;
			}

			if (this.hurtResistantTime > 0 && !(this is EntityPlayerMP))
			{
				--this.hurtResistantTime;
			}

			if (this.Health <= 0.0F)
			{
				this.onDeathUpdate();
			}

			if (this.recentlyHit > 0)
			{
				--this.recentlyHit;
			}
			else
			{
				this.attackingPlayer = null;
			}

			if (this.lastAttacker != null && !this.lastAttacker.EntityAlive)
			{
				this.lastAttacker = null;
			}

			if (this.entityLivingToAttack != null)
			{
				if (!this.entityLivingToAttack.EntityAlive)
				{
					this.RevengeTarget = (EntityLivingBase)null;
				}
				else if (this.ticksExisted - this.revengeTimer > 100)
				{
					this.RevengeTarget = (EntityLivingBase)null;
				}
			}

			this.updatePotionEffects();
			this.field_70763_ax = this.field_70764_aw;
			this.prevRenderYawOffset = this.renderYawOffset;
			this.prevRotationYawHead = this.rotationYawHead;
			this.prevRotationYaw = this.rotationYaw;
			this.prevRotationPitch = this.rotationPitch;
			this.worldObj.theProfiler.endSection();
		}

///    
///     <summary> * If Animal, checks if the age timer is negative </summary>
///     
		public virtual bool isChild()
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * handles entity death timer, experience orb and particle creation </summary>
///     
		protected internal virtual void onDeathUpdate()
		{
			++this.deathTime;

			if (this.deathTime == 20)
			{
				int var1;

				if (!this.worldObj.isClient && (this.recentlyHit > 0 || this.Player) && this.func_146066_aG() && this.worldObj.GameRules.getGameRuleBooleanValue("doMobLoot"))
				{
					var1 = this.getExperiencePoints(this.attackingPlayer);

					while (var1 > 0)
					{
						int var2 = EntityXPOrb.getXPSplit(var1);
						var1 -= var2;
						this.worldObj.spawnEntityInWorld(new EntityXPOrb(this.worldObj, this.posX, this.posY, this.posZ, var2));
					}
				}

				this.setDead();

				for (var1 = 0; var1 < 20; ++var1)
				{
					double var8 = this.rand.nextGaussian() * 0.02D;
					double var4 = this.rand.nextGaussian() * 0.02D;
					double var6 = this.rand.nextGaussian() * 0.02D;
					this.worldObj.spawnParticle("explode", this.posX + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, this.posY + (double)(this.rand.nextFloat() * this.height), this.posZ + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width, var8, var4, var6);
				}
			}
		}

		protected internal virtual bool func_146066_aG()
		{
			return !this.Child;
		}

///    
///     <summary> * Decrements the entity's air supply when underwater </summary>
///     
		protected internal virtual int decreaseAirSupply(int p_70682_1_)
		{
			int var2 = EnchantmentHelper.getRespiration(this);
			return var2 > 0 && this.rand.Next(var2 + 1) > 0 ? p_70682_1_ : p_70682_1_ - 1;
		}

///    
///     <summary> * Get the experience points the entity currently has. </summary>
///     
		protected internal virtual int getExperiencePoints(EntityPlayer p_70693_1_)
		{
			return 0;
		}

///    
///     <summary> * Only use is to identify if class is an instance of player for experience dropping </summary>
///     
		protected internal virtual bool isPlayer()
		{
			get
			{
				return false;
			}
		}

		public virtual Random RNG
		{
			get
			{
				return this.rand;
			}
		}

		public virtual EntityLivingBase AITarget
		{
			get
			{
				return this.entityLivingToAttack;
			}
		}

		public virtual int func_142015_aE()
		{
			return this.revengeTimer;
		}

		public virtual EntityLivingBase RevengeTarget
		{
			set
			{
				this.entityLivingToAttack = value;
				this.revengeTimer = this.ticksExisted;
			}
		}

		public virtual EntityLivingBase LastAttacker
		{
			get
			{
				return this.lastAttacker;
			}
			set
			{
				if (value is EntityLivingBase)
				{
					this.lastAttacker = (EntityLivingBase)value;
				}
				else
				{
					this.lastAttacker = null;
				}
	
				this.lastAttackerTime = this.ticksExisted;
			}
		}

		public virtual int LastAttackerTime
		{
			get
			{
				return this.lastAttackerTime;
			}
		}


		public virtual int Age
		{
			get
			{
				return this.entityAge;
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			p_70014_1_.setFloat("HealF", this.Health);
			p_70014_1_.setShort("Health", (short)((int)Math.Ceiling((double)this.Health)));
			p_70014_1_.setShort("HurtTime", (short)this.hurtTime);
			p_70014_1_.setShort("DeathTime", (short)this.deathTime);
			p_70014_1_.setShort("AttackTime", (short)this.attackTime);
			p_70014_1_.setFloat("AbsorptionAmount", this.AbsorptionAmount);
			ItemStack[] var2 = this.LastActiveItems;
			int var3 = var2.Length;
			int var4;
			ItemStack var5;

			for (var4 = 0; var4 < var3; ++var4)
			{
				var5 = var2[var4];

				if (var5 != null)
				{
					this.attributeMap.removeAttributeModifiers(var5.AttributeModifiers);
				}
			}

			p_70014_1_.setTag("Attributes", SharedMonsterAttributes.writeBaseAttributeMapToNBT(this.AttributeMap));
			var2 = this.LastActiveItems;
			var3 = var2.Length;

			for (var4 = 0; var4 < var3; ++var4)
			{
				var5 = var2[var4];

				if (var5 != null)
				{
					this.attributeMap.applyAttributeModifiers(var5.AttributeModifiers);
				}
			}

			if (!this.activePotionsMap.Count == 0)
			{
				NBTTagList var6 = new NBTTagList();
				IEnumerator var7 = this.activePotionsMap.Values.GetEnumerator();

				while (var7.MoveNext())
				{
					PotionEffect var8 = (PotionEffect)var7.Current;
					var6.appendTag(var8.writeCustomPotionEffectToNBT(new NBTTagCompound()));
				}

				p_70014_1_.setTag("ActiveEffects", var6);
			}
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			this.AbsorptionAmount = p_70037_1_.getFloat("AbsorptionAmount");

			if (p_70037_1_.func_150297_b("Attributes", 9) && this.worldObj != null && !this.worldObj.isClient)
			{
				SharedMonsterAttributes.func_151475_a(this.AttributeMap, p_70037_1_.getTagList("Attributes", 10));
			}

			if (p_70037_1_.func_150297_b("ActiveEffects", 9))
			{
				NBTTagList var2 = p_70037_1_.getTagList("ActiveEffects", 10);

				for (int var3 = 0; var3 < var2.tagCount(); ++var3)
				{
					NBTTagCompound var4 = var2.getCompoundTagAt(var3);
					PotionEffect var5 = PotionEffect.readCustomPotionEffectFromNBT(var4);

					if (var5 != null)
					{
						this.activePotionsMap.Add(Convert.ToInt32(var5.PotionID), var5);
					}
				}
			}

			if (p_70037_1_.func_150297_b("HealF", 99))
			{
				this.Health = p_70037_1_.getFloat("HealF");
			}
			else
			{
				NBTBase var6 = p_70037_1_.getTag("Health");

				if (var6 == null)
				{
					this.Health = this.MaxHealth;
				}
				else if (var6.Id == 5)
				{
					this.Health = ((NBTTagFloat)var6).func_150288_h();
				}
				else if (var6.Id == 2)
				{
					this.Health = (float)((NBTTagShort)var6).func_150289_e();
				}
			}

			this.hurtTime = p_70037_1_.getShort("HurtTime");
			this.deathTime = p_70037_1_.getShort("DeathTime");
			this.attackTime = p_70037_1_.getShort("AttackTime");
		}

		protected internal virtual void updatePotionEffects()
		{
			IEnumerator var1 = this.activePotionsMap.Keys.GetEnumerator();

			while (var1.MoveNext())
			{
				int? var2 = (int?)var1.Current;
				PotionEffect var3 = (PotionEffect)this.activePotionsMap.get(var2);

				if (!var3.onUpdate(this))
				{
					if (!this.worldObj.isClient)
					{
						var1.remove();
						this.onFinishedPotionEffect(var3);
					}
				}
				else if (var3.Duration % 600 == 0)
				{
					this.onChangedPotionEffect(var3, false);
				}
			}

			int var11;

			if (this.potionsNeedUpdate)
			{
				if (!this.worldObj.isClient)
				{
					if (this.activePotionsMap.Count == 0)
					{
						this.dataWatcher.updateObject(8, Convert.ToByte((sbyte)0));
						this.dataWatcher.updateObject(7, Convert.ToInt32(0));
						this.Invisible = false;
					}
					else
					{
						var11 = PotionHelper.calcPotionLiquidColor(this.activePotionsMap.Values);
						this.dataWatcher.updateObject(8, Convert.ToByte((sbyte)(PotionHelper.func_82817_b(this.activePotionsMap.Values) ? 1 : 0)));
						this.dataWatcher.updateObject(7, Convert.ToInt32(var11));
						this.Invisible = this.isPotionActive(Potion.invisibility.id);
					}
				}

				this.potionsNeedUpdate = false;
			}

			var11 = this.dataWatcher.getWatchableObjectInt(7);
			bool var12 = this.dataWatcher.getWatchableObjectByte(8) > 0;

			if (var11 > 0)
			{
				bool var4 = false;

				if (!this.Invisible)
				{
					var4 = this.rand.nextBoolean();
				}
				else
				{
					var4 = this.rand.Next(15) == 0;
				}

				if (var12)
				{
					var4 &= this.rand.Next(5) == 0;
				}

				if (var4 && var11 > 0)
				{
					double var5 = (double)(var11 >> 16 & 255) / 255.0D;
					double var7 = (double)(var11 >> 8 & 255) / 255.0D;
					double var9 = (double)(var11 >> 0 & 255) / 255.0D;
					this.worldObj.spawnParticle(var12 ? "mobSpellAmbient" : "mobSpell", this.posX + (this.rand.NextDouble() - 0.5D) * (double)this.width, this.posY + this.rand.NextDouble() * (double)this.height - (double)this.yOffset, this.posZ + (this.rand.NextDouble() - 0.5D) * (double)this.width, var5, var7, var9);
				}
			}
		}

		public virtual void clearActivePotions()
		{
			IEnumerator var1 = this.activePotionsMap.Keys.GetEnumerator();

			while (var1.MoveNext())
			{
				int? var2 = (int?)var1.Current;
				PotionEffect var3 = (PotionEffect)this.activePotionsMap.get(var2);

				if (!this.worldObj.isClient)
				{
					var1.remove();
					this.onFinishedPotionEffect(var3);
				}
			}
		}

		public virtual ICollection ActivePotionEffects
		{
			get
			{
				return this.activePotionsMap.Values;
			}
		}

		public virtual bool isPotionActive(int p_82165_1_)
		{
			return this.activePotionsMap.ContainsKey(Convert.ToInt32(p_82165_1_));
		}

		public virtual bool isPotionActive(Potion p_70644_1_)
		{
			return this.activePotionsMap.ContainsKey(Convert.ToInt32(p_70644_1_.id));
		}

///    
///     <summary> * returns the PotionEffect for the supplied Potion if it is active, null otherwise. </summary>
///     
		public virtual PotionEffect getActivePotionEffect(Potion p_70660_1_)
		{
			return (PotionEffect)this.activePotionsMap.get(Convert.ToInt32(p_70660_1_.id));
		}

///    
///     <summary> * adds a PotionEffect to the entity </summary>
///     
		public virtual void addPotionEffect(PotionEffect p_70690_1_)
		{
			if (this.isPotionApplicable(p_70690_1_))
			{
				if (this.activePotionsMap.ContainsKey(Convert.ToInt32(p_70690_1_.PotionID)))
				{
					((PotionEffect)this.activePotionsMap.get(Convert.ToInt32(p_70690_1_.PotionID))).combine(p_70690_1_);
					this.onChangedPotionEffect((PotionEffect)this.activePotionsMap.get(Convert.ToInt32(p_70690_1_.PotionID)), true);
				}
				else
				{
					this.activePotionsMap.Add(Convert.ToInt32(p_70690_1_.PotionID), p_70690_1_);
					this.onNewPotionEffect(p_70690_1_);
				}
			}
		}

		public virtual bool isPotionApplicable(PotionEffect p_70687_1_)
		{
			if (this.CreatureAttribute == EnumCreatureAttribute.UNDEAD)
			{
				int var2 = p_70687_1_.PotionID;

				if (var2 == Potion.regeneration.id || var2 == Potion.poison.id)
				{
					return false;
				}
			}

			return true;
		}

///    
///     <summary> * Returns true if this entity is undead. </summary>
///     
		public virtual bool isEntityUndead()
		{
			get
			{
				return this.CreatureAttribute == EnumCreatureAttribute.UNDEAD;
			}
		}

///    
///     <summary> * Remove the speified potion effect from this entity. </summary>
///     
		public virtual void removePotionEffectClient(int p_70618_1_)
		{
			this.activePotionsMap.Remove(Convert.ToInt32(p_70618_1_));
		}

///    
///     <summary> * Remove the specified potion effect from this entity. </summary>
///     
		public virtual void removePotionEffect(int p_82170_1_)
		{
			PotionEffect var2 = (PotionEffect)this.activePotionsMap.remove(Convert.ToInt32(p_82170_1_));

			if (var2 != null)
			{
				this.onFinishedPotionEffect(var2);
			}
		}

		protected internal virtual void onNewPotionEffect(PotionEffect p_70670_1_)
		{
			this.potionsNeedUpdate = true;

			if (!this.worldObj.isClient)
			{
				Potion.potionTypes[p_70670_1_.PotionID].applyAttributesModifiersToEntity(this, this.AttributeMap, p_70670_1_.Amplifier);
			}
		}

		protected internal virtual void onChangedPotionEffect(PotionEffect p_70695_1_, bool p_70695_2_)
		{
			this.potionsNeedUpdate = true;

			if (p_70695_2_ && !this.worldObj.isClient)
			{
				Potion.potionTypes[p_70695_1_.PotionID].removeAttributesModifiersFromEntity(this, this.AttributeMap, p_70695_1_.Amplifier);
				Potion.potionTypes[p_70695_1_.PotionID].applyAttributesModifiersToEntity(this, this.AttributeMap, p_70695_1_.Amplifier);
			}
		}

		protected internal virtual void onFinishedPotionEffect(PotionEffect p_70688_1_)
		{
			this.potionsNeedUpdate = true;

			if (!this.worldObj.isClient)
			{
				Potion.potionTypes[p_70688_1_.PotionID].removeAttributesModifiersFromEntity(this, this.AttributeMap, p_70688_1_.Amplifier);
			}
		}

///    
///     <summary> * Heal living entity (param: amount of half-hearts) </summary>
///     
		public virtual void heal(float p_70691_1_)
		{
			float var2 = this.Health;

			if (var2 > 0.0F)
			{
				this.Health = var2 + p_70691_1_;
			}
		}

		public float Health
		{
			get
			{
				return this.dataWatcher.getWatchableObjectFloat(6);
			}
			set
			{
				this.dataWatcher.updateObject(6, Convert.ToSingle(MathHelper.clamp_float(value, 0.0F, this.MaxHealth)));
			}
		}


///    
///     <summary> * Called when the entity is attacked. </summary>
///     
		public override bool attackEntityFrom(DamageSource p_70097_1_, float p_70097_2_)
		{
			if (this.EntityInvulnerable)
			{
				return false;
			}
			else if (this.worldObj.isClient)
			{
				return false;
			}
			else
			{
				this.entityAge = 0;

				if (this.Health <= 0.0F)
				{
					return false;
				}
				else if (p_70097_1_.FireDamage && this.isPotionActive(Potion.fireResistance))
				{
					return false;
				}
				else
				{
					if ((p_70097_1_ == DamageSource.anvil || p_70097_1_ == DamageSource.fallingBlock) && this.getEquipmentInSlot(4) != null)
					{
						this.getEquipmentInSlot(4).damageItem((int)(p_70097_2_ * 4.0F + this.rand.nextFloat() * p_70097_2_ * 2.0F), this);
						p_70097_2_ *= 0.75F;
					}

					this.limbSwingAmount = 1.5F;
					bool var3 = true;

					if ((float)this.hurtResistantTime > (float)this.maxHurtResistantTime / 2.0F)
					{
						if (p_70097_2_ <= this.lastDamage)
						{
							return false;
						}

						this.damageEntity(p_70097_1_, p_70097_2_ - this.lastDamage);
						this.lastDamage = p_70097_2_;
						var3 = false;
					}
					else
					{
						this.lastDamage = p_70097_2_;
						this.prevHealth = this.Health;
						this.hurtResistantTime = this.maxHurtResistantTime;
						this.damageEntity(p_70097_1_, p_70097_2_);
						this.hurtTime = this.maxHurtTime = 10;
					}

					this.attackedAtYaw = 0.0F;
					Entity var4 = p_70097_1_.Entity;

					if (var4 != null)
					{
						if (var4 is EntityLivingBase)
						{
							this.RevengeTarget = (EntityLivingBase)var4;
						}

						if (var4 is EntityPlayer)
						{
							this.recentlyHit = 100;
							this.attackingPlayer = (EntityPlayer)var4;
						}
						else if (var4 is EntityWolf)
						{
							EntityWolf var5 = (EntityWolf)var4;

							if (var5.Tamed)
							{
								this.recentlyHit = 100;
								this.attackingPlayer = null;
							}
						}
					}

					if (var3)
					{
						this.worldObj.setEntityState(this, (sbyte)2);

						if (p_70097_1_ != DamageSource.drown)
						{
							this.setBeenAttacked();
						}

						if (var4 != null)
						{
							double var9 = var4.posX - this.posX;
							double var7;

							for (var7 = var4.posZ - this.posZ; var9 * var9 + var7 * var7 < 1.0E-4D; var7 = (new Random(1).NextDouble() - new Random(2).NextDouble()) * 0.01D)
							{
								var9 = (new Random(3).NextDouble() - new Random(4).NextDouble()) * 0.01D;
							}

							this.attackedAtYaw = (float)(Math.Atan2(var7, var9) * 180.0D / Math.PI) - this.rotationYaw;
							this.knockBack(var4, p_70097_2_, var9, var7);
						}
						else
						{
							this.attackedAtYaw = (float)((int)(new Random(5).NextDouble() * 2.0D) * 180);
						}
					}

					string var10;

					if (this.Health <= 0.0F)
					{
						var10 = this.DeathSound;

						if (var3 && var10 != null)
						{
							this.playSound(var10, this.SoundVolume, this.SoundPitch);
						}

						this.onDeath(p_70097_1_);
					}
					else
					{
						var10 = this.HurtSound;

						if (var3 && var10 != null)
						{
							this.playSound(var10, this.SoundVolume, this.SoundPitch);
						}
					}

					return true;
				}
			}
		}

///    
///     <summary> * Renders broken item particles using the given ItemStack </summary>
///     
		public virtual void renderBrokenItemStack(ItemStack p_70669_1_)
		{
			this.playSound("random.break", 0.8F, 0.8F + this.worldObj.rand.nextFloat() * 0.4F);

			for (int var2 = 0; var2 < 5; ++var2)
			{
				Vec3 var3 = Vec3.createVectorHelper(((double)this.rand.nextFloat() - 0.5D) * 0.1D, new Random(1).NextDouble() * 0.1D + 0.1D, 0.0D);
				var3.rotateAroundX(-this.rotationPitch * (float)Math.PI / 180.0F);
				var3.rotateAroundY(-this.rotationYaw * (float)Math.PI / 180.0F);
				Vec3 var4 = Vec3.createVectorHelper(((double)this.rand.nextFloat() - 0.5D) * 0.3D, (double)(-this.rand.nextFloat()) * 0.6D - 0.3D, 0.6D);
				var4.rotateAroundX(-this.rotationPitch * (float)Math.PI / 180.0F);
				var4.rotateAroundY(-this.rotationYaw * (float)Math.PI / 180.0F);
				var4 = var4.addVector(this.posX, this.posY + (double)this.EyeHeight, this.posZ);
				this.worldObj.spawnParticle("iconcrack_" + Item.getIdFromItem(p_70669_1_.Item), var4.xCoord, var4.yCoord, var4.zCoord, var3.xCoord, var3.yCoord + 0.05D, var3.zCoord);
			}
		}

///    
///     <summary> * Called when the mob's health reaches 0. </summary>
///     
		public virtual void onDeath(DamageSource p_70645_1_)
		{
			Entity var2 = p_70645_1_.Entity;
			EntityLivingBase var3 = this.func_94060_bK();

			if (this.scoreValue >= 0 && var3 != null)
			{
				var3.addToPlayerScore(this, this.scoreValue);
			}

			if (var2 != null)
			{
				var2.onKillEntity(this);
			}

			this.dead = true;
			this.func_110142_aN().func_94549_h();

			if (!this.worldObj.isClient)
			{
				int var4 = 0;

				if (var2 is EntityPlayer)
				{
					var4 = EnchantmentHelper.getLootingModifier((EntityLivingBase)var2);
				}

				if (this.func_146066_aG() && this.worldObj.GameRules.getGameRuleBooleanValue("doMobLoot"))
				{
					this.dropFewItems(this.recentlyHit > 0, var4);
					this.dropEquipment(this.recentlyHit > 0, var4);

					if (this.recentlyHit > 0)
					{
						int var5 = this.rand.Next(200) - var4;

						if (var5 < 5)
						{
							this.dropRareDrop(var5 <= 0 ? 1 : 0);
						}
					}
				}
			}

			this.worldObj.setEntityState(this, (sbyte)3);
		}

///    
///     <summary> * Drop the equipment for this entity. </summary>
///     
		protected internal virtual void dropEquipment(bool p_82160_1_, int p_82160_2_)
		{
		}

///    
///     <summary> * knocks back this entity </summary>
///     
		public virtual void knockBack(Entity p_70653_1_, float p_70653_2_, double p_70653_3_, double p_70653_5_)
		{
			if (this.rand.NextDouble() >= this.getEntityAttribute(SharedMonsterAttributes.knockbackResistance).AttributeValue)
			{
				this.isAirBorne = true;
				float var7 = MathHelper.sqrt_double(p_70653_3_ * p_70653_3_ + p_70653_5_ * p_70653_5_);
				float var8 = 0.4F;
				this.motionX /= 2.0D;
				this.motionY /= 2.0D;
				this.motionZ /= 2.0D;
				this.motionX -= p_70653_3_ / (double)var7 * (double)var8;
				this.motionY += (double)var8;
				this.motionZ -= p_70653_5_ / (double)var7 * (double)var8;

				if (this.motionY > 0.4000000059604645D)
				{
					this.motionY = 0.4000000059604645D;
				}
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal virtual string HurtSound
		{
			get
			{
				return "game.neutral.hurt";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal virtual string DeathSound
		{
			get
			{
				return "game.neutral.die";
			}
		}

		protected internal virtual void dropRareDrop(int p_70600_1_)
		{
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal virtual void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
		}

///    
///     <summary> * returns true if this entity is by a ladder, false otherwise </summary>
///     
		public virtual bool isOnLadder()
		{
			get
			{
				int var1 = MathHelper.floor_double(this.posX);
				int var2 = MathHelper.floor_double(this.boundingBox.minY);
				int var3 = MathHelper.floor_double(this.posZ);
				Block var4 = this.worldObj.getBlock(var1, var2, var3);
				return var4 == Blocks.ladder || var4 == Blocks.vine;
			}
		}

///    
///     <summary> * Checks whether target entity is alive. </summary>
///     
		public override bool isEntityAlive()
		{
			get
			{
				return !this.isDead && this.Health > 0.0F;
			}
		}

///    
///     <summary> * Called when the mob is falling. Calculates and applies fall damage. </summary>
///     
		protected internal override void fall(float p_70069_1_)
		{
			base.fall(p_70069_1_);
			PotionEffect var2 = this.getActivePotionEffect(Potion.jump);
			float var3 = var2 != null ? (float)(var2.Amplifier + 1) : 0.0F;
			int var4 = MathHelper.ceiling_float_int(p_70069_1_ - 3.0F - var3);

			if (var4 > 0)
			{
				this.playSound(this.func_146067_o(var4), 1.0F, 1.0F);
				this.attackEntityFrom(DamageSource.fall, (float)var4);
				int var5 = MathHelper.floor_double(this.posX);
				int var6 = MathHelper.floor_double(this.posY - 0.20000000298023224D - (double)this.yOffset);
				int var7 = MathHelper.floor_double(this.posZ);
				Block var8 = this.worldObj.getBlock(var5, var6, var7);

				if (var8.Material != Material.air)
				{
					SoundType var9 = var8.stepSound;
					this.playSound(var9.func_150498_e(), var9.func_150497_c() * 0.5F, var9.func_150494_d() * 0.75F);
				}
			}
		}

		protected internal virtual string func_146067_o(int p_146067_1_)
		{
			return p_146067_1_ > 4 ? "game.neutral.hurt.fall.big" : "game.neutral.hurt.fall.small";
		}

///    
///     <summary> * Setups the entity to do the hurt animation. Only used by packets in multiplayer. </summary>
///     
		public override void performHurtAnimation()
		{
			this.hurtTime = this.maxHurtTime = 10;
			this.attackedAtYaw = 0.0F;
		}

///    
///     <summary> * Returns the current armor value as determined by a call to InventoryPlayer.getTotalArmorValue </summary>
///     
		public virtual int TotalArmorValue
		{
			get
			{
				int var1 = 0;
				ItemStack[] var2 = this.LastActiveItems;
				int var3 = var2.Length;
	
				for (int var4 = 0; var4 < var3; ++var4)
				{
					ItemStack var5 = var2[var4];
	
					if (var5 != null && var5.Item is ItemArmor)
					{
						int var6 = ((ItemArmor)var5.Item).damageReduceAmount;
						var1 += var6;
					}
				}
	
				return var1;
			}
		}

		protected internal virtual void damageArmor(float p_70675_1_)
		{
		}

///    
///     <summary> * Reduces damage, depending on armor </summary>
///     
		protected internal virtual float applyArmorCalculations(DamageSource p_70655_1_, float p_70655_2_)
		{
			if (!p_70655_1_.Unblockable)
			{
				int var3 = 25 - this.TotalArmorValue;
				float var4 = p_70655_2_ * (float)var3;
				this.damageArmor(p_70655_2_);
				p_70655_2_ = var4 / 25.0F;
			}

			return p_70655_2_;
		}

///    
///     <summary> * Reduces damage, depending on potions </summary>
///     
		protected internal virtual float applyPotionDamageCalculations(DamageSource p_70672_1_, float p_70672_2_)
		{
			if (p_70672_1_.DamageAbsolute)
			{
				return p_70672_2_;
			}
			else
			{
				if (this is EntityZombie)
				{
					p_70672_2_ = p_70672_2_;
				}

				int var3;
				int var4;
				float var5;

				if (this.isPotionActive(Potion.resistance) && p_70672_1_ != DamageSource.outOfWorld)
				{
					var3 = (this.getActivePotionEffect(Potion.resistance).Amplifier + 1) * 5;
					var4 = 25 - var3;
					var5 = p_70672_2_ * (float)var4;
					p_70672_2_ = var5 / 25.0F;
				}

				if (p_70672_2_ <= 0.0F)
				{
					return 0.0F;
				}
				else
				{
					var3 = EnchantmentHelper.getEnchantmentModifierDamage(this.LastActiveItems, p_70672_1_);

					if (var3 > 20)
					{
						var3 = 20;
					}

					if (var3 > 0 && var3 <= 20)
					{
						var4 = 25 - var3;
						var5 = p_70672_2_ * (float)var4;
						p_70672_2_ = var5 / 25.0F;
					}

					return p_70672_2_;
				}
			}
		}

///    
///     <summary> * Deals damage to the entity. If its a EntityPlayer then will take damage from the armor first and then health
///     * second with the reduced value. Args: damageAmount </summary>
///     
		protected internal virtual void damageEntity(DamageSource p_70665_1_, float p_70665_2_)
		{
			if (!this.EntityInvulnerable)
			{
				p_70665_2_ = this.applyArmorCalculations(p_70665_1_, p_70665_2_);
				p_70665_2_ = this.applyPotionDamageCalculations(p_70665_1_, p_70665_2_);
				float var3 = p_70665_2_;
				p_70665_2_ = Math.Max(p_70665_2_ - this.AbsorptionAmount, 0.0F);
				this.AbsorptionAmount = this.AbsorptionAmount - (var3 - p_70665_2_);

				if (p_70665_2_ != 0.0F)
				{
					float var4 = this.Health;
					this.Health = var4 - p_70665_2_;
					this.func_110142_aN().func_94547_a(p_70665_1_, var4, p_70665_2_);
					this.AbsorptionAmount = this.AbsorptionAmount - p_70665_2_;
				}
			}
		}

		public virtual CombatTracker func_110142_aN()
		{
			return this._combatTracker;
		}

		public virtual EntityLivingBase func_94060_bK()
		{
			return (EntityLivingBase)(this._combatTracker.func_94550_c() != null ? this._combatTracker.func_94550_c() : (this.attackingPlayer != null ? this.attackingPlayer : (this.entityLivingToAttack != null ? this.entityLivingToAttack : null)));
		}

		public float MaxHealth
		{
			get
			{
				return (float)this.getEntityAttribute(SharedMonsterAttributes.maxHealth).AttributeValue;
			}
		}

///    
///     <summary> * counts the amount of arrows stuck in the entity. getting hit by arrows increases this, used in rendering </summary>
///     
		public int ArrowCountInEntity
		{
			get
			{
				return this.dataWatcher.getWatchableObjectByte(9);
			}
			set
			{
				this.dataWatcher.updateObject(9, Convert.ToByte((sbyte)value));
			}
		}

///    
///     <summary> * sets the amount of arrows stuck in the entity. used for rendering those </summary>
///     

///    
///     <summary> * Returns an integer indicating the end point of the swing animation, used by <seealso cref="#swingProgress"/> to provide a
///     * progress indicator. Takes dig speed enchantments into account. </summary>
///     
		private int ArmSwingAnimationEnd
		{
			get
			{
				return this.isPotionActive(Potion.digSpeed) ? 6 - (1 + this.getActivePotionEffect(Potion.digSpeed).Amplifier) * 1 : (this.isPotionActive(Potion.digSlowdown) ? 6 + (1 + this.getActivePotionEffect(Potion.digSlowdown).Amplifier) * 2 : 6);
			}
		}

///    
///     <summary> * Swings the item the player is holding. </summary>
///     
		public virtual void swingItem()
		{
			if (!this.isSwingInProgress || this.swingProgressInt >= this.ArmSwingAnimationEnd / 2 || this.swingProgressInt < 0)
			{
				this.swingProgressInt = -1;
				this.isSwingInProgress = true;

				if (this.worldObj is WorldServer)
				{
					((WorldServer)this.worldObj).EntityTracker.func_151247_a(this, new S0BPacketAnimation(this, 0));
				}
			}
		}

		public override void handleHealthUpdate(sbyte p_70103_1_)
		{
			if (p_70103_1_ == 2)
			{
				this.limbSwingAmount = 1.5F;
				this.hurtResistantTime = this.maxHurtResistantTime;
				this.hurtTime = this.maxHurtTime = 10;
				this.attackedAtYaw = 0.0F;
				this.playSound(this.HurtSound, this.SoundVolume, (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F);
				this.attackEntityFrom(DamageSource.generic, 0.0F);
			}
			else if (p_70103_1_ == 3)
			{
				this.playSound(this.DeathSound, this.SoundVolume, (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F);
				this.Health = 0.0F;
				this.onDeath(DamageSource.generic);
			}
			else
			{
				base.handleHealthUpdate(p_70103_1_);
			}
		}

///    
///     <summary> * sets the dead flag. Used when you fall off the bottom of the world. </summary>
///     
		protected internal override void kill()
		{
			this.attackEntityFrom(DamageSource.outOfWorld, 4.0F);
		}

///    
///     <summary> * Updates the arm swing progress counters and animation progress </summary>
///     
		protected internal virtual void updateArmSwingProgress()
		{
			int var1 = this.ArmSwingAnimationEnd;

			if (this.isSwingInProgress)
			{
				++this.swingProgressInt;

				if (this.swingProgressInt >= var1)
				{
					this.swingProgressInt = 0;
					this.isSwingInProgress = false;
				}
			}
			else
			{
				this.swingProgressInt = 0;
			}

			this.swingProgress = (float)this.swingProgressInt / (float)var1;
		}

		public virtual IAttributeInstance getEntityAttribute(IAttribute p_110148_1_)
		{
			return this.AttributeMap.getAttributeInstance(p_110148_1_);
		}

		public virtual BaseAttributeMap AttributeMap
		{
			get
			{
				if (this.attributeMap == null)
				{
					this.attributeMap = new ServersideAttributeMap();
				}
	
				return this.attributeMap;
			}
		}

///    
///     <summary> * Get this Entity's EnumCreatureAttribute </summary>
///     
		public virtual EnumCreatureAttribute CreatureAttribute
		{
			get
			{
				return EnumCreatureAttribute.UNDEFINED;
			}
		}

///    
///     <summary> * Returns the item that this EntityLiving is holding, if any. </summary>
///     
		public abstract ItemStack HeldItem {get;}

///    
///     <summary> * 0: Tool in Hand; 1-4: Armor </summary>
///     
		public abstract ItemStack getEquipmentInSlot(int p_71124_1_);

///    
///     <summary> * Sets the held item, or an armor slot. Slot 0 is held item. Slot 1-4 is armor. Params: Item, slot </summary>
///     
		public abstract void setCurrentItemOrArmor(int p_70062_1_, ItemStack p_70062_2_);

///    
///     <summary> * Set sprinting switch for Entity. </summary>
///     
		public override bool Sprinting
		{
			set
			{
				base.Sprinting = value;
				IAttributeInstance var2 = this.getEntityAttribute(SharedMonsterAttributes.movementSpeed);
	
				if (var2.getModifier(sprintingSpeedBoostModifierUUID) != null)
				{
					var2.removeModifier(sprintingSpeedBoostModifier);
				}
	
				if (value)
				{
					var2.applyModifier(sprintingSpeedBoostModifier);
				}
			}
		}

		public abstract ItemStack[] LastActiveItems {get;}

///    
///     <summary> * Returns the volume for the sounds this mob makes. </summary>
///     
		protected internal virtual float SoundVolume
		{
			get
			{
				return 1.0F;
			}
		}

///    
///     <summary> * Gets the pitch of living sounds in living entities. </summary>
///     
		protected internal virtual float SoundPitch
		{
			get
			{
				return this.Child ? (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.5F : (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F;
			}
		}

///    
///     <summary> * Dead and sleeping entities cannot move </summary>
///     
		protected internal virtual bool isMovementBlocked()
		{
			get
			{
				return this.Health <= 0.0F;
			}
		}

///    
///     <summary> * Sets the position of the entity and updates the 'last' variables </summary>
///     
		public virtual void setPositionAndUpdate(double p_70634_1_, double p_70634_3_, double p_70634_5_)
		{
			this.setLocationAndAngles(p_70634_1_, p_70634_3_, p_70634_5_, this.rotationYaw, this.rotationPitch);
		}

///    
///     <summary> * Moves the entity to a position out of the way of its mount. </summary>
///     
		public virtual void dismountEntity(Entity p_110145_1_)
		{
			double var3 = p_110145_1_.posX;
			double var5 = p_110145_1_.boundingBox.minY + (double)p_110145_1_.height;
			double var7 = p_110145_1_.posZ;
			sbyte var9 = 1;

			for (int var10 = -var9; var10 <= var9; ++var10)
			{
				for (int var11 = -var9; var11 < var9; ++var11)
				{
					if (var10 != 0 || var11 != 0)
					{
						int var12 = (int)(this.posX + (double)var10);
						int var13 = (int)(this.posZ + (double)var11);
						AxisAlignedBB var2 = this.boundingBox.getOffsetBoundingBox((double)var10, 1.0D, (double)var11);

						if (this.worldObj.func_147461_a(var2).Empty)
						{
							if (World.doesBlockHaveSolidTopSurface(this.worldObj, var12, (int)this.posY, var13))
							{
								this.setPositionAndUpdate(this.posX + (double)var10, this.posY + 1.0D, this.posZ + (double)var11);
								return;
							}

							if (World.doesBlockHaveSolidTopSurface(this.worldObj, var12, (int)this.posY - 1, var13) || this.worldObj.getBlock(var12, (int)this.posY - 1, var13).Material == Material.water)
							{
								var3 = this.posX + (double)var10;
								var5 = this.posY + 1.0D;
								var7 = this.posZ + (double)var11;
							}
						}
					}
				}
			}

			this.setPositionAndUpdate(var3, var5, var7);
		}

		public virtual bool AlwaysRenderNameTagForRender
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * Gets the Icon Index of the item currently held </summary>
///     
		public virtual IIcon getItemIcon(ItemStack p_70620_1_, int p_70620_2_)
		{
			return p_70620_1_.Item.requiresMultipleRenderPasses() ? p_70620_1_.Item.getIconFromDamageForRenderPass(p_70620_1_.ItemDamage, p_70620_2_) : p_70620_1_.IconIndex;
		}

///    
///     <summary> * Causes this entity to do an upwards motion (jumping). </summary>
///     
		protected internal virtual void jump()
		{
			this.motionY = 0.41999998688697815D;

			if (this.isPotionActive(Potion.jump))
			{
				this.motionY += (double)((float)(this.getActivePotionEffect(Potion.jump).Amplifier + 1) * 0.1F);
			}

			if (this.Sprinting)
			{
				float var1 = this.rotationYaw * 0.017453292F;
				this.motionX -= (double)(MathHelper.sin(var1) * 0.2F);
				this.motionZ += (double)(MathHelper.cos(var1) * 0.2F);
			}

			this.isAirBorne = true;
		}

///    
///     <summary> * Moves the entity based on the specified heading.  Args: strafe, forward </summary>
///     
		public virtual void moveEntityWithHeading(float p_70612_1_, float p_70612_2_)
		{
			double var8;

			if (this.InWater && (!(this is EntityPlayer) || !((EntityPlayer)this).capabilities.isFlying))
			{
				var8 = this.posY;
				this.moveFlying(p_70612_1_, p_70612_2_, this.AIEnabled ? 0.04F : 0.02F);
				this.moveEntity(this.motionX, this.motionY, this.motionZ);
				this.motionX *= 0.800000011920929D;
				this.motionY *= 0.800000011920929D;
				this.motionZ *= 0.800000011920929D;
				this.motionY -= 0.02D;

				if (this.isCollidedHorizontally && this.isOffsetPositionInLiquid(this.motionX, this.motionY + 0.6000000238418579D - this.posY + var8, this.motionZ))
				{
					this.motionY = 0.30000001192092896D;
				}
			}
			else if (this.handleLavaMovement() && (!(this is EntityPlayer) || !((EntityPlayer)this).capabilities.isFlying))
			{
				var8 = this.posY;
				this.moveFlying(p_70612_1_, p_70612_2_, 0.02F);
				this.moveEntity(this.motionX, this.motionY, this.motionZ);
				this.motionX *= 0.5D;
				this.motionY *= 0.5D;
				this.motionZ *= 0.5D;
				this.motionY -= 0.02D;

				if (this.isCollidedHorizontally && this.isOffsetPositionInLiquid(this.motionX, this.motionY + 0.6000000238418579D - this.posY + var8, this.motionZ))
				{
					this.motionY = 0.30000001192092896D;
				}
			}
			else
			{
				float var3 = 0.91F;

				if (this.onGround)
				{
					var3 = this.worldObj.getBlock(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.boundingBox.minY) - 1, MathHelper.floor_double(this.posZ)).slipperiness * 0.91F;
				}

				float var4 = 0.16277136F / (var3 * var3 * var3);
				float var5;

				if (this.onGround)
				{
					var5 = this.AIMoveSpeed * var4;
				}
				else
				{
					var5 = this.jumpMovementFactor;
				}

				this.moveFlying(p_70612_1_, p_70612_2_, var5);
				var3 = 0.91F;

				if (this.onGround)
				{
					var3 = this.worldObj.getBlock(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.boundingBox.minY) - 1, MathHelper.floor_double(this.posZ)).slipperiness * 0.91F;
				}

				if (this.OnLadder)
				{
					float var6 = 0.15F;

					if (this.motionX < (double)(-var6))
					{
						this.motionX = (double)(-var6);
					}

					if (this.motionX > (double)var6)
					{
						this.motionX = (double)var6;
					}

					if (this.motionZ < (double)(-var6))
					{
						this.motionZ = (double)(-var6);
					}

					if (this.motionZ > (double)var6)
					{
						this.motionZ = (double)var6;
					}

					this.fallDistance = 0.0F;

					if (this.motionY < -0.15D)
					{
						this.motionY = -0.15D;
					}

					bool var7 = this.Sneaking && this is EntityPlayer;

					if (var7 && this.motionY < 0.0D)
					{
						this.motionY = 0.0D;
					}
				}

				this.moveEntity(this.motionX, this.motionY, this.motionZ);

				if (this.isCollidedHorizontally && this.OnLadder)
				{
					this.motionY = 0.2D;
				}

				if (this.worldObj.isClient && (!this.worldObj.blockExists((int)this.posX, 0, (int)this.posZ) || !this.worldObj.getChunkFromBlockCoords((int)this.posX, (int)this.posZ).isChunkLoaded))
				{
					if (this.posY > 0.0D)
					{
						this.motionY = -0.1D;
					}
					else
					{
						this.motionY = 0.0D;
					}
				}
				else
				{
					this.motionY -= 0.08D;
				}

				this.motionY *= 0.9800000190734863D;
				this.motionX *= (double)var3;
				this.motionZ *= (double)var3;
			}

			this.prevLimbSwingAmount = this.limbSwingAmount;
			var8 = this.posX - this.prevPosX;
			double var9 = this.posZ - this.prevPosZ;
			float var10 = MathHelper.sqrt_double(var8 * var8 + var9 * var9) * 4.0F;

			if (var10 > 1.0F)
			{
				var10 = 1.0F;
			}

			this.limbSwingAmount += (var10 - this.limbSwingAmount) * 0.4F;
			this.limbSwing += this.limbSwingAmount;
		}

///    
///     <summary> * Returns true if the newer Entity AI code should be run </summary>
///     
		protected internal virtual bool isAIEnabled()
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * the movespeed used for the new AI system </summary>
///     
		public virtual float AIMoveSpeed
		{
			get
			{
				return this.AIEnabled ? this.landMovementFactor : 0.1F;
			}
			set
			{
				this.landMovementFactor = value;
			}
		}

///    
///     <summary> * set the movespeed used for the new AI system </summary>
///     

		public virtual bool attackEntityAsMob(Entity p_70652_1_)
		{
			this.LastAttacker = p_70652_1_;
			return false;
		}

///    
///     <summary> * Returns whether player is sleeping or not </summary>
///     
		public virtual bool isPlayerSleeping()
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			base.onUpdate();

			if (!this.worldObj.isClient)
			{
				int var1 = this.ArrowCountInEntity;

				if (var1 > 0)
				{
					if (this.arrowHitTimer <= 0)
					{
						this.arrowHitTimer = 20 * (30 - var1);
					}

					--this.arrowHitTimer;

					if (this.arrowHitTimer <= 0)
					{
						this.ArrowCountInEntity = var1 - 1;
					}
				}

				for (int var2 = 0; var2 < 5; ++var2)
				{
					ItemStack var3 = this.previousEquipment[var2];
					ItemStack var4 = this.getEquipmentInSlot(var2);

					if (!ItemStack.areItemStacksEqual(var4, var3))
					{
						((WorldServer)this.worldObj).EntityTracker.func_151247_a(this, new S04PacketEntityEquipment(this.EntityId, var2, var4));

						if (var3 != null)
						{
							this.attributeMap.removeAttributeModifiers(var3.AttributeModifiers);
						}

						if (var4 != null)
						{
							this.attributeMap.applyAttributeModifiers(var4.AttributeModifiers);
						}

						this.previousEquipment[var2] = var4 == null ? null : var4.copy();
					}
				}

				if (this.ticksExisted % 20 == 0)
				{
					this.func_110142_aN().func_94549_h();
				}
			}

			this.onLivingUpdate();
			double var9 = this.posX - this.prevPosX;
			double var10 = this.posZ - this.prevPosZ;
			float var5 = (float)(var9 * var9 + var10 * var10);
			float var6 = this.renderYawOffset;
			float var7 = 0.0F;
			this.field_70768_au = this.field_110154_aX;
			float var8 = 0.0F;

			if (var5 > 0.0025000002F)
			{
				var8 = 1.0F;
				var7 = (float)Math.Sqrt((double)var5) * 3.0F;
				var6 = (float)Math.Atan2(var10, var9) * 180.0F / (float)Math.PI - 90.0F;
			}

			if (this.swingProgress > 0.0F)
			{
				var6 = this.rotationYaw;
			}

			if (!this.onGround)
			{
				var8 = 0.0F;
			}

			this.field_110154_aX += (var8 - this.field_110154_aX) * 0.3F;
			this.worldObj.theProfiler.startSection("headTurn");
			var7 = this.func_110146_f(var6, var7);
			this.worldObj.theProfiler.endSection();
			this.worldObj.theProfiler.startSection("rangeChecks");

			while (this.rotationYaw - this.prevRotationYaw < -180.0F)
			{
				this.prevRotationYaw -= 360.0F;
			}

			while (this.rotationYaw - this.prevRotationYaw >= 180.0F)
			{
				this.prevRotationYaw += 360.0F;
			}

			while (this.renderYawOffset - this.prevRenderYawOffset < -180.0F)
			{
				this.prevRenderYawOffset -= 360.0F;
			}

			while (this.renderYawOffset - this.prevRenderYawOffset >= 180.0F)
			{
				this.prevRenderYawOffset += 360.0F;
			}

			while (this.rotationPitch - this.prevRotationPitch < -180.0F)
			{
				this.prevRotationPitch -= 360.0F;
			}

			while (this.rotationPitch - this.prevRotationPitch >= 180.0F)
			{
				this.prevRotationPitch += 360.0F;
			}

			while (this.rotationYawHead - this.prevRotationYawHead < -180.0F)
			{
				this.prevRotationYawHead -= 360.0F;
			}

			while (this.rotationYawHead - this.prevRotationYawHead >= 180.0F)
			{
				this.prevRotationYawHead += 360.0F;
			}

			this.worldObj.theProfiler.endSection();
			this.field_70764_aw += var7;
		}

		protected internal virtual float func_110146_f(float p_110146_1_, float p_110146_2_)
		{
			float var3 = MathHelper.wrapAngleTo180_float(p_110146_1_ - this.renderYawOffset);
			this.renderYawOffset += var3 * 0.3F;
			float var4 = MathHelper.wrapAngleTo180_float(this.rotationYaw - this.renderYawOffset);
			bool var5 = var4 < -90.0F || var4 >= 90.0F;

			if (var4 < -75.0F)
			{
				var4 = -75.0F;
			}

			if (var4 >= 75.0F)
			{
				var4 = 75.0F;
			}

			this.renderYawOffset = this.rotationYaw - var4;

			if (var4 * var4 > 2500.0F)
			{
				this.renderYawOffset += var4 * 0.2F;
			}

			if (var5)
			{
				p_110146_2_ *= -1.0F;
			}

			return p_110146_2_;
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public virtual void onLivingUpdate()
		{
			if (this.jumpTicks > 0)
			{
				--this.jumpTicks;
			}

			if (this.newPosRotationIncrements > 0)
			{
				double var1 = this.posX + (this.newPosX - this.posX) / (double)this.newPosRotationIncrements;
				double var3 = this.posY + (this.newPosY - this.posY) / (double)this.newPosRotationIncrements;
				double var5 = this.posZ + (this.newPosZ - this.posZ) / (double)this.newPosRotationIncrements;
				double var7 = MathHelper.wrapAngleTo180_double(this.newRotationYaw - (double)this.rotationYaw);
				this.rotationYaw = (float)((double)this.rotationYaw + var7 / (double)this.newPosRotationIncrements);
				this.rotationPitch = (float)((double)this.rotationPitch + (this.newRotationPitch - (double)this.rotationPitch) / (double)this.newPosRotationIncrements);
				--this.newPosRotationIncrements;
				this.setPosition(var1, var3, var5);
				this.setRotation(this.rotationYaw, this.rotationPitch);
			}
			else if (!this.ClientWorld)
			{
				this.motionX *= 0.98D;
				this.motionY *= 0.98D;
				this.motionZ *= 0.98D;
			}

			if (Math.Abs(this.motionX) < 0.005D)
			{
				this.motionX = 0.0D;
			}

			if (Math.Abs(this.motionY) < 0.005D)
			{
				this.motionY = 0.0D;
			}

			if (Math.Abs(this.motionZ) < 0.005D)
			{
				this.motionZ = 0.0D;
			}

			this.worldObj.theProfiler.startSection("ai");

			if (this.MovementBlocked)
			{
				this.isJumping = false;
				this.moveStrafing = 0.0F;
				this.moveForward = 0.0F;
				this.randomYawVelocity = 0.0F;
			}
			else if (this.ClientWorld)
			{
				if (this.AIEnabled)
				{
					this.worldObj.theProfiler.startSection("newAi");
					this.updateAITasks();
					this.worldObj.theProfiler.endSection();
				}
				else
				{
					this.worldObj.theProfiler.startSection("oldAi");
					this.updateEntityActionState();
					this.worldObj.theProfiler.endSection();
					this.rotationYawHead = this.rotationYaw;
				}
			}

			this.worldObj.theProfiler.endSection();
			this.worldObj.theProfiler.startSection("jump");

			if (this.isJumping)
			{
				if (!this.InWater && !this.handleLavaMovement())
				{
					if (this.onGround && this.jumpTicks == 0)
					{
						this.jump();
						this.jumpTicks = 10;
					}
				}
				else
				{
					this.motionY += 0.03999999910593033D;
				}
			}
			else
			{
				this.jumpTicks = 0;
			}

			this.worldObj.theProfiler.endSection();
			this.worldObj.theProfiler.startSection("travel");
			this.moveStrafing *= 0.98F;
			this.moveForward *= 0.98F;
			this.randomYawVelocity *= 0.9F;
			this.moveEntityWithHeading(this.moveStrafing, this.moveForward);
			this.worldObj.theProfiler.endSection();
			this.worldObj.theProfiler.startSection("push");

			if (!this.worldObj.isClient)
			{
				this.collideWithNearbyEntities();
			}

			this.worldObj.theProfiler.endSection();
		}

		protected internal virtual void updateAITasks()
		{
		}

		protected internal virtual void collideWithNearbyEntities()
		{
			IList var1 = this.worldObj.getEntitiesWithinAABBExcludingEntity(this, this.boundingBox.expand(0.20000000298023224D, 0.0D, 0.20000000298023224D));

			if (var1 != null && !var1.Count == 0)
			{
				for (int var2 = 0; var2 < var1.Count; ++var2)
				{
					Entity var3 = (Entity)var1[var2];

					if (var3.canBePushed())
					{
						this.collideWithEntity(var3);
					}
				}
			}
		}

		protected internal virtual void collideWithEntity(Entity p_82167_1_)
		{
			p_82167_1_.applyEntityCollision(this);
		}

///    
///     <summary> * Handles updating while being ridden by an entity </summary>
///     
		public override void updateRidden()
		{
			base.updateRidden();
			this.field_70768_au = this.field_110154_aX;
			this.field_110154_aX = 0.0F;
			this.fallDistance = 0.0F;
		}

///    
///     <summary> * Sets the position and rotation. Only difference from the other one is no bounding on the rotation. Args: posX,
///     * posY, posZ, yaw, pitch </summary>
///     
		public override void setPositionAndRotation2(double p_70056_1_, double p_70056_3_, double p_70056_5_, float p_70056_7_, float p_70056_8_, int p_70056_9_)
		{
			this.yOffset = 0.0F;
			this.newPosX = p_70056_1_;
			this.newPosY = p_70056_3_;
			this.newPosZ = p_70056_5_;
			this.newRotationYaw = (double)p_70056_7_;
			this.newRotationPitch = (double)p_70056_8_;
			this.newPosRotationIncrements = p_70056_9_;
		}

///    
///     <summary> * main AI tick function, replaces updateEntityActionState </summary>
///     
		protected internal virtual void updateAITick()
		{
		}

		protected internal virtual void updateEntityActionState()
		{
			++this.entityAge;
		}

		public virtual bool Jumping
		{
			set
			{
				this.isJumping = value;
			}
		}

///    
///     <summary> * Called whenever an item is picked up from walking over it. Args: pickedUpEntity, stackSize </summary>
///     
		public virtual void onItemPickup(Entity p_71001_1_, int p_71001_2_)
		{
			if (!p_71001_1_.isDead && !this.worldObj.isClient)
			{
				EntityTracker var3 = ((WorldServer)this.worldObj).EntityTracker;

				if (p_71001_1_ is EntityItem)
				{
					var3.func_151247_a(p_71001_1_, new S0DPacketCollectItem(p_71001_1_.EntityId, this.EntityId));
				}

				if (p_71001_1_ is EntityArrow)
				{
					var3.func_151247_a(p_71001_1_, new S0DPacketCollectItem(p_71001_1_.EntityId, this.EntityId));
				}

				if (p_71001_1_ is EntityXPOrb)
				{
					var3.func_151247_a(p_71001_1_, new S0DPacketCollectItem(p_71001_1_.EntityId, this.EntityId));
				}
			}
		}

///    
///     <summary> * returns true if the entity provided in the argument can be seen. (Raytrace) </summary>
///     
		public virtual bool canEntityBeSeen(Entity p_70685_1_)
		{
			return this.worldObj.rayTraceBlocks(Vec3.createVectorHelper(this.posX, this.posY + (double)this.EyeHeight, this.posZ), Vec3.createVectorHelper(p_70685_1_.posX, p_70685_1_.posY + (double)p_70685_1_.EyeHeight, p_70685_1_.posZ)) == null;
		}

///    
///     <summary> * returns a (normalized) vector of where this entity is looking </summary>
///     
		public override Vec3 LookVec
		{
			get
			{
				return this.getLook(1.0F);
			}
		}

///    
///     <summary> * interpolated look vector </summary>
///     
		public virtual Vec3 getLook(float p_70676_1_)
		{
			float var2;
			float var3;
			float var4;
			float var5;

			if (p_70676_1_ == 1.0F)
			{
				var2 = MathHelper.cos(-this.rotationYaw * 0.017453292F - (float)Math.PI);
				var3 = MathHelper.sin(-this.rotationYaw * 0.017453292F - (float)Math.PI);
				var4 = -MathHelper.cos(-this.rotationPitch * 0.017453292F);
				var5 = MathHelper.sin(-this.rotationPitch * 0.017453292F);
				return Vec3.createVectorHelper((double)(var3 * var4), (double)var5, (double)(var2 * var4));
			}
			else
			{
				var2 = this.prevRotationPitch + (this.rotationPitch - this.prevRotationPitch) * p_70676_1_;
				var3 = this.prevRotationYaw + (this.rotationYaw - this.prevRotationYaw) * p_70676_1_;
				var4 = MathHelper.cos(-var3 * 0.017453292F - (float)Math.PI);
				var5 = MathHelper.sin(-var3 * 0.017453292F - (float)Math.PI);
				float var6 = -MathHelper.cos(-var2 * 0.017453292F);
				float var7 = MathHelper.sin(-var2 * 0.017453292F);
				return Vec3.createVectorHelper((double)(var5 * var6), (double)var7, (double)(var4 * var6));
			}
		}

///    
///     <summary> * Returns where in the swing animation the living entity is (from 0 to 1).  Args: partialTickTime </summary>
///     
		public virtual float getSwingProgress(float p_70678_1_)
		{
			float var2 = this.swingProgress - this.prevSwingProgress;

			if (var2 < 0.0F)
			{
				++var2;
			}

			return this.prevSwingProgress + var2 * p_70678_1_;
		}

///    
///     <summary> * interpolated position vector </summary>
///     
		public virtual Vec3 getPosition(float p_70666_1_)
		{
			if (p_70666_1_ == 1.0F)
			{
				return Vec3.createVectorHelper(this.posX, this.posY, this.posZ);
			}
			else
			{
				double var2 = this.prevPosX + (this.posX - this.prevPosX) * (double)p_70666_1_;
				double var4 = this.prevPosY + (this.posY - this.prevPosY) * (double)p_70666_1_;
				double var6 = this.prevPosZ + (this.posZ - this.prevPosZ) * (double)p_70666_1_;
				return Vec3.createVectorHelper(var2, var4, var6);
			}
		}

///    
///     <summary> * Performs a ray trace for the distance specified and using the partial tick time. Args: distance, partialTickTime </summary>
///     
		public virtual MovingObjectPosition rayTrace(double p_70614_1_, float p_70614_3_)
		{
			Vec3 var4 = this.getPosition(p_70614_3_);
			Vec3 var5 = this.getLook(p_70614_3_);
			Vec3 var6 = var4.addVector(var5.xCoord * p_70614_1_, var5.yCoord * p_70614_1_, var5.zCoord * p_70614_1_);
			return this.worldObj.func_147447_a(var4, var6, false, false, true);
		}

///    
///     <summary> * Returns whether the entity is in a local (client) world </summary>
///     
		public virtual bool isClientWorld()
		{
			get
			{
				return !this.worldObj.isClient;
			}
		}

///    
///     <summary> * Returns true if other Entities should be prevented from moving through this Entity. </summary>
///     
		public override bool canBeCollidedWith()
		{
			return !this.isDead;
		}

///    
///     <summary> * Returns true if this entity should push and be pushed by other entities when colliding. </summary>
///     
		public override bool canBePushed()
		{
			return !this.isDead;
		}

		public override float EyeHeight
		{
			get
			{
				return this.height * 0.85F;
			}
		}

///    
///     <summary> * Sets that this entity has been attacked. </summary>
///     
		protected internal override void setBeenAttacked()
		{
			this.velocityChanged = this.rand.NextDouble() >= this.getEntityAttribute(SharedMonsterAttributes.knockbackResistance).AttributeValue;
		}

		public override float RotationYawHead
		{
			get
			{
				return this.rotationYawHead;
			}
			set
			{
				this.rotationYawHead = value;
			}
		}

///    
///     <summary> * Sets the head's yaw rotation of the entity. </summary>
///     

		public virtual float AbsorptionAmount
		{
			get
			{
				return this.field_110151_bq;
			}
			set
			{
				if (value < 0.0F)
				{
					value = 0.0F;
				}
	
				this.field_110151_bq = value;
			}
		}


		public virtual Team Team
		{
			get
			{
				return null;
			}
		}

		public virtual bool isOnSameTeam(EntityLivingBase p_142014_1_)
		{
			return this.isOnTeam(p_142014_1_.Team);
		}

///    
///     <summary> * Returns true if the entity is on a specific team. </summary>
///     
		public virtual bool isOnTeam(Team p_142012_1_)
		{
			return this.Team != null ? this.Team.isSameTeam(p_142012_1_) : false;
		}

		public virtual void func_152111_bt()
		{
		}

		public virtual void func_152112_bu()
		{
		}
	}

}