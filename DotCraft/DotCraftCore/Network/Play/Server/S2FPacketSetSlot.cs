namespace DotCraftCore.Network.Play.Server
{

	using ItemStack = DotCraftCore.Item.ItemStack;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S2FPacketSetSlot : Packet
	{
		private int field_149179_a;
		private int field_149177_b;
		private ItemStack field_149178_c;
		

		public S2FPacketSetSlot()
		{
		}

		public S2FPacketSetSlot(int p_i45188_1_, int p_i45188_2_, ItemStack p_i45188_3_)
		{
			this.field_149179_a = p_i45188_1_;
			this.field_149177_b = p_i45188_2_;
			this.field_149178_c = p_i45188_3_ == null ? null : p_i45188_3_.copy();
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleSetSlot(this);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149179_a = p_148837_1_.readByte();
			this.field_149177_b = p_148837_1_.readShort();
			this.field_149178_c = p_148837_1_.readItemStackFromBuffer();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeByte(this.field_149179_a);
			p_148840_1_.writeShort(this.field_149177_b);
			p_148840_1_.writeItemStackToBuffer(this.field_149178_c);
		}

		public virtual int func_149175_c()
		{
			return this.field_149179_a;
		}

		public virtual int func_149173_d()
		{
			return this.field_149177_b;
		}

		public virtual ItemStack func_149174_e()
		{
			return this.field_149178_c;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}