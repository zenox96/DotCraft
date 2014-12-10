using System.Collections;

namespace DotCraftCore.Item.Crafting
{

	using InventoryCrafting = DotCraftCore.inventory.InventoryCrafting;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using World = DotCraftCore.World.World;

	public class ShapelessRecipes : IRecipe
	{
	/// <summary> Is the ItemStack that you get when craft the recipe.  </summary>
		private readonly ItemStack recipeOutput;

	/// <summary> Is a List of ItemStack that composes the recipe.  </summary>
		private readonly IList recipeItems;
		private const string __OBFID = "CL_00000094";

		public ShapelessRecipes(ItemStack p_i1918_1_, IList p_i1918_2_)
		{
			this.recipeOutput = p_i1918_1_;
			this.recipeItems = p_i1918_2_;
		}

		public virtual ItemStack RecipeOutput
		{
			get
			{
				return this.recipeOutput;
			}
		}

///    
///     <summary> * Used to check if a recipe matches current crafting inventory </summary>
///     
		public virtual bool matches(InventoryCrafting p_77569_1_, World p_77569_2_)
		{
			ArrayList var3 = new ArrayList(this.recipeItems);

			for (int var4 = 0; var4 < 3; ++var4)
			{
				for (int var5 = 0; var5 < 3; ++var5)
				{
					ItemStack var6 = p_77569_1_.getStackInRowAndColumn(var5, var4);

					if (var6 != null)
					{
						bool var7 = false;
						IEnumerator var8 = var3.GetEnumerator();

						while (var8.MoveNext())
						{
							ItemStack var9 = (ItemStack)var8.Current;

							if (var6.Item == var9.Item && (var9.ItemDamage == 32767 || var6.ItemDamage == var9.ItemDamage))
							{
								var7 = true;
								var3.Remove(var9);
								break;
							}
						}

						if (!var7)
						{
							return false;
						}
					}
				}
			}

			return var3.Count == 0;
		}

///    
///     <summary> * Returns an Item that is the result of this recipe </summary>
///     
		public virtual ItemStack getCraftingResult(InventoryCrafting p_77572_1_)
		{
			return this.recipeOutput.copy();
		}

///    
///     <summary> * Returns the size of the recipe area </summary>
///     
		public virtual int RecipeSize
		{
			get
			{
				return this.recipeItems.Count;
			}
		}
	}

}