using System;
using System.Collections;
using System.Text;

namespace DotCraftCore.Network.Play.Server
{

	using INetHandler = DotCraftCore.Network.INetHandler;
	using Packet = DotCraftCore.Network.Packet;
	using PacketBuffer = DotCraftCore.Network.PacketBuffer;
	using INetHandlerPlayClient = DotCraftCore.Network.Play.INetHandlerPlayClient;
	using Chunk = DotCraftCore.World.Chunk.Chunk;

	public class S26PacketMapChunkBulk : Packet
	{
		private int[] field_149266_a;
		private int[] field_149264_b;
		private int[] field_149265_c;
		private int[] field_149262_d;
		private sbyte[] field_149263_e;
		private sbyte[][] field_149260_f;
		private int field_149261_g;
		private bool field_149267_h;
		private static sbyte[] field_149268_i = new sbyte[0];
		private const string __OBFID = "CL_00001306";

		public S26PacketMapChunkBulk()
		{
		}

		public S26PacketMapChunkBulk(IList p_i45197_1_)
		{
			int var2 = p_i45197_1_.Count;
			this.field_149266_a = new int[var2];
			this.field_149264_b = new int[var2];
			this.field_149265_c = new int[var2];
			this.field_149262_d = new int[var2];
			this.field_149260_f = new sbyte[var2][];
			this.field_149267_h = !p_i45197_1_.Count == 0 && !((Chunk)p_i45197_1_[0]).worldObj.provider.hasNoSky;
			int var3 = 0;

			for (int var4 = 0; var4 < var2; ++var4)
			{
				Chunk var5 = (Chunk)p_i45197_1_[var4];
				S21PacketChunkData.Extracted var6 = S21PacketChunkData.func_149269_a(var5, true, 65535);

				if (field_149268_i.Length < var3 + var6.field_150282_a.Length)
				{
					sbyte[] var7 = new sbyte[var3 + var6.field_150282_a.Length];
					Array.Copy(field_149268_i, 0, var7, 0, field_149268_i.Length);
					field_149268_i = var7;
				}

				Array.Copy(var6.field_150282_a, 0, field_149268_i, var3, var6.field_150282_a.Length);
				var3 += var6.field_150282_a.Length;
				this.field_149266_a[var4] = var5.xPosition;
				this.field_149264_b[var4] = var5.zPosition;
				this.field_149265_c[var4] = var6.field_150280_b;
				this.field_149262_d[var4] = var6.field_150281_c;
				this.field_149260_f[var4] = var6.field_150282_a;
			}

			Deflater var11 = new Deflater(-1);

			try
			{
				var11.setInput(field_149268_i, 0, var3);
				var11.finish();
				this.field_149263_e = new sbyte[var3];
				this.field_149261_g = var11.deflate(this.field_149263_e);
			}
			finally
			{
				var11.end();
			}
		}

		public static int func_149258_c()
		{
			return 5;
		}

///    
///     <summary> * Reads the raw packet data from the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void readPacketData(PacketBuffer p_148837_1_) throws IOException
		public override void readPacketData(PacketBuffer p_148837_1_)
		{
			short var2 = p_148837_1_.readShort();
			this.field_149261_g = p_148837_1_.readInt();
			this.field_149267_h = p_148837_1_.readBoolean();
			this.field_149266_a = new int[var2];
			this.field_149264_b = new int[var2];
			this.field_149265_c = new int[var2];
			this.field_149262_d = new int[var2];
			this.field_149260_f = new sbyte[var2][];

			if (field_149268_i.Length < this.field_149261_g)
			{
				field_149268_i = new sbyte[this.field_149261_g];
			}

			p_148837_1_.readBytes(field_149268_i, 0, this.field_149261_g);
			sbyte[] var3 = new sbyte[S21PacketChunkData.func_149275_c() * var2];
			Inflater var4 = new Inflater();
			var4.setInput(field_149268_i, 0, this.field_149261_g);

			try
			{
				var4.inflate(var3);
			}
			catch (DataFormatException var12)
			{
				throw new IOException("Bad compressed data format");
			}
			finally
			{
				var4.end();
			}

			int var5 = 0;

			for (int var6 = 0; var6 < var2; ++var6)
			{
				this.field_149266_a[var6] = p_148837_1_.readInt();
				this.field_149264_b[var6] = p_148837_1_.readInt();
				this.field_149265_c[var6] = p_148837_1_.readShort();
				this.field_149262_d[var6] = p_148837_1_.readShort();
				int var7 = 0;
				int var8 = 0;
				int var9;

				for (var9 = 0; var9 < 16; ++var9)
				{
					var7 += this.field_149265_c[var6] >> var9 & 1;
					var8 += this.field_149262_d[var6] >> var9 & 1;
				}

				var9 = 2048 * 4 * var7 + 256;
				var9 += 2048 * var8;

				if (this.field_149267_h)
				{
					var9 += 2048 * var7;
				}

				this.field_149260_f[var6] = new sbyte[var9];
				Array.Copy(var3, var5, this.field_149260_f[var6], 0, var9);
				var5 += var9;
			}
		}

///    
///     <summary> * Writes the raw packet data to the data stream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writePacketData(PacketBuffer p_148840_1_) throws IOException
		public override void writePacketData(PacketBuffer p_148840_1_)
		{
			p_148840_1_.writeShort(this.field_149266_a.Length);
			p_148840_1_.writeInt(this.field_149261_g);
			p_148840_1_.writeBoolean(this.field_149267_h);
			p_148840_1_.writeBytes(this.field_149263_e, 0, this.field_149261_g);

			for (int var2 = 0; var2 < this.field_149266_a.Length; ++var2)
			{
				p_148840_1_.writeInt(this.field_149266_a[var2]);
				p_148840_1_.writeInt(this.field_149264_b[var2]);
				p_148840_1_.writeShort((short)(this.field_149265_c[var2] & 65535));
				p_148840_1_.writeShort((short)(this.field_149262_d[var2] & 65535));
			}
		}

		public virtual void processPacket(INetHandlerPlayClient p_148833_1_)
		{
			p_148833_1_.handleMapChunkBulk(this);
		}

		public virtual int func_149255_a(int p_149255_1_)
		{
			return this.field_149266_a[p_149255_1_];
		}

		public virtual int func_149253_b(int p_149253_1_)
		{
			return this.field_149264_b[p_149253_1_];
		}

		public virtual int func_149254_d()
		{
			return this.field_149266_a.Length;
		}

		public virtual sbyte[] func_149256_c(int p_149256_1_)
		{
			return this.field_149260_f[p_149256_1_];
		}

///    
///     <summary> * Returns a string formatted as comma separated [field]=[value] values. Used by Minecraft for logging purposes. </summary>
///     
		public override string serialize()
		{
			StringBuilder var1 = new StringBuilder();

			for (int var2 = 0; var2 < this.field_149266_a.Length; ++var2)
			{
				if (var2 > 0)
				{
					var1.Append(", ");
				}

				var1.Append(string.Format("{{x={0:D}, z={1:D}, sections={2:D}, adds={3:D}, data={4:D}}}", new object[] {Convert.ToInt32(this.field_149266_a[var2]), Convert.ToInt32(this.field_149264_b[var2]), Convert.ToInt32(this.field_149265_c[var2]), Convert.ToInt32(this.field_149262_d[var2]), Convert.ToInt32(this.field_149260_f[var2].Length)}));
			}

			return string.Format("size={0:D}, chunks={1:D}[{2}]", new object[] {Convert.ToInt32(this.field_149261_g), Convert.ToInt32(this.field_149266_a.Length), var1});
		}

		public virtual int[] func_149252_e()
		{
			return this.field_149265_c;
		}

		public virtual int[] func_149257_f()
		{
			return this.field_149262_d;
		}

		public override void processPacket(INetHandler p_148833_1_)
		{
			this.processPacket((INetHandlerPlayClient)p_148833_1_);
		}
	}

}