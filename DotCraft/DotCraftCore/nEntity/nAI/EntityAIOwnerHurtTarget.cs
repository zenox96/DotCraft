namespace DotCraftCore.nEntity.nAI
{

	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using EntityTameable = DotCraftCore.nEntity.nPassive.EntityTameable;

	public class EntityAIOwnerHurtTarget : EntityAITarget
	{
		internal EntityTameable theEntityTameable;
		internal EntityLivingBase theTarget;
		private int field_142050_e;
		

		public EntityAIOwnerHurtTarget(EntityTameable p_i1668_1_) : base(p_i1668_1_, false)
		{
			this.theEntityTameable = p_i1668_1_;
			this.MutexBits = 1;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (!this.theEntityTameable.Tamed)
			{
				return false;
			}
			else
			{
				EntityLivingBase var1 = this.theEntityTameable.Owner;

				if (var1 == null)
				{
					return false;
				}
				else
				{
					this.theTarget = var1.LastAttacker;
					int var2 = var1.LastAttackerTime;
					return var2 != this.field_142050_e && this.isSuitableTarget(this.theTarget, false) && this.theEntityTameable.func_142018_a(this.theTarget, var1);
				}
			}
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.taskOwner.AttackTarget = this.theTarget;
			EntityLivingBase var1 = this.theEntityTameable.Owner;

			if (var1 != null)
			{
				this.field_142050_e = var1.LastAttackerTime;
			}

			base.startExecuting();
		}
	}

}