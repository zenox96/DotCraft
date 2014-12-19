namespace DotCraftCore.nNetwork.nPlay.nServer
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.nNetwork.nPlay.INetHandlerPlayClient;

	public class S1FPacketSetExperience : Packet
	{
		private float field_149401_a;
		private int field_149399_b;
		private int field_149400_c;
		

		public S1FPacketSetExperience()
		{
		}

		public S1FPacketSetExperience(float p_i45222_1_, int p_i45222_2_, int p_i45222_3_)
		{
			this.field_149401_a = p_i45222_1_;
			this.field_149399_b = p_i45222_2_;
			this.field_149400_c = p_i45222_3_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149401_a = p_148837_1_.readFloat();
			this.field_149400_c = p_148837_1_.readShort();
			this.field_149399_b = p_148837_1_.readShort();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeFloat(this.field_149401_a);
			p_148840_1_.writeShort(this.field_149400_c);
			p_148840_1_.writeShort(this.field_149399_b);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleSetExperience(this);
		}

		public virtual float func_149397_c()
		{
			return this.field_149401_a;
		}

		public virtual int func_149396_d()
		{
			return this.field_149399_b;
		}

		public virtual int func_149395_e()
		{
			return this.field_149400_c;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}