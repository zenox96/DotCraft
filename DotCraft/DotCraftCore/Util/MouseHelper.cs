namespace DotCraftCore.Util
{

	using Mouse = org.lwjgl.input.Mouse;
	using Display = org.lwjgl.opengl.Display;

	public class MouseHelper
	{
	/// <summary> Mouse delta X this frame  </summary>
		public int deltaX;

	/// <summary> Mouse delta Y this frame  </summary>
		public int deltaY;
		private const string __OBFID = "CL_00000648";

///    
///     <summary> * Grabs the mouse cursor it doesn't move and isn't seen. </summary>
///     
		public virtual void grabMouseCursor()
		{
			Mouse.Grabbed = true;
			this.deltaX = 0;
			this.deltaY = 0;
		}

///    
///     <summary> * Ungrabs the mouse cursor so it can be moved and set it to the center of the screen </summary>
///     
		public virtual void ungrabMouseCursor()
		{
			Mouse.setCursorPosition(Display.Width / 2, Display.Height / 2);
			Mouse.Grabbed = false;
		}

		public virtual void mouseXYChange()
		{
			this.deltaX = Mouse.DX;
			this.deltaY = Mouse.DY;
		}
	}

}