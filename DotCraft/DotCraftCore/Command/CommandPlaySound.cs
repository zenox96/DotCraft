using System;

namespace DotCraftCore.Command
{

	using EntityPlayerMP = DotCraftCore.Entity.Player.EntityPlayerMP;
	using S29PacketSoundEffect = DotCraftCore.network.play.server.S29PacketSoundEffect;

	public class CommandPlaySound : CommandBase
	{
		private const string __OBFID = "CL_00000774";

		public virtual string CommandName
		{
			get
			{
				return "playsound";
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
			return "commands.playsound.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_2_.Length < 2)
			{
				throw new WrongUsageException(this.getCommandUsage(p_71515_1_), new object[0]);
			}
			else
			{
				sbyte var3 = 0;
				int var36 = var3 + 1;
				string var4 = p_71515_2_[var3];
				EntityPlayerMP var5 = getPlayer(p_71515_1_, p_71515_2_[var36++]);
				double var6 = (double)var5.PlayerCoordinates.posX;
				double var8 = (double)var5.PlayerCoordinates.posY;
				double var10 = (double)var5.PlayerCoordinates.posZ;
				double var12 = 1.0D;
				double var14 = 1.0D;
				double var16 = 0.0D;

				if(p_71515_2_.Length > var36)
				{
					var6 = func_110666_a(p_71515_1_, var6, p_71515_2_[var36++]);
				}

				if(p_71515_2_.Length > var36)
				{
					var8 = func_110665_a(p_71515_1_, var8, p_71515_2_[var36++], 0, 0);
				}

				if(p_71515_2_.Length > var36)
				{
					var10 = func_110666_a(p_71515_1_, var10, p_71515_2_[var36++]);
				}

				if(p_71515_2_.Length > var36)
				{
					var12 = parseDoubleBounded(p_71515_1_, p_71515_2_[var36++], 0.0D, 3.4028234663852886E38D);
				}

				if(p_71515_2_.Length > var36)
				{
					var14 = parseDoubleBounded(p_71515_1_, p_71515_2_[var36++], 0.0D, 2.0D);
				}

				if(p_71515_2_.Length > var36)
				{
					var16 = parseDoubleBounded(p_71515_1_, p_71515_2_[var36++], 0.0D, 1.0D);
				}

				double var18 = var12 > 1.0D ? var12 * 16.0D : 16.0D;
				double var20 = var5.getDistance(var6, var8, var10);

				if(var20 > var18)
				{
					if(var16 <= 0.0D)
					{
						throw new CommandException("commands.playsound.playerTooFar", new object[] {var5.CommandSenderName});
					}

					double var22 = var6 - var5.posX;
					double var24 = var8 - var5.posY;
					double var26 = var10 - var5.posZ;
					double var28 = Math.Sqrt(var22 * var22 + var24 * var24 + var26 * var26);
					double var30 = var5.posX;
					double var32 = var5.posY;
					double var34 = var5.posZ;

					if(var28 > 0.0D)
					{
						var30 += var22 / var28 * 2.0D;
						var32 += var24 / var28 * 2.0D;
						var34 += var26 / var28 * 2.0D;
					}

					var5.playerNetServerHandler.sendPacket(new S29PacketSoundEffect(var4, var30, var32, var34, (float)var16, (float)var14));
				}
				else
				{
					var5.playerNetServerHandler.sendPacket(new S29PacketSoundEffect(var4, var6, var8, var10, (float)var12, (float)var14));
				}

				func_152373_a(p_71515_1_, this, "commands.playsound.success", new object[] {var4, var5.CommandSenderName});
			}
		}

///    
///     <summary> * Return whether the specified command parameter index is a username parameter. </summary>
///     
		public override bool isUsernameIndex(string[] p_82358_1_, int p_82358_2_)
		{
			return p_82358_2_ == 1;
		}
	}

}