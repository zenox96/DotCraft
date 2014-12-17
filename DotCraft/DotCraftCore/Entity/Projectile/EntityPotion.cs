using System;
using System.Collections;

namespace DotCraftCore.nEntity.nProjectile
{

	using EntityLivingBase = DotCraftCore.nEntity.EntityLivingBase;
	using Items = DotCraftCore.nInit.Items;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using Potion = DotCraftCore.nPotion.Potion;
	using PotionEffect = DotCraftCore.nPotion.PotionEffect;
	using AxisAlignedBB = DotCraftCore.nUtil.AxisAlignedBB;
	using MovingObjectPosition = DotCraftCore.nUtil.MovingObjectPosition;
	using World = DotCraftCore.nWorld.World;

	public class EntityPotion : EntityThrowable
	{
///    
///     <summary> * The damage value of the thrown potion that this EntityPotion represents. </summary>
///     
		private ItemStack potionDamage;
		

		public EntityPotion(World p_i1788_1_) : base(p_i1788_1_)
		{
		}

		public EntityPotion(World p_i1789_1_, EntityLivingBase p_i1789_2_, int p_i1789_3_) : this(p_i1789_1_, p_i1789_2_, new ItemStack(Items.potionitem, 1, p_i1789_3_))
		{
		}

		public EntityPotion(World p_i1790_1_, EntityLivingBase p_i1790_2_, ItemStack p_i1790_3_) : base(p_i1790_1_, p_i1790_2_)
		{
			this.potionDamage = p_i1790_3_;
		}

		public EntityPotion(World p_i1791_1_, double p_i1791_2_, double p_i1791_4_, double p_i1791_6_, int p_i1791_8_) : this(p_i1791_1_, p_i1791_2_, p_i1791_4_, p_i1791_6_, new ItemStack(Items.potionitem, 1, p_i1791_8_))
		{
		}

		public EntityPotion(World p_i1792_1_, double p_i1792_2_, double p_i1792_4_, double p_i1792_6_, ItemStack p_i1792_8_) : base(p_i1792_1_, p_i1792_2_, p_i1792_4_, p_i1792_6_)
		{
			this.potionDamage = p_i1792_8_;
		}

///    
///     <summary> * Gets the amount of gravity to apply to the thrown entity with each tick. </summary>
///     
		protected internal override float GravityVelocity
		{
			get
			{
				return 0.05F;
			}
		}

		protected internal override float func_70182_d()
		{
			return 0.5F;
		}

		protected internal override float func_70183_g()
		{
			return -20.0F;
		}

		public virtual int PotionDamage
		{
			set
			{
				if (this.potionDamage == null)
				{
					this.potionDamage = new ItemStack(Items.potionitem, 1, 0);
				}
	
				this.potionDamage.ItemDamage = value;
			}
			get
			{
				if (this.potionDamage == null)
				{
					this.potionDamage = new ItemStack(Items.potionitem, 1, 0);
				}
	
				return this.potionDamage.ItemDamage;
			}
		}

///    
///     <summary> * Returns the damage value of the thrown potion that this EntityPotion represents. </summary>
///     

///    
///     <summary> * Called when this EntityThrowable hits a block or entity. </summary>
///     
		protected internal override void onImpact(MovingObjectPosition p_70184_1_)
		{
			if (!this.worldObj.isClient)
			{
				IList var2 = Items.potionitem.getEffects(this.potionDamage);

				if (var2 != null && !var2.Count == 0)
				{
					AxisAlignedBB var3 = this.boundingBox.expand(4.0D, 2.0D, 4.0D);
					IList var4 = this.worldObj.getEntitiesWithinAABB(typeof(EntityLivingBase), var3);

					if (var4 != null && !var4.Count == 0)
					{
						IEnumerator var5 = var4.GetEnumerator();

						while (var5.MoveNext())
						{
							EntityLivingBase var6 = (EntityLivingBase)var5.Current;
							double var7 = this.getDistanceSqToEntity(var6);

							if (var7 < 16.0D)
							{
								double var9 = 1.0D - Math.Sqrt(var7) / 4.0D;

								if (var6 == p_70184_1_.entityHit)
								{
									var9 = 1.0D;
								}

								IEnumerator var11 = var2.GetEnumerator();

								while (var11.MoveNext())
								{
									PotionEffect var12 = (PotionEffect)var11.Current;
									int var13 = var12.PotionID;

									if (Potion.potionTypes[var13].Instant)
									{
										Potion.potionTypes[var13].affectEntity(this.Thrower, var6, var12.Amplifier, var9);
									}
									else
									{
										int var14 = (int)(var9 * (double)var12.Duration + 0.5D);

										if (var14 > 20)
										{
											var6.addPotionEffect(new PotionEffect(var13, var14, var12.Amplifier));
										}
									}
								}
							}
						}
					}
				}

				this.worldObj.playAuxSFX(2002, (int)Math.Round(this.posX), (int)Math.Round(this.posY), (int)Math.Round(this.posZ), this.PotionDamage);
				this.setDead();
			}
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);

			if (p_70037_1_.func_150297_b("Potion", 10))
			{
				this.potionDamage = ItemStack.loadItemStackFromNBT(p_70037_1_.getCompoundTag("Potion"));
			}
			else
			{
				this.PotionDamage = p_70037_1_.getInteger("potionValue");
			}

			if (this.potionDamage == null)
			{
				this.setDead();
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);

			if (this.potionDamage != null)
			{
				p_70014_1_.setTag("Potion", this.potionDamage.writeToNBT(new NBTTagCompound()));
			}
		}
	}

}