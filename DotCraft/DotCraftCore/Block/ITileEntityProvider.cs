namespace DotCraftCore.Block
{

	using TileEntity = DotCraftCore.TileEntity.TileEntity;
	using World = DotCraftCore.World.World;

	public interface ITileEntityProvider
	{
///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_);
	}

}