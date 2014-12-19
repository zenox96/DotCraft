using System;

namespace DotCraftCore.nNBT
{


	public class NBTTagByteArray : NBTBase
	{
	/// <summary> The byte array stored in the tag.  </summary>
		private sbyte[] byteArray;
		

		internal NBTTagByteArray()
		{
		}

		public NBTTagByteArray(sbyte[] p_i45128_1_)
		{
			this.byteArray = p_i45128_1_;
		}

///    
///     <summary> * Write the actual data contents of the tag, implemented in NBT extension classes </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void write(DataOutput p_74734_1_) throws IOException
		internal override void write(DataOutput p_74734_1_)
		{
			p_74734_1_.writeInt(this.byteArray.Length);
			p_74734_1_.write(this.byteArray);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_) throws IOException
		internal override void func_152446_a(DataInput p_152446_1_, int p_152446_2_, NBTSizeTracker p_152446_3_)
		{
			int var4 = p_152446_1_.readInt();
			p_152446_3_.func_152450_a((long)(8 * var4));
			this.byteArray = new sbyte[var4];
			p_152446_1_.readFully(this.byteArray);
		}

///    
///     <summary> * Gets the type byte for the tag. </summary>
///     
		public override sbyte Id
		{
			get
			{
				return (sbyte)7;
			}
		}

		public override string ToString()
		{
			return "[" + this.byteArray.Length + " bytes]";
		}

///    
///     <summary> * Creates a clone of the tag. </summary>
///     
		public override NBTBase copy()
		{
			sbyte[] var1 = new sbyte[this.byteArray.Length];
			Array.Copy(this.byteArray, 0, var1, 0, this.byteArray.Length);
			return new NBTTagByteArray(var1);
		}

		public override bool Equals(object p_equals_1_)
		{
			return base.Equals(p_equals_1_) ? Array.Equals(this.byteArray, ((NBTTagByteArray)p_equals_1_).byteArray) : false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ Arrays.GetHashCode(this.byteArray);
		}

		public virtual sbyte[] func_150292_c()
		{
			return this.byteArray;
		}
	}

}