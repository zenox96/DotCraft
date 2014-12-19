namespace DotCraftCore.nNetwork.nPlay.nServer
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.nNetwork.nPlay.INetHandlerPlayClient;
	using Validate = org.apache.commons.lang3.Validate;

	public class S29PacketSoundEffect : Packet
	{
		private string field_149219_a;
		private int field_149217_b;
		private int field_149218_c = int.MAX_VALUE;
		private int field_149215_d;
		private float field_149216_e;
		private int field_149214_f;
		

		public S29PacketSoundEffect()
		{
		}

		public S29PacketSoundEffect(string p_i45200_1_, double p_i45200_2_, double p_i45200_4_, double p_i45200_6_, float p_i45200_8_, float p_i45200_9_)
		{
			Validate.notNull(p_i45200_1_, "name", new object[0]);
			this.field_149219_a = p_i45200_1_;
			this.field_149217_b = (int)(p_i45200_2_ * 8.0D);
			this.field_149218_c = (int)(p_i45200_4_ * 8.0D);
			this.field_149215_d = (int)(p_i45200_6_ * 8.0D);
			this.field_149216_e = p_i45200_8_;
			this.field_149214_f = (int)(p_i45200_9_ * 63.0F);

			if (this.field_149214_f < 0)
			{
				this.field_149214_f = 0;
			}

			if (this.field_149214_f > 255)
			{
				this.field_149214_f = 255;
			}
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149219_a = p_148837_1_.readStringFromBuffer(256);
			this.field_149217_b = p_148837_1_.readInt();
			this.field_149218_c = p_148837_1_.readInt();
			this.field_149215_d = p_148837_1_.readInt();
			this.field_149216_e = p_148837_1_.readFloat();
			this.field_149214_f = p_148837_1_.readUnsignedByte();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeStringToBuffer(this.field_149219_a);
			p_148840_1_.writeInt(this.field_149217_b);
			p_148840_1_.writeInt(this.field_149218_c);
			p_148840_1_.writeInt(this.field_149215_d);
			p_148840_1_.writeFloat(this.field_149216_e);
			p_148840_1_.writeByte(this.field_149214_f);
		}

		public virtual string func_149212_c()
		{
			return this.field_149219_a;
		}

		public virtual double func_149207_d()
		{
			return (double)((float)this.field_149217_b / 8.0F);
		}

		public virtual double func_149211_e()
		{
			return (double)((float)this.field_149218_c / 8.0F);
		}

		public virtual double func_149210_f()
		{
			return (double)((float)this.field_149215_d / 8.0F);
		}

		public virtual float func_149208_g()
		{
			return this.field_149216_e;
		}

		public virtual float func_149209_h()
		{
			return (float)this.field_149214_f / 63.0F;
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleSoundEffect(this);
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}