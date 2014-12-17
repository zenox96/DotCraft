using System;
using System.Collections;

namespace DotCraftCore.nItem
{

	using Enchantment = DotCraftCore.enchantment.Enchantment;
	using EnchantmentData = DotCraftCore.enchantment.EnchantmentData;
	using EnchantmentHelper = DotCraftCore.enchantment.EnchantmentHelper;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Items = DotCraftCore.init.Items;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using NBTTagList = DotCraftCore.nNBT.NBTTagList;
	using WeightedRandomChestContent = DotCraftCore.nUtil.WeightedRandomChestContent;

	public class ItemEnchantedBook : Item
	{
		

		public virtual bool hasEffect(ItemStack p_77636_1_)
		{
			return true;
		}

///    
///     <summary> * Checks isDamagable and if it cannot be stacked </summary>
///     
		public virtual bool isItemTool(ItemStack p_77616_1_)
		{
			return false;
		}

///    
///     <summary> * Return an item rarity from EnumRarity </summary>
///     
		public virtual EnumRarity getRarity(ItemStack p_77613_1_)
		{
			return this.func_92110_g(p_77613_1_).tagCount() > 0 ? EnumRarity.uncommon : base.getRarity(p_77613_1_);
		}

		public virtual NBTTagList func_92110_g(ItemStack p_92110_1_)
		{
			return p_92110_1_.stackTagCompound != null && p_92110_1_.stackTagCompound.func_150297_b("StoredEnchantments", 9) ? (NBTTagList)p_92110_1_.stackTagCompound.getTag("StoredEnchantments") : new NBTTagList();
		}

///    
///     <summary> * allows items to add custom lines of information to the mouseover description </summary>
///     
		public virtual void addInformation(ItemStack p_77624_1_, EntityPlayer p_77624_2_, IList p_77624_3_, bool p_77624_4_)
		{
			base.addInformation(p_77624_1_, p_77624_2_, p_77624_3_, p_77624_4_);
			NBTTagList var5 = this.func_92110_g(p_77624_1_);

			if (var5 != null)
			{
				for (int var6 = 0; var6 < var5.tagCount(); ++var6)
				{
					short var7 = var5.getCompoundTagAt(var6).getShort("id");
					short var8 = var5.getCompoundTagAt(var6).getShort("lvl");

					if (Enchantment.enchantmentsList[var7] != null)
					{
						p_77624_3_.Add(Enchantment.enchantmentsList[var7].getTranslatedName(var8));
					}
				}
			}
		}

///    
///     <summary> * Adds an stored enchantment to an enchanted book ItemStack </summary>
///     
		public virtual void addEnchantment(ItemStack p_92115_1_, EnchantmentData p_92115_2_)
		{
			NBTTagList var3 = this.func_92110_g(p_92115_1_);
			bool var4 = true;

			for (int var5 = 0; var5 < var3.tagCount(); ++var5)
			{
				NBTTagCompound var6 = var3.getCompoundTagAt(var5);

				if (var6.getShort("id") == p_92115_2_.enchantmentobj.effectId)
				{
					if (var6.getShort("lvl") < p_92115_2_.enchantmentLevel)
					{
						var6.setShort("lvl", (short)p_92115_2_.enchantmentLevel);
					}

					var4 = false;
					break;
				}
			}

			if (var4)
			{
				NBTTagCompound var7 = new NBTTagCompound();
				var7.setShort("id", (short)p_92115_2_.enchantmentobj.effectId);
				var7.setShort("lvl", (short)p_92115_2_.enchantmentLevel);
				var3.appendTag(var7);
			}

			if (!p_92115_1_.hasTagCompound())
			{
				p_92115_1_.TagCompound = new NBTTagCompound();
			}

			p_92115_1_.TagCompound.setTag("StoredEnchantments", var3);
		}

///    
///     <summary> * Returns the ItemStack of an enchanted version of this item. </summary>
///     
		public virtual ItemStack getEnchantedItemStack(EnchantmentData p_92111_1_)
		{
			ItemStack var2 = new ItemStack(this);
			this.addEnchantment(var2, p_92111_1_);
			return var2;
		}

		public virtual void func_92113_a(Enchantment p_92113_1_, IList p_92113_2_)
		{
			for (int var3 = p_92113_1_.MinLevel; var3 <= p_92113_1_.MaxLevel; ++var3)
			{
				p_92113_2_.Add(this.getEnchantedItemStack(new EnchantmentData(p_92113_1_, var3)));
			}
		}

		public virtual WeightedRandomChestContent func_92114_b(Random p_92114_1_)
		{
			return this.func_92112_a(p_92114_1_, 1, 1, 1);
		}

		public virtual WeightedRandomChestContent func_92112_a(Random p_92112_1_, int p_92112_2_, int p_92112_3_, int p_92112_4_)
		{
			ItemStack var5 = new ItemStack(Items.book, 1, 0);
			EnchantmentHelper.addRandomEnchantment(p_92112_1_, var5, 30);
			return new WeightedRandomChestContent(var5, p_92112_2_, p_92112_3_, p_92112_4_);
		}
	}

}