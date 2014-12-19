namespace DotCraftCore.nUtil
{


	public class StringUtils
	{
		private static readonly Pattern patternControlCode = Pattern.compile("(?i)\\u00A7[0-9A-FK-OR]");
		

///    
///     <summary> * Returns the time elapsed for the given number of ticks, in "mm:ss" format. </summary>
///     
		public static string ticksToElapsedTime(int p_76337_0_)
		{
			int var1 = p_76337_0_ / 20;
			int var2 = var1 / 60;
			var1 %= 60;
			return var1 < 10 ? var2 + ":0" + var1 : var2 + ":" + var1;
		}

		public static string stripControlCodes(string p_76338_0_)
		{
			return patternControlCode.matcher(p_76338_0_).replaceAll("");
		}

///    
///     <summary> * Returns a value indicating whether the given string is null or empty. </summary>
///     
		public static bool isNullOrEmpty(string p_151246_0_)
		{
			return p_151246_0_ == null || "".Equals(p_151246_0_);
		}
	}

}