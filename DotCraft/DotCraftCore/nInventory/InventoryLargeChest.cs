namespace DotCraftCore.nInventory
{

	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using ItemStack = DotCraftCore.nItem.ItemStack;

	public class InventoryLargeChest : IInventory
	{
	/// <summary> Name of the chest.  </summary>
		private string name;

	/// <summary> Inventory object corresponding to double chest upper part  </summary>
		private IInventory upperChest;

	/// <summary> Inventory object corresponding to double chest lower part  </summary>
		private IInventory lowerChest;
		

		public InventoryLargeChest(string p_i1559_1_, IInventory p_i1559_2_, IInventory p_i1559_3_)
		{
			this.name = p_i1559_1_;

			if (p_i1559_2_ == null)
			{
				p_i1559_2_ = p_i1559_3_;
			}

			if (p_i1559_3_ == null)
			{
				p_i1559_3_ = p_i1559_2_;
			}

			this.upperChest = p_i1559_2_;
			this.lowerChest = p_i1559_3_;
		}

///    
///     <summary> * Returns the number of slots in the inventory. </summary>
///     
		public virtual int SizeInventory
		{
			get
			{
				return this.upperChest.SizeInventory + this.lowerChest.SizeInventory;
			}
		}

///    
///     <summary> * Return whether the given inventory is part of this large chest. </summary>
///     
		public virtual bool isPartOfLargeChest(IInventory p_90010_1_)
		{
			return this.upperChest == p_90010_1_ || this.lowerChest == p_90010_1_;
		}

///    
///     <summary> * Returns the name of the inventory </summary>
///     
		public virtual string InventoryName
		{
			get
			{
				return this.upperChest.InventoryNameLocalized ? this.upperChest.InventoryName : (this.lowerChest.InventoryNameLocalized ? this.lowerChest.InventoryName : this.name);
			}
		}

///    
///     <summary> * Returns if the inventory name is localized </summary>
///     
		public virtual bool isInventoryNameLocalized()
		{
			get
			{
				return this.upperChest.InventoryNameLocalized || this.lowerChest.InventoryNameLocalized;
			}
		}

///    
///     <summary> * Returns the stack in slot i </summary>
///     
		public virtual ItemStack getStackInSlot(int p_70301_1_)
		{
			return p_70301_1_ >= this.upperChest.SizeInventory ? this.lowerChest.getStackInSlot(p_70301_1_ - this.upperChest.SizeInventory) : this.upperChest.getStackInSlot(p_70301_1_);
		}

///    
///     <summary> * Removes from an inventory slot (first arg) up to a specified number (second arg) of items and returns them in a
///     * new stack. </summary>
///     
		public virtual ItemStack decrStackSize(int p_70298_1_, int p_70298_2_)
		{
			return p_70298_1_ >= this.upperChest.SizeInventory ? this.lowerChest.decrStackSize(p_70298_1_ - this.upperChest.SizeInventory, p_70298_2_) : this.upperChest.decrStackSize(p_70298_1_, p_70298_2_);
		}

///    
///     <summary> * When some containers are closed they call this on each slot, then drop whatever it returns as an EntityItem -
///     * like when you close a workbench GUI. </summary>
///     
		public virtual ItemStack getStackInSlotOnClosing(int p_70304_1_)
		{
			return p_70304_1_ >= this.upperChest.SizeInventory ? this.lowerChest.getStackInSlotOnClosing(p_70304_1_ - this.upperChest.SizeInventory) : this.upperChest.getStackInSlotOnClosing(p_70304_1_);
		}

///    
///     <summary> * Sets the given item stack to the specified slot in the inventory (can be crafting or armor sections). </summary>
///     
		public virtual void setInventorySlotContents(int p_70299_1_, ItemStack p_70299_2_)
		{
			if (p_70299_1_ >= this.upperChest.SizeInventory)
			{
				this.lowerChest.setInventorySlotContents(p_70299_1_ - this.upperChest.SizeInventory, p_70299_2_);
			}
			else
			{
				this.upperChest.setInventorySlotContents(p_70299_1_, p_70299_2_);
			}
		}

///    
///     <summary> * Returns the maximum stack size for a inventory slot. </summary>
///     
		public virtual int InventoryStackLimit
		{
			get
			{
				return this.upperChest.InventoryStackLimit;
			}
		}

///    
///     <summary> * Called when an the contents of an Inventory change, usually </summary>
///     
		public virtual void onInventoryChanged()
		{
			this.upperChest.onInventoryChanged();
			this.lowerChest.onInventoryChanged();
		}

///    
///     <summary> * Do not make give this method the name canInteractWith because it clashes with Container </summary>
///     
		public virtual bool isUseableByPlayer(EntityPlayer p_70300_1_)
		{
			return this.upperChest.isUseableByPlayer(p_70300_1_) && this.lowerChest.isUseableByPlayer(p_70300_1_);
		}

		public virtual void openInventory()
		{
			this.upperChest.openInventory();
			this.lowerChest.openInventory();
		}

		public virtual void closeInventory()
		{
			this.upperChest.closeInventory();
			this.lowerChest.closeInventory();
		}

///    
///     <summary> * Returns true if automation is allowed to insert the given stack (ignoring stack size) into the given slot. </summary>
///     
		public virtual bool isItemValidForSlot(int p_94041_1_, ItemStack p_94041_2_)
		{
			return true;
		}
	}

}