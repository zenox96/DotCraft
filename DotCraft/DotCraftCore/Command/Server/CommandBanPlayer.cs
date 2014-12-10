using System;
using System.Collections;

namespace DotCraftCore.Command.Server
{

	using GameProfile = com.mojang.authlib.GameProfile;
	using CommandBase = DotCraftCore.Command.CommandBase;
	using CommandException = DotCraftCore.Command.CommandException;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using WrongUsageException = DotCraftCore.Command.WrongUsageException;
	using EntityPlayerMP = DotCraftCore.Entity.Player.EntityPlayerMP;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;
	using UserListBansEntry = DotCraftCore.server.management.UserListBansEntry;

	public class CommandBanPlayer : CommandBase
	{
		private const string __OBFID = "CL_00000165";

		public virtual string CommandName
		{
			get
			{
				return "ban";
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
			return "commands.ban.usage";
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
			if(p_71515_2_.Length >= 1 && p_71515_2_[0].Length > 0)
			{
				MinecraftServer var3 = MinecraftServer.Server;
				GameProfile var4 = var3.func_152358_ax().func_152655_a(p_71515_2_[0]);

				if(var4 == null)
				{
					throw new CommandException("commands.ban.failed", new object[] {p_71515_2_[0]});
				}
				else
				{
					string var5 = null;

					if(p_71515_2_.Length >= 2)
					{
						var5 = func_147178_a(p_71515_1_, p_71515_2_, 1).UnformattedText;
					}

					UserListBansEntry var6 = new UserListBansEntry(var4, (DateTime)null, p_71515_1_.CommandSenderName, (DateTime)null, var5);
					var3.ConfigurationManager.func_152608_h().func_152687_a(var6);
					EntityPlayerMP var7 = var3.ConfigurationManager.func_152612_a(p_71515_2_[0]);

					if(var7 != null)
					{
						var7.playerNetServerHandler.kickPlayerFromServer("You are banned from this server.");
					}

					func_152373_a(p_71515_1_, this, "commands.ban.success", new object[] {p_71515_2_[0]});
				}
			}
			else
			{
				throw new WrongUsageException("commands.ban.usage", new object[0]);
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