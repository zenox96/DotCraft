using System;

namespace DotCraftCore.Entity.Item
{

	using Entity = DotCraftCore.Entity.Entity;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using World = DotCraftCore.World.World;

	public class EntityTNTPrimed : Entity
	{
	/// <summary> How long the fuse is  </summary>
		public int fuse;
		private EntityLivingBase tntPlacedBy;
		private const string __OBFID = "CL_00001681";

		public EntityTNTPrimed(World p_i1729_1_) : base(p_i1729_1_)
		{
			this.preventEntitySpawning = true;
			this.setSize(0.98F, 0.98F);
			this.yOffset = this.height / 2.0F;
		}

		public EntityTNTPrimed(World p_i1730_1_, double p_i1730_2_, double p_i1730_4_, double p_i1730_6_, EntityLivingBase p_i1730_8_) : this(p_i1730_1_)
		{
			this.setPosition(p_i1730_2_, p_i1730_4_, p_i1730_6_);
			float var9 = (float)(new Random(1).NextDouble() * Math.PI * 2.0D);
			this.motionX = (double)(-((float)Math.Sin((double)var9)) * 0.02F);
			this.motionY = 0.20000000298023224D;
			this.motionZ = (double)(-((float)Math.Cos((double)var9)) * 0.02F);
			this.fuse = 80;
			this.prevPosX = p_i1730_2_;
			this.prevPosY = p_i1730_4_;
			this.prevPosZ = p_i1730_6_;
			this.tntPlacedBy = p_i1730_8_;
		}

		protected internal override void entityInit()
		{
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
///     <summary> * Returns true if other Entities should be prevented from moving through this Entity. </summary>
///     
		public override bool canBeCollidedWith()
		{
			return !this.isDead;
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			this.prevPosX = this.posX;
			this.prevPosY = this.posY;
			this.prevPosZ = this.posZ;
			this.motionY -= 0.03999999910593033D;
			this.moveEntity(this.motionX, this.motionY, this.motionZ);
			this.motionX *= 0.9800000190734863D;
			this.motionY *= 0.9800000190734863D;
			this.motionZ *= 0.9800000190734863D;

			if (this.onGround)
			{
				this.motionX *= 0.699999988079071D;
				this.motionZ *= 0.699999988079071D;
				this.motionY *= -0.5D;
			}

			if (this.fuse-- <= 0)
			{
				this.setDead();

				if (!this.worldObj.isClient)
				{
					this.explode();
				}
			}
			else
			{
				this.worldObj.spawnParticle("smoke", this.posX, this.posY + 0.5D, this.posZ, 0.0D, 0.0D, 0.0D);
			}
		}

		private void explode()
		{
			float var1 = 4.0F;
			this.worldObj.createExplosion(this, this.posX, this.posY, this.posZ, var1, true);
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		protected internal override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			p_70014_1_.setByte("Fuse", (sbyte)this.fuse);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		protected internal override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			this.fuse = p_70037_1_.getByte("Fuse");
		}

		public override float ShadowSize
		{
			get
			{
				return 0.0F;
			}
		}

///    
///     <summary> * returns null or the entityliving it was placed or ignited by </summary>
///     
		public virtual EntityLivingBase TntPlacedBy
		{
			get
			{
				return this.tntPlacedBy;
			}
		}
	}

}