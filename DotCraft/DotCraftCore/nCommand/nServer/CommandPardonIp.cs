using System.Collections;

namespace DotCraftCore.nCommand.nServer
{

	using CommandBase = DotCraftCore.nCommand.CommandBase;
	using ICommandSender = DotCraftCore.nCommand.ICommandSender;
	using SyntaxErrorException = DotCraftCore.nCommand.SyntaxErrorException;
	using WrongUsageException = DotCraftCore.nCommand.WrongUsageException;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;

	public class CommandPardonIp : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "pardon-ip";
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

///    
///     <summary> * Returns true if the given command sender is allowed to use this command. </summary>
///     
		public override bool CanCommandSenderUseCommand(ICommandSender p_71519_1_)
		{
			return MinecraftServer.Server.ConfigurationManager.BannedIPs.func_152689_b() && base.CanCommandSenderUseCommand(p_71519_1_);
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.unbanip.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length == 1 && p_71515_2_[0].Length > 1)
			{
				Matcher var3 = CommandBanIp.field_147211_a.matcher(p_71515_2_[0]);

				if(var3.matches())
				{
					MinecraftServer.Server.ConfigurationManager.BannedIPs.func_152684_c(p_71515_2_[0]);
					func_152373_a(p_71515_1_, this, "commands.unbanip.success", new object[] {p_71515_2_[0]});
				}
				else
				{
					throw new SyntaxErrorException("commands.unbanip.invalid", new object[0]);
				}
			}
			else
			{
				throw new WrongUsageException("commands.unbanip.usage", new object[0]);
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList AddTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? GetListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.ConfigurationManager.BannedIPs.func_152685_a()) : null;
		}
	}

}