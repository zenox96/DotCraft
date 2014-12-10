using System;
using System.Collections;
using System.Text;

namespace DotCraftCore.Command
{

	using Profiler = DotCraftCore.profiler.Profiler;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class CommandDebug : CommandBase
	{
		private static readonly Logger logger = LogManager.Logger;
		private long field_147206_b;
		private int field_147207_c;
		private const string __OBFID = "CL_00000270";

		public virtual string CommandName
		{
			get
			{
				return "debug";
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
			return "commands.debug.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length == 1)
			{
				if(p_71515_2_[0].Equals("start"))
				{
					func_152373_a(p_71515_1_, this, "commands.debug.start", new object[0]);
					MinecraftServer.Server.enableProfiling();
					this.field_147206_b = MinecraftServer.SystemTimeMillis;
					this.field_147207_c = MinecraftServer.Server.TickCounter;
					return;
				}

				if(p_71515_2_[0].Equals("stop"))
				{
					if(!MinecraftServer.Server.theProfiler.profilingEnabled)
					{
						throw new CommandException("commands.debug.notStarted", new object[0]);
					}

					long var3 = MinecraftServer.SystemTimeMillis;
					int var5 = MinecraftServer.Server.TickCounter;
					long var6 = var3 - this.field_147206_b;
					int var8 = var5 - this.field_147207_c;
					this.func_147205_a(var6, var8);
					MinecraftServer.Server.theProfiler.profilingEnabled = false;
					func_152373_a(p_71515_1_, this, "commands.debug.stop", new object[] {Convert.ToSingle((float)var6 / 1000.0F), Convert.ToInt32(var8)});
					return;
				}
			}

			throw new WrongUsageException("commands.debug.usage", new object[0]);
		}

		private void func_147205_a(long p_147205_1_, int p_147205_3_)
		{
			File var4 = new File(MinecraftServer.Server.getFile("debug"), "profile-results-" + (new SimpleDateFormat("yyyy-MM-dd_HH.mm.ss")).format(DateTime.Now) + ".txt");
			var4.ParentFile.mkdirs();

			try
			{
				FileWriter var5 = new FileWriter(var4);
				var5.write(this.func_147204_b(p_147205_1_, p_147205_3_));
				var5.close();
			}
			catch (Exception var6)
			{
				logger.error("Could not save profiler results to " + var4, var6);
			}
		}

		private string func_147204_b(long p_147204_1_, int p_147204_3_)
		{
			StringBuilder var4 = new StringBuilder();
			var4.Append("---- Minecraft Profiler Results ----\n");
			var4.Append("// ");
			var4.Append(func_147203_d());
			var4.Append("\n\n");
			var4.Append("Time span: ").append(p_147204_1_).append(" ms\n");
			var4.Append("Tick span: ").append(p_147204_3_).append(" ticks\n");
			var4.Append("// This is approximately ").append(string.Format("{0:F2}", new object[] {Convert.ToSingle((float)p_147204_3_ / ((float)p_147204_1_ / 1000.0F))})).append(" ticks per second. It should be ").append(20).append(" ticks per second\n\n");
			var4.Append("--- BEGIN PROFILE DUMP ---\n\n");
			this.func_147202_a(0, "root", var4);
			var4.Append("--- END PROFILE DUMP ---\n\n");
			return var4.ToString();
		}

		private void func_147202_a(int p_147202_1_, string p_147202_2_, StringBuilder p_147202_3_)
		{
			IList var4 = MinecraftServer.Server.theProfiler.getProfilingData(p_147202_2_);

			if(var4 != null && var4.Count >= 3)
			{
				for (int var5 = 1; var5 < var4.Count; ++var5)
				{
					Profiler.Result var6 = (Profiler.Result)var4[var5];
					p_147202_3_.Append(string.Format("[{0:D2}] ", new object[] {Convert.ToInt32(p_147202_1_)}));

					for (int var7 = 0; var7 < p_147202_1_; ++var7)
					{
						p_147202_3_.Append(" ");
					}

					p_147202_3_.Append(var6.field_76331_c);
					p_147202_3_.Append(" - ");
					p_147202_3_.Append(string.Format("{0:F2}", new object[] {Convert.ToDouble(var6.field_76332_a)}));
					p_147202_3_.Append("%/");
					p_147202_3_.Append(string.Format("{0:F2}", new object[] {Convert.ToDouble(var6.field_76330_b)}));
					p_147202_3_.Append("%\n");

					if(!var6.field_76331_c.Equals("unspecified"))
					{
						try
						{
							this.func_147202_a(p_147202_1_ + 1, p_147202_2_ + "." + var6.field_76331_c, p_147202_3_);
						}
						catch (Exception var8)
						{
							p_147202_3_.Append("[[ EXCEPTION " + var8 + " ]]");
						}
					}
				}
			}
		}

		private static string func_147203_d()
		{
			string[] var0 = new string[] {"Shiny numbers!", "Am I not running fast enough? :(", "I\'m working as hard as I can!", "Will I ever be good enough for you? :(", "Speedy. Zoooooom!", "Hello world", "40% better than a crash report.", "Now with extra numbers", "Now with less numbers", "Now with the same numbers", "You should add flames to things, it makes them go faster!", "Do you feel the need for... optimization?", "*cracks redstone whip*", "Maybe if you treated it better then it\'ll have more motivation to work faster! Poor server."};

			try
			{
				return var0[(int)(System.nanoTime() % (long)var0.Length)];
			}
			catch (Exception var2)
			{
				return "Witty comment unavailable :(";
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"start", "stop"}): null;
		}
	}

}