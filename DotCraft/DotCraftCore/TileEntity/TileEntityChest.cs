using System.Collections;

namespace DotCraftCore.TileEntity
{

	using Block = DotCraftCore.block.Block;
	using BlockChest = DotCraftCore.block.BlockChest;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using ContainerChest = DotCraftCore.inventory.ContainerChest;
	using IInventory = DotCraftCore.inventory.IInventory;
	using InventoryLargeChest = DotCraftCore.inventory.InventoryLargeChest;
	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using NBTTagList = DotCraftCore.nbt.NBTTagList;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;

	public class TileEntityChest : TileEntity, IInventory
	{
		private ItemStack[] field_145985_p = new ItemStack[36];
		public bool field_145984_a;
		public TileEntityChest field_145992_i;
		public TileEntityChest field_145990_j;
		public TileEntityChest field_145991_k;
		public TileEntityChest field_145988_l;
		public float field_145989_m;
		public float field_145986_n;
		public int field_145987_o;
		private int field_145983_q;
		private int field_145982_r;
		private string field_145981_s;
		

		public TileEntityChest()
		{
			this.field_145982_r = -1;
		}

		public TileEntityChest(int p_i2350_1_)
		{
			this.field_145982_r = p_i2350_1_;
		}

///    
///     <summary> * Returns the number of slots in the inventory. </summary>
///     
		public virtual int SizeInventory
		{
			get
			{
				return 27;
			}
		}

///    
///     <summary> * Returns the stack in slot i </summary>
///     
		public virtual ItemStack getStackInSlot(int p_70301_1_)
		{
			return this.field_145985_p[p_70301_1_];
		}

///    
///     <summary> * Removes from an inventory slot (first arg) up to a specified number (second arg) of items and returns them in a
///     * new stack. </summary>
///     
		public virtual ItemStack decrStackSize(int p_70298_1_, int p_70298_2_)
		{
			if(this.field_145985_p[p_70298_1_] != null)
			{
				ItemStack var3;

				if(this.field_145985_p[p_70298_1_].stackSize <= p_70298_2_)
				{
					var3 = this.field_145985_p[p_70298_1_];
					this.field_145985_p[p_70298_1_] = null;
					this.onInventoryChanged();
					return var3;
				}
				else
				{
					var3 = this.field_145985_p[p_70298_1_].splitStack(p_70298_2_);

					if(this.field_145985_p[p_70298_1_].stackSize == 0)
					{
						this.field_145985_p[p_70298_1_] = null;
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
			if(this.field_145985_p[p_70304_1_] != null)
			{
				ItemStack var2 = this.field_145985_p[p_70304_1_];
				this.field_145985_p[p_70304_1_] = null;
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
			this.field_145985_p[p_70299_1_] = p_70299_2_;

			if(p_70299_2_ != null && p_70299_2_.stackSize > this.InventoryStackLimit)
			{
				p_70299_2_.stackSize = this.InventoryStackLimit;
			}

			this.onInventoryChanged();
		}

///    
///     <summary> * Returns the name of the inventory </summary>
///     
		public virtual string InventoryName
		{
			get
			{
				return this.InventoryNameLocalized ? this.field_145981_s : "container.chest";
			}
		}

///    
///     <summary> * Returns if the inventory name is localized </summary>
///     
		public virtual bool isInventoryNameLocalized()
		{
			get
			{
				return this.field_145981_s != null && this.field_145981_s.Length > 0;
			}
		}

		public virtual void func_145976_a(string p_145976_1_)
		{
			this.field_145981_s = p_145976_1_;
		}

		public override void readFromNBT(NBTTagCompound p_145839_1_)
		{
			base.readFromNBT(p_145839_1_);
			NBTTagList var2 = p_145839_1_.getTagList("Items", 10);
			this.field_145985_p = new ItemStack[this.SizeInventory];

			if(p_145839_1_.func_150297_b("CustomName", 8))
			{
				this.field_145981_s = p_145839_1_.getString("CustomName");
			}

			for(int var3 = 0; var3 < var2.tagCount(); ++var3)
			{
				NBTTagCompound var4 = var2.getCompoundTagAt(var3);
				int var5 = var4.getByte("Slot") & 255;

				if(var5 >= 0 && var5 < this.field_145985_p.Length)
				{
					this.field_145985_p[var5] = ItemStack.loadItemStackFromNBT(var4);
				}
			}
		}

		public override void writeToNBT(NBTTagCompound p_145841_1_)
		{
			base.writeToNBT(p_145841_1_);
			NBTTagList var2 = new NBTTagList();

			for(int var3 = 0; var3 < this.field_145985_p.Length; ++var3)
			{
				if(this.field_145985_p[var3] != null)
				{
					NBTTagCompound var4 = new NBTTagCompound();
					var4.setByte("Slot", (sbyte)var3);
					this.field_145985_p[var3].writeToNBT(var4);
					var2.appendTag(var4);
				}
			}

			p_145841_1_.setTag("Items", var2);

			if(this.InventoryNameLocalized)
			{
				p_145841_1_.setString("CustomName", this.field_145981_s);
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

		public override void updateContainingBlockInfo()
		{
			base.updateContainingBlockInfo();
			this.field_145984_a = false;
		}

		private void func_145978_a(TileEntityChest p_145978_1_, int p_145978_2_)
		{
			if(p_145978_1_.Invalid)
			{
				this.field_145984_a = false;
			}
			else if(this.field_145984_a)
			{
				switch (p_145978_2_)
				{
					case 0:
						if(this.field_145988_l != p_145978_1_)
						{
							this.field_145984_a = false;
						}

						break;

					case 1:
						if(this.field_145991_k != p_145978_1_)
						{
							this.field_145984_a = false;
						}

						break;

					case 2:
						if(this.field_145992_i != p_145978_1_)
						{
							this.field_145984_a = false;
						}

						break;

					case 3:
						if(this.field_145990_j != p_145978_1_)
						{
							this.field_145984_a = false;
						}
					break;
				}
			}
		}

		public virtual void func_145979_i()
		{
			if(!this.field_145984_a)
			{
				this.field_145984_a = true;
				this.field_145992_i = null;
				this.field_145990_j = null;
				this.field_145991_k = null;
				this.field_145988_l = null;

				if(this.func_145977_a(this.field_145851_c - 1, this.field_145848_d, this.field_145849_e))
				{
					this.field_145991_k = (TileEntityChest)this.worldObj.getTileEntity(this.field_145851_c - 1, this.field_145848_d, this.field_145849_e);
				}

				if(this.func_145977_a(this.field_145851_c + 1, this.field_145848_d, this.field_145849_e))
				{
					this.field_145990_j = (TileEntityChest)this.worldObj.getTileEntity(this.field_145851_c + 1, this.field_145848_d, this.field_145849_e);
				}

				if(this.func_145977_a(this.field_145851_c, this.field_145848_d, this.field_145849_e - 1))
				{
					this.field_145992_i = (TileEntityChest)this.worldObj.getTileEntity(this.field_145851_c, this.field_145848_d, this.field_145849_e - 1);
				}

				if(this.func_145977_a(this.field_145851_c, this.field_145848_d, this.field_145849_e + 1))
				{
					this.field_145988_l = (TileEntityChest)this.worldObj.getTileEntity(this.field_145851_c, this.field_145848_d, this.field_145849_e + 1);
				}

				if(this.field_145992_i != null)
				{
					this.field_145992_i.func_145978_a(this, 0);
				}

				if(this.field_145988_l != null)
				{
					this.field_145988_l.func_145978_a(this, 2);
				}

				if(this.field_145990_j != null)
				{
					this.field_145990_j.func_145978_a(this, 1);
				}

				if(this.field_145991_k != null)
				{
					this.field_145991_k.func_145978_a(this, 3);
				}
			}
		}

		private bool func_145977_a(int p_145977_1_, int p_145977_2_, int p_145977_3_)
		{
			if(this.worldObj == null)
			{
				return false;
			}
			else
			{
				Block var4 = this.worldObj.getBlock(p_145977_1_, p_145977_2_, p_145977_3_);
				return var4 is BlockChest && ((BlockChest)var4).field_149956_a == this.func_145980_j();
			}
		}

		public override void updateEntity()
		{
			base.updateEntity();
			this.func_145979_i();
			++this.field_145983_q;
			float var1;

			if(!this.worldObj.isClient && this.field_145987_o != 0 && (this.field_145983_q + this.field_145851_c + this.field_145848_d + this.field_145849_e) % 200 == 0)
			{
				this.field_145987_o = 0;
				var1 = 5.0F;
				IList var2 = this.worldObj.getEntitiesWithinAABB(typeof(EntityPlayer), AxisAlignedBB.getBoundingBox((double)((float)this.field_145851_c - var1), (double)((float)this.field_145848_d - var1), (double)((float)this.field_145849_e - var1), (double)((float)(this.field_145851_c + 1) + var1), (double)((float)(this.field_145848_d + 1) + var1), (double)((float)(this.field_145849_e + 1) + var1)));
				IEnumerator var3 = var2.GetEnumerator();

				while(var3.MoveNext())
				{
					EntityPlayer var4 = (EntityPlayer)var3.Current;

					if(var4.openContainer is ContainerChest)
					{
						IInventory var5 = ((ContainerChest)var4.openContainer).LowerChestInventory;

						if(var5 == this || var5 is InventoryLargeChest && ((InventoryLargeChest)var5).isPartOfLargeChest(this))
						{
							++this.field_145987_o;
						}
					}
				}
			}

			this.field_145986_n = this.field_145989_m;
			var1 = 0.1F;
			double var11;

			if(this.field_145987_o > 0 && this.field_145989_m == 0.0F && this.field_145992_i == null && this.field_145991_k == null)
			{
				double var8 = (double)this.field_145851_c + 0.5D;
				var11 = (double)this.field_145849_e + 0.5D;

				if(this.field_145988_l != null)
				{
					var11 += 0.5D;
				}

				if(this.field_145990_j != null)
				{
					var8 += 0.5D;
				}

				this.worldObj.playSoundEffect(var8, (double)this.field_145848_d + 0.5D, var11, "random.chestopen", 0.5F, this.worldObj.rand.nextFloat() * 0.1F + 0.9F);
			}

			if(this.field_145987_o == 0 && this.field_145989_m > 0.0F || this.field_145987_o > 0 && this.field_145989_m < 1.0F)
			{
				float var9 = this.field_145989_m;

				if(this.field_145987_o > 0)
				{
					this.field_145989_m += var1;
				}
				else
				{
					this.field_145989_m -= var1;
				}

				if(this.field_145989_m > 1.0F)
				{
					this.field_145989_m = 1.0F;
				}

				float var10 = 0.5F;

				if(this.field_145989_m < var10 && var9 >= var10 && this.field_145992_i == null && this.field_145991_k == null)
				{
					var11 = (double)this.field_145851_c + 0.5D;
					double var6 = (double)this.field_145849_e + 0.5D;

					if(this.field_145988_l != null)
					{
						var6 += 0.5D;
					}

					if(this.field_145990_j != null)
					{
						var11 += 0.5D;
					}

					this.worldObj.playSoundEffect(var11, (double)this.field_145848_d + 0.5D, var6, "random.chestclosed", 0.5F, this.worldObj.rand.nextFloat() * 0.1F + 0.9F);
				}

				if(this.field_145989_m < 0.0F)
				{
					this.field_145989_m = 0.0F;
				}
			}
		}

		public override bool receiveClientEvent(int p_145842_1_, int p_145842_2_)
		{
			if(p_145842_1_ == 1)
			{
				this.field_145987_o = p_145842_2_;
				return true;
			}
			else
			{
				return base.receiveClientEvent(p_145842_1_, p_145842_2_);
			}
		}

		public virtual void openInventory()
		{
			if(this.field_145987_o < 0)
			{
				this.field_145987_o = 0;
			}

			++this.field_145987_o;
			this.worldObj.func_147452_c(this.field_145851_c, this.field_145848_d, this.field_145849_e, this.BlockType, 1, this.field_145987_o);
			this.worldObj.notifyBlocksOfNeighborChange(this.field_145851_c, this.field_145848_d, this.field_145849_e, this.BlockType);
			this.worldObj.notifyBlocksOfNeighborChange(this.field_145851_c, this.field_145848_d - 1, this.field_145849_e, this.BlockType);
		}

		public virtual void closeInventory()
		{
			if(this.BlockType is BlockChest)
			{
				--this.field_145987_o;
				this.worldObj.func_147452_c(this.field_145851_c, this.field_145848_d, this.field_145849_e, this.BlockType, 1, this.field_145987_o);
				this.worldObj.notifyBlocksOfNeighborChange(this.field_145851_c, this.field_145848_d, this.field_145849_e, this.BlockType);
				this.worldObj.notifyBlocksOfNeighborChange(this.field_145851_c, this.field_145848_d - 1, this.field_145849_e, this.BlockType);
			}
		}

///    
///     <summary> * Returns true if automation is allowed to insert the given stack (ignoring stack size) into the given slot. </summary>
///     
		public virtual bool isItemValidForSlot(int p_94041_1_, ItemStack p_94041_2_)
		{
			return true;
		}

///    
///     <summary> * invalidates a tile entity </summary>
///     
		public override void invalidate()
		{
			base.invalidate();
			this.updateContainingBlockInfo();
			this.func_145979_i();
		}

		public virtual int func_145980_j()
		{
			if(this.field_145982_r == -1)
			{
				if(this.worldObj == null || !(this.BlockType is BlockChest))
				{
					return 0;
				}

				this.field_145982_r = ((BlockChest)this.BlockType).field_149956_a;
			}

			return this.field_145982_r;
		}
	}

}