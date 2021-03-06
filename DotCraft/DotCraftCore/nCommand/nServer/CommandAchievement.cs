using System.Collections;

namespace DotCraftCore.nCommand.nServer
{
	using EntityPlayerMP = DotCraftCore.nEntity.nPlayer.EntityPlayerMP;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using Achievement = DotCraftCore.nStats.Achievement;
	using AchievementList = DotCraftCore.nStats.AchievementList;
	using StatBase = DotCraftCore.nStats.StatBase;
	using StatList = DotCraftCore.nStats.StatList;

	public class CommandAchievement : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "achievement";
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
			return "commands.achievement.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length >= 2)
			{
				StatBase var3 = StatList.func_151177_a(p_71515_2_[1]);

				if(var3 == null && !p_71515_2_[1].Equals("*"))
				{
					throw new CommandException("commands.achievement.unknownAchievement", new object[] {p_71515_2_[1]});
				}

				EntityPlayerMP var4;

				if(p_71515_2_.Length >= 3)
				{
					var4 = GetPlayer(p_71515_1_, p_71515_2_[2]);
				}
				else
				{
					var4 = GetCommandSenderAsPlayer(p_71515_1_);
				}

				if(p_71515_2_[0].equalsIgnoreCase("give"))
				{
					if(var3 == null)
					{
						IEnumerator var5 = AchievementList.achievementList.GetEnumerator();

						while (var5.MoveNext())
						{
							Achievement var6 = (Achievement)var5.Current;
							var4.triggerAchievement(var6);
						}

						func_152373_a(p_71515_1_, this, "commands.achievement.give.success.all", new object[] {var4.CommandSenderName});
					}
					else
					{
						if(var3 is Achievement)
						{
							Achievement var9 = (Achievement)var3;
							ArrayList var10;

							for (var10 = Lists.newArrayList(); var9.parentAchievement != null && !var4.func_147099_x().hasAchievementUnlocked(var9.parentAchievement); var9 = var9.parentAchievement)
							{
								var10.Add(var9.parentAchievement);
							}

							IEnumerator var7 = Lists.reverse(var10).GetEnumerator();

							while (var7.MoveNext())
							{
								Achievement var8 = (Achievement)var7.Current;
								var4.triggerAchievement(var8);
							}
						}

						var4.triggerAchievement(var3);
						func_152373_a(p_71515_1_, this, "commands.achievement.give.success.one", new object[] {var4.CommandSenderName, var3.func_150955_j()});
					}

					return;
				}
			}

			throw new WrongUsageException("commands.achievement.usage", new object[0]);
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList AddTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			if(p_71516_2_.Length == 1)
			{
				return GetListOfStringsMatchingLastWord(p_71516_2_, new string[] {"give"});
			}
			else if(p_71516_2_.Length != 2)
			{
				return p_71516_2_.Length == 3 ? GetListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.AllUsernames) : null;
			}
			else
			{
				ArrayList var3 = Lists.newArrayList();
				IEnumerator var4 = StatList.allStats.GetEnumerator();

				while (var4.MoveNext())
				{
					StatBase var5 = (StatBase)var4.Current;
					var3.Add(var5.statId);
				}

				return GetListOfStringsFromIterableMatchingLastWord(p_71516_2_, var3);
			}
		}

///    
///     <summary> * Return whether the specified command parameter index is a username parameter. </summary>
///     
		public override bool IsUsernameIndex(string[] p_82358_1_, int p_82358_2_)
		{
			return p_82358_2_ == 2;
		}
	}

}