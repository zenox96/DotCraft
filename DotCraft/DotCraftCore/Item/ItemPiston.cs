namespace DotCraftCore.Item
{

	using Block = DotCraftCore.block.Block;

	public class ItemPiston : ItemBlock
	{
		private const string __OBFID = "CL_00000054";

		public ItemPiston(Block p_i45348_1_) : base(p_i45348_1_)
		{
		}

///    
///     <summary> * Returns the metadata of the block which this Item (ItemBlock) can place </summary>
///     
		public virtual int getMetadata(int p_77647_1_)
		{
			return 7;
		}
	}

}