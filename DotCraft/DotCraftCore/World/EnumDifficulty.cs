namespace DotCraftCore.World
{

	public enum EnumDifficulty
	{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		PEACEFUL(0, "options.difficulty.peaceful"),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		EASY(1, "options.difficulty.easy"),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		NORMAL(2, "options.difficulty.normal"),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		HARD(3, "options.difficulty.hard");
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private static final EnumDifficulty[] difficultyEnums = new EnumDifficulty[values().length];
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final int difficultyId;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final String difficultyResourceKey;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		

//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		private EnumDifficulty(int p_i45312_3_, String p_i45312_4_)
//	{
//		this.difficultyId = p_i45312_3_;
//		this.difficultyResourceKey = p_i45312_4_;
//	}


//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		public static EnumDifficulty getDifficultyEnum(int p_151523_0_)
//	{
//		return difficultyEnums[p_151523_0_ % difficultyEnums.length];
//	}


//JAVA TO VB & C# CONVERTER NOTE: This static initializer block is converted to a static constructor, but there is no current class:
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		static ImpliedClass()
//	{
//		EnumDifficulty[] var0 = values();
//		int var1 = var0.length;
//
//		for (int var2 = 0; var2 < var1; ++var2)
//		{
//			EnumDifficulty var3 = var0[var2];
//			difficultyEnums[var3.difficultyId] = var3;
//		}
//	}
	}
	public static partial class EnumExtensionMethods
	{
			public int getDifficultyId(this EnumDifficulty instanceJavaToDotNetTempPropertyGetDifficultyId)
		{
			return instance.difficultyId;
		}
			public string getDifficultyResourceKey(this EnumDifficulty instanceJavaToDotNetTempPropertyGetDifficultyResourceKey)
		{
			return instance.difficultyResourceKey;
		}
	}

}