namespace DotCraftCore.nUtil
{

	public interface IProgressUpdate
	{
///    
///     <summary> * "Saving level", or the loading,or downloading equivelent </summary>
///     
		void displayProgressMessage(string p_73720_1_);

///    
///     <summary> * this string, followed by "working..." and then the "% complete" are the 3 lines shown. This resets progress to 0,
///     * and the WorkingString to "working...". </summary>
///     
		void resetProgressAndMessage(string p_73721_1_);

///    
///     <summary> * This is called with "Working..." by resetProgressAndMessage </summary>
///     
		void resetProgresAndWorkingMessage(string p_73719_1_);

///    
///     <summary> * Updates the progress bar on the loading screen to the specified amount. Args: loadProgress </summary>
///     
		int LoadingProgress {set;}

		void func_146586_a();
	}

}