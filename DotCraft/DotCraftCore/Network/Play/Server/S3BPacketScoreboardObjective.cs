namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using ScoreObjective = DotCraftCore.Scoreboard.ScoreObjective;

	public class S3BPacketScoreboardObjective : Packet
	{
		private string field_149343_a;
		private string field_149341_b;
		private int field_149342_c;
		private const string __OBFID = "CL_00001333";

		public S3BPacketScoreboardObjective()
		{
		}

		public S3BPacketScoreboardObjective(ScoreObjective p_i45224_1_, int p_i45224_2_)
		{
			this.field_149343_a = p_i45224_1_.Name;
			this.field_149341_b = p_i45224_1_.DisplayName;
			this.field_149342_c = p_i45224_2_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149343_a = p_148837_1_.readStringFromBuffer(16);
			this.field_149341_b = p_148837_1_.readStringFromBuffer(32);
			this.field_149342_c = p_148837_1_.readByte();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeStringToBuffer(this.field_149343_a);
			p_148840_1_.writeStringToBuffer(this.field_149341_b);
			p_148840_1_.writeByte(this.field_149342_c);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleScoreboardObjective(this);
		}

		public virtual string func_149339_c()
		{
			return this.field_149343_a;
		}

		public virtual string func_149337_d()
		{
			return this.field_149341_b;
		}

		public virtual int func_149338_e()
		{
			return this.field_149342_c;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}