namespace DotCraftCore.nNetwork.nPlay.nServer
{

	using Entity = DotCraftCore.entity.Entity;
	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.nNetwork.nPlay.INetHandlerPlayClient;

	public class S1BPacketEntityAttach : Packet
	{
		private int field_149408_a;
		private int field_149406_b;
		private int field_149407_c;
		

		public S1BPacketEntityAttach()
		{
		}

		public S1BPacketEntityAttach(int p_i45218_1_, Entity p_i45218_2_, Entity p_i45218_3_)
		{
			this.field_149408_a = p_i45218_1_;
			this.field_149406_b = p_i45218_2_.EntityId;
			this.field_149407_c = p_i45218_3_ != null ? p_i45218_3_.EntityId : -1;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149406_b = p_148837_1_.readInt();
			this.field_149407_c = p_148837_1_.readInt();
			this.field_149408_a = p_148837_1_.readUnsignedByte();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149406_b);
			p_148840_1_.writeInt(this.field_149407_c);
			p_148840_1_.writeByte(this.field_149408_a);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleEntityAttach(this);
		}

		public virtual int func_149404_c()
		{
			return this.field_149408_a;
		}

		public virtual int func_149403_d()
		{
			return this.field_149406_b;
		}

		public virtual int func_149402_e()
		{
			return this.field_149407_c;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}