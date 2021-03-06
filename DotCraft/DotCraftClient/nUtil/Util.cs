using System;

namespace DotCraftServer.nUtil
{
	public class Util
	{
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
			LINUX = 0,
			SOLARIS = 1,
			WINDOWS = 2,
			OSX = 3,
			UNKNOWN = 4
		}
	}
}