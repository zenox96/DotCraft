namespace DotCraftCore.nWorld.nChunk.nStorage
{

	using MinecraftException = DotCraftCore.nWorld.MinecraftException;
	using World = DotCraftCore.nWorld.World;
	using Chunk = DotCraftCore.nWorld.nChunk.Chunk;

	public interface IChunkLoader
	{
///    
///     <summary> * Loads the specified(XZ) chunk into the specified world. </summary>
///     
//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: Chunk loadChunk(World p_75815_1_, int p_75815_2_, int p_75815_3_) throws IOException;
		Chunk loadChunk(World p_75815_1_, int p_75815_2_, int p_75815_3_);

//JAVA TO VB & C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: void saveChunk(World p_75816_1_, Chunk p_75816_2_) throws MinecraftException, IOException;
		void saveChunk(World p_75816_1_, Chunk p_75816_2_);

///    
///     <summary> * Save extra data associated with this Chunk not normally saved during autosave, only during chunk unload.
///     * Currently unused. </summary>
///     
		void saveExtraChunkData(World p_75819_1_, Chunk p_75819_2_);

///    
///     <summary> * Called every World.tick() </summary>
///     
		void chunkTick();

///    
///     <summary> * Save extra data not associated with any Chunk.  Not saved during autosave, only during world unload.  Currently
///     * unused. </summary>
///     
		void saveExtraData();
	}

}