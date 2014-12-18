namespace DotCraftCore.nCommand
{

	using EntityPlayerMP = DotCraftCore.nEntity.nPlayer.EntityPlayerMP;
	using ChatComponentTranslation = DotCraftCore.nUtil.ChatComponentTranslation;
	using DamageSource = DotCraftCore.nUtil.DamageSource;

	public class CommandKill : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "kill";
			}
		}

///    
///     <summary> * Return the required permission level for this command. </summary>
///     
		public override int RequiredPermissionLevel
		{
			get
			{
				return 0;
			}
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.kill.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			EntityPlayerMP var3 = getCommandSenderAsPlayer(p_71515_1_);
			var3.attackEntityFrom(DamageSource.outOfWorld, float.MaxValue);
			p_71515_1_.addChatMessage(new ChatComponentTranslation("commands.kill.success", new object[0]));
		}
	}

}