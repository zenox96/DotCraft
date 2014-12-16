using System;

namespace DotCraftCore.Entity.Projectile
{

	using Block = DotCraftCore.block.Block;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using Blocks = DotCraftCore.Init.Blocks;
	using Potion = DotCraftCore.Potion.Potion;
	using PotionEffect = DotCraftCore.Potion.PotionEffect;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using MovingObjectPosition = DotCraftCore.Util.MovingObjectPosition;
	using EnumDifficulty = DotCraftCore.World.EnumDifficulty;
	using Explosion = DotCraftCore.World.Explosion;
	using World = DotCraftCore.World.World;

	public class EntityWitherSkull : EntityFireball
	{
		

		public EntityWitherSkull(World p_i1793_1_) : base(p_i1793_1_)
		{
			this.setSize(0.3125F, 0.3125F);
		}

		public EntityWitherSkull(World p_i1794_1_, EntityLivingBase p_i1794_2_, double p_i1794_3_, double p_i1794_5_, double p_i1794_7_) : base(p_i1794_1_, p_i1794_2_, p_i1794_3_, p_i1794_5_, p_i1794_7_)
		{
			this.setSize(0.3125F, 0.3125F);
		}

///    
///     <summary> * Return the motion factor for this projectile. The factor is multiplied by the original motion. </summary>
///     
		protected internal override float MotionFactor
		{
			get
			{
				return this.Invulnerable ? 0.73F : base.MotionFactor;
			}
		}

		public EntityWitherSkull(World p_i1795_1_, double p_i1795_2_, double p_i1795_4_, double p_i1795_6_, double p_i1795_8_, double p_i1795_10_, double p_i1795_12_) : base(p_i1795_1_, p_i1795_2_, p_i1795_4_, p_i1795_6_, p_i1795_8_, p_i1795_10_, p_i1795_12_)
		{
			this.setSize(0.3125F, 0.3125F);
		}

///    
///     <summary> * Returns true if the entity is on fire. Used by render to add the fire effect on rendering. </summary>
///     
		public override bool isBurning()
		{
			get
			{
				return false;
			}
		}

		public override float func_145772_a(Explosion p_145772_1_, World p_145772_2_, int p_145772_3_, int p_145772_4_, int p_145772_5_, Block p_145772_6_)
		{
			float var7 = base.func_145772_a(p_145772_1_, p_145772_2_, p_145772_3_, p_145772_4_, p_145772_5_, p_145772_6_);

			if (this.Invulnerable && p_145772_6_ != Blocks.bedrock && p_145772_6_ != Blocks.end_portal && p_145772_6_ != Blocks.end_portal_frame && p_145772_6_ != Blocks.command_block)
			{
				var7 = Math.Min(0.8F, var7);
			}

			return var7;
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
					if (this.shootingEntity != null)
					{
						if (p_70227_1_.entityHit.attackEntityFrom(DamageSource.causeMobDamage(this.shootingEntity), 8.0F) && !p_70227_1_.entityHit.EntityAlive)
						{
							this.shootingEntity.heal(5.0F);
						}
					}
					else
					{
						p_70227_1_.entityHit.attackEntityFrom(DamageSource.magic, 5.0F);
					}

					if (p_70227_1_.entityHit is EntityLivingBase)
					{
						sbyte var2 = 0;

						if (this.worldObj.difficultySetting == EnumDifficulty.NORMAL)
						{
							var2 = 10;
						}
						else if (this.worldObj.difficultySetting == EnumDifficulty.HARD)
						{
							var2 = 40;
						}

						if (var2 > 0)
						{
							((EntityLivingBase)p_70227_1_.entityHit).addPotionEffect(new PotionEffect(Potion.wither.id, 20 * var2, 1));
						}
					}
				}

				this.worldObj.newExplosion(this, this.posX, this.posY, this.posZ, 1.0F, false, this.worldObj.GameRules.getGameRuleBooleanValue("mobGriefing"));
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

		protected internal override void entityInit()
		{
			this.dataWatcher.addObject(10, Convert.ToByte((sbyte)0));
		}

///    
///     <summary> * Return whether this skull comes from an invulnerable (aura) wither boss. </summary>
///     
		public virtual bool isInvulnerable()
		{
			get
			{
				return this.dataWatcher.getWatchableObjectByte(10) == 1;
			}
			set
			{
				this.dataWatcher.updateObject(10, Convert.ToByte((sbyte)(value ? 1 : 0)));
			}
		}

///    
///     <summary> * Set whether this skull comes from an invulnerable (aura) wither boss. </summary>
///     
	}

}