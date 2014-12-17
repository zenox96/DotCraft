using System;
using System.Collections;

namespace DotCraftCore.nEntity
{

	using CrashReport = DotCraftCore.crash.CrashReport;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using EntityDragon = DotCraftCore.nEntity.nBoss.EntityDragon;
	using EntityWither = DotCraftCore.nEntity.nBoss.EntityWither;
	using EntityBoat = DotCraftCore.nEntity.nItem.EntityBoat;
	using EntityEnderCrystal = DotCraftCore.nEntity.nItem.EntityEnderCrystal;
	using EntityEnderEye = DotCraftCore.nEntity.nItem.EntityEnderEye;
	using EntityEnderPearl = DotCraftCore.nEntity.nItem.EntityEnderPearl;
	using EntityExpBottle = DotCraftCore.nEntity.nItem.EntityExpBottle;
	using EntityFallingBlock = DotCraftCore.nEntity.nItem.EntityFallingBlock;
	using EntityFireworkRocket = DotCraftCore.nEntity.nItem.EntityFireworkRocket;
	using EntityItem = DotCraftCore.nEntity.nItem.EntityItem;
	using EntityMinecart = DotCraftCore.nEntity.nItem.EntityMinecart;
	using EntityTNTPrimed = DotCraftCore.nEntity.nItem.EntityTNTPrimed;
	using EntityXPOrb = DotCraftCore.nEntity.nItem.EntityXPOrb;
	using EntityBat = DotCraftCore.nEntity.nPassive.EntityBat;
	using EntitySquid = DotCraftCore.nEntity.nPassive.EntitySquid;
	using IAnimals = DotCraftCore.nEntity.nPassive.IAnimals;
	using EntityPlayerMP = DotCraftCore.nEntity.nPlayer.EntityPlayerMP;
	using EntityArrow = DotCraftCore.nEntity.nProjectile.EntityArrow;
	using EntityEgg = DotCraftCore.nEntity.nProjectile.EntityEgg;
	using EntityFireball = DotCraftCore.nEntity.nProjectile.EntityFireball;
	using EntityFishHook = DotCraftCore.nEntity.nProjectile.EntityFishHook;
	using EntityPotion = DotCraftCore.nEntity.nProjectile.EntityPotion;
	using EntitySmallFireball = DotCraftCore.nEntity.nProjectile.EntitySmallFireball;
	using EntitySnowball = DotCraftCore.nEntity.nProjectile.EntitySnowball;
	using Packet = DotCraftCore.nNetwork.Packet;
	using IntHashMap = DotCraftCore.nUtil.IntHashMap;
	using ReportedException = DotCraftCore.nUtil.ReportedException;
	using WorldServer = DotCraftCore.nWorld.WorldServer;
	using Chunk = DotCraftCore.nWorld.nChunk.Chunk;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class EntityTracker
	{
		private static readonly Logger logger = LogManager.Logger;
		private readonly WorldServer theWorld;

///    
///     <summary> * List of tracked entities, used for iteration operations on tracked entities. </summary>
///     
		private Set trackedEntities = new HashSet();
		private IntHashMap trackedEntityIDs = new IntHashMap();
		private int entityViewDistance;
		

		public EntityTracker(WorldServer p_i1516_1_)
		{
			this.theWorld = p_i1516_1_;
			this.entityViewDistance = p_i1516_1_.func_73046_m().ConfigurationManager.EntityViewDistance;
		}

///    
///     <summary> * if entity is a player sends all tracked events to the player, otherwise, adds with a visibility and update arate
///     * based on the class type </summary>
///     
		public virtual void addEntityToTracker(Entity p_72786_1_)
		{
			if (p_72786_1_ is EntityPlayerMP)
			{
				this.addEntityToTracker(p_72786_1_, 512, 2);
				EntityPlayerMP var2 = (EntityPlayerMP)p_72786_1_;
				IEnumerator var3 = this.trackedEntities.GetEnumerator();

				while (var3.MoveNext())
				{
					EntityTrackerEntry var4 = (EntityTrackerEntry)var3.Current;

					if (var4.myEntity != var2)
					{
						var4.tryStartWachingThis(var2);
					}
				}
			}
			else if (p_72786_1_ is EntityFishHook)
			{
				this.addEntityToTracker(p_72786_1_, 64, 5, true);
			}
			else if (p_72786_1_ is EntityArrow)
			{
				this.addEntityToTracker(p_72786_1_, 64, 20, false);
			}
			else if (p_72786_1_ is EntitySmallFireball)
			{
				this.addEntityToTracker(p_72786_1_, 64, 10, false);
			}
			else if (p_72786_1_ is EntityFireball)
			{
				this.addEntityToTracker(p_72786_1_, 64, 10, false);
			}
			else if (p_72786_1_ is EntitySnowball)
			{
				this.addEntityToTracker(p_72786_1_, 64, 10, true);
			}
			else if (p_72786_1_ is EntityEnderPearl)
			{
				this.addEntityToTracker(p_72786_1_, 64, 10, true);
			}
			else if (p_72786_1_ is EntityEnderEye)
			{
				this.addEntityToTracker(p_72786_1_, 64, 4, true);
			}
			else if (p_72786_1_ is EntityEgg)
			{
				this.addEntityToTracker(p_72786_1_, 64, 10, true);
			}
			else if (p_72786_1_ is EntityPotion)
			{
				this.addEntityToTracker(p_72786_1_, 64, 10, true);
			}
			else if (p_72786_1_ is EntityExpBottle)
			{
				this.addEntityToTracker(p_72786_1_, 64, 10, true);
			}
			else if (p_72786_1_ is EntityFireworkRocket)
			{
				this.addEntityToTracker(p_72786_1_, 64, 10, true);
			}
			else if (p_72786_1_ is EntityItem)
			{
				this.addEntityToTracker(p_72786_1_, 64, 20, true);
			}
			else if (p_72786_1_ is EntityMinecart)
			{
				this.addEntityToTracker(p_72786_1_, 80, 3, true);
			}
			else if (p_72786_1_ is EntityBoat)
			{
				this.addEntityToTracker(p_72786_1_, 80, 3, true);
			}
			else if (p_72786_1_ is EntitySquid)
			{
				this.addEntityToTracker(p_72786_1_, 64, 3, true);
			}
			else if (p_72786_1_ is EntityWither)
			{
				this.addEntityToTracker(p_72786_1_, 80, 3, false);
			}
			else if (p_72786_1_ is EntityBat)
			{
				this.addEntityToTracker(p_72786_1_, 80, 3, false);
			}
			else if (p_72786_1_ is IAnimals)
			{
				this.addEntityToTracker(p_72786_1_, 80, 3, true);
			}
			else if (p_72786_1_ is EntityDragon)
			{
				this.addEntityToTracker(p_72786_1_, 160, 3, true);
			}
			else if (p_72786_1_ is EntityTNTPrimed)
			{
				this.addEntityToTracker(p_72786_1_, 160, 10, true);
			}
			else if (p_72786_1_ is EntityFallingBlock)
			{
				this.addEntityToTracker(p_72786_1_, 160, 20, true);
			}
			else if (p_72786_1_ is EntityHanging)
			{
				this.addEntityToTracker(p_72786_1_, 160, int.MaxValue, false);
			}
			else if (p_72786_1_ is EntityXPOrb)
			{
				this.addEntityToTracker(p_72786_1_, 160, 20, true);
			}
			else if (p_72786_1_ is EntityEnderCrystal)
			{
				this.addEntityToTracker(p_72786_1_, 256, int.MaxValue, false);
			}
		}

		public virtual void addEntityToTracker(Entity p_72791_1_, int p_72791_2_, int p_72791_3_)
		{
			this.addEntityToTracker(p_72791_1_, p_72791_2_, p_72791_3_, false);
		}

//JAVA TO VB & C# CONVERTER WARNING: 'final' parameters are not allowed in .NET:
//ORIGINAL LINE: public void addEntityToTracker(Entity p_72785_1_, int p_72785_2_, final int p_72785_3_, boolean p_72785_4_)
		public virtual void addEntityToTracker(Entity p_72785_1_, int p_72785_2_, int p_72785_3_, bool p_72785_4_)
		{
			if (p_72785_2_ > this.entityViewDistance)
			{
				p_72785_2_ = this.entityViewDistance;
			}

			try
			{
				if (this.trackedEntityIDs.containsItem(p_72785_1_.EntityId))
				{
					throw new IllegalStateException("Entity is already tracked!");
				}

				EntityTrackerEntry var5 = new EntityTrackerEntry(p_72785_1_, p_72785_2_, p_72785_3_, p_72785_4_);
				this.trackedEntities.add(var5);
				this.trackedEntityIDs.addKey(p_72785_1_.EntityId, var5);
				var5.sendEventsToPlayers(this.theWorld.playerEntities);
			}
			catch (Exception var11)
			{
				CrashReport var6 = CrashReport.makeCrashReport(var11, "Adding entity to track");
				CrashReportCategory var7 = var6.makeCategory("Entity To Track");
				var7.addCrashSection("Tracking range", p_72785_2_ + " blocks");
				var7.addCrashSectionCallable("Update interval", new Callable() {  public string call() { string var1 = "Once per " + p_72785_3_ + " ticks"; if (p_72785_3_ == int.MaxValue) { var1 = "Maximum (" + var1 + ")"; } return var1; } });
				p_72785_1_.addEntityCrashInfo(var7);
				CrashReportCategory var8 = var6.makeCategory("Entity That Is Already Tracked");
				((EntityTrackerEntry)this.trackedEntityIDs.lookup(p_72785_1_.EntityId)).myEntity.addEntityCrashInfo(var8);

				try
				{
					throw new ReportedException(var6);
				}
				catch (ReportedException var10)
				{
					logger.error("\"Silently\" catching entity tracking error.", var10);
				}
			}
		}

		public virtual void removeEntityFromAllTrackingPlayers(Entity p_72790_1_)
		{
			if (p_72790_1_ is EntityPlayerMP)
			{
				EntityPlayerMP var2 = (EntityPlayerMP)p_72790_1_;
				IEnumerator var3 = this.trackedEntities.GetEnumerator();

				while (var3.MoveNext())
				{
					EntityTrackerEntry var4 = (EntityTrackerEntry)var3.Current;
					var4.removeFromWatchingList(var2);
				}
			}

			EntityTrackerEntry var5 = (EntityTrackerEntry)this.trackedEntityIDs.removeObject(p_72790_1_.EntityId);

			if (var5 != null)
			{
				this.trackedEntities.remove(var5);
				var5.informAllAssociatedPlayersOfItemDestruction();
			}
		}

		public virtual void updateTrackedEntities()
		{
			ArrayList var1 = new ArrayList();
			IEnumerator var2 = this.trackedEntities.GetEnumerator();

			while (var2.MoveNext())
			{
				EntityTrackerEntry var3 = (EntityTrackerEntry)var2.Current;
				var3.sendLocationToAllClients(this.theWorld.playerEntities);

				if (var3.playerEntitiesUpdated && var3.myEntity is EntityPlayerMP)
				{
					var1.Add((EntityPlayerMP)var3.myEntity);
				}
			}

			for (int var6 = 0; var6 < var1.Count; ++var6)
			{
				EntityPlayerMP var7 = (EntityPlayerMP)var1[var6];
				IEnumerator var4 = this.trackedEntities.GetEnumerator();

				while (var4.MoveNext())
				{
					EntityTrackerEntry var5 = (EntityTrackerEntry)var4.Current;

					if (var5.myEntity != var7)
					{
						var5.tryStartWachingThis(var7);
					}
				}
			}
		}

		public virtual void func_151247_a(Entity p_151247_1_, Packet p_151247_2_)
		{
			EntityTrackerEntry var3 = (EntityTrackerEntry)this.trackedEntityIDs.lookup(p_151247_1_.EntityId);

			if (var3 != null)
			{
				var3.func_151259_a(p_151247_2_);
			}
		}

		public virtual void func_151248_b(Entity p_151248_1_, Packet p_151248_2_)
		{
			EntityTrackerEntry var3 = (EntityTrackerEntry)this.trackedEntityIDs.lookup(p_151248_1_.EntityId);

			if (var3 != null)
			{
				var3.func_151261_b(p_151248_2_);
			}
		}

		public virtual void removePlayerFromTrackers(EntityPlayerMP p_72787_1_)
		{
			IEnumerator var2 = this.trackedEntities.GetEnumerator();

			while (var2.MoveNext())
			{
				EntityTrackerEntry var3 = (EntityTrackerEntry)var2.Current;
				var3.removePlayerFromTracker(p_72787_1_);
			}
		}

		public virtual void func_85172_a(EntityPlayerMP p_85172_1_, Chunk p_85172_2_)
		{
			IEnumerator var3 = this.trackedEntities.GetEnumerator();

			while (var3.MoveNext())
			{
				EntityTrackerEntry var4 = (EntityTrackerEntry)var3.Current;

				if (var4.myEntity != p_85172_1_ && var4.myEntity.chunkCoordX == p_85172_2_.xPosition && var4.myEntity.chunkCoordZ == p_85172_2_.zPosition)
				{
					var4.tryStartWachingThis(p_85172_1_);
				}
			}
		}
	}

}