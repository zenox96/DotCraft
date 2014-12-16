namespace DotCraftCore.Entity.AI
{

	using EntityIronGolem = DotCraftCore.Entity.Monster.EntityIronGolem;
	using EntityVillager = DotCraftCore.Entity.Passive.EntityVillager;

	public class EntityAILookAtVillager : EntityAIBase
	{
		private EntityIronGolem theGolem;
		private EntityVillager theVillager;
		private int lookTime;
		

		public EntityAILookAtVillager(EntityIronGolem p_i1643_1_)
		{
			this.theGolem = p_i1643_1_;
			this.MutexBits = 3;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (!this.theGolem.worldObj.Daytime)
			{
				return false;
			}
			else if (this.theGolem.RNG.Next(8000) != 0)
			{
				return false;
			}
			else
			{
				this.theVillager = (EntityVillager)this.theGolem.worldObj.findNearestEntityWithinAABB(typeof(EntityVillager), this.theGolem.boundingBox.expand(6.0D, 2.0D, 6.0D), this.theGolem);
				return this.theVillager != null;
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return this.lookTime > 0;
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.lookTime = 400;
			this.theGolem.HoldingRose = true;
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.theGolem.HoldingRose = false;
			this.theVillager = null;
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			this.theGolem.LookHelper.setLookPositionWithEntity(this.theVillager, 30.0F, 30.0F);
			--this.lookTime;
		}
	}

}