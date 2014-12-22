namespace DotCraftServer.nRealms
{
	public class Realms
	{
		

		public static bool isTouchScreen()
		{
			get
			{
				return Minecraft.Minecraft.gameSettings.touchscreen;
			}
		}

		public static Proxy Proxy
		{
			get
			{
				return Minecraft.Minecraft.Proxy;
			}
		}

		public static string sessionId()
		{
			Session var0 = Minecraft.Minecraft.Session;
			return var0 == null ? null : var0.SessionID;
		}

		public static string userName()
		{
			Session var0 = Minecraft.Minecraft.Session;
			return var0 == null ? null : var0.Username;
		}

		public static long currentTimeMillis()
		{
			return Minecraft.SystemTime;
		}

		public static string SessionId
		{
			get
			{
				return Minecraft.Minecraft.Session.SessionID;
			}
		}

		public static string Name
		{
			get
			{
				return Minecraft.Minecraft.Session.Username;
			}
		}

		public static string uuidToName(string p_uuidToName_0_)
		{
			return Minecraft.Minecraft.func_152347_ac().fillProfileProperties(new GameProfile(UUID.fromString(p_uuidToName_0_.replaceAll("(\\w{8})(\\w{4})(\\w{4})(\\w{4})(\\w{12})", "$1-$2-$3-$4-$5")), (string)null), false).Name;
		}

		public static RealmsScreen Screen
		{
			set
			{
				Minecraft.Minecraft.displayGuiScreen(value.Proxy);
			}
		}

		public static string GameDirectoryPath
		{
			get
			{
				return Minecraft.Minecraft.mcDataDir.AbsolutePath;
			}
		}

		public static int survivalId()
		{
			return WorldSettings.GameType.SURVIVAL.ID;
		}

		public static int creativeId()
		{
			return WorldSettings.GameType.CREATIVE.ID;
		}

		public static int adventureId()
		{
			return WorldSettings.GameType.ADVENTURE.ID;
		}
	}

}