namespace DotCraftCore.Network.Play.Server
{

	using Entity = DotCraftCore.entity.Entity;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using World = DotCraftCore.World.World;

	public class S19PacketEntityStatus : Packet
	{
		private int field_149164_a;
		private sbyte field_149163_b;
		private const string __OBFID = "CL_00001299";

		public S19PacketEntityStatus()
		{
		}

		public S19PacketEntityStatus(Entity p_i45192_1_, sbyte p_i45192_2_)
		{
			this.field_149164_a = p_i45192_1_.EntityId;
			this.field_149163_b = p_i45192_2_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149164_a = p_148837_1_.readInt();
			this.field_149163_b = p_148837_1_.readByte();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149164_a);
			p_148840_1_.writeByte(this.field_149163_b);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleEntityStatus(this);
		}

		public virtual Entity func_149161_a(World p_149161_1_)
		{
			return p_149161_1_.getEntityByID(this.field_149164_a);
		}

		public virtual sbyte func_149160_c()
		{
			return this.field_149163_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}