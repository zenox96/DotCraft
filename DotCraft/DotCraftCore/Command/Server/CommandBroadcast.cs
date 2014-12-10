using System.Collections;

namespace DotCraftCore.Command.Server
{

	using CommandBase = DotCraftCore.Command.CommandBase;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using WrongUsageException = DotCraftCore.Command.WrongUsageException;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.util.ChatComponentTranslation;
	using IChatComponent = DotCraftCore.util.IChatComponent;

	public class CommandBroadcast : CommandBase
	{
		private const string __OBFID = "CL_00000191";

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
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length >= 1 ? getListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.AllUsernames) : null;
		}
	}

}