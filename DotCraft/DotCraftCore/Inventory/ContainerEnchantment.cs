using System;
using System.Collections;

namespace DotCraftCore.Inventory
{

	using EnchantmentData = DotCraftCore.Enchantment.EnchantmentData;
	using EnchantmentHelper = DotCraftCore.Enchantment.EnchantmentHelper;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using InventoryPlayer = DotCraftCore.Entity.Player.InventoryPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using World = DotCraftCore.World.World;

	public class ContainerEnchantment : Container
	{
	/// <summary> SlotEnchantmentTable object with ItemStack to be enchanted  </summary>
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		public IInventory tableInventory = new InventoryBasic("Enchant", true, 1)
//	{
//		
//		public int getInventoryStackLimit()
//		{
//			return 1;
//		}
//		public void onInventoryChanged()
//		{
//			base.onInventoryChanged();
//			ContainerEnchantment.onCraftMatrixChanged(this);
//		}
//	};

	/// <summary> current world (for bookshelf counting)  </summary>
		private World worldPointer;
		private int posX;
		private int posY;
		private int posZ;
		private Random rand = new Random();

	/// <summary> used as seed for EnchantmentNameParts (see GuiEnchantment)  </summary>
		public long nameSeed;

	/// <summary> 3-member array storing the enchantment levels of each slot  </summary>
		public int[] enchantLevels = new int[3];
		

		public ContainerEnchantment(InventoryPlayer p_i1811_1_, World p_i1811_2_, int p_i1811_3_, int p_i1811_4_, int p_i1811_5_)
		{
			this.worldPointer = p_i1811_2_;
			this.posX = p_i1811_3_;
			this.posY = p_i1811_4_;
			this.posZ = p_i1811_5_;
			this.addSlotToContainer(new Slot(this.tableInventory, 0, 25, 47) {  public bool isItemValid(ItemStack p_75214_1_) { return true; } });
			int var6;

			for (var6 = 0; var6 < 3; ++var6)
			{
				for (int var7 = 0; var7 < 9; ++var7)
				{
					this.addSlotToContainer(new Slot(p_i1811_1_, var7 + var6 * 9 + 9, 8 + var7 * 18, 84 + var6 * 18));
				}
			}

			for (var6 = 0; var6 < 9; ++var6)
			{
				this.addSlotToContainer(new Slot(p_i1811_1_, var6, 8 + var6 * 18, 142));
			}
		}

		public override void addCraftingToCrafters(ICrafting p_75132_1_)
		{
			base.addCraftingToCrafters(p_75132_1_);
			p_75132_1_.sendProgressBarUpdate(this, 0, this.enchantLevels[0]);
			p_75132_1_.sendProgressBarUpdate(this, 1, this.enchantLevels[1]);
			p_75132_1_.sendProgressBarUpdate(this, 2, this.enchantLevels[2]);
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
				var2.sendProgressBarUpdate(this, 0, this.enchantLevels[0]);
				var2.sendProgressBarUpdate(this, 1, this.enchantLevels[1]);
				var2.sendProgressBarUpdate(this, 2, this.enchantLevels[2]);
			}
		}

		public override void updateProgressBar(int p_75137_1_, int p_75137_2_)
		{
			if (p_75137_1_ >= 0 && p_75137_1_ <= 2)
			{
				this.enchantLevels[p_75137_1_] = p_75137_2_;
			}
			else
			{
				base.updateProgressBar(p_75137_1_, p_75137_2_);
			}
		}

///    
///     <summary> * Callback for when the crafting matrix is changed. </summary>
///     
		public override void onCraftMatrixChanged(IInventory p_75130_1_)
		{
			if (p_75130_1_ == this.tableInventory)
			{
				ItemStack var2 = p_75130_1_.getStackInSlot(0);
				int var3;

				if (var2 != null && var2.ItemEnchantable)
				{
					this.nameSeed = this.rand.nextLong();

					if (!this.worldPointer.isClient)
					{
						var3 = 0;
						int var4;

						for (var4 = -1; var4 <= 1; ++var4)
						{
							for (int var5 = -1; var5 <= 1; ++var5)
							{
								if ((var4 != 0 || var5 != 0) && this.worldPointer.isAirBlock(this.posX + var5, this.posY, this.posZ + var4) && this.worldPointer.isAirBlock(this.posX + var5, this.posY + 1, this.posZ + var4))
								{
									if (this.worldPointer.getBlock(this.posX + var5 * 2, this.posY, this.posZ + var4 * 2) == Blocks.bookshelf)
									{
										++var3;
									}

									if (this.worldPointer.getBlock(this.posX + var5 * 2, this.posY + 1, this.posZ + var4 * 2) == Blocks.bookshelf)
									{
										++var3;
									}

									if (var5 != 0 && var4 != 0)
									{
										if (this.worldPointer.getBlock(this.posX + var5 * 2, this.posY, this.posZ + var4) == Blocks.bookshelf)
										{
											++var3;
										}

										if (this.worldPointer.getBlock(this.posX + var5 * 2, this.posY + 1, this.posZ + var4) == Blocks.bookshelf)
										{
											++var3;
										}

										if (this.worldPointer.getBlock(this.posX + var5, this.posY, this.posZ + var4 * 2) == Blocks.bookshelf)
										{
											++var3;
										}

										if (this.worldPointer.getBlock(this.posX + var5, this.posY + 1, this.posZ + var4 * 2) == Blocks.bookshelf)
										{
											++var3;
										}
									}
								}
							}
						}

						for (var4 = 0; var4 < 3; ++var4)
						{
							this.enchantLevels[var4] = EnchantmentHelper.calcItemStackEnchantability(this.rand, var4, var3, var2);
						}

						this.detectAndSendChanges();
					}
				}
				else
				{
					for (var3 = 0; var3 < 3; ++var3)
					{
						this.enchantLevels[var3] = 0;
					}
				}
			}
		}

///    
///     <summary> * enchants the item on the table using the specified slot; also deducts XP from player </summary>
///     
		public override bool enchantItem(EntityPlayer p_75140_1_, int p_75140_2_)
		{
			ItemStack var3 = this.tableInventory.getStackInSlot(0);

			if (this.enchantLevels[p_75140_2_] > 0 && var3 != null && (p_75140_1_.experienceLevel >= this.enchantLevels[p_75140_2_] || p_75140_1_.capabilities.isCreativeMode))
			{
				if (!this.worldPointer.isClient)
				{
					IList var4 = EnchantmentHelper.buildEnchantmentList(this.rand, var3, this.enchantLevels[p_75140_2_]);
					bool var5 = var3.Item == Items.book;

					if (var4 != null)
					{
						p_75140_1_.addExperienceLevel(-this.enchantLevels[p_75140_2_]);

						if (var5)
						{
							var3.func_150996_a(Items.enchanted_book);
						}

						int var6 = var5 && var4.Count > 1 ? this.rand.Next(var4.Count) : -1;

						for (int var7 = 0; var7 < var4.Count; ++var7)
						{
							EnchantmentData var8 = (EnchantmentData)var4[var7];

							if (!var5 || var7 != var6)
							{
								if (var5)
								{
									Items.enchanted_book.addEnchantment(var3, var8);
								}
								else
								{
									var3.addEnchantment(var8.enchantmentobj, var8.enchantmentLevel);
								}
							}
						}

						this.onCraftMatrixChanged(this.tableInventory);
					}
				}

				return true;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Called when the container is closed. </summary>
///     
		public override void onContainerClosed(EntityPlayer p_75134_1_)
		{
			base.onContainerClosed(p_75134_1_);

			if (!this.worldPointer.isClient)
			{
				ItemStack var2 = this.tableInventory.getStackInSlotOnClosing(0);

				if (var2 != null)
				{
					p_75134_1_.dropPlayerItemWithRandomChoice(var2, false);
				}
			}
		}

		public override bool canInteractWith(EntityPlayer p_75145_1_)
		{
			return this.worldPointer.getBlock(this.posX, this.posY, this.posZ) != Blocks.enchanting_table ? false : p_75145_1_.getDistanceSq((double)this.posX + 0.5D, (double)this.posY + 0.5D, (double)this.posZ + 0.5D) <= 64.0D;
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
					if (!this.mergeItemStack(var5, 1, 37, true))
					{
						return null;
					}
				}
				else
				{
					if (((Slot)this.inventorySlots.get(0)).HasStack || !((Slot)this.inventorySlots.get(0)).isItemValid(var5))
					{
						return null;
					}

					if (var5.hasTagCompound() && var5.stackSize == 1)
					{
						((Slot)this.inventorySlots.get(0)).putStack(var5.copy());
						var5.stackSize = 0;
					}
					else if (var5.stackSize >= 1)
					{
						((Slot)this.inventorySlots.get(0)).putStack(new ItemStack(var5.Item, 1, var5.ItemDamage));
						--var5.stackSize;
					}
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