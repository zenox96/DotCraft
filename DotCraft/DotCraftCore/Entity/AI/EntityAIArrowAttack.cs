namespace DotCraftCore.Entity.AI
{

	using EntityLiving = DotCraftCore.Entity.EntityLiving;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using IRangedAttackMob = DotCraftCore.Entity.IRangedAttackMob;
	using MathHelper = DotCraftCore.Util.MathHelper;

	public class EntityAIArrowAttack : EntityAIBase
	{
	/// <summary> The entity the AI instance has been applied to  </summary>
		private readonly EntityLiving entityHost;

///    
///     <summary> * The entity (as a RangedAttackMob) the AI instance has been applied to. </summary>
///     
		private readonly IRangedAttackMob rangedAttackEntityHost;
		private EntityLivingBase attackTarget;

///    
///     <summary> * A decrementing tick that spawns a ranged attack once this value reaches 0. It is then set back to the
///     * maxRangedAttackTime. </summary>
///     
		private int rangedAttackTime;
		private double entityMoveSpeed;
		private int field_75318_f;
		private int field_96561_g;

///    
///     <summary> * The maximum time the AI has to wait before peforming another ranged attack. </summary>
///     
		private int maxRangedAttackTime;
		private float field_96562_i;
		private float field_82642_h;
		private const string __OBFID = "CL_00001609";

		public EntityAIArrowAttack(IRangedAttackMob p_i1649_1_, double p_i1649_2_, int p_i1649_4_, float p_i1649_5_) : this(p_i1649_1_, p_i1649_2_, p_i1649_4_, p_i1649_4_, p_i1649_5_)
		{
		}

		public EntityAIArrowAttack(IRangedAttackMob p_i1650_1_, double p_i1650_2_, int p_i1650_4_, int p_i1650_5_, float p_i1650_6_)
		{
			this.rangedAttackTime = -1;

			if (!(p_i1650_1_ is EntityLivingBase))
			{
				throw new System.ArgumentException("ArrowAttackGoal requires Mob implements RangedAttackMob");
			}
			else
			{
				this.rangedAttackEntityHost = p_i1650_1_;
				this.entityHost = (EntityLiving)p_i1650_1_;
				this.entityMoveSpeed = p_i1650_2_;
				this.field_96561_g = p_i1650_4_;
				this.maxRangedAttackTime = p_i1650_5_;
				this.field_96562_i = p_i1650_6_;
				this.field_82642_h = p_i1650_6_ * p_i1650_6_;
				this.MutexBits = 3;
			}
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			EntityLivingBase var1 = this.entityHost.AttackTarget;

			if (var1 == null)
			{
				return false;
			}
			else
			{
				this.attackTarget = var1;
				return true;
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return this.shouldExecute() || !this.entityHost.Navigator.noPath();
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.attackTarget = null;
			this.field_75318_f = 0;
			this.rangedAttackTime = -1;
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			double var1 = this.entityHost.getDistanceSq(this.attackTarget.posX, this.attackTarget.boundingBox.minY, this.attackTarget.posZ);
			bool var3 = this.entityHost.EntitySenses.canSee(this.attackTarget);

			if (var3)
			{
				++this.field_75318_f;
			}
			else
			{
				this.field_75318_f = 0;
			}

			if (var1 <= (double)this.field_82642_h && this.field_75318_f >= 20)
			{
				this.entityHost.Navigator.clearPathEntity();
			}
			else
			{
				this.entityHost.Navigator.tryMoveToEntityLiving(this.attackTarget, this.entityMoveSpeed);
			}

			this.entityHost.LookHelper.setLookPositionWithEntity(this.attackTarget, 30.0F, 30.0F);
			float var4;

			if (--this.rangedAttackTime == 0)
			{
				if (var1 > (double)this.field_82642_h || !var3)
				{
					return;
				}

				var4 = MathHelper.sqrt_double(var1) / this.field_96562_i;
				float var5 = var4;

				if (var4 < 0.1F)
				{
					var5 = 0.1F;
				}

				if (var5 > 1.0F)
				{
					var5 = 1.0F;
				}

				this.rangedAttackEntityHost.attackEntityWithRangedAttack(this.attackTarget, var5);
				this.rangedAttackTime = MathHelper.floor_float(var4 * (float)(this.maxRangedAttackTime - this.field_96561_g) + (float)this.field_96561_g);
			}
			else if (this.rangedAttackTime < 0)
			{
				var4 = MathHelper.sqrt_double(var1) / this.field_96562_i;
				this.rangedAttackTime = MathHelper.floor_float(var4 * (float)(this.maxRangedAttackTime - this.field_96561_g) + (float)this.field_96561_g);
			}
		}
	}

}