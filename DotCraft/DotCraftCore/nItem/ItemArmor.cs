using DotCraftCore.nBlock;
using DotCraftCore.nCommand;
using DotCraftCore.nDispenser;
using DotCraftCore.nEntity;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nNBT;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
namespace DotCraftCore.nItem
{
	public class ItemArmor : Item
	{
	/// <summary> Holds the 'base' maxDamage that each armorType have.  </summary>
		private static readonly int[] maxDamageArray = new int[] {11, 16, 15, 13};
		private static readonly string[] CLOTH_OVERLAY_NAMES = new string[] {"leather_helmet_overlay", "leather_chestplate_overlay", "leather_leggings_overlay", "leather_boots_overlay"};
		public static readonly string[] EMPTY_SLOT_NAMES = new string[] {"empty_armor_slot_helmet", "empty_armor_slot_chestplate", "empty_armor_slot_leggings", "empty_armor_slot_boots"};
		private static readonly IBehaviorDispenseItem dispenserBehavior = new InnerBehaviorDispenseItem();

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
		private readonly ArmorMaterial material;

		public ItemArmor(ArmorMaterial p_i45325_1_, int p_i45325_2_, int p_i45325_3_)
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
			return this.material == ArmorMaterial.Cloth;
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
		public virtual ArmorMaterial ItemArmorMaterial
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
			return this.material != ArmorMaterial.Cloth ? false : (!p_82816_1_.hasTagCompound() ? false : (!p_82816_1_.TagCompound.func_150297_b("display", 10) ? false : p_82816_1_.TagCompound.getCompoundTag("display").func_150297_b("color", 3)));
		}

///    
///     <summary> * Return the color for the specified armor ItemStack. </summary>
///     
		public virtual int getColor(ItemStack p_82814_1_)
		{
			if (this.material != ArmorMaterial.Cloth)
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
///     <summary> * Remove the color from the specified armor ItemStack. </summary>
///     
		public virtual void removeColor(ItemStack p_82815_1_)
		{
			if (this.material == ArmorMaterial.Cloth)
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
			if (this.material != ArmorMaterial.Cloth)
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

		public class ArmorMaterial
		{
            public enum ArmorMaterialEnum
            {
			    CLOTH = 0,
                CHAIN = 1,
                IRON = 2,
                GOLD = 3,
                DIAMOND = 4
            }

            public static readonly ArmorMaterial Cloth = new ArmorMaterial(ArmorMaterialEnum.CLOTH, 5, new int[]{1, 3, 2, 1}, 15);
            public static readonly ArmorMaterial Chain = new ArmorMaterial(ArmorMaterialEnum.CHAIN, 15, new int[]{2, 5, 4, 1}, 12);
            public static readonly ArmorMaterial Iron = new ArmorMaterial(ArmorMaterialEnum.IRON, 15, new int[]{2, 6, 5, 2}, 9);
            public static readonly ArmorMaterial Gold = new ArmorMaterial(ArmorMaterialEnum.GOLD, 7, new int[]{2, 5, 3, 1}, 25);
            public static readonly ArmorMaterial Diamond = new ArmorMaterial(ArmorMaterialEnum.DIAMOND, 33, new int[]{3, 8, 6, 3}, 10);

			private int maxDamageFactor;
			private int[] damageReductionAmountArray;

			private ArmorMaterial(ArmorMaterialEnum value, int damageFactor, int[] damageReduction, int enchant)
			{
                this.Enum = value;
				this.maxDamageFactor = damageFactor;
				this.damageReductionAmountArray = damageReduction;
				this.Enchantability = enchant;
			}

            public virtual ArmorMaterialEnum Enum
            {
                get;
                protected set;
            }

			public int getDurability(int index)
			{
				return ItemArmor.maxDamageArray[index] * this.maxDamageFactor;
			}

			public int getDamageReductionAmount(int index)
			{
				return this.damageReductionAmountArray[index];
			}

			public virtual int Enchantability
			{
				get;
                protected set;
			}

			public Item func_151685_b()
			{
                switch (this.Enum)
                {
                    case ArmorMaterialEnum.CLOTH:
                        return Items.leather;
                    case ArmorMaterialEnum.CHAIN:
                        return Items.iron_ingot;
                    case ArmorMaterialEnum.GOLD:
                        return Items.gold_ingot;
                    case ArmorMaterialEnum.IRON:
                        return Items.iron_ingot;
                    case ArmorMaterialEnum.DIAMOND:
                        return Items.diamond;
                    default:
                        return null;
                }
			}
		}

        private class InnerBehaviorDispenseItem : BehaviorDefaultDispenseItem
        {
            protected override ItemStack dispenseStack(IBlockSource p_82487_1_, ItemStack p_82487_2_)
        	{
    			EnumFacing var3 = BlockDispenser.func_149937_b(p_82487_1_.getBlockMetadata());
    			int var4 = p_82487_1_.getXInt() + var3.getFrontOffsetX();
    			int var5 = p_82487_1_.getYInt() + var3.getFrontOffsetY();
    			int var6 = p_82487_1_.getZInt() + var3.getFrontOffsetZ();
    			AxisAlignedBB var7 = AxisAlignedBB.getBoundingBox((double)var4, (double)var5, (double)var6, (double)(var4 + 1), (double)(var5 + 1), (double)(var6 + 1));
    			List var8 = p_82487_1_.getWorld().selectEntitiesWithinAABB(typeof(EntityLivingBase), var7, new IEntitySelector.ArmoredMob(p_82487_2_));
    
    			if (var8.size() > 0)
    			{
    				EntityLivingBase var9 = (EntityLivingBase)var8.get(0);
    				int var10 = var9 is EntityPlayer ? 1 : 0;
    				int var11 = EntityLiving.getArmorPosition(p_82487_2_);
    				ItemStack var12 = p_82487_2_.copy();
    				var12.stackSize = 1;
    				var9.setCurrentItemOrArmor(var11 - var10, var12);
    
    				if (var9 is EntityLiving)
    				{
    					((EntityLiving)var9).setEquipmentDropChance(var11, 2.0F);
    				}
    
    				--p_82487_2_.stackSize;
    				return p_82487_2_;
    			}
    			else
    			{
    				return base.dispenseStack(p_82487_1_, p_82487_2_);
    			}
            }
        }
	}

}