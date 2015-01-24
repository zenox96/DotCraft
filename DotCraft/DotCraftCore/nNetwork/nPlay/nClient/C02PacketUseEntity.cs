using DotCraftCore.nEntity;
using DotCraftCore.nWorld;
using System;

namespace DotCraftCore.nNetwork.nPlay.nClient
{
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
            var values = Enum.GetValues(typeof(C02PacketUseEntity.Action));
			this.field_149566_b = (Action)values.GetValue(p_148837_1_.readByte() % values.Length);
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149567_a);
			p_148840_1_.writeByte((int)this.field_149566_b);
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
            INTERACT = 0,
            ATTACK = 1
        }
	}
}