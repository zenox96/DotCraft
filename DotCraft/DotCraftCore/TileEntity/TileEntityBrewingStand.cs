using System.Collections;

namespace DotCraftCore.TileEntity
{

	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Items = DotCraftCore.init.Items;
	using ISidedInventory = DotCraftCore.inventory.ISidedInventory;
	using Item = DotCraftCore.item.Item;
	using ItemPotion = DotCraftCore.item.ItemPotion;
	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using NBTTagList = DotCraftCore.nbt.NBTTagList;
	using PotionHelper = DotCraftCore.potion.PotionHelper;

	public class TileEntityBrewingStand : TileEntity, ISidedInventory
	{
		private static readonly int[] field_145941_a = new int[] {3};
		private static readonly int[] field_145947_i = new int[] {0, 1, 2};
		private ItemStack[] field_145945_j = new ItemStack[4];
		private int field_145946_k;
		private int field_145943_l;
		private Item field_145944_m;
		private string field_145942_n;
		

///    
///     <summary> * Returns the name of the inventory </summary>
///     
		public virtual string InventoryName
		{
			get
			{
				return this.InventoryNameLocalized ? this.field_145942_n : "container.brewing";
			}
		}

///    
///     <summary> * Returns if the inventory name is localized </summary>
///     
		public virtual bool isInventoryNameLocalized()
		{
			get
			{
				return this.field_145942_n != null && this.field_145942_n.Length > 0;
			}
		}

		public virtual void func_145937_a(string p_145937_1_)
		{
			this.field_145942_n = p_145937_1_;
		}

///    
///     <summary> * Returns the number of slots in the inventory. </summary>
///     
		public virtual int SizeInventory
		{
			get
			{
				return this.field_145945_j.Length;
			}
		}

		public override void updateEntity()
		{
			if(this.field_145946_k > 0)
			{
				--this.field_145946_k;

				if(this.field_145946_k == 0)
				{
					this.func_145940_l();
					this.onInventoryChanged();
				}
				else if(!this.func_145934_k())
				{
					this.field_145946_k = 0;
					this.onInventoryChanged();
				}
				else if(this.field_145944_m != this.field_145945_j[3].Item)
				{
					this.field_145946_k = 0;
					this.onInventoryChanged();
				}
			}
			else if(this.func_145934_k())
			{
				this.field_145946_k = 400;
				this.field_145944_m = this.field_145945_j[3].Item;
			}

			int var1 = this.func_145939_j();

			if(var1 != this.field_145943_l)
			{
				this.field_145943_l = var1;
				this.worldObj.setBlockMetadataWithNotify(this.field_145851_c, this.field_145848_d, this.field_145849_e, var1, 2);
			}

			base.updateEntity();
		}

		public virtual int func_145935_i()
		{
			return this.field_145946_k;
		}

		private bool func_145934_k()
		{
			if(this.field_145945_j[3] != null && this.field_145945_j[3].stackSize > 0)
			{
				ItemStack var1 = this.field_145945_j[3];

				if(!var1.Item.isPotionIngredient(var1))
				{
					return false;
				}
				else
				{
					bool var2 = false;

					for(int var3 = 0; var3 < 3; ++var3)
					{
						if(this.field_145945_j[var3] != null && this.field_145945_j[var3].Item == Items.potionitem)
						{
							int var4 = this.field_145945_j[var3].ItemDamage;
							int var5 = this.func_145936_c(var4, var1);

							if(!ItemPotion.isSplash(var4) && ItemPotion.isSplash(var5))
							{
								var2 = true;
								break;
							}

							IList var6 = Items.potionitem.getEffects(var4);
							IList var7 = Items.potionitem.getEffects(var5);

							if((var4 <= 0 || var6 != var7) && (var6 == null || !var6.Equals(var7) && var7 != null) && var4 != var5)
							{
								var2 = true;
								break;
							}
						}
					}

					return var2;
				}
			}
			else
			{
				return false;
			}
		}

		private void func_145940_l()
		{
			if(this.func_145934_k())
			{
				ItemStack var1 = this.field_145945_j[3];

				for(int var2 = 0; var2 < 3; ++var2)
				{
					if(this.field_145945_j[var2] != null && this.field_145945_j[var2].Item == Items.potionitem)
					{
						int var3 = this.field_145945_j[var2].ItemDamage;
						int var4 = this.func_145936_c(var3, var1);
						IList var5 = Items.potionitem.getEffects(var3);
						IList var6 = Items.potionitem.getEffects(var4);

						if((var3 <= 0 || var5 != var6) && (var5 == null || !var5.Equals(var6) && var6 != null))
						{
							if(var3 != var4)
							{
								this.field_145945_j[var2].ItemDamage = var4;
							}
						}
						else if(!ItemPotion.isSplash(var3) && ItemPotion.isSplash(var4))
						{
							this.field_145945_j[var2].ItemDamage = var4;
						}
					}
				}

				if(var1.Item.hasContainerItem())
				{
					this.field_145945_j[3] = new ItemStack(var1.Item.ContainerItem);
				}
				else
				{
					--this.field_145945_j[3].stackSize;

					if(this.field_145945_j[3].stackSize <= 0)
					{
						this.field_145945_j[3] = null;
					}
				}
			}
		}

		private int func_145936_c(int p_145936_1_, ItemStack p_145936_2_)
		{
			return p_145936_2_ == null ? p_145936_1_ : (p_145936_2_.Item.isPotionIngredient(p_145936_2_) ? PotionHelper.applyIngredient(p_145936_1_, p_145936_2_.Item.getPotionEffect(p_145936_2_)) : p_145936_1_);
		}

		public override void readFromNBT(NBTTagCompound p_145839_1_)
		{
			base.readFromNBT(p_145839_1_);
			NBTTagList var2 = p_145839_1_.getTagList("Items", 10);
			this.field_145945_j = new ItemStack[this.SizeInventory];

			for(int var3 = 0; var3 < var2.tagCount(); ++var3)
			{
				NBTTagCompound var4 = var2.getCompoundTagAt(var3);
				sbyte var5 = var4.getByte("Slot");

				if(var5 >= 0 && var5 < this.field_145945_j.Length)
				{
					this.field_145945_j[var5] = ItemStack.loadItemStackFromNBT(var4);
				}
			}

			this.field_145946_k = p_145839_1_.getShort("BrewTime");

			if(p_145839_1_.func_150297_b("CustomName", 8))
			{
				this.field_145942_n = p_145839_1_.getString("CustomName");
			}
		}

		public override void writeToNBT(NBTTagCompound p_145841_1_)
		{
			base.writeToNBT(p_145841_1_);
			p_145841_1_.setShort("BrewTime", (short)this.field_145946_k);
			NBTTagList var2 = new NBTTagList();

			for(int var3 = 0; var3 < this.field_145945_j.Length; ++var3)
			{
				if(this.field_145945_j[var3] != null)
				{
					NBTTagCompound var4 = new NBTTagCompound();
					var4.setByte("Slot", (sbyte)var3);
					this.field_145945_j[var3].writeToNBT(var4);
					var2.appendTag(var4);
				}
			}

			p_145841_1_.setTag("Items", var2);

			if(this.InventoryNameLocalized)
			{
				p_145841_1_.setString("CustomName", this.field_145942_n);
			}
		}

///    
///     <summary> * Returns the stack in slot i </summary>
///     
		public virtual ItemStack getStackInSlot(int p_70301_1_)
		{
			return p_70301_1_ >= 0 && p_70301_1_ < this.field_145945_j.Length ? this.field_145945_j[p_70301_1_] : null;
		}

///    
///     <summary> * Removes from an inventory slot (first arg) up to a specified number (second arg) of items and returns them in a
///     * new stack. </summary>
///     
		public virtual ItemStack decrStackSize(int p_70298_1_, int p_70298_2_)
		{
			if(p_70298_1_ >= 0 && p_70298_1_ < this.field_145945_j.Length)
			{
				ItemStack var3 = this.field_145945_j[p_70298_1_];
				this.field_145945_j[p_70298_1_] = null;
				return var3;
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
			if(p_70304_1_ >= 0 && p_70304_1_ < this.field_145945_j.Length)
			{
				ItemStack var2 = this.field_145945_j[p_70304_1_];
				this.field_145945_j[p_70304_1_] = null;
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
			if(p_70299_1_ >= 0 && p_70299_1_ < this.field_145945_j.Length)
			{
				this.field_145945_j[p_70299_1_] = p_70299_2_;
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
			return this.worldObj.getTileEntity(this.field_145851_c, this.field_145848_d, this.field_145849_e) != this ? false : p_70300_1_.getDistanceSq((double)this.field_145851_c + 0.5D, (double)this.field_145848_d + 0.5D, (double)this.field_145849_e + 0.5D) <= 64.0D;
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
			return p_94041_1_ == 3 ? p_94041_2_.Item.isPotionIngredient(p_94041_2_) : p_94041_2_.Item == Items.potionitem || p_94041_2_.Item == Items.glass_bottle;
		}

		public virtual void func_145938_d(int p_145938_1_)
		{
			this.field_145946_k = p_145938_1_;
		}

		public virtual int func_145939_j()
		{
			int var1 = 0;

			for(int var2 = 0; var2 < 3; ++var2)
			{
				if(this.field_145945_j[var2] != null)
				{
					var1 |= 1 << var2;
				}
			}

			return var1;
		}

///    
///     <summary> * Returns an array containing the indices of the slots that can be accessed by automation on the given side of this
///     * block. </summary>
///     
		public virtual int[] getAccessibleSlotsFromSide(int p_94128_1_)
		{
			return p_94128_1_ == 1 ? field_145941_a : field_145947_i;
		}

///    
///     <summary> * Returns true if automation can insert the given item in the given slot from the given side. Args: Slot, item,
///     * side </summary>
///     
		public virtual bool canInsertItem(int p_102007_1_, ItemStack p_102007_2_, int p_102007_3_)
		{
			return this.isItemValidForSlot(p_102007_1_, p_102007_2_);
		}

///    
///     <summary> * Returns true if automation can extract the given item in the given slot from the given side. Args: Slot, item,
///     * side </summary>
///     
		public virtual bool canExtractItem(int p_102008_1_, ItemStack p_102008_2_, int p_102008_3_)
		{
			return true;
		}
	}

}