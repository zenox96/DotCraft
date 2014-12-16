namespace DotCraftCore.Entity.AI
{

	using EntityLiving = DotCraftCore.Entity.EntityLiving;

	public class EntityJumpHelper
	{
		private EntityLiving entity;
		private bool isJumping;
		

		public EntityJumpHelper(EntityLiving p_i1612_1_)
		{
			this.entity = p_i1612_1_;
		}

		public virtual void setJumping()
		{
			this.isJumping = true;
		}

///    
///     <summary> * Called to actually make the entity jump if isJumping is true. </summary>
///     
		public virtual void doJump()
		{
			this.entity.Jumping = this.isJumping;
			this.isJumping = false;
		}
	}

}