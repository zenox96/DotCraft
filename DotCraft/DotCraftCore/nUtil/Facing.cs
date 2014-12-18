namespace DotCraftCore.nUtil
{

	public class Facing
	{
///    
///     <summary> * Converts a side to the opposite side. This is the same as XOR'ing it with 1. </summary>
///     
		public static readonly int[] oppositeSide = new int[] {1, 0, 3, 2, 5, 4};

///    
///     <summary> * gives the offset required for this axis to get the block at that side. </summary>
///     
		public static readonly int[] offsetsXForSide = new int[] {0, 0, 0, 0, -1, 1};

///    
///     <summary> * gives the offset required for this axis to get the block at that side. </summary>
///     
		public static readonly int[] offsetsYForSide = new int[] { -1, 1, 0, 0, 0, 0};

///    
///     <summary> * gives the offset required for this axis to get the block at that side. </summary>
///     
		public static readonly int[] offsetsZForSide = new int[] {0, 0, -1, 1, 0, 0};
		public static readonly string[] facings = new string[] {"DOWN", "UP", "NORTH", "SOUTH", "WEST", "EAST"};
		
	}

}