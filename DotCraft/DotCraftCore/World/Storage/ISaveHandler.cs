namespace DotCraftCore.World.Storage
{

	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using MinecraftException = DotCraftCore.World.MinecraftException;
	using WorldProvider = DotCraftCore.World.WorldProvider;
	using IChunkLoader = DotCraftCore.World.Chunk.Storage.IChunkLoader;

	public interface ISaveHandler
	{
///    
///     <summary> * Loads and returns the world info </summary>
///     
		WorldInfo loadWorldInfo();

///    
///     <summary> * Checks the session lock to prevent save collisions </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void checkSessionLock() throws MinecraftException;
		void checkSessionLock();

///    
///     <summary> * Returns the chunk loader with the provided world provider </summary>
///     
		IChunkLoader getChunkLoader(WorldProvider p_75763_1_);

///    
///     <summary> * Saves the given World Info with the given NBTTagCompound as the Player. </summary>
///     
		void saveWorldInfoWithPlayer(WorldInfo p_75755_1_, NBTTagCompound p_75755_2_);

///    
///     <summary> * Saves the passed in world info. </summary>
///     
		void saveWorldInfo(WorldInfo p_75761_1_);

///    
///     <summary> * returns null if no saveHandler is relevent (eg. SMP) </summary>
///     
		IPlayerFileData SaveHandler {get;}

///    
///     <summary> * Called to flush all changes to disk, waiting for them to complete. </summary>
///     
		void flush();

///    
///     <summary> * Gets the File object corresponding to the base directory of this world. </summary>
///     
		File WorldDirectory {get;}

///    
///     <summary> * Gets the file location of the given map </summary>
///     
		File getMapFileFromName(string p_75758_1_);

///    
///     <summary> * Returns the name of the directory where world information is saved. </summary>
///     
		string WorldDirectoryName {get;}
	}

}