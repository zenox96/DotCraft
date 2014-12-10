using System;

namespace DotCraftCore.Entity.Item
{

	using Entity = DotCraftCore.Entity.Entity;
	using Blocks = DotCraftCore.Init.Blocks;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using DamageSource = DotCraftCore.util.DamageSource;
	using MathHelper = DotCraftCore.util.MathHelper;
	using World = DotCraftCore.world.World;
	using WorldProviderEnd = DotCraftCore.world.WorldProviderEnd;

	public class EntityEnderCrystal : Entity
	{
	/// <summary> Used to create the rotation animation when rendering the crystal.  </summary>
		public int innerRotation;
		public int health;
		private const string __OBFID = "CL_00001658";

		public EntityEnderCrystal(World p_i1698_1_) : base(p_i1698_1_)
		{
			this.preventEntitySpawning = true;
			this.setSize(2.0F, 2.0F);
			this.yOffset = this.height / 2.0F;
			this.health = 5;
			this.innerRotation = this.rand.Next(100000);
		}

		public EntityEnderCrystal(World p_i1699_1_, double p_i1699_2_, double p_i1699_4_, double p_i1699_6_) : this(p_i1699_1_)
		{
			this.setPosition(p_i1699_2_, p_i1699_4_, p_i1699_6_);
		}

///    
///     <summary> * returns if this entity triggers Block.onEntityWalking on the blocks they walk on. used for spiders and wolves to
///     * prevent them from trampling crops </summary>
///     
		protected internal override bool canTriggerWalking()
		{
			return false;
		}

		protected internal override void entityInit()
		{
			this.dataWatcher.addObject(8, Convert.ToInt32(this.health));
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			this.prevPosX = this.posX;
			this.prevPosY = this.posY;
			this.prevPosZ = this.posZ;
			++this.innerRotation;
			this.dataWatcher.updateObject(8, Convert.ToInt32(this.health));
			int var1 = MathHelper.floor_double(this.posX);
			int var2 = MathHelper.floor_double(this.posY);
			int var3 = MathHelper.floor_double(this.posZ);

			if (this.worldObj.provider is WorldProviderEnd && this.worldObj.getBlock(var1, var2, var3) != Blocks.fire)
			{
				this.worldObj.setBlock(var1, var2, var3, Blocks.fire);
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		protected internal override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		protected internal override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
		}

		public override float ShadowSize
		{
			get
			{
				return 0.0F;
			}
		}

///    
///     <summary> * Returns true if other Entities should be prevented from moving through this Entity. </summary>
///     
		public override bool canBeCollidedWith()
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
				if (!this.isDead && !this.worldObj.isClient)
				{
					this.health = 0;

					if (this.health <= 0)
					{
						this.setDead();

						if (!this.worldObj.isClient)
						{
							this.worldObj.createExplosion((Entity)null, this.posX, this.posY, this.posZ, 6.0F, true);
						}
					}
				}

				return true;
			}
		}
	}

}