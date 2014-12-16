using System;
using System.Collections;

namespace DotCraftCore.Entity
{

	using Block = DotCraftCore.block.Block;
	using BlockLiquid = DotCraftCore.block.BlockLiquid;
	using Material = DotCraftCore.block.material.Material;
	using CrashReport = DotCraftCore.crash.CrashReport;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using EnchantmentProtection = DotCraftCore.Enchantment.EnchantmentProtection;
	using EntityLightningBolt = DotCraftCore.Entity.Effect.EntityLightningBolt;
	using EntityItem = DotCraftCore.Entity.Item.EntityItem;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using NBTTagDouble = DotCraftCore.NBT.NBTTagDouble;
	using NBTTagFloat = DotCraftCore.NBT.NBTTagFloat;
	using NBTTagList = DotCraftCore.NBT.NBTTagList;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using ChatComponentText = DotCraftCore.Util.ChatComponentText;
	using ChunkCoordinates = DotCraftCore.Util.ChunkCoordinates;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using Direction = DotCraftCore.Util.Direction;
	using IChatComponent = DotCraftCore.Util.IChatComponent;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using ReportedException = DotCraftCore.Util.ReportedException;
	using StatCollector = DotCraftCore.Util.StatCollector;
	using Vec3 = DotCraftCore.Util.Vec3;
	using Explosion = DotCraftCore.World.Explosion;
	using World = DotCraftCore.World.World;
	using WorldServer = DotCraftCore.World.WorldServer;

	public abstract class Entity
	{
		private static int nextEntityID;
		private int field_145783_c;
		public double renderDistanceWeight;

///    
///     <summary> * Blocks entities from spawning when they do their AABB check to make sure the spot is clear of entities that can
///     * prevent spawning. </summary>
///     
		public bool preventEntitySpawning;

	/// <summary> The entity that is riding this entity  </summary>
		public Entity riddenByEntity;

	/// <summary> The entity we are currently riding  </summary>
		public Entity ridingEntity;
		public bool forceSpawn;

	/// <summary> Reference to the World object.  </summary>
		public World worldObj;
		public double prevPosX;
		public double prevPosY;
		public double prevPosZ;

	/// <summary> Entity position X  </summary>
		public double posX;

	/// <summary> Entity position Y  </summary>
		public double posY;

	/// <summary> Entity position Z  </summary>
		public double posZ;

	/// <summary> Entity motion X  </summary>
		public double motionX;

	/// <summary> Entity motion Y  </summary>
		public double motionY;

	/// <summary> Entity motion Z  </summary>
		public double motionZ;

	/// <summary> Entity rotation Yaw  </summary>
		public float rotationYaw;

	/// <summary> Entity rotation Pitch  </summary>
		public float rotationPitch;
		public float prevRotationYaw;
		public float prevRotationPitch;

	/// <summary> Axis aligned bounding box.  </summary>
		public readonly AxisAlignedBB boundingBox;
		public bool onGround;

///    
///     <summary> * True if after a move this entity has collided with something on X- or Z-axis </summary>
///     
		public bool isCollidedHorizontally;

///    
///     <summary> * True if after a move this entity has collided with something on Y-axis </summary>
///     
		public bool isCollidedVertically;

///    
///     <summary> * True if after a move this entity has collided with something either vertically or horizontally </summary>
///     
		public bool isCollided;
		public bool velocityChanged;
		protected internal bool isInWeb;
		public bool field_70135_K;

///    
///     <summary> * Gets set by setDead, so this must be the flag whether an Entity is dead (inactive may be better term) </summary>
///     
		public bool isDead;
		public float yOffset;

	/// <summary> How wide this entity is considered to be  </summary>
		public float width;

	/// <summary> How high this entity is considered to be  </summary>
		public float height;

	/// <summary> The previous ticks distance walked multiplied by 0.6  </summary>
		public float prevDistanceWalkedModified;

	/// <summary> The distance walked multiplied by 0.6  </summary>
		public float distanceWalkedModified;
		public float distanceWalkedOnStepModified;
		public float fallDistance;

///    
///     <summary> * The distance that has to be exceeded in order to triger a new step sound and an onEntityWalking event on a block </summary>
///     
		private int nextStepDistance;

///    
///     <summary> * The entity's X coordinate at the previous tick, used to calculate position during rendering routines </summary>
///     
		public double lastTickPosX;

///    
///     <summary> * The entity's Y coordinate at the previous tick, used to calculate position during rendering routines </summary>
///     
		public double lastTickPosY;

///    
///     <summary> * The entity's Z coordinate at the previous tick, used to calculate position during rendering routines </summary>
///     
		public double lastTickPosZ;
		public float ySize;

///    
///     <summary> * How high this entity can step up when running into a block to try to get over it (currently make note the entity
///     * will always step up this amount and not just the amount needed) </summary>
///     
		public float stepHeight;

///    
///     <summary> * Whether this entity won't clip with collision or not (make note it won't disable gravity) </summary>
///     
		public bool noClip;

///    
///     <summary> * Reduces the velocity applied by entity collisions by the specified percent. </summary>
///     
		public float entityCollisionReduction;
		protected internal Random rand;

	/// <summary> How many ticks has this entity had ran since being alive  </summary>
		public int ticksExisted;

///    
///     <summary> * The amount of ticks you have to stand inside of fire before be set on fire </summary>
///     
		public int fireResistance;
		private int fire;

///    
///     <summary> * Whether this entity is currently inside of water (if it handles water movement that is) </summary>
///     
		protected internal bool inWater;

///    
///     <summary> * Remaining time an entity will be "immune" to further damage after being hurt. </summary>
///     
		public int hurtResistantTime;
		private bool firstUpdate;
		protected internal bool isImmuneToFire;
		protected internal DataWatcher dataWatcher;
		private double entityRiderPitchDelta;
		private double entityRiderYawDelta;

	/// <summary> Has this entity been added to the chunk its within  </summary>
		public bool addedToChunk;
		public int chunkCoordX;
		public int chunkCoordY;
		public int chunkCoordZ;
		public int serverPosX;
		public int serverPosY;
		public int serverPosZ;

///    
///     <summary> * Render entity even if it is outside the camera frustum. Only true in EntityFish for now. Used in RenderGlobal:
///     * render if ignoreFrustumCheck or in frustum. </summary>
///     
		public bool ignoreFrustumCheck;
		public bool isAirBorne;
		public int timeUntilPortal;

	/// <summary> Whether the entity is inside a Portal  </summary>
		protected internal bool inPortal;
		protected internal int portalCounter;

	/// <summary> Which dimension the player is in (-1 = the Nether, 0 = normal world)  </summary>
		public int dimension;
		protected internal int teleportDirection;
		private bool invulnerable;
		protected internal UUID entityUniqueID;
		public Entity.EnumEntitySize myEntitySize;
		

		public virtual int EntityId
		{
			get
			{
				return this.field_145783_c;
			}
			set
			{
				this.field_145783_c = value;
			}
		}


		public Entity(World p_i1582_1_)
		{
			this.field_145783_c = nextEntityID++;
			this.renderDistanceWeight = 1.0D;
			this.boundingBox = AxisAlignedBB.getBoundingBox(0.0D, 0.0D, 0.0D, 0.0D, 0.0D, 0.0D);
			this.field_70135_K = true;
			this.width = 0.6F;
			this.height = 1.8F;
			this.nextStepDistance = 1;
			this.rand = new Random();
			this.fireResistance = 1;
			this.firstUpdate = true;
			this.entityUniqueID = UUID.randomUUID();
			this.myEntitySize = Entity.EnumEntitySize.SIZE_2;
			this.worldObj = p_i1582_1_;
			this.setPosition(0.0D, 0.0D, 0.0D);

			if (p_i1582_1_ != null)
			{
				this.dimension = p_i1582_1_.provider.dimensionId;
			}

			this.dataWatcher = new DataWatcher(this);
			this.dataWatcher.addObject(0, Convert.ToByte((sbyte)0));
			this.dataWatcher.addObject(1, Convert.ToInt16((short)300));
			this.entityInit();
		}

		protected internal abstract void entityInit();

		public virtual DataWatcher DataWatcher
		{
			get
			{
				return this.dataWatcher;
			}
		}

		public override bool Equals(object p_equals_1_)
		{
			return p_equals_1_ is Entity ? ((Entity)p_equals_1_).field_145783_c == this.field_145783_c : false;
		}

		public override int GetHashCode()
		{
			return this.field_145783_c;
		}

///    
///     <summary> * Keeps moving the entity up so it isn't colliding with blocks and other requirements for this entity to be spawned
///     * (only actually used on players though its also on Entity) </summary>
///     
		protected internal virtual void preparePlayerToSpawn()
		{
			if (this.worldObj != null)
			{
				while (this.posY > 0.0D)
				{
					this.setPosition(this.posX, this.posY, this.posZ);

					if (this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox).Empty)
					{
						break;
					}

					++this.posY;
				}

				this.motionX = this.motionY = this.motionZ = 0.0D;
				this.rotationPitch = 0.0F;
			}
		}

///    
///     <summary> * Will get destroyed next tick. </summary>
///     
		public virtual void setDead()
		{
			this.isDead = true;
		}

///    
///     <summary> * Sets the width and height of the entity. Args: width, height </summary>
///     
		protected internal virtual void setSize(float p_70105_1_, float p_70105_2_)
		{
			float var3;

			if (p_70105_1_ != this.width || p_70105_2_ != this.height)
			{
				var3 = this.width;
				this.width = p_70105_1_;
				this.height = p_70105_2_;
				this.boundingBox.maxX = this.boundingBox.minX + (double)this.width;
				this.boundingBox.maxZ = this.boundingBox.minZ + (double)this.width;
				this.boundingBox.maxY = this.boundingBox.minY + (double)this.height;

				if (this.width > var3 && !this.firstUpdate && !this.worldObj.isClient)
				{
					this.moveEntity((double)(var3 - this.width), 0.0D, (double)(var3 - this.width));
				}
			}

			var3 = p_70105_1_ % 2.0F;

			if ((double)var3 < 0.375D)
			{
				this.myEntitySize = Entity.EnumEntitySize.SIZE_1;
			}
			else if ((double)var3 < 0.75D)
			{
				this.myEntitySize = Entity.EnumEntitySize.SIZE_2;
			}
			else if ((double)var3 < 1.0D)
			{
				this.myEntitySize = Entity.EnumEntitySize.SIZE_3;
			}
			else if ((double)var3 < 1.375D)
			{
				this.myEntitySize = Entity.EnumEntitySize.SIZE_4;
			}
			else if ((double)var3 < 1.75D)
			{
				this.myEntitySize = Entity.EnumEntitySize.SIZE_5;
			}
			else
			{
				this.myEntitySize = Entity.EnumEntitySize.SIZE_6;
			}
		}

///    
///     <summary> * Sets the rotation of the entity </summary>
///     
		protected internal virtual void setRotation(float p_70101_1_, float p_70101_2_)
		{
			this.rotationYaw = p_70101_1_ % 360.0F;
			this.rotationPitch = p_70101_2_ % 360.0F;
		}

///    
///     <summary> * Sets the x,y,z of the entity from the given parameters. Also seems to set up a bounding box. </summary>
///     
		public virtual void setPosition(double p_70107_1_, double p_70107_3_, double p_70107_5_)
		{
			this.posX = p_70107_1_;
			this.posY = p_70107_3_;
			this.posZ = p_70107_5_;
			float var7 = this.width / 2.0F;
			float var8 = this.height;
			this.boundingBox.setBounds(p_70107_1_ - (double)var7, p_70107_3_ - (double)this.yOffset + (double)this.ySize, p_70107_5_ - (double)var7, p_70107_1_ + (double)var7, p_70107_3_ - (double)this.yOffset + (double)this.ySize + (double)var8, p_70107_5_ + (double)var7);
		}

///    
///     <summary> * Adds par1*0.15 to the entity's yaw, and *subtracts* par2*0.15 from the pitch. Clamps pitch from -90 to 90. Both
///     * arguments in degrees. </summary>
///     
		public virtual void setAngles(float p_70082_1_, float p_70082_2_)
		{
			float var3 = this.rotationPitch;
			float var4 = this.rotationYaw;
			this.rotationYaw = (float)((double)this.rotationYaw + (double)p_70082_1_ * 0.15D);
			this.rotationPitch = (float)((double)this.rotationPitch - (double)p_70082_2_ * 0.15D);

			if (this.rotationPitch < -90.0F)
			{
				this.rotationPitch = -90.0F;
			}

			if (this.rotationPitch > 90.0F)
			{
				this.rotationPitch = 90.0F;
			}

			this.prevRotationPitch += this.rotationPitch - var3;
			this.prevRotationYaw += this.rotationYaw - var4;
		}

///    
///     <summary> * Called to update the entity's position/logic. </summary>
///     
		public virtual void onUpdate()
		{
			this.onEntityUpdate();
		}

///    
///     <summary> * Gets called every tick from main Entity class </summary>
///     
		public virtual void onEntityUpdate()
		{
			this.worldObj.theProfiler.startSection("entityBaseTick");

			if (this.ridingEntity != null && this.ridingEntity.isDead)
			{
				this.ridingEntity = null;
			}

			this.prevDistanceWalkedModified = this.distanceWalkedModified;
			this.prevPosX = this.posX;
			this.prevPosY = this.posY;
			this.prevPosZ = this.posZ;
			this.prevRotationPitch = this.rotationPitch;
			this.prevRotationYaw = this.rotationYaw;
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

			if (this.Sprinting && !this.InWater)
			{
				int var5 = MathHelper.floor_double(this.posX);
				var2 = MathHelper.floor_double(this.posY - 0.20000000298023224D - (double)this.yOffset);
				int var6 = MathHelper.floor_double(this.posZ);
				Block var4 = this.worldObj.getBlock(var5, var2, var6);

				if (var4.Material != Material.air)
				{
					this.worldObj.spawnParticle("blockcrack_" + Block.getIdFromBlock(var4) + "_" + this.worldObj.getBlockMetadata(var5, var2, var6), this.posX + ((double)this.rand.nextFloat() - 0.5D) * (double)this.width, this.boundingBox.minY + 0.1D, this.posZ + ((double)this.rand.nextFloat() - 0.5D) * (double)this.width, -this.motionX * 4.0D, 1.5D, -this.motionZ * 4.0D);
				}
			}

			this.handleWaterMovement();

			if (this.worldObj.isClient)
			{
				this.fire = 0;
			}
			else if (this.fire > 0)
			{
				if (this.isImmuneToFire)
				{
					this.fire -= 4;

					if (this.fire < 0)
					{
						this.fire = 0;
					}
				}
				else
				{
					if (this.fire % 20 == 0)
					{
						this.attackEntityFrom(DamageSource.onFire, 1.0F);
					}

					--this.fire;
				}
			}

			if (this.handleLavaMovement())
			{
				this.setOnFireFromLava();
				this.fallDistance *= 0.5F;
			}

			if (this.posY < -64.0D)
			{
				this.kill();
			}

			if (!this.worldObj.isClient)
			{
				this.setFlag(0, this.fire > 0);
			}

			this.firstUpdate = false;
			this.worldObj.theProfiler.endSection();
		}

///    
///     <summary> * Return the amount of time this entity should stay in a portal before being transported. </summary>
///     
		public virtual int MaxInPortalTime
		{
			get
			{
				return 0;
			}
		}

///    
///     <summary> * Called whenever the entity is walking inside of lava. </summary>
///     
		protected internal virtual void setOnFireFromLava()
		{
			if (!this.isImmuneToFire)
			{
				this.attackEntityFrom(DamageSource.lava, 4.0F);
				this.Fire = 15;
			}
		}

///    
///     <summary> * Sets entity to burn for x amount of seconds, cannot lower amount of existing fire. </summary>
///     
		public virtual int Fire
		{
			set
			{
				int var2 = value * 20;
				var2 = EnchantmentProtection.getFireTimeForEntity(this, var2);
	
				if (this.fire < var2)
				{
					this.fire = var2;
				}
			}
		}

///    
///     <summary> * Removes fire from entity. </summary>
///     
		public virtual void extinguish()
		{
			this.fire = 0;
		}

///    
///     <summary> * sets the dead flag. Used when you fall off the bottom of the world. </summary>
///     
		protected internal virtual void kill()
		{
			this.setDead();
		}

///    
///     <summary> * Checks if the offset position from the entity's current position is inside of liquid. Args: x, y, z </summary>
///     
		public virtual bool isOffsetPositionInLiquid(double p_70038_1_, double p_70038_3_, double p_70038_5_)
		{
			AxisAlignedBB var7 = this.boundingBox.getOffsetBoundingBox(p_70038_1_, p_70038_3_, p_70038_5_);
			IList var8 = this.worldObj.getCollidingBoundingBoxes(this, var7);
			return !var8.Count == 0 ? false : !this.worldObj.isAnyLiquid(var7);
		}

///    
///     <summary> * Tries to moves the entity by the passed in displacement. Args: x, y, z </summary>
///     
		public virtual void moveEntity(double p_70091_1_, double p_70091_3_, double p_70091_5_)
		{
			if (this.noClip)
			{
				this.boundingBox.offset(p_70091_1_, p_70091_3_, p_70091_5_);
				this.posX = (this.boundingBox.minX + this.boundingBox.maxX) / 2.0D;
				this.posY = this.boundingBox.minY + (double)this.yOffset - (double)this.ySize;
				this.posZ = (this.boundingBox.minZ + this.boundingBox.maxZ) / 2.0D;
			}
			else
			{
				this.worldObj.theProfiler.startSection("move");
				this.ySize *= 0.4F;
				double var7 = this.posX;
				double var9 = this.posY;
				double var11 = this.posZ;

				if (this.isInWeb)
				{
					this.isInWeb = false;
					p_70091_1_ *= 0.25D;
					p_70091_3_ *= 0.05000000074505806D;
					p_70091_5_ *= 0.25D;
					this.motionX = 0.0D;
					this.motionY = 0.0D;
					this.motionZ = 0.0D;
				}

				double var13 = p_70091_1_;
				double var15 = p_70091_3_;
				double var17 = p_70091_5_;
				AxisAlignedBB var19 = this.boundingBox.copy();
				bool var20 = this.onGround && this.Sneaking && this is EntityPlayer;

				if (var20)
				{
					double var21;

					for (var21 = 0.05D; p_70091_1_ != 0.0D && this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox.getOffsetBoundingBox(p_70091_1_, -1.0D, 0.0D)).Empty; var13 = p_70091_1_)
					{
						if (p_70091_1_ < var21 && p_70091_1_ >= -var21)
						{
							p_70091_1_ = 0.0D;
						}
						else if (p_70091_1_ > 0.0D)
						{
							p_70091_1_ -= var21;
						}
						else
						{
							p_70091_1_ += var21;
						}
					}

					for (; p_70091_5_ != 0.0D && this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox.getOffsetBoundingBox(0.0D, -1.0D, p_70091_5_)).Empty; var17 = p_70091_5_)
					{
						if (p_70091_5_ < var21 && p_70091_5_ >= -var21)
						{
							p_70091_5_ = 0.0D;
						}
						else if (p_70091_5_ > 0.0D)
						{
							p_70091_5_ -= var21;
						}
						else
						{
							p_70091_5_ += var21;
						}
					}

					while (p_70091_1_ != 0.0D && p_70091_5_ != 0.0D && this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox.getOffsetBoundingBox(p_70091_1_, -1.0D, p_70091_5_)).Empty)
					{
						if (p_70091_1_ < var21 && p_70091_1_ >= -var21)
						{
							p_70091_1_ = 0.0D;
						}
						else if (p_70091_1_ > 0.0D)
						{
							p_70091_1_ -= var21;
						}
						else
						{
							p_70091_1_ += var21;
						}

						if (p_70091_5_ < var21 && p_70091_5_ >= -var21)
						{
							p_70091_5_ = 0.0D;
						}
						else if (p_70091_5_ > 0.0D)
						{
							p_70091_5_ -= var21;
						}
						else
						{
							p_70091_5_ += var21;
						}

						var13 = p_70091_1_;
						var17 = p_70091_5_;
					}
				}

				IList var36 = this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox.addCoord(p_70091_1_, p_70091_3_, p_70091_5_));

				for (int var22 = 0; var22 < var36.Count; ++var22)
				{
					p_70091_3_ = ((AxisAlignedBB)var36[var22]).calculateYOffset(this.boundingBox, p_70091_3_);
				}

				this.boundingBox.offset(0.0D, p_70091_3_, 0.0D);

				if (!this.field_70135_K && var15 != p_70091_3_)
				{
					p_70091_5_ = 0.0D;
					p_70091_3_ = 0.0D;
					p_70091_1_ = 0.0D;
				}

				bool var37 = this.onGround || var15 != p_70091_3_ && var15 < 0.0D;
				int var23;

				for (var23 = 0; var23 < var36.Count; ++var23)
				{
					p_70091_1_ = ((AxisAlignedBB)var36[var23]).calculateXOffset(this.boundingBox, p_70091_1_);
				}

				this.boundingBox.offset(p_70091_1_, 0.0D, 0.0D);

				if (!this.field_70135_K && var13 != p_70091_1_)
				{
					p_70091_5_ = 0.0D;
					p_70091_3_ = 0.0D;
					p_70091_1_ = 0.0D;
				}

				for (var23 = 0; var23 < var36.Count; ++var23)
				{
					p_70091_5_ = ((AxisAlignedBB)var36[var23]).calculateZOffset(this.boundingBox, p_70091_5_);
				}

				this.boundingBox.offset(0.0D, 0.0D, p_70091_5_);

				if (!this.field_70135_K && var17 != p_70091_5_)
				{
					p_70091_5_ = 0.0D;
					p_70091_3_ = 0.0D;
					p_70091_1_ = 0.0D;
				}

				double var25;
				double var27;
				int var30;
				double var38;

				if (this.stepHeight > 0.0F && var37 && (var20 || this.ySize < 0.05F) && (var13 != p_70091_1_ || var17 != p_70091_5_))
				{
					var38 = p_70091_1_;
					var25 = p_70091_3_;
					var27 = p_70091_5_;
					p_70091_1_ = var13;
					p_70091_3_ = (double)this.stepHeight;
					p_70091_5_ = var17;
					AxisAlignedBB var29 = this.boundingBox.copy();
					this.boundingBox.BB = var19;
					var36 = this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox.addCoord(var13, p_70091_3_, var17));

					for (var30 = 0; var30 < var36.Count; ++var30)
					{
						p_70091_3_ = ((AxisAlignedBB)var36[var30]).calculateYOffset(this.boundingBox, p_70091_3_);
					}

					this.boundingBox.offset(0.0D, p_70091_3_, 0.0D);

					if (!this.field_70135_K && var15 != p_70091_3_)
					{
						p_70091_5_ = 0.0D;
						p_70091_3_ = 0.0D;
						p_70091_1_ = 0.0D;
					}

					for (var30 = 0; var30 < var36.Count; ++var30)
					{
						p_70091_1_ = ((AxisAlignedBB)var36[var30]).calculateXOffset(this.boundingBox, p_70091_1_);
					}

					this.boundingBox.offset(p_70091_1_, 0.0D, 0.0D);

					if (!this.field_70135_K && var13 != p_70091_1_)
					{
						p_70091_5_ = 0.0D;
						p_70091_3_ = 0.0D;
						p_70091_1_ = 0.0D;
					}

					for (var30 = 0; var30 < var36.Count; ++var30)
					{
						p_70091_5_ = ((AxisAlignedBB)var36[var30]).calculateZOffset(this.boundingBox, p_70091_5_);
					}

					this.boundingBox.offset(0.0D, 0.0D, p_70091_5_);

					if (!this.field_70135_K && var17 != p_70091_5_)
					{
						p_70091_5_ = 0.0D;
						p_70091_3_ = 0.0D;
						p_70091_1_ = 0.0D;
					}

					if (!this.field_70135_K && var15 != p_70091_3_)
					{
						p_70091_5_ = 0.0D;
						p_70091_3_ = 0.0D;
						p_70091_1_ = 0.0D;
					}
					else
					{
						p_70091_3_ = (double)(-this.stepHeight);

						for (var30 = 0; var30 < var36.Count; ++var30)
						{
							p_70091_3_ = ((AxisAlignedBB)var36[var30]).calculateYOffset(this.boundingBox, p_70091_3_);
						}

						this.boundingBox.offset(0.0D, p_70091_3_, 0.0D);
					}

					if (var38 * var38 + var27 * var27 >= p_70091_1_ * p_70091_1_ + p_70091_5_ * p_70091_5_)
					{
						p_70091_1_ = var38;
						p_70091_3_ = var25;
						p_70091_5_ = var27;
						this.boundingBox.BB = var29;
					}
				}

				this.worldObj.theProfiler.endSection();
				this.worldObj.theProfiler.startSection("rest");
				this.posX = (this.boundingBox.minX + this.boundingBox.maxX) / 2.0D;
				this.posY = this.boundingBox.minY + (double)this.yOffset - (double)this.ySize;
				this.posZ = (this.boundingBox.minZ + this.boundingBox.maxZ) / 2.0D;
				this.isCollidedHorizontally = var13 != p_70091_1_ || var17 != p_70091_5_;
				this.isCollidedVertically = var15 != p_70091_3_;
				this.onGround = var15 != p_70091_3_ && var15 < 0.0D;
				this.isCollided = this.isCollidedHorizontally || this.isCollidedVertically;
				this.updateFallState(p_70091_3_, this.onGround);

				if (var13 != p_70091_1_)
				{
					this.motionX = 0.0D;
				}

				if (var15 != p_70091_3_)
				{
					this.motionY = 0.0D;
				}

				if (var17 != p_70091_5_)
				{
					this.motionZ = 0.0D;
				}

				var38 = this.posX - var7;
				var25 = this.posY - var9;
				var27 = this.posZ - var11;

				if (this.canTriggerWalking() && !var20 && this.ridingEntity == null)
				{
					int var39 = MathHelper.floor_double(this.posX);
					var30 = MathHelper.floor_double(this.posY - 0.20000000298023224D - (double)this.yOffset);
					int var31 = MathHelper.floor_double(this.posZ);
					Block var32 = this.worldObj.getBlock(var39, var30, var31);
					int var33 = this.worldObj.getBlock(var39, var30 - 1, var31).RenderType;

					if (var33 == 11 || var33 == 32 || var33 == 21)
					{
						var32 = this.worldObj.getBlock(var39, var30 - 1, var31);
					}

					if (var32 != Blocks.ladder)
					{
						var25 = 0.0D;
					}

					this.distanceWalkedModified = (float)((double)this.distanceWalkedModified + (double)MathHelper.sqrt_double(var38 * var38 + var27 * var27) * 0.6D);
					this.distanceWalkedOnStepModified = (float)((double)this.distanceWalkedOnStepModified + (double)MathHelper.sqrt_double(var38 * var38 + var25 * var25 + var27 * var27) * 0.6D);

					if (this.distanceWalkedOnStepModified > (float)this.nextStepDistance && var32.Material != Material.air)
					{
						this.nextStepDistance = (int)this.distanceWalkedOnStepModified + 1;

						if (this.InWater)
						{
							float var34 = MathHelper.sqrt_double(this.motionX * this.motionX * 0.20000000298023224D + this.motionY * this.motionY + this.motionZ * this.motionZ * 0.20000000298023224D) * 0.35F;

							if (var34 > 1.0F)
							{
								var34 = 1.0F;
							}

							this.playSound(this.SwimSound, var34, 1.0F + (this.rand.nextFloat() - this.rand.nextFloat()) * 0.4F);
						}

						this.func_145780_a(var39, var30, var31, var32);
						var32.onEntityWalking(this.worldObj, var39, var30, var31, this);
					}
				}

				try
				{
					this.func_145775_I();
				}
				catch (Exception var35)
				{
					CrashReport var41 = CrashReport.makeCrashReport(var35, "Checking entity block collision");
					CrashReportCategory var42 = var41.makeCategory("Entity being checked for collision");
					this.addEntityCrashInfo(var42);
					throw new ReportedException(var41);
				}

				bool var40 = this.Wet;

				if (this.worldObj.func_147470_e(this.boundingBox.contract(0.001D, 0.001D, 0.001D)))
				{
					this.dealFireDamage(1);

					if (!var40)
					{
						++this.fire;

						if (this.fire == 0)
						{
							this.Fire = 8;
						}
					}
				}
				else if (this.fire <= 0)
				{
					this.fire = -this.fireResistance;
				}

				if (var40 && this.fire > 0)
				{
					this.playSound("random.fizz", 0.7F, 1.6F + (this.rand.nextFloat() - this.rand.nextFloat()) * 0.4F);
					this.fire = -this.fireResistance;
				}

				this.worldObj.theProfiler.endSection();
			}
		}

		protected internal virtual string SwimSound
		{
			get
			{
				return "game.neutral.swim";
			}
		}

		protected internal virtual void func_145775_I()
		{
			int var1 = MathHelper.floor_double(this.boundingBox.minX + 0.001D);
			int var2 = MathHelper.floor_double(this.boundingBox.minY + 0.001D);
			int var3 = MathHelper.floor_double(this.boundingBox.minZ + 0.001D);
			int var4 = MathHelper.floor_double(this.boundingBox.maxX - 0.001D);
			int var5 = MathHelper.floor_double(this.boundingBox.maxY - 0.001D);
			int var6 = MathHelper.floor_double(this.boundingBox.maxZ - 0.001D);

			if (this.worldObj.checkChunksExist(var1, var2, var3, var4, var5, var6))
			{
				for (int var7 = var1; var7 <= var4; ++var7)
				{
					for (int var8 = var2; var8 <= var5; ++var8)
					{
						for (int var9 = var3; var9 <= var6; ++var9)
						{
							Block var10 = this.worldObj.getBlock(var7, var8, var9);

							try
							{
								var10.onEntityCollidedWithBlock(this.worldObj, var7, var8, var9, this);
							}
							catch (Exception var14)
							{
								CrashReport var12 = CrashReport.makeCrashReport(var14, "Colliding entity with block");
								CrashReportCategory var13 = var12.makeCategory("Block being collided with");
								CrashReportCategory.func_147153_a(var13, var7, var8, var9, var10, this.worldObj.getBlockMetadata(var7, var8, var9));
								throw new ReportedException(var12);
							}
						}
					}
				}
			}
		}

		protected internal virtual void func_145780_a(int p_145780_1_, int p_145780_2_, int p_145780_3_, Block p_145780_4_)
		{
			SoundType var5 = p_145780_4_.stepSound;

			if (this.worldObj.getBlock(p_145780_1_, p_145780_2_ + 1, p_145780_3_) == Blocks.snow_layer)
			{
				var5 = Blocks.snow_layer.stepSound;
				this.playSound(var5.func_150498_e(), var5.func_150497_c() * 0.15F, var5.func_150494_d());
			}
			else if (!p_145780_4_.Material.Liquid)
			{
				this.playSound(var5.func_150498_e(), var5.func_150497_c() * 0.15F, var5.func_150494_d());
			}
		}

		public virtual void playSound(string p_85030_1_, float p_85030_2_, float p_85030_3_)
		{
			this.worldObj.playSoundAtEntity(this, p_85030_1_, p_85030_2_, p_85030_3_);
		}

///    
///     <summary> * returns if this entity triggers Block.onEntityWalking on the blocks they walk on. used for spiders and wolves to
///     * prevent them from trampling crops </summary>
///     
		protected internal virtual bool canTriggerWalking()
		{
			return true;
		}

///    
///     <summary> * Takes in the distance the entity has fallen this tick and whether its on the ground to update the fall distance
///     * and deal fall damage if landing on the ground.  Args: distanceFallenThisTick, onGround </summary>
///     
		protected internal virtual void updateFallState(double p_70064_1_, bool p_70064_3_)
		{
			if (p_70064_3_)
			{
				if (this.fallDistance > 0.0F)
				{
					this.fall(this.fallDistance);
					this.fallDistance = 0.0F;
				}
			}
			else if (p_70064_1_ < 0.0D)
			{
				this.fallDistance = (float)((double)this.fallDistance - p_70064_1_);
			}
		}

///    
///     <summary> * returns the bounding box for this entity </summary>
///     
		public virtual AxisAlignedBB BoundingBox
		{
			get
			{
				return null;
			}
		}

///    
///     <summary> * Will deal the specified amount of damage to the entity if the entity isn't immune to fire damage. Args:
///     * amountDamage </summary>
///     
		protected internal virtual void dealFireDamage(int p_70081_1_)
		{
			if (!this.isImmuneToFire)
			{
				this.attackEntityFrom(DamageSource.inFire, (float)p_70081_1_);
			}
		}

		public bool isImmuneToFire()
		{
			get
			{
				return this.isImmuneToFire;
			}
		}

///    
///     <summary> * Called when the mob is falling. Calculates and applies fall damage. </summary>
///     
		protected internal virtual void fall(float p_70069_1_)
		{
			if (this.riddenByEntity != null)
			{
				this.riddenByEntity.fall(p_70069_1_);
			}
		}

///    
///     <summary> * Checks if this entity is either in water or on an open air block in rain (used in wolves). </summary>
///     
		public virtual bool isWet()
		{
			get
			{
				return this.inWater || this.worldObj.canLightningStrikeAt(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ)) || this.worldObj.canLightningStrikeAt(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY + (double)this.height), MathHelper.floor_double(this.posZ));
			}
		}

///    
///     <summary> * Checks if this entity is inside water (if inWater field is true as a result of handleWaterMovement() returning
///     * true) </summary>
///     
		public virtual bool isInWater()
		{
			get
			{
				return this.inWater;
			}
		}

///    
///     <summary> * Returns if this entity is in water and will end up adding the waters velocity to the entity </summary>
///     
		public virtual bool handleWaterMovement()
		{
			if (this.worldObj.handleMaterialAcceleration(this.boundingBox.expand(0.0D, -0.4000000059604645D, 0.0D).contract(0.001D, 0.001D, 0.001D), Material.water, this))
			{
				if (!this.inWater && !this.firstUpdate)
				{
					float var1 = MathHelper.sqrt_double(this.motionX * this.motionX * 0.20000000298023224D + this.motionY * this.motionY + this.motionZ * this.motionZ * 0.20000000298023224D) * 0.2F;

					if (var1 > 1.0F)
					{
						var1 = 1.0F;
					}

					this.playSound(this.SplashSound, var1, 1.0F + (this.rand.nextFloat() - this.rand.nextFloat()) * 0.4F);
					float var2 = (float)MathHelper.floor_double(this.boundingBox.minY);
					int var3;
					float var4;
					float var5;

					for (var3 = 0; (float)var3 < 1.0F + this.width * 20.0F; ++var3)
					{
						var4 = (this.rand.nextFloat() * 2.0F - 1.0F) * this.width;
						var5 = (this.rand.nextFloat() * 2.0F - 1.0F) * this.width;
						this.worldObj.spawnParticle("bubble", this.posX + (double)var4, (double)(var2 + 1.0F), this.posZ + (double)var5, this.motionX, this.motionY - (double)(this.rand.nextFloat() * 0.2F), this.motionZ);
					}

					for (var3 = 0; (float)var3 < 1.0F + this.width * 20.0F; ++var3)
					{
						var4 = (this.rand.nextFloat() * 2.0F - 1.0F) * this.width;
						var5 = (this.rand.nextFloat() * 2.0F - 1.0F) * this.width;
						this.worldObj.spawnParticle("splash", this.posX + (double)var4, (double)(var2 + 1.0F), this.posZ + (double)var5, this.motionX, this.motionY, this.motionZ);
					}
				}

				this.fallDistance = 0.0F;
				this.inWater = true;
				this.fire = 0;
			}
			else
			{
				this.inWater = false;
			}

			return this.inWater;
		}

		protected internal virtual string SplashSound
		{
			get
			{
				return "game.neutral.swim.splash";
			}
		}

///    
///     <summary> * Checks if the current block the entity is within of the specified material type </summary>
///     
		public virtual bool isInsideOfMaterial(Material p_70055_1_)
		{
			double var2 = this.posY + (double)this.EyeHeight;
			int var4 = MathHelper.floor_double(this.posX);
			int var5 = MathHelper.floor_float((float)MathHelper.floor_double(var2));
			int var6 = MathHelper.floor_double(this.posZ);
			Block var7 = this.worldObj.getBlock(var4, var5, var6);

			if (var7.Material == p_70055_1_)
			{
				float var8 = BlockLiquid.func_149801_b(this.worldObj.getBlockMetadata(var4, var5, var6)) - 0.11111111F;
				float var9 = (float)(var5 + 1) - var8;
				return var2 < (double)var9;
			}
			else
			{
				return false;
			}
		}

		public virtual float EyeHeight
		{
			get
			{
				return 0.0F;
			}
		}

///    
///     <summary> * Whether or not the current entity is in lava </summary>
///     
		public virtual bool handleLavaMovement()
		{
			return this.worldObj.isMaterialInBB(this.boundingBox.expand(-0.10000000149011612D, -0.4000000059604645D, -0.10000000149011612D), Material.lava);
		}

///    
///     <summary> * Used in both water and by flying objects </summary>
///     
		public virtual void moveFlying(float p_70060_1_, float p_70060_2_, float p_70060_3_)
		{
			float var4 = p_70060_1_ * p_70060_1_ + p_70060_2_ * p_70060_2_;

			if (var4 >= 1.0E-4F)
			{
				var4 = MathHelper.sqrt_float(var4);

				if (var4 < 1.0F)
				{
					var4 = 1.0F;
				}

				var4 = p_70060_3_ / var4;
				p_70060_1_ *= var4;
				p_70060_2_ *= var4;
				float var5 = MathHelper.sin(this.rotationYaw * (float)Math.PI / 180.0F);
				float var6 = MathHelper.cos(this.rotationYaw * (float)Math.PI / 180.0F);
				this.motionX += (double)(p_70060_1_ * var6 - p_70060_2_ * var5);
				this.motionZ += (double)(p_70060_2_ * var6 + p_70060_1_ * var5);
			}
		}

		public virtual int getBrightnessForRender(float p_70070_1_)
		{
			int var2 = MathHelper.floor_double(this.posX);
			int var3 = MathHelper.floor_double(this.posZ);

			if (this.worldObj.blockExists(var2, 0, var3))
			{
				double var4 = (this.boundingBox.maxY - this.boundingBox.minY) * 0.66D;
				int var6 = MathHelper.floor_double(this.posY - (double)this.yOffset + var4);
				return this.worldObj.getLightBrightnessForSkyBlocks(var2, var6, var3, 0);
			}
			else
			{
				return 0;
			}
		}

///    
///     <summary> * Gets how bright this entity is. </summary>
///     
		public virtual float getBrightness(float p_70013_1_)
		{
			int var2 = MathHelper.floor_double(this.posX);
			int var3 = MathHelper.floor_double(this.posZ);

			if (this.worldObj.blockExists(var2, 0, var3))
			{
				double var4 = (this.boundingBox.maxY - this.boundingBox.minY) * 0.66D;
				int var6 = MathHelper.floor_double(this.posY - (double)this.yOffset + var4);
				return this.worldObj.getLightBrightness(var2, var6, var3);
			}
			else
			{
				return 0.0F;
			}
		}

///    
///     <summary> * Sets the reference to the World object. </summary>
///     
		public virtual World World
		{
			set
			{
				this.worldObj = value;
			}
		}

///    
///     <summary> * Sets the entity's position and rotation. Args: posX, posY, posZ, yaw, pitch </summary>
///     
		public virtual void setPositionAndRotation(double p_70080_1_, double p_70080_3_, double p_70080_5_, float p_70080_7_, float p_70080_8_)
		{
			this.prevPosX = this.posX = p_70080_1_;
			this.prevPosY = this.posY = p_70080_3_;
			this.prevPosZ = this.posZ = p_70080_5_;
			this.prevRotationYaw = this.rotationYaw = p_70080_7_;
			this.prevRotationPitch = this.rotationPitch = p_70080_8_;
			this.ySize = 0.0F;
			double var9 = (double)(this.prevRotationYaw - p_70080_7_);

			if (var9 < -180.0D)
			{
				this.prevRotationYaw += 360.0F;
			}

			if (var9 >= 180.0D)
			{
				this.prevRotationYaw -= 360.0F;
			}

			this.setPosition(this.posX, this.posY, this.posZ);
			this.setRotation(p_70080_7_, p_70080_8_);
		}

///    
///     <summary> * Sets the location and Yaw/Pitch of an entity in the world </summary>
///     
		public virtual void setLocationAndAngles(double p_70012_1_, double p_70012_3_, double p_70012_5_, float p_70012_7_, float p_70012_8_)
		{
			this.lastTickPosX = this.prevPosX = this.posX = p_70012_1_;
			this.lastTickPosY = this.prevPosY = this.posY = p_70012_3_ + (double)this.yOffset;
			this.lastTickPosZ = this.prevPosZ = this.posZ = p_70012_5_;
			this.rotationYaw = p_70012_7_;
			this.rotationPitch = p_70012_8_;
			this.setPosition(this.posX, this.posY, this.posZ);
		}

///    
///     <summary> * Returns the distance to the entity. Args: entity </summary>
///     
		public virtual float getDistanceToEntity(Entity p_70032_1_)
		{
			float var2 = (float)(this.posX - p_70032_1_.posX);
			float var3 = (float)(this.posY - p_70032_1_.posY);
			float var4 = (float)(this.posZ - p_70032_1_.posZ);
			return MathHelper.sqrt_float(var2 * var2 + var3 * var3 + var4 * var4);
		}

///    
///     <summary> * Gets the squared distance to the position. Args: x, y, z </summary>
///     
		public virtual double getDistanceSq(double p_70092_1_, double p_70092_3_, double p_70092_5_)
		{
			double var7 = this.posX - p_70092_1_;
			double var9 = this.posY - p_70092_3_;
			double var11 = this.posZ - p_70092_5_;
			return var7 * var7 + var9 * var9 + var11 * var11;
		}

///    
///     <summary> * Gets the distance to the position. Args: x, y, z </summary>
///     
		public virtual double getDistance(double p_70011_1_, double p_70011_3_, double p_70011_5_)
		{
			double var7 = this.posX - p_70011_1_;
			double var9 = this.posY - p_70011_3_;
			double var11 = this.posZ - p_70011_5_;
			return (double)MathHelper.sqrt_double(var7 * var7 + var9 * var9 + var11 * var11);
		}

///    
///     <summary> * Returns the squared distance to the entity. Args: entity </summary>
///     
		public virtual double getDistanceSqToEntity(Entity p_70068_1_)
		{
			double var2 = this.posX - p_70068_1_.posX;
			double var4 = this.posY - p_70068_1_.posY;
			double var6 = this.posZ - p_70068_1_.posZ;
			return var2 * var2 + var4 * var4 + var6 * var6;
		}

///    
///     <summary> * Called by a player entity when they collide with an entity </summary>
///     
		public virtual void onCollideWithPlayer(EntityPlayer p_70100_1_)
		{
		}

///    
///     <summary> * Applies a velocity to each of the entities pushing them away from each other. Args: entity </summary>
///     
		public virtual void applyEntityCollision(Entity p_70108_1_)
		{
			if (p_70108_1_.riddenByEntity != this && p_70108_1_.ridingEntity != this)
			{
				double var2 = p_70108_1_.posX - this.posX;
				double var4 = p_70108_1_.posZ - this.posZ;
				double var6 = MathHelper.abs_max(var2, var4);

				if (var6 >= 0.009999999776482582D)
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
					var2 *= 0.05000000074505806D;
					var4 *= 0.05000000074505806D;
					var2 *= (double)(1.0F - this.entityCollisionReduction);
					var4 *= (double)(1.0F - this.entityCollisionReduction);
					this.addVelocity(-var2, 0.0D, -var4);
					p_70108_1_.addVelocity(var2, 0.0D, var4);
				}
			}
		}

///    
///     <summary> * Adds to the current velocity of the entity. Args: x, y, z </summary>
///     
		public virtual void addVelocity(double p_70024_1_, double p_70024_3_, double p_70024_5_)
		{
			this.motionX += p_70024_1_;
			this.motionY += p_70024_3_;
			this.motionZ += p_70024_5_;
			this.isAirBorne = true;
		}

///    
///     <summary> * Sets that this entity has been attacked. </summary>
///     
		protected internal virtual void setBeenAttacked()
		{
			this.velocityChanged = true;
		}

///    
///     <summary> * Called when the entity is attacked. </summary>
///     
		public virtual bool attackEntityFrom(DamageSource p_70097_1_, float p_70097_2_)
		{
			if (this.EntityInvulnerable)
			{
				return false;
			}
			else
			{
				this.setBeenAttacked();
				return false;
			}
		}

///    
///     <summary> * Returns true if other Entities should be prevented from moving through this Entity. </summary>
///     
		public virtual bool canBeCollidedWith()
		{
			return false;
		}

///    
///     <summary> * Returns true if this entity should push and be pushed by other entities when colliding. </summary>
///     
		public virtual bool canBePushed()
		{
			return false;
		}

///    
///     <summary> * Adds a value to the player score. Currently not actually used and the entity passed in does nothing. Args:
///     * entity, scoreToAdd </summary>
///     
		public virtual void addToPlayerScore(Entity p_70084_1_, int p_70084_2_)
		{
		}

		public virtual bool isInRangeToRender3d(double p_145770_1_, double p_145770_3_, double p_145770_5_)
		{
			double var7 = this.posX - p_145770_1_;
			double var9 = this.posY - p_145770_3_;
			double var11 = this.posZ - p_145770_5_;
			double var13 = var7 * var7 + var9 * var9 + var11 * var11;
			return this.isInRangeToRenderDist(var13);
		}

///    
///     <summary> * Checks if the entity is in range to render by using the past in distance and comparing it to its average edge
///     * length * 64 * renderDistanceWeight Args: distance </summary>
///     
		public virtual bool isInRangeToRenderDist(double p_70112_1_)
		{
			double var3 = this.boundingBox.AverageEdgeLength;
			var3 *= 64.0D * this.renderDistanceWeight;
			return p_70112_1_ < var3 * var3;
		}

///    
///     <summary> * Like writeToNBTOptional but does not check if the entity is ridden. Used for saving ridden entities with their
///     * riders. </summary>
///     
		public virtual bool writeMountToNBT(NBTTagCompound p_98035_1_)
		{
			string var2 = this.EntityString;

			if (!this.isDead && var2 != null)
			{
				p_98035_1_.setString("id", var2);
				this.writeToNBT(p_98035_1_);
				return true;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Either write this entity to the NBT tag given and return true, or return false without doing anything. If this
///     * returns false the entity is not saved on disk. Ridden entities return false here as they are saved with their
///     * rider. </summary>
///     
		public virtual bool writeToNBTOptional(NBTTagCompound p_70039_1_)
		{
			string var2 = this.EntityString;

			if (!this.isDead && var2 != null && this.riddenByEntity == null)
			{
				p_70039_1_.setString("id", var2);
				this.writeToNBT(p_70039_1_);
				return true;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Save the entity to NBT (calls an abstract helper method to write extra data) </summary>
///     
		public virtual void writeToNBT(NBTTagCompound p_70109_1_)
		{
			try
			{
				p_70109_1_.setTag("Pos", this.newDoubleNBTList(new double[] {this.posX, this.posY + (double)this.ySize, this.posZ}));
				p_70109_1_.setTag("Motion", this.newDoubleNBTList(new double[] {this.motionX, this.motionY, this.motionZ}));
				p_70109_1_.setTag("Rotation", this.newFloatNBTList(new float[] {this.rotationYaw, this.rotationPitch}));
				p_70109_1_.setFloat("FallDistance", this.fallDistance);
				p_70109_1_.setShort("Fire", (short)this.fire);
				p_70109_1_.setShort("Air", (short)this.Air);
				p_70109_1_.setBoolean("OnGround", this.onGround);
				p_70109_1_.setInteger("Dimension", this.dimension);
				p_70109_1_.setBoolean("Invulnerable", this.invulnerable);
				p_70109_1_.setInteger("PortalCooldown", this.timeUntilPortal);
				p_70109_1_.setLong("UUIDMost", this.UniqueID.MostSignificantBits);
				p_70109_1_.setLong("UUIDLeast", this.UniqueID.LeastSignificantBits);
				this.writeEntityToNBT(p_70109_1_);

				if (this.ridingEntity != null)
				{
					NBTTagCompound var2 = new NBTTagCompound();

					if (this.ridingEntity.writeMountToNBT(var2))
					{
						p_70109_1_.setTag("Riding", var2);
					}
				}
			}
			catch (Exception var5)
			{
				CrashReport var3 = CrashReport.makeCrashReport(var5, "Saving entity NBT");
				CrashReportCategory var4 = var3.makeCategory("Entity being saved");
				this.addEntityCrashInfo(var4);
				throw new ReportedException(var3);
			}
		}

///    
///     <summary> * Reads the entity from NBT (calls an abstract helper method to read specialized data) </summary>
///     
		public virtual void readFromNBT(NBTTagCompound p_70020_1_)
		{
			try
			{
				NBTTagList var2 = p_70020_1_.getTagList("Pos", 6);
				NBTTagList var6 = p_70020_1_.getTagList("Motion", 6);
				NBTTagList var7 = p_70020_1_.getTagList("Rotation", 5);
				this.motionX = var6.func_150309_d(0);
				this.motionY = var6.func_150309_d(1);
				this.motionZ = var6.func_150309_d(2);

				if (Math.Abs(this.motionX) > 10.0D)
				{
					this.motionX = 0.0D;
				}

				if (Math.Abs(this.motionY) > 10.0D)
				{
					this.motionY = 0.0D;
				}

				if (Math.Abs(this.motionZ) > 10.0D)
				{
					this.motionZ = 0.0D;
				}

				this.prevPosX = this.lastTickPosX = this.posX = var2.func_150309_d(0);
				this.prevPosY = this.lastTickPosY = this.posY = var2.func_150309_d(1);
				this.prevPosZ = this.lastTickPosZ = this.posZ = var2.func_150309_d(2);
				this.prevRotationYaw = this.rotationYaw = var7.func_150308_e(0);
				this.prevRotationPitch = this.rotationPitch = var7.func_150308_e(1);
				this.fallDistance = p_70020_1_.getFloat("FallDistance");
				this.fire = p_70020_1_.getShort("Fire");
				this.Air = p_70020_1_.getShort("Air");
				this.onGround = p_70020_1_.getBoolean("OnGround");
				this.dimension = p_70020_1_.getInteger("Dimension");
				this.invulnerable = p_70020_1_.getBoolean("Invulnerable");
				this.timeUntilPortal = p_70020_1_.getInteger("PortalCooldown");

				if (p_70020_1_.func_150297_b("UUIDMost", 4) && p_70020_1_.func_150297_b("UUIDLeast", 4))
				{
					this.entityUniqueID = new UUID(p_70020_1_.getLong("UUIDMost"), p_70020_1_.getLong("UUIDLeast"));
				}

				this.setPosition(this.posX, this.posY, this.posZ);
				this.setRotation(this.rotationYaw, this.rotationPitch);
				this.readEntityFromNBT(p_70020_1_);

				if (this.shouldSetPosAfterLoading())
				{
					this.setPosition(this.posX, this.posY, this.posZ);
				}
			}
			catch (Exception var5)
			{
				CrashReport var3 = CrashReport.makeCrashReport(var5, "Loading entity NBT");
				CrashReportCategory var4 = var3.makeCategory("Entity being loaded");
				this.addEntityCrashInfo(var4);
				throw new ReportedException(var3);
			}
		}

		protected internal virtual bool shouldSetPosAfterLoading()
		{
			return true;
		}

///    
///     <summary> * Returns the string that identifies this Entity's class </summary>
///     
		protected internal string EntityString
		{
			get
			{
				return EntityList.getEntityString(this);
			}
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		protected internal abstract void readEntityFromNBT(NBTTagCompound p_70037_1_);

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		protected internal abstract void writeEntityToNBT(NBTTagCompound p_70014_1_);

		public virtual void onChunkLoad()
		{
		}

///    
///     <summary> * creates a NBT list from the array of doubles passed to this function </summary>
///     
		protected internal virtual NBTTagList newDoubleNBTList(params double[] p_70087_1_)
		{
			NBTTagList var2 = new NBTTagList();
			double[] var3 = p_70087_1_;
			int var4 = p_70087_1_.length;

			for (int var5 = 0; var5 < var4; ++var5)
			{
				double var6 = var3[var5];
				var2.appendTag(new NBTTagDouble(var6));
			}

			return var2;
		}

///    
///     <summary> * Returns a new NBTTagList filled with the specified floats </summary>
///     
		protected internal virtual NBTTagList newFloatNBTList(params float[] p_70049_1_)
		{
			NBTTagList var2 = new NBTTagList();
			float[] var3 = p_70049_1_;
			int var4 = p_70049_1_.length;

			for (int var5 = 0; var5 < var4; ++var5)
			{
				float var6 = var3[var5];
				var2.appendTag(new NBTTagFloat(var6));
			}

			return var2;
		}

		public virtual float ShadowSize
		{
			get
			{
				return this.height / 2.0F;
			}
		}

		public virtual EntityItem func_145779_a(Item p_145779_1_, int p_145779_2_)
		{
			return this.func_145778_a(p_145779_1_, p_145779_2_, 0.0F);
		}

		public virtual EntityItem func_145778_a(Item p_145778_1_, int p_145778_2_, float p_145778_3_)
		{
			return this.entityDropItem(new ItemStack(p_145778_1_, p_145778_2_, 0), p_145778_3_);
		}

///    
///     <summary> * Drops an item at the position of the entity. </summary>
///     
		public virtual EntityItem entityDropItem(ItemStack p_70099_1_, float p_70099_2_)
		{
			if (p_70099_1_.stackSize != 0 && p_70099_1_.Item != null)
			{
				EntityItem var3 = new EntityItem(this.worldObj, this.posX, this.posY + (double)p_70099_2_, this.posZ, p_70099_1_);
				var3.delayBeforeCanPickup = 10;
				this.worldObj.spawnEntityInWorld(var3);
				return var3;
			}
			else
			{
				return null;
			}
		}

///    
///     <summary> * Checks whether target entity is alive. </summary>
///     
		public virtual bool isEntityAlive()
		{
			get
			{
				return !this.isDead;
			}
		}

///    
///     <summary> * Checks if this entity is inside of an opaque block </summary>
///     
		public virtual bool isEntityInsideOpaqueBlock()
		{
			get
			{
				for (int var1 = 0; var1 < 8; ++var1)
				{
					float var2 = ((float)((var1 >> 0) % 2) - 0.5F) * this.width * 0.8F;
					float var3 = ((float)((var1 >> 1) % 2) - 0.5F) * 0.1F;
					float var4 = ((float)((var1 >> 2) % 2) - 0.5F) * this.width * 0.8F;
					int var5 = MathHelper.floor_double(this.posX + (double)var2);
					int var6 = MathHelper.floor_double(this.posY + (double)this.EyeHeight + (double)var3);
					int var7 = MathHelper.floor_double(this.posZ + (double)var4);
	
					if (this.worldObj.getBlock(var5, var6, var7).NormalCube)
					{
						return true;
					}
				}
	
				return false;
			}
		}

///    
///     <summary> * First layer of player interaction </summary>
///     
		public virtual bool interactFirst(EntityPlayer p_130002_1_)
		{
			return false;
		}

///    
///     <summary> * Returns a boundingBox used to collide the entity with other entities and blocks. This enables the entity to be
///     * pushable on contact, like boats or minecarts. </summary>
///     
		public virtual AxisAlignedBB getCollisionBox(Entity p_70114_1_)
		{
			return null;
		}

///    
///     <summary> * Handles updating while being ridden by an entity </summary>
///     
		public virtual void updateRidden()
		{
			if (this.ridingEntity.isDead)
			{
				this.ridingEntity = null;
			}
			else
			{
				this.motionX = 0.0D;
				this.motionY = 0.0D;
				this.motionZ = 0.0D;
				this.onUpdate();

				if (this.ridingEntity != null)
				{
					this.ridingEntity.updateRiderPosition();
					this.entityRiderYawDelta += (double)(this.ridingEntity.rotationYaw - this.ridingEntity.prevRotationYaw);

					for (this.entityRiderPitchDelta += (double)(this.ridingEntity.rotationPitch - this.ridingEntity.prevRotationPitch); this.entityRiderYawDelta >= 180.0D; this.entityRiderYawDelta -= 360.0D)
					{
						;
					}

					while (this.entityRiderYawDelta < -180.0D)
					{
						this.entityRiderYawDelta += 360.0D;
					}

					while (this.entityRiderPitchDelta >= 180.0D)
					{
						this.entityRiderPitchDelta -= 360.0D;
					}

					while (this.entityRiderPitchDelta < -180.0D)
					{
						this.entityRiderPitchDelta += 360.0D;
					}

					double var1 = this.entityRiderYawDelta * 0.5D;
					double var3 = this.entityRiderPitchDelta * 0.5D;
					float var5 = 10.0F;

					if (var1 > (double)var5)
					{
						var1 = (double)var5;
					}

					if (var1 < (double)(-var5))
					{
						var1 = (double)(-var5);
					}

					if (var3 > (double)var5)
					{
						var3 = (double)var5;
					}

					if (var3 < (double)(-var5))
					{
						var3 = (double)(-var5);
					}

					this.entityRiderYawDelta -= var1;
					this.entityRiderPitchDelta -= var3;
				}
			}
		}

		public virtual void updateRiderPosition()
		{
			if (this.riddenByEntity != null)
			{
				this.riddenByEntity.setPosition(this.posX, this.posY + this.MountedYOffset + this.riddenByEntity.YOffset, this.posZ);
			}
		}

///    
///     <summary> * Returns the Y Offset of this entity. </summary>
///     
		public virtual double YOffset
		{
			get
			{
				return (double)this.yOffset;
			}
		}

///    
///     <summary> * Returns the Y offset from the entity's position for any entity riding this one. </summary>
///     
		public virtual double MountedYOffset
		{
			get
			{
				return (double)this.height * 0.75D;
			}
		}

///    
///     <summary> * Called when a player mounts an entity. e.g. mounts a pig, mounts a boat. </summary>
///     
		public virtual void mountEntity(Entity p_70078_1_)
		{
			this.entityRiderPitchDelta = 0.0D;
			this.entityRiderYawDelta = 0.0D;

			if (p_70078_1_ == null)
			{
				if (this.ridingEntity != null)
				{
					this.setLocationAndAngles(this.ridingEntity.posX, this.ridingEntity.boundingBox.minY + (double)this.ridingEntity.height, this.ridingEntity.posZ, this.rotationYaw, this.rotationPitch);
					this.ridingEntity.riddenByEntity = null;
				}

				this.ridingEntity = null;
			}
			else
			{
				if (this.ridingEntity != null)
				{
					this.ridingEntity.riddenByEntity = null;
				}

				if (p_70078_1_ != null)
				{
					for (Entity var2 = p_70078_1_.ridingEntity; var2 != null; var2 = var2.ridingEntity)
					{
						if (var2 == this)
						{
							return;
						}
					}
				}

				this.ridingEntity = p_70078_1_;
				p_70078_1_.riddenByEntity = this;
			}
		}

///    
///     <summary> * Sets the position and rotation. Only difference from the other one is no bounding on the rotation. Args: posX,
///     * posY, posZ, yaw, pitch </summary>
///     
		public virtual void setPositionAndRotation2(double p_70056_1_, double p_70056_3_, double p_70056_5_, float p_70056_7_, float p_70056_8_, int p_70056_9_)
		{
			this.setPosition(p_70056_1_, p_70056_3_, p_70056_5_);
			this.setRotation(p_70056_7_, p_70056_8_);
			IList var10 = this.worldObj.getCollidingBoundingBoxes(this, this.boundingBox.contract(0.03125D, 0.0D, 0.03125D));

			if (!var10.Count == 0)
			{
				double var11 = 0.0D;

				for (int var13 = 0; var13 < var10.Count; ++var13)
				{
					AxisAlignedBB var14 = (AxisAlignedBB)var10[var13];

					if (var14.maxY > var11)
					{
						var11 = var14.maxY;
					}
				}

				p_70056_3_ += var11 - this.boundingBox.minY;
				this.setPosition(p_70056_1_, p_70056_3_, p_70056_5_);
			}
		}

		public virtual float CollisionBorderSize
		{
			get
			{
				return 0.1F;
			}
		}

///    
///     <summary> * returns a (normalized) vector of where this entity is looking </summary>
///     
		public virtual Vec3 LookVec
		{
			get
			{
				return null;
			}
		}

///    
///     <summary> * Called by portal blocks when an entity is within it. </summary>
///     
		public virtual void setInPortal()
		{
			if (this.timeUntilPortal > 0)
			{
				this.timeUntilPortal = this.PortalCooldown;
			}
			else
			{
				double var1 = this.prevPosX - this.posX;
				double var3 = this.prevPosZ - this.posZ;

				if (!this.worldObj.isClient && !this.inPortal)
				{
					this.teleportDirection = Direction.getMovementDirection(var1, var3);
				}

				this.inPortal = true;
			}
		}

///    
///     <summary> * Return the amount of cooldown before this entity can use a portal again. </summary>
///     
		public virtual int PortalCooldown
		{
			get
			{
				return 300;
			}
		}

///    
///     <summary> * Sets the velocity to the args. Args: x, y, z </summary>
///     
		public virtual void setVelocity(double p_70016_1_, double p_70016_3_, double p_70016_5_)
		{
			this.motionX = p_70016_1_;
			this.motionY = p_70016_3_;
			this.motionZ = p_70016_5_;
		}

		public virtual void handleHealthUpdate(sbyte p_70103_1_)
		{
		}

///    
///     <summary> * Setups the entity to do the hurt animation. Only used by packets in multiplayer. </summary>
///     
		public virtual void performHurtAnimation()
		{
		}

		public virtual ItemStack[] LastActiveItems
		{
			get
			{
				return null;
			}
		}

///    
///     <summary> * Sets the held item, or an armor slot. Slot 0 is held item. Slot 1-4 is armor. Params: Item, slot </summary>
///     
		public virtual void setCurrentItemOrArmor(int p_70062_1_, ItemStack p_70062_2_)
		{
		}

///    
///     <summary> * Returns true if the entity is on fire. Used by render to add the fire effect on rendering. </summary>
///     
		public virtual bool isBurning()
		{
			get
			{
				bool var1 = this.worldObj != null && this.worldObj.isClient;
				return !this.isImmuneToFire && (this.fire > 0 || var1 && this.getFlag(0));
			}
		}

///    
///     <summary> * Returns true if the entity is riding another entity, used by render to rotate the legs to be in 'sit' position
///     * for players. </summary>
///     
		public virtual bool isRiding()
		{
			get
			{
				return this.ridingEntity != null;
			}
		}

///    
///     <summary> * Returns if this entity is sneaking. </summary>
///     
		public virtual bool isSneaking()
		{
			get
			{
				return this.getFlag(1);
			}
			set
			{
				this.setFlag(1, value);
			}
		}

///    
///     <summary> * Sets the sneaking flag. </summary>
///     

///    
///     <summary> * Get if the Entity is sprinting. </summary>
///     
		public virtual bool isSprinting()
		{
			get
			{
				return this.getFlag(3);
			}
			set
			{
				this.setFlag(3, value);
			}
		}

///    
///     <summary> * Set sprinting switch for Entity. </summary>
///     

		public virtual bool isInvisible()
		{
			get
			{
				return this.getFlag(5);
			}
			set
			{
				this.setFlag(5, value);
			}
		}

///    
///     <summary> * Only used by renderer in EntityLivingBase subclasses.\nDetermines if an entity is visible or not to a specfic
///     * player, if the entity is normally invisible.\nFor EntityLivingBase subclasses, returning false when invisible
///     * will render the entity semitransparent. </summary>
///     
		public virtual bool isInvisibleToPlayer(EntityPlayer p_98034_1_)
		{
			return this.Invisible;
		}


		public virtual bool isEating()
		{
			get
			{
				return this.getFlag(4);
			}
			set
			{
				this.setFlag(4, value);
			}
		}


///    
///     <summary> * Returns true if the flag is active for the entity. Known flags: 0) is burning; 1) is sneaking; 2) is riding
///     * something; 3) is sprinting; 4) is eating </summary>
///     
		protected internal virtual bool getFlag(int p_70083_1_)
		{
			return (this.dataWatcher.getWatchableObjectByte(0) & 1 << p_70083_1_) != 0;
		}

///    
///     <summary> * Enable or disable a entity flag, see getEntityFlag to read the know flags. </summary>
///     
		protected internal virtual void setFlag(int p_70052_1_, bool p_70052_2_)
		{
			sbyte var3 = this.dataWatcher.getWatchableObjectByte(0);

			if (p_70052_2_)
			{
				this.dataWatcher.updateObject(0, Convert.ToByte((sbyte)(var3 | 1 << p_70052_1_)));
			}
			else
			{
				this.dataWatcher.updateObject(0, Convert.ToByte((sbyte)(var3 & ~(1 << p_70052_1_))));
			}
		}

		public virtual int Air
		{
			get
			{
				return this.dataWatcher.getWatchableObjectShort(1);
			}
			set
			{
				this.dataWatcher.updateObject(1, Convert.ToInt16((short)value));
			}
		}


///    
///     <summary> * Called when a lightning bolt hits the entity. </summary>
///     
		public virtual void onStruckByLightning(EntityLightningBolt p_70077_1_)
		{
			this.dealFireDamage(5);
			++this.fire;

			if (this.fire == 0)
			{
				this.Fire = 8;
			}
		}

///    
///     <summary> * This method gets called when the entity kills another one. </summary>
///     
		public virtual void onKillEntity(EntityLivingBase p_70074_1_)
		{
		}

		protected internal virtual bool func_145771_j(double p_145771_1_, double p_145771_3_, double p_145771_5_)
		{
			int var7 = MathHelper.floor_double(p_145771_1_);
			int var8 = MathHelper.floor_double(p_145771_3_);
			int var9 = MathHelper.floor_double(p_145771_5_);
			double var10 = p_145771_1_ - (double)var7;
			double var12 = p_145771_3_ - (double)var8;
			double var14 = p_145771_5_ - (double)var9;
			IList var16 = this.worldObj.func_147461_a(this.boundingBox);

			if (var16.Count == 0 && !this.worldObj.func_147469_q(var7, var8, var9))
			{
				return false;
			}
			else
			{
				bool var17 = !this.worldObj.func_147469_q(var7 - 1, var8, var9);
				bool var18 = !this.worldObj.func_147469_q(var7 + 1, var8, var9);
				bool var19 = !this.worldObj.func_147469_q(var7, var8 - 1, var9);
				bool var20 = !this.worldObj.func_147469_q(var7, var8 + 1, var9);
				bool var21 = !this.worldObj.func_147469_q(var7, var8, var9 - 1);
				bool var22 = !this.worldObj.func_147469_q(var7, var8, var9 + 1);
				sbyte var23 = 3;
				double var24 = 9999.0D;

				if (var17 && var10 < var24)
				{
					var24 = var10;
					var23 = 0;
				}

				if (var18 && 1.0D - var10 < var24)
				{
					var24 = 1.0D - var10;
					var23 = 1;
				}

				if (var20 && 1.0D - var12 < var24)
				{
					var24 = 1.0D - var12;
					var23 = 3;
				}

				if (var21 && var14 < var24)
				{
					var24 = var14;
					var23 = 4;
				}

				if (var22 && 1.0D - var14 < var24)
				{
					var24 = 1.0D - var14;
					var23 = 5;
				}

				float var26 = this.rand.nextFloat() * 0.2F + 0.1F;

				if (var23 == 0)
				{
					this.motionX = (double)(-var26);
				}

				if (var23 == 1)
				{
					this.motionX = (double)var26;
				}

				if (var23 == 2)
				{
					this.motionY = (double)(-var26);
				}

				if (var23 == 3)
				{
					this.motionY = (double)var26;
				}

				if (var23 == 4)
				{
					this.motionZ = (double)(-var26);
				}

				if (var23 == 5)
				{
					this.motionZ = (double)var26;
				}

				return true;
			}
		}

///    
///     <summary> * Sets the Entity inside a web block. </summary>
///     
		public virtual void setInWeb()
		{
			this.isInWeb = true;
			this.fallDistance = 0.0F;
		}

///    
///     <summary> * Gets the name of this command sender (usually username, but possibly "Rcon") </summary>
///     
		public virtual string CommandSenderName
		{
			get
			{
				string var1 = EntityList.getEntityString(this);
	
				if (var1 == null)
				{
					var1 = "generic";
				}
	
				return StatCollector.translateToLocal("entity." + var1 + ".name");
			}
		}

///    
///     <summary> * Return the Entity parts making up this Entity (currently only for dragons) </summary>
///     
		public virtual Entity[] Parts
		{
			get
			{
				return null;
			}
		}

///    
///     <summary> * Returns true if Entity argument is equal to this Entity </summary>
///     
		public virtual bool isEntityEqual(Entity p_70028_1_)
		{
			return this == p_70028_1_;
		}

		public virtual float RotationYawHead
		{
			get
			{
				return 0.0F;
			}
			set
			{
			}
		}

///    
///     <summary> * Sets the head's yaw rotation of the entity. </summary>
///     

///    
///     <summary> * If returns false, the item will not inflict any damage against entities. </summary>
///     
		public virtual bool canAttackWithItem()
		{
			return true;
		}

///    
///     <summary> * Called when a player attacks an entity. If this returns true the attack will not happen. </summary>
///     
		public virtual bool hitByEntity(Entity p_85031_1_)
		{
			return false;
		}

		public override string ToString()
		{
			return string.Format("{0}[\'{1}\'/{2:D}, l=\'{3}\', x={4:F2}, y={5:F2}, z={6:F2}]", new object[] {this.GetType().SimpleName, this.CommandSenderName, Convert.ToInt32(this.field_145783_c), this.worldObj == null ? "~NULL~" : this.worldObj.WorldInfo.WorldName, Convert.ToDouble(this.posX), Convert.ToDouble(this.posY), Convert.ToDouble(this.posZ)});
		}

///    
///     <summary> * Return whether this entity is invulnerable to damage. </summary>
///     
		public virtual bool isEntityInvulnerable()
		{
			get
			{
				return this.invulnerable;
			}
		}

///    
///     <summary> * Sets this entity's location and angles to the location and angles of the passed in entity. </summary>
///     
		public virtual void copyLocationAndAnglesFrom(Entity p_82149_1_)
		{
			this.setLocationAndAngles(p_82149_1_.posX, p_82149_1_.posY, p_82149_1_.posZ, p_82149_1_.rotationYaw, p_82149_1_.rotationPitch);
		}

///    
///     <summary> * Copies important data from another entity to this entity. Used when teleporting entities between worlds, as this
///     * actually deletes the teleporting entity and re-creates it on the other side. Params: Entity to copy from, unused
///     * (always true) </summary>
///     
		public virtual void copyDataFrom(Entity p_82141_1_, bool p_82141_2_)
		{
			NBTTagCompound var3 = new NBTTagCompound();
			p_82141_1_.writeToNBT(var3);
			this.readFromNBT(var3);
			this.timeUntilPortal = p_82141_1_.timeUntilPortal;
			this.teleportDirection = p_82141_1_.teleportDirection;
		}

///    
///     <summary> * Teleports the entity to another dimension. Params: Dimension number to teleport to </summary>
///     
		public virtual void travelToDimension(int p_71027_1_)
		{
			if (!this.worldObj.isClient && !this.isDead)
			{
				this.worldObj.theProfiler.startSection("changeDimension");
				MinecraftServer var2 = MinecraftServer.Server;
				int var3 = this.dimension;
				WorldServer var4 = var2.worldServerForDimension(var3);
				WorldServer var5 = var2.worldServerForDimension(p_71027_1_);
				this.dimension = p_71027_1_;

				if (var3 == 1 && p_71027_1_ == 1)
				{
					var5 = var2.worldServerForDimension(0);
					this.dimension = 0;
				}

				this.worldObj.removeEntity(this);
				this.isDead = false;
				this.worldObj.theProfiler.startSection("reposition");
				var2.ConfigurationManager.transferEntityToWorld(this, var3, var4, var5);
				this.worldObj.theProfiler.endStartSection("reloading");
				Entity var6 = EntityList.createEntityByName(EntityList.getEntityString(this), var5);

				if (var6 != null)
				{
					var6.copyDataFrom(this, true);

					if (var3 == 1 && p_71027_1_ == 1)
					{
						ChunkCoordinates var7 = var5.SpawnPoint;
						var7.posY = this.worldObj.getTopSolidOrLiquidBlock(var7.posX, var7.posZ);
						var6.setLocationAndAngles((double)var7.posX, (double)var7.posY, (double)var7.posZ, var6.rotationYaw, var6.rotationPitch);
					}

					var5.spawnEntityInWorld(var6);
				}

				this.isDead = true;
				this.worldObj.theProfiler.endSection();
				var4.resetUpdateEntityTick();
				var5.resetUpdateEntityTick();
				this.worldObj.theProfiler.endSection();
			}
		}

		public virtual float func_145772_a(Explosion p_145772_1_, World p_145772_2_, int p_145772_3_, int p_145772_4_, int p_145772_5_, Block p_145772_6_)
		{
			return p_145772_6_.getExplosionResistance(this);
		}

		public virtual bool func_145774_a(Explosion p_145774_1_, World p_145774_2_, int p_145774_3_, int p_145774_4_, int p_145774_5_, Block p_145774_6_, float p_145774_7_)
		{
			return true;
		}

///    
///     <summary> * The number of iterations PathFinder.getSafePoint will execute before giving up. </summary>
///     
		public virtual int MaxSafePointTries
		{
			get
			{
				return 3;
			}
		}

		public virtual int TeleportDirection
		{
			get
			{
				return this.teleportDirection;
			}
		}

		public virtual bool doesEntityNotTriggerPressurePlate()
		{
			return false;
		}

		public virtual void addEntityCrashInfo(CrashReportCategory p_85029_1_)
		{
			p_85029_1_.addCrashSectionCallable("Entity Type", new Callable() {  public string call() { return EntityList.getEntityString(Entity.this) + " (" + Entity.GetType().CanonicalName + ")"; } });
			p_85029_1_.addCrashSection("Entity ID", Convert.ToInt32(this.field_145783_c));
			p_85029_1_.addCrashSectionCallable("Entity Name", new Callable() {  public string call() { return Entity.CommandSenderName; } });
			p_85029_1_.addCrashSection("Entity\'s Exact location", string.Format("{0:F2}, {1:F2}, {2:F2}", new object[] {Convert.ToDouble(this.posX), Convert.ToDouble(this.posY), Convert.ToDouble(this.posZ)}));
			p_85029_1_.addCrashSection("Entity\'s Block location", CrashReportCategory.getLocationInfo(MathHelper.floor_double(this.posX), MathHelper.floor_double(this.posY), MathHelper.floor_double(this.posZ)));
			p_85029_1_.addCrashSection("Entity\'s Momentum", string.Format("{0:F2}, {1:F2}, {2:F2}", new object[] {Convert.ToDouble(this.motionX), Convert.ToDouble(this.motionY), Convert.ToDouble(this.motionZ)}));
		}

///    
///     <summary> * Return whether this entity should be rendered as on fire. </summary>
///     
		public virtual bool canRenderOnFire()
		{
			return this.Burning;
		}

		public virtual UUID UniqueID
		{
			get
			{
				return this.entityUniqueID;
			}
		}

		public virtual bool isPushedByWater()
		{
			get
			{
				return true;
			}
		}

		public virtual IChatComponent func_145748_c_()
		{
			return new ChatComponentText(this.CommandSenderName);
		}

		public virtual void func_145781_i(int p_145781_1_)
		{
		}

		public enum EnumEntitySize
		{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			SIZE_1("SIZE_1", 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			SIZE_2("SIZE_2", 1),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			SIZE_3("SIZE_3", 2),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			SIZE_4("SIZE_4", 3),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			SIZE_5("SIZE_5", 4),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			SIZE_6("SIZE_6", 5);

			@private static final Entity.EnumEntitySize[] $VALUES = new Entity.EnumEntitySize[]{SIZE_1, SIZE_2, SIZE_3, SIZE_4, SIZE_5, SIZE_6
		}
			

			private EnumEntitySize(string p_i1581_1_, int p_i1581_2_)
			{
			}

			public virtual int multiplyBy32AndRound(double p_75630_1_)
			{
				double var3 = p_75630_1_ - ((double)MathHelper.floor_double(p_75630_1_) + 0.5D);

				switch (Entity.SwitchEnumEntitySize.field_96565_a[this.ordinal()])
				{
					case 1:
						if (var3 < 0.0D)
						{
							if (var3 < -0.3125D)
							{
								return MathHelper.ceiling_double_int(p_75630_1_ * 32.0D);
							}
						}
						else if (var3 < 0.3125D)
						{
							return MathHelper.ceiling_double_int(p_75630_1_ * 32.0D);
						}

						return MathHelper.floor_double(p_75630_1_ * 32.0D);

					case 2:
						if (var3 < 0.0D)
						{
							if (var3 < -0.3125D)
							{
								return MathHelper.floor_double(p_75630_1_ * 32.0D);
							}
						}
						else if (var3 < 0.3125D)
						{
							return MathHelper.floor_double(p_75630_1_ * 32.0D);
						}

						return MathHelper.ceiling_double_int(p_75630_1_ * 32.0D);

					case 3:
						if (var3 > 0.0D)
						{
							return MathHelper.floor_double(p_75630_1_ * 32.0D);
						}

						return MathHelper.ceiling_double_int(p_75630_1_ * 32.0D);

					case 4:
						if (var3 < 0.0D)
						{
							if (var3 < -0.1875D)
							{
								return MathHelper.ceiling_double_int(p_75630_1_ * 32.0D);
							}
						}
						else if (var3 < 0.1875D)
						{
							return MathHelper.ceiling_double_int(p_75630_1_ * 32.0D);
						}

						return MathHelper.floor_double(p_75630_1_ * 32.0D);

					case 5:
						if (var3 < 0.0D)
						{
							if (var3 < -0.1875D)
							{
								return MathHelper.floor_double(p_75630_1_ * 32.0D);
							}
						}
						else if (var3 < 0.1875D)
						{
							return MathHelper.floor_double(p_75630_1_ * 32.0D);
						}

						return MathHelper.ceiling_double_int(p_75630_1_ * 32.0D);

					case 6:
					default:
						if (var3 > 0.0D)
						{
							return MathHelper.ceiling_double_int(p_75630_1_ * 32.0D);
						}
						else
						{
							return MathHelper.floor_double(p_75630_1_ * 32.0D);
						}
				}
			}
		}

		internal sealed class SwitchEnumEntitySize
		{
			internal static readonly int[] field_96565_a = new int[Entity.EnumEntitySize.values().length];
			

			static SwitchEnumEntitySize()
			{
				try
				{
					field_96565_a[Entity.EnumEntitySize.SIZE_1.ordinal()] = 1;
				}
				catch (NoSuchFieldError var6)
				{
					;
				}

				try
				{
					field_96565_a[Entity.EnumEntitySize.SIZE_2.ordinal()] = 2;
				}
				catch (NoSuchFieldError var5)
				{
					;
				}

				try
				{
					field_96565_a[Entity.EnumEntitySize.SIZE_3.ordinal()] = 3;
				}
				catch (NoSuchFieldError var4)
				{
					;
				}

				try
				{
					field_96565_a[Entity.EnumEntitySize.SIZE_4.ordinal()] = 4;
				}
				catch (NoSuchFieldError var3)
				{
					;
				}

				try
				{
					field_96565_a[Entity.EnumEntitySize.SIZE_5.ordinal()] = 5;
				}
				catch (NoSuchFieldError var2)
				{
					;
				}

				try
				{
					field_96565_a[Entity.EnumEntitySize.SIZE_6.ordinal()] = 6;
				}
				catch (NoSuchFieldError var1)
				{
					;
				}
			}
		}
	}

}