using System.Collections;

namespace DotCraftCore.Command.Server
{

	using GameProfile = com.mojang.authlib.GameProfile;
	using CommandBase = DotCraftCore.Command.CommandBase;
	using CommandException = DotCraftCore.Command.CommandException;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using WrongUsageException = DotCraftCore.Command.WrongUsageException;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;

	public class CommandOp : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "op";
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
			return "commands.op.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length == 1 && p_71515_2_[0].Length > 0)
			{
				MinecraftServer var3 = MinecraftServer.Server;
				GameProfile var4 = var3.func_152358_ax().func_152655_a(p_71515_2_[0]);

				if(var4 == null)
				{
					throw new CommandException("commands.op.failed", new object[] {p_71515_2_[0]});
				}
				else
				{
					var3.ConfigurationManager.func_152605_a(var4);
					func_152373_a(p_71515_1_, this, "commands.op.success", new object[] {p_71515_2_[0]});
				}
			}
			else
			{
				throw new WrongUsageException("commands.op.usage", new object[0]);
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			if(p_71516_2_.Length == 1)
			{
				string var3 = p_71516_2_[p_71516_2_.Length - 1];
				ArrayList var4 = new ArrayList();
				GameProfile[] var5 = MinecraftServer.Server.func_152357_F();
				int var6 = var5.Length;

				for (int var7 = 0; var7 < var6; ++var7)
				{
					GameProfile var8 = var5[var7];

					if(!MinecraftServer.Server.ConfigurationManager.func_152596_g(var8) && doesStringStartWith(var3, var8.Name))
					{
						var4.Add(var8.Name);
					}
				}

				return var4;
			}
			else
			{
				return null;
			}
		}
	}

}