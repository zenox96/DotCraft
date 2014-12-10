using System;
using System.Collections;

namespace DotCraftCore.Command.Server
{

	using CommandBase = DotCraftCore.Command.CommandBase;
	using CommandException = DotCraftCore.Command.CommandException;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using SyntaxErrorException = DotCraftCore.Command.SyntaxErrorException;
	using WrongUsageException = DotCraftCore.Command.WrongUsageException;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using IScoreObjectiveCriteria = DotCraftCore.Scoreboard.IScoreObjectiveCriteria;
	using Score = DotCraftCore.Scoreboard.Score;
	using ScoreObjective = DotCraftCore.Scoreboard.ScoreObjective;
	using ScorePlayerTeam = DotCraftCore.Scoreboard.ScorePlayerTeam;
	using Scoreboard = DotCraftCore.Scoreboard.Scoreboard;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using ChatComponentText = DotCraftCore.Util.ChatComponentText;
	using ChatComponentTranslation = DotCraftCore.Util.ChatComponentTranslation;
	using EnumChatFormatting = DotCraftCore.Util.EnumChatFormatting;

	public class CommandScoreboard : CommandBase
	{
		private const string __OBFID = "CL_00000896";

		public virtual string CommandName
		{
			get
			{
				return "scoreboard";
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
			return "commands.scoreboard.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length >= 1)
			{
				if(p_71515_2_[0].equalsIgnoreCase("objectives"))
				{
					if(p_71515_2_.Length == 1)
					{
						throw new WrongUsageException("commands.scoreboard.objectives.usage", new object[0]);
					}

					if(p_71515_2_[1].equalsIgnoreCase("list"))
					{
						this.func_147196_d(p_71515_1_);
					}
					else if(p_71515_2_[1].equalsIgnoreCase("add"))
					{
						if(p_71515_2_.Length < 4)
						{
							throw new WrongUsageException("commands.scoreboard.objectives.add.usage", new object[0]);
						}

						this.func_147193_c(p_71515_1_, p_71515_2_, 2);
					}
					else if(p_71515_2_[1].equalsIgnoreCase("remove"))
					{
						if(p_71515_2_.Length != 3)
						{
							throw new WrongUsageException("commands.scoreboard.objectives.remove.usage", new object[0]);
						}

						this.func_147191_h(p_71515_1_, p_71515_2_[2]);
					}
					else
					{
						if(!p_71515_2_[1].equalsIgnoreCase("setdisplay"))
						{
							throw new WrongUsageException("commands.scoreboard.objectives.usage", new object[0]);
						}

						if(p_71515_2_.Length != 3 && p_71515_2_.Length != 4)
						{
							throw new WrongUsageException("commands.scoreboard.objectives.setdisplay.usage", new object[0]);
						}

						this.func_147198_k(p_71515_1_, p_71515_2_, 2);
					}

					return;
				}

				if(p_71515_2_[0].equalsIgnoreCase("players"))
				{
					if(p_71515_2_.Length == 1)
					{
						throw new WrongUsageException("commands.scoreboard.players.usage", new object[0]);
					}

					if(p_71515_2_[1].equalsIgnoreCase("list"))
					{
						if(p_71515_2_.Length > 3)
						{
							throw new WrongUsageException("commands.scoreboard.players.list.usage", new object[0]);
						}

						this.func_147195_l(p_71515_1_, p_71515_2_, 2);
					}
					else if(p_71515_2_[1].equalsIgnoreCase("add"))
					{
						if(p_71515_2_.Length != 5)
						{
							throw new WrongUsageException("commands.scoreboard.players.add.usage", new object[0]);
						}

						this.func_147197_m(p_71515_1_, p_71515_2_, 2);
					}
					else if(p_71515_2_[1].equalsIgnoreCase("remove"))
					{
						if(p_71515_2_.Length != 5)
						{
							throw new WrongUsageException("commands.scoreboard.players.remove.usage", new object[0]);
						}

						this.func_147197_m(p_71515_1_, p_71515_2_, 2);
					}
					else if(p_71515_2_[1].equalsIgnoreCase("set"))
					{
						if(p_71515_2_.Length != 5)
						{
							throw new WrongUsageException("commands.scoreboard.players.set.usage", new object[0]);
						}

						this.func_147197_m(p_71515_1_, p_71515_2_, 2);
					}
					else
					{
						if(!p_71515_2_[1].equalsIgnoreCase("reset"))
						{
							throw new WrongUsageException("commands.scoreboard.players.usage", new object[0]);
						}

						if(p_71515_2_.Length != 3)
						{
							throw new WrongUsageException("commands.scoreboard.players.reset.usage", new object[0]);
						}

						this.func_147187_n(p_71515_1_, p_71515_2_, 2);
					}

					return;
				}

				if(p_71515_2_[0].equalsIgnoreCase("teams"))
				{
					if(p_71515_2_.Length == 1)
					{
						throw new WrongUsageException("commands.scoreboard.teams.usage", new object[0]);
					}

					if(p_71515_2_[1].equalsIgnoreCase("list"))
					{
						if(p_71515_2_.Length > 3)
						{
							throw new WrongUsageException("commands.scoreboard.teams.list.usage", new object[0]);
						}

						this.func_147186_g(p_71515_1_, p_71515_2_, 2);
					}
					else if(p_71515_2_[1].equalsIgnoreCase("add"))
					{
						if(p_71515_2_.Length < 3)
						{
							throw new WrongUsageException("commands.scoreboard.teams.add.usage", new object[0]);
						}

						this.func_147185_d(p_71515_1_, p_71515_2_, 2);
					}
					else if(p_71515_2_[1].equalsIgnoreCase("remove"))
					{
						if(p_71515_2_.Length != 3)
						{
							throw new WrongUsageException("commands.scoreboard.teams.remove.usage", new object[0]);
						}

						this.func_147194_f(p_71515_1_, p_71515_2_, 2);
					}
					else if(p_71515_2_[1].equalsIgnoreCase("empty"))
					{
						if(p_71515_2_.Length != 3)
						{
							throw new WrongUsageException("commands.scoreboard.teams.empty.usage", new object[0]);
						}

						this.func_147188_j(p_71515_1_, p_71515_2_, 2);
					}
					else if(p_71515_2_[1].equalsIgnoreCase("join"))
					{
						if(p_71515_2_.Length < 4 && (p_71515_2_.Length != 3 || !(p_71515_1_ is EntityPlayer)))
						{
							throw new WrongUsageException("commands.scoreboard.teams.join.usage", new object[0]);
						}

						this.func_147190_h(p_71515_1_, p_71515_2_, 2);
					}
					else if(p_71515_2_[1].equalsIgnoreCase("leave"))
					{
						if(p_71515_2_.Length < 3 && !(p_71515_1_ is EntityPlayer))
						{
							throw new WrongUsageException("commands.scoreboard.teams.leave.usage", new object[0]);
						}

						this.func_147199_i(p_71515_1_, p_71515_2_, 2);
					}
					else
					{
						if(!p_71515_2_[1].equalsIgnoreCase("option"))
						{
							throw new WrongUsageException("commands.scoreboard.teams.usage", new object[0]);
						}

						if(p_71515_2_.Length != 4 && p_71515_2_.Length != 5)
						{
							throw new WrongUsageException("commands.scoreboard.teams.option.usage", new object[0]);
						}

						this.func_147200_e(p_71515_1_, p_71515_2_, 2);
					}

					return;
				}
			}

			throw new WrongUsageException("commands.scoreboard.usage", new object[0]);
		}

		protected internal virtual Scoreboard func_147192_d()
		{
			return MinecraftServer.Server.worldServerForDimension(0).Scoreboard;
		}

		protected internal virtual ScoreObjective func_147189_a(string p_147189_1_, bool p_147189_2_)
		{
			Scoreboard var3 = this.func_147192_d();
			ScoreObjective var4 = var3.getObjective(p_147189_1_);

			if(var4 == null)
			{
				throw new CommandException("commands.scoreboard.objectiveNotFound", new object[] {p_147189_1_});
			}
			else if(p_147189_2_ && var4.Criteria.ReadOnly)
			{
				throw new CommandException("commands.scoreboard.objectiveReadOnly", new object[] {p_147189_1_});
			}
			else
			{
				return var4;
			}
		}

		protected internal virtual ScorePlayerTeam func_147183_a(string p_147183_1_)
		{
			Scoreboard var2 = this.func_147192_d();
			ScorePlayerTeam var3 = var2.getTeam(p_147183_1_);

			if(var3 == null)
			{
				throw new CommandException("commands.scoreboard.teamNotFound", new object[] {p_147183_1_});
			}
			else
			{
				return var3;
			}
		}

		protected internal virtual void func_147193_c(ICommandSender p_147193_1_, string[] p_147193_2_, int p_147193_3_)
		{
			string var4 = p_147193_2_[p_147193_3_++];
			string var5 = p_147193_2_[p_147193_3_++];
			Scoreboard var6 = this.func_147192_d();
			IScoreObjectiveCriteria var7 = (IScoreObjectiveCriteria)IScoreObjectiveCriteria.field_96643_a.get(var5);

			if(var7 == null)
			{
				throw new WrongUsageException("commands.scoreboard.objectives.add.wrongType", new object[] {var5});
			}
			else if(var6.getObjective(var4) != null)
			{
				throw new CommandException("commands.scoreboard.objectives.add.alreadyExists", new object[] {var4});
			}
			else if(var4.Length > 16)
			{
				throw new SyntaxErrorException("commands.scoreboard.objectives.add.tooLong", new object[] {var4, Convert.ToInt32(16)});
			}
			else if(var4.Length == 0)
			{
				throw new WrongUsageException("commands.scoreboard.objectives.add.usage", new object[0]);
			}
			else
			{
				if(p_147193_2_.Length > p_147193_3_)
				{
					string var8 = func_147178_a(p_147193_1_, p_147193_2_, p_147193_3_).UnformattedText;

					if(var8.Length > 32)
					{
						throw new SyntaxErrorException("commands.scoreboard.objectives.add.displayTooLong", new object[] {var8, Convert.ToInt32(32)});
					}

					if(var8.Length > 0)
					{
						var6.addScoreObjective(var4, var7).DisplayName = var8;
					}
					else
					{
						var6.addScoreObjective(var4, var7);
					}
				}
				else
				{
					var6.addScoreObjective(var4, var7);
				}

				func_152373_a(p_147193_1_, this, "commands.scoreboard.objectives.add.success", new object[] {var4});
			}
		}

		protected internal virtual void func_147185_d(ICommandSender p_147185_1_, string[] p_147185_2_, int p_147185_3_)
		{
			string var4 = p_147185_2_[p_147185_3_++];
			Scoreboard var5 = this.func_147192_d();

			if(var5.getTeam(var4) != null)
			{
				throw new CommandException("commands.scoreboard.teams.add.alreadyExists", new object[] {var4});
			}
			else if(var4.Length > 16)
			{
				throw new SyntaxErrorException("commands.scoreboard.teams.add.tooLong", new object[] {var4, Convert.ToInt32(16)});
			}
			else if(var4.Length == 0)
			{
				throw new WrongUsageException("commands.scoreboard.teams.add.usage", new object[0]);
			}
			else
			{
				if(p_147185_2_.Length > p_147185_3_)
				{
					string var6 = func_147178_a(p_147185_1_, p_147185_2_, p_147185_3_).UnformattedText;

					if(var6.Length > 32)
					{
						throw new SyntaxErrorException("commands.scoreboard.teams.add.displayTooLong", new object[] {var6, Convert.ToInt32(32)});
					}

					if(var6.Length > 0)
					{
						var5.createTeam(var4).TeamName = var6;
					}
					else
					{
						var5.createTeam(var4);
					}
				}
				else
				{
					var5.createTeam(var4);
				}

				func_152373_a(p_147185_1_, this, "commands.scoreboard.teams.add.success", new object[] {var4});
			}
		}

		protected internal virtual void func_147200_e(ICommandSender p_147200_1_, string[] p_147200_2_, int p_147200_3_)
		{
			ScorePlayerTeam var4 = this.func_147183_a(p_147200_2_[p_147200_3_++]);

			if(var4 != null)
			{
				string var5 = p_147200_2_[p_147200_3_++].ToLower();

				if(!var5.equalsIgnoreCase("color") && !var5.equalsIgnoreCase("friendlyfire") && !var5.equalsIgnoreCase("seeFriendlyInvisibles"))
				{
					throw new WrongUsageException("commands.scoreboard.teams.option.usage", new object[0]);
				}
				else if(p_147200_2_.Length == 4)
				{
					if(var5.equalsIgnoreCase("color"))
					{
						throw new WrongUsageException("commands.scoreboard.teams.option.noValue", new object[] {var5, joinNiceStringFromCollection(EnumChatFormatting.getValidValues(true, false))});
					}
					else if(!var5.equalsIgnoreCase("friendlyfire") && !var5.equalsIgnoreCase("seeFriendlyInvisibles"))
					{
						throw new WrongUsageException("commands.scoreboard.teams.option.usage", new object[0]);
					}
					else
					{
						throw new WrongUsageException("commands.scoreboard.teams.option.noValue", new object[] {var5, joinNiceStringFromCollection(new string[]{"true", "false"})});
					}
				}
				else
				{
					string var6 = p_147200_2_[p_147200_3_++];

					if(var5.equalsIgnoreCase("color"))
					{
						EnumChatFormatting var7 = EnumChatFormatting.getValueByName(var6);

						if(var7 == null || var7.FancyStyling)
						{
							throw new WrongUsageException("commands.scoreboard.teams.option.noValue", new object[] {var5, joinNiceStringFromCollection(EnumChatFormatting.getValidValues(true, false))});
						}

						var4.NamePrefix = var7.ToString();
						var4.NameSuffix = EnumChatFormatting.RESET.ToString();
					}
					else if(var5.equalsIgnoreCase("friendlyfire"))
					{
						if(!var6.equalsIgnoreCase("true") && !var6.equalsIgnoreCase("false"))
						{
							throw new WrongUsageException("commands.scoreboard.teams.option.noValue", new object[] {var5, joinNiceStringFromCollection(new string[]{"true", "false"})});
						}

						var4.AllowFriendlyFire = var6.equalsIgnoreCase("true");
					}
					else if(var5.equalsIgnoreCase("seeFriendlyInvisibles"))
					{
						if(!var6.equalsIgnoreCase("true") && !var6.equalsIgnoreCase("false"))
						{
							throw new WrongUsageException("commands.scoreboard.teams.option.noValue", new object[] {var5, joinNiceStringFromCollection(new string[]{"true", "false"})});
						}

						var4.SeeFriendlyInvisiblesEnabled = var6.equalsIgnoreCase("true");
					}

					func_152373_a(p_147200_1_, this, "commands.scoreboard.teams.option.success", new object[] {var5, var4.RegisteredName, var6});
				}
			}
		}

		protected internal virtual void func_147194_f(ICommandSender p_147194_1_, string[] p_147194_2_, int p_147194_3_)
		{
			Scoreboard var4 = this.func_147192_d();
			ScorePlayerTeam var5 = this.func_147183_a(p_147194_2_[p_147194_3_++]);

			if(var5 != null)
			{
				var4.removeTeam(var5);
				func_152373_a(p_147194_1_, this, "commands.scoreboard.teams.remove.success", new object[] {var5.RegisteredName});
			}
		}

		protected internal virtual void func_147186_g(ICommandSender p_147186_1_, string[] p_147186_2_, int p_147186_3_)
		{
			Scoreboard var4 = this.func_147192_d();

			if(p_147186_2_.Length > p_147186_3_)
			{
				ScorePlayerTeam var5 = this.func_147183_a(p_147186_2_[p_147186_3_++]);

				if(var5 == null)
				{
					return;
				}

				ICollection var6 = var5.MembershipCollection;

				if(var6.size() <= 0)
				{
					throw new CommandException("commands.scoreboard.teams.list.player.empty", new object[] {var5.RegisteredName});
				}

				ChatComponentTranslation var7 = new ChatComponentTranslation("commands.scoreboard.teams.list.player.count", new object[] {Convert.ToInt32(var6.size()), var5.RegisteredName});
				var7.ChatStyle.Color = EnumChatFormatting.DARK_GREEN;
				p_147186_1_.addChatMessage(var7);
				p_147186_1_.addChatMessage(new ChatComponentText(joinNiceString(var6.ToArray())));
			}
			else
			{
				ICollection var9 = var4.Teams;

				if(var9.size() <= 0)
				{
					throw new CommandException("commands.scoreboard.teams.list.empty", new object[0]);
				}

				ChatComponentTranslation var10 = new ChatComponentTranslation("commands.scoreboard.teams.list.count", new object[] {Convert.ToInt32(var9.size())});
				var10.ChatStyle.Color = EnumChatFormatting.DARK_GREEN;
				p_147186_1_.addChatMessage(var10);
				IEnumerator var11 = var9.GetEnumerator();

				while (var11.MoveNext())
				{
					ScorePlayerTeam var8 = (ScorePlayerTeam)var11.Current;
					p_147186_1_.addChatMessage(new ChatComponentTranslation("commands.scoreboard.teams.list.entry", new object[] {var8.RegisteredName, var8.func_96669_c(), Convert.ToInt32(var8.MembershipCollection.size())}));
				}
			}
		}

		protected internal virtual void func_147190_h(ICommandSender p_147190_1_, string[] p_147190_2_, int p_147190_3_)
		{
			Scoreboard var4 = this.func_147192_d();
			string var5 = p_147190_2_[p_147190_3_++];
			HashSet var6 = new HashSet();
			HashSet var7 = new HashSet();
			string var8;

			if(p_147190_1_ is EntityPlayer && p_147190_3_ == p_147190_2_.Length)
			{
				var8 = getCommandSenderAsPlayer(p_147190_1_).CommandSenderName;

				if(var4.func_151392_a(var8, var5))
				{
					var6.Add(var8);
				}
				else
				{
					var7.Add(var8);
				}
			}
			else
			{
				while (p_147190_3_ < p_147190_2_.Length)
				{
					var8 = func_96332_d(p_147190_1_, p_147190_2_[p_147190_3_++]);

					if(var4.func_151392_a(var8, var5))
					{
						var6.Add(var8);
					}
					else
					{
						var7.Add(var8);
					}
				}
			}

			if(!var6.Count == 0)
			{
				func_152373_a(p_147190_1_, this, "commands.scoreboard.teams.join.success", new object[] {Convert.ToInt32(var6.Count), var5, joinNiceString(var6.ToArray(new string[0]))});
			}

			if(!var7.Count == 0)
			{
				throw new CommandException("commands.scoreboard.teams.join.failure", new object[] {Convert.ToInt32(var7.Count), var5, joinNiceString(var7.ToArray(new string[0]))});
			}
		}

		protected internal virtual void func_147199_i(ICommandSender p_147199_1_, string[] p_147199_2_, int p_147199_3_)
		{
			Scoreboard var4 = this.func_147192_d();
			HashSet var5 = new HashSet();
			HashSet var6 = new HashSet();
			string var7;

			if(p_147199_1_ is EntityPlayer && p_147199_3_ == p_147199_2_.Length)
			{
				var7 = getCommandSenderAsPlayer(p_147199_1_).CommandSenderName;

				if(var4.func_96524_g(var7))
				{
					var5.Add(var7);
				}
				else
				{
					var6.Add(var7);
				}
			}
			else
			{
				while (p_147199_3_ < p_147199_2_.Length)
				{
					var7 = func_96332_d(p_147199_1_, p_147199_2_[p_147199_3_++]);

					if(var4.func_96524_g(var7))
					{
						var5.Add(var7);
					}
					else
					{
						var6.Add(var7);
					}
				}
			}

			if(!var5.Count == 0)
			{
				func_152373_a(p_147199_1_, this, "commands.scoreboard.teams.leave.success", new object[] {Convert.ToInt32(var5.Count), joinNiceString(var5.ToArray(new string[0]))});
			}

			if(!var6.Count == 0)
			{
				throw new CommandException("commands.scoreboard.teams.leave.failure", new object[] {Convert.ToInt32(var6.Count), joinNiceString(var6.ToArray(new string[0]))});
			}
		}

		protected internal virtual void func_147188_j(ICommandSender p_147188_1_, string[] p_147188_2_, int p_147188_3_)
		{
			Scoreboard var4 = this.func_147192_d();
			ScorePlayerTeam var5 = this.func_147183_a(p_147188_2_[p_147188_3_++]);

			if(var5 != null)
			{
				ArrayList var6 = new ArrayList(var5.MembershipCollection);

				if(var6.Count == 0)
				{
					throw new CommandException("commands.scoreboard.teams.empty.alreadyEmpty", new object[] {var5.RegisteredName});
				}
				else
				{
					IEnumerator var7 = var6.GetEnumerator();

					while (var7.MoveNext())
					{
						string var8 = (string)var7.Current;
						var4.removePlayerFromTeam(var8, var5);
					}

					func_152373_a(p_147188_1_, this, "commands.scoreboard.teams.empty.success", new object[] {Convert.ToInt32(var6.Count), var5.RegisteredName});
				}
			}
		}

		protected internal virtual void func_147191_h(ICommandSender p_147191_1_, string p_147191_2_)
		{
			Scoreboard var3 = this.func_147192_d();
			ScoreObjective var4 = this.func_147189_a(p_147191_2_, false);
			var3.func_96519_k(var4);
			func_152373_a(p_147191_1_, this, "commands.scoreboard.objectives.remove.success", new object[] {p_147191_2_});
		}

		protected internal virtual void func_147196_d(ICommandSender p_147196_1_)
		{
			Scoreboard var2 = this.func_147192_d();
			ICollection var3 = var2.ScoreObjectives;

			if(var3.size() <= 0)
			{
				throw new CommandException("commands.scoreboard.objectives.list.empty", new object[0]);
			}
			else
			{
				ChatComponentTranslation var4 = new ChatComponentTranslation("commands.scoreboard.objectives.list.count", new object[] {Convert.ToInt32(var3.size())});
				var4.ChatStyle.Color = EnumChatFormatting.DARK_GREEN;
				p_147196_1_.addChatMessage(var4);
				IEnumerator var5 = var3.GetEnumerator();

				while (var5.MoveNext())
				{
					ScoreObjective var6 = (ScoreObjective)var5.Current;
					p_147196_1_.addChatMessage(new ChatComponentTranslation("commands.scoreboard.objectives.list.entry", new object[] {var6.Name, var6.DisplayName, var6.Criteria.func_96636_a()}));
				}
			}
		}

		protected internal virtual void func_147198_k(ICommandSender p_147198_1_, string[] p_147198_2_, int p_147198_3_)
		{
			Scoreboard var4 = this.func_147192_d();
			string var5 = p_147198_2_[p_147198_3_++];
			int var6 = Scoreboard.getObjectiveDisplaySlotNumber(var5);
			ScoreObjective var7 = null;

			if(p_147198_2_.Length == 4)
			{
				var7 = this.func_147189_a(p_147198_2_[p_147198_3_++], false);
			}

			if(var6 < 0)
			{
				throw new CommandException("commands.scoreboard.objectives.setdisplay.invalidSlot", new object[] {var5});
			}
			else
			{
				var4.func_96530_a(var6, var7);

				if(var7 != null)
				{
					func_152373_a(p_147198_1_, this, "commands.scoreboard.objectives.setdisplay.successSet", new object[] {Scoreboard.getObjectiveDisplaySlot(var6), var7.Name});
				}
				else
				{
					func_152373_a(p_147198_1_, this, "commands.scoreboard.objectives.setdisplay.successCleared", new object[] {Scoreboard.getObjectiveDisplaySlot(var6)});
				}
			}
		}

		protected internal virtual void func_147195_l(ICommandSender p_147195_1_, string[] p_147195_2_, int p_147195_3_)
		{
			Scoreboard var4 = this.func_147192_d();

			if(p_147195_2_.Length > p_147195_3_)
			{
				string var5 = func_96332_d(p_147195_1_, p_147195_2_[p_147195_3_++]);
				IDictionary var6 = var4.func_96510_d(var5);

				if(var6.Count <= 0)
				{
					throw new CommandException("commands.scoreboard.players.list.player.empty", new object[] {var5});
				}

				ChatComponentTranslation var7 = new ChatComponentTranslation("commands.scoreboard.players.list.player.count", new object[] {Convert.ToInt32(var6.Count), var5});
				var7.ChatStyle.Color = EnumChatFormatting.DARK_GREEN;
				p_147195_1_.addChatMessage(var7);
				IEnumerator var8 = var6.Values.GetEnumerator();

				while (var8.MoveNext())
				{
					Score var9 = (Score)var8.Current;
					p_147195_1_.addChatMessage(new ChatComponentTranslation("commands.scoreboard.players.list.player.entry", new object[] {Convert.ToInt32(var9.ScorePoints), var9.func_96645_d().DisplayName, var9.func_96645_d().Name}));
				}
			}
			else
			{
				ICollection var10 = var4.ObjectiveNames;

				if(var10.size() <= 0)
				{
					throw new CommandException("commands.scoreboard.players.list.empty", new object[0]);
				}

				ChatComponentTranslation var11 = new ChatComponentTranslation("commands.scoreboard.players.list.count", new object[] {Convert.ToInt32(var10.size())});
				var11.ChatStyle.Color = EnumChatFormatting.DARK_GREEN;
				p_147195_1_.addChatMessage(var11);
				p_147195_1_.addChatMessage(new ChatComponentText(joinNiceString(var10.ToArray())));
			}
		}

		protected internal virtual void func_147197_m(ICommandSender p_147197_1_, string[] p_147197_2_, int p_147197_3_)
		{
			string var4 = p_147197_2_[p_147197_3_ - 1];
			string var5 = func_96332_d(p_147197_1_, p_147197_2_[p_147197_3_++]);
			ScoreObjective var6 = this.func_147189_a(p_147197_2_[p_147197_3_++], true);
			int var7 = var4.equalsIgnoreCase("set") ? parseInt(p_147197_1_, p_147197_2_[p_147197_3_++]) : parseIntWithMin(p_147197_1_, p_147197_2_[p_147197_3_++], 1);
			Scoreboard var8 = this.func_147192_d();
			Score var9 = var8.func_96529_a(var5, var6);

			if(var4.equalsIgnoreCase("set"))
			{
				var9.func_96647_c(var7);
			}
			else if(var4.equalsIgnoreCase("add"))
			{
				var9.func_96649_a(var7);
			}
			else
			{
				var9.func_96646_b(var7);
			}

			func_152373_a(p_147197_1_, this, "commands.scoreboard.players.set.success", new object[] {var6.Name, var5, Convert.ToInt32(var9.ScorePoints)});
		}

		protected internal virtual void func_147187_n(ICommandSender p_147187_1_, string[] p_147187_2_, int p_147187_3_)
		{
			Scoreboard var4 = this.func_147192_d();
			string var5 = func_96332_d(p_147187_1_, p_147187_2_[p_147187_3_++]);
			var4.func_96515_c(var5);
			func_152373_a(p_147187_1_, this, "commands.scoreboard.players.reset.success", new object[] {var5});
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			if(p_71516_2_.Length == 1)
			{
				return getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"objectives", "players", "teams"});
			}
			else
			{
				if(p_71516_2_[0].equalsIgnoreCase("objectives"))
				{
					if(p_71516_2_.Length == 2)
					{
						return getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"list", "add", "remove", "setdisplay"});
					}

					if(p_71516_2_[1].equalsIgnoreCase("add"))
					{
						if(p_71516_2_.Length == 4)
						{
							Set var3 = IScoreObjectiveCriteria.field_96643_a.Keys;
							return getListOfStringsFromIterableMatchingLastWord(p_71516_2_, var3);
						}
					}
					else if(p_71516_2_[1].equalsIgnoreCase("remove"))
					{
						if(p_71516_2_.Length == 3)
						{
							return getListOfStringsFromIterableMatchingLastWord(p_71516_2_, this.func_147184_a(false));
						}
					}
					else if(p_71516_2_[1].equalsIgnoreCase("setdisplay"))
					{
						if(p_71516_2_.Length == 3)
						{
							return getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"list", "sidebar", "belowName"});
						}

						if(p_71516_2_.Length == 4)
						{
							return getListOfStringsFromIterableMatchingLastWord(p_71516_2_, this.func_147184_a(false));
						}
					}
				}
				else if(p_71516_2_[0].equalsIgnoreCase("players"))
				{
					if(p_71516_2_.Length == 2)
					{
						return getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"set", "add", "remove", "reset", "list"});
					}

					if(!p_71516_2_[1].equalsIgnoreCase("set") && !p_71516_2_[1].equalsIgnoreCase("add") && !p_71516_2_[1].equalsIgnoreCase("remove"))
					{
						if((p_71516_2_[1].equalsIgnoreCase("reset") || p_71516_2_[1].equalsIgnoreCase("list")) && p_71516_2_.Length == 3)
						{
							return getListOfStringsFromIterableMatchingLastWord(p_71516_2_, this.func_147192_d().ObjectiveNames);
						}
					}
					else
					{
						if(p_71516_2_.Length == 3)
						{
							return getListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.AllUsernames);
						}

						if(p_71516_2_.Length == 4)
						{
							return getListOfStringsFromIterableMatchingLastWord(p_71516_2_, this.func_147184_a(true));
						}
					}
				}
				else if(p_71516_2_[0].equalsIgnoreCase("teams"))
				{
					if(p_71516_2_.Length == 2)
					{
						return getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"add", "remove", "join", "leave", "empty", "list", "option"});
					}

					if(p_71516_2_[1].equalsIgnoreCase("join"))
					{
						if(p_71516_2_.Length == 3)
						{
							return getListOfStringsFromIterableMatchingLastWord(p_71516_2_, this.func_147192_d().TeamNames);
						}

						if(p_71516_2_.Length >= 4)
						{
							return getListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.AllUsernames);
						}
					}
					else
					{
						if(p_71516_2_[1].equalsIgnoreCase("leave"))
						{
							return getListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.AllUsernames);
						}

						if(!p_71516_2_[1].equalsIgnoreCase("empty") && !p_71516_2_[1].equalsIgnoreCase("list") && !p_71516_2_[1].equalsIgnoreCase("remove"))
						{
							if(p_71516_2_[1].equalsIgnoreCase("option"))
							{
								if(p_71516_2_.Length == 3)
								{
									return getListOfStringsFromIterableMatchingLastWord(p_71516_2_, this.func_147192_d().TeamNames);
								}

								if(p_71516_2_.Length == 4)
								{
									return getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"color", "friendlyfire", "seeFriendlyInvisibles"});
								}

								if(p_71516_2_.Length == 5)
								{
									if(p_71516_2_[3].equalsIgnoreCase("color"))
									{
										return getListOfStringsFromIterableMatchingLastWord(p_71516_2_, EnumChatFormatting.getValidValues(true, false));
									}

									if(p_71516_2_[3].equalsIgnoreCase("friendlyfire") || p_71516_2_[3].equalsIgnoreCase("seeFriendlyInvisibles"))
									{
										return getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"true", "false"});
									}
								}
							}
						}
						else if(p_71516_2_.Length == 3)
						{
							return getListOfStringsFromIterableMatchingLastWord(p_71516_2_, this.func_147192_d().TeamNames);
						}
					}
				}

				return null;
			}
		}

		protected internal virtual IList func_147184_a(bool p_147184_1_)
		{
			ICollection var2 = this.func_147192_d().ScoreObjectives;
			ArrayList var3 = new ArrayList();
			IEnumerator var4 = var2.GetEnumerator();

			while (var4.MoveNext())
			{
				ScoreObjective var5 = (ScoreObjective)var4.Current;

				if(!p_147184_1_ || !var5.Criteria.ReadOnly)
				{
					var3.Add(var5.Name);
				}
			}

			return var3;
		}

///    
///     <summary> * Return whether the specified command parameter index is a username parameter. </summary>
///     
		public override bool isUsernameIndex(string[] p_82358_1_, int p_82358_2_)
		{
			return p_82358_1_[0].equalsIgnoreCase("players") ? p_82358_2_ == 2 : (!p_82358_1_[0].equalsIgnoreCase("teams") ? false : p_82358_2_ == 2 || p_82358_2_ == 3);
		}
	}

}