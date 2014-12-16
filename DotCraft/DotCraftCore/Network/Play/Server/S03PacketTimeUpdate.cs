using System;

namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S03PacketTimeUpdate : Packet
	{
		private long field_149369_a;
		private long field_149368_b;
		

		public S03PacketTimeUpdate()
		{
		}

		public S03PacketTimeUpdate(long p_i45230_1_, long p_i45230_3_, bool p_i45230_5_)
		{
			this.field_149369_a = p_i45230_1_;
			this.field_149368_b = p_i45230_3_;

			if (!p_i45230_5_)
			{
				this.field_149368_b = -this.field_149368_b;

				if (this.field_149368_b == 0L)
				{
					this.field_149368_b = -1L;
				}
			}
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149369_a = p_148837_1_.readLong();
			this.field_149368_b = p_148837_1_.readLong();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeLong(this.field_149369_a);
			p_148840_1_.writeLong(this.field_149368_b);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleTimeUpdate(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("time={0:D},dtime={1:D}", new object[] {Convert.ToInt64(this.field_149369_a), Convert.ToInt64(this.field_149368_b)});
		}

		public virtual long func_149366_c()
		{
			return this.field_149369_a;
		}

		public virtual long func_149365_d()
		{
			return this.field_149368_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}