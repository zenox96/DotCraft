namespace DotCraftCore.Entity.AI
{

	using EntityCreature = DotCraftCore.Entity.EntityCreature;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using Vec3 = DotCraftCore.Util.Vec3;
	using Village = DotCraftCore.Village.Village;
	using VillageDoorInfo = DotCraftCore.Village.VillageDoorInfo;

	public class EntityAIMoveIndoors : EntityAIBase
	{
		private EntityCreature entityObj;
		private VillageDoorInfo doorInfo;
		private int insidePosX = -1;
		private int insidePosZ = -1;
		

		public EntityAIMoveIndoors(EntityCreature p_i1637_1_)
		{
			this.entityObj = p_i1637_1_;
			this.MutexBits = 1;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			int var1 = MathHelper.floor_double(this.entityObj.posX);
			int var2 = MathHelper.floor_double(this.entityObj.posY);
			int var3 = MathHelper.floor_double(this.entityObj.posZ);

			if ((!this.entityObj.worldObj.Daytime || this.entityObj.worldObj.Raining || !this.entityObj.worldObj.getBiomeGenForCoords(var1, var3).canSpawnLightningBolt()) && !this.entityObj.worldObj.provider.hasNoSky)
			{
				if (this.entityObj.RNG.Next(50) != 0)
				{
					return false;
				}
				else if (this.insidePosX != -1 && this.entityObj.getDistanceSq((double)this.insidePosX, this.entityObj.posY, (double)this.insidePosZ) < 4.0D)
				{
					return false;
				}
				else
				{
					Village var4 = this.entityObj.worldObj.villageCollectionObj.findNearestVillage(var1, var2, var3, 14);

					if (var4 == null)
					{
						return false;
					}
					else
					{
						this.doorInfo = var4.findNearestDoorUnrestricted(var1, var2, var3);
						return this.doorInfo != null;
					}
				}
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return !this.entityObj.Navigator.noPath();
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.insidePosX = -1;

			if (this.entityObj.getDistanceSq((double)this.doorInfo.InsidePosX, (double)this.doorInfo.posY, (double)this.doorInfo.InsidePosZ) > 256.0D)
			{
				Vec3 var1 = RandomPositionGenerator.findRandomTargetBlockTowards(this.entityObj, 14, 3, Vec3.createVectorHelper((double)this.doorInfo.InsidePosX + 0.5D, (double)this.doorInfo.InsidePosY, (double)this.doorInfo.InsidePosZ + 0.5D));

				if (var1 != null)
				{
					this.entityObj.Navigator.tryMoveToXYZ(var1.xCoord, var1.yCoord, var1.zCoord, 1.0D);
				}
			}
			else
			{
				this.entityObj.Navigator.tryMoveToXYZ((double)this.doorInfo.InsidePosX + 0.5D, (double)this.doorInfo.InsidePosY, (double)this.doorInfo.InsidePosZ + 0.5D, 1.0D);
			}
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.insidePosX = this.doorInfo.InsidePosX;
			this.insidePosZ = this.doorInfo.InsidePosZ;
			this.doorInfo = null;
		}
	}

}