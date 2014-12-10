namespace DotCraftCore.NBT
{

	using MathHelper = DotCraftCore.Util.MathHelper;

	public class NBTTagFloat : NBTBase.NBTPrimitive
	{
	/// <summary> The float value for the tag.  </summary>
		private float data;
		private const string __OBFID = "CL_00001220";

		internal NBTTagFloat()
		{
		}

		public NBTTagFloat(float p_i45131_1_)
		{
			this.data = p_i45131_1_;
		}

///    
///     <summary> * Write the actual data contents of the tag, implemented in NBT extension classes </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void write(DataOutput p_74734_1_) throws IOException
		internal virtual void write(DataOutput p_74734_1_)
		{
			p_74734_1_.writeFloat(this.data);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_) throws IOException
		internal virtual void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_)
		{
			p_152446_3_.func_152450_a(32L);
			this.data = p_152446_1_.readFloat();
		}

///    
///     <summary> * Gets the type byte for the tag. </summary>
///     
		public virtual sbyte Id
		{
			get
			{
				return (sbyte)5;
			}
		}

		public override string ToString()
		{
			return "" + this.data + "f";
		}

///    
///     <summary> * Creates a clone of the tag. </summary>
///     
		public virtual NBTBase copy()
		{
			return new NBTTagFloat(this.data);
		}

		public override bool Equals(object p_equals_1_)
		{
			if (base.Equals(p_equals_1_))
			{
				NBTTagFloat var2 = (NBTTagFloat)p_equals_1_;
				return this.data == var2.data;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ float.floatToIntBits(this.data);
		}

		public virtual long func_150291_c()
		{
			return (long)this.data;
		}

		public virtual int func_150287_d()
		{
			return MathHelper.floor_float(this.data);
		}

		public virtual short func_150289_e()
		{
			return (short)(MathHelper.floor_float(this.data) & 65535);
		}

		public virtual sbyte func_150290_f()
		{
			return (sbyte)(MathHelper.floor_float(this.data) & 255);
		}

		public virtual double func_150286_g()
		{
			return (double)this.data;
		}

		public virtual float func_150288_h()
		{
			return this.data;
		}
	}

}