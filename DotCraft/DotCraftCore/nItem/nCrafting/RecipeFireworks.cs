using System;
using System.Collections;

namespace DotCraftCore.nItem.nCrafting
{

	using Items = DotCraftCore.init.Items;
	using InventoryCrafting = DotCraftCore.inventory.InventoryCrafting;
	using ItemDye = DotCraftCore.nItem.ItemDye;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using NBTTagList = DotCraftCore.nNBT.NBTTagList;
	using World = DotCraftCore.nWorld.World;

	public class RecipeFireworks : IRecipe
	{
		private ItemStack field_92102_a;
		

///    
///     <summary> * Used to check if a recipe matches current crafting inventory </summary>
///     
		public virtual bool matches(InventoryCrafting p_77569_1_, World p_77569_2_)
		{
			this.field_92102_a = null;
			int var3 = 0;
			int var4 = 0;
			int var5 = 0;
			int var6 = 0;
			int var7 = 0;
			int var8 = 0;

			for (int var9 = 0; var9 < p_77569_1_.SizeInventory; ++var9)
			{
				ItemStack var10 = p_77569_1_.getStackInSlot(var9);

				if (var10 != null)
				{
					if (var10.Item == Items.gunpowder)
					{
						++var4;
					}
					else if (var10.Item == Items.firework_charge)
					{
						++var6;
					}
					else if (var10.Item == Items.dye)
					{
						++var5;
					}
					else if (var10.Item == Items.paper)
					{
						++var3;
					}
					else if (var10.Item == Items.glowstone_dust)
					{
						++var7;
					}
					else if (var10.Item == Items.diamond)
					{
						++var7;
					}
					else if (var10.Item == Items.fire_charge)
					{
						++var8;
					}
					else if (var10.Item == Items.feather)
					{
						++var8;
					}
					else if (var10.Item == Items.gold_nugget)
					{
						++var8;
					}
					else
					{
						if (var10.Item != Items.skull)
						{
							return false;
						}

						++var8;
					}
				}
			}

			var7 += var5 + var8;

			if (var4 <= 3 && var3 <= 1)
			{
				NBTTagCompound var16;
				NBTTagCompound var19;

				if (var4 >= 1 && var3 == 1 && var7 == 0)
				{
					this.field_92102_a = new ItemStack(Items.fireworks);

					if (var6 > 0)
					{
						var16 = new NBTTagCompound();
						var19 = new NBTTagCompound();
						NBTTagList var23 = new NBTTagList();

						for (int var24 = 0; var24 < p_77569_1_.SizeInventory; ++var24)
						{
							ItemStack var26 = p_77569_1_.getStackInSlot(var24);

							if (var26 != null && var26.Item == Items.firework_charge && var26.hasTagCompound() && var26.TagCompound.func_150297_b("Explosion", 10))
							{
								var23.appendTag(var26.TagCompound.getCompoundTag("Explosion"));
							}
						}

						var19.setTag("Explosions", var23);
						var19.setByte("Flight", (sbyte)var4);
						var16.setTag("Fireworks", var19);
						this.field_92102_a.TagCompound = var16;
					}

					return true;
				}
				else if (var4 == 1 && var3 == 0 && var6 == 0 && var5 > 0 && var8 <= 1)
				{
					this.field_92102_a = new ItemStack(Items.firework_charge);
					var16 = new NBTTagCompound();
					var19 = new NBTTagCompound();
					sbyte var22 = 0;
					ArrayList var12 = new ArrayList();

					for (int var13 = 0; var13 < p_77569_1_.SizeInventory; ++var13)
					{
						ItemStack var14 = p_77569_1_.getStackInSlot(var13);

						if (var14 != null)
						{
							if (var14.Item == Items.dye)
							{
								var12.Add(Convert.ToInt32(ItemDye.field_150922_c[var14.ItemDamage]));
							}
							else if (var14.Item == Items.glowstone_dust)
							{
								var19.setBoolean("Flicker", true);
							}
							else if (var14.Item == Items.diamond)
							{
								var19.setBoolean("Trail", true);
							}
							else if (var14.Item == Items.fire_charge)
							{
								var22 = 1;
							}
							else if (var14.Item == Items.feather)
							{
								var22 = 4;
							}
							else if (var14.Item == Items.gold_nugget)
							{
								var22 = 2;
							}
							else if (var14.Item == Items.skull)
							{
								var22 = 3;
							}
						}
					}

					int[] var25 = new int[var12.Count];

					for (int var27 = 0; var27 < var25.Length; ++var27)
					{
						var25[var27] = (int)((int?)var12[var27]);
					}

					var19.setIntArray("Colors", var25);
					var19.setByte("Type", var22);
					var16.setTag("Explosion", var19);
					this.field_92102_a.TagCompound = var16;
					return true;
				}
				else if (var4 == 0 && var3 == 0 && var6 == 1 && var5 > 0 && var5 == var7)
				{
					ArrayList var15 = new ArrayList();

					for (int var17 = 0; var17 < p_77569_1_.SizeInventory; ++var17)
					{
						ItemStack var11 = p_77569_1_.getStackInSlot(var17);

						if (var11 != null)
						{
							if (var11.Item == Items.dye)
							{
								var15.Add(Convert.ToInt32(ItemDye.field_150922_c[var11.ItemDamage]));
							}
							else if (var11.Item == Items.firework_charge)
							{
								this.field_92102_a = var11.copy();
								this.field_92102_a.stackSize = 1;
							}
						}
					}

					int[] var18 = new int[var15.Count];

					for (int var20 = 0; var20 < var18.Length; ++var20)
					{
						var18[var20] = (int)((int?)var15[var20]);
					}

					if (this.field_92102_a != null && this.field_92102_a.hasTagCompound())
					{
						NBTTagCompound var21 = this.field_92102_a.TagCompound.getCompoundTag("Explosion");

						if (var21 == null)
						{
							return false;
						}
						else
						{
							var21.setIntArray("FadeColors", var18);
							return true;
						}
					}
					else
					{
						return false;
					}
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Returns an Item that is the result of this recipe </summary>
///     
		public virtual ItemStack getCraftingResult(InventoryCrafting p_77572_1_)
		{
			return this.field_92102_a.copy();
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
				return this.field_92102_a;
			}
		}
	}

}