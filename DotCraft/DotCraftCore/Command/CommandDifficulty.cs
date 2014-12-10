using System.Collections;

namespace DotCraftCore.Command
{

	using MinecraftServer = DotCraftCore.server.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.util.ChatComponentTranslation;
	using EnumDifficulty = DotCraftCore.world.EnumDifficulty;

	public class CommandDifficulty : CommandBase
	{
		private const string __OBFID = "CL_00000422";

		public virtual string CommandName
		{
			get
			{
				return "difficulty";
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
			return "commands.difficulty.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length > 0)
			{
				EnumDifficulty var3 = this.func_147201_h(p_71515_1_, p_71515_2_[0]);
				MinecraftServer.Server.func_147139_a(var3);
				func_152373_a(p_71515_1_, this, "commands.difficulty.success", new object[] {new ChatComponentTranslation(var3.DifficultyResourceKey, new object[0])});
			}
			else
			{
				throw new WrongUsageException("commands.difficulty.usage", new object[0]);
			}
		}

		protected internal virtual EnumDifficulty func_147201_h(ICommandSender p_147201_1_, string p_147201_2_)
		{
			return !p_147201_2_.equalsIgnoreCase("peaceful") && !p_147201_2_.equalsIgnoreCase("p") ? (!p_147201_2_.equalsIgnoreCase("easy") && !p_147201_2_.equalsIgnoreCase("e") ? (!p_147201_2_.equalsIgnoreCase("normal") && !p_147201_2_.equalsIgnoreCase("n") ? (!p_147201_2_.equalsIgnoreCase("hard") && !p_147201_2_.equalsIgnoreCase("h") ? EnumDifficulty.getDifficultyEnum(parseIntBounded(p_147201_1_, p_147201_2_, 0, 3)) : EnumDifficulty.HARD) : EnumDifficulty.NORMAL) : EnumDifficulty.EASY) : EnumDifficulty.PEACEFUL;
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"peaceful", "easy", "normal", "hard"}): null;
		}
	}

}