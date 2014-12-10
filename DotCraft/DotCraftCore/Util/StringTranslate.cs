using System.Runtime.CompilerServices;
using System.Collections;

namespace DotCraftCore.Util
{

	using Splitter = com.google.common.base.Splitter;
	using Iterables = com.google.common.collect.Iterables;
	using Maps = com.google.common.collect.Maps;
	using Charsets = org.apache.commons.io.Charsets;
	using IOUtils = org.apache.commons.io.IOUtils;

	public class StringTranslate
	{
///    
///     <summary> * Pattern that matches numeric variable placeholders in a resource string, such as "%d", "%3$d", "%.2f" </summary>
///     
		private static readonly Pattern numericVariablePattern = Pattern.compile("%(\\d+\\$)?[\\d\\.]*[df]");

///    
///     <summary> * A Splitter that splits a string on the first "=".  For example, "a=b=c" would split into ["a", "b=c"]. </summary>
///     
		private static readonly Splitter equalSignSplitter = Splitter.on('=').limit(2);

	/// <summary> Is the private singleton instance of StringTranslate.  </summary>
		private static StringTranslate instance = new StringTranslate();
		private readonly IDictionary languageList = Maps.newHashMap();

///    
///     <summary> * The time, in milliseconds since epoch, that this instance was last updated </summary>
///     
		private long lastUpdateTimeInMilliseconds;
		private const string __OBFID = "CL_00001212";

		public StringTranslate()
		{
			try
			{
				InputStream var1 = typeof(StringTranslate).getResourceAsStream("/assets/minecraft/lang/en_US.lang");
				IEnumerator var2 = IOUtils.readLines(var1, Charsets.UTF_8).GetEnumerator();

				while(var2.MoveNext())
				{
					string var3 = (string)var2.Current;

					if(!var3.Length == 0 && var3[0] != 35)
					{
						string[] var4 = (string[])Iterables.ToArray(equalSignSplitter.Split(var3), typeof(string));

						if(var4 != null && var4.Length == 2)
						{
							string var5 = var4[0];
							string var6 = numericVariablePattern.matcher(var4[1]).replaceAll("%$1s");
							this.languageList.Add(var5, var6);
						}
					}
				}

				this.lastUpdateTimeInMilliseconds = System.currentTimeMillis();
			}
			catch (IOException var7)
			{
				;
			}
		}

///    
///     <summary> * Return the StringTranslate singleton instance </summary>
///     
		static StringTranslate Instance
		{
			get
			{
				return instance;
			}
		}

///    
///     <summary> * Replaces all the current instance's translations with the ones that are passed in. </summary>
///     
		[MethodImpl(MethodImplOptions.Synchronized)]
		public static void replaceWith(IDictionary p_135063_0_)
		{
			instance.languageList.Clear();
//JAVA TO VB & C# CONVERTER TODO TASK: There is no .NET Dictionary equivalent to the Java 'putAll' method:
			instance.languageList.putAll(p_135063_0_);
			instance.lastUpdateTimeInMilliseconds = System.currentTimeMillis();
		}

///    
///     <summary> * Translate a key to current language. </summary>
///     
		[MethodImpl(MethodImplOptions.Synchronized)]
		public virtual string translateKey(string p_74805_1_)
		{
			return this.tryTranslateKey(p_74805_1_);
		}

///    
///     <summary> * Translate a key to current language applying String.format() </summary>
///     
		[MethodImpl(MethodImplOptions.Synchronized)]
		public virtual string translateKeyFormat(string p_74803_1_, params object[] p_74803_2_)
		{
			string var3 = this.tryTranslateKey(p_74803_1_);

			try
			{
				return string.format(var3, p_74803_2_);
			}
			catch (IllegalFormatException var5)
			{
				return "Format error: " + var3;
			}
		}

///    
///     <summary> * Tries to look up a translation for the given key; spits back the key if no result was found. </summary>
///     
		private string tryTranslateKey(string p_135064_1_)
		{
			string var2 = (string)this.languageList.get(p_135064_1_);
			return var2 == null ? p_135064_1_ : var2;
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public virtual bool containsTranslateKey(string p_94520_1_)
		{
			return this.languageList.ContainsKey(p_94520_1_);
		}

///    
///     <summary> * Gets the time, in milliseconds since epoch, that this instance was last updated </summary>
///     
		public virtual long LastUpdateTimeInMilliseconds
		{
			get
			{
				return this.lastUpdateTimeInMilliseconds;
			}
		}
	}

}