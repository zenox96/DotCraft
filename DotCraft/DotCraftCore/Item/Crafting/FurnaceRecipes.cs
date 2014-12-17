using System;
using System.Collections;

namespace DotCraftCore.nItem.nCrafting
{

	using Block = DotCraftCore.nBlock.Block;
	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using Item = DotCraftCore.nItem.Item;
	using ItemFishFood = DotCraftCore.nItem.ItemFishFood;
	using ItemStack = DotCraftCore.nItem.ItemStack;

	public class FurnaceRecipes
	{
		private static readonly FurnaceRecipes smeltingBase = new FurnaceRecipes();

	/// <summary> The list of smelting results.  </summary>
		private IDictionary smeltingList = new Hashtable();
		private IDictionary experienceList = new Hashtable();
		

///    
///     <summary> * Used to call methods addSmelting and getSmeltingResult. </summary>
///     
		public static FurnaceRecipes smelting()
		{
			return smeltingBase;
		}

		private FurnaceRecipes()
		{
			this.func_151393_a(Blocks.iron_ore, new ItemStack(Items.iron_ingot), 0.7F);
			this.func_151393_a(Blocks.gold_ore, new ItemStack(Items.gold_ingot), 1.0F);
			this.func_151393_a(Blocks.diamond_ore, new ItemStack(Items.diamond), 1.0F);
			this.func_151393_a(Blocks.sand, new ItemStack(Blocks.glass), 0.1F);
			this.func_151396_a(Items.porkchop, new ItemStack(Items.cooked_porkchop), 0.35F);
			this.func_151396_a(Items.beef, new ItemStack(Items.cooked_beef), 0.35F);
			this.func_151396_a(Items.chicken, new ItemStack(Items.cooked_chicken), 0.35F);
			this.func_151393_a(Blocks.cobblestone, new ItemStack(Blocks.stone), 0.1F);
			this.func_151396_a(Items.clay_ball, new ItemStack(Items.brick), 0.3F);
			this.func_151393_a(Blocks.clay, new ItemStack(Blocks.hardened_clay), 0.35F);
			this.func_151393_a(Blocks.cactus, new ItemStack(Items.dye, 1, 2), 0.2F);
			this.func_151393_a(Blocks.log, new ItemStack(Items.coal, 1, 1), 0.15F);
			this.func_151393_a(Blocks.log2, new ItemStack(Items.coal, 1, 1), 0.15F);
			this.func_151393_a(Blocks.emerald_ore, new ItemStack(Items.emerald), 1.0F);
			this.func_151396_a(Items.potato, new ItemStack(Items.baked_potato), 0.35F);
			this.func_151393_a(Blocks.netherrack, new ItemStack(Items.netherbrick), 0.1F);
			ItemFishFood.FishType[] var1 = ItemFishFood.FishType.values();
			int var2 = var1.Length;

			for (int var3 = 0; var3 < var2; ++var3)
			{
				ItemFishFood.FishType var4 = var1[var3];

				if (var4.func_150973_i())
				{
					this.func_151394_a(new ItemStack(Items.fish, 1, var4.func_150976_a()), new ItemStack(Items.cooked_fished, 1, var4.func_150976_a()), 0.35F);
				}
			}

			this.func_151393_a(Blocks.coal_ore, new ItemStack(Items.coal), 0.1F);
			this.func_151393_a(Blocks.redstone_ore, new ItemStack(Items.redstone), 0.7F);
			this.func_151393_a(Blocks.lapis_ore, new ItemStack(Items.dye, 1, 4), 0.2F);
			this.func_151393_a(Blocks.quartz_ore, new ItemStack(Items.quartz), 0.2F);
		}

		public virtual void func_151393_a(Block p_151393_1_, ItemStack p_151393_2_, float p_151393_3_)
		{
			this.func_151396_a(Item.getItemFromBlock(p_151393_1_), p_151393_2_, p_151393_3_);
		}

		public virtual void func_151396_a(Item p_151396_1_, ItemStack p_151396_2_, float p_151396_3_)
		{
			this.func_151394_a(new ItemStack(p_151396_1_, 1, 32767), p_151396_2_, p_151396_3_);
		}

		public virtual void func_151394_a(ItemStack p_151394_1_, ItemStack p_151394_2_, float p_151394_3_)
		{
			this.smeltingList.Add(p_151394_1_, p_151394_2_);
			this.experienceList.Add(p_151394_2_, Convert.ToSingle(p_151394_3_));
		}

		public virtual ItemStack func_151395_a(ItemStack p_151395_1_)
		{
			IEnumerator var2 = this.smeltingList.GetEnumerator();
			Entry var3;

			do
			{
				if (!var2.MoveNext())
				{
					return null;
				}

				var3 = (Entry)var2.Current;
			}
			while (!this.func_151397_a(p_151395_1_, (ItemStack)var3.Key));

			return (ItemStack)var3.Value;
		}

		private bool func_151397_a(ItemStack p_151397_1_, ItemStack p_151397_2_)
		{
			return p_151397_2_.Item == p_151397_1_.Item && (p_151397_2_.ItemDamage == 32767 || p_151397_2_.ItemDamage == p_151397_1_.ItemDamage);
		}

		public virtual IDictionary SmeltingList
		{
			get
			{
				return this.smeltingList;
			}
		}

		public virtual float func_151398_b(ItemStack p_151398_1_)
		{
			IEnumerator var2 = this.experienceList.GetEnumerator();
			Entry var3;

			do
			{
				if (!var2.MoveNext())
				{
					return 0.0F;
				}

				var3 = (Entry)var2.Current;
			}
			while (!this.func_151397_a(p_151398_1_, (ItemStack)var3.Key));

			return (float)((float?)var3.Value);
		}
	}

}