namespace DotCraftCore.nTileEntity
{

	using IInventory = DotCraftCore.nInventory.IInventory;
	using World = DotCraftCore.nWorld.World;

	public interface IHopper : IInventory
	{
///    
///     <summary> * Returns the worldObj for this tileEntity. </summary>
///     
		World WorldObj {get;}

///    
///     <summary> * Gets the world X position for this hopper entity. </summary>
///     
		double XPos {get;}

///    
///     <summary> * Gets the world Y position for this hopper entity. </summary>
///     
		double YPos {get;}

///    
///     <summary> * Gets the world Z position for this hopper entity. </summary>
///     
		double ZPos {get;}
	}

}