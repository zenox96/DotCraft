using System;

namespace DotCraftCore.Entity.Item
{

	using Entity = DotCraftCore.Entity.Entity;
	using EntityHanging = DotCraftCore.Entity.EntityHanging;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Items = DotCraftCore.Init.Items;
	using ItemMap = DotCraftCore.Item.ItemMap;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using World = DotCraftCore.World.World;
	using MapData = DotCraftCore.World.Storage.MapData;

	public class EntityItemFrame : EntityHanging
	{
	/// <summary> Chance for this item frame's item to drop from the frame.  </summary>
		private float itemDropChance = 1.0F;
		private const string __OBFID = "CL_00001547";

		public EntityItemFrame(World p_i1590_1_) : base(p_i1590_1_)
		{
		}

		public EntityItemFrame(World p_i1591_1_, int p_i1591_2_, int p_i1591_3_, int p_i1591_4_, int p_i1591_5_) : base(p_i1591_1_, p_i1591_2_, p_i1591_3_, p_i1591_4_, p_i1591_5_)
		{
			this.Direction = p_i1591_5_;
		}

		protected internal override void entityInit()
		{
			this.DataWatcher.addObjectByDataType(2, 5);
			this.DataWatcher.addObject(3, Convert.ToByte((sbyte)0));
		}

///    
///     <summary> * Called when the entity is attacked. </summary>
///     
		public override bool attackEntityFrom(DamageSource p_70097_1_, float p_70097_2_)
		{
			if (this.EntityInvulnerable)
			{
				return false;
			}
			else if (this.DisplayedItem != null)
			{
				if (!this.worldObj.isClient)
				{
					this.func_146065_b(p_70097_1_.Entity, false);
					this.DisplayedItem = (ItemStack)null;
				}

				return true;
			}
			else
			{
				return base.attackEntityFrom(p_70097_1_, p_70097_2_);
			}
		}

		public override int WidthPixels
		{
			get
			{
				return 9;
			}
		}

		public override int HeightPixels
		{
			get
			{
				return 9;
			}
		}

///    
///     <summary> * Checks if the entity is in range to render by using the past in distance and comparing it to its average edge
///     * length * 64 * renderDistanceWeight Args: distance </summary>
///     
		public override bool isInRangeToRenderDist(double p_70112_1_)
		{
			double var3 = 16.0D;
			var3 *= 64.0D * this.renderDistanceWeight;
			return p_70112_1_ < var3 * var3;
		}

///    
///     <summary> * Called when this entity is broken. Entity parameter may be null. </summary>
///     
		public override void onBroken(Entity p_110128_1_)
		{
			this.func_146065_b(p_110128_1_, true);
		}

		public virtual void func_146065_b(Entity p_146065_1_, bool p_146065_2_)
		{
			ItemStack var3 = this.DisplayedItem;

			if (p_146065_1_ is EntityPlayer)
			{
				EntityPlayer var4 = (EntityPlayer)p_146065_1_;

				if (var4.capabilities.isCreativeMode)
				{
					this.removeFrameFromMap(var3);
					return;
				}
			}

			if (p_146065_2_)
			{
				this.entityDropItem(new ItemStack(Items.item_frame), 0.0F);
			}

			if (var3 != null && this.rand.nextFloat() < this.itemDropChance)
			{
				var3 = var3.copy();
				this.removeFrameFromMap(var3);
				this.entityDropItem(var3, 0.0F);
			}
		}

///    
///     <summary> * Removes the dot representing this frame's position from the map when the item frame is broken. </summary>
///     
		private void removeFrameFromMap(ItemStack p_110131_1_)
		{
			if (p_110131_1_ != null)
			{
				if (p_110131_1_.Item == Items.filled_map)
				{
					MapData var2 = ((ItemMap)p_110131_1_.Item).getMapData(p_110131_1_, this.worldObj);
					var2.playersVisibleOnMap.remove("frame-" + this.EntityId);
				}

				p_110131_1_.ItemFrame = (EntityItemFrame)null;
			}
		}

		public virtual ItemStack DisplayedItem
		{
			get
			{
				return this.DataWatcher.getWatchableObjectItemStack(2);
			}
			set
			{
				if (value != null)
				{
					value = value.copy();
					value.stackSize = 1;
					value.ItemFrame = this;
				}
	
				this.DataWatcher.updateObject(2, value);
				this.DataWatcher.ObjectWatched = 2;
			}
		}


///    
///     <summary> * Return the rotation of the item currently on this frame. </summary>
///     
		public virtual int Rotation
		{
			get
			{
				return this.DataWatcher.getWatchableObjectByte(3);
			}
		}

		public virtual int ItemRotation
		{
			set
			{
				this.DataWatcher.updateObject(3, Convert.ToByte((sbyte)(value % 4)));
			}
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		public override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			if (this.DisplayedItem != null)
			{
				p_70014_1_.setTag("Item", this.DisplayedItem.writeToNBT(new NBTTagCompound()));
				p_70014_1_.setByte("ItemRotation", (sbyte)this.Rotation);
				p_70014_1_.setFloat("ItemDropChance", this.itemDropChance);
			}

			base.writeEntityToNBT(p_70014_1_);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		public override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			NBTTagCompound var2 = p_70037_1_.getCompoundTag("Item");

			if (var2 != null && !var2.hasNoTags())
			{
				this.DisplayedItem = ItemStack.loadItemStackFromNBT(var2);
				this.ItemRotation = p_70037_1_.getByte("ItemRotation");

				if (p_70037_1_.func_150297_b("ItemDropChance", 99))
				{
					this.itemDropChance = p_70037_1_.getFloat("ItemDropChance");
				}
			}

			base.readEntityFromNBT(p_70037_1_);
		}

///    
///     <summary> * First layer of player interaction </summary>
///     
		public override bool interactFirst(EntityPlayer p_130002_1_)
		{
			if (this.DisplayedItem == null)
			{
				ItemStack var2 = p_130002_1_.HeldItem;

				if (var2 != null && !this.worldObj.isClient)
				{
					this.DisplayedItem = var2;

					if (!p_130002_1_.capabilities.isCreativeMode && --var2.stackSize <= 0)
					{
						p_130002_1_.inventory.setInventorySlotContents(p_130002_1_.inventory.currentItem, (ItemStack)null);
					}
				}
			}
			else if (!this.worldObj.isClient)
			{
				this.ItemRotation = this.Rotation + 1;
			}

			return true;
		}
	}

}