using System;
using System.Collections;

namespace DotCraftCore.nCommand.nServer
{

	using CommandBase = DotCraftCore.nCommand.CommandBase;
	using ICommandSender = DotCraftCore.nCommand.ICommandSender;
	using PlayerNotFoundException = DotCraftCore.nCommand.PlayerNotFoundException;
	using WrongUsageException = DotCraftCore.nCommand.WrongUsageException;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using EntityPlayerMP = DotCraftCore.nEntity.nPlayer.EntityPlayerMP;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.nUtil.ChatComponentTranslation;
	using EnumChatFormatting = DotCraftCore.nUtil.EnumChatFormatting;
	using IChatComponent = DotCraftCore.nUtil.IChatComponent;

	public class CommandMessage : CommandBase
	{
		

		public override IList CommandAliases
		{
			get
			{
				return new string[] {"w", "msg"};
			}
		}

		public virtual string CommandName
		{
			get
			{
				return "tell";
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
			return "commands.message.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length < 2)
			{
				throw new WrongUsageException("commands.message.usage", new object[0]);
			}
			else
			{
				EntityPlayerMP var3 = GetPlayer(p_71515_1_, p_71515_2_[0]);

				if(var3 == null)
				{
					throw new PlayerNotFoundException();
				}
				else if(var3 == p_71515_1_)
				{
					throw new PlayerNotFoundException("commands.message.sameTarget", new object[0]);
				}
				else
				{
					IChatComponent var4 = func_147176_a(p_71515_1_, p_71515_2_, 1, !(p_71515_1_ is EntityPlayer));
					ChatComponentTranslation var5 = new ChatComponentTranslation("commands.message.display.incoming", new object[] {p_71515_1_.func_145748_c_(), var4.createCopy()});
					ChatComponentTranslation var6 = new ChatComponentTranslation("commands.message.display.outgoing", new object[] {var3.func_145748_c_(), var4.createCopy()});
					var5.ChatStyle.setColor(EnumChatFormatting.GRAY).setItalic(Convert.ToBoolean(true));
					var6.ChatStyle.setColor(EnumChatFormatting.GRAY).setItalic(Convert.ToBoolean(true));
					var3.addChatMessage(var5);
					p_71515_1_.AddChatMessage(var6);
				}
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList AddTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return GetListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.AllUsernames);
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