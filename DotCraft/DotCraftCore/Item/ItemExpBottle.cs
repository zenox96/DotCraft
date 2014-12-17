namespace DotCraftCore.nItem
{

	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityExpBottle = DotCraftCore.entity.item.EntityExpBottle;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using World = DotCraftCore.nWorld.World;

	public class ItemExpBottle : Item
	{
		

		public ItemExpBottle()
		{
			this.CreativeTab = CreativeTabs.tabMisc;
		}

		public virtual bool hasEffect(ItemStack p_77636_1_)
		{
			return true;
		}

///    
///     <summary> * Called whenever this item is equipped and the right mouse button is pressed. Args: itemStack, world, entityPlayer </summary>
///     
		public virtual ItemStack onItemRightClick(ItemStack p_77659_1_, World p_77659_2_, EntityPlayer p_77659_3_)
		{
			if (!p_77659_3_.capabilities.isCreativeMode)
			{
				--p_77659_1_.stackSize;
			}

			p_77659_2_.playSoundAtEntity(p_77659_3_, "random.bow", 0.5F, 0.4F / (itemRand.nextFloat() * 0.4F + 0.8F));

			if (!p_77659_2_.isClient)
			{
				p_77659_2_.spawnEntityInWorld(new EntityExpBottle(p_77659_2_, p_77659_3_));
			}

			return p_77659_1_;
		}
	}

}