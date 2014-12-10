using System;
using System.Collections;

namespace DotCraftCore.Entity.Item
{

	using Block = DotCraftCore.block.Block;
	using BlockRailBase = DotCraftCore.block.BlockRailBase;
	using Material = DotCraftCore.block.material.Material;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EntityMinecartCommandBlock = DotCraftCore.Entity.EntityMinecartCommandBlock;
	using EntityMinecartMobSpawner = DotCraftCore.Entity.AI.EntityMinecartMobSpawner;
	using EntityIronGolem = DotCraftCore.Entity.Monster.EntityIronGolem;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using Vec3 = DotCraftCore.Util.Vec3;
	using World = DotCraftCore.World.World;
	using WorldServer = DotCraftCore.World.WorldServer;

	public abstract class EntityMinecart : Entity
	{
		private bool isInReverse;
		private string entityName;

	/// <summary> Minecart rotational logic matrix  </summary>
		private static readonly int[][][] matrix = new int[][][] {{{0, 0, -1}, {0, 0, 1}}, {{ -1, 0, 0}, {1, 0, 0}}, {{ -1, -1, 0}, {1, 0, 0}}, {{ -1, 0, 0}, {1, -1, 0}}, {{0, 0, -1}, {0, -1, 1}}, {{0, -1, -1}, {0, 0, 1}}, {{0, 0, 1}, {1, 0, 0}}, {{0, 0, 1}, { -1, 0, 0}}, {{0, 0, -1}, { -1, 0, 0}}, {{0, 0, -1}, {1, 0, 0}}};

	/// <summary> appears to be the progress of the turn  </summary>
		private int turnProgress;
		private double minecartX;
		private double minecartY;
		private double minecartZ;
		private double minecartYaw;
		private double minecartPitch;
		private double velocityX;
		private double velocityY;
		private double velocityZ;
		private const string __OBFID = "CL_00001670";

		public EntityMinecart(World p_i1712_1_) : base(p_i1712_1_)
		{
			this.preventEntitySpawning = true;
			this.setSize(0.98F, 0.7F);
			this.yOffset = this.height / 2.0F;
		}

///    
///     <summary> * Creates a new minecart of the specified type in the specified location in the given world. par0World - world to
///     * create the minecart in, double par1,par3,par5 represent x,y,z respectively. int par7 specifies the type: 1 for
///     * MinecartChest, 2 for MinecartFurnace, 3 for MinecartTNT, 4 for MinecartMobSpawner, 5 for MinecartHopper and 0 for
///     * a standard empty minecart </summary>
///     
		public static EntityMinecart createMinecart(World p_94090_0_, double p_94090_1_, double p_94090_3_, double p_94090_5_, int p_94090_7_)
		{
			switch (p_94090_7_)
			{
				case 1:
					return new EntityMinecartChest(p_94090_0_, p_94090_1_, p_94090_3_, p_94090_5_);

				case 2:
					return new EntityMinecartFurnace(p_94090_0_, p_94090_1_, p_94090_3_, p_94090_5_);

				case 3:
					return new EntityMinecartTNT(p_94090_0_, p_94090_1_, p_94090_3_, p_94090_5_);

				case 4:
					return new EntityMinecartMobSpawner(p_94090_0_, p_94090_1_, p_94090_3_, p_94090_5_);

				case 5:
					return new EntityMinecartHopper(p_94090_0_, p_94090_1_, p_94090_3_, p_94090_5_);

				case 6:
					return new EntityMinecartCommandBlock(p_94090_0_, p_94090_1_, p_94090_3_, p_94090_5_);

				default:
					return new EntityMinecartEmpty(p_94090_0_, p_94090_1_, p_94090_3_, p_94090_5_);
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

		protected internal override void entityInit()
		{
			this.dataWatcher.addObject(17, new int?(0));
			this.dataWatcher.addObject(18, new int?(1));
			this.dataWatcher.addObject(19, new float?(0.0F));
			this.dataWatcher.addObject(20, new int?(0));
			this.dataWatcher.addObject(21, new int?(6));
			this.dataWatcher.addObject(22, Convert.ToByte((sbyte)0));
		}

///    
///     <summary> * Returns a boundingBox used to collide the entity with other entities and blocks. This enables the entity to be
///     * pushable on contact, like boats or minecarts. </summary>
///     
		public override AxisAlignedBB getCollisionBox(Entity p_70114_1_)
		{
			return p_70114_1_.canBePushed() ? p_70114_1_.boundingBox : null;
		}

///    
///     <summary> * returns the bounding box for this entity </summary>
///     
		public override AxisAlignedBB BoundingBox
		{
			get
			{
				return null;
			}
		}

///    
///     <summary> * Returns true if this entity should push and be pushed by other entities when colliding. </summary>
///     
		public override bool canBePushed()
		{
			return true;
		}

		public EntityMinecart(World p_i1713_1_, double p_i1713_2_, double p_i1713_4_, double p_i1713_6_) : this(p_i1713_1_)
		{
			this.setPosition(p_i1713_2_, p_i1713_4_, p_i1713_6_);
			this.motionX = 0.0D;
			this.motionY = 0.0D;
			this.motionZ = 0.0D;
			this.prevPosX = p_i1713_2_;
			this.prevPosY = p_i1713_4_;
			this.prevPosZ = p_i1713_6_;
		}

///    
///     <summary> * Returns the Y offset from the entity's position for any entity riding this one. </summary>
///     
		public override double MountedYOffset
		{
			get
			{
				return (double)this.height * 0.0D - 0.30000001192092896D;
			}
		}

///    
///     <summary> * Called when the entity is attacked. </summary>
///     
		public override bool attackEntityFrom(DamageSource p_70097_1_, float p_70097_2_)
		{
			if (!this.worldObj.isClient && !this.isDead)
			{
				if (this.EntityInvulnerable)
				{
					return false;
				}
				else
				{
					this.RollingDirection = -this.RollingDirection;
					this.RollingAmplitude = 10;
					this.setBeenAttacked();
					this.Damage = this.Damage + p_70097_2_ * 10.0F;
					bool var3 = p_70097_1_.Entity is EntityPlayer && ((EntityPlayer)p_70097_1_.Entity).capabilities.isCreativeMode;

					if (var3 || this.Damage > 40.0F)
					{
						if (this.riddenByEntity != null)
						{
							this.riddenByEntity.mountEntity(this);
						}

						if (var3 && !this.InventoryNameLocalized)
						{
							this.setDead();
						}
						else
						{
							this.killMinecart(p_70097_1_);
						}
					}

					return true;
				}
			}
			else
			{
				return true;
			}
		}

		public virtual void killMinecart(DamageSource p_94095_1_)
		{
			this.setDead();
			ItemStack var2 = new ItemStack(Items.minecart, 1);

			if (this.entityName != null)
			{
				var2.StackDisplayName = this.entityName;
			}

			this.entityDropItem(var2, 0.0F);
		}

///    
///     <summary> * Setups the entity to do the hurt animation. Only used by packets in multiplayer. </summary>
///     
		public override void performHurtAnimation()
		{
			this.RollingDirection = -this.RollingDirection;
			this.RollingAmplitude = 10;
			this.Damage = this.Damage + this.Damage * 10.0F;
		}

///    
///     <summary> * Returns true if other Entities should be prevented from moving through this Entity. </summary>
///     
		public override bool canBeCollidedWith()
		{
			return !this.isDead;
		}

///    
///     <summary> * Will get destroyed next tick. </summary>
///     
		public override void setDead()
		{
			base.setDead();
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public override void onUpdate()
		{
			if (this.RollingAmplitude > 0)
			{
				this.RollingAmplitude = this.RollingAmplitude - 1;
			}

			if (this.Damage > 0.0F)
			{
				this.Damage = this.Damage - 1.0F;
			}

			if (this.posY < -64.0D)
			{
				this.kill();
			}

			int var2;

			if (!this.worldObj.isClient && this.worldObj is WorldServer)
			{
				this.worldObj.theProfiler.startSection("portal");
				MinecraftServer var1 = ((WorldServer)this.worldObj).func_73046_m();
				var2 = this.MaxInPortalTime;

				if (this.inPortal)
				{
					if (var1.AllowNether)
					{
						if (this.ridingEntity == null && this.portalCounter++ >= var2)
						{
							this.portalCounter = var2;
							this.timeUntilPortal = this.PortalCooldown;
							sbyte var3;

							if (this.worldObj.provider.dimensionId == -1)
							{
								var3 = 0;
							}
							else
							{
								var3 = -1;
							}

							this.travelToDimension(var3);
						}

						this.inPortal = false;
					}
				}
				else
				{
					if (this.portalCounter > 0)
					{
						this.portalCounter -= 4;
					}

					if (this.portalCounter < 0)
					{
						this.portalCounter = 0;
					}
				}

				if (this.timeUntilPortal > 0)
				{
					--this.timeUntilPortal;
				}

				this.worldObj.theProfiler.endSection();
			}

			if (this.worldObj.isClient)
			{
				if (this.turnProgress > 0)
				{
					double var19 = this.posX + (this.minecartX - this.posX) / (double)this.turnProgress;
					double var21 = this.posY + (this.minecartY - this.posY) / (double)this.turnProgress;
					double var5 = this.posZ + (this.minecartZ - this.posZ) / (double)this.turnProgress;
					double var7 = MathHelper.wrapAngleTo180_double(this.minecartYaw - (double)this.rotationYaw);
					this.rotationYaw = (float)((double)this.rotationYaw + var7 / (double)this.turnProgress);
					this.rotationPitch = (float)((double)this.rotationPitch + (this.minecartPitch - (double)this.rotationPitch) / (double)this.turnProgress);
					--this.turnProgress;
					this.setPosition(var19, var21, var5);
					this.setRotation(this.rotationYaw, this.rotationPitch);
				}
				else
				{
					this.setPosition(this.posX, this.posY, this.posZ);
					this.setRotation(this.rotationYaw, this.rotationPitch);
				}
			}
			else
			{
				this.prevPosX = this.posX;
				this.prevPosY = this.posY;
				this.prevPosZ = this.posZ;
				this.motionY -= 0.03999999910593033D;
				int var18 = MathHelper.floor_double(this.posX);
				var2 = MathHelper.floor_double(this.posY);
				int var20 = MathHelper.floor_double(this.posZ);

				if (BlockRailBase.func_150049_b_(this.worldObj, var18, var2 - 1, var20))
				{
					--var2;
				}

				double var4 = 0.4D;
				double var6 = 0.0078125D;
				Block var8 = this.worldObj.getBlock(var18, var2, var20);

				if (BlockRailBase.func_150051_a(var8))
				{
					int var9 = this.worldObj.getBlockMetadata(var18, var2, var20);
					this.func_145821_a(var18, var2, var20, var4, var6, var8, var9);

					if (var8 == Blocks.activator_rail)
					{
						this.onActivatorRailPass(var18, var2, var20, (var9 & 8) != 0);
					}
				}
				else
				{
					this.func_94088_b(var4);
				}

				this.func_145775_I();
				this.rotationPitch = 0.0F;
				double var22 = this.prevPosX - this.posX;
				double var11 = this.prevPosZ - this.posZ;

				if (var22 * var22 + var11 * var11 > 0.001D)
				{
					this.rotationYaw = (float)(Math.Atan2(var11, var22) * 180.0D / Math.PI);

					if (this.isInReverse)
					{
						this.rotationYaw += 180.0F;
					}
				}

				double var13 = (double)MathHelper.wrapAngleTo180_float(this.rotationYaw - this.prevRotationYaw);

				if (var13 < -170.0D || var13 >= 170.0D)
				{
					this.rotationYaw += 180.0F;
					this.isInReverse = !this.isInReverse;
				}

				this.setRotation(this.rotationYaw, this.rotationPitch);
				IList var15 = this.worldObj.getEntitiesWithinAABBExcludingEntity(this, this.boundingBox.expand(0.20000000298023224D, 0.0D, 0.20000000298023224D));

				if (var15 != null && !var15.Count == 0)
				{
					for (int var16 = 0; var16 < var15.Count; ++var16)
					{
						Entity var17 = (Entity)var15[var16];

						if (var17 != this.riddenByEntity && var17.canBePushed() && var17 is EntityMinecart)
						{
							var17.applyEntityCollision(this);
						}
					}
				}

				if (this.riddenByEntity != null && this.riddenByEntity.isDead)
				{
					if (this.riddenByEntity.ridingEntity == this)
					{
						this.riddenByEntity.ridingEntity = null;
					}

					this.riddenByEntity = null;
				}
			}
		}

///    
///     <summary> * Called every tick the minecart is on an activator rail. Args: x, y, z, is the rail receiving power </summary>
///     
		public virtual void onActivatorRailPass(int p_96095_1_, int p_96095_2_, int p_96095_3_, bool p_96095_4_)
		{
		}

		protected internal virtual void func_94088_b(double p_94088_1_)
		{
			if (this.motionX < -p_94088_1_)
			{
				this.motionX = -p_94088_1_;
			}

			if (this.motionX > p_94088_1_)
			{
				this.motionX = p_94088_1_;
			}

			if (this.motionZ < -p_94088_1_)
			{
				this.motionZ = -p_94088_1_;
			}

			if (this.motionZ > p_94088_1_)
			{
				this.motionZ = p_94088_1_;
			}

			if (this.onGround)
			{
				this.motionX *= 0.5D;
				this.motionY *= 0.5D;
				this.motionZ *= 0.5D;
			}

			this.moveEntity(this.motionX, this.motionY, this.motionZ);

			if (!this.onGround)
			{
				this.motionX *= 0.949999988079071D;
				this.motionY *= 0.949999988079071D;
				this.motionZ *= 0.949999988079071D;
			}
		}

		protected internal virtual void func_145821_a(int p_145821_1_, int p_145821_2_, int p_145821_3_, double p_145821_4_, double p_145821_6_, Block p_145821_8_, int p_145821_9_)
		{
			this.fallDistance = 0.0F;
			Vec3 var10 = this.func_70489_a(this.posX, this.posY, this.posZ);
			this.posY = (double)p_145821_2_;
			bool var11 = false;
			bool var12 = false;

			if (p_145821_8_ == Blocks.golden_rail)
			{
				var11 = (p_145821_9_ & 8) != 0;
				var12 = !var11;
			}

			if (((BlockRailBase)p_145821_8_).func_150050_e())
			{
				p_145821_9_ &= 7;
			}

			if (p_145821_9_ >= 2 && p_145821_9_ <= 5)
			{
				this.posY = (double)(p_145821_2_ + 1);
			}

			if (p_145821_9_ == 2)
			{
				this.motionX -= p_145821_6_;
			}

			if (p_145821_9_ == 3)
			{
				this.motionX += p_145821_6_;
			}

			if (p_145821_9_ == 4)
			{
				this.motionZ += p_145821_6_;
			}

			if (p_145821_9_ == 5)
			{
				this.motionZ -= p_145821_6_;
			}

			int[][] var13 = matrix[p_145821_9_];
			double var14 = (double)(var13[1][0] - var13[0][0]);
			double var16 = (double)(var13[1][2] - var13[0][2]);
			double var18 = Math.Sqrt(var14 * var14 + var16 * var16);
			double var20 = this.motionX * var14 + this.motionZ * var16;

			if (var20 < 0.0D)
			{
				var14 = -var14;
				var16 = -var16;
			}

			double var22 = Math.Sqrt(this.motionX * this.motionX + this.motionZ * this.motionZ);

			if (var22 > 2.0D)
			{
				var22 = 2.0D;
			}

			this.motionX = var22 * var14 / var18;
			this.motionZ = var22 * var16 / var18;
			double var24;
			double var26;
			double var28;
			double var30;

			if (this.riddenByEntity != null && this.riddenByEntity is EntityLivingBase)
			{
				var24 = (double)((EntityLivingBase)this.riddenByEntity).moveForward;

				if (var24 > 0.0D)
				{
					var26 = -Math.Sin((double)(this.riddenByEntity.rotationYaw * (float)Math.PI / 180.0F));
					var28 = Math.Cos((double)(this.riddenByEntity.rotationYaw * (float)Math.PI / 180.0F));
					var30 = this.motionX * this.motionX + this.motionZ * this.motionZ;

					if (var30 < 0.01D)
					{
						this.motionX += var26 * 0.1D;
						this.motionZ += var28 * 0.1D;
						var12 = false;
					}
				}
			}

			if (var12)
			{
				var24 = Math.Sqrt(this.motionX * this.motionX + this.motionZ * this.motionZ);

				if (var24 < 0.03D)
				{
					this.motionX *= 0.0D;
					this.motionY *= 0.0D;
					this.motionZ *= 0.0D;
				}
				else
				{
					this.motionX *= 0.5D;
					this.motionY *= 0.0D;
					this.motionZ *= 0.5D;
				}
			}

			var24 = 0.0D;
			var26 = (double)p_145821_1_ + 0.5D + (double)var13[0][0] * 0.5D;
			var28 = (double)p_145821_3_ + 0.5D + (double)var13[0][2] * 0.5D;
			var30 = (double)p_145821_1_ + 0.5D + (double)var13[1][0] * 0.5D;
			double var32 = (double)p_145821_3_ + 0.5D + (double)var13[1][2] * 0.5D;
			var14 = var30 - var26;
			var16 = var32 - var28;
			double var34;
			double var36;

			if (var14 == 0.0D)
			{
				this.posX = (double)p_145821_1_ + 0.5D;
				var24 = this.posZ - (double)p_145821_3_;
			}
			else if (var16 == 0.0D)
			{
				this.posZ = (double)p_145821_3_ + 0.5D;
				var24 = this.posX - (double)p_145821_1_;
			}
			else
			{
				var34 = this.posX - var26;
				var36 = this.posZ - var28;
				var24 = (var34 * var14 + var36 * var16) * 2.0D;
			}

			this.posX = var26 + var14 * var24;
			this.posZ = var28 + var16 * var24;
			this.setPosition(this.posX, this.posY + (double)this.yOffset, this.posZ);
			var34 = this.motionX;
			var36 = this.motionZ;

			if (this.riddenByEntity != null)
			{
				var34 *= 0.75D;
				var36 *= 0.75D;
			}

			if (var34 < -p_145821_4_)
			{
				var34 = -p_145821_4_;
			}

			if (var34 > p_145821_4_)
			{
				var34 = p_145821_4_;
			}

			if (var36 < -p_145821_4_)
			{
				var36 = -p_145821_4_;
			}

			if (var36 > p_145821_4_)
			{
				var36 = p_145821_4_;
			}

			this.moveEntity(var34, 0.0D, var36);

			if (var13[0][1] != 0 && MathHelper.floor_double(this.posX) - p_145821_1_ == var13[0][0] && MathHelper.floor_double(this.posZ) - p_145821_3_ == var13[0][2])
			{
				this.setPosition(this.posX, this.posY + (double)var13[0][1], this.posZ);
			}
			else if (var13[1][1] != 0 && MathHelper.floor_double(this.posX) - p_145821_1_ == var13[1][0] && MathHelper.floor_double(this.posZ) - p_145821_3_ == var13[1][2])
			{
				this.setPosition(this.posX, this.posY + (double)var13[1][1], this.posZ);
			}

			this.applyDrag();
			Vec3 var38 = this.func_70489_a(this.posX, this.posY, this.posZ);

			if (var38 != null && var10 != null)
			{
				double var39 = (var10.yCoord - var38.yCoord) * 0.05D;
				var22 = Math.Sqrt(this.motionX * this.motionX + this.motionZ * this.motionZ);

				if (var22 > 0.0D)
				{
					this.motionX = this.motionX / var22 * (var22 + var39);
					this.motionZ = this.motionZ / var22 * (var22 + var39);
				}

				this.setPosition(this.posX, var38.yCoord, this.posZ);
			}

			int var45 = MathHelper.floor_double(this.posX);
			int var40 = MathHelper.floor_double(this.posZ);

			if (var45 != p_145821_1_ || var40 != p_145821_3_)
			{
				var22 = Math.Sqrt(this.motionX * this.motionX + this.motionZ * this.motionZ);
				this.motionX = var22 * (double)(var45 - p_145821_1_);
				this.motionZ = var22 * (double)(var40 - p_145821_3_);
			}

			if (var11)
			{
				double var41 = Math.Sqrt(this.motionX * this.motionX + this.motionZ * this.motionZ);

				if (var41 > 0.01D)
				{
					double var43 = 0.06D;
					this.motionX += this.motionX / var41 * var43;
					this.motionZ += this.motionZ / var41 * var43;
				}
				else if (p_145821_9_ == 1)
				{
					if (this.worldObj.getBlock(p_145821_1_ - 1, p_145821_2_, p_145821_3_).NormalCube)
					{
						this.motionX = 0.02D;
					}
					else if (this.worldObj.getBlock(p_145821_1_ + 1, p_145821_2_, p_145821_3_).NormalCube)
					{
						this.motionX = -0.02D;
					}
				}
				else if (p_145821_9_ == 0)
				{
					if (this.worldObj.getBlock(p_145821_1_, p_145821_2_, p_145821_3_ - 1).NormalCube)
					{
						this.motionZ = 0.02D;
					}
					else if (this.worldObj.getBlock(p_145821_1_, p_145821_2_, p_145821_3_ + 1).NormalCube)
					{
						this.motionZ = -0.02D;
					}
				}
			}
		}

		protected internal virtual void applyDrag()
		{
			if (this.riddenByEntity != null)
			{
				this.motionX *= 0.996999979019165D;
				this.motionY *= 0.0D;
				this.motionZ *= 0.996999979019165D;
			}
			else
			{
				this.motionX *= 0.9599999785423279D;
				this.motionY *= 0.0D;
				this.motionZ *= 0.9599999785423279D;
			}
		}

		public virtual Vec3 func_70495_a(double p_70495_1_, double p_70495_3_, double p_70495_5_, double p_70495_7_)
		{
			int var9 = MathHelper.floor_double(p_70495_1_);
			int var10 = MathHelper.floor_double(p_70495_3_);
			int var11 = MathHelper.floor_double(p_70495_5_);

			if (BlockRailBase.func_150049_b_(this.worldObj, var9, var10 - 1, var11))
			{
				--var10;
			}

			Block var12 = this.worldObj.getBlock(var9, var10, var11);

			if (!BlockRailBase.func_150051_a(var12))
			{
				return null;
			}
			else
			{
				int var13 = this.worldObj.getBlockMetadata(var9, var10, var11);

				if (((BlockRailBase)var12).func_150050_e())
				{
					var13 &= 7;
				}

				p_70495_3_ = (double)var10;

				if (var13 >= 2 && var13 <= 5)
				{
					p_70495_3_ = (double)(var10 + 1);
				}

				int[][] var14 = matrix[var13];
				double var15 = (double)(var14[1][0] - var14[0][0]);
				double var17 = (double)(var14[1][2] - var14[0][2]);
				double var19 = Math.Sqrt(var15 * var15 + var17 * var17);
				var15 /= var19;
				var17 /= var19;
				p_70495_1_ += var15 * p_70495_7_;
				p_70495_5_ += var17 * p_70495_7_;

				if (var14[0][1] != 0 && MathHelper.floor_double(p_70495_1_) - var9 == var14[0][0] && MathHelper.floor_double(p_70495_5_) - var11 == var14[0][2])
				{
					p_70495_3_ += (double)var14[0][1];
				}
				else if (var14[1][1] != 0 && MathHelper.floor_double(p_70495_1_) - var9 == var14[1][0] && MathHelper.floor_double(p_70495_5_) - var11 == var14[1][2])
				{
					p_70495_3_ += (double)var14[1][1];
				}

				return this.func_70489_a(p_70495_1_, p_70495_3_, p_70495_5_);
			}
		}

		public virtual Vec3 func_70489_a(double p_70489_1_, double p_70489_3_, double p_70489_5_)
		{
			int var7 = MathHelper.floor_double(p_70489_1_);
			int var8 = MathHelper.floor_double(p_70489_3_);
			int var9 = MathHelper.floor_double(p_70489_5_);

			if (BlockRailBase.func_150049_b_(this.worldObj, var7, var8 - 1, var9))
			{
				--var8;
			}

			Block var10 = this.worldObj.getBlock(var7, var8, var9);

			if (BlockRailBase.func_150051_a(var10))
			{
				int var11 = this.worldObj.getBlockMetadata(var7, var8, var9);
				p_70489_3_ = (double)var8;

				if (((BlockRailBase)var10).func_150050_e())
				{
					var11 &= 7;
				}

				if (var11 >= 2 && var11 <= 5)
				{
					p_70489_3_ = (double)(var8 + 1);
				}

				int[][] var12 = matrix[var11];
				double var13 = 0.0D;
				double var15 = (double)var7 + 0.5D + (double)var12[0][0] * 0.5D;
				double var17 = (double)var8 + 0.5D + (double)var12[0][1] * 0.5D;
				double var19 = (double)var9 + 0.5D + (double)var12[0][2] * 0.5D;
				double var21 = (double)var7 + 0.5D + (double)var12[1][0] * 0.5D;
				double var23 = (double)var8 + 0.5D + (double)var12[1][1] * 0.5D;
				double var25 = (double)var9 + 0.5D + (double)var12[1][2] * 0.5D;
				double var27 = var21 - var15;
				double var29 = (var23 - var17) * 2.0D;
				double var31 = var25 - var19;

				if (var27 == 0.0D)
				{
					p_70489_1_ = (double)var7 + 0.5D;
					var13 = p_70489_5_ - (double)var9;
				}
				else if (var31 == 0.0D)
				{
					p_70489_5_ = (double)var9 + 0.5D;
					var13 = p_70489_1_ - (double)var7;
				}
				else
				{
					double var33 = p_70489_1_ - var15;
					double var35 = p_70489_5_ - var19;
					var13 = (var33 * var27 + var35 * var31) * 2.0D;
				}

				p_70489_1_ = var15 + var27 * var13;
				p_70489_3_ = var17 + var29 * var13;
				p_70489_5_ = var19 + var31 * var13;

				if (var29 < 0.0D)
				{
					++p_70489_3_;
				}

				if (var29 > 0.0D)
				{
					p_70489_3_ += 0.5D;
				}

				return Vec3.createVectorHelper(p_70489_1_, p_70489_3_, p_70489_5_);
			}
			else
			{
				return null;
			}
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		protected internal override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			if (p_70037_1_.getBoolean("CustomDisplayTile"))
			{
				this.func_145819_k(p_70037_1_.getInteger("DisplayTile"));
				this.DisplayTileData = p_70037_1_.getInteger("DisplayData");
				this.DisplayTileOffset = p_70037_1_.getInteger("DisplayOffset");
			}

			if (p_70037_1_.func_150297_b("CustomName", 8) && p_70037_1_.getString("CustomName").Length > 0)
			{
				this.entityName = p_70037_1_.getString("CustomName");
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		protected internal override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			if (this.hasDisplayTile())
			{
				p_70014_1_.setBoolean("CustomDisplayTile", true);
				p_70014_1_.setInteger("DisplayTile", this.func_145820_n().Material == Material.air ? 0 : Block.getIdFromBlock(this.func_145820_n()));
				p_70014_1_.setInteger("DisplayData", this.DisplayTileData);
				p_70014_1_.setInteger("DisplayOffset", this.DisplayTileOffset);
			}

			if (this.entityName != null && this.entityName.Length > 0)
			{
				p_70014_1_.setString("CustomName", this.entityName);
			}
		}

		public override float ShadowSize
		{
			get
			{
				return 0.0F;
			}
		}

///    
///     <summary> * Applies a velocity to each of the entities pushing them away from each other. Args: entity </summary>
///     
		public override void applyEntityCollision(Entity p_70108_1_)
		{
			if (!this.worldObj.isClient)
			{
				if (p_70108_1_ != this.riddenByEntity)
				{
					if (p_70108_1_ is EntityLivingBase && !(p_70108_1_ is EntityPlayer) && !(p_70108_1_ is EntityIronGolem) && this.MinecartType == 0 && this.motionX * this.motionX + this.motionZ * this.motionZ > 0.01D && this.riddenByEntity == null && p_70108_1_.ridingEntity == null)
					{
						p_70108_1_.mountEntity(this);
					}

					double var2 = p_70108_1_.posX - this.posX;
					double var4 = p_70108_1_.posZ - this.posZ;
					double var6 = var2 * var2 + var4 * var4;

					if (var6 >= 9.999999747378752E-5D)
					{
						var6 = (double)MathHelper.sqrt_double(var6);
						var2 /= var6;
						var4 /= var6;
						double var8 = 1.0D / var6;

						if (var8 > 1.0D)
						{
							var8 = 1.0D;
						}

						var2 *= var8;
						var4 *= var8;
						var2 *= 0.10000000149011612D;
						var4 *= 0.10000000149011612D;
						var2 *= (double)(1.0F - this.entityCollisionReduction);
						var4 *= (double)(1.0F - this.entityCollisionReduction);
						var2 *= 0.5D;
						var4 *= 0.5D;

						if (p_70108_1_ is EntityMinecart)
						{
							double var10 = p_70108_1_.posX - this.posX;
							double var12 = p_70108_1_.posZ - this.posZ;
							Vec3 var14 = Vec3.createVectorHelper(var10, 0.0D, var12).normalize();
							Vec3 var15 = Vec3.createVectorHelper((double)MathHelper.cos(this.rotationYaw * (float)Math.PI / 180.0F), 0.0D, (double)MathHelper.sin(this.rotationYaw * (float)Math.PI / 180.0F)).normalize();
							double var16 = Math.Abs(var14.dotProduct(var15));

							if (var16 < 0.800000011920929D)
							{
								return;
							}

							double var18 = p_70108_1_.motionX + this.motionX;
							double var20 = p_70108_1_.motionZ + this.motionZ;

							if (((EntityMinecart)p_70108_1_).MinecartType == 2 && this.MinecartType != 2)
							{
								this.motionX *= 0.20000000298023224D;
								this.motionZ *= 0.20000000298023224D;
								this.addVelocity(p_70108_1_.motionX - var2, 0.0D, p_70108_1_.motionZ - var4);
								p_70108_1_.motionX *= 0.949999988079071D;
								p_70108_1_.motionZ *= 0.949999988079071D;
							}
							else if (((EntityMinecart)p_70108_1_).MinecartType != 2 && this.MinecartType == 2)
							{
								p_70108_1_.motionX *= 0.20000000298023224D;
								p_70108_1_.motionZ *= 0.20000000298023224D;
								p_70108_1_.addVelocity(this.motionX + var2, 0.0D, this.motionZ + var4);
								this.motionX *= 0.949999988079071D;
								this.motionZ *= 0.949999988079071D;
							}
							else
							{
								var18 /= 2.0D;
								var20 /= 2.0D;
								this.motionX *= 0.20000000298023224D;
								this.motionZ *= 0.20000000298023224D;
								this.addVelocity(var18 - var2, 0.0D, var20 - var4);
								p_70108_1_.motionX *= 0.20000000298023224D;
								p_70108_1_.motionZ *= 0.20000000298023224D;
								p_70108_1_.addVelocity(var18 + var2, 0.0D, var20 + var4);
							}
						}
						else
						{
							this.addVelocity(-var2, 0.0D, -var4);
							p_70108_1_.addVelocity(var2 / 4.0D, 0.0D, var4 / 4.0D);
						}
					}
				}
			}
		}

///    
///     <summary> * Sets the position and rotation. Only difference from the other one is no bounding on the rotation. Args: posX,
///     * posY, posZ, yaw, pitch </summary>
///     
		public override void setPositionAndRotation2(double p_70056_1_, double p_70056_3_, double p_70056_5_, float p_70056_7_, float p_70056_8_, int p_70056_9_)
		{
			this.minecartX = p_70056_1_;
			this.minecartY = p_70056_3_;
			this.minecartZ = p_70056_5_;
			this.minecartYaw = (double)p_70056_7_;
			this.minecartPitch = (double)p_70056_8_;
			this.turnProgress = p_70056_9_ + 2;
			this.motionX = this.velocityX;
			this.motionY = this.velocityY;
			this.motionZ = this.velocityZ;
		}

///    
///     <summary> * Sets the velocity to the args. Args: x, y, z </summary>
///     
		public override void setVelocity(double p_70016_1_, double p_70016_3_, double p_70016_5_)
		{
			this.velocityX = this.motionX = p_70016_1_;
			this.velocityY = this.motionY = p_70016_3_;
			this.velocityZ = this.motionZ = p_70016_5_;
		}

///    
///     <summary> * Sets the current amount of damage the minecart has taken. Decreases over time. The cart breaks when this is over
///     * 40. </summary>
///     
		public virtual float Damage
		{
			set
			{
				this.dataWatcher.updateObject(19, Convert.ToSingle(value));
			}
			get
			{
				return this.dataWatcher.getWatchableObjectFloat(19);
			}
		}

///    
///     <summary> * Gets the current amount of damage the minecart has taken. Decreases over time. The cart breaks when this is over
///     * 40. </summary>
///     

///    
///     <summary> * Sets the rolling amplitude the cart rolls while being attacked. </summary>
///     
		public virtual int RollingAmplitude
		{
			set
			{
				this.dataWatcher.updateObject(17, Convert.ToInt32(value));
			}
			get
			{
				return this.dataWatcher.getWatchableObjectInt(17);
			}
		}

///    
///     <summary> * Gets the rolling amplitude the cart rolls while being attacked. </summary>
///     

///    
///     <summary> * Sets the rolling direction the cart rolls while being attacked. Can be 1 or -1. </summary>
///     
		public virtual int RollingDirection
		{
			set
			{
				this.dataWatcher.updateObject(18, Convert.ToInt32(value));
			}
			get
			{
				return this.dataWatcher.getWatchableObjectInt(18);
			}
		}

///    
///     <summary> * Gets the rolling direction the cart rolls while being attacked. Can be 1 or -1. </summary>
///     

		public abstract int MinecartType {get;}

		public virtual Block func_145820_n()
		{
			if (!this.hasDisplayTile())
			{
				return this.func_145817_o();
			}
			else
			{
				int var1 = this.DataWatcher.getWatchableObjectInt(20) & 65535;
				return Block.getBlockById(var1);
			}
		}

		public virtual Block func_145817_o()
		{
			return Blocks.air;
		}

		public virtual int DisplayTileData
		{
			get
			{
				return !this.hasDisplayTile() ? this.DefaultDisplayTileData : this.DataWatcher.getWatchableObjectInt(20) >> 16;
			}
			set
			{
				this.DataWatcher.updateObject(20, Convert.ToInt32(Block.getIdFromBlock(this.func_145820_n()) & 65535 | value << 16));
				this.HasDisplayTile = true;
			}
		}

		public virtual int DefaultDisplayTileData
		{
			get
			{
				return 0;
			}
		}

		public virtual int DisplayTileOffset
		{
			get
			{
				return !this.hasDisplayTile() ? this.DefaultDisplayTileOffset : this.DataWatcher.getWatchableObjectInt(21);
			}
			set
			{
				this.DataWatcher.updateObject(21, Convert.ToInt32(value));
				this.HasDisplayTile = true;
			}
		}

		public virtual int DefaultDisplayTileOffset
		{
			get
			{
				return 6;
			}
		}

		public virtual void func_145819_k(int p_145819_1_)
		{
			this.DataWatcher.updateObject(20, Convert.ToInt32(p_145819_1_ & 65535 | this.DisplayTileData << 16));
			this.HasDisplayTile = true;
		}



		public virtual bool hasDisplayTile()
		{
			return this.DataWatcher.getWatchableObjectByte(22) == 1;
		}

		public virtual bool HasDisplayTile
		{
			set
			{
				this.DataWatcher.updateObject(22, Convert.ToByte((sbyte)(value ? 1 : 0)));
			}
		}

///    
///     <summary> * Sets the minecart's name. </summary>
///     
		public virtual string MinecartName
		{
			set
			{
				this.entityName = value;
			}
		}

///    
///     <summary> * Gets the name of this command sender (usually username, but possibly "Rcon") </summary>
///     
		public override string CommandSenderName
		{
			get
			{
				return this.entityName != null ? this.entityName : base.CommandSenderName;
			}
		}

///    
///     <summary> * Returns if the inventory name is localized </summary>
///     
		public virtual bool isInventoryNameLocalized()
		{
			get
			{
				return this.entityName != null;
			}
		}

		public virtual string func_95999_t()
		{
			return this.entityName;
		}
	}

}