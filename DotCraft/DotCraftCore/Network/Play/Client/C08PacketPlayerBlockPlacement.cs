namespace DotCraftCore.Network.Play.Client
{

	using ItemStack = DotCraftCore.Item.ItemStack;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayServer = DotCraftCore.Network.Play.INetHandlerPlayServer;

	public class C08PacketPlayerBlockPlacement : Packet
	{
		private int field_149583_a;
		private int field_149581_b;
		private int field_149582_c;
		private int field_149579_d;
		private ItemStack field_149580_e;
		private float field_149577_f;
		private float field_149578_g;
		private float field_149584_h;
		private const string __OBFID = "CL_00001371";

		public C08PacketPlayerBlockPlacement()
		{
		}

		public C08PacketPlayerBlockPlacement(int p_i45265_1_, int p_i45265_2_, int p_i45265_3_, int p_i45265_4_, ItemStack p_i45265_5_, float p_i45265_6_, float p_i45265_7_, float p_i45265_8_)
		{
			this.field_149583_a = p_i45265_1_;
			this.field_149581_b = p_i45265_2_;
			this.field_149582_c = p_i45265_3_;
			this.field_149579_d = p_i45265_4_;
			this.field_149580_e = p_i45265_5_ != null ? p_i45265_5_.copy() : null;
			this.field_149577_f = p_i45265_6_;
			this.field_149578_g = p_i45265_7_;
			this.field_149584_h = p_i45265_8_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149583_a = p_148837_1_.readInt();
			this.field_149581_b = p_148837_1_.readUnsignedByte();
			this.field_149582_c = p_148837_1_.readInt();
			this.field_149579_d = p_148837_1_.readUnsignedByte();
			this.field_149580_e = p_148837_1_.readItemStackFromBuffer();
			this.field_149577_f = (float)p_148837_1_.readUnsignedByte() / 16.0F;
			this.field_149578_g = (float)p_148837_1_.readUnsignedByte() / 16.0F;
			this.field_149584_h = (float)p_148837_1_.readUnsignedByte() / 16.0F;
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149583_a);
			p_148840_1_.writeByte(this.field_149581_b);
			p_148840_1_.writeInt(this.field_149582_c);
			p_148840_1_.writeByte(this.field_149579_d);
			p_148840_1_.writeItemStackToBuffer(this.field_149580_e);
			p_148840_1_.writeByte((int)(this.field_149577_f * 16.0F));
			p_148840_1_.writeByte((int)(this.field_149578_g * 16.0F));
			p_148840_1_.writeByte((int)(this.field_149584_h * 16.0F));
		}

		public virtual void processPacket(INetHandlerPlayServer p_148833_1_)
		{
			p_148833_1_.processPlayerBlockPlacement(this);
		}

		public virtual int func_149576_c()
		{
			return this.field_149583_a;
		}

		public virtual int func_149571_d()
		{
			return this.field_149581_b;
		}

		public virtual int func_149570_e()
		{
			return this.field_149582_c;
		}

		public virtual int func_149568_f()
		{
			return this.field_149579_d;
		}

		public virtual ItemStack func_149574_g()
		{
			return this.field_149580_e;
		}

		public virtual float func_149573_h()
		{
			return this.field_149577_f;
		}

		public virtual float func_149569_i()
		{
			return this.field_149578_g;
		}

		public virtual float func_149575_j()
		{
			return this.field_149584_h;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayServer)p_148833_1_);
		}
	}

}