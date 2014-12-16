namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using PotionEffect = DotCraftCore.Potion.PotionEffect;

	public class S1DPacketEntityEffect : Packet
	{
		private int field_149434_a;
		private sbyte field_149432_b;
		private sbyte field_149433_c;
		private short field_149431_d;
		

		public S1DPacketEntityEffect()
		{
		}

		public S1DPacketEntityEffect(int p_i45237_1_, PotionEffect p_i45237_2_)
		{
			this.field_149434_a = p_i45237_1_;
			this.field_149432_b = (sbyte)(p_i45237_2_.PotionID & 255);
			this.field_149433_c = (sbyte)(p_i45237_2_.Amplifier & 255);

			if (p_i45237_2_.Duration > 32767)
			{
				this.field_149431_d = 32767;
			}
			else
			{
				this.field_149431_d = (short)p_i45237_2_.Duration;
			}
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149434_a = p_148837_1_.readInt();
			this.field_149432_b = p_148837_1_.readByte();
			this.field_149433_c = p_148837_1_.readByte();
			this.field_149431_d = p_148837_1_.readShort();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149434_a);
			p_148840_1_.writeByte(this.field_149432_b);
			p_148840_1_.writeByte(this.field_149433_c);
			p_148840_1_.writeShort(this.field_149431_d);
		}

		public virtual bool func_149429_c()
		{
			return this.field_149431_d == 32767;
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleEntityEffect(this);
		}

		public virtual int func_149426_d()
		{
			return this.field_149434_a;
		}

		public virtual sbyte func_149427_e()
		{
			return this.field_149432_b;
		}

		public virtual sbyte func_149428_f()
		{
			return this.field_149433_c;
		}

		public virtual short func_149425_g()
		{
			return this.field_149431_d;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}