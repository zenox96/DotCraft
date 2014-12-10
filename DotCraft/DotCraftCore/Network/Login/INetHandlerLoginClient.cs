namespace DotCraftCore.Network.Login
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using S00PacketDisconnect = DotCraftCore.Network.Login.Server.S00PacketDisconnect;
	using S01PacketEncryptionRequest = DotCraftCore.Network.Login.Server.S01PacketEncryptionRequest;
	using S02PacketLoginSuccess = DotCraftCore.Network.Login.Server.S02PacketLoginSuccess;

	public interface INetHandlerLoginClient : INetHandler
	{
		void handleEncryptionRequest(S01PacketEncryptionRequest p_147389_1_);

		void handleLoginSuccess(S02PacketLoginSuccess p_147390_1_);

		void handleDisconnect(S00PacketDisconnect p_147388_1_);
	}

}