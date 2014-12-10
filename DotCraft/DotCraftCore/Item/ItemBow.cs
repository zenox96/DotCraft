namespace DotCraftCore.Item
{

	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using Enchantment = DotCraftCore.enchantment.Enchantment;
	using EnchantmentHelper = DotCraftCore.enchantment.EnchantmentHelper;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using EntityArrow = DotCraftCore.entity.projectile.EntityArrow;
	using Items = DotCraftCore.init.Items;
	using IIcon = DotCraftCore.Util.IIcon;
	using World = DotCraftCore.World.World;

	public class ItemBow : Item
	{
		public static readonly string[] bowPullIconNameArray = new string[] {"pulling_0", "pulling_1", "pulling_2"};
		private IIcon[] iconArray;
		private const string __OBFID = "CL_00001777";

		public ItemBow()
		{
			this.maxStackSize = 1;
			this.MaxDamage = 384;
			this.CreativeTab = CreativeTabs.tabCombat;
		}

///    
///     <summary> * called when the player releases the use item button. Args: itemstack, world, entityplayer, itemInUseCount </summary>
///     
		public virtual void onPlayerStoppedUsing(ItemStack p_77615_1_, World p_77615_2_, EntityPlayer p_77615_3_, int p_77615_4_)
		{
			bool var5 = p_77615_3_.capabilities.isCreativeMode || EnchantmentHelper.getEnchantmentLevel(Enchantment.infinity.effectId, p_77615_1_) > 0;

			if (var5 || p_77615_3_.inventory.hasItem(Items.arrow))
			{
				int var6 = this.getMaxItemUseDuration(p_77615_1_) - p_77615_4_;
				float var7 = (float)var6 / 20.0F;
				var7 = (var7 * var7 + var7 * 2.0F) / 3.0F;

				if ((double)var7 < 0.1D)
				{
					return;
				}

				if (var7 > 1.0F)
				{
					var7 = 1.0F;
				}

				EntityArrow var8 = new EntityArrow(p_77615_2_, p_77615_3_, var7 * 2.0F);

				if (var7 == 1.0F)
				{
					var8.IsCritical = true;
				}

				int var9 = EnchantmentHelper.getEnchantmentLevel(Enchantment.power.effectId, p_77615_1_);

				if (var9 > 0)
				{
					var8.Damage = var8.Damage + (double)var9 * 0.5D + 0.5D;
				}

				int var10 = EnchantmentHelper.getEnchantmentLevel(Enchantment.punch.effectId, p_77615_1_);

				if (var10 > 0)
				{
					var8.KnockbackStrength = var10;
				}

				if (EnchantmentHelper.getEnchantmentLevel(Enchantment.flame.effectId, p_77615_1_) > 0)
				{
					var8.Fire = 100;
				}

				p_77615_1_.damageItem(1, p_77615_3_);
				p_77615_2_.playSoundAtEntity(p_77615_3_, "random.bow", 1.0F, 1.0F / (itemRand.nextFloat() * 0.4F + 1.2F) + var7 * 0.5F);

				if (var5)
				{
					var8.canBePickedUp = 2;
				}
				else
				{
					p_77615_3_.inventory.consumeInventoryItem(Items.arrow);
				}

				if (!p_77615_2_.isClient)
				{
					p_77615_2_.spawnEntityInWorld(var8);
				}
			}
		}

		public virtual ItemStack onEaten(ItemStack p_77654_1_, World p_77654_2_, EntityPlayer p_77654_3_)
		{
			return p_77654_1_;
		}

///    
///     <summary> * How long it takes to use or consume an item </summary>
///     
		public virtual int getMaxItemUseDuration(ItemStack p_77626_1_)
		{
			return 72000;
		}

///    
///     <summary> * returns the action that specifies what animation to play when the items is being used </summary>
///     
		public virtual EnumAction getItemUseAction(ItemStack p_77661_1_)
		{
			return EnumAction.bow;
		}

///    
///     <summary> * Called whenever this item is equipped and the right mouse button is pressed. Args: itemStack, world, entityPlayer </summary>
///     
		public virtual ItemStack onItemRightClick(ItemStack p_77659_1_, World p_77659_2_, EntityPlayer p_77659_3_)
		{
			if (p_77659_3_.capabilities.isCreativeMode || p_77659_3_.inventory.hasItem(Items.arrow))
			{
				p_77659_3_.setItemInUse(p_77659_1_, this.getMaxItemUseDuration(p_77659_1_));
			}

			return p_77659_1_;
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

		public virtual void registerIcons(IIconRegister p_94581_1_)
		{
			this.itemIcon = p_94581_1_.registerIcon(this.IconString + "_standby");
			this.iconArray = new IIcon[bowPullIconNameArray.Length];

			for (int var2 = 0; var2 < this.iconArray.Length; ++var2)
			{
				this.iconArray[var2] = p_94581_1_.registerIcon(this.IconString + "_" + bowPullIconNameArray[var2]);
			}
		}

///    
///     <summary> * used to cycle through icons based on their used duration, i.e. for the bow </summary>
///     
		public virtual IIcon getItemIconForUseDuration(int p_94599_1_)
		{
			return this.iconArray[p_94599_1_];
		}
	}

}