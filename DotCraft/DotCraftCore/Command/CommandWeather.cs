using System;
using System.Collections;

namespace DotCraftCore.Command
{

	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using WorldServer = DotCraftCore.World.WorldServer;
	using WorldInfo = DotCraftCore.World.Storage.WorldInfo;

	public class CommandWeather : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "weather";
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
			return "commands.weather.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length >= 1 && p_71515_2_.Length <= 2)
			{
				int var3 = (300 + (new Random()).Next(600)) * 20;

				if(p_71515_2_.Length >= 2)
				{
					var3 = parseIntBounded(p_71515_1_, p_71515_2_[1], 1, 1000000) * 20;
				}

				WorldServer var4 = MinecraftServer.Server.worldServers[0];
				WorldInfo var5 = var4.WorldInfo;

				if("clear".equalsIgnoreCase(p_71515_2_[0]))
				{
					var5.RainTime = 0;
					var5.ThunderTime = 0;
					var5.Raining = false;
					var5.Thundering = false;
					func_152373_a(p_71515_1_, this, "commands.weather.clear", new object[0]);
				}
				else if("rain".equalsIgnoreCase(p_71515_2_[0]))
				{
					var5.RainTime = var3;
					var5.Raining = true;
					var5.Thundering = false;
					func_152373_a(p_71515_1_, this, "commands.weather.rain", new object[0]);
				}
				else
				{
					if(!"thunder".equalsIgnoreCase(p_71515_2_[0]))
					{
						throw new WrongUsageException("commands.weather.usage", new object[0]);
					}

					var5.RainTime = var3;
					var5.ThunderTime = var3;
					var5.Raining = true;
					var5.Thundering = true;
					func_152373_a(p_71515_1_, this, "commands.weather.thunder", new object[0]);
				}
			}
			else
			{
				throw new WrongUsageException("commands.weather.usage", new object[0]);
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"clear", "rain", "thunder"}): null;
		}
	}

}