namespace DotCraftCore.nItem
{

	using Multimap = com.google.common.collect.Multimap;
	using Block = DotCraftCore.nBlock.Block;
	using Material = DotCraftCore.nBlock.nMaterial.Material;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using SharedMonsterAttributes = DotCraftCore.entity.SharedMonsterAttributes;
	using AttributeModifier = DotCraftCore.entity.ai.attributes.AttributeModifier;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.nWorld.World;

	public class ItemSword : Item
	{
		private float field_150934_a;
		private readonly Item.ToolMaterial field_150933_b;
		

		public ItemSword(Item.ToolMaterial p_i45356_1_)
		{
			this.field_150933_b = p_i45356_1_;
			this.maxStackSize = 1;
			this.MaxDamage = p_i45356_1_.MaxUses;
			this.CreativeTab = CreativeTabs.tabCombat;
			this.field_150934_a = 4.0F + p_i45356_1_.DamageVsEntity;
		}

		public virtual float func_150931_i()
		{
			return this.field_150933_b.DamageVsEntity;
		}

		public virtual float func_150893_a(ItemStack p_150893_1_, Block p_150893_2_)
		{
			if (p_150893_2_ == Blocks.web)
			{
				return 15.0F;
			}
			else
			{
				Material var3 = p_150893_2_.Material;
				return var3 != Material.plants && var3 != Material.vine && var3 != Material.coral && var3 != Material.leaves && var3 != Material.field_151572_C ? 1.0F : 1.5F;
			}
		}

///    
///     <summary> * Current implementations of this method in child classes do not use the entry argument beside ev. They just raise
///     * the damage on the stack. </summary>
///     
		public virtual bool hitEntity(ItemStack p_77644_1_, EntityLivingBase p_77644_2_, EntityLivingBase p_77644_3_)
		{
			p_77644_1_.damageItem(1, p_77644_3_);
			return true;
		}

		public virtual bool onBlockDestroyed(ItemStack p_150894_1_, World p_150894_2_, Block p_150894_3_, int p_150894_4_, int p_150894_5_, int p_150894_6_, EntityLivingBase p_150894_7_)
		{
			if ((double)p_150894_3_.getBlockHardness(p_150894_2_, p_150894_4_, p_150894_5_, p_150894_6_) != 0.0D)
			{
				p_150894_1_.damageItem(2, p_150894_7_);
			}

			return true;
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
///     <summary> * returns the action that specifies what animation to play when the items is being used </summary>
///     
		public virtual EnumAction getItemUseAction(ItemStack p_77661_1_)
		{
			return EnumAction.block;
		}

///    
///     <summary> * How long it takes to use or consume an item </summary>
///     
		public virtual int getMaxItemUseDuration(ItemStack p_77626_1_)
		{
			return 72000;
		}

///    
///     <summary> * Called whenever this item is equipped and the right mouse button is pressed. Args: itemStack, world, entityPlayer </summary>
///     
		public virtual ItemStack onItemRightClick(ItemStack p_77659_1_, World p_77659_2_, EntityPlayer p_77659_3_)
		{
			p_77659_3_.setItemInUse(p_77659_1_, this.getMaxItemUseDuration(p_77659_1_));
			return p_77659_1_;
		}

		public virtual bool func_150897_b(Block p_150897_1_)
		{
			return p_150897_1_ == Blocks.web;
		}

///    
///     <summary> * Return the enchantability factor of the item, most of the time is based on material. </summary>
///     
		public virtual int ItemEnchantability
		{
			get
			{
				return this.field_150933_b.Enchantability;
			}
		}

		public virtual string func_150932_j()
		{
			return this.field_150933_b.ToString();
		}

///    
///     <summary> * Return whether this item is repairable in an anvil. </summary>
///     
		public virtual bool getIsRepairable(ItemStack p_82789_1_, ItemStack p_82789_2_)
		{
			return this.field_150933_b.func_150995_f() == p_82789_2_.Item ? true : base.getIsRepairable(p_82789_1_, p_82789_2_);
		}

///    
///     <summary> * Gets a map of item attribute modifiers, used by ItemSword to increase hit damage. </summary>
///     
		public virtual Multimap ItemAttributeModifiers
		{
			get
			{
				Multimap var1 = base.ItemAttributeModifiers;
				var1.put(SharedMonsterAttributes.attackDamage.AttributeUnlocalizedName, new AttributeModifier(field_111210_e, "Weapon modifier", (double)this.field_150934_a, 0));
				return var1;
			}
		}
	}

}