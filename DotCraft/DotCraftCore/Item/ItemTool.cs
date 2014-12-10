namespace DotCraftCore.Item
{

	using Multimap = com.google.common.collect.Multimap;
	using Block = DotCraftCore.block.Block;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using SharedMonsterAttributes = DotCraftCore.entity.SharedMonsterAttributes;
	using AttributeModifier = DotCraftCore.entity.ai.attributes.AttributeModifier;
	using World = DotCraftCore.World.World;

	public class ItemTool : Item
	{
		private Set field_150914_c;
		protected internal float efficiencyOnProperMaterial = 4.0F;

	/// <summary> Damage versus entities.  </summary>
		private float damageVsEntity;

	/// <summary> The material this tool is made from.  </summary>
		protected internal Item.ToolMaterial toolMaterial;
		private const string __OBFID = "CL_00000019";

		protected internal ItemTool(float p_i45333_1_, Item.ToolMaterial p_i45333_2_, Set p_i45333_3_)
		{
			this.toolMaterial = p_i45333_2_;
			this.field_150914_c = p_i45333_3_;
			this.maxStackSize = 1;
			this.MaxDamage = p_i45333_2_.MaxUses;
			this.efficiencyOnProperMaterial = p_i45333_2_.EfficiencyOnProperMaterial;
			this.damageVsEntity = p_i45333_1_ + p_i45333_2_.DamageVsEntity;
			this.CreativeTab = CreativeTabs.tabTools;
		}

		public virtual float func_150893_a(ItemStack p_150893_1_, Block p_150893_2_)
		{
			return this.field_150914_c.contains(p_150893_2_) ? this.efficiencyOnProperMaterial : 1.0F;
		}

///    
///     <summary> * Current implementations of this method in child classes do not use the entry argument beside ev. They just raise
///     * the damage on the stack. </summary>
///     
		public virtual bool hitEntity(ItemStack p_77644_1_, EntityLivingBase p_77644_2_, EntityLivingBase p_77644_3_)
		{
			p_77644_1_.damageItem(2, p_77644_3_);
			return true;
		}

		public virtual bool onBlockDestroyed(ItemStack p_150894_1_, World p_150894_2_, Block p_150894_3_, int p_150894_4_, int p_150894_5_, int p_150894_6_, EntityLivingBase p_150894_7_)
		{
			if ((double)p_150894_3_.getBlockHardness(p_150894_2_, p_150894_4_, p_150894_5_, p_150894_6_) != 0.0D)
			{
				p_150894_1_.damageItem(1, p_150894_7_);
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

		public virtual Item.ToolMaterial func_150913_i()
		{
			return this.toolMaterial;
		}

///    
///     <summary> * Return the enchantability factor of the item, most of the time is based on material. </summary>
///     
		public virtual int ItemEnchantability
		{
			get
			{
				return this.toolMaterial.Enchantability;
			}
		}

///    
///     <summary> * Return the name for this tool's material. </summary>
///     
		public virtual string ToolMaterialName
		{
			get
			{
				return this.toolMaterial.ToString();
			}
		}

///    
///     <summary> * Return whether this item is repairable in an anvil. </summary>
///     
		public virtual bool getIsRepairable(ItemStack p_82789_1_, ItemStack p_82789_2_)
		{
			return this.toolMaterial.func_150995_f() == p_82789_2_.Item ? true : base.getIsRepairable(p_82789_1_, p_82789_2_);
		}

///    
///     <summary> * Gets a map of item attribute modifiers, used by ItemSword to increase hit damage. </summary>
///     
		public virtual Multimap ItemAttributeModifiers
		{
			get
			{
				Multimap var1 = base.ItemAttributeModifiers;
				var1.put(SharedMonsterAttributes.attackDamage.AttributeUnlocalizedName, new AttributeModifier(field_111210_e, "Tool modifier", (double)this.damageVsEntity, 0));
				return var1;
			}
		}
	}

}