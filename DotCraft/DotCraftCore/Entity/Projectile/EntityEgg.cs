namespace DotCraftCore.nEntity.nProjectile
{

	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using EntityChicken = DotCraftCore.nEntity.nPassive.EntityChicken;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using MovingObjectPosition = DotCraftCore.nUtil.MovingObjectPosition;
	using World = DotCraftCore.nWorld.World;

	public class EntityEgg : EntityThrowable
	{
		

		public EntityEgg(World p_i1779_1_) : base(p_i1779_1_)
		{
		}

		public EntityEgg(World p_i1780_1_, EntityLivingBase p_i1780_2_) : base(p_i1780_1_, p_i1780_2_)
		{
		}

		public EntityEgg(World p_i1781_1_, double p_i1781_2_, double p_i1781_4_, double p_i1781_6_) : base(p_i1781_1_, p_i1781_2_, p_i1781_4_, p_i1781_6_)
		{
		}

///    
///     <summary> * Called when this EntityThrowable hits a block or entity. </summary>
///     
		protected internal override void onImpact(MovingObjectPosition p_70184_1_)
		{
			if (p_70184_1_.entityHit != null)
			{
				p_70184_1_.entityHit.attackEntityFrom(DamageSource.causeThrownDamage(this, this.Thrower), 0.0F);
			}

			if (!this.worldObj.isClient && this.rand.Next(8) == 0)
			{
				sbyte var2 = 1;

				if (this.rand.Next(32) == 0)
				{
					var2 = 4;
				}

				for (int var3 = 0; var3 < var2; ++var3)
				{
					EntityChicken var4 = new EntityChicken(this.worldObj);
					var4.GrowingAge = -24000;
					var4.setLocationAndAngles(this.posX, this.posY, this.posZ, this.rotationYaw, 0.0F);
					this.worldObj.spawnEntityInWorld(var4);
				}
			}

			for (int var5 = 0; var5 < 8; ++var5)
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