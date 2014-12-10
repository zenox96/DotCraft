namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S0DPacketCollectItem : Packet
	{
		private int field_149357_a;
		private int field_149356_b;
		private const string __OBFID = "CL_00001339";

		public S0DPacketCollectItem()
		{
		}

		public S0DPacketCollectItem(int p_i45232_1_, int p_i45232_2_)
		{
			this.field_149357_a = p_i45232_1_;
			this.field_149356_b = p_i45232_2_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149357_a = p_148837_1_.readInt();
			this.field_149356_b = p_148837_1_.readInt();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149357_a);
			p_148840_1_.writeInt(this.field_149356_b);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleCollectItem(this);
		}

		public virtual int func_149354_c()
		{
			return this.field_149357_a;
		}

		public virtual int func_149353_d()
		{
			return this.field_149356_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}