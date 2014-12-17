using System;

namespace DotCraftCore.nNetwork.nPlay.nClient
{

	using Entity = DotCraftCore.entity.Entity;
	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayServer = DotCraftCore.nNetwork.nPlay.INetHandlerPlayServer;

	public class C0APacketAnimation : Packet
	{
		private int field_149424_a;
		private int field_149423_b;
		

		public C0APacketAnimation()
		{
		}

		public C0APacketAnimation(Entity p_i45238_1_, int p_i45238_2_)
		{
			this.field_149424_a = p_i45238_1_.EntityId;
			this.field_149423_b = p_i45238_2_;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149424_a = p_148837_1_.readInt();
			this.field_149423_b = p_148837_1_.readByte();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149424_a);
			p_148840_1_.writeByte(this.field_149423_b);
		}

		public virtual void processPacket(INetHandlerPlayServer p_148833_1_)
		{
			p_148833_1_.processAnimation(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("id={0:D}, type={1:D}", new object[] {Convert.ToInt32(this.field_149424_a), Convert.ToInt32(this.field_149423_b)});
		}

		public virtual int func_149421_d()
		{
			return this.field_149423_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayServer)p_148833_1_);
		}
	}

}