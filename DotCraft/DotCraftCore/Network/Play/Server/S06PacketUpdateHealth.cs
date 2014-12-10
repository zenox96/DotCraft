namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S06PacketUpdateHealth : Packet
	{
		private float field_149336_a;
		private int field_149334_b;
		private float field_149335_c;
		private const string __OBFID = "CL_00001332";

		public S06PacketUpdateHealth()
		{
		}

		public S06PacketUpdateHealth(float p_i45223_1_, int p_i45223_2_, float p_i45223_3_)
		{
			this.field_149336_a = p_i45223_1_;
			this.field_149334_b = p_i45223_2_;
			this.field_149335_c = p_i45223_3_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149336_a = p_148837_1_.readFloat();
			this.field_149334_b = p_148837_1_.readShort();
			this.field_149335_c = p_148837_1_.readFloat();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeFloat(this.field_149336_a);
			p_148840_1_.writeShort(this.field_149334_b);
			p_148840_1_.writeFloat(this.field_149335_c);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleUpdateHealth(this);
		}

		public virtual float func_149332_c()
		{
			return this.field_149336_a;
		}

		public virtual int func_149330_d()
		{
			return this.field_149334_b;
		}

		public virtual float func_149331_e()
		{
			return this.field_149335_c;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}