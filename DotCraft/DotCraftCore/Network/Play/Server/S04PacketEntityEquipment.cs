using System;

namespace DotCraftCore.Network.Play.Server
{

	using ItemStack = DotCraftCore.Item.ItemStack;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;

	public class S04PacketEntityEquipment : Packet
	{
		private int field_149394_a;
		private int field_149392_b;
		private ItemStack field_149393_c;
		

		public S04PacketEntityEquipment()
		{
		}

		public S04PacketEntityEquipment(int p_i45221_1_, int p_i45221_2_, ItemStack p_i45221_3_)
		{
			this.field_149394_a = p_i45221_1_;
			this.field_149392_b = p_i45221_2_;
			this.field_149393_c = p_i45221_3_ == null ? null : p_i45221_3_.copy();
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149394_a = p_148837_1_.readInt();
			this.field_149392_b = p_148837_1_.readShort();
			this.field_149393_c = p_148837_1_.readItemStackFromBuffer();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149394_a);
			p_148840_1_.writeShort(this.field_149392_b);
			p_148840_1_.writeItemStackToBuffer(this.field_149393_c);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleEntityEquipment(this);
		}

		public virtual ItemStack func_149390_c()
		{
			return this.field_149393_c;
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("entity={0:D}, slot={1:D}, item={2}", new object[] {Convert.ToInt32(this.field_149394_a), Convert.ToInt32(this.field_149392_b), this.field_149393_c});
		}

		public virtual int func_149389_d()
		{
			return this.field_149394_a;
		}

		public virtual int func_149388_e()
		{
			return this.field_149392_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}