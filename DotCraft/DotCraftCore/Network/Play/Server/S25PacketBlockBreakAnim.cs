namespace DotCraftCore.nNetwork.nPlay.nServer
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.nNetwork.nPlay.INetHandlerPlayClient;

	public class S25PacketBlockBreakAnim : Packet
	{
		private int field_148852_a;
		private int field_148850_b;
		private int field_148851_c;
		private int field_148848_d;
		private int field_148849_e;
		

		public S25PacketBlockBreakAnim()
		{
		}

		public S25PacketBlockBreakAnim(int p_i45174_1_, int p_i45174_2_, int p_i45174_3_, int p_i45174_4_, int p_i45174_5_)
		{
			this.field_148852_a = p_i45174_1_;
			this.field_148850_b = p_i45174_2_;
			this.field_148851_c = p_i45174_3_;
			this.field_148848_d = p_i45174_4_;
			this.field_148849_e = p_i45174_5_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_148852_a = p_148837_1_.readVarIntFromBuffer();
			this.field_148850_b = p_148837_1_.readInt();
			this.field_148851_c = p_148837_1_.readInt();
			this.field_148848_d = p_148837_1_.readInt();
			this.field_148849_e = p_148837_1_.readUnsignedByte();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeVarIntToBuffer(this.field_148852_a);
			p_148840_1_.writeInt(this.field_148850_b);
			p_148840_1_.writeInt(this.field_148851_c);
			p_148840_1_.writeInt(this.field_148848_d);
			p_148840_1_.writeByte(this.field_148849_e);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleBlockBreakAnim(this);
		}

		public virtual int func_148845_c()
		{
			return this.field_148852_a;
		}

		public virtual int func_148844_d()
		{
			return this.field_148850_b;
		}

		public virtual int func_148843_e()
		{
			return this.field_148851_c;
		}

		public virtual int func_148842_f()
		{
			return this.field_148848_d;
		}

		public virtual int func_148846_g()
		{
			return this.field_148849_e;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}