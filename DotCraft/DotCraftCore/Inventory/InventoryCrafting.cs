namespace DotCraftCore.Inventory
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using ItemStack = DotCraftCore.item.ItemStack;

	public class InventoryCrafting : IInventory
	{
	/// <summary> List of the stacks in the crafting matrix.  </summary>
		private ItemStack[] stackList;

	/// <summary> the width of the crafting inventory  </summary>
		private int inventoryWidth;

///    
///     <summary> * Class containing the callbacks for the events on_GUIClosed and on_CraftMaxtrixChanged. </summary>
///     
		private Container eventHandler;
		private const string __OBFID = "CL_00001743";

		public InventoryCrafting(Container p_i1807_1_, int p_i1807_2_, int p_i1807_3_)
		{
			int var4 = p_i1807_2_ * p_i1807_3_;
			this.stackList = new ItemStack[var4];
			this.eventHandler = p_i1807_1_;
			this.inventoryWidth = p_i1807_2_;
		}

///    
///     <summary> * Returns the number of slots in the inventory. </summary>
///     
		public virtual int SizeInventory
		{
			get
			{
				return this.stackList.Length;
			}
		}

///    
///     <summary> * Returns the stack in slot i </summary>
///     
		public virtual ItemStack getStackInSlot(int p_70301_1_)
		{
			return p_70301_1_ >= this.SizeInventory ? null : this.stackList[p_70301_1_];
		}

///    
///     <summary> * Returns the itemstack in the slot specified (Top left is 0, 0). Args: row, column </summary>
///     
		public virtual ItemStack getStackInRowAndColumn(int p_70463_1_, int p_70463_2_)
		{
			if (p_70463_1_ >= 0 && p_70463_1_ < this.inventoryWidth)
			{
				int var3 = p_70463_1_ + p_70463_2_ * this.inventoryWidth;
				return this.getStackInSlot(var3);
			}
			else
			{
				return null;
			}
		}

///    
///     <summary> * Returns the name of the inventory </summary>
///     
		public virtual string InventoryName
		{
			get
			{
				return "container.crafting";
			}
		}

///    
///     <summary> * Returns if the inventory name is localized </summary>
///     
		public virtual bool isInventoryNameLocalized()
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * When some containers are closed they call this on each slot, then drop whatever it returns as an EntityItem -
///     * like when you close a workbench GUI. </summary>
///     
		public virtual ItemStack getStackInSlotOnClosing(int p_70304_1_)
		{
			if (this.stackList[p_70304_1_] != null)
			{
				ItemStack var2 = this.stackList[p_70304_1_];
				this.stackList[p_70304_1_] = null;
				return var2;
			}
			else
			{
				return null;
			}
		}

///    
///     <summary> * Removes from an inventory slot (first arg) up to a specified number (second arg) of items and returns them in a
///     * new stack. </summary>
///     
		public virtual ItemStack decrStackSize(int p_70298_1_, int p_70298_2_)
		{
			if (this.stackList[p_70298_1_] != null)
			{
				ItemStack var3;

				if (this.stackList[p_70298_1_].stackSize <= p_70298_2_)
				{
					var3 = this.stackList[p_70298_1_];
					this.stackList[p_70298_1_] = null;
					this.eventHandler.onCraftMatrixChanged(this);
					return var3;
				}
				else
				{
					var3 = this.stackList[p_70298_1_].splitStack(p_70298_2_);

					if (this.stackList[p_70298_1_].stackSize == 0)
					{
						this.stackList[p_70298_1_] = null;
					}

					this.eventHandler.onCraftMatrixChanged(this);
					return var3;
				}
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
			this.stackList[p_70299_1_] = p_70299_2_;
			this.eventHandler.onCraftMatrixChanged(this);
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
			return true;
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
	}

}