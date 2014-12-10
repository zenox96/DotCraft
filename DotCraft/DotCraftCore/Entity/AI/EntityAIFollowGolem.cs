using System.Collections;

namespace DotCraftCore.Entity.AI
{

	using EntityIronGolem = DotCraftCore.Entity.Monster.EntityIronGolem;
	using EntityVillager = DotCraftCore.Entity.Passive.EntityVillager;

	public class EntityAIFollowGolem : EntityAIBase
	{
		private EntityVillager theVillager;
		private EntityIronGolem theGolem;
		private int takeGolemRoseTick;
		private bool tookGolemRose;
		private const string __OBFID = "CL_00001615";

		public EntityAIFollowGolem(EntityVillager p_i1656_1_)
		{
			this.theVillager = p_i1656_1_;
			this.MutexBits = 3;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (this.theVillager.GrowingAge >= 0)
			{
				return false;
			}
			else if (!this.theVillager.worldObj.Daytime)
			{
				return false;
			}
			else
			{
				IList var1 = this.theVillager.worldObj.getEntitiesWithinAABB(typeof(EntityIronGolem), this.theVillager.boundingBox.expand(6.0D, 2.0D, 6.0D));

				if (var1.Count == 0)
				{
					return false;
				}
				else
				{
					IEnumerator var2 = var1.GetEnumerator();

					while (var2.MoveNext())
					{
						EntityIronGolem var3 = (EntityIronGolem)var2.Current;

						if (var3.HoldRoseTick > 0)
						{
							this.theGolem = var3;
							break;
						}
					}

					return this.theGolem != null;
				}
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return this.theGolem.HoldRoseTick > 0;
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.takeGolemRoseTick = this.theVillager.RNG.Next(320);
			this.tookGolemRose = false;
			this.theGolem.Navigator.clearPathEntity();
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.theGolem = null;
			this.theVillager.Navigator.clearPathEntity();
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			this.theVillager.LookHelper.setLookPositionWithEntity(this.theGolem, 30.0F, 30.0F);

			if (this.theGolem.HoldRoseTick == this.takeGolemRoseTick)
			{
				this.theVillager.Navigator.tryMoveToEntityLiving(this.theGolem, 0.5D);
				this.tookGolemRose = true;
			}

			if (this.tookGolemRose && this.theVillager.getDistanceSqToEntity(this.theGolem) < 4.0D)
			{
				this.theGolem.HoldingRose = false;
				this.theVillager.Navigator.clearPathEntity();
			}
		}
	}

}