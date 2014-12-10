using System;

namespace DotCraftCore.NBT
{


	public class NBTTagIntArray : NBTBase
	{
	/// <summary> The array of saved integers  </summary>
		private int[] intArray;
		private const string __OBFID = "CL_00001221";

		internal NBTTagIntArray()
		{
		}

		public NBTTagIntArray(int[] p_i45132_1_)
		{
			this.intArray = p_i45132_1_;
		}

///    
///     <summary> * Write the actual data contents of the tag, implemented in NBT extension classes </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void write(DataOutput p_74734_1_) throws IOException
		internal override void write(DataOutput p_74734_1_)
		{
			p_74734_1_.writeInt(this.intArray.Length);

			for (int var2 = 0; var2 < this.intArray.Length; ++var2)
			{
				p_74734_1_.writeInt(this.intArray[var2]);
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_) throws IOException
		internal override void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_)
		{
			int var4 = p_152446_1_.readInt();
			p_152446_3_.func_152450_a((long)(32 * var4));
			this.intArray = new int[var4];

			for (int var5 = 0; var5 < var4; ++var5)
			{
				this.intArray[var5] = p_152446_1_.readInt();
			}
		}

///    
///     <summary> * Gets the type byte for the tag. </summary>
///     
		public override sbyte Id
		{
			get
			{
				return (sbyte)11;
			}
		}

		public override string ToString()
		{
			string var1 = "[";
			int[] var2 = this.intArray;
			int var3 = var2.Length;

			for (int var4 = 0; var4 < var3; ++var4)
			{
				int var5 = var2[var4];
				var1 = var1 + var5 + ",";
			}

			return var1 + "]";
		}

///    
///     <summary> * Creates a clone of the tag. </summary>
///     
		public override NBTBase copy()
		{
			int[] var1 = new int[this.intArray.Length];
			Array.Copy(this.intArray, 0, var1, 0, this.intArray.Length);
			return new NBTTagIntArray(var1);
		}

		public override bool Equals(object p_equals_1_)
		{
			return base.Equals(p_equals_1_) ? Array.Equals(this.intArray, ((NBTTagIntArray)p_equals_1_).intArray) : false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ Arrays.GetHashCode(this.intArray);
		}

		public virtual int[] func_150302_c()
		{
			return this.intArray;
		}
	}

}