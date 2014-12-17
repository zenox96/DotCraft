namespace DotCraftCore.nItem.nCrafting
{

	using Items = DotCraftCore.init.Items;
	using InventoryCrafting = DotCraftCore.inventory.InventoryCrafting;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using World = DotCraftCore.nWorld.World;
	using MapData = DotCraftCore.nWorld.nStorage.MapData;

	public class RecipesMapExtending : ShapedRecipes
	{
		

		public RecipesMapExtending() : base(3, 3, new ItemStack[] {new ItemStack(Items.paper), new ItemStack(Items.paper), new ItemStack(Items.paper), new ItemStack(Items.paper), new ItemStack(Items.filled_map, 0, 32767), new ItemStack(Items.paper), new ItemStack(Items.paper), new ItemStack(Items.paper), new ItemStack(Items.paper)}, new ItemStack(Items.map, 0, 0))
		{
		}

///    
///     <summary> * Used to check if a recipe matches current crafting inventory </summary>
///     
		public override bool matches(InventoryCrafting p_77569_1_, World p_77569_2_)
		{
			if (!base.matches(p_77569_1_, p_77569_2_))
			{
				return false;
			}
			else
			{
				ItemStack var3 = null;

				for (int var4 = 0; var4 < p_77569_1_.SizeInventory && var3 == null; ++var4)
				{
					ItemStack var5 = p_77569_1_.getStackInSlot(var4);

					if (var5 != null && var5.Item == Items.filled_map)
					{
						var3 = var5;
					}
				}

				if (var3 == null)
				{
					return false;
				}
				else
				{
					MapData var6 = Items.filled_map.getMapData(var3, p_77569_2_);
					return var6 == null ? false : var6.scale < 4;
				}
			}
		}

///    
///     <summary> * Returns an Item that is the result of this recipe </summary>
///     
		public override ItemStack getCraftingResult(InventoryCrafting p_77572_1_)
		{
			ItemStack var2 = null;

			for (int var3 = 0; var3 < p_77572_1_.SizeInventory && var2 == null; ++var3)
			{
				ItemStack var4 = p_77572_1_.getStackInSlot(var3);

				if (var4 != null && var4.Item == Items.filled_map)
				{
					var2 = var4;
				}
			}

			var2 = var2.copy();
			var2.stackSize = 1;

			if (var2.TagCompound == null)
			{
				var2.TagCompound = new NBTTagCompound();
			}

			var2.TagCompound.setBoolean("map_is_scaling", true);
			return var2;
		}
	}

}