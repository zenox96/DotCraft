using System;

namespace DotCraftCore.nCommand.nServer
{

	using CommandBase = DotCraftCore.nCommand.CommandBase;
	using ICommandSender = DotCraftCore.nCommand.ICommandSender;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using ChatComponentText = DotCraftCore.nUtil.ChatComponentText;
	using ChatComponentTranslation = DotCraftCore.nUtil.ChatComponentTranslation;

	public class CommandListPlayers : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "list";
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
			return "commands.players.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			p_71515_1_.addChatMessage(new ChatComponentTranslation("commands.players.list", new object[] {Convert.ToInt32(MinecraftServer.Server.CurrentPlayerCount), Convert.ToInt32(MinecraftServer.Server.MaxPlayers)}));
			p_71515_1_.addChatMessage(new ChatComponentText(MinecraftServer.Server.ConfigurationManager.func_152609_b(p_71515_2_.Length > 0 && "uuids".equalsIgnoreCase(p_71515_2_[0]))));
		}
	}

}