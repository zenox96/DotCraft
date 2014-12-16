using System;

namespace DotCraftCore.Entity.Monster
{

	using Entity = DotCraftCore.Entity.Entity;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using EntitySmallFireball = DotCraftCore.Entity.Projectile.EntitySmallFireball;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;

	public class EntityBlaze : EntityMob
	{
	/// <summary> Random offset used in floating behaviour  </summary>
		private float heightOffset = 0.5F;

	/// <summary> ticks until heightOffset is randomized  </summary>
		private int heightOffsetUpdateTime;
		private int field_70846_g;
		

		public EntityBlaze(World p_i1731_1_) : base(p_i1731_1_)
		{
			this.isImmuneToFire = true;
			this.experienceValue = 10;
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.attackDamage).BaseValue = 6.0D;
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, new sbyte?((sbyte)0));
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return "mob.blaze.breathe";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.blaze.hit";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.blaze.death";
			}
		}

		public override int getBrightnessForRender(float p_70070_1_)
		{
			return 15728880;
		}

///    
///     <summary> * Gets how bright this entity is. </summary>
///     
		public override float getBrightness(float p_70013_1_)
		{
			return 1.0F;
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public override void onLivingUpdate()
		{
			if (!this.worldObj.isClient)
			{
				if (this.Wet)
				{
					this.attackEntityFrom(DamageSource.drown, 1.0F);
				}

				--this.heightOffsetUpdateTime;

				if (this.heightOffsetUpdateTime <= 0)
				{
					this.heightOffsetUpdateTime = 100;
					this.heightOffset = 0.5F + (float)this.rand.nextGaussian() * 3.0F;
				}

				if (this.EntityToAttack != null && this.EntityToAttack.posY + (double)this.EntityToAttack.EyeHeight > this.posY + (double)this.EyeHeight + (double)this.heightOffset)
				{
					this.motionY += (0.30000001192092896D - this.motionY) * 0.30000001192092896D;
				}
			}

			if (this.rand.Next(24) == 0)
			{
				this.worldObj.playSoundEffect(this.posX + 0.5D, this.posY + 0.5D, this.posZ + 0.5D, "fire.fire", 1.0F + this.rand.nextFloat(), this.rand.nextFloat() * 0.7F + 0.3F);
			}

			if (!this.onGround && this.motionY < 0.0D)
			{
				this.motionY *= 0.6D;
			}

			for (int var1 = 0; var1 < 2; ++var1)
			{
				this.worldObj.spawnParticle("largesmoke", this.posX + (this.rand.NextDouble() - 0.5D) * (double)this.width, this.posY + this.rand.NextDouble() * (double)this.height, this.posZ + (this.rand.NextDouble() - 0.5D) * (double)this.width, 0.0D, 0.0D, 0.0D);
			}

			base.onLivingUpdate();
		}

///    
///     <summary> * Basic mob attack. Default to touch of death in EntityCreature. Overridden by each mob to define their attack. </summary>
///     
		protected internal override void attackEntity(Entity p_70785_1_, float p_70785_2_)
		{
			if (this.attackTime <= 0 && p_70785_2_ < 2.0F && p_70785_1_.boundingBox.maxY > this.boundingBox.minY && p_70785_1_.boundingBox.minY < this.boundingBox.maxY)
			{
				this.attackTime = 20;
				this.attackEntityAsMob(p_70785_1_);
			}
			else if (p_70785_2_ < 30.0F)
			{
				double var3 = p_70785_1_.posX - this.posX;
				double var5 = p_70785_1_.boundingBox.minY + (double)(p_70785_1_.height / 2.0F) - (this.posY + (double)(this.height / 2.0F));
				double var7 = p_70785_1_.posZ - this.posZ;

				if (this.attackTime == 0)
				{
					++this.field_70846_g;

					if (this.field_70846_g == 1)
					{
						this.attackTime = 60;
						this.func_70844_e(true);
					}
					else if (this.field_70846_g <= 4)
					{
						this.attackTime = 6;
					}
					else
					{
						this.attackTime = 100;
						this.field_70846_g = 0;
						this.func_70844_e(false);
					}

					if (this.field_70846_g > 1)
					{
						float var9 = MathHelper.sqrt_float(p_70785_2_) * 0.5F;
						this.worldObj.playAuxSFXAtEntity((EntityPlayer)null, 1009, (int)this.posX, (int)this.posY, (int)this.posZ, 0);

						for (int var10 = 0; var10 < 1; ++var10)
						{
							EntitySmallFireball var11 = new EntitySmallFireball(this.worldObj, this, var3 + this.rand.nextGaussian() * (double)var9, var5, var7 + this.rand.nextGaussian() * (double)var9);
							var11.posY = this.posY + (double)(this.height / 2.0F) + 0.5D;
							this.worldObj.spawnEntityInWorld(var11);
						}
					}
				}

				this.rotationYaw = (float)(Math.Atan2(var7, var3) * 180.0D / Math.PI) - 90.0F;
				this.hasAttacked = true;
			}
		}

///    
///     <summary> * Called when the mob is falling. Calculates and applies fall damage. </summary>
///     
		protected internal override void fall(float p_70069_1_)
		{
		}

		protected internal override Item func_146068_u()
		{
			return Items.blaze_rod;
		}

///    
///     <summary> * Returns true if the entity is on fire. Used by render to add the fire effect on rendering. </summary>
///     
		public override bool isBurning()
		{
			get
			{
				return this.func_70845_n();
			}
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			if (p_70628_1_)
			{
				int var3 = this.rand.Next(2 + p_70628_2_);

				for (int var4 = 0; var4 < var3; ++var4)
				{
					this.func_145779_a(Items.blaze_rod, 1);
				}
			}
		}

		public virtual bool func_70845_n()
		{
			return (this.dataWatcher.getWatchableObjectByte(16) & 1) != 0;
		}

		public virtual void func_70844_e(bool p_70844_1_)
		{
			sbyte var2 = this.dataWatcher.getWatchableObjectByte(16);

			if (p_70844_1_)
			{
				var2 = (sbyte)(var2 | 1);
			}
			else
			{
				var2 &= -2;
			}

			this.dataWatcher.updateObject(16, Convert.ToByte(var2));
		}

///    
///     <summary> * Checks to make sure the light is not too bright where the mob is spawning </summary>
///     
		protected internal override bool isValidLightLevel()
		{
			get
			{
				return true;
			}
		}
	}

}