namespace DotCraftCore.nDispenser
{

	using TileEntity = DotCraftCore.nTileEntity.TileEntity;

	public interface IBlockSource : ILocatableSource
	{
		double X {get;}

		double Y {get;}

		double Z {get;}

		int XInt {get;}

		int YInt {get;}

		int ZInt {get;}

		int BlockMetadata {get;}

		TileEntity BlockTileEntity {get;}
	}

}