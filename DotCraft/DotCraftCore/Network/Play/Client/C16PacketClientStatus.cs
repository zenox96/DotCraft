namespace DotCraftCore.nNetwork.nPlay.nClient
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayServer = DotCraftCore.nNetwork.nPlay.INetHandlerPlayServer;

	public class C16PacketClientStatus : Packet
	{
		private C16PacketClientStatus.EnumState field_149437_a;
		

		public C16PacketClientStatus()
		{
		}

		public C16PacketClientStatus(C16PacketClientStatus.EnumState p_i45242_1_)
		{
			this.field_149437_a = p_i45242_1_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149437_a = C16PacketClientStatus.EnumState.field_151404_e[p_148837_1_.readByte() % C16PacketClientStatus.EnumState.field_151404_e.length];
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeByte(this.field_149437_a.field_151403_d);
		}

		public virtual void processPacket(INetHandlerPlayServer p_148833_1_)
		{
			p_148833_1_.processClientStatus(this);
		}

		public virtual C16PacketClientStatus.EnumState func_149435_c()
		{
			return this.field_149437_a;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayServer)p_148833_1_);
		}

		public enum EnumState
		{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			PERFORM_RESPAWN("PERFORM_RESPAWN", 0, 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			REQUEST_STATS("REQUEST_STATS", 1, 1),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			OPEN_INVENTORY_ACHIEVEMENT("OPEN_INVENTORY_ACHIEVEMENT", 2, 2);
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private final int field_151403_d;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private static final C16PacketClientStatus.EnumState[] field_151404_e = new C16PacketClientStatus.EnumState[values().length];

			@private static final C16PacketClientStatus.EnumState[] $VALUES = new C16PacketClientStatus.EnumState[]{PERFORM_RESPAWN, REQUEST_STATS, OPEN_INVENTORY_ACHIEVEMENT
		}
			

			private EnumState(string p_i45241_1_, int p_i45241_2_, int p_i45241_3_)
			{
				this.field_151403_d = p_i45241_3_;
			}

			static C16PacketClientStatus()
			{
				C16PacketClientStatus.EnumState[] var0 = values();
				int var1 = var0.Length;

				for (int var2 = 0; var2 < var1; ++var2)
				{
					C16PacketClientStatus.EnumState var3 = var0[var2];
					field_151404_e[var3.field_151403_d] = var3;
				}
			}
		}
	}

}