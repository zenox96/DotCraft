using System.Collections;

namespace DotCraftCore.Util
{

	using ByteBuf = io.netty.buffer.ByteBuf;
	using Unpooled = io.netty.buffer.Unpooled;
	using ChannelHandlerContext = io.netty.channel.ChannelHandlerContext;
	using ByteToMessageDecoder = io.netty.handler.codec.ByteToMessageDecoder;
	using CorruptedFrameException = io.netty.handler.codec.CorruptedFrameException;
	using PacketBuffer = DotCraftCore.network.PacketBuffer;

	public class MessageDeserializer2 : ByteToMessageDecoder
	{
		

		protected internal virtual void decode(ChannelHandlerContext p_decode_1_, ByteBuf p_decode_2_, IList p_decode_3_)
		{
			p_decode_2_.markReaderIndex();
			sbyte[] var4 = new sbyte[3];

			for(int var5 = 0; var5 < var4.Length; ++var5)
			{
				if(!p_decode_2_.Readable)
				{
					p_decode_2_.resetReaderIndex();
					return;
				}

				var4[var5] = p_decode_2_.readByte();

				if(var4[var5] >= 0)
				{
					PacketBuffer var6 = new PacketBuffer(Unpooled.wrappedBuffer(var4));

					try
					{
						int var7 = var6.readVarIntFromBuffer();

						if(p_decode_2_.readableBytes() < var7)
						{
							p_decode_2_.resetReaderIndex();
							return;
						}

						p_decode_3_.Add(p_decode_2_.readBytes(var7));
					}
					finally
					{
						var6.release();
					}

					return;
				}
			}

			throw new CorruptedFrameException("length wider than 21-bit");
		}
	}

}