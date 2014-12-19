namespace DotCraftCore.nVillage
{

	using Item = DotCraftCore.nItem.Item;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;

	public class MerchantRecipe
	{
	/// <summary> Item the Villager buys.  </summary>
		private ItemStack itemToBuy;

	/// <summary> Second Item the Villager buys.  </summary>
		private ItemStack secondItemToBuy;

	/// <summary> Item the Villager sells.  </summary>
		private ItemStack itemToSell;

///    
///     <summary> * Saves how much has been tool used when put into to slot to be enchanted. </summary>
///     
		private int toolUses;

	/// <summary> Maximum times this trade can be used.  </summary>
		private int maxTradeUses;
		

		public MerchantRecipe(NBTTagCompound p_i1940_1_)
		{
			this.readFromTags(p_i1940_1_);
		}

		public MerchantRecipe(ItemStack p_i1941_1_, ItemStack p_i1941_2_, ItemStack p_i1941_3_)
		{
			this.itemToBuy = p_i1941_1_;
			this.secondItemToBuy = p_i1941_2_;
			this.itemToSell = p_i1941_3_;
			this.maxTradeUses = 7;
		}

		public MerchantRecipe(ItemStack p_i1942_1_, ItemStack p_i1942_2_) : this(p_i1942_1_, (ItemStack)null, p_i1942_2_)
		{
		}

		public MerchantRecipe(ItemStack p_i1943_1_, Item p_i1943_2_) : this(p_i1943_1_, new ItemStack(p_i1943_2_))
		{
		}

///    
///     <summary> * Gets the itemToBuy. </summary>
///     
		public virtual ItemStack ItemToBuy
		{
			get
			{
				return this.itemToBuy;
			}
		}

///    
///     <summary> * Gets secondItemToBuy. </summary>
///     
		public virtual ItemStack SecondItemToBuy
		{
			get
			{
				return this.secondItemToBuy;
			}
		}

///    
///     <summary> * Gets if Villager has secondItemToBuy. </summary>
///     
		public virtual bool hasSecondItemToBuy()
		{
			return this.secondItemToBuy != null;
		}

///    
///     <summary> * Gets itemToSell. </summary>
///     
		public virtual ItemStack ItemToSell
		{
			get
			{
				return this.itemToSell;
			}
		}

///    
///     <summary> * checks if both the first and second ItemToBuy IDs are the same </summary>
///     
		public virtual bool hasSameIDsAs(MerchantRecipe p_77393_1_)
		{
			return this.itemToBuy.Item == p_77393_1_.itemToBuy.Item && this.itemToSell.Item == p_77393_1_.itemToSell.Item ? this.secondItemToBuy == null && p_77393_1_.secondItemToBuy == null || this.secondItemToBuy != null && p_77393_1_.secondItemToBuy != null && this.secondItemToBuy.Item == p_77393_1_.secondItemToBuy.Item : false;
		}

///    
///     <summary> * checks first and second ItemToBuy ID's and count. Calls hasSameIDs </summary>
///     
		public virtual bool hasSameItemsAs(MerchantRecipe p_77391_1_)
		{
			return this.hasSameIDsAs(p_77391_1_) && (this.itemToBuy.stackSize < p_77391_1_.itemToBuy.stackSize || this.secondItemToBuy != null && this.secondItemToBuy.stackSize < p_77391_1_.secondItemToBuy.stackSize);
		}

		public virtual void incrementToolUses()
		{
			++this.toolUses;
		}

		public virtual void func_82783_a(int p_82783_1_)
		{
			this.maxTradeUses += p_82783_1_;
		}

		public virtual bool isRecipeDisabled()
		{
			get
			{
				return this.toolUses >= this.maxTradeUses;
			}
		}

		public virtual void func_82785_h()
		{
			this.toolUses = this.maxTradeUses;
		}

		public virtual void readFromTags(NBTTagCompound p_77390_1_)
		{
			NBTTagCompound var2 = p_77390_1_.getCompoundTag("buy");
			this.itemToBuy = ItemStack.loadItemStackFromNBT(var2);
			NBTTagCompound var3 = p_77390_1_.getCompoundTag("sell");
			this.itemToSell = ItemStack.loadItemStackFromNBT(var3);

			if(p_77390_1_.func_150297_b("buyB", 10))
			{
				this.secondItemToBuy = ItemStack.loadItemStackFromNBT(p_77390_1_.getCompoundTag("buyB"));
			}

			if(p_77390_1_.func_150297_b("uses", 99))
			{
				this.toolUses = p_77390_1_.getInteger("uses");
			}

			if(p_77390_1_.func_150297_b("maxUses", 99))
			{
				this.maxTradeUses = p_77390_1_.getInteger("maxUses");
			}
			else
			{
				this.maxTradeUses = 7;
			}
		}

		public virtual NBTTagCompound writeToTags()
		{
			NBTTagCompound var1 = new NBTTagCompound();
			var1.setTag("buy", this.itemToBuy.writeToNBT(new NBTTagCompound()));
			var1.setTag("sell", this.itemToSell.writeToNBT(new NBTTagCompound()));

			if(this.secondItemToBuy != null)
			{
				var1.setTag("buyB", this.secondItemToBuy.writeToNBT(new NBTTagCompound()));
			}

			var1.setInteger("uses", this.toolUses);
			var1.setInteger("maxUses", this.maxTradeUses);
			return var1;
		}
	}

}