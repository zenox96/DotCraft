using System.Collections;

namespace DotCraftCore.Inventory
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using IIcon = DotCraftCore.Util.IIcon;

	public class Slot
	{
	/// <summary> The index of the slot in the inventory.  </summary>
		private readonly int slotIndex;

	/// <summary> The inventory we want to extract a slot from.  </summary>
		public readonly IInventory inventory;

	/// <summary> the id of the slot(also the index in the inventory arraylist)  </summary>
		public int slotNumber;

	/// <summary> display position of the inventory slot on the screen x axis  </summary>
		public int xDisplayPosition;

	/// <summary> display position of the inventory slot on the screen y axis  </summary>
		public int yDisplayPosition;
		

		public Slot(IInventory p_i1824_1_, int p_i1824_2_, int p_i1824_3_, int p_i1824_4_)
		{
			this.inventory = p_i1824_1_;
			this.slotIndex = p_i1824_2_;
			this.xDisplayPosition = p_i1824_3_;
			this.yDisplayPosition = p_i1824_4_;
		}

///    
///     <summary> * if par2 has more items than par1, onCrafting(item,countIncrease) is called </summary>
///     
		public virtual void onSlotChange(ItemStack p_75220_1_, ItemStack p_75220_2_)
		{
			if (p_75220_1_ != null && p_75220_2_ != null)
			{
				if (p_75220_1_.Item == p_75220_2_.Item)
				{
					int var3 = p_75220_2_.stackSize - p_75220_1_.stackSize;

					if (var3 > 0)
					{
						this.onCrafting(p_75220_1_, var3);
					}
				}
			}
		}

///    
///     <summary> * the itemStack passed in is the output - ie, iron ingots, and pickaxes, not ore and wood. Typically increases an
///     * internal count then calls onCrafting(item). </summary>
///     
		protected internal virtual void onCrafting(ItemStack p_75210_1_, int p_75210_2_)
		{
		}

///    
///     <summary> * the itemStack passed in is the output - ie, iron ingots, and pickaxes, not ore and wood. </summary>
///     
		protected internal virtual void onCrafting(ItemStack p_75208_1_)
		{
		}

		public virtual void onPickupFromSlot(EntityPlayer p_82870_1_, ItemStack p_82870_2_)
		{
			this.onSlotChanged();
		}

///    
///     <summary> * Check if the stack is a valid item for this slot. Always true beside for the armor slots. </summary>
///     
		public virtual bool isItemValid(ItemStack p_75214_1_)
		{
			return true;
		}

///    
///     <summary> * Helper fnct to get the stack in the slot. </summary>
///     
		public virtual ItemStack Stack
		{
			get
			{
				return this.inventory.getStackInSlot(this.slotIndex);
			}
		}

///    
///     <summary> * Returns if this slot contains a stack. </summary>
///     
		public virtual bool HasStack
		{
			get
			{
				return this.Stack != null;
			}
		}

///    
///     <summary> * Helper method to put a stack in the slot. </summary>
///     
		public virtual void putStack(ItemStack p_75215_1_)
		{
			this.inventory.setInventorySlotContents(this.slotIndex, p_75215_1_);
			this.onSlotChanged();
		}

///    
///     <summary> * Called when the stack in a Slot changes </summary>
///     
		public virtual void onSlotChanged()
		{
			this.inventory.onInventoryChanged();
		}

///    
///     <summary> * Returns the maximum stack size for a given slot (usually the same as getInventoryStackLimit(), but 1 in the case
///     * of armor slots) </summary>
///     
		public virtual int SlotStackLimit
		{
			get
			{
				return this.inventory.InventoryStackLimit;
			}
		}

///    
///     <summary> * Returns the icon index on items.png that is used as background image of the slot. </summary>
///     
		public virtual IIcon BackgroundIconIndex
		{
			get
			{
				return null;
			}
		}

///    
///     <summary> * Decrease the size of the stack in slot (first int arg) by the amount of the second int arg. Returns the new
///     * stack. </summary>
///     
		public virtual ItemStack decrStackSize(int p_75209_1_)
		{
			return this.inventory.decrStackSize(this.slotIndex, p_75209_1_);
		}

///    
///     <summary> * returns true if this slot is in par2 of par1 </summary>
///     
		public virtual bool isSlotInInventory(IInventory p_75217_1_, int p_75217_2_)
		{
			return p_75217_1_ == this.inventory && p_75217_2_ == this.slotIndex;
		}

///    
///     <summary> * Return whether this slot's stack can be taken from this slot. </summary>
///     
		public virtual bool canTakeStack(EntityPlayer p_82869_1_)
		{
			return true;
		}

		public virtual bool func_111238_b()
		{
			return true;
		}
	}

}