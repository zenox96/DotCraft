using System;
using System.Collections;

namespace DotCraftCore.Command
{

	using EntityPlayerMP = DotCraftCore.Entity.Player.EntityPlayerMP;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;

	public class CommandXP : CommandBase
	{
		private const string __OBFID = "CL_00000398";

		public virtual string CommandName
		{
			get
			{
				return "xp";
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
			return "commands.xp.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length <= 0)
			{
				throw new WrongUsageException("commands.xp.usage", new object[0]);
			}
			else
			{
				string var4 = p_71515_2_[0];
				bool var5 = var4.EndsWith("l") || var4.EndsWith("L");

				if(var5 && var4.Length > 1)
				{
					var4 = var4.Substring(0, var4.Length - 1);
				}

				int var6 = parseInt(p_71515_1_, var4);
				bool var7 = var6 < 0;

				if(var7)
				{
					var6 *= -1;
				}

				EntityPlayerMP var3;

				if(p_71515_2_.Length > 1)
				{
					var3 = getPlayer(p_71515_1_, p_71515_2_[1]);
				}
				else
				{
					var3 = getCommandSenderAsPlayer(p_71515_1_);
				}

				if(var5)
				{
					if(var7)
					{
						var3.addExperienceLevel(-var6);
						func_152373_a(p_71515_1_, this, "commands.xp.success.negative.levels", new object[] {Convert.ToInt32(var6), var3.CommandSenderName});
					}
					else
					{
						var3.addExperienceLevel(var6);
						func_152373_a(p_71515_1_, this, "commands.xp.success.levels", new object[] {Convert.ToInt32(var6), var3.CommandSenderName});
					}
				}
				else
				{
					if(var7)
					{
						throw new WrongUsageException("commands.xp.failure.widthdrawXp", new object[0]);
					}

					var3.addExperience(var6);
					func_152373_a(p_71515_1_, this, "commands.xp.success", new object[] {Convert.ToInt32(var6), var3.CommandSenderName});
				}
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 2 ? getListOfStringsMatchingLastWord(p_71516_2_, this.AllUsernames) : null;
		}

		protected internal virtual string[] AllUsernames
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