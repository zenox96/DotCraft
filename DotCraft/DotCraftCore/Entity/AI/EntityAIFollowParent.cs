using System.Collections;

namespace DotCraftCore.Entity.AI
{

	using EntityAnimal = DotCraftCore.Entity.Passive.EntityAnimal;

	public class EntityAIFollowParent : EntityAIBase
	{
	/// <summary> The child that is following its parent.  </summary>
		internal EntityAnimal childAnimal;
		internal EntityAnimal parentAnimal;
		internal double field_75347_c;
		private int field_75345_d;
		private const string __OBFID = "CL_00001586";

		public EntityAIFollowParent(EntityAnimal p_i1626_1_, double p_i1626_2_)
		{
			this.childAnimal = p_i1626_1_;
			this.field_75347_c = p_i1626_2_;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (this.childAnimal.GrowingAge >= 0)
			{
				return false;
			}
			else
			{
				IList var1 = this.childAnimal.worldObj.getEntitiesWithinAABB(this.childAnimal.GetType(), this.childAnimal.boundingBox.expand(8.0D, 4.0D, 8.0D));
				EntityAnimal var2 = null;
				double var3 = double.MaxValue;
				IEnumerator var5 = var1.GetEnumerator();

				while (var5.MoveNext())
				{
					EntityAnimal var6 = (EntityAnimal)var5.Current;

					if (var6.GrowingAge >= 0)
					{
						double var7 = this.childAnimal.getDistanceSqToEntity(var6);

						if (var7 <= var3)
						{
							var3 = var7;
							var2 = var6;
						}
					}
				}

				if (var2 == null)
				{
					return false;
				}
				else if (var3 < 9.0D)
				{
					return false;
				}
				else
				{
					this.parentAnimal = var2;
					return true;
				}
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			if (!this.parentAnimal.EntityAlive)
			{
				return false;
			}
			else
			{
				double var1 = this.childAnimal.getDistanceSqToEntity(this.parentAnimal);
				return var1 >= 9.0D && var1 <= 256.0D;
			}
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.field_75345_d = 0;
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.parentAnimal = null;
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			if (--this.field_75345_d <= 0)
			{
				this.field_75345_d = 10;
				this.childAnimal.Navigator.tryMoveToEntityLiving(this.parentAnimal, this.field_75347_c);
			}
		}
	}

}