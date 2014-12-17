namespace DotCraftCore.nInventory
{

	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using NBTTagList = DotCraftCore.nNBT.NBTTagList;
	using TileEntityEnderChest = DotCraftCore.nTileEntity.TileEntityEnderChest;

	public class InventoryEnderChest : InventoryBasic
	{
		private TileEntityEnderChest associatedChest;
		

		public InventoryEnderChest() : base("container.enderchest", false, 27)
		{
		}

		public virtual void func_146031_a(TileEntityEnderChest p_146031_1_)
		{
			this.associatedChest = p_146031_1_;
		}

		public virtual void loadInventoryFromNBT(NBTTagList p_70486_1_)
		{
			int var2;

			for (var2 = 0; var2 < this.SizeInventory; ++var2)
			{
				this.setInventorySlotContents(var2, (ItemStack)null);
			}

			for (var2 = 0; var2 < p_70486_1_.tagCount(); ++var2)
			{
				NBTTagCompound var3 = p_70486_1_.getCompoundTagAt(var2);
				int var4 = var3.getByte("Slot") & 255;

				if (var4 >= 0 && var4 < this.SizeInventory)
				{
					this.setInventorySlotContents(var4, ItemStack.loadItemStackFromNBT(var3));
				}
			}
		}

		public virtual NBTTagList saveInventoryToNBT()
		{
			NBTTagList var1 = new NBTTagList();

			for (int var2 = 0; var2 < this.SizeInventory; ++var2)
			{
				ItemStack var3 = this.getStackInSlot(var2);

				if (var3 != null)
				{
					NBTTagCompound var4 = new NBTTagCompound();
					var4.setByte("Slot", (sbyte)var2);
					var3.writeToNBT(var4);
					var1.appendTag(var4);
				}
			}

			return var1;
		}

///    
///     <summary> * Do not make give this method the name canInteractWith because it clashes with Container </summary>
///     
		public override bool isUseableByPlayer(EntityPlayer p_70300_1_)
		{
			return this.associatedChest != null && !this.associatedChest.func_145971_a(p_70300_1_) ? false : base.isUseableByPlayer(p_70300_1_);
		}

		public override void openInventory()
		{
			if (this.associatedChest != null)
			{
				this.associatedChest.func_145969_a();
			}

			base.openInventory();
		}

		public override void closeInventory()
		{
			if (this.associatedChest != null)
			{
				this.associatedChest.func_145970_b();
			}

			base.closeInventory();
			this.associatedChest = null;
		}
	}

}