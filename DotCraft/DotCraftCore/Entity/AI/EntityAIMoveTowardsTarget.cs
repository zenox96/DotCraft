namespace DotCraftCore.Entity.AI
{

	using EntityCreature = DotCraftCore.Entity.EntityCreature;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using Vec3 = DotCraftCore.util.Vec3;

	public class EntityAIMoveTowardsTarget : EntityAIBase
	{
		private EntityCreature theEntity;
		private EntityLivingBase targetEntity;
		private double movePosX;
		private double movePosY;
		private double movePosZ;
		private double speed;

///    
///     <summary> * If the distance to the target entity is further than this, this AI task will not run. </summary>
///     
		private float maxTargetDistance;
		private const string __OBFID = "CL_00001599";

		public EntityAIMoveTowardsTarget(EntityCreature p_i1640_1_, double p_i1640_2_, float p_i1640_4_)
		{
			this.theEntity = p_i1640_1_;
			this.speed = p_i1640_2_;
			this.maxTargetDistance = p_i1640_4_;
			this.MutexBits = 1;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			this.targetEntity = this.theEntity.AttackTarget;

			if (this.targetEntity == null)
			{
				return false;
			}
			else if (this.targetEntity.getDistanceSqToEntity(this.theEntity) > (double)(this.maxTargetDistance * this.maxTargetDistance))
			{
				return false;
			}
			else
			{
				Vec3 var1 = RandomPositionGenerator.findRandomTargetBlockTowards(this.theEntity, 16, 7, Vec3.createVectorHelper(this.targetEntity.posX, this.targetEntity.posY, this.targetEntity.posZ));

				if (var1 == null)
				{
					return false;
				}
				else
				{
					this.movePosX = var1.xCoord;
					this.movePosY = var1.yCoord;
					this.movePosZ = var1.zCoord;
					return true;
				}
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return !this.theEntity.Navigator.noPath() && this.targetEntity.EntityAlive && this.targetEntity.getDistanceSqToEntity(this.theEntity) < (double)(this.maxTargetDistance * this.maxTargetDistance);
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.targetEntity = null;
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.theEntity.Navigator.tryMoveToXYZ(this.movePosX, this.movePosY, this.movePosZ, this.speed);
		}
	}

}