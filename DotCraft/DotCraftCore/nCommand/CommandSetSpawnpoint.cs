using System;
using System.Collections;

namespace DotCraftCore.nCommand
{

	using EntityPlayerMP = DotCraftCore.nEntity.nPlayer.EntityPlayerMP;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;
	using ChunkCoordinates = DotCraftCore.nUtil.ChunkCoordinates;

	public class CommandSetSpawnpoint : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "spawnpoint";
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
			return "commands.spawnpoint.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			EntityPlayerMP var3 = p_71515_2_.Length == 0 ? GetCommandSenderAsPlayer(p_71515_1_) : GetPlayer(p_71515_1_, p_71515_2_[0]);

			if(p_71515_2_.Length == 4)
			{
				if(var3.worldObj != null)
				{
					sbyte var4 = 1;
					int var5 = 30000000;
					int var9 = var4 + 1;
					int var6 = parseIntBounded(p_71515_1_, p_71515_2_[var4], -var5, var5);
					int var7 = parseIntBounded(p_71515_1_, p_71515_2_[var9++], 0, 256);
					int var8 = parseIntBounded(p_71515_1_, p_71515_2_[var9++], -var5, var5);
					var3.setSpawnChunk(new ChunkCoordinates(var6, var7, var8), true);
					func_152373_a(p_71515_1_, this, "commands.spawnpoint.success", new object[] {var3.CommandSenderName, Convert.ToInt32(var6), Convert.ToInt32(var7), Convert.ToInt32(var8)});
				}
			}
			else
			{
				if(p_71515_2_.Length > 1)
				{
					throw new WrongUsageException("commands.spawnpoint.usage", new object[0]);
				}

				ChunkCoordinates var10 = var3.PlayerCoordinates;
				var3.setSpawnChunk(var10, true);
				func_152373_a(p_71515_1_, this, "commands.spawnpoint.success", new object[] {var3.CommandSenderName, Convert.ToInt32(var10.posX), Convert.ToInt32(var10.posY), Convert.ToInt32(var10.posZ)});
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