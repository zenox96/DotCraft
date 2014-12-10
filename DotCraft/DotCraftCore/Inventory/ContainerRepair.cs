using System;
using System.Collections;

namespace DotCraftCore.Inventory
{

	using Enchantment = DotCraftCore.Enchantment.Enchantment;
	using EnchantmentHelper = DotCraftCore.Enchantment.EnchantmentHelper;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using InventoryPlayer = DotCraftCore.Entity.Player.InventoryPlayer;
	using Blocks = DotCraftCore.Init.Blocks;
	using Items = DotCraftCore.Init.Items;
	using ItemStack = DotCraftCore.item.ItemStack;
	using World = DotCraftCore.world.World;
	using StringUtils = org.apache.commons.lang3.StringUtils;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class ContainerRepair : Container
	{
		private static readonly Logger logger = LogManager.Logger;

	/// <summary> Here comes out item you merged and/or renamed.  </summary>
		private IInventory outputSlot = new InventoryCraftResult();

///    
///     <summary> * The 2slots where you put your items in that you want to merge and/or rename. </summary>
///     
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		private IInventory inputSlots = new InventoryBasic("Repair", true, 2)
//	{
//		private static final String __OBFID = "CL_00001733";
//		public void onInventoryChanged()
//		{
//			base.onInventoryChanged();
//			ContainerRepair.onCraftMatrixChanged(this);
//		}
//	};
		private World theWorld;
		private int field_82861_i;
		private int field_82858_j;
		private int field_82859_k;

	/// <summary> The maximum cost of repairing/renaming in the anvil.  </summary>
		public int maximumCost;

	/// <summary> determined by damage of input item and stackSize of repair materials  </summary>
		private int stackSizeToBeUsedInRepair;
		private string repairedItemName;

	/// <summary> The player that has this container open.  </summary>
		private readonly EntityPlayer thePlayer;
		private const string __OBFID = "CL_00001732";

//JAVA TO VB & C# CONVERTER WARNING: 'final' parameters are not allowed in .NET:
//ORIGINAL LINE: public ContainerRepair(InventoryPlayer p_i1800_1_, final World p_i1800_2_, final int p_i1800_3_, final int p_i1800_4_, final int p_i1800_5_, EntityPlayer p_i1800_6_)
		public ContainerRepair(InventoryPlayer p_i1800_1_, World p_i1800_2_, int p_i1800_3_, int p_i1800_4_, int p_i1800_5_, EntityPlayer p_i1800_6_)
		{
			this.theWorld = p_i1800_2_;
			this.field_82861_i = p_i1800_3_;
			this.field_82858_j = p_i1800_4_;
			this.field_82859_k = p_i1800_5_;
			this.thePlayer = p_i1800_6_;
			this.addSlotToContainer(new Slot(this.inputSlots, 0, 27, 47));
			this.addSlotToContainer(new Slot(this.inputSlots, 1, 76, 47));
			this.addSlotToContainer(new Slot(this.outputSlot, 2, 134, 47) { private static final string __OBFID = "CL_00001734"; public bool isItemValid(ItemStack p_75214_1_) { return false; } public bool canTakeStack(EntityPlayer p_82869_1_) { return (p_82869_1_.capabilities.isCreativeMode || p_82869_1_.experienceLevel >= ContainerRepair.maximumCost) && ContainerRepair.maximumCost > 0 && this.HasStack; } public void onPickupFromSlot(EntityPlayer p_82870_1_, ItemStack p_82870_2_) { if (!p_82870_1_.capabilities.isCreativeMode) { p_82870_1_.addExperienceLevel(-ContainerRepair.maximumCost); } ContainerRepair.inputSlots.setInventorySlotContents(0, (ItemStack)null); if (ContainerRepair.stackSizeToBeUsedInRepair > 0) { ItemStack var3 = ContainerRepair.inputSlots.getStackInSlot(1); if (var3 != null && var3.stackSize > ContainerRepair.stackSizeToBeUsedInRepair) { var3.stackSize -= ContainerRepair.stackSizeToBeUsedInRepair; ContainerRepair.inputSlots.setInventorySlotContents(1, var3); } else { ContainerRepair.inputSlots.setInventorySlotContents(1, (ItemStack)null); } } else { ContainerRepair.inputSlots.setInventorySlotContents(1, (ItemStack)null); } ContainerRepair.maximumCost = 0; if (!p_82870_1_.capabilities.isCreativeMode && !p_i1800_2_.isClient && p_i1800_2_.getBlock(p_i1800_3_, p_i1800_4_, p_i1800_5_) == Blocks.anvil && p_82870_1_.RNG.nextFloat() < 0.12F) { int var6 = p_i1800_2_.getBlockMetadata(p_i1800_3_, p_i1800_4_, p_i1800_5_); int var4 = var6 & 3; int var5 = var6 >> 2; ++var5; if (var5 > 2) { p_i1800_2_.setBlockToAir(p_i1800_3_, p_i1800_4_, p_i1800_5_); p_i1800_2_.playAuxSFX(1020, p_i1800_3_, p_i1800_4_, p_i1800_5_, 0); } else { p_i1800_2_.setBlockMetadataWithNotify(p_i1800_3_, p_i1800_4_, p_i1800_5_, var4 | var5 << 2, 2); p_i1800_2_.playAuxSFX(1021, p_i1800_3_, p_i1800_4_, p_i1800_5_, 0); } } else if (!p_i1800_2_.isClient) { p_i1800_2_.playAuxSFX(1021, p_i1800_3_, p_i1800_4_, p_i1800_5_, 0); } } });
			int var7;

			for (var7 = 0; var7 < 3; ++var7)
			{
				for (int var8 = 0; var8 < 9; ++var8)
				{
					this.addSlotToContainer(new Slot(p_i1800_1_, var8 + var7 * 9 + 9, 8 + var8 * 18, 84 + var7 * 18));
				}
			}

			for (var7 = 0; var7 < 9; ++var7)
			{
				this.addSlotToContainer(new Slot(p_i1800_1_, var7, 8 + var7 * 18, 142));
			}
		}

///    
///     <summary> * Callback for when the crafting matrix is changed. </summary>
///     
		public override void onCraftMatrixChanged(IInventory p_75130_1_)
		{
			base.onCraftMatrixChanged(p_75130_1_);

			if (p_75130_1_ == this.inputSlots)
			{
				this.updateRepairOutput();
			}
		}

///    
///     <summary> * called when the Anvil Input Slot changes, calculates the new result and puts it in the output slot </summary>
///     
		public virtual void updateRepairOutput()
		{
			ItemStack var1 = this.inputSlots.getStackInSlot(0);
			this.maximumCost = 0;
			int var2 = 0;
			sbyte var3 = 0;
			int var4 = 0;

			if (var1 == null)
			{
				this.outputSlot.setInventorySlotContents(0, (ItemStack)null);
				this.maximumCost = 0;
			}
			else
			{
				ItemStack var5 = var1.copy();
				ItemStack var6 = this.inputSlots.getStackInSlot(1);
				IDictionary var7 = EnchantmentHelper.getEnchantments(var5);
				bool var8 = false;
				int var19 = var3 + var1.RepairCost + (var6 == null ? 0 : var6.RepairCost);
				this.stackSizeToBeUsedInRepair = 0;
				int var9;
				int var10;
				int var11;
				int var13;
				int var14;
				IEnumerator var21;
				Enchantment var22;

				if (var6 != null)
				{
					var8 = var6.Item == Items.enchanted_book && Items.enchanted_book.func_92110_g(var6).tagCount() > 0;

					if (var5.ItemStackDamageable && var5.Item.getIsRepairable(var1, var6))
					{
						var9 = Math.Min(var5.ItemDamageForDisplay, var5.MaxDamage / 4);

						if (var9 <= 0)
						{
							this.outputSlot.setInventorySlotContents(0, (ItemStack)null);
							this.maximumCost = 0;
							return;
						}

						for (var10 = 0; var9 > 0 && var10 < var6.stackSize; ++var10)
						{
							var11 = var5.ItemDamageForDisplay - var9;
							var5.ItemDamage = var11;
							var2 += Math.Max(1, var9 / 100) + var7.Count;
							var9 = Math.Min(var5.ItemDamageForDisplay, var5.MaxDamage / 4);
						}

						this.stackSizeToBeUsedInRepair = var10;
					}
					else
					{
						if (!var8 && (var5.Item != var6.Item || !var5.ItemStackDamageable))
						{
							this.outputSlot.setInventorySlotContents(0, (ItemStack)null);
							this.maximumCost = 0;
							return;
						}

						if (var5.ItemStackDamageable && !var8)
						{
							var9 = var1.MaxDamage - var1.ItemDamageForDisplay;
							var10 = var6.MaxDamage - var6.ItemDamageForDisplay;
							var11 = var10 + var5.MaxDamage * 12 / 100;
							int var12 = var9 + var11;
							var13 = var5.MaxDamage - var12;

							if (var13 < 0)
							{
								var13 = 0;
							}

							if (var13 < var5.ItemDamage)
							{
								var5.ItemDamage = var13;
								var2 += Math.Max(1, var11 / 100);
							}
						}

						IDictionary var20 = EnchantmentHelper.getEnchantments(var6);
						var21 = var20.Keys.GetEnumerator();

						while (var21.MoveNext())
						{
							var11 = (int)((int?)var21.Current);
							var22 = Enchantment.enchantmentsList[var11];
							var13 = var7.ContainsKey(Convert.ToInt32(var11)) ? (int)((int?)var7[Convert.ToInt32(var11)]) : 0;
							var14 = (int)((int?)var20[Convert.ToInt32(var11)]);
							int var10000;

							if (var13 == var14)
							{
								++var14;
								var10000 = var14;
							}
							else
							{
								var10000 = Math.Max(var14, var13);
							}

							var14 = var10000;
							int var15 = var14 - var13;
							bool var16 = var22.canApply(var1);

							if (this.thePlayer.capabilities.isCreativeMode || var1.Item == Items.enchanted_book)
							{
								var16 = true;
							}

							IEnumerator var17 = var7.Keys.GetEnumerator();

							while (var17.MoveNext())
							{
								int var18 = (int)((int?)var17.Current);

								if (var18 != var11 && !var22.canApplyTogether(Enchantment.enchantmentsList[var18]))
								{
									var16 = false;
									var2 += var15;
								}
							}

							if (var16)
							{
								if (var14 > var22.MaxLevel)
								{
									var14 = var22.MaxLevel;
								}

								var7.Add(Convert.ToInt32(var11), Convert.ToInt32(var14));
								int var23 = 0;

								switch (var22.Weight)
								{
									case 1:
										var23 = 8;
										break;

									case 2:
										var23 = 4;

									goto case 3;
									case 3:
									case 4:
									case 6:
									case 7:
									case 8:
									case 9:
									default:
										break;

									case 5:
										var23 = 2;
										break;

									case 10:
										var23 = 1;
									break;
								}

								if (var8)
								{
									var23 = Math.Max(1, var23 / 2);
								}

								var2 += var23 * var15;
							}
						}
					}
				}

				if (StringUtils.isBlank(this.repairedItemName))
				{
					if (var1.hasDisplayName())
					{
						var4 = var1.ItemStackDamageable ? 7 : var1.stackSize * 5;
						var2 += var4;
						var5.func_135074_t();
					}
				}
				else if (!this.repairedItemName.Equals(var1.DisplayName))
				{
					var4 = var1.ItemStackDamageable ? 7 : var1.stackSize * 5;
					var2 += var4;

					if (var1.hasDisplayName())
					{
						var19 += var4 / 2;
					}

					var5.StackDisplayName = this.repairedItemName;
				}

				var9 = 0;

				for (var21 = var7.Keys.GetEnumerator(); var21.MoveNext(); var19 += var9 + var13 * var14)
				{
					var11 = (int)((int?)var21.Current);
					var22 = Enchantment.enchantmentsList[var11];
					var13 = (int)((int?)var7[Convert.ToInt32(var11)]);
					var14 = 0;
					++var9;

					switch (var22.Weight)
					{
						case 1:
							var14 = 8;
							break;

						case 2:
							var14 = 4;

						goto case 3;
						case 3:
						case 4:
						case 6:
						case 7:
						case 8:
						case 9:
						default:
							break;

						case 5:
							var14 = 2;
							break;

						case 10:
							var14 = 1;
						break;
					}

					if (var8)
					{
						var14 = Math.Max(1, var14 / 2);
					}
				}

				if (var8)
				{
					var19 = Math.Max(1, var19 / 2);
				}

				this.maximumCost = var19 + var2;

				if (var2 <= 0)
				{
					var5 = null;
				}

				if (var4 == var2 && var4 > 0 && this.maximumCost >= 40)
				{
					this.maximumCost = 39;
				}

				if (this.maximumCost >= 40 && !this.thePlayer.capabilities.isCreativeMode)
				{
					var5 = null;
				}

				if (var5 != null)
				{
					var10 = var5.RepairCost;

					if (var6 != null && var10 < var6.RepairCost)
					{
						var10 = var6.RepairCost;
					}

					if (var5.hasDisplayName())
					{
						var10 -= 9;
					}

					if (var10 < 0)
					{
						var10 = 0;
					}

					var10 += 2;
					var5.RepairCost = var10;
					EnchantmentHelper.setEnchantments(var7, var5);
				}

				this.outputSlot.setInventorySlotContents(0, var5);
				this.detectAndSendChanges();
			}
		}

		public override void addCraftingToCrafters(ICrafting p_75132_1_)
		{
			base.addCraftingToCrafters(p_75132_1_);
			p_75132_1_.sendProgressBarUpdate(this, 0, this.maximumCost);
		}

		public override void updateProgressBar(int p_75137_1_, int p_75137_2_)
		{
			if (p_75137_1_ == 0)
			{
				this.maximumCost = p_75137_2_;
			}
		}

///    
///     <summary> * Called when the container is closed. </summary>
///     
		public override void onContainerClosed(EntityPlayer p_75134_1_)
		{
			base.onContainerClosed(p_75134_1_);

			if (!this.theWorld.isClient)
			{
				for (int var2 = 0; var2 < this.inputSlots.SizeInventory; ++var2)
				{
					ItemStack var3 = this.inputSlots.getStackInSlotOnClosing(var2);

					if (var3 != null)
					{
						p_75134_1_.dropPlayerItemWithRandomChoice(var3, false);
					}
				}
			}
		}

		public override bool canInteractWith(EntityPlayer p_75145_1_)
		{
			return this.theWorld.getBlock(this.field_82861_i, this.field_82858_j, this.field_82859_k) != Blocks.anvil ? false : p_75145_1_.getDistanceSq((double)this.field_82861_i + 0.5D, (double)this.field_82858_j + 0.5D, (double)this.field_82859_k + 0.5D) <= 64.0D;
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
					if (p_82846_2_ >= 3 && p_82846_2_ < 39 && !this.mergeItemStack(var5, 0, 2, false))
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
///     <summary> * used by the Anvil GUI to update the Item Name being typed by the player </summary>
///     
		public virtual void updateItemName(string p_82850_1_)
		{
			this.repairedItemName = p_82850_1_;

			if (this.getSlot(2).HasStack)
			{
				ItemStack var2 = this.getSlot(2).Stack;

				if (StringUtils.isBlank(p_82850_1_))
				{
					var2.func_135074_t();
				}
				else
				{
					var2.StackDisplayName = this.repairedItemName;
				}
			}

			this.updateRepairOutput();
		}
	}

}