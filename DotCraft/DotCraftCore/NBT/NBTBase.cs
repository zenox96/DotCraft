namespace DotCraftCore.NBT
{


	public abstract class NBTBase
	{
		public static readonly string[] NBTTypes = new string[] {"END", "BYTE", "SHORT", "INT", "LONG", "FLOAT", "DOUBLE", "BYTE[]", "STRING", "LIST", "COMPOUND", "INT[]"};
		

///    
///     <summary> * Write the actual data contents of the tag, implemented in NBT extension classes </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: abstract void write(DataOutput p_74734_1_) throws IOException;
		internal abstract void write(DataOutput p_74734_1_);

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: abstract void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_) throws IOException;
		internal abstract void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_);

		public abstract string ToString();

///    
///     <summary> * Gets the type byte for the tag. </summary>
///     
		public abstract sbyte Id {get;}

		protected internal static NBTBase func_150284_a(sbyte p_150284_0_)
		{
			switch (p_150284_0_)
			{
				case 0:
					return new NBTTagEnd();

				case 1:
					return new NBTTagByte();

				case 2:
					return new NBTTagShort();

				case 3:
					return new NBTTagInt();

				case 4:
					return new NBTTagLong();

				case 5:
					return new NBTTagFloat();

				case 6:
					return new NBTTagDouble();

				case 7:
					return new NBTTagByteArray();

				case 8:
					return new NBTTagString();

				case 9:
					return new NBTTagList();

				case 10:
					return new NBTTagCompound();

				case 11:
					return new NBTTagIntArray();

				default:
					return null;
			}
		}

///    
///     <summary> * Creates a clone of the tag. </summary>
///     
		public abstract NBTBase copy();

		public override bool Equals(object p_equals_1_)
		{
			if (!(p_equals_1_ is NBTBase))
			{
				return false;
			}
			else
			{
				NBTBase var2 = (NBTBase)p_equals_1_;
				return this.Id == var2.Id;
			}
		}

		public override int GetHashCode()
		{
			return this.Id;
		}

		protected internal virtual string func_150285_a_()
		{
			return this.ToString();
		}

		public abstract class NBTPrimitive : NBTBase
		{
			

			public abstract long func_150291_c();

			public abstract int func_150287_d();

			public abstract short func_150289_e();

			public abstract sbyte func_150290_f();

			public abstract double func_150286_g();

			public abstract float func_150288_h();
		}
	}

}