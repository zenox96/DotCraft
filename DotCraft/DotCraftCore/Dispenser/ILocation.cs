namespace DotCraftCore.Dispenser
{

	using World = DotCraftCore.world.World;

	public interface ILocation : IPosition
	{
		World World {get;}
	}

}