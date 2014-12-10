using System;

namespace DotCraftCore.Network
{

	using Charsets = com.google.common.base.Charsets;
	using ByteBuf = io.netty.buffer.ByteBuf;
	using Unpooled = io.netty.buffer.Unpooled;
	using ChannelFutureListener = io.netty.channel.ChannelFutureListener;
	using ChannelHandlerContext = io.netty.channel.ChannelHandlerContext;
	using ChannelInboundHandlerAdapter = io.netty.channel.ChannelInboundHandlerAdapter;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class PingResponseHandler : ChannelInboundHandlerAdapter
	{
		private static readonly Logger logger = LogManager.Logger;
		private NetworkSystem field_151257_b;
		private const string __OBFID = "CL_00001444";

		public PingResponseHandler(NetworkSystem p_i45286_1_)
		{
			this.field_151257_b = p_i45286_1_;
		}

		public virtual void channelRead(ChannelHandlerContext p_channelRead_1_, object p_channelRead_2_)
		{
			ByteBuf var3 = (ByteBuf)p_channelRead_2_;
			var3.markReaderIndex();
			bool var4 = true;

			try
			{
				try
				{
					if (var3.readUnsignedByte() != 254)
					{
						return;
					}

					InetSocketAddress var5 = (InetSocketAddress)p_channelRead_1_.channel().remoteAddress();
					MinecraftServer var6 = this.field_151257_b.func_151267_d();
					int var7 = var3.readableBytes();
					string var8;

					switch (var7)
					{
						case 0:
							logger.debug("Ping: (<1.3.x) from {}:{}", new object[] {var5.Address, Convert.ToInt32(var5.Port)});
							var8 = string.Format("{0}\u00a7{1:D}\u00a7{2:D}", new object[] {var6.MOTD, Convert.ToInt32(var6.CurrentPlayerCount), Convert.ToInt32(var6.MaxPlayers)});
							this.func_151256_a(p_channelRead_1_, this.func_151255_a(var8));
							break;

						case 1:
							if (var3.readUnsignedByte() != 1)
							{
								return;
							}

							logger.debug("Ping: (1.4-1.5.x) from {}:{}", new object[] {var5.Address, Convert.ToInt32(var5.Port)});
							var8 = string.Format("\u00a71\u0000{0:D}\u0000{1}\u0000{2}\u0000{3:D}\u0000{4:D}", new object[] {Convert.ToInt32(127), var6.MinecraftVersion, var6.MOTD, Convert.ToInt32(var6.CurrentPlayerCount), Convert.ToInt32(var6.MaxPlayers)});
							this.func_151256_a(p_channelRead_1_, this.func_151255_a(var8));
							break;

						default:
							bool var23 = var3.readUnsignedByte() == 1;
							var23 &= var3.readUnsignedByte() == 250;
							var23 &= "MC|PingHost".Equals(new string(var3.readBytes(var3.readShort() * 2).array(), Charsets.UTF_16BE));
							int var9 = var3.readUnsignedShort();
							var23 &= var3.readUnsignedByte() >= 73;
							var23 &= 3 + var3.readBytes(var3.readShort() * 2).array().length + 4 == var9;
							var23 &= var3.readInt() <= 65535;
							var23 &= var3.readableBytes() == 0;

							if (!var23)
							{
								return;
							}

							logger.debug("Ping: (1.6) from {}:{}", new object[] {var5.Address, Convert.ToInt32(var5.Port)});
							string var10 = string.Format("\u00a71\u0000{0:D}\u0000{1}\u0000{2}\u0000{3:D}\u0000{4:D}", new object[] {Convert.ToInt32(127), var6.MinecraftVersion, var6.MOTD, Convert.ToInt32(var6.CurrentPlayerCount), Convert.ToInt32(var6.MaxPlayers)});
							ByteBuf var11 = this.func_151255_a(var10);

							try
							{
								this.func_151256_a(p_channelRead_1_, var11);
							}
							finally
							{
								var11.release();
							}
						break;
					}

					var3.release();
					var4 = false;
				}
				catch (Exception var21)
				{
					;
				}
			}
			finally
			{
				if (var4)
				{
					var3.resetReaderIndex();
					p_channelRead_1_.channel().pipeline().remove("legacy_query");
					p_channelRead_1_.fireChannelRead(p_channelRead_2_);
				}
			}
		}

		private void func_151256_a(ChannelHandlerContext p_151256_1_, ByteBuf p_151256_2_)
		{
			p_151256_1_.pipeline().firstContext().writeAndFlush(p_151256_2_).addListener(ChannelFutureListener.CLOSE);
		}

		private ByteBuf func_151255_a(string p_151255_1_)
		{
			ByteBuf var2 = Unpooled.buffer();
			var2.writeByte(255);
			char[] var3 = p_151255_1_.ToCharArray();
			var2.writeShort(var3.Length);
			char[] var4 = var3;
			int var5 = var3.Length;

			for (int var6 = 0; var6 < var5; ++var6)
			{
				char var7 = var4[var6];
				var2.writeChar(var7);
			}

			return var2;
		}
	}

}