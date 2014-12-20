using System;
using System.Collections;
using System.Text;
using System.Threading;

namespace DotCraftCore.nCrash
{
	public class CrashReportCategory
	{
		private readonly CrashReport theCrashReport;
		private readonly string field_85076_b;
		private readonly IList field_85077_c = new ArrayList();
		private StackTraceElement[] stackTrace = new StackTraceElement[0];
		private const string __OBFID = "CL_00001409";

		public CrashReportCategory(CrashReport p_i1353_1_, string p_i1353_2_)
		{
			this.theCrashReport = p_i1353_1_;
			this.field_85076_b = p_i1353_2_;
		}

		public static string func_85074_a(double p_850740_, double p_850742_, double p_850744_)
		{
			return string.Format("{0:F2},{1:F2},{2:F2} - {3}", new object[] {Convert.ToDouble(p_850740_), Convert.ToDouble(p_850742_), Convert.ToDouble(p_850744_), getLocationInfo(MathHelper.floor_double(p_850740_), MathHelper.floor_double(p_850742_), MathHelper.floor_double(p_850744_))});
		}

		/// <summary>
		/// Returns a string with world information on location.Args:x,y,z
		/// </summary>
		public static string getLocationInfo(int p_850710_, int p_850711_, int p_850712_)
		{
			StringBuilder var3 = new StringBuilder();

			try
			{
				var3.Append(string.Format("World: ({0:D},{1:D},{2:D})", new object[] {Convert.ToInt32(p_850710_), Convert.ToInt32(p_850711_), Convert.ToInt32(p_850712_)}));
			}
			catch (Exception)
			{
				var3.Append("(Error finding world loc)");
			}

			var3.Append(", ");
			int var4;
			int var5;
			int var6;
			int var7;
			int var8;
			int var9;
			int var10;
			int var11;
			int var12;

			try
			{
				var4 = p_850710_ >> 4;
				var5 = p_850712_ >> 4;
				var6 = p_850710_ & 15;
				var7 = p_850711_ >> 4;
				var8 = p_850712_ & 15;
				var9 = var4 << 4;
				var10 = var5 << 4;
				var11 = (var4 + 1 << 4) - 1;
				var12 = (var5 + 1 << 4) - 1;
				var3.Append(string.Format("Chunk: (at {0:D},{1:D},{2:D} in {3:D},{4:D}; contains blocks {5:D},0,{6:D} to {7:D},255,{8:D})", new object[] {Convert.ToInt32(var6), Convert.ToInt32(var7), Convert.ToInt32(var8), Convert.ToInt32(var4), Convert.ToInt32(var5), Convert.ToInt32(var9), Convert.ToInt32(var10), Convert.ToInt32(var11), Convert.ToInt32(var12)}));
			}
			catch (Exception)
			{
				var3.Append("(Error finding chunk loc)");
			}

			var3.Append(", ");

			try
			{
				var4 = p_850710_ >> 9;
				var5 = p_850712_ >> 9;
				var6 = var4 << 5;
				var7 = var5 << 5;
				var8 = (var4 + 1 << 5) - 1;
				var9 = (var5 + 1 << 5) - 1;
				var10 = var4 << 9;
				var11 = var5 << 9;
				var12 = (var4 + 1 << 9) - 1;
				int var13 = (var5 + 1 << 9) - 1;
				var3.Append(string.Format("Region: ({0:D},{1:D}; contains chunks {2:D},{3:D} to {4:D},{5:D}, blocks {6:D},0,{7:D} to {8:D},255,{9:D})", new object[] {Convert.ToInt32(var4), Convert.ToInt32(var5), Convert.ToInt32(var6), Convert.ToInt32(var7), Convert.ToInt32(var8), Convert.ToInt32(var9), Convert.ToInt32(var10), Convert.ToInt32(var11), Convert.ToInt32(var12), Convert.ToInt32(var13)}));
			}
			catch (Exception)
			{
				var3.Append("(Error finding world loc)");
			}

			return var3.ToString();
		}

		/// <summary>
		/// Adds a Crashreport section with the given name with the value set to the result of the given Callable;
		/// </summary>
		public virtual void addCrashSectionCallable(string p_715001_, Callable p_715002_)
		{
			try
			{
				this.addCrashSection(p_715001_, p_715002_.call());
			}
			catch (Exception var4)
			{
				this.addCrashSectionThrowable(p_715001_, var4);
			}
		}

		/// <summary>
		/// Adds a Crashreport section with the given name with the given value (convered .toString())
		/// </summary>
		public virtual void addCrashSection(string p_715071_, object p_715072_)
		{
			this.field_85077_c.Add(new CrashReportCategory.Entry(p_715071_, p_715072_));
		}

		/// <summary>
		/// Adds a Crashreport section with the given name with the given Throwable
		/// </summary>
		public virtual void addCrashSectionThrowable(string p_714991_, Exception p_714992_)
		{
			this.addCrashSection(p_714991_, p_714992_);
		}

		/// <summary>
		/// Resets our stack trace according to the current trace, pruning the deepest 3 entries.  The parameter indicates
		/// how many additional deepest entries to prune.  Returns the number of entries in the resulting pruned stack trace.
		/// </summary>
		public virtual int getPrunedStackTrace(int p_850731_)
		{
			StackTraceElement[] var2 = Thread.CurrentThread.StackTrace;

			if (var2.Length <= 0)
			{
				return 0;
			}
			else
			{
				this.stackTrace = new StackTraceElement[var2.Length - 3 - p_850731_];
				Array.Copy(var2, 3 + p_850731_, this.stackTrace, 0, this.stackTrace.Length);
				return this.stackTrace.Length;
			}
		}

		/// <summary>
		/// Do the deepest two elements of our saved stack trace match the given elements, in order from the deepest?
		/// </summary>
		public virtual bool firstTwoElementsOfStackTraceMatch(StackTraceElement p_850691_, StackTraceElement p_850692_)
		{
			if (this.stackTrace.Length != 0 && p_850691_ != null)
			{
				StackTraceElement var3 = this.stackTrace[0];

				if (var3.NativeMethod == p_850691_.NativeMethod && var3.ClassName.Equals(p_850691_.ClassName) && var3.FileName.Equals(p_850691_.FileName) && var3.MethodName.Equals(p_850691_.MethodName))
				{
					if (p_850692_ != null != this.stackTrace.Length > 1)
					{
						return false;
					}
					else if (p_850692_ != null && !this.stackTrace[1].Equals(p_850692_))
					{
						return false;
					}
					else
					{
						this.stackTrace[0] = p_850691_;
						return true;
					}
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Removes the given number entries from the bottom of the stack trace.
		/// </summary>
		public virtual void trimStackTraceEntriesFromBottom(int p_850701_)
		{
			StackTraceElement[] var2 = new StackTraceElement[this.stackTrace.Length - p_850701_];
			Array.Copy(this.stackTrace, 0, var2, 0, var2.Length);
			this.stackTrace = var2;
		}

		public virtual void appendToStringBuilder(StringBuilder p_850721_)
		{
			p_850721_.Append("-- ").Append(this.field_85076_b).Append(" --\n");
			p_850721_.Append("Details:");
			IEnumerator var2 = this.field_85077_c.GetEnumerator();

			while (var2.hasNext())
			{
				CrashReportCategory.Entry var3 = (CrashReportCategory.Entry)var2.next();
				p_850721_.Append("\n\t");
				p_850721_.Append(var3.func_85089_a());
				p_850721_.Append(": ");
				p_850721_.Append(var3.func_85090_b());
			}

			if (this.stackTrace != null && this.stackTrace.Length > 0)
			{
				p_850721_.Append("\nStacktrace:");
				StackTraceElement[] var6 = this.stackTrace;
				int var7 = var6.Length;

				for (int var4 = 0; var4 < var7; ++var4)
				{
					StackTraceElement var5 = var6[var4];
					p_850721_.Append("\n\tat ");
					p_850721_.Append(var5.ToString());
				}
			}
		}

		public virtual StackTraceElement[] func_147152_a()
		{
			return this.stackTrace;
		}

//JAVA TO C# CONVERTER WARNING: 'final' parameters are not available in .NET:
//ORIGINAL LINE: public static void func_147153_a(CrashReportCategory p_1471530_, final int p_1471531_, final int p_1471532_, final int p_1471533_, final net.minecraft.block.Block p_1471534_, final int p_1471535_)
		public static void func_147153_a(CrashReportCategory p_1471530_, int p_1471531_, int p_1471532_, int p_1471533_, Block p_1471534_, int p_1471535_)
		{
//JAVA TO C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final int var6 = net.minecraft.block.Block.getIdFromBlock(p_1471534_);
			int var6 = Block.getIdFromBlock(p_1471534_);
			p_1471530_.addCrashSectionCallable("Block type", new CallableAnonymousInnerClassHelper(p_1471534_, var6));
			p_1471530_.addCrashSectionCallable("Block data value", new CallableAnonymousInnerClassHelper2(p_1471535_));
			p_1471530_.addCrashSectionCallable("Block location", new CallableAnonymousInnerClassHelper3(p_1471531_, p_1471532_, p_1471533_));
		}

		private class CallableAnonymousInnerClassHelper : Callable
		{
			private Block p_1471534_;
			private int var6;

			public CallableAnonymousInnerClassHelper(Block p_1471534_, int var6)
			{
				this.p_1471534_ = p_1471534_;
				this.var6 = var6;
			}

			private const string __OBFID = "CL_00001426";
			public virtual string call()
			{
				try
				{
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getCanonicalName method:
					return string.Format("ID #{0:D} ({1} // {2})", new object[] {Convert.ToInt32(var6), p_1471534_.UnlocalizedName, p_1471534_.GetType().FullName});
				}
				catch (Exception)
				{
					return "ID #" + var6;
				}
			}
		}

		private class CallableAnonymousInnerClassHelper2 : Callable
		{
			private int p_1471535_;

			public CallableAnonymousInnerClassHelper2(int p_1471535_)
			{
				this.p_1471535_ = p_1471535_;
			}

			private const string __OBFID = "CL_00001441";
			public virtual string call()
			{
				if (p_1471535_ < 0)
				{
					return "Unknown? (Got " + p_1471535_ + ")";
				}
				else
				{
					string var1 = string.Format("{0,4}", new object[] {int.toBinaryString(p_1471535_)}).replace(" ", "0");
					return string.Format("{0:D} / 0x{0:X} / 0b{1}", new object[] {Convert.ToInt32(p_1471535_), var1});
				}
			}
		}

		private class CallableAnonymousInnerClassHelper3 : Callable
		{
			private int p_1471531_;
			private int p_1471532_;
			private int p_1471533_;

			public CallableAnonymousInnerClassHelper3(int p_1471531_, int p_1471532_, int p_1471533_)
			{
				this.p_1471531_ = p_1471531_;
				this.p_1471532_ = p_1471532_;
				this.p_1471533_ = p_1471533_;
			}

			private const string __OBFID = "CL_00001465";
			public virtual string call()
			{
				return CrashReportCategory.getLocationInfo(p_1471531_, p_1471532_, p_1471533_);
			}
		}

		internal class Entry
		{
			internal readonly string field_85092_a;
			internal readonly string field_85091_b;
			internal const string __OBFID = "CL_00001489";

			public Entry(string p_i1352_1_, object p_i1352_2_)
			{
				this.field_85092_a = p_i1352_1_;

				if (p_i1352_2_ == null)
				{
					this.field_85091_b = "~~NULL~~";
				}
				else if (p_i1352_2_ is Exception)
				{
					Exception var3 = (Exception)p_i1352_2_;
					this.field_85091_b = "~~ERROR~~ " + var3.GetType().Name + ": " + var3.Message;
				}
				else
				{
					this.field_85091_b = p_i1352_2_.ToString();
				}
			}

			public virtual string func_85089_a()
			{
				return this.field_85092_a;
			}

			public virtual string func_85090_b()
			{
				return this.field_85091_b;
			}
		}
	}

}