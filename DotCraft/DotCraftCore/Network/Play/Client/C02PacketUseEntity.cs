namespace DotCraftCore.Network.Play.Client
{

	using Entity = DotCraftCore.entity.Entity;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayServer = DotCraftCore.Network.Play.INetHandlerPlayServer;
	using World = DotCraftCore.World.World;

	public class C02PacketUseEntity : Packet
	{
		private int field_149567_a;
		private C02PacketUseEntity.Action field_149566_b;
		

		public C02PacketUseEntity()
		{
		}

		public C02PacketUseEntity(Entity p_i45251_1_, C02PacketUseEntity.Action p_i45251_2_)
		{
			this.field_149567_a = p_i45251_1_.EntityId;
			this.field_149566_b = p_i45251_2_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149567_a = p_148837_1_.readInt();
			this.field_149566_b = C02PacketUseEntity.Action.field_151421_c[p_148837_1_.readByte() % C02PacketUseEntity.Action.field_151421_c.length];
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149567_a);
			p_148840_1_.writeByte(this.field_149566_b.field_151418_d);
		}

		public virtual void processPacket(INetHandlerPlayServer p_148833_1_)
		{
			p_148833_1_.processUseEntity(this);
		}

		public virtual Entity func_149564_a(World p_149564_1_)
		{
			return p_149564_1_.getEntityByID(this.field_149567_a);
		}

		public virtual C02PacketUseEntity.Action func_149565_c()
		{
			return this.field_149566_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayServer)p_148833_1_);
		}

		public enum Action
		{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			INTERACT("INTERACT", 0, 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			ATTACK("ATTACK", 1, 1);
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private static final C02PacketUseEntity.Action[] field_151421_c = new C02PacketUseEntity.Action[values().length];
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//			private final int field_151418_d;

			@private static final C02PacketUseEntity.Action[] $VALUES = new C02PacketUseEntity.Action[]{INTERACT, ATTACK
		}
			

			private Action(string p_i45250_1_, int p_i45250_2_, int p_i45250_3_)
			{
				this.field_151418_d = p_i45250_3_;
			}

			static C02PacketUseEntity()
			{
				C02PacketUseEntity.Action[] var0 = values();
				int var1 = var0.Length;

				for (int var2 = 0; var2 < var1; ++var2)
				{
					C02PacketUseEntity.Action var3 = var0[var2];
					field_151421_c[var3.field_151418_d] = var3;
				}
			}
		}
	}

}