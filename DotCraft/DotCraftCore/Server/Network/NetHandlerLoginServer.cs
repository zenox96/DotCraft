using System;
using System.Threading;

namespace DotCraftCore.Server.Network
{

	using Charsets = com.google.common.base.Charsets;
	using GameProfile = com.mojang.authlib.GameProfile;
	using AuthenticationUnavailableException = com.mojang.authlib.exceptions.AuthenticationUnavailableException;
	using GenericFutureListener = io.netty.util.concurrent.GenericFutureListener;
	using SecretKey = javax.crypto.SecretKey;
	using EnumConnectionState = DotCraftCore.network.EnumConnectionState;
	using NetworkManager = DotCraftCore.network.NetworkManager;
	using INetHandlerLoginServer = DotCraftCore.network.login.INetHandlerLoginServer;
	using C00PacketLoginStart = DotCraftCore.network.login.client.C00PacketLoginStart;
	using C01PacketEncryptionResponse = DotCraftCore.network.login.client.C01PacketEncryptionResponse;
	using S00PacketDisconnect = DotCraftCore.network.login.server.S00PacketDisconnect;
	using S01PacketEncryptionRequest = DotCraftCore.network.login.server.S01PacketEncryptionRequest;
	using S02PacketLoginSuccess = DotCraftCore.network.login.server.S02PacketLoginSuccess;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using ChatComponentText = DotCraftCore.Util.ChatComponentText;
	using CryptManager = DotCraftCore.Util.CryptManager;
	using IChatComponent = DotCraftCore.Util.IChatComponent;
	using Validate = org.apache.commons.lang3.Validate;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class NetHandlerLoginServer : INetHandlerLoginServer
	{
		private static readonly AtomicInteger field_147331_b = new AtomicInteger(0);
		private static readonly Logger logger = LogManager.Logger;
		private static readonly Random field_147329_d = new Random();
		private readonly sbyte[] field_147330_e = new sbyte[4];
		private readonly MinecraftServer field_147327_f;
		public readonly NetworkManager field_147333_a;
		private NetHandlerLoginServer.LoginState field_147328_g;
		private int field_147336_h;
		private GameProfile field_147337_i;
		private string field_147334_j;
		private SecretKey field_147335_k;
		

		public NetHandlerLoginServer(MinecraftServer p_i45298_1_, NetworkManager p_i45298_2_)
		{
			this.field_147328_g = NetHandlerLoginServer.LoginState.HELLO;
			this.field_147334_j = "";
			this.field_147327_f = p_i45298_1_;
			this.field_147333_a = p_i45298_2_;
			field_147329_d.nextBytes(this.field_147330_e);
		}

///    
///     <summary> * For scheduled network tasks. Used in NetHandlerPlayServer to send keep-alive packets and in NetHandlerLoginServer
///     * for a login-timeout </summary>
///     
		public virtual void onNetworkTick()
		{
			if(this.field_147328_g == NetHandlerLoginServer.LoginState.READY_TO_ACCEPT)
			{
				this.func_147326_c();
			}

			if(this.field_147336_h++ == 600)
			{
				this.func_147322_a("Took too long to log in");
			}
		}

		public virtual void func_147322_a(string p_147322_1_)
		{
			try
			{
				logger.info("Disconnecting " + this.func_147317_d() + ": " + p_147322_1_);
				ChatComponentText var2 = new ChatComponentText(p_147322_1_);
				this.field_147333_a.scheduleOutboundPacket(new S00PacketDisconnect(var2), new GenericFutureListener[0]);
				this.field_147333_a.closeChannel(var2);
			}
			catch (Exception var3)
			{
				logger.error("Error whilst disconnecting player", var3);
			}
		}

		public virtual void func_147326_c()
		{
			if(!this.field_147337_i.Complete)
			{
				this.field_147337_i = this.func_152506_a(this.field_147337_i);
			}

			string var1 = this.field_147327_f.ConfigurationManager.func_148542_a(this.field_147333_a.SocketAddress, this.field_147337_i);

			if(var1 != null)
			{
				this.func_147322_a(var1);
			}
			else
			{
				this.field_147328_g = NetHandlerLoginServer.LoginState.ACCEPTED;
				this.field_147333_a.scheduleOutboundPacket(new S02PacketLoginSuccess(this.field_147337_i), new GenericFutureListener[0]);
				this.field_147327_f.ConfigurationManager.initializeConnectionToPlayer(this.field_147333_a, this.field_147327_f.ConfigurationManager.func_148545_a(this.field_147337_i));
			}
		}

///    
///     <summary> * Invoked when disconnecting, the parameter is a ChatComponent describing the reason for termination </summary>
///     
		public virtual void onDisconnect(IChatComponent p_147231_1_)
		{
			logger.info(this.func_147317_d() + " lost connection: " + p_147231_1_.UnformattedText);
		}

		public virtual string func_147317_d()
		{
			return this.field_147337_i != null ? this.field_147337_i.ToString() + " (" + this.field_147333_a.SocketAddress.ToString() + ")" : Convert.ToString(this.field_147333_a.SocketAddress);
		}

///    
///     <summary> * Allows validation of the connection state transition. Parameters: from, to (connection state). Typically throws
///     * IllegalStateException or UnsupportedOperationException if validation fails </summary>
///     
		public virtual void onConnectionStateTransition(EnumConnectionState p_147232_1_, EnumConnectionState p_147232_2_)
		{
			Validate.validState(this.field_147328_g == NetHandlerLoginServer.LoginState.ACCEPTED || this.field_147328_g == NetHandlerLoginServer.LoginState.HELLO, "Unexpected change in protocol", new object[0]);
			Validate.validState(p_147232_2_ == EnumConnectionState.PLAY || p_147232_2_ == EnumConnectionState.LOGIN, "Unexpected protocol " + p_147232_2_, new object[0]);
		}

		public virtual void processLoginStart(C00PacketLoginStart p_147316_1_)
		{
			Validate.validState(this.field_147328_g == NetHandlerLoginServer.LoginState.HELLO, "Unexpected hello packet", new object[0]);
			this.field_147337_i = p_147316_1_.func_149304_c();

			if(this.field_147327_f.ServerInOnlineMode && !this.field_147333_a.LocalChannel)
			{
				this.field_147328_g = NetHandlerLoginServer.LoginState.KEY;
				this.field_147333_a.scheduleOutboundPacket(new S01PacketEncryptionRequest(this.field_147334_j, this.field_147327_f.KeyPair.Public, this.field_147330_e), new GenericFutureListener[0]);
			}
			else
			{
				this.field_147328_g = NetHandlerLoginServer.LoginState.READY_TO_ACCEPT;
			}
		}

		public virtual void processEncryptionResponse(C01PacketEncryptionResponse p_147315_1_)
		{
			Validate.validState(this.field_147328_g == NetHandlerLoginServer.LoginState.KEY, "Unexpected key packet", new object[0]);
			PrivateKey var2 = this.field_147327_f.KeyPair.Private;

			if(!Array.Equals(this.field_147330_e, p_147315_1_.func_149299_b(var2)))
			{
				throw new IllegalStateException("Invalid nonce!");
			}
			else
			{
				this.field_147335_k = p_147315_1_.func_149300_a(var2);
				this.field_147328_g = NetHandlerLoginServer.LoginState.AUTHENTICATING;
				this.field_147333_a.enableEncryption(this.field_147335_k);
				(new Thread("User Authenticator #" + field_147331_b.incrementAndGet()) {  public void run() { GameProfile var1 = NetHandlerLoginServer.field_147337_i; try { string var2 = (new BigInteger(CryptManager.getServerIdHash(NetHandlerLoginServer.field_147334_j, NetHandlerLoginServer.field_147327_f.KeyPair.Public, NetHandlerLoginServer.field_147335_k))).ToString(16); NetHandlerLoginServer.field_147337_i = NetHandlerLoginServer.field_147327_f.func_147130_as().hasJoinedServer(new GameProfile((UUID)null, var1.Name), var2); if(NetHandlerLoginServer.field_147337_i != null) { NetHandlerLoginServer.logger.info("UUID of player " + NetHandlerLoginServer.field_147337_i.Name + " is " + NetHandlerLoginServer.field_147337_i.Id); NetHandlerLoginServer.field_147328_g = NetHandlerLoginServer.LoginState.READY_TO_ACCEPT; } else if(NetHandlerLoginServer.field_147327_f.SinglePlayer) { NetHandlerLoginServer.logger.warn("Failed to verify username but will let them in anyway!"); NetHandlerLoginServer.field_147337_i = NetHandlerLoginServer.func_152506_a(var1); NetHandlerLoginServer.field_147328_g = NetHandlerLoginServer.LoginState.READY_TO_ACCEPT; } else { NetHandlerLoginServer.func_147322_a("Failed to verify username!"); NetHandlerLoginServer.logger.error("Username \'" + NetHandlerLoginServer.field_147337_i.Name + "\' tried to join with an invalid session"); } } catch (AuthenticationUnavailableException var3) { if(NetHandlerLoginServer.field_147327_f.SinglePlayer) { NetHandlerLoginServer.logger.warn("Authentication servers are down but will let them in anyway!"); NetHandlerLoginServer.field_147337_i = NetHandlerLoginServer.func_152506_a(var1); NetHandlerLoginServer.field_147328_g = NetHandlerLoginServer.LoginState.READY_TO_ACCEPT; } else { NetHandlerLoginServer.func_147322_a("Authentication servers are down. Please try again later, sorry!"); NetHandlerLoginServer.logger.error("Couldn\'t verify username because servers are unavailable"); } } } }).start();
			}
		}

		protected internal virtual GameProfile func_152506_a(GameProfile p_152506_1_)
		{
			UUID var2 = UUID.nameUUIDFromBytes(("OfflinePlayer:" + p_152506_1_.Name).getBytes(Charsets.UTF_8));
			return new GameProfile(var2, p_152506_1_.Name);
		}

		internal enum LoginState
		{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			HELLO("HELLO", 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			KEY("KEY", 1),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			AUTHENTICATING("AUTHENTICATING", 2),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			READY_TO_ACCEPT("READY_TO_ACCEPT", 3),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			ACCEPTED("ACCEPTED", 4);

			@private static final NetHandlerLoginServer.LoginState[] $VALUES = new NetHandlerLoginServer.LoginState[]{HELLO, KEY, AUTHENTICATING, READY_TO_ACCEPT, ACCEPTED
		}
			

			private LoginState(string p_i45297_1_, int p_i45297_2_)
			{
			}
		}
	}

}