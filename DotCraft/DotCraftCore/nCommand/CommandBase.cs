using DotCraftCore.nBlock;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nItem;
using DotCraftCore.nServer;
using DotCraftCore.nUtil;
using System;
using System.Collections;
using System.Text;

namespace DotCraftCore.nCommand
{
	public abstract class CommandBase : ICommand
	{
		private static IAdminCommand theAdmin;

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

		public abstract override IList CommandAliases
		{
			get;
		}

        public abstract override string CommandName
        {
            get;
        }

///    
///     <summary> * Returns true if the given command sender is allowed to use this command. </summary>
///     
		public virtual bool CanCommandSenderUseCommand(ICommandSender sender)
		{
			return sender.CanCommandSenderUseCommand(this.RequiredPermissionLevel, this.CommandName);
		}

///    
///     <summary> * Adds the strings available in this command to the given list of tab completion options. </summary>
///     
		public virtual IList AddTabCompletionOptions(ICommandSender sender, string[] p_71516_2_)
		{
			return null;
		}

///    
///     <summary> * Parses an int from the given string. </summary>
///     
		public static int ParseInt(ICommandSender sender, string stringToInt)
		{
			try
			{
				return Convert.ToInt32(stringToInt);
			}
			catch (FormatException)
			{
				throw new NumberInvalidException("commands.generic.num.invalid", new object[] {stringToInt});
			}
		}

///    
///     <summary> * Parses an int from the given sring with a specified minimum. </summary>
///     
		public static int ParseIntWithMin(ICommandSender sender, string stringToInt, int minimumValue)
		{
			return ParseIntBounded(sender, stringToInt, minimumValue, int.MaxValue);
		}

///    
///     <summary> * Parses an int from the given string within a specified bound. </summary>
///     
		public static int ParseIntBounded(ICommandSender sender, string stringToInt, int minimumValue, int maximumValue)
		{
			int var4 = ParseInt(sender, stringToInt);

			if(var4 < minimumValue)
			{
				throw new NumberInvalidException("commands.generic.num.tooSmall", new object[] {Convert.ToInt32(var4), Convert.ToInt32(minimumValue)});
			}
			else if(var4 > maximumValue)
			{
				throw new NumberInvalidException("commands.generic.num.tooBig", new object[] {Convert.ToInt32(var4), Convert.ToInt32(maximumValue)});
			}
			else
			{
				return var4;
			}
		}

///    
///     <summary> * Parses a double from the given string or throws an exception if it's not a double. </summary>
///     
		public static double ParseDouble(ICommandSender sender, string p_82363_1_)
		{
			try
			{
				double var2 = Convert.ToDouble(p_82363_1_);

				if(double.IsInfinity(var2) | double.IsNaN(var2))
				{
					throw new NumberInvalidException("commands.generic.num.invalid", new object[] {p_82363_1_});
				}
				else
				{
					return var2;
				}
			}
			catch (FormatException)
			{
				throw new NumberInvalidException("commands.generic.num.invalid", new object[] {p_82363_1_});
			}
		}

///    
///     <summary> * Parses a double from the given string.  Throws if the string could not be parsed as a double, or if it's less
///     * than the given minimum value. </summary>
///     
        public static double ParseDoubleWithMin(ICommandSender sender, string p_110664_1_, double p_110664_2_)
		{
            return parseDoubleBounded(sender, p_110664_1_, p_110664_2_, double.MaxValue);
		}

///    
///     <summary> * Parses a double from the given string.  Throws if the string could not be parsed as a double, or if it's not
///     * between the given min and max values. </summary>
///     
        public static double parseDoubleBounded(ICommandSender sender, string p_110661_1_, double p_110661_2_, double p_110661_4_)
		{
            double var6 = ParseDouble(sender, p_110661_1_);

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
        public static bool ParseBoolean(ICommandSender sender, string p_110662_1_)
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
        public static EntityPlayerMP GetCommandSenderAsPlayer(ICommandSender sender)
		{
			if(sender is EntityPlayerMP)
			{
				return (EntityPlayerMP)sender;
			}
			else
			{
				throw new PlayerNotFoundException("You must specify which player you wish to perform this action on.", new object[0]);
			}
		}

        public static EntityPlayerMP GetPlayer(ICommandSender sender, string p_82359_1_)
		{
			EntityPlayerMP var2 = PlayerSelector.matchOnePlayer(sender, p_82359_1_);

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

        public static string func_96332_d(ICommandSender sender, string p_96332_1_)
		{
			EntityPlayerMP var2 = PlayerSelector.matchOnePlayer(sender, p_96332_1_);

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

        public static IChatComponent func_147178_a(ICommandSender sender, string[] p_147178_1_, int p_147178_2_)
		{
			return func_147176_a(sender, p_147178_1_, p_147178_2_, false);
		}

        public static IChatComponent func_147176_a(ICommandSender sender, string[] p_147176_1_, int p_147176_2_, bool p_147176_3_)
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
					IChatComponent var7 = PlayerSelector.func_150869_b(sender, p_147176_1_[var5]);

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

        public static string func_82360_a(ICommandSender sender, string[] p_82360_1_, int p_82360_2_)
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

        public static double func_110666_a(ICommandSender sender, double p_110666_1_, string p_110666_3_)
		{
			return func_110665_a(sender, p_110666_1_, p_110666_3_, -30000000, 30000000);
		}

        public static double func_110665_a(ICommandSender sender, double p_110665_1_, string p_110665_3_, int p_110665_4_, int p_110665_5_)
		{
			bool var6 = p_110665_3_.StartsWith("~");

			if(var6 && double.IsNaN(p_110665_1_))
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

					var7 += ParseDouble(sender, p_110665_3_);

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
        public static Item GetItemByText(ICommandSender sender, string p_147179_1_)
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
						sender.AddChatMessage(var4);
					}

					var2 = var3;
				}
				catch (FormatException)
				{
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
        public static Block GetBlockByText(ICommandSender sender, string p_147180_1_)
		{
			if(Block.blockRegistry.containsKey(p_147180_1_))
			{
				return Block.blockRegistry.GetObject(p_147180_1_);
			}
			else
			{
				try
				{
					int var2 = Convert.ToInt32(p_147180_1_);

					if(Block.blockRegistry.ContainsID(var2))
					{
						Block var3 = Block.getBlockById(var2);
						ChatComponentTranslation var4 = new ChatComponentTranslation("commands.generic.deprecatedId", new object[] {Block.blockRegistry.GetNameForObject(var3)});
						var4.ChatStyle.Color = EnumChatFormatting.GRAY;
						sender.AddChatMessage(var4);
						return var3;
					}
				}
				catch (FormatException var5)
				{
				}

				throw new NumberInvalidException("commands.give.notFound", new object[] {p_147180_1_});
			}
		}

///    
///     <summary> * Creates a linguistic series joining the input objects together.  Examples: 1) {} --> "",  2) {"Steve"} -->
///     * "Steve",  3) {"Steve", "Phil"} --> "Steve and Phil",  4) {"Steve", "Phil", "Mark"} --> "Steve, Phil and Mark" </summary>
///     
		public static string JoinNiceString(object[] p_71527_0_)
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
		public static IChatComponent JoinNiceString(IChatComponent[] p_147177_0_)
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
		public static string JoinNiceStringFromCollection(ICollection p_96333_0_)
		{
            var tmp = new string[p_96333_0_.Count];
            p_96333_0_.CopyTo(tmp, 0);
			return JoinNiceString(tmp);
		}

///    
///     <summary> * Returns true if the given substring is exactly equal to the start of the given string (case insensitive). </summary>
///     
		public static bool DoesStringStartWith(string p_71523_0_, string p_71523_1_)
		{
            return p_71523_0_.ToLower().Equals(p_71523_1_.ToLower().Substring(0, p_71523_0_.Length));
		}

///    
///     <summary> * Returns a List of strings (chosen from the given strings) which the last word in the given string array is a
///     * beginning-match for. (Tab completion). </summary>
///     
		public static IList GetListOfStringsMatchingLastWord(string[] p_71530_0_, params string[] p_71530_1_)
		{
			string var2 = p_71530_0_[p_71530_0_.Length - 1];
			ArrayList var3 = new ArrayList();
			string[] var4 = p_71530_1_;
			int var5 = p_71530_1_.Length;

			for (int var6 = 0; var6 < var5; ++var6)
			{
				string var7 = var4[var6];

				if(DoesStringStartWith(var2, var7))
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
		public static IList GetListOfStringsFromIterableMatchingLastWord(string[] p_71531_0_, IEnumerable p_71531_1_)
		{
			string var2 = p_71531_0_[p_71531_0_.Length - 1];
			ArrayList var3 = new ArrayList();
			IEnumerator var4 = p_71531_1_.GetEnumerator();

			while (var4.MoveNext())
			{
				string var5 = (string)var4.Current;

				if(DoesStringStartWith(var2, var5))
				{
					var3.Add(var5);
				}
			}

			return var3;
		}

///    
///     <summary> * Return whether the specified command parameter index is a username parameter. </summary>
///     
		public virtual bool IsUsernameIndex(string[] p_82358_1_, int p_82358_2_)
		{
			return false;
		}

        public static void func_152373_a(ICommandSender sender, ICommand p_152373_1_, string p_152373_2_, params object[] p_152373_3_)
		{
			func_152374_a(sender, p_152373_1_, 0, p_152373_2_, p_152373_3_);
		}

        public static void func_152374_a(ICommandSender sender, ICommand p_152374_1_, int p_152374_2_, string p_152374_3_, params object[] p_152374_4_)
		{
			if(theAdmin != null)
			{
				theAdmin.func_152372_a(sender, p_152374_1_, p_152374_2_, p_152374_3_, p_152374_4_);
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

		public virtual int CompareTo(ICommand p_compareTo_1_)
		{
			return this.CommandName.CompareTo(p_compareTo_1_.CommandName);
		}
	}
}