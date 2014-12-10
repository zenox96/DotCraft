using System.Collections;

namespace DotCraftCore.Command
{

	using EntityPlayerMP = DotCraftCore.Entity.Player.EntityPlayerMP;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;

	public class CommandServerKick : CommandBase
	{
		private const string __OBFID = "CL_00000550";

		public virtual string CommandName
		{
			get
			{
				return "kick";
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
			return "commands.kick.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length > 0 && p_71515_2_[0].Length > 1)
			{
				EntityPlayerMP var3 = MinecraftServer.Server.ConfigurationManager.func_152612_a(p_71515_2_[0]);
				string var4 = "Kicked by an operator.";
				bool var5 = false;

				if(var3 == null)
				{
					throw new PlayerNotFoundException();
				}
				else
				{
					if(p_71515_2_.Length >= 2)
					{
						var4 = func_147178_a(p_71515_1_, p_71515_2_, 1).UnformattedText;
						var5 = true;
					}

					var3.playerNetServerHandler.kickPlayerFromServer(var4);

					if(var5)
					{
						func_152373_a(p_71515_1_, this, "commands.kick.success.reason", new object[] {var3.CommandSenderName, var4});
					}
					else
					{
						func_152373_a(p_71515_1_, this, "commands.kick.success", new object[] {var3.CommandSenderName});
					}
				}
			}
			else
			{
				throw new WrongUsageException("commands.kick.usage", new object[0]);
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