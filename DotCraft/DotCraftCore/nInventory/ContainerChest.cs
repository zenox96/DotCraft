using System.Collections;

namespace DotCraftCore.nInventory
{

	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using ItemStack = DotCraftCore.nItem.ItemStack;

	public class ContainerChest : Container
	{
		private IInventory lowerChestInventory;
		private int numRows;
		

		public ContainerChest(IInventory p_i1806_1_, IInventory p_i1806_2_)
		{
			this.lowerChestInventory = p_i1806_2_;
			this.numRows = p_i1806_2_.SizeInventory / 9;
			p_i1806_2_.openInventory();
			int var3 = (this.numRows - 4) * 18;
			int var4;
			int var5;

			for (var4 = 0; var4 < this.numRows; ++var4)
			{
				for (var5 = 0; var5 < 9; ++var5)
				{
					this.addSlotToContainer(new Slot(p_i1806_2_, var5 + var4 * 9, 8 + var5 * 18, 18 + var4 * 18));
				}
			}

			for (var4 = 0; var4 < 3; ++var4)
			{
				for (var5 = 0; var5 < 9; ++var5)
				{
					this.addSlotToContainer(new Slot(p_i1806_1_, var5 + var4 * 9 + 9, 8 + var5 * 18, 103 + var4 * 18 + var3));
				}
			}

			for (var4 = 0; var4 < 9; ++var4)
			{
				this.addSlotToContainer(new Slot(p_i1806_1_, var4, 8 + var4 * 18, 161 + var3));
			}
		}

		public override bool canInteractWith(EntityPlayer p_75145_1_)
		{
			return this.lowerChestInventory.isUseableByPlayer(p_75145_1_);
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

				if (p_82846_2_ < this.numRows * 9)
				{
					if (!this.mergeItemStack(var5, this.numRows * 9, this.inventorySlots.Count, true))
					{
						return null;
					}
				}
				else if (!this.mergeItemStack(var5, 0, this.numRows * 9, false))
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
			}

			return var3;
		}

///    
///     <summary> * Called when the container is closed. </summary>
///     
		public override void onContainerClosed(EntityPlayer p_75134_1_)
		{
			base.onContainerClosed(p_75134_1_);
			this.lowerChestInventory.closeInventory();
		}

///    
///     <summary> * Return this chest container's lower chest inventory. </summary>
///     
		public virtual IInventory LowerChestInventory
		{
			get
			{
				return this.lowerChestInventory;
			}
		}
	}

}