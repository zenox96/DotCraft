namespace DotCraftCore.Item
{

	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using EntityEgg = DotCraftCore.entity.projectile.EntityEgg;
	using World = DotCraftCore.World.World;

	public class ItemEgg : Item
	{
		private const string __OBFID = "CL_00000023";

		public ItemEgg()
		{
			this.maxStackSize = 16;
			this.CreativeTab = CreativeTabs.tabMaterials;
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
				p_77659_2_.spawnEntityInWorld(new EntityEgg(p_77659_2_, p_77659_3_));
			}

			return p_77659_1_;
		}
	}

}