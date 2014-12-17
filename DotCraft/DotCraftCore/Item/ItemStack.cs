using System;
using System.Collections;

namespace DotCraftCore.nItem
{

	using HashMultimap = com.google.common.collect.HashMultimap;
	using Multimap = com.google.common.collect.Multimap;
	using Block = DotCraftCore.nBlock.Block;
	using Enchantment = DotCraftCore.enchantment.Enchantment;
	using EnchantmentDurability = DotCraftCore.enchantment.EnchantmentDurability;
	using EnchantmentHelper = DotCraftCore.enchantment.EnchantmentHelper;
	using Entity = DotCraftCore.entity.Entity;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using EnumCreatureAttribute = DotCraftCore.entity.EnumCreatureAttribute;
	using SharedMonsterAttributes = DotCraftCore.entity.SharedMonsterAttributes;
	using AttributeModifier = DotCraftCore.entity.ai.attributes.AttributeModifier;
	using EntityItemFrame = DotCraftCore.entity.item.EntityItemFrame;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using HoverEvent = DotCraftCore.event.HoverEvent;
	using Items = DotCraftCore.init.Items;
	using NBTBase = DotCraftCore.NBT.NBTBase;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using NBTTagList = DotCraftCore.NBT.NBTTagList;
	using StatList = DotCraftCore.Stats.StatList;
	using ChatComponentText = DotCraftCore.Util.ChatComponentText;
	using EnumChatFormatting = DotCraftCore.Util.EnumChatFormatting;
	using IChatComponent = DotCraftCore.Util.IChatComponent;
	using IIcon = DotCraftCore.Util.IIcon;
	using StatCollector = DotCraftCore.Util.StatCollector;
	using World = DotCraftCore.World.World;

	public sealed class ItemStack
	{
		public static readonly DecimalFormat field_111284_a = new DecimalFormat("#.###");

	/// <summary> Size of the stack.  </summary>
		public int stackSize;

///    
///     <summary> * Number of animation frames to go when receiving an item (by walking into it, for example). </summary>
///     
		public int animationsToGo;
		private Item field_151002_e;

///    
///     <summary> * A NBTTagMap containing data about an ItemStack. Can only be used for non stackable items </summary>
///     
		public NBTTagCompound stackTagCompound;

	/// <summary> Damage dealt to the item or number of use. Raise when using items.  </summary>
		private int itemDamage;

	/// <summary> Item frame this stack is on, or null if not on an item frame.  </summary>
		private EntityItemFrame itemFrame;
		

		public ItemStack(Block p_i1876_1_) : this(p_i1876_1_, 1)
		{
		}

		public ItemStack(Block p_i1877_1_, int p_i1877_2_) : this(p_i1877_1_, p_i1877_2_, 0)
		{
		}

		public ItemStack(Block p_i1878_1_, int p_i1878_2_, int p_i1878_3_) : this(Item.getItemFromBlock(p_i1878_1_), p_i1878_2_, p_i1878_3_)
		{
		}

		public ItemStack(Item p_i1879_1_) : this(p_i1879_1_, 1)
		{
		}

		public ItemStack(Item p_i1880_1_, int p_i1880_2_) : this(p_i1880_1_, p_i1880_2_, 0)
		{
		}

		public ItemStack(Item p_i1881_1_, int p_i1881_2_, int p_i1881_3_)
		{
			this.field_151002_e = p_i1881_1_;
			this.stackSize = p_i1881_2_;
			this.itemDamage = p_i1881_3_;

			if (this.itemDamage < 0)
			{
				this.itemDamage = 0;
			}
		}

		public static ItemStack loadItemStackFromNBT(NBTTagCompound p_77949_0_)
		{
			ItemStack var1 = new ItemStack();
			var1.readFromNBT(p_77949_0_);
			return var1.Item != null ? var1 : null;
		}

		private ItemStack()
		{
		}

///    
///     <summary> * Remove the argument from the stack size. Return a new stack object with argument size. </summary>
///     
		public ItemStack splitStack(int p_77979_1_)
		{
			ItemStack var2 = new ItemStack(this.field_151002_e, p_77979_1_, this.itemDamage);

			if (this.stackTagCompound != null)
			{
				var2.stackTagCompound = (NBTTagCompound)this.stackTagCompound.copy();
			}

			this.stackSize -= p_77979_1_;
			return var2;
		}

///    
///     <summary> * Returns the object corresponding to the stack. </summary>
///     
		public Item Item
		{
			get
			{
				return this.field_151002_e;
			}
		}

///    
///     <summary> * Returns the icon index of the current stack. </summary>
///     
		public IIcon IconIndex
		{
			get
			{
				return this.Item.getIconIndex(this);
			}
		}

		public int ItemSpriteNumber
		{
			get
			{
				return this.Item.SpriteNumber;
			}
		}

		public bool tryPlaceItemIntoWorld(EntityPlayer p_77943_1_, nWorld p_77943_2_, int p_77943_3_, int p_77943_4_, int p_77943_5_, int p_77943_6_, float p_77943_7_, float p_77943_8_, float p_77943_9_)
		{
			bool var10 = this.Item.onItemUse(this, p_77943_1_, p_77943_2_, p_77943_3_, p_77943_4_, p_77943_5_, p_77943_6_, p_77943_7_, p_77943_8_, p_77943_9_);

			if (var10)
			{
				p_77943_1_.addStat(StatList.objectUseStats[Item.getIdFromItem(this.field_151002_e)], 1);
			}

			return var10;
		}

		public float func_150997_a(Block p_150997_1_)
		{
			return this.Item.func_150893_a(this, p_150997_1_);
		}

///    
///     <summary> * Called whenever this item stack is equipped and right clicked. Returns the new item stack to put in the position
///     * where this item is. Args: world, player </summary>
///     
		public ItemStack useItemRightClick(nWorld p_77957_1_, EntityPlayer p_77957_2_)
		{
			return this.Item.onItemRightClick(this, p_77957_1_, p_77957_2_);
		}

		public ItemStack onFoodEaten(nWorld p_77950_1_, EntityPlayer p_77950_2_)
		{
			return this.Item.onEaten(this, p_77950_1_, p_77950_2_);
		}

///    
///     <summary> * Write the stack fields to a NBT object. Return the new NBT object. </summary>
///     
		public NBTTagCompound writeToNBT(NBTTagCompound p_77955_1_)
		{
			p_77955_1_.setShort("id", (short)Item.getIdFromItem(this.field_151002_e));
			p_77955_1_.setByte("Count", (sbyte)this.stackSize);
			p_77955_1_.setShort("Damage", (short)this.itemDamage);

			if (this.stackTagCompound != null)
			{
				p_77955_1_.setTag("tag", this.stackTagCompound);
			}

			return p_77955_1_;
		}

///    
///     <summary> * Read the stack fields from a NBT object. </summary>
///     
		public void readFromNBT(NBTTagCompound p_77963_1_)
		{
			this.field_151002_e = Item.getItemById(p_77963_1_.getShort("id"));
			this.stackSize = p_77963_1_.getByte("Count");
			this.itemDamage = p_77963_1_.getShort("Damage");

			if (this.itemDamage < 0)
			{
				this.itemDamage = 0;
			}

			if (p_77963_1_.func_150297_b("tag", 10))
			{
				this.stackTagCompound = p_77963_1_.getCompoundTag("tag");
			}
		}

///    
///     <summary> * Returns maximum size of the stack. </summary>
///     
		public int MaxStackSize
		{
			get
			{
				return this.Item.ItemStackLimit;
			}
		}

///    
///     <summary> * Returns true if the ItemStack can hold 2 or more units of the item. </summary>
///     
		public bool isStackable()
		{
			get
			{
				return this.MaxStackSize > 1 && (!this.ItemStackDamageable || !this.ItemDamaged);
			}
		}

///    
///     <summary> * true if this itemStack is damageable </summary>
///     
		public bool isItemStackDamageable()
		{
			get
			{
				return this.field_151002_e.MaxDamage <= 0 ? false : !this.hasTagCompound() || !this.TagCompound.getBoolean("Unbreakable");
			}
		}

		public bool HasSubtypes
		{
			get
			{
				return this.field_151002_e.HasSubtypes;
			}
		}

///    
///     <summary> * returns true when a damageable item is damaged </summary>
///     
		public bool isItemDamaged()
		{
			get
			{
				return this.ItemStackDamageable && this.itemDamage > 0;
			}
		}

///    
///     <summary> * gets the damage of an itemstack, for displaying purposes </summary>
///     
		public int ItemDamageForDisplay
		{
			get
			{
				return this.itemDamage;
			}
		}

///    
///     <summary> * gets the damage of an itemstack </summary>
///     
		public int ItemDamage
		{
			get
			{
				return this.itemDamage;
			}
			set
			{
				this.itemDamage = value;
	
				if (this.itemDamage < 0)
				{
					this.itemDamage = 0;
				}
			}
		}

///    
///     <summary> * Sets the item damage of the ItemStack. </summary>
///     

///    
///     <summary> * Returns the max damage an item in the stack can take. </summary>
///     
		public int MaxDamage
		{
			get
			{
				return this.field_151002_e.MaxDamage;
			}
		}

///    
///     <summary> * Attempts to damage the ItemStack with par1 amount of damage, If the ItemStack has the Unbreaking enchantment
///     * there is a chance for each point of damage to be negated. Returns true if it takes more damage than
///     * getMaxDamage(). Returns false otherwise or if the ItemStack can't be damaged or if all points of damage are
///     * negated. </summary>
///     
		public bool attemptDamageItem(int p_96631_1_, Random p_96631_2_)
		{
			if (!this.ItemStackDamageable)
			{
				return false;
			}
			else
			{
				if (p_96631_1_ > 0)
				{
					int var3 = EnchantmentHelper.getEnchantmentLevel(Enchantment.unbreaking.effectId, this);
					int var4 = 0;

					for (int var5 = 0; var3 > 0 && var5 < p_96631_1_; ++var5)
					{
						if (EnchantmentDurability.negateDamage(this, var3, p_96631_2_))
						{
							++var4;
						}
					}

					p_96631_1_ -= var4;

					if (p_96631_1_ <= 0)
					{
						return false;
					}
				}

				this.itemDamage += p_96631_1_;
				return this.itemDamage > this.MaxDamage;
			}
		}

///    
///     <summary> * Damages the item in the ItemStack </summary>
///     
		public void damageItem(int p_77972_1_, EntityLivingBase p_77972_2_)
		{
			if (!(p_77972_2_ is EntityPlayer) || !((EntityPlayer)p_77972_2_).capabilities.isCreativeMode)
			{
				if (this.ItemStackDamageable)
				{
					if (this.attemptDamageItem(p_77972_1_, p_77972_2_.RNG))
					{
						p_77972_2_.renderBrokenItemStack(this);
						--this.stackSize;

						if (p_77972_2_ is EntityPlayer)
						{
							EntityPlayer var3 = (EntityPlayer)p_77972_2_;
							var3.addStat(StatList.objectBreakStats[Item.getIdFromItem(this.field_151002_e)], 1);

							if (this.stackSize == 0 && this.Item is ItemBow)
							{
								var3.destroyCurrentEquippedItem();
							}
						}

						if (this.stackSize < 0)
						{
							this.stackSize = 0;
						}

						this.itemDamage = 0;
					}
				}
			}
		}

///    
///     <summary> * Calls the corresponding fct in di </summary>
///     
		public void hitEntity(EntityLivingBase p_77961_1_, EntityPlayer p_77961_2_)
		{
			bool var3 = this.field_151002_e.hitEntity(this, p_77961_1_, p_77961_2_);

			if (var3)
			{
				p_77961_2_.addStat(StatList.objectUseStats[Item.getIdFromItem(this.field_151002_e)], 1);
			}
		}

		public void func_150999_a(nWorld p_150999_1_, Block p_150999_2_, int p_150999_3_, int p_150999_4_, int p_150999_5_, EntityPlayer p_150999_6_)
		{
			bool var7 = this.field_151002_e.onBlockDestroyed(this, p_150999_1_, p_150999_2_, p_150999_3_, p_150999_4_, p_150999_5_, p_150999_6_);

			if (var7)
			{
				p_150999_6_.addStat(StatList.objectUseStats[Item.getIdFromItem(this.field_151002_e)], 1);
			}
		}

		public bool func_150998_b(Block p_150998_1_)
		{
			return this.field_151002_e.func_150897_b(p_150998_1_);
		}

		public bool interactWithEntity(EntityPlayer p_111282_1_, EntityLivingBase p_111282_2_)
		{
			return this.field_151002_e.itemInteractionForEntity(this, p_111282_1_, p_111282_2_);
		}

///    
///     <summary> * Returns a new stack with the same properties. </summary>
///     
		public ItemStack copy()
		{
			ItemStack var1 = new ItemStack(this.field_151002_e, this.stackSize, this.itemDamage);

			if (this.stackTagCompound != null)
			{
				var1.stackTagCompound = (NBTTagCompound)this.stackTagCompound.copy();
			}

			return var1;
		}

		public static bool areItemStackTagsEqual(ItemStack p_77970_0_, ItemStack p_77970_1_)
		{
			return p_77970_0_ == null && p_77970_1_ == null ? true : (p_77970_0_ != null && p_77970_1_ != null ? (p_77970_0_.stackTagCompound == null && p_77970_1_.stackTagCompound != null ? false : p_77970_0_.stackTagCompound == null || p_77970_0_.stackTagCompound.Equals(p_77970_1_.stackTagCompound)) : false);
		}

///    
///     <summary> * compares ItemStack argument1 with ItemStack argument2; returns true if both ItemStacks are equal </summary>
///     
		public static bool areItemStacksEqual(ItemStack p_77989_0_, ItemStack p_77989_1_)
		{
			return p_77989_0_ == null && p_77989_1_ == null ? true : (p_77989_0_ != null && p_77989_1_ != null ? p_77989_0_.isItemStackEqual(p_77989_1_) : false);
		}

///    
///     <summary> * compares ItemStack argument to the instance ItemStack; returns true if both ItemStacks are equal </summary>
///     
		private bool isItemStackEqual(ItemStack p_77959_1_)
		{
			return this.stackSize != p_77959_1_.stackSize ? false : (this.field_151002_e != p_77959_1_.field_151002_e ? false : (this.itemDamage != p_77959_1_.itemDamage ? false : (this.stackTagCompound == null && p_77959_1_.stackTagCompound != null ? false : this.stackTagCompound == null || this.stackTagCompound.Equals(p_77959_1_.stackTagCompound))));
		}

///    
///     <summary> * compares ItemStack argument to the instance ItemStack; returns true if the Items contained in both ItemStacks are
///     * equal </summary>
///     
		public bool isItemEqual(ItemStack p_77969_1_)
		{
			return this.field_151002_e == p_77969_1_.field_151002_e && this.itemDamage == p_77969_1_.itemDamage;
		}

		public string UnlocalizedName
		{
			get
			{
				return this.field_151002_e.getUnlocalizedName(this);
			}
		}

///    
///     <summary> * Creates a copy of a ItemStack, a null parameters will return a null. </summary>
///     
		public static ItemStack copyItemStack(ItemStack p_77944_0_)
		{
			return p_77944_0_ == null ? null : p_77944_0_.copy();
		}

		public override string ToString()
		{
			return this.stackSize + "x" + this.field_151002_e.UnlocalizedName + "@" + this.itemDamage;
		}

///    
///     <summary> * Called each tick as long the ItemStack in on player inventory. Used to progress the pickup animation and update
///     * maps. </summary>
///     
		public void updateAnimation(nWorld p_77945_1_, Entity p_77945_2_, int p_77945_3_, bool p_77945_4_)
		{
			if (this.animationsToGo > 0)
			{
				--this.animationsToGo;
			}

			this.field_151002_e.onUpdate(this, p_77945_1_, p_77945_2_, p_77945_3_, p_77945_4_);
		}

		public void onCrafting(nWorld p_77980_1_, EntityPlayer p_77980_2_, int p_77980_3_)
		{
			p_77980_2_.addStat(StatList.objectCraftStats[Item.getIdFromItem(this.field_151002_e)], p_77980_3_);
			this.field_151002_e.onCreated(this, p_77980_1_, p_77980_2_);
		}

		public int MaxItemUseDuration
		{
			get
			{
				return this.Item.getMaxItemUseDuration(this);
			}
		}

		public EnumAction ItemUseAction
		{
			get
			{
				return this.Item.getItemUseAction(this);
			}
		}

///    
///     <summary> * Called when the player releases the use item button. Args: world, entityplayer, itemInUseCount </summary>
///     
		public void onPlayerStoppedUsing(nWorld p_77974_1_, EntityPlayer p_77974_2_, int p_77974_3_)
		{
			this.Item.onPlayerStoppedUsing(this, p_77974_1_, p_77974_2_, p_77974_3_);
		}

///    
///     <summary> * Returns true if the ItemStack has an NBTTagCompound. Currently used to store enchantments. </summary>
///     
		public bool hasTagCompound()
		{
			return this.stackTagCompound != null;
		}

///    
///     <summary> * Returns the NBTTagCompound of the ItemStack. </summary>
///     
		public NBTTagCompound TagCompound
		{
			get
			{
				return this.stackTagCompound;
			}
			set
			{
				this.stackTagCompound = value;
			}
		}

		public NBTTagList EnchantmentTagList
		{
			get
			{
				return this.stackTagCompound == null ? null : this.stackTagCompound.getTagList("ench", 10);
			}
		}

///    
///     <summary> * Assigns a NBTTagCompound to the ItemStack, minecraft validates that only non-stackable items can have it. </summary>
///     

///    
///     <summary> * returns the display name of the itemstack </summary>
///     
		public string DisplayName
		{
			get
			{
				string var1 = this.Item.getItemStackDisplayName(this);
	
				if (this.stackTagCompound != null && this.stackTagCompound.func_150297_b("display", 10))
				{
					NBTTagCompound var2 = this.stackTagCompound.getCompoundTag("display");
	
					if (var2.func_150297_b("Name", 8))
					{
						var1 = var2.getString("Name");
					}
				}
	
				return var1;
			}
		}

		public ItemStack StackDisplayName
		{
			set
			{
				if (this.stackTagCompound == null)
				{
					this.stackTagCompound = new NBTTagCompound();
				}
	
				if (!this.stackTagCompound.func_150297_b("display", 10))
				{
					this.stackTagCompound.setTag("display", new NBTTagCompound());
				}
	
				this.stackTagCompound.getCompoundTag("display").setString("Name", value);
				return this;
			}
		}

		public void func_135074_t()
		{
			if (this.stackTagCompound != null)
			{
				if (this.stackTagCompound.func_150297_b("display", 10))
				{
					NBTTagCompound var1 = this.stackTagCompound.getCompoundTag("display");
					var1.removeTag("Name");

					if (var1.hasNoTags())
					{
						this.stackTagCompound.removeTag("display");

						if (this.stackTagCompound.hasNoTags())
						{
							this.TagCompound = (NBTTagCompound)null;
						}
					}
				}
			}
		}

///    
///     <summary> * Returns true if the itemstack has a display name </summary>
///     
		public bool hasDisplayName()
		{
			return this.stackTagCompound == null ? false : (!this.stackTagCompound.func_150297_b("display", 10) ? false : this.stackTagCompound.getCompoundTag("display").func_150297_b("Name", 8));
		}

///    
///     <summary> * Return a list of strings containing information about the item </summary>
///     
		public IList getTooltip(EntityPlayer p_82840_1_, bool p_82840_2_)
		{
			ArrayList var3 = new ArrayList();
			string var4 = this.DisplayName;

			if (this.hasDisplayName())
			{
				var4 = EnumChatFormatting.ITALIC + var4 + EnumChatFormatting.RESET;
			}

			int var6;

			if (p_82840_2_)
			{
				string var5 = "";

				if (var4.Length > 0)
				{
					var4 = var4 + " (";
					var5 = ")";
				}

				var6 = Item.getIdFromItem(this.field_151002_e);

				if (this.HasSubtypes)
				{
					var4 = var4 + string.Format("#{0:D4}/{1:D}{2}", new object[] {Convert.ToInt32(var6), Convert.ToInt32(this.itemDamage), var5});
				}
				else
				{
					var4 = var4 + string.Format("#{0:D4}{1}", new object[] {Convert.ToInt32(var6), var5});
				}
			}
			else if (!this.hasDisplayName() && this.field_151002_e == Items.filled_map)
			{
				var4 = var4 + " #" + this.itemDamage;
			}

			var3.Add(var4);
			this.field_151002_e.addInformation(this, p_82840_1_, var3, p_82840_2_);

			if (this.hasTagCompound())
			{
				NBTTagList var13 = this.EnchantmentTagList;

				if (var13 != null)
				{
					for (var6 = 0; var6 < var13.tagCount(); ++var6)
					{
						short var7 = var13.getCompoundTagAt(var6).getShort("id");
						short var8 = var13.getCompoundTagAt(var6).getShort("lvl");

						if (Enchantment.enchantmentsList[var7] != null)
						{
							var3.Add(Enchantment.enchantmentsList[var7].getTranslatedName(var8));
						}
					}
				}

				if (this.stackTagCompound.func_150297_b("display", 10))
				{
					NBTTagCompound var15 = this.stackTagCompound.getCompoundTag("display");

					if (var15.func_150297_b("color", 3))
					{
						if (p_82840_2_)
						{
							var3.Add("Color: #" + int.toHexString(var15.getInteger("color")).ToUpper());
						}
						else
						{
							var3.Add(EnumChatFormatting.ITALIC + StatCollector.translateToLocal("item.dyed"));
						}
					}

					if (var15.func_150299_b("Lore") == 9)
					{
						NBTTagList var17 = var15.getTagList("Lore", 8);

						if (var17.tagCount() > 0)
						{
							for (int var19 = 0; var19 < var17.tagCount(); ++var19)
							{
								var3.Add(EnumChatFormatting.DARK_PURPLE + "" + EnumChatFormatting.ITALIC + var17.getStringTagAt(var19));
							}
						}
					}
				}
			}

			Multimap var14 = this.AttributeModifiers;

			if (!var14.Empty)
			{
				var3.Add("");
				IEnumerator var16 = var14.entries().GetEnumerator();

				while (var16.MoveNext())
				{
					Entry var18 = (Entry)var16.Current;
					AttributeModifier var20 = (AttributeModifier)var18.Value;
					double var9 = var20.Amount;

					if (var20.ID == Item.field_111210_e)
					{
						var9 += (double)EnchantmentHelper.func_152377_a(this, EnumCreatureAttribute.UNDEFINED);
					}

					double var11;

					if (var20.Operation != 1 && var20.Operation != 2)
					{
						var11 = var9;
					}
					else
					{
						var11 = var9 * 100.0D;
					}

					if (var9 > 0.0D)
					{
						var3.Add(EnumChatFormatting.BLUE + StatCollector.translateToLocalFormatted("attribute.modifier.plus." + var20.Operation, new object[] {field_111284_a.format(var11), StatCollector.translateToLocal("attribute.name." + (string)var18.Key)}));
					}
					else if (var9 < 0.0D)
					{
						var11 *= -1.0D;
						var3.Add(EnumChatFormatting.RED + StatCollector.translateToLocalFormatted("attribute.modifier.take." + var20.Operation, new object[] {field_111284_a.format(var11), StatCollector.translateToLocal("attribute.name." + (string)var18.Key)}));
					}
				}
			}

			if (this.hasTagCompound() && this.TagCompound.getBoolean("Unbreakable"))
			{
				var3.Add(EnumChatFormatting.BLUE + StatCollector.translateToLocal("item.unbreakable"));
			}

			if (p_82840_2_ && this.ItemDamaged)
			{
				var3.Add("Durability: " + (this.MaxDamage - this.ItemDamageForDisplay) + " / " + this.MaxDamage);
			}

			return var3;
		}

		public bool hasEffect()
		{
			return this.Item.hasEffect(this);
		}

		public EnumRarity Rarity
		{
			get
			{
				return this.Item.getRarity(this);
			}
		}

///    
///     <summary> * True if it is a tool and has no enchantments to begin with </summary>
///     
		public bool isItemEnchantable()
		{
			get
			{
				return !this.Item.isItemTool(this) ? false : !this.ItemEnchanted;
			}
		}

///    
///     <summary> * Adds an enchantment with a desired level on the ItemStack. </summary>
///     
		public void addEnchantment(Enchantment p_77966_1_, int p_77966_2_)
		{
			if (this.stackTagCompound == null)
			{
				this.TagCompound = new NBTTagCompound();
			}

			if (!this.stackTagCompound.func_150297_b("ench", 9))
			{
				this.stackTagCompound.setTag("ench", new NBTTagList());
			}

			NBTTagList var3 = this.stackTagCompound.getTagList("ench", 10);
			NBTTagCompound var4 = new NBTTagCompound();
			var4.setShort("id", (short)p_77966_1_.effectId);
			var4.setShort("lvl", (short)((sbyte)p_77966_2_));
			var3.appendTag(var4);
		}

///    
///     <summary> * True if the item has enchantment data </summary>
///     
		public bool isItemEnchanted()
		{
			get
			{
				return this.stackTagCompound != null && this.stackTagCompound.func_150297_b("ench", 9);
			}
		}

		public void setTagInfo(string p_77983_1_, NBTBase p_77983_2_)
		{
			if (this.stackTagCompound == null)
			{
				this.TagCompound = new NBTTagCompound();
			}

			this.stackTagCompound.setTag(p_77983_1_, p_77983_2_);
		}

		public bool canEditBlocks()
		{
			return this.Item.canItemEditBlocks();
		}

///    
///     <summary> * Return whether this stack is on an item frame. </summary>
///     
		public bool isOnItemFrame()
		{
			get
			{
				return this.itemFrame != null;
			}
		}

///    
///     <summary> * Set the item frame this stack is on. </summary>
///     
		public EntityItemFrame ItemFrame
		{
			set
			{
				this.itemFrame = value;
			}
			get
			{
				return this.itemFrame;
			}
		}

///    
///     <summary> * Return the item frame this stack is on. Returns null if not on an item frame. </summary>
///     

///    
///     <summary> * Get this stack's repair cost, or 0 if no repair cost is defined. </summary>
///     
		public int RepairCost
		{
			get
			{
				return this.hasTagCompound() && this.stackTagCompound.func_150297_b("RepairCost", 3) ? this.stackTagCompound.getInteger("RepairCost") : 0;
			}
			set
			{
				if (!this.hasTagCompound())
				{
					this.stackTagCompound = new NBTTagCompound();
				}
	
				this.stackTagCompound.setInteger("RepairCost", value);
			}
		}

///    
///     <summary> * Set this stack's repair cost. </summary>
///     

///    
///     <summary> * Gets the attribute modifiers for this ItemStack.\nWill check for an NBT tag list containing modifiers for the
///     * stack. </summary>
///     
		public Multimap AttributeModifiers
		{
			get
			{
				object var1;
	
				if (this.hasTagCompound() && this.stackTagCompound.func_150297_b("AttributeModifiers", 9))
				{
					var1 = HashMultimap.create();
					NBTTagList var2 = this.stackTagCompound.getTagList("AttributeModifiers", 10);
	
					for (int var3 = 0; var3 < var2.tagCount(); ++var3)
					{
						NBTTagCompound var4 = var2.getCompoundTagAt(var3);
						AttributeModifier var5 = SharedMonsterAttributes.readAttributeModifierFromNBT(var4);
	
						if (var5.ID.LeastSignificantBits != 0L && var5.ID.MostSignificantBits != 0L)
						{
							((Multimap)var1).put(var4.getString("AttributeName"), var5);
						}
					}
				}
				else
				{
					var1 = this.Item.ItemAttributeModifiers;
				}
	
				return (Multimap)var1;
			}
		}

		public void func_150996_a(Item p_150996_1_)
		{
			this.field_151002_e = p_150996_1_;
		}

		public IChatComponent func_151000_E()
		{
			IChatComponent var1 = (new ChatComponentText("[")).appendText(this.DisplayName).appendText("]");

			if (this.field_151002_e != null)
			{
				NBTTagCompound var2 = new NBTTagCompound();
				this.writeToNBT(var2);
				var1.ChatStyle.ChatHoverEvent = new HoverEvent(HoverEvent.Action.SHOW_ITEM, new ChatComponentText(var2.ToString()));
				var1.ChatStyle.Color = this.Rarity.rarityColor;
			}

			return var1;
		}
	}

}