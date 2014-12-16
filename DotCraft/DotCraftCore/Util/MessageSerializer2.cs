namespace DotCraftCore.Util
{

	using ByteBuf = io.netty.buffer.ByteBuf;
	using ChannelHandlerContext = io.netty.channel.ChannelHandlerContext;
	using MessageToByteEncoder = io.netty.handler.codec.MessageToByteEncoder;
	using PacketBuffer = DotCraftCore.network.PacketBuffer;

	public class MessageSerializer2 : MessageToByteEncoder
	{
		

		protected internal virtual void encode(ChannelHandlerContext p_encode_1_, ByteBuf p_encode_2_, ByteBuf p_encode_3_)
		{
			int var4 = p_encode_2_.readableBytes();
			int var5 = PacketBuffer.getVarIntSize(var4);

			if(var5 > 3)
			{
				throw new System.ArgumentException("unable to fit " + var4 + " into " + 3);
			}
			else
			{
				PacketBuffer var6 = new PacketBuffer(p_encode_3_);
				var6.ensureWritable(var5 + var4);
				var6.writeVarIntToBuffer(var4);
				var6.writeBytes(p_encode_2_, p_encode_2_.readerIndex(), var4);
			}
		}

		protected internal virtual void encode(ChannelHandlerContext p_encode_1_, object p_encode_2_, ByteBuf p_encode_3_)
		{
			this.encode(p_encode_1_, (ByteBuf)p_encode_2_, p_encode_3_);
		}
	}

}