namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using EnumDifficulty = DotCraftCore.World.EnumDifficulty;
	using WorldSettings = DotCraftCore.World.WorldSettings;
	using WorldType = DotCraftCore.World.WorldType;

	public class S07PacketRespawn : Packet
	{
		private int field_149088_a;
		private EnumDifficulty field_149086_b;
		private WorldSettings.GameType field_149087_c;
		private WorldType field_149085_d;
		

		public S07PacketRespawn()
		{
		}

		public S07PacketRespawn(int p_i45213_1_, EnumDifficulty p_i45213_2_, WorldType p_i45213_3_, WorldSettings.GameType p_i45213_4_)
		{
			this.field_149088_a = p_i45213_1_;
			this.field_149086_b = p_i45213_2_;
			this.field_149087_c = p_i45213_4_;
			this.field_149085_d = p_i45213_3_;
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleRespawn(this);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149088_a = p_148837_1_.readInt();
			this.field_149086_b = EnumDifficulty.getDifficultyEnum(p_148837_1_.readUnsignedByte());
			this.field_149087_c = WorldSettings.GameType.getByID(p_148837_1_.readUnsignedByte());
			this.field_149085_d = WorldType.parseWorldType(p_148837_1_.readStringFromBuffer(16));

			if (this.field_149085_d == null)
			{
				this.field_149085_d = WorldType.DEFAULT;
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149088_a);
			p_148840_1_.writeByte(this.field_149086_b.DifficultyId);
			p_148840_1_.writeByte(this.field_149087_c.ID);
			p_148840_1_.writeStringToBuffer(this.field_149085_d.WorldTypeName);
		}

		public virtual int func_149082_c()
		{
			return this.field_149088_a;
		}

		public virtual EnumDifficulty func_149081_d()
		{
			return this.field_149086_b;
		}

		public virtual WorldSettings.GameType func_149083_e()
		{
			return this.field_149087_c;
		}

		public virtual WorldType func_149080_f()
		{
			return this.field_149085_d;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}