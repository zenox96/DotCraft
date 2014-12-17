namespace DotCraftCore.nNetwork.nPlay.nServer
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.nNetwork.nPlay.INetHandlerPlayClient;

	public class S33PacketUpdateSign : Packet
	{
		private int field_149352_a;
		private int field_149350_b;
		private int field_149351_c;
		private string[] field_149349_d;
		

		public S33PacketUpdateSign()
		{
		}

		public S33PacketUpdateSign(int p_i45231_1_, int p_i45231_2_, int p_i45231_3_, string[] p_i45231_4_)
		{
			this.field_149352_a = p_i45231_1_;
			this.field_149350_b = p_i45231_2_;
			this.field_149351_c = p_i45231_3_;
			this.field_149349_d = new string[] {p_i45231_4_[0], p_i45231_4_[1], p_i45231_4_[2], p_i45231_4_[3]};
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149352_a = p_148837_1_.readInt();
			this.field_149350_b = p_148837_1_.readShort();
			this.field_149351_c = p_148837_1_.readInt();
			this.field_149349_d = new string[4];

			for (int var2 = 0; var2 < 4; ++var2)
			{
				this.field_149349_d[var2] = p_148837_1_.readStringFromBuffer(15);
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149352_a);
			p_148840_1_.writeShort(this.field_149350_b);
			p_148840_1_.writeInt(this.field_149351_c);

			for (int var2 = 0; var2 < 4; ++var2)
			{
				p_148840_1_.writeStringToBuffer(this.field_149349_d[var2]);
			}
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleUpdateSign(this);
		}

		public virtual int func_149346_c()
		{
			return this.field_149352_a;
		}

		public virtual int func_149345_d()
		{
			return this.field_149350_b;
		}

		public virtual int func_149344_e()
		{
			return this.field_149351_c;
		}

		public virtual string[] func_149347_f()
		{
			return this.field_149349_d;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}