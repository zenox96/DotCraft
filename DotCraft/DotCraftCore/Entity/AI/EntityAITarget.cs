namespace DotCraftCore.Entity.AI
{

	using EntityCreature = DotCraftCore.Entity.EntityCreature;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using IEntityOwnable = DotCraftCore.Entity.IEntityOwnable;
	using SharedMonsterAttributes = DotCraftCore.Entity.SharedMonsterAttributes;
	using IAttributeInstance = DotCraftCore.Entity.AI.Attributes.IAttributeInstance;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using EntityPlayerMP = DotCraftCore.Entity.Player.EntityPlayerMP;
	using PathEntity = DotCraftCore.Pathfinding.PathEntity;
	using PathPoint = DotCraftCore.Pathfinding.PathPoint;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using StringUtils = org.apache.commons.lang3.StringUtils;

	public abstract class EntityAITarget : EntityAIBase
	{
	/// <summary> The entity that this task belongs to  </summary>
		protected internal EntityCreature taskOwner;

///    
///     <summary> * If true, EntityAI targets must be able to be seen (cannot be blocked by walls) to be suitable targets. </summary>
///     
		protected internal bool shouldCheckSight;

///    
///     <summary> * When true, only entities that can be reached with minimal effort will be targetted. </summary>
///     
		private bool nearbyOnly;

///    
///     <summary> * When nearbyOnly is true: 0 -> No target, but OK to search; 1 -> Nearby target found; 2 -> Target too far. </summary>
///     
		private int targetSearchStatus;

///    
///     <summary> * When nearbyOnly is true, this throttles target searching to avoid excessive pathfinding. </summary>
///     
		private int targetSearchDelay;
		private int field_75298_g;
		

		public EntityAITarget(EntityCreature p_i1669_1_, bool p_i1669_2_) : this(p_i1669_1_, p_i1669_2_, false)
		{
		}

		public EntityAITarget(EntityCreature p_i1670_1_, bool p_i1670_2_, bool p_i1670_3_)
		{
			this.taskOwner = p_i1670_1_;
			this.shouldCheckSight = p_i1670_2_;
			this.nearbyOnly = p_i1670_3_;
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			EntityLivingBase var1 = this.taskOwner.AttackTarget;

			if (var1 == null)
			{
				return false;
			}
			else if (!var1.EntityAlive)
			{
				return false;
			}
			else
			{
				double var2 = this.TargetDistance;

				if (this.taskOwner.getDistanceSqToEntity(var1) > var2 * var2)
				{
					return false;
				}
				else
				{
					if (this.shouldCheckSight)
					{
						if (this.taskOwner.EntitySenses.canSee(var1))
						{
							this.field_75298_g = 0;
						}
						else if (++this.field_75298_g > 60)
						{
							return false;
						}
					}

					return !(var1 is EntityPlayerMP) || !((EntityPlayerMP)var1).theItemInWorldManager.Creative;
				}
			}
		}

		protected internal virtual double TargetDistance
		{
			get
			{
				IAttributeInstance var1 = this.taskOwner.getEntityAttribute(SharedMonsterAttributes.followRange);
				return var1 == null ? 16.0D : var1.AttributeValue;
			}
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.targetSearchStatus = 0;
			this.targetSearchDelay = 0;
			this.field_75298_g = 0;
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.taskOwner.AttackTarget = (EntityLivingBase)null;
		}

///    
///     <summary> * A method used to see if an entity is a suitable target through a number of checks. </summary>
///     
		protected internal virtual bool isSuitableTarget(EntityLivingBase p_75296_1_, bool p_75296_2_)
		{
			if (p_75296_1_ == null)
			{
				return false;
			}
			else if (p_75296_1_ == this.taskOwner)
			{
				return false;
			}
			else if (!p_75296_1_.EntityAlive)
			{
				return false;
			}
			else if (!this.taskOwner.canAttackClass(p_75296_1_.GetType()))
			{
				return false;
			}
			else
			{
				if (this.taskOwner is IEntityOwnable && StringUtils.isNotEmpty(((IEntityOwnable)this.taskOwner).func_152113_b()))
				{
					if (p_75296_1_ is IEntityOwnable && ((IEntityOwnable)this.taskOwner).func_152113_b().Equals(((IEntityOwnable)p_75296_1_).func_152113_b()))
					{
						return false;
					}

					if (p_75296_1_ == ((IEntityOwnable)this.taskOwner).Owner)
					{
						return false;
					}
				}
				else if (p_75296_1_ is EntityPlayer && !p_75296_2_ && ((EntityPlayer)p_75296_1_).capabilities.disableDamage)
				{
					return false;
				}

				if (!this.taskOwner.isWithinHomeDistance(MathHelper.floor_double(p_75296_1_.posX), MathHelper.floor_double(p_75296_1_.posY), MathHelper.floor_double(p_75296_1_.posZ)))
				{
					return false;
				}
				else if (this.shouldCheckSight && !this.taskOwner.EntitySenses.canSee(p_75296_1_))
				{
					return false;
				}
				else
				{
					if (this.nearbyOnly)
					{
						if (--this.targetSearchDelay <= 0)
						{
							this.targetSearchStatus = 0;
						}

						if (this.targetSearchStatus == 0)
						{
							this.targetSearchStatus = this.canEasilyReach(p_75296_1_) ? 1 : 2;
						}

						if (this.targetSearchStatus == 2)
						{
							return false;
						}
					}

					return true;
				}
			}
		}

///    
///     <summary> * Checks to see if this entity can find a short path to the given target. </summary>
///     
		private bool canEasilyReach(EntityLivingBase p_75295_1_)
		{
			this.targetSearchDelay = 10 + this.taskOwner.RNG.Next(5);
			PathEntity var2 = this.taskOwner.Navigator.getPathToEntityLiving(p_75295_1_);

			if (var2 == null)
			{
				return false;
			}
			else
			{
				PathPoint var3 = var2.FinalPathPoint;

				if (var3 == null)
				{
					return false;
				}
				else
				{
					int var4 = var3.xCoord - MathHelper.floor_double(p_75295_1_.posX);
					int var5 = var3.zCoord - MathHelper.floor_double(p_75295_1_.posZ);
					return (double)(var4 * var4 + var5 * var5) <= 2.25D;
				}
			}
		}
	}

}