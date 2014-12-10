using System;

namespace DotCraftCore.Network
{

	using Charsets = com.google.common.base.Charsets;
	using ByteBuf = io.netty.buffer.ByteBuf;
	using ByteBufAllocator = io.netty.buffer.ByteBufAllocator;
	using ByteBufProcessor = io.netty.buffer.ByteBufProcessor;
	using ReferenceCounted = io.netty.util.ReferenceCounted;
	using Item = DotCraftCore.Item.Item;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using CompressedStreamTools = DotCraftCore.NBT.CompressedStreamTools;
	using NBTSizeTracker = DotCraftCore.NBT.NBTSizeTracker;
	using NBTTagCompound = DotCraftCore.NBT.NBTTagCompound;

	public class PacketBuffer : ByteBuf
	{
		private readonly ByteBuf field_150794_a;
		private const string __OBFID = "CL_00001251";

		public PacketBuffer(ByteBuf p_i45154_1_)
		{
			this.field_150794_a = p_i45154_1_;
		}

///    
///     <summary> * Calculates the number of bytes required to fit the supplied int (0-5) if it were to be read/written using
///     * readVarIntFromBuffer or writeVarIntToBuffer </summary>
///     
		public static int getVarIntSize(int p_150790_0_)
		{
			return (p_150790_0_ & -128) == 0 ? 1 : ((p_150790_0_ & -16384) == 0 ? 2 : ((p_150790_0_ & -2097152) == 0 ? 3 : ((p_150790_0_ & -268435456) == 0 ? 4 : 5)));
		}

///    
///     <summary> * Reads a compressed int from the buffer. To do so it maximally reads 5 byte-sized chunks whose most significant
///     * bit dictates whether another byte should be read. </summary>
///     
		public virtual int readVarIntFromBuffer()
		{
			int var1 = 0;
			int var2 = 0;
			sbyte var3;

			do
			{
				var3 = this.readByte();
				var1 |= (var3 & 127) << var2++ * 7;

				if (var2 > 5)
				{
					throw new Exception("VarInt too big");
				}
			}
			while ((var3 & 128) == 128);

			return var1;
		}

///    
///     <summary> * Writes a compressed int to the buffer. The smallest number of bytes to fit the passed int will be written. Of
///     * each such byte only 7 bits will be used to describe the actual value since its most significant bit dictates
///     * whether the next byte is part of that same int. Micro-optimization for int values that are expected to have
///     * values below 128. </summary>
///     
		public virtual void writeVarIntToBuffer(int p_150787_1_)
		{
			while ((p_150787_1_ & -128) != 0)
			{
				this.writeByte(p_150787_1_ & 127 | 128);
				p_150787_1_ >>>= 7;
			}

			this.writeByte(p_150787_1_);
		}

///    
///     <summary> * Writes a compressed NBTTagCompound to this buffer </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writeNBTTagCompoundToBuffer(NBTTagCompound p_150786_1_) throws IOException
		public virtual void writeNBTTagCompoundToBuffer(NBTTagCompound p_150786_1_)
		{
			if (p_150786_1_ == null)
			{
				this.writeShort(-1);
			}
			else
			{
				sbyte[] var2 = CompressedStreamTools.compress(p_150786_1_);
				this.writeShort((short)var2.Length);
				this.writeBytes(var2);
			}
		}

///    
///     <summary> * Reads a compressed NBTTagCompound from this buffer </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public NBTTagCompound readNBTTagCompoundFromBuffer() throws IOException
		public virtual NBTTagCompound readNBTTagCompoundFromBuffer()
		{
			short var1 = this.readShort();

			if (var1 < 0)
			{
				return null;
			}
			else
			{
				sbyte[] var2 = new sbyte[var1];
				this.readBytes(var2);
				return CompressedStreamTools.func_152457_a(var2, new NBTSizeTracker(2097152L));
			}
		}

///    
///     <summary> * Writes the ItemStack's ID (short), then size (byte), then damage. (short) </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writeItemStackToBuffer(ItemStack p_150788_1_) throws IOException
		public virtual void writeItemStackToBuffer(ItemStack p_150788_1_)
		{
			if (p_150788_1_ == null)
			{
				this.writeShort(-1);
			}
			else
			{
				this.writeShort(Item.getIdFromItem(p_150788_1_.Item));
				this.writeByte(p_150788_1_.stackSize);
				this.writeShort(p_150788_1_.ItemDamage);
				NBTTagCompound var2 = null;

				if (p_150788_1_.Item.Damageable || p_150788_1_.Item.ShareTag)
				{
					var2 = p_150788_1_.stackTagCompound;
				}

				this.writeNBTTagCompoundToBuffer(var2);
			}
		}

///    
///     <summary> * Reads an ItemStack from this buffer </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ItemStack readItemStackFromBuffer() throws IOException
		public virtual ItemStack readItemStackFromBuffer()
		{
			ItemStack var1 = null;
			short var2 = this.readShort();

			if (var2 >= 0)
			{
				sbyte var3 = this.readByte();
				short var4 = this.readShort();
				var1 = new ItemStack(Item.getItemById(var2), var3, var4);
				var1.stackTagCompound = this.readNBTTagCompoundFromBuffer();
			}

			return var1;
		}

///    
///     <summary> * Reads a string from this buffer. Expected parameter is maximum allowed string length. Will throw IOException if
///     * string length exceeds this value! </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public String readStringFromBuffer(int p_150789_1_) throws IOException
		public virtual string readStringFromBuffer(int p_150789_1_)
		{
			int var2 = this.readVarIntFromBuffer();

			if (var2 > p_150789_1_ * 4)
			{
				throw new IOException("The received encoded string buffer length is longer than maximum allowed (" + var2 + " > " + p_150789_1_ * 4 + ")");
			}
			else if (var2 < 0)
			{
				throw new IOException("The received encoded string buffer length is less than zero! Weird string!");
			}
			else
			{
				string var3 = new string(this.readBytes(var2).array(), Charsets.UTF_8);

				if (var3.Length > p_150789_1_)
				{
					throw new IOException("The received string length is longer than maximum allowed (" + var2 + " > " + p_150789_1_ + ")");
				}
				else
				{
					return var3;
				}
			}
		}

///    
///     <summary> * Writes a (UTF-8 encoded) String to this buffer. Will throw IOException if String length exceeds 32767 bytes </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void writeStringToBuffer(String p_150785_1_) throws IOException
		public virtual void writeStringToBuffer(string p_150785_1_)
		{
			sbyte[] var2 = p_150785_1_.getBytes(Charsets.UTF_8);

			if (var2.Length > 32767)
			{
				throw new IOException("String too big (was " + p_150785_1_.Length + " bytes encoded, max " + 32767 + ")");
			}
			else
			{
				this.writeVarIntToBuffer(var2.Length);
				this.writeBytes(var2);
			}
		}

		public virtual int capacity()
		{
			return this.field_150794_a.capacity();
		}

		public virtual ByteBuf capacity(int p_capacity_1_)
		{
			return this.field_150794_a.capacity(p_capacity_1_);
		}

		public virtual int maxCapacity()
		{
			return this.field_150794_a.maxCapacity();
		}

		public virtual ByteBufAllocator alloc()
		{
			return this.field_150794_a.alloc();
		}

		public virtual ByteOrder order()
		{
			return this.field_150794_a.order();
		}

		public virtual ByteBuf order(ByteOrder p_order_1_)
		{
			return this.field_150794_a.order(p_order_1_);
		}

		public virtual ByteBuf unwrap()
		{
			return this.field_150794_a.unwrap();
		}

		public virtual bool isDirect()
		{
			get
			{
				return this.field_150794_a.Direct;
			}
		}

		public virtual int readerIndex()
		{
			return this.field_150794_a.readerIndex();
		}

		public virtual ByteBuf readerIndex(int p_readerIndex_1_)
		{
			return this.field_150794_a.readerIndex(p_readerIndex_1_);
		}

		public virtual int writerIndex()
		{
			return this.field_150794_a.writerIndex();
		}

		public virtual ByteBuf writerIndex(int p_writerIndex_1_)
		{
			return this.field_150794_a.writerIndex(p_writerIndex_1_);
		}

		public virtual ByteBuf setIndex(int p_setIndex_1_, int p_setIndex_2_)
		{
			return this.field_150794_a.setIndex(p_setIndex_1_, p_setIndex_2_);
		}

		public virtual int readableBytes()
		{
			return this.field_150794_a.readableBytes();
		}

		public virtual int writableBytes()
		{
			return this.field_150794_a.writableBytes();
		}

		public virtual int maxWritableBytes()
		{
			return this.field_150794_a.maxWritableBytes();
		}

		public virtual bool isReadable()
		{
			get
			{
				return this.field_150794_a.Readable;
			}
		}

		public virtual bool isReadable(int p_isReadable_1_)
		{
			return this.field_150794_a.isReadable(p_isReadable_1_);
		}

		public virtual bool isWritable()
		{
			get
			{
				return this.field_150794_a.Writable;
			}
		}

		public virtual bool isWritable(int p_isWritable_1_)
		{
			return this.field_150794_a.isWritable(p_isWritable_1_);
		}

		public virtual ByteBuf clear()
		{
			return this.field_150794_a.clear();
		}

		public virtual ByteBuf markReaderIndex()
		{
			return this.field_150794_a.markReaderIndex();
		}

		public virtual ByteBuf resetReaderIndex()
		{
			return this.field_150794_a.resetReaderIndex();
		}

		public virtual ByteBuf markWriterIndex()
		{
			return this.field_150794_a.markWriterIndex();
		}

		public virtual ByteBuf resetWriterIndex()
		{
			return this.field_150794_a.resetWriterIndex();
		}

		public virtual ByteBuf discardReadBytes()
		{
			return this.field_150794_a.discardReadBytes();
		}

		public virtual ByteBuf discardSomeReadBytes()
		{
			return this.field_150794_a.discardSomeReadBytes();
		}

		public virtual ByteBuf ensureWritable(int p_ensureWritable_1_)
		{
			return this.field_150794_a.ensureWritable(p_ensureWritable_1_);
		}

		public virtual int ensureWritable(int p_ensureWritable_1_, bool p_ensureWritable_2_)
		{
			return this.field_150794_a.ensureWritable(p_ensureWritable_1_, p_ensureWritable_2_);
		}

		public virtual bool getBoolean(int p_getBoolean_1_)
		{
			return this.field_150794_a.getBoolean(p_getBoolean_1_);
		}

		public virtual sbyte getByte(int p_getByte_1_)
		{
			return this.field_150794_a.getByte(p_getByte_1_);
		}

		public virtual short getUnsignedByte(int p_getUnsignedByte_1_)
		{
			return this.field_150794_a.getUnsignedByte(p_getUnsignedByte_1_);
		}

		public virtual short getShort(int p_getShort_1_)
		{
			return this.field_150794_a.getShort(p_getShort_1_);
		}

		public virtual int getUnsignedShort(int p_getUnsignedShort_1_)
		{
			return this.field_150794_a.getUnsignedShort(p_getUnsignedShort_1_);
		}

		public virtual int getMedium(int p_getMedium_1_)
		{
			return this.field_150794_a.getMedium(p_getMedium_1_);
		}

		public virtual int getUnsignedMedium(int p_getUnsignedMedium_1_)
		{
			return this.field_150794_a.getUnsignedMedium(p_getUnsignedMedium_1_);
		}

		public virtual int getInt(int p_getInt_1_)
		{
			return this.field_150794_a.getInt(p_getInt_1_);
		}

		public virtual long getUnsignedInt(int p_getUnsignedInt_1_)
		{
			return this.field_150794_a.getUnsignedInt(p_getUnsignedInt_1_);
		}

		public virtual long getLong(int p_getLong_1_)
		{
			return this.field_150794_a.getLong(p_getLong_1_);
		}

		public virtual char getChar(int p_getChar_1_)
		{
			return this.field_150794_a.getChar(p_getChar_1_);
		}

		public virtual float getFloat(int p_getFloat_1_)
		{
			return this.field_150794_a.getFloat(p_getFloat_1_);
		}

		public virtual double getDouble(int p_getDouble_1_)
		{
			return this.field_150794_a.getDouble(p_getDouble_1_);
		}

		public virtual ByteBuf getBytes(int p_getBytes_1_, ByteBuf p_getBytes_2_)
		{
			return this.field_150794_a.getBytes(p_getBytes_1_, p_getBytes_2_);
		}

		public virtual ByteBuf getBytes(int p_getBytes_1_, ByteBuf p_getBytes_2_, int p_getBytes_3_)
		{
			return this.field_150794_a.getBytes(p_getBytes_1_, p_getBytes_2_, p_getBytes_3_);
		}

		public virtual ByteBuf getBytes(int p_getBytes_1_, ByteBuf p_getBytes_2_, int p_getBytes_3_, int p_getBytes_4_)
		{
			return this.field_150794_a.getBytes(p_getBytes_1_, p_getBytes_2_, p_getBytes_3_, p_getBytes_4_);
		}

		public virtual ByteBuf getBytes(int p_getBytes_1_, sbyte[] p_getBytes_2_)
		{
			return this.field_150794_a.getBytes(p_getBytes_1_, p_getBytes_2_);
		}

		public virtual ByteBuf getBytes(int p_getBytes_1_, sbyte[] p_getBytes_2_, int p_getBytes_3_, int p_getBytes_4_)
		{
			return this.field_150794_a.getBytes(p_getBytes_1_, p_getBytes_2_, p_getBytes_3_, p_getBytes_4_);
		}

		public virtual ByteBuf getBytes(int p_getBytes_1_, ByteBuffer p_getBytes_2_)
		{
			return this.field_150794_a.getBytes(p_getBytes_1_, p_getBytes_2_);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ByteBuf getBytes(int p_getBytes_1_, OutputStream p_getBytes_2_, int p_getBytes_3_) throws IOException
		public virtual ByteBuf getBytes(int p_getBytes_1_, OutputStream p_getBytes_2_, int p_getBytes_3_)
		{
			return this.field_150794_a.getBytes(p_getBytes_1_, p_getBytes_2_, p_getBytes_3_);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public int getBytes(int p_getBytes_1_, GatheringByteChannel p_getBytes_2_, int p_getBytes_3_) throws IOException
		public virtual int getBytes(int p_getBytes_1_, GatheringByteChannel p_getBytes_2_, int p_getBytes_3_)
		{
			return this.field_150794_a.getBytes(p_getBytes_1_, p_getBytes_2_, p_getBytes_3_);
		}

		public virtual ByteBuf setBoolean(int p_setBoolean_1_, bool p_setBoolean_2_)
		{
			return this.field_150794_a.setBoolean(p_setBoolean_1_, p_setBoolean_2_);
		}

		public virtual ByteBuf setByte(int p_setByte_1_, int p_setByte_2_)
		{
			return this.field_150794_a.setByte(p_setByte_1_, p_setByte_2_);
		}

		public virtual ByteBuf setShort(int p_setShort_1_, int p_setShort_2_)
		{
			return this.field_150794_a.setShort(p_setShort_1_, p_setShort_2_);
		}

		public virtual ByteBuf setMedium(int p_setMedium_1_, int p_setMedium_2_)
		{
			return this.field_150794_a.setMedium(p_setMedium_1_, p_setMedium_2_);
		}

		public virtual ByteBuf setInt(int p_setInt_1_, int p_setInt_2_)
		{
			return this.field_150794_a.setInt(p_setInt_1_, p_setInt_2_);
		}

		public virtual ByteBuf setLong(int p_setLong_1_, long p_setLong_2_)
		{
			return this.field_150794_a.setLong(p_setLong_1_, p_setLong_2_);
		}

		public virtual ByteBuf setChar(int p_setChar_1_, int p_setChar_2_)
		{
			return this.field_150794_a.setChar(p_setChar_1_, p_setChar_2_);
		}

		public virtual ByteBuf setFloat(int p_setFloat_1_, float p_setFloat_2_)
		{
			return this.field_150794_a.setFloat(p_setFloat_1_, p_setFloat_2_);
		}

		public virtual ByteBuf setDouble(int p_setDouble_1_, double p_setDouble_2_)
		{
			return this.field_150794_a.setDouble(p_setDouble_1_, p_setDouble_2_);
		}

		public virtual ByteBuf setBytes(int p_setBytes_1_, ByteBuf p_setBytes_2_)
		{
			return this.field_150794_a.setBytes(p_setBytes_1_, p_setBytes_2_);
		}

		public virtual ByteBuf setBytes(int p_setBytes_1_, ByteBuf p_setBytes_2_, int p_setBytes_3_)
		{
			return this.field_150794_a.setBytes(p_setBytes_1_, p_setBytes_2_, p_setBytes_3_);
		}

		public virtual ByteBuf setBytes(int p_setBytes_1_, ByteBuf p_setBytes_2_, int p_setBytes_3_, int p_setBytes_4_)
		{
			return this.field_150794_a.setBytes(p_setBytes_1_, p_setBytes_2_, p_setBytes_3_, p_setBytes_4_);
		}

		public virtual ByteBuf setBytes(int p_setBytes_1_, sbyte[] p_setBytes_2_)
		{
			return this.field_150794_a.setBytes(p_setBytes_1_, p_setBytes_2_);
		}

		public virtual ByteBuf setBytes(int p_setBytes_1_, sbyte[] p_setBytes_2_, int p_setBytes_3_, int p_setBytes_4_)
		{
			return this.field_150794_a.setBytes(p_setBytes_1_, p_setBytes_2_, p_setBytes_3_, p_setBytes_4_);
		}

		public virtual ByteBuf setBytes(int p_setBytes_1_, ByteBuffer p_setBytes_2_)
		{
			return this.field_150794_a.setBytes(p_setBytes_1_, p_setBytes_2_);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public int setBytes(int p_setBytes_1_, InputStream p_setBytes_2_, int p_setBytes_3_) throws IOException
		public virtual int setBytes(int p_setBytes_1_, InputStream p_setBytes_2_, int p_setBytes_3_)
		{
			return this.field_150794_a.setBytes(p_setBytes_1_, p_setBytes_2_, p_setBytes_3_);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public int setBytes(int p_setBytes_1_, ScatteringByteChannel p_setBytes_2_, int p_setBytes_3_) throws IOException
		public virtual int setBytes(int p_setBytes_1_, ScatteringByteChannel p_setBytes_2_, int p_setBytes_3_)
		{
			return this.field_150794_a.setBytes(p_setBytes_1_, p_setBytes_2_, p_setBytes_3_);
		}

		public virtual ByteBuf setZero(int p_setZero_1_, int p_setZero_2_)
		{
			return this.field_150794_a.setZero(p_setZero_1_, p_setZero_2_);
		}

		public virtual bool readBoolean()
		{
			return this.field_150794_a.readBoolean();
		}

		public virtual sbyte readByte()
		{
			return this.field_150794_a.readByte();
		}

		public virtual short readUnsignedByte()
		{
			return this.field_150794_a.readUnsignedByte();
		}

		public virtual short readShort()
		{
			return this.field_150794_a.readShort();
		}

		public virtual int readUnsignedShort()
		{
			return this.field_150794_a.readUnsignedShort();
		}

		public virtual int readMedium()
		{
			return this.field_150794_a.readMedium();
		}

		public virtual int readUnsignedMedium()
		{
			return this.field_150794_a.readUnsignedMedium();
		}

		public virtual int readInt()
		{
			return this.field_150794_a.readInt();
		}

		public virtual long readUnsignedInt()
		{
			return this.field_150794_a.readUnsignedInt();
		}

		public virtual long readLong()
		{
			return this.field_150794_a.readLong();
		}

		public virtual char readChar()
		{
			return this.field_150794_a.readChar();
		}

		public virtual float readFloat()
		{
			return this.field_150794_a.readFloat();
		}

		public virtual double readDouble()
		{
			return this.field_150794_a.readDouble();
		}

		public virtual ByteBuf readBytes(int p_readBytes_1_)
		{
			return this.field_150794_a.readBytes(p_readBytes_1_);
		}

		public virtual ByteBuf readSlice(int p_readSlice_1_)
		{
			return this.field_150794_a.readSlice(p_readSlice_1_);
		}

		public virtual ByteBuf readBytes(ByteBuf p_readBytes_1_)
		{
			return this.field_150794_a.readBytes(p_readBytes_1_);
		}

		public virtual ByteBuf readBytes(ByteBuf p_readBytes_1_, int p_readBytes_2_)
		{
			return this.field_150794_a.readBytes(p_readBytes_1_, p_readBytes_2_);
		}

		public virtual ByteBuf readBytes(ByteBuf p_readBytes_1_, int p_readBytes_2_, int p_readBytes_3_)
		{
			return this.field_150794_a.readBytes(p_readBytes_1_, p_readBytes_2_, p_readBytes_3_);
		}

		public virtual ByteBuf readBytes(sbyte[] p_readBytes_1_)
		{
			return this.field_150794_a.readBytes(p_readBytes_1_);
		}

		public virtual ByteBuf readBytes(sbyte[] p_readBytes_1_, int p_readBytes_2_, int p_readBytes_3_)
		{
			return this.field_150794_a.readBytes(p_readBytes_1_, p_readBytes_2_, p_readBytes_3_);
		}

		public virtual ByteBuf readBytes(ByteBuffer p_readBytes_1_)
		{
			return this.field_150794_a.readBytes(p_readBytes_1_);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public ByteBuf readBytes(OutputStream p_readBytes_1_, int p_readBytes_2_) throws IOException
		public virtual ByteBuf readBytes(OutputStream p_readBytes_1_, int p_readBytes_2_)
		{
			return this.field_150794_a.readBytes(p_readBytes_1_, p_readBytes_2_);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public int readBytes(GatheringByteChannel p_readBytes_1_, int p_readBytes_2_) throws IOException
		public virtual int readBytes(GatheringByteChannel p_readBytes_1_, int p_readBytes_2_)
		{
			return this.field_150794_a.readBytes(p_readBytes_1_, p_readBytes_2_);
		}

		public virtual ByteBuf skipBytes(int p_skipBytes_1_)
		{
			return this.field_150794_a.skipBytes(p_skipBytes_1_);
		}

		public virtual ByteBuf writeBoolean(bool p_writeBoolean_1_)
		{
			return this.field_150794_a.writeBoolean(p_writeBoolean_1_);
		}

		public virtual ByteBuf writeByte(int p_writeByte_1_)
		{
			return this.field_150794_a.writeByte(p_writeByte_1_);
		}

		public virtual ByteBuf writeShort(int p_writeShort_1_)
		{
			return this.field_150794_a.writeShort(p_writeShort_1_);
		}

		public virtual ByteBuf writeMedium(int p_writeMedium_1_)
		{
			return this.field_150794_a.writeMedium(p_writeMedium_1_);
		}

		public virtual ByteBuf writeInt(int p_writeInt_1_)
		{
			return this.field_150794_a.writeInt(p_writeInt_1_);
		}

		public virtual ByteBuf writeLong(long p_writeLong_1_)
		{
			return this.field_150794_a.writeLong(p_writeLong_1_);
		}

		public virtual ByteBuf writeChar(int p_writeChar_1_)
		{
			return this.field_150794_a.writeChar(p_writeChar_1_);
		}

		public virtual ByteBuf writeFloat(float p_writeFloat_1_)
		{
			return this.field_150794_a.writeFloat(p_writeFloat_1_);
		}

		public virtual ByteBuf writeDouble(double p_writeDouble_1_)
		{
			return this.field_150794_a.writeDouble(p_writeDouble_1_);
		}

		public virtual ByteBuf writeBytes(ByteBuf p_writeBytes_1_)
		{
			return this.field_150794_a.writeBytes(p_writeBytes_1_);
		}

		public virtual ByteBuf writeBytes(ByteBuf p_writeBytes_1_, int p_writeBytes_2_)
		{
			return this.field_150794_a.writeBytes(p_writeBytes_1_, p_writeBytes_2_);
		}

		public virtual ByteBuf writeBytes(ByteBuf p_writeBytes_1_, int p_writeBytes_2_, int p_writeBytes_3_)
		{
			return this.field_150794_a.writeBytes(p_writeBytes_1_, p_writeBytes_2_, p_writeBytes_3_);
		}

		public virtual ByteBuf writeBytes(sbyte[] p_writeBytes_1_)
		{
			return this.field_150794_a.writeBytes(p_writeBytes_1_);
		}

		public virtual ByteBuf writeBytes(sbyte[] p_writeBytes_1_, int p_writeBytes_2_, int p_writeBytes_3_)
		{
			return this.field_150794_a.writeBytes(p_writeBytes_1_, p_writeBytes_2_, p_writeBytes_3_);
		}

		public virtual ByteBuf writeBytes(ByteBuffer p_writeBytes_1_)
		{
			return this.field_150794_a.writeBytes(p_writeBytes_1_);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public int writeBytes(InputStream p_writeBytes_1_, int p_writeBytes_2_) throws IOException
		public virtual int writeBytes(InputStream p_writeBytes_1_, int p_writeBytes_2_)
		{
			return this.field_150794_a.writeBytes(p_writeBytes_1_, p_writeBytes_2_);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public int writeBytes(ScatteringByteChannel p_writeBytes_1_, int p_writeBytes_2_) throws IOException
		public virtual int writeBytes(ScatteringByteChannel p_writeBytes_1_, int p_writeBytes_2_)
		{
			return this.field_150794_a.writeBytes(p_writeBytes_1_, p_writeBytes_2_);
		}

		public virtual ByteBuf writeZero(int p_writeZero_1_)
		{
			return this.field_150794_a.writeZero(p_writeZero_1_);
		}

		public virtual int indexOf(int p_indexOf_1_, int p_indexOf_2_, sbyte p_indexOf_3_)
		{
			return this.field_150794_a.IndexOf(p_indexOf_1_, p_indexOf_2_, p_indexOf_3_);
		}

		public virtual int bytesBefore(sbyte p_bytesBefore_1_)
		{
			return this.field_150794_a.bytesBefore(p_bytesBefore_1_);
		}

		public virtual int bytesBefore(int p_bytesBefore_1_, sbyte p_bytesBefore_2_)
		{
			return this.field_150794_a.bytesBefore(p_bytesBefore_1_, p_bytesBefore_2_);
		}

		public virtual int bytesBefore(int p_bytesBefore_1_, int p_bytesBefore_2_, sbyte p_bytesBefore_3_)
		{
			return this.field_150794_a.bytesBefore(p_bytesBefore_1_, p_bytesBefore_2_, p_bytesBefore_3_);
		}

		public virtual int forEachByte(ByteBufProcessor p_forEachByte_1_)
		{
			return this.field_150794_a.forEachByte(p_forEachByte_1_);
		}

		public virtual int forEachByte(int p_forEachByte_1_, int p_forEachByte_2_, ByteBufProcessor p_forEachByte_3_)
		{
			return this.field_150794_a.forEachByte(p_forEachByte_1_, p_forEachByte_2_, p_forEachByte_3_);
		}

		public virtual int forEachByteDesc(ByteBufProcessor p_forEachByteDesc_1_)
		{
			return this.field_150794_a.forEachByteDesc(p_forEachByteDesc_1_);
		}

		public virtual int forEachByteDesc(int p_forEachByteDesc_1_, int p_forEachByteDesc_2_, ByteBufProcessor p_forEachByteDesc_3_)
		{
			return this.field_150794_a.forEachByteDesc(p_forEachByteDesc_1_, p_forEachByteDesc_2_, p_forEachByteDesc_3_);
		}

		public virtual ByteBuf copy()
		{
			return this.field_150794_a.copy();
		}

		public virtual ByteBuf copy(int p_copy_1_, int p_copy_2_)
		{
			return this.field_150794_a.copy(p_copy_1_, p_copy_2_);
		}

		public virtual ByteBuf slice()
		{
			return this.field_150794_a.slice();
		}

		public virtual ByteBuf slice(int p_slice_1_, int p_slice_2_)
		{
			return this.field_150794_a.slice(p_slice_1_, p_slice_2_);
		}

		public virtual ByteBuf duplicate()
		{
			return this.field_150794_a.duplicate();
		}

		public virtual int nioBufferCount()
		{
			return this.field_150794_a.nioBufferCount();
		}

		public virtual ByteBuffer nioBuffer()
		{
			return this.field_150794_a.nioBuffer();
		}

		public virtual ByteBuffer nioBuffer(int p_nioBuffer_1_, int p_nioBuffer_2_)
		{
			return this.field_150794_a.nioBuffer(p_nioBuffer_1_, p_nioBuffer_2_);
		}

		public virtual ByteBuffer internalNioBuffer(int p_internalNioBuffer_1_, int p_internalNioBuffer_2_)
		{
			return this.field_150794_a.internalNioBuffer(p_internalNioBuffer_1_, p_internalNioBuffer_2_);
		}

		public virtual ByteBuffer[] nioBuffers()
		{
			return this.field_150794_a.nioBuffers();
		}

		public virtual ByteBuffer[] nioBuffers(int p_nioBuffers_1_, int p_nioBuffers_2_)
		{
			return this.field_150794_a.nioBuffers(p_nioBuffers_1_, p_nioBuffers_2_);
		}

		public virtual bool hasArray()
		{
			return this.field_150794_a.hasArray();
		}

		public virtual sbyte[] array()
		{
			return this.field_150794_a.array();
		}

		public virtual int arrayOffset()
		{
			return this.field_150794_a.arrayOffset();
		}

		public virtual bool hasMemoryAddress()
		{
			return this.field_150794_a.hasMemoryAddress();
		}

		public virtual long memoryAddress()
		{
			return this.field_150794_a.memoryAddress();
		}

		public override string ToString(Charset p_toString_1_)
		{
			return this.field_150794_a.ToString(p_toString_1_);
		}

		public override string ToString(int p_toString_1_, int p_toString_2_, Charset p_toString_3_)
		{
			return this.field_150794_a.ToString(p_toString_1_, p_toString_2_, p_toString_3_);
		}

		public override int GetHashCode()
		{
			return this.field_150794_a.GetHashCode();
		}

		public override bool Equals(object p_equals_1_)
		{
			return this.field_150794_a.Equals(p_equals_1_);
		}

		public virtual int compareTo(ByteBuf p_compareTo_1_)
		{
			return this.field_150794_a.CompareTo(p_compareTo_1_);
		}

		public override string ToString()
		{
			return this.field_150794_a.ToString();
		}

		public virtual ByteBuf retain(int p_retain_1_)
		{
			return this.field_150794_a.retain(p_retain_1_);
		}

		public virtual ByteBuf retain()
		{
			return this.field_150794_a.retain();
		}

		public virtual int refCnt()
		{
			return this.field_150794_a.refCnt();
		}

		public virtual bool release()
		{
			return this.field_150794_a.release();
		}

		public virtual bool release(int p_release_1_)
		{
			return this.field_150794_a.release(p_release_1_);
		}
	}

}