using System.Text;

namespace DotCraftCore.nNetwork.nRcon
{

	using ICommandSender = DotCraftCore.nCommand.ICommandSender;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using ChatComponentText = DotCraftCore.nUtil.ChatComponentText;
	using ChunkCoordinates = DotCraftCore.nUtil.ChunkCoordinates;
	using IChatComponent = DotCraftCore.nUtil.IChatComponent;
	using World = DotCraftCore.nWorld.World;

	public class RConConsoleSource : ICommandSender
	{
		public static readonly RConConsoleSource field_70010_a = new RConConsoleSource();
		private StringBuilder field_70009_b = new StringBuilder();
		

///    
///     <summary> * Gets the name of this command sender (usually username, but possibly "Rcon") </summary>
///     
		public virtual string CommandSenderName
		{
			get
			{
				return "Rcon";
			}
		}

		public virtual IChatComponent func_145748_c_()
		{
			return new ChatComponentText(this.CommandSenderName);
		}

///    
///     <summary> * Notifies this sender of some sort of information.  This is for messages intended to display to the user.  Used
///     * for typical output (like "you asked for whether or not this game rule is set, so here's your answer"), warnings
///     * (like "I fetched this block for you by ID, but I'd like you to know that every time you do this, I die a little
///     * inside"), and errors (like "it's not called iron_pixacke, silly"). </summary>
///     
		public virtual void addChatMessage(IChatComponent p_145747_1_)
		{
			this.field_70009_b.Append(p_145747_1_.UnformattedText);
		}

///    
///     <summary> * Returns true if the command sender is allowed to use the given command. </summary>
///     
		public virtual bool canCommandSenderUseCommand(int p_70003_1_, string p_70003_2_)
		{
			return true;
		}

///    
///     <summary> * Return the position for this command sender. </summary>
///     
		public virtual ChunkCoordinates PlayerCoordinates
		{
			get
			{
				return new ChunkCoordinates(0, 0, 0);
			}
		}

		public virtual World EntityWorld
		{
			get
			{
				return MinecraftServer.Server.EntityWorld;
			}
		}
	}

}