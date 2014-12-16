namespace DotCraftCore.Entity.AI
{

	using EntityCreature = DotCraftCore.Entity.EntityCreature;
	using Vec3 = DotCraftCore.Util.Vec3;

	public class EntityAIWander : EntityAIBase
	{
		private EntityCreature entity;
		private double xPosition;
		private double yPosition;
		private double zPosition;
		private double speed;
		

		public EntityAIWander(EntityCreature p_i1648_1_, double p_i1648_2_)
		{
			this.entity = p_i1648_1_;
			this.speed = p_i1648_2_;
			this.MutexBits = 1;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (this.entity.Age >= 100)
			{
				return false;
			}
			else if (this.entity.RNG.Next(120) != 0)
			{
				return false;
			}
			else
			{
				Vec3 var1 = RandomPositionGenerator.findRandomTarget(this.entity, 10, 7);

				if (var1 == null)
				{
					return false;
				}
				else
				{
					this.xPosition = var1.xCoord;
					this.yPosition = var1.yCoord;
					this.zPosition = var1.zCoord;
					return true;
				}
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return !this.entity.Navigator.noPath();
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.entity.Navigator.tryMoveToXYZ(this.xPosition, this.yPosition, this.zPosition, this.speed);
		}
	}

}