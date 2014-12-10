namespace DotCraftCore.Command
{

	using EntityPlayerMP = DotCraftCore.Entity.Player.EntityPlayerMP;
	using ChatComponentTranslation = DotCraftCore.Util.ChatComponentTranslation;
	using DamageSource = DotCraftCore.Util.DamageSource;

	public class CommandKill : CommandBase
	{
		private const string __OBFID = "CL_00000570";

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