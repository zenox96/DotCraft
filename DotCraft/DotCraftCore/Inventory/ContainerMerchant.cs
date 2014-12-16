using System.Collections;

namespace DotCraftCore.Inventory
{

	using IMerchant = DotCraftCore.Entity.IMerchant;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using InventoryPlayer = DotCraftCore.Entity.Player.InventoryPlayer;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using World = DotCraftCore.World.World;

	public class ContainerMerchant : Container
	{
	/// <summary> Instance of Merchant.  </summary>
		private IMerchant theMerchant;
		private InventoryMerchant merchantInventory;

	/// <summary> Instance of World.  </summary>
		private readonly World theWorld;
		

		public ContainerMerchant(InventoryPlayer p_i1821_1_, IMerchant p_i1821_2_, World p_i1821_3_)
		{
			this.theMerchant = p_i1821_2_;
			this.theWorld = p_i1821_3_;
			this.merchantInventory = new InventoryMerchant(p_i1821_1_.player, p_i1821_2_);
			this.addSlotToContainer(new Slot(this.merchantInventory, 0, 36, 53));
			this.addSlotToContainer(new Slot(this.merchantInventory, 1, 62, 53));
			this.addSlotToContainer(new SlotMerchantResult(p_i1821_1_.player, p_i1821_2_, this.merchantInventory, 2, 120, 53));
			int var4;

			for (var4 = 0; var4 < 3; ++var4)
			{
				for (int var5 = 0; var5 < 9; ++var5)
				{
					this.addSlotToContainer(new Slot(p_i1821_1_, var5 + var4 * 9 + 9, 8 + var5 * 18, 84 + var4 * 18));
				}
			}

			for (var4 = 0; var4 < 9; ++var4)
			{
				this.addSlotToContainer(new Slot(p_i1821_1_, var4, 8 + var4 * 18, 142));
			}
		}

		public virtual InventoryMerchant MerchantInventory
		{
			get
			{
				return this.merchantInventory;
			}
		}

		public override void addCraftingToCrafters(ICrafting p_75132_1_)
		{
			base.addCraftingToCrafters(p_75132_1_);
		}

///    
///     <summary> * Looks for changes made in the container, sends them to every listener. </summary>
///     
		public override void detectAndSendChanges()
		{
			base.detectAndSendChanges();
		}

///    
///     <summary> * Callback for when the crafting matrix is changed. </summary>
///     
		public override void onCraftMatrixChanged(IInventory p_75130_1_)
		{
			this.merchantInventory.resetRecipeAndSlots();
			base.onCraftMatrixChanged(p_75130_1_);
		}

		public virtual int CurrentRecipeIndex
		{
			set
			{
				this.merchantInventory.CurrentRecipeIndex = value;
			}
		}

		public override void updateProgressBar(int p_75137_1_, int p_75137_2_)
		{
		}

		public override bool canInteractWith(EntityPlayer p_75145_1_)
		{
			return this.theMerchant.Customer == p_75145_1_;
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
				else if (p_82846_2_ != 0 && p_82846_2_ != 1)
				{
					if (p_82846_2_ >= 3 && p_82846_2_ < 30)
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

///    
///     <summary> * Called when the container is closed. </summary>
///     
		public override void onContainerClosed(EntityPlayer p_75134_1_)
		{
			base.onContainerClosed(p_75134_1_);
			this.theMerchant.Customer = (EntityPlayer)null;
			base.onContainerClosed(p_75134_1_);

			if (!this.theWorld.isClient)
			{
				ItemStack var2 = this.merchantInventory.getStackInSlotOnClosing(0);

				if (var2 != null)
				{
					p_75134_1_.dropPlayerItemWithRandomChoice(var2, false);
				}

				var2 = this.merchantInventory.getStackInSlotOnClosing(1);

				if (var2 != null)
				{
					p_75134_1_.dropPlayerItemWithRandomChoice(var2, false);
				}
			}
		}
	}

}