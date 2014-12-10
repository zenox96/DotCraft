namespace DotCraftCore.Network.Handshake
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using C00Handshake = DotCraftCore.Network.Handshake.Client.C00Handshake;

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