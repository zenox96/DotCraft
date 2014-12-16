namespace DotCraftCore.Inventory
{

	using IMerchant = DotCraftCore.Entity.IMerchant;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using MerchantRecipe = DotCraftCore.Village.MerchantRecipe;
	using MerchantRecipeList = DotCraftCore.Village.MerchantRecipeList;

	public class InventoryMerchant : IInventory
	{
		private readonly IMerchant theMerchant;
		private ItemStack[] theInventory = new ItemStack[3];
		private readonly EntityPlayer thePlayer;
		private MerchantRecipe currentRecipe;
		private int currentRecipeIndex;
		

		public InventoryMerchant(EntityPlayer p_i1820_1_, IMerchant p_i1820_2_)
		{
			this.thePlayer = p_i1820_1_;
			this.theMerchant = p_i1820_2_;
		}

///    
///     <summary> * Returns the number of slots in the inventory. </summary>
///     
		public virtual int SizeInventory
		{
			get
			{
				return this.theInventory.Length;
			}
		}

///    
///     <summary> * Returns the stack in slot i </summary>
///     
		public virtual ItemStack getStackInSlot(int p_70301_1_)
		{
			return this.theInventory[p_70301_1_];
		}

///    
///     <summary> * Removes from an inventory slot (first arg) up to a specified number (second arg) of items and returns them in a
///     * new stack. </summary>
///     
		public virtual ItemStack decrStackSize(int p_70298_1_, int p_70298_2_)
		{
			if (this.theInventory[p_70298_1_] != null)
			{
				ItemStack var3;

				if (p_70298_1_ == 2)
				{
					var3 = this.theInventory[p_70298_1_];
					this.theInventory[p_70298_1_] = null;
					return var3;
				}
				else if (this.theInventory[p_70298_1_].stackSize <= p_70298_2_)
				{
					var3 = this.theInventory[p_70298_1_];
					this.theInventory[p_70298_1_] = null;

					if (this.inventoryResetNeededOnSlotChange(p_70298_1_))
					{
						this.resetRecipeAndSlots();
					}

					return var3;
				}
				else
				{
					var3 = this.theInventory[p_70298_1_].splitStack(p_70298_2_);

					if (this.theInventory[p_70298_1_].stackSize == 0)
					{
						this.theInventory[p_70298_1_] = null;
					}

					if (this.inventoryResetNeededOnSlotChange(p_70298_1_))
					{
						this.resetRecipeAndSlots();
					}

					return var3;
				}
			}
			else
			{
				return null;
			}
		}

///    
///     <summary> * if par1 slot has changed, does resetRecipeAndSlots need to be called? </summary>
///     
		private bool inventoryResetNeededOnSlotChange(int p_70469_1_)
		{
			return p_70469_1_ == 0 || p_70469_1_ == 1;
		}

///    
///     <summary> * When some containers are closed they call this on each slot, then drop whatever it returns as an EntityItem -
///     * like when you close a workbench GUI. </summary>
///     
		public virtual ItemStack getStackInSlotOnClosing(int p_70304_1_)
		{
			if (this.theInventory[p_70304_1_] != null)
			{
				ItemStack var2 = this.theInventory[p_70304_1_];
				this.theInventory[p_70304_1_] = null;
				return var2;
			}
			else
			{
				return null;
			}
		}

///    
///     <summary> * Sets the given item stack to the specified slot in the inventory (can be crafting or armor sections). </summary>
///     
		public virtual void setInventorySlotContents(int p_70299_1_, ItemStack p_70299_2_)
		{
			this.theInventory[p_70299_1_] = p_70299_2_;

			if (p_70299_2_ != null && p_70299_2_.stackSize > this.InventoryStackLimit)
			{
				p_70299_2_.stackSize = this.InventoryStackLimit;
			}

			if (this.inventoryResetNeededOnSlotChange(p_70299_1_))
			{
				this.resetRecipeAndSlots();
			}
		}

///    
///     <summary> * Returns the name of the inventory </summary>
///     
		public virtual string InventoryName
		{
			get
			{
				return "mob.villager";
			}
		}

///    
///     <summary> * Returns if the inventory name is localized </summary>
///     
		public virtual bool isInventoryNameLocalized()
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * Returns the maximum stack size for a inventory slot. </summary>
///     
		public virtual int InventoryStackLimit
		{
			get
			{
				return 64;
			}
		}

///    
///     <summary> * Do not make give this method the name canInteractWith because it clashes with Container </summary>
///     
		public virtual bool isUseableByPlayer(EntityPlayer p_70300_1_)
		{
			return this.theMerchant.Customer == p_70300_1_;
		}

		public virtual void openInventory()
		{
		}

		public virtual void closeInventory()
		{
		}

///    
///     <summary> * Returns true if automation is allowed to insert the given stack (ignoring stack size) into the given slot. </summary>
///     
		public virtual bool isItemValidForSlot(int p_94041_1_, ItemStack p_94041_2_)
		{
			return true;
		}

///    
///     <summary> * Called when an the contents of an Inventory change, usually </summary>
///     
		public virtual void onInventoryChanged()
		{
			this.resetRecipeAndSlots();
		}

		public virtual void resetRecipeAndSlots()
		{
			this.currentRecipe = null;
			ItemStack var1 = this.theInventory[0];
			ItemStack var2 = this.theInventory[1];

			if (var1 == null)
			{
				var1 = var2;
				var2 = null;
			}

			if (var1 == null)
			{
				this.setInventorySlotContents(2, (ItemStack)null);
			}
			else
			{
				MerchantRecipeList var3 = this.theMerchant.getRecipes(this.thePlayer);

				if (var3 != null)
				{
					MerchantRecipe var4 = var3.canRecipeBeUsed(var1, var2, this.currentRecipeIndex);

					if (var4 != null && !var4.RecipeDisabled)
					{
						this.currentRecipe = var4;
						this.setInventorySlotContents(2, var4.ItemToSell.copy());
					}
					else if (var2 != null)
					{
						var4 = var3.canRecipeBeUsed(var2, var1, this.currentRecipeIndex);

						if (var4 != null && !var4.RecipeDisabled)
						{
							this.currentRecipe = var4;
							this.setInventorySlotContents(2, var4.ItemToSell.copy());
						}
						else
						{
							this.setInventorySlotContents(2, (ItemStack)null);
						}
					}
					else
					{
						this.setInventorySlotContents(2, (ItemStack)null);
					}
				}
			}

			this.theMerchant.func_110297_a_(this.getStackInSlot(2));
		}

		public virtual MerchantRecipe CurrentRecipe
		{
			get
			{
				return this.currentRecipe;
			}
		}

		public virtual int CurrentRecipeIndex
		{
			set
			{
				this.currentRecipeIndex = value;
				this.resetRecipeAndSlots();
			}
		}
	}

}