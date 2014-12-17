namespace DotCraftCore.nCommand.nServer
{

	using CommandBase = DotCraftCore.nCommand.CommandBase;
	using ICommandSender = DotCraftCore.nCommand.ICommandSender;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;

	public class CommandStop : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "stop";
			}
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.stop.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(MinecraftServer.Server.worldServers != null)
			{
				func_152373_a(p_71515_1_, this, "commands.stop.start", new object[0]);
			}

			MinecraftServer.Server.initiateShutdown();
		}
	}

}