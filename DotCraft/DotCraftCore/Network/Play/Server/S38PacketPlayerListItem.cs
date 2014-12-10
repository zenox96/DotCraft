namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S38PacketPlayerListItem : Packet
	{
		private string field_149126_a;
		private bool field_149124_b;
		private int field_149125_c;
		private const string __OBFID = "CL_00001318";

		public S38PacketPlayerListItem()
		{
		}

		public S38PacketPlayerListItem(string p_i45209_1_, bool p_i45209_2_, int p_i45209_3_)
		{
			this.field_149126_a = p_i45209_1_;
			this.field_149124_b = p_i45209_2_;
			this.field_149125_c = p_i45209_3_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149126_a = p_148837_1_.readStringFromBuffer(16);
			this.field_149124_b = p_148837_1_.readBoolean();
			this.field_149125_c = p_148837_1_.readShort();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeStringToBuffer(this.field_149126_a);
			p_148840_1_.writeBoolean(this.field_149124_b);
			p_148840_1_.writeShort(this.field_149125_c);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handlePlayerListItem(this);
		}

		public virtual string func_149122_c()
		{
			return this.field_149126_a;
		}

		public virtual bool func_149121_d()
		{
			return this.field_149124_b;
		}

		public virtual int func_149120_e()
		{
			return this.field_149125_c;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}