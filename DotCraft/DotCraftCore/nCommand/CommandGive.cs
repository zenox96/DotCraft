using DotCraftCore.nEntity.nItem;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nItem;
using DotCraftCore.nNBT;
using DotCraftCore.nServer;
using System;
using System.Collections;

namespace DotCraftCore.nCommand
{
	public class CommandGive : CommandBase
	{
		public override string CommandName
		{
			get
			{
				return "give";
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

		public override string GetCommandUsage(ICommandSender sender)
		{
			return "commands.give.usage";
		}

		public override void ProcessCommand(ICommandSender sender, string[] p_71515_2_)
		{
			if(p_71515_2_.Length < 2)
			{
				throw new WrongUsageException("commands.give.usage", new object[0]);
			}
			else
			{
				EntityPlayerMP var3 = GetPlayer(sender, p_71515_2_[0]);
				Item var4 = GetItemByText(sender, p_71515_2_[1]);
				int var5 = 1;
				int var6 = 0;

				if(p_71515_2_.Length >= 3)
				{
					var5 = ParseIntBounded(sender, p_71515_2_[2], 1, 64);
				}

				if(p_71515_2_.Length >= 4)
				{
					var6 = ParseInt(sender, p_71515_2_[3]);
				}

				ItemStack var7 = new ItemStack(var4, var5, var6);

				if(p_71515_2_.Length >= 5)
				{
					string var8 = func_147178_a(sender, p_71515_2_, 4).UnformattedText;

					try
					{
						NBTBase var9 = JsonToNBT.func_150315_a(var8);

						if(!(var9 is NBTTagCompound))
						{
							func_152373_a(sender, this, "commands.give.tagError", new object[] {"Not a valid tag"});
							return;
						}

						var7.TagCompound = (NBTTagCompound)var9;
					}
					catch (NBTException var10)
					{
						func_152373_a(sender, this, "commands.give.tagError", new object[] {var10.Message});
						return;
					}
				}

				EntityItem var11 = var3.dropPlayerItemWithRandomChoice(var7, false);
				var11.delayBeforeCanPickup = 0;
				var11.func_145797_a(var3.CommandSenderName);
				func_152373_a(sender, this, "commands.give.success", new object[] {var7.func_151000_E(), Convert.ToInt32(var5), var3.CommandSenderName});
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList AddTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? GetListOfStringsMatchingLastWord(p_71516_2_, this.Players) : (p_71516_2_.Length == 2 ? GetListOfStringsFromIterableMatchingLastWord(p_71516_2_, Item.itemRegistry.Keys) : null);
		}

		protected internal override string[] Players
		{
			get
			{
				return MinecraftServer.Server.AllUsernames;
			}
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