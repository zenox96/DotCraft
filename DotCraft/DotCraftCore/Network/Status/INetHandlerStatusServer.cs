namespace DotCraftCore.nNetwork.nStatus
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using C00PacketServerQuery = DotCraftCore.nNetwork.nStatus.nClient.C00PacketServerQuery;
	using C01PacketPing = DotCraftCore.nNetwork.nStatus.nClient.C01PacketPing;

	public interface INetHandlerStatusServer : INetHandler
	{
		void processPing(C01PacketPing p_147311_1_);

		void processServerQuery(C00PacketServerQuery p_147312_1_);
	}

}