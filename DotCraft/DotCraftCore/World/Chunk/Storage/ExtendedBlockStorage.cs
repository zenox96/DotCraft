namespace DotCraftCore.World.Chunk.Storage
{

	using Block = DotCraftCore.block.Block;
	using Blocks = DotCraftCore.init.Blocks;
	using NibbleArray = DotCraftCore.World.Chunk.NibbleArray;

	public class ExtendedBlockStorage
	{
///    
///     <summary> * Contains the bottom-most Y block represented by this ExtendedBlockStorage. Typically a multiple of 16. </summary>
///     
		private int yBase;

///    
///     <summary> * A total count of the number of non-air blocks in this block storage's Chunk. </summary>
///     
		private int blockRefCount;

///    
///     <summary> * Contains the number of blocks in this block storage's parent chunk that require random ticking. Used to cull the
///     * Chunk from random tick updates for performance reasons. </summary>
///     
		private int tickRefCount;

///    
///     <summary> * Contains the least significant 8 bits of each block ID belonging to this block storage's parent Chunk. </summary>
///     
		private sbyte[] blockLSBArray;

///    
///     <summary> * Contains the most significant 4 bits of each block ID belonging to this block storage's parent Chunk. </summary>
///     
		private NibbleArray blockMSBArray;

///    
///     <summary> * Stores the metadata associated with blocks in this ExtendedBlockStorage. </summary>
///     
		private NibbleArray blockMetadataArray;

	/// <summary> The NibbleArray containing a block of Block-light data.  </summary>
		private NibbleArray blocklightArray;

	/// <summary> The NibbleArray containing a block of Sky-light data.  </summary>
		private NibbleArray skylightArray;
		private const string __OBFID = "CL_00000375";

		public ExtendedBlockStorage(int p_i1997_1_, bool p_i1997_2_)
		{
			this.yBase = p_i1997_1_;
			this.blockLSBArray = new sbyte[4096];
			this.blockMetadataArray = new NibbleArray(this.blockLSBArray.Length, 4);
			this.blocklightArray = new NibbleArray(this.blockLSBArray.Length, 4);

			if (p_i1997_2_)
			{
				this.skylightArray = new NibbleArray(this.blockLSBArray.Length, 4);
			}
		}

		public virtual Block func_150819_a(int p_150819_1_, int p_150819_2_, int p_150819_3_)
		{
			int var4 = this.blockLSBArray[p_150819_2_ << 8 | p_150819_3_ << 4 | p_150819_1_] & 255;

			if (this.blockMSBArray != null)
			{
				var4 |= this.blockMSBArray.get(p_150819_1_, p_150819_2_, p_150819_3_) << 8;
			}

			return Block.getBlockById(var4);
		}

		public virtual void func_150818_a(int p_150818_1_, int p_150818_2_, int p_150818_3_, Block p_150818_4_)
		{
			int var5 = this.blockLSBArray[p_150818_2_ << 8 | p_150818_3_ << 4 | p_150818_1_] & 255;

			if (this.blockMSBArray != null)
			{
				var5 |= this.blockMSBArray.get(p_150818_1_, p_150818_2_, p_150818_3_) << 8;
			}

			Block var6 = Block.getBlockById(var5);

			if (var6 != Blocks.air)
			{
				--this.blockRefCount;

				if (var6.TickRandomly)
				{
					--this.tickRefCount;
				}
			}

			if (p_150818_4_ != Blocks.air)
			{
				++this.blockRefCount;

				if (p_150818_4_.TickRandomly)
				{
					++this.tickRefCount;
				}
			}

			int var7 = Block.getIdFromBlock(p_150818_4_);
			this.blockLSBArray[p_150818_2_ << 8 | p_150818_3_ << 4 | p_150818_1_] = (sbyte)(var7 & 255);

			if (var7 > 255)
			{
				if (this.blockMSBArray == null)
				{
					this.blockMSBArray = new NibbleArray(this.blockLSBArray.Length, 4);
				}

				this.blockMSBArray.set(p_150818_1_, p_150818_2_, p_150818_3_, (var7 & 3840) >> 8);
			}
			else if (this.blockMSBArray != null)
			{
				this.blockMSBArray.set(p_150818_1_, p_150818_2_, p_150818_3_, 0);
			}
		}

///    
///     <summary> * Returns the metadata associated with the block at the given coordinates in this ExtendedBlockStorage. </summary>
///     
		public virtual int getExtBlockMetadata(int p_76665_1_, int p_76665_2_, int p_76665_3_)
		{
			return this.blockMetadataArray.get(p_76665_1_, p_76665_2_, p_76665_3_);
		}

///    
///     <summary> * Sets the metadata of the Block at the given coordinates in this ExtendedBlockStorage to the given metadata. </summary>
///     
		public virtual void setExtBlockMetadata(int p_76654_1_, int p_76654_2_, int p_76654_3_, int p_76654_4_)
		{
			this.blockMetadataArray.set(p_76654_1_, p_76654_2_, p_76654_3_, p_76654_4_);
		}

///    
///     <summary> * Returns whether or not this block storage's Chunk is fully empty, based on its internal reference count. </summary>
///     
		public virtual bool isEmpty()
		{
			get
			{
				return this.blockRefCount == 0;
			}
		}

///    
///     <summary> * Returns whether or not this block storage's Chunk will require random ticking, used to avoid looping through
///     * random block ticks when there are no blocks that would randomly tick. </summary>
///     
		public virtual bool NeedsRandomTick
		{
			get
			{
				return this.tickRefCount > 0;
			}
		}

///    
///     <summary> * Returns the Y location of this ExtendedBlockStorage. </summary>
///     
		public virtual int YLocation
		{
			get
			{
				return this.yBase;
			}
		}

///    
///     <summary> * Sets the saved Sky-light value in the extended block storage structure. </summary>
///     
		public virtual void setExtSkylightValue(int p_76657_1_, int p_76657_2_, int p_76657_3_, int p_76657_4_)
		{
			this.skylightArray.set(p_76657_1_, p_76657_2_, p_76657_3_, p_76657_4_);
		}

///    
///     <summary> * Gets the saved Sky-light value in the extended block storage structure. </summary>
///     
		public virtual int getExtSkylightValue(int p_76670_1_, int p_76670_2_, int p_76670_3_)
		{
			return this.skylightArray.get(p_76670_1_, p_76670_2_, p_76670_3_);
		}

///    
///     <summary> * Sets the saved Block-light value in the extended block storage structure. </summary>
///     
		public virtual void setExtBlocklightValue(int p_76677_1_, int p_76677_2_, int p_76677_3_, int p_76677_4_)
		{
			this.blocklightArray.set(p_76677_1_, p_76677_2_, p_76677_3_, p_76677_4_);
		}

///    
///     <summary> * Gets the saved Block-light value in the extended block storage structure. </summary>
///     
		public virtual int getExtBlocklightValue(int p_76674_1_, int p_76674_2_, int p_76674_3_)
		{
			return this.blocklightArray.get(p_76674_1_, p_76674_2_, p_76674_3_);
		}

		public virtual void removeInvalidBlocks()
		{
			this.blockRefCount = 0;
			this.tickRefCount = 0;

			for (int var1 = 0; var1 < 16; ++var1)
			{
				for (int var2 = 0; var2 < 16; ++var2)
				{
					for (int var3 = 0; var3 < 16; ++var3)
					{
						Block var4 = this.func_150819_a(var1, var2, var3);

						if (var4 != Blocks.air)
						{
							++this.blockRefCount;

							if (var4.TickRandomly)
							{
								++this.tickRefCount;
							}
						}
					}
				}
			}
		}

		public virtual sbyte[] BlockLSBArray
		{
			get
			{
				return this.blockLSBArray;
			}
			set
			{
				this.blockLSBArray = value;
			}
		}

		public virtual void clearMSBArray()
		{
			this.blockMSBArray = null;
		}

///    
///     <summary> * Returns the block ID MSB (bits 11..8) array for this storage array's Chunk. </summary>
///     
		public virtual NibbleArray BlockMSBArray
		{
			get
			{
				return this.blockMSBArray;
			}
			set
			{
				this.blockMSBArray = value;
			}
		}

		public virtual NibbleArray MetadataArray
		{
			get
			{
				return this.blockMetadataArray;
			}
		}

///    
///     <summary> * Returns the NibbleArray instance containing Block-light data. </summary>
///     
		public virtual NibbleArray BlocklightArray
		{
			get
			{
				return this.blocklightArray;
			}
			set
			{
				this.blocklightArray = value;
			}
		}

///    
///     <summary> * Returns the NibbleArray instance containing Sky-light data. </summary>
///     
		public virtual NibbleArray SkylightArray
		{
			get
			{
				return this.skylightArray;
			}
			set
			{
				this.skylightArray = value;
			}
		}

///    
///     <summary> * Sets the array of block ID least significant bits for this ExtendedBlockStorage. </summary>
///     

///    
///     <summary> * Sets the array of blockID most significant bits (blockMSBArray) for this ExtendedBlockStorage. </summary>
///     

///    
///     <summary> * Sets the NibbleArray of block metadata (blockMetadataArray) for this ExtendedBlockStorage. </summary>
///     
		public virtual NibbleArray BlockMetadataArray
		{
			set
			{
				this.blockMetadataArray = value;
			}
		}

///    
///     <summary> * Sets the NibbleArray instance used for Block-light values in this particular storage block. </summary>
///     

///    
///     <summary> * Sets the NibbleArray instance used for Sky-light values in this particular storage block. </summary>
///     

///    
///     <summary> * Called by a Chunk to initialize the MSB array if getBlockMSBArray returns null. Returns the newly-created
///     * NibbleArray instance. </summary>
///     
		public virtual NibbleArray createBlockMSBArray()
		{
			this.blockMSBArray = new NibbleArray(this.blockLSBArray.Length, 4);
			return this.blockMSBArray;
		}
	}

}