using System;

namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S34PacketMaps : Packet
	{
		private int field_149191_a;
		private sbyte[] field_149190_b;
		private const string __OBFID = "CL_00001311";

		public S34PacketMaps()
		{
		}

		public S34PacketMaps(int p_i45202_1_, sbyte[] p_i45202_2_)
		{
			this.field_149191_a = p_i45202_1_;
			this.field_149190_b = p_i45202_2_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149191_a = p_148837_1_.readVarIntFromBuffer();
			this.field_149190_b = new sbyte[p_148837_1_.readUnsignedShort()];
			p_148837_1_.readBytes(this.field_149190_b);
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeVarIntToBuffer(this.field_149191_a);
			p_148840_1_.writeShort(this.field_149190_b.Length);
			p_148840_1_.writeBytes(this.field_149190_b);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleMaps(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("id={0:D}, length={1:D}", new object[] {Convert.ToInt32(this.field_149191_a), Convert.ToInt32(this.field_149190_b.Length)});
		}

		public virtual int func_149188_c()
		{
			return this.field_149191_a;
		}

		public virtual sbyte[] func_149187_d()
		{
			return this.field_149190_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}