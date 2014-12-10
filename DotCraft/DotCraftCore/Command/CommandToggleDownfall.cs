namespace DotCraftCore.Command
{

	using MinecraftServer = DotCraftCore.server.MinecraftServer;
	using WorldInfo = DotCraftCore.world.storage.WorldInfo;

	public class CommandToggleDownfall : CommandBase
	{
		private const string __OBFID = "CL_00001184";

		public virtual string CommandName
		{
			get
			{
				return "toggledownfall";
			}
		}

///    
///     <summary> * Return the required permission level for this command. </summary>
///     
		public override int RequiredPermissionLevel
		{
			get
			{
				return 2;
			}
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.downfall.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			this.toggleDownfall();
			func_152373_a(p_71515_1_, this, "commands.downfall.success", new object[0]);
		}

///    
///     <summary> * Toggle rain and enable thundering. </summary>
///     
		protected internal virtual void toggleDownfall()
		{
			WorldInfo var1 = MinecraftServer.Server.worldServers[0].WorldInfo;
			var1.Raining = !var1.Raining;
		}
	}

}