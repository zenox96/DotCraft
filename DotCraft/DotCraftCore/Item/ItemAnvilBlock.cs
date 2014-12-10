namespace DotCraftCore.Item
{

	using Block = DotCraftCore.block.Block;
	using BlockAnvil = DotCraftCore.block.BlockAnvil;

	public class ItemAnvilBlock : ItemMultiTexture
	{
		private const string __OBFID = "CL_00001764";

		public ItemAnvilBlock(Block p_i1826_1_) : base(p_i1826_1_, p_i1826_1_, BlockAnvil.field_149834_a)
		{
		}

///    
///     <summary> * Returns the metadata of the block which this Item (ItemBlock) can place </summary>
///     
		public override int getMetadata(int p_77647_1_)
		{
			return p_77647_1_ << 2;
		}
	}

}