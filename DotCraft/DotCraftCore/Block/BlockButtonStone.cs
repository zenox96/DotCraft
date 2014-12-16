namespace DotCraftCore.Block
{

	using Blocks = DotCraftCore.Init.Blocks;
	using IIcon = DotCraftCore.Util.IIcon;

	public class BlockButtonStone : BlockButton
	{
		

		protected internal BlockButtonStone() : base(false)
		{
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return Blocks.stone.getBlockTextureFromSide(1);
		}
	}

}