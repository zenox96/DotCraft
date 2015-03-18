using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nUtil;
using System.Collections;

namespace DotCraftCore.nServer.nManagement
{
	public class PreYggdrasilConverter
	{
		private static readonly Logger field_152732_e = LogManager.Logger;
		public static readonly File field_152728_a = new File("banned-ips.txt");
		public static readonly File field_152729_b = new File("banned-players.txt");
		public static readonly File field_152730_c = new File("ops.txt");
		public static readonly File field_152731_d = new File("white-list.txt");
		

		private static void func_152717_a(MinecraftServer p_152717_0_, ICollection p_152717_1_, ProfileLookupCallback p_152717_2_)
		{
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: String[] var3 = (String[])Iterators.toArray(Iterators.filter(p_152717_1_.iterator(), new Predicate() {  public boolean func_152733_a(String p_152733_1_) { return !StringUtils.isNullOrEmpty(p_152733_1_); } public boolean apply(Object p_apply_1_) { return this.func_152733_a((String)p_apply_1_); } }), String.class);
			//string[] var3 = (string[])Iterators.ToArray(Iterators.filter(p_152717_1_.GetEnumerator(), new Predicate() {  public bool func_152733_a(string p_152733_1_) { return !StringUtils.isNullOrEmpty(p_152733_1_); } public bool apply(object p_apply_1_) { return this.func_152733_a((string)p_apply_1_); } }), typeof(string));

			if(p_152717_0_.ServerInOnlineMode)
			{
				p_152717_0_.func_152359_aw().findProfilesByNames(var3, Agent.MINECRAFT, p_152717_2_);
			}
			else
			{
				string[] var4 = var3;
				int var5 = var3.Length;

				for(int var6 = 0; var6 < var5; ++var6)
				{
					string var7 = var4[var6];
					UUID var8 = EntityPlayer.func_146094_a(new GameProfile((UUID)null, var7));
					GameProfile var9 = new GameProfile(var8, var7);
					p_152717_2_.onProfileLookupSucceeded(var9);
				}
			}
		}

		public static string func_152719_a(string p_152719_0_)
		{
			if(!StringUtils.isNullOrEmpty(p_152719_0_) && p_152719_0_.Length <= 16)
			{
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final MinecraftServer var1 = MinecraftServer.getServer();
				MinecraftServer var1 = MinecraftServer.Server;
				GameProfile var2 = var1.func_152358_ax().func_152655_a(p_152719_0_);

				if(var2 != null && var2.Id != null)
				{
					return var2.Id.ToString();
				}
				else if(!var1.SinglePlayer && var1.ServerInOnlineMode)
				{
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final ArrayList var3 = Lists.newArrayList();
					ArrayList var3 = Lists.newArrayList();
//JAVA TO VB & C# CONVERTER TODO TASK: Anonymous inner classes are not converted to .NET:
//					ProfileLookupCallback var4 = new ProfileLookupCallback()
//				{
//					
//					public void onProfileLookupSucceeded(GameProfile p_onProfileLookupSucceeded_1_)
//					{
//						var1.func_152358_ax().func_152649_a(p_onProfileLookupSucceeded_1_);
//						var3.add(p_onProfileLookupSucceeded_1_);
//					}
//					public void onProfileLookupFailed(GameProfile p_onProfileLookupFailed_1_, Exception p_onProfileLookupFailed_2_)
//					{
//						PreYggdrasilConverter.field_152732_e.warn("Could not lookup user whitelist entry for " + p_onProfileLookupFailed_1_.getName(), p_onProfileLookupFailed_2_);
//					}
//				};
					func_152717_a(var1, Lists.newArrayList(new string[] {p_152719_0_}), var4);
					return var3.Count > 0 && ((GameProfile)var3[0]).Id != null ? ((GameProfile)var3[0]).Id.ToString() : "";
				}
				else
				{
					return EntityPlayer.func_146094_a(new GameProfile((UUID)null, p_152719_0_)).ToString();
				}
			}
			else
			{
				return p_152719_0_;
			}
		}
	}

}