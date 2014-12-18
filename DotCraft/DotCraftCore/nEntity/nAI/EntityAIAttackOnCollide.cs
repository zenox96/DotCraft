using System;

namespace DotCraftCore.nEntity.nAI
{

	using EntityCreature = DotCraftCore.nEntity.EntityCreature;
	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using PathEntity = DotCraftCore.nPathfinding.PathEntity;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using World = DotCraftCore.nWorld.World;

	public class EntityAIAttackOnCollide : EntityAIBase
	{
		internal World worldObj;
		internal EntityCreature attacker;

///    
///     <summary> * An amount of decrementing ticks that allows the entity to attack once the tick reaches 0. </summary>
///     
		internal int attackTick;

	/// <summary> The speed with which the mob will approach the target  </summary>
		internal double speedTowardsTarget;

///    
///     <summary> * When true, the mob will continue chasing its target, even if it can't find a path to them right now. </summary>
///     
		internal bool longMemory;

	/// <summary> The PathEntity of our entity.  </summary>
		internal PathEntity entityPathEntity;
		internal Type classTarget;
		private int field_75445_i;
		private double field_151497_i;
		private double field_151495_j;
		private double field_151496_k;
		

		public EntityAIAttackOnCollide(EntityCreature p_i1635_1_, Type p_i1635_2_, double p_i1635_3_, bool p_i1635_5_) : this(p_i1635_1_, p_i1635_3_, p_i1635_5_)
		{
			this.classTarget = p_i1635_2_;
		}

		public EntityAIAttackOnCollide(EntityCreature p_i1636_1_, double p_i1636_2_, bool p_i1636_4_)
		{
			this.attacker = p_i1636_1_;
			this.worldObj = p_i1636_1_.worldObj;
			this.speedTowardsTarget = p_i1636_2_;
			this.longMemory = p_i1636_4_;
			this.MutexBits = 3;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			EntityLivingBase var1 = this.attacker.AttackTarget;

			if (var1 == null)
			{
				return false;
			}
			else if (!var1.EntityAlive)
			{
				return false;
			}
			else if (this.classTarget != null && !this.classTarget.isAssignableFrom(var1.GetType()))
			{
				return false;
			}
			else
			{
				this.entityPathEntity = this.attacker.Navigator.getPathToEntityLiving(var1);
				return this.entityPathEntity != null;
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			EntityLivingBase var1 = this.attacker.AttackTarget;
			return var1 == null ? false : (!var1.EntityAlive ? false : (!this.longMemory ? !this.attacker.Navigator.noPath() : this.attacker.isWithinHomeDistance(MathHelper.floor_double(var1.posX), MathHelper.floor_double(var1.posY), MathHelper.floor_double(var1.posZ))));
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.attacker.Navigator.setPath(this.entityPathEntity, this.speedTowardsTarget);
			this.field_75445_i = 0;
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.attacker.Navigator.clearPathEntity();
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			EntityLivingBase var1 = this.attacker.AttackTarget;
			this.attacker.LookHelper.setLookPositionWithEntity(var1, 30.0F, 30.0F);
			double var2 = this.attacker.getDistanceSq(var1.posX, var1.boundingBox.minY, var1.posZ);
			double var4 = (double)(this.attacker.width * 2.0F * this.attacker.width * 2.0F + var1.width);
			--this.field_75445_i;

			if ((this.longMemory || this.attacker.EntitySenses.canSee(var1)) && this.field_75445_i <= 0 && (this.field_151497_i == 0.0D && this.field_151495_j == 0.0D && this.field_151496_k == 0.0D || var1.getDistanceSq(this.field_151497_i, this.field_151495_j, this.field_151496_k) >= 1.0D || this.attacker.RNG.nextFloat() < 0.05F))
			{
				this.field_151497_i = var1.posX;
				this.field_151495_j = var1.boundingBox.minY;
				this.field_151496_k = var1.posZ;
				this.field_75445_i = 4 + this.attacker.RNG.Next(7);

				if (var2 > 1024.0D)
				{
					this.field_75445_i += 10;
				}
				else if (var2 > 256.0D)
				{
					this.field_75445_i += 5;
				}

				if (!this.attacker.Navigator.tryMoveToEntityLiving(var1, this.speedTowardsTarget))
				{
					this.field_75445_i += 15;
				}
			}

			this.attackTick = Math.Max(this.attackTick - 1, 0);

			if (var2 <= var4 && this.attackTick <= 20)
			{
				this.attackTick = 20;

				if (this.attacker.HeldItem != null)
				{
					this.attacker.swingItem();
				}

				this.attacker.attackEntityAsMob(var1);
			}
		}
	}

}