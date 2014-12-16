using System.Collections;

namespace DotCraftCore.Network
{

	using ByteBuf = io.netty.buffer.ByteBuf;
	using ChannelHandlerContext = io.netty.channel.ChannelHandlerContext;
	using MessageToMessageDecoder = io.netty.handler.codec.MessageToMessageDecoder;
	using Cipher = javax.crypto.Cipher;
	using ShortBufferException = javax.crypto.ShortBufferException;

	public class NettyEncryptingDecoder : MessageToMessageDecoder
	{
		private readonly NettyEncryptionTranslator field_150509_a;
		

		public NettyEncryptingDecoder(Cipher p_i45141_1_)
		{
			this.field_150509_a = new NettyEncryptionTranslator(p_i45141_1_);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void decode(ChannelHandlerContext p_decode_1_, ByteBuf p_decode_2_, List p_decode_3_) throws ShortBufferException
		protected internal virtual void decode(ChannelHandlerContext p_decode_1_, ByteBuf p_decode_2_, IList p_decode_3_)
		{
			p_decode_3_.Add(this.field_150509_a.func_150503_a(p_decode_1_, p_decode_2_));
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void decode(ChannelHandlerContext p_decode_1_, Object p_decode_2_, List p_decode_3_) throws ShortBufferException
		protected internal virtual void decode(ChannelHandlerContext p_decode_1_, object p_decode_2_, IList p_decode_3_)
		{
			this.decode(p_decode_1_, (ByteBuf)p_decode_2_, p_decode_3_);
		}
	}

}