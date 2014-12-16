namespace DotCraftCore.Util
{

	public class Direction
	{
		public static readonly int[] offsetX = new int[] {0, -1, 0, 1};
		public static readonly int[] offsetZ = new int[] {1, 0, -1, 0};
		public static readonly string[] directions = new string[] {"SOUTH", "WEST", "NORTH", "EAST"};

	/// <summary> Maps a Direction value (2D) to a Facing value (3D).  </summary>
		public static readonly int[] directionToFacing = new int[] {3, 4, 2, 5};

	/// <summary> Maps a Facing value (3D) to a Direction value (2D).  </summary>
		public static readonly int[] facingToDirection = new int[] { -1, -1, 2, 0, 1, 3};

	/// <summary> Maps a direction to that opposite of it.  </summary>
		public static readonly int[] rotateOpposite = new int[] {2, 3, 0, 1};

	/// <summary> Maps a direction to that to the right of it.  </summary>
		public static readonly int[] rotateRight = new int[] {1, 2, 3, 0};

	/// <summary> Maps a direction to that to the left of it.  </summary>
		public static readonly int[] rotateLeft = new int[] {3, 0, 1, 2};
		public static readonly int[][] bedDirection = new int[][] {{1, 0, 3, 2, 5, 4}, {1, 0, 5, 4, 2, 3}, {1, 0, 2, 3, 4, 5}, {1, 0, 4, 5, 3, 2}};
		

///    
///     <summary> * Returns the movement direction from a velocity vector. </summary>
///     
		public static int getMovementDirection(double p_82372_0_, double p_82372_2_)
		{
			return MathHelper.abs((float)p_82372_0_) > MathHelper.abs((float)p_82372_2_) ? (p_82372_0_ > 0.0D ? 1 : 3) : (p_82372_2_ > 0.0D ? 2 : 0);
		}
	}

}