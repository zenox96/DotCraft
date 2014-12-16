namespace DotCraftCore.Item.Crafting
{

	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using ItemStack = DotCraftCore.Item.ItemStack;

	public class RecipesCrafting
	{
		

///    
///     <summary> * Adds the crafting recipes to the CraftingManager. </summary>
///     
		public virtual void addRecipes(CraftingManager p_77589_1_)
		{
			p_77589_1_.addRecipe(new ItemStack(Blocks.chest), new object[] {"###", "# #", "###", '#', Blocks.planks});
			p_77589_1_.addRecipe(new ItemStack(Blocks.trapped_chest), new object[] {"#-", '#', Blocks.chest, '-', Blocks.tripwire_hook});
			p_77589_1_.addRecipe(new ItemStack(Blocks.ender_chest), new object[] {"###", "#E#", "###", '#', Blocks.obsidian, 'E', Items.ender_eye});
			p_77589_1_.addRecipe(new ItemStack(Blocks.furnace), new object[] {"###", "# #", "###", '#', Blocks.cobblestone});
			p_77589_1_.addRecipe(new ItemStack(Blocks.crafting_table), new object[] {"##", "##", '#', Blocks.planks});
			p_77589_1_.addRecipe(new ItemStack(Blocks.sandstone), new object[] {"##", "##", '#', new ItemStack(Blocks.sand, 1, 0)});
			p_77589_1_.addRecipe(new ItemStack(Blocks.sandstone, 4, 2), new object[] {"##", "##", '#', Blocks.sandstone});
			p_77589_1_.addRecipe(new ItemStack(Blocks.sandstone, 1, 1), new object[] {"#", "#", '#', new ItemStack(Blocks.stone_slab, 1, 1)});
			p_77589_1_.addRecipe(new ItemStack(Blocks.quartz_block, 1, 1), new object[] {"#", "#", '#', new ItemStack(Blocks.stone_slab, 1, 7)});
			p_77589_1_.addRecipe(new ItemStack(Blocks.quartz_block, 2, 2), new object[] {"#", "#", '#', new ItemStack(Blocks.quartz_block, 1, 0)});
			p_77589_1_.addRecipe(new ItemStack(Blocks.stonebrick, 4), new object[] {"##", "##", '#', Blocks.stone});
			p_77589_1_.addRecipe(new ItemStack(Blocks.iron_bars, 16), new object[] {"###", "###", '#', Items.iron_ingot});
			p_77589_1_.addRecipe(new ItemStack(Blocks.glass_pane, 16), new object[] {"###", "###", '#', Blocks.glass});
			p_77589_1_.addRecipe(new ItemStack(Blocks.redstone_lamp, 1), new object[] {" R ", "RGR", " R ", 'R', Items.redstone, 'G', Blocks.glowstone});
			p_77589_1_.addRecipe(new ItemStack(Blocks.beacon, 1), new object[] {"GGG", "GSG", "OOO", 'G', Blocks.glass, 'S', Items.nether_star, 'O', Blocks.obsidian});
			p_77589_1_.addRecipe(new ItemStack(Blocks.nether_brick, 1), new object[] {"NN", "NN", 'N', Items.netherbrick});
		}
	}

}