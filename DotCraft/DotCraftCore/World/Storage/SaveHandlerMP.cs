namespace DotCraftCore.nWorld.nStorage
{

	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using MinecraftException = DotCraftCore.nWorld.MinecraftException;
	using WorldProvider = DotCraftCore.nWorld.WorldProvider;
	using IChunkLoader = DotCraftCore.nWorld.nChunk.nStorage.IChunkLoader;

	public class SaveHandlerMP : ISaveHandler
	{
		

///    
///     <summary> * Loads and returns the world info </summary>
///     
		public virtual WorldInfo loadWorldInfo()
		{
			return null;
		}

///    
///     <summary> * Checks the session lock to prevent save collisions </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void checkSessionLock() throws MinecraftException
		public virtual void checkSessionLock()
		{
		}

///    
///     <summary> * Returns the chunk loader with the provided world provider </summary>
///     
		public virtual IChunkLoader getChunkLoader(WorldProvider p_75763_1_)
		{
			return null;
		}

///    
///     <summary> * Saves the given World Info with the given NBTTagCompound as the Player. </summary>
///     
		public virtual void saveWorldInfoWithPlayer(WorldInfo p_75755_1_, NBTTagCompound p_75755_2_)
		{
		}

///    
///     <summary> * Saves the passed in world info. </summary>
///     
		public virtual void saveWorldInfo(WorldInfo p_75761_1_)
		{
		}

///    
///     <summary> * returns null if no saveHandler is relevent (eg. SMP) </summary>
///     
		public virtual IPlayerFileData SaveHandler
		{
			get
			{
				return null;
			}
		}

///    
///     <summary> * Called to flush all changes to disk, waiting for them to complete. </summary>
///     
		public virtual void flush()
		{
		}

///    
///     <summary> * Gets the file location of the given map </summary>
///     
		public virtual File getMapFileFromName(string p_75758_1_)
		{
			return null;
		}

///    
///     <summary> * Returns the name of the directory where world information is saved. </summary>
///     
		public virtual string WorldDirectoryName
		{
			get
			{
				return "none";
			}
		}

///    
///     <summary> * Gets the File object corresponding to the base directory of this world. </summary>
///     
		public virtual File WorldDirectory
		{
			get
			{
				return null;
			}
		}
	}

}