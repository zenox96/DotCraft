namespace DotCraftCore.Network
{

	using ByteBuf = io.netty.buffer.ByteBuf;
	using ChannelHandlerContext = io.netty.channel.ChannelHandlerContext;
	using MessageToByteEncoder = io.netty.handler.codec.MessageToByteEncoder;
	using Cipher = javax.crypto.Cipher;
	using ShortBufferException = javax.crypto.ShortBufferException;

	public class NettyEncryptingEncoder : MessageToByteEncoder
	{
		private readonly NettyEncryptionTranslator field_150750_a;
		

		public NettyEncryptingEncoder(Cipher p_i45142_1_)
		{
			this.field_150750_a = new NettyEncryptionTranslator(p_i45142_1_);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void encode(ChannelHandlerContext p_encode_1_, ByteBuf p_encode_2_, ByteBuf p_encode_3_) throws ShortBufferException
		protected internal virtual void encode(ChannelHandlerContext p_encode_1_, ByteBuf p_encode_2_, ByteBuf p_encode_3_)
		{
			this.field_150750_a.func_150504_a(p_encode_2_, p_encode_3_);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void encode(ChannelHandlerContext p_encode_1_, Object p_encode_2_, ByteBuf p_encode_3_) throws ShortBufferException
		protected internal virtual void encode(ChannelHandlerContext p_encode_1_, object p_encode_2_, ByteBuf p_encode_3_)
		{
			this.encode(p_encode_1_, (ByteBuf)p_encode_2_, p_encode_3_);
		}
	}

}