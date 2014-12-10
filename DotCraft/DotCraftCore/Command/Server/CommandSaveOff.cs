namespace DotCraftCore.Command.Server
{

	using CommandBase = DotCraftCore.Command.CommandBase;
	using CommandException = DotCraftCore.Command.CommandException;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;
	using WorldServer = DotCraftCore.world.WorldServer;

	public class CommandSaveOff : CommandBase
	{
		private const string __OBFID = "CL_00000847";

		public virtual string CommandName
		{
			get
			{
				return "save-off";
			}
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.save-off.usage";
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

					if(!var6.levelSaving)
					{
						var6.levelSaving = true;
						var4 = true;
					}
				}
			}

			if(var4)
			{
				func_152373_a(p_71515_1_, this, "commands.save.disabled", new object[0]);
			}
			else
			{
				throw new CommandException("commands.save-off.alreadyOff", new object[0]);
			}
		}
	}

}