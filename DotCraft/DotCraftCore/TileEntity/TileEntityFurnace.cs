namespace DotCraftCore.TileEntity
{

	using Block = DotCraftCore.block.Block;
	using BlockFurnace = DotCraftCore.block.BlockFurnace;
	using Material = DotCraftCore.block.material.Material;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using ISidedInventory = DotCraftCore.inventory.ISidedInventory;
	using Item = DotCraftCore.item.Item;
	using ItemBlock = DotCraftCore.item.ItemBlock;
	using ItemHoe = DotCraftCore.item.ItemHoe;
	using ItemStack = DotCraftCore.item.ItemStack;
	using ItemSword = DotCraftCore.item.ItemSword;
	using ItemTool = DotCraftCore.item.ItemTool;
	using FurnaceRecipes = DotCraftCore.item.crafting.FurnaceRecipes;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using NBTTagList = DotCraftCore.nbt.NBTTagList;

	public class TileEntityFurnace : TileEntity, ISidedInventory
	{
		private static readonly int[] field_145962_k = new int[] {0};
		private static readonly int[] field_145959_l = new int[] {2, 1};
		private static readonly int[] field_145960_m = new int[] {1};
		private ItemStack[] field_145957_n = new ItemStack[3];
		public int field_145956_a;
		public int field_145963_i;
		public int field_145961_j;
		private string field_145958_o;
		private const string __OBFID = "CL_00000357";

///    
///     <summary> * Returns the number of slots in the inventory. </summary>
///     
		public virtual int SizeInventory
		{
			get
			{
				return this.field_145957_n.Length;
			}
		}

///    
///     <summary> * Returns the stack in slot i </summary>
///     
		public virtual ItemStack getStackInSlot(int p_70301_1_)
		{
			return this.field_145957_n[p_70301_1_];
		}

///    
///     <summary> * Removes from an inventory slot (first arg) up to a specified number (second arg) of items and returns them in a
///     * new stack. </summary>
///     
		public virtual ItemStack decrStackSize(int p_70298_1_, int p_70298_2_)
		{
			if(this.field_145957_n[p_70298_1_] != null)
			{
				ItemStack var3;

				if(this.field_145957_n[p_70298_1_].stackSize <= p_70298_2_)
				{
					var3 = this.field_145957_n[p_70298_1_];
					this.field_145957_n[p_70298_1_] = null;
					return var3;
				}
				else
				{
					var3 = this.field_145957_n[p_70298_1_].splitStack(p_70298_2_);

					if(this.field_145957_n[p_70298_1_].stackSize == 0)
					{
						this.field_145957_n[p_70298_1_] = null;
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
///     <summary> * When some containers are closed they call this on each slot, then drop whatever it returns as an EntityItem -
///     * like when you close a workbench GUI. </summary>
///     
		public virtual ItemStack getStackInSlotOnClosing(int p_70304_1_)
		{
			if(this.field_145957_n[p_70304_1_] != null)
			{
				ItemStack var2 = this.field_145957_n[p_70304_1_];
				this.field_145957_n[p_70304_1_] = null;
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
			this.field_145957_n[p_70299_1_] = p_70299_2_;

			if(p_70299_2_ != null && p_70299_2_.stackSize > this.InventoryStackLimit)
			{
				p_70299_2_.stackSize = this.InventoryStackLimit;
			}
		}

///    
///     <summary> * Returns the name of the inventory </summary>
///     
		public virtual string InventoryName
		{
			get
			{
				return this.InventoryNameLocalized ? this.field_145958_o : "container.furnace";
			}
		}

///    
///     <summary> * Returns if the inventory name is localized </summary>
///     
		public virtual bool isInventoryNameLocalized()
		{
			get
			{
				return this.field_145958_o != null && this.field_145958_o.Length > 0;
			}
		}

		public virtual void func_145951_a(string p_145951_1_)
		{
			this.field_145958_o = p_145951_1_;
		}

		public override void readFromNBT(NBTTagCompound p_145839_1_)
		{
			base.readFromNBT(p_145839_1_);
			NBTTagList var2 = p_145839_1_.getTagList("Items", 10);
			this.field_145957_n = new ItemStack[this.SizeInventory];

			for(int var3 = 0; var3 < var2.tagCount(); ++var3)
			{
				NBTTagCompound var4 = var2.getCompoundTagAt(var3);
				sbyte var5 = var4.getByte("Slot");

				if(var5 >= 0 && var5 < this.field_145957_n.Length)
				{
					this.field_145957_n[var5] = ItemStack.loadItemStackFromNBT(var4);
				}
			}

			this.field_145956_a = p_145839_1_.getShort("BurnTime");
			this.field_145961_j = p_145839_1_.getShort("CookTime");
			this.field_145963_i = func_145952_a(this.field_145957_n[1]);

			if(p_145839_1_.func_150297_b("CustomName", 8))
			{
				this.field_145958_o = p_145839_1_.getString("CustomName");
			}
		}

		public override void writeToNBT(NBTTagCompound p_145841_1_)
		{
			base.writeToNBT(p_145841_1_);
			p_145841_1_.setShort("BurnTime", (short)this.field_145956_a);
			p_145841_1_.setShort("CookTime", (short)this.field_145961_j);
			NBTTagList var2 = new NBTTagList();

			for(int var3 = 0; var3 < this.field_145957_n.Length; ++var3)
			{
				if(this.field_145957_n[var3] != null)
				{
					NBTTagCompound var4 = new NBTTagCompound();
					var4.setByte("Slot", (sbyte)var3);
					this.field_145957_n[var3].writeToNBT(var4);
					var2.appendTag(var4);
				}
			}

			p_145841_1_.setTag("Items", var2);

			if(this.InventoryNameLocalized)
			{
				p_145841_1_.setString("CustomName", this.field_145958_o);
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

		public virtual int func_145953_d(int p_145953_1_)
		{
			return this.field_145961_j * p_145953_1_ / 200;
		}

		public virtual int func_145955_e(int p_145955_1_)
		{
			if(this.field_145963_i == 0)
			{
				this.field_145963_i = 200;
			}

			return this.field_145956_a * p_145955_1_ / this.field_145963_i;
		}

		public virtual bool func_145950_i()
		{
			return this.field_145956_a > 0;
		}

		public override void updateEntity()
		{
			bool var1 = this.field_145956_a > 0;
			bool var2 = false;

			if(this.field_145956_a > 0)
			{
				--this.field_145956_a;
			}

			if(!this.worldObj.isClient)
			{
				if(this.field_145956_a != 0 || this.field_145957_n[1] != null && this.field_145957_n[0] != null)
				{
					if(this.field_145956_a == 0 && this.func_145948_k())
					{
						this.field_145963_i = this.field_145956_a = func_145952_a(this.field_145957_n[1]);

						if(this.field_145956_a > 0)
						{
							var2 = true;

							if(this.field_145957_n[1] != null)
							{
								--this.field_145957_n[1].stackSize;

								if(this.field_145957_n[1].stackSize == 0)
								{
									Item var3 = this.field_145957_n[1].Item.ContainerItem;
									this.field_145957_n[1] = var3 != null ? new ItemStack(var3) : null;
								}
							}
						}
					}

					if(this.func_145950_i() && this.func_145948_k())
					{
						++this.field_145961_j;

						if(this.field_145961_j == 200)
						{
							this.field_145961_j = 0;
							this.func_145949_j();
							var2 = true;
						}
					}
					else
					{
						this.field_145961_j = 0;
					}
				}

				if(var1 != this.field_145956_a > 0)
				{
					var2 = true;
					BlockFurnace.func_149931_a(this.field_145956_a > 0, this.worldObj, this.field_145851_c, this.field_145848_d, this.field_145849_e);
				}
			}

			if(var2)
			{
				this.onInventoryChanged();
			}
		}

		private bool func_145948_k()
		{
			if(this.field_145957_n[0] == null)
			{
				return false;
			}
			else
			{
				ItemStack var1 = FurnaceRecipes.smelting().func_151395_a(this.field_145957_n[0]);
				return var1 == null ? false : (this.field_145957_n[2] == null ? true : (!this.field_145957_n[2].isItemEqual(var1) ? false : (this.field_145957_n[2].stackSize < this.InventoryStackLimit && this.field_145957_n[2].stackSize < this.field_145957_n[2].MaxStackSize ? true : this.field_145957_n[2].stackSize < var1.MaxStackSize)));
			}
		}

		public virtual void func_145949_j()
		{
			if(this.func_145948_k())
			{
				ItemStack var1 = FurnaceRecipes.smelting().func_151395_a(this.field_145957_n[0]);

				if(this.field_145957_n[2] == null)
				{
					this.field_145957_n[2] = var1.copy();
				}
				else if(this.field_145957_n[2].Item == var1.Item)
				{
					++this.field_145957_n[2].stackSize;
				}

				--this.field_145957_n[0].stackSize;

				if(this.field_145957_n[0].stackSize <= 0)
				{
					this.field_145957_n[0] = null;
				}
			}
		}

		public static int func_145952_a(ItemStack p_145952_0_)
		{
			if(p_145952_0_ == null)
			{
				return 0;
			}
			else
			{
				Item var1 = p_145952_0_.Item;

				if(var1 is ItemBlock && Block.getBlockFromItem(var1) != Blocks.air)
				{
					Block var2 = Block.getBlockFromItem(var1);

					if(var2 == Blocks.wooden_slab)
					{
						return 150;
					}

					if(var2.Material == Material.wood)
					{
						return 300;
					}

					if(var2 == Blocks.coal_block)
					{
						return 16000;
					}
				}

				return var1 is ItemTool && ((ItemTool)var1).ToolMaterialName.Equals("WOOD") ? 200 : (var1 is ItemSword && ((ItemSword)var1).func_150932_j().Equals("WOOD") ? 200 : (var1 is ItemHoe && ((ItemHoe)var1).MaterialName.Equals("WOOD") ? 200 : (var1 == Items.stick ? 100 : (var1 == Items.coal ? 1600 : (var1 == Items.lava_bucket ? 20000 : (var1 == Item.getItemFromBlock(Blocks.sapling) ? 100 : (var1 == Items.blaze_rod ? 2400 : 0)))))));
			}
		}

		public static bool func_145954_b(ItemStack p_145954_0_)
		{
			return func_145952_a(p_145954_0_) > 0;
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
			return p_94041_1_ == 2 ? false : (p_94041_1_ == 1 ? func_145954_b(p_94041_2_) : true);
		}

///    
///     <summary> * Returns an array containing the indices of the slots that can be accessed by automation on the given side of this
///     * block. </summary>
///     
		public virtual int[] getAccessibleSlotsFromSide(int p_94128_1_)
		{
			return p_94128_1_ == 0 ? field_145959_l : (p_94128_1_ == 1 ? field_145962_k : field_145960_m);
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
			return p_102008_3_ != 0 || p_102008_1_ != 1 || p_102008_2_.Item == Items.bucket;
		}
	}

}