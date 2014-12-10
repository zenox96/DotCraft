using System;

namespace DotCraftCore.Network.Play.Client
{

	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayServer = DotCraftCore.Network.Play.INetHandlerPlayServer;

	public class C0EPacketClickWindow : Packet
	{
		private int field_149554_a;
		private int field_149552_b;
		private int field_149553_c;
		private short field_149550_d;
		private ItemStack field_149551_e;
		private int field_149549_f;
		private const string __OBFID = "CL_00001353";

		public C0EPacketClickWindow()
		{
		}

		public C0EPacketClickWindow(int p_i45246_1_, int p_i45246_2_, int p_i45246_3_, int p_i45246_4_, ItemStack p_i45246_5_, short p_i45246_6_)
		{
			this.field_149554_a = p_i45246_1_;
			this.field_149552_b = p_i45246_2_;
			this.field_149553_c = p_i45246_3_;
			this.field_149551_e = p_i45246_5_ != null ? p_i45246_5_.copy() : null;
			this.field_149550_d = p_i45246_6_;
			this.field_149549_f = p_i45246_4_;
		}

		public virtual void processPacket(INetHandlerPlayServer p_148833_1_)
		{
			p_148833_1_.processClickWindow(this);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149554_a = p_148837_1_.readByte();
			this.field_149552_b = p_148837_1_.readShort();
			this.field_149553_c = p_148837_1_.readByte();
			this.field_149550_d = p_148837_1_.readShort();
			this.field_149549_f = p_148837_1_.readByte();
			this.field_149551_e = p_148837_1_.readItemStackFromBuffer();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeByte(this.field_149554_a);
			p_148840_1_.writeShort(this.field_149552_b);
			p_148840_1_.writeByte(this.field_149553_c);
			p_148840_1_.writeShort(this.field_149550_d);
			p_148840_1_.writeByte(this.field_149549_f);
			p_148840_1_.writeItemStackToBuffer(this.field_149551_e);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return this.field_149551_e != null ? string.Format("id={0:D}, slot={1:D}, button={2:D}, type={3:D}, itemid={4:D}, itemcount={5:D}, itemaux={6:D}", new object[] {Convert.ToInt32(this.field_149554_a), Convert.ToInt32(this.field_149552_b), Convert.ToInt32(this.field_149553_c), Convert.ToInt32(this.field_149549_f), Convert.ToInt32(Item.getIdFromItem(this.field_149551_e.Item)), Convert.ToInt32(this.field_149551_e.stackSize), Convert.ToInt32(this.field_149551_e.ItemDamage)}): string.Format("id={0:D}, slot={1:D}, button={2:D}, type={3:D}, itemid=-1", new object[] {Convert.ToInt32(this.field_149554_a), Convert.ToInt32(this.field_149552_b), Convert.ToInt32(this.field_149553_c), Convert.ToInt32(this.field_149549_f)});
		}

		public virtual int func_149548_c()
		{
			return this.field_149554_a;
		}

		public virtual int func_149544_d()
		{
			return this.field_149552_b;
		}

		public virtual int func_149543_e()
		{
			return this.field_149553_c;
		}

		public virtual short func_149547_f()
		{
			return this.field_149550_d;
		}

		public virtual ItemStack func_149546_g()
		{
			return this.field_149551_e;
		}

		public virtual int func_149542_h()
		{
			return this.field_149549_f;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayServer)p_148833_1_);
		}
	}

}