namespace DotCraftCore.Network.Handshake.Client
{

	using EnumConnectionState = DotCraftCore.Network.EnumConnectionState;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerHandshakeServer = DotCraftCore.Network.Handshake.INetHandlerHandshakeServer;

	public class C00Handshake : Packet
	{
		private int field_149600_a;
		private string field_149598_b;
		private int field_149599_c;
		private EnumConnectionState field_149597_d;
		

		public C00Handshake()
		{
		}

		public C00Handshake(int p_i45266_1_, string p_i45266_2_, int p_i45266_3_, EnumConnectionState p_i45266_4_)
		{
			this.field_149600_a = p_i45266_1_;
			this.field_149598_b = p_i45266_2_;
			this.field_149599_c = p_i45266_3_;
			this.field_149597_d = p_i45266_4_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149600_a = p_148837_1_.readVarIntFromBuffer();
			this.field_149598_b = p_148837_1_.readStringFromBuffer(255);
			this.field_149599_c = p_148837_1_.readUnsignedShort();
			this.field_149597_d = EnumConnectionState.func_150760_a(p_148837_1_.readVarIntFromBuffer());
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeVarIntToBuffer(this.field_149600_a);
			p_148840_1_.writeStringToBuffer(this.field_149598_b);
			p_148840_1_.writeShort(this.field_149599_c);
			p_148840_1_.writeVarIntToBuffer(this.field_149597_d.func_150759_c());
		}

		public virtual void processPacket(INetHandlerHandshakeServer p_148833_1_)
		{
			p_148833_1_.processHandshake(this);
		}

///    
///     <summary> * If true, the network manager will process the packet immediately when received, otherwise it will queue it for
///     * processing. Currently true for: Disconnect, LoginSuccess, KeepAlive, ServerQuery/Info, Ping/Pong </summary>
///     
		public override bool hasPriority()
		{
			return true;
		}

		public virtual EnumConnectionState func_149594_c()
		{
			return this.field_149597_d;
		}

		public virtual int func_149595_d()
		{
			return this.field_149600_a;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerHandshakeServer)p_148833_1_);
		}
	}

}