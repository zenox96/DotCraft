using System;

namespace DotCraftCore.Entity
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Items = DotCraftCore.Init.Items;
	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using World = DotCraftCore.world.World;

	public abstract class EntityAgeable : EntityCreature
	{
		private float field_98056_d = -1.0F;
		private float field_98057_e;
		private const string __OBFID = "CL_00001530";

		public EntityAgeable(World p_i1578_1_) : base(p_i1578_1_)
		{
		}

		public abstract EntityAgeable createChild(EntityAgeable p_90011_1_);

///    
///     <summary> * Called when a player interacts with a mob. e.g. gets milk from a cow, gets into the saddle on a pig. </summary>
///     
		public virtual bool interact(EntityPlayer p_70085_1_)
		{
			ItemStack var2 = p_70085_1_.inventory.CurrentItem;

			if (var2 != null && var2.Item == Items.spawn_egg)
			{
				if (!this.worldObj.isClient)
				{
					Type var3 = EntityList.getClassFromID(var2.ItemDamage);

					if (var3 != null && var3.isAssignableFrom(this.GetType()))
					{
						EntityAgeable var4 = this.createChild(this);

						if (var4 != null)
						{
							var4.GrowingAge = -24000;
							var4.setLocationAndAngles(this.posX, this.posY, this.posZ, 0.0F, 0.0F);
							this.worldObj.spawnEntityInWorld(var4);

							if (var2.hasDisplayName())
							{
								var4.CustomNameTag = var2.DisplayName;
							}

							if (!p_70085_1_.capabilities.isCreativeMode)
							{
								--var2.stackSize;

								if (var2.stackSize <= 0)
								{
									p_70085_1_.inventory.setInventorySlotContents(p_70085_1_.inventory.currentItem, (ItemStack)null);
								}
							}
						}
					}
				}

				return true;
			}
			else
			{
				return false;
			}
		}

		protected internal virtual void entityInit()
		{
			base.entityInit();
			this.dataWatcher.addObject(12, new int?(0));
		}

///    
///     <summary> * The age value may be negative or positive or zero. If it's negative, it get's incremented on each tick, if it's
///     * positive, it get's decremented each tick. Don't confuse this with EntityLiving.getAge. With a negative value the
///     * Entity is considered a child. </summary>
///     
		public virtual int GrowingAge
		{
			get
			{
				return this.dataWatcher.getWatchableObjectInt(12);
			}
			set
			{
				this.dataWatcher.updateObject(12, Convert.ToInt32(value));
				this.ScaleForAge = this.Child;
			}
		}

///    
///     <summary> * "Adds the value of the parameter times 20 to the age of this entity. If the entity is an adult (if the entity's
///     * age is greater than 0), it will have no effect." </summary>
///     
		public virtual void addGrowth(int p_110195_1_)
		{
			int var2 = this.GrowingAge;
			var2 += p_110195_1_ * 20;

			if (var2 > 0)
			{
				var2 = 0;
			}

			this.GrowingAge = var2;
		}

///    
///     <summary> * The age value may be negative or positive or zero. If it's negative, it get's incremented on each tick, if it's
///     * positive, it get's decremented each tick. With a negative value the Entity is considered a child. </summary>
///     

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public virtual void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			p_70014_1_.setInteger("Age", this.GrowingAge);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public virtual void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			this.GrowingAge = p_70037_1_.getInteger("Age");
		}

///    
///     <summary> * Called frequently so the entity can update its state every tick as required. For example, zombies and skeletons
///     * use this to react to sunlight and start to burn. </summary>
///     
		public virtual void onLivingUpdate()
		{
			base.onLivingUpdate();

			if (this.worldObj.isClient)
			{
				this.ScaleForAge = this.Child;
			}
			else
			{
				int var1 = this.GrowingAge;

				if (var1 < 0)
				{
					++var1;
					this.GrowingAge = var1;
				}
				else if (var1 > 0)
				{
					--var1;
					this.GrowingAge = var1;
				}
			}
		}

///    
///     <summary> * If Animal, checks if the age timer is negative </summary>
///     
		public virtual bool isChild()
		{
			get
			{
				return this.GrowingAge < 0;
			}
		}

///    
///     <summary> * "Sets the scale for an ageable entity according to the boolean parameter, which says if it's a child." </summary>
///     
		public virtual bool ScaleForAge
		{
			set
			{
				this.Scale = value ? 0.5F : 1.0F;
			}
		}

///    
///     <summary> * Sets the width and height of the entity. Args: width, height </summary>
///     
		protected internal void setSize(float p_70105_1_, float p_70105_2_)
		{
			bool var3 = this.field_98056_d > 0.0F;
			this.field_98056_d = p_70105_1_;
			this.field_98057_e = p_70105_2_;

			if (!var3)
			{
				this.Scale = 1.0F;
			}
		}

		protected internal float Scale
		{
			set
			{
				base.setSize(this.field_98056_d * value, this.field_98057_e * value);
			}
		}
	}

}