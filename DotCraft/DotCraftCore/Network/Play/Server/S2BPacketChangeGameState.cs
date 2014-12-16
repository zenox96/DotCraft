namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S2BPacketChangeGameState : Packet
	{
		public static readonly string[] field_149142_a = new string[] {"tile.bed.notValid", null, null, "gameMode.changed"};
		private int field_149140_b;
		private float field_149141_c;
		

		public S2BPacketChangeGameState()
		{
		}

		public S2BPacketChangeGameState(int p_i45194_1_, float p_i45194_2_)
		{
			this.field_149140_b = p_i45194_1_;
			this.field_149141_c = p_i45194_2_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149140_b = p_148837_1_.readUnsignedByte();
			this.field_149141_c = p_148837_1_.readFloat();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeByte(this.field_149140_b);
			p_148840_1_.writeFloat(this.field_149141_c);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleChangeGameState(this);
		}

		public virtual int func_149138_c()
		{
			return this.field_149140_b;
		}

		public virtual float func_149137_d()
		{
			return this.field_149141_c;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}