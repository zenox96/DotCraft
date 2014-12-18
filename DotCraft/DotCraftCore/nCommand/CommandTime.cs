using System;
using System.Collections;

namespace DotCraftCore.nCommand
{

	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using WorldServer = DotCraftCore.nWorld.WorldServer;

	public class CommandTime : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "time";
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
			return "commands.time.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length > 1)
			{
				int var3;

				if(p_71515_2_[0].Equals("set"))
				{
					if(p_71515_2_[1].Equals("day"))
					{
						var3 = 1000;
					}
					else if(p_71515_2_[1].Equals("night"))
					{
						var3 = 13000;
					}
					else
					{
						var3 = parseIntWithMin(p_71515_1_, p_71515_2_[1], 0);
					}

					this.setTime(p_71515_1_, var3);
					func_152373_a(p_71515_1_, this, "commands.time.set", new object[] {Convert.ToInt32(var3)});
					return;
				}

				if(p_71515_2_[0].Equals("add"))
				{
					var3 = parseIntWithMin(p_71515_1_, p_71515_2_[1], 0);
					this.addTime(p_71515_1_, var3);
					func_152373_a(p_71515_1_, this, "commands.time.added", new object[] {Convert.ToInt32(var3)});
					return;
				}
			}

			throw new WrongUsageException("commands.time.usage", new object[0]);
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"set", "add"}): (p_71516_2_.Length == 2 && p_71516_2_[0].Equals("set") ? getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"day", "night"}): null);
		}

///    
///     <summary> * Set the time in the server object. </summary>
///     
		protected internal virtual void setTime(ICommandSender p_71552_1_, int p_71552_2_)
		{
			for (int var3 = 0; var3 < MinecraftServer.Server.worldServers.length; ++var3)
			{
				MinecraftServer.Server.worldServers[var3].WorldTime = (long)p_71552_2_;
			}
		}

///    
///     <summary> * Adds (or removes) time in the server object. </summary>
///     
		protected internal virtual void addTime(ICommandSender p_71553_1_, int p_71553_2_)
		{
			for (int var3 = 0; var3 < MinecraftServer.Server.worldServers.length; ++var3)
			{
				WorldServer var4 = MinecraftServer.Server.worldServers[var3];
				var4.WorldTime = var4.WorldTime + (long)p_71553_2_;
			}
		}
	}

}