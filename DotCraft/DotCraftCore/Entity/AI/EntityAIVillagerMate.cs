namespace DotCraftCore.nEntity.nAI
{

	using Entity = DotCraftCore.nEntity.Entity;
	using EntityVillager = DotCraftCore.nEntity.nPassive.EntityVillager;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using Village = DotCraftCore.nVillage.Village;
	using World = DotCraftCore.nWorld.World;

	public class EntityAIVillagerMate : EntityAIBase
	{
		private EntityVillager villagerObj;
		private EntityVillager mate;
		private World worldObj;
		private int matingTimeout;
		internal Village villageObj;
		

		public EntityAIVillagerMate(EntityVillager p_i1634_1_)
		{
			this.villagerObj = p_i1634_1_;
			this.worldObj = p_i1634_1_.worldObj;
			this.MutexBits = 3;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (this.villagerObj.GrowingAge != 0)
			{
				return false;
			}
			else if (this.villagerObj.RNG.Next(500) != 0)
			{
				return false;
			}
			else
			{
				this.villageObj = this.worldObj.villageCollectionObj.findNearestVillage(MathHelper.floor_double(this.villagerObj.posX), MathHelper.floor_double(this.villagerObj.posY), MathHelper.floor_double(this.villagerObj.posZ), 0);

				if (this.villageObj == null)
				{
					return false;
				}
				else if (!this.checkSufficientDoorsPresentForNewVillager())
				{
					return false;
				}
				else
				{
					Entity var1 = this.worldObj.findNearestEntityWithinAABB(typeof(EntityVillager), this.villagerObj.boundingBox.expand(8.0D, 3.0D, 8.0D), this.villagerObj);

					if (var1 == null)
					{
						return false;
					}
					else
					{
						this.mate = (EntityVillager)var1;
						return this.mate.GrowingAge == 0;
					}
				}
			}
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.matingTimeout = 300;
			this.villagerObj.Mating = true;
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.villageObj = null;
			this.mate = null;
			this.villagerObj.Mating = false;
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return this.matingTimeout >= 0 && this.checkSufficientDoorsPresentForNewVillager() && this.villagerObj.GrowingAge == 0;
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			--this.matingTimeout;
			this.villagerObj.LookHelper.setLookPositionWithEntity(this.mate, 10.0F, 30.0F);

			if (this.villagerObj.getDistanceSqToEntity(this.mate) > 2.25D)
			{
				this.villagerObj.Navigator.tryMoveToEntityLiving(this.mate, 0.25D);
			}
			else if (this.matingTimeout == 0 && this.mate.Mating)
			{
				this.giveBirth();
			}

			if (this.villagerObj.RNG.Next(35) == 0)
			{
				this.worldObj.setEntityState(this.villagerObj, (sbyte)12);
			}
		}

		private bool checkSufficientDoorsPresentForNewVillager()
		{
			if (!this.villageObj.MatingSeason)
			{
				return false;
			}
			else
			{
				int var1 = (int)((double)((float)this.villageObj.NumVillageDoors) * 0.35D);
				return this.villageObj.NumVillagers < var1;
			}
		}

		private void giveBirth()
		{
			EntityVillager var1 = this.villagerObj.createChild(this.mate);
			this.mate.GrowingAge = 6000;
			this.villagerObj.GrowingAge = 6000;
			var1.GrowingAge = -24000;
			var1.setLocationAndAngles(this.villagerObj.posX, this.villagerObj.posY, this.villagerObj.posZ, 0.0F, 0.0F);
			this.worldObj.spawnEntityInWorld(var1);
			this.worldObj.setEntityState(var1, (sbyte)12);
		}
	}

}