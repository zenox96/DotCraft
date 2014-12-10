using System;
using System.Collections;
using System.Text;

namespace DotCraftCore.Command
{
	using EntityPlayerMP = DotCraftCore.Entity.Player.EntityPlayerMP;
	using Item = DotCraftCore.item.Item;
	using MinecraftServer = DotCraftCore.server.MinecraftServer;
	using ChatComponentText = DotCraftCore.util.ChatComponentText;
	using ChatComponentTranslation = DotCraftCore.util.ChatComponentTranslation;
	using EnumChatFormatting = DotCraftCore.util.EnumChatFormatting;
	using IChatComponent = DotCraftCore.util.IChatComponent;

	public abstract class CommandBase : ICommand
	{
		private static IAdminCommand theAdmin;
		private const string __OBFID = "CL_00001739";

///    
///     <summary> * Return the required permission level for this command. </summary>
///     
		public virtual int RequiredPermissionLevel
		{
			get
			{
				return 4;
			}
		}

		public virtual IList CommandAliases
		{
			get
			{
				return null;
			}
		}

///    
///     <summary> * Returns true if the given command sender is allowed to use this command. </summary>
///     
		public virtual bool canCommandSenderUseCommand(ICommandSender p_71519_1_)
		{
			return p_71519_1_.canCommandSenderUseCommand(this.RequiredPermissionLevel, this.CommandName);
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public virtual IList addTabCompletionOptions(ICommandSender p_71516_1_, string[] p_71516_2_)
		{
			return null;
		}

///    
///     <summary> * Parses an int from the given string. </summary>
///     
		public static int parseInt(ICommandSender p_71526_0_, string p_71526_1_)
		{
			try
			{
				return Convert.ToInt32(p_71526_1_);
			}
			catch (NumberFormatException var3)
			{
				throw new NumberInvalidException("commands.generic.num.invalid", new object[] {p_71526_1_});
			}
		}

///    
///     <summary> * Parses an int from the given sring with a specified minimum. </summary>
///     
		public static int parseIntWithMin(ICommandSender p_71528_0_, string p_71528_1_, int p_71528_2_)
		{
			return parseIntBounded(p_71528_0_, p_71528_1_, p_71528_2_, int.MaxValue);
		}

///    
///     <summary> * Parses an int from the given string within a specified bound. </summary>
///     
		public static int parseIntBounded(ICommandSender p_71532_0_, string p_71532_1_, int p_71532_2_, int p_71532_3_)
		{
			int var4 = parseInt(p_71532_0_, p_71532_1_);

			if(var4 < p_71532_2_)
			{
				throw new NumberInvalidException("commands.generic.num.tooSmall", new object[] {Convert.ToInt32(var4), Convert.ToInt32(p_71532_2_)});
			}
			else if(var4 > p_71532_3_)
			{
				throw new NumberInvalidException("commands.generic.num.tooBig", new object[] {Convert.ToInt32(var4), Convert.ToInt32(p_71532_3_)});
			}
			else
			{
				return var4;
			}
		}

///    
///     <summary> * Parses a double from the given string or throws an exception if it's not a double. </summary>
///     
		public static double parseDouble(ICommandSender p_82363_0_, string p_82363_1_)
		{
			try
			{
				double var2 = Convert.ToDouble(p_82363_1_);

				if(!Doubles.isFinite(var2))
				{
					throw new NumberInvalidException("commands.generic.num.invalid", new object[] {p_82363_1_});
				}
				else
				{
					return var2;
				}
			}
			catch (NumberFormatException var4)
			{
				throw new NumberInvalidException("commands.generic.num.invalid", new object[] {p_82363_1_});
			}
		}

///    
///     <summary> * Parses a double from the given string.  Throws if the string could not be parsed as a double, or if it's less
///     * than the given minimum value. </summary>
///     
		public static double parseDoubleWithMin(ICommandSender p_110664_0_, string p_110664_1_, double p_110664_2_)
		{
			return parseDoubleBounded(p_110664_0_, p_110664_1_, p_110664_2_, double.MaxValue);
		}

///    
///     <summary> * Parses a double from the given string.  Throws if the string could not be parsed as a double, or if it's not
///     * between the given min and max values. </summary>
///     
		public static double parseDoubleBounded(ICommandSender p_110661_0_, string p_110661_1_, double p_110661_2_, double p_110661_4_)
		{
			double var6 = parseDouble(p_110661_0_, p_110661_1_);

			if(var6 < p_110661_2_)
			{
				throw new NumberInvalidException("commands.generic.double.tooSmall", new object[] {Convert.ToDouble(var6), Convert.ToDouble(p_110661_2_)});
			}
			else if(var6 > p_110661_4_)
			{
				throw new NumberInvalidException("commands.generic.double.tooBig", new object[] {Convert.ToDouble(var6), Convert.ToDouble(p_110661_4_)});
			}
			else
			{
				return var6;
			}
		}

///    
///     <summary> * Parses a boolean value from the given string.  Throws if the string does not contain a boolean value.  Accepted
///     * values are (case-sensitive): "true", "false", "0", "1". </summary>
///     
		public static bool parseBoolean(ICommandSender p_110662_0_, string p_110662_1_)
		{
			if(!p_110662_1_.Equals("true") && !p_110662_1_.Equals("1"))
			{
				if(!p_110662_1_.Equals("false") && !p_110662_1_.Equals("0"))
				{
					throw new CommandException("commands.generic.boolean.invalid", new object[] {p_110662_1_});
				}
				else
				{
					return false;
				}
			}
			else
			{
				return true;
			}
		}

///    
///     <summary> * Returns the given ICommandSender as a EntityPlayer or throw an exception. </summary>
///     
		public static EntityPlayerMP getCommandSenderAsPlayer(ICommandSender p_71521_0_)
		{
			if(p_71521_0_ is EntityPlayerMP)
			{
				return (EntityPlayerMP)p_71521_0_;
			}
			else
			{
				throw new PlayerNotFoundException("You must specify which player you wish to perform this action on.", new object[0]);
			}
		}

		public static EntityPlayerMP getPlayer(ICommandSender p_82359_0_, string p_82359_1_)
		{
			EntityPlayerMP var2 = PlayerSelector.matchOnePlayer(p_82359_0_, p_82359_1_);

			if(var2 != null)
			{
				return var2;
			}
			else
			{
				var2 = MinecraftServer.Server.ConfigurationManager.func_152612_a(p_82359_1_);

				if(var2 == null)
				{
					throw new PlayerNotFoundException();
				}
				else
				{
					return var2;
				}
			}
		}

		public static string func_96332_d(ICommandSender p_96332_0_, string p_96332_1_)
		{
			EntityPlayerMP var2 = PlayerSelector.matchOnePlayer(p_96332_0_, p_96332_1_);

			if(var2 != null)
			{
				return var2.CommandSenderName;
			}
			else if(PlayerSelector.hasArguments(p_96332_1_))
			{
				throw new PlayerNotFoundException();
			}
			else
			{
				return p_96332_1_;
			}
		}

		public static IChatComponent func_147178_a(ICommandSender p_147178_0_, string[] p_147178_1_, int p_147178_2_)
		{
			return func_147176_a(p_147178_0_, p_147178_1_, p_147178_2_, false);
		}

		public static IChatComponent func_147176_a(ICommandSender p_147176_0_, string[] p_147176_1_, int p_147176_2_, bool p_147176_3_)
		{
			ChatComponentText var4 = new ChatComponentText("");

			for (int var5 = p_147176_2_; var5 < p_147176_1_.Length; ++var5)
			{
				if(var5 > p_147176_2_)
				{
					var4.appendText(" ");
				}

				object var6 = new ChatComponentText(p_147176_1_[var5]);

				if(p_147176_3_)
				{
					IChatComponent var7 = PlayerSelector.func_150869_b(p_147176_0_, p_147176_1_[var5]);

					if(var7 != null)
					{
						var6 = var7;
					}
					else if(PlayerSelector.hasArguments(p_147176_1_[var5]))
					{
						throw new PlayerNotFoundException();
					}
				}

				var4.appendSibling((IChatComponent)var6);
			}

			return var4;
		}

		public static string func_82360_a(ICommandSender p_82360_0_, string[] p_82360_1_, int p_82360_2_)
		{
			StringBuilder var3 = new StringBuilder();

			for (int var4 = p_82360_2_; var4 < p_82360_1_.Length; ++var4)
			{
				if(var4 > p_82360_2_)
				{
					var3.Append(" ");
				}

				string var5 = p_82360_1_[var4];
				var3.Append(var5);
			}

			return var3.ToString();
		}

		public static double func_110666_a(ICommandSender p_110666_0_, double p_110666_1_, string p_110666_3_)
		{
			return func_110665_a(p_110666_0_, p_110666_1_, p_110666_3_, -30000000, 30000000);
		}

		public static double func_110665_a(ICommandSender p_110665_0_, double p_110665_1_, string p_110665_3_, int p_110665_4_, int p_110665_5_)
		{
			bool var6 = p_110665_3_.StartsWith("~");

			if(var6 && double.isNaN(p_110665_1_))
			{
				throw new NumberInvalidException("commands.generic.num.invalid", new object[] {Convert.ToDouble(p_110665_1_)});
			}
			else
			{
				double var7 = var6 ? p_110665_1_ : 0.0D;

				if(!var6 || p_110665_3_.Length > 1)
				{
					bool var9 = p_110665_3_.Contains(".");

					if(var6)
					{
						p_110665_3_ = p_110665_3_.Substring(1);
					}

					var7 += parseDouble(p_110665_0_, p_110665_3_);

					if(!var9 && !var6)
					{
						var7 += 0.5D;
					}
				}

				if(p_110665_4_ != 0 || p_110665_5_ != 0)
				{
					if(var7 < (double)p_110665_4_)
					{
						throw new NumberInvalidException("commands.generic.double.tooSmall", new object[] {Convert.ToDouble(var7), Convert.ToInt32(p_110665_4_)});
					}

					if(var7 > (double)p_110665_5_)
					{
						throw new NumberInvalidException("commands.generic.double.tooBig", new object[] {Convert.ToDouble(var7), Convert.ToInt32(p_110665_5_)});
					}
				}

				return var7;
			}
		}

///    
///     <summary> * Gets the Item specified by the given text string.  First checks the item registry, then tries by parsing the
///     * string as an integer ID (deprecated).  Warns the sender if we matched by parsing the ID.  Throws if the item
///     * wasn't found.  Returns the item if it was found. </summary>
///     
		public static Item getItemByText(ICommandSender p_147179_0_, string p_147179_1_)
		{
			Item var2 = (Item)Item.itemRegistry.getObject(p_147179_1_);

			if(var2 == null)
			{
				try
				{
					Item var3 = Item.getItemById(Convert.ToInt32(p_147179_1_));

					if(var3 != null)
					{
						ChatComponentTranslation var4 = new ChatComponentTranslation("commands.generic.deprecatedId", new object[] {Item.itemRegistry.getNameForObject(var3)});
						var4.ChatStyle.Color = EnumChatFormatting.GRAY;
						p_147179_0_.addChatMessage(var4);
					}

					var2 = var3;
				}
				catch (NumberFormatException var5)
				{
					;
				}
			}

			if(var2 == null)
			{
				throw new NumberInvalidException("commands.give.notFound", new object[] {p_147179_1_});
			}
			else
			{
				return var2;
			}
		}

///    
///     <summary> * Gets the Block specified by the given text string.  First checks the block registry, then tries by parsing the
///     * string as an integer ID (deprecated).  Warns the sender if we matched by parsing the ID.  Throws if the block
///     * wasn't found.  Returns the block if it was found. </summary>
///     
		public static Block getBlockByText(ICommandSender p_147180_0_, string p_147180_1_)
		{
			if(Block.blockRegistry.containsKey(p_147180_1_))
			{
				return (Block)Block.blockRegistry.getObject(p_147180_1_);
			}
			else
			{
				try
				{
					int var2 = Convert.ToInt32(p_147180_1_);

					if(Block.blockRegistry.containsID(var2))
					{
						Block var3 = Block.getBlockById(var2);
						ChatComponentTranslation var4 = new ChatComponentTranslation("commands.generic.deprecatedId", new object[] {Block.blockRegistry.getNameForObject(var3)});
						var4.ChatStyle.Color = EnumChatFormatting.GRAY;
						p_147180_0_.addChatMessage(var4);
						return var3;
					}
				}
				catch (NumberFormatException var5)
				{
					;
				}

				throw new NumberInvalidException("commands.give.notFound", new object[] {p_147180_1_});
			}
		}

///    
///     <summary> * Creates a linguistic series joining the input objects together.  Examples: 1) {} --> "",  2) {"Steve"} -->
///     * "Steve",  3) {"Steve", "Phil"} --> "Steve and Phil",  4) {"Steve", "Phil", "Mark"} --> "Steve, Phil and Mark" </summary>
///     
		public static string joinNiceString(object[] p_71527_0_)
		{
			StringBuilder var1 = new StringBuilder();

			for (int var2 = 0; var2 < p_71527_0_.Length; ++var2)
			{
				string var3 = p_71527_0_[var2].ToString();

				if(var2 > 0)
				{
					if(var2 == p_71527_0_.Length - 1)
					{
						var1.Append(" and ");
					}
					else
					{
						var1.Append(", ");
					}
				}

				var1.Append(var3);
			}

			return var1.ToString();
		}

///    
///     <summary> * Creates a linguistic series joining the input chat components.  Examples: 1) {} --> "",  2) {"Steve"} -->
///     * "Steve",  3) {"Steve", "Phil"} --> "Steve and Phil",  4) {"Steve", "Phil", "Mark"} --> "Steve, Phil and Mark" </summary>
///     
		public static IChatComponent joinNiceString(IChatComponent[] p_147177_0_)
		{
			ChatComponentText var1 = new ChatComponentText("");

			for (int var2 = 0; var2 < p_147177_0_.Length; ++var2)
			{
				if(var2 > 0)
				{
					if(var2 == p_147177_0_.Length - 1)
					{
						var1.appendText(" and ");
					}
					else if(var2 > 0)
					{
						var1.appendText(", ");
					}
				}

				var1.appendSibling(p_147177_0_[var2]);
			}

			return var1;
		}

///    
///     <summary> * Creates a linguistic series joining together the elements of the given collection.  Examples: 1) {} --> "",  2)
///     * {"Steve"} --> "Steve",  3) {"Steve", "Phil"} --> "Steve and Phil",  4) {"Steve", "Phil", "Mark"} --> "Steve, Phil
///     * and Mark" </summary>
///     
		public static string joinNiceStringFromCollection(ICollection p_96333_0_)
		{
			return joinNiceString(p_96333_0_.ToArray(new string[p_96333_0_.size()]));
		}

///    
///     <summary> * Returns true if the given substring is exactly equal to the start of the given string (case insensitive). </summary>
///     
		public static bool doesStringStartWith(string p_71523_0_, string p_71523_1_)
		{
			return p_71523_1_.regionMatches(true, 0, p_71523_0_, 0, p_71523_0_.Length);
		}

///    
///     <summary> * Returns a List of strings (chosen from the given strings) which the last word in the given string array is a
///     * beginning-match for. (Tab completion). </summary>
///     
		public static IList getListOfStringsMatchingLastWord(string[] p_71530_0_, params string[] p_71530_1_)
		{
			string var2 = p_71530_0_[p_71530_0_.Length - 1];
			ArrayList var3 = new ArrayList();
			string[] var4 = p_71530_1_;
			int var5 = p_71530_1_.length;

			for (int var6 = 0; var6 < var5; ++var6)
			{
				string var7 = var4[var6];

				if(doesStringStartWith(var2, var7))
				{
					var3.Add(var7);
				}
			}

			return var3;
		}

///    
///     <summary> * Returns a List of strings (chosen from the given string iterable) which the last word in the given string array
///     * is a beginning-match for. (Tab completion). </summary>
///     
		public static IList getListOfStringsFromIterableMatchingLastWord(string[] p_71531_0_, IEnumerable p_71531_1_)
		{
			string var2 = p_71531_0_[p_71531_0_.Length - 1];
			ArrayList var3 = new ArrayList();
			IEnumerator var4 = p_71531_1_.GetEnumerator();

			while (var4.MoveNext())
			{
				string var5 = (string)var4.Current;

				if(doesStringStartWith(var2, var5))
				{
					var3.Add(var5);
				}
			}

			return var3;
		}

///    
///     <summary> * Return whether the specified command parameter index is a username parameter. </summary>
///     
		public virtual bool isUsernameIndex(string[] p_82358_1_, int p_82358_2_)
		{
			return false;
		}

		public static void func_152373_a(ICommandSender p_152373_0_, ICommand p_152373_1_, string p_152373_2_, params object[] p_152373_3_)
		{
			func_152374_a(p_152373_0_, p_152373_1_, 0, p_152373_2_, p_152373_3_);
		}

		public static void func_152374_a(ICommandSender p_152374_0_, ICommand p_152374_1_, int p_152374_2_, string p_152374_3_, params object[] p_152374_4_)
		{
			if(theAdmin != null)
			{
				theAdmin.func_152372_a(p_152374_0_, p_152374_1_, p_152374_2_, p_152374_3_, p_152374_4_);
			}
		}

///    
///     <summary> * Sets the static IAdminCommander. </summary>
///     
		public static IAdminCommand AdminCommander
		{
			set
			{
				theAdmin = value;
			}
		}

		public virtual int compareTo(ICommand p_compareTo_1_)
		{
			return this.CommandName.CompareTo(p_compareTo_1_.CommandName);
		}

		public virtual int compareTo(object p_compareTo_1_)
		{
			return this.CompareTo((ICommand)p_compareTo_1_);
		}
	}

}