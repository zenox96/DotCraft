namespace DotCraftCore.nBlock
{

	using Blocks = DotCraftCore.nInit.Blocks;
	using IIcon = DotCraftCore.nUtil.IIcon;

	public class BlockButtonWood : BlockButton
	{
		

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