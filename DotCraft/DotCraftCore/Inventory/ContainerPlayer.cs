using System.Collections;

namespace DotCraftCore.Inventory
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using InventoryPlayer = DotCraftCore.Entity.Player.InventoryPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemArmor = DotCraftCore.Item.ItemArmor;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using CraftingManager = DotCraftCore.Item.Crafting.CraftingManager;
	using IIcon = DotCraftCore.Util.IIcon;

	public class ContainerPlayer : Container
	{
	/// <summary> The crafting matrix inventory.  </summary>
		public InventoryCrafting craftMatrix = new InventoryCrafting(this, 2, 2);
		public IInventory craftResult = new InventoryCraftResult();

	/// <summary> Determines if inventory manipulation should be handled.  </summary>
		public bool isLocalWorld;
		private readonly EntityPlayer thePlayer;
		private const string __OBFID = "CL_00001754";

//JAVA TO VB & C# CONVERTER WARNING: 'final' parameters are not allowed in .NET:
//ORIGINAL LINE: public ContainerPlayer(final InventoryPlayer p_i1819_1_, boolean p_i1819_2_, EntityPlayer p_i1819_3_)
		public ContainerPlayer(InventoryPlayer p_i1819_1_, bool p_i1819_2_, EntityPlayer p_i1819_3_)
		{
			this.isLocalWorld = p_i1819_2_;
			this.thePlayer = p_i1819_3_;
			this.addSlotToContainer(new SlotCrafting(p_i1819_1_.player, this.craftMatrix, this.craftResult, 0, 144, 36));
			int var4;
			int var5;

			for (var4 = 0; var4 < 2; ++var4)
			{
				for (var5 = 0; var5 < 2; ++var5)
				{
					this.addSlotToContainer(new Slot(this.craftMatrix, var5 + var4 * 2, 88 + var5 * 18, 26 + var4 * 18));
				}
			}

			for (var4 = 0; var4 < 4; ++var4)
			{
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final int var44 = var4;
				int var44 = var4;
				this.addSlotToContainer(new Slot(p_i1819_1_, p_i1819_1_.SizeInventory - 1 - var4, 8, 8 + var4 * 18) { private static final string __OBFID = "CL_00001755"; public int SlotStackLimit { return 1; } public bool isItemValid(ItemStack p_75214_1_) { return p_75214_1_ == null ? false : (p_75214_1_.Item is ItemArmor ? ((ItemArmor)p_75214_1_.Item).armorType == var44 : (p_75214_1_.Item != Item.getItemFromBlock(Blocks.pumpkin) && p_75214_1_.Item != Items.skull ? false : var44 == 0)); } public IIcon BackgroundIconIndex { return ItemArmor.func_94602_b(var44); } });
			}

			for (var4 = 0; var4 < 3; ++var4)
			{
				for (var5 = 0; var5 < 9; ++var5)
				{
					this.addSlotToContainer(new Slot(p_i1819_1_, var5 + (var4 + 1) * 9, 8 + var5 * 18, 84 + var4 * 18));
				}
			}

			for (var4 = 0; var4 < 9; ++var4)
			{
				this.addSlotToContainer(new Slot(p_i1819_1_, var4, 8 + var4 * 18, 142));
			}

			this.onCraftMatrixChanged(this.craftMatrix);
		}

///    
///     <summary> * Callback for when the crafting matrix is changed. </summary>
///     
		public override void onCraftMatrixChanged(IInventory p_75130_1_)
		{
			this.craftResult.setInventorySlotContents(0, CraftingManager.Instance.findMatchingRecipe(this.craftMatrix, this.thePlayer.worldObj));
		}

///    
///     <summary> * Called when the container is closed. </summary>
///     
		public override void onContainerClosed(EntityPlayer p_75134_1_)
		{
			base.onContainerClosed(p_75134_1_);

			for (int var2 = 0; var2 < 4; ++var2)
			{
				ItemStack var3 = this.craftMatrix.getStackInSlotOnClosing(var2);

				if (var3 != null)
				{
					p_75134_1_.dropPlayerItemWithRandomChoice(var3, false);
				}
			}

			this.craftResult.setInventorySlotContents(0, (ItemStack)null);
		}

		public override bool canInteractWith(EntityPlayer p_75145_1_)
		{
			return true;
		}

///    
///     <summary> * Called when a player shift-clicks on a slot. You must override this or you will crash when someone does that. </summary>
///     
		public override ItemStack transferStackInSlot(EntityPlayer p_82846_1_, int p_82846_2_)
		{
			ItemStack var3 = null;
			Slot var4 = (Slot)this.inventorySlots.get(p_82846_2_);

			if (var4 != null && var4.HasStack)
			{
				ItemStack var5 = var4.Stack;
				var3 = var5.copy();

				if (p_82846_2_ == 0)
				{
					if (!this.mergeItemStack(var5, 9, 45, true))
					{
						return null;
					}

					var4.onSlotChange(var5, var3);
				}
				else if (p_82846_2_ >= 1 && p_82846_2_ < 5)
				{
					if (!this.mergeItemStack(var5, 9, 45, false))
					{
						return null;
					}
				}
				else if (p_82846_2_ >= 5 && p_82846_2_ < 9)
				{
					if (!this.mergeItemStack(var5, 9, 45, false))
					{
						return null;
					}
				}
				else if (var3.Item is ItemArmor && !((Slot)this.inventorySlots.get(5 + ((ItemArmor)var3.Item).armorType)).HasStack)
				{
					int var6 = 5 + ((ItemArmor)var3.Item).armorType;

					if (!this.mergeItemStack(var5, var6, var6 + 1, false))
					{
						return null;
					}
				}
				else if (p_82846_2_ >= 9 && p_82846_2_ < 36)
				{
					if (!this.mergeItemStack(var5, 36, 45, false))
					{
						return null;
					}
				}
				else if (p_82846_2_ >= 36 && p_82846_2_ < 45)
				{
					if (!this.mergeItemStack(var5, 9, 36, false))
					{
						return null;
					}
				}
				else if (!this.mergeItemStack(var5, 9, 45, false))
				{
					return null;
				}

				if (var5.stackSize == 0)
				{
					var4.putStack((ItemStack)null);
				}
				else
				{
					var4.onSlotChanged();
				}

				if (var5.stackSize == var3.stackSize)
				{
					return null;
				}

				var4.onPickupFromSlot(p_82846_1_, var5);
			}

			return var3;
		}

		public override bool func_94530_a(ItemStack p_94530_1_, Slot p_94530_2_)
		{
			return p_94530_2_.inventory != this.craftResult && base.func_94530_a(p_94530_1_, p_94530_2_);
		}
	}

}