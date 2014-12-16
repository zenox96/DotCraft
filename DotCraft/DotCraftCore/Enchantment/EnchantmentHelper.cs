using System;
using System.Collections;

namespace DotCraftCore.Enchantment
{

	using Entity = DotCraftCore.Entity.Entity;
	using EntityLivingBase = DotCraftCore.Entity.EntityLivingBase;
	using EnumCreatureAttribute = DotCraftCore.Entity.EnumCreatureAttribute;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Items = DotCraftCore.Init.Items;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using NBTTagList = DotCraftCore.NBT.NBTTagList;
	using DamageSource = DotCraftCore.Util.DamageSource;
	using WeightedRandom = DotCraftCore.Util.WeightedRandom;

	public class EnchantmentHelper
	{
	/// <summary> Is the random seed of enchantment effects.  </summary>
		private static readonly Random enchantmentRand = new Random();

///    
///     <summary> * Used to calculate the extra armor of enchantments on armors equipped on player. </summary>
///     
		private static readonly EnchantmentHelper.ModifierDamage enchantmentModifierDamage = new EnchantmentHelper.ModifierDamage(null);

///    
///     <summary> * Used to calculate the (magic) extra damage done by enchantments on current equipped item of player. </summary>
///     
		private static readonly EnchantmentHelper.ModifierLiving enchantmentModifierLiving = new EnchantmentHelper.ModifierLiving(null);
		private static readonly EnchantmentHelper.HurtIterator field_151388_d = new EnchantmentHelper.HurtIterator(null);
		private static readonly EnchantmentHelper.DamageIterator field_151389_e = new EnchantmentHelper.DamageIterator(null);
		

///    
///     <summary> * Returns the level of enchantment on the ItemStack passed. </summary>
///     
		public static int getEnchantmentLevel(int p_77506_0_, ItemStack p_77506_1_)
		{
			if (p_77506_1_ == null)
			{
				return 0;
			}
			else
			{
				NBTTagList var2 = p_77506_1_.EnchantmentTagList;

				if (var2 == null)
				{
					return 0;
				}
				else
				{
					for (int var3 = 0; var3 < var2.tagCount(); ++var3)
					{
						short var4 = var2.getCompoundTagAt(var3).getShort("id");
						short var5 = var2.getCompoundTagAt(var3).getShort("lvl");

						if (var4 == p_77506_0_)
						{
							return var5;
						}
					}

					return 0;
				}
			}
		}

///    
///     <summary> * Return the enchantments for the specified stack. </summary>
///     
		public static IDictionary getEnchantments(ItemStack p_82781_0_)
		{
			LinkedHashMap var1 = new LinkedHashMap();
			NBTTagList var2 = p_82781_0_.Item == Items.enchanted_book ? Items.enchanted_book.func_92110_g(p_82781_0_) : p_82781_0_.EnchantmentTagList;

			if (var2 != null)
			{
				for (int var3 = 0; var3 < var2.tagCount(); ++var3)
				{
					short var4 = var2.getCompoundTagAt(var3).getShort("id");
					short var5 = var2.getCompoundTagAt(var3).getShort("lvl");
					var1.put(Convert.ToInt32(var4), Convert.ToInt32(var5));
				}
			}

			return var1;
		}

///    
///     <summary> * Set the enchantments for the specified stack. </summary>
///     
		public static void setEnchantments(IDictionary p_82782_0_, ItemStack p_82782_1_)
		{
			NBTTagList var2 = new NBTTagList();
			IEnumerator var3 = p_82782_0_.Keys.GetEnumerator();

			while (var3.MoveNext())
			{
				int var4 = (int)((int?)var3.Current);
				NBTTagCompound var5 = new NBTTagCompound();
				var5.setShort("id", (short)var4);
				var5.setShort("lvl", (int)(short)((int?)p_82782_0_[Convert.ToInt32(var4)]));
				var2.appendTag(var5);

				if (p_82782_1_.Item == Items.enchanted_book)
				{
					Items.enchanted_book.addEnchantment(p_82782_1_, new EnchantmentData(var4, (int)((int?)p_82782_0_[Convert.ToInt32(var4)])));
				}
			}

			if (var2.tagCount() > 0)
			{
				if (p_82782_1_.Item != Items.enchanted_book)
				{
					p_82782_1_.setTagInfo("ench", var2);
				}
			}
			else if (p_82782_1_.hasTagCompound())
			{
				p_82782_1_.TagCompound.removeTag("ench");
			}
		}

///    
///     <summary> * Returns the biggest level of the enchantment on the array of ItemStack passed. </summary>
///     
		public static int getMaxEnchantmentLevel(int p_77511_0_, ItemStack[] p_77511_1_)
		{
			if (p_77511_1_ == null)
			{
				return 0;
			}
			else
			{
				int var2 = 0;
				ItemStack[] var3 = p_77511_1_;
				int var4 = p_77511_1_.Length;

				for (int var5 = 0; var5 < var4; ++var5)
				{
					ItemStack var6 = var3[var5];
					int var7 = getEnchantmentLevel(p_77511_0_, var6);

					if (var7 > var2)
					{
						var2 = var7;
					}
				}

				return var2;
			}
		}

///    
///     <summary> * Executes the enchantment modifier on the ItemStack passed. </summary>
///     
		private static void applyEnchantmentModifier(EnchantmentHelper.IModifier p_77518_0_, ItemStack p_77518_1_)
		{
			if (p_77518_1_ != null)
			{
				NBTTagList var2 = p_77518_1_.EnchantmentTagList;

				if (var2 != null)
				{
					for (int var3 = 0; var3 < var2.tagCount(); ++var3)
					{
						short var4 = var2.getCompoundTagAt(var3).getShort("id");
						short var5 = var2.getCompoundTagAt(var3).getShort("lvl");

						if (Enchantment.enchantmentsList[var4] != null)
						{
							p_77518_0_.calculateModifier(Enchantment.enchantmentsList[var4], var5);
						}
					}
				}
			}
		}

///    
///     <summary> * Executes the enchantment modifier on the array of ItemStack passed. </summary>
///     
		private static void applyEnchantmentModifierArray(EnchantmentHelper.IModifier p_77516_0_, ItemStack[] p_77516_1_)
		{
			ItemStack[] var2 = p_77516_1_;
			int var3 = p_77516_1_.Length;

			for (int var4 = 0; var4 < var3; ++var4)
			{
				ItemStack var5 = var2[var4];
				applyEnchantmentModifier(p_77516_0_, var5);
			}
		}

///    
///     <summary> * Returns the modifier of protection enchantments on armors equipped on player. </summary>
///     
		public static int getEnchantmentModifierDamage(ItemStack[] p_77508_0_, DamageSource p_77508_1_)
		{
			enchantmentModifierDamage.damageModifier = 0;
			enchantmentModifierDamage.source = p_77508_1_;
			applyEnchantmentModifierArray(enchantmentModifierDamage, p_77508_0_);

			if (enchantmentModifierDamage.damageModifier > 25)
			{
				enchantmentModifierDamage.damageModifier = 25;
			}

			return (enchantmentModifierDamage.damageModifier + 1 >> 1) + enchantmentRand.Next((enchantmentModifierDamage.damageModifier >> 1) + 1);
		}

///    
///     <summary> * Return the (magic) extra damage of the enchantments on player equipped item. </summary>
///     
		public static float getEnchantmentModifierLiving(EntityLivingBase p_77512_0_, EntityLivingBase p_77512_1_)
		{
			return func_152377_a(p_77512_0_.HeldItem, p_77512_1_.CreatureAttribute);
		}

		public static float func_152377_a(ItemStack p_152377_0_, EnumCreatureAttribute p_152377_1_)
		{
			enchantmentModifierLiving.livingModifier = 0.0F;
			enchantmentModifierLiving.entityLiving = p_152377_1_;
			applyEnchantmentModifier(enchantmentModifierLiving, p_152377_0_);
			return enchantmentModifierLiving.livingModifier;
		}

		public static void func_151384_a(EntityLivingBase p_151384_0_, Entity p_151384_1_)
		{
			field_151388_d.field_151363_b = p_151384_1_;
			field_151388_d.field_151364_a = p_151384_0_;
			applyEnchantmentModifierArray(field_151388_d, p_151384_0_.LastActiveItems);

			if (p_151384_1_ is EntityPlayer)
			{
				applyEnchantmentModifier(field_151388_d, p_151384_0_.HeldItem);
			}
		}

		public static void func_151385_b(EntityLivingBase p_151385_0_, Entity p_151385_1_)
		{
			field_151389_e.field_151366_a = p_151385_0_;
			field_151389_e.field_151365_b = p_151385_1_;
			applyEnchantmentModifierArray(field_151389_e, p_151385_0_.LastActiveItems);

			if (p_151385_0_ is EntityPlayer)
			{
				applyEnchantmentModifier(field_151389_e, p_151385_0_.HeldItem);
			}
		}

///    
///     <summary> * Returns the knockback value of enchantments on equipped player item. </summary>
///     
		public static int getKnockbackModifier(EntityLivingBase p_77507_0_, EntityLivingBase p_77507_1_)
		{
			return getEnchantmentLevel(Enchantment.knockback.effectId, p_77507_0_.HeldItem);
		}

		public static int getFireAspectModifier(EntityLivingBase p_90036_0_)
		{
			return getEnchantmentLevel(Enchantment.fireAspect.effectId, p_90036_0_.HeldItem);
		}

///    
///     <summary> * Returns the 'Water Breathing' modifier of enchantments on player equipped armors. </summary>
///     
		public static int getRespiration(EntityLivingBase p_77501_0_)
		{
			return getMaxEnchantmentLevel(Enchantment.respiration.effectId, p_77501_0_.LastActiveItems);
		}

///    
///     <summary> * Return the extra efficiency of tools based on enchantments on equipped player item. </summary>
///     
		public static int getEfficiencyModifier(EntityLivingBase p_77509_0_)
		{
			return getEnchantmentLevel(Enchantment.efficiency.effectId, p_77509_0_.HeldItem);
		}

///    
///     <summary> * Returns the silk touch status of enchantments on current equipped item of player. </summary>
///     
		public static bool getSilkTouchModifier(EntityLivingBase p_77502_0_)
		{
			return getEnchantmentLevel(Enchantment.silkTouch.effectId, p_77502_0_.HeldItem) > 0;
		}

///    
///     <summary> * Returns the fortune enchantment modifier of the current equipped item of player. </summary>
///     
		public static int getFortuneModifier(EntityLivingBase p_77517_0_)
		{
			return getEnchantmentLevel(Enchantment.fortune.effectId, p_77517_0_.HeldItem);
		}

		public static int func_151386_g(EntityLivingBase p_151386_0_)
		{
			return getEnchantmentLevel(Enchantment.field_151370_z.effectId, p_151386_0_.HeldItem);
		}

		public static int func_151387_h(EntityLivingBase p_151387_0_)
		{
			return getEnchantmentLevel(Enchantment.field_151369_A.effectId, p_151387_0_.HeldItem);
		}

///    
///     <summary> * Returns the looting enchantment modifier of the current equipped item of player. </summary>
///     
		public static int getLootingModifier(EntityLivingBase p_77519_0_)
		{
			return getEnchantmentLevel(Enchantment.looting.effectId, p_77519_0_.HeldItem);
		}

///    
///     <summary> * Returns the aqua affinity status of enchantments on current equipped item of player. </summary>
///     
		public static bool getAquaAffinityModifier(EntityLivingBase p_77510_0_)
		{
			return getMaxEnchantmentLevel(Enchantment.aquaAffinity.effectId, p_77510_0_.LastActiveItems) > 0;
		}

		public static ItemStack func_92099_a(Enchantment p_92099_0_, EntityLivingBase p_92099_1_)
		{
			ItemStack[] var2 = p_92099_1_.LastActiveItems;
			int var3 = var2.Length;

			for (int var4 = 0; var4 < var3; ++var4)
			{
				ItemStack var5 = var2[var4];

				if (var5 != null && getEnchantmentLevel(p_92099_0_.effectId, var5) > 0)
				{
					return var5;
				}
			}

			return null;
		}

///    
///     <summary> * Returns the enchantability of itemstack, it's uses a singular formula for each index (2nd parameter: 0, 1 and 2),
///     * cutting to the max enchantability power of the table (3rd parameter) </summary>
///     
		public static int calcItemStackEnchantability(Random p_77514_0_, int p_77514_1_, int p_77514_2_, ItemStack p_77514_3_)
		{
			Item var4 = p_77514_3_.Item;
			int var5 = var4.ItemEnchantability;

			if (var5 <= 0)
			{
				return 0;
			}
			else
			{
				if (p_77514_2_ > 15)
				{
					p_77514_2_ = 15;
				}

				int var6 = p_77514_0_.Next(8) + 1 + (p_77514_2_ >> 1) + p_77514_0_.Next(p_77514_2_ + 1);
				return p_77514_1_ == 0 ? Math.Max(var6 / 3, 1) : (p_77514_1_ == 1 ? var6 * 2 / 3 + 1 : Math.Max(var6, p_77514_2_ * 2));
			}
		}

///    
///     <summary> * Adds a random enchantment to the specified item. Args: random, itemStack, enchantabilityLevel </summary>
///     
		public static ItemStack addRandomEnchantment(Random p_77504_0_, ItemStack p_77504_1_, int p_77504_2_)
		{
			IList var3 = buildEnchantmentList(p_77504_0_, p_77504_1_, p_77504_2_);
			bool var4 = p_77504_1_.Item == Items.book;

			if (var4)
			{
				p_77504_1_.func_150996_a(Items.enchanted_book);
			}

			if (var3 != null)
			{
				IEnumerator var5 = var3.GetEnumerator();

				while (var5.MoveNext())
				{
					EnchantmentData var6 = (EnchantmentData)var5.Current;

					if (var4)
					{
						Items.enchanted_book.addEnchantment(p_77504_1_, var6);
					}
					else
					{
						p_77504_1_.addEnchantment(var6.enchantmentobj, var6.enchantmentLevel);
					}
				}
			}

			return p_77504_1_;
		}

///    
///     <summary> * Create a list of random EnchantmentData (enchantments) that can be added together to the ItemStack, the 3rd
///     * parameter is the total enchantability level. </summary>
///     
		public static IList buildEnchantmentList(Random p_77513_0_, ItemStack p_77513_1_, int p_77513_2_)
		{
			Item var3 = p_77513_1_.Item;
			int var4 = var3.ItemEnchantability;

			if (var4 <= 0)
			{
				return null;
			}
			else
			{
				var4 /= 2;
				var4 = 1 + p_77513_0_.Next((var4 >> 1) + 1) + p_77513_0_.Next((var4 >> 1) + 1);
				int var5 = var4 + p_77513_2_;
				float var6 = (p_77513_0_.nextFloat() + p_77513_0_.nextFloat() - 1.0F) * 0.15F;
				int var7 = (int)((float)var5 * (1.0F + var6) + 0.5F);

				if (var7 < 1)
				{
					var7 = 1;
				}

				ArrayList var8 = null;
				IDictionary var9 = mapEnchantmentData(var7, p_77513_1_);

				if (var9 != null && !var9.Count == 0)
				{
					EnchantmentData var10 = (EnchantmentData)WeightedRandom.getRandomItem(p_77513_0_, var9.Values);

					if (var10 != null)
					{
						var8 = new ArrayList();
						var8.Add(var10);

						for (int var11 = var7; p_77513_0_.Next(50) <= var11; var11 >>= 1)
						{
							IEnumerator var12 = var9.Keys.GetEnumerator();

							while (var12.MoveNext())
							{
								int? var13 = (int?)var12.Current;
								bool var14 = true;
								IEnumerator var15 = var8.GetEnumerator();

								while (true)
								{
									if (var15.MoveNext())
									{
										EnchantmentData var16 = (EnchantmentData)var15.Current;

										if (var16.enchantmentobj.canApplyTogether(Enchantment.enchantmentsList[(int)var13]))
										{
											continue;
										}

										var14 = false;
									}

									if (!var14)
									{
										var12.remove();
									}

									break;
								}
							}

							if (!var9.Count == 0)
							{
								EnchantmentData var17 = (EnchantmentData)WeightedRandom.getRandomItem(p_77513_0_, var9.Values);
								var8.Add(var17);
							}
						}
					}
				}

				return var8;
			}
		}

///    
///     <summary> * Creates a 'Map' of EnchantmentData (enchantments) possible to add on the ItemStack and the enchantability level
///     * passed. </summary>
///     
		public static IDictionary mapEnchantmentData(int p_77505_0_, ItemStack p_77505_1_)
		{
			Item var2 = p_77505_1_.Item;
			Hashtable var3 = null;
			bool var4 = p_77505_1_.Item == Items.book;
			Enchantment[] var5 = Enchantment.enchantmentsList;
			int var6 = var5.Length;

			for (int var7 = 0; var7 < var6; ++var7)
			{
				Enchantment var8 = var5[var7];

				if (var8 != null && (var8.type.canEnchantItem(var2) || var4))
				{
					for (int var9 = var8.MinLevel; var9 <= var8.MaxLevel; ++var9)
					{
						if (p_77505_0_ >= var8.getMinEnchantability(var9) && p_77505_0_ <= var8.getMaxEnchantability(var9))
						{
							if (var3 == null)
							{
								var3 = new Hashtable();
							}

							var3.Add(Convert.ToInt32(var8.effectId), new EnchantmentData(var8, var9));
						}
					}
				}
			}

			return var3;
		}

		internal sealed class DamageIterator : EnchantmentHelper.IModifier
		{
			public EntityLivingBase field_151366_a;
			public Entity field_151365_b;
			

			private DamageIterator()
			{
			}

			public void calculateModifier(Enchantment p_77493_1_, int p_77493_2_)
			{
				p_77493_1_.func_151368_a(this.field_151366_a, this.field_151365_b, p_77493_2_);
			}

			internal DamageIterator(object p_i45359_1_) : this()
			{
			}
		}

		internal sealed class HurtIterator : EnchantmentHelper.IModifier
		{
			public EntityLivingBase field_151364_a;
			public Entity field_151363_b;
			

			private HurtIterator()
			{
			}

			public void calculateModifier(Enchantment p_77493_1_, int p_77493_2_)
			{
				p_77493_1_.func_151367_b(this.field_151364_a, this.field_151363_b, p_77493_2_);
			}

			internal HurtIterator(object p_i45360_1_) : this()
			{
			}
		}

		internal interface IModifier
		{
			void calculateModifier(Enchantment p_77493_1_, int p_77493_2_);
		}

		internal sealed class ModifierDamage : EnchantmentHelper.IModifier
		{
			public int damageModifier;
			public DamageSource source;
			

			private ModifierDamage()
			{
			}

			public void calculateModifier(Enchantment p_77493_1_, int p_77493_2_)
			{
				this.damageModifier += p_77493_1_.calcModifierDamage(p_77493_2_, this.source);
			}

			internal ModifierDamage(object p_i1929_1_) : this()
			{
			}
		}

		internal sealed class ModifierLiving : EnchantmentHelper.IModifier
		{
			public float livingModifier;
			public EnumCreatureAttribute entityLiving;
			

			private ModifierLiving()
			{
			}

			public void calculateModifier(Enchantment p_77493_1_, int p_77493_2_)
			{
				this.livingModifier += p_77493_1_.func_152376_a(p_77493_2_, this.entityLiving);
			}

			internal ModifierLiving(object p_i1928_1_) : this()
			{
			}
		}
	}

}