using System;

namespace DotCraftCore.nCommand
{

	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.nUtil.ChatComponentTranslation;
	using World = DotCraftCore.nWorld.World;

	public class CommandShowSeed : CommandBase
	{
		

///    
///     <summary> * Returns true if the given command sender is allowed to use this command. </summary>
///     
		public override bool CanCommandSenderUseCommand(ICommandSender p_71519_1_)
		{
			return MinecraftServer.Server.SinglePlayer || base.CanCommandSenderUseCommand(p_71519_1_);
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
			p_71515_1_.AddChatMessage(new ChatComponentTranslation("commands.seed.success", new object[] {Convert.ToInt64(((World)var3).Seed)}));
		}
	}

}