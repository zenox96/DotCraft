namespace DotCraftCore.Network.Play.Server
{

	using Entity = DotCraftCore.entity.Entity;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using MathHelper = DotCraftCore.Util.MathHelper;

	public class S18PacketEntityTeleport : Packet
	{
		private int field_149458_a;
		private int field_149456_b;
		private int field_149457_c;
		private int field_149454_d;
		private sbyte field_149455_e;
		private sbyte field_149453_f;
		

		public S18PacketEntityTeleport()
		{
		}

		public S18PacketEntityTeleport(Entity p_i45233_1_)
		{
			this.field_149458_a = p_i45233_1_.EntityId;
			this.field_149456_b = MathHelper.floor_double(p_i45233_1_.posX * 32.0D);
			this.field_149457_c = MathHelper.floor_double(p_i45233_1_.posY * 32.0D);
			this.field_149454_d = MathHelper.floor_double(p_i45233_1_.posZ * 32.0D);
			this.field_149455_e = (sbyte)((int)(p_i45233_1_.rotationYaw * 256.0F / 360.0F));
			this.field_149453_f = (sbyte)((int)(p_i45233_1_.rotationPitch * 256.0F / 360.0F));
		}

		public S18PacketEntityTeleport(int p_i45234_1_, int p_i45234_2_, int p_i45234_3_, int p_i45234_4_, sbyte p_i45234_5_, sbyte p_i45234_6_)
		{
			this.field_149458_a = p_i45234_1_;
			this.field_149456_b = p_i45234_2_;
			this.field_149457_c = p_i45234_3_;
			this.field_149454_d = p_i45234_4_;
			this.field_149455_e = p_i45234_5_;
			this.field_149453_f = p_i45234_6_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149458_a = p_148837_1_.readInt();
			this.field_149456_b = p_148837_1_.readInt();
			this.field_149457_c = p_148837_1_.readInt();
			this.field_149454_d = p_148837_1_.readInt();
			this.field_149455_e = p_148837_1_.readByte();
			this.field_149453_f = p_148837_1_.readByte();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149458_a);
			p_148840_1_.writeInt(this.field_149456_b);
			p_148840_1_.writeInt(this.field_149457_c);
			p_148840_1_.writeInt(this.field_149454_d);
			p_148840_1_.writeByte(this.field_149455_e);
			p_148840_1_.writeByte(this.field_149453_f);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleEntityTeleport(this);
		}

		public virtual int func_149451_c()
		{
			return this.field_149458_a;
		}

		public virtual int func_149449_d()
		{
			return this.field_149456_b;
		}

		public virtual int func_149448_e()
		{
			return this.field_149457_c;
		}

		public virtual int func_149446_f()
		{
			return this.field_149454_d;
		}

		public virtual sbyte func_149450_g()
		{
			return this.field_149455_e;
		}

		public virtual sbyte func_149447_h()
		{
			return this.field_149453_f;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}