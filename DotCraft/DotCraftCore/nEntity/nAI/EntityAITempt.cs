using System;

namespace DotCraftCore.nEntity.nAI
{

	using EntityCreature = DotCraftCore.nEntity.EntityCreature;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using Item = DotCraftCore.nItem.Item;
	using ItemStack = DotCraftCore.nItem.ItemStack;

	public class EntityAITempt : EntityAIBase
	{
	/// <summary> The entity using this AI that is tempted by the player.  </summary>
		private EntityCreature temptedEntity;
		private double field_75282_b;

	/// <summary> X position of player tempting this mob  </summary>
		private double targetX;

	/// <summary> Y position of player tempting this mob  </summary>
		private double targetY;

	/// <summary> Z position of player tempting this mob  </summary>
		private double targetZ;
		private double field_75278_f;
		private double field_75279_g;

	/// <summary> The player that is tempting the entity that is using this AI.  </summary>
		private EntityPlayer temptingPlayer;

///    
///     <summary> * A counter that is decremented each time the shouldExecute method is called. The shouldExecute method will always
///     * return false if delayTemptCounter is greater than 0. </summary>
///     
		private int delayTemptCounter;

	/// <summary> True if this EntityAITempt task is running  </summary>
		private bool isRunning;
		private Item field_151484_k;

///    
///     <summary> * Whether the entity using this AI will be scared by the tempter's sudden movement. </summary>
///     
		private bool scaredByPlayerMovement;
		private bool field_75286_m;
		

		public EntityAITempt(EntityCreature p_i45316_1_, double p_i45316_2_, Item p_i45316_4_, bool p_i45316_5_)
		{
			this.temptedEntity = p_i45316_1_;
			this.field_75282_b = p_i45316_2_;
			this.field_151484_k = p_i45316_4_;
			this.scaredByPlayerMovement = p_i45316_5_;
			this.MutexBits = 3;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (this.delayTemptCounter > 0)
			{
				--this.delayTemptCounter;
				return false;
			}
			else
			{
				this.temptingPlayer = this.temptedEntity.worldObj.getClosestPlayerToEntity(this.temptedEntity, 10.0D);

				if (this.temptingPlayer == null)
				{
					return false;
				}
				else
				{
					ItemStack var1 = this.temptingPlayer.CurrentEquippedItem;
					return var1 == null ? false : var1.Item == this.field_151484_k;
				}
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			if (this.scaredByPlayerMovement)
			{
				if (this.temptedEntity.getDistanceSqToEntity(this.temptingPlayer) < 36.0D)
				{
					if (this.temptingPlayer.getDistanceSq(this.targetX, this.targetY, this.targetZ) > 0.010000000000000002D)
					{
						return false;
					}

					if (Math.Abs((double)this.temptingPlayer.rotationPitch - this.field_75278_f) > 5.0D || Math.Abs((double)this.temptingPlayer.rotationYaw - this.field_75279_g) > 5.0D)
					{
						return false;
					}
				}
				else
				{
					this.targetX = this.temptingPlayer.posX;
					this.targetY = this.temptingPlayer.posY;
					this.targetZ = this.temptingPlayer.posZ;
				}

				this.field_75278_f = (double)this.temptingPlayer.rotationPitch;
				this.field_75279_g = (double)this.temptingPlayer.rotationYaw;
			}

			return this.shouldExecute();
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.targetX = this.temptingPlayer.posX;
			this.targetY = this.temptingPlayer.posY;
			this.targetZ = this.temptingPlayer.posZ;
			this.isRunning = true;
			this.field_75286_m = this.temptedEntity.Navigator.AvoidsWater;
			this.temptedEntity.Navigator.AvoidsWater = false;
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.temptingPlayer = null;
			this.temptedEntity.Navigator.clearPathEntity();
			this.delayTemptCounter = 100;
			this.isRunning = false;
			this.temptedEntity.Navigator.AvoidsWater = this.field_75286_m;
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			this.temptedEntity.LookHelper.setLookPositionWithEntity(this.temptingPlayer, 30.0F, (float)this.temptedEntity.VerticalFaceSpeed);

			if (this.temptedEntity.getDistanceSqToEntity(this.temptingPlayer) < 6.25D)
			{
				this.temptedEntity.Navigator.clearPathEntity();
			}
			else
			{
				this.temptedEntity.Navigator.tryMoveToEntityLiving(this.temptingPlayer, this.field_75282_b);
			}
		}

///    
///     * <seealso cref= #isRunning </seealso>
///     
		public virtual bool isRunning()
		{
			get
			{
				return this.isRunning;
			}
		}
	}

}