using System;
using System.Collections;

namespace DotCraftCore.Item.Crafting
{

	using Block = DotCraftCore.block.Block;
	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using InventoryCrafting = DotCraftCore.inventory.InventoryCrafting;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using World = DotCraftCore.World.World;

	public class CraftingManager
	{
	/// <summary> The static instance of this class  </summary>
		private static readonly CraftingManager instance = new CraftingManager();

	/// <summary> A list of all the recipes added  </summary>
		private IList recipes = new ArrayList();
		private const string __OBFID = "CL_00000090";

///    
///     <summary> * Returns the static instance of this class </summary>
///     
		public static CraftingManager Instance
		{
			get
			{
				return instance;
			}
		}

		private CraftingManager()
		{
			(new RecipesTools()).addRecipes(this);
			(new RecipesWeapons()).addRecipes(this);
			(new RecipesIngots()).addRecipes(this);
			(new RecipesFood()).addRecipes(this);
			(new RecipesCrafting()).addRecipes(this);
			(new RecipesArmor()).addRecipes(this);
			(new RecipesDyes()).addRecipes(this);
			this.recipes.Add(new RecipesArmorDyes());
			this.recipes.Add(new RecipeBookCloning());
			this.recipes.Add(new RecipesMapCloning());
			this.recipes.Add(new RecipesMapExtending());
			this.recipes.Add(new RecipeFireworks());
			this.addRecipe(new ItemStack(Items.paper, 3), new object[] {"###", '#', Items.reeds});
			this.addShapelessRecipe(new ItemStack(Items.book, 1), new object[] {Items.paper, Items.paper, Items.paper, Items.leather});
			this.addShapelessRecipe(new ItemStack(Items.writable_book, 1), new object[] {Items.book, new ItemStack(Items.dye, 1, 0), Items.feather});
			this.addRecipe(new ItemStack(Blocks.fence, 2), new object[] {"###", "###", '#', Items.stick});
			this.addRecipe(new ItemStack(Blocks.cobblestone_wall, 6, 0), new object[] {"###", "###", '#', Blocks.cobblestone});
			this.addRecipe(new ItemStack(Blocks.cobblestone_wall, 6, 1), new object[] {"###", "###", '#', Blocks.mossy_cobblestone});
			this.addRecipe(new ItemStack(Blocks.nether_brick_fence, 6), new object[] {"###", "###", '#', Blocks.nether_brick});
			this.addRecipe(new ItemStack(Blocks.fence_gate, 1), new object[] {"#W#", "#W#", '#', Items.stick, 'W', Blocks.planks});
			this.addRecipe(new ItemStack(Blocks.jukebox, 1), new object[] {"###", "#X#", "###", '#', Blocks.planks, 'X', Items.diamond});
			this.addRecipe(new ItemStack(Items.lead, 2), new object[] {"~~ ", "~O ", "  ~", '~', Items.string, 'O', Items.slime_ball});
			this.addRecipe(new ItemStack(Blocks.noteblock, 1), new object[] {"###", "#X#", "###", '#', Blocks.planks, 'X', Items.redstone});
			this.addRecipe(new ItemStack(Blocks.bookshelf, 1), new object[] {"###", "XXX", "###", '#', Blocks.planks, 'X', Items.book});
			this.addRecipe(new ItemStack(Blocks.snow, 1), new object[] {"##", "##", '#', Items.snowball});
			this.addRecipe(new ItemStack(Blocks.snow_layer, 6), new object[] {"###", '#', Blocks.snow});
			this.addRecipe(new ItemStack(Blocks.clay, 1), new object[] {"##", "##", '#', Items.clay_ball});
			this.addRecipe(new ItemStack(Blocks.brick_block, 1), new object[] {"##", "##", '#', Items.brick});
			this.addRecipe(new ItemStack(Blocks.glowstone, 1), new object[] {"##", "##", '#', Items.glowstone_dust});
			this.addRecipe(new ItemStack(Blocks.quartz_block, 1), new object[] {"##", "##", '#', Items.quartz});
			this.addRecipe(new ItemStack(Blocks.wool, 1), new object[] {"##", "##", '#', Items.string});
			this.addRecipe(new ItemStack(Blocks.tnt, 1), new object[] {"X#X", "#X#", "X#X", 'X', Items.gunpowder, '#', Blocks.sand});
			this.addRecipe(new ItemStack(Blocks.stone_slab, 6, 3), new object[] {"###", '#', Blocks.cobblestone});
			this.addRecipe(new ItemStack(Blocks.stone_slab, 6, 0), new object[] {"###", '#', Blocks.stone});
			this.addRecipe(new ItemStack(Blocks.stone_slab, 6, 1), new object[] {"###", '#', Blocks.sandstone});
			this.addRecipe(new ItemStack(Blocks.stone_slab, 6, 4), new object[] {"###", '#', Blocks.brick_block});
			this.addRecipe(new ItemStack(Blocks.stone_slab, 6, 5), new object[] {"###", '#', Blocks.stonebrick});
			this.addRecipe(new ItemStack(Blocks.stone_slab, 6, 6), new object[] {"###", '#', Blocks.nether_brick});
			this.addRecipe(new ItemStack(Blocks.stone_slab, 6, 7), new object[] {"###", '#', Blocks.quartz_block});
			this.addRecipe(new ItemStack(Blocks.wooden_slab, 6, 0), new object[] {"###", '#', new ItemStack(Blocks.planks, 1, 0)});
			this.addRecipe(new ItemStack(Blocks.wooden_slab, 6, 2), new object[] {"###", '#', new ItemStack(Blocks.planks, 1, 2)});
			this.addRecipe(new ItemStack(Blocks.wooden_slab, 6, 1), new object[] {"###", '#', new ItemStack(Blocks.planks, 1, 1)});
			this.addRecipe(new ItemStack(Blocks.wooden_slab, 6, 3), new object[] {"###", '#', new ItemStack(Blocks.planks, 1, 3)});
			this.addRecipe(new ItemStack(Blocks.wooden_slab, 6, 4), new object[] {"###", '#', new ItemStack(Blocks.planks, 1, 4)});
			this.addRecipe(new ItemStack(Blocks.wooden_slab, 6, 5), new object[] {"###", '#', new ItemStack(Blocks.planks, 1, 5)});
			this.addRecipe(new ItemStack(Blocks.ladder, 3), new object[] {"# #", "###", "# #", '#', Items.stick});
			this.addRecipe(new ItemStack(Items.wooden_door, 1), new object[] {"##", "##", "##", '#', Blocks.planks});
			this.addRecipe(new ItemStack(Blocks.trapdoor, 2), new object[] {"###", "###", '#', Blocks.planks});
			this.addRecipe(new ItemStack(Items.iron_door, 1), new object[] {"##", "##", "##", '#', Items.iron_ingot});
			this.addRecipe(new ItemStack(Items.sign, 3), new object[] {"###", "###", " X ", '#', Blocks.planks, 'X', Items.stick});
			this.addRecipe(new ItemStack(Items.cake, 1), new object[] {"AAA", "BEB", "CCC", 'A', Items.milk_bucket, 'B', Items.sugar, 'C', Items.wheat, 'E', Items.egg});
			this.addRecipe(new ItemStack(Items.sugar, 1), new object[] {"#", '#', Items.reeds});
			this.addRecipe(new ItemStack(Blocks.planks, 4, 0), new object[] {"#", '#', new ItemStack(Blocks.log, 1, 0)});
			this.addRecipe(new ItemStack(Blocks.planks, 4, 1), new object[] {"#", '#', new ItemStack(Blocks.log, 1, 1)});
			this.addRecipe(new ItemStack(Blocks.planks, 4, 2), new object[] {"#", '#', new ItemStack(Blocks.log, 1, 2)});
			this.addRecipe(new ItemStack(Blocks.planks, 4, 3), new object[] {"#", '#', new ItemStack(Blocks.log, 1, 3)});
			this.addRecipe(new ItemStack(Blocks.planks, 4, 4), new object[] {"#", '#', new ItemStack(Blocks.log2, 1, 0)});
			this.addRecipe(new ItemStack(Blocks.planks, 4, 5), new object[] {"#", '#', new ItemStack(Blocks.log2, 1, 1)});
			this.addRecipe(new ItemStack(Items.stick, 4), new object[] {"#", "#", '#', Blocks.planks});
			this.addRecipe(new ItemStack(Blocks.torch, 4), new object[] {"X", "#", 'X', Items.coal, '#', Items.stick});
			this.addRecipe(new ItemStack(Blocks.torch, 4), new object[] {"X", "#", 'X', new ItemStack(Items.coal, 1, 1), '#', Items.stick});
			this.addRecipe(new ItemStack(Items.bowl, 4), new object[] {"# #", " # ", '#', Blocks.planks});
			this.addRecipe(new ItemStack(Items.glass_bottle, 3), new object[] {"# #", " # ", '#', Blocks.glass});
			this.addRecipe(new ItemStack(Blocks.rail, 16), new object[] {"X X", "X#X", "X X", 'X', Items.iron_ingot, '#', Items.stick});
			this.addRecipe(new ItemStack(Blocks.golden_rail, 6), new object[] {"X X", "X#X", "XRX", 'X', Items.gold_ingot, 'R', Items.redstone, '#', Items.stick});
			this.addRecipe(new ItemStack(Blocks.activator_rail, 6), new object[] {"XSX", "X#X", "XSX", 'X', Items.iron_ingot, '#', Blocks.redstone_torch, 'S', Items.stick});
			this.addRecipe(new ItemStack(Blocks.detector_rail, 6), new object[] {"X X", "X#X", "XRX", 'X', Items.iron_ingot, 'R', Items.redstone, '#', Blocks.stone_pressure_plate});
			this.addRecipe(new ItemStack(Items.minecart, 1), new object[] {"# #", "###", '#', Items.iron_ingot});
			this.addRecipe(new ItemStack(Items.cauldron, 1), new object[] {"# #", "# #", "###", '#', Items.iron_ingot});
			this.addRecipe(new ItemStack(Items.brewing_stand, 1), new object[] {" B ", "###", '#', Blocks.cobblestone, 'B', Items.blaze_rod});
			this.addRecipe(new ItemStack(Blocks.lit_pumpkin, 1), new object[] {"A", "B", 'A', Blocks.pumpkin, 'B', Blocks.torch});
			this.addRecipe(new ItemStack(Items.chest_minecart, 1), new object[] {"A", "B", 'A', Blocks.chest, 'B', Items.minecart});
			this.addRecipe(new ItemStack(Items.furnace_minecart, 1), new object[] {"A", "B", 'A', Blocks.furnace, 'B', Items.minecart});
			this.addRecipe(new ItemStack(Items.tnt_minecart, 1), new object[] {"A", "B", 'A', Blocks.tnt, 'B', Items.minecart});
			this.addRecipe(new ItemStack(Items.hopper_minecart, 1), new object[] {"A", "B", 'A', Blocks.hopper, 'B', Items.minecart});
			this.addRecipe(new ItemStack(Items.boat, 1), new object[] {"# #", "###", '#', Blocks.planks});
			this.addRecipe(new ItemStack(Items.bucket, 1), new object[] {"# #", " # ", '#', Items.iron_ingot});
			this.addRecipe(new ItemStack(Items.flower_pot, 1), new object[] {"# #", " # ", '#', Items.brick});
			this.addShapelessRecipe(new ItemStack(Items.flint_and_steel, 1), new object[] {new ItemStack(Items.iron_ingot, 1), new ItemStack(Items.flint, 1)});
			this.addRecipe(new ItemStack(Items.bread, 1), new object[] {"###", '#', Items.wheat});
			this.addRecipe(new ItemStack(Blocks.oak_stairs, 4), new object[] {"#  ", "## ", "###", '#', new ItemStack(Blocks.planks, 1, 0)});
			this.addRecipe(new ItemStack(Blocks.birch_stairs, 4), new object[] {"#  ", "## ", "###", '#', new ItemStack(Blocks.planks, 1, 2)});
			this.addRecipe(new ItemStack(Blocks.spruce_stairs, 4), new object[] {"#  ", "## ", "###", '#', new ItemStack(Blocks.planks, 1, 1)});
			this.addRecipe(new ItemStack(Blocks.jungle_stairs, 4), new object[] {"#  ", "## ", "###", '#', new ItemStack(Blocks.planks, 1, 3)});
			this.addRecipe(new ItemStack(Blocks.acacia_stairs, 4), new object[] {"#  ", "## ", "###", '#', new ItemStack(Blocks.planks, 1, 4)});
			this.addRecipe(new ItemStack(Blocks.dark_oak_stairs, 4), new object[] {"#  ", "## ", "###", '#', new ItemStack(Blocks.planks, 1, 5)});
			this.addRecipe(new ItemStack(Items.fishing_rod, 1), new object[] {"  #", " #X", "# X", '#', Items.stick, 'X', Items.string});
			this.addRecipe(new ItemStack(Items.carrot_on_a_stick, 1), new object[] {"# ", " X", '#', Items.fishing_rod, 'X', Items.carrot}).func_92100_c();
			this.addRecipe(new ItemStack(Blocks.stone_stairs, 4), new object[] {"#  ", "## ", "###", '#', Blocks.cobblestone});
			this.addRecipe(new ItemStack(Blocks.brick_stairs, 4), new object[] {"#  ", "## ", "###", '#', Blocks.brick_block});
			this.addRecipe(new ItemStack(Blocks.stone_brick_stairs, 4), new object[] {"#  ", "## ", "###", '#', Blocks.stonebrick});
			this.addRecipe(new ItemStack(Blocks.nether_brick_stairs, 4), new object[] {"#  ", "## ", "###", '#', Blocks.nether_brick});
			this.addRecipe(new ItemStack(Blocks.sandstone_stairs, 4), new object[] {"#  ", "## ", "###", '#', Blocks.sandstone});
			this.addRecipe(new ItemStack(Blocks.quartz_stairs, 4), new object[] {"#  ", "## ", "###", '#', Blocks.quartz_block});
			this.addRecipe(new ItemStack(Items.painting, 1), new object[] {"###", "#X#", "###", '#', Items.stick, 'X', Blocks.wool});
			this.addRecipe(new ItemStack(Items.item_frame, 1), new object[] {"###", "#X#", "###", '#', Items.stick, 'X', Items.leather});
			this.addRecipe(new ItemStack(Items.golden_apple, 1, 0), new object[] {"###", "#X#", "###", '#', Items.gold_ingot, 'X', Items.apple});
			this.addRecipe(new ItemStack(Items.golden_apple, 1, 1), new object[] {"###", "#X#", "###", '#', Blocks.gold_block, 'X', Items.apple});
			this.addRecipe(new ItemStack(Items.golden_carrot, 1, 0), new object[] {"###", "#X#", "###", '#', Items.gold_nugget, 'X', Items.carrot});
			this.addRecipe(new ItemStack(Items.speckled_melon, 1), new object[] {"###", "#X#", "###", '#', Items.gold_nugget, 'X', Items.melon});
			this.addRecipe(new ItemStack(Blocks.lever, 1), new object[] {"X", "#", '#', Blocks.cobblestone, 'X', Items.stick});
			this.addRecipe(new ItemStack(Blocks.tripwire_hook, 2), new object[] {"I", "S", "#", '#', Blocks.planks, 'S', Items.stick, 'I', Items.iron_ingot});
			this.addRecipe(new ItemStack(Blocks.redstone_torch, 1), new object[] {"X", "#", '#', Items.stick, 'X', Items.redstone});
			this.addRecipe(new ItemStack(Items.repeater, 1), new object[] {"#X#", "III", '#', Blocks.redstone_torch, 'X', Items.redstone, 'I', Blocks.stone});
			this.addRecipe(new ItemStack(Items.comparator, 1), new object[] {" # ", "#X#", "III", '#', Blocks.redstone_torch, 'X', Items.quartz, 'I', Blocks.stone});
			this.addRecipe(new ItemStack(Items.clock, 1), new object[] {" # ", "#X#", " # ", '#', Items.gold_ingot, 'X', Items.redstone});
			this.addRecipe(new ItemStack(Items.compass, 1), new object[] {" # ", "#X#", " # ", '#', Items.iron_ingot, 'X', Items.redstone});
			this.addRecipe(new ItemStack(Items.map, 1), new object[] {"###", "#X#", "###", '#', Items.paper, 'X', Items.compass});
			this.addRecipe(new ItemStack(Blocks.stone_button, 1), new object[] {"#", '#', Blocks.stone});
			this.addRecipe(new ItemStack(Blocks.wooden_button, 1), new object[] {"#", '#', Blocks.planks});
			this.addRecipe(new ItemStack(Blocks.stone_pressure_plate, 1), new object[] {"##", '#', Blocks.stone});
			this.addRecipe(new ItemStack(Blocks.wooden_pressure_plate, 1), new object[] {"##", '#', Blocks.planks});
			this.addRecipe(new ItemStack(Blocks.heavy_weighted_pressure_plate, 1), new object[] {"##", '#', Items.iron_ingot});
			this.addRecipe(new ItemStack(Blocks.light_weighted_pressure_plate, 1), new object[] {"##", '#', Items.gold_ingot});
			this.addRecipe(new ItemStack(Blocks.dispenser, 1), new object[] {"###", "#X#", "#R#", '#', Blocks.cobblestone, 'X', Items.bow, 'R', Items.redstone});
			this.addRecipe(new ItemStack(Blocks.dropper, 1), new object[] {"###", "# #", "#R#", '#', Blocks.cobblestone, 'R', Items.redstone});
			this.addRecipe(new ItemStack(Blocks.piston, 1), new object[] {"TTT", "#X#", "#R#", '#', Blocks.cobblestone, 'X', Items.iron_ingot, 'R', Items.redstone, 'T', Blocks.planks});
			this.addRecipe(new ItemStack(Blocks.sticky_piston, 1), new object[] {"S", "P", 'S', Items.slime_ball, 'P', Blocks.piston});
			this.addRecipe(new ItemStack(Items.bed, 1), new object[] {"###", "XXX", '#', Blocks.wool, 'X', Blocks.planks});
			this.addRecipe(new ItemStack(Blocks.enchanting_table, 1), new object[] {" B ", "D#D", "###", '#', Blocks.obsidian, 'B', Items.book, 'D', Items.diamond});
			this.addRecipe(new ItemStack(Blocks.anvil, 1), new object[] {"III", " i ", "iii", 'I', Blocks.iron_block, 'i', Items.iron_ingot});
			this.addShapelessRecipe(new ItemStack(Items.ender_eye, 1), new object[] {Items.ender_pearl, Items.blaze_powder});
			this.addShapelessRecipe(new ItemStack(Items.fire_charge, 3), new object[] {Items.gunpowder, Items.blaze_powder, Items.coal});
			this.addShapelessRecipe(new ItemStack(Items.fire_charge, 3), new object[] {Items.gunpowder, Items.blaze_powder, new ItemStack(Items.coal, 1, 1)});
			this.addRecipe(new ItemStack(Blocks.daylight_detector), new object[] {"GGG", "QQQ", "WWW", 'G', Blocks.glass, 'Q', Items.quartz, 'W', Blocks.wooden_slab});
			this.addRecipe(new ItemStack(Blocks.hopper), new object[] {"I I", "ICI", " I ", 'I', Items.iron_ingot, 'C', Blocks.chest});
			Collections.sort(this.recipes, new IComparer() { private static final string __OBFID = "CL_00000091"; public int compare(IRecipe p_compare_1_, IRecipe p_compare_2_) { return p_compare_1_ is ShapelessRecipes && p_compare_2_ is ShapedRecipes ? 1 : (p_compare_2_ is ShapelessRecipes && p_compare_1_ is ShapedRecipes ? -1 : (p_compare_2_.RecipeSize < p_compare_1_.RecipeSize ? -1 : (p_compare_2_.RecipeSize > p_compare_1_.RecipeSize ? 1 : 0))); } public int compare(object p_compare_1_, object p_compare_2_) { return this.compare((IRecipe)p_compare_1_, (IRecipe)p_compare_2_); } });
		}

		internal virtual ShapedRecipes addRecipe(ItemStack p_92103_1_, params object[] p_92103_2_)
		{
			string var3 = "";
			int var4 = 0;
			int var5 = 0;
			int var6 = 0;

			if (p_92103_2_[var4] is string[])
			{
				string[] var7 = (string[])((string[])p_92103_2_[var4++]);

				for (int var8 = 0; var8 < var7.Length; ++var8)
				{
					string var9 = var7[var8];
					++var6;
					var5 = var9.Length;
					var3 = var3 + var9;
				}
			}
			else
			{
				while (p_92103_2_[var4] is string)
				{
					string var11 = (string)p_92103_2_[var4++];
					++var6;
					var5 = var11.Length;
					var3 = var3 + var11;
				}
			}

			Hashtable var12;

			for (var12 = new Hashtable(); var4 < p_92103_2_.length; var4 += 2)
			{
				char? var13 = (char?)p_92103_2_[var4];
				ItemStack var15 = null;

				if (p_92103_2_[var4 + 1] is Item)
				{
					var15 = new ItemStack((Item)p_92103_2_[var4 + 1]);
				}
				else if (p_92103_2_[var4 + 1] is Block)
				{
					var15 = new ItemStack((Block)p_92103_2_[var4 + 1], 1, 32767);
				}
				else if (p_92103_2_[var4 + 1] is ItemStack)
				{
					var15 = (ItemStack)p_92103_2_[var4 + 1];
				}

				var12.Add(var13, var15);
			}

			ItemStack[] var14 = new ItemStack[var5 * var6];

			for (int var16 = 0; var16 < var5 * var6; ++var16)
			{
				char var10 = var3[var16];

				if (var12.ContainsKey(Convert.ToChar(var10)))
				{
					var14[var16] = ((ItemStack)var12[Convert.ToChar(var10)]).copy();
				}
				else
				{
					var14[var16] = null;
				}
			}

			ShapedRecipes var17 = new ShapedRecipes(var5, var6, var14, p_92103_1_);
			this.recipes.Add(var17);
			return var17;
		}

		internal virtual void addShapelessRecipe(ItemStack p_77596_1_, params object[] p_77596_2_)
		{
			ArrayList var3 = new ArrayList();
			object[] var4 = p_77596_2_;
			int var5 = p_77596_2_.length;

			for (int var6 = 0; var6 < var5; ++var6)
			{
				object var7 = var4[var6];

				if (var7 is ItemStack)
				{
					var3.Add(((ItemStack)var7).copy());
				}
				else if (var7 is Item)
				{
					var3.Add(new ItemStack((Item)var7));
				}
				else
				{
					if (!(var7 is Block))
					{
						throw new Exception("Invalid shapeless recipy!");
					}

					var3.Add(new ItemStack((Block)var7));
				}
			}

			this.recipes.Add(new ShapelessRecipes(p_77596_1_, var3));
		}

		public virtual ItemStack findMatchingRecipe(InventoryCrafting p_82787_1_, World p_82787_2_)
		{
			int var3 = 0;
			ItemStack var4 = null;
			ItemStack var5 = null;
			int var6;

			for (var6 = 0; var6 < p_82787_1_.SizeInventory; ++var6)
			{
				ItemStack var7 = p_82787_1_.getStackInSlot(var6);

				if (var7 != null)
				{
					if (var3 == 0)
					{
						var4 = var7;
					}

					if (var3 == 1)
					{
						var5 = var7;
					}

					++var3;
				}
			}

			if (var3 == 2 && var4.Item == var5.Item && var4.stackSize == 1 && var5.stackSize == 1 && var4.Item.Damageable)
			{
				Item var11 = var4.Item;
				int var13 = var11.MaxDamage - var4.ItemDamageForDisplay;
				int var8 = var11.MaxDamage - var5.ItemDamageForDisplay;
				int var9 = var13 + var8 + var11.MaxDamage * 5 / 100;
				int var10 = var11.MaxDamage - var9;

				if (var10 < 0)
				{
					var10 = 0;
				}

				return new ItemStack(var4.Item, 1, var10);
			}
			else
			{
				for (var6 = 0; var6 < this.recipes.Count; ++var6)
				{
					IRecipe var12 = (IRecipe)this.recipes.get(var6);

					if (var12.matches(p_82787_1_, p_82787_2_))
					{
						return var12.getCraftingResult(p_82787_1_);
					}
				}

				return null;
			}
		}

///    
///     <summary> * returns the List<> of all recipes </summary>
///     
		public virtual IList RecipeList
		{
			get
			{
				return this.recipes;
			}
		}
	}

}