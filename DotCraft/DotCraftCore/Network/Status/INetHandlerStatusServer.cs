namespace DotCraftCore.Network.Status
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using C00PacketServerQuery = DotCraftCore.Network.Status.Client.C00PacketServerQuery;
	using C01PacketPing = DotCraftCore.Network.Status.Client.C01PacketPing;

	public interface INetHandlerStatusServer : INetHandler
	{
		void processPing(C01PacketPing p_147311_1_);

		void processServerQuery(C00PacketServerQuery p_147312_1_);
	}

}