using System;
using System.Collections;

namespace DotCraftCore.Command.Server
{

	using CommandBase = DotCraftCore.Command.CommandBase;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using PlayerNotFoundException = DotCraftCore.Command.PlayerNotFoundException;
	using WrongUsageException = DotCraftCore.Command.WrongUsageException;
	using EntityPlayerMP = DotCraftCore.Entity.Player.EntityPlayerMP;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using IPBanEntry = DotCraftCore.Server.Management.IPBanEntry;
	using IChatComponent = DotCraftCore.Util.IChatComponent;

	public class CommandBanIp : CommandBase
	{
		public static readonly Pattern field_147211_a = Pattern.compile("^([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])\\.([01]?\\d\\d?|2[0-4]\\d|25[0-5])$");
		

		public virtual string CommandName
		{
			get
			{
				return "ban-ip";
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
			return MinecraftServer.Server.ConfigurationManager.BannedIPs.func_152689_b() && base.canCommandSenderUseCommand(p_71519_1_);
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.banip.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length >= 1 && p_71515_2_[0].Length > 1)
			{
				Matcher var3 = field_147211_a.matcher(p_71515_2_[0]);
				IChatComponent var4 = null;

				if(p_71515_2_.Length >= 2)
				{
					var4 = func_147178_a(p_71515_1_, p_71515_2_, 1);
				}

				if(var3.matches())
				{
					this.func_147210_a(p_71515_1_, p_71515_2_[0], var4 == null ? null : var4.UnformattedText);
				}
				else
				{
					EntityPlayerMP var5 = MinecraftServer.Server.ConfigurationManager.func_152612_a(p_71515_2_[0]);

					if(var5 == null)
					{
						throw new PlayerNotFoundException("commands.banip.invalid", new object[0]);
					}

					this.func_147210_a(p_71515_1_, var5.PlayerIP, var4 == null ? null : var4.UnformattedText);
				}
			}
			else
			{
				throw new WrongUsageException("commands.banip.usage", new object[0]);
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? getListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.AllUsernames) : null;
		}

		protected internal virtual void func_147210_a(ICommandSender p_147210_1_, string p_147210_2_, string p_147210_3_)
		{
			IPBanEntry var4 = new IPBanEntry(p_147210_2_, (DateTime)null, p_147210_1_.CommandSenderName, (DateTime)null, p_147210_3_);
			MinecraftServer.Server.ConfigurationManager.BannedIPs.func_152687_a(var4);
			IList var5 = MinecraftServer.Server.ConfigurationManager.getPlayerList(p_147210_2_);
			string[] var6 = new string[var5.Count];
			int var7 = 0;
			EntityPlayerMP var9;

			for (IEnumerator var8 = var5.GetEnumerator(); var8.MoveNext(); var6[var7++] = var9.CommandSenderName)
			{
				var9 = (EntityPlayerMP)var8.Current;
				var9.playerNetServerHandler.kickPlayerFromServer("You have been IP banned.");
			}

			if(var5.Count == 0)
			{
				func_152373_a(p_147210_1_, this, "commands.banip.success", new object[] {p_147210_2_});
			}
			else
			{
				func_152373_a(p_147210_1_, this, "commands.banip.success.players", new object[] {p_147210_2_, joinNiceString(var6)});
			}
		}
	}

}