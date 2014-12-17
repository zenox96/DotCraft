using System.Collections;

namespace DotCraftCore.nCommand
{

	using EntityPlayerMP = DotCraftCore.nEntity.nPlayer.EntityPlayerMP;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.nUtil.ChatComponentTranslation;
	using WorldSettings = DotCraftCore.nWorld.WorldSettings;

	public class CommandDefaultGameMode : CommandGameMode
	{
		

		public override string CommandName
		{
			get
			{
				return "defaultgamemode";
			}
		}

		public override string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.defaultgamemode.usage";
		}

		public override void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length > 0)
			{
				WorldSettings.GameType var3 = this.getGameModeFromCommand(p_71515_1_, p_71515_2_[0]);
				this.GameType = var3;
				func_152373_a(p_71515_1_, this, "commands.defaultgamemode.success", new object[] {new ChatComponentTranslation("gameMode." + var3.Name, new object[0])});
			}
			else
			{
				throw new WrongUsageException("commands.defaultgamemode.usage", new object[0]);
			}
		}

		protected internal virtual WorldSettings.GameType GameType
		{
			set
			{
				MinecraftServer var2 = MinecraftServer.Server;
				var2.GameType = value;
				EntityPlayerMP var4;
	
				if(var2.ForceGamemode)
				{
					for (IEnumerator var3 = MinecraftServer.Server.ConfigurationManager.playerEntityList.GetEnumerator(); var3.MoveNext(); var4.fallDistance = 0.0F)
					{
						var4 = (EntityPlayerMP)var3.Current;
						var4.GameType = value;
					}
				}
			}
		}
	}

}