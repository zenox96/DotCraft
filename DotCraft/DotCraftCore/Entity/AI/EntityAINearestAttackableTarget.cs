using System;
using System.Collections;

namespace DotCraftCore.Entity.AI
{

	using IEntitySelector = DotCraftCore.command.IEntitySelector;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityCreature = DotCraftCore.Entity.EntityCreature;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;

	public class EntityAINearestAttackableTarget : EntityAITarget
	{
		private readonly Type targetClass;
		private readonly int targetChance;

	/// <summary> Instance of EntityAINearestAttackableTargetSorter.  </summary>
		private readonly EntityAINearestAttackableTarget.Sorter theNearestAttackableTargetSorter;

///    
///     <summary> * This filter is applied to the Entity search.  Only matching entities will be targetted.  (null -> no
///     * restrictions) </summary>
///     
		private readonly IEntitySelector targetEntitySelector;
		private EntityLivingBase targetEntity;
		private const string __OBFID = "CL_00001620";

		public EntityAINearestAttackableTarget(EntityCreature p_i1663_1_, Type p_i1663_2_, int p_i1663_3_, bool p_i1663_4_) : this(p_i1663_1_, p_i1663_2_, p_i1663_3_, p_i1663_4_, false)
		{
		}

		public EntityAINearestAttackableTarget(EntityCreature p_i1664_1_, Type p_i1664_2_, int p_i1664_3_, bool p_i1664_4_, bool p_i1664_5_) : this(p_i1664_1_, p_i1664_2_, p_i1664_3_, p_i1664_4_, p_i1664_5_, (IEntitySelector)null)
		{
		}

//JAVA TO VB & C# CONVERTER WARNING: 'final' parameters are not allowed in .NET:
//ORIGINAL LINE: public EntityAINearestAttackableTarget(EntityCreature p_i1665_1_, Class p_i1665_2_, int p_i1665_3_, boolean p_i1665_4_, boolean p_i1665_5_, final IEntitySelector p_i1665_6_)
		public EntityAINearestAttackableTarget(EntityCreature p_i1665_1_, Type p_i1665_2_, int p_i1665_3_, bool p_i1665_4_, bool p_i1665_5_, IEntitySelector p_i1665_6_) : base(p_i1665_1_, p_i1665_4_, p_i1665_5_)
		{
			this.targetClass = p_i1665_2_;
			this.targetChance = p_i1665_3_;
			this.theNearestAttackableTargetSorter = new EntityAINearestAttackableTarget.Sorter(p_i1665_1_);
			this.MutexBits = 1;
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//			this.targetEntitySelector = new IEntitySelector()
//		{
//			private static final String __OBFID = "CL_00001621";
//			public boolean isEntityApplicable(Entity p_82704_1_)
//			{
//				return !(p_82704_1_ instanceof EntityLivingBase) ? false : (p_i1665_6_ != null && !p_i1665_6_.isEntityApplicable(p_82704_1_) ? false : EntityAINearestAttackableTarget.isSuitableTarget((EntityLivingBase)p_82704_1_, false));
//			}
//		};
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			if (this.targetChance > 0 && this.taskOwner.RNG.Next(this.targetChance) != 0)
			{
				return false;
			}
			else
			{
				double var1 = this.TargetDistance;
				IList var3 = this.taskOwner.worldObj.selectEntitiesWithinAABB(this.targetClass, this.taskOwner.boundingBox.expand(var1, 4.0D, var1), this.targetEntitySelector);
				Collections.sort(var3, this.theNearestAttackableTargetSorter);

				if (var3.Count == 0)
				{
					return false;
				}
				else
				{
					this.targetEntity = (EntityLivingBase)var3[0];
					return true;
				}
			}
		}

///    
///     <summary> * Execute a one shot task or start executing a continuous task </summary>
///     
		public override void startExecuting()
		{
			this.taskOwner.AttackTarget = this.targetEntity;
			base.startExecuting();
		}

		public class Sorter : IComparer
		{
			private readonly Entity theEntity;
			private const string __OBFID = "CL_00001622";

			public Sorter(Entity p_i1662_1_)
			{
				this.theEntity = p_i1662_1_;
			}

			public virtual int compare(Entity p_compare_1_, Entity p_compare_2_)
			{
				double var3 = this.theEntity.getDistanceSqToEntity(p_compare_1_);
				double var5 = this.theEntity.getDistanceSqToEntity(p_compare_2_);
				return var3 < var5 ? -1 : (var3 > var5 ? 1 : 0);
			}

			public virtual int compare(object p_compare_1_, object p_compare_2_)
			{
				return this.compare((Entity)p_compare_1_, (Entity)p_compare_2_);
			}
		}
	}

}