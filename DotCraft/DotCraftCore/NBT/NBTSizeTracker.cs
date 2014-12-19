using System;

namespace DotCraftCore.nNBT
{

	public class NBTSizeTracker
	{
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//		public static final NBTSizeTracker field_152451_a = new NBTSizeTracker(0L)
//	{
//		
//		public void func_152450_a(long p_152450_1_)
//		{
//		}
//	};
		private readonly long field_152452_b;
		private long field_152453_c;
		

		public NBTSizeTracker(long p_i1203_1_)
		{
			this.field_152452_b = p_i1203_1_;
		}

		public virtual void func_152450_a(long p_152450_1_)
		{
			this.field_152453_c += p_152450_1_ / 8L;

			if (this.field_152453_c > this.field_152452_b)
			{
				throw new Exception("Tried to read NBT tag that was too big; tried to allocate: " + this.field_152453_c + "bytes where max allowed: " + this.field_152452_b);
			}
		}
	}

}