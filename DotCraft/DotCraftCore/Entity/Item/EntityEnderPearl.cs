namespace DotCraftCore.Entity.Item
{

	using Entity = DotCraftCore.Entity.Entity;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EntityPlayerMP = DotCraftCore.Entity.Player.EntityPlayerMP;
	using EntityThrowable = DotCraftCore.Entity.Projectile.EntityThrowable;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using MovingObjectPosition = DotCraftCore.Util.MovingObjectPosition;
	using World = DotCraftCore.World.World;

	public class EntityEnderPearl : EntityThrowable
	{
		private const string __OBFID = "CL_00001725";

		public EntityEnderPearl(World p_i1782_1_) : base(p_i1782_1_)
		{
		}

		public EntityEnderPearl(World p_i1783_1_, EntityLivingBase p_i1783_2_) : base(p_i1783_1_, p_i1783_2_)
		{
		}

		public EntityEnderPearl(World p_i1784_1_, double p_i1784_2_, double p_i1784_4_, double p_i1784_6_) : base(p_i1784_1_, p_i1784_2_, p_i1784_4_, p_i1784_6_)
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

			for (int var2 = 0; var2 < 32; ++var2)
			{
				this.worldObj.spawnParticle("portal", this.posX, this.posY + this.rand.NextDouble() * 2.0D, this.posZ, this.rand.nextGaussian(), 0.0D, this.rand.nextGaussian());
			}

			if (!this.worldObj.isClient)
			{
				if (this.Thrower != null && this.Thrower is EntityPlayerMP)
				{
					EntityPlayerMP var3 = (EntityPlayerMP)this.Thrower;

					if (var3.playerNetServerHandler.func_147362_b().ChannelOpen && var3.worldObj == this.worldObj)
					{
						if (this.Thrower.Riding)
						{
							this.Thrower.mountEntity((Entity)null);
						}

						this.Thrower.setPositionAndUpdate(this.posX, this.posY, this.posZ);
						this.Thrower.fallDistance = 0.0F;
						this.Thrower.attackEntityFrom(DamageSource.fall, 5.0F);
					}
				}

				this.setDead();
			}
		}
	}

}