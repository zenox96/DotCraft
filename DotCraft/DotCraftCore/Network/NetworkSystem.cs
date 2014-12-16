using System;
using System.Collections;

namespace DotCraftCore.Network
{

	using ThreadFactoryBuilder = com.google.common.util.concurrent.ThreadFactoryBuilder;
	using ServerBootstrap = io.netty.bootstrap.ServerBootstrap;
	using Channel = io.netty.channel.Channel;
	using ChannelException = io.netty.channel.ChannelException;
	using ChannelFuture = io.netty.channel.ChannelFuture;
	using ChannelInitializer = io.netty.channel.ChannelInitializer;
	using ChannelOption = io.netty.channel.ChannelOption;
	using LocalAddress = io.netty.channel.local.LocalAddress;
	using LocalServerChannel = io.netty.channel.local.LocalServerChannel;
	using NioEventLoopGroup = io.netty.channel.nio.NioEventLoopGroup;
	using NioServerSocketChannel = io.netty.channel.socket.nio.NioServerSocketChannel;
	using ReadTimeoutHandler = io.netty.handler.timeout.ReadTimeoutHandler;
	using Future = io.netty.util.concurrent.Future;
	using GenericFutureListener = io.netty.util.concurrent.GenericFutureListener;
	using NetHandlerHandshakeMemory = DotCraftCore.client.network.NetHandlerHandshakeMemory;
	using CrashReport = DotCraftCore.crash.CrashReport;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using S40PacketDisconnect = DotCraftCore.Network.Play.Server.S40PacketDisconnect;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using NetHandlerHandshakeTCP = DotCraftCore.Server.Network.NetHandlerHandshakeTCP;
	using ChatComponentText = DotCraftCore.Util.ChatComponentText;
	using MessageDeserializer = DotCraftCore.Util.MessageDeserializer;
	using MessageDeserializer2 = DotCraftCore.Util.MessageDeserializer2;
	using MessageSerializer = DotCraftCore.Util.MessageSerializer;
	using MessageSerializer2 = DotCraftCore.Util.MessageSerializer2;
	using ReportedException = DotCraftCore.Util.ReportedException;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class NetworkSystem
	{
		private static readonly Logger logger = LogManager.Logger;
		private static readonly NioEventLoopGroup eventLoops = new NioEventLoopGroup(0, (new ThreadFactoryBuilder()).setNameFormat("Netty IO #%d").setDaemon(true).build());

	/// <summary> Reference to the MinecraftServer object.  </summary>
		private readonly MinecraftServer mcServer;

	/// <summary> True if this NetworkSystem has never had his endpoints terminated  </summary>
		public volatile bool isAlive;

	/// <summary> Contains all endpoints added to this NetworkSystem  </summary>
		private readonly IList endpoints = Collections.synchronizedList(new ArrayList());

	/// <summary> A list containing all NetworkManager instances of all endpoints  </summary>
		private readonly IList networkManagers = Collections.synchronizedList(new ArrayList());
		

		public NetworkSystem(MinecraftServer p_i45292_1_)
		{
			this.mcServer = p_i45292_1_;
			this.isAlive = true;
		}

///    
///     <summary> * Adds a channel that listens on publicly accessible network ports </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void addLanEndpoint(InetAddress p_151265_1_, int p_151265_2_) throws IOException
		public virtual void addLanEndpoint(InetAddress p_151265_1_, int p_151265_2_)
		{
			IList var3 = this.endpoints;

			lock (this.endpoints)
			{
				this.endpoints.Add(((ServerBootstrap)((ServerBootstrap)(new ServerBootstrap()).channel(typeof(NioServerSocketChannel))).childHandler(new ChannelInitializer() {  protected void initChannel(Channel p_initChannel_1_) { try { p_initChannel_1_.config().setOption(ChannelOption.IP_TOS, Convert.ToInt32(24)); } catch (ChannelException var4) { ; } try { p_initChannel_1_.config().setOption(ChannelOption.TCP_NODELAY, Convert.ToBoolean(false)); } catch (ChannelException var3) { ; } p_initChannel_1_.pipeline().addLast("timeout", new ReadTimeoutHandler(30)).addLast("legacy_query", new PingResponseHandler(NetworkSystem.this)).addLast("splitter", new MessageDeserializer2()).addLast("decoder", new MessageDeserializer(NetworkManager.field_152462_h)).addLast("prepender", new MessageSerializer2()).addLast("encoder", new MessageSerializer(NetworkManager.field_152462_h)); NetworkManager var2 = new NetworkManager(false); NetworkSystem.networkManagers.add(var2); p_initChannel_1_.pipeline().addLast("packet_handler", var2); var2.setNetHandler(new NetHandlerHandshakeTCP(NetworkSystem.mcServer, var2)); } }).group(eventLoops).localAddress(p_151265_1_, p_151265_2_)).bind().syncUninterruptibly());
			}
		}

///    
///     <summary> * Adds a channel that listens locally </summary>
///     
		public virtual SocketAddress addLocalEndpoint()
		{
			IList var2 = this.endpoints;
			ChannelFuture var1;

			lock (this.endpoints)
			{
				var1 = ((ServerBootstrap)((ServerBootstrap)(new ServerBootstrap()).channel(typeof(LocalServerChannel))).childHandler(new ChannelInitializer() {  protected void initChannel(Channel p_initChannel_1_) { NetworkManager var2 = new NetworkManager(false); var2.setNetHandler(new NetHandlerHandshakeMemory(NetworkSystem.mcServer, var2)); NetworkSystem.networkManagers.add(var2); p_initChannel_1_.pipeline().addLast("packet_handler", var2); } }).group(eventLoops).localAddress(LocalAddress.ANY)).bind().syncUninterruptibly();
				this.endpoints.Add(var1);
			}

			return var1.channel().localAddress();
		}

///    
///     <summary> * Shuts down all open endpoints (with immediate effect?) </summary>
///     
		public virtual void terminateEndpoints()
		{
			this.isAlive = false;
			IEnumerator var1 = this.endpoints.GetEnumerator();

			while (var1.MoveNext())
			{
				ChannelFuture var2 = (ChannelFuture)var1.Current;
				var2.channel().close().syncUninterruptibly();
			}
		}

///    
///     <summary> * Will try to process the packets received by each NetworkManager, gracefully manage processing failures and cleans
///     * up dead connections </summary>
///     
		public virtual void networkTick()
		{
			IList var1 = this.networkManagers;

			lock (this.networkManagers)
			{
				IEnumerator var2 = this.networkManagers.GetEnumerator();

				while (var2.MoveNext())
				{
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final NetworkManager var3 = (NetworkManager)var2.next();
					NetworkManager var3 = (NetworkManager)var2.Current;

					if (!var3.ChannelOpen)
					{
						var2.remove();

						if (var3.ExitMessage != null)
						{
							var3.NetHandler.onDisconnect(var3.ExitMessage);
						}
						else if (var3.NetHandler != null)
						{
							var3.NetHandler.onDisconnect(new ChatComponentText("Disconnected"));
						}
					}
					else
					{
						try
						{
							var3.processReceivedPackets();
						}
						catch (Exception var8)
						{
							if (var3.LocalChannel)
							{
								CrashReport var10 = CrashReport.makeCrashReport(var8, "Ticking memory connection");
								CrashReportCategory var6 = var10.makeCategory("Ticking connection");
								var6.addCrashSectionCallable("Connection", new Callable() {  public string call() { return var3.ToString(); } });
								throw new ReportedException(var10);
							}

							logger.warn("Failed to handle packet for " + var3.SocketAddress, var8);
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final ChatComponentText var5 = new ChatComponentText("Internal server error");
							ChatComponentText var5 = new ChatComponentText("Internal server error");
							var3.scheduleOutboundPacket(new S40PacketDisconnect(var5), new GenericFutureListener[] {new GenericFutureListener() {  public void operationComplete(Future p_operationComplete_1_) { var3.closeChannel(var5); } } });
							var3.disableAutoRead();
						}
					}
				}
			}
		}

		public virtual MinecraftServer func_151267_d()
		{
			return this.mcServer;
		}
	}

}