namespace DotCraftCore.nWorld.nDemo
{

	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using ItemStack = DotCraftCore.item.ItemStack;
	using S2BPacketChangeGameState = DotCraftCore.network.play.server.S2BPacketChangeGameState;
	using ItemInWorldManager = DotCraftCore.nServer.nManagement.ItemInWorldManager;
	using ChatComponentTranslation = DotCraftCore.nUtil.ChatComponentTranslation;
	using World = DotCraftCore.nWorld.World;

	public class DemoWorldManager : ItemInWorldManager
	{
		private bool field_73105_c;
		private bool demoTimeExpired;
		private int field_73104_e;
		private int field_73102_f;
		

		public DemoWorldManager(World p_i1513_1_) : base(p_i1513_1_)
		{
		}

		public virtual void updateBlockRemoving()
		{
			base.updateBlockRemoving();
			++this.field_73102_f;
			long var1 = this.theWorld.TotalWorldTime;
			long var3 = var1 / 24000L + 1L;

			if (!this.field_73105_c && this.field_73102_f > 20)
			{
				this.field_73105_c = true;
				this.thisPlayerMP.playerNetServerHandler.sendPacket(new S2BPacketChangeGameState(5, 0.0F));
			}

			this.demoTimeExpired = var1 > 120500L;

			if (this.demoTimeExpired)
			{
				++this.field_73104_e;
			}

			if (var1 % 24000L == 500L)
			{
				if (var3 <= 6L)
				{
					this.thisPlayerMP.addChatMessage(new ChatComponentTranslation("demo.day." + var3, new object[0]));
				}
			}
			else if (var3 == 1L)
			{
				if (var1 == 100L)
				{
					this.thisPlayerMP.playerNetServerHandler.sendPacket(new S2BPacketChangeGameState(5, 101.0F));
				}
				else if (var1 == 175L)
				{
					this.thisPlayerMP.playerNetServerHandler.sendPacket(new S2BPacketChangeGameState(5, 102.0F));
				}
				else if (var1 == 250L)
				{
					this.thisPlayerMP.playerNetServerHandler.sendPacket(new S2BPacketChangeGameState(5, 103.0F));
				}
			}
			else if (var3 == 5L && var1 % 24000L == 22000L)
			{
				this.thisPlayerMP.addChatMessage(new ChatComponentTranslation("demo.day.warning", new object[0]));
			}
		}

///    
///     <summary> * Sends a message to the player reminding them that this is the demo version </summary>
///     
		private void sendDemoReminder()
		{
			if (this.field_73104_e > 100)
			{
				this.thisPlayerMP.addChatMessage(new ChatComponentTranslation("demo.reminder", new object[0]));
				this.field_73104_e = 0;
			}
		}

///    
///     <summary> * if not creative, it calls destroyBlockInWorldPartially untill the block is broken first. par4 is the specific
///     * side. tryHarvestBlock can also be the result of this call </summary>
///     
		public virtual void onBlockClicked(int p_73074_1_, int p_73074_2_, int p_73074_3_, int p_73074_4_)
		{
			if (this.demoTimeExpired)
			{
				this.sendDemoReminder();
			}
			else
			{
				base.onBlockClicked(p_73074_1_, p_73074_2_, p_73074_3_, p_73074_4_);
			}
		}

		public virtual void uncheckedTryHarvestBlock(int p_73082_1_, int p_73082_2_, int p_73082_3_)
		{
			if (!this.demoTimeExpired)
			{
				base.uncheckedTryHarvestBlock(p_73082_1_, p_73082_2_, p_73082_3_);
			}
		}

///    
///     <summary> * Attempts to harvest a block at the given coordinate </summary>
///     
		public virtual bool tryHarvestBlock(int p_73084_1_, int p_73084_2_, int p_73084_3_)
		{
			return this.demoTimeExpired ? false : base.tryHarvestBlock(p_73084_1_, p_73084_2_, p_73084_3_);
		}

///    
///     <summary> * Attempts to right-click use an item by the given EntityPlayer in the given World </summary>
///     
		public virtual bool tryUseItem(EntityPlayer p_73085_1_, World p_73085_2_, ItemStack p_73085_3_)
		{
			if (this.demoTimeExpired)
			{
				this.sendDemoReminder();
				return false;
			}
			else
			{
				return base.tryUseItem(p_73085_1_, p_73085_2_, p_73085_3_);
			}
		}

///    
///     <summary> * Activate the clicked on block, otherwise use the held item. Args: player, world, itemStack, x, y, z, side,
///     * xOffset, yOffset, zOffset </summary>
///     
		public virtual bool activateBlockOrUseItem(EntityPlayer p_73078_1_, World p_73078_2_, ItemStack p_73078_3_, int p_73078_4_, int p_73078_5_, int p_73078_6_, int p_73078_7_, float p_73078_8_, float p_73078_9_, float p_73078_10_)
		{
			if (this.demoTimeExpired)
			{
				this.sendDemoReminder();
				return false;
			}
			else
			{
				return base.activateBlockOrUseItem(p_73078_1_, p_73078_2_, p_73078_3_, p_73078_4_, p_73078_5_, p_73078_6_, p_73078_7_, p_73078_8_, p_73078_9_, p_73078_10_);
			}
		}
	}

}