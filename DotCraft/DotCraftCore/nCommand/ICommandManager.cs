using System.Collections;

namespace DotCraftCore.nCommand
{


	public interface ICommandManager
	{
		int executeCommand(ICommandSender p_71556_1_, string p_71556_2_);

///    
///     <summary> * Performs a "begins with" string match on each token in par2. Only returns commands that par1 can use. </summary>
///     
		IList getPossibleCommands(ICommandSender p_71558_1_, string p_71558_2_);

///    
///     <summary> * returns all commands that the commandSender can use </summary>
///     
		IList getPossibleCommands(ICommandSender p_71557_1_);

///    
///     <summary> * returns a map of string to commads. All commands are returned, not just ones which someone has permission to use. </summary>
///     
		IDictionary Commands {get;}
	}

}