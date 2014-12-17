namespace DotCraftCore.nWorld
{

	using Block = DotCraftCore.nBlock.Block;
	using TileEntity = DotCraftCore.nTileEntity.TileEntity;
	using BiomeGenBase = DotCraftCore.nWorld.nBiome.BiomeGenBase;

	public interface IBlockAccess
	{
		Block getBlock(int p_147439_1_, int p_147439_2_, int p_147439_3_);

		TileEntity getTileEntity(int p_147438_1_, int p_147438_2_, int p_147438_3_);

///    
///     <summary> * Any Light rendered on a 1.8 Block goes through here </summary>
///     
		int getLightBrightnessForSkyBlocks(int p_72802_1_, int p_72802_2_, int p_72802_3_, int p_72802_4_);

///    
///     <summary> * Returns the block metadata at coords x,y,z </summary>
///     
		int getBlockMetadata(int p_72805_1_, int p_72805_2_, int p_72805_3_);

///    
///     <summary> * Returns true if the block at the specified coordinates is empty </summary>
///     
		bool isAirBlock(int p_147437_1_, int p_147437_2_, int p_147437_3_);

///    
///     <summary> * Gets the biome for a given set of x/z coordinates </summary>
///     
		BiomeGenBase getBiomeGenForCoords(int p_72807_1_, int p_72807_2_);

///    
///     <summary> * Returns current world height. </summary>
///     
		int Height {get;}

///    
///     <summary> * set by !chunk.getAreLevelsEmpty </summary>
///     
		bool extendedLevelsInChunkCache();

///    
///     <summary> * Is this block powering in the specified direction Args: x, y, z, direction </summary>
///     
		int isBlockProvidingPowerTo(int p_72879_1_, int p_72879_2_, int p_72879_3_, int p_72879_4_);
	}

}