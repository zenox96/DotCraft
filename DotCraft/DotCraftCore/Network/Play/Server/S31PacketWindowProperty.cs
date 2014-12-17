namespace DotCraftCore.nNetwork.nPlay.nServer
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.nNetwork.nPlay.INetHandlerPlayClient;

	public class S31PacketWindowProperty : Packet
	{
		private int field_149186_a;
		private int field_149184_b;
		private int field_149185_c;
		

		public S31PacketWindowProperty()
		{
		}

		public S31PacketWindowProperty(int p_i45187_1_, int p_i45187_2_, int p_i45187_3_)
		{
			this.field_149186_a = p_i45187_1_;
			this.field_149184_b = p_i45187_2_;
			this.field_149185_c = p_i45187_3_;
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleWindowProperty(this);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149186_a = p_148837_1_.readUnsignedByte();
			this.field_149184_b = p_148837_1_.readShort();
			this.field_149185_c = p_148837_1_.readShort();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeByte(this.field_149186_a);
			p_148840_1_.writeShort(this.field_149184_b);
			p_148840_1_.writeShort(this.field_149185_c);
		}

		public virtual int func_149182_c()
		{
			return this.field_149186_a;
		}

		public virtual int func_149181_d()
		{
			return this.field_149184_b;
		}

		public virtual int func_149180_e()
		{
			return this.field_149185_c;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}