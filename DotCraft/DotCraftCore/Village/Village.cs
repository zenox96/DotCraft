using System;
using System.Collections;

namespace DotCraftCore.Village
{

	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using EntityIronGolem = DotCraftCore.entity.monster.EntityIronGolem;
	using EntityVillager = DotCraftCore.entity.passive.EntityVillager;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using NBTTagList = DotCraftCore.nbt.NBTTagList;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using ChunkCoordinates = DotCraftCore.Util.ChunkCoordinates;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using Vec3 = DotCraftCore.Util.Vec3;
	using World = DotCraftCore.World.World;

	public class Village
	{
		private World worldObj;

	/// <summary> list of VillageDoorInfo objects  </summary>
		private readonly IList villageDoorInfoList = new ArrayList();

///    
///     <summary> * This is the sum of all door coordinates and used to calculate the actual village center by dividing by the number
///     * of doors. </summary>
///     
		private readonly ChunkCoordinates centerHelper = new ChunkCoordinates(0, 0, 0);

	/// <summary> This is the actual village center.  </summary>
		private readonly ChunkCoordinates center = new ChunkCoordinates(0, 0, 0);
		private int villageRadius;
		private int lastAddDoorTimestamp;
		private int tickCounter;
		private int numVillagers;

	/// <summary> Timestamp of tick count when villager last bred  </summary>
		private int noBreedTicks;

	/// <summary> List of player reputations with this village  </summary>
		private SortedDictionary playerReputation = new SortedDictionary();
		private IList villageAgressors = new ArrayList();
		private int numIronGolems;
		

		public Village()
		{
		}

		public Village(World p_i1675_1_)
		{
			this.worldObj = p_i1675_1_;
		}

		public virtual void func_82691_a(World p_82691_1_)
		{
			this.worldObj = p_82691_1_;
		}

///    
///     <summary> * Called periodically by VillageCollection </summary>
///     
		public virtual void tick(int p_75560_1_)
		{
			this.tickCounter = p_75560_1_;
			this.removeDeadAndOutOfRangeDoors();
			this.removeDeadAndOldAgressors();

			if(p_75560_1_ % 20 == 0)
			{
				this.updateNumVillagers();
			}

			if(p_75560_1_ % 30 == 0)
			{
				this.updateNumIronGolems();
			}

			int var2 = this.numVillagers / 10;

			if(this.numIronGolems < var2 && this.villageDoorInfoList.Count > 20 && this.worldObj.rand.Next(7000) == 0)
			{
				Vec3 var3 = this.tryGetIronGolemSpawningLocation(MathHelper.floor_float((float)this.center.posX), MathHelper.floor_float((float)this.center.posY), MathHelper.floor_float((float)this.center.posZ), 2, 4, 2);

				if(var3 != null)
				{
					EntityIronGolem var4 = new EntityIronGolem(this.worldObj);
					var4.setPosition(var3.xCoord, var3.yCoord, var3.zCoord);
					this.worldObj.spawnEntityInWorld(var4);
					++this.numIronGolems;
				}
			}
		}

///    
///     <summary> * Tries up to 10 times to get a valid spawning location before eventually failing and returning null. </summary>
///     
		private Vec3 tryGetIronGolemSpawningLocation(int p_75559_1_, int p_75559_2_, int p_75559_3_, int p_75559_4_, int p_75559_5_, int p_75559_6_)
		{
			for(int var7 = 0; var7 < 10; ++var7)
			{
				int var8 = p_75559_1_ + this.worldObj.rand.Next(16) - 8;
				int var9 = p_75559_2_ + this.worldObj.rand.Next(6) - 3;
				int var10 = p_75559_3_ + this.worldObj.rand.Next(16) - 8;

				if(this.isInRange(var8, var9, var10) && this.isValidIronGolemSpawningLocation(var8, var9, var10, p_75559_4_, p_75559_5_, p_75559_6_))
				{
					return Vec3.createVectorHelper((double)var8, (double)var9, (double)var10);
				}
			}

			return null;
		}

		private bool isValidIronGolemSpawningLocation(int p_75563_1_, int p_75563_2_, int p_75563_3_, int p_75563_4_, int p_75563_5_, int p_75563_6_)
		{
			if(!World.doesBlockHaveSolidTopSurface(this.worldObj, p_75563_1_, p_75563_2_ - 1, p_75563_3_))
			{
				return false;
			}
			else
			{
				int var7 = p_75563_1_ - p_75563_4_ / 2;
				int var8 = p_75563_3_ - p_75563_6_ / 2;

				for(int var9 = var7; var9 < var7 + p_75563_4_; ++var9)
				{
					for(int var10 = p_75563_2_; var10 < p_75563_2_ + p_75563_5_; ++var10)
					{
						for(int var11 = var8; var11 < var8 + p_75563_6_; ++var11)
						{
							if(this.worldObj.getBlock(var9, var10, var11).NormalCube)
							{
								return false;
							}
						}
					}
				}

				return true;
			}
		}

		private void updateNumIronGolems()
		{
			IList var1 = this.worldObj.getEntitiesWithinAABB(typeof(EntityIronGolem), AxisAlignedBB.getBoundingBox((double)(this.center.posX - this.villageRadius), (double)(this.center.posY - 4), (double)(this.center.posZ - this.villageRadius), (double)(this.center.posX + this.villageRadius), (double)(this.center.posY + 4), (double)(this.center.posZ + this.villageRadius)));
			this.numIronGolems = var1.Count;
		}

		private void updateNumVillagers()
		{
			IList var1 = this.worldObj.getEntitiesWithinAABB(typeof(EntityVillager), AxisAlignedBB.getBoundingBox((double)(this.center.posX - this.villageRadius), (double)(this.center.posY - 4), (double)(this.center.posZ - this.villageRadius), (double)(this.center.posX + this.villageRadius), (double)(this.center.posY + 4), (double)(this.center.posZ + this.villageRadius)));
			this.numVillagers = var1.Count;

			if(this.numVillagers == 0)
			{
				this.playerReputation.Clear();
			}
		}

		public virtual ChunkCoordinates Center
		{
			get
			{
				return this.center;
			}
		}

		public virtual int VillageRadius
		{
			get
			{
				return this.villageRadius;
			}
		}

///    
///     <summary> * Actually get num village door info entries, but that boils down to number of doors. Called by
///     * EntityAIVillagerMate and VillageSiege </summary>
///     
		public virtual int NumVillageDoors
		{
			get
			{
				return this.villageDoorInfoList.Count;
			}
		}

		public virtual int TicksSinceLastDoorAdding
		{
			get
			{
				return this.tickCounter - this.lastAddDoorTimestamp;
			}
		}

		public virtual int NumVillagers
		{
			get
			{
				return this.numVillagers;
			}
		}

///    
///     <summary> * Returns true, if the given coordinates are within the bounding box of the village. </summary>
///     
		public virtual bool isInRange(int p_75570_1_, int p_75570_2_, int p_75570_3_)
		{
			return this.center.getDistanceSquared(p_75570_1_, p_75570_2_, p_75570_3_) < (float)(this.villageRadius * this.villageRadius);
		}

///    
///     <summary> * called only by class EntityAIMoveThroughVillage </summary>
///     
		public virtual IList VillageDoorInfoList
		{
			get
			{
				return this.villageDoorInfoList;
			}
		}

		public virtual VillageDoorInfo findNearestDoor(int p_75564_1_, int p_75564_2_, int p_75564_3_)
		{
			VillageDoorInfo var4 = null;
			int var5 = int.MaxValue;
			IEnumerator var6 = this.villageDoorInfoList.GetEnumerator();

			while(var6.MoveNext())
			{
				VillageDoorInfo var7 = (VillageDoorInfo)var6.Current;
				int var8 = var7.getDistanceSquared(p_75564_1_, p_75564_2_, p_75564_3_);

				if(var8 < var5)
				{
					var4 = var7;
					var5 = var8;
				}
			}

			return var4;
		}

///    
///     <summary> * Find a door suitable for shelter. If there are more doors in a distance of 16 blocks, then the least restricted
///     * one (i.e. the one protecting the lowest number of villagers) of them is chosen, else the nearest one regardless
///     * of restriction. </summary>
///     
		public virtual VillageDoorInfo findNearestDoorUnrestricted(int p_75569_1_, int p_75569_2_, int p_75569_3_)
		{
			VillageDoorInfo var4 = null;
			int var5 = int.MaxValue;
			IEnumerator var6 = this.villageDoorInfoList.GetEnumerator();

			while(var6.MoveNext())
			{
				VillageDoorInfo var7 = (VillageDoorInfo)var6.Current;
				int var8 = var7.getDistanceSquared(p_75569_1_, p_75569_2_, p_75569_3_);

				if(var8 > 256)
				{
					var8 *= 1000;
				}
				else
				{
					var8 = var7.DoorOpeningRestrictionCounter;
				}

				if(var8 < var5)
				{
					var4 = var7;
					var5 = var8;
				}
			}

			return var4;
		}

		public virtual VillageDoorInfo getVillageDoorAt(int p_75578_1_, int p_75578_2_, int p_75578_3_)
		{
			if(this.center.getDistanceSquared(p_75578_1_, p_75578_2_, p_75578_3_) > (float)(this.villageRadius * this.villageRadius))
			{
				return null;
			}
			else
			{
				IEnumerator var4 = this.villageDoorInfoList.GetEnumerator();
				VillageDoorInfo var5;

				do
				{
					if(!var4.MoveNext())
					{
						return null;
					}

					var5 = (VillageDoorInfo)var4.Current;
				}
				while(var5.posX != p_75578_1_ || var5.posZ != p_75578_3_ || Math.Abs(var5.posY - p_75578_2_) > 1);

				return var5;
			}
		}

		public virtual void addVillageDoorInfo(VillageDoorInfo p_75576_1_)
		{
			this.villageDoorInfoList.Add(p_75576_1_);
			this.centerHelper.posX += p_75576_1_.posX;
			this.centerHelper.posY += p_75576_1_.posY;
			this.centerHelper.posZ += p_75576_1_.posZ;
			this.updateVillageRadiusAndCenter();
			this.lastAddDoorTimestamp = p_75576_1_.lastActivityTimestamp;
		}

///    
///     <summary> * Returns true, if there is not a single village door left. Called by VillageCollection </summary>
///     
		public virtual bool isAnnihilated()
		{
			get
			{
				return this.villageDoorInfoList.Count == 0;
			}
		}

		public virtual void addOrRenewAgressor(EntityLivingBase p_75575_1_)
		{
			IEnumerator var2 = this.villageAgressors.GetEnumerator();
			Village.VillageAgressor var3;

			do
			{
				if(!var2.MoveNext())
				{
					this.villageAgressors.Add(new Village.VillageAgressor(p_75575_1_, this.tickCounter));
					return;
				}

				var3 = (Village.VillageAgressor)var2.Current;
			}
			while(var3.agressor != p_75575_1_);

			var3.agressionTime = this.tickCounter;
		}

		public virtual EntityLivingBase findNearestVillageAggressor(EntityLivingBase p_75571_1_)
		{
			double var2 = double.MaxValue;
			Village.VillageAgressor var4 = null;

			for(int var5 = 0; var5 < this.villageAgressors.Count; ++var5)
			{
				Village.VillageAgressor var6 = (Village.VillageAgressor)this.villageAgressors.get(var5);
				double var7 = var6.agressor.getDistanceSqToEntity(p_75571_1_);

				if(var7 <= var2)
				{
					var4 = var6;
					var2 = var7;
				}
			}

			return var4 != null ? var4.agressor : null;
		}

		public virtual EntityPlayer func_82685_c(EntityLivingBase p_82685_1_)
		{
			double var2 = double.MaxValue;
			EntityPlayer var4 = null;
			IEnumerator var5 = this.playerReputation.Keys.GetEnumerator();

			while(var5.MoveNext())
			{
				string var6 = (string)var5.Current;

				if(this.isPlayerReputationTooLow(var6))
				{
					EntityPlayer var7 = this.worldObj.getPlayerEntityByName(var6);

					if(var7 != null)
					{
						double var8 = var7.getDistanceSqToEntity(p_82685_1_);

						if(var8 <= var2)
						{
							var4 = var7;
							var2 = var8;
						}
					}
				}
			}

			return var4;
		}

		private void removeDeadAndOldAgressors()
		{
			IEnumerator var1 = this.villageAgressors.GetEnumerator();

			while(var1.MoveNext())
			{
				Village.VillageAgressor var2 = (Village.VillageAgressor)var1.Current;

				if(!var2.agressor.EntityAlive || Math.Abs(this.tickCounter - var2.agressionTime) > 300)
				{
					var1.remove();
				}
			}
		}

		private void removeDeadAndOutOfRangeDoors()
		{
			bool var1 = false;
			bool var2 = this.worldObj.rand.Next(50) == 0;
			IEnumerator var3 = this.villageDoorInfoList.GetEnumerator();

			while(var3.MoveNext())
			{
				VillageDoorInfo var4 = (VillageDoorInfo)var3.Current;

				if(var2)
				{
					var4.resetDoorOpeningRestrictionCounter();
				}

				if(!this.isBlockDoor(var4.posX, var4.posY, var4.posZ) || Math.Abs(this.tickCounter - var4.lastActivityTimestamp) > 1200)
				{
					this.centerHelper.posX -= var4.posX;
					this.centerHelper.posY -= var4.posY;
					this.centerHelper.posZ -= var4.posZ;
					var1 = true;
					var4.isDetachedFromVillageFlag = true;
					var3.remove();
				}
			}

			if(var1)
			{
				this.updateVillageRadiusAndCenter();
			}
		}

		private bool isBlockDoor(int p_75574_1_, int p_75574_2_, int p_75574_3_)
		{
			return this.worldObj.getBlock(p_75574_1_, p_75574_2_, p_75574_3_) == Blocks.wooden_door;
		}

		private void updateVillageRadiusAndCenter()
		{
			int var1 = this.villageDoorInfoList.Count;

			if(var1 == 0)
			{
				this.center.set(0, 0, 0);
				this.villageRadius = 0;
			}
			else
			{
				this.center.set(this.centerHelper.posX / var1, this.centerHelper.posY / var1, this.centerHelper.posZ / var1);
				int var2 = 0;
				VillageDoorInfo var4;

				for(IEnumerator var3 = this.villageDoorInfoList.GetEnumerator(); var3.MoveNext(); var2 = Math.Max(var4.getDistanceSquared(this.center.posX, this.center.posY, this.center.posZ), var2))
				{
					var4 = (VillageDoorInfo)var3.Current;
				}

				this.villageRadius = Math.Max(32, (int)Math.Sqrt((double)var2) + 1);
			}
		}

///    
///     <summary> * Return the village reputation for a player </summary>
///     
		public virtual int getReputationForPlayer(string p_82684_1_)
		{
			int? var2 = (int?)this.playerReputation.get(p_82684_1_);
			return var2 != null ? (int)var2 : 0;
		}

///    
///     <summary> * Set the village reputation for a player. </summary>
///     
		public virtual int setReputationForPlayer(string p_82688_1_, int p_82688_2_)
		{
			int var3 = this.getReputationForPlayer(p_82688_1_);
			int var4 = MathHelper.clamp_int(var3 + p_82688_2_, -30, 10);
			this.playerReputation.Add(p_82688_1_, Convert.ToInt32(var4));
			return var4;
		}

///    
///     <summary> * Return whether this player has a too low reputation with this village. </summary>
///     
		public virtual bool isPlayerReputationTooLow(string p_82687_1_)
		{
			return this.getReputationForPlayer(p_82687_1_) <= -15;
		}

///    
///     <summary> * Read this village's data from NBT. </summary>
///     
		public virtual void readVillageDataFromNBT(NBTTagCompound p_82690_1_)
		{
			this.numVillagers = p_82690_1_.getInteger("PopSize");
			this.villageRadius = p_82690_1_.getInteger("Radius");
			this.numIronGolems = p_82690_1_.getInteger("Golems");
			this.lastAddDoorTimestamp = p_82690_1_.getInteger("Stable");
			this.tickCounter = p_82690_1_.getInteger("Tick");
			this.noBreedTicks = p_82690_1_.getInteger("MTick");
			this.center.posX = p_82690_1_.getInteger("CX");
			this.center.posY = p_82690_1_.getInteger("CY");
			this.center.posZ = p_82690_1_.getInteger("CZ");
			this.centerHelper.posX = p_82690_1_.getInteger("ACX");
			this.centerHelper.posY = p_82690_1_.getInteger("ACY");
			this.centerHelper.posZ = p_82690_1_.getInteger("ACZ");
			NBTTagList var2 = p_82690_1_.getTagList("Doors", 10);

			for(int var3 = 0; var3 < var2.tagCount(); ++var3)
			{
				NBTTagCompound var4 = var2.getCompoundTagAt(var3);
				VillageDoorInfo var5 = new VillageDoorInfo(var4.getInteger("X"), var4.getInteger("Y"), var4.getInteger("Z"), var4.getInteger("IDX"), var4.getInteger("IDZ"), var4.getInteger("TS"));
				this.villageDoorInfoList.Add(var5);
			}

			NBTTagList var6 = p_82690_1_.getTagList("Players", 10);

			for(int var7 = 0; var7 < var6.tagCount(); ++var7)
			{
				NBTTagCompound var8 = var6.getCompoundTagAt(var7);
				this.playerReputation.Add(var8.getString("Name"), Convert.ToInt32(var8.getInteger("S")));
			}
		}

///    
///     <summary> * Write this village's data to NBT. </summary>
///     
		public virtual void writeVillageDataToNBT(NBTTagCompound p_82689_1_)
		{
			p_82689_1_.setInteger("PopSize", this.numVillagers);
			p_82689_1_.setInteger("Radius", this.villageRadius);
			p_82689_1_.setInteger("Golems", this.numIronGolems);
			p_82689_1_.setInteger("Stable", this.lastAddDoorTimestamp);
			p_82689_1_.setInteger("Tick", this.tickCounter);
			p_82689_1_.setInteger("MTick", this.noBreedTicks);
			p_82689_1_.setInteger("CX", this.center.posX);
			p_82689_1_.setInteger("CY", this.center.posY);
			p_82689_1_.setInteger("CZ", this.center.posZ);
			p_82689_1_.setInteger("ACX", this.centerHelper.posX);
			p_82689_1_.setInteger("ACY", this.centerHelper.posY);
			p_82689_1_.setInteger("ACZ", this.centerHelper.posZ);
			NBTTagList var2 = new NBTTagList();
			IEnumerator var3 = this.villageDoorInfoList.GetEnumerator();

			while(var3.MoveNext())
			{
				VillageDoorInfo var4 = (VillageDoorInfo)var3.Current;
				NBTTagCompound var5 = new NBTTagCompound();
				var5.setInteger("X", var4.posX);
				var5.setInteger("Y", var4.posY);
				var5.setInteger("Z", var4.posZ);
				var5.setInteger("IDX", var4.insideDirectionX);
				var5.setInteger("IDZ", var4.insideDirectionZ);
				var5.setInteger("TS", var4.lastActivityTimestamp);
				var2.appendTag(var5);
			}

			p_82689_1_.setTag("Doors", var2);
			NBTTagList var7 = new NBTTagList();
			IEnumerator var8 = this.playerReputation.Keys.GetEnumerator();

			while(var8.MoveNext())
			{
				string var9 = (string)var8.Current;
				NBTTagCompound var6 = new NBTTagCompound();
				var6.setString("Name", var9);
				var6.setInteger("S", (int)((int?)this.playerReputation.get(var9)));
				var7.appendTag(var6);
			}

			p_82689_1_.setTag("Players", var7);
		}

///    
///     <summary> * Prevent villager breeding for a fixed interval of time </summary>
///     
		public virtual void endMatingSeason()
		{
			this.noBreedTicks = this.tickCounter;
		}

///    
///     <summary> * Return whether villagers mating refractory period has passed </summary>
///     
		public virtual bool isMatingSeason()
		{
			get
			{
				return this.noBreedTicks == 0 || this.tickCounter - this.noBreedTicks >= 3600;
			}
		}

		public virtual int DefaultPlayerReputation
		{
			set
			{
				IEnumerator var2 = this.playerReputation.Keys.GetEnumerator();
	
				while(var2.MoveNext())
				{
					string var3 = (string)var2.Current;
					this.setReputationForPlayer(var3, value);
				}
			}
		}

		internal class VillageAgressor
		{
			public EntityLivingBase agressor;
			public int agressionTime;
			

			internal VillageAgressor(EntityLivingBase p_i1674_2_, int p_i1674_3_)
			{
				this.agressor = p_i1674_2_;
				this.agressionTime = p_i1674_3_;
			}
		}
	}

}