namespace DotCraftCore.nEntity
{

	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using MerchantRecipe = DotCraftCore.nVillage.MerchantRecipe;
	using MerchantRecipeList = DotCraftCore.nVillage.MerchantRecipeList;

	public interface IMerchant
	{
		EntityPlayer Customer {set;get;}


		MerchantRecipeList getRecipes(EntityPlayer p_70934_1_);

		MerchantRecipeList Recipes {set;}

		void useRecipe(MerchantRecipe p_70933_1_);

		void func_110297_a_(ItemStack p_110297_1_);
	}

}