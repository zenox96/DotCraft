using System;

namespace DotCraftCore.nUtil
{

	public class ChatComponentTranslationFormatException : System.ArgumentException
	{
		

		public ChatComponentTranslationFormatException(ChatComponentTranslation p_i45161_1_, string p_i45161_2_) : base(string.format("Error parsing: %s: %s", new object[] {p_i45161_1_, p_i45161_2_}))
		{
		}

		public ChatComponentTranslationFormatException(ChatComponentTranslation p_i45162_1_, int p_i45162_2_) : base(string.format("Invalid index %d requested for %s", new object[] {int.valueOf(p_i45162_2_), p_i45162_1_}))
		{
		}

		public ChatComponentTranslationFormatException(ChatComponentTranslation p_i45163_1_, Exception p_i45163_2_) : base(string.format("Error while parsing: %s", new object[] {p_i45163_1_}), p_i45163_2_)
		{
		}
	}

}