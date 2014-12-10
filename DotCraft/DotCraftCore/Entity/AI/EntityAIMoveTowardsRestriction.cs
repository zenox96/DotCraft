namespace DotCraftCore.Entity.AI
{

	using EntityCreature = DotCraftCore.Entity.EntityCreature;
	using ChunkCoordinates = DotCraftCore.util.ChunkCoordinates;
	using Vec3 = DotCraftCore.util.Vec3;

	public class EntityAIMoveTowardsRestriction : EntityAIBase
	{
		private EntityCreature theEntity;
		private double movePosX;
		private double movePosY;
		private double movePosZ;
		private double movementSpeed;
		private const string __OBFID = "CL_00001598";

		public EntityAIMoveTowardsRestriction(EntityCreature p_i2347_1_, double p_i2347_2_)
		{
			this.theEntity = p_i2347_1_;
			this.movementSpeed = p_i2347_2_;
			this.MutexBits = 1;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (this.theEntity.WithinHomeDistanceCurrentPosition)
			{
				return false;
			}
			else
			{
				ChunkCoordinates var1 = this.theEntity.HomePosition;
				Vec3 var2 = RandomPositionGenerator.findRandomTargetBlockTowards(this.theEntity, 16, 7, Vec3.createVectorHelper((double)var1.posX, (double)var1.posY, (double)var1.posZ));

				if (var2 == null)
				{
					return false;
				}
				else
				{
					this.movePosX = var2.xCoord;
					this.movePosY = var2.yCoord;
					this.movePosZ = var2.zCoord;
					return true;
				}
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return !this.theEntity.Navigator.noPath();
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.theEntity.Navigator.tryMoveToXYZ(this.movePosX, this.movePosY, this.movePosZ, this.movementSpeed);
		}
	}

}