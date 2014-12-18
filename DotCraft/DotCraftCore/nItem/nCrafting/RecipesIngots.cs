namespace DotCraftCore.nItem.nCrafting
{

	using Block = DotCraftCore.nBlock.Block;
	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using ItemStack = DotCraftCore.nItem.ItemStack;

	public class RecipesIngots
	{
		private object[][] recipeItems;
		

		public RecipesIngots()
		{
			this.recipeItems = new object[][] {{Blocks.gold_block, new ItemStack(Items.gold_ingot, 9)}, {Blocks.iron_block, new ItemStack(Items.iron_ingot, 9)}, {Blocks.diamond_block, new ItemStack(Items.diamond, 9)}, {Blocks.emerald_block, new ItemStack(Items.emerald, 9)}, {Blocks.lapis_block, new ItemStack(Items.dye, 9, 4)}, {Blocks.redstone_block, new ItemStack(Items.redstone, 9)}, {Blocks.coal_block, new ItemStack(Items.coal, 9, 0)}, {Blocks.hay_block, new ItemStack(Items.wheat, 9)}};
		}

///    
///     <summary> * Adds the ingot recipes to the CraftingManager. </summary>
///     
		public virtual void addRecipes(CraftingManager p_77590_1_)
		{
			for (int var2 = 0; var2 < this.recipeItems.Length; ++var2)
			{
				Block var3 = (Block)this.recipeItems[var2][0];
				ItemStack var4 = (ItemStack)this.recipeItems[var2][1];
				p_77590_1_.addRecipe(new ItemStack(var3), new object[] {"###", "###", "###", '#', var4});
				p_77590_1_.addRecipe(var4, new object[] {"#", '#', var3});
			}

			p_77590_1_.addRecipe(new ItemStack(Items.gold_ingot), new object[] {"###", "###", "###", '#', Items.gold_nugget});
			p_77590_1_.addRecipe(new ItemStack(Items.gold_nugget, 9), new object[] {"#", '#', Items.gold_ingot});
		}
	}

}