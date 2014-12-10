using System.Collections;

namespace DotCraftCore.World.Storage
{

	using AnvilConverterException = DotCraftCore.client.AnvilConverterException;
	using IProgressUpdate = DotCraftCore.Util.IProgressUpdate;

	public interface ISaveFormat
	{
		string func_154333_a();

///    
///     <summary> * Returns back a loader for the specified save directory </summary>
///     
		ISaveHandler getSaveLoader(string p_75804_1_, bool p_75804_2_);

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: List getSaveList() throws AnvilConverterException;
		IList SaveList {get;}

		void flushCache();

///    
///     <summary> * gets the world info </summary>
///     
		WorldInfo getWorldInfo(string p_75803_1_);

		bool func_154335_d(string p_154335_1_);

///    
///     <summary> * @args: Takes one argument - the name of the directory of the world to delete. @desc: Delete the world by deleting
///     * the associated directory recursively. </summary>
///     
		bool deleteWorldDirectory(string p_75802_1_);

///    
///     <summary> * @args: Takes two arguments - first the name of the directory containing the world and second the new name for
///     * that world. @desc: Renames the world by storing the new name in level.dat. It does *not* rename the directory
///     * containing the world data. </summary>
///     
		void renameWorld(string p_75806_1_, string p_75806_2_);

		bool func_154334_a(string p_154334_1_);

///    
///     <summary> * Checks if the save directory uses the old map format </summary>
///     
		bool isOldMapFormat(string p_75801_1_);

///    
///     <summary> * Converts the specified map to the new map format. Args: worldName, loadingScreen </summary>
///     
		bool convertMapFormat(string p_75805_1_, IProgressUpdate p_75805_2_);

///    
///     <summary> * Return whether the given world can be loaded. </summary>
///     
		bool canLoadWorld(string p_90033_1_);
	}

}