using System.Collections;

namespace DotCraftCore.Command.Server
{

	using GameProfile = com.mojang.authlib.GameProfile;
	using CommandBase = DotCraftCore.Command.CommandBase;
	using CommandException = DotCraftCore.Command.CommandException;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using WrongUsageException = DotCraftCore.Command.WrongUsageException;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;

	public class CommandPardonPlayer : CommandBase
	{
		private const string __OBFID = "CL_00000747";

		public virtual string CommandName
		{
			get
			{
				return "pardon";
			}
		}

///    
///     <summary> * Return the required permission level for this command. </summary>
///     
		public override int RequiredPermissionLevel
		{
			get
			{
				return 3;
			}
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.unban.usage";
		}

///    
///     <summary> * Returns true if the given command sender is allowed to use this command. </summary>
///     
		public override bool canCommandSenderUseCommand(ICommandSender p_71519_1_)
		{
			return MinecraftServer.Server.ConfigurationManager.func_152608_h().func_152689_b() && base.canCommandSenderUseCommand(p_71519_1_);
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length == 1 && p_71515_2_[0].Length > 0)
			{
				MinecraftServer var3 = MinecraftServer.Server;
				GameProfile var4 = var3.ConfigurationManager.func_152608_h().func_152703_a(p_71515_2_[0]);

				if(var4 == null)
				{
					throw new CommandException("commands.unban.failed", new object[] {p_71515_2_[0]});
				}
				else
				{
					var3.ConfigurationManager.func_152608_h().func_152684_c(var4);
					func_152373_a(p_71515_1_, this, "commands.unban.success", new object[] {p_71515_2_[0]});
				}
			}
			else
			{
				throw new WrongUsageException("commands.unban.usage", new object[0]);
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? getListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.ConfigurationManager.func_152608_h().func_152685_a()) : null;
		}
	}

}