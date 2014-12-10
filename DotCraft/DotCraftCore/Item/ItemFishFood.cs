using System;
using System.Collections;

namespace DotCraftCore.Item
{

	using Maps = com.google.common.collect.Maps;
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Potion = DotCraftCore.Potion.Potion;
	using PotionEffect = DotCraftCore.Potion.PotionEffect;
	using PotionHelper = DotCraftCore.Potion.PotionHelper;
	using IIcon = DotCraftCore.Util.IIcon;
	using World = DotCraftCore.World.World;

	public class ItemFishFood : ItemFood
	{
		private readonly bool field_150907_b;
		private const string __OBFID = "CL_00000032";

		public ItemFishFood(bool p_i45338_1_) : base(0, 0.0F, false)
		{
			this.field_150907_b = p_i45338_1_;
		}

		public override int func_150905_g(ItemStack p_150905_1_)
		{
			ItemFishFood.FishType var2 = ItemFishFood.FishType.func_150978_a(p_150905_1_);
			return this.field_150907_b && var2.func_150973_i() ? var2.func_150970_e() : var2.func_150975_c();
		}

		public override float func_150906_h(ItemStack p_150906_1_)
		{
			ItemFishFood.FishType var2 = ItemFishFood.FishType.func_150978_a(p_150906_1_);
			return this.field_150907_b && var2.func_150973_i() ? var2.func_150977_f() : var2.func_150967_d();
		}

		public virtual string getPotionEffect(ItemStack p_150896_1_)
		{
			return ItemFishFood.FishType.func_150978_a(p_150896_1_) == ItemFishFood.FishType.PUFFERFISH ? PotionHelper.field_151423_m : null;
		}

		public virtual void registerIcons(IIconRegister p_94581_1_)
		{
			ItemFishFood.FishType[] var2 = ItemFishFood.FishType.values();
			int var3 = var2.Length;

			for (int var4 = 0; var4 < var3; ++var4)
			{
				ItemFishFood.FishType var5 = var2[var4];
				var5.func_150968_a(p_94581_1_);
			}
		}

		protected internal override void onFoodEaten(ItemStack p_77849_1_, World p_77849_2_, EntityPlayer p_77849_3_)
		{
			ItemFishFood.FishType var4 = ItemFishFood.FishType.func_150978_a(p_77849_1_);

			if (var4 == ItemFishFood.FishType.PUFFERFISH)
			{
				p_77849_3_.addPotionEffect(new PotionEffect(Potion.poison.id, 1200, 3));
				p_77849_3_.addPotionEffect(new PotionEffect(Potion.hunger.id, 300, 2));
				p_77849_3_.addPotionEffect(new PotionEffect(Potion.confusion.id, 300, 1));
			}

			base.onFoodEaten(p_77849_1_, p_77849_2_, p_77849_3_);
		}

///    
///     <summary> * Gets an icon index based on an item's damage value </summary>
///     
		public virtual IIcon getIconFromDamage(int p_77617_1_)
		{
			ItemFishFood.FishType var2 = ItemFishFood.FishType.func_150974_a(p_77617_1_);
			return this.field_150907_b && var2.func_150973_i() ? var2.func_150979_h() : var2.func_150971_g();
		}

///    
///     <summary> * This returns the sub items </summary>
///     
		public virtual void getSubItems(Item p_150895_1_, CreativeTabs p_150895_2_, IList p_150895_3_)
		{
			ItemFishFood.FishType[] var4 = ItemFishFood.FishType.values();
			int var5 = var4.Length;

			for (int var6 = 0; var6 < var5; ++var6)
			{
				ItemFishFood.FishType var7 = var4[var6];

				if (!this.field_150907_b || var7.func_150973_i())
				{
					p_150895_3_.Add(new ItemStack(this, 1, var7.func_150976_a()));
				}
			}
		}

///    
///     <summary> * Returns the unlocalized name of this item. This version accepts an ItemStack so different stacks can have
///     * different names based on their damage or NBT. </summary>
///     
		public virtual string getUnlocalizedName(ItemStack p_77667_1_)
		{
			ItemFishFood.FishType var2 = ItemFishFood.FishType.func_150978_a(p_77667_1_);
			return this.UnlocalizedName + "." + var2.func_150972_b() + "." + (this.field_150907_b && var2.func_150973_i() ? "cooked" : "raw");
		}

		public enum FishType
		{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			COD("COD", 0, 0, "cod", 2, 0.1F, 5, 0.6F),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			SALMON("SALMON", 1, 1, "salmon", 2, 0.1F, 6, 0.8F),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			CLOWNFISH("CLOWNFISH", 2, 2, "clownfish", 1, 0.1F),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			PUFFERFISH("PUFFERFISH", 3, 3, "pufferfish", 1, 0.1F);
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private static final Map field_150983_e = Maps.newHashMap();
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private final int field_150980_f;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private final String field_150981_g;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private IIcon field_150993_h;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private IIcon field_150994_i;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private final int field_150991_j;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private final float field_150992_k;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private final int field_150989_l;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private final float field_150990_m;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private boolean field_150987_n = false;

			@private static final ItemFishFood.FishType[] $VALUES = new ItemFishFood.FishType[]{COD, SALMON, CLOWNFISH, PUFFERFISH
		}
			private const string __OBFID = "CL_00000033";

			private FishType(string p_i45336_1_, int p_i45336_2_, int p_i45336_3_, string p_i45336_4_, int p_i45336_5_, float p_i45336_6_, int p_i45336_7_, float p_i45336_8_)
			{
				this.field_150980_f = p_i45336_3_;
				this.field_150981_g = p_i45336_4_;
				this.field_150991_j = p_i45336_5_;
				this.field_150992_k = p_i45336_6_;
				this.field_150989_l = p_i45336_7_;
				this.field_150990_m = p_i45336_8_;
				this.field_150987_n = true;
			}

			private FishType(string p_i45337_1_, int p_i45337_2_, int p_i45337_3_, string p_i45337_4_, int p_i45337_5_, float p_i45337_6_)
			{
				this.field_150980_f = p_i45337_3_;
				this.field_150981_g = p_i45337_4_;
				this.field_150991_j = p_i45337_5_;
				this.field_150992_k = p_i45337_6_;
				this.field_150989_l = 0;
				this.field_150990_m = 0.0F;
				this.field_150987_n = false;
			}

			public virtual int func_150976_a()
			{
				return this.field_150980_f;
			}

			public virtual string func_150972_b()
			{
				return this.field_150981_g;
			}

			public virtual int func_150975_c()
			{
				return this.field_150991_j;
			}

			public virtual float func_150967_d()
			{
				return this.field_150992_k;
			}

			public virtual int func_150970_e()
			{
				return this.field_150989_l;
			}

			public virtual float func_150977_f()
			{
				return this.field_150990_m;
			}

			public virtual void func_150968_a(IIconRegister p_150968_1_)
			{
				this.field_150993_h = p_150968_1_.registerIcon("fish_" + this.field_150981_g + "_raw");

				if (this.field_150987_n)
				{
					this.field_150994_i = p_150968_1_.registerIcon("fish_" + this.field_150981_g + "_cooked");
				}
			}

			public virtual IIcon func_150971_g()
			{
				return this.field_150993_h;
			}

			public virtual IIcon func_150979_h()
			{
				return this.field_150994_i;
			}

			public virtual bool func_150973_i()
			{
				return this.field_150987_n;
			}

			public static ItemFishFood.FishType func_150974_a(int p_150974_0_)
			{
				ItemFishFood.FishType var1 = (ItemFishFood.FishType)field_150983_e.get(Convert.ToInt32(p_150974_0_));
				return var1 == null ? COD : var1;
			}

			public static ItemFishFood.FishType func_150978_a(ItemStack p_150978_0_)
			{
				return p_150978_0_.Item is ItemFishFood ? func_150974_a(p_150978_0_.ItemDamage) : COD;
			}

			static ItemFishFood()
			{
				ItemFishFood.FishType[] var0 = values();
				int var1 = var0.Length;

				for (int var2 = 0; var2 < var1; ++var2)
				{
					ItemFishFood.FishType var3 = var0[var2];
					field_150983_e.put(Convert.ToInt32(var3.func_150976_a()), var3);
				}
			}
		}
	}

}