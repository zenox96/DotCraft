namespace DotCraftCore.Util
{

	using GameSettings = DotCraftCore.client.settings.GameSettings;

	public class MovementInputFromOptions : MovementInput
	{
		private GameSettings gameSettings;
		private const string __OBFID = "CL_00000937";

		public MovementInputFromOptions(GameSettings p_i1237_1_)
		{
			this.gameSettings = p_i1237_1_;
		}

		public override void updatePlayerMoveState()
		{
			this.moveStrafe = 0.0F;
			this.moveForward = 0.0F;

			if(this.gameSettings.keyBindForward.IsKeyPressed)
			{
				++this.moveForward;
			}

			if(this.gameSettings.keyBindBack.IsKeyPressed)
			{
				--this.moveForward;
			}

			if(this.gameSettings.keyBindLeft.IsKeyPressed)
			{
				++this.moveStrafe;
			}

			if(this.gameSettings.keyBindRight.IsKeyPressed)
			{
				--this.moveStrafe;
			}

			this.jump = this.gameSettings.keyBindJump.IsKeyPressed;
			this.sneak = this.gameSettings.keyBindSneak.IsKeyPressed;

			if(this.sneak)
			{
				this.moveStrafe = (float)((double)this.moveStrafe * 0.3D);
				this.moveForward = (float)((double)this.moveForward * 0.3D);
			}
		}
	}

}