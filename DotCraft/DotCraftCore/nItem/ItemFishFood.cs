using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nInventory;
using DotCraftCore.nPotion;
using DotCraftCore.nWorld;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DotCraftCore.nItem
{
	public class ItemFishFood : ItemFood
	{
		private readonly bool field_150907_b;

		public ItemFishFood(bool p_i45338_1_) : base(0, 0.0F, false)
		{
			this.field_150907_b = p_i45338_1_;
		}

		public override int func_150905_g(ItemStack p_150905_1_)
		{
            EnumFishType var2 = default(EnumFishType).func_150978_a(p_150905_1_);
			return this.field_150907_b && var2.func_150973_i() ? var2.func_150970_e() : var2.func_150975_c();
		}

		public override float func_150906_h(ItemStack p_150906_1_)
		{
            EnumFishType var2 = default(EnumFishType).func_150978_a(p_150906_1_);
			return this.field_150907_b && var2.func_150973_i() ? var2.func_150977_f() : var2.func_150967_d();
		}

		public virtual string getPotionEffect(ItemStack p_150896_1_)
		{
            return default(EnumFishType).func_150978_a(p_150896_1_) == EnumFishType.PUFFERFISH ? PotionHelper.field_151423_m : null;
		}

		protected internal override void onFoodEaten(ItemStack p_77849_1_, World p_77849_2_, EntityPlayer p_77849_3_)
		{
            EnumFishType var4 = default(EnumFishType).func_150978_a(p_77849_1_);

            if (var4 == EnumFishType.PUFFERFISH)
			{
				p_77849_3_.addPotionEffect(new PotionEffect(Potion.poison.id, 1200, 3));
				p_77849_3_.addPotionEffect(new PotionEffect(Potion.hunger.id, 300, 2));
				p_77849_3_.addPotionEffect(new PotionEffect(Potion.confusion.id, 300, 1));
			}

			base.onFoodEaten(p_77849_1_, p_77849_2_, p_77849_3_);
		}

///    
///     <summary> * This returns the sub items </summary>
///     
		public virtual void getSubItems(Item p_150895_1_, CreativeTabs p_150895_2_, IList p_150895_3_)
		{
            var values = Enum.GetValues(typeof(EnumFishType));

            for (int var6 = 0; var6 < values.Length; ++var6)
			{
				EnumFishType var7 = (EnumFishType)values.GetValue(var6);

				if (!this.field_150907_b || var7.func_150973_i())
				{
					p_150895_3_.Add(new ItemStack(this, 1, (int)var7));
				}
			}
		}

///    
///     <summary> * Returns the unlocalized name of this item. This version accepts an ItemStack so different stacks can have
///     * different names based on their damage or NBT. </summary>
///     
		public virtual string getUnlocalizedName(ItemStack p_77667_1_)
		{
			EnumFishType var2 = default(EnumFishType).func_150978_a(p_77667_1_);
			return this.UnlocalizedName + "." + var2.func_150972_b() + "." + (this.field_150907_b && var2.func_150973_i() ? "cooked" : "raw");
		}
    }

	public enum EnumFishType
	{
        COD = 0,
        SALMON = 1,
        CLOWNFISH = 2,
        PUFFERFISH = 3
    }

    static class EnumFishTypeExtension
    {
        private static readonly Dictionary<Int32, EnumFishType> mapping;
		private static readonly String[] canonicalNameMap;
        private static readonly int[] rawHungerRestoreMap;
        private static readonly float[] field_150992_kMap;
        private static readonly int[] cookedHungerRestoreMap;
        private static readonly float[] field_150990_mMap;
        private static readonly bool[] cookableMap;

        [Obsolete("Use integer cast")]
        public static int func_150976_a(this EnumFishType e)
		{
			return (int)e;
		}

        public static string func_150972_b(this EnumFishType e)
		{
            return EnumFishTypeExtension.canonicalNameMap[(int)e];
		}

        public static int func_150975_c(this EnumFishType e)
		{
            return EnumFishTypeExtension.rawHungerRestoreMap[(int)e];
		}

        public static float func_150967_d(this EnumFishType e)
		{
            return EnumFishTypeExtension.field_150992_kMap[(int)e];
		}

        public static int func_150970_e(this EnumFishType e)
		{
            return EnumFishTypeExtension.cookedHungerRestoreMap[(int)e];
		}

        public static float func_150977_f(this EnumFishType e)
		{
            return EnumFishTypeExtension.field_150990_mMap[(int)e];
		}

        public static bool func_150973_i(this EnumFishType e)
		{
            return EnumFishTypeExtension.cookableMap[(int)e];
		}

        public static EnumFishType func_150974_a(this EnumFishType e, int p_150974_0_)
		{
			return func_150974_a_internal(e, p_150974_0_);
		}

        private static EnumFishType func_150974_a_internal(EnumFishType e, int p_150974_0_){
            EnumFishType var1 = (EnumFishType)mapping[Convert.ToInt32(p_150974_0_)];
			return var1 == null ? EnumFishType.COD : var1;
        }

        public static EnumFishType func_150978_a(this EnumFishType e, ItemStack p_150978_0_)
		{
			return p_150978_0_.Item is ItemFishFood ? func_150974_a_internal(e, p_150978_0_.ItemDamage) : EnumFishType.COD;
		}

		static EnumFishTypeExtension()
		{
            canonicalNameMap = new string[] {"cod", "salmon", "clownfish", "pufferfish"};
			rawHungerRestoreMap = new int[] {2, 2, 1, 1};
			field_150992_kMap = new float[] {0.1F, 0.1F, 0.1F, 0.1F};
			cookedHungerRestoreMap = new int[] {5, 6, 0, 0};
			field_150990_mMap = new float[] {0.6F, 0.8F, 0.0F, 0.0F};
			cookableMap = new bool[] {true, true, false, false};

            var values = Enum.GetValues(typeof(EnumFishType));
            foreach (EnumFishType e in values)
            {
                mapping.Add((int)e, e);
            }
		}
	}
}