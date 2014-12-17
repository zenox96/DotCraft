using System;
using System.Collections;

namespace DotCraftCore.nCommand.nServer
{

	using CommandBase = DotCraftCore.nCommand.CommandBase;
	using ICommandSender = DotCraftCore.nCommand.ICommandSender;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using ChatComponentText = DotCraftCore.nUtil.ChatComponentText;
	using ChatComponentTranslation = DotCraftCore.nUtil.ChatComponentTranslation;

	public class CommandListBans : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "banlist";
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
		public override bool canCommandSenderUseCommand(ICommandSender p_71519_1_)
		{
			return (MinecraftServer.Server.ConfigurationManager.BannedIPs.func_152689_b() || MinecraftServer.Server.ConfigurationManager.func_152608_h().func_152689_b()) && base.canCommandSenderUseCommand(p_71519_1_);
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.banlist.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length >= 1 && p_71515_2_[0].equalsIgnoreCase("ips"))
			{
				p_71515_1_.addChatMessage(new ChatComponentTranslation("commands.banlist.ips", new object[] {Convert.ToInt32(MinecraftServer.Server.ConfigurationManager.BannedIPs.func_152685_a().length)}));
				p_71515_1_.addChatMessage(new ChatComponentText(joinNiceString(MinecraftServer.Server.ConfigurationManager.BannedIPs.func_152685_a())));
			}
			else
			{
				p_71515_1_.addChatMessage(new ChatComponentTranslation("commands.banlist.players", new object[] {Convert.ToInt32(MinecraftServer.Server.ConfigurationManager.func_152608_h().func_152685_a().length)}));
				p_71515_1_.addChatMessage(new ChatComponentText(joinNiceString(MinecraftServer.Server.ConfigurationManager.func_152608_h().func_152685_a())));
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"players", "ips"}): null;
		}
	}

}