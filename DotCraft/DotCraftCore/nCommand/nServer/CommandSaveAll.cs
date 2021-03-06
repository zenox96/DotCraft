namespace DotCraftCore.nCommand.nServer
{

	using CommandBase = DotCraftCore.nCommand.CommandBase;
	using ICommandSender = DotCraftCore.nCommand.ICommandSender;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.nUtil.ChatComponentTranslation;
	using IProgressUpdate = DotCraftCore.nUtil.IProgressUpdate;
	using MinecraftException = DotCraftCore.nWorld.MinecraftException;
	using WorldServer = DotCraftCore.nWorld.WorldServer;

	public class CommandSaveAll : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "save-all";
			}
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.save.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			MinecraftServer var3 = MinecraftServer.Server;
			p_71515_1_.AddChatMessage(new ChatComponentTranslation("commands.save.start", new object[0]));

			if(var3.ConfigurationManager != null)
			{
				var3.ConfigurationManager.saveAllPlayerData();
			}

			try
			{
				int var4;
				WorldServer var5;
				bool var6;

				for (var4 = 0; var4 < var3.worldServers.length; ++var4)
				{
					if(var3.worldServers[var4] != null)
					{
						var5 = var3.worldServers[var4];
						var6 = var5.levelSaving;
						var5.levelSaving = false;
						var5.saveAllChunks(true, (IProgressUpdate)null);
						var5.levelSaving = var6;
					}
				}

				if(p_71515_2_.Length > 0 && "flush".Equals(p_71515_2_[0]))
				{
					p_71515_1_.AddChatMessage(new ChatComponentTranslation("commands.save.flushStart", new object[0]));

					for (var4 = 0; var4 < var3.worldServers.length; ++var4)
					{
						if(var3.worldServers[var4] != null)
						{
							var5 = var3.worldServers[var4];
							var6 = var5.levelSaving;
							var5.levelSaving = false;
							var5.saveChunkData();
							var5.levelSaving = var6;
						}
					}

					p_71515_1_.AddChatMessage(new ChatComponentTranslation("commands.save.flushEnd", new object[0]));
				}
			}
			catch (MinecraftException var7)
			{
				func_152373_a(p_71515_1_, this, "commands.save.failed", new object[] {var7.Message});
				return;
			}

			func_152373_a(p_71515_1_, this, "commands.save.success", new object[0]);
		}
	}

}