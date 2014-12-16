using System;

namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S32PacketConfirmTransaction : Packet
	{
		private int field_148894_a;
		private short field_148892_b;
		private bool field_148893_c;
		

		public S32PacketConfirmTransaction()
		{
		}

		public S32PacketConfirmTransaction(int p_i45182_1_, short p_i45182_2_, bool p_i45182_3_)
		{
			this.field_148894_a = p_i45182_1_;
			this.field_148892_b = p_i45182_2_;
			this.field_148893_c = p_i45182_3_;
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleConfirmTransaction(this);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_148894_a = p_148837_1_.readUnsignedByte();
			this.field_148892_b = p_148837_1_.readShort();
			this.field_148893_c = p_148837_1_.readBoolean();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeByte(this.field_148894_a);
			p_148840_1_.writeShort(this.field_148892_b);
			p_148840_1_.writeBoolean(this.field_148893_c);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
//JAVA TO VB & C# CONVERTER TODO TASK: The following line has a Java format specifier which cannot be directly translated to .NET:
//ORIGINAL LINE: return String.format("id=%d, uid=%d, accepted=%b", new Object[] {Integer.valueOf(this.field_148894_a), Short.valueOf(this.field_148892_b), Boolean.valueOf(this.field_148893_c)});
			return string.Format("id=%d, uid=%d, accepted=%b", new object[] {Convert.ToInt32(this.field_148894_a), Convert.ToInt16(this.field_148892_b), Convert.ToBoolean(this.field_148893_c)});
		}

		public virtual int func_148889_c()
		{
			return this.field_148894_a;
		}

		public virtual short func_148890_d()
		{
			return this.field_148892_b;
		}

		public virtual bool func_148888_e()
		{
			return this.field_148893_c;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}