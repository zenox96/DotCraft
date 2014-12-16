namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using PotionEffect = DotCraftCore.Potion.PotionEffect;

	public class S1EPacketRemoveEntityEffect : Packet
	{
		private int field_149079_a;
		private int field_149078_b;
		

		public S1EPacketRemoveEntityEffect()
		{
		}

		public S1EPacketRemoveEntityEffect(int p_i45212_1_, PotionEffect p_i45212_2_)
		{
			this.field_149079_a = p_i45212_1_;
			this.field_149078_b = p_i45212_2_.PotionID;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149079_a = p_148837_1_.readInt();
			this.field_149078_b = p_148837_1_.readUnsignedByte();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149079_a);
			p_148840_1_.writeByte(this.field_149078_b);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleRemoveEntityEffect(this);
		}

		public virtual int func_149076_c()
		{
			return this.field_149079_a;
		}

		public virtual int func_149075_d()
		{
			return this.field_149078_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}