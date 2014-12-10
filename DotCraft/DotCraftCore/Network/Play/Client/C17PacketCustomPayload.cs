namespace DotCraftCore.Network.Play.Client
{

	using ByteBuf = io.netty.buffer.ByteBuf;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayServer = DotCraftCore.Network.Play.INetHandlerPlayServer;

	public class C17PacketCustomPayload : Packet
	{
		private string field_149562_a;
		private int field_149560_b;
		private sbyte[] field_149561_c;
		private const string __OBFID = "CL_00001356";

		public C17PacketCustomPayload()
		{
		}

		public C17PacketCustomPayload(string p_i45248_1_, ByteBuf p_i45248_2_) : this(p_i45248_1_, p_i45248_2_.array())
		{
		}

		public C17PacketCustomPayload(string p_i45249_1_, sbyte[] p_i45249_2_)
		{
			this.field_149562_a = p_i45249_1_;
			this.field_149561_c = p_i45249_2_;

			if (p_i45249_2_ != null)
			{
				this.field_149560_b = p_i45249_2_.Length;

				if (this.field_149560_b >= 32767)
				{
					throw new System.ArgumentException("Payload may not be larger than 32k");
				}
			}
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149562_a = p_148837_1_.readStringFromBuffer(20);
			this.field_149560_b = p_148837_1_.readShort();

			if (this.field_149560_b > 0 && this.field_149560_b < 32767)
			{
				this.field_149561_c = new sbyte[this.field_149560_b];
				p_148837_1_.readBytes(this.field_149561_c);
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeStringToBuffer(this.field_149562_a);
			p_148840_1_.writeShort((short)this.field_149560_b);

			if (this.field_149561_c != null)
			{
				p_148840_1_.writeBytes(this.field_149561_c);
			}
		}

		public virtual void processPacket(INetHandlerPlayServer p_148833_1_)
		{
			p_148833_1_.processVanilla250Packet(this);
		}

		public virtual string func_149559_c()
		{
			return this.field_149562_a;
		}

		public virtual sbyte[] func_149558_e()
		{
			return this.field_149561_c;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayServer)p_148833_1_);
		}
	}

}