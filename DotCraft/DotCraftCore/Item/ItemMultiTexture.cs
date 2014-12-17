namespace DotCraftCore.nItem
{

	using Block = DotCraftCore.nBlock.Block;
	using IIcon = DotCraftCore.nUtil.IIcon;

	public class ItemMultiTexture : ItemBlock
	{
		protected internal readonly Block field_150941_b;
		protected internal readonly string[] field_150942_c;
		

		public ItemMultiTexture(Block p_i45346_1_, Block p_i45346_2_, string[] p_i45346_3_) : base(p_i45346_1_)
		{
			this.field_150941_b = p_i45346_2_;
			this.field_150942_c = p_i45346_3_;
			this.MaxDamage = 0;
			this.HasSubtypes = true;
		}

///    
///     <summary> * Gets an icon index based on an item's damage value </summary>
///     
		public override IIcon getIconFromDamage(int p_77617_1_)
		{
			return this.field_150941_b.getIcon(2, p_77617_1_);
		}

///    
///     <summary> * Returns the metadata of the block which this Item (ItemBlock) can place </summary>
///     
		public virtual int getMetadata(int p_77647_1_)
		{
			return p_77647_1_;
		}

///    
///     <summary> * Returns the unlocalized name of this item. This version accepts an ItemStack so different stacks can have
///     * different names based on their damage or NBT. </summary>
///     
		public override string getUnlocalizedName(ItemStack p_77667_1_)
		{
			int var2 = p_77667_1_.ItemDamage;

			if (var2 < 0 || var2 >= this.field_150942_c.Length)
			{
				var2 = 0;
			}

			return base.UnlocalizedName + "." + this.field_150942_c[var2];
		}
	}

}