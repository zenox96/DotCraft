namespace DotCraftCore.Util
{

	public class Util
	{
		private const string __OBFID = "CL_00001633";

		public static Util.EnumOS OSType
		{
			get
			{
				string var0 = System.getProperty("os.name").ToLower();
				return var0.Contains("win") ? Util.EnumOS.WINDOWS : (var0.Contains("mac") ? Util.EnumOS.OSX : (var0.Contains("solaris") ? Util.EnumOS.SOLARIS : (var0.Contains("sunos") ? Util.EnumOS.SOLARIS : (var0.Contains("linux") ? Util.EnumOS.LINUX : (var0.Contains("unix") ? Util.EnumOS.LINUX : Util.EnumOS.UNKNOWN)))));
			}
		}

		public enum EnumOS
		{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			LINUX("LINUX", 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			SOLARIS("SOLARIS", 1),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			WINDOWS("WINDOWS", 2),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			OSX("OSX", 3),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			UNKNOWN("UNKNOWN", 4);

			@private static final Util.EnumOS[] $VALUES = new Util.EnumOS[]{LINUX, SOLARIS, WINDOWS, OSX, UNKNOWN
		}
			private const string __OBFID = "CL_00001660";

			private EnumOS(string p_i1357_1_, int p_i1357_2_)
			{
			}
		}
	}

}