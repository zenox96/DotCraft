namespace DotCraftCore.nItem
{

	using Block = DotCraftCore.nBlock.Block;
	using BlockColored = DotCraftCore.nBlock.BlockColored;
	using IIcon = DotCraftCore.nUtil.IIcon;

	public class ItemCloth : ItemBlock
	{
		

		public ItemCloth(Block p_i45358_1_) : base(p_i45358_1_)
		{
			this.MaxDamage = 0;
			this.HasSubtypes = true;
		}

///    
///     <summary> * Gets an icon index based on an item's damage value </summary>
///     
		public override IIcon getIconFromDamage(int p_77617_1_)
		{
			return this.field_150939_a.func_149735_b(2, BlockColored.func_150032_b(p_77617_1_));
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
			return base.UnlocalizedName + "." + ItemDye.field_150923_a[BlockColored.func_150032_b(p_77667_1_.ItemDamage)];
		}
	}

}