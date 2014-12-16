using System;

namespace DotCraftCore.Entity.Passive
{

	using Material = DotCraftCore.block.material.Material;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;

	public class EntitySquid : EntityWaterMob
	{
		public float squidPitch;
		public float prevSquidPitch;
		public float squidYaw;
		public float prevSquidYaw;

///    
///     <summary> * appears to be rotation in radians; we already have pitch & yaw, so this completes the triumvirate. </summary>
///     
		public float squidRotation;

	/// <summary> previous squidRotation in radians  </summary>
		public float prevSquidRotation;

	/// <summary> angle of the tentacles in radians  </summary>
		public float tentacleAngle;

	/// <summary> the last calculated angle of the tentacles in radians  </summary>
		public float lastTentacleAngle;
		private float randomMotionSpeed;

	/// <summary> change in squidRotation in radians.  </summary>
		private float rotationVelocity;
		private float field_70871_bB;
		private float randomMotionVecX;
		private float randomMotionVecY;
		private float randomMotionVecZ;
		

		public EntitySquid(World p_i1693_1_) : base(p_i1693_1_)
		{
			this.setSize(0.95F, 0.95F);
			this.rotationVelocity = 1.0F / (this.rand.nextFloat() + 1.0F) * 0.2F;
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 10.0D;
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return null;
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return null;
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return null;
			}
		}

///    
///     <summary> * Returns the volume for the sounds this mob makes. </summary>
///     
		protected internal override float SoundVolume
		{
			get
			{
				return 0.4F;
			}
		}

		protected internal override Item func_146068_u()
		{
			return Item.getItemById(0);
		}

///    
///     <summary> * returns if this entity triggers Block.onEntityWalking on the blocks they walk on. used for spiders and wolves to
///     * prevent them from trampling crops </summary>
///     
		protected internal override bool canTriggerWalking()
		{
			return false;
		}

///    
///     <summary> * Drop 0-2 items of this living's type </summary>
///     
		protected internal override void dropFewItems(bool p_70628_1_, int p_70628_2_)
		{
			int var3 = this.rand.Next(3 + p_70628_2_) + 1;

			for (int var4 = 0; var4 < var3; ++var4)
			{
				this.entityDropItem(new ItemStack(Items.dye, 1, 0), 0.0F);
			}
		}

///    
///     <summary> * Checks if this entity is inside water (if inWater field is true as a result of handleWaterMovement() returning
///     * true) </summary>
///     
		public override bool isInWater()
		{
			get
			{
				return this.worldObj.handleMaterialAcceleration(this.boundingBox.expand(0.0D, -0.6000000238418579D, 0.0D), Material.water, this);
			}
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public override void onLivingUpdate()
		{
			base.onLivingUpdate();
			this.prevSquidPitch = this.squidPitch;
			this.prevSquidYaw = this.squidYaw;
			this.prevSquidRotation = this.squidRotation;
			this.lastTentacleAngle = this.tentacleAngle;
			this.squidRotation += this.rotationVelocity;

			if (this.squidRotation > ((float)Math.PI * 2F))
			{
				this.squidRotation -= ((float)Math.PI * 2F);

				if (this.rand.Next(10) == 0)
				{
					this.rotationVelocity = 1.0F / (this.rand.nextFloat() + 1.0F) * 0.2F;
				}
			}

			if (this.InWater)
			{
				float var1;

				if (this.squidRotation < (float)Math.PI)
				{
					var1 = this.squidRotation / (float)Math.PI;
					this.tentacleAngle = MathHelper.sin(var1 * var1 * (float)Math.PI) * (float)Math.PI * 0.25F;

					if ((double)var1 > 0.75D)
					{
						this.randomMotionSpeed = 1.0F;
						this.field_70871_bB = 1.0F;
					}
					else
					{
						this.field_70871_bB *= 0.8F;
					}
				}
				else
				{
					this.tentacleAngle = 0.0F;
					this.randomMotionSpeed *= 0.9F;
					this.field_70871_bB *= 0.99F;
				}

				if (!this.worldObj.isClient)
				{
					this.motionX = (double)(this.randomMotionVecX * this.randomMotionSpeed);
					this.motionY = (double)(this.randomMotionVecY * this.randomMotionSpeed);
					this.motionZ = (double)(this.randomMotionVecZ * this.randomMotionSpeed);
				}

				var1 = MathHelper.sqrt_double(this.motionX * this.motionX + this.motionZ * this.motionZ);
				this.renderYawOffset += (-((float)Math.Atan2(this.motionX, this.motionZ)) * 180.0F / (float)Math.PI - this.renderYawOffset) * 0.1F;
				this.rotationYaw = this.renderYawOffset;
				this.squidYaw += (float)Math.PI * this.field_70871_bB * 1.5F;
				this.squidPitch += (-((float)Math.Atan2((double)var1, this.motionY)) * 180.0F / (float)Math.PI - this.squidPitch) * 0.1F;
			}
			else
			{
				this.tentacleAngle = MathHelper.abs(MathHelper.sin(this.squidRotation)) * (float)Math.PI * 0.25F;

				if (!this.worldObj.isClient)
				{
					this.motionX = 0.0D;
					this.motionY -= 0.08D;
					this.motionY *= 0.9800000190734863D;
					this.motionZ = 0.0D;
				}

				this.squidPitch = (float)((double)this.squidPitch + (double)(-90.0F - this.squidPitch) * 0.02D);
			}
		}

///    
///     <summary> * Moves the entity based on the specified heading.  Args: strafe, forward </summary>
///     
		public override void moveEntityWithHeading(float p_70612_1_, float p_70612_2_)
		{
			this.moveEntity(this.motionX, this.motionY, this.motionZ);
		}

		protected internal override void updateEntityActionState()
		{
			++this.entityAge;

			if (this.entityAge > 100)
			{
				this.randomMotionVecX = this.randomMotionVecY = this.randomMotionVecZ = 0.0F;
			}
			else if (this.rand.Next(50) == 0 || !this.inWater || this.randomMotionVecX == 0.0F && this.randomMotionVecY == 0.0F && this.randomMotionVecZ == 0.0F)
			{
				float var1 = this.rand.nextFloat() * (float)Math.PI * 2.0F;
				this.randomMotionVecX = MathHelper.cos(var1) * 0.2F;
				this.randomMotionVecY = -0.1F + this.rand.nextFloat() * 0.2F;
				this.randomMotionVecZ = MathHelper.sin(var1) * 0.2F;
			}

			this.despawnEntity();
		}

///    
///     <summary> * Checks if the entity's current position is a valid location to spawn this entity. </summary>
///     
		public override bool CanSpawnHere
		{
			get
			{
				return this.posY > 45.0D && this.posY < 63.0D && base.CanSpawnHere;
			}
		}
	}

}