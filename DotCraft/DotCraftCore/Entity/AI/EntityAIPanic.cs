namespace DotCraftCore.nEntity.nAI
{

	using EntityCreature = DotCraftCore.nEntity.EntityCreature;
	using Vec3 = DotCraftCore.nUtil.Vec3;

	public class EntityAIPanic : EntityAIBase
	{
		private EntityCreature theEntityCreature;
		private double speed;
		private double randPosX;
		private double randPosY;
		private double randPosZ;
		

		public EntityAIPanic(EntityCreature p_i1645_1_, double p_i1645_2_)
		{
			this.theEntityCreature = p_i1645_1_;
			this.speed = p_i1645_2_;
			this.MutexBits = 1;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (this.theEntityCreature.AITarget == null && !this.theEntityCreature.Burning)
			{
				return false;
			}
			else
			{
				Vec3 var1 = RandomPositionGenerator.findRandomTarget(this.theEntityCreature, 5, 4);

				if (var1 == null)
				{
					return false;
				}
				else
				{
					this.randPosX = var1.xCoord;
					this.randPosY = var1.yCoord;
					this.randPosZ = var1.zCoord;
					return true;
				}
			}
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.theEntityCreature.Navigator.tryMoveToXYZ(this.randPosX, this.randPosY, this.randPosZ, this.speed);
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return !this.theEntityCreature.Navigator.noPath();
		}
	}

}