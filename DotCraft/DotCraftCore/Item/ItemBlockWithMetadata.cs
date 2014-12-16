namespace DotCraftCore.Item
{

	using Block = DotCraftCore.block.Block;
	using IIcon = DotCraftCore.Util.IIcon;

	public class ItemBlockWithMetadata : ItemBlock
	{
		private Block field_150950_b;
		

		public ItemBlockWithMetadata(Block p_i45326_1_, Block p_i45326_2_) : base(p_i45326_1_)
		{
			this.field_150950_b = p_i45326_2_;
			this.MaxDamage = 0;
			this.HasSubtypes = true;
		}

///    
///     <summary> * Gets an icon index based on an item's damage value </summary>
///     
		public override IIcon getIconFromDamage(int p_77617_1_)
		{
			return this.field_150950_b.getIcon(2, p_77617_1_);
		}

///    
///     <summary> * Returns the metadata of the block which this Item (ItemBlock) can place </summary>
///     
		public virtual int getMetadata(int p_77647_1_)
		{
			return p_77647_1_;
		}
	}

}