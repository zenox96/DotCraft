using System;
using System.Collections;

namespace DotCraftCore.nWorld.nChunk
{

	using Block = DotCraftCore.nBlock.Block;
	using IEntitySelector = DotCraftCore.nCommand.IEntitySelector;
	using Entity = DotCraftCore.entity.Entity;
	using Blocks = DotCraftCore.init.Blocks;
	using TileEntity = DotCraftCore.nTileEntity.TileEntity;
	using AxisAlignedBB = DotCraftCore.nUtil.AxisAlignedBB;
	using EnumSkyBlock = DotCraftCore.nWorld.EnumSkyBlock;
	using World = DotCraftCore.nWorld.World;

	public class EmptyChunk : Chunk
	{
		

		public EmptyChunk(World p_i1994_1_, int p_i1994_2_, int p_i1994_3_) : base(p_i1994_1_, p_i1994_2_, p_i1994_3_)
		{
		}

///    
///     <summary> * Checks whether the chunk is at the X/Z location specified </summary>
///     
		public override bool isAtLocation(int p_76600_1_, int p_76600_2_)
		{
			return p_76600_1_ == this.xPosition && p_76600_2_ == this.zPosition;
		}

///    
///     <summary> * Returns the value in the height map at this x, z coordinate in the chunk </summary>
///     
		public override int getHeightValue(int p_76611_1_, int p_76611_2_)
		{
			return 0;
		}

///    
///     <summary> * Generates the height map for a chunk from scratch </summary>
///     
		public override void generateHeightMap()
		{
		}

///    
///     <summary> * Generates the initial skylight map for the chunk upon generation or load. </summary>
///     
		public override void generateSkylightMap()
		{
		}

		public virtual Block func_150810_a(int p_150810_1_, int p_150810_2_, int p_150810_3_)
		{
			return Blocks.air;
		}

		public override int func_150808_b(int p_150808_1_, int p_150808_2_, int p_150808_3_)
		{
			return 255;
		}

		public override bool func_150807_a(int p_150807_1_, int p_150807_2_, int p_150807_3_, Block p_150807_4_, int p_150807_5_)
		{
			return true;
		}

///    
///     <summary> * Return the metadata corresponding to the given coordinates inside a chunk. </summary>
///     
		public override int getBlockMetadata(int p_76628_1_, int p_76628_2_, int p_76628_3_)
		{
			return 0;
		}

///    
///     <summary> * Set the metadata of a block in the chunk </summary>
///     
		public override bool setBlockMetadata(int p_76589_1_, int p_76589_2_, int p_76589_3_, int p_76589_4_)
		{
			return false;
		}

///    
///     <summary> * Gets the amount of light saved in this block (doesn't adjust for daylight) </summary>
///     
		public override int getSavedLightValue(EnumSkyBlock p_76614_1_, int p_76614_2_, int p_76614_3_, int p_76614_4_)
		{
			return 0;
		}

///    
///     <summary> * Sets the light value at the coordinate. If enumskyblock is set to sky it sets it in the skylightmap and if its a
///     * block then into the blocklightmap. Args enumSkyBlock, x, y, z, lightValue </summary>
///     
		public override void setLightValue(EnumSkyBlock p_76633_1_, int p_76633_2_, int p_76633_3_, int p_76633_4_, int p_76633_5_)
		{
		}

///    
///     <summary> * Gets the amount of light on a block taking into account sunlight </summary>
///     
		public override int getBlockLightValue(int p_76629_1_, int p_76629_2_, int p_76629_3_, int p_76629_4_)
		{
			return 0;
		}

///    
///     <summary> * Adds an entity to the chunk. Args: entity </summary>
///     
		public override void addEntity(Entity p_76612_1_)
		{
		}

///    
///     <summary> * removes entity using its y chunk coordinate as its index </summary>
///     
		public override void removeEntity(Entity p_76622_1_)
		{
		}

///    
///     <summary> * Removes entity at the specified index from the entity array. </summary>
///     
		public override void removeEntityAtIndex(Entity p_76608_1_, int p_76608_2_)
		{
		}

///    
///     <summary> * Returns whether is not a block above this one blocking sight to the sky (done via checking against the heightmap) </summary>
///     
		public override bool canBlockSeeTheSky(int p_76619_1_, int p_76619_2_, int p_76619_3_)
		{
			return false;
		}

		public override TileEntity func_150806_e(int p_150806_1_, int p_150806_2_, int p_150806_3_)
		{
			return null;
		}

		public override void addTileEntity(TileEntity p_150813_1_)
		{
		}

		public override void func_150812_a(int p_150812_1_, int p_150812_2_, int p_150812_3_, TileEntity p_150812_4_)
		{
		}

		public override void removeTileEntity(int p_150805_1_, int p_150805_2_, int p_150805_3_)
		{
		}

///    
///     <summary> * Called when this Chunk is loaded by the ChunkProvider </summary>
///     
		public override void onChunkLoad()
		{
		}

///    
///     <summary> * Called when this Chunk is unloaded by the ChunkProvider </summary>
///     
		public override void onChunkUnload()
		{
		}

///    
///     <summary> * Sets the isModified flag for this Chunk </summary>
///     
		public override void setChunkModified()
		{
		}

///    
///     <summary> * Fills the given list of all entities that intersect within the given bounding box that aren't the passed entity
///     * Args: entity, aabb, listToFill </summary>
///     
		public override void getEntitiesWithinAABBForEntity(Entity p_76588_1_, AxisAlignedBB p_76588_2_, IList p_76588_3_, IEntitySelector p_76588_4_)
		{
		}

///    
///     <summary> * Gets all entities that can be assigned to the specified class. Args: entityClass, aabb, listToFill </summary>
///     
		public override void getEntitiesOfTypeWithinAAAB(Type p_76618_1_, AxisAlignedBB p_76618_2_, IList p_76618_3_, IEntitySelector p_76618_4_)
		{
		}

///    
///     <summary> * Returns true if this Chunk needs to be saved </summary>
///     
		public override bool needsSaving(bool p_76601_1_)
		{
			return false;
		}

		public override Random getRandomWithSeed(long p_76617_1_)
		{
			return new Random(this.worldObj.Seed + (long)(this.xPosition * this.xPosition * 4987142) + (long)(this.xPosition * 5947611) + (long)(this.zPosition * this.zPosition) * 4392871L + (long)(this.zPosition * 389711) ^ p_76617_1_);
		}

		public override bool isEmpty()
		{
			get
			{
				return true;
			}
		}

///    
///     <summary> * Returns whether the ExtendedBlockStorages containing levels (in blocks) from arg 1 to arg 2 are fully empty
///     * (true) or not (false). </summary>
///     
		public override bool getAreLevelsEmpty(int p_76606_1_, int p_76606_2_)
		{
			return true;
		}
	}

}