using System;
using System.Threading;

namespace DotCraftCore.Realms
{

	using GenericFutureListener = io.netty.util.concurrent.GenericFutureListener;
	using Minecraft = DotCraftCore.client.Minecraft;
	using NetHandlerLoginClient = DotCraftCore.client.network.NetHandlerLoginClient;
	using EnumConnectionState = DotCraftCore.Network.EnumConnectionState;
	using NetworkManager = DotCraftCore.Network.NetworkManager;
	using C00Handshake = DotCraftCore.Network.Handshake.Client.C00Handshake;
	using C00PacketLoginStart = DotCraftCore.Network.Login.Client.C00PacketLoginStart;
	using ChatComponentTranslation = DotCraftCore.Util.ChatComponentTranslation;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class RealmsConnect
	{
		private static readonly Logger LOGGER = LogManager.Logger;
		private readonly RealmsScreen onlineScreen;
		private volatile bool aborted = false;
		private NetworkManager connection;
		private const string __OBFID = "CL_00001844";

		public RealmsConnect(RealmsScreen p_i1079_1_)
		{
			this.onlineScreen = p_i1079_1_;
		}

//JAVA TO VB & C# CONVERTER WARNING: 'final' parameters are not allowed in .NET:
//ORIGINAL LINE: public void connect(final String p_connect_1_, final int p_connect_2_)
		public virtual void connect(string p_connect_1_, int p_connect_2_)
		{
			(new Thread("Realms-connect-task") { private static final string __OBFID = "CL_00001808"; public void run() { InetAddress var1 = null; try { var1 = InetAddress.getByName(p_connect_1_); if (RealmsConnect.aborted) { return; } RealmsConnect.connection = NetworkManager.provideLanClient(var1, p_connect_2_); if (RealmsConnect.aborted) { return; } RealmsConnect.connection.setNetHandler(new NetHandlerLoginClient(RealmsConnect.connection, Minecraft.Minecraft, RealmsConnect.onlineScreen.Proxy)); if (RealmsConnect.aborted) { return; } RealmsConnect.connection.scheduleOutboundPacket(new C00Handshake(5, p_connect_1_, p_connect_2_, EnumConnectionState.LOGIN), new GenericFutureListener[0]); if (RealmsConnect.aborted) { return; } RealmsConnect.connection.scheduleOutboundPacket(new C00PacketLoginStart(Minecraft.Minecraft.Session.func_148256_e()), new GenericFutureListener[0]); } catch (UnknownHostException var5) { if (RealmsConnect.aborted) { return; } RealmsConnect.LOGGER.error("Couldn\'t connect to world", var5); Realms.setScreen(new DisconnectedOnlineScreen(RealmsConnect.onlineScreen, "connect.failed", new ChatComponentTranslation("disconnect.genericReason", new object[] {"Unknown host \'" + p_connect_1_ + "\'"}))); } catch (Exception var6) { if (RealmsConnect.aborted) { return; } RealmsConnect.LOGGER.error("Couldn\'t connect to world", var6); string var3 = var6.ToString(); if (var1 != null) { string var4 = var1.ToString() + ":" + p_connect_2_; var3 = var3.replaceAll(var4, ""); } Realms.setScreen(new DisconnectedOnlineScreen(RealmsConnect.onlineScreen, "connect.failed", new ChatComponentTranslation("disconnect.genericReason", new object[] {var3}))); } } }).start();
		}

		public virtual void abort()
		{
			this.aborted = true;
		}

		public virtual void tick()
		{
			if (this.connection != null)
			{
				if (this.connection.ChannelOpen)
				{
					this.connection.processReceivedPackets();
				}
				else if (this.connection.ExitMessage != null)
				{
					this.connection.NetHandler.onDisconnect(this.connection.ExitMessage);
				}
			}
		}
	}

}