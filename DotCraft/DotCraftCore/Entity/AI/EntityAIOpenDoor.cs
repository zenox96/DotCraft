namespace DotCraftCore.Entity.AI
{

	using EntityLiving = DotCraftCore.Entity.EntityLiving;

	public class EntityAIOpenDoor : EntityAIDoorInteract
	{
		internal bool field_75361_i;
		internal int field_75360_j;
		

		public EntityAIOpenDoor(EntityLiving p_i1644_1_, bool p_i1644_2_) : base(p_i1644_1_)
		{
			this.theEntity = p_i1644_1_;
			this.field_75361_i = p_i1644_2_;
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return this.field_75361_i && this.field_75360_j > 0 && base.continueExecuting();
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.field_75360_j = 20;
			this.field_151504_e.func_150014_a(this.theEntity.worldObj, this.entityPosX, this.entityPosY, this.entityPosZ, true);
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			if (this.field_75361_i)
			{
				this.field_151504_e.func_150014_a(this.theEntity.worldObj, this.entityPosX, this.entityPosY, this.entityPosZ, false);
			}
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			--this.field_75360_j;
			base.updateTask();
		}
	}

}