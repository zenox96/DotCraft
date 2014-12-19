namespace DotCraftCore.nNetwork.nStatus
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using S00PacketServerInfo = DotCraftCore.nNetwork.nStatus.nServer.S00PacketServerInfo;
	using S01PacketPong = DotCraftCore.nNetwork.nStatus.nServer.S01PacketPong;

	public interface INetHandlerStatusClient : INetHandler
	{
		void handleServerInfo(S00PacketServerInfo p_147397_1_);

		void handlePong(S01PacketPong p_147398_1_);
	}

}