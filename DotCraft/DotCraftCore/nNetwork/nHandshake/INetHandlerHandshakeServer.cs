namespace DotCraftCore.nNetwork.nHandshake
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using C00Handshake = DotCraftCore.nNetwork.nHandshake.nClient.C00Handshake;

	public interface INetHandlerHandshakeServer : INetHandler
	{
///    
///     <summary> * There are two recognized intentions for initiating a handshake: logging in and acquiring server status. The
///     * NetworkManager's protocol will be reconfigured according to the specified intention, although a login-intention
///     * must pass a versioncheck or receive a disconnect otherwise </summary>
///     
		void processHandshake(C00Handshake p_147383_1_);
	}

}