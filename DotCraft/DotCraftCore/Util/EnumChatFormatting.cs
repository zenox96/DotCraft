namespace DotCraftCore.Util
{


	public enum EnumChatFormatting
	{
		BLACK = '0',
		DARK_BLUE = '1',
		DARK_GREEN = '2',
		DARK_AQUA = '3',
		DARK_RED = '4',
		DARK_PURPLE = '5',
		GOLD = '6',
		GRAY = '7',
		DARK_GRAY = '8',
		BLUE = '9',
		GREEN = 'a',
		AQUA = 'b',
		RED = 'c',
		LIGHT_PURPLE = 'd',
		YELLOW = 'e',
		WHITE = 'f',
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		OBFUSCATED('k', true),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		BOLD('l', true),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		STRIKETHROUGH('m', true),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		UNDERLINE('n', true),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		ITALIC('o', true),
		RESET = 'r'

///    
///     <summary> * Maps a formatting code (e.g., 'f') to its corresponding enum value (e.g., WHITE). </summary>
///     
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private static final Map formattingCodeMapping = new HashMap();

///    
///     <summary> * Maps a name (e.g., 'underline') to its corresponding enum value (e.g., UNDERLINE). </summary>
///     
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private static final Map nameMapping = new HashMap();

///    
///     <summary> * Matches formatting codes that indicate that the client should treat the following text as bold, recolored,
///     * obfuscated, etc. </summary>
///     
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values cannot be strings in .NET:
		private static final Pattern formattingCodePattern = Pattern.compile("(?i)" + String.valueOf('\u00a7') + "[0-9A-FK-OR]");

	/// <summary> The formatting code that produces this format.  </summary>
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final char formattingCode;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final boolean fancyStyling;

///    
///     <summary> * The control string (section sign + formatting code) that can be inserted into client-side text to display
///     * subsequent text in this format. </summary>
///     
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final String controlString;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		

//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		private EnumChatFormatting(char p_i1336_3_)
//	{
//		this(p_i1336_3_, false);
//	}

//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		private EnumChatFormatting(char p_i1337_3_, boolean p_i1337_4_)
//	{
//		this.formattingCode = p_i1337_3_;
//		this.fancyStyling = p_i1337_4_;
//		this.controlString = "\u00a7" + p_i1337_3_;
//	}

///    
///     <summary> * Gets the formatting code that produces this format. </summary>
///     

///    
///     <summary> * False if this is just changing the color or resetting; true otherwise. </summary>
///     

///    
///     <summary> * Checks if typo is a color. </summary>
///     

///    
///     <summary> * Gets the friendly name of this value. </summary>
///     


///    
///     <summary> * Returns a copy of the given string, with formatting codes stripped away. </summary>
///     
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		public static String getTextWithoutFormattingCodes(String p_110646_0_)
//	{
//		return p_110646_0_ == null ? null : formattingCodePattern.matcher(p_110646_0_).replaceAll("");
//	}

///    
///     <summary> * Gets a value by its friendly name; null if the given name does not map to a defined value. </summary>
///     
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		public static EnumChatFormatting getValueByName(String p_96300_0_)
//	{
//		return p_96300_0_ == null ? null : (EnumChatFormatting)nameMapping.get(p_96300_0_.toLowerCase());
//	}

///     </param>
///     * Gets all the valid values. Args: <param name="par0">: Whether or not to include color values. <param name="par1">: Whether or not
///     * to include fancy-styling values (anything that isn't a color value or the "reset" value). </param>
///     
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		public static Collection getValidValues(boolean p_96296_0_, boolean p_96296_1_)
//	{
//		ArrayList var2 = new ArrayList();
//		EnumChatFormatting[] var3 = values();
//		int var4 = var3.length;
//
//		for (int var5 = 0; var5 < var4; ++var5)
//		{
//			EnumChatFormatting var6 = var3[var5];
//
//			if ((!var6.isColor() || p_96296_0_) && (!var6.isFancyStyling() || p_96296_1_))
//			{
//				var2.add(var6.getFriendlyName());
//			}
//		}
//
//		return var2;
//	}

//JAVA TO VB & C# CONVERTER NOTE: This static initializer block is converted to a static constructor, but there is no current class:
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		static ImpliedClass()
//	{
//		EnumChatFormatting[] var0 = values();
//		int var1 = var0.length;
//
//		for (int var2 = 0; var2 < var1; ++var2)
//		{
//			EnumChatFormatting var3 = var0[var2];
//			formattingCodeMapping.put(Character.valueOf(var3.getFormattingCode()), var3);
//			nameMapping.put(var3.getFriendlyName(), var3);
//		}
//	}
	}
	public static partial class EnumExtensionMethods
	{
			public char getFormattingCode(this EnumChatFormatting instanceJavaToDotNetTempPropertyGetFormattingCode)
		{
			return instance.formattingCode;
		}
			public bool isFancyStyling(this EnumChatFormatting instanceJavaToDotNetTempPropertyGetFancyStyling)
		{
			return instance.fancyStyling;
		}
			public bool isColor(this EnumChatFormatting instanceJavaToDotNetTempPropertyGetColor)
		{
			return !instance.fancyStyling && instance != RESET;
		}
			public string getFriendlyName(this EnumChatFormatting instanceJavaToDotNetTempPropertyGetFriendlyName)
		{
			return instance.name().ToLower();
		}
			public override string ToString(this EnumChatFormatting instance)
		{
			return instance.controlString;
		}
	}

}