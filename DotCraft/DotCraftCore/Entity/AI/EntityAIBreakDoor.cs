namespace DotCraftCore.Entity.AI
{

	using Block = DotCraftCore.block.Block;
	using EntityLiving = DotCraftCore.Entity.EntityLiving;
	using EnumDifficulty = DotCraftCore.world.EnumDifficulty;

	public class EntityAIBreakDoor : EntityAIDoorInteract
	{
		private int breakingTime;
		private int field_75358_j = -1;
		private const string __OBFID = "CL_00001577";

		public EntityAIBreakDoor(EntityLiving p_i1618_1_) : base(p_i1618_1_)
		{
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			return !base.shouldExecute() ? false : (!this.theEntity.worldObj.GameRules.getGameRuleBooleanValue("mobGriefing") ? false : !this.field_151504_e.func_150015_f(this.theEntity.worldObj, this.entityPosX, this.entityPosY, this.entityPosZ));
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			base.startExecuting();
			this.breakingTime = 0;
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			double var1 = this.theEntity.getDistanceSq((double)this.entityPosX, (double)this.entityPosY, (double)this.entityPosZ);
			return this.breakingTime <= 240 && !this.field_151504_e.func_150015_f(this.theEntity.worldObj, this.entityPosX, this.entityPosY, this.entityPosZ) && var1 < 4.0D;
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			base.resetTask();
			this.theEntity.worldObj.destroyBlockInWorldPartially(this.theEntity.EntityId, this.entityPosX, this.entityPosY, this.entityPosZ, -1);
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			base.updateTask();

			if (this.theEntity.RNG.Next(20) == 0)
			{
				this.theEntity.worldObj.playAuxSFX(1010, this.entityPosX, this.entityPosY, this.entityPosZ, 0);
			}

			++this.breakingTime;
			int var1 = (int)((float)this.breakingTime / 240.0F * 10.0F);

			if (var1 != this.field_75358_j)
			{
				this.theEntity.worldObj.destroyBlockInWorldPartially(this.theEntity.EntityId, this.entityPosX, this.entityPosY, this.entityPosZ, var1);
				this.field_75358_j = var1;
			}

			if (this.breakingTime == 240 && this.theEntity.worldObj.difficultySetting == EnumDifficulty.HARD)
			{
				this.theEntity.worldObj.setBlockToAir(this.entityPosX, this.entityPosY, this.entityPosZ);
				this.theEntity.worldObj.playAuxSFX(1012, this.entityPosX, this.entityPosY, this.entityPosZ, 0);
				this.theEntity.worldObj.playAuxSFX(2001, this.entityPosX, this.entityPosY, this.entityPosZ, Block.getIdFromBlock(this.field_151504_e));
			}
		}
	}

}