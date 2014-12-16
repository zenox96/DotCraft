using System;
using System.Collections;

namespace DotCraftCore.Item.Crafting
{

	using BlockColored = DotCraftCore.block.BlockColored;
	using EntitySheep = DotCraftCore.entity.passive.EntitySheep;
	using Items = DotCraftCore.init.Items;
	using InventoryCrafting = DotCraftCore.inventory.InventoryCrafting;
	using ItemArmor = DotCraftCore.Item.ItemArmor;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using World = DotCraftCore.World.World;

	public class RecipesArmorDyes : IRecipe
	{
		

///    
///     <summary> * Used to check if a recipe matches current crafting inventory </summary>
///     
		public virtual bool matches(InventoryCrafting p_77569_1_, World p_77569_2_)
		{
			ItemStack var3 = null;
			ArrayList var4 = new ArrayList();

			for (int var5 = 0; var5 < p_77569_1_.SizeInventory; ++var5)
			{
				ItemStack var6 = p_77569_1_.getStackInSlot(var5);

				if (var6 != null)
				{
					if (var6.Item is ItemArmor)
					{
						ItemArmor var7 = (ItemArmor)var6.Item;

						if (var7.ArmorMaterial != ItemArmor.ArmorMaterial.CLOTH || var3 != null)
						{
							return false;
						}

						var3 = var6;
					}
					else
					{
						if (var6.Item != Items.dye)
						{
							return false;
						}

						var4.Add(var6);
					}
				}
			}

			return var3 != null && !var4.Count == 0;
		}

///    
///     <summary> * Returns an Item that is the result of this recipe </summary>
///     
		public virtual ItemStack getCraftingResult(InventoryCrafting p_77572_1_)
		{
			ItemStack var2 = null;
			int[] var3 = new int[3];
			int var4 = 0;
			int var5 = 0;
			ItemArmor var6 = null;
			int var7;
			int var9;
			float var10;
			float var11;
			int var17;

			for (var7 = 0; var7 < p_77572_1_.SizeInventory; ++var7)
			{
				ItemStack var8 = p_77572_1_.getStackInSlot(var7);

				if (var8 != null)
				{
					if (var8.Item is ItemArmor)
					{
						var6 = (ItemArmor)var8.Item;

						if (var6.ArmorMaterial != ItemArmor.ArmorMaterial.CLOTH || var2 != null)
						{
							return null;
						}

						var2 = var8.copy();
						var2.stackSize = 1;

						if (var6.hasColor(var8))
						{
							var9 = var6.getColor(var2);
							var10 = (float)(var9 >> 16 & 255) / 255.0F;
							var11 = (float)(var9 >> 8 & 255) / 255.0F;
							float var12 = (float)(var9 & 255) / 255.0F;
							var4 = (int)((float)var4 + Math.Max(var10, Math.Max(var11, var12)) * 255.0F);
							var3[0] = (int)((float)var3[0] + var10 * 255.0F);
							var3[1] = (int)((float)var3[1] + var11 * 255.0F);
							var3[2] = (int)((float)var3[2] + var12 * 255.0F);
							++var5;
						}
					}
					else
					{
						if (var8.Item != Items.dye)
						{
							return null;
						}

						float[] var14 = EntitySheep.fleeceColorTable[BlockColored.func_150032_b(var8.ItemDamage)];
						int var15 = (int)(var14[0] * 255.0F);
						int var16 = (int)(var14[1] * 255.0F);
						var17 = (int)(var14[2] * 255.0F);
						var4 += Math.Max(var15, Math.Max(var16, var17));
						var3[0] += var15;
						var3[1] += var16;
						var3[2] += var17;
						++var5;
					}
				}
			}

			if (var6 == null)
			{
				return null;
			}
			else
			{
				var7 = var3[0] / var5;
				int var13 = var3[1] / var5;
				var9 = var3[2] / var5;
				var10 = (float)var4 / (float)var5;
				var11 = (float)Math.Max(var7, Math.Max(var13, var9));
				var7 = (int)((float)var7 * var10 / var11);
				var13 = (int)((float)var13 * var10 / var11);
				var9 = (int)((float)var9 * var10 / var11);
				var17 = (var7 << 8) + var13;
				var17 = (var17 << 8) + var9;
				var6.func_82813_b(var2, var17);
				return var2;
			}
		}

///    
///     <summary> * Returns the size of the recipe area </summary>
///     
		public virtual int RecipeSize
		{
			get
			{
				return 10;
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