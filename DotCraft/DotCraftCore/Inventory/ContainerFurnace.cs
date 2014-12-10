using System.Collections;

namespace DotCraftCore.Inventory
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using InventoryPlayer = DotCraftCore.Entity.Player.InventoryPlayer;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using FurnaceRecipes = DotCraftCore.Item.Crafting.FurnaceRecipes;
	using TileEntityFurnace = DotCraftCore.TileEntity.TileEntityFurnace;

	public class ContainerFurnace : Container
	{
		private TileEntityFurnace furnace;
		private int lastCookTime;
		private int lastBurnTime;
		private int lastItemBurnTime;
		private const string __OBFID = "CL_00001748";

		public ContainerFurnace(InventoryPlayer p_i1812_1_, TileEntityFurnace p_i1812_2_)
		{
			this.furnace = p_i1812_2_;
			this.addSlotToContainer(new Slot(p_i1812_2_, 0, 56, 17));
			this.addSlotToContainer(new Slot(p_i1812_2_, 1, 56, 53));
			this.addSlotToContainer(new SlotFurnace(p_i1812_1_.player, p_i1812_2_, 2, 116, 35));
			int var3;

			for (var3 = 0; var3 < 3; ++var3)
			{
				for (int var4 = 0; var4 < 9; ++var4)
				{
					this.addSlotToContainer(new Slot(p_i1812_1_, var4 + var3 * 9 + 9, 8 + var4 * 18, 84 + var3 * 18));
				}
			}

			for (var3 = 0; var3 < 9; ++var3)
			{
				this.addSlotToContainer(new Slot(p_i1812_1_, var3, 8 + var3 * 18, 142));
			}
		}

		public override void addCraftingToCrafters(ICrafting p_75132_1_)
		{
			base.addCraftingToCrafters(p_75132_1_);
			p_75132_1_.sendProgressBarUpdate(this, 0, this.furnace.field_145961_j);
			p_75132_1_.sendProgressBarUpdate(this, 1, this.furnace.field_145956_a);
			p_75132_1_.sendProgressBarUpdate(this, 2, this.furnace.field_145963_i);
		}

///    
///     <summary> * Looks for changes made in the container, sends them to every listener. </summary>
///     
		public override void detectAndSendChanges()
		{
			base.detectAndSendChanges();

			for (int var1 = 0; var1 < this.crafters.Count; ++var1)
			{
				ICrafting var2 = (ICrafting)this.crafters.get(var1);

				if (this.lastCookTime != this.furnace.field_145961_j)
				{
					var2.sendProgressBarUpdate(this, 0, this.furnace.field_145961_j);
				}

				if (this.lastBurnTime != this.furnace.field_145956_a)
				{
					var2.sendProgressBarUpdate(this, 1, this.furnace.field_145956_a);
				}

				if (this.lastItemBurnTime != this.furnace.field_145963_i)
				{
					var2.sendProgressBarUpdate(this, 2, this.furnace.field_145963_i);
				}
			}

			this.lastCookTime = this.furnace.field_145961_j;
			this.lastBurnTime = this.furnace.field_145956_a;
			this.lastItemBurnTime = this.furnace.field_145963_i;
		}

		public override void updateProgressBar(int p_75137_1_, int p_75137_2_)
		{
			if (p_75137_1_ == 0)
			{
				this.furnace.field_145961_j = p_75137_2_;
			}

			if (p_75137_1_ == 1)
			{
				this.furnace.field_145956_a = p_75137_2_;
			}

			if (p_75137_1_ == 2)
			{
				this.furnace.field_145963_i = p_75137_2_;
			}
		}

		public override bool canInteractWith(EntityPlayer p_75145_1_)
		{
			return this.furnace.isUseableByPlayer(p_75145_1_);
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

				if (p_82846_2_ == 2)
				{
					if (!this.mergeItemStack(var5, 3, 39, true))
					{
						return null;
					}

					var4.onSlotChange(var5, var3);
				}
				else if (p_82846_2_ != 1 && p_82846_2_ != 0)
				{
					if (FurnaceRecipes.smelting().func_151395_a(var5) != null)
					{
						if (!this.mergeItemStack(var5, 0, 1, false))
						{
							return null;
						}
					}
					else if (TileEntityFurnace.func_145954_b(var5))
					{
						if (!this.mergeItemStack(var5, 1, 2, false))
						{
							return null;
						}
					}
					else if (p_82846_2_ >= 3 && p_82846_2_ < 30)
					{
						if (!this.mergeItemStack(var5, 30, 39, false))
						{
							return null;
						}
					}
					else if (p_82846_2_ >= 30 && p_82846_2_ < 39 && !this.mergeItemStack(var5, 3, 30, false))
					{
						return null;
					}
				}
				else if (!this.mergeItemStack(var5, 3, 39, false))
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
	}

}