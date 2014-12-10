namespace DotCraftCore.Item
{

	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Items = DotCraftCore.init.Items;
	using World = DotCraftCore.World.World;

	public class ItemBucketMilk : Item
	{
		private const string __OBFID = "CL_00000048";

		public ItemBucketMilk()
		{
			this.MaxStackSize = 1;
			this.CreativeTab = CreativeTabs.tabMisc;
		}

		public virtual ItemStack onEaten(ItemStack p_77654_1_, World p_77654_2_, EntityPlayer p_77654_3_)
		{
			if (!p_77654_3_.capabilities.isCreativeMode)
			{
				--p_77654_1_.stackSize;
			}

			if (!p_77654_2_.isClient)
			{
				p_77654_3_.clearActivePotions();
			}

			return p_77654_1_.stackSize <= 0 ? new ItemStack(Items.bucket) : p_77654_1_;
		}

///    
///     <summary> * How long it takes to use or consume an item </summary>
///     
		public virtual int getMaxItemUseDuration(ItemStack p_77626_1_)
		{
			return 32;
		}

///    
///     <summary> * returns the action that specifies what animation to play when the items is being used </summary>
///     
		public virtual EnumAction getItemUseAction(ItemStack p_77661_1_)
		{
			return EnumAction.drink;
		}

///    
///     <summary> * Called whenever this item is equipped and the right mouse button is pressed. Args: itemStack, world, entityPlayer </summary>
///     
		public virtual ItemStack onItemRightClick(ItemStack p_77659_1_, World p_77659_2_, EntityPlayer p_77659_3_)
		{
			p_77659_3_.setItemInUse(p_77659_1_, this.getMaxItemUseDuration(p_77659_1_));
			return p_77659_1_;
		}
	}

}