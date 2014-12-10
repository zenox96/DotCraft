namespace DotCraftCore.Command
{

	using ChunkCoordinates = DotCraftCore.util.ChunkCoordinates;
	using IChatComponent = DotCraftCore.util.IChatComponent;
	using World = DotCraftCore.world.World;

	public interface ICommandSender
	{
///    
///     <summary> * Gets the name of this command sender (usually username, but possibly "Rcon") </summary>
///     
		string CommandSenderName {get;}

		IChatComponent func_145748_c_();

///    
///     <summary> * Notifies this sender of some sort of information.  This is for messages intended to display to the user.  Used
///     * for typical output (like "you asked for whether or not this game rule is set, so here's your answer"), warnings
///     * (like "I fetched this block for you by ID, but I'd like you to know that every time you do this, I die a little
///     * inside"), and errors (like "it's not called iron_pixacke, silly"). </summary>
///     
		void addChatMessage(IChatComponent p_145747_1_);

///    
///     <summary> * Returns true if the command sender is allowed to use the given command. </summary>
///     
		bool canCommandSenderUseCommand(int p_70003_1_, string p_70003_2_);

///    
///     <summary> * Return the position for this command sender. </summary>
///     
		ChunkCoordinates PlayerCoordinates {get;}

		World EntityWorld {get;}
	}

}