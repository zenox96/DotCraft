namespace DotCraftCore.NBT
{


	public class NBTTagShort : NBTBase.NBTPrimitive
	{
	/// <summary> The short value for the tag.  </summary>
		private short data;
		private const string __OBFID = "CL_00001227";

		public NBTTagShort()
		{
		}

		public NBTTagShort(short p_i45135_1_)
		{
			this.data = p_i45135_1_;
		}

///    
///     <summary> * Write the actual data contents of the tag, implemented in NBT extension classes </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void write(DataOutput p_74734_1_) throws IOException
		internal virtual void write(DataOutput p_74734_1_)
		{
			p_74734_1_.writeShort(this.data);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_) throws IOException
		internal virtual void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_)
		{
			p_152446_3_.func_152450_a(16L);
			this.data = p_152446_1_.readShort();
		}

///    
///     <summary> * Gets the type byte for the tag. </summary>
///     
		public virtual sbyte Id
		{
			get
			{
				return (sbyte)2;
			}
		}

		public override string ToString()
		{
			return "" + this.data + "s";
		}

///    
///     <summary> * Creates a clone of the tag. </summary>
///     
		public virtual NBTBase copy()
		{
			return new NBTTagShort(this.data);
		}

		public override bool Equals(object p_equals_1_)
		{
			if (base.Equals(p_equals_1_))
			{
				NBTTagShort var2 = (NBTTagShort)p_equals_1_;
				return this.data == var2.data;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.data;
		}

		public virtual long func_150291_c()
		{
			return (long)this.data;
		}

		public virtual int func_150287_d()
		{
			return this.data;
		}

		public virtual short func_150289_e()
		{
			return this.data;
		}

		public virtual sbyte func_150290_f()
		{
			return (sbyte)(this.data & 255);
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