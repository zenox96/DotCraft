using System;
using System.Collections;

namespace DotCraftCore.Network
{

	using Charsets = com.google.common.base.Charsets;
	using Lists = com.google.common.collect.Lists;
	using Unpooled = io.netty.buffer.Unpooled;
	using Future = io.netty.util.concurrent.Future;
	using GenericFutureListener = io.netty.util.concurrent.GenericFutureListener;
	using Material = DotCraftCore.block.material.Material;
	using CommandBlockLogic = DotCraftCore.command.server.CommandBlockLogic;
	using CrashReport = DotCraftCore.crash.CrashReport;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using Entity = DotCraftCore.entity.Entity;
	using EntityMinecartCommandBlock = DotCraftCore.entity.EntityMinecartCommandBlock;
	using EntityItem = DotCraftCore.entity.item.EntityItem;
	using EntityXPOrb = DotCraftCore.entity.item.EntityXPOrb;
	using EntityHorse = DotCraftCore.entity.passive.EntityHorse;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using EntityPlayerMP = DotCraftCore.entity.player.EntityPlayerMP;
	using InventoryPlayer = DotCraftCore.entity.player.InventoryPlayer;
	using EntityArrow = DotCraftCore.entity.projectile.EntityArrow;
	using Items = DotCraftCore.init.Items;
	using Container = DotCraftCore.inventory.Container;
	using ContainerBeacon = DotCraftCore.inventory.ContainerBeacon;
	using ContainerMerchant = DotCraftCore.inventory.ContainerMerchant;
	using ContainerRepair = DotCraftCore.inventory.ContainerRepair;
	using Slot = DotCraftCore.inventory.Slot;
	using ItemEditableBook = DotCraftCore.Item.ItemEditableBook;
	using ItemStack = DotCraftCore.Item.ItemStack;
	using ItemWritableBook = DotCraftCore.Item.ItemWritableBook;
	using NBTTagString = DotCraftCore.NBT.NBTTagString;
	using INetHandlerPlayServer = DotCraftCore.Network.Play.INetHandlerPlayServer;
	using C00PacketKeepAlive = DotCraftCore.Network.Play.Client.C00PacketKeepAlive;
	using C01PacketChatMessage = DotCraftCore.Network.Play.Client.C01PacketChatMessage;
	using C02PacketUseEntity = DotCraftCore.Network.Play.Client.C02PacketUseEntity;
	using C03PacketPlayer = DotCraftCore.Network.Play.Client.C03PacketPlayer;
	using C07PacketPlayerDigging = DotCraftCore.Network.Play.Client.C07PacketPlayerDigging;
	using C08PacketPlayerBlockPlacement = DotCraftCore.Network.Play.Client.C08PacketPlayerBlockPlacement;
	using C09PacketHeldItemChange = DotCraftCore.Network.Play.Client.C09PacketHeldItemChange;
	using C0APacketAnimation = DotCraftCore.Network.Play.Client.C0APacketAnimation;
	using C0BPacketEntityAction = DotCraftCore.Network.Play.Client.C0BPacketEntityAction;
	using C0CPacketInput = DotCraftCore.Network.Play.Client.C0CPacketInput;
	using C0DPacketCloseWindow = DotCraftCore.Network.Play.Client.C0DPacketCloseWindow;
	using C0EPacketClickWindow = DotCraftCore.Network.Play.Client.C0EPacketClickWindow;
	using C0FPacketConfirmTransaction = DotCraftCore.Network.Play.Client.C0FPacketConfirmTransaction;
	using C10PacketCreativeInventoryAction = DotCraftCore.Network.Play.Client.C10PacketCreativeInventoryAction;
	using C11PacketEnchantItem = DotCraftCore.Network.Play.Client.C11PacketEnchantItem;
	using C12PacketUpdateSign = DotCraftCore.Network.Play.Client.C12PacketUpdateSign;
	using C13PacketPlayerAbilities = DotCraftCore.Network.Play.Client.C13PacketPlayerAbilities;
	using C14PacketTabComplete = DotCraftCore.Network.Play.Client.C14PacketTabComplete;
	using C15PacketClientSettings = DotCraftCore.Network.Play.Client.C15PacketClientSettings;
	using C16PacketClientStatus = DotCraftCore.Network.Play.Client.C16PacketClientStatus;
	using C17PacketCustomPayload = DotCraftCore.Network.Play.Client.C17PacketCustomPayload;
	using S00PacketKeepAlive = DotCraftCore.Network.Play.Server.S00PacketKeepAlive;
	using S02PacketChat = DotCraftCore.Network.Play.Server.S02PacketChat;
	using S08PacketPlayerPosLook = DotCraftCore.Network.Play.Server.S08PacketPlayerPosLook;
	using S23PacketBlockChange = DotCraftCore.Network.Play.Server.S23PacketBlockChange;
	using S2FPacketSetSlot = DotCraftCore.Network.Play.Server.S2FPacketSetSlot;
	using S32PacketConfirmTransaction = DotCraftCore.Network.Play.Server.S32PacketConfirmTransaction;
	using S3APacketTabComplete = DotCraftCore.Network.Play.Server.S3APacketTabComplete;
	using S40PacketDisconnect = DotCraftCore.Network.Play.Server.S40PacketDisconnect;
	using MinecraftServer = DotCraftCore.Server.MinecraftServer;
	using UserListBansEntry = DotCraftCore.Server.Management.UserListBansEntry;
	using AchievementList = DotCraftCore.Stats.AchievementList;
	using TileEntity = DotCraftCore.TileEntity.TileEntity;
	using TileEntityBeacon = DotCraftCore.TileEntity.TileEntityBeacon;
	using TileEntityCommandBlock = DotCraftCore.TileEntity.TileEntityCommandBlock;
	using TileEntitySign = DotCraftCore.TileEntity.TileEntitySign;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using ChatAllowedCharacters = DotCraftCore.Util.ChatAllowedCharacters;
	using ChatComponentText = DotCraftCore.Util.ChatComponentText;
	using ChatComponentTranslation = DotCraftCore.Util.ChatComponentTranslation;
	using EnumChatFormatting = DotCraftCore.Util.EnumChatFormatting;
	using IChatComponent = DotCraftCore.Util.IChatComponent;
	using IntHashMap = DotCraftCore.Util.IntHashMap;
	using ReportedException = DotCraftCore.Util.ReportedException;
	using WorldServer = DotCraftCore.World.WorldServer;
	using StringUtils = org.apache.commons.lang3.StringUtils;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class NetHandlerPlayServer : INetHandlerPlayServer
	{
		private static readonly Logger logger = LogManager.Logger;
		public readonly NetworkManager netManager;
		private readonly MinecraftServer serverController;
		public EntityPlayerMP playerEntity;
		private int networkTickCount;

///    
///     <summary> * Used to keep track of how the player is floating while gamerules should prevent that. Surpassing 80 ticks means
///     * kick </summary>
///     
		private int floatingTickCount;
		private bool field_147366_g;
		private int field_147378_h;
		private long field_147379_i;
		private static Random field_147376_j = new Random();
		private long field_147377_k;

///    
///     <summary> * Incremented by 20 each time a user sends a chat message, decreased by one every tick. Non-ops kicked when over
///     * 200 </summary>
///     
		private int chatSpamThresholdCount;
		private int field_147375_m;
		private IntHashMap field_147372_n = new IntHashMap();
		private double lastPosX;
		private double lastPosY;
		private double lastPosZ;
		private bool hasMoved = true;
		

		public NetHandlerPlayServer(MinecraftServer p_i1530_1_, NetworkManager p_i1530_2_, EntityPlayerMP p_i1530_3_)
		{
			this.serverController = p_i1530_1_;
			this.netManager = p_i1530_2_;
			p_i1530_2_.NetHandler = this;
			this.playerEntity = p_i1530_3_;
			p_i1530_3_.playerNetServerHandler = this;
		}

///    
///     <summary> * For scheduled network tasks. Used in NetHandlerPlayServer to send keep-alive packets and in NetHandlerLoginServer
///     * for a login-timeout </summary>
///     
		public virtual void onNetworkTick()
		{
			this.field_147366_g = false;
			++this.networkTickCount;
			this.serverController.theProfiler.startSection("keepAlive");

			if ((long)this.networkTickCount - this.field_147377_k > 40L)
			{
				this.field_147377_k = (long)this.networkTickCount;
				this.field_147379_i = this.func_147363_d();
				this.field_147378_h = (int)this.field_147379_i;
				this.sendPacket(new S00PacketKeepAlive(this.field_147378_h));
			}

			if (this.chatSpamThresholdCount > 0)
			{
				--this.chatSpamThresholdCount;
			}

			if (this.field_147375_m > 0)
			{
				--this.field_147375_m;
			}

			if (this.playerEntity.func_154331_x() > 0L && this.serverController.func_143007_ar() > 0 && MinecraftServer.SystemTimeMillis - this.playerEntity.func_154331_x() > (long)(this.serverController.func_143007_ar() * 1000 * 60))
			{
				this.kickPlayerFromServer("You have been idle for too long!");
			}
		}

		public virtual NetworkManager func_147362_b()
		{
			return this.netManager;
		}

///    
///     <summary> * Kick a player from the server with a reason </summary>
///     
		public virtual void kickPlayerFromServer(string p_147360_1_)
		{
//JAVA TO VB & C# CONVERTER WARNING: The original Java variable was marked 'final':
//ORIGINAL LINE: final ChatComponentText var2 = new ChatComponentText(p_147360_1_);
			ChatComponentText var2 = new ChatComponentText(p_147360_1_);
			this.netManager.scheduleOutboundPacket(new S40PacketDisconnect(var2), new GenericFutureListener[] {new GenericFutureListener() {  public void operationComplete(Future p_operationComplete_1_) { NetHandlerPlayServer.netManager.closeChannel(var2); } } });
			this.netManager.disableAutoRead();
		}

///    
///     <summary> * Processes player movement input. Includes walking, strafing, jumping, sneaking; excludes riding and toggling
///     * flying/sprinting </summary>
///     
		public virtual void processInput(C0CPacketInput p_147358_1_)
		{
			this.playerEntity.setEntityActionState(p_147358_1_.func_149620_c(), p_147358_1_.func_149616_d(), p_147358_1_.func_149618_e(), p_147358_1_.func_149617_f());
		}

///    
///     <summary> * Processes clients perspective on player positioning and/or orientation </summary>
///     
		public virtual void processPlayer(C03PacketPlayer p_147347_1_)
		{
			WorldServer var2 = this.serverController.worldServerForDimension(this.playerEntity.dimension);
			this.field_147366_g = true;

			if (!this.playerEntity.playerConqueredTheEnd)
			{
				double var3;

				if (!this.hasMoved)
				{
					var3 = p_147347_1_.func_149467_d() - this.lastPosY;

					if (p_147347_1_.func_149464_c() == this.lastPosX && var3 * var3 < 0.01D && p_147347_1_.func_149472_e() == this.lastPosZ)
					{
						this.hasMoved = true;
					}
				}

				if (this.hasMoved)
				{
					double var5;
					double var7;
					double var9;

					if (this.playerEntity.ridingEntity != null)
					{
						float var34 = this.playerEntity.rotationYaw;
						float var4 = this.playerEntity.rotationPitch;
						this.playerEntity.ridingEntity.updateRiderPosition();
						var5 = this.playerEntity.posX;
						var7 = this.playerEntity.posY;
						var9 = this.playerEntity.posZ;

						if (p_147347_1_.func_149463_k())
						{
							var34 = p_147347_1_.func_149462_g();
							var4 = p_147347_1_.func_149470_h();
						}

						this.playerEntity.onGround = p_147347_1_.func_149465_i();
						this.playerEntity.onUpdateEntity();
						this.playerEntity.ySize = 0.0F;
						this.playerEntity.setPositionAndRotation(var5, var7, var9, var34, var4);

						if (this.playerEntity.ridingEntity != null)
						{
							this.playerEntity.ridingEntity.updateRiderPosition();
						}

						this.serverController.ConfigurationManager.serverUpdateMountedMovingPlayer(this.playerEntity);

						if (this.hasMoved)
						{
							this.lastPosX = this.playerEntity.posX;
							this.lastPosY = this.playerEntity.posY;
							this.lastPosZ = this.playerEntity.posZ;
						}

						var2.updateEntity(this.playerEntity);
						return;
					}

					if (this.playerEntity.PlayerSleeping)
					{
						this.playerEntity.onUpdateEntity();
						this.playerEntity.setPositionAndRotation(this.lastPosX, this.lastPosY, this.lastPosZ, this.playerEntity.rotationYaw, this.playerEntity.rotationPitch);
						var2.updateEntity(this.playerEntity);
						return;
					}

					var3 = this.playerEntity.posY;
					this.lastPosX = this.playerEntity.posX;
					this.lastPosY = this.playerEntity.posY;
					this.lastPosZ = this.playerEntity.posZ;
					var5 = this.playerEntity.posX;
					var7 = this.playerEntity.posY;
					var9 = this.playerEntity.posZ;
					float var11 = this.playerEntity.rotationYaw;
					float var12 = this.playerEntity.rotationPitch;

					if (p_147347_1_.func_149466_j() && p_147347_1_.func_149467_d() == -999.0D && p_147347_1_.func_149471_f() == -999.0D)
					{
						p_147347_1_.func_149469_a(false);
					}

					double var13;

					if (p_147347_1_.func_149466_j())
					{
						var5 = p_147347_1_.func_149464_c();
						var7 = p_147347_1_.func_149467_d();
						var9 = p_147347_1_.func_149472_e();
						var13 = p_147347_1_.func_149471_f() - p_147347_1_.func_149467_d();

						if (!this.playerEntity.PlayerSleeping && (var13 > 1.65D || var13 < 0.1D))
						{
							this.kickPlayerFromServer("Illegal stance");
							logger.warn(this.playerEntity.CommandSenderName + " had an illegal stance: " + var13);
							return;
						}

						if (Math.Abs(p_147347_1_.func_149464_c()) > 3.2E7D || Math.Abs(p_147347_1_.func_149472_e()) > 3.2E7D)
						{
							this.kickPlayerFromServer("Illegal position");
							return;
						}
					}

					if (p_147347_1_.func_149463_k())
					{
						var11 = p_147347_1_.func_149462_g();
						var12 = p_147347_1_.func_149470_h();
					}

					this.playerEntity.onUpdateEntity();
					this.playerEntity.ySize = 0.0F;
					this.playerEntity.setPositionAndRotation(this.lastPosX, this.lastPosY, this.lastPosZ, var11, var12);

					if (!this.hasMoved)
					{
						return;
					}

					var13 = var5 - this.playerEntity.posX;
					double var15 = var7 - this.playerEntity.posY;
					double var17 = var9 - this.playerEntity.posZ;
					double var19 = Math.Min(Math.Abs(var13), Math.Abs(this.playerEntity.motionX));
					double var21 = Math.Min(Math.Abs(var15), Math.Abs(this.playerEntity.motionY));
					double var23 = Math.Min(Math.Abs(var17), Math.Abs(this.playerEntity.motionZ));
					double var25 = var19 * var19 + var21 * var21 + var23 * var23;

					if (var25 > 100.0D && (!this.serverController.SinglePlayer || !this.serverController.ServerOwner.Equals(this.playerEntity.CommandSenderName)))
					{
						logger.warn(this.playerEntity.CommandSenderName + " moved too quickly! " + var13 + "," + var15 + "," + var17 + " (" + var19 + ", " + var21 + ", " + var23 + ")");
						this.setPlayerLocation(this.lastPosX, this.lastPosY, this.lastPosZ, this.playerEntity.rotationYaw, this.playerEntity.rotationPitch);
						return;
					}

					float var27 = 0.0625F;
					bool var28 = var2.getCollidingBoundingBoxes(this.playerEntity, this.playerEntity.boundingBox.copy().contract((double)var27, (double)var27, (double)var27)).Empty;

					if (this.playerEntity.onGround && !p_147347_1_.func_149465_i() && var15 > 0.0D)
					{
						this.playerEntity.jump();
					}

					this.playerEntity.moveEntity(var13, var15, var17);
					this.playerEntity.onGround = p_147347_1_.func_149465_i();
					this.playerEntity.addMovementStat(var13, var15, var17);
					double var29 = var15;
					var13 = var5 - this.playerEntity.posX;
					var15 = var7 - this.playerEntity.posY;

					if (var15 > -0.5D || var15 < 0.5D)
					{
						var15 = 0.0D;
					}

					var17 = var9 - this.playerEntity.posZ;
					var25 = var13 * var13 + var15 * var15 + var17 * var17;
					bool var31 = false;

					if (var25 > 0.0625D && !this.playerEntity.PlayerSleeping && !this.playerEntity.theItemInWorldManager.Creative)
					{
						var31 = true;
						logger.warn(this.playerEntity.CommandSenderName + " moved wrongly!");
					}

					this.playerEntity.setPositionAndRotation(var5, var7, var9, var11, var12);
					bool var32 = var2.getCollidingBoundingBoxes(this.playerEntity, this.playerEntity.boundingBox.copy().contract((double)var27, (double)var27, (double)var27)).Empty;

					if (var28 && (var31 || !var32) && !this.playerEntity.PlayerSleeping)
					{
						this.setPlayerLocation(this.lastPosX, this.lastPosY, this.lastPosZ, var11, var12);
						return;
					}

					AxisAlignedBB var33 = this.playerEntity.boundingBox.copy().expand((double)var27, (double)var27, (double)var27).addCoord(0.0D, -0.55D, 0.0D);

					if (!this.serverController.FlightAllowed && !this.playerEntity.theItemInWorldManager.Creative && !var2.checkBlockCollision(var33))
					{
						if (var29 >= -0.03125D)
						{
							++this.floatingTickCount;

							if (this.floatingTickCount > 80)
							{
								logger.warn(this.playerEntity.CommandSenderName + " was kicked for floating too long!");
								this.kickPlayerFromServer("Flying is not enabled on this server");
								return;
							}
						}
					}
					else
					{
						this.floatingTickCount = 0;
					}

					this.playerEntity.onGround = p_147347_1_.func_149465_i();
					this.serverController.ConfigurationManager.serverUpdateMountedMovingPlayer(this.playerEntity);
					this.playerEntity.handleFalling(this.playerEntity.posY - var3, p_147347_1_.func_149465_i());
				}
				else if (this.networkTickCount % 20 == 0)
				{
					this.setPlayerLocation(this.lastPosX, this.lastPosY, this.lastPosZ, this.playerEntity.rotationYaw, this.playerEntity.rotationPitch);
				}
			}
		}

		public virtual void setPlayerLocation(double p_147364_1_, double p_147364_3_, double p_147364_5_, float p_147364_7_, float p_147364_8_)
		{
			this.hasMoved = false;
			this.lastPosX = p_147364_1_;
			this.lastPosY = p_147364_3_;
			this.lastPosZ = p_147364_5_;
			this.playerEntity.setPositionAndRotation(p_147364_1_, p_147364_3_, p_147364_5_, p_147364_7_, p_147364_8_);
			this.playerEntity.playerNetServerHandler.sendPacket(new S08PacketPlayerPosLook(p_147364_1_, p_147364_3_ + 1.6200000047683716D, p_147364_5_, p_147364_7_, p_147364_8_, false));
		}

///    
///     <summary> * Processes the player initiating/stopping digging on a particular spot, as well as a player dropping items?. (0:
///     * initiated, 1: reinitiated, 2? , 3-4 drop item (respectively without or with player control), 5: stopped; x,y,z,
///     * side clicked on;) </summary>
///     
		public virtual void processPlayerDigging(C07PacketPlayerDigging p_147345_1_)
		{
			WorldServer var2 = this.serverController.worldServerForDimension(this.playerEntity.dimension);
			this.playerEntity.func_143004_u();

			if (p_147345_1_.func_149506_g() == 4)
			{
				this.playerEntity.dropOneItem(false);
			}
			else if (p_147345_1_.func_149506_g() == 3)
			{
				this.playerEntity.dropOneItem(true);
			}
			else if (p_147345_1_.func_149506_g() == 5)
			{
				this.playerEntity.stopUsingItem();
			}
			else
			{
				bool var3 = false;

				if (p_147345_1_.func_149506_g() == 0)
				{
					var3 = true;
				}

				if (p_147345_1_.func_149506_g() == 1)
				{
					var3 = true;
				}

				if (p_147345_1_.func_149506_g() == 2)
				{
					var3 = true;
				}

				int var4 = p_147345_1_.func_149505_c();
				int var5 = p_147345_1_.func_149503_d();
				int var6 = p_147345_1_.func_149502_e();

				if (var3)
				{
					double var7 = this.playerEntity.posX - ((double)var4 + 0.5D);
					double var9 = this.playerEntity.posY - ((double)var5 + 0.5D) + 1.5D;
					double var11 = this.playerEntity.posZ - ((double)var6 + 0.5D);
					double var13 = var7 * var7 + var9 * var9 + var11 * var11;

					if (var13 > 36.0D)
					{
						return;
					}

					if (var5 >= this.serverController.BuildLimit)
					{
						return;
					}
				}

				if (p_147345_1_.func_149506_g() == 0)
				{
					if (!this.serverController.isBlockProtected(var2, var4, var5, var6, this.playerEntity))
					{
						this.playerEntity.theItemInWorldManager.onBlockClicked(var4, var5, var6, p_147345_1_.func_149501_f());
					}
					else
					{
						this.playerEntity.playerNetServerHandler.sendPacket(new S23PacketBlockChange(var4, var5, var6, var2));
					}
				}
				else if (p_147345_1_.func_149506_g() == 2)
				{
					this.playerEntity.theItemInWorldManager.uncheckedTryHarvestBlock(var4, var5, var6);

					if (var2.getBlock(var4, var5, var6).Material != Material.air)
					{
						this.playerEntity.playerNetServerHandler.sendPacket(new S23PacketBlockChange(var4, var5, var6, var2));
					}
				}
				else if (p_147345_1_.func_149506_g() == 1)
				{
					this.playerEntity.theItemInWorldManager.cancelDestroyingBlock(var4, var5, var6);

					if (var2.getBlock(var4, var5, var6).Material != Material.air)
					{
						this.playerEntity.playerNetServerHandler.sendPacket(new S23PacketBlockChange(var4, var5, var6, var2));
					}
				}
			}
		}

///    
///     <summary> * Processes block placement and block activation (anvil, furnace, etc.) </summary>
///     
		public virtual void processPlayerBlockPlacement(C08PacketPlayerBlockPlacement p_147346_1_)
		{
			WorldServer var2 = this.serverController.worldServerForDimension(this.playerEntity.dimension);
			ItemStack var3 = this.playerEntity.inventory.CurrentItem;
			bool var4 = false;
			int var5 = p_147346_1_.func_149576_c();
			int var6 = p_147346_1_.func_149571_d();
			int var7 = p_147346_1_.func_149570_e();
			int var8 = p_147346_1_.func_149568_f();
			this.playerEntity.func_143004_u();

			if (p_147346_1_.func_149568_f() == 255)
			{
				if (var3 == null)
				{
					return;
				}

				this.playerEntity.theItemInWorldManager.tryUseItem(this.playerEntity, var2, var3);
			}
			else if (p_147346_1_.func_149571_d() >= this.serverController.BuildLimit - 1 && (p_147346_1_.func_149568_f() == 1 || p_147346_1_.func_149571_d() >= this.serverController.BuildLimit))
			{
				ChatComponentTranslation var9 = new ChatComponentTranslation("build.tooHigh", new object[] {Convert.ToInt32(this.serverController.BuildLimit)});
				var9.ChatStyle.Color = EnumChatFormatting.RED;
				this.playerEntity.playerNetServerHandler.sendPacket(new S02PacketChat(var9));
				var4 = true;
			}
			else
			{
				if (this.hasMoved && this.playerEntity.getDistanceSq((double)var5 + 0.5D, (double)var6 + 0.5D, (double)var7 + 0.5D) < 64.0D && !this.serverController.isBlockProtected(var2, var5, var6, var7, this.playerEntity))
				{
					this.playerEntity.theItemInWorldManager.activateBlockOrUseItem(this.playerEntity, var2, var3, var5, var6, var7, var8, p_147346_1_.func_149573_h(), p_147346_1_.func_149569_i(), p_147346_1_.func_149575_j());
				}

				var4 = true;
			}

			if (var4)
			{
				this.playerEntity.playerNetServerHandler.sendPacket(new S23PacketBlockChange(var5, var6, var7, var2));

				if (var8 == 0)
				{
					--var6;
				}

				if (var8 == 1)
				{
					++var6;
				}

				if (var8 == 2)
				{
					--var7;
				}

				if (var8 == 3)
				{
					++var7;
				}

				if (var8 == 4)
				{
					--var5;
				}

				if (var8 == 5)
				{
					++var5;
				}

				this.playerEntity.playerNetServerHandler.sendPacket(new S23PacketBlockChange(var5, var6, var7, var2));
			}

			var3 = this.playerEntity.inventory.CurrentItem;

			if (var3 != null && var3.stackSize == 0)
			{
				this.playerEntity.inventory.mainInventory[this.playerEntity.inventory.currentItem] = null;
				var3 = null;
			}

			if (var3 == null || var3.MaxItemUseDuration == 0)
			{
				this.playerEntity.isChangingQuantityOnly = true;
				this.playerEntity.inventory.mainInventory[this.playerEntity.inventory.currentItem] = ItemStack.copyItemStack(this.playerEntity.inventory.mainInventory[this.playerEntity.inventory.currentItem]);
				Slot var10 = this.playerEntity.openContainer.getSlotFromInventory(this.playerEntity.inventory, this.playerEntity.inventory.currentItem);
				this.playerEntity.openContainer.detectAndSendChanges();
				this.playerEntity.isChangingQuantityOnly = false;

				if (!ItemStack.areItemStacksEqual(this.playerEntity.inventory.CurrentItem, p_147346_1_.func_149574_g()))
				{
					this.sendPacket(new S2FPacketSetSlot(this.playerEntity.openContainer.windowId, var10.slotNumber, this.playerEntity.inventory.CurrentItem));
				}
			}
		}

///    
///     <summary> * Invoked when disconnecting, the parameter is a ChatComponent describing the reason for termination </summary>
///     
		public virtual void onDisconnect(IChatComponent p_147231_1_)
		{
			logger.info(this.playerEntity.CommandSenderName + " lost connection: " + p_147231_1_);
			this.serverController.func_147132_au();
			ChatComponentTranslation var2 = new ChatComponentTranslation("multiplayer.player.left", new object[] {this.playerEntity.func_145748_c_()});
			var2.ChatStyle.Color = EnumChatFormatting.YELLOW;
			this.serverController.ConfigurationManager.func_148539_a(var2);
			this.playerEntity.mountEntityAndWakeUp();
			this.serverController.ConfigurationManager.playerLoggedOut(this.playerEntity);

			if (this.serverController.SinglePlayer && this.playerEntity.CommandSenderName.Equals(this.serverController.ServerOwner))
			{
				logger.info("Stopping singleplayer server as player logged out");
				this.serverController.initiateShutdown();
			}
		}

//JAVA TO VB & C# CONVERTER WARNING: 'final' parameters are not allowed in .NET:
//ORIGINAL LINE: public void sendPacket(final Packet p_147359_1_)
		public virtual void sendPacket(Packet p_147359_1_)
		{
			if (p_147359_1_ is S02PacketChat)
			{
				S02PacketChat var2 = (S02PacketChat)p_147359_1_;
				EntityPlayer.EnumChatVisibility var3 = this.playerEntity.func_147096_v();

				if (var3 == EntityPlayer.EnumChatVisibility.HIDDEN)
				{
					return;
				}

				if (var3 == EntityPlayer.EnumChatVisibility.SYSTEM && !var2.func_148916_d())
				{
					return;
				}
			}

			try
			{
				this.netManager.scheduleOutboundPacket(p_147359_1_, new GenericFutureListener[0]);
			}
			catch (Exception var5)
			{
				CrashReport var6 = CrashReport.makeCrashReport(var5, "Sending packet");
				CrashReportCategory var4 = var6.makeCategory("Packet being sent");
				var4.addCrashSectionCallable("Packet class", new Callable() {  public string call() { return p_147359_1_.GetType().CanonicalName; } });
				throw new ReportedException(var6);
			}
		}

///    
///     <summary> * Updates which quickbar slot is selected </summary>
///     
		public virtual void processHeldItemChange(C09PacketHeldItemChange p_147355_1_)
		{
			if (p_147355_1_.func_149614_c() >= 0 && p_147355_1_.func_149614_c() < InventoryPlayer.HotbarSize)
			{
				this.playerEntity.inventory.currentItem = p_147355_1_.func_149614_c();
				this.playerEntity.func_143004_u();
			}
			else
			{
				logger.warn(this.playerEntity.CommandSenderName + " tried to set an invalid carried item");
			}
		}

///    
///     <summary> * Process chat messages (broadcast back to clients) and commands (executes) </summary>
///     
		public virtual void processChatMessage(C01PacketChatMessage p_147354_1_)
		{
			if (this.playerEntity.func_147096_v() == EntityPlayer.EnumChatVisibility.HIDDEN)
			{
				ChatComponentTranslation var4 = new ChatComponentTranslation("chat.cannotSend", new object[0]);
				var4.ChatStyle.Color = EnumChatFormatting.RED;
				this.sendPacket(new S02PacketChat(var4));
			}
			else
			{
				this.playerEntity.func_143004_u();
				string var2 = p_147354_1_.func_149439_c();
				var2 = StringUtils.normalizeSpace(var2);

				for (int var3 = 0; var3 < var2.Length; ++var3)
				{
					if (!ChatAllowedCharacters.isAllowedCharacter(var2[var3]))
					{
						this.kickPlayerFromServer("Illegal characters in chat");
						return;
					}
				}

				if (var2.StartsWith("/"))
				{
					this.handleSlashCommand(var2);
				}
				else
				{
					ChatComponentTranslation var5 = new ChatComponentTranslation("chat.type.text", new object[] {this.playerEntity.func_145748_c_(), var2});
					this.serverController.ConfigurationManager.func_148544_a(var5, false);
				}

				this.chatSpamThresholdCount += 20;

				if (this.chatSpamThresholdCount > 200 && !this.serverController.ConfigurationManager.func_152596_g(this.playerEntity.GameProfile))
				{
					this.kickPlayerFromServer("disconnect.spam");
				}
			}
		}

///    
///     <summary> * Handle commands that start with a / </summary>
///     
		private void handleSlashCommand(string p_147361_1_)
		{
			this.serverController.CommandManager.executeCommand(this.playerEntity, p_147361_1_);
		}

///    
///     <summary> * Processes the player swinging its held item </summary>
///     
		public virtual void processAnimation(C0APacketAnimation p_147350_1_)
		{
			this.playerEntity.func_143004_u();

			if (p_147350_1_.func_149421_d() == 1)
			{
				this.playerEntity.swingItem();
			}
		}

///    
///     <summary> * Processes a range of action-types: sneaking, sprinting, waking from sleep, opening the inventory or setting jump
///     * height of the horse the player is riding </summary>
///     
		public virtual void processEntityAction(C0BPacketEntityAction p_147357_1_)
		{
			this.playerEntity.func_143004_u();

			if (p_147357_1_.func_149513_d() == 1)
			{
				this.playerEntity.Sneaking = true;
			}
			else if (p_147357_1_.func_149513_d() == 2)
			{
				this.playerEntity.Sneaking = false;
			}
			else if (p_147357_1_.func_149513_d() == 4)
			{
				this.playerEntity.Sprinting = true;
			}
			else if (p_147357_1_.func_149513_d() == 5)
			{
				this.playerEntity.Sprinting = false;
			}
			else if (p_147357_1_.func_149513_d() == 3)
			{
				this.playerEntity.wakeUpPlayer(false, true, true);
				this.hasMoved = false;
			}
			else if (p_147357_1_.func_149513_d() == 6)
			{
				if (this.playerEntity.ridingEntity != null && this.playerEntity.ridingEntity is EntityHorse)
				{
					((EntityHorse)this.playerEntity.ridingEntity).JumpPower = p_147357_1_.func_149512_e();
				}
			}
			else if (p_147357_1_.func_149513_d() == 7 && this.playerEntity.ridingEntity != null && this.playerEntity.ridingEntity is EntityHorse)
			{
				((EntityHorse)this.playerEntity.ridingEntity).openGUI(this.playerEntity);
			}
		}

///    
///     <summary> * Processes interactions ((un)leashing, opening command block GUI) and attacks on an entity with players currently
///     * equipped item </summary>
///     
		public virtual void processUseEntity(C02PacketUseEntity p_147340_1_)
		{
			WorldServer var2 = this.serverController.worldServerForDimension(this.playerEntity.dimension);
			Entity var3 = p_147340_1_.func_149564_a(var2);
			this.playerEntity.func_143004_u();

			if (var3 != null)
			{
				bool var4 = this.playerEntity.canEntityBeSeen(var3);
				double var5 = 36.0D;

				if (!var4)
				{
					var5 = 9.0D;
				}

				if (this.playerEntity.getDistanceSqToEntity(var3) < var5)
				{
					if (p_147340_1_.func_149565_c() == C02PacketUseEntity.Action.INTERACT)
					{
						this.playerEntity.interactWith(var3);
					}
					else if (p_147340_1_.func_149565_c() == C02PacketUseEntity.Action.ATTACK)
					{
						if (var3 is EntityItem || var3 is EntityXPOrb || var3 is EntityArrow || var3 == this.playerEntity)
						{
							this.kickPlayerFromServer("Attempting to attack an invalid entity");
							this.serverController.logWarning("Player " + this.playerEntity.CommandSenderName + " tried to attack an invalid entity");
							return;
						}

						this.playerEntity.attackTargetEntityWithCurrentItem(var3);
					}
				}
			}
		}

///    
///     <summary> * Processes the client status updates: respawn attempt from player, opening statistics or achievements, or
///     * acquiring 'open inventory' achievement </summary>
///     
		public virtual void processClientStatus(C16PacketClientStatus p_147342_1_)
		{
			this.playerEntity.func_143004_u();
			C16PacketClientStatus.EnumState var2 = p_147342_1_.func_149435_c();

			switch (NetHandlerPlayServer.SwitchEnumState.field_151290_a[var2.ordinal()])
			{
				case 1:
					if (this.playerEntity.playerConqueredTheEnd)
					{
						this.playerEntity = this.serverController.ConfigurationManager.respawnPlayer(this.playerEntity, 0, true);
					}
					else if (this.playerEntity.ServerForPlayer.WorldInfo.HardcoreModeEnabled)
					{
						if (this.serverController.SinglePlayer && this.playerEntity.CommandSenderName.Equals(this.serverController.ServerOwner))
						{
							this.playerEntity.playerNetServerHandler.kickPlayerFromServer("You have died. Game over, man, it\'s game over!");
							this.serverController.deleteWorldAndStopServer();
						}
						else
						{
							UserListBansEntry var3 = new UserListBansEntry(this.playerEntity.GameProfile, (DateTime)null, "(You just lost the game)", (DateTime)null, "Death in Hardcore");
							this.serverController.ConfigurationManager.func_152608_h().func_152687_a(var3);
							this.playerEntity.playerNetServerHandler.kickPlayerFromServer("You have died. Game over, man, it\'s game over!");
						}
					}
					else
					{
						if (this.playerEntity.Health > 0.0F)
						{
							return;
						}

						this.playerEntity = this.serverController.ConfigurationManager.respawnPlayer(this.playerEntity, 0, false);
					}

					break;

				case 2:
					this.playerEntity.func_147099_x().func_150876_a(this.playerEntity);
					break;

				case 3:
					this.playerEntity.triggerAchievement(AchievementList.openInventory);
				break;
			}
		}

///    
///     <summary> * Processes the client closing windows (container) </summary>
///     
		public virtual void processCloseWindow(C0DPacketCloseWindow p_147356_1_)
		{
			this.playerEntity.closeContainer();
		}

///    
///     <summary> * Executes a container/inventory slot manipulation as indicated by the packet. Sends the serverside result if they
///     * didn't match the indicated result and prevents further manipulation by the player until he confirms that it has
///     * the same open container/inventory </summary>
///     
		public virtual void processClickWindow(C0EPacketClickWindow p_147351_1_)
		{
			this.playerEntity.func_143004_u();

			if (this.playerEntity.openContainer.windowId == p_147351_1_.func_149548_c() && this.playerEntity.openContainer.isPlayerNotUsingContainer(this.playerEntity))
			{
				ItemStack var2 = this.playerEntity.openContainer.slotClick(p_147351_1_.func_149544_d(), p_147351_1_.func_149543_e(), p_147351_1_.func_149542_h(), this.playerEntity);

				if (ItemStack.areItemStacksEqual(p_147351_1_.func_149546_g(), var2))
				{
					this.playerEntity.playerNetServerHandler.sendPacket(new S32PacketConfirmTransaction(p_147351_1_.func_149548_c(), p_147351_1_.func_149547_f(), true));
					this.playerEntity.isChangingQuantityOnly = true;
					this.playerEntity.openContainer.detectAndSendChanges();
					this.playerEntity.updateHeldItem();
					this.playerEntity.isChangingQuantityOnly = false;
				}
				else
				{
					this.field_147372_n.addKey(this.playerEntity.openContainer.windowId, Convert.ToInt16(p_147351_1_.func_149547_f()));
					this.playerEntity.playerNetServerHandler.sendPacket(new S32PacketConfirmTransaction(p_147351_1_.func_149548_c(), p_147351_1_.func_149547_f(), false));
					this.playerEntity.openContainer.setPlayerIsPresent(this.playerEntity, false);
					ArrayList var3 = new ArrayList();

					for (int var4 = 0; var4 < this.playerEntity.openContainer.inventorySlots.size(); ++var4)
					{
						var3.Add(((Slot)this.playerEntity.openContainer.inventorySlots.get(var4)).Stack);
					}

					this.playerEntity.sendContainerAndContentsToPlayer(this.playerEntity.openContainer, var3);
				}
			}
		}

///    
///     <summary> * Enchants the item identified by the packet given some convoluted conditions (matching window, which
///     * should/shouldn't be in use?) </summary>
///     
		public virtual void processEnchantItem(C11PacketEnchantItem p_147338_1_)
		{
			this.playerEntity.func_143004_u();

			if (this.playerEntity.openContainer.windowId == p_147338_1_.func_149539_c() && this.playerEntity.openContainer.isPlayerNotUsingContainer(this.playerEntity))
			{
				this.playerEntity.openContainer.enchantItem(this.playerEntity, p_147338_1_.func_149537_d());
				this.playerEntity.openContainer.detectAndSendChanges();
			}
		}

///    
///     <summary> * Update the server with an ItemStack in a slot. </summary>
///     
		public virtual void processCreativeInventoryAction(C10PacketCreativeInventoryAction p_147344_1_)
		{
			if (this.playerEntity.theItemInWorldManager.Creative)
			{
				bool var2 = p_147344_1_.func_149627_c() < 0;
				ItemStack var3 = p_147344_1_.func_149625_d();
				bool var4 = p_147344_1_.func_149627_c() >= 1 && p_147344_1_.func_149627_c() < 36 + InventoryPlayer.HotbarSize;
				bool var5 = var3 == null || var3.Item != null;
				bool var6 = var3 == null || var3.ItemDamage >= 0 && var3.stackSize <= 64 && var3.stackSize > 0;

				if (var4 && var5 && var6)
				{
					if (var3 == null)
					{
						this.playerEntity.inventoryContainer.putStackInSlot(p_147344_1_.func_149627_c(), (ItemStack)null);
					}
					else
					{
						this.playerEntity.inventoryContainer.putStackInSlot(p_147344_1_.func_149627_c(), var3);
					}

					this.playerEntity.inventoryContainer.setPlayerIsPresent(this.playerEntity, true);
				}
				else if (var2 && var5 && var6 && this.field_147375_m < 200)
				{
					this.field_147375_m += 20;
					EntityItem var7 = this.playerEntity.dropPlayerItemWithRandomChoice(var3, true);

					if (var7 != null)
					{
						var7.setAgeToCreativeDespawnTime();
					}
				}
			}
		}

///    
///     <summary> * Received in response to the server requesting to confirm that the client-side open container matches the servers'
///     * after a mismatched container-slot manipulation. It will unlock the player's ability to manipulate the container
///     * contents </summary>
///     
		public virtual void processConfirmTransaction(C0FPacketConfirmTransaction p_147339_1_)
		{
			short? var2 = (short?)this.field_147372_n.lookup(this.playerEntity.openContainer.windowId);

			if (var2 != null && p_147339_1_.func_149533_d() == (short)var2 && this.playerEntity.openContainer.windowId == p_147339_1_.func_149532_c() && !this.playerEntity.openContainer.isPlayerNotUsingContainer(this.playerEntity))
			{
				this.playerEntity.openContainer.setPlayerIsPresent(this.playerEntity, true);
			}
		}

		public virtual void processUpdateSign(C12PacketUpdateSign p_147343_1_)
		{
			this.playerEntity.func_143004_u();
			WorldServer var2 = this.serverController.worldServerForDimension(this.playerEntity.dimension);

			if (var2.blockExists(p_147343_1_.func_149588_c(), p_147343_1_.func_149586_d(), p_147343_1_.func_149585_e()))
			{
				TileEntity var3 = var2.getTileEntity(p_147343_1_.func_149588_c(), p_147343_1_.func_149586_d(), p_147343_1_.func_149585_e());

				if (var3 is TileEntitySign)
				{
					TileEntitySign var4 = (TileEntitySign)var3;

					if (!var4.func_145914_a() || var4.func_145911_b() != this.playerEntity)
					{
						this.serverController.logWarning("Player " + this.playerEntity.CommandSenderName + " just tried to change non-editable sign");
						return;
					}
				}

				int var6;
				int var8;

				for (var8 = 0; var8 < 4; ++var8)
				{
					bool var5 = true;

					if (p_147343_1_.func_149589_f()[var8].Length > 15)
					{
						var5 = false;
					}
					else
					{
						for (var6 = 0; var6 < p_147343_1_.func_149589_f()[var8].Length; ++var6)
						{
							if (!ChatAllowedCharacters.isAllowedCharacter(p_147343_1_.func_149589_f()[var8][var6]))
							{
								var5 = false;
							}
						}
					}

					if (!var5)
					{
						p_147343_1_.func_149589_f()[var8] = "!?";
					}
				}

				if (var3 is TileEntitySign)
				{
					var8 = p_147343_1_.func_149588_c();
					int var9 = p_147343_1_.func_149586_d();
					var6 = p_147343_1_.func_149585_e();
					TileEntitySign var7 = (TileEntitySign)var3;
					Array.Copy(p_147343_1_.func_149589_f(), 0, var7.field_145915_a, 0, 4);
					var7.onInventoryChanged();
					var2.func_147471_g(var8, var9, var6);
				}
			}
		}

///    
///     <summary> * Updates a players' ping statistics </summary>
///     
		public virtual void processKeepAlive(C00PacketKeepAlive p_147353_1_)
		{
			if (p_147353_1_.func_149460_c() == this.field_147378_h)
			{
				int var2 = (int)(this.func_147363_d() - this.field_147379_i);
				this.playerEntity.ping = (this.playerEntity.ping * 3 + var2) / 4;
			}
		}

		private long func_147363_d()
		{
			return System.nanoTime() / 1000000L;
		}

///    
///     <summary> * Processes a player starting/stopping flying </summary>
///     
		public virtual void processPlayerAbilities(C13PacketPlayerAbilities p_147348_1_)
		{
			this.playerEntity.capabilities.isFlying = p_147348_1_.func_149488_d() && this.playerEntity.capabilities.allowFlying;
		}

///    
///     <summary> * Retrieves possible tab completions for the requested command string and sends them to the client </summary>
///     
		public virtual void processTabComplete(C14PacketTabComplete p_147341_1_)
		{
			ArrayList var2 = Lists.newArrayList();
			IEnumerator var3 = this.serverController.getPossibleCompletions(this.playerEntity, p_147341_1_.func_149419_c()).GetEnumerator();

			while (var3.MoveNext())
			{
				string var4 = (string)var3.Current;
				var2.Add(var4);
			}

			this.playerEntity.playerNetServerHandler.sendPacket(new S3APacketTabComplete((string[])var2.ToArray()));
		}

///    
///     <summary> * Updates serverside copy of client settings: language, render distance, chat visibility, chat colours, difficulty,
///     * and whether to show the cape </summary>
///     
		public virtual void processClientSettings(C15PacketClientSettings p_147352_1_)
		{
			this.playerEntity.func_147100_a(p_147352_1_);
		}

///    
///     <summary> * Synchronizes serverside and clientside book contents and signing </summary>
///     
		public virtual void processVanilla250Packet(C17PacketCustomPayload p_147349_1_)
		{
			PacketBuffer var2;
			ItemStack var3;
			ItemStack var4;

			if ("MC|BEdit".Equals(p_147349_1_.func_149559_c()))
			{
				var2 = new PacketBuffer(Unpooled.wrappedBuffer(p_147349_1_.func_149558_e()));

				try
				{
					var3 = var2.readItemStackFromBuffer();

					if (var3 == null)
					{
						return;
					}

					if (!ItemWritableBook.func_150930_a(var3.TagCompound))
					{
						throw new IOException("Invalid book tag!");
					}

					var4 = this.playerEntity.inventory.CurrentItem;

					if (var4 != null)
					{
						if (var3.Item == Items.writable_book && var3.Item == var4.Item)
						{
							var4.setTagInfo("pages", var3.TagCompound.getTagList("pages", 8));
						}

						return;
					}
				}
				catch (Exception var38)
				{
					logger.error("Couldn\'t handle book info", var38);
					return;
				}
				finally
				{
					var2.release();
				}

				return;
			}
			else if ("MC|BSign".Equals(p_147349_1_.func_149559_c()))
			{
				var2 = new PacketBuffer(Unpooled.wrappedBuffer(p_147349_1_.func_149558_e()));

				try
				{
					var3 = var2.readItemStackFromBuffer();

					if (var3 != null)
					{
						if (!ItemEditableBook.validBookTagContents(var3.TagCompound))
						{
							throw new IOException("Invalid book tag!");
						}

						var4 = this.playerEntity.inventory.CurrentItem;

						if (var4 == null)
						{
							return;
						}

						if (var3.Item == Items.written_book && var4.Item == Items.writable_book)
						{
							var4.setTagInfo("author", new NBTTagString(this.playerEntity.CommandSenderName));
							var4.setTagInfo("title", new NBTTagString(var3.TagCompound.getString("title")));
							var4.setTagInfo("pages", var3.TagCompound.getTagList("pages", 8));
							var4.func_150996_a(Items.written_book);
						}

						return;
					}
				}
				catch (Exception var36)
				{
					logger.error("Couldn\'t sign book", var36);
					return;
				}
				finally
				{
					var2.release();
				}

				return;
			}
			else
			{
				DataInputStream var40;
				int var42;

				if ("MC|TrSel".Equals(p_147349_1_.func_149559_c()))
				{
					try
					{
						var40 = new DataInputStream(new ByteArrayInputStream(p_147349_1_.func_149558_e()));
						var42 = var40.readInt();
						Container var45 = this.playerEntity.openContainer;

						if (var45 is ContainerMerchant)
						{
							((ContainerMerchant)var45).CurrentRecipeIndex = var42;
						}
					}
					catch (Exception var35)
					{
						logger.error("Couldn\'t select trade", var35);
					}
				}
				else if ("MC|AdvCdm".Equals(p_147349_1_.func_149559_c()))
				{
					if (!this.serverController.CommandBlockEnabled)
					{
						this.playerEntity.addChatMessage(new ChatComponentTranslation("advMode.notEnabled", new object[0]));
					}
					else if (this.playerEntity.canCommandSenderUseCommand(2, "") && this.playerEntity.capabilities.isCreativeMode)
					{
						var2 = new PacketBuffer(Unpooled.wrappedBuffer(p_147349_1_.func_149558_e()));

						try
						{
							sbyte var43 = var2.readByte();
							CommandBlockLogic var46 = null;

							if (var43 == 0)
							{
								TileEntity var5 = this.playerEntity.worldObj.getTileEntity(var2.readInt(), var2.readInt(), var2.readInt());

								if (var5 is TileEntityCommandBlock)
								{
									var46 = ((TileEntityCommandBlock)var5).func_145993_a();
								}
							}
							else if (var43 == 1)
							{
								Entity var48 = this.playerEntity.worldObj.getEntityByID(var2.readInt());

								if (var48 is EntityMinecartCommandBlock)
								{
									var46 = ((EntityMinecartCommandBlock)var48).func_145822_e();
								}
							}

							string var49 = var2.readStringFromBuffer(var2.readableBytes());

							if (var46 != null)
							{
								var46.func_145752_a(var49);
								var46.func_145756_e();
								this.playerEntity.addChatMessage(new ChatComponentTranslation("advMode.setCommand.success", new object[] {var49}));
							}
						}
						catch (Exception var33)
						{
							logger.error("Couldn\'t set command block", var33);
						}
						finally
						{
							var2.release();
						}
					}
					else
					{
						this.playerEntity.addChatMessage(new ChatComponentTranslation("advMode.notAllowed", new object[0]));
					}
				}
				else if ("MC|Beacon".Equals(p_147349_1_.func_149559_c()))
				{
					if (this.playerEntity.openContainer is ContainerBeacon)
					{
						try
						{
							var40 = new DataInputStream(new ByteArrayInputStream(p_147349_1_.func_149558_e()));
							var42 = var40.readInt();
							int var47 = var40.readInt();
							ContainerBeacon var50 = (ContainerBeacon)this.playerEntity.openContainer;
							Slot var6 = var50.getSlot(0);

							if (var6.HasStack)
							{
								var6.decrStackSize(1);
								TileEntityBeacon var7 = var50.func_148327_e();
								var7.func_146001_d(var42);
								var7.func_146004_e(var47);
								var7.onInventoryChanged();
							}
						}
						catch (Exception var32)
						{
							logger.error("Couldn\'t set beacon", var32);
						}
					}
				}
				else if ("MC|ItemName".Equals(p_147349_1_.func_149559_c()) && this.playerEntity.openContainer is ContainerRepair)
				{
					ContainerRepair var41 = (ContainerRepair)this.playerEntity.openContainer;

					if (p_147349_1_.func_149558_e() != null && p_147349_1_.func_149558_e().Length >= 1)
					{
						string var44 = ChatAllowedCharacters.filerAllowedCharacters(new string(p_147349_1_.func_149558_e(), Charsets.UTF_8));

						if (var44.Length <= 30)
						{
							var41.updateItemName(var44);
						}
					}
					else
					{
						var41.updateItemName("");
					}
				}
			}
		}

///    
///     <summary> * Allows validation of the connection state transition. Parameters: from, to (connection state). Typically throws
///     * IllegalStateException or UnsupportedOperationException if validation fails </summary>
///     
		public virtual void onConnectionStateTransition(EnumConnectionState p_147232_1_, EnumConnectionState p_147232_2_)
		{
			if (p_147232_2_ != EnumConnectionState.PLAY)
			{
				throw new IllegalStateException("Unexpected change in protocol!");
			}
		}

		internal sealed class SwitchEnumState
		{
			internal static readonly int[] field_151290_a = new int[C16PacketClientStatus.EnumState.values().length];
			

			static SwitchEnumState()
			{
				try
				{
					field_151290_a[C16PacketClientStatus.EnumState.PERFORM_RESPAWN.ordinal()] = 1;
				}
				catch (NoSuchFieldError var3)
				{
					;
				}

				try
				{
					field_151290_a[C16PacketClientStatus.EnumState.REQUEST_STATS.ordinal()] = 2;
				}
				catch (NoSuchFieldError var2)
				{
					;
				}

				try
				{
					field_151290_a[C16PacketClientStatus.EnumState.OPEN_INVENTORY_ACHIEVEMENT.ordinal()] = 3;
				}
				catch (NoSuchFieldError var1)
				{
					;
				}
			}
		}
	}

}