namespace DotCraftCore.Util
{

	using BiMap = com.google.common.collect.BiMap;
	using ByteBuf = io.netty.buffer.ByteBuf;
	using ChannelHandlerContext = io.netty.channel.ChannelHandlerContext;
	using MessageToByteEncoder = io.netty.handler.codec.MessageToByteEncoder;
	using NetworkManager = DotCraftCore.network.NetworkManager;
	using NetworkStatistics = DotCraftCore.network.NetworkStatistics;
	using Packet = DotCraftCore.network.Packet;
	using PacketBuffer = DotCraftCore.network.PacketBuffer;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;
	using Marker = org.apache.logging.log4j.Marker;
	using MarkerManager = org.apache.logging.log4j.MarkerManager;

	public class MessageSerializer : MessageToByteEncoder
	{
		private static readonly Logger logger = LogManager.Logger;
		private static readonly Marker field_150797_b = MarkerManager.getMarker("PACKET_SENT", NetworkManager.logMarkerPackets);
		private readonly NetworkStatistics field_152500_c;
		private const string __OBFID = "CL_00001253";

		public MessageSerializer(NetworkStatistics p_i1182_1_)
		{
			this.field_152500_c = p_i1182_1_;
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void encode(ChannelHandlerContext p_encode_1_, Packet p_encode_2_, ByteBuf p_encode_3_) throws IOException
		protected internal virtual void encode(ChannelHandlerContext p_encode_1_, Packet p_encode_2_, ByteBuf p_encode_3_)
		{
			int? var4 = (int?)((BiMap)p_encode_1_.channel().attr(NetworkManager.attrKeySendable).get()).inverse().get(p_encode_2_.GetType());

			if(logger.DebugEnabled)
			{
				logger.debug(field_150797_b, "OUT: [{}:{}] {}[{}]", new object[] {p_encode_1_.channel().attr(NetworkManager.attrKeyConnectionState).get(), var4, p_encode_2_.GetType().Name, p_encode_2_.serialize()});
			}

			if(var4 == null)
			{
				throw new IOException("Can\'t serialize unregistered packet");
			}
			else
			{
				PacketBuffer var5 = new PacketBuffer(p_encode_3_);
				var5.writeVarIntToBuffer((int)var4);
				p_encode_2_.writePacketData(var5);
				this.field_152500_c.func_152464_b((int)var4, (long)var5.readableBytes());
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void encode(ChannelHandlerContext p_encode_1_, Object p_encode_2_, ByteBuf p_encode_3_) throws IOException
		protected internal virtual void encode(ChannelHandlerContext p_encode_1_, object p_encode_2_, ByteBuf p_encode_3_)
		{
			this.encode(p_encode_1_, (Packet)p_encode_2_, p_encode_3_);
		}
	}

}