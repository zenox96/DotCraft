namespace DotCraftCore.nNetwork
{

	using ByteBuf = io.netty.buffer.ByteBuf;
	using ChannelHandlerContext = io.netty.channel.ChannelHandlerContext;
	using Cipher = javax.crypto.Cipher;
	using ShortBufferException = javax.crypto.ShortBufferException;

	public class NettyEncryptionTranslator
	{
		private readonly Cipher field_150507_a;
		private sbyte[] field_150505_b = new sbyte[0];
		private sbyte[] field_150506_c = new sbyte[0];
		

		protected internal NettyEncryptionTranslator(Cipher p_i45140_1_)
		{
			this.field_150507_a = p_i45140_1_;
		}

		private sbyte[] func_150502_a(ByteBuf p_150502_1_)
		{
			int var2 = p_150502_1_.readableBytes();

			if (this.field_150505_b.Length < var2)
			{
				this.field_150505_b = new sbyte[var2];
			}

			p_150502_1_.readBytes(this.field_150505_b, 0, var2);
			return this.field_150505_b;
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected ByteBuf func_150503_a(ChannelHandlerContext p_150503_1_, ByteBuf p_150503_2_) throws ShortBufferException
		protected internal virtual ByteBuf func_150503_a(ChannelHandlerContext p_150503_1_, ByteBuf p_150503_2_)
		{
			int var3 = p_150503_2_.readableBytes();
			sbyte[] var4 = this.func_150502_a(p_150503_2_);
			ByteBuf var5 = p_150503_1_.alloc().heapBuffer(this.field_150507_a.getOutputSize(var3));
			var5.writerIndex(this.field_150507_a.update(var4, 0, var3, var5.array(), var5.arrayOffset()));
			return var5;
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void func_150504_a(ByteBuf p_150504_1_, ByteBuf p_150504_2_) throws ShortBufferException
		protected internal virtual void func_150504_a(ByteBuf p_150504_1_, ByteBuf p_150504_2_)
		{
			int var3 = p_150504_1_.readableBytes();
			sbyte[] var4 = this.func_150502_a(p_150504_1_);
			int var5 = this.field_150507_a.getOutputSize(var3);

			if (this.field_150506_c.Length < var5)
			{
				this.field_150506_c = new sbyte[var5];
			}

			p_150504_2_.writeBytes(this.field_150506_c, 0, this.field_150507_a.update(var4, 0, var3, this.field_150506_c));
		}
	}

}