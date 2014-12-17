namespace DotCraftCore.nEntity
{

	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using InventoryMerchant = DotCraftCore.nInventory.InventoryMerchant;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using MerchantRecipe = DotCraftCore.nVillage.MerchantRecipe;
	using MerchantRecipeList = DotCraftCore.nVillage.MerchantRecipeList;

	public class NpcMerchant : IMerchant
	{
	/// <summary> Instance of Merchants Inventory.  </summary>
		private InventoryMerchant theMerchantInventory;

	/// <summary> This merchant's current player customer.  </summary>
		private EntityPlayer customer;

	/// <summary> The MerchantRecipeList instance.  </summary>
		private MerchantRecipeList recipeList;
		

		public NpcMerchant(EntityPlayer p_i1746_1_)
		{
			this.customer = p_i1746_1_;
			this.theMerchantInventory = new InventoryMerchant(p_i1746_1_, this);
		}

		public virtual EntityPlayer Customer
		{
			get
			{
				return this.customer;
			}
			set
			{
			}
		}


		public virtual MerchantRecipeList getRecipes(EntityPlayer p_70934_1_)
		{
			return this.recipeList;
		}

		public virtual MerchantRecipeList Recipes
		{
			set
			{
				this.recipeList = value;
			}
		}

		public virtual void useRecipe(MerchantRecipe p_70933_1_)
		{
		}

		public virtual void func_110297_a_(ItemStack p_110297_1_)
		{
		}
	}

}