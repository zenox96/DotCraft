using System;

namespace DotCraftCore.Entity.Player
{

	using Block = DotCraftCore.block.Block;
	using CrashReport = DotCraftCore.crash.CrashReport;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using IInventory = DotCraftCore.Inventory.IInventory;
	using Item = DotCraftCore.Item.Item;
	using ItemArmor = DotCraftCore.Item.ItemArmor;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using NBTTagList = DotCraftCore.NBT.NBTTagList;
	using ReportedException = DotCraftCore.Util.ReportedException;

	public class InventoryPlayer : IInventory
	{
///    
///     <summary> * An array of 36 item stacks indicating the main player inventory (including the visible bar). </summary>
///     
		public ItemStack[] mainInventory = new ItemStack[36];

	/// <summary> An array of 4 item stacks containing the currently worn armor pieces.  </summary>
		public ItemStack[] armorInventory = new ItemStack[4];

	/// <summary> The index of the currently held item (0-8).  </summary>
		public int currentItem;

	/// <summary> The current ItemStack.  </summary>
		private ItemStack currentItemStack;

	/// <summary> The player whose inventory this is.  </summary>
		public EntityPlayer player;
		private ItemStack itemStack;

///    
///     <summary> * Set true whenever the inventory changes. Nothing sets it false so you will have to write your own code to check
///     * it and reset the value. </summary>
///     
		public bool inventoryChanged;
		private const string __OBFID = "CL_00001709";

		public InventoryPlayer(EntityPlayer p_i1750_1_)
		{
			this.player = p_i1750_1_;
		}

///    
///     <summary> * Returns the item stack currently held by the player. </summary>
///     
		public virtual ItemStack CurrentItem
		{
			get
			{
				return this.currentItem < 9 && this.currentItem >= 0 ? this.mainInventory[this.currentItem] : null;
			}
		}

///    
///     <summary> * Get the size of the player hotbar inventory </summary>
///     
		public static int HotbarSize
		{
			get
			{
				return 9;
			}
		}

		private int func_146029_c(Item p_146029_1_)
		{
			for (int var2 = 0; var2 < this.mainInventory.Length; ++var2)
			{
				if (this.mainInventory[var2] != null && this.mainInventory[var2].Item == p_146029_1_)
				{
					return var2;
				}
			}

			return -1;
		}

		private int func_146024_c(Item p_146024_1_, int p_146024_2_)
		{
			for (int var3 = 0; var3 < this.mainInventory.Length; ++var3)
			{
				if (this.mainInventory[var3] != null && this.mainInventory[var3].Item == p_146024_1_ && this.mainInventory[var3].ItemDamage == p_146024_2_)
				{
					return var3;
				}
			}

			return -1;
		}

///    
///     <summary> * stores an itemstack in the users inventory </summary>
///     
		private int storeItemStack(ItemStack p_70432_1_)
		{
			for (int var2 = 0; var2 < this.mainInventory.Length; ++var2)
			{
				if (this.mainInventory[var2] != null && this.mainInventory[var2].Item == p_70432_1_.Item && this.mainInventory[var2].Stackable && this.mainInventory[var2].stackSize < this.mainInventory[var2].MaxStackSize && this.mainInventory[var2].stackSize < this.InventoryStackLimit && (!this.mainInventory[var2].HasSubtypes || this.mainInventory[var2].ItemDamage == p_70432_1_.ItemDamage) && ItemStack.areItemStackTagsEqual(this.mainInventory[var2], p_70432_1_))
				{
					return var2;
				}
			}

			return -1;
		}

///    
///     <summary> * Returns the first item stack that is empty. </summary>
///     
		public virtual int FirstEmptyStack
		{
			get
			{
				for (int var1 = 0; var1 < this.mainInventory.Length; ++var1)
				{
					if (this.mainInventory[var1] == null)
					{
						return var1;
					}
				}
	
				return -1;
			}
		}

		public virtual void func_146030_a(Item p_146030_1_, int p_146030_2_, bool p_146030_3_, bool p_146030_4_)
		{
			bool var5 = true;
			this.currentItemStack = this.CurrentItem;
			int var7;

			if (p_146030_3_)
			{
				var7 = this.func_146024_c(p_146030_1_, p_146030_2_);
			}
			else
			{
				var7 = this.func_146029_c(p_146030_1_);
			}

			if (var7 >= 0 && var7 < 9)
			{
				this.currentItem = var7;
			}
			else
			{
				if (p_146030_4_ && p_146030_1_ != null)
				{
					int var6 = this.FirstEmptyStack;

					if (var6 >= 0 && var6 < 9)
					{
						this.currentItem = var6;
					}

					this.func_70439_a(p_146030_1_, p_146030_2_);
				}
			}
		}

///    
///     <summary> * Switch the current item to the next one or the previous one </summary>
///     
		public virtual void changeCurrentItem(int p_70453_1_)
		{
			if (p_70453_1_ > 0)
			{
				p_70453_1_ = 1;
			}

			if (p_70453_1_ < 0)
			{
				p_70453_1_ = -1;
			}

			for (this.currentItem -= p_70453_1_; this.currentItem < 0; this.currentItem += 9)
			{
				;
			}

			while (this.currentItem >= 9)
			{
				this.currentItem -= 9;
			}
		}

///    
///     <summary> * Removes all items from player inventory, including armor </summary>
///     
		public virtual int clearInventory(Item p_146027_1_, int p_146027_2_)
		{
			int var3 = 0;
			int var4;
			ItemStack var5;

			for (var4 = 0; var4 < this.mainInventory.Length; ++var4)
			{
				var5 = this.mainInventory[var4];

				if (var5 != null && (p_146027_1_ == null || var5.Item == p_146027_1_) && (p_146027_2_ <= -1 || var5.ItemDamage == p_146027_2_))
				{
					var3 += var5.stackSize;
					this.mainInventory[var4] = null;
				}
			}

			for (var4 = 0; var4 < this.armorInventory.Length; ++var4)
			{
				var5 = this.armorInventory[var4];

				if (var5 != null && (p_146027_1_ == null || var5.Item == p_146027_1_) && (p_146027_2_ <= -1 || var5.ItemDamage == p_146027_2_))
				{
					var3 += var5.stackSize;
					this.armorInventory[var4] = null;
				}
			}

			if (this.itemStack != null)
			{
				if (p_146027_1_ != null && this.itemStack.Item != p_146027_1_)
				{
					return var3;
				}

				if (p_146027_2_ > -1 && this.itemStack.ItemDamage != p_146027_2_)
				{
					return var3;
				}

				var3 += this.itemStack.stackSize;
				this.ItemStack = (ItemStack)null;
			}

			return var3;
		}

		public virtual void func_70439_a(Item p_70439_1_, int p_70439_2_)
		{
			if (p_70439_1_ != null)
			{
				if (this.currentItemStack != null && this.currentItemStack.ItemEnchantable && this.func_146024_c(this.currentItemStack.Item, this.currentItemStack.ItemDamageForDisplay) == this.currentItem)
				{
					return;
				}

				int var3 = this.func_146024_c(p_70439_1_, p_70439_2_);

				if (var3 >= 0)
				{
					int var4 = this.mainInventory[var3].stackSize;
					this.mainInventory[var3] = this.mainInventory[this.currentItem];
					this.mainInventory[this.currentItem] = new ItemStack(p_70439_1_, var4, p_70439_2_);
				}
				else
				{
					this.mainInventory[this.currentItem] = new ItemStack(p_70439_1_, 1, p_70439_2_);
				}
			}
		}

///    
///     <summary> * This function stores as many items of an ItemStack as possible in a matching slot and returns the quantity of
///     * left over items. </summary>
///     
		private int storePartialItemStack(ItemStack p_70452_1_)
		{
			Item var2 = p_70452_1_.Item;
			int var3 = p_70452_1_.stackSize;
			int var4;

			if (p_70452_1_.MaxStackSize == 1)
			{
				var4 = this.FirstEmptyStack;

				if (var4 < 0)
				{
					return var3;
				}
				else
				{
					if (this.mainInventory[var4] == null)
					{
						this.mainInventory[var4] = ItemStack.copyItemStack(p_70452_1_);
					}

					return 0;
				}
			}
			else
			{
				var4 = this.storeItemStack(p_70452_1_);

				if (var4 < 0)
				{
					var4 = this.FirstEmptyStack;
				}

				if (var4 < 0)
				{
					return var3;
				}
				else
				{
					if (this.mainInventory[var4] == null)
					{
						this.mainInventory[var4] = new ItemStack(var2, 0, p_70452_1_.ItemDamage);

						if (p_70452_1_.hasTagCompound())
						{
							this.mainInventory[var4].TagCompound = (NBTTagCompound)p_70452_1_.TagCompound.copy();
						}
					}

					int var5 = var3;

					if (var3 > this.mainInventory[var4].MaxStackSize - this.mainInventory[var4].stackSize)
					{
						var5 = this.mainInventory[var4].MaxStackSize - this.mainInventory[var4].stackSize;
					}

					if (var5 > this.InventoryStackLimit - this.mainInventory[var4].stackSize)
					{
						var5 = this.InventoryStackLimit - this.mainInventory[var4].stackSize;
					}

					if (var5 == 0)
					{
						return var3;
					}
					else
					{
						var3 -= var5;
						this.mainInventory[var4].stackSize += var5;
						this.mainInventory[var4].animationsToGo = 5;
						return var3;
					}
				}
			}
		}

///    
///     <summary> * Decrement the number of animations remaining. Only called on client side. This is used to handle the animation of
///     * receiving a block. </summary>
///     
		public virtual void decrementAnimations()
		{
			for (int var1 = 0; var1 < this.mainInventory.Length; ++var1)
			{
				if (this.mainInventory[var1] != null)
				{
					this.mainInventory[var1].updateAnimation(this.player.worldObj, this.player, var1, this.currentItem == var1);
				}
			}
		}

		public virtual bool consumeInventoryItem(Item p_146026_1_)
		{
			int var2 = this.func_146029_c(p_146026_1_);

			if (var2 < 0)
			{
				return false;
			}
			else
			{
				if (--this.mainInventory[var2].stackSize <= 0)
				{
					this.mainInventory[var2] = null;
				}

				return true;
			}
		}

		public virtual bool hasItem(Item p_146028_1_)
		{
			int var2 = this.func_146029_c(p_146028_1_);
			return var2 >= 0;
		}

///    
///     <summary> * Adds the item stack to the inventory, returns false if it is impossible. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: 'final' parameters are not allowed in .NET:
//ORIGINAL LINE: public boolean addItemStackToInventory(final ItemStack p_70441_1_)
		public virtual bool addItemStackToInventory(ItemStack p_70441_1_)
		{
			if (p_70441_1_ != null && p_70441_1_.stackSize != 0 && p_70441_1_.Item != null)
			{
				try
				{
					int var2;

					if (p_70441_1_.ItemDamaged)
					{
						var2 = this.FirstEmptyStack;

						if (var2 >= 0)
						{
							this.mainInventory[var2] = ItemStack.copyItemStack(p_70441_1_);
							this.mainInventory[var2].animationsToGo = 5;
							p_70441_1_.stackSize = 0;
							return true;
						}
						else if (this.player.capabilities.isCreativeMode)
						{
							p_70441_1_.stackSize = 0;
							return true;
						}
						else
						{
							return false;
						}
					}
					else
					{
						do
						{
							var2 = p_70441_1_.stackSize;
							p_70441_1_.stackSize = this.storePartialItemStack(p_70441_1_);
						}
						while (p_70441_1_.stackSize > 0 && p_70441_1_.stackSize < var2);

						if (p_70441_1_.stackSize == var2 && this.player.capabilities.isCreativeMode)
						{
							p_70441_1_.stackSize = 0;
							return true;
						}
						else
						{
							return p_70441_1_.stackSize < var2;
						}
					}
				}
				catch (Exception var5)
				{
					CrashReport var3 = CrashReport.makeCrashReport(var5, "Adding item to inventory");
					CrashReportCategory var4 = var3.makeCategory("Item being added");
					var4.addCrashSection("Item ID", Convert.ToInt32(Item.getIdFromItem(p_70441_1_.Item)));
					var4.addCrashSection("Item data", Convert.ToInt32(p_70441_1_.ItemDamage));
					var4.addCrashSectionCallable("Item name", new Callable() { private static final string __OBFID = "CL_00001710"; public string call() { return p_70441_1_.DisplayName; } });
					throw new ReportedException(var3);
				}
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Removes from an inventory slot (first arg) up to a specified number (second arg) of items and returns them in a
///     * new stack. </summary>
///     
		public virtual ItemStack decrStackSize(int p_70298_1_, int p_70298_2_)
		{
			ItemStack[] var3 = this.mainInventory;

			if (p_70298_1_ >= this.mainInventory.Length)
			{
				var3 = this.armorInventory;
				p_70298_1_ -= this.mainInventory.Length;
			}

			if (var3[p_70298_1_] != null)
			{
				ItemStack var4;

				if (var3[p_70298_1_].stackSize <= p_70298_2_)
				{
					var4 = var3[p_70298_1_];
					var3[p_70298_1_] = null;
					return var4;
				}
				else
				{
					var4 = var3[p_70298_1_].splitStack(p_70298_2_);

					if (var3[p_70298_1_].stackSize == 0)
					{
						var3[p_70298_1_] = null;
					}

					return var4;
				}
			}
			else
			{
				return null;
			}
		}

///    
///     <summary> * When some containers are closed they call this on each slot, then drop whatever it returns as an EntityItem -
///     * like when you close a workbench GUI. </summary>
///     
		public virtual ItemStack getStackInSlotOnClosing(int p_70304_1_)
		{
			ItemStack[] var2 = this.mainInventory;

			if (p_70304_1_ >= this.mainInventory.Length)
			{
				var2 = this.armorInventory;
				p_70304_1_ -= this.mainInventory.Length;
			}

			if (var2[p_70304_1_] != null)
			{
				ItemStack var3 = var2[p_70304_1_];
				var2[p_70304_1_] = null;
				return var3;
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
			ItemStack[] var3 = this.mainInventory;

			if (p_70299_1_ >= var3.Length)
			{
				p_70299_1_ -= var3.Length;
				var3 = this.armorInventory;
			}

			var3[p_70299_1_] = p_70299_2_;
		}

		public virtual float func_146023_a(Block p_146023_1_)
		{
			float var2 = 1.0F;

			if (this.mainInventory[this.currentItem] != null)
			{
				var2 *= this.mainInventory[this.currentItem].func_150997_a(p_146023_1_);
			}

			return var2;
		}

///    
///     <summary> * Writes the inventory out as a list of compound tags. This is where the slot indices are used (+100 for armor, +80
///     * for crafting). </summary>
///     
		public virtual NBTTagList writeToNBT(NBTTagList p_70442_1_)
		{
			int var2;
			NBTTagCompound var3;

			for (var2 = 0; var2 < this.mainInventory.Length; ++var2)
			{
				if (this.mainInventory[var2] != null)
				{
					var3 = new NBTTagCompound();
					var3.setByte("Slot", (sbyte)var2);
					this.mainInventory[var2].writeToNBT(var3);
					p_70442_1_.appendTag(var3);
				}
			}

			for (var2 = 0; var2 < this.armorInventory.Length; ++var2)
			{
				if (this.armorInventory[var2] != null)
				{
					var3 = new NBTTagCompound();
					var3.setByte("Slot", (sbyte)(var2 + 100));
					this.armorInventory[var2].writeToNBT(var3);
					p_70442_1_.appendTag(var3);
				}
			}

			return p_70442_1_;
		}

///    
///     <summary> * Reads from the given tag list and fills the slots in the inventory with the correct items. </summary>
///     
		public virtual void readFromNBT(NBTTagList p_70443_1_)
		{
			this.mainInventory = new ItemStack[36];
			this.armorInventory = new ItemStack[4];

			for (int var2 = 0; var2 < p_70443_1_.tagCount(); ++var2)
			{
				NBTTagCompound var3 = p_70443_1_.getCompoundTagAt(var2);
				int var4 = var3.getByte("Slot") & 255;
				ItemStack var5 = ItemStack.loadItemStackFromNBT(var3);

				if (var5 != null)
				{
					if (var4 >= 0 && var4 < this.mainInventory.Length)
					{
						this.mainInventory[var4] = var5;
					}

					if (var4 >= 100 && var4 < this.armorInventory.Length + 100)
					{
						this.armorInventory[var4 - 100] = var5;
					}
				}
			}
		}

///    
///     <summary> * Returns the number of slots in the inventory. </summary>
///     
		public virtual int SizeInventory
		{
			get
			{
				return this.mainInventory.Length + 4;
			}
		}

///    
///     <summary> * Returns the stack in slot i </summary>
///     
		public virtual ItemStack getStackInSlot(int p_70301_1_)
		{
			ItemStack[] var2 = this.mainInventory;

			if (p_70301_1_ >= var2.Length)
			{
				p_70301_1_ -= var2.Length;
				var2 = this.armorInventory;
			}

			return var2[p_70301_1_];
		}

///    
///     <summary> * Returns the name of the inventory </summary>
///     
		public virtual string InventoryName
		{
			get
			{
				return "container.inventory";
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

		public virtual bool func_146025_b(Block p_146025_1_)
		{
			if (p_146025_1_.Material.ToolNotRequired)
			{
				return true;
			}
			else
			{
				ItemStack var2 = this.getStackInSlot(this.currentItem);
				return var2 != null ? var2.func_150998_b(p_146025_1_) : false;
			}
		}

///    
///     <summary> * returns a player armor item (as itemstack) contained in specified armor slot. </summary>
///     
		public virtual ItemStack armorItemInSlot(int p_70440_1_)
		{
			return this.armorInventory[p_70440_1_];
		}

///    
///     <summary> * Based on the damage values and maximum damage values of each armor item, returns the current armor value. </summary>
///     
		public virtual int TotalArmorValue
		{
			get
			{
				int var1 = 0;
	
				for (int var2 = 0; var2 < this.armorInventory.Length; ++var2)
				{
					if (this.armorInventory[var2] != null && this.armorInventory[var2].Item is ItemArmor)
					{
						int var3 = ((ItemArmor)this.armorInventory[var2].Item).damageReduceAmount;
						var1 += var3;
					}
				}
	
				return var1;
			}
		}

///    
///     <summary> * Damages armor in each slot by the specified amount. </summary>
///     
		public virtual void damageArmor(float p_70449_1_)
		{
			p_70449_1_ /= 4.0F;

			if (p_70449_1_ < 1.0F)
			{
				p_70449_1_ = 1.0F;
			}

			for (int var2 = 0; var2 < this.armorInventory.Length; ++var2)
			{
				if (this.armorInventory[var2] != null && this.armorInventory[var2].Item is ItemArmor)
				{
					this.armorInventory[var2].damageItem((int)p_70449_1_, this.player);

					if (this.armorInventory[var2].stackSize == 0)
					{
						this.armorInventory[var2] = null;
					}
				}
			}
		}

///    
///     <summary> * Drop all armor and main inventory items. </summary>
///     
		public virtual void dropAllItems()
		{
			int var1;

			for (var1 = 0; var1 < this.mainInventory.Length; ++var1)
			{
				if (this.mainInventory[var1] != null)
				{
					this.player.func_146097_a(this.mainInventory[var1], true, false);
					this.mainInventory[var1] = null;
				}
			}

			for (var1 = 0; var1 < this.armorInventory.Length; ++var1)
			{
				if (this.armorInventory[var1] != null)
				{
					this.player.func_146097_a(this.armorInventory[var1], true, false);
					this.armorInventory[var1] = null;
				}
			}
		}

///    
///     <summary> * Called when an the contents of an Inventory change, usually </summary>
///     
		public virtual void onInventoryChanged()
		{
			this.inventoryChanged = true;
		}

		public virtual ItemStack ItemStack
		{
			set
			{
				this.itemStack = value;
			}
			get
			{
				return this.itemStack;
			}
		}


///    
///     <summary> * Do not make give this method the name canInteractWith because it clashes with Container </summary>
///     
		public virtual bool isUseableByPlayer(EntityPlayer p_70300_1_)
		{
			return this.player.isDead ? false : p_70300_1_.getDistanceSqToEntity(this.player) <= 64.0D;
		}

///    
///     <summary> * Returns true if the specified ItemStack exists in the inventory. </summary>
///     
		public virtual bool hasItemStack(ItemStack p_70431_1_)
		{
			int var2;

			for (var2 = 0; var2 < this.armorInventory.Length; ++var2)
			{
				if (this.armorInventory[var2] != null && this.armorInventory[var2].isItemEqual(p_70431_1_))
				{
					return true;
				}
			}

			for (var2 = 0; var2 < this.mainInventory.Length; ++var2)
			{
				if (this.mainInventory[var2] != null && this.mainInventory[var2].isItemEqual(p_70431_1_))
				{
					return true;
				}
			}

			return false;
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
///     <summary> * Copy the ItemStack contents from another InventoryPlayer instance </summary>
///     
		public virtual void copyInventory(InventoryPlayer p_70455_1_)
		{
			int var2;

			for (var2 = 0; var2 < this.mainInventory.Length; ++var2)
			{
				this.mainInventory[var2] = ItemStack.copyItemStack(p_70455_1_.mainInventory[var2]);
			}

			for (var2 = 0; var2 < this.armorInventory.Length; ++var2)
			{
				this.armorInventory[var2] = ItemStack.copyItemStack(p_70455_1_.armorInventory[var2]);
			}

			this.currentItem = p_70455_1_.currentItem;
		}
	}

}