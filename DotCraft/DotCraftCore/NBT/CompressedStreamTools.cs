using System;

namespace DotCraftCore.NBT
{

	using CrashReport = DotCraftCore.crash.CrashReport;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using ReportedException = DotCraftCore.Util.ReportedException;

	public class CompressedStreamTools
	{
		private const string __OBFID = "CL_00001226";

///    
///     <summary> * Load the gzipped compound from the inputstream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static NBTTagCompound readCompressed(InputStream p_74796_0_) throws IOException
		public static NBTTagCompound readCompressed(InputStream p_74796_0_)
		{
			DataInputStream var1 = new DataInputStream(new BufferedInputStream(new GZIPInputStream(p_74796_0_)));
			NBTTagCompound var2;

			try
			{
				var2 = func_152456_a(var1, NBTSizeTracker.field_152451_a);
			}
			finally
			{
				var1.close();
			}

			return var2;
		}

///    
///     <summary> * Write the compound, gzipped, to the outputstream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static void writeCompressed(NBTTagCompound p_74799_0_, OutputStream p_74799_1_) throws IOException
		public static void writeCompressed(NBTTagCompound p_74799_0_, OutputStream p_74799_1_)
		{
			DataOutputStream var2 = new DataOutputStream(new BufferedOutputStream(new GZIPOutputStream(p_74799_1_)));

			try
			{
				write(p_74799_0_, var2);
			}
			finally
			{
				var2.close();
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static NBTTagCompound func_152457_a(byte[] p_152457_0_, NBTSizeTracker p_152457_1_) throws IOException
		public static NBTTagCompound func_152457_a(sbyte[] p_152457_0_, NBTSizeTracker p_152457_1_)
		{
			DataInputStream var2 = new DataInputStream(new BufferedInputStream(new GZIPInputStream(new ByteArrayInputStream(p_152457_0_))));
			NBTTagCompound var3;

			try
			{
				var3 = func_152456_a(var2, p_152457_1_);
			}
			finally
			{
				var2.close();
			}

			return var3;
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static byte[] compress(NBTTagCompound p_74798_0_) throws IOException
		public static sbyte[] compress(NBTTagCompound p_74798_0_)
		{
			ByteArrayOutputStream var1 = new ByteArrayOutputStream();
			DataOutputStream var2 = new DataOutputStream(new GZIPOutputStream(var1));

			try
			{
				write(p_74798_0_, var2);
			}
			finally
			{
				var2.close();
			}

			return var1.toByteArray();
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static void safeWrite(NBTTagCompound p_74793_0_, File p_74793_1_) throws IOException
		public static void safeWrite(NBTTagCompound p_74793_0_, File p_74793_1_)
		{
			File var2 = new File(p_74793_1_.AbsolutePath + "_tmp");

			if (var2.exists())
			{
				var2.delete();
			}

			write(p_74793_0_, var2);

			if (p_74793_1_.exists())
			{
				p_74793_1_.delete();
			}

			if (p_74793_1_.exists())
			{
				throw new IOException("Failed to delete " + p_74793_1_);
			}
			else
			{
				var2.renameTo(p_74793_1_);
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static void write(NBTTagCompound p_74795_0_, File p_74795_1_) throws IOException
		public static void write(NBTTagCompound p_74795_0_, File p_74795_1_)
		{
			DataOutputStream var2 = new DataOutputStream(new FileOutputStream(p_74795_1_));

			try
			{
				write(p_74795_0_, var2);
			}
			finally
			{
				var2.close();
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static NBTTagCompound read(File p_74797_0_) throws IOException
		public static NBTTagCompound read(File p_74797_0_)
		{
			return func_152458_a(p_74797_0_, NBTSizeTracker.field_152451_a);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static NBTTagCompound func_152458_a(File p_152458_0_, NBTSizeTracker p_152458_1_) throws IOException
		public static NBTTagCompound func_152458_a(File p_152458_0_, NBTSizeTracker p_152458_1_)
		{
			if (!p_152458_0_.exists())
			{
				return null;
			}
			else
			{
				DataInputStream var2 = new DataInputStream(new FileInputStream(p_152458_0_));
				NBTTagCompound var3;

				try
				{
					var3 = func_152456_a(var2, p_152458_1_);
				}
				finally
				{
					var2.close();
				}

				return var3;
			}
		}

///    
///     <summary> * Reads from a CompressedStream. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static NBTTagCompound read(DataInputStream p_74794_0_) throws IOException
		public static NBTTagCompound read(DataInputStream p_74794_0_)
		{
			return func_152456_a(p_74794_0_, NBTSizeTracker.field_152451_a);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static NBTTagCompound func_152456_a(DataInput p_152456_0_, NBTSizeTracker p_152456_1_) throws IOException
		public static NBTTagCompound func_152456_a(DataInput p_152456_0_, NBTSizeTracker p_152456_1_)
		{
			NBTBase var2 = func_152455_a(p_152456_0_, 0, p_152456_1_);

			if (var2 is NBTTagCompound)
			{
				return (NBTTagCompound)var2;
			}
			else
			{
				throw new IOException("Root tag must be a named compound tag");
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public static void write(NBTTagCompound p_74800_0_, DataOutput p_74800_1_) throws IOException
		public static void write(NBTTagCompound p_74800_0_, DataOutput p_74800_1_)
		{
			func_150663_a(p_74800_0_, p_74800_1_);
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private static void func_150663_a(NBTBase p_150663_0_, DataOutput p_150663_1_) throws IOException
		private static void func_150663_a(NBTBase p_150663_0_, DataOutput p_150663_1_)
		{
			p_150663_1_.writeByte(p_150663_0_.Id);

			if (p_150663_0_.Id != 0)
			{
				p_150663_1_.writeUTF("");
				p_150663_0_.write(p_150663_1_);
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: private static NBTBase func_152455_a(DataInput p_152455_0_, int p_152455_1_, NBTSizeTracker p_152455_2_) throws IOException
		private static NBTBase func_152455_a(DataInput p_152455_0_, int p_152455_1_, NBTSizeTracker p_152455_2_)
		{
			sbyte var3 = p_152455_0_.readByte();

			if (var3 == 0)
			{
				return new NBTTagEnd();
			}
			else
			{
				p_152455_0_.readUTF();
				NBTBase var4 = NBTBase.func_150284_a(var3);

				try
				{
					var4.func_152446_a(p_152455_0_, p_152455_1_, p_152455_2_);
					return var4;
				}
				catch (IOException var8)
				{
					CrashReport var6 = CrashReport.makeCrashReport(var8, "Loading NBT data");
					CrashReportCategory var7 = var6.makeCategory("NBT Tag");
					var7.addCrashSection("Tag name", "[UNNAMED TAG]");
					var7.addCrashSection("Tag type", Convert.ToByte(var3));
					throw new ReportedException(var6);
				}
			}
		}
	}

}