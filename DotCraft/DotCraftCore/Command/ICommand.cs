using System.Collections;

namespace DotCraftCore.Command
{


	public interface ICommand : Comparable
	{
		string CommandName {get;}

		string getCommandUsage(ICommandSender p_71518_1_);

		IList CommandAliases {get;}

		void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_);

///    
///     <summary> * Returns true if the given command sender is allowed to use this command. </summary>
///     
		bool canCommandSenderUseCommand(ICommandSender p_71519_1_);

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_);

///    
///     <summary> * Return whether the specified command parameter index is a username parameter. </summary>
///     
		bool isUsernameIndex(string[] p_82358_1_, int p_82358_2_);
	}

}