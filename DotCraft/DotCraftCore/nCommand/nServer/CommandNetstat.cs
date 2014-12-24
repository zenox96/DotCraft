using System;

namespace DotCraftCore.nCommand.nServer
{

	using CommandBase = DotCraftCore.nCommand.CommandBase;
	using ICommandSender = DotCraftCore.nCommand.ICommandSender;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using NetworkManager = DotCraftCore.nNetwork.NetworkManager;
	using NetworkStatistics = DotCraftCore.nNetwork.NetworkStatistics;
	using ChatComponentText = DotCraftCore.nUtil.ChatComponentText;

	public class CommandNetstat : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "netstat";
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
			return "commands.players.usage";
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			if(p_71515_1_ is EntityPlayer)
			{
				p_71515_1_.AddChatMessage(new ChatComponentText("Command is not available for players"));
			}
			else
			{
				if(p_71515_2_.Length > 0 && p_71515_2_[0].Length > 1)
				{
					if("hottest-read".Equals(p_71515_2_[0]))
					{
						p_71515_1_.AddChatMessage(new ChatComponentText(NetworkManager.field_152462_h.func_152477_e().ToString()));
					}
					else if("hottest-write".Equals(p_71515_2_[0]))
					{
						p_71515_1_.AddChatMessage(new ChatComponentText(NetworkManager.field_152462_h.func_152475_g().ToString()));
					}
					else if("most-read".Equals(p_71515_2_[0]))
					{
						p_71515_1_.AddChatMessage(new ChatComponentText(NetworkManager.field_152462_h.func_152467_f().ToString()));
					}
					else if("most-write".Equals(p_71515_2_[0]))
					{
						p_71515_1_.AddChatMessage(new ChatComponentText(NetworkManager.field_152462_h.func_152470_h().ToString()));
					}
					else
					{
						NetworkStatistics.PacketStat var4;
						int var7;

						if("packet-read".Equals(p_71515_2_[0]))
						{
							if(p_71515_2_.Length > 1 && p_71515_2_[1].Length > 0)
							{
								try
								{
									var7 = Convert.ToInt32(p_71515_2_[1].Trim());
									var4 = NetworkManager.field_152462_h.func_152466_a(var7);
									this.func_152375_a(p_71515_1_, var7, var4);
								}
								catch (Exception var6)
								{
									p_71515_1_.AddChatMessage(new ChatComponentText("Packet " + p_71515_2_[1] + " not found!"));
								}
							}
							else
							{
								p_71515_1_.AddChatMessage(new ChatComponentText("Packet id is missing"));
							}
						}
						else if("packet-write".Equals(p_71515_2_[0]))
						{
							if(p_71515_2_.Length > 1 && p_71515_2_[1].Length > 0)
							{
								try
								{
									var7 = Convert.ToInt32(p_71515_2_[1].Trim());
									var4 = NetworkManager.field_152462_h.func_152468_b(var7);
									this.func_152375_a(p_71515_1_, var7, var4);
								}
								catch (Exception var5)
								{
									p_71515_1_.AddChatMessage(new ChatComponentText("Packet " + p_71515_2_[1] + " not found!"));
								}
							}
							else
							{
								p_71515_1_.AddChatMessage(new ChatComponentText("Packet id is missing"));
							}
						}
						else if("read-count".Equals(p_71515_2_[0]))
						{
							p_71515_1_.AddChatMessage(new ChatComponentText("total-read-count" + Convert.ToString(NetworkManager.field_152462_h.func_152472_c())));
						}
						else if("write-count".Equals(p_71515_2_[0]))
						{
							p_71515_1_.AddChatMessage(new ChatComponentText("total-write-count" + Convert.ToString(NetworkManager.field_152462_h.func_152473_d())));
						}
						else
						{
							p_71515_1_.AddChatMessage(new ChatComponentText("Unrecognized: " + p_71515_2_[0]));
						}
					}
				}
				else
				{
					string var3 = "reads: " + NetworkManager.field_152462_h.func_152465_a();
					var3 = var3 + ", writes: " + NetworkManager.field_152462_h.func_152471_b();
					p_71515_1_.AddChatMessage(new ChatComponentText(var3));
				}
			}
		}

		private void func_152375_a(ICommandSender p_152375_1_, int p_152375_2_, NetworkStatistics.PacketStat p_152375_3_)
		{
			if(p_152375_3_ != null)
			{
				p_152375_1_.AddChatMessage(new ChatComponentText(p_152375_3_.ToString()));
			}
			else
			{
				p_152375_1_.AddChatMessage(new ChatComponentText("Packet " + p_152375_2_ + " not found!"));
			}
		}
	}

}