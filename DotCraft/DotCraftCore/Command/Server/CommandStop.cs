namespace DotCraftCore.Command.Server
{

	using CommandBase = DotCraftCore.Command.CommandBase;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;

	public class CommandStop : CommandBase
	{
		private const string __OBFID = "CL_00001132";

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