namespace DotCraftCore.Command.Server
{

	using CommandBase = DotCraftCore.Command.CommandBase;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using WorldSettings = DotCraftCore.World.WorldSettings;

	public class CommandPublishLocalServer : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "publish";
			}
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.publish.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			string var3 = MinecraftServer.Server.shareToLAN(WorldSettings.GameType.SURVIVAL, false);

			if(var3 != null)
			{
				func_152373_a(p_71515_1_, this, "commands.publish.started", new object[] {var3});
			}
			else
			{
				func_152373_a(p_71515_1_, this, "commands.publish.failed", new object[0]);
			}
		}
	}

}