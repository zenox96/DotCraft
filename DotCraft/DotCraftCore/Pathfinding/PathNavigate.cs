using System;

namespace DotCraftCore.Pathfinding
{

	using Block = DotCraftCore.block.Block;
	using Material = DotCraftCore.block.material.Material;
	using Entity = DotCraftCore.entity.Entity;
	using EntityLiving = DotCraftCore.entity.EntityLiving;
	using SharedMonsterAttributes = DotCraftCore.entity.SharedMonsterAttributes;
	using IAttributeInstance = DotCraftCore.entity.ai.attributes.IAttributeInstance;
	using EntityZombie = DotCraftCore.entity.monster.EntityZombie;
	using EntityChicken = DotCraftCore.entity.passive.EntityChicken;
	using Blocks = DotCraftCore.init.Blocks;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using Vec3 = DotCraftCore.Util.Vec3;
	using World = DotCraftCore.World.World;

	public class PathNavigate
	{
		private EntityLiving theEntity;
		private World worldObj;

	/// <summary> The PathEntity being followed.  </summary>
		private PathEntity currentPath;
		private double speed;

///    
///     <summary> * The number of blocks (extra) +/- in each axis that get pulled out as cache for the pathfinder's search space </summary>
///     
		private IAttributeInstance pathSearchRange;
		private bool noSunPathfind;

	/// <summary> Time, in number of ticks, following the current path  </summary>
		private int totalTicks;

///    
///     <summary> * The time when the last position check was done (to detect successful movement) </summary>
///     
		private int ticksAtLastPos;

///    
///     <summary> * Coordinates of the entity's position last time a check was done (part of monitoring getting 'stuck') </summary>
///     
		private Vec3 lastPosCheck = Vec3.createVectorHelper(0.0D, 0.0D, 0.0D);

///    
///     <summary> * Specifically, if a wooden door block is even considered to be passable by the pathfinder </summary>
///     
		private bool canPassOpenWoodenDoors = true;

	/// <summary> If door blocks are considered passable even when closed  </summary>
		private bool canPassClosedWoodenDoors;

	/// <summary> If water blocks are avoided (at least by the pathfinder)  </summary>
		private bool avoidsWater;

///    
///     <summary> * If the entity can swim. Swimming AI enables this and the pathfinder will also cause the entity to swim straight
///     * upwards when underwater </summary>
///     
		private bool canSwim;
		private const string __OBFID = "CL_00001627";

		public PathNavigate(EntityLiving p_i1671_1_, World p_i1671_2_)
		{
			this.theEntity = p_i1671_1_;
			this.worldObj = p_i1671_2_;
			this.pathSearchRange = p_i1671_1_.getEntityAttribute(SharedMonsterAttributes.followRange);
		}

		public virtual bool AvoidsWater
		{
			set
			{
				this.avoidsWater = value;
			}
			get
			{
				return this.avoidsWater;
			}
		}


		public virtual bool BreakDoors
		{
			set
			{
				this.canPassClosedWoodenDoors = value;
			}
		}

///    
///     <summary> * Sets if the entity can enter open doors </summary>
///     
		public virtual bool EnterDoors
		{
			set
			{
				this.canPassOpenWoodenDoors = value;
			}
		}

///    
///     <summary> * Returns true if the entity can break doors, false otherwise </summary>
///     
		public virtual bool CanBreakDoors
		{
			get
			{
				return this.canPassClosedWoodenDoors;
			}
		}

///    
///     <summary> * Sets if the path should avoid sunlight </summary>
///     
		public virtual bool AvoidSun
		{
			set
			{
				this.noSunPathfind = value;
			}
		}

///    
///     <summary> * Sets the speed </summary>
///     
		public virtual double Speed
		{
			set
			{
				this.speed = value;
			}
		}

///    
///     <summary> * Sets if the entity can swim </summary>
///     
		public virtual bool CanSwim
		{
			set
			{
				this.canSwim = value;
			}
		}

		public virtual float func_111269_d()
		{
			return (float)this.pathSearchRange.AttributeValue;
		}

///    
///     <summary> * Returns the path to the given coordinates </summary>
///     
		public virtual PathEntity getPathToXYZ(double p_75488_1_, double p_75488_3_, double p_75488_5_)
		{
			return !this.canNavigate() ? null : this.worldObj.getEntityPathToXYZ(this.theEntity, MathHelper.floor_double(p_75488_1_), (int)p_75488_3_, MathHelper.floor_double(p_75488_5_), this.func_111269_d(), this.canPassOpenWoodenDoors, this.canPassClosedWoodenDoors, this.avoidsWater, this.canSwim);
		}

///    
///     <summary> * Try to find and set a path to XYZ. Returns true if successful. </summary>
///     
		public virtual bool tryMoveToXYZ(double p_75492_1_, double p_75492_3_, double p_75492_5_, double p_75492_7_)
		{
			PathEntity var9 = this.getPathToXYZ((double)MathHelper.floor_double(p_75492_1_), (double)((int)p_75492_3_), (double)MathHelper.floor_double(p_75492_5_));
			return this.setPath(var9, p_75492_7_);
		}

///    
///     <summary> * Returns the path to the given EntityLiving </summary>
///     
		public virtual PathEntity getPathToEntityLiving(Entity p_75494_1_)
		{
			return !this.canNavigate() ? null : this.worldObj.getPathEntityToEntity(this.theEntity, p_75494_1_, this.func_111269_d(), this.canPassOpenWoodenDoors, this.canPassClosedWoodenDoors, this.avoidsWater, this.canSwim);
		}

///    
///     <summary> * Try to find and set a path to EntityLiving. Returns true if successful. </summary>
///     
		public virtual bool tryMoveToEntityLiving(Entity p_75497_1_, double p_75497_2_)
		{
			PathEntity var4 = this.getPathToEntityLiving(p_75497_1_);
			return var4 != null ? this.setPath(var4, p_75497_2_) : false;
		}

///    
///     <summary> * sets the active path data if path is 100% unique compared to old path, checks to adjust path for sun avoiding
///     * ents and stores end coords </summary>
///     
		public virtual bool setPath(PathEntity p_75484_1_, double p_75484_2_)
		{
			if (p_75484_1_ == null)
			{
				this.currentPath = null;
				return false;
			}
			else
			{
				if (!p_75484_1_.isSamePath(this.currentPath))
				{
					this.currentPath = p_75484_1_;
				}

				if (this.noSunPathfind)
				{
					this.removeSunnyPath();
				}

				if (this.currentPath.CurrentPathLength == 0)
				{
					return false;
				}
				else
				{
					this.speed = p_75484_2_;
					Vec3 var4 = this.EntityPosition;
					this.ticksAtLastPos = this.totalTicks;
					this.lastPosCheck.xCoord = var4.xCoord;
					this.lastPosCheck.yCoord = var4.yCoord;
					this.lastPosCheck.zCoord = var4.zCoord;
					return true;
				}
			}
		}

///    
///     <summary> * gets the actively used PathEntity </summary>
///     
		public virtual PathEntity Path
		{
			get
			{
				return this.currentPath;
			}
		}

		public virtual void onUpdateNavigation()
		{
			++this.totalTicks;

			if (!this.noPath())
			{
				if (this.canNavigate())
				{
					this.pathFollow();
				}

				if (!this.noPath())
				{
					Vec3 var1 = this.currentPath.getPosition(this.theEntity);

					if (var1 != null)
					{
						this.theEntity.MoveHelper.setMoveTo(var1.xCoord, var1.yCoord, var1.zCoord, this.speed);
					}
				}
			}
		}

		private void pathFollow()
		{
			Vec3 var1 = this.EntityPosition;
			int var2 = this.currentPath.CurrentPathLength;

			for (int var3 = this.currentPath.CurrentPathIndex; var3 < this.currentPath.CurrentPathLength; ++var3)
			{
				if (this.currentPath.getPathPointFromIndex(var3).yCoord != (int)var1.yCoord)
				{
					var2 = var3;
					break;
				}
			}

			float var8 = this.theEntity.width * this.theEntity.width;
			int var4;

			for (var4 = this.currentPath.CurrentPathIndex; var4 < var2; ++var4)
			{
				if (var1.squareDistanceTo(this.currentPath.getVectorFromIndex(this.theEntity, var4)) < (double)var8)
				{
					this.currentPath.CurrentPathIndex = var4 + 1;
				}
			}

			var4 = MathHelper.ceiling_float_int(this.theEntity.width);
			int var5 = (int)this.theEntity.height + 1;
			int var6 = var4;

			for (int var7 = var2 - 1; var7 >= this.currentPath.CurrentPathIndex; --var7)
			{
				if (this.isDirectPathBetweenPoints(var1, this.currentPath.getVectorFromIndex(this.theEntity, var7), var4, var5, var6))
				{
					this.currentPath.CurrentPathIndex = var7;
					break;
				}
			}

			if (this.totalTicks - this.ticksAtLastPos > 100)
			{
				if (var1.squareDistanceTo(this.lastPosCheck) < 2.25D)
				{
					this.clearPathEntity();
				}

				this.ticksAtLastPos = this.totalTicks;
				this.lastPosCheck.xCoord = var1.xCoord;
				this.lastPosCheck.yCoord = var1.yCoord;
				this.lastPosCheck.zCoord = var1.zCoord;
			}
		}

///    
///     <summary> * If null path or reached the end </summary>
///     
		public virtual bool noPath()
		{
			return this.currentPath == null || this.currentPath.Finished;
		}

///    
///     <summary> * sets active PathEntity to null </summary>
///     
		public virtual void clearPathEntity()
		{
			this.currentPath = null;
		}

		private Vec3 EntityPosition
		{
			get
			{
				return Vec3.createVectorHelper(this.theEntity.posX, (double)this.PathableYPos, this.theEntity.posZ);
			}
		}

///    
///     <summary> * Gets the safe pathing Y position for the entity depending on if it can path swim or not </summary>
///     
		private int PathableYPos
		{
			get
			{
				if (this.theEntity.InWater && this.canSwim)
				{
					int var1 = (int)this.theEntity.boundingBox.minY;
					Block var2 = this.worldObj.getBlock(MathHelper.floor_double(this.theEntity.posX), var1, MathHelper.floor_double(this.theEntity.posZ));
					int var3 = 0;
	
					do
					{
						if (var2 != Blocks.flowing_water && var2 != Blocks.water)
						{
							return var1;
						}
	
						++var1;
						var2 = this.worldObj.getBlock(MathHelper.floor_double(this.theEntity.posX), var1, MathHelper.floor_double(this.theEntity.posZ));
						++var3;
					}
					while (var3 <= 16);
	
					return (int)this.theEntity.boundingBox.minY;
				}
				else
				{
					return (int)(this.theEntity.boundingBox.minY + 0.5D);
				}
			}
		}

///    
///     <summary> * If on ground or swimming and can swim </summary>
///     
		private bool canNavigate()
		{
			return this.theEntity.onGround || this.canSwim && this.InFluid || this.theEntity.Riding && this.theEntity is EntityZombie && this.theEntity.ridingEntity is EntityChicken;
		}

///    
///     <summary> * Returns true if the entity is in water or lava, false otherwise </summary>
///     
		private bool isInFluid()
		{
			get
			{
				return this.theEntity.InWater || this.theEntity.handleLavaMovement();
			}
		}

///    
///     <summary> * Trims path data from the end to the first sun covered block </summary>
///     
		private void removeSunnyPath()
		{
			if (!this.worldObj.canBlockSeeTheSky(MathHelper.floor_double(this.theEntity.posX), (int)(this.theEntity.boundingBox.minY + 0.5D), MathHelper.floor_double(this.theEntity.posZ)))
			{
				for (int var1 = 0; var1 < this.currentPath.CurrentPathLength; ++var1)
				{
					PathPoint var2 = this.currentPath.getPathPointFromIndex(var1);

					if (this.worldObj.canBlockSeeTheSky(var2.xCoord, var2.yCoord, var2.zCoord))
					{
						this.currentPath.CurrentPathLength = var1 - 1;
						return;
					}
				}
			}
		}

///    
///     <summary> * Returns true when an entity of specified size could safely walk in a straight line between the two points. Args:
///     * pos1, pos2, entityXSize, entityYSize, entityZSize </summary>
///     
		private bool isDirectPathBetweenPoints(Vec3 p_75493_1_, Vec3 p_75493_2_, int p_75493_3_, int p_75493_4_, int p_75493_5_)
		{
			int var6 = MathHelper.floor_double(p_75493_1_.xCoord);
			int var7 = MathHelper.floor_double(p_75493_1_.zCoord);
			double var8 = p_75493_2_.xCoord - p_75493_1_.xCoord;
			double var10 = p_75493_2_.zCoord - p_75493_1_.zCoord;
			double var12 = var8 * var8 + var10 * var10;

			if (var12 < 1.0E-8D)
			{
				return false;
			}
			else
			{
				double var14 = 1.0D / Math.Sqrt(var12);
				var8 *= var14;
				var10 *= var14;
				p_75493_3_ += 2;
				p_75493_5_ += 2;

				if (!this.isSafeToStandAt(var6, (int)p_75493_1_.yCoord, var7, p_75493_3_, p_75493_4_, p_75493_5_, p_75493_1_, var8, var10))
				{
					return false;
				}
				else
				{
					p_75493_3_ -= 2;
					p_75493_5_ -= 2;
					double var16 = 1.0D / Math.Abs(var8);
					double var18 = 1.0D / Math.Abs(var10);
					double var20 = (double)(var6 * 1) - p_75493_1_.xCoord;
					double var22 = (double)(var7 * 1) - p_75493_1_.zCoord;

					if (var8 >= 0.0D)
					{
						++var20;
					}

					if (var10 >= 0.0D)
					{
						++var22;
					}

					var20 /= var8;
					var22 /= var10;
					int var24 = var8 < 0.0D ? -1 : 1;
					int var25 = var10 < 0.0D ? -1 : 1;
					int var26 = MathHelper.floor_double(p_75493_2_.xCoord);
					int var27 = MathHelper.floor_double(p_75493_2_.zCoord);
					int var28 = var26 - var6;
					int var29 = var27 - var7;

					do
					{
						if (var28 * var24 <= 0 && var29 * var25 <= 0)
						{
							return true;
						}

						if (var20 < var22)
						{
							var20 += var16;
							var6 += var24;
							var28 = var26 - var6;
						}
						else
						{
							var22 += var18;
							var7 += var25;
							var29 = var27 - var7;
						}
					}
					while (this.isSafeToStandAt(var6, (int)p_75493_1_.yCoord, var7, p_75493_3_, p_75493_4_, p_75493_5_, p_75493_1_, var8, var10));

					return false;
				}
			}
		}

///    
///     <summary> * Returns true when an entity could stand at a position, including solid blocks under the entire entity. Args:
///     * xOffset, yOffset, zOffset, entityXSize, entityYSize, entityZSize, originPosition, vecX, vecZ </summary>
///     
		private bool isSafeToStandAt(int p_75483_1_, int p_75483_2_, int p_75483_3_, int p_75483_4_, int p_75483_5_, int p_75483_6_, Vec3 p_75483_7_, double p_75483_8_, double p_75483_10_)
		{
			int var12 = p_75483_1_ - p_75483_4_ / 2;
			int var13 = p_75483_3_ - p_75483_6_ / 2;

			if (!this.isPositionClear(var12, p_75483_2_, var13, p_75483_4_, p_75483_5_, p_75483_6_, p_75483_7_, p_75483_8_, p_75483_10_))
			{
				return false;
			}
			else
			{
				for (int var14 = var12; var14 < var12 + p_75483_4_; ++var14)
				{
					for (int var15 = var13; var15 < var13 + p_75483_6_; ++var15)
					{
						double var16 = (double)var14 + 0.5D - p_75483_7_.xCoord;
						double var18 = (double)var15 + 0.5D - p_75483_7_.zCoord;

						if (var16 * p_75483_8_ + var18 * p_75483_10_ >= 0.0D)
						{
							Block var20 = this.worldObj.getBlock(var14, p_75483_2_ - 1, var15);
							Material var21 = var20.Material;

							if (var21 == Material.air)
							{
								return false;
							}

							if (var21 == Material.water && !this.theEntity.InWater)
							{
								return false;
							}

							if (var21 == Material.lava)
							{
								return false;
							}
						}
					}
				}

				return true;
			}
		}

///    
///     <summary> * Returns true if an entity does not collide with any solid blocks at the position. Args: xOffset, yOffset,
///     * zOffset, entityXSize, entityYSize, entityZSize, originPosition, vecX, vecZ </summary>
///     
		private bool isPositionClear(int p_75496_1_, int p_75496_2_, int p_75496_3_, int p_75496_4_, int p_75496_5_, int p_75496_6_, Vec3 p_75496_7_, double p_75496_8_, double p_75496_10_)
		{
			for (int var12 = p_75496_1_; var12 < p_75496_1_ + p_75496_4_; ++var12)
			{
				for (int var13 = p_75496_2_; var13 < p_75496_2_ + p_75496_5_; ++var13)
				{
					for (int var14 = p_75496_3_; var14 < p_75496_3_ + p_75496_6_; ++var14)
					{
						double var15 = (double)var12 + 0.5D - p_75496_7_.xCoord;
						double var17 = (double)var14 + 0.5D - p_75496_7_.zCoord;

						if (var15 * p_75496_8_ + var17 * p_75496_10_ >= 0.0D)
						{
							Block var19 = this.worldObj.getBlock(var12, var13, var14);

							if (!var19.getBlocksMovement(this.worldObj, var12, var13, var14))
							{
								return false;
							}
						}
					}
				}
			}

			return true;
		}
	}

}