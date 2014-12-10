using System;

namespace DotCraftCore.Network.Play.Client
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayServer = DotCraftCore.Network.Play.INetHandlerPlayServer;

	public class C11PacketEnchantItem : Packet
	{
		private int field_149541_a;
		private int field_149540_b;
		private const string __OBFID = "CL_00001352";

		public C11PacketEnchantItem()
		{
		}

		public C11PacketEnchantItem(int p_i45245_1_, int p_i45245_2_)
		{
			this.field_149541_a = p_i45245_1_;
			this.field_149540_b = p_i45245_2_;
		}

		public virtual void processPacket(INetHandlerPlayServer p_148833_1_)
		{
			p_148833_1_.processEnchantItem(this);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149541_a = p_148837_1_.readByte();
			this.field_149540_b = p_148837_1_.readByte();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeByte(this.field_149541_a);
			p_148840_1_.writeByte(this.field_149540_b);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("id={0:D}, button={1:D}", new object[] {Convert.ToInt32(this.field_149541_a), Convert.ToInt32(this.field_149540_b)});
		}

		public virtual int func_149539_c()
		{
			return this.field_149541_a;
		}

		public virtual int func_149537_d()
		{
			return this.field_149540_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayServer)p_148833_1_);
		}
	}

}