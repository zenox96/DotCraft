using System;

namespace DotCraftCore.nEntity.nAI
{

	using EntityLiving = DotCraftCore.nEntity.EntityLiving;
	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using World = DotCraftCore.nWorld.World;

	public class EntityAIOcelotAttack : EntityAIBase
	{
		internal World theWorld;
		internal EntityLiving theEntity;
		internal EntityLivingBase theVictim;
		internal int attackCountdown;
		

		public EntityAIOcelotAttack(EntityLiving p_i1641_1_)
		{
			this.theEntity = p_i1641_1_;
			this.theWorld = p_i1641_1_.worldObj;
			this.MutexBits = 3;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			EntityLivingBase var1 = this.theEntity.AttackTarget;

			if (var1 == null)
			{
				return false;
			}
			else
			{
				this.theVictim = var1;
				return true;
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return !this.theVictim.EntityAlive ? false : (this.theEntity.getDistanceSqToEntity(this.theVictim) > 225.0D ? false : !this.theEntity.Navigator.noPath() || this.shouldExecute());
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.theVictim = null;
			this.theEntity.Navigator.clearPathEntity();
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			this.theEntity.LookHelper.setLookPositionWithEntity(this.theVictim, 30.0F, 30.0F);
			double var1 = (double)(this.theEntity.width * 2.0F * this.theEntity.width * 2.0F);
			double var3 = this.theEntity.getDistanceSq(this.theVictim.posX, this.theVictim.boundingBox.minY, this.theVictim.posZ);
			double var5 = 0.8D;

			if (var3 > var1 && var3 < 16.0D)
			{
				var5 = 1.33D;
			}
			else if (var3 < 225.0D)
			{
				var5 = 0.6D;
			}

			this.theEntity.Navigator.tryMoveToEntityLiving(this.theVictim, var5);
			this.attackCountdown = Math.Max(this.attackCountdown - 1, 0);

			if (var3 <= var1)
			{
				if (this.attackCountdown <= 0)
				{
					this.attackCountdown = 20;
					this.theEntity.attackEntityAsMob(this.theVictim);
				}
			}
		}
	}

}