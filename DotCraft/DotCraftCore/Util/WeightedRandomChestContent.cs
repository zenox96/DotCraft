using System;

namespace DotCraftCore.Util
{

	using IInventory = DotCraftCore.inventory.IInventory;
	using Item = DotCraftCore.item.Item;
	using ItemStack = DotCraftCore.item.ItemStack;
	using TileEntityDispenser = DotCraftCore.TileEntity.TileEntityDispenser;

	public class WeightedRandomChestContent : WeightedRandom.Item
	{
	/// <summary> The Item/Block ID to generate in the Chest.  </summary>
		private ItemStack theItemId;

	/// <summary> The minimum chance of item generating.  </summary>
		private int theMinimumChanceToGenerateItem;

	/// <summary> The maximum chance of item generating.  </summary>
		private int theMaximumChanceToGenerateItem;
		private const string __OBFID = "CL_00001505";

		public WeightedRandomChestContent(Item p_i45311_1_, int p_i45311_2_, int p_i45311_3_, int p_i45311_4_, int p_i45311_5_) : base(p_i45311_5_)
		{
			this.theItemId = new ItemStack(p_i45311_1_, 1, p_i45311_2_);
			this.theMinimumChanceToGenerateItem = p_i45311_3_;
			this.theMaximumChanceToGenerateItem = p_i45311_4_;
		}

		public WeightedRandomChestContent(ItemStack p_i1558_1_, int p_i1558_2_, int p_i1558_3_, int p_i1558_4_) : base(p_i1558_4_)
		{
			this.theItemId = p_i1558_1_;
			this.theMinimumChanceToGenerateItem = p_i1558_2_;
			this.theMaximumChanceToGenerateItem = p_i1558_3_;
		}

///    
///     <summary> * Generates the Chest contents. </summary>
///     
		public static void generateChestContents(Random p_76293_0_, WeightedRandomChestContent[] p_76293_1_, IInventory p_76293_2_, int p_76293_3_)
		{
			for(int var4 = 0; var4 < p_76293_3_; ++var4)
			{
				WeightedRandomChestContent var5 = (WeightedRandomChestContent)WeightedRandom.getRandomItem(p_76293_0_, p_76293_1_);
				int var6 = var5.theMinimumChanceToGenerateItem + p_76293_0_.Next(var5.theMaximumChanceToGenerateItem - var5.theMinimumChanceToGenerateItem + 1);

				if(var5.theItemId.MaxStackSize >= var6)
				{
					ItemStack var7 = var5.theItemId.copy();
					var7.stackSize = var6;
					p_76293_2_.setInventorySlotContents(p_76293_0_.Next(p_76293_2_.SizeInventory), var7);
				}
				else
				{
					for(int var9 = 0; var9 < var6; ++var9)
					{
						ItemStack var8 = var5.theItemId.copy();
						var8.stackSize = 1;
						p_76293_2_.setInventorySlotContents(p_76293_0_.Next(p_76293_2_.SizeInventory), var8);
					}
				}
			}
		}

		public static void func_150706_a(Random p_150706_0_, WeightedRandomChestContent[] p_150706_1_, TileEntityDispenser p_150706_2_, int p_150706_3_)
		{
			for(int var4 = 0; var4 < p_150706_3_; ++var4)
			{
				WeightedRandomChestContent var5 = (WeightedRandomChestContent)WeightedRandom.getRandomItem(p_150706_0_, p_150706_1_);
				int var6 = var5.theMinimumChanceToGenerateItem + p_150706_0_.Next(var5.theMaximumChanceToGenerateItem - var5.theMinimumChanceToGenerateItem + 1);

				if(var5.theItemId.MaxStackSize >= var6)
				{
					ItemStack var7 = var5.theItemId.copy();
					var7.stackSize = var6;
					p_150706_2_.setInventorySlotContents(p_150706_0_.Next(p_150706_2_.SizeInventory), var7);
				}
				else
				{
					for(int var9 = 0; var9 < var6; ++var9)
					{
						ItemStack var8 = var5.theItemId.copy();
						var8.stackSize = 1;
						p_150706_2_.setInventorySlotContents(p_150706_0_.Next(p_150706_2_.SizeInventory), var8);
					}
				}
			}
		}

		public static WeightedRandomChestContent[] func_92080_a(WeightedRandomChestContent[] p_92080_0_, params WeightedRandomChestContent[] p_92080_1_)
		{
			WeightedRandomChestContent[] var2 = new WeightedRandomChestContent[p_92080_0_.Length + p_92080_1_.length];
			int var3 = 0;

			for(int var4 = 0; var4 < p_92080_0_.Length; ++var4)
			{
				var2[var3++] = p_92080_0_[var4];
			}

			WeightedRandomChestContent[] var8 = p_92080_1_;
			int var5 = p_92080_1_.length;

			for(int var6 = 0; var6 < var5; ++var6)
			{
				WeightedRandomChestContent var7 = var8[var6];
				var2[var3++] = var7;
			}

			return var2;
		}
	}

}