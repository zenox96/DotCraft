namespace DotCraftCore.Entity.AI
{

	using EntityCreature = DotCraftCore.Entity.EntityCreature;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using Village = DotCraftCore.Village.Village;
	using VillageDoorInfo = DotCraftCore.Village.VillageDoorInfo;

	public class EntityAIRestrictOpenDoor : EntityAIBase
	{
		private EntityCreature entityObj;
		private VillageDoorInfo frontDoor;
		

		public EntityAIRestrictOpenDoor(EntityCreature p_i1651_1_)
		{
			this.entityObj = p_i1651_1_;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (this.entityObj.worldObj.Daytime)
			{
				return false;
			}
			else
			{
				Village var1 = this.entityObj.worldObj.villageCollectionObj.findNearestVillage(MathHelper.floor_double(this.entityObj.posX), MathHelper.floor_double(this.entityObj.posY), MathHelper.floor_double(this.entityObj.posZ), 16);

				if (var1 == null)
				{
					return false;
				}
				else
				{
					this.frontDoor = var1.findNearestDoor(MathHelper.floor_double(this.entityObj.posX), MathHelper.floor_double(this.entityObj.posY), MathHelper.floor_double(this.entityObj.posZ));
					return this.frontDoor == null ? false : (double)this.frontDoor.getInsideDistanceSquare(MathHelper.floor_double(this.entityObj.posX), MathHelper.floor_double(this.entityObj.posY), MathHelper.floor_double(this.entityObj.posZ)) < 2.25D;
				}
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return this.entityObj.worldObj.Daytime ? false : !this.frontDoor.isDetachedFromVillageFlag && this.frontDoor.isInside(MathHelper.floor_double(this.entityObj.posX), MathHelper.floor_double(this.entityObj.posZ));
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.entityObj.Navigator.BreakDoors = false;
			this.entityObj.Navigator.EnterDoors = false;
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.entityObj.Navigator.BreakDoors = true;
			this.entityObj.Navigator.EnterDoors = true;
			this.frontDoor = null;
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			this.frontDoor.incrementDoorOpeningRestrictionCounter();
		}
	}

}