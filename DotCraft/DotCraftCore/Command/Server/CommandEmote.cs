using System.Collections;

namespace DotCraftCore.Command.Server
{

	using CommandBase = DotCraftCore.Command.CommandBase;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using WrongUsageException = DotCraftCore.Command.WrongUsageException;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.Util.ChatComponentTranslation;
	using IChatComponent = DotCraftCore.Util.IChatComponent;

	public class CommandEmote : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "me";
			}
		}

///    
///     <summary> * Return the required permission level for this command. </summary>
///     
		public override int RequiredPermissionLevel
		{
			get
			{
				return 0;
			}
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.me.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length > 0)
			{
				IChatComponent var3 = func_147176_a(p_71515_1_, p_71515_2_, 0, p_71515_1_.canCommandSenderUseCommand(1, "me"));
				MinecraftServer.Server.ConfigurationManager.func_148539_a(new ChatComponentTranslation("chat.type.emote", new object[] {p_71515_1_.func_145748_c_(), var3}));
			}
			else
			{
				throw new WrongUsageException("commands.me.usage", new object[0]);
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return getListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.AllUsernames);
		}
	}

}