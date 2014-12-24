using System.Collections;

namespace DotCraftCore.nCommand
{
	public interface ICommand
	{
		string CommandName {get;}

        IList CommandAliases { get; }

		string GetCommandUsage(ICommandSender p_71518_1_);

		void ProcessCommand(ICommandSender p_71515_1_, string[] p_71515_2_);

///    
///     <summary> * Returns true if the given command sender is allowed to use this command. </summary>
///     
		bool CanCommandSenderUseCommand(ICommandSender p_71519_1_);

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		IList AddTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_);

///    
///     <summary> * Return whether the specified command parameter index is a username parameter. </summary>
///     
		bool IsUsernameIndex(string[] p_82358_1_, int p_82358_2_);
	}
}