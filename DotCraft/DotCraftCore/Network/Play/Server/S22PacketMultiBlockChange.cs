using System;

namespace DotCraftCore.nNetwork.nPlay.nServer
{

	using Block = DotCraftCore.nBlock.Block;
	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.nNetwork.nPlay.INetHandlerPlayClient;
	using ChunkCoordIntPair = DotCraftCore.nWorld.ChunkCoordIntPair;
	using Chunk = DotCraftCore.nWorld.nChunk.Chunk;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class S22PacketMultiBlockChange : Packet
	{
		private static readonly Logger logger = LogManager.Logger;
		private ChunkCoordIntPair field_148925_b;
		private sbyte[] field_148926_c;
		private int field_148924_d;
		

		public S22PacketMultiBlockChange()
		{
		}

		public S22PacketMultiBlockChange(int p_i45181_1_, short[] p_i45181_2_, Chunk p_i45181_3_)
		{
			this.field_148925_b = new ChunkCoordIntPair(p_i45181_3_.xPosition, p_i45181_3_.zPosition);
			this.field_148924_d = p_i45181_1_;
			int var4 = 4 * p_i45181_1_;

			try
			{
				ByteArrayOutputStream var5 = new ByteArrayOutputStream(var4);
				DataOutputStream var6 = new DataOutputStream(var5);

				for (int var7 = 0; var7 < p_i45181_1_; ++var7)
				{
					int var8 = p_i45181_2_[var7] >> 12 & 15;
					int var9 = p_i45181_2_[var7] >> 8 & 15;
					int var10 = p_i45181_2_[var7] & 255;
					var6.writeShort(p_i45181_2_[var7]);
					var6.writeShort((short)((Block.getIdFromBlock(p_i45181_3_.func_150810_a(var8, var10, var9)) & 4095) << 4 | p_i45181_3_.getBlockMetadata(var8, var10, var9) & 15));
				}

				this.field_148926_c = var5.toByteArray();

				if (this.field_148926_c.Length != var4)
				{
					throw new Exception("Expected length " + var4 + " doesn\'t match received length " + this.field_148926_c.Length);
				}
			}
			catch (IOException var11)
			{
				logger.error("Couldn\'t create bulk block update packet", var11);
				this.field_148926_c = null;
			}
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_148925_b = new ChunkCoordIntPair(p_148837_1_.readInt(), p_148837_1_.readInt());
			this.field_148924_d = p_148837_1_.readShort() & 65535;
			int var2 = p_148837_1_.readInt();

			if (var2 > 0)
			{
				this.field_148926_c = new sbyte[var2];
				p_148837_1_.readBytes(this.field_148926_c);
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_148925_b.chunkXPos);
			p_148840_1_.writeInt(this.field_148925_b.chunkZPos);
			p_148840_1_.writeShort((short)this.field_148924_d);

			if (this.field_148926_c != null)
			{
				p_148840_1_.writeInt(this.field_148926_c.Length);
				p_148840_1_.writeBytes(this.field_148926_c);
			}
			else
			{
				p_148840_1_.writeInt(0);
			}
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleMultiBlockChange(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			return string.Format("xc={0:D}, zc={1:D}, count={2:D}", new object[] {Convert.ToInt32(this.field_148925_b.chunkXPos), Convert.ToInt32(this.field_148925_b.chunkZPos), Convert.ToInt32(this.field_148924_d)});
		}

		public virtual ChunkCoordIntPair func_148920_c()
		{
			return this.field_148925_b;
		}

		public virtual sbyte[] func_148921_d()
		{
			return this.field_148926_c;
		}

		public virtual int func_148922_e()
		{
			return this.field_148924_d;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}