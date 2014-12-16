using System;

namespace DotCraftCore.Realms
{

	using Minecraft = DotCraftCore.client.Minecraft;
	using GuiScreen = DotCraftCore.client.gui.GuiScreen;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class RealmsBridge : RealmsScreen
	{
		private static readonly Logger LOGGER = LogManager.Logger;
		private GuiScreen previousScreen;
		

		public virtual void switchToRealms(GuiScreen p_switchToRealms_1_)
		{
			this.previousScreen = p_switchToRealms_1_;

			try
			{
				Type var2 = Type.GetType("com.mojang.realmsclient.RealmsMainScreen");
				Constructor var3 = var2.getDeclaredConstructor(new Class[] {typeof(RealmsScreen)});
				var3.Accessible = true;
				object var4 = var3.newInstance(new object[] {this});
				Minecraft.Minecraft.displayGuiScreen(((RealmsScreen)var4).Proxy);
			}
			catch (Exception var5)
			{
				LOGGER.error("Realms module missing", var5);
			}
		}

		public override void init()
		{
			Minecraft.Minecraft.displayGuiScreen(this.previousScreen);
		}
	}

}