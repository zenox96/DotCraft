using System.Collections;

namespace DotCraftCore.nWorld.nChunk
{

	using EnumCreatureType = DotCraftCore.entity.EnumCreatureType;
	using IProgressUpdate = DotCraftCore.nUtil.IProgressUpdate;
	using ChunkPosition = DotCraftCore.nWorld.ChunkPosition;
	using World = DotCraftCore.nWorld.World;

	public interface IChunkProvider
	{
///    
///     <summary> * Checks to see if a chunk exists at x, y </summary>
///     
		bool chunkExists(int p_73149_1_, int p_73149_2_);

///    
///     <summary> * Will return back a chunk, if it doesn't exist and its not a MP client it will generates all the blocks for the
///     * specified chunk from the map seed and chunk seed </summary>
///     
		Chunk provideChunk(int p_73154_1_, int p_73154_2_);

///    
///     <summary> * loads or generates the chunk at the chunk location specified </summary>
///     
		Chunk loadChunk(int p_73158_1_, int p_73158_2_);

///    
///     <summary> * Populates chunk with ores etc etc </summary>
///     
		void populate(IChunkProvider p_73153_1_, int p_73153_2_, int p_73153_3_);

///    
///     <summary> * Two modes of operation: if passed true, save all Chunks in one go.  If passed false, save up to two chunks.
///     * Return true if all chunks have been saved. </summary>
///     
		bool saveChunks(bool p_73151_1_, IProgressUpdate p_73151_2_);

///    
///     <summary> * Unloads chunks that are marked to be unloaded. This is not guaranteed to unload every such chunk. </summary>
///     
		bool unloadQueuedChunks();

///    
///     <summary> * Returns if the IChunkProvider supports saving. </summary>
///     
		bool canSave();

///    
///     <summary> * Converts the instance data to a readable string. </summary>
///     
		string makeString();

///    
///     <summary> * Returns a list of creatures of the specified type that can spawn at the given location. </summary>
///     
		IList getPossibleCreatures(EnumCreatureType p_73155_1_, int p_73155_2_, int p_73155_3_, int p_73155_4_);

		ChunkPosition func_147416_a(World p_147416_1_, string p_147416_2_, int p_147416_3_, int p_147416_4_, int p_147416_5_);

		int LoadedChunkCount {get;}

		void recreateStructures(int p_82695_1_, int p_82695_2_);

///    
///     <summary> * Save extra data not associated with any Chunk.  Not saved during autosave, only during world unload.  Currently
///     * unimplemented. </summary>
///     
		void saveExtraData();
	}

}