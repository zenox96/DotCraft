namespace DotCraftCore.nNetwork.nLogin
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using C00PacketLoginStart = DotCraftCore.nNetwork.nLogin.nClient.C00PacketLoginStart;
	using C01PacketEncryptionResponse = DotCraftCore.nNetwork.nLogin.nClient.C01PacketEncryptionResponse;

	public interface INetHandlerLoginServer : INetHandler
	{
		void processLoginStart(C00PacketLoginStart p_147316_1_);

		void processEncryptionResponse(C01PacketEncryptionResponse p_147315_1_);
	}

}