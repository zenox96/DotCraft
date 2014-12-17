using System;

namespace DotCraftCore.nEntity.nMonster
{

	using Block = DotCraftCore.nBlock.Block;
	using Entity = DotCraftCore.nEntity.Entity;
	using EnumCreatureAttribute = DotCraftCore.nEntity.EnumCreatureAttribute;
	using IEntityLivingData = DotCraftCore.nEntity.IEntityLivingData;
	using SharedMonsterAttributes = DotCraftCore.nEntity.SharedMonsterAttributes;
	using Items = DotCraftCore.nInit.Items;
	using Item = DotCraftCore.nItem.Item;
	using Potion = DotCraftCore.nPotion.Potion;
	using PotionEffect = DotCraftCore.nPotion.PotionEffect;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using EnumDifficulty = DotCraftCore.nWorld.EnumDifficulty;
	using World = DotCraftCore.nWorld.World;

	public class EntitySpider : EntityMob
	{
		

		public EntitySpider(World p_i1743_1_) : base(p_i1743_1_)
		{
			this.setSize(1.4F, 0.9F);
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, new sbyte?((sbyte)0));
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			base.onUpdate();

			if (!this.worldObj.isClient)
			{
				this.BesideClimbableBlock = this.isCollidedHorizontally;
			}
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 16.0D;
			this.getEntityAttribute(SharedMonsterAttributes.movementSpeed).BaseValue = 0.800000011920929D;
		}

///    
///     <summary> * Finds the closest player within 16 blocks to attack, or null if this Entity isn't interested in attacking
///     * (Animals, Spiders at day, peaceful PigZombies). </summary>
///     
		protected internal override Entity findPlayerToAttack()
		{
			float var1 = this.getBrightness(1.0F);

			if (var1 < 0.5F)
			{
				double var2 = 16.0D;
				return this.worldObj.getClosestVulnerablePlayerToEntity(this, var2);
			}
			else
			{
				return null;
			}
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return "mob.spider.say";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.spider.say";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.spider.death";
			}
		}

		protected internal override void func_145780_a(int p_145780_1_, int p_145780_2_, int p_145780_3_, Block p_145780_4_)
		{
			this.playSound("mob.spider.step", 0.15F, 1.0F);
		}

///    
///     <summary> * Basic mob attack. Default to touch of death in EntityCreature. Overridden by each mob to define their attack. </summary>
///     
		protected internal override void attackEntity(Entity p_70785_1_, float p_70785_2_)
		{
			float var3 = this.getBrightness(1.0F);

			if (var3 > 0.5F && this.rand.Next(100) == 0)
			{
				this.entityToAttack = null;
			}
			else
			{
				if (p_70785_2_ > 2.0F && p_70785_2_ < 6.0F && this.rand.Next(10) == 0)
				{
					if (this.onGround)
					{
						double var4 = p_70785_1_.posX - this.posX;
						double var6 = p_70785_1_.posZ - this.posZ;
						float var8 = MathHelper.sqrt_double(var4 * var4 + var6 * var6);
						this.motionX = var4 / (double)var8 * 0.5D * 0.800000011920929D + this.motionX * 0.20000000298023224D;
						this.motionZ = var6 / (double)var8 * 0.5D * 0.800000011920929D + this.motionZ * 0.20000000298023224D;
						this.motionY = 0.4000000059604645D;
					}
				}
				else
				{
					base.attackEntity(p_70785_1_, p_70785_2_);
				}
			}
		}

		protected internal override Item func_146068_u()
		{
			return Items.string;
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			base.dropFewItems(p_70628_1_, p_70628_2_);

			if (p_70628_1_ && (this.rand.Next(3) == 0 || this.rand.Next(1 + p_70628_2_) > 0))
			{
				this.func_145779_a(Items.spider_eye, 1);
			}
		}

///    
///     <summary> * returns true if this entity is by a ladder, false otherwise </summary>
///     
		public override bool isOnLadder()
		{
			get
			{
				return this.BesideClimbableBlock;
			}
		}

///    
///     <summary> * Sets the Entity inside a web block. </summary>
///     
		public override void setInWeb()
		{
		}

///    
///     <summary> * Get this Entity's EnumCreatureAttribute </summary>
///     
		public override EnumCreatureAttribute CreatureAttribute
		{
			get
			{
				return EnumCreatureAttribute.ARTHROPOD;
			}
		}

		public override bool isPotionApplicable(PotionEffect p_70687_1_)
		{
			return p_70687_1_.PotionID == Potion.poison.id ? false : base.isPotionApplicable(p_70687_1_);
		}

///    
///     <summary> * Returns true if the WatchableObject (Byte) is 0x01 otherwise returns false. The WatchableObject is updated using
///     * setBesideClimableBlock. </summary>
///     
		public virtual bool isBesideClimbableBlock()
		{
			get
			{
				return (this.dataWatcher.getWatchableObjectByte(16) & 1) != 0;
			}
			set
			{
				sbyte var2 = this.dataWatcher.getWatchableObjectByte(16);
	
				if (value)
				{
					var2 = (sbyte)(var2 | 1);
				}
				else
				{
					var2 &= -2;
				}
	
				this.dataWatcher.updateObject(16, Convert.ToByte(var2));
			}
		}

///    
///     <summary> * Updates the WatchableObject (Byte) created in entityInit(), setting it to 0x01 if par1 is true or 0x00 if it is
///     * false. </summary>
///     

		public override IEntityLivingData onSpawnWithEgg(IEntityLivingData p_110161_1_)
		{
			object p_110161_1_1 = base.onSpawnWithEgg(p_110161_1_);

			if (this.worldObj.rand.Next(100) == 0)
			{
				EntitySkeleton var2 = new EntitySkeleton(this.worldObj);
				var2.setLocationAndAngles(this.posX, this.posY, this.posZ, this.rotationYaw, 0.0F);
				var2.onSpawnWithEgg((IEntityLivingData)null);
				this.worldObj.spawnEntityInWorld(var2);
				var2.mountEntity(this);
			}

			if (p_110161_1_1 == null)
			{
				p_110161_1_1 = new EntitySpider.GroupData();

				if (this.worldObj.difficultySetting == EnumDifficulty.HARD && this.worldObj.rand.nextFloat() < 0.1F * this.worldObj.func_147462_b(this.posX, this.posY, this.posZ))
				{
					((EntitySpider.GroupData)p_110161_1_1).func_111104_a(this.worldObj.rand);
				}
			}

			if (p_110161_1_1 is EntitySpider.GroupData)
			{
				int var4 = ((EntitySpider.GroupData)p_110161_1_1).field_111105_a;

				if (var4 > 0 && Potion.potionTypes[var4] != null)
				{
					this.addPotionEffect(new PotionEffect(var4, int.MaxValue));
				}
			}

			return (IEntityLivingData)p_110161_1_1;
		}

		public class GroupData : IEntityLivingData
		{
			public int field_111105_a;
			

			public virtual void func_111104_a(Random p_111104_1_)
			{
				int var2 = p_111104_1_.Next(5);

				if (var2 <= 1)
				{
					this.field_111105_a = Potion.moveSpeed.id;
				}
				else if (var2 <= 2)
				{
					this.field_111105_a = Potion.damageBoost.id;
				}
				else if (var2 <= 3)
				{
					this.field_111105_a = Potion.regeneration.id;
				}
				else if (var2 <= 4)
				{
					this.field_111105_a = Potion.invisibility.id;
				}
			}
		}
	}

}