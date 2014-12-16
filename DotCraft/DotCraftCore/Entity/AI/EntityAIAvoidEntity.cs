using System;
using System.Collections;

namespace DotCraftCore.Entity.AI
{

	using IEntitySelector = DotCraftCore.command.IEntitySelector;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityCreature = DotCraftCore.Entity.EntityCreature;
	using EntityTameable = DotCraftCore.Entity.Passive.EntityTameable;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using PathEntity = DotCraftCore.Pathfinding.PathEntity;
	using PathNavigate = DotCraftCore.Pathfinding.PathNavigate;
	using Vec3 = DotCraftCore.Util.Vec3;

	public class EntityAIAvoidEntity : EntityAIBase
	{
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		public final IEntitySelector field_98218_a = new IEntitySelector()
//	{
//		
//		public boolean isEntityApplicable(Entity p_82704_1_)
//		{
//			return p_82704_1_.isEntityAlive() && EntityAIAvoidEntity.theEntity.getEntitySenses().canSee(p_82704_1_);
//		}
//	};

	/// <summary> The entity we are attached to  </summary>
		private EntityCreature theEntity;
		private double farSpeed;
		private double nearSpeed;
		private Entity closestLivingEntity;
		private float distanceFromEntity;

	/// <summary> The PathEntity of our entity  </summary>
		private PathEntity entityPathEntity;

	/// <summary> The PathNavigate of our entity  </summary>
		private PathNavigate entityPathNavigate;

	/// <summary> The class of the entity we should avoid  </summary>
		private Type targetEntityClass;
		

		public EntityAIAvoidEntity(EntityCreature p_i1616_1_, Type p_i1616_2_, float p_i1616_3_, double p_i1616_4_, double p_i1616_6_)
		{
			this.theEntity = p_i1616_1_;
			this.targetEntityClass = p_i1616_2_;
			this.distanceFromEntity = p_i1616_3_;
			this.farSpeed = p_i1616_4_;
			this.nearSpeed = p_i1616_6_;
			this.entityPathNavigate = p_i1616_1_.Navigator;
			this.MutexBits = 1;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (this.targetEntityClass == typeof(EntityPlayer))
			{
				if (this.theEntity is EntityTameable && ((EntityTameable)this.theEntity).Tamed)
				{
					return false;
				}

				this.closestLivingEntity = this.theEntity.worldObj.getClosestPlayerToEntity(this.theEntity, (double)this.distanceFromEntity);

				if (this.closestLivingEntity == null)
				{
					return false;
				}
			}
			else
			{
				IList var1 = this.theEntity.worldObj.selectEntitiesWithinAABB(this.targetEntityClass, this.theEntity.boundingBox.expand((double)this.distanceFromEntity, 3.0D, (double)this.distanceFromEntity), this.field_98218_a);

				if (var1.Count == 0)
				{
					return false;
				}

				this.closestLivingEntity = (Entity)var1[0];
			}

			Vec3 var2 = RandomPositionGenerator.findRandomTargetBlockAwayFrom(this.theEntity, 16, 7, Vec3.createVectorHelper(this.closestLivingEntity.posX, this.closestLivingEntity.posY, this.closestLivingEntity.posZ));

			if (var2 == null)
			{
				return false;
			}
			else if (this.closestLivingEntity.getDistanceSq(var2.xCoord, var2.yCoord, var2.zCoord) < this.closestLivingEntity.getDistanceSqToEntity(this.theEntity))
			{
				return false;
			}
			else
			{
				this.entityPathEntity = this.entityPathNavigate.getPathToXYZ(var2.xCoord, var2.yCoord, var2.zCoord);
				return this.entityPathEntity == null ? false : this.entityPathEntity.isDestinationSame(var2);
			}
		}

///    
///     <summary> * Returns whether an in-progress EntityAIBase should continue executing </summary>
///     
		public override bool continueExecuting()
		{
			return !this.entityPathNavigate.noPath();
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.entityPathNavigate.setPath(this.entityPathEntity, this.farSpeed);
		}

///    
///     <summary> * Resets the task </summary>
///     
		public override void resetTask()
		{
			this.closestLivingEntity = null;
		}

///    
///     <summary> * Updates the task </summary>
///     
		public override void updateTask()
		{
			if (this.theEntity.getDistanceSqToEntity(this.closestLivingEntity) < 49.0D)
			{
				this.theEntity.Navigator.Speed = this.nearSpeed;
			}
			else
			{
				this.theEntity.Navigator.Speed = this.farSpeed;
			}
		}
	}

}