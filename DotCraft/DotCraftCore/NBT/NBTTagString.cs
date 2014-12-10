namespace DotCraftCore.NBT
{


	public class NBTTagString : NBTBase
	{
	/// <summary> The string value for the tag (cannot be empty).  </summary>
		private string data;
		private const string __OBFID = "CL_00001228";

		public NBTTagString()
		{
			this.data = "";
		}

		public NBTTagString(string p_i1389_1_)
		{
			this.data = p_i1389_1_;

			if (p_i1389_1_ == null)
			{
				throw new System.ArgumentException("Empty string not allowed");
			}
		}

///    
///     <summary> * Write the actual data contents of the tag, implemented in NBT extension classes </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void write(DataOutput p_74734_1_) throws IOException
		internal override void write(DataOutput p_74734_1_)
		{
			p_74734_1_.writeUTF(this.data);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_) throws IOException
		internal override void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_)
		{
			this.data = p_152446_1_.readUTF();
			p_152446_3_.func_152450_a((long)(16 * this.data.Length));
		}

///    
///     <summary> * Gets the type byte for the tag. </summary>
///     
		public override sbyte Id
		{
			get
			{
				return (sbyte)8;
			}
		}

		public override string ToString()
		{
			return "\"" + this.data + "\"";
		}

///    
///     <summary> * Creates a clone of the tag. </summary>
///     
		public override NBTBase copy()
		{
			return new NBTTagString(this.data);
		}

		public override bool Equals(object p_equals_1_)
		{
			if (!base.Equals(p_equals_1_))
			{
				return false;
			}
			else
			{
				NBTTagString var2 = (NBTTagString)p_equals_1_;
				return this.data == null && var2.data == null || this.data != null && this.data.Equals(var2.data);
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ this.data.GetHashCode();
		}

		public override string func_150285_a_()
		{
			return this.data;
		}
	}

}