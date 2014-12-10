namespace DotCraftCore.World
{

	using Entity = DotCraftCore.entity.Entity;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;

	public interface IWorldAccess
	{
///    
///     <summary> * On the client, re-renders the block. On the server, sends the block to the client (which will re-render it),
///     * including the tile entity description packet if applicable. Args: x, y, z </summary>
///     
		void markBlockForUpdate(int p_147586_1_, int p_147586_2_, int p_147586_3_);

///    
///     <summary> * On the client, re-renders this block. On the server, does nothing. Used for lighting updates. </summary>
///     
		void markBlockForRenderUpdate(int p_147588_1_, int p_147588_2_, int p_147588_3_);

///    
///     <summary> * On the client, re-renders all blocks in this range, inclusive. On the server, does nothing. Args: min x, min y,
///     * min z, max x, max y, max z </summary>
///     
		void markBlockRangeForRenderUpdate(int p_147585_1_, int p_147585_2_, int p_147585_3_, int p_147585_4_, int p_147585_5_, int p_147585_6_);

///    
///     <summary> * Plays the specified sound. Arg: soundName, x, y, z, volume, pitch </summary>
///     
		void playSound(string p_72704_1_, double p_72704_2_, double p_72704_4_, double p_72704_6_, float p_72704_8_, float p_72704_9_);

///    
///     <summary> * Plays sound to all near players except the player reference given </summary>
///     
		void playSoundToNearExcept(EntityPlayer p_85102_1_, string p_85102_2_, double p_85102_3_, double p_85102_5_, double p_85102_7_, float p_85102_9_, float p_85102_10_);

///    
///     <summary> * Spawns a particle. Arg: particleType, x, y, z, velX, velY, velZ </summary>
///     
		void spawnParticle(string p_72708_1_, double p_72708_2_, double p_72708_4_, double p_72708_6_, double p_72708_8_, double p_72708_10_, double p_72708_12_);

///    
///     <summary> * Called on all IWorldAccesses when an entity is created or loaded. On client worlds, starts downloading any
///     * necessary textures. On server worlds, adds the entity to the entity tracker. </summary>
///     
		void onEntityCreate(Entity p_72703_1_);

///    
///     <summary> * Called on all IWorldAccesses when an entity is unloaded or destroyed. On client worlds, releases any downloaded
///     * textures. On server worlds, removes the entity from the entity tracker. </summary>
///     
		void onEntityDestroy(Entity p_72709_1_);

///    
///     <summary> * Plays the specified record. Arg: recordName, x, y, z </summary>
///     
		void playRecord(string p_72702_1_, int p_72702_2_, int p_72702_3_, int p_72702_4_);

		void broadcastSound(int p_82746_1_, int p_82746_2_, int p_82746_3_, int p_82746_4_, int p_82746_5_);

///    
///     <summary> * Plays a pre-canned sound effect along with potentially auxiliary data-driven one-shot behaviour (particles, etc). </summary>
///     
		void playAuxSFX(EntityPlayer p_72706_1_, int p_72706_2_, int p_72706_3_, int p_72706_4_, int p_72706_5_, int p_72706_6_);

///    
///     <summary> * Starts (or continues) destroying a block with given ID at the given coordinates for the given partially destroyed
///     * value </summary>
///     
		void destroyBlockPartially(int p_147587_1_, int p_147587_2_, int p_147587_3_, int p_147587_4_, int p_147587_5_);

		void onStaticEntitiesChanged();
	}

}