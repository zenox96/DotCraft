namespace DotCraftCore.nNetwork.nPlay.nClient
{

	using Entity = DotCraftCore.entity.Entity;
	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayServer = DotCraftCore.nNetwork.nPlay.INetHandlerPlayServer;

	public class C0BPacketEntityAction : Packet
	{
		private int field_149517_a;
		private int field_149515_b;
		private int field_149516_c;
		

		public C0BPacketEntityAction()
		{
		}

		public C0BPacketEntityAction(Entity p_i45259_1_, int p_i45259_2_) : this(p_i45259_1_, p_i45259_2_, 0)
		{
		}

		public C0BPacketEntityAction(Entity p_i45260_1_, int p_i45260_2_, int p_i45260_3_)
		{
			this.field_149517_a = p_i45260_1_.EntityId;
			this.field_149515_b = p_i45260_2_;
			this.field_149516_c = p_i45260_3_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149517_a = p_148837_1_.readInt();
			this.field_149515_b = p_148837_1_.readByte();
			this.field_149516_c = p_148837_1_.readInt();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149517_a);
			p_148840_1_.writeByte(this.field_149515_b);
			p_148840_1_.writeInt(this.field_149516_c);
		}

		public virtual void processPacket(INetHandlerPlayServer p_148833_1_)
		{
			p_148833_1_.processEntityAction(this);
		}

		public virtual int func_149513_d()
		{
			return this.field_149515_b;
		}

		public virtual int func_149512_e()
		{
			return this.field_149516_c;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayServer)p_148833_1_);
		}
	}

}