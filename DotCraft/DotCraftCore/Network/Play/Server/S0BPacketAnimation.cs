using System;

namespace DotCraftCore.Network.Play.Server
{

	using Entity = DotCraftCore.entity.Entity;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S0BPacketAnimation : Packet
	{
		private int field_148981_a;
		private int field_148980_b;
		private const string __OBFID = "CL_00001282";

		public S0BPacketAnimation()
		{
		}

		public S0BPacketAnimation(Entity p_i45172_1_, int p_i45172_2_)
		{
			this.field_148981_a = p_i45172_1_.EntityId;
			this.field_148980_b = p_i45172_2_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_148981_a = p_148837_1_.readVarIntFromBuffer();
			this.field_148980_b = p_148837_1_.readUnsignedByte();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeVarIntToBuffer(this.field_148981_a);
			p_148840_1_.writeByte(this.field_148980_b);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleAnimation(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("id={0:D}, type={1:D}", new object[] {Convert.ToInt32(this.field_148981_a), Convert.ToInt32(this.field_148980_b)});
		}

		public virtual int func_148978_c()
		{
			return this.field_148981_a;
		}

		public virtual int func_148977_d()
		{
			return this.field_148980_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}