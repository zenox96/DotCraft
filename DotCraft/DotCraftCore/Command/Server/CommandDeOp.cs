using System.Collections;

namespace DotCraftCore.Command.Server
{

	using GameProfile = com.mojang.authlib.GameProfile;
	using CommandBase = DotCraftCore.Command.CommandBase;
	using CommandException = DotCraftCore.Command.CommandException;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using WrongUsageException = DotCraftCore.Command.WrongUsageException;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;

	public class CommandDeOp : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "deop";
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
			return "commands.deop.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length == 1 && p_71515_2_[0].Length > 0)
			{
				MinecraftServer var3 = MinecraftServer.Server;
				GameProfile var4 = var3.ConfigurationManager.func_152603_m().func_152700_a(p_71515_2_[0]);

				if(var4 == null)
				{
					throw new CommandException("commands.deop.failed", new object[] {p_71515_2_[0]});
				}
				else
				{
					var3.ConfigurationManager.func_152610_b(var4);
					func_152373_a(p_71515_1_, this, "commands.deop.success", new object[] {p_71515_2_[0]});
				}
			}
			else
			{
				throw new WrongUsageException("commands.deop.usage", new object[0]);
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? getListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.ConfigurationManager.func_152606_n()) : null;
		}
	}

}