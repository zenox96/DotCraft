using System.Collections;

namespace DotCraftCore.Inventory
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using ItemStack = DotCraftCore.Item.ItemStack;

	public class InventoryBasic : IInventory
	{
		private string inventoryTitle;
		private int slotsCount;
		private ItemStack[] inventoryContents;
		private IList field_70480_d;
		private bool field_94051_e;
		

		public InventoryBasic(string p_i1561_1_, bool p_i1561_2_, int p_i1561_3_)
		{
			this.inventoryTitle = p_i1561_1_;
			this.field_94051_e = p_i1561_2_;
			this.slotsCount = p_i1561_3_;
			this.inventoryContents = new ItemStack[p_i1561_3_];
		}

		public virtual void func_110134_a(IInvBasic p_110134_1_)
		{
			if (this.field_70480_d == null)
			{
				this.field_70480_d = new ArrayList();
			}

			this.field_70480_d.Add(p_110134_1_);
		}

		public virtual void func_110132_b(IInvBasic p_110132_1_)
		{
			this.field_70480_d.Remove(p_110132_1_);
		}

///    
///     <summary> * Returns the stack in slot i </summary>
///     
		public virtual ItemStack getStackInSlot(int p_70301_1_)
		{
			return p_70301_1_ >= 0 && p_70301_1_ < this.inventoryContents.Length ? this.inventoryContents[p_70301_1_] : null;
		}

///    
///     <summary> * Removes from an inventory slot (first arg) up to a specified number (second arg) of items and returns them in a
///     * new stack. </summary>
///     
		public virtual ItemStack decrStackSize(int p_70298_1_, int p_70298_2_)
		{
			if (this.inventoryContents[p_70298_1_] != null)
			{
				ItemStack var3;

				if (this.inventoryContents[p_70298_1_].stackSize <= p_70298_2_)
				{
					var3 = this.inventoryContents[p_70298_1_];
					this.inventoryContents[p_70298_1_] = null;
					this.onInventoryChanged();
					return var3;
				}
				else
				{
					var3 = this.inventoryContents[p_70298_1_].splitStack(p_70298_2_);

					if (this.inventoryContents[p_70298_1_].stackSize == 0)
					{
						this.inventoryContents[p_70298_1_] = null;
					}

					this.onInventoryChanged();
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
			if (this.inventoryContents[p_70304_1_] != null)
			{
				ItemStack var2 = this.inventoryContents[p_70304_1_];
				this.inventoryContents[p_70304_1_] = null;
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
			this.inventoryContents[p_70299_1_] = p_70299_2_;

			if (p_70299_2_ != null && p_70299_2_.stackSize > this.InventoryStackLimit)
			{
				p_70299_2_.stackSize = this.InventoryStackLimit;
			}

			this.onInventoryChanged();
		}

///    
///     <summary> * Returns the number of slots in the inventory. </summary>
///     
		public virtual int SizeInventory
		{
			get
			{
				return this.slotsCount;
			}
		}

///    
///     <summary> * Returns the name of the inventory </summary>
///     
		public virtual string InventoryName
		{
			get
			{
				return this.inventoryTitle;
			}
		}

///    
///     <summary> * Returns if the inventory name is localized </summary>
///     
		public virtual bool isInventoryNameLocalized()
		{
			get
			{
				return this.field_94051_e;
			}
		}

		public virtual void func_110133_a(string p_110133_1_)
		{
			this.field_94051_e = true;
			this.inventoryTitle = p_110133_1_;
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
			if (this.field_70480_d != null)
			{
				for (int var1 = 0; var1 < this.field_70480_d.Count; ++var1)
				{
					((IInvBasic)this.field_70480_d.get(var1)).onInventoryChanged(this);
				}
			}
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