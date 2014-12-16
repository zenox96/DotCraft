namespace DotCraftCore.Item
{

	using BlockLeaves = DotCraftCore.block.BlockLeaves;
	using IIcon = DotCraftCore.Util.IIcon;

	public class ItemLeaves : ItemBlock
	{
		private readonly BlockLeaves field_150940_b;
		

		public ItemLeaves(BlockLeaves p_i45344_1_) : base(p_i45344_1_)
		{
			this.field_150940_b = p_i45344_1_;
			this.MaxDamage = 0;
			this.HasSubtypes = true;
		}

///    
///     <summary> * Returns the metadata of the block which this Item (ItemBlock) can place </summary>
///     
		public virtual int getMetadata(int p_77647_1_)
		{
			return p_77647_1_ | 4;
		}

///    
///     <summary> * Gets an icon index based on an item's damage value </summary>
///     
		public override IIcon getIconFromDamage(int p_77617_1_)
		{
			return this.field_150940_b.getIcon(0, p_77617_1_);
		}

		public virtual int getColorFromItemStack(ItemStack p_82790_1_, int p_82790_2_)
		{
			return this.field_150940_b.getRenderColor(p_82790_1_.ItemDamage);
		}

///    
///     <summary> * Returns the unlocalized name of this item. This version accepts an ItemStack so different stacks can have
///     * different names based on their damage or NBT. </summary>
///     
		public override string getUnlocalizedName(ItemStack p_77667_1_)
		{
			int var2 = p_77667_1_.ItemDamage;

			if (var2 < 0 || var2 >= this.field_150940_b.func_150125_e().length)
			{
				var2 = 0;
			}

			return base.UnlocalizedName + "." + this.field_150940_b.func_150125_e()[var2];
		}
	}

}