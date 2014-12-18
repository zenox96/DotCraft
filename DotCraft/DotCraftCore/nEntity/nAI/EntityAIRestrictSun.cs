namespace DotCraftCore.nEntity.nAI
{

	using EntityCreature = DotCraftCore.nEntity.EntityCreature;

	public class EntityAIRestrictSun : EntityAIBase
	{
		private EntityCreature theEntity;
		

		public EntityAIRestrictSun(EntityCreature p_i1652_1_)
		{
			this.theEntity = p_i1652_1_;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			return this.theEntity.worldObj.Daytime;
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.theEntity.Navigator.AvoidSun = true;
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.theEntity.Navigator.AvoidSun = false;
		}
	}

}