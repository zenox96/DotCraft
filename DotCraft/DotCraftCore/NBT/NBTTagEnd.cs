namespace DotCraftCore.nNBT
{


	public class NBTTagEnd : NBTBase
	{
		

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_) throws IOException
		internal override void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_)
		{
		}

///    
///     <summary> * Write the actual data contents of the tag, implemented in NBT extension classes </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void write(DataOutput p_74734_1_) throws IOException
		internal override void write(DataOutput p_74734_1_)
		{
		}

///    
///     <summary> * Gets the type byte for the tag. </summary>
///     
		public override sbyte Id
		{
			get
			{
				return (sbyte)0;
			}
		}

		public override string ToString()
		{
			return "END";
		}

///    
///     <summary> * Creates a clone of the tag. </summary>
///     
		public override NBTBase copy()
		{
			return new NBTTagEnd();
		}
	}

}