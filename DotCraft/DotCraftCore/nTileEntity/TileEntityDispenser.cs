using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using System;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nNBT;

namespace DotCraftCore.nTileEntity
{
	public class TileEntityDispenser : TileEntity, IInventory
	{
		private ItemStack[] field_146022_i = new ItemStack[9];
		private Random field_146021_j = new Random();
		protected internal string field_146020_a;
		

///    
///     <summary> * Returns the number of slots in the inventory. </summary>
///     
		public virtual int SizeInventory
		{
			get
			{
				return 9;
			}
		}

///    
///     <summary> * Returns the stack in slot i </summary>
///     
		public virtual ItemStack getStackInSlot(int p_70301_1_)
		{
			return this.field_146022_i[p_70301_1_];
		}

///    
///     <summary> * Removes from an inventory slot (first arg) up to a specified number (second arg) of items and returns them in a
///     * new stack. </summary>
///     
		public virtual ItemStack decrStackSize(int p_70298_1_, int p_70298_2_)
		{
			if(this.field_146022_i[p_70298_1_] != null)
			{
				ItemStack var3;

				if(this.field_146022_i[p_70298_1_].stackSize <= p_70298_2_)
				{
					var3 = this.field_146022_i[p_70298_1_];
					this.field_146022_i[p_70298_1_] = null;
					this.onInventoryChanged();
					return var3;
				}
				else
				{
					var3 = this.field_146022_i[p_70298_1_].splitStack(p_70298_2_);

					if(this.field_146022_i[p_70298_1_].stackSize == 0)
					{
						this.field_146022_i[p_70298_1_] = null;
					}

					this.onInventoryChanged();
					return var3;
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
			if(this.field_146022_i[p_70304_1_] != null)
			{
				ItemStack var2 = this.field_146022_i[p_70304_1_];
				this.field_146022_i[p_70304_1_] = null;
				return var2;
			}
			else
			{
				return null;
			}
		}

		public virtual int func_146017_i()
		{
			int var1 = -1;
			int var2 = 1;

			for(int var3 = 0; var3 < this.field_146022_i.Length; ++var3)
			{
				if(this.field_146022_i[var3] != null && this.field_146021_j.Next(var2++) == 0)
				{
					var1 = var3;
				}
			}

			return var1;
		}

///    
///     <summary> * Sets the given item stack to the specified slot in the inventory (can be crafting or armor sections). </summary>
///     
		public virtual void setInventorySlotContents(int p_70299_1_, ItemStack p_70299_2_)
		{
			this.field_146022_i[p_70299_1_] = p_70299_2_;

			if(p_70299_2_ != null && p_70299_2_.stackSize > this.InventoryStackLimit)
			{
				p_70299_2_.stackSize = this.InventoryStackLimit;
			}

			this.onInventoryChanged();
		}

		public virtual int func_146019_a(ItemStack p_146019_1_)
		{
			for(int var2 = 0; var2 < this.field_146022_i.Length; ++var2)
			{
				if(this.field_146022_i[var2] == null || this.field_146022_i[var2].Item == null)
				{
					this.setInventorySlotContents(var2, p_146019_1_);
					return var2;
				}
			}

			return -1;
		}

///    
///     <summary> * Returns the name of the inventory </summary>
///     
		public virtual string InventoryName
		{
			get
			{
				return this.isInventoryNameLocalized ? this.field_146020_a : "container.dispenser";
			}
		}

		public virtual void func_146018_a(string p_146018_1_)
		{
			this.field_146020_a = p_146018_1_;
		}

///    
///     <summary> * Returns if the inventory name is localized </summary>
///     
		public virtual bool isInventoryNameLocalized
		{
			get
			{
				return this.field_146020_a != null;
			}
		}

		public override void readFromNBT(NBTTagCompound p_145839_1_)
		{
			base.readFromNBT(p_145839_1_);
			NBTTagList var2 = p_145839_1_.getTagList("Items", 10);
			this.field_146022_i = new ItemStack[this.SizeInventory];

			for(int var3 = 0; var3 < var2.tagCount(); ++var3)
			{
				NBTTagCompound var4 = var2.getCompoundTagAt(var3);
				int var5 = var4.getByte("Slot") & 255;

				if(var5 >= 0 && var5 < this.field_146022_i.Length)
				{
					this.field_146022_i[var5] = ItemStack.loadItemStackFromNBT(var4);
				}
			}

			if(p_145839_1_.func_150297_b("CustomName", 8))
			{
				this.field_146020_a = p_145839_1_.getString("CustomName");
			}
		}

		public override void writeToNBT(NBTTagCompound p_145841_1_)
		{
			base.writeToNBT(p_145841_1_);
			NBTTagList var2 = new NBTTagList();

			for(int var3 = 0; var3 < this.field_146022_i.Length; ++var3)
			{
				if(this.field_146022_i[var3] != null)
				{
					NBTTagCompound var4 = new NBTTagCompound();
					var4.setByte("Slot", (sbyte)var3);
					this.field_146022_i[var3].writeToNBT(var4);
					var2.appendTag(var4);
				}
			}

			p_145841_1_.setTag("Items", var2);

			if(this.isInventoryNameLocalized)
			{
				p_145841_1_.setString("CustomName", this.field_146020_a);
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
			return true;
		}
    }

}