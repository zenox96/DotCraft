using System;
using System.Collections;

namespace DotCraftCore.Entity
{

	using EnchantmentHelper = DotCraftCore.Enchantment.EnchantmentHelper;
	using EntityAITasks = DotCraftCore.Entity.AI.EntityAITasks;
	using EntityJumpHelper = DotCraftCore.Entity.AI.EntityJumpHelper;
	using EntityLookHelper = DotCraftCore.Entity.AI.EntityLookHelper;
	using EntityMoveHelper = DotCraftCore.Entity.AI.EntityMoveHelper;
	using EntitySenses = DotCraftCore.Entity.AI.EntitySenses;
	using AttributeModifier = DotCraftCore.Entity.AI.Attributes.AttributeModifier;
	using EntityItem = DotCraftCore.Entity.Item.EntityItem;
	using EntityCreeper = DotCraftCore.Entity.Monster.EntityCreeper;
	using EntityGhast = DotCraftCore.Entity.Monster.EntityGhast;
	using IMob = DotCraftCore.Entity.Monster.IMob;
	using EntityTameable = DotCraftCore.Entity.Passive.EntityTameable;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemArmor = DotCraftCore.Item.ItemArmor;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using ItemSword = DotCraftCore.Item.ItemSword;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using NBTTagFloat = DotCraftCore.NBT.NBTTagFloat;
	using NBTTagList = DotCraftCore.NBT.NBTTagList;
	using S1BPacketEntityAttach = DotCraftCore.Network.Play.Server.S1BPacketEntityAttach;
	using PathNavigate = DotCraftCore.Pathfinding.PathNavigate;
	using AchievementList = DotCraftCore.Stats.AchievementList;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using EnumDifficulty = DotCraftCore.World.EnumDifficulty;
	using World = DotCraftCore.World.World;
	using WorldServer = DotCraftCore.World.WorldServer;

	public abstract class EntityLiving : EntityLivingBase
	{
	/// <summary> Number of ticks since this EntityLiving last produced its sound  </summary>
		public int livingSoundTime;

	/// <summary> The experience points the Entity gives.  </summary>
		protected internal int experienceValue;
		private EntityLookHelper lookHelper;
		private EntityMoveHelper moveHelper;

	/// <summary> Entity jumping helper  </summary>
		private EntityJumpHelper jumpHelper;
		private EntityBodyHelper bodyHelper;
		private PathNavigate navigator;
		protected internal readonly EntityAITasks tasks;
		protected internal readonly EntityAITasks targetTasks;

	/// <summary> The active target the Task system uses for tracking  </summary>
		private EntityLivingBase attackTarget;
		private EntitySenses senses;

	/// <summary> Equipment (armor and held item) for this entity.  </summary>
		private ItemStack[] equipment = new ItemStack[5];

	/// <summary> Chances for each equipment piece from dropping when this entity dies.  </summary>
		protected internal float[] equipmentDropChances = new float[5];

	/// <summary> Whether this entity can pick up items from the ground.  </summary>
		private bool canPickUpLoot;

	/// <summary> Whether this entity should NOT despawn.  </summary>
		private bool persistenceRequired;
		protected internal float defaultPitch;

	/// <summary> This entity's current target.  </summary>
		private Entity currentTarget;

	/// <summary> How long to keep a specific target entity  </summary>
		protected internal int numTicksToChaseTarget;
		private bool isLeashed;
		private Entity leashedToEntity;
		private NBTTagCompound field_110170_bx;
		private const string __OBFID = "CL_00001550";

		public EntityLiving(World p_i1595_1_) : base(p_i1595_1_)
		{
			this.tasks = new EntityAITasks(p_i1595_1_ != null && p_i1595_1_.theProfiler != null ? p_i1595_1_.theProfiler : null);
			this.targetTasks = new EntityAITasks(p_i1595_1_ != null && p_i1595_1_.theProfiler != null ? p_i1595_1_.theProfiler : null);
			this.lookHelper = new EntityLookHelper(this);
			this.moveHelper = new EntityMoveHelper(this);
			this.jumpHelper = new EntityJumpHelper(this);
			this.bodyHelper = new EntityBodyHelper(this);
			this.navigator = new PathNavigate(this, p_i1595_1_);
			this.senses = new EntitySenses(this);

			for (int var2 = 0; var2 < this.equipmentDropChances.Length; ++var2)
			{
				this.equipmentDropChances[var2] = 0.085F;
			}
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.AttributeMap.registerAttribute(SharedMonsterAttributes.followRange).BaseValue = 16.0D;
		}

		public virtual EntityLookHelper LookHelper
		{
			get
			{
				return this.lookHelper;
			}
		}

		public virtual EntityMoveHelper MoveHelper
		{
			get
			{
				return this.moveHelper;
			}
		}

		public virtual EntityJumpHelper JumpHelper
		{
			get
			{
				return this.jumpHelper;
			}
		}

		public virtual PathNavigate Navigator
		{
			get
			{
				return this.navigator;
			}
		}

///    
///     <summary> * returns the EntitySenses Object for the EntityLiving </summary>
///     
		public virtual EntitySenses EntitySenses
		{
			get
			{
				return this.senses;
			}
		}

///    
///     <summary> * Gets the active target the Task system uses for tracking </summary>
///     
		public virtual EntityLivingBase AttackTarget
		{
			get
			{
				return this.attackTarget;
			}
			set
			{
				this.attackTarget = value;
			}
		}

///    
///     <summary> * Sets the active target the Task system uses for tracking </summary>
///     

///    
///     <summary> * Returns true if this entity can attack entities of the specified class. </summary>
///     
		public virtual bool canAttackClass(Type p_70686_1_)
		{
			return typeof(EntityCreeper) != p_70686_1_ && typeof(EntityGhast) != p_70686_1_;
		}

///    
///     <summary> * This function applies the benefits of growing back wool and faster growing up to the acting entity. (This
///     * function is used in the AIEatGrass) </summary>
///     
		public virtual void eatGrassBonus()
		{
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(11, Convert.ToByte((sbyte)0));
			this.dataWatcher.addObject(10, "");
		}

///    
///     <summary> * Get number of ticks, at least during which the living entity will be silent. </summary>
///     
		public virtual int TalkInterval
		{
			get
			{
				return 80;
			}
		}

///    
///     <summary> * Plays living's sound at its position </summary>
///     
		public virtual void playLivingSound()
		{
			string var1 = this.LivingSound;

			if (var1 != null)
			{
				this.playSound(var1, this.SoundVolume, this.SoundPitch);
			}
		}

///    
///     <summary> * Gets called every tick from main Entity class </summary>
///     
		public override void onEntityUpdate()
		{
			base.onEntityUpdate();
			this.worldObj.theProfiler.startSection("mobBaseTick");

			if (this.EntityAlive && this.rand.Next(1000) < this.livingSoundTime++)
			{
				this.livingSoundTime = -this.TalkInterval;
				this.playLivingSound();
			}

			this.worldObj.theProfiler.endSection();
		}

///    
///     <summary> * Get the experience points the entity currently has. </summary>
///     
		protected internal override int getExperiencePoints(EntityPlayer p_70693_1_)
		{
			if (this.experienceValue > 0)
			{
				int var2 = this.experienceValue;
				ItemStack[] var3 = this.LastActiveItems;

				for (int var4 = 0; var4 < var3.Length; ++var4)
				{
					if (var3[var4] != null && this.equipmentDropChances[var4] <= 1.0F)
					{
						var2 += 1 + this.rand.Next(3);
					}
				}

				return var2;
			}
			else
			{
				return this.experienceValue;
			}
		}

///    
///     <summary> * Spawns an explosion particle around the Entity's location </summary>
///     
		public virtual void spawnExplosionParticle()
		{
			for (int var1 = 0; var1 < 20; ++var1)
			{
				double var2 = this.rand.nextGaussian() * 0.02D;
				double var4 = this.rand.nextGaussian() * 0.02D;
				double var6 = this.rand.nextGaussian() * 0.02D;
				double var8 = 10.0D;
				this.worldObj.spawnParticle("explode", this.posX + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width - var2 * var8, this.posY + (double)(this.rand.nextFloat() * this.height) - var4 * var8, this.posZ + (double)(this.rand.nextFloat() * this.width * 2.0F) - (double)this.width - var6 * var8, var2, var4, var6);
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
				this.updateLeashedState();
			}
		}

		protected internal override float func_110146_f(float p_110146_1_, float p_110146_2_)
		{
			if (this.AIEnabled)
			{
				this.bodyHelper.func_75664_a();
				return p_110146_2_;
			}
			else
			{
				return base.func_110146_f(p_110146_1_, p_110146_2_);
			}
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal virtual string LivingSound
		{
			get
			{
				return null;
			}
		}

		protected internal virtual Item func_146068_u()
		{
			return Item.getItemById(0);
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			Item var3 = this.func_146068_u();

			if (var3 != null)
			{
				int var4 = this.rand.Next(3);

				if (p_70628_2_ > 0)
				{
					var4 += this.rand.Next(p_70628_2_ + 1);
				}

				for (int var5 = 0; var5 < var4; ++var5)
				{
					this.func_145779_a(var3, 1);
				}
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setBoolean("CanPickUpLoot", this.canPickUpLoot());
			p_70014_1_.setBoolean("PersistenceRequired", this.persistenceRequired);
			NBTTagList var2 = new NBTTagList();
			NBTTagCompound var4;

			for (int var3 = 0; var3 < this.equipment.Length; ++var3)
			{
				var4 = new NBTTagCompound();

				if (this.equipment[var3] != null)
				{
					this.equipment[var3].writeToNBT(var4);
				}

				var2.appendTag(var4);
			}

			p_70014_1_.setTag("Equipment", var2);
			NBTTagList var6 = new NBTTagList();

			for (int var7 = 0; var7 < this.equipmentDropChances.Length; ++var7)
			{
				var6.appendTag(new NBTTagFloat(this.equipmentDropChances[var7]));
			}

			p_70014_1_.setTag("DropChances", var6);
			p_70014_1_.setString("CustomName", this.CustomNameTag);
			p_70014_1_.setBoolean("CustomNameVisible", this.AlwaysRenderNameTag);
			p_70014_1_.setBoolean("Leashed", this.isLeashed);

			if (this.leashedToEntity != null)
			{
				var4 = new NBTTagCompound();

				if (this.leashedToEntity is EntityLivingBase)
				{
					var4.setLong("UUIDMost", this.leashedToEntity.UniqueID.MostSignificantBits);
					var4.setLong("UUIDLeast", this.leashedToEntity.UniqueID.LeastSignificantBits);
				}
				else if (this.leashedToEntity is EntityHanging)
				{
					EntityHanging var5 = (EntityHanging)this.leashedToEntity;
					var4.setInteger("X", var5.field_146063_b);
					var4.setInteger("Y", var5.field_146064_c);
					var4.setInteger("Z", var5.field_146062_d);
				}

				p_70014_1_.setTag("Leash", var4);
			}
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.CanPickUpLoot = p_70037_1_.getBoolean("CanPickUpLoot");
			this.persistenceRequired = p_70037_1_.getBoolean("PersistenceRequired");

			if (p_70037_1_.func_150297_b("CustomName", 8) && p_70037_1_.getString("CustomName").Length > 0)
			{
				this.CustomNameTag = p_70037_1_.getString("CustomName");
			}

			this.AlwaysRenderNameTag = p_70037_1_.getBoolean("CustomNameVisible");
			NBTTagList var2;
			int var3;

			if (p_70037_1_.func_150297_b("Equipment", 9))
			{
				var2 = p_70037_1_.getTagList("Equipment", 10);

				for (var3 = 0; var3 < this.equipment.Length; ++var3)
				{
					this.equipment[var3] = ItemStack.loadItemStackFromNBT(var2.getCompoundTagAt(var3));
				}
			}

			if (p_70037_1_.func_150297_b("DropChances", 9))
			{
				var2 = p_70037_1_.getTagList("DropChances", 5);

				for (var3 = 0; var3 < var2.tagCount(); ++var3)
				{
					this.equipmentDropChances[var3] = var2.func_150308_e(var3);
				}
			}

			this.isLeashed = p_70037_1_.getBoolean("Leashed");

			if (this.isLeashed && p_70037_1_.func_150297_b("Leash", 10))
			{
				this.field_110170_bx = p_70037_1_.getCompoundTag("Leash");
			}
		}

		public virtual float MoveForward
		{
			set
			{
				this.moveForward = value;
			}
		}

///    
///     <summary> * set the movespeed used for the new AI system </summary>
///     
		public override float AIMoveSpeed
		{
			set
			{
				base.AIMoveSpeed = value;
				this.MoveForward = value;
			}
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public override void onLivingUpdate()
		{
			base.onLivingUpdate();
			this.worldObj.theProfiler.startSection("looting");

			if (!this.worldObj.isClient && this.canPickUpLoot() && !this.dead && this.worldObj.GameRules.getGameRuleBooleanValue("mobGriefing"))
			{
				IList var1 = this.worldObj.getEntitiesWithinAABB(typeof(EntityItem), this.boundingBox.expand(1.0D, 0.0D, 1.0D));
				IEnumerator var2 = var1.GetEnumerator();

				while (var2.MoveNext())
				{
					EntityItem var3 = (EntityItem)var2.Current;

					if (!var3.isDead && var3.EntityItem != null)
					{
						ItemStack var4 = var3.EntityItem;
						int var5 = getArmorPosition(var4);

						if (var5 > -1)
						{
							bool var6 = true;
							ItemStack var7 = this.getEquipmentInSlot(var5);

							if (var7 != null)
							{
								if (var5 == 0)
								{
									if (var4.Item is ItemSword && !(var7.Item is ItemSword))
									{
										var6 = true;
									}
									else if (var4.Item is ItemSword && var7.Item is ItemSword)
									{
										ItemSword var8 = (ItemSword)var4.Item;
										ItemSword var9 = (ItemSword)var7.Item;

										if (var8.func_150931_i() == var9.func_150931_i())
										{
											var6 = var4.ItemDamage > var7.ItemDamage || var4.hasTagCompound() && !var7.hasTagCompound();
										}
										else
										{
											var6 = var8.func_150931_i() > var9.func_150931_i();
										}
									}
									else
									{
										var6 = false;
									}
								}
								else if (var4.Item is ItemArmor && !(var7.Item is ItemArmor))
								{
									var6 = true;
								}
								else if (var4.Item is ItemArmor && var7.Item is ItemArmor)
								{
									ItemArmor var10 = (ItemArmor)var4.Item;
									ItemArmor var12 = (ItemArmor)var7.Item;

									if (var10.damageReduceAmount == var12.damageReduceAmount)
									{
										var6 = var4.ItemDamage > var7.ItemDamage || var4.hasTagCompound() && !var7.hasTagCompound();
									}
									else
									{
										var6 = var10.damageReduceAmount > var12.damageReduceAmount;
									}
								}
								else
								{
									var6 = false;
								}
							}

							if (var6)
							{
								if (var7 != null && this.rand.nextFloat() - 0.1F < this.equipmentDropChances[var5])
								{
									this.entityDropItem(var7, 0.0F);
								}

								if (var4.Item == Items.diamond && var3.func_145800_j() != null)
								{
									EntityPlayer var11 = this.worldObj.getPlayerEntityByName(var3.func_145800_j());

									if (var11 != null)
									{
										var11.triggerAchievement(AchievementList.field_150966_x);
									}
								}

								this.setCurrentItemOrArmor(var5, var4);
								this.equipmentDropChances[var5] = 2.0F;
								this.persistenceRequired = true;
								this.onItemPickup(var3, 1);
								var3.setDead();
							}
						}
					}
				}
			}

			this.worldObj.theProfiler.endSection();
		}

///    
///     <summary> * Returns true if the newer Entity AI code should be run </summary>
///     
		protected internal override bool isAIEnabled()
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * Determines if an entity can be despawned, used on idle far away entities </summary>
///     
		protected internal virtual bool canDespawn()
		{
			return true;
		}

///    
///     <summary> * Makes the entity despawn if requirements are reached </summary>
///     
		protected internal virtual void despawnEntity()
		{
			if (this.persistenceRequired)
			{
				this.entityAge = 0;
			}
			else
			{
				EntityPlayer var1 = this.worldObj.getClosestPlayerToEntity(this, -1.0D);

				if (var1 != null)
				{
					double var2 = var1.posX - this.posX;
					double var4 = var1.posY - this.posY;
					double var6 = var1.posZ - this.posZ;
					double var8 = var2 * var2 + var4 * var4 + var6 * var6;

					if (this.canDespawn() && var8 > 16384.0D)
					{
						this.setDead();
					}

					if (this.entityAge > 600 && this.rand.Next(800) == 0 && var8 > 1024.0D && this.canDespawn())
					{
						this.setDead();
					}
					else if (var8 < 1024.0D)
					{
						this.entityAge = 0;
					}
				}
			}
		}

		protected internal override void updateAITasks()
		{
			++this.entityAge;
			this.worldObj.theProfiler.startSection("checkDespawn");
			this.despawnEntity();
			this.worldObj.theProfiler.endSection();
			this.worldObj.theProfiler.startSection("sensing");
			this.senses.clearSensingCache();
			this.worldObj.theProfiler.endSection();
			this.worldObj.theProfiler.startSection("targetSelector");
			this.targetTasks.onUpdateTasks();
			this.worldObj.theProfiler.endSection();
			this.worldObj.theProfiler.startSection("goalSelector");
			this.tasks.onUpdateTasks();
			this.worldObj.theProfiler.endSection();
			this.worldObj.theProfiler.startSection("navigation");
			this.navigator.onUpdateNavigation();
			this.worldObj.theProfiler.endSection();
			this.worldObj.theProfiler.startSection("mob tick");
			this.updateAITick();
			this.worldObj.theProfiler.endSection();
			this.worldObj.theProfiler.startSection("controls");
			this.worldObj.theProfiler.startSection("move");
			this.moveHelper.onUpdateMoveHelper();
			this.worldObj.theProfiler.endStartSection("look");
			this.lookHelper.onUpdateLook();
			this.worldObj.theProfiler.endStartSection("jump");
			this.jumpHelper.doJump();
			this.worldObj.theProfiler.endSection();
			this.worldObj.theProfiler.endSection();
		}

		protected internal override void updateEntityActionState()
		{
			base.updateEntityActionState();
			this.moveStrafing = 0.0F;
			this.moveForward = 0.0F;
			this.despawnEntity();
			float var1 = 8.0F;

			if (this.rand.nextFloat() < 0.02F)
			{
				EntityPlayer var2 = this.worldObj.getClosestPlayerToEntity(this, (double)var1);

				if (var2 != null)
				{
					this.currentTarget = var2;
					this.numTicksToChaseTarget = 10 + this.rand.Next(20);
				}
				else
				{
					this.randomYawVelocity = (this.rand.nextFloat() - 0.5F) * 20.0F;
				}
			}

			if (this.currentTarget != null)
			{
				this.faceEntity(this.currentTarget, 10.0F, (float)this.VerticalFaceSpeed);

				if (this.numTicksToChaseTarget-- <= 0 || this.currentTarget.isDead || this.currentTarget.getDistanceSqToEntity(this) > (double)(var1 * var1))
				{
					this.currentTarget = null;
				}
			}
			else
			{
				if (this.rand.nextFloat() < 0.05F)
				{
					this.randomYawVelocity = (this.rand.nextFloat() - 0.5F) * 20.0F;
				}

				this.rotationYaw += this.randomYawVelocity;
				this.rotationPitch = this.defaultPitch;
			}

			bool var4 = this.InWater;
			bool var3 = this.handleLavaMovement();

			if (var4 || var3)
			{
				this.isJumping = this.rand.nextFloat() < 0.8F;
			}
		}

///    
///     <summary> * The speed it takes to move the entityliving's rotationPitch through the faceEntity method. This is only currently
///     * use in wolves. </summary>
///     
		public virtual int VerticalFaceSpeed
		{
			get
			{
				return 40;
			}
		}

///    
///     <summary> * Changes pitch and yaw so that the entity calling the function is facing the entity provided as an argument. </summary>
///     
		public virtual void faceEntity(Entity p_70625_1_, float p_70625_2_, float p_70625_3_)
		{
			double var4 = p_70625_1_.posX - this.posX;
			double var8 = p_70625_1_.posZ - this.posZ;
			double var6;

			if (p_70625_1_ is EntityLivingBase)
			{
				EntityLivingBase var10 = (EntityLivingBase)p_70625_1_;
				var6 = var10.posY + (double)var10.EyeHeight - (this.posY + (double)this.EyeHeight);
			}
			else
			{
				var6 = (p_70625_1_.boundingBox.minY + p_70625_1_.boundingBox.maxY) / 2.0D - (this.posY + (double)this.EyeHeight);
			}

			double var14 = (double)MathHelper.sqrt_double(var4 * var4 + var8 * var8);
			float var12 = (float)(Math.Atan2(var8, var4) * 180.0D / Math.PI) - 90.0F;
			float var13 = (float)(-(Math.Atan2(var6, var14) * 180.0D / Math.PI));
			this.rotationPitch = this.updateRotation(this.rotationPitch, var13, p_70625_3_);
			this.rotationYaw = this.updateRotation(this.rotationYaw, var12, p_70625_2_);
		}

///    
///     <summary> * Arguments: current rotation, intended rotation, max increment. </summary>
///     
		private float updateRotation(float p_70663_1_, float p_70663_2_, float p_70663_3_)
		{
			float var4 = MathHelper.wrapAngleTo180_float(p_70663_2_ - p_70663_1_);

			if (var4 > p_70663_3_)
			{
				var4 = p_70663_3_;
			}

			if (var4 < -p_70663_3_)
			{
				var4 = -p_70663_3_;
			}

			return p_70663_1_ + var4;
		}

///    
///     <summary> * Checks if the entity's current position is a valid location to spawn this entity. </summary>
///     
		public virtual bool CanSpawnHere
		{
			get
			{
				return this.worldObj.checkNoEntityCollision(this.boundingBox) && this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox).Empty && !this.worldObj.isAnyLiquid(this.boundingBox);
			}
		}

///    
///     <summary> * Returns render size modifier </summary>
///     
		public virtual float RenderSizeModifier
		{
			get
			{
				return 1.0F;
			}
		}

///    
///     <summary> * Will return how many at most can spawn in a chunk at once. </summary>
///     
		public virtual int MaxSpawnedInChunk
		{
			get
			{
				return 4;
			}
		}

///    
///     <summary> * The number of iterations PathFinder.getSafePoint will execute before giving up. </summary>
///     
		public override int MaxSafePointTries
		{
			get
			{
				if (this.AttackTarget == null)
				{
					return 3;
				}
				else
				{
					int var1 = (int)(this.Health - this.MaxHealth * 0.33F);
					var1 -= (3 - this.worldObj.difficultySetting.DifficultyId) * 4;
	
					if (var1 < 0)
					{
						var1 = 0;
					}
	
					return var1 + 3;
				}
			}
		}

///    
///     <summary> * Returns the item that this EntityLiving is holding, if any. </summary>
///     
		public override ItemStack HeldItem
		{
			get
			{
				return this.equipment[0];
			}
		}

///    
///     <summary> * 0: Tool in Hand; 1-4: Armor </summary>
///     
		public override ItemStack getEquipmentInSlot(int p_71124_1_)
		{
			return this.equipment[p_71124_1_];
		}

		public virtual ItemStack func_130225_q(int p_130225_1_)
		{
			return this.equipment[p_130225_1_ + 1];
		}

///    
///     <summary> * Sets the held item, or an armor slot. Slot 0 is held item. Slot 1-4 is armor. Params: Item, slot </summary>
///     
		public override void setCurrentItemOrArmor(int p_70062_1_, ItemStack p_70062_2_)
		{
			this.equipment[p_70062_1_] = p_70062_2_;
		}

		public override ItemStack[] LastActiveItems
		{
			get
			{
				return this.equipment;
			}
		}

///    
///     <summary> * Drop the equipment for this entity. </summary>
///     
		protected internal override void dropEquipment(bool p_82160_1_, int p_82160_2_)
		{
			for (int var3 = 0; var3 < this.LastActiveItems.Length; ++var3)
			{
				ItemStack var4 = this.getEquipmentInSlot(var3);
				bool var5 = this.equipmentDropChances[var3] > 1.0F;

				if (var4 != null && (p_82160_1_ || var5) && this.rand.nextFloat() - (float)p_82160_2_ * 0.01F < this.equipmentDropChances[var3])
				{
					if (!var5 && var4.ItemStackDamageable)
					{
						int var6 = Math.Max(var4.MaxDamage - 25, 1);
						int var7 = var4.MaxDamage - this.rand.Next(this.rand.Next(var6) + 1);

						if (var7 > var6)
						{
							var7 = var6;
						}

						if (var7 < 1)
						{
							var7 = 1;
						}

						var4.ItemDamage = var7;
					}

					this.entityDropItem(var4, 0.0F);
				}
			}
		}

///    
///     <summary> * Makes entity wear random armor based on difficulty </summary>
///     
		protected internal virtual void addRandomArmor()
		{
			if (this.rand.nextFloat() < 0.15F * this.worldObj.func_147462_b(this.posX, this.posY, this.posZ))
			{
				int var1 = this.rand.Next(2);
				float var2 = this.worldObj.difficultySetting == EnumDifficulty.HARD ? 0.1F : 0.25F;

				if (this.rand.nextFloat() < 0.095F)
				{
					++var1;
				}

				if (this.rand.nextFloat() < 0.095F)
				{
					++var1;
				}

				if (this.rand.nextFloat() < 0.095F)
				{
					++var1;
				}

				for (int var3 = 3; var3 >= 0; --var3)
				{
					ItemStack var4 = this.func_130225_q(var3);

					if (var3 < 3 && this.rand.nextFloat() < var2)
					{
						break;
					}

					if (var4 == null)
					{
						Item var5 = getArmorItemForSlot(var3 + 1, var1);

						if (var5 != null)
						{
							this.setCurrentItemOrArmor(var3 + 1, new ItemStack(var5));
						}
					}
				}
			}
		}

		public static int getArmorPosition(ItemStack p_82159_0_)
		{
			if (p_82159_0_.Item != Item.getItemFromBlock(Blocks.pumpkin) && p_82159_0_.Item != Items.skull)
			{
				if (p_82159_0_.Item is ItemArmor)
				{
					switch (((ItemArmor)p_82159_0_.Item).armorType)
					{
						case 0:
							return 4;

						case 1:
							return 3;

						case 2:
							return 2;

						case 3:
							return 1;
					}
				}

				return 0;
			}
			else
			{
				return 4;
			}
		}

///    
///     <summary> * Params: Armor slot, Item tier </summary>
///     
		public static Item getArmorItemForSlot(int p_82161_0_, int p_82161_1_)
		{
			switch (p_82161_0_)
			{
				case 4:
					if (p_82161_1_ == 0)
					{
						return Items.leather_helmet;
					}
					else if (p_82161_1_ == 1)
					{
						return Items.golden_helmet;
					}
					else if (p_82161_1_ == 2)
					{
						return Items.chainmail_helmet;
					}
					else if (p_82161_1_ == 3)
					{
						return Items.iron_helmet;
					}
					else if (p_82161_1_ == 4)
					{
						return Items.diamond_helmet;
					}

				case 3:
					if (p_82161_1_ == 0)
					{
						return Items.leather_chestplate;
					}
					else if (p_82161_1_ == 1)
					{
						return Items.golden_chestplate;
					}
					else if (p_82161_1_ == 2)
					{
						return Items.chainmail_chestplate;
					}
					else if (p_82161_1_ == 3)
					{
						return Items.iron_chestplate;
					}
					else if (p_82161_1_ == 4)
					{
						return Items.diamond_chestplate;
					}

				case 2:
					if (p_82161_1_ == 0)
					{
						return Items.leather_leggings;
					}
					else if (p_82161_1_ == 1)
					{
						return Items.golden_leggings;
					}
					else if (p_82161_1_ == 2)
					{
						return Items.chainmail_leggings;
					}
					else if (p_82161_1_ == 3)
					{
						return Items.iron_leggings;
					}
					else if (p_82161_1_ == 4)
					{
						return Items.diamond_leggings;
					}

				case 1:
					if (p_82161_1_ == 0)
					{
						return Items.leather_boots;
					}
					else if (p_82161_1_ == 1)
					{
						return Items.golden_boots;
					}
					else if (p_82161_1_ == 2)
					{
						return Items.chainmail_boots;
					}
					else if (p_82161_1_ == 3)
					{
						return Items.iron_boots;
					}
					else if (p_82161_1_ == 4)
					{
						return Items.diamond_boots;
					}

				default:
					return null;
			}
		}

///    
///     <summary> * Enchants the entity's armor and held item based on difficulty </summary>
///     
		protected internal virtual void enchantEquipment()
		{
			float var1 = this.worldObj.func_147462_b(this.posX, this.posY, this.posZ);

			if (this.HeldItem != null && this.rand.nextFloat() < 0.25F * var1)
			{
				EnchantmentHelper.addRandomEnchantment(this.rand, this.HeldItem, (int)(5.0F + var1 * (float)this.rand.Next(18)));
			}

			for (int var2 = 0; var2 < 4; ++var2)
			{
				ItemStack var3 = this.func_130225_q(var2);

				if (var3 != null && this.rand.nextFloat() < 0.5F * var1)
				{
					EnchantmentHelper.addRandomEnchantment(this.rand, var3, (int)(5.0F + var1 * (float)this.rand.Next(18)));
				}
			}
		}

		public virtual IEntityLivingData onSpawnWithEgg(IEntityLivingData p_110161_1_)
		{
			this.getEntityAttribute(SharedMonsterAttributes.followRange).applyModifier(new AttributeModifier("Random spawn bonus", this.rand.nextGaussian() * 0.05D, 1));
			return p_110161_1_;
		}

///    
///     <summary> * returns true if all the conditions for steering the entity are met. For pigs, this is true if it is being ridden
///     * by a player and the player is holding a carrot-on-a-stick </summary>
///     
		public virtual bool canBeSteered()
		{
			return false;
		}

///    
///     <summary> * Gets the name of this command sender (usually username, but possibly "Rcon") </summary>
///     
		public override string CommandSenderName
		{
			get
			{
				return this.hasCustomNameTag() ? this.CustomNameTag : base.CommandSenderName;
			}
		}

		public virtual void func_110163_bv()
		{
			this.persistenceRequired = true;
		}

		public virtual string CustomNameTag
		{
			set
			{
				this.dataWatcher.updateObject(10, value);
			}
			get
			{
				return this.dataWatcher.getWatchableObjectString(10);
			}
		}


		public virtual bool hasCustomNameTag()
		{
			return this.dataWatcher.getWatchableObjectString(10).Length > 0;
		}

		public virtual bool AlwaysRenderNameTag
		{
			set
			{
				this.dataWatcher.updateObject(11, Convert.ToByte((sbyte)(value ? 1 : 0)));
			}
			get
			{
				return this.dataWatcher.getWatchableObjectByte(11) == 1;
			}
		}


		public override bool AlwaysRenderNameTagForRender
		{
			get
			{
				return this.AlwaysRenderNameTag;
			}
		}

		public virtual void setEquipmentDropChance(int p_96120_1_, float p_96120_2_)
		{
			this.equipmentDropChances[p_96120_1_] = p_96120_2_;
		}

		public virtual bool canPickUpLoot()
		{
			return this.canPickUpLoot;
		}

		public virtual bool CanPickUpLoot
		{
			set
			{
				this.canPickUpLoot = value;
			}
		}

		public virtual bool isNoDespawnRequired()
		{
			get
			{
				return this.persistenceRequired;
			}
		}

///    
///     <summary> * First layer of player interaction </summary>
///     
		public sealed override bool interactFirst(EntityPlayer p_130002_1_)
		{
			if (this.Leashed && this.LeashedToEntity == p_130002_1_)
			{
				this.clearLeashed(true, !p_130002_1_.capabilities.isCreativeMode);
				return true;
			}
			else
			{
				ItemStack var2 = p_130002_1_.inventory.CurrentItem;

				if (var2 != null && var2.Item == Items.lead && this.allowLeashing())
				{
					if (!(this is EntityTameable) || !((EntityTameable)this).Tamed)
					{
						this.setLeashedToEntity(p_130002_1_, true);
						--var2.stackSize;
						return true;
					}

					if (((EntityTameable)this).func_152114_e(p_130002_1_))
					{
						this.setLeashedToEntity(p_130002_1_, true);
						--var2.stackSize;
						return true;
					}
				}

				return this.interact(p_130002_1_) ? true : base.interactFirst(p_130002_1_);
			}
		}

///    
///     <summary> * Called when a player interacts with a mob. e.g. gets milk from a cow, gets into the saddle on a pig. </summary>
///     
		protected internal virtual bool interact(EntityPlayer p_70085_1_)
		{
			return false;
		}

///    
///     <summary> * Applies logic related to leashes, for example dragging the entity or breaking the leash. </summary>
///     
		protected internal virtual void updateLeashedState()
		{
			if (this.field_110170_bx != null)
			{
				this.recreateLeash();
			}

			if (this.isLeashed)
			{
				if (this.leashedToEntity == null || this.leashedToEntity.isDead)
				{
					this.clearLeashed(true, true);
				}
			}
		}

///    
///     <summary> * Removes the leash from this entity. Second parameter tells whether to send a packet to surrounding players. </summary>
///     
		public virtual void clearLeashed(bool p_110160_1_, bool p_110160_2_)
		{
			if (this.isLeashed)
			{
				this.isLeashed = false;
				this.leashedToEntity = null;

				if (!this.worldObj.isClient && p_110160_2_)
				{
					this.func_145779_a(Items.lead, 1);
				}

				if (!this.worldObj.isClient && p_110160_1_ && this.worldObj is WorldServer)
				{
					((WorldServer)this.worldObj).EntityTracker.func_151247_a(this, new S1BPacketEntityAttach(1, this, (Entity)null));
				}
			}
		}

		public virtual bool allowLeashing()
		{
			return !this.Leashed && !(this is IMob);
		}

		public virtual bool Leashed
		{
			get
			{
				return this.isLeashed;
			}
		}

		public virtual Entity LeashedToEntity
		{
			get
			{
				return this.leashedToEntity;
			}
		}

///     </param>
///     * Sets the entity to be leashed to.\nArgs:\n<param name="par1Entity">: The entity to be tethered to.\n<param name="par2">: Whether
///     * to send an attaching notification packet to surrounding players. </param>
///     
		public virtual void setLeashedToEntity(Entity p_110162_1_, bool p_110162_2_)
		{
			this.isLeashed = true;
			this.leashedToEntity = p_110162_1_;

			if (!this.worldObj.isClient && p_110162_2_ && this.worldObj is WorldServer)
			{
				((WorldServer)this.worldObj).EntityTracker.func_151247_a(this, new S1BPacketEntityAttach(1, this, this.leashedToEntity));
			}
		}

		private void recreateLeash()
		{
			if (this.isLeashed && this.field_110170_bx != null)
			{
				if (this.field_110170_bx.func_150297_b("UUIDMost", 4) && this.field_110170_bx.func_150297_b("UUIDLeast", 4))
				{
					UUID var5 = new UUID(this.field_110170_bx.getLong("UUIDMost"), this.field_110170_bx.getLong("UUIDLeast"));
					IList var6 = this.worldObj.getEntitiesWithinAABB(typeof(EntityLivingBase), this.boundingBox.expand(10.0D, 10.0D, 10.0D));
					IEnumerator var7 = var6.GetEnumerator();

					while (var7.MoveNext())
					{
						EntityLivingBase var8 = (EntityLivingBase)var7.Current;

						if (var8.UniqueID.Equals(var5))
						{
							this.leashedToEntity = var8;
							break;
						}
					}
				}
				else if (this.field_110170_bx.func_150297_b("X", 99) && this.field_110170_bx.func_150297_b("Y", 99) && this.field_110170_bx.func_150297_b("Z", 99))
				{
					int var1 = this.field_110170_bx.getInteger("X");
					int var2 = this.field_110170_bx.getInteger("Y");
					int var3 = this.field_110170_bx.getInteger("Z");
					EntityLeashKnot var4 = EntityLeashKnot.getKnotForBlock(this.worldObj, var1, var2, var3);

					if (var4 == null)
					{
						var4 = EntityLeashKnot.func_110129_a(this.worldObj, var1, var2, var3);
					}

					this.leashedToEntity = var4;
				}
				else
				{
					this.clearLeashed(false, true);
				}
			}

			this.field_110170_bx = null;
		}
	}

}