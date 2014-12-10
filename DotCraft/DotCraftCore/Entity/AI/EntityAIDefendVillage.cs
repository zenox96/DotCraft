namespace DotCraftCore.Entity.AI
{

	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EntityIronGolem = DotCraftCore.Entity.Monster.EntityIronGolem;
	using Village = DotCraftCore.village.Village;

	public class EntityAIDefendVillage : EntityAITarget
	{
		internal EntityIronGolem irongolem;

///    
///     <summary> * The aggressor of the iron golem's village which is now the golem's attack target. </summary>
///     
		internal EntityLivingBase villageAgressorTarget;
		private const string __OBFID = "CL_00001618";

		public EntityAIDefendVillage(EntityIronGolem p_i1659_1_) : base(p_i1659_1_, false, true)
		{
			this.irongolem = p_i1659_1_;
			this.MutexBits = 1;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			Village var1 = this.irongolem.Village;

			if (var1 == null)
			{
				return false;
			}
			else
			{
				this.villageAgressorTarget = var1.findNearestVillageAggressor(this.irongolem);

				if (!this.isSuitableTarget(this.villageAgressorTarget, false))
				{
					if (this.taskOwner.RNG.Next(20) == 0)
					{
						this.villageAgressorTarget = var1.func_82685_c(this.irongolem);
						return this.isSuitableTarget(this.villageAgressorTarget, false);
					}
					else
					{
						return false;
					}
				}
				else
				{
					return true;
				}
			}
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.irongolem.AttackTarget = this.villageAgressorTarget;
			base.startExecuting();
		}
	}

}