using System.Collections;

namespace DotCraftCore.Inventory
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using InventoryPlayer = DotCraftCore.Entity.Player.InventoryPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using CraftingManager = DotCraftCore.Item.Crafting.CraftingManager;
	using World = DotCraftCore.World.World;

	public class ContainerWorkbench : Container
	{
	/// <summary> The crafting matrix inventory (3x3).  </summary>
		public InventoryCrafting craftMatrix = new InventoryCrafting(this, 3, 3);
		public IInventory craftResult = new InventoryCraftResult();
		private World worldObj;
		private int posX;
		private int posY;
		private int posZ;
		private const string __OBFID = "CL_00001744";

		public ContainerWorkbench(InventoryPlayer p_i1808_1_, World p_i1808_2_, int p_i1808_3_, int p_i1808_4_, int p_i1808_5_)
		{
			this.worldObj = p_i1808_2_;
			this.posX = p_i1808_3_;
			this.posY = p_i1808_4_;
			this.posZ = p_i1808_5_;
			this.addSlotToContainer(new SlotCrafting(p_i1808_1_.player, this.craftMatrix, this.craftResult, 0, 124, 35));
			int var6;
			int var7;

			for (var6 = 0; var6 < 3; ++var6)
			{
				for (var7 = 0; var7 < 3; ++var7)
				{
					this.addSlotToContainer(new Slot(this.craftMatrix, var7 + var6 * 3, 30 + var7 * 18, 17 + var6 * 18));
				}
			}

			for (var6 = 0; var6 < 3; ++var6)
			{
				for (var7 = 0; var7 < 9; ++var7)
				{
					this.addSlotToContainer(new Slot(p_i1808_1_, var7 + var6 * 9 + 9, 8 + var7 * 18, 84 + var6 * 18));
				}
			}

			for (var6 = 0; var6 < 9; ++var6)
			{
				this.addSlotToContainer(new Slot(p_i1808_1_, var6, 8 + var6 * 18, 142));
			}

			this.onCraftMatrixChanged(this.craftMatrix);
		}

///    
///     <summary> * Callback for when the crafting matrix is changed. </summary>
///     
		public override void onCraftMatrixChanged(IInventory p_75130_1_)
		{
			this.craftResult.setInventorySlotContents(0, CraftingManager.Instance.findMatchingRecipe(this.craftMatrix, this.worldObj));
		}

///    
///     <summary> * Called when the container is closed. </summary>
///     
		public override void onContainerClosed(EntityPlayer p_75134_1_)
		{
			base.onContainerClosed(p_75134_1_);

			if (!this.worldObj.isClient)
			{
				for (int var2 = 0; var2 < 9; ++var2)
				{
					ItemStack var3 = this.craftMatrix.getStackInSlotOnClosing(var2);

					if (var3 != null)
					{
						p_75134_1_.dropPlayerItemWithRandomChoice(var3, false);
					}
				}
			}
		}

		public override bool canInteractWith(EntityPlayer p_75145_1_)
		{
			return this.worldObj.getBlock(this.posX, this.posY, this.posZ) != Blocks.crafting_table ? false : p_75145_1_.getDistanceSq((double)this.posX + 0.5D, (double)this.posY + 0.5D, (double)this.posZ + 0.5D) <= 64.0D;
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
					if (!this.mergeItemStack(var5, 10, 46, true))
					{
						return null;
					}

					var4.onSlotChange(var5, var3);
				}
				else if (p_82846_2_ >= 10 && p_82846_2_ < 37)
				{
					if (!this.mergeItemStack(var5, 37, 46, false))
					{
						return null;
					}
				}
				else if (p_82846_2_ >= 37 && p_82846_2_ < 46)
				{
					if (!this.mergeItemStack(var5, 10, 37, false))
					{
						return null;
					}
				}
				else if (!this.mergeItemStack(var5, 10, 46, false))
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