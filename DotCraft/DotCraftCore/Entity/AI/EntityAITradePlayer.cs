namespace DotCraftCore.nEntity.nAI
{

	using EntityVillager = DotCraftCore.nEntity.nPassive.EntityVillager;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using Container = DotCraftCore.nInventory.Container;

	public class EntityAITradePlayer : EntityAIBase
	{
		private EntityVillager villager;
		

		public EntityAITradePlayer(EntityVillager p_i1658_1_)
		{
			this.villager = p_i1658_1_;
			this.MutexBits = 5;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (!this.villager.EntityAlive)
			{
				return false;
			}
			else if (this.villager.InWater)
			{
				return false;
			}
			else if (!this.villager.onGround)
			{
				return false;
			}
			else if (this.villager.velocityChanged)
			{
				return false;
			}
			else
			{
				EntityPlayer var1 = this.villager.Customer;
				return var1 == null ? false : (this.villager.getDistanceSqToEntity(var1) > 16.0D ? false : var1.openContainer is Container);
			}
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.villager.Navigator.clearPathEntity();
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.villager.Customer = (EntityPlayer)null;
		}
	}

}