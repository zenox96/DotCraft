namespace DotCraftCore.nNetwork.nStatus.nClient
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerStatusServer = DotCraftCore.nNetwork.nStatus.INetHandlerStatusServer;

	public class C01PacketPing : Packet
	{
		private long field_149290_a;
		

		public C01PacketPing()
		{
		}

		public C01PacketPing(long p_i45276_1_)
		{
			this.field_149290_a = p_i45276_1_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149290_a = p_148837_1_.readLong();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeLong(this.field_149290_a);
		}

		public virtual void processPacket(INetHandlerStatusServer p_148833_1_)
		{
			p_148833_1_.processPing(this);
		}

///    
///     <summary> * If true, the network manager will process the packet immediately when received, otherwise it will queue it for
///     * processing. Currently true for: Disconnect, LoginSuccess, KeepAlive, ServerQuery/Info, Ping/Pong </summary>
///     
		public override bool hasPriority()
		{
			return true;
		}

		public virtual long func_149289_c()
		{
			return this.field_149290_a;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerStatusServer)p_148833_1_);
		}
	}

}