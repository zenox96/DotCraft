namespace DotCraftCore.nNetwork.nPlay.nClient
{

	using ItemStack = DotCraftCore.nItem.ItemStack;
	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayServer = DotCraftCore.nNetwork.nPlay.INetHandlerPlayServer;

	public class C10PacketCreativeInventoryAction : Packet
	{
		private int field_149629_a;
		private ItemStack field_149628_b;
		

		public C10PacketCreativeInventoryAction()
		{
		}

		public C10PacketCreativeInventoryAction(int p_i45263_1_, ItemStack p_i45263_2_)
		{
			this.field_149629_a = p_i45263_1_;
			this.field_149628_b = p_i45263_2_ != null ? p_i45263_2_.copy() : null;
		}

		public virtual void processPacket(INetHandlerPlayServer p_148833_1_)
		{
			p_148833_1_.processCreativeInventoryAction(this);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149629_a = p_148837_1_.readShort();
			this.field_149628_b = p_148837_1_.readItemStackFromBuffer();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeShort(this.field_149629_a);
			p_148840_1_.writeItemStackToBuffer(this.field_149628_b);
		}

		public virtual int func_149627_c()
		{
			return this.field_149629_a;
		}

		public virtual ItemStack func_149625_d()
		{
			return this.field_149628_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayServer)p_148833_1_);
		}
	}

}