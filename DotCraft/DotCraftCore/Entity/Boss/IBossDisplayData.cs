namespace DotCraftCore.Entity.Boss
{

	using IChatComponent = DotCraftCore.Util.IChatComponent;

	public interface IBossDisplayData
	{
		float MaxHealth {get;}

		float Health {get;}

		IChatComponent func_145748_c_();
	}

}