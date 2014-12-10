namespace DotCraftCore.Entity.AI
{

	using EntityVillager = DotCraftCore.Entity.Passive.EntityVillager;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;

	public class EntityAILookAtTradePlayer : EntityAIWatchClosest
	{
		private readonly EntityVillager theMerchant;
		private const string __OBFID = "CL_00001593";

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