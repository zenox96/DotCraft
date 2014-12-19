using System.Collections;

namespace DotCraftCore.nNetwork.nPlay.nServer
{

	using ItemStack = DotCraftCore.nItem.ItemStack;
	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.nNetwork.nPlay.INetHandlerPlayClient;

	public class S30PacketWindowItems : Packet
	{
		private int field_148914_a;
		private ItemStack[] field_148913_b;
		

		public S30PacketWindowItems()
		{
		}

		public S30PacketWindowItems(int p_i45186_1_, IList p_i45186_2_)
		{
			this.field_148914_a = p_i45186_1_;
			this.field_148913_b = new ItemStack[p_i45186_2_.Count];

			for (int var3 = 0; var3 < this.field_148913_b.Length; ++var3)
			{
				ItemStack var4 = (ItemStack)p_i45186_2_[var3];
				this.field_148913_b[var3] = var4 == null ? null : var4.copy();
			}
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_148914_a = p_148837_1_.readUnsignedByte();
			short var2 = p_148837_1_.readShort();
			this.field_148913_b = new ItemStack[var2];

			for (int var3 = 0; var3 < var2; ++var3)
			{
				this.field_148913_b[var3] = p_148837_1_.readItemStackFromBuffer();
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeByte(this.field_148914_a);
			p_148840_1_.writeShort(this.field_148913_b.Length);
			ItemStack[] var2 = this.field_148913_b;
			int var3 = var2.Length;

			for (int var4 = 0; var4 < var3; ++var4)
			{
				ItemStack var5 = var2[var4];
				p_148840_1_.writeItemStackToBuffer(var5);
			}
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleWindowItems(this);
		}

		public virtual int func_148911_c()
		{
			return this.field_148914_a;
		}

		public virtual ItemStack[] func_148910_d()
		{
			return this.field_148913_b;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}