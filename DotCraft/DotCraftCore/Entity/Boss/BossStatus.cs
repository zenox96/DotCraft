namespace DotCraftCore.Entity.Boss
{

	public sealed class BossStatus
	{
		public static float healthScale;
		public static int statusBarTime;
		public static string bossName;
		public static bool hasColorModifier;
		private const string __OBFID = "CL_00000941";

		public static void setBossStatus(IBossDisplayData p_82824_0_, bool p_82824_1_)
		{
			healthScale = p_82824_0_.Health / p_82824_0_.MaxHealth;
			statusBarTime = 100;
			bossName = p_82824_0_.func_145748_c_().FormattedText;
			hasColorModifier = p_82824_1_;
		}
	}

}