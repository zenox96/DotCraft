using System;
using System.Collections;

namespace DotCraftCore.Inventory
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using InventoryPlayer = DotCraftCore.Entity.Player.InventoryPlayer;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using MathHelper = DotCraftCore.Util.MathHelper;

	public abstract class Container
	{
	/// <summary> the list of all items(stacks) for the corresponding slot  </summary>
		public IList inventoryItemStacks = new ArrayList();

	/// <summary> the list of all slots in the inventory  </summary>
		public IList inventorySlots = new ArrayList();
		public int windowId;
		private short transactionID;
		private int field_94535_f = -1;
		private int field_94536_g;
		private readonly Set field_94537_h = new HashSet();

///    
///     <summary> * list of all people that need to be notified when this craftinventory changes </summary>
///     
		protected internal IList crafters = new ArrayList();
		private Set playerList = new HashSet();
		private const string __OBFID = "CL_00001730";

///    
///     <summary> * the slot is assumed empty </summary>
///     
		protected internal virtual Slot addSlotToContainer(Slot p_75146_1_)
		{
			p_75146_1_.slotNumber = this.inventorySlots.Count;
			this.inventorySlots.Add(p_75146_1_);
			this.inventoryItemStacks.Add((object)null);
			return p_75146_1_;
		}

		public virtual void addCraftingToCrafters(ICrafting p_75132_1_)
		{
			if (this.crafters.Contains(p_75132_1_))
			{
				throw new System.ArgumentException("Listener already listening");
			}
			else
			{
				this.crafters.Add(p_75132_1_);
				p_75132_1_.sendContainerAndContentsToPlayer(this, this.Inventory);
				this.detectAndSendChanges();
			}
		}

///    
///     <summary> * Remove this crafting listener from the listener list. </summary>
///     
		public virtual void removeCraftingFromCrafters(ICrafting p_82847_1_)
		{
			this.crafters.Remove(p_82847_1_);
		}

///    
///     <summary> * returns a list if itemStacks, for each slot. </summary>
///     
		public virtual IList Inventory
		{
			get
			{
				ArrayList var1 = new ArrayList();
	
				for (int var2 = 0; var2 < this.inventorySlots.Count; ++var2)
				{
					var1.Add(((Slot)this.inventorySlots.get(var2)).Stack);
				}
	
				return var1;
			}
		}

///    
///     <summary> * Looks for changes made in the container, sends them to every listener. </summary>
///     
		public virtual void detectAndSendChanges()
		{
			for (int var1 = 0; var1 < this.inventorySlots.Count; ++var1)
			{
				ItemStack var2 = ((Slot)this.inventorySlots.get(var1)).Stack;
				ItemStack var3 = (ItemStack)this.inventoryItemStacks.get(var1);

				if (!ItemStack.areItemStacksEqual(var3, var2))
				{
					var3 = var2 == null ? null : var2.copy();
					this.inventoryItemStacks[var1] = var3;

					for (int var4 = 0; var4 < this.crafters.Count; ++var4)
					{
						((ICrafting)this.crafters.get(var4)).sendSlotContents(this, var1, var3);
					}
				}
			}
		}

///    
///     <summary> * enchants the item on the table using the specified slot; also deducts XP from player </summary>
///     
		public virtual bool enchantItem(EntityPlayer p_75140_1_, int p_75140_2_)
		{
			return false;
		}

		public virtual Slot getSlotFromInventory(IInventory p_75147_1_, int p_75147_2_)
		{
			for (int var3 = 0; var3 < this.inventorySlots.Count; ++var3)
			{
				Slot var4 = (Slot)this.inventorySlots.get(var3);

				if (var4.isSlotInInventory(p_75147_1_, p_75147_2_))
				{
					return var4;
				}
			}

			return null;
		}

		public virtual Slot getSlot(int p_75139_1_)
		{
			return (Slot)this.inventorySlots.get(p_75139_1_);
		}

///    
///     <summary> * Called when a player shift-clicks on a slot. You must override this or you will crash when someone does that. </summary>
///     
		public virtual ItemStack transferStackInSlot(EntityPlayer p_82846_1_, int p_82846_2_)
		{
			Slot var3 = (Slot)this.inventorySlots.get(p_82846_2_);
			return var3 != null ? var3.Stack : null;
		}

		public virtual ItemStack slotClick(int p_75144_1_, int p_75144_2_, int p_75144_3_, EntityPlayer p_75144_4_)
		{
			ItemStack var5 = null;
			InventoryPlayer var6 = p_75144_4_.inventory;
			int var9;
			ItemStack var17;

			if (p_75144_3_ == 5)
			{
				int var7 = this.field_94536_g;
				this.field_94536_g = func_94532_c(p_75144_2_);

				if ((var7 != 1 || this.field_94536_g != 2) && var7 != this.field_94536_g)
				{
					this.func_94533_d();
				}
				else if (var6.ItemStack == null)
				{
					this.func_94533_d();
				}
				else if (this.field_94536_g == 0)
				{
					this.field_94535_f = func_94529_b(p_75144_2_);

					if (func_94528_d(this.field_94535_f))
					{
						this.field_94536_g = 1;
						this.field_94537_h.clear();
					}
					else
					{
						this.func_94533_d();
					}
				}
				else if (this.field_94536_g == 1)
				{
					Slot var8 = (Slot)this.inventorySlots.get(p_75144_1_);

					if (var8 != null && func_94527_a(var8, var6.ItemStack, true) && var8.isItemValid(var6.ItemStack) && var6.ItemStack.stackSize > this.field_94537_h.size() && this.canDragIntoSlot(var8))
					{
						this.field_94537_h.add(var8);
					}
				}
				else if (this.field_94536_g == 2)
				{
					if (!this.field_94537_h.Empty)
					{
						var17 = var6.ItemStack.copy();
						var9 = var6.ItemStack.stackSize;
						IEnumerator var10 = this.field_94537_h.GetEnumerator();

						while (var10.MoveNext())
						{
							Slot var11 = (Slot)var10.Current;

							if (var11 != null && func_94527_a(var11, var6.ItemStack, true) && var11.isItemValid(var6.ItemStack) && var6.ItemStack.stackSize >= this.field_94537_h.size() && this.canDragIntoSlot(var11))
							{
								ItemStack var12 = var17.copy();
								int var13 = var11.HasStack ? var11.Stack.stackSize : 0;
								func_94525_a(this.field_94537_h, this.field_94535_f, var12, var13);

								if (var12.stackSize > var12.MaxStackSize)
								{
									var12.stackSize = var12.MaxStackSize;
								}

								if (var12.stackSize > var11.SlotStackLimit)
								{
									var12.stackSize = var11.SlotStackLimit;
								}

								var9 -= var12.stackSize - var13;
								var11.putStack(var12);
							}
						}

						var17.stackSize = var9;

						if (var17.stackSize <= 0)
						{
							var17 = null;
						}

						var6.ItemStack = var17;
					}

					this.func_94533_d();
				}
				else
				{
					this.func_94533_d();
				}
			}
			else if (this.field_94536_g != 0)
			{
				this.func_94533_d();
			}
			else
			{
				Slot var16;
				int var21;
				ItemStack var23;

				if ((p_75144_3_ == 0 || p_75144_3_ == 1) && (p_75144_2_ == 0 || p_75144_2_ == 1))
				{
					if (p_75144_1_ == -999)
					{
						if (var6.ItemStack != null && p_75144_1_ == -999)
						{
							if (p_75144_2_ == 0)
							{
								p_75144_4_.dropPlayerItemWithRandomChoice(var6.ItemStack, true);
								var6.ItemStack = (ItemStack)null;
							}

							if (p_75144_2_ == 1)
							{
								p_75144_4_.dropPlayerItemWithRandomChoice(var6.ItemStack.splitStack(1), true);

								if (var6.ItemStack.stackSize == 0)
								{
									var6.ItemStack = (ItemStack)null;
								}
							}
						}
					}
					else if (p_75144_3_ == 1)
					{
						if (p_75144_1_ < 0)
						{
							return null;
						}

						var16 = (Slot)this.inventorySlots.get(p_75144_1_);

						if (var16 != null && var16.canTakeStack(p_75144_4_))
						{
							var17 = this.transferStackInSlot(p_75144_4_, p_75144_1_);

							if (var17 != null)
							{
								Item var19 = var17.Item;
								var5 = var17.copy();

								if (var16.Stack != null && var16.Stack.Item == var19)
								{
									this.retrySlotClick(p_75144_1_, p_75144_2_, true, p_75144_4_);
								}
							}
						}
					}
					else
					{
						if (p_75144_1_ < 0)
						{
							return null;
						}

						var16 = (Slot)this.inventorySlots.get(p_75144_1_);

						if (var16 != null)
						{
							var17 = var16.Stack;
							ItemStack var20 = var6.ItemStack;

							if (var17 != null)
							{
								var5 = var17.copy();
							}

							if (var17 == null)
							{
								if (var20 != null && var16.isItemValid(var20))
								{
									var21 = p_75144_2_ == 0 ? var20.stackSize : 1;

									if (var21 > var16.SlotStackLimit)
									{
										var21 = var16.SlotStackLimit;
									}

									if (var20.stackSize >= var21)
									{
										var16.putStack(var20.splitStack(var21));
									}

									if (var20.stackSize == 0)
									{
										var6.ItemStack = (ItemStack)null;
									}
								}
							}
							else if (var16.canTakeStack(p_75144_4_))
							{
								if (var20 == null)
								{
									var21 = p_75144_2_ == 0 ? var17.stackSize : (var17.stackSize + 1) / 2;
									var23 = var16.decrStackSize(var21);
									var6.ItemStack = var23;

									if (var17.stackSize == 0)
									{
										var16.putStack((ItemStack)null);
									}

									var16.onPickupFromSlot(p_75144_4_, var6.ItemStack);
								}
								else if (var16.isItemValid(var20))
								{
									if (var17.Item == var20.Item && var17.ItemDamage == var20.ItemDamage && ItemStack.areItemStackTagsEqual(var17, var20))
									{
										var21 = p_75144_2_ == 0 ? var20.stackSize : 1;

										if (var21 > var16.SlotStackLimit - var17.stackSize)
										{
											var21 = var16.SlotStackLimit - var17.stackSize;
										}

										if (var21 > var20.MaxStackSize - var17.stackSize)
										{
											var21 = var20.MaxStackSize - var17.stackSize;
										}

										var20.splitStack(var21);

										if (var20.stackSize == 0)
										{
											var6.ItemStack = (ItemStack)null;
										}

										var17.stackSize += var21;
									}
									else if (var20.stackSize <= var16.SlotStackLimit)
									{
										var16.putStack(var20);
										var6.ItemStack = var17;
									}
								}
								else if (var17.Item == var20.Item && var20.MaxStackSize > 1 && (!var17.HasSubtypes || var17.ItemDamage == var20.ItemDamage) && ItemStack.areItemStackTagsEqual(var17, var20))
								{
									var21 = var17.stackSize;

									if (var21 > 0 && var21 + var20.stackSize <= var20.MaxStackSize)
									{
										var20.stackSize += var21;
										var17 = var16.decrStackSize(var21);

										if (var17.stackSize == 0)
										{
											var16.putStack((ItemStack)null);
										}

										var16.onPickupFromSlot(p_75144_4_, var6.ItemStack);
									}
								}
							}

							var16.onSlotChanged();
						}
					}
				}
				else if (p_75144_3_ == 2 && p_75144_2_ >= 0 && p_75144_2_ < 9)
				{
					var16 = (Slot)this.inventorySlots.get(p_75144_1_);

					if (var16.canTakeStack(p_75144_4_))
					{
						var17 = var6.getStackInSlot(p_75144_2_);
						bool var18 = var17 == null || var16.inventory == var6 && var16.isItemValid(var17);
						var21 = -1;

						if (!var18)
						{
							var21 = var6.FirstEmptyStack;
							var18 |= var21 > -1;
						}

						if (var16.HasStack && var18)
						{
							var23 = var16.Stack;
							var6.setInventorySlotContents(p_75144_2_, var23.copy());

							if ((var16.inventory != var6 || !var16.isItemValid(var17)) && var17 != null)
							{
								if (var21 > -1)
								{
									var6.addItemStackToInventory(var17);
									var16.decrStackSize(var23.stackSize);
									var16.putStack((ItemStack)null);
									var16.onPickupFromSlot(p_75144_4_, var23);
								}
							}
							else
							{
								var16.decrStackSize(var23.stackSize);
								var16.putStack(var17);
								var16.onPickupFromSlot(p_75144_4_, var23);
							}
						}
						else if (!var16.HasStack && var17 != null && var16.isItemValid(var17))
						{
							var6.setInventorySlotContents(p_75144_2_, (ItemStack)null);
							var16.putStack(var17);
						}
					}
				}
				else if (p_75144_3_ == 3 && p_75144_4_.capabilities.isCreativeMode && var6.ItemStack == null && p_75144_1_ >= 0)
				{
					var16 = (Slot)this.inventorySlots.get(p_75144_1_);

					if (var16 != null && var16.HasStack)
					{
						var17 = var16.Stack.copy();
						var17.stackSize = var17.MaxStackSize;
						var6.ItemStack = var17;
					}
				}
				else if (p_75144_3_ == 4 && var6.ItemStack == null && p_75144_1_ >= 0)
				{
					var16 = (Slot)this.inventorySlots.get(p_75144_1_);

					if (var16 != null && var16.HasStack && var16.canTakeStack(p_75144_4_))
					{
						var17 = var16.decrStackSize(p_75144_2_ == 0 ? 1 : var16.Stack.stackSize);
						var16.onPickupFromSlot(p_75144_4_, var17);
						p_75144_4_.dropPlayerItemWithRandomChoice(var17, true);
					}
				}
				else if (p_75144_3_ == 6 && p_75144_1_ >= 0)
				{
					var16 = (Slot)this.inventorySlots.get(p_75144_1_);
					var17 = var6.ItemStack;

					if (var17 != null && (var16 == null || !var16.HasStack || !var16.canTakeStack(p_75144_4_)))
					{
						var9 = p_75144_2_ == 0 ? 0 : this.inventorySlots.Count - 1;
						var21 = p_75144_2_ == 0 ? 1 : -1;

						for (int var22 = 0; var22 < 2; ++var22)
						{
							for (int var24 = var9; var24 >= 0 && var24 < this.inventorySlots.Count && var17.stackSize < var17.MaxStackSize; var24 += var21)
							{
								Slot var25 = (Slot)this.inventorySlots.get(var24);

								if (var25.HasStack && func_94527_a(var25, var17, true) && var25.canTakeStack(p_75144_4_) && this.func_94530_a(var17, var25) && (var22 != 0 || var25.Stack.stackSize != var25.Stack.MaxStackSize))
								{
									int var14 = Math.Min(var17.MaxStackSize - var17.stackSize, var25.Stack.stackSize);
									ItemStack var15 = var25.decrStackSize(var14);
									var17.stackSize += var14;

									if (var15.stackSize <= 0)
									{
										var25.putStack((ItemStack)null);
									}

									var25.onPickupFromSlot(p_75144_4_, var15);
								}
							}
						}
					}

					this.detectAndSendChanges();
				}
			}

			return var5;
		}

		public virtual bool func_94530_a(ItemStack p_94530_1_, Slot p_94530_2_)
		{
			return true;
		}

		protected internal virtual void retrySlotClick(int p_75133_1_, int p_75133_2_, bool p_75133_3_, EntityPlayer p_75133_4_)
		{
			this.slotClick(p_75133_1_, p_75133_2_, 1, p_75133_4_);
		}

///    
///     <summary> * Called when the container is closed. </summary>
///     
		public virtual void onContainerClosed(EntityPlayer p_75134_1_)
		{
			InventoryPlayer var2 = p_75134_1_.inventory;

			if (var2.ItemStack != null)
			{
				p_75134_1_.dropPlayerItemWithRandomChoice(var2.ItemStack, false);
				var2.ItemStack = (ItemStack)null;
			}
		}

///    
///     <summary> * Callback for when the crafting matrix is changed. </summary>
///     
		public virtual void onCraftMatrixChanged(IInventory p_75130_1_)
		{
			this.detectAndSendChanges();
		}

///    
///     <summary> * args: slotID, itemStack to put in slot </summary>
///     
		public virtual void putStackInSlot(int p_75141_1_, ItemStack p_75141_2_)
		{
			this.getSlot(p_75141_1_).putStack(p_75141_2_);
		}

///    
///     <summary> * places itemstacks in first x slots, x being aitemstack.lenght </summary>
///     
		public virtual void putStacksInSlots(ItemStack[] p_75131_1_)
		{
			for (int var2 = 0; var2 < p_75131_1_.Length; ++var2)
			{
				this.getSlot(var2).putStack(p_75131_1_[var2]);
			}
		}

		public virtual void updateProgressBar(int p_75137_1_, int p_75137_2_)
		{
		}

///    
///     <summary> * Gets a unique transaction ID. Parameter is unused. </summary>
///     
		public virtual short getNextTransactionID(InventoryPlayer p_75136_1_)
		{
			++this.transactionID;
			return this.transactionID;
		}

///    
///     <summary> * NotUsing because adding a player twice is an error </summary>
///     
		public virtual bool isPlayerNotUsingContainer(EntityPlayer p_75129_1_)
		{
			return !this.playerList.contains(p_75129_1_);
		}

///    
///     <summary> * adds or removes the player from the container based on par2 </summary>
///     
		public virtual void setPlayerIsPresent(EntityPlayer p_75128_1_, bool p_75128_2_)
		{
			if (p_75128_2_)
			{
				this.playerList.remove(p_75128_1_);
			}
			else
			{
				this.playerList.add(p_75128_1_);
			}
		}

		public abstract bool canInteractWith(EntityPlayer p_75145_1_);

///    
///     <summary> * merges provided ItemStack with the first avaliable one in the container/player inventory </summary>
///     
		protected internal virtual bool mergeItemStack(ItemStack p_75135_1_, int p_75135_2_, int p_75135_3_, bool p_75135_4_)
		{
			bool var5 = false;
			int var6 = p_75135_2_;

			if (p_75135_4_)
			{
				var6 = p_75135_3_ - 1;
			}

			Slot var7;
			ItemStack var8;

			if (p_75135_1_.Stackable)
			{
				while (p_75135_1_.stackSize > 0 && (!p_75135_4_ && var6 < p_75135_3_ || p_75135_4_ && var6 >= p_75135_2_))
				{
					var7 = (Slot)this.inventorySlots.get(var6);
					var8 = var7.Stack;

					if (var8 != null && var8.Item == p_75135_1_.Item && (!p_75135_1_.HasSubtypes || p_75135_1_.ItemDamage == var8.ItemDamage) && ItemStack.areItemStackTagsEqual(p_75135_1_, var8))
					{
						int var9 = var8.stackSize + p_75135_1_.stackSize;

						if (var9 <= p_75135_1_.MaxStackSize)
						{
							p_75135_1_.stackSize = 0;
							var8.stackSize = var9;
							var7.onSlotChanged();
							var5 = true;
						}
						else if (var8.stackSize < p_75135_1_.MaxStackSize)
						{
							p_75135_1_.stackSize -= p_75135_1_.MaxStackSize - var8.stackSize;
							var8.stackSize = p_75135_1_.MaxStackSize;
							var7.onSlotChanged();
							var5 = true;
						}
					}

					if (p_75135_4_)
					{
						--var6;
					}
					else
					{
						++var6;
					}
				}
			}

			if (p_75135_1_.stackSize > 0)
			{
				if (p_75135_4_)
				{
					var6 = p_75135_3_ - 1;
				}
				else
				{
					var6 = p_75135_2_;
				}

				while (!p_75135_4_ && var6 < p_75135_3_ || p_75135_4_ && var6 >= p_75135_2_)
				{
					var7 = (Slot)this.inventorySlots.get(var6);
					var8 = var7.Stack;

					if (var8 == null)
					{
						var7.putStack(p_75135_1_.copy());
						var7.onSlotChanged();
						p_75135_1_.stackSize = 0;
						var5 = true;
						break;
					}

					if (p_75135_4_)
					{
						--var6;
					}
					else
					{
						++var6;
					}
				}
			}

			return var5;
		}

		public static int func_94529_b(int p_94529_0_)
		{
			return p_94529_0_ >> 2 & 3;
		}

		public static int func_94532_c(int p_94532_0_)
		{
			return p_94532_0_ & 3;
		}

		public static int func_94534_d(int p_94534_0_, int p_94534_1_)
		{
			return p_94534_0_ & 3 | (p_94534_1_ & 3) << 2;
		}

		public static bool func_94528_d(int p_94528_0_)
		{
			return p_94528_0_ == 0 || p_94528_0_ == 1;
		}

		protected internal virtual void func_94533_d()
		{
			this.field_94536_g = 0;
			this.field_94537_h.clear();
		}

		public static bool func_94527_a(Slot p_94527_0_, ItemStack p_94527_1_, bool p_94527_2_)
		{
			bool var3 = p_94527_0_ == null || !p_94527_0_.HasStack;

			if (p_94527_0_ != null && p_94527_0_.HasStack && p_94527_1_ != null && p_94527_1_.isItemEqual(p_94527_0_.Stack) && ItemStack.areItemStackTagsEqual(p_94527_0_.Stack, p_94527_1_))
			{
				int var10002 = p_94527_2_ ? 0 : p_94527_1_.stackSize;
				var3 |= p_94527_0_.Stack.stackSize + var10002 <= p_94527_1_.MaxStackSize;
			}

			return var3;
		}

		public static void func_94525_a(Set p_94525_0_, int p_94525_1_, ItemStack p_94525_2_, int p_94525_3_)
		{
			switch (p_94525_1_)
			{
				case 0:
					p_94525_2_.stackSize = MathHelper.floor_float((float)p_94525_2_.stackSize / (float)p_94525_0_.size());
					break;

				case 1:
					p_94525_2_.stackSize = 1;
				break;
			}

			p_94525_2_.stackSize += p_94525_3_;
		}

///    
///     <summary> * Returns true if the player can "drag-spilt" items into this slot,. returns true by default. Called to check if
///     * the slot can be added to a list of Slots to split the held ItemStack across. </summary>
///     
		public virtual bool canDragIntoSlot(Slot p_94531_1_)
		{
			return true;
		}

		public static int calcRedstoneFromInventory(IInventory p_94526_0_)
		{
			if (p_94526_0_ == null)
			{
				return 0;
			}
			else
			{
				int var1 = 0;
				float var2 = 0.0F;

				for (int var3 = 0; var3 < p_94526_0_.SizeInventory; ++var3)
				{
					ItemStack var4 = p_94526_0_.getStackInSlot(var3);

					if (var4 != null)
					{
						var2 += (float)var4.stackSize / (float)Math.Min(p_94526_0_.InventoryStackLimit, var4.MaxStackSize);
						++var1;
					}
				}

				var2 /= (float)p_94526_0_.SizeInventory;
				return MathHelper.floor_float(var2 * 14.0F) + (var1 > 0 ? 1 : 0);
			}
		}
	}

}