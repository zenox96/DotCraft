namespace DotCraftCore.nEntity.nBoss
{

	using IChatComponent = DotCraftCore.nUtil.IChatComponent;

	public interface IBossDisplayData
	{
		float MaxHealth {get;}

		float Health {get;}

		IChatComponent func_145748_c_();
	}

}