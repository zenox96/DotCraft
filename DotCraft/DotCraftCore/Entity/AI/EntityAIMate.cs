using System;
using System.Collections;

namespace DotCraftCore.nEntity.nAI
{

	using EntityAgeable = DotCraftCore.nEntity.EntityAgeable;
	using EntityXPOrb = DotCraftCore.nEntity.nItem.EntityXPOrb;
	using EntityAnimal = DotCraftCore.nEntity.nPassive.EntityAnimal;
	using EntityCow = DotCraftCore.nEntity.nPassive.EntityCow;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using AchievementList = DotCraftCore.nStats.AchievementList;
	using StatList = DotCraftCore.nStats.StatList;
	using World = DotCraftCore.nWorld.World;

	public class EntityAIMate : EntityAIBase
	{
		private EntityAnimal theAnimal;
		internal World theWorld;
		private EntityAnimal targetMate;

///    
///     <summary> * Delay preventing a baby from spawning immediately when two mate-able animals find each other. </summary>
///     
		internal int spawnBabyDelay;

	/// <summary> The speed the creature moves at during mating behavior.  </summary>
		internal double moveSpeed;
		

		public EntityAIMate(EntityAnimal p_i1619_1_, double p_i1619_2_)
		{
			this.theAnimal = p_i1619_1_;
			this.theWorld = p_i1619_1_.worldObj;
			this.moveSpeed = p_i1619_2_;
			this.MutexBits = 3;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (!this.theAnimal.InLove)
			{
				return false;
			}
			else
			{
				this.targetMate = this.NearbyMate;
				return this.targetMate != null;
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return this.targetMate.EntityAlive && this.targetMate.InLove && this.spawnBabyDelay < 60;
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.targetMate = null;
			this.spawnBabyDelay = 0;
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			this.theAnimal.LookHelper.setLookPositionWithEntity(this.targetMate, 10.0F, (float)this.theAnimal.VerticalFaceSpeed);
			this.theAnimal.Navigator.tryMoveToEntityLiving(this.targetMate, this.moveSpeed);
			++this.spawnBabyDelay;

			if (this.spawnBabyDelay >= 60 && this.theAnimal.getDistanceSqToEntity(this.targetMate) < 9.0D)
			{
				this.spawnBaby();
			}
		}

///    
///     <summary> * Loops through nearby animals and finds another animal of the same type that can be mated with. Returns the first
///     * valid mate found. </summary>
///     
		private EntityAnimal NearbyMate
		{
			get
			{
				float var1 = 8.0F;
				IList var2 = this.theWorld.getEntitiesWithinAABB(this.theAnimal.GetType(), this.theAnimal.boundingBox.expand((double)var1, (double)var1, (double)var1));
				double var3 = double.MaxValue;
				EntityAnimal var5 = null;
				IEnumerator var6 = var2.GetEnumerator();
	
				while (var6.MoveNext())
				{
					EntityAnimal var7 = (EntityAnimal)var6.Current;
	
					if (this.theAnimal.canMateWith(var7) && this.theAnimal.getDistanceSqToEntity(var7) < var3)
					{
						var5 = var7;
						var3 = this.theAnimal.getDistanceSqToEntity(var7);
					}
				}
	
				return var5;
			}
		}

///    
///     <summary> * Spawns a baby animal of the same type. </summary>
///     
		private void spawnBaby()
		{
			EntityAgeable var1 = this.theAnimal.createChild(this.targetMate);

			if (var1 != null)
			{
				EntityPlayer var2 = this.theAnimal.func_146083_cb();

				if (var2 == null && this.targetMate.func_146083_cb() != null)
				{
					var2 = this.targetMate.func_146083_cb();
				}

				if (var2 != null)
				{
					var2.triggerAchievement(StatList.field_151186_x);

					if (this.theAnimal is EntityCow)
					{
						var2.triggerAchievement(AchievementList.field_150962_H);
					}
				}

				this.theAnimal.GrowingAge = 6000;
				this.targetMate.GrowingAge = 6000;
				this.theAnimal.resetInLove();
				this.targetMate.resetInLove();
				var1.GrowingAge = -24000;
				var1.setLocationAndAngles(this.theAnimal.posX, this.theAnimal.posY, this.theAnimal.posZ, 0.0F, 0.0F);
				this.theWorld.spawnEntityInWorld(var1);
				Random var3 = this.theAnimal.RNG;

				for (int var4 = 0; var4 < 7; ++var4)
				{
					double var5 = var3.nextGaussian() * 0.02D;
					double var7 = var3.nextGaussian() * 0.02D;
					double var9 = var3.nextGaussian() * 0.02D;
					this.theWorld.spawnParticle("heart", this.theAnimal.posX + (double)(var3.nextFloat() * this.theAnimal.width * 2.0F) - (double)this.theAnimal.width, this.theAnimal.posY + 0.5D + (double)(var3.nextFloat() * this.theAnimal.height), this.theAnimal.posZ + (double)(var3.nextFloat() * this.theAnimal.width * 2.0F) - (double)this.theAnimal.width, var5, var7, var9);
				}

				if (this.theWorld.GameRules.getGameRuleBooleanValue("doMobLoot"))
				{
					this.theWorld.spawnEntityInWorld(new EntityXPOrb(this.theWorld, this.theAnimal.posX, this.theAnimal.posY, this.theAnimal.posZ, var3.Next(7) + 1));
				}
			}
		}
	}

}