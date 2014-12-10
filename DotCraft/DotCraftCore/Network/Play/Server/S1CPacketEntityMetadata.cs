using System.Collections;

namespace DotCraftCore.Network.Play.Server
{

	using DataWatcher = DotCraftCore.entity.DataWatcher;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S1CPacketEntityMetadata : Packet
	{
		private int field_149379_a;
		private IList field_149378_b;
		private const string __OBFID = "CL_00001326";

		public S1CPacketEntityMetadata()
		{
		}

		public S1CPacketEntityMetadata(int p_i45217_1_, DataWatcher p_i45217_2_, bool p_i45217_3_)
		{
			this.field_149379_a = p_i45217_1_;

			if (p_i45217_3_)
			{
				this.field_149378_b = p_i45217_2_.AllWatched;
			}
			else
			{
				this.field_149378_b = p_i45217_2_.Changed;
			}
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149379_a = p_148837_1_.readInt();
			this.field_149378_b = DataWatcher.readWatchedListFromPacketBuffer(p_148837_1_);
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149379_a);
			DataWatcher.writeWatchedListToPacketBuffer(this.field_149378_b, p_148840_1_);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleEntityMetadata(this);
		}

		public virtual IList func_149376_c()
		{
			return this.field_149378_b;
		}

		public virtual int func_149375_d()
		{
			return this.field_149379_a;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}