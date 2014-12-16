using System;
using System.Collections;

namespace DotCraftCore.Command
{

	using EntityPlayerMP = DotCraftCore.Entity.Player.EntityPlayerMP;
	using ChatComponentTranslation = DotCraftCore.Util.ChatComponentTranslation;
	using EnumChatFormatting = DotCraftCore.Util.EnumChatFormatting;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class CommandHandler : ICommandManager
	{
		private static readonly Logger logger = LogManager.Logger;

	/// <summary> Map of Strings to the ICommand objects they represent  </summary>
		private readonly IDictionary commandMap = new Hashtable();

	/// <summary> The set of ICommand objects currently loaded.  </summary>
		private readonly Set commandSet = new HashSet();
		

		public virtual int executeCommand(ICommandSender p_71556_1_, string p_71556_2_)
		{
			p_71556_2_ = p_71556_2_.Trim();

			if(p_71556_2_.StartsWith("/"))
			{
				p_71556_2_ = p_71556_2_.Substring(1);
			}

			string[] var3 = StringHelperClass.StringSplit(p_71556_2_, " ", true);
			string var4 = var3[0];
			var3 = dropFirstString(var3);
			ICommand var5 = (ICommand)this.commandMap.get(var4);
			int var6 = this.getUsernameIndex(var5, var3);
			int var7 = 0;
			ChatComponentTranslation var9;

			try
			{
				if(var5 == null)
				{
					throw new CommandNotFoundException();
				}

				if(var5.canCommandSenderUseCommand(p_71556_1_))
				{
					if(var6 > -1)
					{
						EntityPlayerMP[] var8 = PlayerSelector.matchPlayers(p_71556_1_, var3[var6]);
						string var22 = var3[var6];
						EntityPlayerMP[] var10 = var8;
						int var11 = var8.Length;

						for (int var12 = 0; var12 < var11; ++var12)
						{
							EntityPlayerMP var13 = var10[var12];
							var3[var6] = var13.CommandSenderName;

							try
							{
								var5.processCommand(p_71556_1_, var3);
								++var7;
							}
							catch (CommandException var17)
							{
								ChatComponentTranslation var15 = new ChatComponentTranslation(var17.Message, var17.ErrorOjbects);
								var15.ChatStyle.Color = EnumChatFormatting.RED;
								p_71556_1_.addChatMessage(var15);
							}
						}

						var3[var6] = var22;
					}
					else
					{
						try
						{
							var5.processCommand(p_71556_1_, var3);
							++var7;
						}
						catch (CommandException var16)
						{
							var9 = new ChatComponentTranslation(var16.Message, var16.ErrorOjbects);
							var9.ChatStyle.Color = EnumChatFormatting.RED;
							p_71556_1_.addChatMessage(var9);
						}
					}
				}
				else
				{
					ChatComponentTranslation var21 = new ChatComponentTranslation("commands.generic.permission", new object[0]);
					var21.ChatStyle.Color = EnumChatFormatting.RED;
					p_71556_1_.addChatMessage(var21);
				}
			}
			catch (WrongUsageException var18)
			{
				var9 = new ChatComponentTranslation("commands.generic.usage", new object[] {new ChatComponentTranslation(var18.Message, var18.ErrorOjbects)});
				var9.ChatStyle.Color = EnumChatFormatting.RED;
				p_71556_1_.addChatMessage(var9);
			}
			catch (CommandException var19)
			{
				var9 = new ChatComponentTranslation(var19.Message, var19.ErrorOjbects);
				var9.ChatStyle.Color = EnumChatFormatting.RED;
				p_71556_1_.addChatMessage(var9);
			}
			catch (Exception var20)
			{
				var9 = new ChatComponentTranslation("commands.generic.exception", new object[0]);
				var9.ChatStyle.Color = EnumChatFormatting.RED;
				p_71556_1_.addChatMessage(var9);
				logger.error("Couldn\'t process command: \'" + p_71556_2_ + "\'", var20);
			}

			return var7;
		}

///    
///     <summary> * adds the command and any aliases it has to the internal map of available commands </summary>
///     
		public virtual ICommand registerCommand(ICommand p_71560_1_)
		{
			IList var2 = p_71560_1_.CommandAliases;
			this.commandMap.Add(p_71560_1_.CommandName, p_71560_1_);
			this.commandSet.add(p_71560_1_);

			if(var2 != null)
			{
				IEnumerator var3 = var2.GetEnumerator();

				while (var3.MoveNext())
				{
					string var4 = (string)var3.Current;
					ICommand var5 = (ICommand)this.commandMap.get(var4);

					if(var5 == null || !var5.CommandName.Equals(var4))
					{
						this.commandMap.Add(var4, p_71560_1_);
					}
				}
			}

			return p_71560_1_;
		}

///    
///     <summary> * creates a new array and sets elements 0..n-2 to be 0..n-1 of the input (n elements) </summary>
///     
		private static string[] dropFirstString(string[] p_71559_0_)
		{
			string[] var1 = new string[p_71559_0_.Length - 1];

			for (int var2 = 1; var2 < p_71559_0_.Length; ++var2)
			{
				var1[var2 - 1] = p_71559_0_[var2];
			}

			return var1;
		}

///    
///     <summary> * Performs a "begins with" string match on each token in par2. Only returns commands that par1 can use. </summary>
///     
		public virtual IList getPossibleCommands(ICommandSender p_71558_1_, string p_71558_2_)
		{
			string[] var3 = StringHelperClass.StringSplit(p_71558_2_, " ", false);
			string var4 = var3[0];

			if(var3.Length == 1)
			{
				ArrayList var8 = new ArrayList();
				IEnumerator var6 = this.commandMap.GetEnumerator();

				while (var6.MoveNext())
				{
					Entry var7 = (Entry)var6.Current;

					if(CommandBase.doesStringStartWith(var4, (string)var7.Key) && ((ICommand)var7.Value).canCommandSenderUseCommand(p_71558_1_))
					{
						var8.Add(var7.Key);
					}
				}

				return var8;
			}
			else
			{
				if(var3.Length > 1)
				{
					ICommand var5 = (ICommand)this.commandMap.get(var4);

					if(var5 != null)
					{
						return var5.addTabCompletionOptions(p_71558_1_, dropFirstString(var3));
					}
				}

				return null;
			}
		}

///    
///     <summary> * returns all commands that the commandSender can use </summary>
///     
		public virtual IList getPossibleCommands(ICommandSender p_71557_1_)
		{
			ArrayList var2 = new ArrayList();
			IEnumerator var3 = this.commandSet.GetEnumerator();

			while (var3.MoveNext())
			{
				ICommand var4 = (ICommand)var3.Current;

				if(var4.canCommandSenderUseCommand(p_71557_1_))
				{
					var2.Add(var4);
				}
			}

			return var2;
		}

///    
///     <summary> * returns a map of string to commads. All commands are returned, not just ones which someone has permission to use. </summary>
///     
		public virtual IDictionary Commands
		{
			get
			{
				return this.commandMap;
			}
		}

///    
///     <summary> * Return a command's first parameter index containing a valid username. </summary>
///     
		private int getUsernameIndex(ICommand p_82370_1_, string[] p_82370_2_)
		{
			if(p_82370_1_ == null)
			{
				return -1;
			}
			else
			{
				for (int var3 = 0; var3 < p_82370_2_.Length; ++var3)
				{
					if(p_82370_1_.isUsernameIndex(p_82370_2_, var3) && PlayerSelector.matchesMultiplePlayers(p_82370_2_[var3]))
					{
						return var3;
					}
				}

				return -1;
			}
		}
	}

}