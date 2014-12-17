namespace DotCraftCore.nEntity.nAI
{

	using EntityVillager = DotCraftCore.nEntity.nPassive.EntityVillager;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;

	public class EntityAILookAtTradePlayer : EntityAIWatchClosest
	{
		private readonly EntityVillager theMerchant;
		

		public EntityAILookAtTradePlayer(EntityVillager p_i1633_1_) : base(p_i1633_1_, EntityPlayer.class, 8.0F)
		{
			this.theMerchant = p_i1633_1_;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (this.theMerchant.Trading)
			{
				this.closestEntity = this.theMerchant.Customer;
				return true;
			}
			else
			{
				return false;
			}
		}
	}

}