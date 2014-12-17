namespace DotCraftCore.nItem.nCrafting
{

	using InventoryCrafting = DotCraftCore.inventory.InventoryCrafting;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using World = DotCraftCore.nWorld.World;

	public class ShapedRecipes : IRecipe
	{
	/// <summary> How many horizontal slots this recipe is wide.  </summary>
		private int recipeWidth;

	/// <summary> How many vertical slots this recipe uses.  </summary>
		private int recipeHeight;

	/// <summary> Is a array of ItemStack that composes the recipe.  </summary>
		private ItemStack[] recipeItems;

	/// <summary> Is the ItemStack that you get when craft the recipe.  </summary>
		private ItemStack recipeOutput;
		private bool field_92101_f;
		

		public ShapedRecipes(int p_i1917_1_, int p_i1917_2_, ItemStack[] p_i1917_3_, ItemStack p_i1917_4_)
		{
			this.recipeWidth = p_i1917_1_;
			this.recipeHeight = p_i1917_2_;
			this.recipeItems = p_i1917_3_;
			this.recipeOutput = p_i1917_4_;
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
			for (int var3 = 0; var3 <= 3 - this.recipeWidth; ++var3)
			{
				for (int var4 = 0; var4 <= 3 - this.recipeHeight; ++var4)
				{
					if (this.checkMatch(p_77569_1_, var3, var4, true))
					{
						return true;
					}

					if (this.checkMatch(p_77569_1_, var3, var4, false))
					{
						return true;
					}
				}
			}

			return false;
		}

///    
///     <summary> * Checks if the region of a crafting inventory is match for the recipe. </summary>
///     
		private bool checkMatch(InventoryCrafting p_77573_1_, int p_77573_2_, int p_77573_3_, bool p_77573_4_)
		{
			for (int var5 = 0; var5 < 3; ++var5)
			{
				for (int var6 = 0; var6 < 3; ++var6)
				{
					int var7 = var5 - p_77573_2_;
					int var8 = var6 - p_77573_3_;
					ItemStack var9 = null;

					if (var7 >= 0 && var8 >= 0 && var7 < this.recipeWidth && var8 < this.recipeHeight)
					{
						if (p_77573_4_)
						{
							var9 = this.recipeItems[this.recipeWidth - var7 - 1 + var8 * this.recipeWidth];
						}
						else
						{
							var9 = this.recipeItems[var7 + var8 * this.recipeWidth];
						}
					}

					ItemStack var10 = p_77573_1_.getStackInRowAndColumn(var5, var6);

					if (var10 != null || var9 != null)
					{
						if (var10 == null && var9 != null || var10 != null && var9 == null)
						{
							return false;
						}

						if (var9.Item != var10.Item)
						{
							return false;
						}

						if (var9.ItemDamage != 32767 && var9.ItemDamage != var10.ItemDamage)
						{
							return false;
						}
					}
				}
			}

			return true;
		}

///    
///     <summary> * Returns an Item that is the result of this recipe </summary>
///     
		public virtual ItemStack getCraftingResult(InventoryCrafting p_77572_1_)
		{
			ItemStack var2 = this.RecipeOutput.copy();

			if (this.field_92101_f)
			{
				for (int var3 = 0; var3 < p_77572_1_.SizeInventory; ++var3)
				{
					ItemStack var4 = p_77572_1_.getStackInSlot(var3);

					if (var4 != null && var4.hasTagCompound())
					{
						var2.TagCompound = (NBTTagCompound)var4.stackTagCompound.copy();
					}
				}
			}

			return var2;
		}

///    
///     <summary> * Returns the size of the recipe area </summary>
///     
		public virtual int RecipeSize
		{
			get
			{
				return this.recipeWidth * this.recipeHeight;
			}
		}

		public virtual ShapedRecipes func_92100_c()
		{
			this.field_92101_f = true;
			return this;
		}
	}

}