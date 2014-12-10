namespace DotCraftCore.Network
{

	using IChatComponent = DotCraftCore.Util.IChatComponent;

	public interface INetHandler
	{
///    
///     <summary> * Invoked when disconnecting, the parameter is a ChatComponent describing the reason for termination </summary>
///     
		void onDisconnect(IChatComponent p_147231_1_);

///    
///     <summary> * Allows validation of the connection state transition. Parameters: from, to (connection state). Typically throws
///     * IllegalStateException or UnsupportedOperationException if validation fails </summary>
///     
		void onConnectionStateTransition(EnumConnectionState p_147232_1_, EnumConnectionState p_147232_2_);

///    
///     <summary> * For scheduled network tasks. Used in NetHandlerPlayServer to send keep-alive packets and in NetHandlerLoginServer
///     * for a login-timeout </summary>
///     
		void onNetworkTick();
	}

}