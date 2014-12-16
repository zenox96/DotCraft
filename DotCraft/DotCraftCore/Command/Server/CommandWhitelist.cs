using System;
using System.Collections;

namespace DotCraftCore.Command.Server
{

	using GameProfile = com.mojang.authlib.GameProfile;
	using CommandBase = DotCraftCore.Command.CommandBase;
	using CommandException = DotCraftCore.Command.CommandException;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using WrongUsageException = DotCraftCore.Command.WrongUsageException;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using ChatComponentText = DotCraftCore.Util.ChatComponentText;
	using ChatComponentTranslation = DotCraftCore.Util.ChatComponentTranslation;

	public class CommandWhitelist : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "whitelist";
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
			return "commands.whitelist.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length >= 1)
			{
				MinecraftServer var3 = MinecraftServer.Server;

				if(p_71515_2_[0].Equals("on"))
				{
					var3.ConfigurationManager.WhiteListEnabled = true;
					func_152373_a(p_71515_1_, this, "commands.whitelist.enabled", new object[0]);
					return;
				}

				if(p_71515_2_[0].Equals("off"))
				{
					var3.ConfigurationManager.WhiteListEnabled = false;
					func_152373_a(p_71515_1_, this, "commands.whitelist.disabled", new object[0]);
					return;
				}

				if(p_71515_2_[0].Equals("list"))
				{
					p_71515_1_.addChatMessage(new ChatComponentTranslation("commands.whitelist.list", new object[] {Convert.ToInt32(var3.ConfigurationManager.func_152598_l().length), Convert.ToInt32(var3.ConfigurationManager.AvailablePlayerDat.length)}));
					string[] var5 = var3.ConfigurationManager.func_152598_l();
					p_71515_1_.addChatMessage(new ChatComponentText(joinNiceString(var5)));
					return;
				}

				GameProfile var4;

				if(p_71515_2_[0].Equals("add"))
				{
					if(p_71515_2_.Length < 2)
					{
						throw new WrongUsageException("commands.whitelist.add.usage", new object[0]);
					}

					var4 = var3.func_152358_ax().func_152655_a(p_71515_2_[1]);

					if(var4 == null)
					{
						throw new CommandException("commands.whitelist.add.failed", new object[] {p_71515_2_[1]});
					}

					var3.ConfigurationManager.func_152601_d(var4);
					func_152373_a(p_71515_1_, this, "commands.whitelist.add.success", new object[] {p_71515_2_[1]});
					return;
				}

				if(p_71515_2_[0].Equals("remove"))
				{
					if(p_71515_2_.Length < 2)
					{
						throw new WrongUsageException("commands.whitelist.remove.usage", new object[0]);
					}

					var4 = var3.ConfigurationManager.func_152599_k().func_152706_a(p_71515_2_[1]);

					if(var4 == null)
					{
						throw new CommandException("commands.whitelist.remove.failed", new object[] {p_71515_2_[1]});
					}

					var3.ConfigurationManager.func_152597_c(var4);
					func_152373_a(p_71515_1_, this, "commands.whitelist.remove.success", new object[] {p_71515_2_[1]});
					return;
				}

				if(p_71515_2_[0].Equals("reload"))
				{
					var3.ConfigurationManager.loadWhiteList();
					func_152373_a(p_71515_1_, this, "commands.whitelist.reloaded", new object[0]);
					return;
				}
			}

			throw new WrongUsageException("commands.whitelist.usage", new object[0]);
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			if(p_71516_2_.Length == 1)
			{
				return getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"on", "off", "list", "add", "remove", "reload"});
			}
			else
			{
				if(p_71516_2_.Length == 2)
				{
					if(p_71516_2_[0].Equals("remove"))
					{
						return getListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.ConfigurationManager.func_152598_l());
					}

					if(p_71516_2_[0].Equals("add"))
					{
						return getListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.func_152358_ax().func_152654_a());
					}
				}

				return null;
			}
		}
	}

}