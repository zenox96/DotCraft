using System;

namespace DotCraftCore.nNetwork.nPlay.nServer
{

	using Entity = DotCraftCore.entity.Entity;
	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.nNetwork.nPlay.INetHandlerPlayClient;
	using World = DotCraftCore.nWorld.World;

	public class S19PacketEntityHeadLook : Packet
	{
		private int field_149384_a;
		private sbyte field_149383_b;
		

		public S19PacketEntityHeadLook()
		{
		}

		public S19PacketEntityHeadLook(Entity p_i45214_1_, sbyte p_i45214_2_)
		{
			this.field_149384_a = p_i45214_1_.EntityId;
			this.field_149383_b = p_i45214_2_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149384_a = p_148837_1_.readInt();
			this.field_149383_b = p_148837_1_.readByte();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149384_a);
			p_148840_1_.writeByte(this.field_149383_b);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleEntityHeadLook(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("id={0:D}, rot={1:D}", new object[] {Convert.ToInt32(this.field_149384_a), Convert.ToByte(this.field_149383_b)});
		}

		public virtual Entity func_149381_a(World p_149381_1_)
		{
			return p_149381_1_.getEntityByID(this.field_149384_a);
		}

		public virtual sbyte func_149380_c()
		{
			return this.field_149383_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}