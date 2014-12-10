namespace DotCraftCore.Network.Play.Server
{

	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using World = DotCraftCore.World.World;

	public class S0APacketUseBed : Packet
	{
		private int field_149097_a;
		private int field_149095_b;
		private int field_149096_c;
		private int field_149094_d;
		private const string __OBFID = "CL_00001319";

		public S0APacketUseBed()
		{
		}

		public S0APacketUseBed(EntityPlayer p_i45210_1_, int p_i45210_2_, int p_i45210_3_, int p_i45210_4_)
		{
			this.field_149095_b = p_i45210_2_;
			this.field_149096_c = p_i45210_3_;
			this.field_149094_d = p_i45210_4_;
			this.field_149097_a = p_i45210_1_.EntityId;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149097_a = p_148837_1_.readInt();
			this.field_149095_b = p_148837_1_.readInt();
			this.field_149096_c = p_148837_1_.readByte();
			this.field_149094_d = p_148837_1_.readInt();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149097_a);
			p_148840_1_.writeInt(this.field_149095_b);
			p_148840_1_.writeByte(this.field_149096_c);
			p_148840_1_.writeInt(this.field_149094_d);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleUseBed(this);
		}

		public virtual EntityPlayer func_149091_a(World p_149091_1_)
		{
			return (EntityPlayer)p_149091_1_.getEntityByID(this.field_149097_a);
		}

		public virtual int func_149092_c()
		{
			return this.field_149095_b;
		}

		public virtual int func_149090_d()
		{
			return this.field_149096_c;
		}

		public virtual int func_149089_e()
		{
			return this.field_149094_d;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}