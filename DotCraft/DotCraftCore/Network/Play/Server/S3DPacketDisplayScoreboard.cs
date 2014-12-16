namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using ScoreObjective = DotCraftCore.Scoreboard.ScoreObjective;

	public class S3DPacketDisplayScoreboard : Packet
	{
		private int field_149374_a;
		private string field_149373_b;
		

		public S3DPacketDisplayScoreboard()
		{
		}

		public S3DPacketDisplayScoreboard(int p_i45216_1_, ScoreObjective p_i45216_2_)
		{
			this.field_149374_a = p_i45216_1_;

			if (p_i45216_2_ == null)
			{
				this.field_149373_b = "";
			}
			else
			{
				this.field_149373_b = p_i45216_2_.Name;
			}
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149374_a = p_148837_1_.readByte();
			this.field_149373_b = p_148837_1_.readStringFromBuffer(16);
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeByte(this.field_149374_a);
			p_148840_1_.writeStringToBuffer(this.field_149373_b);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleDisplayScoreboard(this);
		}

		public virtual int func_149371_c()
		{
			return this.field_149374_a;
		}

		public virtual string func_149370_d()
		{
			return this.field_149373_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}