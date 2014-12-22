using System;
using System.Collections;

namespace DotCraftServer.nRealms
{
	public class RealmsServerStatusPinger
	{
		private static readonly Logger LOGGER = LogManager.Logger;
		private readonly IList connections = Collections.synchronizedList(new ArrayList());
		

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void pingServer(final String p_pingServer_1_, final ServerPing p_pingServer_2_) throws IOException
//JAVA TO VB & C# CONVERTER WARNING: 'final' parameters are not allowed in .NET:
		public virtual void pingServer(string p_pingServer_1_, ServerPing p_pingServer_2_)
		{
			if (p_pingServer_1_ != null && !p_pingServer_1_.StartsWith("0.0.0.0") && !p_pingServer_1_.Length == 0)
			{
				RealmsServerAddress var3 = RealmsServerAddress.parseString(p_pingServer_1_);
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final NetworkManager var4 = NetworkManager.provideLanClient(InetAddress.getByName(var3.getHost()), var3.getPort());
				NetworkManager var4 = NetworkManager.provideLanClient(InetAddress.getByName(var3.Host), var3.Port);
				this.connections.Add(var4);
				var4.NetHandler = new INetHandlerStatusClient() { private bool field_154345_e = false;  public void handleServerInfo(S00PacketServerInfo p_147397_1_) { ServerStatusResponse var2 = p_147397_1_.func_149294_c(); if (var2.func_151318_b() != null) { p_pingServer_2_.nrOfPlayers = Convert.ToString(var2.func_151318_b().func_151333_b()); } var4.scheduleOutboundPacket(new C01PacketPing(Realms.currentTimeMillis()), new GenericFutureListener[0]); this.field_154345_e = true; } public void handlePong(S01PacketPong p_147398_1_) { var4.closeChannel(new ChatComponentText("Finished")); } public void onDisconnect(IChatComponent p_147231_1_) { if (!this.field_154345_e) { RealmsServerStatusPinger.LOGGER.error("Can\'t ping " + p_pingServer_1_ + ": " + p_147231_1_.UnformattedText); } } public void onConnectionStateTransition(EnumConnectionState p_147232_1_, EnumConnectionState p_147232_2_) { if (p_147232_2_ != EnumConnectionState.STATUS) { throw new UnsupportedOperationException("Unexpected change in protocol to " + p_147232_2_); } } public void onNetworkTick() {} };

				try
				{
					var4.scheduleOutboundPacket(new C00Handshake(RealmsSharedConstants.NETWORK_PROTOCOL_VERSION, var3.Host, var3.Port, EnumConnectionState.STATUS), new GenericFutureListener[0]);
					var4.scheduleOutboundPacket(new C00PacketServerQuery(), new GenericFutureListener[0]);
				}
				catch (Exception var6)
				{
					LOGGER.error(var6);
				}
			}
		}

		public virtual void tick()
		{
			IList var1 = this.connections;

			lock (this.connections)
			{
				IEnumerator var2 = this.connections.GetEnumerator();

				while (var2.MoveNext())
				{
					NetworkManager var3 = (NetworkManager)var2.Current;

					if (var3.ChannelOpen)
					{
						var3.processReceivedPackets();
					}
					else
					{
						var2.remove();

						if (var3.ExitMessage != null)
						{
							var3.NetHandler.onDisconnect(var3.ExitMessage);
						}
					}
				}
			}
		}

		public virtual void removeAll()
		{
			IList var1 = this.connections;

			lock (this.connections)
			{
				IEnumerator var2 = this.connections.GetEnumerator();

				while (var2.MoveNext())
				{
					NetworkManager var3 = (NetworkManager)var2.Current;

					if (var3.ChannelOpen)
					{
						var2.remove();
						var3.closeChannel(new ChatComponentText("Cancelled"));
					}
				}
			}
		}
	}

}