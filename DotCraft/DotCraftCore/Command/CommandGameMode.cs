using System.Collections;

namespace DotCraftCore.Command
{

	using EntityPlayerMP = DotCraftCore.Entity.Player.EntityPlayerMP;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.Util.ChatComponentTranslation;
	using WorldSettings = DotCraftCore.World.WorldSettings;

	public class CommandGameMode : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "gamemode";
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
			return "commands.gamemode.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length > 0)
			{
				WorldSettings.GameType var3 = this.getGameModeFromCommand(p_71515_1_, p_71515_2_[0]);
				EntityPlayerMP var4 = p_71515_2_.Length >= 2 ? getPlayer(p_71515_1_, p_71515_2_[1]) : getCommandSenderAsPlayer(p_71515_1_);
				var4.GameType = var3;
				var4.fallDistance = 0.0F;
				ChatComponentTranslation var5 = new ChatComponentTranslation("gameMode." + var3.Name, new object[0]);

				if(var4 != p_71515_1_)
				{
					func_152374_a(p_71515_1_, this, 1, "commands.gamemode.success.other", new object[] {var4.CommandSenderName, var5});
				}
				else
				{
					func_152374_a(p_71515_1_, this, 1, "commands.gamemode.success.self", new object[] {var5});
				}
			}
			else
			{
				throw new WrongUsageException("commands.gamemode.usage", new object[0]);
			}
		}

///    
///     <summary> * Gets the Game Mode specified in the command. </summary>
///     
		protected internal virtual WorldSettings.GameType getGameModeFromCommand(ICommandSender p_71539_1_, string p_71539_2_)
		{
			return !p_71539_2_.equalsIgnoreCase(WorldSettings.GameType.SURVIVAL.Name) && !p_71539_2_.equalsIgnoreCase("s") ? (!p_71539_2_.equalsIgnoreCase(WorldSettings.GameType.CREATIVE.Name) && !p_71539_2_.equalsIgnoreCase("c") ? (!p_71539_2_.equalsIgnoreCase(WorldSettings.GameType.ADVENTURE.Name) && !p_71539_2_.equalsIgnoreCase("a") ? WorldSettings.getGameTypeById(parseIntBounded(p_71539_1_, p_71539_2_, 0, WorldSettings.GameType.values().length - 2)) : WorldSettings.GameType.ADVENTURE) : WorldSettings.GameType.CREATIVE) : WorldSettings.GameType.SURVIVAL;
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"survival", "creative", "adventure"}): (p_71516_2_.Length == 2 ? getListOfStringsMatchingLastWord(p_71516_2_, this.ListOfPlayerUsernames) : null);
		}

///    
///     <summary> * Returns String array containing all player usernames in the server. </summary>
///     
		protected internal virtual string[] ListOfPlayerUsernames
		{
			get
			{
				return MinecraftServer.Server.AllUsernames;
			}
		}

///    
///     <summary> * Return whether the specified command parameter index is a username parameter. </summary>
///     
		public override bool isUsernameIndex(string[] p_82358_1_, int p_82358_2_)
		{
			return p_82358_2_ == 1;
		}
	}

}