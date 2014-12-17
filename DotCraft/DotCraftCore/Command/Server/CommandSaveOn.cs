namespace DotCraftCore.nCommand.nServer
{

	using CommandBase = DotCraftCore.nCommand.CommandBase;
	using CommandException = DotCraftCore.nCommand.CommandException;
	using ICommandSender = DotCraftCore.nCommand.ICommandSender;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using WorldServer = DotCraftCore.nWorld.WorldServer;

	public class CommandSaveOn : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "save-on";
			}
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.save-on.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			MinecraftServer var3 = MinecraftServer.Server;
			bool var4 = false;

			for (int var5 = 0; var5 < var3.worldServers.length; ++var5)
			{
				if(var3.worldServers[var5] != null)
				{
					WorldServer var6 = var3.worldServers[var5];

					if(var6.levelSaving)
					{
						var6.levelSaving = false;
						var4 = true;
					}
				}
			}

			if(var4)
			{
				func_152373_a(p_71515_1_, this, "commands.save.enabled", new object[0]);
			}
			else
			{
				throw new CommandException("commands.save-on.alreadyOn", new object[0]);
			}
		}
	}

}