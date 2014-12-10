namespace DotCraftCore.Block
{

	using Blocks = DotCraftCore.init.Blocks;
	using IIcon = DotCraftCore.util.IIcon;

	public class BlockButtonStone : BlockButton
	{
		private const string __OBFID = "CL_00000319";

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