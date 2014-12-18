namespace DotCraftCore.nItem.nCrafting
{

	using Items = DotCraftCore.init.Items;
	using InventoryCrafting = DotCraftCore.inventory.InventoryCrafting;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using World = DotCraftCore.nWorld.World;

	public class RecipesMapCloning : IRecipe
	{
		

///    
///     <summary> * Used to check if a recipe matches current crafting inventory </summary>
///     
		public virtual bool matches(InventoryCrafting p_77569_1_, World p_77569_2_)
		{
			int var3 = 0;
			ItemStack var4 = null;

			for (int var5 = 0; var5 < p_77569_1_.SizeInventory; ++var5)
			{
				ItemStack var6 = p_77569_1_.getStackInSlot(var5);

				if (var6 != null)
				{
					if (var6.Item == Items.filled_map)
					{
						if (var4 != null)
						{
							return false;
						}

						var4 = var6;
					}
					else
					{
						if (var6.Item != Items.map)
						{
							return false;
						}

						++var3;
					}
				}
			}

			return var4 != null && var3 > 0;
		}

///    
///     <summary> * Returns an Item that is the result of this recipe </summary>
///     
		public virtual ItemStack getCraftingResult(InventoryCrafting p_77572_1_)
		{
			int var2 = 0;
			ItemStack var3 = null;

			for (int var4 = 0; var4 < p_77572_1_.SizeInventory; ++var4)
			{
				ItemStack var5 = p_77572_1_.getStackInSlot(var4);

				if (var5 != null)
				{
					if (var5.Item == Items.filled_map)
					{
						if (var3 != null)
						{
							return null;
						}

						var3 = var5;
					}
					else
					{
						if (var5.Item != Items.map)
						{
							return null;
						}

						++var2;
					}
				}
			}

			if (var3 != null && var2 >= 1)
			{
				ItemStack var6 = new ItemStack(Items.filled_map, var2 + 1, var3.ItemDamage);

				if (var3.hasDisplayName())
				{
					var6.StackDisplayName = var3.DisplayName;
				}

				return var6;
			}
			else
			{
				return null;
			}
		}

///    
///     <summary> * Returns the size of the recipe area </summary>
///     
		public virtual int RecipeSize
		{
			get
			{
				return 9;
			}
		}

		public virtual ItemStack RecipeOutput
		{
			get
			{
				return null;
			}
		}
	}

}