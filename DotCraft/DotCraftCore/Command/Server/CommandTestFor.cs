namespace DotCraftCore.Command.Server
{

	using CommandBase = DotCraftCore.Command.CommandBase;
	using CommandException = DotCraftCore.Command.CommandException;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using WrongUsageException = DotCraftCore.Command.WrongUsageException;

	public class CommandTestFor : CommandBase
	{
		private const string __OBFID = "CL_00001182";

		public virtual string CommandName
		{
			get
			{
				return "testfor";
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
			return "commands.testfor.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length != 1)
			{
				throw new WrongUsageException("commands.testfor.usage", new object[0]);
			}
			else if(!(p_71515_1_ is CommandBlockLogic))
			{
				throw new CommandException("commands.testfor.failed", new object[0]);
			}
			else
			{
				getPlayer(p_71515_1_, p_71515_2_[0]);
			}
		}

///    
///     <summary> * Return whether the specified command parameter index is a username parameter. </summary>
///     
		public override bool isUsernameIndex(string[] p_82358_1_, int p_82358_2_)
		{
			return p_82358_2_ == 0;
		}
	}

}