using System.Collections;

namespace DotCraftCore.Inventory
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using InventoryPlayer = DotCraftCore.Entity.Player.InventoryPlayer;
	using Items = DotCraftCore.Init.Items;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using AchievementList = DotCraftCore.Stats.AchievementList;
	using TileEntityBrewingStand = DotCraftCore.TileEntity.TileEntityBrewingStand;

	public class ContainerBrewingStand : Container
	{
		private TileEntityBrewingStand tileBrewingStand;

	/// <summary> Instance of Slot.  </summary>
		private readonly Slot theSlot;
		private int brewTime;
		

		public ContainerBrewingStand(InventoryPlayer p_i1805_1_, TileEntityBrewingStand p_i1805_2_)
		{
			this.tileBrewingStand = p_i1805_2_;
			this.addSlotToContainer(new ContainerBrewingStand.Potion(p_i1805_1_.player, p_i1805_2_, 0, 56, 46));
			this.addSlotToContainer(new ContainerBrewingStand.Potion(p_i1805_1_.player, p_i1805_2_, 1, 79, 53));
			this.addSlotToContainer(new ContainerBrewingStand.Potion(p_i1805_1_.player, p_i1805_2_, 2, 102, 46));
			this.theSlot = this.addSlotToContainer(new ContainerBrewingStand.Ingredient(p_i1805_2_, 3, 79, 17));
			int var3;

			for (var3 = 0; var3 < 3; ++var3)
			{
				for (int var4 = 0; var4 < 9; ++var4)
				{
					this.addSlotToContainer(new Slot(p_i1805_1_, var4 + var3 * 9 + 9, 8 + var4 * 18, 84 + var3 * 18));
				}
			}

			for (var3 = 0; var3 < 9; ++var3)
			{
				this.addSlotToContainer(new Slot(p_i1805_1_, var3, 8 + var3 * 18, 142));
			}
		}

		public override void addCraftingToCrafters(ICrafting p_75132_1_)
		{
			base.addCraftingToCrafters(p_75132_1_);
			p_75132_1_.sendProgressBarUpdate(this, 0, this.tileBrewingStand.func_145935_i());
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

				if (this.brewTime != this.tileBrewingStand.func_145935_i())
				{
					var2.sendProgressBarUpdate(this, 0, this.tileBrewingStand.func_145935_i());
				}
			}

			this.brewTime = this.tileBrewingStand.func_145935_i();
		}

		public override void updateProgressBar(int p_75137_1_, int p_75137_2_)
		{
			if (p_75137_1_ == 0)
			{
				this.tileBrewingStand.func_145938_d(p_75137_2_);
			}
		}

		public override bool canInteractWith(EntityPlayer p_75145_1_)
		{
			return this.tileBrewingStand.isUseableByPlayer(p_75145_1_);
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

				if ((p_82846_2_ < 0 || p_82846_2_ > 2) && p_82846_2_ != 3)
				{
					if (!this.theSlot.HasStack && this.theSlot.isItemValid(var5))
					{
						if (!this.mergeItemStack(var5, 3, 4, false))
						{
							return null;
						}
					}
					else if (ContainerBrewingStand.Potion.canHoldPotion(var3))
					{
						if (!this.mergeItemStack(var5, 0, 3, false))
						{
							return null;
						}
					}
					else if (p_82846_2_ >= 4 && p_82846_2_ < 31)
					{
						if (!this.mergeItemStack(var5, 31, 40, false))
						{
							return null;
						}
					}
					else if (p_82846_2_ >= 31 && p_82846_2_ < 40)
					{
						if (!this.mergeItemStack(var5, 4, 31, false))
						{
							return null;
						}
					}
					else if (!this.mergeItemStack(var5, 4, 40, false))
					{
						return null;
					}
				}
				else
				{
					if (!this.mergeItemStack(var5, 4, 40, true))
					{
						return null;
					}

					var4.onSlotChange(var5, var3);
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

		internal class Ingredient : Slot
		{
			

			public Ingredient(IInventory p_i1803_2_, int p_i1803_3_, int p_i1803_4_, int p_i1803_5_) : base(p_i1803_2_, p_i1803_3_, p_i1803_4_, p_i1803_5_)
			{
			}

			public override bool isItemValid(ItemStack p_75214_1_)
			{
				return p_75214_1_ != null ? p_75214_1_.Item.isPotionIngredient(p_75214_1_) : false;
			}

			public override int SlotStackLimit
			{
				get
				{
					return 64;
				}
			}
		}

		internal class Potion : Slot
		{
			private EntityPlayer player;
			

			public Potion(EntityPlayer p_i1804_1_, IInventory p_i1804_2_, int p_i1804_3_, int p_i1804_4_, int p_i1804_5_) : base(p_i1804_2_, p_i1804_3_, p_i1804_4_, p_i1804_5_)
			{
				this.player = p_i1804_1_;
			}

			public override bool isItemValid(ItemStack p_75214_1_)
			{
				return canHoldPotion(p_75214_1_);
			}

			public override int SlotStackLimit
			{
				get
				{
					return 1;
				}
			}

			public override void onPickupFromSlot(EntityPlayer p_82870_1_, ItemStack p_82870_2_)
			{
				if (p_82870_2_.Item == Items.potionitem && p_82870_2_.ItemDamage > 0)
				{
					this.player.addStat(AchievementList.potion, 1);
				}

				base.onPickupFromSlot(p_82870_1_, p_82870_2_);
			}

			public static bool canHoldPotion(ItemStack p_75243_0_)
			{
				return p_75243_0_ != null && (p_75243_0_.Item == Items.potionitem || p_75243_0_.Item == Items.glass_bottle);
			}
		}
	}

}