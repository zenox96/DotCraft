namespace DotCraftCore.nNetwork.nPlay.nServer
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.nNetwork.nPlay.INetHandlerPlayClient;

	public class S2DPacketOpenWindow : Packet
	{
		private int field_148909_a;
		private int field_148907_b;
		private string field_148908_c;
		private int field_148905_d;
		private bool field_148906_e;
		private int field_148904_f;
		

		public S2DPacketOpenWindow()
		{
		}

		public S2DPacketOpenWindow(int p_i45184_1_, int p_i45184_2_, string p_i45184_3_, int p_i45184_4_, bool p_i45184_5_)
		{
			this.field_148909_a = p_i45184_1_;
			this.field_148907_b = p_i45184_2_;
			this.field_148908_c = p_i45184_3_;
			this.field_148905_d = p_i45184_4_;
			this.field_148906_e = p_i45184_5_;
		}

		public S2DPacketOpenWindow(int p_i45185_1_, int p_i45185_2_, string p_i45185_3_, int p_i45185_4_, bool p_i45185_5_, int p_i45185_6_) : this(p_i45185_1_, p_i45185_2_, p_i45185_3_, p_i45185_4_, p_i45185_5_)
		{
			this.field_148904_f = p_i45185_6_;
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleOpenWindow(this);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_148909_a = p_148837_1_.readUnsignedByte();
			this.field_148907_b = p_148837_1_.readUnsignedByte();
			this.field_148908_c = p_148837_1_.readStringFromBuffer(32);
			this.field_148905_d = p_148837_1_.readUnsignedByte();
			this.field_148906_e = p_148837_1_.readBoolean();

			if (this.field_148907_b == 11)
			{
				this.field_148904_f = p_148837_1_.readInt();
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeByte(this.field_148909_a);
			p_148840_1_.writeByte(this.field_148907_b);
			p_148840_1_.writeStringToBuffer(this.field_148908_c);
			p_148840_1_.writeByte(this.field_148905_d);
			p_148840_1_.writeBoolean(this.field_148906_e);

			if (this.field_148907_b == 11)
			{
				p_148840_1_.writeInt(this.field_148904_f);
			}
		}

		public virtual int func_148901_c()
		{
			return this.field_148909_a;
		}

		public virtual int func_148899_d()
		{
			return this.field_148907_b;
		}

		public virtual string func_148902_e()
		{
			return this.field_148908_c;
		}

		public virtual int func_148898_f()
		{
			return this.field_148905_d;
		}

		public virtual bool func_148900_g()
		{
			return this.field_148906_e;
		}

		public virtual int func_148897_h()
		{
			return this.field_148904_f;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}