using System;

namespace DotCraftCore.Command
{

	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.util.ChatComponentTranslation;
	using World = DotCraftCore.world.World;

	public class CommandShowSeed : CommandBase
	{
		private const string __OBFID = "CL_00001053";

///    
///     <summary> * Returns true if the given command sender is allowed to use this command. </summary>
///     
		public override bool canCommandSenderUseCommand(ICommandSender p_71519_1_)
		{
			return MinecraftServer.Server.SinglePlayer || base.canCommandSenderUseCommand(p_71519_1_);
		}

		public virtual string CommandName
		{
			get
			{
				return "seed";
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
			return "commands.seed.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			object var3 = p_71515_1_ is EntityPlayer ? ((EntityPlayer)p_71515_1_).worldObj : MinecraftServer.Server.worldServerForDimension(0);
			p_71515_1_.addChatMessage(new ChatComponentTranslation("commands.seed.success", new object[] {Convert.ToInt64(((World)var3).Seed)}));
		}
	}

}