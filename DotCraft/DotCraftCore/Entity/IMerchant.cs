namespace DotCraftCore.Entity
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using MerchantRecipe = DotCraftCore.Village.MerchantRecipe;
	using MerchantRecipeList = DotCraftCore.Village.MerchantRecipeList;

	public interface IMerchant
	{
		EntityPlayer Customer {set;get;}


		MerchantRecipeList getRecipes(EntityPlayer p_70934_1_);

		MerchantRecipeList Recipes {set;}

		void useRecipe(MerchantRecipe p_70933_1_);

		void func_110297_a_(ItemStack p_110297_1_);
	}

}