namespace DotCraftCore.Block
{

	using Blocks = DotCraftCore.Init.Blocks;
	using IIcon = DotCraftCore.Util.IIcon;

	public class BlockButtonWood : BlockButton
	{
		private const string __OBFID = "CL_00000336";

		protected internal BlockButtonWood() : base(true)
		{
		}

///    
///     <summary> * Gets the block's texture. Args: side, meta </summary>
///     
		public virtual IIcon getIcon(int p_149691_1_, int p_149691_2_)
		{
			return Blocks.planks.getBlockTextureFromSide(1);
		}
	}

}