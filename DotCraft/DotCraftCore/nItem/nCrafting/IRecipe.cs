namespace DotCraftCore.nItem.nCrafting
{

	using InventoryCrafting = DotCraftCore.inventory.InventoryCrafting;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using World = DotCraftCore.nWorld.World;

	public interface IRecipe
	{
///    
///     <summary> * Used to check if a recipe matches current crafting inventory </summary>
///     
		bool matches(InventoryCrafting p_77569_1_, World p_77569_2_);

///    
///     <summary> * Returns an Item that is the result of this recipe </summary>
///     
		ItemStack getCraftingResult(InventoryCrafting p_77572_1_);

///    
///     <summary> * Returns the size of the recipe area </summary>
///     
		int RecipeSize {get;}

		ItemStack RecipeOutput {get;}
	}

}