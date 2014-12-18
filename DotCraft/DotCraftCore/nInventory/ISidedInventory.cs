namespace DotCraftCore.nInventory
{

	using ItemStack = DotCraftCore.nItem.ItemStack;

	public interface ISidedInventory : IInventory
	{
///    
///     <summary> * Returns an array containing the indices of the slots that can be accessed by automation on the given side of this
///     * block. </summary>
///     
		int[] getAccessibleSlotsFromSide(int p_94128_1_);

///    
///     <summary> * Returns true if automation can insert the given item in the given slot from the given side. Args: Slot, item,
///     * side </summary>
///     
		bool canInsertItem(int p_102007_1_, ItemStack p_102007_2_, int p_102007_3_);

///    
///     <summary> * Returns true if automation can extract the given item in the given slot from the given side. Args: Slot, item,
///     * side </summary>
///     
		bool canExtractItem(int p_102008_1_, ItemStack p_102008_2_, int p_102008_3_);
	}

}