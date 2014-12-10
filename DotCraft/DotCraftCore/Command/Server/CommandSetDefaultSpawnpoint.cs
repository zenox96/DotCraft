using System;

namespace DotCraftCore.Command.Server
{

	using CommandBase = DotCraftCore.Command.CommandBase;
	using ICommandSender = DotCraftCore.Command.ICommandSender;
	using WrongUsageException = DotCraftCore.Command.WrongUsageException;
	using ChunkCoordinates = DotCraftCore.util.ChunkCoordinates;

	public class CommandSetDefaultSpawnpoint : CommandBase
	{
		private const string __OBFID = "CL_00000973";

		public virtual string CommandName
		{
			get
			{
				return "setworldspawn";
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
			return "commands.setworldspawn.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length == 3)
			{
				if(p_71515_1_.EntityWorld == null)
				{
					throw new WrongUsageException("commands.setworldspawn.usage", new object[0]);
				}

				sbyte var3 = 0;
				int var7 = var3 + 1;
				int var4 = parseIntBounded(p_71515_1_, p_71515_2_[var3], -30000000, 30000000);
				int var5 = parseIntBounded(p_71515_1_, p_71515_2_[var7++], 0, 256);
				int var6 = parseIntBounded(p_71515_1_, p_71515_2_[var7++], -30000000, 30000000);
				p_71515_1_.EntityWorld.setSpawnLocation(var4, var5, var6);
				func_152373_a(p_71515_1_, this, "commands.setworldspawn.success", new object[] {Convert.ToInt32(var4), Convert.ToInt32(var5), Convert.ToInt32(var6)});
			}
			else
			{
				if(p_71515_2_.Length != 0)
				{
					throw new WrongUsageException("commands.setworldspawn.usage", new object[0]);
				}

				ChunkCoordinates var8 = getCommandSenderAsPlayer(p_71515_1_).PlayerCoordinates;
				p_71515_1_.EntityWorld.setSpawnLocation(var8.posX, var8.posY, var8.posZ);
				func_152373_a(p_71515_1_, this, "commands.setworldspawn.success", new object[] {Convert.ToInt32(var8.posX), Convert.ToInt32(var8.posY), Convert.ToInt32(var8.posZ)});
			}
		}
	}

}