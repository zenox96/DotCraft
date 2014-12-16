using System;
using System.Collections;

namespace DotCraftCore.Item
{

	using HashMultimap = com.google.common.collect.HashMultimap;
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using AttributeModifier = DotCraftCore.entity.ai.attributes.AttributeModifier;
	using IAttribute = DotCraftCore.entity.ai.attributes.IAttribute;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using EntityPotion = DotCraftCore.entity.projectile.EntityPotion;
	using Items = DotCraftCore.init.Items;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using NBTTagList = DotCraftCore.NBT.NBTTagList;
	using Potion = DotCraftCore.Potion.Potion;
	using PotionEffect = DotCraftCore.Potion.PotionEffect;
	using PotionHelper = DotCraftCore.Potion.PotionHelper;
	using EnumChatFormatting = DotCraftCore.Util.EnumChatFormatting;
	using IIcon = DotCraftCore.Util.IIcon;
	using StatCollector = DotCraftCore.Util.StatCollector;
	using World = DotCraftCore.World.World;

	public class ItemPotion : Item
	{
///    
///     <summary> * Contains a map from integers to the list of potion effects that potions with that damage value confer (to prevent
///     * recalculating it). </summary>
///     
		private Hashtable effectCache = new Hashtable();
		private static readonly IDictionary field_77835_b = new LinkedHashMap();
		private IIcon field_94591_c;
		private IIcon field_94590_d;
		private IIcon field_94592_ct;
		

		public ItemPotion()
		{
			this.MaxStackSize = 1;
			this.HasSubtypes = true;
			this.MaxDamage = 0;
			this.CreativeTab = CreativeTabs.tabBrewing;
		}

///    
///     <summary> * Returns a list of potion effects for the specified itemstack. </summary>
///     
		public virtual IList getEffects(ItemStack p_77832_1_)
		{
			if (p_77832_1_.hasTagCompound() && p_77832_1_.TagCompound.func_150297_b("CustomPotionEffects", 9))
			{
				ArrayList var7 = new ArrayList();
				NBTTagList var3 = p_77832_1_.TagCompound.getTagList("CustomPotionEffects", 10);

				for (int var4 = 0; var4 < var3.tagCount(); ++var4)
				{
					NBTTagCompound var5 = var3.getCompoundTagAt(var4);
					PotionEffect var6 = PotionEffect.readCustomPotionEffectFromNBT(var5);

					if (var6 != null)
					{
						var7.Add(var6);
					}
				}

				return var7;
			}
			else
			{
				IList var2 = (IList)this.effectCache.get(Convert.ToInt32(p_77832_1_.ItemDamage));

				if (var2 == null)
				{
					var2 = PotionHelper.getPotionEffects(p_77832_1_.ItemDamage, false);
					this.effectCache.Add(Convert.ToInt32(p_77832_1_.ItemDamage), var2);
				}

				return var2;
			}
		}

///    
///     <summary> * Returns a list of effects for the specified potion damage value. </summary>
///     
		public virtual IList getEffects(int p_77834_1_)
		{
			IList var2 = (IList)this.effectCache.get(Convert.ToInt32(p_77834_1_));

			if (var2 == null)
			{
				var2 = PotionHelper.getPotionEffects(p_77834_1_, false);
				this.effectCache.Add(Convert.ToInt32(p_77834_1_), var2);
			}

			return var2;
		}

		public virtual ItemStack onEaten(ItemStack p_77654_1_, World p_77654_2_, EntityPlayer p_77654_3_)
		{
			if (!p_77654_3_.capabilities.isCreativeMode)
			{
				--p_77654_1_.stackSize;
			}

			if (!p_77654_2_.isClient)
			{
				IList var4 = this.getEffects(p_77654_1_);

				if (var4 != null)
				{
					IEnumerator var5 = var4.GetEnumerator();

					while (var5.MoveNext())
					{
						PotionEffect var6 = (PotionEffect)var5.Current;
						p_77654_3_.addPotionEffect(new PotionEffect(var6));
					}
				}
			}

			if (!p_77654_3_.capabilities.isCreativeMode)
			{
				if (p_77654_1_.stackSize <= 0)
				{
					return new ItemStack(Items.glass_bottle);
				}

				p_77654_3_.inventory.addItemStackToInventory(new ItemStack(Items.glass_bottle));
			}

			return p_77654_1_;
		}

///    
///     <summary> * How long it takes to use or consume an item </summary>
///     
		public virtual int getMaxItemUseDuration(ItemStack p_77626_1_)
		{
			return 32;
		}

///    
///     <summary> * returns the action that specifies what animation to play when the items is being used </summary>
///     
		public virtual EnumAction getItemUseAction(ItemStack p_77661_1_)
		{
			return EnumAction.drink;
		}

///    
///     <summary> * Called whenever this item is equipped and the right mouse button is pressed. Args: itemStack, world, entityPlayer </summary>
///     
		public virtual ItemStack onItemRightClick(ItemStack p_77659_1_, World p_77659_2_, EntityPlayer p_77659_3_)
		{
			if (isSplash(p_77659_1_.ItemDamage))
			{
				if (!p_77659_3_.capabilities.isCreativeMode)
				{
					--p_77659_1_.stackSize;
				}

				p_77659_2_.playSoundAtEntity(p_77659_3_, "random.bow", 0.5F, 0.4F / (itemRand.nextFloat() * 0.4F + 0.8F));

				if (!p_77659_2_.isClient)
				{
					p_77659_2_.spawnEntityInWorld(new EntityPotion(p_77659_2_, p_77659_3_, p_77659_1_));
				}

				return p_77659_1_;
			}
			else
			{
				p_77659_3_.setItemInUse(p_77659_1_, this.getMaxItemUseDuration(p_77659_1_));
				return p_77659_1_;
			}
		}

///    
///     <summary> * Callback for item usage. If the item does something special on right clicking, he will have one of those. Return
///     * True if something happen and false if it don't. This is for ITEMS, not BLOCKS </summary>
///     
		public virtual bool onItemUse(ItemStack p_77648_1_, EntityPlayer p_77648_2_, World p_77648_3_, int p_77648_4_, int p_77648_5_, int p_77648_6_, int p_77648_7_, float p_77648_8_, float p_77648_9_, float p_77648_10_)
		{
			return false;
		}

///    
///     <summary> * Gets an icon index based on an item's damage value </summary>
///     
		public virtual IIcon getIconFromDamage(int p_77617_1_)
		{
			return isSplash(p_77617_1_) ? this.field_94591_c : this.field_94590_d;
		}

///    
///     <summary> * Gets an icon index based on an item's damage value and the given render pass </summary>
///     
		public virtual IIcon getIconFromDamageForRenderPass(int p_77618_1_, int p_77618_2_)
		{
			return p_77618_2_ == 0 ? this.field_94592_ct : base.getIconFromDamageForRenderPass(p_77618_1_, p_77618_2_);
		}

///    
///     <summary> * returns wether or not a potion is a throwable splash potion based on damage value </summary>
///     
		public static bool isSplash(int p_77831_0_)
		{
			return (p_77831_0_ & 16384) != 0;
		}

		public virtual int getColorFromDamage(int p_77620_1_)
		{
			return PotionHelper.func_77915_a(p_77620_1_, false);
		}

		public virtual int getColorFromItemStack(ItemStack p_82790_1_, int p_82790_2_)
		{
			return p_82790_2_ > 0 ? 16777215 : this.getColorFromDamage(p_82790_1_.ItemDamage);
		}

		public virtual bool requiresMultipleRenderPasses()
		{
			return true;
		}

		public virtual bool isEffectInstant(int p_77833_1_)
		{
			IList var2 = this.getEffects(p_77833_1_);

			if (var2 != null && !var2.Count == 0)
			{
				IEnumerator var3 = var2.GetEnumerator();
				PotionEffect var4;

				do
				{
					if (!var3.MoveNext())
					{
						return false;
					}

					var4 = (PotionEffect)var3.Current;
				}
				while (!Potion.potionTypes[var4.PotionID].Instant);

				return true;
			}
			else
			{
				return false;
			}
		}

		public virtual string getItemStackDisplayName(ItemStack p_77653_1_)
		{
			if (p_77653_1_.ItemDamage == 0)
			{
				return StatCollector.translateToLocal("item.emptyPotion.name").Trim();
			}
			else
			{
				string var2 = "";

				if (isSplash(p_77653_1_.ItemDamage))
				{
					var2 = StatCollector.translateToLocal("potion.prefix.grenade").Trim() + " ";
				}

				IList var3 = Items.potionitem.getEffects(p_77653_1_);
				string var4;

				if (var3 != null && !var3.Count == 0)
				{
					var4 = ((PotionEffect)var3[0]).EffectName;
					var4 = var4 + ".postfix";
					return var2 + StatCollector.translateToLocal(var4).Trim();
				}
				else
				{
					var4 = PotionHelper.func_77905_c(p_77653_1_.ItemDamage);
					return StatCollector.translateToLocal(var4).Trim() + " " + base.getItemStackDisplayName(p_77653_1_);
				}
			}
		}

///    
///     <summary> * allows items to add custom lines of information to the mouseover description </summary>
///     
		public virtual void addInformation(ItemStack p_77624_1_, EntityPlayer p_77624_2_, IList p_77624_3_, bool p_77624_4_)
		{
			if (p_77624_1_.ItemDamage != 0)
			{
				IList var5 = Items.potionitem.getEffects(p_77624_1_);
				HashMultimap var6 = HashMultimap.create();
				IEnumerator var16;

				if (var5 != null && !var5.Count == 0)
				{
					var16 = var5.GetEnumerator();

					while (var16.MoveNext())
					{
						PotionEffect var8 = (PotionEffect)var16.Current;
						string var9 = StatCollector.translateToLocal(var8.EffectName).Trim();
						Potion var10 = Potion.potionTypes[var8.PotionID];
						IDictionary var11 = var10.func_111186_k();

						if (var11 != null && var11.Count > 0)
						{
							IEnumerator var12 = var11.GetEnumerator();

							while (var12.MoveNext())
							{
								Entry var13 = (Entry)var12.Current;
								AttributeModifier var14 = (AttributeModifier)var13.Value;
								AttributeModifier var15 = new AttributeModifier(var14.Name, var10.func_111183_a(var8.Amplifier, var14), var14.Operation);
								var6.put(((IAttribute)var13.Key).AttributeUnlocalizedName, var15);
							}
						}

						if (var8.Amplifier > 0)
						{
							var9 = var9 + " " + StatCollector.translateToLocal("potion.potency." + var8.Amplifier).Trim();
						}

						if (var8.Duration > 20)
						{
							var9 = var9 + " (" + Potion.getDurationString(var8) + ")";
						}

						if (var10.BadEffect)
						{
							p_77624_3_.Add(EnumChatFormatting.RED + var9);
						}
						else
						{
							p_77624_3_.Add(EnumChatFormatting.GRAY + var9);
						}
					}
				}
				else
				{
					string var7 = StatCollector.translateToLocal("potion.empty").Trim();
					p_77624_3_.Add(EnumChatFormatting.GRAY + var7);
				}

				if (!var6.Empty)
				{
					p_77624_3_.Add("");
					p_77624_3_.Add(EnumChatFormatting.DARK_PURPLE + StatCollector.translateToLocal("potion.effects.whenDrank"));
					var16 = var6.entries().GetEnumerator();

					while (var16.MoveNext())
					{
						Entry var17 = (Entry)var16.Current;
						AttributeModifier var18 = (AttributeModifier)var17.Value;
						double var19 = var18.Amount;
						double var20;

						if (var18.Operation != 1 && var18.Operation != 2)
						{
							var20 = var18.Amount;
						}
						else
						{
							var20 = var18.Amount * 100.0D;
						}

						if (var19 > 0.0D)
						{
							p_77624_3_.Add(EnumChatFormatting.BLUE + StatCollector.translateToLocalFormatted("attribute.modifier.plus." + var18.Operation, new object[] {ItemStack.field_111284_a.format(var20), StatCollector.translateToLocal("attribute.name." + (string)var17.Key)}));
						}
						else if (var19 < 0.0D)
						{
							var20 *= -1.0D;
							p_77624_3_.Add(EnumChatFormatting.RED + StatCollector.translateToLocalFormatted("attribute.modifier.take." + var18.Operation, new object[] {ItemStack.field_111284_a.format(var20), StatCollector.translateToLocal("attribute.name." + (string)var17.Key)}));
						}
					}
				}
			}
		}

		public virtual bool hasEffect(ItemStack p_77636_1_)
		{
			IList var2 = this.getEffects(p_77636_1_);
			return var2 != null && !var2.Count == 0;
		}

///    
///     <summary> * This returns the sub items </summary>
///     
		public virtual void getSubItems(Item p_150895_1_, CreativeTabs p_150895_2_, IList p_150895_3_)
		{
			base.getSubItems(p_150895_1_, p_150895_2_, p_150895_3_);
			int var5;

			if (field_77835_b.Count == 0)
			{
				for (int var4 = 0; var4 <= 15; ++var4)
				{
					for (var5 = 0; var5 <= 1; ++var5)
					{
						int var6;

						if (var5 == 0)
						{
							var6 = var4 | 8192;
						}
						else
						{
							var6 = var4 | 16384;
						}

						for (int var7 = 0; var7 <= 2; ++var7)
						{
							int var8 = var6;

							if (var7 != 0)
							{
								if (var7 == 1)
								{
									var8 = var6 | 32;
								}
								else if (var7 == 2)
								{
									var8 = var6 | 64;
								}
							}

							IList var9 = PotionHelper.getPotionEffects(var8, false);

							if (var9 != null && !var9.Count == 0)
							{
								field_77835_b.Add(var9, Convert.ToInt32(var8));
							}
						}
					}
				}
			}

			IEnumerator var10 = field_77835_b.Values.GetEnumerator();

			while (var10.MoveNext())
			{
				var5 = (int)((int?)var10.Current);
				p_150895_3_.Add(new ItemStack(p_150895_1_, 1, var5));
			}
		}

		public virtual void registerIcons(IIconRegister p_94581_1_)
		{
			this.field_94590_d = p_94581_1_.registerIcon(this.IconString + "_" + "bottle_drinkable");
			this.field_94591_c = p_94581_1_.registerIcon(this.IconString + "_" + "bottle_splash");
			this.field_94592_ct = p_94581_1_.registerIcon(this.IconString + "_" + "overlay");
		}

		public static IIcon func_94589_d(string p_94589_0_)
		{
			return p_94589_0_.Equals("bottle_drinkable") ? Items.potionitem.field_94590_d : (p_94589_0_.Equals("bottle_splash") ? Items.potionitem.field_94591_c : (p_94589_0_.Equals("overlay") ? Items.potionitem.field_94592_ct : null));
		}
	}

}