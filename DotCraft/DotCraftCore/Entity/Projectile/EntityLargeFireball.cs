namespace DotCraftCore.nEntity.nProjectile
{

	using Entity = DotCraftCore.nEntity.Entity;
	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using MovingObjectPosition = DotCraftCore.nUtil.MovingObjectPosition;
	using World = DotCraftCore.nWorld.World;

	public class EntityLargeFireball : EntityFireball
	{
		public int field_92057_e = 1;
		

		public EntityLargeFireball(World p_i1767_1_) : base(p_i1767_1_)
		{
		}

		public EntityLargeFireball(World p_i1768_1_, double p_i1768_2_, double p_i1768_4_, double p_i1768_6_, double p_i1768_8_, double p_i1768_10_, double p_i1768_12_) : base(p_i1768_1_, p_i1768_2_, p_i1768_4_, p_i1768_6_, p_i1768_8_, p_i1768_10_, p_i1768_12_)
		{
		}

		public EntityLargeFireball(World p_i1769_1_, EntityLivingBase p_i1769_2_, double p_i1769_3_, double p_i1769_5_, double p_i1769_7_) : base(p_i1769_1_, p_i1769_2_, p_i1769_3_, p_i1769_5_, p_i1769_7_)
		{
		}

///    
///     <summary> * Called when this EntityFireball hits a block or entity. </summary>
///     
		protected internal override void onImpact(MovingObjectPosition p_70227_1_)
		{
			if (!this.worldObj.isClient)
			{
				if (p_70227_1_.entityHit != null)
				{
					p_70227_1_.entityHit.attackEntityFrom(DamageSource.causeFireballDamage(this, this.shootingEntity), 6.0F);
				}

				this.worldObj.newExplosion((Entity)null, this.posX, this.posY, this.posZ, (float)this.field_92057_e, true, this.worldObj.GameRules.getGameRuleBooleanValue("mobGriefing"));
				this.setDead();
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setInteger("ExplosionPower", this.field_92057_e);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);

			if (p_70037_1_.func_150297_b("ExplosionPower", 99))
			{
				this.field_92057_e = p_70037_1_.getInteger("ExplosionPower");
			}
		}
	}

}