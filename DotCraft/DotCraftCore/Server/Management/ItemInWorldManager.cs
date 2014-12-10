namespace DotCraftCore.Server.Management
{

	using Block = DotCraftCore.block.Block;
	using Material = DotCraftCore.block.material.Material;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using EntityPlayerMP = DotCraftCore.entity.player.EntityPlayerMP;
	using ItemStack = DotCraftCore.item.ItemStack;
	using ItemSword = DotCraftCore.item.ItemSword;
	using S23PacketBlockChange = DotCraftCore.network.play.server.S23PacketBlockChange;
	using World = DotCraftCore.World.World;
	using WorldServer = DotCraftCore.World.WorldServer;
	using WorldSettings = DotCraftCore.World.WorldSettings;

	public class ItemInWorldManager
	{
	/// <summary> The world object that this object is connected to.  </summary>
		public World theWorld;

	/// <summary> The EntityPlayerMP object that this object is connected to.  </summary>
		public EntityPlayerMP thisPlayerMP;
		private WorldSettings.GameType gameType;

	/// <summary> True if the player is destroying a block  </summary>
		private bool isDestroyingBlock;
		private int initialDamage;
		private int partiallyDestroyedBlockX;
		private int partiallyDestroyedBlockY;
		private int partiallyDestroyedBlockZ;
		private int curblockDamage;

///    
///     <summary> * Set to true when the "finished destroying block" packet is received but the block wasn't fully damaged yet. The
///     * block will not be destroyed while this is false. </summary>
///     
		private bool receivedFinishDiggingPacket;
		private int posX;
		private int posY;
		private int posZ;
		private int initialBlockDamage;
		private int durabilityRemainingOnBlock;
		private const string __OBFID = "CL_00001442";

		public ItemInWorldManager(World p_i1524_1_)
		{
			this.gameType = WorldSettings.GameType.NOT_SET;
			this.durabilityRemainingOnBlock = -1;
			this.theWorld = p_i1524_1_;
		}

		public virtual WorldSettings.GameType GameType
		{
			set
			{
				this.gameType = value;
				value.configurePlayerCapabilities(this.thisPlayerMP.capabilities);
				this.thisPlayerMP.sendPlayerAbilities();
			}
			get
			{
				return this.gameType;
			}
		}


///    
///     <summary> * Get if we are in creative game mode. </summary>
///     
		public virtual bool isCreative()
		{
			get
			{
				return this.gameType.Creative;
			}
		}

///    
///     <summary> * if the gameType is currently NOT_SET then change it to par1 </summary>
///     
		public virtual void initializeGameType(WorldSettings.GameType p_73077_1_)
		{
			if(this.gameType == WorldSettings.GameType.NOT_SET)
			{
				this.gameType = p_73077_1_;
			}

			this.GameType = this.gameType;
		}

		public virtual void updateBlockRemoving()
		{
			++this.curblockDamage;
			float var3;
			int var4;

			if(this.receivedFinishDiggingPacket)
			{
				int var1 = this.curblockDamage - this.initialBlockDamage;
				Block var2 = this.theWorld.getBlock(this.posX, this.posY, this.posZ);

				if(var2.Material == Material.air)
				{
					this.receivedFinishDiggingPacket = false;
				}
				else
				{
					var3 = var2.getPlayerRelativeBlockHardness(this.thisPlayerMP, this.thisPlayerMP.worldObj, this.posX, this.posY, this.posZ) * (float)(var1 + 1);
					var4 = (int)(var3 * 10.0F);

					if(var4 != this.durabilityRemainingOnBlock)
					{
						this.theWorld.destroyBlockInWorldPartially(this.thisPlayerMP.EntityId, this.posX, this.posY, this.posZ, var4);
						this.durabilityRemainingOnBlock = var4;
					}

					if(var3 >= 1.0F)
					{
						this.receivedFinishDiggingPacket = false;
						this.tryHarvestBlock(this.posX, this.posY, this.posZ);
					}
				}
			}
			else if(this.isDestroyingBlock)
			{
				Block var5 = this.theWorld.getBlock(this.partiallyDestroyedBlockX, this.partiallyDestroyedBlockY, this.partiallyDestroyedBlockZ);

				if(var5.Material == Material.air)
				{
					this.theWorld.destroyBlockInWorldPartially(this.thisPlayerMP.EntityId, this.partiallyDestroyedBlockX, this.partiallyDestroyedBlockY, this.partiallyDestroyedBlockZ, -1);
					this.durabilityRemainingOnBlock = -1;
					this.isDestroyingBlock = false;
				}
				else
				{
					int var6 = this.curblockDamage - this.initialDamage;
					var3 = var5.getPlayerRelativeBlockHardness(this.thisPlayerMP, this.thisPlayerMP.worldObj, this.partiallyDestroyedBlockX, this.partiallyDestroyedBlockY, this.partiallyDestroyedBlockZ) * (float)(var6 + 1);
					var4 = (int)(var3 * 10.0F);

					if(var4 != this.durabilityRemainingOnBlock)
					{
						this.theWorld.destroyBlockInWorldPartially(this.thisPlayerMP.EntityId, this.partiallyDestroyedBlockX, this.partiallyDestroyedBlockY, this.partiallyDestroyedBlockZ, var4);
						this.durabilityRemainingOnBlock = var4;
					}
				}
			}
		}

///    
///     <summary> * if not creative, it calls destroyBlockInWorldPartially untill the block is broken first. par4 is the specific
///     * side. tryHarvestBlock can also be the result of this call </summary>
///     
		public virtual void onBlockClicked(int p_73074_1_, int p_73074_2_, int p_73074_3_, int p_73074_4_)
		{
			if(!this.gameType.Adventure || this.thisPlayerMP.isCurrentToolAdventureModeExempt(p_73074_1_, p_73074_2_, p_73074_3_))
			{
				if(this.Creative)
				{
					if(!this.theWorld.extinguishFire((EntityPlayer)null, p_73074_1_, p_73074_2_, p_73074_3_, p_73074_4_))
					{
						this.tryHarvestBlock(p_73074_1_, p_73074_2_, p_73074_3_);
					}
				}
				else
				{
					this.theWorld.extinguishFire((EntityPlayer)null, p_73074_1_, p_73074_2_, p_73074_3_, p_73074_4_);
					this.initialDamage = this.curblockDamage;
					float var5 = 1.0F;
					Block var6 = this.theWorld.getBlock(p_73074_1_, p_73074_2_, p_73074_3_);

					if(var6.Material != Material.air)
					{
						var6.onBlockClicked(this.theWorld, p_73074_1_, p_73074_2_, p_73074_3_, this.thisPlayerMP);
						var5 = var6.getPlayerRelativeBlockHardness(this.thisPlayerMP, this.thisPlayerMP.worldObj, p_73074_1_, p_73074_2_, p_73074_3_);
					}

					if(var6.Material != Material.air && var5 >= 1.0F)
					{
						this.tryHarvestBlock(p_73074_1_, p_73074_2_, p_73074_3_);
					}
					else
					{
						this.isDestroyingBlock = true;
						this.partiallyDestroyedBlockX = p_73074_1_;
						this.partiallyDestroyedBlockY = p_73074_2_;
						this.partiallyDestroyedBlockZ = p_73074_3_;
						int var7 = (int)(var5 * 10.0F);
						this.theWorld.destroyBlockInWorldPartially(this.thisPlayerMP.EntityId, p_73074_1_, p_73074_2_, p_73074_3_, var7);
						this.durabilityRemainingOnBlock = var7;
					}
				}
			}
		}

		public virtual void uncheckedTryHarvestBlock(int p_73082_1_, int p_73082_2_, int p_73082_3_)
		{
			if(p_73082_1_ == this.partiallyDestroyedBlockX && p_73082_2_ == this.partiallyDestroyedBlockY && p_73082_3_ == this.partiallyDestroyedBlockZ)
			{
				int var4 = this.curblockDamage - this.initialDamage;
				Block var5 = this.theWorld.getBlock(p_73082_1_, p_73082_2_, p_73082_3_);

				if(var5.Material != Material.air)
				{
					float var6 = var5.getPlayerRelativeBlockHardness(this.thisPlayerMP, this.thisPlayerMP.worldObj, p_73082_1_, p_73082_2_, p_73082_3_) * (float)(var4 + 1);

					if(var6 >= 0.7F)
					{
						this.isDestroyingBlock = false;
						this.theWorld.destroyBlockInWorldPartially(this.thisPlayerMP.EntityId, p_73082_1_, p_73082_2_, p_73082_3_, -1);
						this.tryHarvestBlock(p_73082_1_, p_73082_2_, p_73082_3_);
					}
					else if(!this.receivedFinishDiggingPacket)
					{
						this.isDestroyingBlock = false;
						this.receivedFinishDiggingPacket = true;
						this.posX = p_73082_1_;
						this.posY = p_73082_2_;
						this.posZ = p_73082_3_;
						this.initialBlockDamage = this.initialDamage;
					}
				}
			}
		}

///    
///     <summary> * note: this ignores the pars passed in and continues to destroy the onClickedBlock </summary>
///     
		public virtual void cancelDestroyingBlock(int p_73073_1_, int p_73073_2_, int p_73073_3_)
		{
			this.isDestroyingBlock = false;
			this.theWorld.destroyBlockInWorldPartially(this.thisPlayerMP.EntityId, this.partiallyDestroyedBlockX, this.partiallyDestroyedBlockY, this.partiallyDestroyedBlockZ, -1);
		}

///    
///     <summary> * Removes a block and triggers the appropriate events </summary>
///     
		private bool removeBlock(int p_73079_1_, int p_73079_2_, int p_73079_3_)
		{
			Block var4 = this.theWorld.getBlock(p_73079_1_, p_73079_2_, p_73079_3_);
			int var5 = this.theWorld.getBlockMetadata(p_73079_1_, p_73079_2_, p_73079_3_);
			var4.onBlockHarvested(this.theWorld, p_73079_1_, p_73079_2_, p_73079_3_, var5, this.thisPlayerMP);
			bool var6 = this.theWorld.setBlockToAir(p_73079_1_, p_73079_2_, p_73079_3_);

			if(var6)
			{
				var4.onBlockDestroyedByPlayer(this.theWorld, p_73079_1_, p_73079_2_, p_73079_3_, var5);
			}

			return var6;
		}

///    
///     <summary> * Attempts to harvest a block at the given coordinate </summary>
///     
		public virtual bool tryHarvestBlock(int p_73084_1_, int p_73084_2_, int p_73084_3_)
		{
			if(this.gameType.Adventure && !this.thisPlayerMP.isCurrentToolAdventureModeExempt(p_73084_1_, p_73084_2_, p_73084_3_))
			{
				return false;
			}
			else if(this.gameType.Creative && this.thisPlayerMP.HeldItem != null && this.thisPlayerMP.HeldItem.Item is ItemSword)
			{
				return false;
			}
			else
			{
				Block var4 = this.theWorld.getBlock(p_73084_1_, p_73084_2_, p_73084_3_);
				int var5 = this.theWorld.getBlockMetadata(p_73084_1_, p_73084_2_, p_73084_3_);
				this.theWorld.playAuxSFXAtEntity(this.thisPlayerMP, 2001, p_73084_1_, p_73084_2_, p_73084_3_, Block.getIdFromBlock(var4) + (this.theWorld.getBlockMetadata(p_73084_1_, p_73084_2_, p_73084_3_) << 12));
				bool var6 = this.removeBlock(p_73084_1_, p_73084_2_, p_73084_3_);

				if(this.Creative)
				{
					this.thisPlayerMP.playerNetServerHandler.sendPacket(new S23PacketBlockChange(p_73084_1_, p_73084_2_, p_73084_3_, this.theWorld));
				}
				else
				{
					ItemStack var7 = this.thisPlayerMP.CurrentEquippedItem;
					bool var8 = this.thisPlayerMP.canHarvestBlock(var4);

					if(var7 != null)
					{
						var7.func_150999_a(this.theWorld, var4, p_73084_1_, p_73084_2_, p_73084_3_, this.thisPlayerMP);

						if(var7.stackSize == 0)
						{
							this.thisPlayerMP.destroyCurrentEquippedItem();
						}
					}

					if(var6 && var8)
					{
						var4.harvestBlock(this.theWorld, this.thisPlayerMP, p_73084_1_, p_73084_2_, p_73084_3_, var5);
					}
				}

				return var6;
			}
		}

///    
///     <summary> * Attempts to right-click use an item by the given EntityPlayer in the given World </summary>
///     
		public virtual bool tryUseItem(EntityPlayer p_73085_1_, World p_73085_2_, ItemStack p_73085_3_)
		{
			int var4 = p_73085_3_.stackSize;
			int var5 = p_73085_3_.ItemDamage;
			ItemStack var6 = p_73085_3_.useItemRightClick(p_73085_2_, p_73085_1_);

			if(var6 == p_73085_3_ && (var6 == null || var6.stackSize == var4 && var6.MaxItemUseDuration <= 0 && var6.ItemDamage == var5))
			{
				return false;
			}
			else
			{
				p_73085_1_.inventory.mainInventory[p_73085_1_.inventory.currentItem] = var6;

				if(this.Creative)
				{
					var6.stackSize = var4;

					if(var6.ItemStackDamageable)
					{
						var6.ItemDamage = var5;
					}
				}

				if(var6.stackSize == 0)
				{
					p_73085_1_.inventory.mainInventory[p_73085_1_.inventory.currentItem] = null;
				}

				if(!p_73085_1_.UsingItem)
				{
					((EntityPlayerMP)p_73085_1_).sendContainerToPlayer(p_73085_1_.inventoryContainer);
				}

				return true;
			}
		}

///    
///     <summary> * Activate the clicked on block, otherwise use the held item. Args: player, world, itemStack, x, y, z, side,
///     * xOffset, yOffset, zOffset </summary>
///     
		public virtual bool activateBlockOrUseItem(EntityPlayer p_73078_1_, World p_73078_2_, ItemStack p_73078_3_, int p_73078_4_, int p_73078_5_, int p_73078_6_, int p_73078_7_, float p_73078_8_, float p_73078_9_, float p_73078_10_)
		{
			if((!p_73078_1_.Sneaking || p_73078_1_.HeldItem == null) && p_73078_2_.getBlock(p_73078_4_, p_73078_5_, p_73078_6_).onBlockActivated(p_73078_2_, p_73078_4_, p_73078_5_, p_73078_6_, p_73078_1_, p_73078_7_, p_73078_8_, p_73078_9_, p_73078_10_))
			{
				return true;
			}
			else if(p_73078_3_ == null)
			{
				return false;
			}
			else if(this.Creative)
			{
				int var11 = p_73078_3_.ItemDamage;
				int var12 = p_73078_3_.stackSize;
				bool var13 = p_73078_3_.tryPlaceItemIntoWorld(p_73078_1_, p_73078_2_, p_73078_4_, p_73078_5_, p_73078_6_, p_73078_7_, p_73078_8_, p_73078_9_, p_73078_10_);
				p_73078_3_.ItemDamage = var11;
				p_73078_3_.stackSize = var12;
				return var13;
			}
			else
			{
				return p_73078_3_.tryPlaceItemIntoWorld(p_73078_1_, p_73078_2_, p_73078_4_, p_73078_5_, p_73078_6_, p_73078_7_, p_73078_8_, p_73078_9_, p_73078_10_);
			}
		}

///    
///     <summary> * Sets the world instance. </summary>
///     
		public virtual WorldServer World
		{
			set
			{
				this.theWorld = value;
			}
		}
	}

}