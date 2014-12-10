namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S36PacketSignEditorOpen : Packet
	{
		private int field_149133_a;
		private int field_149131_b;
		private int field_149132_c;
		private const string __OBFID = "CL_00001316";

		public S36PacketSignEditorOpen()
		{
		}

		public S36PacketSignEditorOpen(int p_i45207_1_, int p_i45207_2_, int p_i45207_3_)
		{
			this.field_149133_a = p_i45207_1_;
			this.field_149131_b = p_i45207_2_;
			this.field_149132_c = p_i45207_3_;
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleSignEditorOpen(this);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149133_a = p_148837_1_.readInt();
			this.field_149131_b = p_148837_1_.readInt();
			this.field_149132_c = p_148837_1_.readInt();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149133_a);
			p_148840_1_.writeInt(this.field_149131_b);
			p_148840_1_.writeInt(this.field_149132_c);
		}

		public virtual int func_149129_c()
		{
			return this.field_149133_a;
		}

		public virtual int func_149128_d()
		{
			return this.field_149131_b;
		}

		public virtual int func_149127_e()
		{
			return this.field_149132_c;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}