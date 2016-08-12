using DotCraftCore.nUtil;
using System;
using System.Collections;
using System.Text;

namespace DotCraftCore.nCrash
{

	public class CrashReport
	{
		private bool InstanceFieldsInitialized = false;

		private void InitializeInstanceFields()
		{
			theReportCategory = new CrashReportCategory(this, "System Details");
		}

		/// <summary>
		/// Description of the crash report. </summary>
		private readonly string description;

		/// <summary>
		/// The Throwable that is the "cause" for this crash and Crash Report. </summary>
		private readonly Exception cause;

		/// <summary>
		/// Category of crash </summary>
		private CrashReportCategory theReportCategory;

		/// <summary>
		/// Holds the keys and values of all crash report sections. </summary>
		private readonly IList crashReportSections = new ArrayList();

		/// <summary>
		/// File of crash report. </summary>
		private File crashReportFile;
		private bool field_85059f = true;
		private StackTraceElement[] stacktrace = new StackTraceElement[0];

		public CrashReport(string p_i1348_1_, Exception p_i1348_2_)
		{
			if (!InstanceFieldsInitialized)
			{
				InitializeInstanceFields();
				InstanceFieldsInitialized = true;
			}
			this.description = p_i1348_1_;
			this.cause = p_i1348_2_;
			this.populateEnvironment();
		}

		/// <summary>
		/// Populates this crash report with initial information about the running server and operating system / java
		/// environment
		/// </summary>
		private void populateEnvironment()
		{
			this.theReportCategory.addCrashSectionCallable("Minecraft Version", new CallableAnonymousInnerClassHelper(this));
			this.theReportCategory.addCrashSectionCallable("Operating System", new CallableAnonymousInnerClassHelper2(this));
			this.theReportCategory.addCrashSectionCallable("Java Version", new CallableAnonymousInnerClassHelper3(this));
			this.theReportCategory.addCrashSectionCallable("Java VM Version", new CallableAnonymousInnerClassHelper4(this));
			this.theReportCategory.addCrashSectionCallable("Memory", new CallableAnonymousInnerClassHelper5(this));
			this.theReportCategory.addCrashSectionCallable("JVM Flags", new CallableAnonymousInnerClassHelper6(this));
			this.theReportCategory.addCrashSectionCallable("AABB Pool Size", new CallableAnonymousInnerClassHelper7(this));
			this.theReportCategory.addCrashSectionCallable("IntCache", new CallableAnonymousInnerClassHelper8(this));
		}

		private class CallableAnonymousInnerClassHelper : Callable
		{
			private readonly CrashReport outerInstance;

			public CallableAnonymousInnerClassHelper(CrashReport outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public virtual string call()
			{
				return "1.7.10";
			}
		}

		private class CallableAnonymousInnerClassHelper2 : Callable
		{
			private readonly CrashReport outerInstance;

			public CallableAnonymousInnerClassHelper2(CrashReport outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public virtual string call()
			{
				return System.getProperty("os.name") + " (" + System.getProperty("os.arch") + ") version " + System.getProperty("os.version");
			}
		}

		private class CallableAnonymousInnerClassHelper3 : Callable
		{
			private readonly CrashReport outerInstance;

			public CallableAnonymousInnerClassHelper3(CrashReport outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public virtual string call()
			{
				return System.getProperty("java.version") + ", " + System.getProperty("java.vendor");
			}
		}

		private class CallableAnonymousInnerClassHelper4 : Callable
		{
			private readonly CrashReport outerInstance;

			public CallableAnonymousInnerClassHelper4(CrashReport outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public virtual string call()
			{
				return System.getProperty("java.vm.name") + " (" + System.getProperty("java.vm.info") + "), " + System.getProperty("java.vm.vendor");
			}
		}

		private class CallableAnonymousInnerClassHelper5 : Callable
		{
			private readonly CrashReport outerInstance;

			public CallableAnonymousInnerClassHelper5(CrashReport outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public virtual string call()
			{
				Runtime var1 = Runtime.Runtime;
				long var2 = var1.maxMemory();
				long var4 = var1.totalMemory();
				long var6 = var1.freeMemory();
				long var8 = var2 / 1024L / 1024L;
				long var10 = var4 / 1024L / 1024L;
				long var12 = var6 / 1024L / 1024L;
				return var6 + " bytes (" + var12 + " MB) / " + var4 + " bytes (" + var10 + " MB) up to " + var2 + " bytes (" + var8 + " MB)";
			}
		}

		private class CallableAnonymousInnerClassHelper6 : Callable
		{
			private readonly CrashReport outerInstance;

			public CallableAnonymousInnerClassHelper6(CrashReport outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public virtual string call()
			{
				RuntimeMXBean var1 = ManagementFactory.RuntimeMXBean;
				IList var2 = var1.InputArguments;
				int var3 = 0;
				StringBuilder var4 = new StringBuilder();
				IEnumerator var5 = var2.GetEnumerator();

				while (var5.hasNext())
				{
					string var6 = (string)var5.next();

					if (var6.StartsWith("-X", StringComparison.Ordinal))
					{
						if (var3++ > 0)
						{
							var4.Append(" ");
						}

						var4.Append(var6);
					}
				}

				return string.Format("{0:D} total; {1}", new object[] {Convert.ToInt32(var3), var4.ToString()});
			}
		}

		private class CallableAnonymousInnerClassHelper7 : Callable
		{
			private readonly CrashReport outerInstance;

			public CallableAnonymousInnerClassHelper7(CrashReport outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public virtual string call()
			{
				sbyte var1 = 0;
				int var2 = 56 * var1;
				int var3 = var2 / 1024 / 1024;
				sbyte var4 = 0;
				int var5 = 56 * var4;
				int var6 = var5 / 1024 / 1024;
				return var1 + " (" + var2 + " bytes; " + var3 + " MB) allocated, " + var4 + " (" + var5 + " bytes; " + var6 + " MB) used";
			}
		}

		private class CallableAnonymousInnerClassHelper8 : Callable
		{
			private readonly CrashReport outerInstance;

			public CallableAnonymousInnerClassHelper8(CrashReport outerInstance)
			{
				this.outerInstance = outerInstance;
			}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public String call() throws SecurityException, NoSuchFieldException, IllegalAccessException, IllegalArgumentException
			public virtual string call()
			{
				return IntCache.CacheSizes;
			}
		}

		/// <summary>
		/// Returns the description of the Crash Report.
		/// </summary>
		public virtual string Description
		{
			get
			{
				return this.description;
			}
		}

		/// <summary>
		/// Returns the Throwable object that is the cause for the crash and Crash Report.
		/// </summary>
		public virtual Exception CrashCause
		{
			get
			{
				return this.cause;
			}
		}

		/// <summary>
		/// Gets the various sections of the crash report into the given StringBuilder
		/// </summary>
		public virtual void getSectionsInStringBuilder(StringBuilder p_715061_)
		{
			if ((this.stacktrace == null || this.stacktrace.Length <= 0) && this.crashReportSections.Count > 0)
			{
				this.stacktrace = (StackTraceElement[])ArrayUtils.subarray(((CrashReportCategory)this.crashReportSections[0]).func_147152_a(), 0, 1);
			}

			if (this.stacktrace != null && this.stacktrace.Length > 0)
			{
				p_715061_.Append("-- Head --\n");
				p_715061_.Append("Stacktrace:\n");
				StackTraceElement[] var2 = this.stacktrace;
				int var3 = var2.Length;

				for (int var4 = 0; var4 < var3; ++var4)
				{
					StackTraceElement var5 = var2[var4];
					p_715061_.Append("\t").Append("at ").Append(var5.ToString());
					p_715061_.Append("\n");
				}

				p_715061_.Append("\n");
			}

			IEnumerator var6 = this.crashReportSections.GetEnumerator();

			while (var6.MoveNext())
			{
				CrashReportCategory var7 = (CrashReportCategory)var6.Current;
				var7.appendToStringBuilder(p_715061_);
				p_715061_.Append("\n\n");
			}

			this.theReportCategory.appendToStringBuilder(p_715061_);
		}

		/// <summary>
		/// Gets the stack trace of the Throwable that caused this crash report, or if that fails, the cause .toString().
		/// </summary>
		public virtual string CauseStackTraceOrString
		{
			get
			{
				StringWriter var1 = null;
				PrintWriter var2 = null;
				object var3 = this.cause;
    
				if (((Exception)var3).Message == null)
				{
					if (var3 is System.NullReferenceException)
					{
						var3 = new System.NullReferenceException(this.description);
					}
					else if (var3 is StackOverflowError)
					{
						var3 = new StackOverflowError(this.description);
					}
					else if (var3 is System.OutOfMemoryException)
					{
						var3 = new System.OutOfMemoryException(this.description);
					}
    
					((Exception)var3).StackTrace = this.cause.StackTrace;
				}
    
				string var4 = ((Exception)var3).ToString();
    
				try
				{
					var1 = new StringWriter();
					var2 = new PrintWriter(var1);
					((Exception)var3).printStackTrace(var2);
					var4 = var1.ToString();
				}
				finally
				{
					IOUtils.closeQuietly(var1);
					IOUtils.closeQuietly(var2);
				}
    
				return var4;
			}
		}

		/// <summary>
		/// Gets the complete report with headers, stack trace, and different sections as a string.
		/// </summary>
		public virtual string CompleteReport
		{
			get
			{
				StringBuilder var1 = new StringBuilder();
				var1.Append("---- Minecraft Crash Report ----\n");
				var1.Append("// ");
				var1.Append(WittyComment);
				var1.Append("\n\n");
				var1.Append("Time: ");
				var1.Append((new SimpleDateFormat()).format(DateTime.Now));
				var1.Append("\n");
				var1.Append("Description: ");
				var1.Append(this.description);
				var1.Append("\n\n");
				var1.Append(this.CauseStackTraceOrString);
				var1.Append("\n\nA detailed walkthrough of the error, its code path and all known details is as follows:\n");
    
				for (int var2 = 0; var2 < 87; ++var2)
				{
					var1.Append("-");
				}
    
				var1.Append("\n\n");
				this.getSectionsInStringBuilder(var1);
				return var1.ToString();
			}
		}

		/// <summary>
		/// Gets the file this crash report is saved into.
		/// </summary>
		public virtual File File
		{
			get
			{
				return this.crashReportFile;
			}
		}

		/// <summary>
		/// Saves this CrashReport to the given file and returns a value indicating whether we were successful at doing so.
		/// </summary>
		public virtual bool saveToFile(File p_1471491_)
		{
			if (this.crashReportFile != null)
			{
				return false;
			}
			else
			{
				if (p_1471491_.ParentFile != null)
				{
					p_1471491_.ParentFile.mkdirs();
				}

				try
				{
					System.IO.StreamWriter var2 = new System.IO.StreamWriter(p_1471491_);
					var2.WriteByte(this.CompleteReport);
					var2.Close();
					this.crashReportFile = p_1471491_;
					return true;
				}
				catch (Exception var3)
				{
					logger.error("Could not save crash report to " + p_1471491_, var3);
					return false;
				}
			}
		}

		public virtual CrashReportCategory Category
		{
			get
			{
				return this.theReportCategory;
			}
		}

		/// <summary>
		/// Creates a CrashReportCategory
		/// </summary>
		public virtual CrashReportCategory makeCategory(string p_850581_)
		{
			return this.makeCategoryDepth(p_850581_, 1);
		}

		/// <summary>
		/// Creates a CrashReportCategory for the given stack trace depth
		/// </summary>
		public virtual CrashReportCategory makeCategoryDepth(string p_850571_, int p_850572_)
		{
			CrashReportCategory var3 = new CrashReportCategory(this, p_850571_);

			if (this.field_85059f)
			{
				int var4 = var3.getPrunedStackTrace(p_850572_);
				StackTraceElement[] var5 = this.cause.StackTrace;
				StackTraceElement var6 = null;
				StackTraceElement var7 = null;
				int var8 = var5.Length - var4;

				if (var8 < 0)
				{
					Console.WriteLine("Negative index in crash report handler (" + var5.Length + "/" + var4 + ")");
				}

				if (var5 != null && 0 <= var8 && var8 < var5.Length)
				{
					var6 = var5[var8];

					if (var5.Length + 1 - var4 < var5.Length)
					{
						var7 = var5[var5.Length + 1 - var4];
					}
				}

				this.field_85059f = var3.firstTwoElementsOfStackTraceMatch(var6, var7);

				if (var4 > 0 && this.crashReportSections.Count > 0)
				{
					CrashReportCategory var9 = (CrashReportCategory)this.crashReportSections[this.crashReportSections.Count - 1];
					var9.trimStackTraceEntriesFromBottom(var4);
				}
				else if (var5 != null && var5.Length >= var4 && 0 <= var8 && var8 < var5.Length)
				{
					this.stacktrace = new StackTraceElement[var8];
					Array.Copy(var5, 0, this.stacktrace, 0, this.stacktrace.Length);
				}
				else
				{
					this.field_85059f = false;
				}
			}

			this.crashReportSections.Add(var3);
			return var3;
		}

		/// <summary>
		/// Gets a random witty comment for inclusion in this CrashReport
		/// </summary>
		private static string WittyComment
		{
			get
			{
				string[] var0 = new string[] {"Who set us up the TNT?", "Everything\'s going to plan. No, really, that was supposed to happen.", "Uh... Did I do that?", "Oops.", "Why did you do that?", "I feel sad now :(", "My bad.", "I\'m sorry, Dave.", "I let you down. Sorry :(", "On the bright side, I bought you a teddy bear!", "Daisy, daisy...", "Oh - I know what I did wrong!", "Hey, that tickles! Hehehe!", "I blame Dinnerbone.", "You should try our sister game, Minceraft!", "Don\'t be sad. I\'ll do better next time, I promise!", "Don\'t be sad, have a hug! <3", "I just don\'t know what went wrong :(", "Shall we play a game?", "Quite honestly, I wouldn\'t worry myself about that.", "I bet Cylons wouldn\'t have this problem.", "Sorry :(", "Surprise! Haha. Well, this is awkward.", "Would you like a cupcake?", "Hi. I\'m Minecraft, and I\'m a crashaholic.", "Ooh. Shiny.", "This doesn\'t make any sense!", "Why is it breaking :(", "Don\'t do that.", "Ouch. That hurt :(", "You\'re mean.", "This is a token for 1 free hug. Redeem at your nearest Mojangsta: [~~HUG~~]", "There are four lights!", "But it works on my machine."};
    
				try
				{
					return var0[(int)(System.nanoTime() % (long)var0.Length)];
				}
				catch (Exception)
				{
					return "Witty comment unavailable :(";
				}
			}
		}

		/// <summary>
		/// Creates a crash report for the exception
		/// </summary>
		public static CrashReport makeCrashReport(Exception p_850550_, string p_850551_)
		{
			CrashReport var2;

			if (p_850550_ is ReportedException)
			{
				var2 = ((ReportedException)p_850550_).CrashReport;
			}
			else
			{
				var2 = new CrashReport(p_850551_, p_850550_);
			}

			return var2;
		}
	}

}