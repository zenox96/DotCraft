using System;

namespace DotCraftCore.Network
{

	using Queues = com.google.common.collect.Queues;
	using ThreadFactoryBuilder = com.google.common.util.concurrent.ThreadFactoryBuilder;
	using Bootstrap = io.netty.bootstrap.Bootstrap;
	using Channel = io.netty.channel.Channel;
	using ChannelException = io.netty.channel.ChannelException;
	using ChannelFutureListener = io.netty.channel.ChannelFutureListener;
	using ChannelHandlerContext = io.netty.channel.ChannelHandlerContext;
	using ChannelInitializer = io.netty.channel.ChannelInitializer;
	using ChannelOption = io.netty.channel.ChannelOption;
	using SimpleChannelInboundHandler = io.netty.channel.SimpleChannelInboundHandler;
	using LocalChannel = io.netty.channel.local.LocalChannel;
	using LocalServerChannel = io.netty.channel.local.LocalServerChannel;
	using NioEventLoopGroup = io.netty.channel.nio.NioEventLoopGroup;
	using NioSocketChannel = io.netty.channel.socket.nio.NioSocketChannel;
	using ReadTimeoutHandler = io.netty.handler.timeout.ReadTimeoutHandler;
	using TimeoutException = io.netty.handler.timeout.TimeoutException;
	using AttributeKey = io.netty.util.AttributeKey;
	using GenericFutureListener = io.netty.util.concurrent.GenericFutureListener;
	using SecretKey = javax.crypto.SecretKey;
	using ChatComponentTranslation = DotCraftCore.Util.ChatComponentTranslation;
	using CryptManager = DotCraftCore.Util.CryptManager;
	using IChatComponent = DotCraftCore.Util.IChatComponent;
	using MessageDeserializer = DotCraftCore.Util.MessageDeserializer;
	using MessageDeserializer2 = DotCraftCore.Util.MessageDeserializer2;
	using MessageSerializer = DotCraftCore.Util.MessageSerializer;
	using MessageSerializer2 = DotCraftCore.Util.MessageSerializer2;
	using Validate = org.apache.commons.lang3.Validate;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;
	using Marker = org.apache.logging.log4j.Marker;
	using MarkerManager = org.apache.logging.log4j.MarkerManager;

	public class NetworkManager : SimpleChannelInboundHandler
	{
		private static readonly Logger logger = LogManager.Logger;
		public static readonly Marker logMarkerNetwork = MarkerManager.getMarker("NETWORK");
		public static readonly Marker logMarkerPackets = MarkerManager.getMarker("NETWORK_PACKETS", logMarkerNetwork);
		public static readonly Marker field_152461_c = MarkerManager.getMarker("NETWORK_STAT", logMarkerNetwork);
		public static readonly AttributeKey attrKeyConnectionState = new AttributeKey("protocol");
		public static readonly AttributeKey attrKeyReceivable = new AttributeKey("receivable_packets");
		public static readonly AttributeKey attrKeySendable = new AttributeKey("sendable_packets");
		public static readonly NioEventLoopGroup eventLoops = new NioEventLoopGroup(0, (new ThreadFactoryBuilder()).setNameFormat("Netty Client IO #%d").setDaemon(true).build());
		public static readonly NetworkStatistics field_152462_h = new NetworkStatistics();

///    
///     <summary> * Whether this NetworkManager deals with the client or server side of the connection </summary>
///     
		private readonly bool isClientSide;

///    
///     <summary> * The queue for received, unprioritized packets that will be processed at the earliest opportunity </summary>
///     
		private readonly LinkedList receivedPacketsQueue = Queues.newConcurrentLinkedQueue();

	/// <summary> The queue for packets that require transmission  </summary>
		private readonly LinkedList outboundPacketsQueue = Queues.newConcurrentLinkedQueue();

	/// <summary> The active channel  </summary>
		private Channel channel;

	/// <summary> The address of the remote party  </summary>
		private SocketAddress socketAddress;

	/// <summary> The INetHandler instance responsible for processing received packets  </summary>
		private INetHandler netHandler;

///    
///     <summary> * The current connection state, being one of: HANDSHAKING, PLAY, STATUS, LOGIN </summary>
///     
		private EnumConnectionState connectionState;

	/// <summary> A String indicating why the network has shutdown.  </summary>
		private IChatComponent terminationReason;
		private bool field_152463_r;
		

		public NetworkManager(bool p_i45147_1_)
		{
			this.isClientSide = p_i45147_1_;
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void channelActive(ChannelHandlerContext p_channelActive_1_) throws Exception
		public virtual void channelActive(ChannelHandlerContext p_channelActive_1_)
		{
			base.channelActive(p_channelActive_1_);
			this.channel = p_channelActive_1_.channel();
			this.socketAddress = this.channel.remoteAddress();
			this.ConnectionState = EnumConnectionState.HANDSHAKING;
		}

///    
///     <summary> * Sets the new connection state and registers which packets this channel may send and receive </summary>
///     
		public virtual EnumConnectionState ConnectionState
		{
			set
			{
				this.connectionState = (EnumConnectionState)this.channel.attr(attrKeyConnectionState).getAndSet(value);
				this.channel.attr(attrKeyReceivable).set(value.func_150757_a(this.isClientSide));
				this.channel.attr(attrKeySendable).set(value.func_150754_b(this.isClientSide));
				this.channel.config().AutoRead = true;
				logger.debug("Enabled auto read");
			}
		}

		public virtual void channelInactive(ChannelHandlerContext p_channelInactive_1_)
		{
			this.closeChannel(new ChatComponentTranslation("disconnect.endOfStream", new object[0]));
		}

		public virtual void exceptionCaught(ChannelHandlerContext p_exceptionCaught_1_, Exception p_exceptionCaught_2_)
		{
			ChatComponentTranslation var3;

			if (p_exceptionCaught_2_ is TimeoutException)
			{
				var3 = new ChatComponentTranslation("disconnect.timeout", new object[0]);
			}
			else
			{
				var3 = new ChatComponentTranslation("disconnect.genericReason", new object[] {"Internal Exception: " + p_exceptionCaught_2_});
			}

			this.closeChannel(var3);
		}

		protected internal virtual void channelRead0(ChannelHandlerContext p_channelRead0_1_, Packet p_channelRead0_2_)
		{
			if (this.channel.Open)
			{
				if (p_channelRead0_2_.hasPriority())
				{
					p_channelRead0_2_.processPacket(this.netHandler);
				}
				else
				{
					this.receivedPacketsQueue.AddLast(p_channelRead0_2_);
				}
			}
		}

///    
///     <summary> * Sets the NetHandler for this NetworkManager, no checks are made if this handler is suitable for the particular
///     * connection state (protocol) </summary>
///     
		public virtual INetHandler NetHandler
		{
			set
			{
				Validate.notNull(value, "packetListener", new object[0]);
				logger.debug("Set listener of {} to {}", new object[] {this, value});
				this.netHandler = value;
			}
			get
			{
				return this.netHandler;
			}
		}

///    
///     <summary> * Will flush the outbound queue and dispatch the supplied Packet if the channel is ready, otherwise it adds the
///     * packet to the outbound queue and registers the GenericFutureListener to fire after transmission </summary>
///     
		public virtual void scheduleOutboundPacket(Packet p_150725_1_, params GenericFutureListener[] p_150725_2_)
		{
			if (this.channel != null && this.channel.Open)
			{
				this.flushOutboundQueue();
				this.dispatchPacket(p_150725_1_, p_150725_2_);
			}
			else
			{
				this.outboundPacketsQueue.AddLast(new NetworkManager.InboundHandlerTuplePacketListener(p_150725_1_, p_150725_2_));
			}
		}

///    
///     <summary> * Will commit the packet to the channel. If the current thread 'owns' the channel it will write and flush the
///     * packet, otherwise it will add a task for the channel eventloop thread to do that. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: 'final' parameters are not allowed in .NET:
//ORIGINAL LINE: private void dispatchPacket(final Packet p_150732_1_, final GenericFutureListener[] p_150732_2_)
		private void dispatchPacket(Packet p_150732_1_, GenericFutureListener[] p_150732_2_)
		{
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final EnumConnectionState var3 = EnumConnectionState.func_150752_a(p_150732_1_);
			EnumConnectionState var3 = EnumConnectionState.func_150752_a(p_150732_1_);
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final EnumConnectionState var4 = (EnumConnectionState)this.channel.attr(attrKeyConnectionState).get();
			EnumConnectionState var4 = (EnumConnectionState)this.channel.attr(attrKeyConnectionState).get();

			if (var4 != var3)
			{
				logger.debug("Disabled auto read");
				this.channel.config().AutoRead = false;
			}

			if (this.channel.eventLoop().inEventLoop())
			{
				if (var3 != var4)
				{
					this.ConnectionState = var3;
				}

				this.channel.writeAndFlush(p_150732_1_).addListeners(p_150732_2_).addListener(ChannelFutureListener.FIRE_EXCEPTION_ON_FAILURE);
			}
			else
			{
				this.channel.eventLoop().execute(new Runnable() {  public void run() { if (var3 != var4) { NetworkManager.setConnectionState(var3); } NetworkManager.channel.writeAndFlush(p_150732_1_).addListeners(p_150732_2_).addListener(ChannelFutureListener.FIRE_EXCEPTION_ON_FAILURE); } });
			}
		}

///    
///     <summary> * Will iterate through the outboundPacketQueue and dispatch all Packets </summary>
///     
		private void flushOutboundQueue()
		{
			if (this.channel != null && this.channel.Open)
			{
				while (!this.outboundPacketsQueue.Count == 0)
				{
					NetworkManager.InboundHandlerTuplePacketListener var1 = (NetworkManager.InboundHandlerTuplePacketListener)this.outboundPacketsQueue.poll();
					this.dispatchPacket(var1.field_150774_a, var1.field_150773_b);
				}
			}
		}

///    
///     <summary> * Checks timeouts and processes all packets received </summary>
///     
		public virtual void processReceivedPackets()
		{
			this.flushOutboundQueue();
			EnumConnectionState var1 = (EnumConnectionState)this.channel.attr(attrKeyConnectionState).get();

			if (this.connectionState != var1)
			{
				if (this.connectionState != null)
				{
					this.netHandler.onConnectionStateTransition(this.connectionState, var1);
				}

				this.connectionState = var1;
			}

			if (this.netHandler != null)
			{
				for (int var2 = 1000; !this.receivedPacketsQueue.Count == 0 && var2 >= 0; --var2)
				{
					Packet var3 = (Packet)this.receivedPacketsQueue.poll();
					var3.processPacket(this.netHandler);
				}

				this.netHandler.onNetworkTick();
			}

			this.channel.flush();
		}

///    
///     <summary> * Return the InetSocketAddress of the remote endpoint </summary>
///     
		public virtual SocketAddress SocketAddress
		{
			get
			{
				return this.socketAddress;
			}
		}

///    
///     <summary> * Closes the channel, the parameter can be used for an exit message (not certain how it gets sent) </summary>
///     
		public virtual void closeChannel(IChatComponent p_150718_1_)
		{
			if (this.channel.Open)
			{
				this.channel.close();
				this.terminationReason = p_150718_1_;
			}
		}

///    
///     <summary> * True if this NetworkManager uses a memory connection (single player game). False may imply both an active TCP
///     * connection or simply no active connection at all </summary>
///     
		public virtual bool isLocalChannel()
		{
			get
			{
				return this.channel is LocalChannel || this.channel is LocalServerChannel;
			}
		}

///    
///     <summary> * Prepares a clientside NetworkManager: establishes a connection to the address and port supplied and configures
///     * the channel pipeline. Returns the newly created instance. </summary>
///     
		public static NetworkManager provideLanClient(InetAddress p_150726_0_, int p_150726_1_)
		{
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final NetworkManager var2 = new NetworkManager(true);
			NetworkManager var2 = new NetworkManager(true);
			((Bootstrap)((Bootstrap)((Bootstrap)(new Bootstrap()).group(eventLoops)).handler(new ChannelInitializer() {  protected void initChannel(Channel p_initChannel_1_) { try { p_initChannel_1_.config().setOption(ChannelOption.IP_TOS, Convert.ToInt32(24)); } catch (ChannelException var4) { ; } try { p_initChannel_1_.config().setOption(ChannelOption.TCP_NODELAY, Convert.ToBoolean(false)); } catch (ChannelException var3) { ; } p_initChannel_1_.pipeline().addLast("timeout", new ReadTimeoutHandler(20)).addLast("splitter", new MessageDeserializer2()).addLast("decoder", new MessageDeserializer(NetworkManager.field_152462_h)).addLast("prepender", new MessageSerializer2()).addLast("encoder", new MessageSerializer(NetworkManager.field_152462_h)).addLast("packet_handler", var2); } })).channel(typeof(NioSocketChannel))).connect(p_150726_0_, p_150726_1_).syncUninterruptibly();
			return var2;
		}

///    
///     <summary> * Prepares a clientside NetworkManager: establishes a connection to the socket supplied and configures the channel
///     * pipeline. Returns the newly created instance. </summary>
///     
		public static NetworkManager provideLocalClient(SocketAddress p_150722_0_)
		{
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final NetworkManager var1 = new NetworkManager(true);
			NetworkManager var1 = new NetworkManager(true);
			((Bootstrap)((Bootstrap)((Bootstrap)(new Bootstrap()).group(eventLoops)).handler(new ChannelInitializer() {  protected void initChannel(Channel p_initChannel_1_) { p_initChannel_1_.pipeline().addLast("packet_handler", var1); } })).channel(typeof(LocalChannel))).connect(p_150722_0_).syncUninterruptibly();
			return var1;
		}

///    
///     <summary> * Adds an encoder+decoder to the channel pipeline. The parameter is the secret key used for encrypted communication </summary>
///     
		public virtual void enableEncryption(SecretKey p_150727_1_)
		{
			this.channel.pipeline().addBefore("splitter", "decrypt", new NettyEncryptingDecoder(CryptManager.func_151229_a(2, p_150727_1_)));
			this.channel.pipeline().addBefore("prepender", "encrypt", new NettyEncryptingEncoder(CryptManager.func_151229_a(1, p_150727_1_)));
			this.field_152463_r = true;
		}

///    
///     <summary> * Returns true if this NetworkManager has an active channel, false otherwise </summary>
///     
		public virtual bool isChannelOpen()
		{
			get
			{
				return this.channel != null && this.channel.Open;
			}
		}

///    
///     <summary> * Gets the current handler for processing packets </summary>
///     

///    
///     <summary> * If this channel is closed, returns the exit message, null otherwise. </summary>
///     
		public virtual IChatComponent ExitMessage
		{
			get
			{
				return this.terminationReason;
			}
		}

///    
///     <summary> * Switches the channel to manual reading modus </summary>
///     
		public virtual void disableAutoRead()
		{
			this.channel.config().AutoRead = false;
		}

		protected internal virtual void channelRead0(ChannelHandlerContext p_channelRead0_1_, object p_channelRead0_2_)
		{
			this.channelRead0(p_channelRead0_1_, (Packet)p_channelRead0_2_);
		}

		internal class InboundHandlerTuplePacketListener
		{
			private readonly Packet field_150774_a;
			private readonly GenericFutureListener[] field_150773_b;
			

			public InboundHandlerTuplePacketListener(Packet p_i45146_1_, params GenericFutureListener[] p_i45146_2_)
			{
				this.field_150774_a = p_i45146_1_;
				this.field_150773_b = p_i45146_2_;
			}
		}
	}

}