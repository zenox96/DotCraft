using System;

namespace DotCraftCore.Entity.Item
{

	using Material = DotCraftCore.block.material.Material;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using DamageSource = DotCraftCore.util.DamageSource;
	using MathHelper = DotCraftCore.util.MathHelper;
	using World = DotCraftCore.world.World;

	public class EntityXPOrb : Entity
	{
///    
///     <summary> * A constantly increasing value that RenderXPOrb uses to control the colour shifting (Green / yellow) </summary>
///     
		public int xpColor;

	/// <summary> The age of the XP orb in ticks.  </summary>
		public int xpOrbAge;
		public int field_70532_c;

	/// <summary> The health of this XP orb.  </summary>
		private int xpOrbHealth = 5;

	/// <summary> This is how much XP this orb has.  </summary>
		private int xpValue;

	/// <summary> The closest EntityPlayer to this orb.  </summary>
		private EntityPlayer closestPlayer;

	/// <summary> Threshold color for tracking players  </summary>
		private int xpTargetColor;
		private const string __OBFID = "CL_00001544";

		public EntityXPOrb(World p_i1585_1_, double p_i1585_2_, double p_i1585_4_, double p_i1585_6_, int p_i1585_8_) : base(p_i1585_1_)
		{
			this.setSize(0.5F, 0.5F);
			this.yOffset = this.height / 2.0F;
			this.setPosition(p_i1585_2_, p_i1585_4_, p_i1585_6_);
			this.rotationYaw = (float)(new Random(1).NextDouble() * 360.0D);
			this.motionX = (double)((float)(new Random(2).NextDouble() * 0.20000000298023224D - 0.10000000149011612D) * 2.0F);
			this.motionY = (double)((float)(new Random(3).NextDouble() * 0.2D) * 2.0F);
			this.motionZ = (double)((float)(new Random(4).NextDouble() * 0.20000000298023224D - 0.10000000149011612D) * 2.0F);
			this.xpValue = p_i1585_8_;
		}

///    
///     <summary> * returns if this entity triggers Block.onEntityWalking on the blocks they walk on. used for spiders and wolves to
///     * prevent them from trampling crops </summary>
///     
		protected internal override bool canTriggerWalking()
		{
			return false;
		}

		public EntityXPOrb(World p_i1586_1_) : base(p_i1586_1_)
		{
			this.setSize(0.25F, 0.25F);
			this.yOffset = this.height / 2.0F;
		}

		protected internal override void entityInit()
		{
		}

		public override int getBrightnessForRender(float p_70070_1_)
		{
			float var2 = 0.5F;

			if (var2 < 0.0F)
			{
				var2 = 0.0F;
			}

			if (var2 > 1.0F)
			{
				var2 = 1.0F;
			}

			int var3 = base.getBrightnessForRender(p_70070_1_);
			int var4 = var3 & 255;
			int var5 = var3 >> 16 & 255;
			var4 += (int)(var2 * 15.0F * 16.0F);

			if (var4 > 240)
			{
				var4 = 240;
			}

			return var4 | var5 << 16;
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			base.onUpdate();

			if (this.field_70532_c > 0)
			{
				--this.field_70532_c;
			}

			this.prevPosX = this.posX;
			this.prevPosY = this.posY;
			this.prevPosZ = this.posZ;
			this.motionY -= 0.029999999329447746D;

			if (this.worldObj.getBlock(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ)).Material == Material.lava)
			{
				this.motionY = 0.20000000298023224D;
				this.motionX = (double)((this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F);
				this.motionZ = (double)((this.rand.nextFloat() - this.rand.nextFloat()) * 0.2F);
				this.playSound("random.fizz", 0.4F, 2.0F + this.rand.nextFloat() * 0.4F);
			}

			this.func_145771_j(this.posX, (this.boundingBox.minY + this.boundingBox.maxY) / 2.0D, this.posZ);
			double var1 = 8.0D;

			if (this.xpTargetColor < this.xpColor - 20 + this.EntityId % 100)
			{
				if (this.closestPlayer == null || this.closestPlayer.getDistanceSqToEntity(this) > var1 * var1)
				{
					this.closestPlayer = this.worldObj.getClosestPlayerToEntity(this, var1);
				}

				this.xpTargetColor = this.xpColor;
			}

			if (this.closestPlayer != null)
			{
				double var3 = (this.closestPlayer.posX - this.posX) / var1;
				double var5 = (this.closestPlayer.posY + (double)this.closestPlayer.EyeHeight - this.posY) / var1;
				double var7 = (this.closestPlayer.posZ - this.posZ) / var1;
				double var9 = Math.Sqrt(var3 * var3 + var5 * var5 + var7 * var7);
				double var11 = 1.0D - var9;

				if (var11 > 0.0D)
				{
					var11 *= var11;
					this.motionX += var3 / var9 * var11 * 0.1D;
					this.motionY += var5 / var9 * var11 * 0.1D;
					this.motionZ += var7 / var9 * var11 * 0.1D;
				}
			}

			this.moveEntity(this.motionX, this.motionY, this.motionZ);
			float var13 = 0.98F;

			if (this.onGround)
			{
				var13 = this.worldObj.getBlock(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.boundingBox.minY) - 1, MathHelper.floor_double(this.posZ)).slipperiness * 0.98F;
			}

			this.motionX *= (double)var13;
			this.motionY *= 0.9800000190734863D;
			this.motionZ *= (double)var13;

			if (this.onGround)
			{
				this.motionY *= -0.8999999761581421D;
			}

			++this.xpColor;
			++this.xpOrbAge;

			if (this.xpOrbAge >= 6000)
			{
				this.setDead();
			}
		}

///    
///     <summary> * Returns if this entity is in water and will end up adding the waters velocity to the entity </summary>
///     
		public override bool handleWaterMovement()
		{
			return this.worldObj.handleMaterialAcceleration(this.boundingBox, Material.water, this);
		}

///    
///     <summary> * Will deal the specified amount of damage to the entity if the entity isn't immune to fire damage. Args:
///     * amountDamage </summary>
///     
		protected internal override void dealFireDamage(int p_70081_1_)
		{
			this.attackEntityFrom(DamageSource.inFire, (float)p_70081_1_);
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
				this.setBeenAttacked();
				this.xpOrbHealth = (int)((float)this.xpOrbHealth - p_70097_2_);

				if (this.xpOrbHealth <= 0)
				{
					this.setDead();
				}

				return false;
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			p_70014_1_.setShort("Health", (short)((sbyte)this.xpOrbHealth));
			p_70014_1_.setShort("Age", (short)this.xpOrbAge);
			p_70014_1_.setShort("Value", (short)this.xpValue);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			this.xpOrbHealth = p_70037_1_.getShort("Health") & 255;
			this.xpOrbAge = p_70037_1_.getShort("Age");
			this.xpValue = p_70037_1_.getShort("Value");
		}

///    
///     <summary> * Called by a player entity when they collide with an entity </summary>
///     
		public override void onCollideWithPlayer(EntityPlayer p_70100_1_)
		{
			if (!this.worldObj.isClient)
			{
				if (this.field_70532_c == 0 && p_70100_1_.xpCooldown == 0)
				{
					p_70100_1_.xpCooldown = 2;
					this.worldObj.playSoundAtEntity(p_70100_1_, "random.orb", 0.1F, 0.5F * ((this.rand.nextFloat() - this.rand.nextFloat()) * 0.7F + 1.8F));
					p_70100_1_.onItemPickup(this, 1);
					p_70100_1_.addExperience(this.xpValue);
					this.setDead();
				}
			}
		}

///    
///     <summary> * Returns the XP value of this XP orb. </summary>
///     
		public virtual int XpValue
		{
			get
			{
				return this.xpValue;
			}
		}

///    
///     <summary> * Returns a number from 1 to 10 based on how much XP this orb is worth. This is used by RenderXPOrb to determine
///     * what texture to use. </summary>
///     
		public virtual int TextureByXP
		{
			get
			{
				return this.xpValue >= 2477 ? 10 : (this.xpValue >= 1237 ? 9 : (this.xpValue >= 617 ? 8 : (this.xpValue >= 307 ? 7 : (this.xpValue >= 149 ? 6 : (this.xpValue >= 73 ? 5 : (this.xpValue >= 37 ? 4 : (this.xpValue >= 17 ? 3 : (this.xpValue >= 7 ? 2 : (this.xpValue >= 3 ? 1 : 0)))))))));
			}
		}

///    
///     <summary> * Get a fragment of the maximum experience points value for the supplied value of experience points value. </summary>
///     
		public static int getXPSplit(int p_70527_0_)
		{
			return p_70527_0_ >= 2477 ? 2477 : (p_70527_0_ >= 1237 ? 1237 : (p_70527_0_ >= 617 ? 617 : (p_70527_0_ >= 307 ? 307 : (p_70527_0_ >= 149 ? 149 : (p_70527_0_ >= 73 ? 73 : (p_70527_0_ >= 37 ? 37 : (p_70527_0_ >= 17 ? 17 : (p_70527_0_ >= 7 ? 7 : (p_70527_0_ >= 3 ? 3 : 1)))))))));
		}

///    
///     <summary> * If returns false, the item will not inflict any damage against entities. </summary>
///     
		public override bool canAttackWithItem()
		{
			return false;
		}
	}

}