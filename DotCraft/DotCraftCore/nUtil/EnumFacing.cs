using DotCraftCore.nUtil;

namespace DotCraftCore.nUtil
{
	public enum EnumFacing
	{
		DOWN = 0,
		UP = 1,
		NORTH = 2,
		SOUTH = 3,
		EAST = 4,
		WEST = 5
    }

	public static class EnumFacingExtensionMethods
	{
    /// <summary> Face order for D-U-N-S-E-W.  </summary>
		private static readonly int[] order_a = {0,1,2,3,4,5};
	/// <summary> Face order for U-D-S-N-W-E.  </summary>
		private static readonly int[] order_b = {1,0,3,2,5,4};
		private static readonly int[] frontOffsetX = {0,0,0,0,-1,1};
		private static readonly int[] frontOffsetY = {-1,1,0,0,0,0};
		private static readonly int[] frontOffsetZ = {0,0,-1,1,0,0};
    /// <summary> List of all values in EnumFacing. Order is D-U-N-S-E-W.  </summary>
		private static readonly EnumFacing[] faceList = new EnumFacing[] {EnumFacing.DOWN, EnumFacing.UP, EnumFacing.NORTH, EnumFacing.SOUTH, EnumFacing.EAST, EnumFacing.WEST};

		public static int getFrontOffsetX(this EnumFacing instance)
		{
			return frontOffsetX[(int)instance];
		}

        public static int getFrontOffsetY(this EnumFacing instance)
		{
            return frontOffsetY[(int)instance];
		}

		public static int getFrontOffsetZ(this EnumFacing instance)
		{
            return frontOffsetZ[(int)instance];
		}

        public static EnumFacing getFront(int p_82600_0_)
        {
        	return faceList[p_82600_0_ % faceList.Length];
        }
	}
}