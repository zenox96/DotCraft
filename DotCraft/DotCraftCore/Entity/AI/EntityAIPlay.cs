using System.Collections;

namespace DotCraftCore.nEntity.nAI
{

	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using EntityVillager = DotCraftCore.nEntity.nPassive.EntityVillager;
	using Vec3 = DotCraftCore.nUtil.Vec3;

	public class EntityAIPlay : EntityAIBase
	{
		private EntityVillager villagerObj;
		private EntityLivingBase targetVillager;
		private double field_75261_c;
		private int playTime;
		

		public EntityAIPlay(EntityVillager p_i1646_1_, double p_i1646_2_)
		{
			this.villagerObj = p_i1646_1_;
			this.field_75261_c = p_i1646_2_;
			this.MutexBits = 1;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (this.villagerObj.GrowingAge >= 0)
			{
				return false;
			}
			else if (this.villagerObj.RNG.Next(400) != 0)
			{
				return false;
			}
			else
			{
				IList var1 = this.villagerObj.worldObj.getEntitiesWithinAABB(typeof(EntityVillager), this.villagerObj.boundingBox.expand(6.0D, 3.0D, 6.0D));
				double var2 = double.MaxValue;
				IEnumerator var4 = var1.GetEnumerator();

				while (var4.MoveNext())
				{
					EntityVillager var5 = (EntityVillager)var4.Current;

					if (var5 != this.villagerObj && !var5.Playing && var5.GrowingAge < 0)
					{
						double var6 = var5.getDistanceSqToEntity(this.villagerObj);

						if (var6 <= var2)
						{
							var2 = var6;
							this.targetVillager = var5;
						}
					}
				}

				if (this.targetVillager == null)
				{
					Vec3 var8 = RandomPositionGenerator.findRandomTarget(this.villagerObj, 16, 3);

					if (var8 == null)
					{
						return false;
					}
				}

				return true;
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return this.playTime > 0;
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			if (this.targetVillager != null)
			{
				this.villagerObj.Playing = true;
			}

			this.playTime = 1000;
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.villagerObj.Playing = false;
			this.targetVillager = null;
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			--this.playTime;

			if (this.targetVillager != null)
			{
				if (this.villagerObj.getDistanceSqToEntity(this.targetVillager) > 4.0D)
				{
					this.villagerObj.Navigator.tryMoveToEntityLiving(this.targetVillager, this.field_75261_c);
				}
			}
			else if (this.villagerObj.Navigator.noPath())
			{
				Vec3 var1 = RandomPositionGenerator.findRandomTarget(this.villagerObj, 16, 3);

				if (var1 == null)
				{
					return;
				}

				this.villagerObj.Navigator.tryMoveToXYZ(var1.xCoord, var1.yCoord, var1.zCoord, this.field_75261_c);
			}
		}
	}

}