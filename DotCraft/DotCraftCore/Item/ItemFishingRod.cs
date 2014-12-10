namespace DotCraftCore.Item
{

	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using EntityFishHook = DotCraftCore.entity.projectile.EntityFishHook;
	using IIcon = DotCraftCore.Util.IIcon;
	using World = DotCraftCore.World.World;

	public class ItemFishingRod : Item
	{
		private IIcon theIcon;
		private const string __OBFID = "CL_00000034";

		public ItemFishingRod()
		{
			this.MaxDamage = 64;
			this.MaxStackSize = 1;
			this.CreativeTab = CreativeTabs.tabTools;
		}

///    
///     <summary> * Returns True is the item is renderer in full 3D when hold. </summary>
///     
		public virtual bool isFull3D()
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * Returns true if this item should be rotated by 180 degrees around the Y axis when being held in an entities
///     * hands. </summary>
///     
		public virtual bool shouldRotateAroundWhenRendering()
		{
			return true;
		}

///    
///     <summary> * Called whenever this item is equipped and the right mouse button is pressed. Args: itemStack, world, entityPlayer </summary>
///     
		public virtual ItemStack onItemRightClick(ItemStack p_77659_1_, World p_77659_2_, EntityPlayer p_77659_3_)
		{
			if (p_77659_3_.fishEntity != null)
			{
				int var4 = p_77659_3_.fishEntity.func_146034_e();
				p_77659_1_.damageItem(var4, p_77659_3_);
				p_77659_3_.swingItem();
			}
			else
			{
				p_77659_2_.playSoundAtEntity(p_77659_3_, "random.bow", 0.5F, 0.4F / (itemRand.nextFloat() * 0.4F + 0.8F));

				if (!p_77659_2_.isClient)
				{
					p_77659_2_.spawnEntityInWorld(new EntityFishHook(p_77659_2_, p_77659_3_));
				}

				p_77659_3_.swingItem();
			}

			return p_77659_1_;
		}

		public virtual void registerIcons(IIconRegister p_94581_1_)
		{
			this.itemIcon = p_94581_1_.registerIcon(this.IconString + "_uncast");
			this.theIcon = p_94581_1_.registerIcon(this.IconString + "_cast");
		}

		public virtual IIcon func_94597_g()
		{
			return this.theIcon;
		}

///    
///     <summary> * Checks isDamagable and if it cannot be stacked </summary>
///     
		public virtual bool isItemTool(ItemStack p_77616_1_)
		{
			return base.isItemTool(p_77616_1_);
		}

///    
///     <summary> * Return the enchantability factor of the item, most of the time is based on material. </summary>
///     
		public virtual int ItemEnchantability
		{
			get
			{
				return 1;
			}
		}
	}

}