namespace DotCraftCore.Dispenser
{

	using World = DotCraftCore.World.World;

	public interface ILocation : IPosition
	{
		World World {get;}
	}

}