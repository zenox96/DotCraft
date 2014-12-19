using System;

namespace DotCraftCore.nNetwork.nPlay.nServer
{

	using INetHandler = DotCraftCore.nNetwork.INetHandler;
	using Packet = DotCraftCore.nNetwork.Packet;
	using PacketBuffer = DotCraftCore.nNetwork.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.nNetwork.nPlay.INetHandlerPlayClient;
	using Chunk = DotCraftCore.nWorld.nChunk.Chunk;
	using NibbleArray = DotCraftCore.nWorld.nChunk.NibbleArray;
	using ExtendedBlockStorage = DotCraftCore.nWorld.nChunk.nStorage.ExtendedBlockStorage;

	public class S21PacketChunkData : Packet
	{
		private int field_149284_a;
		private int field_149282_b;
		private int field_149283_c;
		private int field_149280_d;
		private sbyte[] field_149281_e;
		private sbyte[] field_149278_f;
		private bool field_149279_g;
		private int field_149285_h;
		private static sbyte[] field_149286_i = new sbyte[196864];
		

		public S21PacketChunkData()
		{
		}

		public S21PacketChunkData(Chunk p_i45196_1_, bool p_i45196_2_, int p_i45196_3_)
		{
			this.field_149284_a = p_i45196_1_.xPosition;
			this.field_149282_b = p_i45196_1_.zPosition;
			this.field_149279_g = p_i45196_2_;
			S21PacketChunkData.Extracted var4 = func_149269_a(p_i45196_1_, p_i45196_2_, p_i45196_3_);
			Deflater var5 = new Deflater(-1);
			this.field_149280_d = var4.field_150281_c;
			this.field_149283_c = var4.field_150280_b;

			try
			{
				this.field_149278_f = var4.field_150282_a;
				var5.setInput(var4.field_150282_a, 0, var4.field_150282_a.Length);
				var5.finish();
				this.field_149281_e = new sbyte[var4.field_150282_a.Length];
				this.field_149285_h = var5.deflate(this.field_149281_e);
			}
			finally
			{
				var5.end();
			}
		}

		public static int func_149275_c()
		{
			return 196864;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			this.field_149284_a = p_148837_1_.readInt();
			this.field_149282_b = p_148837_1_.readInt();
			this.field_149279_g = p_148837_1_.readBoolean();
			this.field_149283_c = p_148837_1_.readShort();
			this.field_149280_d = p_148837_1_.readShort();
			this.field_149285_h = p_148837_1_.readInt();

			if (field_149286_i.Length < this.field_149285_h)
			{
				field_149286_i = new sbyte[this.field_149285_h];
			}

			p_148837_1_.readBytes(field_149286_i, 0, this.field_149285_h);
			int var2 = 0;
			int var3;

			for (var3 = 0; var3 < 16; ++var3)
			{
				var2 += this.field_149283_c >> var3 & 1;
			}

			var3 = 12288 * var2;

			if (this.field_149279_g)
			{
				var3 += 256;
			}

			this.field_149278_f = new sbyte[var3];
			Inflater var4 = new Inflater();
			var4.setInput(field_149286_i, 0, this.field_149285_h);

			try
			{
				var4.inflate(this.field_149278_f);
			}
			catch (DataFormatException var9)
			{
				throw new IOException("Bad compressed data format");
			}
			finally
			{
				var4.end();
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeInt(this.field_149284_a);
			p_148840_1_.writeInt(this.field_149282_b);
			p_148840_1_.writeBoolean(this.field_149279_g);
			p_148840_1_.writeShort((short)(this.field_149283_c & 65535));
			p_148840_1_.writeShort((short)(this.field_149280_d & 65535));
			p_148840_1_.writeInt(this.field_149285_h);
			p_148840_1_.writeBytes(this.field_149281_e, 0, this.field_149285_h);
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleChunkData(this);
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
//JAVA TO VB & C# CONVERTER TODO TASK: The following line has a Java format specifier which cannot be directly translated to .NET:
//ORIGINAL LINE: return String.format("x=%d, z=%d, full=%b, sects=%d, add=%d, size=%d", new Object[] {Integer.valueOf(this.field_149284_a), Integer.valueOf(this.field_149282_b), Boolean.valueOf(this.field_149279_g), Integer.valueOf(this.field_149283_c), Integer.valueOf(this.field_149280_d), Integer.valueOf(this.field_149285_h)});
			return string.Format("x=%d, z=%d, full=%b, sects=%d, add=%d, size=%d", new object[] {Convert.ToInt32(this.field_149284_a), Convert.ToInt32(this.field_149282_b), Convert.ToBoolean(this.field_149279_g), Convert.ToInt32(this.field_149283_c), Convert.ToInt32(this.field_149280_d), Convert.ToInt32(this.field_149285_h)});
		}

		public virtual sbyte[] func_149272_d()
		{
			return this.field_149278_f;
		}

		public static S21PacketChunkData.Extracted func_149269_a(Chunk p_149269_0_, bool p_149269_1_, int p_149269_2_)
		{
			int var3 = 0;
			ExtendedBlockStorage[] var4 = p_149269_0_.BlockStorageArray;
			int var5 = 0;
			S21PacketChunkData.Extracted var6 = new S21PacketChunkData.Extracted();
			sbyte[] var7 = field_149286_i;

			if (p_149269_1_)
			{
				p_149269_0_.sendUpdates = true;
			}

			int var8;

			for (var8 = 0; var8 < var4.Length; ++var8)
			{
				if (var4[var8] != null && (!p_149269_1_ || !var4[var8].Empty) && (p_149269_2_ & 1 << var8) != 0)
				{
					var6.field_150280_b |= 1 << var8;

					if (var4[var8].BlockMSBArray != null)
					{
						var6.field_150281_c |= 1 << var8;
						++var5;
					}
				}
			}

			for (var8 = 0; var8 < var4.Length; ++var8)
			{
				if (var4[var8] != null && (!p_149269_1_ || !var4[var8].Empty) && (p_149269_2_ & 1 << var8) != 0)
				{
					sbyte[] var9 = var4[var8].BlockLSBArray;
					Array.Copy(var9, 0, var7, var3, var9.Length);
					var3 += var9.Length;
				}
			}

			NibbleArray var11;

			for (var8 = 0; var8 < var4.Length; ++var8)
			{
				if (var4[var8] != null && (!p_149269_1_ || !var4[var8].Empty) && (p_149269_2_ & 1 << var8) != 0)
				{
					var11 = var4[var8].MetadataArray;
					Array.Copy(var11.data, 0, var7, var3, var11.data.length);
					var3 += var11.data.length;
				}
			}

			for (var8 = 0; var8 < var4.Length; ++var8)
			{
				if (var4[var8] != null && (!p_149269_1_ || !var4[var8].Empty) && (p_149269_2_ & 1 << var8) != 0)
				{
					var11 = var4[var8].BlocklightArray;
					Array.Copy(var11.data, 0, var7, var3, var11.data.length);
					var3 += var11.data.length;
				}
			}

			if (!p_149269_0_.worldObj.provider.hasNoSky)
			{
				for (var8 = 0; var8 < var4.Length; ++var8)
				{
					if (var4[var8] != null && (!p_149269_1_ || !var4[var8].Empty) && (p_149269_2_ & 1 << var8) != 0)
					{
						var11 = var4[var8].SkylightArray;
						Array.Copy(var11.data, 0, var7, var3, var11.data.length);
						var3 += var11.data.length;
					}
				}
			}

			if (var5 > 0)
			{
				for (var8 = 0; var8 < var4.Length; ++var8)
				{
					if (var4[var8] != null && (!p_149269_1_ || !var4[var8].Empty) && var4[var8].BlockMSBArray != null && (p_149269_2_ & 1 << var8) != 0)
					{
						var11 = var4[var8].BlockMSBArray;
						Array.Copy(var11.data, 0, var7, var3, var11.data.length);
						var3 += var11.data.length;
					}
				}
			}

			if (p_149269_1_)
			{
				sbyte[] var10 = p_149269_0_.BiomeArray;
				Array.Copy(var10, 0, var7, var3, var10.Length);
				var3 += var10.Length;
			}

			var6.field_150282_a = new sbyte[var3];
			Array.Copy(var7, 0, var6.field_150282_a, 0, var3);
			return var6;
		}

		public virtual int func_149273_e()
		{
			return this.field_149284_a;
		}

		public virtual int func_149271_f()
		{
			return this.field_149282_b;
		}

		public virtual int func_149276_g()
		{
			return this.field_149283_c;
		}

		public virtual int func_149270_h()
		{
			return this.field_149280_d;
		}

		public virtual bool func_149274_i()
		{
			return this.field_149279_g;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}

		public class Extracted
		{
			public sbyte[] field_150282_a;
			public int field_150280_b;
			public int field_150281_c;
			
		}
	}

}