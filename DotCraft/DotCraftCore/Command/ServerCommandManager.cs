using System;
using System.Collections;

namespace DotCraftCore.Command
{

	using CommandAchievement = DotCraftCore.Command.Server.CommandAchievement;
	using CommandBanIp = DotCraftCore.Command.Server.CommandBanIp;
	using CommandBanPlayer = DotCraftCore.Command.Server.CommandBanPlayer;
	using CommandBlockLogic = DotCraftCore.Command.Server.CommandBlockLogic;
	using CommandBroadcast = DotCraftCore.Command.Server.CommandBroadcast;
	using CommandDeOp = DotCraftCore.Command.Server.CommandDeOp;
	using CommandEmote = DotCraftCore.Command.Server.CommandEmote;
	using CommandListBans = DotCraftCore.Command.Server.CommandListBans;
	using CommandListPlayers = DotCraftCore.Command.Server.CommandListPlayers;
	using CommandMessage = DotCraftCore.Command.Server.CommandMessage;
	using CommandMessageRaw = DotCraftCore.Command.Server.CommandMessageRaw;
	using CommandNetstat = DotCraftCore.Command.Server.CommandNetstat;
	using CommandOp = DotCraftCore.Command.Server.CommandOp;
	using CommandPardonIp = DotCraftCore.Command.Server.CommandPardonIp;
	using CommandPardonPlayer = DotCraftCore.Command.Server.CommandPardonPlayer;
	using CommandPublishLocalServer = DotCraftCore.Command.Server.CommandPublishLocalServer;
	using CommandSaveAll = DotCraftCore.Command.Server.CommandSaveAll;
	using CommandSaveOff = DotCraftCore.Command.Server.CommandSaveOff;
	using CommandSaveOn = DotCraftCore.Command.Server.CommandSaveOn;
	using CommandScoreboard = DotCraftCore.Command.Server.CommandScoreboard;
	using CommandSetBlock = DotCraftCore.Command.Server.CommandSetBlock;
	using CommandSetDefaultSpawnpoint = DotCraftCore.Command.Server.CommandSetDefaultSpawnpoint;
	using CommandStop = DotCraftCore.Command.Server.CommandStop;
	using CommandSummon = DotCraftCore.Command.Server.CommandSummon;
	using CommandTeleport = DotCraftCore.Command.Server.CommandTeleport;
	using CommandTestFor = DotCraftCore.Command.Server.CommandTestFor;
	using CommandTestForBlock = DotCraftCore.Command.Server.CommandTestForBlock;
	using CommandWhitelist = DotCraftCore.Command.Server.CommandWhitelist;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using RConConsoleSource = DotCraftCore.network.rcon.RConConsoleSource;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.util.ChatComponentTranslation;
	using EnumChatFormatting = DotCraftCore.util.EnumChatFormatting;

	public class ServerCommandManager : CommandHandler, IAdminCommand
	{
		private const string __OBFID = "CL_00000922";

		public ServerCommandManager()
		{
			this.registerCommand(new CommandTime());
			this.registerCommand(new CommandGameMode());
			this.registerCommand(new CommandDifficulty());
			this.registerCommand(new CommandDefaultGameMode());
			this.registerCommand(new CommandKill());
			this.registerCommand(new CommandToggleDownfall());
			this.registerCommand(new CommandWeather());
			this.registerCommand(new CommandXP());
			this.registerCommand(new CommandTeleport());
			this.registerCommand(new CommandGive());
			this.registerCommand(new CommandEffect());
			this.registerCommand(new CommandEnchant());
			this.registerCommand(new CommandEmote());
			this.registerCommand(new CommandShowSeed());
			this.registerCommand(new CommandHelp());
			this.registerCommand(new CommandDebug());
			this.registerCommand(new CommandMessage());
			this.registerCommand(new CommandBroadcast());
			this.registerCommand(new CommandSetSpawnpoint());
			this.registerCommand(new CommandSetDefaultSpawnpoint());
			this.registerCommand(new CommandGameRule());
			this.registerCommand(new CommandClearInventory());
			this.registerCommand(new CommandTestFor());
			this.registerCommand(new CommandSpreadPlayers());
			this.registerCommand(new CommandPlaySound());
			this.registerCommand(new CommandScoreboard());
			this.registerCommand(new CommandAchievement());
			this.registerCommand(new CommandSummon());
			this.registerCommand(new CommandSetBlock());
			this.registerCommand(new CommandTestForBlock());
			this.registerCommand(new CommandMessageRaw());

			if(MinecraftServer.Server.DedicatedServer)
			{
				this.registerCommand(new CommandOp());
				this.registerCommand(new CommandDeOp());
				this.registerCommand(new CommandStop());
				this.registerCommand(new CommandSaveAll());
				this.registerCommand(new CommandSaveOff());
				this.registerCommand(new CommandSaveOn());
				this.registerCommand(new CommandBanIp());
				this.registerCommand(new CommandPardonIp());
				this.registerCommand(new CommandBanPlayer());
				this.registerCommand(new CommandListBans());
				this.registerCommand(new CommandPardonPlayer());
				this.registerCommand(new CommandServerKick());
				this.registerCommand(new CommandListPlayers());
				this.registerCommand(new CommandWhitelist());
				this.registerCommand(new CommandSetPlayerTimeout());
				this.registerCommand(new CommandNetstat());
			}
			else
			{
				this.registerCommand(new CommandPublishLocalServer());
			}

			CommandBase.AdminCommander = this;
		}

		public virtual void func_152372_a(ICommandSender p_152372_1_, ICommand p_152372_2_, int p_152372_3_, string p_152372_4_, params object[] p_152372_5_)
		{
			bool var6 = true;

			if(p_152372_1_ is CommandBlockLogic && !MinecraftServer.Server.worldServers[0].GameRules.getGameRuleBooleanValue("commandBlockOutput"))
			{
				var6 = false;
			}

			ChatComponentTranslation var7 = new ChatComponentTranslation("chat.type.admin", new object[] {p_152372_1_.CommandSenderName, new ChatComponentTranslation(p_152372_4_, p_152372_5_)});
			var7.ChatStyle.Color = EnumChatFormatting.GRAY;
			var7.ChatStyle.Italic = Convert.ToBoolean(true);

			if(var6)
			{
				IEnumerator var8 = MinecraftServer.Server.ConfigurationManager.playerEntityList.GetEnumerator();

				while (var8.MoveNext())
				{
					EntityPlayer var9 = (EntityPlayer)var8.Current;

					if(var9 != p_152372_1_ && MinecraftServer.Server.ConfigurationManager.func_152596_g(var9.GameProfile) && p_152372_2_.canCommandSenderUseCommand(var9) && (!(p_152372_1_ is RConConsoleSource) || MinecraftServer.Server.func_152363_m()))
					{
						var9.addChatMessage(var7);
					}
				}
			}

			if(p_152372_1_ != MinecraftServer.Server)
			{
				MinecraftServer.Server.addChatMessage(var7);
			}

			if((p_152372_3_ & 1) != 1)
			{
				p_152372_1_.addChatMessage(new ChatComponentTranslation(p_152372_4_, p_152372_5_));
			}
		}
	}

}