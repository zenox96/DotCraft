namespace DotCraftCore.Network.Status
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using S00PacketServerInfo = DotCraftCore.Network.Status.Server.S00PacketServerInfo;
	using S01PacketPong = DotCraftCore.Network.Status.Server.S01PacketPong;

	public interface INetHandlerStatusClient : INetHandler
	{
		void handleServerInfo(S00PacketServerInfo p_147397_1_);

		void handlePong(S01PacketPong p_147398_1_);
	}

}