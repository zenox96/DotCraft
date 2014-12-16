namespace DotCraftCore.Item
{

	using Block = DotCraftCore.block.Block;
	using IIcon = DotCraftCore.Util.IIcon;

	public class ItemColored : ItemBlock
	{
		private readonly Block field_150944_b;
		private string[] field_150945_c;
		

		public ItemColored(Block p_i45332_1_, bool p_i45332_2_) : base(p_i45332_1_)
		{
			this.field_150944_b = p_i45332_1_;

			if (p_i45332_2_)
			{
				this.MaxDamage = 0;
				this.HasSubtypes = true;
			}
		}

		public virtual int getColorFromItemStack(ItemStack p_82790_1_, int p_82790_2_)
		{
			return this.field_150944_b.getRenderColor(p_82790_1_.ItemDamage);
		}

///    
///     <summary> * Gets an icon index based on an item's damage value </summary>
///     
		public override IIcon getIconFromDamage(int p_77617_1_)
		{
			return this.field_150944_b.getIcon(0, p_77617_1_);
		}

///    
///     <summary> * Returns the metadata of the block which this Item (ItemBlock) can place </summary>
///     
		public virtual int getMetadata(int p_77647_1_)
		{
			return p_77647_1_;
		}

		public virtual ItemColored func_150943_a(string[] p_150943_1_)
		{
			this.field_150945_c = p_150943_1_;
			return this;
		}

///    
///     <summary> * Returns the unlocalized name of this item. This version accepts an ItemStack so different stacks can have
///     * different names based on their damage or NBT. </summary>
///     
		public override string getUnlocalizedName(ItemStack p_77667_1_)
		{
			if (this.field_150945_c == null)
			{
				return base.getUnlocalizedName(p_77667_1_);
			}
			else
			{
				int var2 = p_77667_1_.ItemDamage;
				return var2 >= 0 && var2 < this.field_150945_c.Length ? base.getUnlocalizedName(p_77667_1_) + "." + this.field_150945_c[var2] : base.getUnlocalizedName(p_77667_1_);
			}
		}
	}

}