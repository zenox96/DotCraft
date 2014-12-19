namespace DotCraftCore.nNetwork.nLogin
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using S00PacketDisconnect = DotCraftCore.nNetwork.nLogin.nServer.S00PacketDisconnect;
	using S01PacketEncryptionRequest = DotCraftCore.nNetwork.nLogin.nServer.S01PacketEncryptionRequest;
	using S02PacketLoginSuccess = DotCraftCore.nNetwork.nLogin.nServer.S02PacketLoginSuccess;

	public interface INetHandlerLoginClient : INetHandler
	{
		void handleEncryptionRequest(S01PacketEncryptionRequest p_147389_1_);

		void handleLoginSuccess(S02PacketLoginSuccess p_147390_1_);

		void handleDisconnect(S00PacketDisconnect p_147388_1_);
	}

}