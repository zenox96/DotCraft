namespace DotCraftCore.Village
{

	using ItemStack = DotCraftCore.item.ItemStack;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using NBTTagList = DotCraftCore.nbt.NBTTagList;
	using PacketBuffer = DotCraftCore.network.PacketBuffer;

	public class MerchantRecipeList : ArrayList
	{
		

		public MerchantRecipeList()
		{
		}

		public MerchantRecipeList(NBTTagCompound p_i1944_1_)
		{
			this.readRecipiesFromTags(p_i1944_1_);
		}

///    
///     <summary> * can par1,par2 be used to in crafting recipe par3 </summary>
///     
		public virtual MerchantRecipe canRecipeBeUsed(ItemStack p_77203_1_, ItemStack p_77203_2_, int p_77203_3_)
		{
			if(p_77203_3_ > 0 && p_77203_3_ < this.size())
			{
				MerchantRecipe var6 = (MerchantRecipe)this.get(p_77203_3_);
				return p_77203_1_.Item == var6.ItemToBuy.Item && (p_77203_2_ == null && !var6.hasSecondItemToBuy() || var6.hasSecondItemToBuy() && p_77203_2_ != null && var6.SecondItemToBuy.Item == p_77203_2_.Item) && p_77203_1_.stackSize >= var6.ItemToBuy.stackSize && (!var6.hasSecondItemToBuy() || p_77203_2_.stackSize >= var6.SecondItemToBuy.stackSize) ? var6 : null;
			}
			else
			{
				for(int var4 = 0; var4 < this.size(); ++var4)
				{
					MerchantRecipe var5 = (MerchantRecipe)this.get(var4);

					if(p_77203_1_.Item == var5.ItemToBuy.Item && p_77203_1_.stackSize >= var5.ItemToBuy.stackSize && (!var5.hasSecondItemToBuy() && p_77203_2_ == null || var5.hasSecondItemToBuy() && p_77203_2_ != null && var5.SecondItemToBuy.Item == p_77203_2_.Item && p_77203_2_.stackSize >= var5.SecondItemToBuy.stackSize))
					{
						return var5;
					}
				}

				return null;
			}
		}

///    
///     <summary> * checks if there is a recipie for the same ingredients already on the list, and replaces it. otherwise, adds it </summary>
///     
		public virtual void addToListWithCheck(MerchantRecipe p_77205_1_)
		{
			for(int var2 = 0; var2 < this.size(); ++var2)
			{
				MerchantRecipe var3 = (MerchantRecipe)this.get(var2);

				if(p_77205_1_.hasSameIDsAs(var3))
				{
					if(p_77205_1_.hasSameItemsAs(var3))
					{
						this.set(var2, p_77205_1_);
					}

					return;
				}
			}

			this.add(p_77205_1_);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void func_151391_a(PacketBuffer p_151391_1_) throws IOException
		public virtual void func_151391_a(PacketBuffer p_151391_1_)
		{
			p_151391_1_.writeByte((sbyte)(this.size() & 255));

			for(int var2 = 0; var2 < this.size(); ++var2)
			{
				MerchantRecipe var3 = (MerchantRecipe)this.get(var2);
				p_151391_1_.writeItemStackToBuffer(var3.ItemToBuy);
				p_151391_1_.writeItemStackToBuffer(var3.ItemToSell);
				ItemStack var4 = var3.SecondItemToBuy;
				p_151391_1_.writeBoolean(var4 != null);

				if(var4 != null)
				{
					p_151391_1_.writeItemStackToBuffer(var4);
				}

				p_151391_1_.writeBoolean(var3.RecipeDisabled);
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static MerchantRecipeList func_151390_b(PacketBuffer p_151390_0_) throws IOException
		public static MerchantRecipeList func_151390_b(PacketBuffer p_151390_0_)
		{
			MerchantRecipeList var1 = new MerchantRecipeList();
			int var2 = p_151390_0_.readByte() & 255;

			for(int var3 = 0; var3 < var2; ++var3)
			{
				ItemStack var4 = p_151390_0_.readItemStackFromBuffer();
				ItemStack var5 = p_151390_0_.readItemStackFromBuffer();
				ItemStack var6 = null;

				if(p_151390_0_.readBoolean())
				{
					var6 = p_151390_0_.readItemStackFromBuffer();
				}

				bool var7 = p_151390_0_.readBoolean();
				MerchantRecipe var8 = new MerchantRecipe(var4, var6, var5);

				if(var7)
				{
					var8.func_82785_h();
				}

				var1.add(var8);
			}

			return var1;
		}

		public virtual void readRecipiesFromTags(NBTTagCompound p_77201_1_)
		{
			NBTTagList var2 = p_77201_1_.getTagList("Recipes", 10);

			for(int var3 = 0; var3 < var2.tagCount(); ++var3)
			{
				NBTTagCompound var4 = var2.getCompoundTagAt(var3);
				this.add(new MerchantRecipe(var4));
			}
		}

		public virtual NBTTagCompound RecipiesAsTags
		{
			get
			{
				NBTTagCompound var1 = new NBTTagCompound();
				NBTTagList var2 = new NBTTagList();
	
				for(int var3 = 0; var3 < this.size(); ++var3)
				{
					MerchantRecipe var4 = (MerchantRecipe)this.get(var3);
					var2.appendTag(var4.writeToTags());
				}
	
				var1.setTag("Recipes", var2);
				return var1;
			}
		}
	}

}