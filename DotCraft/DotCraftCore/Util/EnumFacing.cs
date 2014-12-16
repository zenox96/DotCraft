namespace DotCraftCore.Util
{

	public enum EnumFacing
	{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		DOWN(0, 1, 0, -1, 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		UP(1, 0, 0, 1, 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		NORTH(2, 3, 0, 0, -1),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		SOUTH(3, 2, 0, 0, 1),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		EAST(4, 5, -1, 0, 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
		WEST(5, 4, 1, 0, 0);

	/// <summary> Face order for D-U-N-S-E-W.  </summary>
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final int order_a;

	/// <summary> Face order for U-D-S-N-W-E.  </summary>
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final int order_b;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final int frontOffsetX;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final int frontOffsetY;
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private final int frontOffsetZ;

	/// <summary> List of all values in EnumFacing. Order is D-U-N-S-E-W.  </summary>
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		private static final EnumFacing[] faceList = new EnumFacing[6];
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain fields in .NET:
//		

//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		private EnumFacing(int p_i1367_3_, int p_i1367_4_, int p_i1367_5_, int p_i1367_6_, int p_i1367_7_)
//	{
//		this.order_a = p_i1367_3_;
//		this.order_b = p_i1367_4_;
//		this.frontOffsetX = p_i1367_5_;
//		this.frontOffsetY = p_i1367_6_;
//		this.frontOffsetZ = p_i1367_7_;
//	}

///    
///     <summary> * Returns a offset that addresses the block in front of this facing. </summary>
///     


///    
///     <summary> * Returns a offset that addresses the block in front of this facing. </summary>
///     

///    
///     <summary> * Returns the facing that represents the block in front of it. </summary>
///     
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		public static EnumFacing getFront(int p_82600_0_)
//	{
//		return faceList[p_82600_0_ % faceList.length];
//	}

//JAVA TO VB & C# CONVERTER NOTE: This static initializer block is converted to a static constructor, but there is no current class:
//JAVA TO VB & C# CONVERTER TODO TASK: Enums cannot contain methods in .NET:
//		static ImpliedClass()
//	{
//		EnumFacing[] var0 = values();
//		int var1 = var0.length;
//
//		for (int var2 = 0; var2 < var1; ++var2)
//		{
//			EnumFacing var3 = var0[var2];
//			faceList[var3.order_a] = var3;
//		}
//	}
	}
	public static partial class EnumExtensionMethods
	{
			public int getFrontOffsetX(this EnumFacing instanceJavaToDotNetTempPropertyGetFrontOffsetX)
		{
			return instance.frontOffsetX;
		}
			public int getFrontOffsetY(this EnumFacing instanceJavaToDotNetTempPropertyGetFrontOffsetY)
		{
			return instance.frontOffsetY;
		}
			public int getFrontOffsetZ(this EnumFacing instanceJavaToDotNetTempPropertyGetFrontOffsetZ)
		{
			return instance.frontOffsetZ;
		}
	}

}