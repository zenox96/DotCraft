namespace DotCraftCore.nDispenser
{

	using World = DotCraftCore.nWorld.World;

	public interface ILocation : IPosition
	{
		World World {get;}
	}

}