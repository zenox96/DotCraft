using System;

namespace DotCraftCore.Entity.Item
{

	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EntityThrowable = DotCraftCore.Entity.Projectile.EntityThrowable;
	using MovingObjectPosition = DotCraftCore.Util.MovingObjectPosition;
	using World = DotCraftCore.World.World;

	public class EntityExpBottle : EntityThrowable
	{
		

		public EntityExpBottle(World p_i1785_1_) : base(p_i1785_1_)
		{
		}

		public EntityExpBottle(World p_i1786_1_, EntityLivingBase p_i1786_2_) : base(p_i1786_1_, p_i1786_2_)
		{
		}

		public EntityExpBottle(World p_i1787_1_, double p_i1787_2_, double p_i1787_4_, double p_i1787_6_) : base(p_i1787_1_, p_i1787_2_, p_i1787_4_, p_i1787_6_)
		{
		}

///    
///     <summary> * Gets the amount of gravity to apply to the thrown entity with each tick. </summary>
///     
		protected internal override float GravityVelocity
		{
			get
			{
				return 0.07F;
			}
		}

		protected internal override float func_70182_d()
		{
			return 0.7F;
		}

		protected internal override float func_70183_g()
		{
			return -20.0F;
		}

///    
///     <summary> * Called when this EntityThrowable hits a block or entity. </summary>
///     
		protected internal override void onImpact(MovingObjectPosition p_70184_1_)
		{
			if (!this.worldObj.isClient)
			{
				this.worldObj.playAuxSFX(2002, (int)Math.Round(this.posX), (int)Math.Round(this.posY), (int)Math.Round(this.posZ), 0);
				int var2 = 3 + this.worldObj.rand.Next(5) + this.worldObj.rand.Next(5);

				while (var2 > 0)
				{
					int var3 = EntityXPOrb.getXPSplit(var2);
					var2 -= var3;
					this.worldObj.spawnEntityInWorld(new EntityXPOrb(this.worldObj, this.posX, this.posY, this.posZ, var3));
				}

				this.setDead();
			}
		}
	}

}