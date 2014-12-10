using System;
using System.Collections;

namespace DotCraftCore.Entity
{

	using Material = DotCraftCore.block.material.Material;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using Direction = DotCraftCore.Util.Direction;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;

	public abstract class EntityHanging : Entity
	{
		private int tickCounter1;
		public int hangingDirection;
		public int field_146063_b;
		public int field_146064_c;
		public int field_146062_d;
		private const string __OBFID = "CL_00001546";

		public EntityHanging(World p_i1588_1_) : base(p_i1588_1_)
		{
			this.yOffset = 0.0F;
			this.setSize(0.5F, 0.5F);
		}

		public EntityHanging(World p_i1589_1_, int p_i1589_2_, int p_i1589_3_, int p_i1589_4_, int p_i1589_5_) : this(p_i1589_1_)
		{
			this.field_146063_b = p_i1589_2_;
			this.field_146064_c = p_i1589_3_;
			this.field_146062_d = p_i1589_4_;
		}

		protected internal override void entityInit()
		{
		}

		public virtual int Direction
		{
			set
			{
				this.hangingDirection = value;
				this.prevRotationYaw = this.rotationYaw = (float)(value * 90);
				float var2 = (float)this.WidthPixels;
				float var3 = (float)this.HeightPixels;
				float var4 = (float)this.WidthPixels;
	
				if (value != 2 && value != 0)
				{
					var2 = 0.5F;
				}
				else
				{
					var4 = 0.5F;
					this.rotationYaw = this.prevRotationYaw = (float)(Direction.rotateOpposite[value] * 90);
				}
	
				var2 /= 32.0F;
				var3 /= 32.0F;
				var4 /= 32.0F;
				float var5 = (float)this.field_146063_b + 0.5F;
				float var6 = (float)this.field_146064_c + 0.5F;
				float var7 = (float)this.field_146062_d + 0.5F;
				float var8 = 0.5625F;
	
				if (value == 2)
				{
					var7 -= var8;
				}
	
				if (value == 1)
				{
					var5 -= var8;
				}
	
				if (value == 0)
				{
					var7 += var8;
				}
	
				if (value == 3)
				{
					var5 += var8;
				}
	
				if (value == 2)
				{
					var5 -= this.func_70517_b(this.WidthPixels);
				}
	
				if (value == 1)
				{
					var7 += this.func_70517_b(this.WidthPixels);
				}
	
				if (value == 0)
				{
					var5 += this.func_70517_b(this.WidthPixels);
				}
	
				if (value == 3)
				{
					var7 -= this.func_70517_b(this.WidthPixels);
				}
	
				var6 += this.func_70517_b(this.HeightPixels);
				this.setPosition((double)var5, (double)var6, (double)var7);
				float var9 = -0.03125F;
				this.boundingBox.setBounds((double)(var5 - var2 - var9), (double)(var6 - var3 - var9), (double)(var7 - var4 - var9), (double)(var5 + var2 + var9), (double)(var6 + var3 + var9), (double)(var7 + var4 + var9));
			}
		}

		private float func_70517_b(int p_70517_1_)
		{
			return p_70517_1_ == 32 ? 0.5F : (p_70517_1_ == 64 ? 0.5F : 0.0F);
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			this.prevPosX = this.posX;
			this.prevPosY = this.posY;
			this.prevPosZ = this.posZ;

			if (this.tickCounter1++ == 100 && !this.worldObj.isClient)
			{
				this.tickCounter1 = 0;

				if (!this.isDead && !this.onValidSurface())
				{
					this.setDead();
					this.onBroken((Entity)null);
				}
			}
		}

///    
///     <summary> * checks to make sure painting can be placed there </summary>
///     
		public virtual bool onValidSurface()
		{
			if (!this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox).Empty)
			{
				return false;
			}
			else
			{
				int var1 = Math.Max(1, this.WidthPixels / 16);
				int var2 = Math.Max(1, this.HeightPixels / 16);
				int var3 = this.field_146063_b;
				int var4 = this.field_146064_c;
				int var5 = this.field_146062_d;

				if (this.hangingDirection == 2)
				{
					var3 = MathHelper.floor_double(this.posX - (double)((float)this.WidthPixels / 32.0F));
				}

				if (this.hangingDirection == 1)
				{
					var5 = MathHelper.floor_double(this.posZ - (double)((float)this.WidthPixels / 32.0F));
				}

				if (this.hangingDirection == 0)
				{
					var3 = MathHelper.floor_double(this.posX - (double)((float)this.WidthPixels / 32.0F));
				}

				if (this.hangingDirection == 3)
				{
					var5 = MathHelper.floor_double(this.posZ - (double)((float)this.WidthPixels / 32.0F));
				}

				var4 = MathHelper.floor_double(this.posY - (double)((float)this.HeightPixels / 32.0F));

				for (int var6 = 0; var6 < var1; ++var6)
				{
					for (int var7 = 0; var7 < var2; ++var7)
					{
						Material var8;

						if (this.hangingDirection != 2 && this.hangingDirection != 0)
						{
							var8 = this.worldObj.getBlock(this.field_146063_b, var4 + var7, var5 + var6).Material;
						}
						else
						{
							var8 = this.worldObj.getBlock(var3 + var6, var4 + var7, this.field_146062_d).Material;
						}

						if (!var8.Solid)
						{
							return false;
						}
					}
				}

				IList var9 = this.worldObj.getEntitiesWithinAABBExcludingEntity(this, this.boundingBox);
				IEnumerator var10 = var9.GetEnumerator();
				Entity var11;

				do
				{
					if (!var10.MoveNext())
					{
						return true;
					}

					var11 = (Entity)var10.Current;
				}
				while (!(var11 is EntityHanging));

				return false;
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
///     <summary> * Called when a player attacks an entity. If this returns true the attack will not happen. </summary>
///     
		public override bool hitByEntity(Entity p_85031_1_)
		{
			return p_85031_1_ is EntityPlayer ? this.attackEntityFrom(DamageSource.causePlayerDamage((EntityPlayer)p_85031_1_), 0.0F) : false;
		}

		public override void func_145781_i(int p_145781_1_)
		{
			this.worldObj.func_147450_X();
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
					this.setDead();
					this.setBeenAttacked();
					this.onBroken(p_70097_1_.Entity);
				}

				return true;
			}
		}

///    
///     <summary> * Tries to moves the entity by the passed in displacement. Args: x, y, z </summary>
///     
		public override void moveEntity(double p_70091_1_, double p_70091_3_, double p_70091_5_)
		{
			if (!this.worldObj.isClient && !this.isDead && p_70091_1_ * p_70091_1_ + p_70091_3_ * p_70091_3_ + p_70091_5_ * p_70091_5_ > 0.0D)
			{
				this.setDead();
				this.onBroken((Entity)null);
			}
		}

///    
///     <summary> * Adds to the current velocity of the entity. Args: x, y, z </summary>
///     
		public override void addVelocity(double p_70024_1_, double p_70024_3_, double p_70024_5_)
		{
			if (!this.worldObj.isClient && !this.isDead && p_70024_1_ * p_70024_1_ + p_70024_3_ * p_70024_3_ + p_70024_5_ * p_70024_5_ > 0.0D)
			{
				this.setDead();
				this.onBroken((Entity)null);
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			p_70014_1_.setByte("Direction", (sbyte)this.hangingDirection);
			p_70014_1_.setInteger("TileX", this.field_146063_b);
			p_70014_1_.setInteger("TileY", this.field_146064_c);
			p_70014_1_.setInteger("TileZ", this.field_146062_d);

			switch (this.hangingDirection)
			{
				case 0:
					p_70014_1_.setByte("Dir", (sbyte)2);
					break;

				case 1:
					p_70014_1_.setByte("Dir", (sbyte)1);
					break;

				case 2:
					p_70014_1_.setByte("Dir", (sbyte)0);
					break;

				case 3:
					p_70014_1_.setByte("Dir", (sbyte)3);
				break;
			}
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			if (p_70037_1_.func_150297_b("Direction", 99))
			{
				this.hangingDirection = p_70037_1_.getByte("Direction");
			}
			else
			{
				switch (p_70037_1_.getByte("Dir"))
				{
					case 0:
						this.hangingDirection = 2;
						break;

					case 1:
						this.hangingDirection = 1;
						break;

					case 2:
						this.hangingDirection = 0;
						break;

					case 3:
						this.hangingDirection = 3;
					break;
				}
			}

			this.field_146063_b = p_70037_1_.getInteger("TileX");
			this.field_146064_c = p_70037_1_.getInteger("TileY");
			this.field_146062_d = p_70037_1_.getInteger("TileZ");
			this.Direction = this.hangingDirection;
		}

		public abstract int WidthPixels {get;}

		public abstract int HeightPixels {get;}

///    
///     <summary> * Called when this entity is broken. Entity parameter may be null. </summary>
///     
		public abstract void onBroken(Entity p_110128_1_);

		protected internal override bool shouldSetPosAfterLoading()
		{
			return false;
		}
	}

}