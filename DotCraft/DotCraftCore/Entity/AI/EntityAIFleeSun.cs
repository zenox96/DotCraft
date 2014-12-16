using System;

namespace DotCraftCore.Entity.AI
{

	using EntityCreature = DotCraftCore.Entity.EntityCreature;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using Vec3 = DotCraftCore.Util.Vec3;
	using World = DotCraftCore.World.World;

	public class EntityAIFleeSun : EntityAIBase
	{
		private EntityCreature theCreature;
		private double shelterX;
		private double shelterY;
		private double shelterZ;
		private double movementSpeed;
		private World theWorld;
		

		public EntityAIFleeSun(EntityCreature p_i1623_1_, double p_i1623_2_)
		{
			this.theCreature = p_i1623_1_;
			this.movementSpeed = p_i1623_2_;
			this.theWorld = p_i1623_1_.worldObj;
			this.MutexBits = 1;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (!this.theWorld.Daytime)
			{
				return false;
			}
			else if (!this.theCreature.Burning)
			{
				return false;
			}
			else if (!this.theWorld.canBlockSeeTheSky(MathHelper.floor_double(this.theCreature.posX), (int)this.theCreature.boundingBox.minY, MathHelper.floor_double(this.theCreature.posZ)))
			{
				return false;
			}
			else
			{
				Vec3 var1 = this.findPossibleShelter();

				if (var1 == null)
				{
					return false;
				}
				else
				{
					this.shelterX = var1.xCoord;
					this.shelterY = var1.yCoord;
					this.shelterZ = var1.zCoord;
					return true;
				}
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return !this.theCreature.Navigator.noPath();
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.theCreature.Navigator.tryMoveToXYZ(this.shelterX, this.shelterY, this.shelterZ, this.movementSpeed);
		}

		private Vec3 findPossibleShelter()
		{
			Random var1 = this.theCreature.RNG;

			for (int var2 = 0; var2 < 10; ++var2)
			{
				int var3 = MathHelper.floor_double(this.theCreature.posX + (double)var1.Next(20) - 10.0D);
				int var4 = MathHelper.floor_double(this.theCreature.boundingBox.minY + (double)var1.Next(6) - 3.0D);
				int var5 = MathHelper.floor_double(this.theCreature.posZ + (double)var1.Next(20) - 10.0D);

				if (!this.theWorld.canBlockSeeTheSky(var3, var4, var5) && this.theCreature.getBlockPathWeight(var3, var4, var5) < 0.0F)
				{
					return Vec3.createVectorHelper((double)var3, (double)var4, (double)var5);
				}
			}

			return null;
		}
	}

}