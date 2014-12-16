using System;

namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S05PacketSpawnPosition : Packet
	{
		private int field_149364_a;
		private int field_149362_b;
		private int field_149363_c;
		

		public S05PacketSpawnPosition()
		{
		}

		public S05PacketSpawnPosition(int p_i45229_1_, int p_i45229_2_, int p_i45229_3_)
		{
			this.field_149364_a = p_i45229_1_;
			this.field_149362_b = p_i45229_2_;
			this.field_149363_c = p_i45229_3_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149364_a = p_148837_1_.readInt();
			this.field_149362_b = p_148837_1_.readInt();
			this.field_149363_c = p_148837_1_.readInt();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149364_a);
			p_148840_1_.writeInt(this.field_149362_b);
			p_148840_1_.writeInt(this.field_149363_c);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleSpawnPosition(this);
		}

///    
///     <summary> * If true, the network manager will process the packet immediately when received, otherwise it will queue it for
///     * processing. Currently true for: Disconnect, LoginSuccess, KeepAlive, ServerQuery/Info, Ping/Pong </summary>
///     
		public override bool hasPriority()
		{
			return false;
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("x={0:D}, y={1:D}, z={2:D}", new object[] {Convert.ToInt32(this.field_149364_a), Convert.ToInt32(this.field_149362_b), Convert.ToInt32(this.field_149363_c)});
		}

		public virtual int func_149360_c()
		{
			return this.field_149364_a;
		}

		public virtual int func_149359_d()
		{
			return this.field_149362_b;
		}

		public virtual int func_149358_e()
		{
			return this.field_149363_c;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}