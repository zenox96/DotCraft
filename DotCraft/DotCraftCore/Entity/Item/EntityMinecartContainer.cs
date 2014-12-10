namespace DotCraftCore.Entity.Item
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Container = DotCraftCore.Inventory.Container;
	using IInventory = DotCraftCore.Inventory.IInventory;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using NBTTagList = DotCraftCore.NBT.NBTTagList;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using World = DotCraftCore.World.World;

	public abstract class EntityMinecartContainer : EntityMinecart, IInventory
	{
		private ItemStack[] minecartContainerItems = new ItemStack[36];

///    
///     <summary> * When set to true, the minecart will drop all items when setDead() is called. When false (such as when travelling
///     * dimensions) it preserves its contents. </summary>
///     
		private bool dropContentsWhenDead = true;
		private const string __OBFID = "CL_00001674";

		public EntityMinecartContainer(World p_i1716_1_) : base(p_i1716_1_)
		{
		}

		public EntityMinecartContainer(World p_i1717_1_, double p_i1717_2_, double p_i1717_4_, double p_i1717_6_) : base(p_i1717_1_, p_i1717_2_, p_i1717_4_, p_i1717_6_)
		{
		}

		public override void killMinecart(DamageSource p_94095_1_)
		{
			base.killMinecart(p_94095_1_);

			for (int var2 = 0; var2 < this.SizeInventory; ++var2)
			{
				ItemStack var3 = this.getStackInSlot(var2);

				if (var3 != null)
				{
					float var4 = this.rand.nextFloat() * 0.8F + 0.1F;
					float var5 = this.rand.nextFloat() * 0.8F + 0.1F;
					float var6 = this.rand.nextFloat() * 0.8F + 0.1F;

					while (var3.stackSize > 0)
					{
						int var7 = this.rand.Next(21) + 10;

						if (var7 > var3.stackSize)
						{
							var7 = var3.stackSize;
						}

						var3.stackSize -= var7;
						EntityItem var8 = new EntityItem(this.worldObj, this.posX + (double)var4, this.posY + (double)var5, this.posZ + (double)var6, new ItemStack(var3.Item, var7, var3.ItemDamage));
						float var9 = 0.05F;
						var8.motionX = (double)((float)this.rand.nextGaussian() * var9);
						var8.motionY = (double)((float)this.rand.nextGaussian() * var9 + 0.2F);
						var8.motionZ = (double)((float)this.rand.nextGaussian() * var9);
						this.worldObj.spawnEntityInWorld(var8);
					}
				}
			}
		}

///    
///     <summary> * Returns the stack in slot i </summary>
///     
		public virtual ItemStack getStackInSlot(int p_70301_1_)
		{
			return this.minecartContainerItems[p_70301_1_];
		}

///    
///     <summary> * Removes from an inventory slot (first arg) up to a specified number (second arg) of items and returns them in a
///     * new stack. </summary>
///     
		public virtual ItemStack decrStackSize(int p_70298_1_, int p_70298_2_)
		{
			if (this.minecartContainerItems[p_70298_1_] != null)
			{
				ItemStack var3;

				if (this.minecartContainerItems[p_70298_1_].stackSize <= p_70298_2_)
				{
					var3 = this.minecartContainerItems[p_70298_1_];
					this.minecartContainerItems[p_70298_1_] = null;
					return var3;
				}
				else
				{
					var3 = this.minecartContainerItems[p_70298_1_].splitStack(p_70298_2_);

					if (this.minecartContainerItems[p_70298_1_].stackSize == 0)
					{
						this.minecartContainerItems[p_70298_1_] = null;
					}

					return var3;
				}
			}
			else
			{
				return null;
			}
		}

///    
///     <summary> * When some containers are closed they call this on each slot, then drop whatever it returns as an EntityItem -
///     * like when you close a workbench GUI. </summary>
///     
		public virtual ItemStack getStackInSlotOnClosing(int p_70304_1_)
		{
			if (this.minecartContainerItems[p_70304_1_] != null)
			{
				ItemStack var2 = this.minecartContainerItems[p_70304_1_];
				this.minecartContainerItems[p_70304_1_] = null;
				return var2;
			}
			else
			{
				return null;
			}
		}

///    
///     <summary> * Sets the given item stack to the specified slot in the inventory (can be crafting or armor sections). </summary>
///     
		public virtual void setInventorySlotContents(int p_70299_1_, ItemStack p_70299_2_)
		{
			this.minecartContainerItems[p_70299_1_] = p_70299_2_;

			if (p_70299_2_ != null && p_70299_2_.stackSize > this.InventoryStackLimit)
			{
				p_70299_2_.stackSize = this.InventoryStackLimit;
			}
		}

///    
///     <summary> * Called when an the contents of an Inventory change, usually </summary>
///     
		public virtual void onInventoryChanged()
		{
		}

///    
///     <summary> * Do not make give this method the name canInteractWith because it clashes with Container </summary>
///     
		public virtual bool isUseableByPlayer(EntityPlayer p_70300_1_)
		{
			return this.isDead ? false : p_70300_1_.getDistanceSqToEntity(this) <= 64.0D;
		}

		public virtual void openInventory()
		{
		}

		public virtual void closeInventory()
		{
		}

///    
///     <summary> * Returns true if automation is allowed to insert the given stack (ignoring stack size) into the given slot. </summary>
///     
		public virtual bool isItemValidForSlot(int p_94041_1_, ItemStack p_94041_2_)
		{
			return true;
		}

///    
///     <summary> * Returns the name of the inventory </summary>
///     
		public virtual string InventoryName
		{
			get
			{
				return this.InventoryNameLocalized ? this.func_95999_t() : "container.minecart";
			}
		}

///    
///     <summary> * Returns the maximum stack size for a inventory slot. </summary>
///     
		public virtual int InventoryStackLimit
		{
			get
			{
				return 64;
			}
		}

///    
///     <summary> * Teleports the entity to another dimension. Params: Dimension number to teleport to </summary>
///     
		public override void travelToDimension(int p_71027_1_)
		{
			this.dropContentsWhenDead = false;
			base.travelToDimension(p_71027_1_);
		}

///    
///     <summary> * Will get destroyed next tick. </summary>
///     
		public override void setDead()
		{
			if (this.dropContentsWhenDead)
			{
				for (int var1 = 0; var1 < this.SizeInventory; ++var1)
				{
					ItemStack var2 = this.getStackInSlot(var1);

					if (var2 != null)
					{
						float var3 = this.rand.nextFloat() * 0.8F + 0.1F;
						float var4 = this.rand.nextFloat() * 0.8F + 0.1F;
						float var5 = this.rand.nextFloat() * 0.8F + 0.1F;

						while (var2.stackSize > 0)
						{
							int var6 = this.rand.Next(21) + 10;

							if (var6 > var2.stackSize)
							{
								var6 = var2.stackSize;
							}

							var2.stackSize -= var6;
							EntityItem var7 = new EntityItem(this.worldObj, this.posX + (double)var3, this.posY + (double)var4, this.posZ + (double)var5, new ItemStack(var2.Item, var6, var2.ItemDamage));

							if (var2.hasTagCompound())
							{
								var7.EntityItem.TagCompound = (NBTTagCompound)var2.TagCompound.copy();
							}

							float var8 = 0.05F;
							var7.motionX = (double)((float)this.rand.nextGaussian() * var8);
							var7.motionY = (double)((float)this.rand.nextGaussian() * var8 + 0.2F);
							var7.motionZ = (double)((float)this.rand.nextGaussian() * var8);
							this.worldObj.spawnEntityInWorld(var7);
						}
					}
				}
			}

			base.setDead();
		}

///    
///     <summary> * (abstract) Protected helper method to write subclass entity data to NBT. </summary>
///     
		protected internal override void writeEntityToNBT(NBTTagCompound p_70014_1_)
		{
			base.writeEntityToNBT(p_70014_1_);
			NBTTagList var2 = new NBTTagList();

			for (int var3 = 0; var3 < this.minecartContainerItems.Length; ++var3)
			{
				if (this.minecartContainerItems[var3] != null)
				{
					NBTTagCompound var4 = new NBTTagCompound();
					var4.setByte("Slot", (sbyte)var3);
					this.minecartContainerItems[var3].writeToNBT(var4);
					var2.appendTag(var4);
				}
			}

			p_70014_1_.setTag("Items", var2);
		}

///    
///     <summary> * (abstract) Protected helper method to read subclass entity data from NBT. </summary>
///     
		protected internal override void readEntityFromNBT(NBTTagCompound p_70037_1_)
		{
			base.readEntityFromNBT(p_70037_1_);
			NBTTagList var2 = p_70037_1_.getTagList("Items", 10);
			this.minecartContainerItems = new ItemStack[this.SizeInventory];

			for (int var3 = 0; var3 < var2.tagCount(); ++var3)
			{
				NBTTagCompound var4 = var2.getCompoundTagAt(var3);
				int var5 = var4.getByte("Slot") & 255;

				if (var5 >= 0 && var5 < this.minecartContainerItems.Length)
				{
					this.minecartContainerItems[var5] = ItemStack.loadItemStackFromNBT(var4);
				}
			}
		}

///    
///     <summary> * First layer of player interaction </summary>
///     
		public override bool interactFirst(EntityPlayer p_130002_1_)
		{
			if (!this.worldObj.isClient)
			{
				p_130002_1_.displayGUIChest(this);
			}

			return true;
		}

		protected internal override void applyDrag()
		{
			int var1 = 15 - Container.calcRedstoneFromInventory(this);
			float var2 = 0.98F + (float)var1 * 0.001F;
			this.motionX *= (double)var2;
			this.motionY *= 0.0D;
			this.motionZ *= (double)var2;
		}
	}

}