namespace DotCraftCore.Network.Play.Server
{

	using Block = DotCraftCore.block.Block;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S24PacketBlockAction : Packet
	{
		private int field_148876_a;
		private int field_148874_b;
		private int field_148875_c;
		private int field_148872_d;
		private int field_148873_e;
		private Block field_148871_f;
		

		public S24PacketBlockAction()
		{
		}

		public S24PacketBlockAction(int p_i45176_1_, int p_i45176_2_, int p_i45176_3_, Block p_i45176_4_, int p_i45176_5_, int p_i45176_6_)
		{
			this.field_148876_a = p_i45176_1_;
			this.field_148874_b = p_i45176_2_;
			this.field_148875_c = p_i45176_3_;
			this.field_148872_d = p_i45176_5_;
			this.field_148873_e = p_i45176_6_;
			this.field_148871_f = p_i45176_4_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_148876_a = p_148837_1_.readInt();
			this.field_148874_b = p_148837_1_.readShort();
			this.field_148875_c = p_148837_1_.readInt();
			this.field_148872_d = p_148837_1_.readUnsignedByte();
			this.field_148873_e = p_148837_1_.readUnsignedByte();
			this.field_148871_f = Block.getBlockById(p_148837_1_.readVarIntFromBuffer() & 4095);
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_148876_a);
			p_148840_1_.writeShort(this.field_148874_b);
			p_148840_1_.writeInt(this.field_148875_c);
			p_148840_1_.writeByte(this.field_148872_d);
			p_148840_1_.writeByte(this.field_148873_e);
			p_148840_1_.writeVarIntToBuffer(Block.getIdFromBlock(this.field_148871_f) & 4095);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleBlockAction(this);
		}

		public virtual Block func_148868_c()
		{
			return this.field_148871_f;
		}

		public virtual int func_148867_d()
		{
			return this.field_148876_a;
		}

		public virtual int func_148866_e()
		{
			return this.field_148874_b;
		}

		public virtual int func_148865_f()
		{
			return this.field_148875_c;
		}

		public virtual int func_148869_g()
		{
			return this.field_148872_d;
		}

		public virtual int func_148864_h()
		{
			return this.field_148873_e;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}