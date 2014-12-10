namespace DotCraftCore.Item
{

	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Items = DotCraftCore.init.Items;
	using World = DotCraftCore.World.World;

	public class ItemSoup : ItemFood
	{
		private const string __OBFID = "CL_00001778";

		public ItemSoup(int p_i45330_1_) : base(p_i45330_1_, false)
		{
			this.MaxStackSize = 1;
		}

		public override ItemStack onEaten(ItemStack p_77654_1_, World p_77654_2_, EntityPlayer p_77654_3_)
		{
			base.onEaten(p_77654_1_, p_77654_2_, p_77654_3_);
			return new ItemStack(Items.bowl);
		}
	}

}