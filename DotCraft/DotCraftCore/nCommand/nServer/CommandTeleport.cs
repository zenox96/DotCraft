using System;
using System.Collections;

namespace DotCraftCore.nCommand.nServer
{

	using CommandBase = DotCraftCore.nCommand.CommandBase;
	using ICommandSender = DotCraftCore.nCommand.ICommandSender;
	using PlayerNotFoundException = DotCraftCore.nCommand.PlayerNotFoundException;
	using WrongUsageException = DotCraftCore.nCommand.WrongUsageException;
	using Entity = DotCraftCore.nEntity.Entity;
	using EntityPlayerMP = DotCraftCore.nEntity.nPlayer.EntityPlayerMP;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;

	public class CommandTeleport : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "tp";
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
			return "commands.tp.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length < 1)
			{
				throw new WrongUsageException("commands.tp.usage", new object[0]);
			}
			else
			{
				EntityPlayerMP var3;

				if(p_71515_2_.Length != 2 && p_71515_2_.Length != 4)
				{
					var3 = GetCommandSenderAsPlayer(p_71515_1_);
				}
				else
				{
					var3 = GetPlayer(p_71515_1_, p_71515_2_[0]);

					if(var3 == null)
					{
						throw new PlayerNotFoundException();
					}
				}

				if(p_71515_2_.Length != 3 && p_71515_2_.Length != 4)
				{
					if(p_71515_2_.Length == 1 || p_71515_2_.Length == 2)
					{
						EntityPlayerMP var11 = GetPlayer(p_71515_1_, p_71515_2_[p_71515_2_.Length - 1]);

						if(var11 == null)
						{
							throw new PlayerNotFoundException();
						}

						if(var11.worldObj != var3.worldObj)
						{
							func_152373_a(p_71515_1_, this, "commands.tp.notSameDimension", new object[0]);
							return;
						}

						var3.mountEntity((Entity)null);
						var3.playerNetServerHandler.setPlayerLocation(var11.posX, var11.posY, var11.posZ, var11.rotationYaw, var11.rotationPitch);
						func_152373_a(p_71515_1_, this, "commands.tp.success", new object[] {var3.CommandSenderName, var11.CommandSenderName});
					}
				}
				else if(var3.worldObj != null)
				{
					int var4 = p_71515_2_.Length - 3;
					double var5 = func_110666_a(p_71515_1_, var3.posX, p_71515_2_[var4++]);
					double var7 = func_110665_a(p_71515_1_, var3.posY, p_71515_2_[var4++], 0, 0);
					double var9 = func_110666_a(p_71515_1_, var3.posZ, p_71515_2_[var4++]);
					var3.mountEntity((Entity)null);
					var3.setPositionAndUpdate(var5, var7, var9);
					func_152373_a(p_71515_1_, this, "commands.tp.success.coordinates", new object[] {var3.CommandSenderName, Convert.ToDouble(var5), Convert.ToDouble(var7), Convert.ToDouble(var9)});
				}
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList AddTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length != 1 && p_71516_2_.Length != 2 ? null : GetListOfStringsMatchingLastWord(p_71516_2_, MinecraftServer.Server.AllUsernames);
		}

///    
///     <summary> * Return whether the specified command parameter index is a username parameter. </summary>
///     
		public override bool IsUsernameIndex(string[] p_82358_1_, int p_82358_2_)
		{
			return p_82358_2_ == 0;
		}
	}

}