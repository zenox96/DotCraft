namespace DotCraftCore.nServer.nNetwork
{

	using GenericFutureListener = io.netty.util.concurrent.GenericFutureListener;
	using EnumConnectionState = DotCraftCore.network.EnumConnectionState;
	using NetworkManager = DotCraftCore.network.NetworkManager;
	using INetHandlerStatusServer = DotCraftCore.network.status.INetHandlerStatusServer;
	using C00PacketServerQuery = DotCraftCore.network.status.client.C00PacketServerQuery;
	using C01PacketPing = DotCraftCore.network.status.client.C01PacketPing;
	using S00PacketServerInfo = DotCraftCore.network.status.server.S00PacketServerInfo;
	using S01PacketPong = DotCraftCore.network.status.server.S01PacketPong;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using IChatComponent = DotCraftCore.nUtil.IChatComponent;

	public class NetHandlerStatusServer : INetHandlerStatusServer
	{
		private readonly MinecraftServer field_147314_a;
		private readonly NetworkManager field_147313_b;
		

		public NetHandlerStatusServer(MinecraftServer p_i45299_1_, NetworkManager p_i45299_2_)
		{
			this.field_147314_a = p_i45299_1_;
			this.field_147313_b = p_i45299_2_;
		}

///    
///     <summary> * Invoked when disconnecting, the parameter is a ChatComponent describing the reason for termination </summary>
///     
		public virtual void onDisconnect(IChatComponent p_147231_1_)
		{
		}

///    
///     <summary> * Allows validation of the connection state transition. Parameters: from, to (connection state). Typically throws
///     * IllegalStateException or UnsupportedOperationException if validation fails </summary>
///     
		public virtual void onConnectionStateTransition(EnumConnectionState p_147232_1_, EnumConnectionState p_147232_2_)
		{
			if(p_147232_2_ != EnumConnectionState.STATUS)
			{
				throw new UnsupportedOperationException("Unexpected change in protocol to " + p_147232_2_);
			}
		}

///    
///     <summary> * For scheduled network tasks. Used in NetHandlerPlayServer to send keep-alive packets and in NetHandlerLoginServer
///     * for a login-timeout </summary>
///     
		public virtual void onNetworkTick()
		{
		}

		public virtual void processServerQuery(C00PacketServerQuery p_147312_1_)
		{
			this.field_147313_b.scheduleOutboundPacket(new S00PacketServerInfo(this.field_147314_a.func_147134_at()), new GenericFutureListener[0]);
		}

		public virtual void processPing(C01PacketPing p_147311_1_)
		{
			this.field_147313_b.scheduleOutboundPacket(new S01PacketPong(p_147311_1_.func_149289_c()), new GenericFutureListener[0]);
		}
	}

}