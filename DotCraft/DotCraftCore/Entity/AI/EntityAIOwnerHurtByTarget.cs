namespace DotCraftCore.Entity.AI
{

	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EntityTameable = DotCraftCore.Entity.Passive.EntityTameable;

	public class EntityAIOwnerHurtByTarget : EntityAITarget
	{
		internal EntityTameable theDefendingTameable;
		internal EntityLivingBase theOwnerAttacker;
		private int field_142051_e;
		private const string __OBFID = "CL_00001624";

		public EntityAIOwnerHurtByTarget(EntityTameable p_i1667_1_) : base(p_i1667_1_, false)
		{
			this.theDefendingTameable = p_i1667_1_;
			this.MutexBits = 1;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (!this.theDefendingTameable.Tamed)
			{
				return false;
			}
			else
			{
				EntityLivingBase var1 = this.theDefendingTameable.Owner;

				if (var1 == null)
				{
					return false;
				}
				else
				{
					this.theOwnerAttacker = var1.AITarget;
					int var2 = var1.func_142015_aE();
					return var2 != this.field_142051_e && this.isSuitableTarget(this.theOwnerAttacker, false) && this.theDefendingTameable.func_142018_a(this.theOwnerAttacker, var1);
				}
			}
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.taskOwner.AttackTarget = this.theOwnerAttacker;
			EntityLivingBase var1 = this.theDefendingTameable.Owner;

			if (var1 != null)
			{
				this.field_142051_e = var1.func_142015_aE();
			}

			base.startExecuting();
		}
	}

}