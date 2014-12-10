using System.Collections;

namespace DotCraftCore.Entity.AI
{

	using EntityCreature = DotCraftCore.Entity.EntityCreature;
	using PathEntity = DotCraftCore.Pathfinding.PathEntity;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using Vec3 = DotCraftCore.Util.Vec3;
	using Village = DotCraftCore.Village.Village;
	using VillageDoorInfo = DotCraftCore.Village.VillageDoorInfo;

	public class EntityAIMoveThroughVillage : EntityAIBase
	{
		private EntityCreature theEntity;
		private double movementSpeed;

	/// <summary> The PathNavigate of our entity.  </summary>
		private PathEntity entityPathNavigate;
		private VillageDoorInfo doorInfo;
		private bool isNocturnal;
		private IList doorList = new ArrayList();
		private const string __OBFID = "CL_00001597";

		public EntityAIMoveThroughVillage(EntityCreature p_i1638_1_, double p_i1638_2_, bool p_i1638_4_)
		{
			this.theEntity = p_i1638_1_;
			this.movementSpeed = p_i1638_2_;
			this.isNocturnal = p_i1638_4_;
			this.MutexBits = 1;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			this.func_75414_f();

			if (this.isNocturnal && this.theEntity.worldObj.Daytime)
			{
				return false;
			}
			else
			{
				Village var1 = this.theEntity.worldObj.villageCollectionObj.findNearestVillage(MathHelper.floor_double(this.theEntity.posX), MathHelper.floor_double(this.theEntity.posY), MathHelper.floor_double(this.theEntity.posZ), 0);

				if (var1 == null)
				{
					return false;
				}
				else
				{
					this.doorInfo = this.func_75412_a(var1);

					if (this.doorInfo == null)
					{
						return false;
					}
					else
					{
						bool var2 = this.theEntity.Navigator.CanBreakDoors;
						this.theEntity.Navigator.BreakDoors = false;
						this.entityPathNavigate = this.theEntity.Navigator.getPathToXYZ((double)this.doorInfo.posX, (double)this.doorInfo.posY, (double)this.doorInfo.posZ);
						this.theEntity.Navigator.BreakDoors = var2;

						if (this.entityPathNavigate != null)
						{
							return true;
						}
						else
						{
							Vec3 var3 = RandomPositionGenerator.findRandomTargetBlockTowards(this.theEntity, 10, 7, Vec3.createVectorHelper((double)this.doorInfo.posX, (double)this.doorInfo.posY, (double)this.doorInfo.posZ));

							if (var3 == null)
							{
								return false;
							}
							else
							{
								this.theEntity.Navigator.BreakDoors = false;
								this.entityPathNavigate = this.theEntity.Navigator.getPathToXYZ(var3.xCoord, var3.yCoord, var3.zCoord);
								this.theEntity.Navigator.BreakDoors = var2;
								return this.entityPathNavigate != null;
							}
						}
					}
				}
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			if (this.theEntity.Navigator.noPath())
			{
				return false;
			}
			else
			{
				float var1 = this.theEntity.width + 4.0F;
				return this.theEntity.getDistanceSq((double)this.doorInfo.posX, (double)this.doorInfo.posY, (double)this.doorInfo.posZ) > (double)(var1 * var1);
			}
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.theEntity.Navigator.setPath(this.entityPathNavigate, this.movementSpeed);
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			if (this.theEntity.Navigator.noPath() || this.theEntity.getDistanceSq((double)this.doorInfo.posX, (double)this.doorInfo.posY, (double)this.doorInfo.posZ) < 16.0D)
			{
				this.doorList.Add(this.doorInfo);
			}
		}

		private VillageDoorInfo func_75412_a(Village p_75412_1_)
		{
			VillageDoorInfo var2 = null;
			int var3 = int.MaxValue;
			IList var4 = p_75412_1_.VillageDoorInfoList;
			IEnumerator var5 = var4.GetEnumerator();

			while (var5.MoveNext())
			{
				VillageDoorInfo var6 = (VillageDoorInfo)var5.Current;
				int var7 = var6.getDistanceSquared(MathHelper.floor_double(this.theEntity.posX), MathHelper.floor_double(this.theEntity.posY), MathHelper.floor_double(this.theEntity.posZ));

				if (var7 < var3 && !this.func_75413_a(var6))
				{
					var2 = var6;
					var3 = var7;
				}
			}

			return var2;
		}

		private bool func_75413_a(VillageDoorInfo p_75413_1_)
		{
			IEnumerator var2 = this.doorList.GetEnumerator();
			VillageDoorInfo var3;

			do
			{
				if (!var2.MoveNext())
				{
					return false;
				}

				var3 = (VillageDoorInfo)var2.Current;
			}
			while (p_75413_1_.posX != var3.posX || p_75413_1_.posY != var3.posY || p_75413_1_.posZ != var3.posZ);

			return true;
		}

		private void func_75414_f()
		{
			if (this.doorList.Count > 15)
			{
				this.doorList.Remove(0);
			}
		}
	}

}