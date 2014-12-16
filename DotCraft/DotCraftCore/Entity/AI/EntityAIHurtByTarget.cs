using System.Collections;

namespace DotCraftCore.Entity.AI
{

	using EntityCreature = DotCraftCore.Entity.EntityCreature;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;

	public class EntityAIHurtByTarget : EntityAITarget
	{
		internal bool entityCallsForHelp;
		private int field_142052_b;
		

		public EntityAIHurtByTarget(EntityCreature p_i1660_1_, bool p_i1660_2_) : base(p_i1660_1_, false)
		{
			this.entityCallsForHelp = p_i1660_2_;
			this.MutexBits = 1;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			int var1 = this.taskOwner.func_142015_aE();
			return var1 != this.field_142052_b && this.isSuitableTarget(this.taskOwner.AITarget, false);
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.taskOwner.AttackTarget = this.taskOwner.AITarget;
			this.field_142052_b = this.taskOwner.func_142015_aE();

			if (this.entityCallsForHelp)
			{
				double var1 = this.TargetDistance;
				IList var3 = this.taskOwner.worldObj.getEntitiesWithinAABB(this.taskOwner.GetType(), AxisAlignedBB.getBoundingBox(this.taskOwner.posX, this.taskOwner.posY, this.taskOwner.posZ, this.taskOwner.posX + 1.0D, this.taskOwner.posY + 1.0D, this.taskOwner.posZ + 1.0D).expand(var1, 10.0D, var1));
				IEnumerator var4 = var3.GetEnumerator();

				while (var4.MoveNext())
				{
					EntityCreature var5 = (EntityCreature)var4.Current;

					if (this.taskOwner != var5 && var5.AttackTarget == null && !var5.isOnSameTeam(this.taskOwner.AITarget))
					{
						var5.AttackTarget = this.taskOwner.AITarget;
					}
				}
			}

			base.startExecuting();
		}
	}

}