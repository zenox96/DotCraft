using System.Text;

namespace DotCraftCore.nUtil
{

	public class ChatAllowedCharacters
	{
///    
///     <summary> * Array of the special characters that are allowed in any text drawing of Minecraft. </summary>
///     
		public static readonly char[] allowedCharacters = new char[] {'/', '\n', '\r', '\t', '\u0000', '\f', '`', '?', '*', '\\', '<', '>', '|', '\"', ':'};
		

		public static bool isAllowedCharacter(char p_71566_0_)
		{
			return p_71566_0_ != 167 && p_71566_0_ >= 32 && p_71566_0_ != 127;
		}

///    
///     <summary> * Filter string by only keeping those characters for which isAllowedCharacter() returns true. </summary>
///     
		public static string filerAllowedCharacters(string p_71565_0_)
		{
			StringBuilder var1 = new StringBuilder();
			char[] var2 = p_71565_0_.ToCharArray();
			int var3 = var2.Length;

			for(int var4 = 0; var4 < var3; ++var4)
			{
				char var5 = var2[var4];

				if(isAllowedCharacter(var5))
				{
					var1.Append(var5);
				}
			}

			return var1.ToString();
		}
	}

}