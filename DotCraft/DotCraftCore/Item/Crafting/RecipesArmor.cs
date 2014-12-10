namespace DotCraftCore.Item.Crafting
{

	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;

	public class RecipesArmor
	{
		private string[][] recipePatterns = new string[][] {{"XXX", "X X"}, {"X X", "XXX", "XXX"}, {"XXX", "X X", "X X"}, {"X X", "X X"}};
		private object[][] recipeItems;
		private const string __OBFID = "CL_00000080";

		public RecipesArmor()
		{
			this.recipeItems = new object[][] {{Items.leather, Blocks.fire, Items.iron_ingot, Items.diamond, Items.gold_ingot}, {Items.leather_helmet, Items.chainmail_helmet, Items.iron_helmet, Items.diamond_helmet, Items.golden_helmet}, {Items.leather_chestplate, Items.chainmail_chestplate, Items.iron_chestplate, Items.diamond_chestplate, Items.golden_chestplate}, {Items.leather_leggings, Items.chainmail_leggings, Items.iron_leggings, Items.diamond_leggings, Items.golden_leggings}, {Items.leather_boots, Items.chainmail_boots, Items.iron_boots, Items.diamond_boots, Items.golden_boots}};
		}

///    
///     <summary> * Adds the armor recipes to the CraftingManager. </summary>
///     
		public virtual void addRecipes(CraftingManager p_77609_1_)
		{
			for (int var2 = 0; var2 < this.recipeItems[0].Length; ++var2)
			{
				object var3 = this.recipeItems[0][var2];

				for (int var4 = 0; var4 < this.recipeItems.Length - 1; ++var4)
				{
					Item var5 = (Item)this.recipeItems[var4 + 1][var2];
					p_77609_1_.addRecipe(new ItemStack(var5), new object[] {this.recipePatterns[var4], 'X', var3});
				}
			}
		}
	}

}