using System;
using System.Collections;

namespace DotCraftCore.nEntity
{

	using Block = DotCraftCore.nBlock.Block;
	using ServersideAttributeMap = DotCraftCore.nEntity.nAI.nAttributes.ServersideAttributeMap;
	using EntityDragon = DotCraftCore.nEntity.nBoss.EntityDragon;
	using EntityBoat = DotCraftCore.nEntity.nItem.EntityBoat;
	using EntityEnderCrystal = DotCraftCore.nEntity.nItem.EntityEnderCrystal;
	using EntityEnderEye = DotCraftCore.nEntity.nItem.EntityEnderEye;
	using EntityEnderPearl = DotCraftCore.nEntity.nItem.EntityEnderPearl;
	using EntityExpBottle = DotCraftCore.nEntity.nItem.EntityExpBottle;
	using EntityFallingBlock = DotCraftCore.nEntity.nItem.EntityFallingBlock;
	using EntityFireworkRocket = DotCraftCore.nEntity.nItem.EntityFireworkRocket;
	using EntityItem = DotCraftCore.nEntity.nItem.EntityItem;
	using EntityItemFrame = DotCraftCore.nEntity.nItem.EntityItemFrame;
	using EntityMinecart = DotCraftCore.nEntity.nItem.EntityMinecart;
	using EntityPainting = DotCraftCore.nEntity.nItem.EntityPainting;
	using EntityTNTPrimed = DotCraftCore.nEntity.nItem.EntityTNTPrimed;
	using EntityXPOrb = DotCraftCore.nEntity.nItem.EntityXPOrb;
	using IAnimals = DotCraftCore.nEntity.nPassive.IAnimals;
	using EntityPlayer = DotCraftCore.nEntity.nPlayer.EntityPlayer;
	using EntityPlayerMP = DotCraftCore.nEntity.nPlayer.EntityPlayerMP;
	using EntityArrow = DotCraftCore.nEntity.nProjectile.EntityArrow;
	using EntityEgg = DotCraftCore.nEntity.nProjectile.EntityEgg;
	using EntityFireball = DotCraftCore.nEntity.nProjectile.EntityFireball;
	using EntityFishHook = DotCraftCore.nEntity.nProjectile.EntityFishHook;
	using EntityPotion = DotCraftCore.nEntity.nProjectile.EntityPotion;
	using EntitySmallFireball = DotCraftCore.nEntity.nProjectile.EntitySmallFireball;
	using EntitySnowball = DotCraftCore.nEntity.nProjectile.EntitySnowball;
	using EntityWitherSkull = DotCraftCore.nEntity.nProjectile.EntityWitherSkull;
	using Items = DotCraftCore.nInit.Items;
	using ItemMap = DotCraftCore.nItem.ItemMap;
	using ItemStack = DotCraftCore.nItem.ItemStack;
	using Packet = DotCraftCore.nNetwork.Packet;
	using S04PacketEntityEquipment = DotCraftCore.nNetwork.nPlay.nServer.S04PacketEntityEquipment;
	using S0APacketUseBed = DotCraftCore.nNetwork.nPlay.nServer.S0APacketUseBed;
	using S0CPacketSpawnPlayer = DotCraftCore.nNetwork.nPlay.nServer.S0CPacketSpawnPlayer;
	using S0EPacketSpawnObject = DotCraftCore.nNetwork.nPlay.nServer.S0EPacketSpawnObject;
	using S0FPacketSpawnMob = DotCraftCore.nNetwork.nPlay.nServer.S0FPacketSpawnMob;
	using S10PacketSpawnPainting = DotCraftCore.nNetwork.nPlay.nServer.S10PacketSpawnPainting;
	using S11PacketSpawnExperienceOrb = DotCraftCore.nNetwork.nPlay.nServer.S11PacketSpawnExperienceOrb;
	using S12PacketEntityVelocity = DotCraftCore.nNetwork.nPlay.nServer.S12PacketEntityVelocity;
	using S14PacketEntity = DotCraftCore.nNetwork.nPlay.nServer.S14PacketEntity;
	using S18PacketEntityTeleport = DotCraftCore.nNetwork.nPlay.nServer.S18PacketEntityTeleport;
	using S19PacketEntityHeadLook = DotCraftCore.nNetwork.nPlay.nServer.S19PacketEntityHeadLook;
	using S1BPacketEntityAttach = DotCraftCore.nNetwork.nPlay.nServer.S1BPacketEntityAttach;
	using S1CPacketEntityMetadata = DotCraftCore.nNetwork.nPlay.nServer.S1CPacketEntityMetadata;
	using S1DPacketEntityEffect = DotCraftCore.nNetwork.nPlay.nServer.S1DPacketEntityEffect;
	using S20PacketEntityProperties = DotCraftCore.nNetwork.nPlay.nServer.S20PacketEntityProperties;
	using PotionEffect = DotCraftCore.nPotion.PotionEffect;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using MapData = DotCraftCore.nWorld.nStorage.MapData;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class EntityTrackerEntry
	{
		private static readonly Logger logger = LogManager.Logger;
		public Entity myEntity;
		public int blocksDistanceThreshold;

	/// <summary> check for sync when ticks % updateFrequency==0  </summary>
		public int updateFrequency;
		public int lastScaledXPosition;
		public int lastScaledYPosition;
		public int lastScaledZPosition;
		public int lastYaw;
		public int lastPitch;
		public int lastHeadMotion;
		public double motionX;
		public double motionY;
		public double motionZ;
		public int ticks;
		private double posX;
		private double posY;
		private double posZ;

	/// <summary> set to true on first sendLocationToClients  </summary>
		private bool isDataInitialized;
		private bool sendVelocityUpdates;

///    
///     <summary> * every 400 ticks a  full teleport packet is sent, rather than just a "move me +x" command, so that position
///     * remains fully synced. </summary>
///     
		private int ticksSinceLastForcedTeleport;
		private Entity field_85178_v;
		private bool ridingEntity;
		public bool playerEntitiesUpdated;

///    
///     <summary> * Holds references to all the players that are currently receiving position updates for this entity. </summary>
///     
		public Set trackingPlayers = new HashSet();
		

		public EntityTrackerEntry(Entity p_i1525_1_, int p_i1525_2_, int p_i1525_3_, bool p_i1525_4_)
		{
			this.myEntity = p_i1525_1_;
			this.blocksDistanceThreshold = p_i1525_2_;
			this.updateFrequency = p_i1525_3_;
			this.sendVelocityUpdates = p_i1525_4_;
			this.lastScaledXPosition = MathHelper.floor_double(p_i1525_1_.posX * 32.0D);
			this.lastScaledYPosition = MathHelper.floor_double(p_i1525_1_.posY * 32.0D);
			this.lastScaledZPosition = MathHelper.floor_double(p_i1525_1_.posZ * 32.0D);
			this.lastYaw = MathHelper.floor_float(p_i1525_1_.rotationYaw * 256.0F / 360.0F);
			this.lastPitch = MathHelper.floor_float(p_i1525_1_.rotationPitch * 256.0F / 360.0F);
			this.lastHeadMotion = MathHelper.floor_float(p_i1525_1_.RotationYawHead * 256.0F / 360.0F);
		}

		public override bool Equals(object p_equals_1_)
		{
			return p_equals_1_ is EntityTrackerEntry ? ((EntityTrackerEntry)p_equals_1_).myEntity.EntityId == this.myEntity.EntityId : false;
		}

		public override int GetHashCode()
		{
			return this.myEntity.EntityId;
		}

///    
///     <summary> * also sends velocity, rotation, and riding info. </summary>
///     
		public virtual void sendLocationToAllClients(IList p_73122_1_)
		{
			this.playerEntitiesUpdated = false;

			if (!this.isDataInitialized || this.myEntity.getDistanceSq(this.posX, this.posY, this.posZ) > 16.0D)
			{
				this.posX = this.myEntity.posX;
				this.posY = this.myEntity.posY;
				this.posZ = this.myEntity.posZ;
				this.isDataInitialized = true;
				this.playerEntitiesUpdated = true;
				this.sendEventsToPlayers(p_73122_1_);
			}

			if (this.field_85178_v != this.myEntity.ridingEntity || this.myEntity.ridingEntity != null && this.ticks % 60 == 0)
			{
				this.field_85178_v = this.myEntity.ridingEntity;
				this.func_151259_a(new S1BPacketEntityAttach(0, this.myEntity, this.myEntity.ridingEntity));
			}

			if (this.myEntity is EntityItemFrame && this.ticks % 10 == 0)
			{
				EntityItemFrame var23 = (EntityItemFrame)this.myEntity;
				ItemStack var24 = var23.DisplayedItem;

				if (var24 != null && var24.Item is ItemMap)
				{
					MapData var26 = Items.filled_map.getMapData(var24, this.myEntity.worldObj);
					IEnumerator var27 = p_73122_1_.GetEnumerator();

					while (var27.MoveNext())
					{
						EntityPlayer var28 = (EntityPlayer)var27.Current;
						EntityPlayerMP var29 = (EntityPlayerMP)var28;
						var26.updateVisiblePlayers(var29, var24);
						Packet var30 = Items.filled_map.func_150911_c(var24, this.myEntity.worldObj, var29);

						if (var30 != null)
						{
							var29.playerNetServerHandler.sendPacket(var30);
						}
					}
				}

				this.func_111190_b();
			}
			else if (this.ticks % this.updateFrequency == 0 || this.myEntity.isAirBorne || this.myEntity.DataWatcher.hasChanges())
			{
				int var2;
				int var3;

				if (this.myEntity.ridingEntity == null)
				{
					++this.ticksSinceLastForcedTeleport;
					var2 = this.myEntity.myEntitySize.multiplyBy32AndRound(this.myEntity.posX);
					var3 = MathHelper.floor_double(this.myEntity.posY * 32.0D);
					int var4 = this.myEntity.myEntitySize.multiplyBy32AndRound(this.myEntity.posZ);
					int var5 = MathHelper.floor_float(this.myEntity.rotationYaw * 256.0F / 360.0F);
					int var6 = MathHelper.floor_float(this.myEntity.rotationPitch * 256.0F / 360.0F);
					int var7 = var2 - this.lastScaledXPosition;
					int var8 = var3 - this.lastScaledYPosition;
					int var9 = var4 - this.lastScaledZPosition;
					object var10 = null;
					bool var11 = Math.Abs(var7) >= 4 || Math.Abs(var8) >= 4 || Math.Abs(var9) >= 4 || this.ticks % 60 == 0;
					bool var12 = Math.Abs(var5 - this.lastYaw) >= 4 || Math.Abs(var6 - this.lastPitch) >= 4;

					if (this.ticks > 0 || this.myEntity is EntityArrow)
					{
						if (var7 >= -128 && var7 < 128 && var8 >= -128 && var8 < 128 && var9 >= -128 && var9 < 128 && this.ticksSinceLastForcedTeleport <= 400 && !this.ridingEntity)
						{
							if (var11 && var12)
							{
								var10 = new S14PacketEntity.S17PacketEntityLookMove(this.myEntity.EntityId, (sbyte)var7, (sbyte)var8, (sbyte)var9, (sbyte)var5, (sbyte)var6);
							}
							else if (var11)
							{
								var10 = new S14PacketEntity.S15PacketEntityRelMove(this.myEntity.EntityId, (sbyte)var7, (sbyte)var8, (sbyte)var9);
							}
							else if (var12)
							{
								var10 = new S14PacketEntity.S16PacketEntityLook(this.myEntity.EntityId, (sbyte)var5, (sbyte)var6);
							}
						}
						else
						{
							this.ticksSinceLastForcedTeleport = 0;
							var10 = new S18PacketEntityTeleport(this.myEntity.EntityId, var2, var3, var4, (sbyte)var5, (sbyte)var6);
						}
					}

					if (this.sendVelocityUpdates)
					{
						double var13 = this.myEntity.motionX - this.motionX;
						double var15 = this.myEntity.motionY - this.motionY;
						double var17 = this.myEntity.motionZ - this.motionZ;
						double var19 = 0.02D;
						double var21 = var13 * var13 + var15 * var15 + var17 * var17;

						if (var21 > var19 * var19 || var21 > 0.0D && this.myEntity.motionX == 0.0D && this.myEntity.motionY == 0.0D && this.myEntity.motionZ == 0.0D)
						{
							this.motionX = this.myEntity.motionX;
							this.motionY = this.myEntity.motionY;
							this.motionZ = this.myEntity.motionZ;
							this.func_151259_a(new S12PacketEntityVelocity(this.myEntity.EntityId, this.motionX, this.motionY, this.motionZ));
						}
					}

					if (var10 != null)
					{
						this.func_151259_a((Packet)var10);
					}

					this.func_111190_b();

					if (var11)
					{
						this.lastScaledXPosition = var2;
						this.lastScaledYPosition = var3;
						this.lastScaledZPosition = var4;
					}

					if (var12)
					{
						this.lastYaw = var5;
						this.lastPitch = var6;
					}

					this.ridingEntity = false;
				}
				else
				{
					var2 = MathHelper.floor_float(this.myEntity.rotationYaw * 256.0F / 360.0F);
					var3 = MathHelper.floor_float(this.myEntity.rotationPitch * 256.0F / 360.0F);
					bool var25 = Math.Abs(var2 - this.lastYaw) >= 4 || Math.Abs(var3 - this.lastPitch) >= 4;

					if (var25)
					{
						this.func_151259_a(new S14PacketEntity.S16PacketEntityLook(this.myEntity.EntityId, (sbyte)var2, (sbyte)var3));
						this.lastYaw = var2;
						this.lastPitch = var3;
					}

					this.lastScaledXPosition = this.myEntity.myEntitySize.multiplyBy32AndRound(this.myEntity.posX);
					this.lastScaledYPosition = MathHelper.floor_double(this.myEntity.posY * 32.0D);
					this.lastScaledZPosition = this.myEntity.myEntitySize.multiplyBy32AndRound(this.myEntity.posZ);
					this.func_111190_b();
					this.ridingEntity = true;
				}

				var2 = MathHelper.floor_float(this.myEntity.RotationYawHead * 256.0F / 360.0F);

				if (Math.Abs(var2 - this.lastHeadMotion) >= 4)
				{
					this.func_151259_a(new S19PacketEntityHeadLook(this.myEntity, (sbyte)var2));
					this.lastHeadMotion = var2;
				}

				this.myEntity.isAirBorne = false;
			}

			++this.ticks;

			if (this.myEntity.velocityChanged)
			{
				this.func_151261_b(new S12PacketEntityVelocity(this.myEntity));
				this.myEntity.velocityChanged = false;
			}
		}

		private void func_111190_b()
		{
			DataWatcher var1 = this.myEntity.DataWatcher;

			if (var1.hasChanges())
			{
				this.func_151261_b(new S1CPacketEntityMetadata(this.myEntity.EntityId, var1, false));
			}

			if (this.myEntity is EntityLivingBase)
			{
				ServersideAttributeMap var2 = (ServersideAttributeMap)((EntityLivingBase)this.myEntity).AttributeMap;
				Set var3 = var2.AttributeInstanceSet;

				if (!var3.Empty)
				{
					this.func_151261_b(new S20PacketEntityProperties(this.myEntity.EntityId, var3));
				}

				var3.clear();
			}
		}

		public virtual void func_151259_a(Packet p_151259_1_)
		{
			IEnumerator var2 = this.trackingPlayers.GetEnumerator();

			while (var2.MoveNext())
			{
				EntityPlayerMP var3 = (EntityPlayerMP)var2.Current;
				var3.playerNetServerHandler.sendPacket(p_151259_1_);
			}
		}

		public virtual void func_151261_b(Packet p_151261_1_)
		{
			this.func_151259_a(p_151261_1_);

			if (this.myEntity is EntityPlayerMP)
			{
				((EntityPlayerMP)this.myEntity).playerNetServerHandler.sendPacket(p_151261_1_);
			}
		}

		public virtual void informAllAssociatedPlayersOfItemDestruction()
		{
			IEnumerator var1 = this.trackingPlayers.GetEnumerator();

			while (var1.MoveNext())
			{
				EntityPlayerMP var2 = (EntityPlayerMP)var1.Current;
				var2.func_152339_d(this.myEntity);
			}
		}

		public virtual void removeFromWatchingList(EntityPlayerMP p_73118_1_)
		{
			if (this.trackingPlayers.contains(p_73118_1_))
			{
				p_73118_1_.func_152339_d(this.myEntity);
				this.trackingPlayers.remove(p_73118_1_);
			}
		}

///    
///     <summary> * if the player is more than the distance threshold (typically 64) then the player is removed instead </summary>
///     
		public virtual void tryStartWachingThis(EntityPlayerMP p_73117_1_)
		{
			if (p_73117_1_ != this.myEntity)
			{
				double var2 = p_73117_1_.posX - (double)(this.lastScaledXPosition / 32);
				double var4 = p_73117_1_.posZ - (double)(this.lastScaledZPosition / 32);

				if (var2 >= (double)(-this.blocksDistanceThreshold) && var2 <= (double)this.blocksDistanceThreshold && var4 >= (double)(-this.blocksDistanceThreshold) && var4 <= (double)this.blocksDistanceThreshold)
				{
					if (!this.trackingPlayers.contains(p_73117_1_) && (this.isPlayerWatchingThisChunk(p_73117_1_) || this.myEntity.forceSpawn))
					{
						this.trackingPlayers.add(p_73117_1_);
						Packet var6 = this.func_151260_c();
						p_73117_1_.playerNetServerHandler.sendPacket(var6);

						if (!this.myEntity.DataWatcher.IsBlank)
						{
							p_73117_1_.playerNetServerHandler.sendPacket(new S1CPacketEntityMetadata(this.myEntity.EntityId, this.myEntity.DataWatcher, true));
						}

						if (this.myEntity is EntityLivingBase)
						{
							ServersideAttributeMap var7 = (ServersideAttributeMap)((EntityLivingBase)this.myEntity).AttributeMap;
							ICollection var8 = var7.WatchedAttributes;

							if (!var8.Empty)
							{
								p_73117_1_.playerNetServerHandler.sendPacket(new S20PacketEntityProperties(this.myEntity.EntityId, var8));
							}
						}

						this.motionX = this.myEntity.motionX;
						this.motionY = this.myEntity.motionY;
						this.motionZ = this.myEntity.motionZ;

						if (this.sendVelocityUpdates && !(var6 is S0FPacketSpawnMob))
						{
							p_73117_1_.playerNetServerHandler.sendPacket(new S12PacketEntityVelocity(this.myEntity.EntityId, this.myEntity.motionX, this.myEntity.motionY, this.myEntity.motionZ));
						}

						if (this.myEntity.ridingEntity != null)
						{
							p_73117_1_.playerNetServerHandler.sendPacket(new S1BPacketEntityAttach(0, this.myEntity, this.myEntity.ridingEntity));
						}

						if (this.myEntity is EntityLiving && ((EntityLiving)this.myEntity).LeashedToEntity != null)
						{
							p_73117_1_.playerNetServerHandler.sendPacket(new S1BPacketEntityAttach(1, this.myEntity, ((EntityLiving)this.myEntity).LeashedToEntity));
						}

						if (this.myEntity is EntityLivingBase)
						{
							for (int var10 = 0; var10 < 5; ++var10)
							{
								ItemStack var13 = ((EntityLivingBase)this.myEntity).getEquipmentInSlot(var10);

								if (var13 != null)
								{
									p_73117_1_.playerNetServerHandler.sendPacket(new S04PacketEntityEquipment(this.myEntity.EntityId, var10, var13));
								}
							}
						}

						if (this.myEntity is EntityPlayer)
						{
							EntityPlayer var11 = (EntityPlayer)this.myEntity;

							if (var11.PlayerSleeping)
							{
								p_73117_1_.playerNetServerHandler.sendPacket(new S0APacketUseBed(var11, MathHelper.floor_double(this.myEntity.posX), MathHelper.floor_double(this.myEntity.posY), MathHelper.floor_double(this.myEntity.posZ)));
							}
						}

						if (this.myEntity is EntityLivingBase)
						{
							EntityLivingBase var12 = (EntityLivingBase)this.myEntity;
							IEnumerator var14 = var12.ActivePotionEffects.GetEnumerator();

							while (var14.MoveNext())
							{
								PotionEffect var9 = (PotionEffect)var14.Current;
								p_73117_1_.playerNetServerHandler.sendPacket(new S1DPacketEntityEffect(this.myEntity.EntityId, var9));
							}
						}
					}
				}
				else if (this.trackingPlayers.contains(p_73117_1_))
				{
					this.trackingPlayers.remove(p_73117_1_);
					p_73117_1_.func_152339_d(this.myEntity);
				}
			}
		}

		private bool isPlayerWatchingThisChunk(EntityPlayerMP p_73121_1_)
		{
			return p_73121_1_.ServerForPlayer.PlayerManager.isPlayerWatchingChunk(p_73121_1_, this.myEntity.chunkCoordX, this.myEntity.chunkCoordZ);
		}

		public virtual void sendEventsToPlayers(IList p_73125_1_)
		{
			for (int var2 = 0; var2 < p_73125_1_.Count; ++var2)
			{
				this.tryStartWachingThis((EntityPlayerMP)p_73125_1_[var2]);
			}
		}

		private Packet func_151260_c()
		{
			if (this.myEntity.isDead)
			{
				logger.warn("Fetching addPacket for removed entity");
			}

			if (this.myEntity is EntityItem)
			{
				return new S0EPacketSpawnObject(this.myEntity, 2, 1);
			}
			else if (this.myEntity is EntityPlayerMP)
			{
				return new S0CPacketSpawnPlayer((EntityPlayer)this.myEntity);
			}
			else if (this.myEntity is EntityMinecart)
			{
				EntityMinecart var9 = (EntityMinecart)this.myEntity;
				return new S0EPacketSpawnObject(this.myEntity, 10, var9.MinecartType);
			}
			else if (this.myEntity is EntityBoat)
			{
				return new S0EPacketSpawnObject(this.myEntity, 1);
			}
			else if (!(this.myEntity is IAnimals) && !(this.myEntity is EntityDragon))
			{
				if (this.myEntity is EntityFishHook)
				{
					EntityPlayer var8 = ((EntityFishHook)this.myEntity).field_146042_b;
					return new S0EPacketSpawnObject(this.myEntity, 90, var8 != null ? var8.EntityId : this.myEntity.EntityId);
				}
				else if (this.myEntity is EntityArrow)
				{
					Entity var7 = ((EntityArrow)this.myEntity).shootingEntity;
					return new S0EPacketSpawnObject(this.myEntity, 60, var7 != null ? var7.EntityId : this.myEntity.EntityId);
				}
				else if (this.myEntity is EntitySnowball)
				{
					return new S0EPacketSpawnObject(this.myEntity, 61);
				}
				else if (this.myEntity is EntityPotion)
				{
					return new S0EPacketSpawnObject(this.myEntity, 73, ((EntityPotion)this.myEntity).PotionDamage);
				}
				else if (this.myEntity is EntityExpBottle)
				{
					return new S0EPacketSpawnObject(this.myEntity, 75);
				}
				else if (this.myEntity is EntityEnderPearl)
				{
					return new S0EPacketSpawnObject(this.myEntity, 65);
				}
				else if (this.myEntity is EntityEnderEye)
				{
					return new S0EPacketSpawnObject(this.myEntity, 72);
				}
				else if (this.myEntity is EntityFireworkRocket)
				{
					return new S0EPacketSpawnObject(this.myEntity, 76);
				}
				else
				{
					S0EPacketSpawnObject var2;

					if (this.myEntity is EntityFireball)
					{
						EntityFireball var6 = (EntityFireball)this.myEntity;
						var2 = null;
						sbyte var3 = 63;

						if (this.myEntity is EntitySmallFireball)
						{
							var3 = 64;
						}
						else if (this.myEntity is EntityWitherSkull)
						{
							var3 = 66;
						}

						if (var6.shootingEntity != null)
						{
							var2 = new S0EPacketSpawnObject(this.myEntity, var3, ((EntityFireball)this.myEntity).shootingEntity.EntityId);
						}
						else
						{
							var2 = new S0EPacketSpawnObject(this.myEntity, var3, 0);
						}

						var2.func_149003_d((int)(var6.accelerationX * 8000.0D));
						var2.func_149000_e((int)(var6.accelerationY * 8000.0D));
						var2.func_149007_f((int)(var6.accelerationZ * 8000.0D));
						return var2;
					}
					else if (this.myEntity is EntityEgg)
					{
						return new S0EPacketSpawnObject(this.myEntity, 62);
					}
					else if (this.myEntity is EntityTNTPrimed)
					{
						return new S0EPacketSpawnObject(this.myEntity, 50);
					}
					else if (this.myEntity is EntityEnderCrystal)
					{
						return new S0EPacketSpawnObject(this.myEntity, 51);
					}
					else if (this.myEntity is EntityFallingBlock)
					{
						EntityFallingBlock var5 = (EntityFallingBlock)this.myEntity;
						return new S0EPacketSpawnObject(this.myEntity, 70, Block.getIdFromBlock(var5.func_145805_f()) | var5.field_145814_a << 16);
					}
					else if (this.myEntity is EntityPainting)
					{
						return new S10PacketSpawnPainting((EntityPainting)this.myEntity);
					}
					else if (this.myEntity is EntityItemFrame)
					{
						EntityItemFrame var4 = (EntityItemFrame)this.myEntity;
						var2 = new S0EPacketSpawnObject(this.myEntity, 71, var4.hangingDirection);
						var2.func_148996_a(MathHelper.floor_float((float)(var4.field_146063_b * 32)));
						var2.func_148995_b(MathHelper.floor_float((float)(var4.field_146064_c * 32)));
						var2.func_149005_c(MathHelper.floor_float((float)(var4.field_146062_d * 32)));
						return var2;
					}
					else if (this.myEntity is EntityLeashKnot)
					{
						EntityLeashKnot var1 = (EntityLeashKnot)this.myEntity;
						var2 = new S0EPacketSpawnObject(this.myEntity, 77);
						var2.func_148996_a(MathHelper.floor_float((float)(var1.field_146063_b * 32)));
						var2.func_148995_b(MathHelper.floor_float((float)(var1.field_146064_c * 32)));
						var2.func_149005_c(MathHelper.floor_float((float)(var1.field_146062_d * 32)));
						return var2;
					}
					else if (this.myEntity is EntityXPOrb)
					{
						return new S11PacketSpawnExperienceOrb((EntityXPOrb)this.myEntity);
					}
					else
					{
						throw new System.ArgumentException("Don\'t know how to add " + this.myEntity.GetType() + "!");
					}
				}
			}
			else
			{
				this.lastHeadMotion = MathHelper.floor_float(this.myEntity.RotationYawHead * 256.0F / 360.0F);
				return new S0FPacketSpawnMob((EntityLivingBase)this.myEntity);
			}
		}

		public virtual void removePlayerFromTracker(EntityPlayerMP p_73123_1_)
		{
			if (this.trackingPlayers.contains(p_73123_1_))
			{
				this.trackingPlayers.remove(p_73123_1_);
				p_73123_1_.func_152339_d(this.myEntity);
			}
		}
	}

}