using System;
using System.Collections;

namespace DotCraftCore.nCommand.nServer
{

	using JsonParseException = com.google.gson.JsonParseException;
	using CommandBase = DotCraftCore.nCommand.CommandBase;
	using ICommandSender = DotCraftCore.nCommand.ICommandSender;
	using SyntaxErrorException = DotCraftCore.nCommand.SyntaxErrorException;
	using WrongUsageException = DotCraftCore.nCommand.WrongUsageException;
	using EntityPlayerMP = DotCraftCore.nEntity.nPlayer.EntityPlayerMP;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using IChatComponent = DotCraftCore.nUtil.IChatComponent;
	using ExceptionUtils = org.apache.commons.lang3.exception.ExceptionUtils;

	public class CommandMessageRaw : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "tellraw";
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
			return "commands.tellraw.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length < 2)
			{
				throw new WrongUsageException("commands.tellraw.usage", new object[0]);
			}
			else
			{
				EntityPlayerMP var3 = GetPlayer(p_71515_1_, p_71515_2_[0]);
				string var4 = func_82360_a(p_71515_1_, p_71515_2_, 1);

				try
				{
					IChatComponent var5 = IChatComponent.Serializer.func_150699_a(var4);
					var3.addChatMessage(var5);
				}
				catch (JsonParseException var7)
				{
					Exception var6 = ExceptionUtils.getRootCause(var7);
					throw new SyntaxErrorException("commands.tellraw.jsonException", new object[] {var6 == null ? "" : var6.Message});
				}
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList AddTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? GetListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.AllUsernames) : null;
		}

///    
///     <summary> * Return whether the specified command parameter index is a username parameter. </summary>
///     
		public override bool IsUsernameIndex(string[] p_82358_1_, int p_82358_2_)
		{
			return p_82358_2_ == 0;
		}
	}

}