using System.Collections;

namespace DotCraftCore.Inventory
{

	using ItemStack = DotCraftCore.item.ItemStack;

	public interface ICrafting
	{
		void sendContainerAndContentsToPlayer(Container p_71110_1_, IList p_71110_2_);

///    
///     <summary> * Sends the contents of an inventory slot to the client-side Container. This doesn't have to match the actual
///     * contents of that slot. Args: Container, slot number, slot contents </summary>
///     
		void sendSlotContents(Container p_71111_1_, int p_71111_2_, ItemStack p_71111_3_);

///    
///     <summary> * Sends two ints to the client-side Container. Used for furnace burning time, smelting progress, brewing progress,
///     * and enchanting level. Normally the first int identifies which variable to update, and the second contains the new
///     * value. Both are truncated to shorts in non-local SMP. </summary>
///     
		void sendProgressBarUpdate(Container p_71112_1_, int p_71112_2_, int p_71112_3_);
	}

}