namespace DotCraftCore.nItem.nCrafting
{

	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using Item = DotCraftCore.nItem.Item;
	using ItemStack = DotCraftCore.nItem.ItemStack;

	public class RecipesTools
	{
		private string[][] recipePatterns = new string[][] {{"XXX", " # ", " # "}, {"X", "#", "#"}, {"XX", "X#", " #"}, {"XX", " #", " #"}};
		private object[][] recipeItems;
		

		public RecipesTools()
		{
			this.recipeItems = new object[][] {{Blocks.planks, Blocks.cobblestone, Items.iron_ingot, Items.diamond, Items.gold_ingot}, {Items.wooden_pickaxe, Items.stone_pickaxe, Items.iron_pickaxe, Items.diamond_pickaxe, Items.golden_pickaxe}, {Items.wooden_shovel, Items.stone_shovel, Items.iron_shovel, Items.diamond_shovel, Items.golden_shovel}, {Items.wooden_axe, Items.stone_axe, Items.iron_axe, Items.diamond_axe, Items.golden_axe}, {Items.wooden_hoe, Items.stone_hoe, Items.iron_hoe, Items.diamond_hoe, Items.golden_hoe}};
		}

///    
///     <summary> * Adds the tool recipes to the CraftingManager. </summary>
///     
		public virtual void addRecipes(CraftingManager p_77586_1_)
		{
			for (int var2 = 0; var2 < this.recipeItems[0].Length; ++var2)
			{
				object var3 = this.recipeItems[0][var2];

				for (int var4 = 0; var4 < this.recipeItems.Length - 1; ++var4)
				{
					Item var5 = (Item)this.recipeItems[var4 + 1][var2];
					p_77586_1_.addRecipe(new ItemStack(var5), new object[] {this.recipePatterns[var4], '#', Items.stick, 'X', var3});
				}
			}

			p_77586_1_.addRecipe(new ItemStack(Items.shears), new object[] {" #", "# ", '#', Items.iron_ingot});
		}
	}

}