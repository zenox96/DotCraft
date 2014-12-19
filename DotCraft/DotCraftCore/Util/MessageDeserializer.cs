using System;
using System.Collections;

namespace DotCraftCore.nUtil
{

	using BiMap = com.google.common.collect.BiMap;
	using ByteBuf = io.netty.buffer.ByteBuf;
	using ChannelHandlerContext = io.netty.channel.ChannelHandlerContext;
	using ByteToMessageDecoder = io.netty.handler.codec.ByteToMessageDecoder;
	using NetworkManager = DotCraftCore.network.NetworkManager;
	using NetworkStatistics = DotCraftCore.network.NetworkStatistics;
	using Packet = DotCraftCore.network.Packet;
	using PacketBuffer = DotCraftCore.network.PacketBuffer;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;
	using Marker = org.apache.logging.log4j.Marker;
	using MarkerManager = org.apache.logging.log4j.MarkerManager;

	public class MessageDeserializer : ByteToMessageDecoder
	{
		private static readonly Logger logger = LogManager.Logger;
		private static readonly Marker field_150799_b = MarkerManager.getMarker("PACKET_RECEIVED", NetworkManager.logMarkerPackets);
		private readonly NetworkStatistics field_152499_c;
		

		public MessageDeserializer(NetworkStatistics p_i1183_1_)
		{
			this.field_152499_c = p_i1183_1_;
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void decode(ChannelHandlerContext p_decode_1_, ByteBuf p_decode_2_, List p_decode_3_) throws IOException
		protected internal virtual void decode(ChannelHandlerContext p_decode_1_, ByteBuf p_decode_2_, IList p_decode_3_)
		{
			int var4 = p_decode_2_.readableBytes();

			if(var4 != 0)
			{
				PacketBuffer var5 = new PacketBuffer(p_decode_2_);
				int var6 = var5.readVarIntFromBuffer();
				Packet var7 = Packet.generatePacket((BiMap)p_decode_1_.channel().attr(NetworkManager.attrKeyReceivable).get(), var6);

				if(var7 == null)
				{
					throw new IOException("Bad packet id " + var6);
				}
				else
				{
					var7.readPacketData(var5);

					if(var5.readableBytes() > 0)
					{
						throw new IOException("Packet was larger than I expected, found " + var5.readableBytes() + " bytes extra whilst reading packet " + var6);
					}
					else
					{
						p_decode_3_.Add(var7);
						this.field_152499_c.func_152469_a(var6, (long)var4);

						if(logger.DebugEnabled)
						{
							logger.debug(field_150799_b, " IN: [{}:{}] {}[{}]", new object[] {p_decode_1_.channel().attr(NetworkManager.attrKeyConnectionState).get(), Convert.ToInt32(var6), var7.GetType().Name, var7.serialize()});
						}
					}
				}
			}
		}
	}

}