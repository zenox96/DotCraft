namespace DotCraftCore.Entity.AI
{

	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EntityTameable = DotCraftCore.Entity.Passive.EntityTameable;

	public class EntityAISit : EntityAIBase
	{
		private EntityTameable theEntity;

	/// <summary> If the EntityTameable is sitting.  </summary>
		private bool isSitting;
		private const string __OBFID = "CL_00001613";

		public EntityAISit(EntityTameable p_i1654_1_)
		{
			this.theEntity = p_i1654_1_;
			this.MutexBits = 5;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (!this.theEntity.Tamed)
			{
				return false;
			}
			else if (this.theEntity.InWater)
			{
				return false;
			}
			else if (!this.theEntity.onGround)
			{
				return false;
			}
			else
			{
				EntityLivingBase var1 = this.theEntity.Owner;
				return var1 == null ? true : (this.theEntity.getDistanceSqToEntity(var1) < 144.0D && var1.AITarget != null ? false : this.isSitting);
			}
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.theEntity.Navigator.clearPathEntity();
			this.theEntity.Sitting = true;
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.theEntity.Sitting = false;
		}

///    
///     <summary> * Sets the sitting flag. </summary>
///     
		public virtual bool Sitting
		{
			set
			{
				this.isSitting = value;
			}
		}
	}

}