using System.Collections;

namespace DotCraftCore.nItem
{

	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using IIcon = DotCraftCore.nUtil.IIcon;

	public class ItemCoal : Item
	{
		private IIcon field_111220_a;
		

		public ItemCoal()
		{
			this.HasSubtypes = true;
			this.MaxDamage = 0;
			this.CreativeTab = CreativeTabs.tabMaterials;
		}

///    
///     <summary> * Returns the unlocalized name of this item. This version accepts an ItemStack so different stacks can have
///     * different names based on their damage or NBT. </summary>
///     
		public virtual string getUnlocalizedName(ItemStack p_77667_1_)
		{
			return p_77667_1_.ItemDamage == 1 ? "item.charcoal" : "item.coal";
		}

///    
///     <summary> * This returns the sub items </summary>
///     
		public virtual void getSubItems(Item p_150895_1_, CreativeTabs p_150895_2_, IList p_150895_3_)
		{
			p_150895_3_.Add(new ItemStack(p_150895_1_, 1, 0));
			p_150895_3_.Add(new ItemStack(p_150895_1_, 1, 1));
		}

///    
///     <summary> * Gets an icon index based on an item's damage value </summary>
///     
		public virtual IIcon getIconFromDamage(int p_77617_1_)
		{
			return p_77617_1_ == 1 ? this.field_111220_a : base.getIconFromDamage(p_77617_1_);
		}

		public virtual void registerIcons(IIconRegister p_94581_1_)
		{
			base.registerIcons(p_94581_1_);
			this.field_111220_a = p_94581_1_.registerIcon("charcoal");
		}
	}

}