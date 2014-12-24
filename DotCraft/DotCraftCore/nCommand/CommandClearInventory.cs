using System;
using System.Collections;

namespace DotCraftCore.nCommand
{

	using EntityPlayerMP = DotCraftCore.nEntity.nPlayer.EntityPlayerMP;
	using Item = DotCraftCore.nItem.Item;
	using MinecraftServer = DotCraftCore.nServer.MinecraftServer;

	public class CommandClearInventory : CommandBase
	{
		

		public virtual string CommandName
		{
			get
			{
				return "clear";
			}
		}

		public virtual string getCommandUsage(ICommandSender p_71518_1_)
		{
			return "commands.clear.usage";
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

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			EntityPlayerMP var3 = p_71515_2_.Length == 0 ? GetCommandSenderAsPlayer(p_71515_1_) : GetPlayer(p_71515_1_, p_71515_2_[0]);
			Item var4 = p_71515_2_.Length >= 2 ? GetItemByText(p_71515_1_, p_71515_2_[1]) : null;
			int var5 = p_71515_2_.Length >= 3 ? parseIntWithMin(p_71515_1_, p_71515_2_[2], 0) : -1;

			if(p_71515_2_.Length >= 2 && var4 == null)
			{
				throw new CommandException("commands.clear.failure", new object[] {var3.CommandSenderName});
			}
			else
			{
				int var6 = var3.inventory.clearInventory(var4, var5);
				var3.inventoryContainer.detectAndSendChanges();

				if(!var3.capabilities.isCreativeMode)
				{
					var3.updateHeldItem();
				}

				if(var6 == 0)
				{
					throw new CommandException("commands.clear.failure", new object[] {var3.CommandSenderName});
				}
				else
				{
					func_152373_a(p_71515_1_, this, "commands.clear.success", new object[] {var3.CommandSenderName, Convert.ToInt32(var6)});
				}
			}
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public override IList AddTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return p_71516_2_.Length == 1 ? GetListOfStringsMatchingLastWord(p_71516_2_, this.func_147209_d()) : (p_71516_2_.Length == 2 ? GetListOfStringsFromIterableMatchingLastWord(p_71516_2_, Item.itemRegistry.Keys) : null);
		}

		protected internal virtual string[] func_147209_d()
		{
			return MinecraftServer.Server.AllUsernames;
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