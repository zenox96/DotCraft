using System.Collections;

namespace DotCraftCore.World
{

	using Entity = DotCraftCore.entity.Entity;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using EntityPlayerMP = DotCraftCore.entity.player.EntityPlayerMP;
	using S25PacketBlockBreakAnim = DotCraftCore.network.play.server.S25PacketBlockBreakAnim;
	using S28PacketEffect = DotCraftCore.network.play.server.S28PacketEffect;
	using S29PacketSoundEffect = DotCraftCore.network.play.server.S29PacketSoundEffect;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;

	public class WorldManager : IWorldAccess
	{
	/// <summary> Reference to the MinecraftServer object.  </summary>
		private MinecraftServer mcServer;

	/// <summary> The WorldServer object.  </summary>
		private WorldServer theWorldServer;
		

		public WorldManager(MinecraftServer p_i1517_1_, WorldServer p_i1517_2_)
		{
			this.mcServer = p_i1517_1_;
			this.theWorldServer = p_i1517_2_;
		}

///    
///     <summary> * Spawns a particle. Arg: particleType, x, y, z, velX, velY, velZ </summary>
///     
		public virtual void spawnParticle(string p_72708_1_, double p_72708_2_, double p_72708_4_, double p_72708_6_, double p_72708_8_, double p_72708_10_, double p_72708_12_)
		{
		}

///    
///     <summary> * Called on all IWorldAccesses when an entity is created or loaded. On client worlds, starts downloading any
///     * necessary textures. On server worlds, adds the entity to the entity tracker. </summary>
///     
		public virtual void onEntityCreate(Entity p_72703_1_)
		{
			this.theWorldServer.EntityTracker.addEntityToTracker(p_72703_1_);
		}

///    
///     <summary> * Called on all IWorldAccesses when an entity is unloaded or destroyed. On client worlds, releases any downloaded
///     * textures. On server worlds, removes the entity from the entity tracker. </summary>
///     
		public virtual void onEntityDestroy(Entity p_72709_1_)
		{
			this.theWorldServer.EntityTracker.removeEntityFromAllTrackingPlayers(p_72709_1_);
		}

///    
///     <summary> * Plays the specified sound. Arg: soundName, x, y, z, volume, pitch </summary>
///     
		public virtual void playSound(string p_72704_1_, double p_72704_2_, double p_72704_4_, double p_72704_6_, float p_72704_8_, float p_72704_9_)
		{
			this.mcServer.ConfigurationManager.func_148541_a(p_72704_2_, p_72704_4_, p_72704_6_, p_72704_8_ > 1.0F ? (double)(16.0F * p_72704_8_) : 16.0D, this.theWorldServer.provider.dimensionId, new S29PacketSoundEffect(p_72704_1_, p_72704_2_, p_72704_4_, p_72704_6_, p_72704_8_, p_72704_9_));
		}

///    
///     <summary> * Plays sound to all near players except the player reference given </summary>
///     
		public virtual void playSoundToNearExcept(EntityPlayer p_85102_1_, string p_85102_2_, double p_85102_3_, double p_85102_5_, double p_85102_7_, float p_85102_9_, float p_85102_10_)
		{
			this.mcServer.ConfigurationManager.func_148543_a(p_85102_1_, p_85102_3_, p_85102_5_, p_85102_7_, p_85102_9_ > 1.0F ? (double)(16.0F * p_85102_9_) : 16.0D, this.theWorldServer.provider.dimensionId, new S29PacketSoundEffect(p_85102_2_, p_85102_3_, p_85102_5_, p_85102_7_, p_85102_9_, p_85102_10_));
		}

///    
///     <summary> * On the client, re-renders all blocks in this range, inclusive. On the server, does nothing. Args: min x, min y,
///     * min z, max x, max y, max z </summary>
///     
		public virtual void markBlockRangeForRenderUpdate(int p_147585_1_, int p_147585_2_, int p_147585_3_, int p_147585_4_, int p_147585_5_, int p_147585_6_)
		{
		}

///    
///     <summary> * On the client, re-renders the block. On the server, sends the block to the client (which will re-render it),
///     * including the tile entity description packet if applicable. Args: x, y, z </summary>
///     
		public virtual void markBlockForUpdate(int p_147586_1_, int p_147586_2_, int p_147586_3_)
		{
			this.theWorldServer.PlayerManager.func_151250_a(p_147586_1_, p_147586_2_, p_147586_3_);
		}

///    
///     <summary> * On the client, re-renders this block. On the server, does nothing. Used for lighting updates. </summary>
///     
		public virtual void markBlockForRenderUpdate(int p_147588_1_, int p_147588_2_, int p_147588_3_)
		{
		}

///    
///     <summary> * Plays the specified record. Arg: recordName, x, y, z </summary>
///     
		public virtual void playRecord(string p_72702_1_, int p_72702_2_, int p_72702_3_, int p_72702_4_)
		{
		}

///    
///     <summary> * Plays a pre-canned sound effect along with potentially auxiliary data-driven one-shot behaviour (particles, etc). </summary>
///     
		public virtual void playAuxSFX(EntityPlayer p_72706_1_, int p_72706_2_, int p_72706_3_, int p_72706_4_, int p_72706_5_, int p_72706_6_)
		{
			this.mcServer.ConfigurationManager.func_148543_a(p_72706_1_, (double)p_72706_3_, (double)p_72706_4_, (double)p_72706_5_, 64.0D, this.theWorldServer.provider.dimensionId, new S28PacketEffect(p_72706_2_, p_72706_3_, p_72706_4_, p_72706_5_, p_72706_6_, false));
		}

		public virtual void broadcastSound(int p_82746_1_, int p_82746_2_, int p_82746_3_, int p_82746_4_, int p_82746_5_)
		{
			this.mcServer.ConfigurationManager.func_148540_a(new S28PacketEffect(p_82746_1_, p_82746_2_, p_82746_3_, p_82746_4_, p_82746_5_, true));
		}

///    
///     <summary> * Starts (or continues) destroying a block with given ID at the given coordinates for the given partially destroyed
///     * value </summary>
///     
		public virtual void destroyBlockPartially(int p_147587_1_, int p_147587_2_, int p_147587_3_, int p_147587_4_, int p_147587_5_)
		{
			IEnumerator var6 = this.mcServer.ConfigurationManager.playerEntityList.GetEnumerator();

			while (var6.MoveNext())
			{
				EntityPlayerMP var7 = (EntityPlayerMP)var6.Current;

				if (var7 != null && var7.worldObj == this.theWorldServer && var7.EntityId != p_147587_1_)
				{
					double var8 = (double)p_147587_2_ - var7.posX;
					double var10 = (double)p_147587_3_ - var7.posY;
					double var12 = (double)p_147587_4_ - var7.posZ;

					if (var8 * var8 + var10 * var10 + var12 * var12 < 1024.0D)
					{
						var7.playerNetServerHandler.sendPacket(new S25PacketBlockBreakAnim(p_147587_1_, p_147587_2_, p_147587_3_, p_147587_4_, p_147587_5_));
					}
				}
			}
		}

		public virtual void onStaticEntitiesChanged()
		{
		}
	}

}