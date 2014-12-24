using System.Collections;

namespace DotCraftCore.nCommand.nServer
{

	using CommandBase = DotCraftCore.nCommand.CommandBase;
	using ICommandSender = DotCraftCore.nCommand.ICommandSender;
	using WrongUsageException = DotCraftCore.nCommand.WrongUsageException;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.nUtil.ChatComponentTranslation;
	using IChatComponent = DotCraftCore.nUtil.IChatComponent;

	public class CommandBroadcast : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "say";
			}
		}

///    
///     <summary> * Return the required permission level for this command. </summary>
///     
		public override int RequiredPermissionLevel
		{
			get
			{
				return 1;
			}
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.say.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length > 0 && p_71515_2_[0].Length > 0)
			{
				IChatComponent var3 = func_147176_a(p_71515_1_, p_71515_2_, 0, true);
				MinecraftServer.Server.ConfigurationManager.func_148539_a(new ChatComponentTranslation("chat.type.announcement", new object[] {p_71515_1_.CommandSenderName, var3}));
			}
			else
			{
				throw new WrongUsageException("commands.say.usage", new object[0]);
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList AddTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length >= 1 ? GetListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.AllUsernames) : null;
		}
	}

}