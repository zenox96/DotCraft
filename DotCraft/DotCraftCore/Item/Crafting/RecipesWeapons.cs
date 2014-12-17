namespace DotCraftCore.nItem.nCrafting
{

	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using Item = DotCraftCore.nItem.Item;
	using ItemStack = DotCraftCore.nItem.ItemStack;

	public class RecipesWeapons
	{
		private string[][] recipePatterns = new string[][] {{"X", "X", "#"}};
		private object[][] recipeItems;
		

		public RecipesWeapons()
		{
			this.recipeItems = new object[][] {{Blocks.planks, Blocks.cobblestone, Items.iron_ingot, Items.diamond, Items.gold_ingot}, {Items.wooden_sword, Items.stone_sword, Items.iron_sword, Items.diamond_sword, Items.golden_sword}};
		}

///    
///     <summary> * Adds the weapon recipes to the CraftingManager. </summary>
///     
		public virtual void addRecipes(CraftingManager p_77583_1_)
		{
			for (int var2 = 0; var2 < this.recipeItems[0].Length; ++var2)
			{
				object var3 = this.recipeItems[0][var2];

				for (int var4 = 0; var4 < this.recipeItems.Length - 1; ++var4)
				{
					Item var5 = (Item)this.recipeItems[var4 + 1][var2];
					p_77583_1_.addRecipe(new ItemStack(var5), new object[] {this.recipePatterns[var4], '#', Items.stick, 'X', var3});
				}
			}

			p_77583_1_.addRecipe(new ItemStack(Items.bow, 1), new object[] {" #X", "# X", " #X", 'X', Items.string, '#', Items.stick});
			p_77583_1_.addRecipe(new ItemStack(Items.arrow, 4), new object[] {"X", "#", "Y", 'Y', Items.feather, 'X', Items.flint, '#', Items.stick});
		}
	}

}