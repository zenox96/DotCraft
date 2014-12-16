namespace DotCraftCore.Network.Play.Server
{

	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S35PacketUpdateTileEntity : Packet
	{
		private int field_148863_a;
		private int field_148861_b;
		private int field_148862_c;
		private int field_148859_d;
		private NBTTagCompound field_148860_e;
		

		public S35PacketUpdateTileEntity()
		{
		}

		public S35PacketUpdateTileEntity(int p_i45175_1_, int p_i45175_2_, int p_i45175_3_, int p_i45175_4_, NBTTagCompound p_i45175_5_)
		{
			this.field_148863_a = p_i45175_1_;
			this.field_148861_b = p_i45175_2_;
			this.field_148862_c = p_i45175_3_;
			this.field_148859_d = p_i45175_4_;
			this.field_148860_e = p_i45175_5_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_148863_a = p_148837_1_.readInt();
			this.field_148861_b = p_148837_1_.readShort();
			this.field_148862_c = p_148837_1_.readInt();
			this.field_148859_d = p_148837_1_.readUnsignedByte();
			this.field_148860_e = p_148837_1_.readNBTTagCompoundFromBuffer();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_148863_a);
			p_148840_1_.writeShort(this.field_148861_b);
			p_148840_1_.writeInt(this.field_148862_c);
			p_148840_1_.writeByte((sbyte)this.field_148859_d);
			p_148840_1_.writeNBTTagCompoundToBuffer(this.field_148860_e);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleUpdateTileEntity(this);
		}

		public virtual int func_148856_c()
		{
			return this.field_148863_a;
		}

		public virtual int func_148855_d()
		{
			return this.field_148861_b;
		}

		public virtual int func_148854_e()
		{
			return this.field_148862_c;
		}

		public virtual int func_148853_f()
		{
			return this.field_148859_d;
		}

		public virtual NBTTagCompound func_148857_g()
		{
			return this.field_148860_e;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}