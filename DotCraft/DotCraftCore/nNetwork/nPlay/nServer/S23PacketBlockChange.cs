using System;

namespace DotCraftCore.nNetwork.nPlay.nServer
{

	using Block = DotCraftCore.nBlock.Block;
	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.nNetwork.nPlay.INetHandlerPlayClient;
	using World = DotCraftCore.nWorld.World;

	public class S23PacketBlockChange : Packet
	{
		private int field_148887_a;
		private int field_148885_b;
		private int field_148886_c;
		private Block field_148883_d;
		private int field_148884_e;
		

		public S23PacketBlockChange()
		{
		}

		public S23PacketBlockChange(int p_i45177_1_, int p_i45177_2_, int p_i45177_3_, World p_i45177_4_)
		{
			this.field_148887_a = p_i45177_1_;
			this.field_148885_b = p_i45177_2_;
			this.field_148886_c = p_i45177_3_;
			this.field_148883_d = p_i45177_4_.getBlock(p_i45177_1_, p_i45177_2_, p_i45177_3_);
			this.field_148884_e = p_i45177_4_.getBlockMetadata(p_i45177_1_, p_i45177_2_, p_i45177_3_);
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_148887_a = p_148837_1_.readInt();
			this.field_148885_b = p_148837_1_.readUnsignedByte();
			this.field_148886_c = p_148837_1_.readInt();
			this.field_148883_d = Block.getBlockById(p_148837_1_.readVarIntFromBuffer());
			this.field_148884_e = p_148837_1_.readUnsignedByte();
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_148887_a);
			p_148840_1_.writeByte(this.field_148885_b);
			p_148840_1_.writeInt(this.field_148886_c);
			p_148840_1_.writeVarIntToBuffer(Block.getIdFromBlock(this.field_148883_d));
			p_148840_1_.writeByte(this.field_148884_e);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleBlockChange(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("type={0:D}, data={1:D}, x={2:D}, y={3:D}, z={4:D}", new object[] {Convert.ToInt32(Block.getIdFromBlock(this.field_148883_d)), Convert.ToInt32(this.field_148884_e), Convert.ToInt32(this.field_148887_a), Convert.ToInt32(this.field_148885_b), Convert.ToInt32(this.field_148886_c)});
		}

		public virtual Block func_148880_c()
		{
			return this.field_148883_d;
		}

		public virtual int func_148879_d()
		{
			return this.field_148887_a;
		}

		public virtual int func_148878_e()
		{
			return this.field_148885_b;
		}

		public virtual int func_148877_f()
		{
			return this.field_148886_c;
		}

		public virtual int func_148881_g()
		{
			return this.field_148884_e;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}