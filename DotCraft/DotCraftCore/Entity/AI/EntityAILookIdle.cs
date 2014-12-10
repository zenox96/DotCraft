using System;

namespace DotCraftCore.Entity.AI
{

	using EntityLiving = DotCraftCore.Entity.EntityLiving;

	public class EntityAILookIdle : EntityAIBase
	{
	/// <summary> The entity that is looking idle.  </summary>
		private EntityLiving idleEntity;

	/// <summary> X offset to look at  </summary>
		private double lookX;

	/// <summary> Z offset to look at  </summary>
		private double lookZ;

///    
///     <summary> * A decrementing tick that stops the entity from being idle once it reaches 0. </summary>
///     
		private int idleTime;
		private const string __OBFID = "CL_00001607";

		public EntityAILookIdle(EntityLiving p_i1647_1_)
		{
			this.idleEntity = p_i1647_1_;
			this.MutexBits = 3;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			return this.idleEntity.RNG.nextFloat() < 0.02F;
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return this.idleTime >= 0;
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			double var1 = (Math.PI * 2D) * this.idleEntity.RNG.NextDouble();
			this.lookX = Math.Cos(var1);
			this.lookZ = Math.Sin(var1);
			this.idleTime = 20 + this.idleEntity.RNG.Next(20);
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			--this.idleTime;
			this.idleEntity.LookHelper.setLookPosition(this.idleEntity.posX + this.lookX, this.idleEntity.posY + (double)this.idleEntity.EyeHeight, this.idleEntity.posZ + this.lookZ, 10.0F, (float)this.idleEntity.VerticalFaceSpeed);
		}
	}

}