namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S08PacketPlayerPosLook : Packet
	{
		private double field_148940_a;
		private double field_148938_b;
		private double field_148939_c;
		private float field_148936_d;
		private float field_148937_e;
		private bool field_148935_f;
		

		public S08PacketPlayerPosLook()
		{
		}

		public S08PacketPlayerPosLook(double p_i45164_1_, double p_i45164_3_, double p_i45164_5_, float p_i45164_7_, float p_i45164_8_, bool p_i45164_9_)
		{
			this.field_148940_a = p_i45164_1_;
			this.field_148938_b = p_i45164_3_;
			this.field_148939_c = p_i45164_5_;
			this.field_148936_d = p_i45164_7_;
			this.field_148937_e = p_i45164_8_;
			this.field_148935_f = p_i45164_9_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_148940_a = p_148837_1_.readDouble();
			this.field_148938_b = p_148837_1_.readDouble();
			this.field_148939_c = p_148837_1_.readDouble();
			this.field_148936_d = p_148837_1_.readFloat();
			this.field_148937_e = p_148837_1_.readFloat();
			this.field_148935_f = p_148837_1_.readBoolean();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeDouble(this.field_148940_a);
			p_148840_1_.writeDouble(this.field_148938_b);
			p_148840_1_.writeDouble(this.field_148939_c);
			p_148840_1_.writeFloat(this.field_148936_d);
			p_148840_1_.writeFloat(this.field_148937_e);
			p_148840_1_.writeBoolean(this.field_148935_f);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handlePlayerPosLook(this);
		}

		public virtual double func_148932_c()
		{
			return this.field_148940_a;
		}

		public virtual double func_148928_d()
		{
			return this.field_148938_b;
		}

		public virtual double func_148933_e()
		{
			return this.field_148939_c;
		}

		public virtual float func_148931_f()
		{
			return this.field_148936_d;
		}

		public virtual float func_148930_g()
		{
			return this.field_148937_e;
		}

		public virtual bool func_148929_h()
		{
			return this.field_148935_f;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}