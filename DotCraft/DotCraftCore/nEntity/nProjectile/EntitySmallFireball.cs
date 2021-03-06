namespace DotCraftCore.nEntity.nProjectile
{

	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using Blocks = DotCraftCore.nInit.Blocks;
	using DamageSource = DotCraftCore.nUtil.DamageSource;
	using MovingObjectPosition = DotCraftCore.nUtil.MovingObjectPosition;
	using World = DotCraftCore.nWorld.World;

	public class EntitySmallFireball : EntityFireball
	{
		

		public EntitySmallFireball(World p_i1770_1_) : base(p_i1770_1_)
		{
			this.setSize(0.3125F, 0.3125F);
		}

		public EntitySmallFireball(World p_i1771_1_, EntityLivingBase p_i1771_2_, double p_i1771_3_, double p_i1771_5_, double p_i1771_7_) : base(p_i1771_1_, p_i1771_2_, p_i1771_3_, p_i1771_5_, p_i1771_7_)
		{
			this.setSize(0.3125F, 0.3125F);
		}

		public EntitySmallFireball(World p_i1772_1_, double p_i1772_2_, double p_i1772_4_, double p_i1772_6_, double p_i1772_8_, double p_i1772_10_, double p_i1772_12_) : base(p_i1772_1_, p_i1772_2_, p_i1772_4_, p_i1772_6_, p_i1772_8_, p_i1772_10_, p_i1772_12_)
		{
			this.setSize(0.3125F, 0.3125F);
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
					if (!p_70227_1_.entityHit.ImmuneToFire && p_70227_1_.entityHit.attackEntityFrom(DamageSource.causeFireballDamage(this, this.shootingEntity), 5.0F))
					{
						p_70227_1_.entityHit.Fire = 5;
					}
				}
				else
				{
					int var2 = p_70227_1_.blockX;
					int var3 = p_70227_1_.blockY;
					int var4 = p_70227_1_.blockZ;

					switch (p_70227_1_.sideHit)
					{
						case 0:
							--var3;
							break;

						case 1:
							++var3;
							break;

						case 2:
							--var4;
							break;

						case 3:
							++var4;
							break;

						case 4:
							--var2;
							break;

						case 5:
							++var2;
						break;
					}

					if (this.worldObj.isAirBlock(var2, var3, var4))
					{
						this.worldObj.setBlock(var2, var3, var4, Blocks.fire);
					}
				}

				this.setDead();
			}
		}

///    
///     <summary> * Returns true if other Entities should be prevented from moving through this Entity. </summary>
///     
		public override bool canBeCollidedWith()
		{
			return false;
		}

///    
///     <summary> * Called when the entity is attacked. </summary>
///     
		public override bool attackEntityFrom(DamageSource p_70097_1_, float p_70097_2_)
		{
			return false;
		}
	}

}