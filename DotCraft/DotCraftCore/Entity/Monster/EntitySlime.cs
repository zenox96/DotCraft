using System;

namespace DotCraftCore.Entity.Monster
{

	using EntityLiving = DotCraftCore.Entity.EntityLiving;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using EnumDifficulty = DotCraftCore.World.EnumDifficulty;
	using World = DotCraftCore.World.World;
	using WorldType = DotCraftCore.World.WorldType;
	using BiomeGenBase = DotCraftCore.World.Biome.BiomeGenBase;
	using Chunk = DotCraftCore.World.Chunk.Chunk;

	public class EntitySlime : EntityLiving, IMob
	{
		public float squishAmount;
		public float squishFactor;
		public float prevSquishFactor;

	/// <summary> ticks until this slime jumps again  </summary>
		private int slimeJumpDelay;
		private const string __OBFID = "CL_00001698";

		public EntitySlime(World p_i1742_1_) : base(p_i1742_1_)
		{
			int var2 = 1 << this.rand.Next(3);
			this.yOffset = 0.0F;
			this.slimeJumpDelay = this.rand.Next(20) + 10;
			this.SlimeSize = var2;
		}

		protected internal override void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(16, new sbyte?((sbyte)1));
		}

		protected internal virtual int SlimeSize
		{
			set
			{
				this.dataWatcher.updateObject(16, new sbyte?((sbyte)value));
				this.setSize(0.6F * (float)value, 0.6F * (float)value);
				this.setPosition(this.posX, this.posY, this.posZ);
				this.getEntityAttribute(SharedMonsterAttributes.maxHealth).BaseValue = (double)(value * value);
				this.Health = this.MaxHealth;
				this.experienceValue = value;
			}
			get
			{
				return this.dataWatcher.getWatchableObjectByte(16);
			}
		}

///    
///     <summary> * Returns the size of the slime. </summary>
///     

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setInteger("Size", this.SlimeSize - 1);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			int var2 = p_70037_1_.getInteger("Size");

			if (var2 < 0)
			{
				var2 = 0;
			}

			this.SlimeSize = var2 + 1;
		}

///    
///     <summary> * Returns the name of a particle effect that may be randomly created by EntitySlime.onUpdate() </summary>
///     
		protected internal virtual string SlimeParticle
		{
			get
			{
				return "slime";
			}
		}

///    
///     <summary> * Returns the name of the sound played when the slime jumps. </summary>
///     
		protected internal virtual string JumpSound
		{
			get
			{
				return "mob.slime." + (this.SlimeSize > 1 ? "big" : "small");
			}
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			if (!this.worldObj.isClient && this.worldObj.difficultySetting == EnumDifficulty.PEACEFUL && this.SlimeSize > 0)
			{
				this.isDead = true;
			}

			this.squishFactor += (this.squishAmount - this.squishFactor) * 0.5F;
			this.prevSquishFactor = this.squishFactor;
			bool var1 = this.onGround;
			base.onUpdate();
			int var2;

			if (this.onGround && !var1)
			{
				var2 = this.SlimeSize;

				for (int var3 = 0; var3 < var2 * 8; ++var3)
				{
					float var4 = this.rand.nextFloat() * (float)Math.PI * 2.0F;
					float var5 = this.rand.nextFloat() * 0.5F + 0.5F;
					float var6 = MathHelper.sin(var4) * (float)var2 * 0.5F * var5;
					float var7 = MathHelper.cos(var4) * (float)var2 * 0.5F * var5;
					this.worldObj.spawnParticle(this.SlimeParticle, this.posX + (double)var6, this.boundingBox.minY, this.posZ + (double)var7, 0.0D, 0.0D, 0.0D);
				}

				if (this.makesSoundOnLand())
				{
					this.playSound(this.JumpSound, this.SoundVolume, ((this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F) / 0.8F);
				}

				this.squishAmount = -0.5F;
			}
			else if (!this.onGround && var1)
			{
				this.squishAmount = 1.0F;
			}

			this.alterSquishAmount();

			if (this.worldObj.isClient)
			{
				var2 = this.SlimeSize;
				this.setSize(0.6F * (float)var2, 0.6F * (float)var2);
			}
		}

		protected internal override void updateEntityActionState()
		{
			this.despawnEntity();
			EntityPlayer var1 = this.worldObj.getClosestVulnerablePlayerToEntity(this, 16.0D);

			if (var1 != null)
			{
				this.faceEntity(var1, 10.0F, 20.0F);
			}

			if (this.onGround && this.slimeJumpDelay-- <= 0)
			{
				this.slimeJumpDelay = this.JumpDelay;

				if (var1 != null)
				{
					this.slimeJumpDelay /= 3;
				}

				this.isJumping = true;

				if (this.makesSoundOnJump())
				{
					this.playSound(this.JumpSound, this.SoundVolume, ((this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F) * 0.8F);
				}

				this.moveStrafing = 1.0F - this.rand.nextFloat() * 2.0F;
				this.moveForward = (float)(1 * this.SlimeSize);
			}
			else
			{
				this.isJumping = false;

				if (this.onGround)
				{
					this.moveStrafing = this.moveForward = 0.0F;
				}
			}
		}

		protected internal virtual void alterSquishAmount()
		{
			this.squishAmount *= 0.6F;
		}

///    
///     <summary> * Gets the amount of time the slime needs to wait between jumps. </summary>
///     
		protected internal virtual int JumpDelay
		{
			get
			{
				return this.rand.Next(20) + 10;
			}
		}

		protected internal virtual EntitySlime createInstance()
		{
			return new EntitySlime(this.worldObj);
		}

///    
///     <summary> * Will get destroyed next tick. </summary>
///     
		public virtual void setDead()
		{
			int var1 = this.SlimeSize;

			if (!this.worldObj.isClient && var1 > 1 && this.Health <= 0.0F)
			{
				int var2 = 2 + this.rand.Next(3);

				for (int var3 = 0; var3 < var2; ++var3)
				{
					float var4 = ((float)(var3 % 2) - 0.5F) * (float)var1 / 4.0F;
					float var5 = ((float)(var3 / 2) - 0.5F) * (float)var1 / 4.0F;
					EntitySlime var6 = this.createInstance();
					var6.SlimeSize = var1 / 2;
					var6.setLocationAndAngles(this.posX + (double)var4, this.posY + 0.5D, this.posZ + (double)var5, this.rand.nextFloat() * 360.0F, 0.0F);
					this.worldObj.spawnEntityInWorld(var6);
				}
			}

			base.setDead();
		}

///    
///     <summary> * Called by a player entity when they collide with an entity </summary>
///     
		public virtual void onCollideWithPlayer(EntityPlayer p_70100_1_)
		{
			if (this.canDamagePlayer())
			{
				int var2 = this.SlimeSize;

				if (this.canEntityBeSeen(p_70100_1_) && this.getDistanceSqToEntity(p_70100_1_) < 0.6D * (double)var2 * 0.6D * (double)var2 && p_70100_1_.attackEntityFrom(DamageSource.causeMobDamage(this), (float)this.AttackStrength))
				{
					this.playSound("mob.attack", 1.0F, (this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F + 1.0F);
				}
			}
		}

///    
///     <summary> * Indicates weather the slime is able to damage the player (based upon the slime's size) </summary>
///     
		protected internal virtual bool canDamagePlayer()
		{
			return this.SlimeSize > 1;
		}

///    
///     <summary> * Gets the amount of damage dealt to the player when "attacked" by the slime. </summary>
///     
		protected internal virtual int AttackStrength
		{
			get
			{
				return this.SlimeSize;
			}
		}

///    
///     <summary> * Returns the sound this mob makes when it is hurt. </summary>
///     
		protected internal virtual string HurtSound
		{
			get
			{
				return "mob.slime." + (this.SlimeSize > 1 ? "big" : "small");
			}
		}

///    
///     <summary> * Returns the sound this mob makes on death. </summary>
///     
		protected internal virtual string DeathSound
		{
			get
			{
				return "mob.slime." + (this.SlimeSize > 1 ? "big" : "small");
			}
		}

		protected internal override Item func_146068_u()
		{
			return this.SlimeSize == 1 ? Items.slime_ball : Item.getItemById(0);
		}

///    
///     <summary> * Checks if the entity's current position is a valid location to spawn this entity. </summary>
///     
		public override bool CanSpawnHere
		{
			get
			{
				Chunk var1 = this.worldObj.getChunkFromBlockCoords(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posZ));
	
				if (this.worldObj.WorldInfo.TerrainType == WorldType.FLAT && this.rand.Next(4) != 1)
				{
					return false;
				}
				else
				{
					if (this.SlimeSize == 1 || this.worldObj.difficultySetting != EnumDifficulty.PEACEFUL)
					{
						BiomeGenBase var2 = this.worldObj.getBiomeGenForCoords(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posZ));
	
						if (var2 == BiomeGenBase.swampland && this.posY > 50.0D && this.posY < 70.0D && this.rand.nextFloat() < 0.5F && this.rand.nextFloat() < this.worldObj.CurrentMoonPhaseFactor && this.worldObj.getBlockLightValue(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ)) <= this.rand.Next(8))
						{
							return base.CanSpawnHere;
						}
	
						if (this.rand.Next(10) == 0 && var1.getRandomWithSeed(987234911L).Next(10) == 0 && this.posY < 40.0D)
						{
							return base.CanSpawnHere;
						}
					}
	
					return false;
				}
			}
		}

///    
///     <summary> * Returns the volume for the sounds this mob makes. </summary>
///     
		protected internal virtual float SoundVolume
		{
			get
			{
				return 0.4F * (float)this.SlimeSize;
			}
		}

///    
///     <summary> * The speed it takes to move the entityliving's rotationPitch through the faceEntity method. This is only currently
///     * use in wolves. </summary>
///     
		public override int VerticalFaceSpeed
		{
			get
			{
				return 0;
			}
		}

///    
///     <summary> * Returns true if the slime makes a sound when it jumps (based upon the slime's size) </summary>
///     
		protected internal virtual bool makesSoundOnJump()
		{
			return this.SlimeSize > 0;
		}

///    
///     <summary> * Returns true if the slime makes a sound when it lands after a jump (based upon the slime's size) </summary>
///     
		protected internal virtual bool makesSoundOnLand()
		{
			return this.SlimeSize > 2;
		}
	}

}