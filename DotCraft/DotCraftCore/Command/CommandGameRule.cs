using System.Collections;

namespace DotCraftCore.Command
{

	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using ChatComponentText = DotCraftCore.Util.ChatComponentText;
	using GameRules = DotCraftCore.World.GameRules;

	public class CommandGameRule : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "gamerule";
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
			return "commands.gamerule.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			string var6;

			if(p_71515_2_.Length == 2)
			{
				var6 = p_71515_2_[0];
				string var7 = p_71515_2_[1];
				GameRules var8 = this.GameRules;

				if(var8.hasRule(var6))
				{
					var8.setOrCreateGameRule(var6, var7);
					func_152373_a(p_71515_1_, this, "commands.gamerule.success", new object[0]);
				}
				else
				{
					func_152373_a(p_71515_1_, this, "commands.gamerule.norule", new object[] {var6});
				}
			}
			else if(p_71515_2_.Length == 1)
			{
				var6 = p_71515_2_[0];
				GameRules var4 = this.GameRules;

				if(var4.hasRule(var6))
				{
					string var5 = var4.getGameRuleStringValue(var6);
					p_71515_1_.addChatMessage((new ChatComponentText(var6)).appendText(" = ").appendText(var5));
				}
				else
				{
					func_152373_a(p_71515_1_, this, "commands.gamerule.norule", new object[] {var6});
				}
			}
			else if(p_71515_2_.Length == 0)
			{
				GameRules var3 = this.GameRules;
				p_71515_1_.addChatMessage(new ChatComponentText(joinNiceString(var3.Rules)));
			}
			else
			{
				throw new WrongUsageException("commands.gamerule.usage", new object[0]);
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? getListOfStringsMatchingLastWord(p_71516_2_, this.GameRules.Rules) : (p_71516_2_.Length == 2 ? getListOfStringsMatchingLastWord(p_71516_2_, new string[] {"true", "false"}): null);
		}

///    
///     <summary> * Return the game rule set this command should be able to manipulate. </summary>
///     
		private GameRules GameRules
		{
			get
			{
				return MinecraftServer.Server.worldServerForDimension(0).GameRules;
			}
		}
	}

}