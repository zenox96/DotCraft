using System;
using System.Collections;

namespace DotCraftCore.nCommand
{

	using CommandAchievement = DotCraftCore.nCommand.nServer.CommandAchievement;
	using CommandBanIp = DotCraftCore.nCommand.nServer.CommandBanIp;
	using CommandBanPlayer = DotCraftCore.nCommand.nServer.CommandBanPlayer;
	using CommandBlockLogic = DotCraftCore.nCommand.nServer.CommandBlockLogic;
	using CommandBroadcast = DotCraftCore.nCommand.nServer.CommandBroadcast;
	using CommandDeOp = DotCraftCore.nCommand.nServer.CommandDeOp;
	using CommandEmote = DotCraftCore.nCommand.nServer.CommandEmote;
	using CommandListBans = DotCraftCore.nCommand.nServer.CommandListBans;
	using CommandListPlayers = DotCraftCore.nCommand.nServer.CommandListPlayers;
	using CommandMessage = DotCraftCore.nCommand.nServer.CommandMessage;
	using CommandMessageRaw = DotCraftCore.nCommand.nServer.CommandMessageRaw;
	using CommandNetstat = DotCraftCore.nCommand.nServer.CommandNetstat;
	using CommandOp = DotCraftCore.nCommand.nServer.CommandOp;
	using CommandPardonIp = DotCraftCore.nCommand.nServer.CommandPardonIp;
	using CommandPardonPlayer = DotCraftCore.nCommand.nServer.CommandPardonPlayer;
	using CommandPublishLocalServer = DotCraftCore.nCommand.nServer.CommandPublishLocalServer;
	using CommandSaveAll = DotCraftCore.nCommand.nServer.CommandSaveAll;
	using CommandSaveOff = DotCraftCore.nCommand.nServer.CommandSaveOff;
	using CommandSaveOn = DotCraftCore.nCommand.nServer.CommandSaveOn;
	using CommandScoreboard = DotCraftCore.nCommand.nServer.CommandScoreboard;
	using CommandSetBlock = DotCraftCore.nCommand.nServer.CommandSetBlock;
	using CommandSetDefaultSpawnpoint = DotCraftCore.nCommand.nServer.CommandSetDefaultSpawnpoint;
	using CommandStop = DotCraftCore.nCommand.nServer.CommandStop;
	using CommandSummon = DotCraftCore.nCommand.nServer.CommandSummon;
	using CommandTeleport = DotCraftCore.nCommand.nServer.CommandTeleport;
	using CommandTestFor = DotCraftCore.nCommand.nServer.CommandTestFor;
	using CommandTestForBlock = DotCraftCore.nCommand.nServer.CommandTestForBlock;
	using CommandWhitelist = DotCraftCore.nCommand.nServer.CommandWhitelist;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using RConConsoleSource = DotCraftCore.nNetwork.nRcon.RConConsoleSource;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.nUtil.ChatComponentTranslation;
	using EnumChatFormatting = DotCraftCore.nUtil.EnumChatFormatting;

	public class ServerCommandManager : CommandHandler, IAdminCommand
	{
		

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