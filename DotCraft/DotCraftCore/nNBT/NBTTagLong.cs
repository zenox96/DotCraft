namespace DotCraftCore.nNBT
{


	public class NBTTagLong : NBTBase.NBTPrimitive
	{
	/// <summary> The long value for the tag.  </summary>
		private long data;
		

		internal NBTTagLong()
		{
		}

		public NBTTagLong(long p_i45134_1_)
		{
			this.data = p_i45134_1_;
		}

///    
///     <summary> * Write the actual data contents of the tag, implemented in NBT extension classes </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void write(DataOutput p_74734_1_) throws IOException
		internal virtual void write(DataOutput p_74734_1_)
		{
			p_74734_1_.writeLong(this.data);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_) throws IOException
		internal virtual void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_)
		{
			p_152446_3_.func_152450_a(64L);
			this.data = p_152446_1_.readLong();
		}

///    
///     <summary> * Gets the type byte for the tag. </summary>
///     
		public virtual sbyte Id
		{
			get
			{
				return (sbyte)4;
			}
		}

		public override string ToString()
		{
			return "" + this.data + "L";
		}

///    
///     <summary> * Creates a clone of the tag. </summary>
///     
		public virtual NBTBase copy()
		{
			return new NBTTagLong(this.data);
		}

		public override bool Equals(object p_equals_1_)
		{
			if (base.Equals(p_equals_1_))
			{
				NBTTagLong var2 = (NBTTagLong)p_equals_1_;
				return this.data == var2.data;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ (int)(this.data ^ (long)((ulong)this.data >> 32));
		}

		public virtual long func_150291_c()
		{
			return this.data;
		}

		public virtual int func_150287_d()
		{
			return (int)(this.data & -1L);
		}

		public virtual short func_150289_e()
		{
			return (short)((int)(this.data & 65535L));
		}

		public virtual sbyte func_150290_f()
		{
			return (sbyte)((int)(this.data & 255L));
		}

		public virtual double func_150286_g()
		{
			return (double)this.data;
		}

		public virtual float func_150288_h()
		{
			return (float)this.data;
		}
	}

}