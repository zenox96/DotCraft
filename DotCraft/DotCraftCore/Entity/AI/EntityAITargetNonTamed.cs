using System;

namespace DotCraftCore.Entity.AI
{

	using EntityTameable = DotCraftCore.Entity.Passive.EntityTameable;

	public class EntityAITargetNonTamed : EntityAINearestAttackableTarget
	{
		private EntityTameable theTameable;
		

		public EntityAITargetNonTamed(EntityTameable p_i1666_1_, Type p_i1666_2_, int p_i1666_3_, bool p_i1666_4_) : base(p_i1666_1_, p_i1666_2_, p_i1666_3_, p_i1666_4_)
		{
			this.theTameable = p_i1666_1_;
		}

///    
///     <summary> * Returns whether the EntityAIBase should begin execution. </summary>
///     
		public override bool shouldExecute()
		{
			return !this.theTameable.Tamed && base.shouldExecute();
		}
	}

}