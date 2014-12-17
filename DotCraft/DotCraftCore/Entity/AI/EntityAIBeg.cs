namespace DotCraftCore.nEntity.nAI
{

	using EntityWolf = DotCraftCore.nEntity.nPassive.EntityWolf;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using Items = DotCraftCore.nInit.Items;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using World = DotCraftCore.nWorld.World;

	public class EntityAIBeg : EntityAIBase
	{
		private EntityWolf theWolf;
		private EntityPlayer thePlayer;
		private World worldObject;
		private float minPlayerDistance;
		private int field_75384_e;
		

		public EntityAIBeg(EntityWolf p_i1617_1_, float p_i1617_2_)
		{
			this.theWolf = p_i1617_1_;
			this.worldObject = p_i1617_1_.worldObj;
			this.minPlayerDistance = p_i1617_2_;
			this.MutexBits = 2;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			this.thePlayer = this.worldObject.getClosestPlayerToEntity(this.theWolf, (double)this.minPlayerDistance);
			return this.thePlayer == null ? false : this.hasPlayerGotBoneInHand(this.thePlayer);
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return !this.thePlayer.EntityAlive ? false : (this.theWolf.getDistanceSqToEntity(this.thePlayer) > (double)(this.minPlayerDistance * this.minPlayerDistance) ? false : this.field_75384_e > 0 && this.hasPlayerGotBoneInHand(this.thePlayer));
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.theWolf.func_70918_i(true);
			this.field_75384_e = 40 + this.theWolf.RNG.Next(40);
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.theWolf.func_70918_i(false);
			this.thePlayer = null;
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			this.theWolf.LookHelper.setLookPosition(this.thePlayer.posX, this.thePlayer.posY + (double)this.thePlayer.EyeHeight, this.thePlayer.posZ, 10.0F, (float)this.theWolf.VerticalFaceSpeed);
			--this.field_75384_e;
		}

///    
///     <summary> * Gets if the Player has the Bone in the hand. </summary>
///     
		private bool hasPlayerGotBoneInHand(EntityPlayer p_75382_1_)
		{
			ItemStack var2 = p_75382_1_.inventory.CurrentItem;
			return var2 == null ? false : (!this.theWolf.Tamed && var2.Item == Items.bone ? true : this.theWolf.isBreedingItem(var2));
		}
	}

}