namespace DotCraftCore.nEntity.nAI
{

	using EntityLiving = DotCraftCore.nEntity.EntityLiving;
	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using MathHelper = DotCraftCore.nUtil.MathHelper;

	public class EntityAILeapAtTarget : EntityAIBase
	{
	/// <summary> The entity that is leaping.  </summary>
		internal EntityLiving leaper;

	/// <summary> The entity that the leaper is leaping towards.  </summary>
		internal EntityLivingBase leapTarget;

	/// <summary> The entity's motionY after leaping.  </summary>
		internal float leapMotionY;
		

		public EntityAILeapAtTarget(EntityLiving p_i1630_1_, float p_i1630_2_)
		{
			this.leaper = p_i1630_1_;
			this.leapMotionY = p_i1630_2_;
			this.MutexBits = 5;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			this.leapTarget = this.leaper.AttackTarget;

			if (this.leapTarget == null)
			{
				return false;
			}
			else
			{
				double var1 = this.leaper.getDistanceSqToEntity(this.leapTarget);
				return var1 >= 4.0D && var1 <= 16.0D ? (!this.leaper.onGround ? false : this.leaper.RNG.Next(5) == 0) : false;
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return !this.leaper.onGround;
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			double var1 = this.leapTarget.posX - this.leaper.posX;
			double var3 = this.leapTarget.posZ - this.leaper.posZ;
			float var5 = MathHelper.sqrt_double(var1 * var1 + var3 * var3);
			this.leaper.motionX += var1 / (double)var5 * 0.5D * 0.800000011920929D + this.leaper.motionX * 0.20000000298023224D;
			this.leaper.motionZ += var3 / (double)var5 * 0.5D * 0.800000011920929D + this.leaper.motionZ * 0.20000000298023224D;
			this.leaper.motionY = (double)this.leapMotionY;
		}
	}

}