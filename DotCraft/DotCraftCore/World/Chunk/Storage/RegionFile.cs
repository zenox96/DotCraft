using System;
using System.Runtime.CompilerServices;
using System.Collections;

namespace DotCraftCore.nWorld.nChunk.nStorage
{

	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;

	public class RegionFile
	{
		private static readonly sbyte[] emptySector = new sbyte[4096];
		private readonly File fileName;
		private RandomAccessFile dataFile;
		private readonly int[] offsets = new int[1024];
		private readonly int[] chunkTimestamps = new int[1024];
		private ArrayList sectorFree;

	/// <summary> McRegion sizeDelta  </summary>
		private int sizeDelta;
		private long lastModified;
		

		public RegionFile(File p_i2001_1_)
		{
			this.fileName = p_i2001_1_;
			this.sizeDelta = 0;

			try
			{
				if (p_i2001_1_.exists())
				{
					this.lastModified = p_i2001_1_.lastModified();
				}

				this.dataFile = new RandomAccessFile(p_i2001_1_, "rw");
				int var2;

				if (this.dataFile.Length < 4096L)
				{
					for (var2 = 0; var2 < 1024; ++var2)
					{
						this.dataFile.writeInt(0);
					}

					for (var2 = 0; var2 < 1024; ++var2)
					{
						this.dataFile.writeInt(0);
					}

					this.sizeDelta += 8192;
				}

				if ((this.dataFile.Length & 4095L) != 0L)
				{
					for (var2 = 0; (long)var2 < (this.dataFile.Length & 4095L); ++var2)
					{
						this.dataFile.write(0);
					}
				}

				var2 = (int)this.dataFile.Length / 4096;
				this.sectorFree = new ArrayList(var2);
				int var3;

				for (var3 = 0; var3 < var2; ++var3)
				{
					this.sectorFree.Add(Convert.ToBoolean(true));
				}

				this.sectorFree[0] = Convert.ToBoolean(false);
				this.sectorFree[1] = Convert.ToBoolean(false);
				this.dataFile.seek(0L);
				int var4;

				for (var3 = 0; var3 < 1024; ++var3)
				{
					var4 = this.dataFile.readInt();
					this.offsets[var3] = var4;

					if (var4 != 0 && (var4 >> 8) + (var4 & 255) <= this.sectorFree.Count)
					{
						for (int var5 = 0; var5 < (var4 & 255); ++var5)
						{
							this.sectorFree[(var4 >> 8) + var5] = Convert.ToBoolean(false);
						}
					}
				}

				for (var3 = 0; var3 < 1024; ++var3)
				{
					var4 = this.dataFile.readInt();
					this.chunkTimestamps[var3] = var4;
				}
			}
			catch (IOException var6)
			{
				var6.printStackTrace();
			}
		}

///    
///     <summary> * args: x, y - get uncompressed chunk stream from the region file </summary>
///     
		[MethodImpl(MethodImplOptions.Synchronized)]
		public virtual DataInputStream getChunkDataInputStream(int p_76704_1_, int p_76704_2_)
		{
			if (this.outOfBounds(p_76704_1_, p_76704_2_))
			{
				return null;
			}
			else
			{
				try
				{
					int var3 = this.getOffset(p_76704_1_, p_76704_2_);

					if (var3 == 0)
					{
						return null;
					}
					else
					{
						int var4 = var3 >> 8;
						int var5 = var3 & 255;

						if (var4 + var5 > this.sectorFree.Count)
						{
							return null;
						}
						else
						{
							this.dataFile.seek((long)(var4 * 4096));
							int var6 = this.dataFile.readInt();

							if (var6 > 4096 * var5)
							{
								return null;
							}
							else if (var6 <= 0)
							{
								return null;
							}
							else
							{
								sbyte var7 = this.dataFile.readByte();
								sbyte[] var8;

								if (var7 == 1)
								{
									var8 = new sbyte[var6 - 1];
									this.dataFile.read(var8);
									return new DataInputStream(new BufferedInputStream(new GZIPInputStream(new ByteArrayInputStream(var8))));
								}
								else if (var7 == 2)
								{
									var8 = new sbyte[var6 - 1];
									this.dataFile.read(var8);
									return new DataInputStream(new BufferedInputStream(new InflaterInputStream(new ByteArrayInputStream(var8))));
								}
								else
								{
									return null;
								}
							}
						}
					}
				}
				catch (IOException var9)
				{
					return null;
				}
			}
		}

///    
///     <summary> * args: x, z - get an output stream used to write chunk data, data is on disk when the returned stream is closed </summary>
///     
		public virtual DataOutputStream getChunkDataOutputStream(int p_76710_1_, int p_76710_2_)
		{
			return this.outOfBounds(p_76710_1_, p_76710_2_) ? null : new DataOutputStream(new DeflaterOutputStream(new RegionFile.ChunkBuffer(p_76710_1_, p_76710_2_)));
		}

///    
///     <summary> * args: x, z, data, length - write chunk data at (x, z) to disk </summary>
///     
		[MethodImpl(MethodImplOptions.Synchronized)]
		protected internal virtual void write(int p_76706_1_, int p_76706_2_, sbyte[] p_76706_3_, int p_76706_4_)
		{
			try
			{
				int var5 = this.getOffset(p_76706_1_, p_76706_2_);
				int var6 = var5 >> 8;
				int var7 = var5 & 255;
				int var8 = (p_76706_4_ + 5) / 4096 + 1;

				if (var8 >= 256)
				{
					return;
				}

				if (var6 != 0 && var7 == var8)
				{
					this.write(var6, p_76706_3_, p_76706_4_);
				}
				else
				{
					int var9;

					for (var9 = 0; var9 < var7; ++var9)
					{
						this.sectorFree[var6 + var9] = Convert.ToBoolean(true);
					}

					var9 = this.sectorFree.IndexOf(Convert.ToBoolean(true));
					int var10 = 0;
					int var11;

					if (var9 != -1)
					{
						for (var11 = var9; var11 < this.sectorFree.Count; ++var11)
						{
							if (var10 != 0)
							{
								if ((bool)((bool?)this.sectorFree.get(var11)))
								{
									++var10;
								}
								else
								{
									var10 = 0;
								}
							}
							else if ((bool)((bool?)this.sectorFree.get(var11)))
							{
								var9 = var11;
								var10 = 1;
							}

							if (var10 >= var8)
							{
								break;
							}
						}
					}

					if (var10 >= var8)
					{
						var6 = var9;
						this.setOffset(p_76706_1_, p_76706_2_, var9 << 8 | var8);

						for (var11 = 0; var11 < var8; ++var11)
						{
							this.sectorFree[var6 + var11] = Convert.ToBoolean(false);
						}

						this.write(var6, p_76706_3_, p_76706_4_);
					}
					else
					{
						this.dataFile.seek(this.dataFile.Length);
						var6 = this.sectorFree.Count;

						for (var11 = 0; var11 < var8; ++var11)
						{
							this.dataFile.write(emptySector);
							this.sectorFree.Add(Convert.ToBoolean(false));
						}

						this.sizeDelta += 4096 * var8;
						this.write(var6, p_76706_3_, p_76706_4_);
						this.setOffset(p_76706_1_, p_76706_2_, var6 << 8 | var8);
					}
				}

				this.setChunkTimestamp(p_76706_1_, p_76706_2_, (int)(MinecraftServer.SystemTimeMillis / 1000L));
			}
			catch (IOException var12)
			{
				var12.printStackTrace();
			}
		}

///    
///     <summary> * args: sectorNumber, data, length - write the chunk data to this RegionFile </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private void write(int p_76712_1_, byte[] p_76712_2_, int p_76712_3_) throws IOException
		private void write(int p_76712_1_, sbyte[] p_76712_2_, int p_76712_3_)
		{
			this.dataFile.seek((long)(p_76712_1_ * 4096));
			this.dataFile.writeInt(p_76712_3_ + 1);
			this.dataFile.writeByte(2);
			this.dataFile.write(p_76712_2_, 0, p_76712_3_);
		}

///    
///     <summary> * args: x, z - check region bounds </summary>
///     
		private bool outOfBounds(int p_76705_1_, int p_76705_2_)
		{
			return p_76705_1_ < 0 || p_76705_1_ >= 32 || p_76705_2_ < 0 || p_76705_2_ >= 32;
		}

///    
///     <summary> * args: x, y - get chunk's offset in region file </summary>
///     
		private int getOffset(int p_76707_1_, int p_76707_2_)
		{
			return this.offsets[p_76707_1_ + p_76707_2_ * 32];
		}

///    
///     <summary> * args: x, z, - true if chunk has been saved / converted </summary>
///     
		public virtual bool isChunkSaved(int p_76709_1_, int p_76709_2_)
		{
			return this.getOffset(p_76709_1_, p_76709_2_) != 0;
		}

///    
///     <summary> * args: x, z, offset - sets the chunk's offset in the region file </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private void setOffset(int p_76711_1_, int p_76711_2_, int p_76711_3_) throws IOException
		private void setOffset(int p_76711_1_, int p_76711_2_, int p_76711_3_)
		{
			this.offsets[p_76711_1_ + p_76711_2_ * 32] = p_76711_3_;
			this.dataFile.seek((long)((p_76711_1_ + p_76711_2_ * 32) * 4));
			this.dataFile.writeInt(p_76711_3_);
		}

///    
///     <summary> * args: x, z, timestamp - sets the chunk's write timestamp </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private void setChunkTimestamp(int p_76713_1_, int p_76713_2_, int p_76713_3_) throws IOException
		private void setChunkTimestamp(int p_76713_1_, int p_76713_2_, int p_76713_3_)
		{
			this.chunkTimestamps[p_76713_1_ + p_76713_2_ * 32] = p_76713_3_;
			this.dataFile.seek((long)(4096 + (p_76713_1_ + p_76713_2_ * 32) * 4));
			this.dataFile.writeInt(p_76713_3_);
		}

///    
///     <summary> * close this RegionFile and prevent further writes </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void close() throws IOException
		public virtual void close()
		{
			if (this.dataFile != null)
			{
				this.dataFile.close();
			}
		}

		internal class ChunkBuffer : ByteArrayOutputStream
		{
			private int chunkX;
			private int chunkZ;
			

			public ChunkBuffer(int p_i2000_2_, int p_i2000_3_) : base(8096)
			{
				this.chunkX = p_i2000_2_;
				this.chunkZ = p_i2000_3_;
			}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void close() throws IOException
			public virtual void close()
			{
				RegionFile.write(this.chunkX, this.chunkZ, this.buf, this.count);
			}
		}
	}

}