namespace DotCraftCore.Entity.AI
{

	using EntityLiving = DotCraftCore.Entity.EntityLiving;

	public class EntityAISwimming : EntityAIBase
	{
		private EntityLiving theEntity;
		

		public EntityAISwimming(EntityLiving p_i1624_1_)
		{
			this.theEntity = p_i1624_1_;
			this.MutexBits = 4;
			p_i1624_1_.Navigator.CanSwim = true;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			return this.theEntity.InWater || this.theEntity.handleLavaMovement();
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			if (this.theEntity.RNG.nextFloat() < 0.8F)
			{
				this.theEntity.JumpHelper.setJumping();
			}
		}
	}

}