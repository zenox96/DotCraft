namespace DotCraftCore.nInventory
{

	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using ItemStack = DotCraftCore.nItem.ItemStack;

	public interface IInventory
	{
///    
///     <summary> * Returns the number of slots in the inventory. </summary>
///     
		int SizeInventory {get;}

///    
///     <summary> * Returns the stack in slot i </summary>
///     
		ItemStack getStackInSlot(int p_70301_1_);

///    
///     <summary> * Removes from an inventory slot (first arg) up to a specified number (second arg) of items and returns them in a
///     * new stack. </summary>
///     
		ItemStack decrStackSize(int p_70298_1_, int p_70298_2_);

///    
///     <summary> * When some containers are closed they call this on each slot, then drop whatever it returns as an EntityItem -
///     * like when you close a workbench GUI. </summary>
///     
		ItemStack getStackInSlotOnClosing(int p_70304_1_);

///    
///     <summary> * Sets the given item stack to the specified slot in the inventory (can be crafting or armor sections). </summary>
///     
		void setInventorySlotContents(int p_70299_1_, ItemStack p_70299_2_);

///    
///     <summary> * Returns the name of the inventory </summary>
///     
		string InventoryName {get;}

///    
///     <summary> * Returns if the inventory name is localized </summary>
///     
		bool isInventoryNameLocalized() {get;}

///    
///     <summary> * Returns the maximum stack size for a inventory slot. </summary>
///     
		int InventoryStackLimit {get;}

///    
///     <summary> * Called when an the contents of an Inventory change, usually </summary>
///     
		void onInventoryChanged();

///    
///     <summary> * Do not make give this method the name canInteractWith because it clashes with Container </summary>
///     
		bool isUseableByPlayer(EntityPlayer p_70300_1_);

		void openInventory();

		void closeInventory();

///    
///     <summary> * Returns true if automation is allowed to insert the given stack (ignoring stack size) into the given slot. </summary>
///     
		bool isItemValidForSlot(int p_94041_1_, ItemStack p_94041_2_);
	}

}