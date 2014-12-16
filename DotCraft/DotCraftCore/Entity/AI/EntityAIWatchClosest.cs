using System;

namespace DotCraftCore.Entity.AI
{

	using Entity = DotCraftCore.Entity.Entity;
	using EntityLiving = DotCraftCore.Entity.EntityLiving;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;

	public class EntityAIWatchClosest : EntityAIBase
	{
		private EntityLiving theWatcher;

	/// <summary> The closest entity which is being watched by this one.  </summary>
		protected internal Entity closestEntity;

	/// <summary> This is the Maximum distance that the AI will look for the Entity  </summary>
		private float maxDistanceForPlayer;
		private int lookTime;
		private float field_75331_e;
		private Type watchedClass;
		

		public EntityAIWatchClosest(EntityLiving p_i1631_1_, Type p_i1631_2_, float p_i1631_3_)
		{
			this.theWatcher = p_i1631_1_;
			this.watchedClass = p_i1631_2_;
			this.maxDistanceForPlayer = p_i1631_3_;
			this.field_75331_e = 0.02F;
			this.MutexBits = 2;
		}

		public EntityAIWatchClosest(EntityLiving p_i1632_1_, Type p_i1632_2_, float p_i1632_3_, float p_i1632_4_)
		{
			this.theWatcher = p_i1632_1_;
			this.watchedClass = p_i1632_2_;
			this.maxDistanceForPlayer = p_i1632_3_;
			this.field_75331_e = p_i1632_4_;
			this.MutexBits = 2;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (this.theWatcher.RNG.nextFloat() >= this.field_75331_e)
			{
				return false;
			}
			else
			{
				if (this.theWatcher.AttackTarget != null)
				{
					this.closestEntity = this.theWatcher.AttackTarget;
				}

				if (this.watchedClass == typeof(EntityPlayer))
				{
					this.closestEntity = this.theWatcher.worldObj.getClosestPlayerToEntity(this.theWatcher, (double)this.maxDistanceForPlayer);
				}
				else
				{
					this.closestEntity = this.theWatcher.worldObj.findNearestEntityWithinAABB(this.watchedClass, this.theWatcher.boundingBox.expand((double)this.maxDistanceForPlayer, 3.0D, (double)this.maxDistanceForPlayer), this.theWatcher);
				}

				return this.closestEntity != null;
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return !this.closestEntity.EntityAlive ? false : (this.theWatcher.getDistanceSqToEntity(this.closestEntity) > (double)(this.maxDistanceForPlayer * this.maxDistanceForPlayer) ? false : this.lookTime > 0);
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.lookTime = 40 + this.theWatcher.RNG.Next(40);
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.closestEntity = null;
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			this.theWatcher.LookHelper.setLookPosition(this.closestEntity.posX, this.closestEntity.posY + (double)this.closestEntity.EyeHeight, this.closestEntity.posZ, 10.0F, (float)this.theWatcher.VerticalFaceSpeed);
			--this.lookTime;
		}
	}

}