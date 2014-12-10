using System;
using System.Collections;

namespace DotCraftCore.Command
{

	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using ClickEvent = DotCraftCore.event.ClickEvent;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;
	using ChatComponentTranslation = DotCraftCore.util.ChatComponentTranslation;
	using EnumChatFormatting = DotCraftCore.util.EnumChatFormatting;
	using MathHelper = DotCraftCore.util.MathHelper;

	public class CommandHelp : CommandBase
	{
		private const string __OBFID = "CL_00000529";

		public virtual string CommandName
		{
			get
			{
				return "help";
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
			return "commands.help.usage";
		}

		public override IList CommandAliases
		{
			get
			{
				return new string[] {"?"};
			}
		}

		public virtual void processCommand(ICommandSender p_71515_1_, string[] p_71515_2_)
		{
			IList var3 = this.getSortedPossibleCommands(p_71515_1_);
			sbyte var4 = 7;
			int var5 = (var3.Count - 1) / var4;
			bool var6 = false;
			int var13;

			try
			{
				var13 = p_71515_2_.Length == 0 ? 0 : parseIntBounded(p_71515_1_, p_71515_2_[0], 1, var5 + 1) - 1;
			}
			catch (NumberInvalidException var12)
			{
				IDictionary var8 = this.Commands;
				ICommand var9 = (ICommand)var8[p_71515_2_[0]];

				if(var9 != null)
				{
					throw new WrongUsageException(var9.getCommandUsage(p_71515_1_), new object[0]);
				}

				if(MathHelper.parseIntWithDefault(p_71515_2_[0], -1) != -1)
				{
					throw var12;
				}

				throw new CommandNotFoundException();
			}

			int var7 = Math.Min((var13 + 1) * var4, var3.Count);
			ChatComponentTranslation var14 = new ChatComponentTranslation("commands.help.header", new object[] {Convert.ToInt32(var13 + 1), Convert.ToInt32(var5 + 1)});
			var14.ChatStyle.Color = EnumChatFormatting.DARK_GREEN;
			p_71515_1_.addChatMessage(var14);

			for (int var15 = var13 * var4; var15 < var7; ++var15)
			{
				ICommand var10 = (ICommand)var3[var15];
				ChatComponentTranslation var11 = new ChatComponentTranslation(var10.getCommandUsage(p_71515_1_), new object[0]);
				var11.ChatStyle.ChatClickEvent = new ClickEvent(ClickEvent.Action.SUGGEST_COMMAND, "/" + var10.CommandName + " ");
				p_71515_1_.addChatMessage(var11);
			}

			if(var13 == 0 && p_71515_1_ is EntityPlayer)
			{
				ChatComponentTranslation var16 = new ChatComponentTranslation("commands.help.footer", new object[0]);
				var16.ChatStyle.Color = EnumChatFormatting.GREEN;
				p_71515_1_.addChatMessage(var16);
			}
		}

///    
///     <summary> * Returns a sorted list of all possible commands for the given ICommandSender. </summary>
///     
		protected internal virtual IList getSortedPossibleCommands(ICommandSender p_71534_1_)
		{
			IList var2 = MinecraftServer.Server.CommandManager.getPossibleCommands(p_71534_1_);
			Collections.sort(var2);
			return var2;
		}

		protected internal virtual IDictionary Commands
		{
			get
			{
				return MinecraftServer.Server.CommandManager.Commands;
			}
		}
	}

}