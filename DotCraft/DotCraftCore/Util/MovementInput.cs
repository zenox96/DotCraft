namespace DotCraftCore.Util
{

	public class MovementInput
	{
///    
///     <summary> * The speed at which the player is strafing. Postive numbers to the left and negative to the right. </summary>
///     
		public float moveStrafe;

///    
///     <summary> * The speed at which the player is moving forward. Negative numbers will move backwards. </summary>
///     
		public float moveForward;
		public bool jump;
		public bool sneak;
		private const string __OBFID = "CL_00000936";

		public virtual void updatePlayerMoveState()
		{
		}
	}

}