using System;

namespace DotCraftCore.nNetwork.nPlay.nClient
{
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
            var values = Enum.GetValues(typeof(C16PacketClientStatus.EnumState));
            this.field_149437_a = (EnumState)values.GetValue(p_148837_1_.readByte( ) % values.Length);
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeByte((int)this.field_149437_a);
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
            PERFORM_RESPAWN = 0,
            REQUEST_STATS = 1,
            OPEN_INVENTORY_ACHIEVEMENT = 2
        }
	}
}