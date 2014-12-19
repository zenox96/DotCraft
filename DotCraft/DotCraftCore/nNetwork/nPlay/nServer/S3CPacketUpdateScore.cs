namespace DotCraftCore.nNetwork.nPlay.nServer
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.nNetwork.nPlay.INetHandlerPlayClient;
	using Score = DotCraftCore.nScoreboard.Score;

	public class S3CPacketUpdateScore : Packet
	{
		private string field_149329_a = "";
		private string field_149327_b = "";
		private int field_149328_c;
		private int field_149326_d;
		

		public S3CPacketUpdateScore()
		{
		}

		public S3CPacketUpdateScore(Score p_i45227_1_, int p_i45227_2_)
		{
			this.field_149329_a = p_i45227_1_.PlayerName;
			this.field_149327_b = p_i45227_1_.func_96645_d().Name;
			this.field_149328_c = p_i45227_1_.ScorePoints;
			this.field_149326_d = p_i45227_2_;
		}

		public S3CPacketUpdateScore(string p_i45228_1_)
		{
			this.field_149329_a = p_i45228_1_;
			this.field_149327_b = "";
			this.field_149328_c = 0;
			this.field_149326_d = 1;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149329_a = p_148837_1_.readStringFromBuffer(16);
			this.field_149326_d = p_148837_1_.readByte();

			if (this.field_149326_d != 1)
			{
				this.field_149327_b = p_148837_1_.readStringFromBuffer(16);
				this.field_149328_c = p_148837_1_.readInt();
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeStringToBuffer(this.field_149329_a);
			p_148840_1_.writeByte(this.field_149326_d);

			if (this.field_149326_d != 1)
			{
				p_148840_1_.writeStringToBuffer(this.field_149327_b);
				p_148840_1_.writeInt(this.field_149328_c);
			}
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleUpdateScore(this);
		}

		public virtual string func_149324_c()
		{
			return this.field_149329_a;
		}

		public virtual string func_149321_d()
		{
			return this.field_149327_b;
		}

		public virtual int func_149323_e()
		{
			return this.field_149328_c;
		}

		public virtual int func_149322_f()
		{
			return this.field_149326_d;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}