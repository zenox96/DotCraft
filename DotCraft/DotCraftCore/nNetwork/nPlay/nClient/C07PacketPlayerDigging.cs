namespace DotCraftCore.nNetwork.nPlay.nClient
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayServer = DotCraftCore.nNetwork.nPlay.INetHandlerPlayServer;

	public class C07PacketPlayerDigging : Packet
	{
		private int field_149511_a;
		private int field_149509_b;
		private int field_149510_c;
		private int field_149507_d;
		private int field_149508_e;
		

		public C07PacketPlayerDigging()
		{
		}

		public C07PacketPlayerDigging(int p_i45258_1_, int p_i45258_2_, int p_i45258_3_, int p_i45258_4_, int p_i45258_5_)
		{
			this.field_149508_e = p_i45258_1_;
			this.field_149511_a = p_i45258_2_;
			this.field_149509_b = p_i45258_3_;
			this.field_149510_c = p_i45258_4_;
			this.field_149507_d = p_i45258_5_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149508_e = p_148837_1_.readUnsignedByte();
			this.field_149511_a = p_148837_1_.readInt();
			this.field_149509_b = p_148837_1_.readUnsignedByte();
			this.field_149510_c = p_148837_1_.readInt();
			this.field_149507_d = p_148837_1_.readUnsignedByte();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeByte(this.field_149508_e);
			p_148840_1_.writeInt(this.field_149511_a);
			p_148840_1_.writeByte(this.field_149509_b);
			p_148840_1_.writeInt(this.field_149510_c);
			p_148840_1_.writeByte(this.field_149507_d);
		}

		public virtual void processPacket(INetHandlerPlayServer p_148833_1_)
		{
			p_148833_1_.processPlayerDigging(this);
		}

		public virtual int func_149505_c()
		{
			return this.field_149511_a;
		}

		public virtual int func_149503_d()
		{
			return this.field_149509_b;
		}

		public virtual int func_149502_e()
		{
			return this.field_149510_c;
		}

		public virtual int func_149501_f()
		{
			return this.field_149507_d;
		}

		public virtual int func_149506_g()
		{
			return this.field_149508_e;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayServer)p_148833_1_);
		}
	}

}