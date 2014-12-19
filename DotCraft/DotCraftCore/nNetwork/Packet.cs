using System;

namespace DotCraftCore.nNetwork
{

	using BiMap = com.google.common.collect.BiMap;
	using ByteBuf = io.netty.buffer.ByteBuf;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public abstract class Packet
	{
		private static readonly Logger logger = LogManager.Logger;
		

///    
///     <summary> * Returns a packet instance, given the params: BiMap<int, (Packet) Class> and (int) id </summary>
///     
		public static Packet generatePacket(BiMap p_148839_0_, int p_148839_1_)
		{
			try
			{
				Type var2 = (Class)p_148839_0_.get(Convert.ToInt32(p_148839_1_));
				return var2 == null ? null : (Packet)var2.newInstance();
			}
			catch (Exception var3)
			{
				logger.error("Couldn\'t create packet " + p_148839_1_, var3);
				return null;
			}
		}

///    
///     <summary> * Will write a byte array to supplied ByteBuf as a separately defined structure by prefixing the byte array with
///     * its length </summary>
///     
		public static void writeBlob(ByteBuf p_148838_0_, sbyte[] p_148838_1_)
		{
			p_148838_0_.writeShort(p_148838_1_.Length);
			p_148838_0_.writeBytes(p_148838_1_);
		}

///    
///     <summary> * Will read a byte array from the supplied ByteBuf, the first short encountered will be interpreted as the size of
///     * the byte array to read in </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static byte[] readBlob(ByteBuf p_148834_0_) throws IOException
		public static sbyte[] readBlob(ByteBuf p_148834_0_)
		{
			short var1 = p_148834_0_.readShort();

			if (var1 < 0)
			{
				throw new IOException("Key was smaller than nothing!  Weird key!");
			}
			else
			{
				sbyte[] var2 = new sbyte[var1];
				p_148834_0_.readBytes(var2);
				return var2;
			}
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public abstract void readPacketData(PacketBuffer p_148837_1_) throws IOException;
		public abstract void readPacketData(PacketBuffer p_148837_1_);

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public abstract void writePacketData(PacketBuffer p_148840_1_) throws IOException;
		public abstract void writePacketData(PacketBuffer p_148840_1_);

		public abstract void processPacket(INetHandler p_148833_1_);

///    
///     <summary> * If true, the network manager will process the packet immediately when received, otherwise it will queue it for
///     * processing. Currently true for: Disconnect, LoginSuccess, KeepAlive, ServerQuery/Info, Ping/Pong </summary>
///     
		public virtual bool hasPriority()
		{
			return false;
		}

		public override string ToString()
		{
			return this.GetType().SimpleName;
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public virtual string serialize()
		{
			return "";
		}
	}

}