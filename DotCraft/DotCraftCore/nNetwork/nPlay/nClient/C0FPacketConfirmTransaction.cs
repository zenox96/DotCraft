using System;

namespace DotCraftCore.nNetwork.nPlay.nClient
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayServer = DotCraftCore.nNetwork.nPlay.INetHandlerPlayServer;

	public class C0FPacketConfirmTransaction : Packet
	{
		private int field_149536_a;
		private short field_149534_b;
		private bool field_149535_c;
		

		public C0FPacketConfirmTransaction()
		{
		}

		public C0FPacketConfirmTransaction(int p_i45244_1_, short p_i45244_2_, bool p_i45244_3_)
		{
			this.field_149536_a = p_i45244_1_;
			this.field_149534_b = p_i45244_2_;
			this.field_149535_c = p_i45244_3_;
		}

		public virtual void processPacket(INetHandlerPlayServer p_148833_1_)
		{
			p_148833_1_.processConfirmTransaction(this);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149536_a = p_148837_1_.readByte();
			this.field_149534_b = p_148837_1_.readShort();
			this.field_149535_c = p_148837_1_.readByte() != 0;
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeByte(this.field_149536_a);
			p_148840_1_.writeShort(this.field_149534_b);
			p_148840_1_.writeByte(this.field_149535_c ? 1 : 0);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
//JAVA TO VB & C# CONVERTER TODO TASK: The following line has a Java format specifier which cannot be directly translated to .NET:
//ORIGINAL LINE: return String.format("id=%d, uid=%d, accepted=%b", new Object[] {Integer.valueOf(this.field_149536_a), Short.valueOf(this.field_149534_b), Boolean.valueOf(this.field_149535_c)});
			return string.Format("id=%d, uid=%d, accepted=%b", new object[] {Convert.ToInt32(this.field_149536_a), Convert.ToInt16(this.field_149534_b), Convert.ToBoolean(this.field_149535_c)});
		}

		public virtual int func_149532_c()
		{
			return this.field_149536_a;
		}

		public virtual short func_149533_d()
		{
			return this.field_149534_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayServer)p_148833_1_);
		}
	}

}