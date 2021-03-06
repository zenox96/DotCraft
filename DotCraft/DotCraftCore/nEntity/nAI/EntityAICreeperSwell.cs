namespace DotCraftCore.nEntity.nAI
{

	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using EntityCreeper = DotCraftCore.nEntity.nMonster.EntityCreeper;

	public class EntityAICreeperSwell : EntityAIBase
	{
	/// <summary> The creeper that is swelling.  </summary>
		internal EntityCreeper swellingCreeper;

///    
///     <summary> * The creeper's attack target. This is used for the changing of the creeper's state. </summary>
///     
		internal EntityLivingBase creeperAttackTarget;
		

		public EntityAICreeperSwell(EntityCreeper p_i1655_1_)
		{
			this.swellingCreeper = p_i1655_1_;
			this.MutexBits = 1;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			EntityLivingBase var1 = this.swellingCreeper.AttackTarget;
			return this.swellingCreeper.CreeperState > 0 || var1 != null && this.swellingCreeper.getDistanceSqToEntity(var1) < 9.0D;
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.swellingCreeper.Navigator.clearPathEntity();
			this.creeperAttackTarget = this.swellingCreeper.AttackTarget;
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.creeperAttackTarget = null;
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			if (this.creeperAttackTarget == null)
			{
				this.swellingCreeper.CreeperState = -1;
			}
			else if (this.swellingCreeper.getDistanceSqToEntity(this.creeperAttackTarget) > 49.0D)
			{
				this.swellingCreeper.CreeperState = -1;
			}
			else if (!this.swellingCreeper.EntitySenses.canSee(this.creeperAttackTarget))
			{
				this.swellingCreeper.CreeperState = -1;
			}
			else
			{
				this.swellingCreeper.CreeperState = 1;
			}
		}
	}

}