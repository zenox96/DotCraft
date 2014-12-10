namespace DotCraftCore.Network.Login
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using C00PacketLoginStart = DotCraftCore.Network.Login.Client.C00PacketLoginStart;
	using C01PacketEncryptionResponse = DotCraftCore.Network.Login.Client.C01PacketEncryptionResponse;

	public interface INetHandlerLoginServer : INetHandler
	{
		void processLoginStart(C00PacketLoginStart p_147316_1_);

		void processEncryptionResponse(C01PacketEncryptionResponse p_147315_1_);
	}

}