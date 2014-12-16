namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S28PacketEffect : Packet
	{
		private int field_149251_a;
		private int field_149249_b;
		private int field_149250_c;
		private int field_149247_d;
		private int field_149248_e;
		private bool field_149246_f;
		

		public S28PacketEffect()
		{
		}

		public S28PacketEffect(int p_i45198_1_, int p_i45198_2_, int p_i45198_3_, int p_i45198_4_, int p_i45198_5_, bool p_i45198_6_)
		{
			this.field_149251_a = p_i45198_1_;
			this.field_149250_c = p_i45198_2_;
			this.field_149247_d = p_i45198_3_;
			this.field_149248_e = p_i45198_4_;
			this.field_149249_b = p_i45198_5_;
			this.field_149246_f = p_i45198_6_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149251_a = p_148837_1_.readInt();
			this.field_149250_c = p_148837_1_.readInt();
			this.field_149247_d = p_148837_1_.readByte() & 255;
			this.field_149248_e = p_148837_1_.readInt();
			this.field_149249_b = p_148837_1_.readInt();
			this.field_149246_f = p_148837_1_.readBoolean();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149251_a);
			p_148840_1_.writeInt(this.field_149250_c);
			p_148840_1_.writeByte(this.field_149247_d & 255);
			p_148840_1_.writeInt(this.field_149248_e);
			p_148840_1_.writeInt(this.field_149249_b);
			p_148840_1_.writeBoolean(this.field_149246_f);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleEffect(this);
		}

		public virtual bool func_149244_c()
		{
			return this.field_149246_f;
		}

		public virtual int func_149242_d()
		{
			return this.field_149251_a;
		}

		public virtual int func_149241_e()
		{
			return this.field_149249_b;
		}

		public virtual int func_149240_f()
		{
			return this.field_149250_c;
		}

		public virtual int func_149243_g()
		{
			return this.field_149247_d;
		}

		public virtual int func_149239_h()
		{
			return this.field_149248_e;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}