namespace DotCraftCore.Entity
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using InventoryMerchant = DotCraftCore.Inventory.InventoryMerchant;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using MerchantRecipe = DotCraftCore.Village.MerchantRecipe;
	using MerchantRecipeList = DotCraftCore.Village.MerchantRecipeList;

	public class NpcMerchant : IMerchant
	{
	/// <summary> Instance of Merchants Inventory.  </summary>
		private InventoryMerchant theMerchantInventory;

	/// <summary> This merchant's current player customer.  </summary>
		private EntityPlayer customer;

	/// <summary> The MerchantRecipeList instance.  </summary>
		private MerchantRecipeList recipeList;
		private const string __OBFID = "CL_00001705";

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