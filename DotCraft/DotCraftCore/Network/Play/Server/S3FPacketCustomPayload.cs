namespace DotCraftCore.Network.Play.Server
{

	using ByteBuf = io.netty.buffer.ByteBuf;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S3FPacketCustomPayload : Packet
	{
		private string field_149172_a;
		private sbyte[] field_149171_b;
		private const string __OBFID = "CL_00001297";

		public S3FPacketCustomPayload()
		{
		}

		public S3FPacketCustomPayload(string p_i45189_1_, ByteBuf p_i45189_2_) : this(p_i45189_1_, p_i45189_2_.array())
		{
		}

		public S3FPacketCustomPayload(string p_i45190_1_, sbyte[] p_i45190_2_)
		{
			this.field_149172_a = p_i45190_1_;
			this.field_149171_b = p_i45190_2_;

			if (p_i45190_2_.Length >= 1048576)
			{
				throw new System.ArgumentException("Payload may not be larger than 1048576 bytes");
			}
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149172_a = p_148837_1_.readStringFromBuffer(20);
			this.field_149171_b = new sbyte[p_148837_1_.readUnsignedShort()];
			p_148837_1_.readBytes(this.field_149171_b);
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeStringToBuffer(this.field_149172_a);
			p_148840_1_.writeShort(this.field_149171_b.Length);
			p_148840_1_.writeBytes(this.field_149171_b);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleCustomPayload(this);
		}

		public virtual string func_149169_c()
		{
			return this.field_149172_a;
		}

		public virtual sbyte[] func_149168_d()
		{
			return this.field_149171_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}