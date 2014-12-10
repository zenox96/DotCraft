namespace DotCraftCore.Network.Play.Client
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayServer = DotCraftCore.Network.Play.INetHandlerPlayServer;

	public class C0CPacketInput : Packet
	{
		private float field_149624_a;
		private float field_149622_b;
		private bool field_149623_c;
		private bool field_149621_d;
		private const string __OBFID = "CL_00001367";

		public C0CPacketInput()
		{
		}

		public C0CPacketInput(float p_i45261_1_, float p_i45261_2_, bool p_i45261_3_, bool p_i45261_4_)
		{
			this.field_149624_a = p_i45261_1_;
			this.field_149622_b = p_i45261_2_;
			this.field_149623_c = p_i45261_3_;
			this.field_149621_d = p_i45261_4_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149624_a = p_148837_1_.readFloat();
			this.field_149622_b = p_148837_1_.readFloat();
			this.field_149623_c = p_148837_1_.readBoolean();
			this.field_149621_d = p_148837_1_.readBoolean();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeFloat(this.field_149624_a);
			p_148840_1_.writeFloat(this.field_149622_b);
			p_148840_1_.writeBoolean(this.field_149623_c);
			p_148840_1_.writeBoolean(this.field_149621_d);
		}

		public virtual void processPacket(INetHandlerPlayServer p_148833_1_)
		{
			p_148833_1_.processInput(this);
		}

		public virtual float func_149620_c()
		{
			return this.field_149624_a;
		}

		public virtual float func_149616_d()
		{
			return this.field_149622_b;
		}

		public virtual bool func_149618_e()
		{
			return this.field_149623_c;
		}

		public virtual bool func_149617_f()
		{
			return this.field_149621_d;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayServer)p_148833_1_);
		}
	}

}