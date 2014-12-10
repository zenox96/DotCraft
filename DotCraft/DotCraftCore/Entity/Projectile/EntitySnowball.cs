namespace DotCraftCore.Entity.Projectile
{

	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EntityBlaze = DotCraftCore.Entity.Monster.EntityBlaze;
	using DamageSource = DotCraftCore.util.DamageSource;
	using MovingObjectPosition = DotCraftCore.util.MovingObjectPosition;
	using World = DotCraftCore.world.World;

	public class EntitySnowball : EntityThrowable
	{
		private const string __OBFID = "CL_00001722";

		public EntitySnowball(World p_i1773_1_) : base(p_i1773_1_)
		{
		}

		public EntitySnowball(World p_i1774_1_, EntityLivingBase p_i1774_2_) : base(p_i1774_1_, p_i1774_2_)
		{
		}

		public EntitySnowball(World p_i1775_1_, double p_i1775_2_, double p_i1775_4_, double p_i1775_6_) : base(p_i1775_1_, p_i1775_2_, p_i1775_4_, p_i1775_6_)
		{
		}

///    
///     <summary> * Called when this EntityThrowable hits a block or entity. </summary>
///     
		protected internal override void onImpact(MovingObjectPosition p_70184_1_)
		{
			if (p_70184_1_.entityHit != null)
			{
				sbyte var2 = 0;

				if (p_70184_1_.entityHit is EntityBlaze)
				{
					var2 = 3;
				}

				p_70184_1_.entityHit.attackEntityFrom(DamageSource.causeThrownDamage(this, this.Thrower), (float)var2);
			}

			for (int var3 = 0; var3 < 8; ++var3)
			{
				this.worldObj.spawnParticle("snowballpoof", this.posX, this.posY, this.posZ, 0.0D, 0.0D, 0.0D);
			}

			if (!this.worldObj.isClient)
			{
				this.setDead();
			}
		}
	}

}