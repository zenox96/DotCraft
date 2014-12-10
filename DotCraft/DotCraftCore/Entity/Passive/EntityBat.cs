using System;

namespace DotCraftCore.Entity.Passive
{

	using Entity = DotCraftCore.Entity.Entity;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using ChunkCoordinates = DotCraftCore.Util.ChunkCoordinates;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;

	public class EntityBat : EntityAmbientCreature
	{
	/// <summary> Coordinates of where the bat spawned.  </summary>
		private ChunkCoordinates spawnPosition;
		private const string __OBFID = "CL_00001637";

		public EntityBat(World p_i1680_1_) : base(p_i1680_1_)
		{
			this.setSize(0.5F, 0.9F);
			this.IsBatHanging = true;
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, new sbyte?((sbyte)0));
		}

///    
///     <summary> * Returns the volume for the sounds this mob makes. </summary>
///     
		protected internal override float SoundVolume
		{
			get
			{
				return 0.1F;
			}
		}

///    
///     <summary> * Gets the pitch of living sounds in living entities. </summary>
///     
		protected internal override float SoundPitch
		{
			get
			{
				return base.SoundPitch * 0.95F;
			}
		}

///    
///     <summary> * Returns the sound this mob makes while it's alive. </summary>
///     
		protected internal override string LivingSound
		{
			get
			{
				return this.IsBatHanging && this.rand.Next(4) != 0 ? null : "mob.bat.idle";
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal override string HurtSound
		{
			get
			{
				return "mob.bat.hurt";
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal override string DeathSound
		{
			get
			{
				return "mob.bat.death";
			}
		}

///    
///     <summary> * Returns true if this entity should push and be pushed by other entities when colliding. </summary>
///     
		public override bool canBePushed()
		{
			return false;
		}

		protected internal override void collideWithEntity(Entity p_82167_1_)
		{
		}

		protected internal override void collideWithNearbyEntities()
		{
		}

		protected internal override void applyEntityAttributes()
		{
			base.applyEntityAttributes();
			this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = 6.0D;
		}

		public virtual bool IsBatHanging
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
					this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(var2 | 1)));
				}
				else
				{
					this.dataWatcher.updateObject(16, Convert.ToByte((sbyte)(var2 & -2)));
				}
			}
		}


///    
///     <summary> * Returns true if the newer Entity AI code should be run </summary>
///     
		protected internal override bool isAIEnabled()
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			base.onUpdate();

			if (this.IsBatHanging)
			{
				this.motionX = this.motionY = this.motionZ = 0.0D;
				this.posY = (double)MathHelper.floor_double(this.posY) + 1.0D - (double)this.height;
			}
			else
			{
				this.motionY *= 0.6000000238418579D;
			}
		}

		protected internal override void updateAITasks()
		{
			base.updateAITasks();

			if (this.IsBatHanging)
			{
				if (!this.worldObj.getBlock(MathHelper.floor_double(this.posX), (int)this.posY + 1, MathHelper.floor_double(this.posZ)).NormalCube)
				{
					this.IsBatHanging = false;
					this.worldObj.playAuxSFXAtEntity((EntityPlayer)null, 1015, (int)this.posX, (int)this.posY, (int)this.posZ, 0);
				}
				else
				{
					if (this.rand.Next(200) == 0)
					{
						this.rotationYawHead = (float)this.rand.Next(360);
					}

					if (this.worldObj.getClosestPlayerToEntity(this, 4.0D) != null)
					{
						this.IsBatHanging = false;
						this.worldObj.playAuxSFXAtEntity((EntityPlayer)null, 1015, (int)this.posX, (int)this.posY, (int)this.posZ, 0);
					}
				}
			}
			else
			{
				if (this.spawnPosition != null && (!this.worldObj.isAirBlock(this.spawnPosition.posX, this.spawnPosition.posY, this.spawnPosition.posZ) || this.spawnPosition.posY < 1))
				{
					this.spawnPosition = null;
				}

				if (this.spawnPosition == null || this.rand.Next(30) == 0 || this.spawnPosition.getDistanceSquared((int)this.posX, (int)this.posY, (int)this.posZ) < 4.0F)
				{
					this.spawnPosition = new ChunkCoordinates((int)this.posX + this.rand.Next(7) - this.rand.Next(7), (int)this.posY + this.rand.Next(6) - 2, (int)this.posZ + this.rand.Next(7) - this.rand.Next(7));
				}

				double var1 = (double)this.spawnPosition.posX + 0.5D - this.posX;
				double var3 = (double)this.spawnPosition.posY + 0.1D - this.posY;
				double var5 = (double)this.spawnPosition.posZ + 0.5D - this.posZ;
				this.motionX += (Math.Sign(var1) * 0.5D - this.motionX) * 0.10000000149011612D;
				this.motionY += (Math.Sign(var3) * 0.699999988079071D - this.motionY) * 0.10000000149011612D;
				this.motionZ += (Math.Sign(var5) * 0.5D - this.motionZ) * 0.10000000149011612D;
				float var7 = (float)(Math.Atan2(this.motionZ, this.motionX) * 180.0D / Math.PI) - 90.0F;
				float var8 = MathHelper.wrapAngleTo180_float(var7 - this.rotationYaw);
				this.moveForward = 0.5F;
				this.rotationYaw += var8;

				if (this.rand.Next(100) == 0 && this.worldObj.getBlock(MathHelper.floor_double(this.posX), (int)this.posY + 1, MathHelper.floor_double(this.posZ)).NormalCube)
				{
					this.IsBatHanging = true;
				}
			}
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
///     <summary> * Called when the mob is falling. Calculates and applies fall damage. </summary>
///     
		protected internal override void fall(float p_70069_1_)
		{
		}

///    
///     <summary> * Takes in the distance the entity has fallen this tick and whether its on the ground to update the fall distance
///     * and deal fall damage if landing on the ground.  Args: distanceFallenThisTick, onGround </summary>
///     
		protected internal override void updateFallState(double p_70064_1_, bool p_70064_3_)
		{
		}

		public override bool doesEntityNotTriggerPressurePlate()
		{
			return true;
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
			else
			{
				if (!this.worldObj.isClient && this.IsBatHanging)
				{
					this.IsBatHanging = false;
				}

				return base.attackEntityFrom(p_70097_1_, p_70097_2_);
			}
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.dataWatcher.updateObject(16, Convert.ToByte(p_70037_1_.getByte("BatFlags")));
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setByte("BatFlags", this.dataWatcher.getWatchableObjectByte(16));
		}

///    
///     <summary> * Checks if the entity's current position is a valid location to spawn this entity. </summary>
///     
		public override bool CanSpawnHere
		{
			get
			{
				int var1 = MathHelper.floor_double(this.boundingBox.minY);
	
				if (var1 >= 63)
				{
					return false;
				}
				else
				{
					int var2 = MathHelper.floor_double(this.posX);
					int var3 = MathHelper.floor_double(this.posZ);
					int var4 = this.worldObj.getBlockLightValue(var2, var1, var3);
					sbyte var5 = 4;
					Calendar var6 = this.worldObj.CurrentDate;
	
					if ((var6.get(2) + 1 != 10 || var6.get(5) < 20) && (var6.get(2) + 1 != 11 || var6.get(5) > 3))
					{
						if (this.rand.nextBoolean())
						{
							return false;
						}
					}
					else
					{
						var5 = 7;
					}
	
					return var4 > this.rand.Next(var5) ? false : base.CanSpawnHere;
				}
			}
		}
	}

}