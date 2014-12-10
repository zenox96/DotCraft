namespace DotCraftCore.Item
{

	using BlockDispenser = DotCraftCore.block.BlockDispenser;
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using IEntitySelector = DotCraftCore.command.IEntitySelector;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using BehaviorDefaultDispenseItem = DotCraftCore.dispenser.BehaviorDefaultDispenseItem;
	using IBehaviorDispenseItem = DotCraftCore.dispenser.IBehaviorDispenseItem;
	using IBlockSource = DotCraftCore.dispenser.IBlockSource;
	using EntityLiving = DotCraftCore.entity.EntityLiving;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Items = DotCraftCore.init.Items;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using EnumFacing = DotCraftCore.Util.EnumFacing;
	using IIcon = DotCraftCore.Util.IIcon;
	using World = DotCraftCore.World.World;

	public class ItemArmor : Item
	{
	/// <summary> Holds the 'base' maxDamage that each armorType have.  </summary>
		private static readonly int[] maxDamageArray = new int[] {11, 16, 15, 13};
		private static readonly string[] CLOTH_OVERLAY_NAMES = new string[] {"leather_helmet_overlay", "leather_chestplate_overlay", "leather_leggings_overlay", "leather_boots_overlay"};
		public static readonly string[] EMPTY_SLOT_NAMES = new string[] {"empty_armor_slot_helmet", "empty_armor_slot_chestplate", "empty_armor_slot_leggings", "empty_armor_slot_boots"};
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		private static final IBehaviorDispenseItem dispenserBehavior = new BehaviorDefaultDispenseItem()
//	{
//		private static final String __OBFID = "CL_00001767";
//		protected ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_)
//		{
//			EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.getBlockMetadata());
//			int var4 = p_82487_1_.getXInt() + var3.getFrontOffsetX();
//			int var5 = p_82487_1_.getYInt() + var3.getFrontOffsetY();
//			int var6 = p_82487_1_.getZInt() + var3.getFrontOffsetZ();
//			AxisAlignedBB var7 = AxisAlignedBB.getBoundingBox((double)var4, (double)var5, (double)var6, (double)(var4 + 1), (double)(var5 + 1), (double)(var6 + 1));
//			List var8 = p_82487_1_.getWorld().selectEntitiesWithinAABB(EntityLivingBase.class, var7, new IEntitySelector.ArmoredMob(p_82487_2_));
//
//			if (var8.size() > 0)
//			{
//				EntityLivingBase var9 = (EntityLivingBase)var8.get(0);
//				int var10 = var9 instanceof EntityPlayer ? 1 : 0;
//				int var11 = EntityLiving.getArmorPosition(p_82487_2_);
//				ItemStack var12 = p_82487_2_.copy();
//				var12.stackSize = 1;
//				var9.setCurrentItemOrArmor(var11 - var10, var12);
//
//				if (var9 instanceof EntityLiving)
//				{
//					((EntityLiving)var9).setEquipmentDropChance(var11, 2.0F);
//				}
//
//				--p_82487_2_.stackSize;
//				return p_82487_2_;
//			}
//			else
//			{
//				return base.dispenseStack(p_82487_1_, p_82487_2_);
//			}
//		}
//	};

///    
///     <summary> * Stores the armor type: 0 is helmet, 1 is plate, 2 is legs and 3 is boots </summary>
///     
		public readonly int armorType;

	/// <summary> Holds the amount of damage that the armor reduces at full durability.  </summary>
		public readonly int damageReduceAmount;

///    
///     <summary> * Used on RenderPlayer to select the correspondent armor to be rendered on the player: 0 is cloth, 1 is chain, 2 is
///     * iron, 3 is diamond and 4 is gold. </summary>
///     
		public readonly int renderIndex;

	/// <summary> The EnumArmorMaterial used for this ItemArmor  </summary>
		private readonly ItemArmor.ArmorMaterial material;
		private IIcon overlayIcon;
		private IIcon emptySlotIcon;
		private const string __OBFID = "CL_00001766";

		public ItemArmor(ItemArmor.ArmorMaterial p_i45325_1_, int p_i45325_2_, int p_i45325_3_)
		{
			this.material = p_i45325_1_;
			this.armorType = p_i45325_3_;
			this.renderIndex = p_i45325_2_;
			this.damageReduceAmount = p_i45325_1_.getDamageReductionAmount(p_i45325_3_);
			this.MaxDamage = p_i45325_1_.getDurability(p_i45325_3_);
			this.maxStackSize = 1;
			this.CreativeTab = CreativeTabs.tabCombat;
			BlockDispenser.dispenseBehaviorRegistry.putObject(this, dispenserBehavior);
		}

		public virtual int getColorFromItemStack(ItemStack p_82790_1_, int p_82790_2_)
		{
			if (p_82790_2_ > 0)
			{
				return 16777215;
			}
			else
			{
				int var3 = this.getColor(p_82790_1_);

				if (var3 < 0)
				{
					var3 = 16777215;
				}

				return var3;
			}
		}

		public virtual bool requiresMultipleRenderPasses()
		{
			return this.material == ItemArmor.ArmorMaterial.CLOTH;
		}

///    
///     <summary> * Return the enchantability factor of the item, most of the time is based on material. </summary>
///     
		public virtual int ItemEnchantability
		{
			get
			{
				return this.material.Enchantability;
			}
		}

///    
///     <summary> * Return the armor material for this armor item. </summary>
///     
		public virtual ItemArmor.ArmorMaterial ArmorMaterial
		{
			get
			{
				return this.material;
			}
		}

///    
///     <summary> * Return whether the specified armor ItemStack has a color. </summary>
///     
		public virtual bool hasColor(ItemStack p_82816_1_)
		{
			return this.material != ItemArmor.ArmorMaterial.CLOTH ? false : (!p_82816_1_.hasTagCompound() ? false : (!p_82816_1_.TagCompound.func_150297_b("display", 10) ? false : p_82816_1_.TagCompound.getCompoundTag("display").func_150297_b("color", 3)));
		}

///    
///     <summary> * Return the color for the specified armor ItemStack. </summary>
///     
		public virtual int getColor(ItemStack p_82814_1_)
		{
			if (this.material != ItemArmor.ArmorMaterial.CLOTH)
			{
				return -1;
			}
			else
			{
				NBTTagCompound var2 = p_82814_1_.TagCompound;

				if (var2 == null)
				{
					return 10511680;
				}
				else
				{
					NBTTagCompound var3 = var2.getCompoundTag("display");
					return var3 == null ? 10511680 : (var3.func_150297_b("color", 3) ? var3.getInteger("color") : 10511680);
				}
			}
		}

///    
///     <summary> * Gets an icon index based on an item's damage value and the given render pass </summary>
///     
		public virtual IIcon getIconFromDamageForRenderPass(int p_77618_1_, int p_77618_2_)
		{
			return p_77618_2_ == 1 ? this.overlayIcon : base.getIconFromDamageForRenderPass(p_77618_1_, p_77618_2_);
		}

///    
///     <summary> * Remove the color from the specified armor ItemStack. </summary>
///     
		public virtual void removeColor(ItemStack p_82815_1_)
		{
			if (this.material == ItemArmor.ArmorMaterial.CLOTH)
			{
				NBTTagCompound var2 = p_82815_1_.TagCompound;

				if (var2 != null)
				{
					NBTTagCompound var3 = var2.getCompoundTag("display");

					if (var3.hasKey("color"))
					{
						var3.removeTag("color");
					}
				}
			}
		}

		public virtual void func_82813_b(ItemStack p_82813_1_, int p_82813_2_)
		{
			if (this.material != ItemArmor.ArmorMaterial.CLOTH)
			{
				throw new UnsupportedOperationException("Can\'t dye non-leather!");
			}
			else
			{
				NBTTagCompound var3 = p_82813_1_.TagCompound;

				if (var3 == null)
				{
					var3 = new NBTTagCompound();
					p_82813_1_.TagCompound = var3;
				}

				NBTTagCompound var4 = var3.getCompoundTag("display");

				if (!var3.func_150297_b("display", 10))
				{
					var3.setTag("display", var4);
				}

				var4.setInteger("color", p_82813_2_);
			}
		}

///    
///     <summary> * Return whether this item is repairable in an anvil. </summary>
///     
		public virtual bool getIsRepairable(ItemStack p_82789_1_, ItemStack p_82789_2_)
		{
			return this.material.func_151685_b() == p_82789_2_.Item ? true : base.getIsRepairable(p_82789_1_, p_82789_2_);
		}

		public virtual void registerIcons(IIconRegister p_94581_1_)
		{
			base.registerIcons(p_94581_1_);

			if (this.material == ItemArmor.ArmorMaterial.CLOTH)
			{
				this.overlayIcon = p_94581_1_.registerIcon(CLOTH_OVERLAY_NAMES[this.armorType]);
			}

			this.emptySlotIcon = p_94581_1_.registerIcon(EMPTY_SLOT_NAMES[this.armorType]);
		}

///    
///     <summary> * Called whenever this item is equipped and the right mouse button is pressed. Args: itemStack, world, entityPlayer </summary>
///     
		public virtual ItemStack onItemRightClick(ItemStack p_77659_1_, World p_77659_2_, EntityPlayer p_77659_3_)
		{
			int var4 = EntityLiving.getArmorPosition(p_77659_1_) - 1;
			ItemStack var5 = p_77659_3_.getCurrentArmor(var4);

			if (var5 == null)
			{
				p_77659_3_.setCurrentItemOrArmor(var4, p_77659_1_.copy());
				p_77659_1_.stackSize = 0;
			}

			return p_77659_1_;
		}

		public static IIcon func_94602_b(int p_94602_0_)
		{
			switch (p_94602_0_)
			{
				case 0:
					return Items.diamond_helmet.emptySlotIcon;

				case 1:
					return Items.diamond_chestplate.emptySlotIcon;

				case 2:
					return Items.diamond_leggings.emptySlotIcon;

				case 3:
					return Items.diamond_boots.emptySlotIcon;

				default:
					return null;
			}
		}

		public enum ArmorMaterial
		{
			CLOTH("CLOTH", 0, 5, new int[]{1, 3, 2, 1
		}
	   , 15), CHAIN("CHAIN", 1, 15, new int[]{2, 5, 4, 1}, 12), IRON("IRON", 2, 15, new int[]{2, 6, 5, 2}, 9), GOLD("GOLD", 3, 7, new int[]{2, 5, 3, 1}, 25), DIAMOND("DIAMOND", 4, 33, new int[]{3, 8, 6, 3}, 10);
			private int maxDamageFactor;
			private int[] damageReductionAmountArray;
			private int enchantability;

			private static final ItemArmor.ArmorMaterial[] $VALUES = new ItemArmor.ArmorMaterial[]{CLOTH, CHAIN, IRON, GOLD, DIAMOND};
			private static final string __OBFID = "CL_00001768";

			private ArmorMaterial(string p_i1827_1_, int p_i1827_2_, int p_i1827_3_, int[] p_i1827_4_, int p_i1827_5_)
			{
				this.maxDamageFactor = p_i1827_3_;
				this.damageReductionAmountArray = p_i1827_4_;
				this.enchantability = p_i1827_5_;
			}

			public int getDurability(int p_78046_1_)
			{
				return ItemArmor.maxDamageArray[p_78046_1_] * this.maxDamageFactor;
			}

			public int getDamageReductionAmount(int p_78044_1_)
			{
				return this.damageReductionAmountArray[p_78044_1_];
			}

			public int Enchantability
			{
				return this.enchantability;
			}

			public Item func_151685_b()
			{
				return this == CLOTH ? Items.leather : (this == CHAIN ? Items.iron_ingot : (this == GOLD ? Items.gold_ingot : (this == IRON ? Items.iron_ingot : (this == DIAMOND ? Items.diamond : null))));
			}
		}
	}

}