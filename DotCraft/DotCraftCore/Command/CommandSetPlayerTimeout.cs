using System;

namespace DotCraftCore.Command
{

	using MinecraftServer = DotCraftCore.Server.MinecraftServer;

	public class CommandSetPlayerTimeout : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "setidletimeout";
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
			return "commands.setidletimeout.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length == 1)
			{
				int var3 = parseIntWithMin(p_71515_1_, p_71515_2_[0], 0);
				MinecraftServer.Server.func_143006_e(var3);
				func_152373_a(p_71515_1_, this, "commands.setidletimeout.success", new object[] {Convert.ToInt32(var3)});
			}
			else
			{
				throw new WrongUsageException("commands.setidletimeout.usage", new object[0]);
			}
		}
	}

}