namespace DotCraftCore.Item.Crafting
{

	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using ItemStack = DotCraftCore.Item.ItemStack;

	public class RecipesFood
	{
		

///    
///     <summary> * Adds the food recipes to the CraftingManager. </summary>
///     
		public virtual void addRecipes(CraftingManager p_77608_1_)
		{
			p_77608_1_.addShapelessRecipe(new ItemStack(Items.mushroom_stew), new object[] {Blocks.brown_mushroom, Blocks.red_mushroom, Items.bowl});
			p_77608_1_.addRecipe(new ItemStack(Items.cookie, 8), new object[] {"#X#", 'X', new ItemStack(Items.dye, 1, 3), '#', Items.wheat});
			p_77608_1_.addRecipe(new ItemStack(Blocks.melon_block), new object[] {"MMM", "MMM", "MMM", 'M', Items.melon});
			p_77608_1_.addRecipe(new ItemStack(Items.melon_seeds), new object[] {"M", 'M', Items.melon});
			p_77608_1_.addRecipe(new ItemStack(Items.pumpkin_seeds, 4), new object[] {"M", 'M', Blocks.pumpkin});
			p_77608_1_.addShapelessRecipe(new ItemStack(Items.pumpkin_pie), new object[] {Blocks.pumpkin, Items.sugar, Items.egg});
			p_77608_1_.addShapelessRecipe(new ItemStack(Items.fermented_spider_eye), new object[] {Items.spider_eye, Blocks.brown_mushroom, Items.sugar});
			p_77608_1_.addShapelessRecipe(new ItemStack(Items.blaze_powder, 2), new object[] {Items.blaze_rod});
			p_77608_1_.addShapelessRecipe(new ItemStack(Items.magma_cream), new object[] {Items.blaze_powder, Items.slime_ball});
		}
	}

}