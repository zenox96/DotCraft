using System;
using System.Collections;

namespace DotCraftCore.Inventory
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemHoe = DotCraftCore.Item.ItemHoe;
	using ItemPickaxe = DotCraftCore.Item.ItemPickaxe;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using ItemSword = DotCraftCore.Item.ItemSword;
	using AchievementList = DotCraftCore.Stats.AchievementList;

	public class SlotCrafting : Slot
	{
	/// <summary> The craft matrix inventory linked to this result slot.  </summary>
		private readonly IInventory craftMatrix;

	/// <summary> The player that is using the GUI where this slot resides.  </summary>
		private EntityPlayer thePlayer;

///    
///     <summary> * The number of items that have been crafted so far. Gets passed to ItemStack.onCrafting before being reset. </summary>
///     
		private int amountCrafted;
		

		public SlotCrafting(EntityPlayer p_i1823_1_, IInventory p_i1823_2_, IInventory p_i1823_3_, int p_i1823_4_, int p_i1823_5_, int p_i1823_6_) : base(p_i1823_3_, p_i1823_4_, p_i1823_5_, p_i1823_6_)
		{
			this.thePlayer = p_i1823_1_;
			this.craftMatrix = p_i1823_2_;
		}

///    
///     <summary> * Check if the stack is a valid item for this slot. Always true beside for the armor slots. </summary>
///     
		public override bool isItemValid(ItemStack p_75214_1_)
		{
			return false;
		}

///    
///     <summary> * Decrease the size of the stack in slot (first int arg) by the amount of the second int arg. Returns the new
///     * stack. </summary>
///     
		public override ItemStack decrStackSize(int p_75209_1_)
		{
			if (this.HasStack)
			{
				this.amountCrafted += Math.Min(p_75209_1_, this.Stack.stackSize);
			}

			return base.decrStackSize(p_75209_1_);
		}

///    
///     <summary> * the itemStack passed in is the output - ie, iron ingots, and pickaxes, not ore and wood. Typically increases an
///     * internal count then calls onCrafting(item). </summary>
///     
		protected internal override void onCrafting(ItemStack p_75210_1_, int p_75210_2_)
		{
			this.amountCrafted += p_75210_2_;
			this.onCrafting(p_75210_1_);
		}

///    
///     <summary> * the itemStack passed in is the output - ie, iron ingots, and pickaxes, not ore and wood. </summary>
///     
		protected internal override void onCrafting(ItemStack p_75208_1_)
		{
			p_75208_1_.onCrafting(this.thePlayer.worldObj, this.thePlayer, this.amountCrafted);
			this.amountCrafted = 0;

			if (p_75208_1_.Item == Item.getItemFromBlock(Blocks.crafting_table))
			{
				this.thePlayer.addStat(AchievementList.buildWorkBench, 1);
			}

			if (p_75208_1_.Item is ItemPickaxe)
			{
				this.thePlayer.addStat(AchievementList.buildPickaxe, 1);
			}

			if (p_75208_1_.Item == Item.getItemFromBlock(Blocks.furnace))
			{
				this.thePlayer.addStat(AchievementList.buildFurnace, 1);
			}

			if (p_75208_1_.Item is ItemHoe)
			{
				this.thePlayer.addStat(AchievementList.buildHoe, 1);
			}

			if (p_75208_1_.Item == Items.bread)
			{
				this.thePlayer.addStat(AchievementList.makeBread, 1);
			}

			if (p_75208_1_.Item == Items.cake)
			{
				this.thePlayer.addStat(AchievementList.bakeCake, 1);
			}

			if (p_75208_1_.Item is ItemPickaxe && ((ItemPickaxe)p_75208_1_.Item).func_150913_i() != Item.ToolMaterial.WOOD)
			{
				this.thePlayer.addStat(AchievementList.buildBetterPickaxe, 1);
			}

			if (p_75208_1_.Item is ItemSword)
			{
				this.thePlayer.addStat(AchievementList.buildSword, 1);
			}

			if (p_75208_1_.Item == Item.getItemFromBlock(Blocks.enchanting_table))
			{
				this.thePlayer.addStat(AchievementList.enchantments, 1);
			}

			if (p_75208_1_.Item == Item.getItemFromBlock(Blocks.bookshelf))
			{
				this.thePlayer.addStat(AchievementList.bookcase, 1);
			}
		}

		public override void onPickupFromSlot(EntityPlayer p_82870_1_, ItemStack p_82870_2_)
		{
			this.onCrafting(p_82870_2_);

			for (int var3 = 0; var3 < this.craftMatrix.SizeInventory; ++var3)
			{
				ItemStack var4 = this.craftMatrix.getStackInSlot(var3);

				if (var4 != null)
				{
					this.craftMatrix.decrStackSize(var3, 1);

					if (var4.Item.hasContainerItem())
					{
						ItemStack var5 = new ItemStack(var4.Item.ContainerItem);

						if (!var4.Item.doesContainerItemLeaveCraftingGrid(var4) || !this.thePlayer.inventory.addItemStackToInventory(var5))
						{
							if (this.craftMatrix.getStackInSlot(var3) == null)
							{
								this.craftMatrix.setInventorySlotContents(var3, var5);
							}
							else
							{
								this.thePlayer.dropPlayerItemWithRandomChoice(var5, false);
							}
						}
					}
				}
			}
		}
	}

}