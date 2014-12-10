using System;
using System.Collections;

namespace DotCraftCore.Village
{

	using BlockDoor = DotCraftCore.block.BlockDoor;
	using Blocks = DotCraftCore.init.Blocks;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using NBTTagList = DotCraftCore.nbt.NBTTagList;
	using ChunkCoordinates = DotCraftCore.Util.ChunkCoordinates;
	using World = DotCraftCore.World.World;
	using WorldSavedData = DotCraftCore.World.WorldSavedData;

	public class VillageCollection : WorldSavedData
	{
		private World worldObj;

///    
///     <summary> * This is a black hole. You can add data to this list through a public interface, but you can't query that
///     * information in any way and it's not used internally either. </summary>
///     
		private readonly IList villagerPositionsList = new ArrayList();
		private readonly IList newDoors = new ArrayList();
		private readonly IList villageList = new ArrayList();
		private int tickCounter;
		private const string __OBFID = "CL_00001635";

		public VillageCollection(string p_i1677_1_) : base(p_i1677_1_)
		{
		}

		public VillageCollection(World p_i1678_1_) : base("villages")
		{
			this.worldObj = p_i1678_1_;
			this.markDirty();
		}

		public virtual void func_82566_a(World p_82566_1_)
		{
			this.worldObj = p_82566_1_;
			IEnumerator var2 = this.villageList.GetEnumerator();

			while(var2.MoveNext())
			{
				Village var3 = (Village)var2.Current;
				var3.func_82691_a(p_82566_1_);
			}
		}

///    
///     <summary> * This is a black hole. You can add data to this list through a public interface, but you can't query that
///     * information in any way and it's not used internally either. </summary>
///     
		public virtual void addVillagerPosition(int p_75551_1_, int p_75551_2_, int p_75551_3_)
		{
			if(this.villagerPositionsList.Count <= 64)
			{
				if(!this.isVillagerPositionPresent(p_75551_1_, p_75551_2_, p_75551_3_))
				{
					this.villagerPositionsList.Add(new ChunkCoordinates(p_75551_1_, p_75551_2_, p_75551_3_));
				}
			}
		}

///    
///     <summary> * Runs a single tick for the village collection </summary>
///     
		public virtual void tick()
		{
			++this.tickCounter;
			IEnumerator var1 = this.villageList.GetEnumerator();

			while(var1.MoveNext())
			{
				Village var2 = (Village)var1.Current;
				var2.tick(this.tickCounter);
			}

			this.removeAnnihilatedVillages();
			this.dropOldestVillagerPosition();
			this.addNewDoorsToVillageOrCreateVillage();

			if(this.tickCounter % 400 == 0)
			{
				this.markDirty();
			}
		}

		private void removeAnnihilatedVillages()
		{
			IEnumerator var1 = this.villageList.GetEnumerator();

			while(var1.MoveNext())
			{
				Village var2 = (Village)var1.Current;

				if(var2.Annihilated)
				{
					var1.remove();
					this.markDirty();
				}
			}
		}

///    
///     <summary> * Get a list of villages. </summary>
///     
		public virtual IList VillageList
		{
			get
			{
				return this.villageList;
			}
		}

///    
///     <summary> * Finds the nearest village, but only the given coordinates are withing it's bounding box plus the given the
///     * distance. </summary>
///     
		public virtual Village findNearestVillage(int p_75550_1_, int p_75550_2_, int p_75550_3_, int p_75550_4_)
		{
			Village var5 = null;
			float var6 = float.MaxValue;
			IEnumerator var7 = this.villageList.GetEnumerator();

			while(var7.MoveNext())
			{
				Village var8 = (Village)var7.Current;
				float var9 = var8.Center.getDistanceSquared(p_75550_1_, p_75550_2_, p_75550_3_);

				if(var9 < var6)
				{
					float var10 = (float)(p_75550_4_ + var8.VillageRadius);

					if(var9 <= var10 * var10)
					{
						var5 = var8;
						var6 = var9;
					}
				}
			}

			return var5;
		}

		private void dropOldestVillagerPosition()
		{
			if(!this.villagerPositionsList.Count == 0)
			{
				this.addUnassignedWoodenDoorsAroundToNewDoorsList((ChunkCoordinates)this.villagerPositionsList.remove(0));
			}
		}

		private void addNewDoorsToVillageOrCreateVillage()
		{
			int var1 = 0;

			while(var1 < this.newDoors.Count)
			{
				VillageDoorInfo var2 = (VillageDoorInfo)this.newDoors.get(var1);
				bool var3 = false;
				IEnumerator var4 = this.villageList.GetEnumerator();

				while(true)
				{
					if(var4.MoveNext())
					{
						Village var5 = (Village)var4.Current;
						int var6 = (int)var5.Center.getDistanceSquared(var2.posX, var2.posY, var2.posZ);
						int var7 = 32 + var5.VillageRadius;

						if(var6 > var7 * var7)
						{
							continue;
						}

						var5.addVillageDoorInfo(var2);
						var3 = true;
					}

					if(!var3)
					{
						Village var8 = new Village(this.worldObj);
						var8.addVillageDoorInfo(var2);
						this.villageList.Add(var8);
						this.markDirty();
					}

					++var1;
					break;
				}
			}

			this.newDoors.Clear();
		}

		private void addUnassignedWoodenDoorsAroundToNewDoorsList(ChunkCoordinates p_75546_1_)
		{
			sbyte var2 = 16;
			sbyte var3 = 4;
			sbyte var4 = 16;

			for(int var5 = p_75546_1_.posX - var2; var5 < p_75546_1_.posX + var2; ++var5)
			{
				for(int var6 = p_75546_1_.posY - var3; var6 < p_75546_1_.posY + var3; ++var6)
				{
					for(int var7 = p_75546_1_.posZ - var4; var7 < p_75546_1_.posZ + var4; ++var7)
					{
						if(this.isWoodenDoorAt(var5, var6, var7))
						{
							VillageDoorInfo var8 = this.getVillageDoorAt(var5, var6, var7);

							if(var8 == null)
							{
								this.addDoorToNewListIfAppropriate(var5, var6, var7);
							}
							else
							{
								var8.lastActivityTimestamp = this.tickCounter;
							}
						}
					}
				}
			}
		}

		private VillageDoorInfo getVillageDoorAt(int p_75547_1_, int p_75547_2_, int p_75547_3_)
		{
			IEnumerator var4 = this.newDoors.GetEnumerator();
			VillageDoorInfo var5;

			do
			{
				if(!var4.MoveNext())
				{
					var4 = this.villageList.GetEnumerator();
					VillageDoorInfo var6;

					do
					{
						if(!var4.MoveNext())
						{
							return null;
						}

						Village var7 = (Village)var4.Current;
						var6 = var7.getVillageDoorAt(p_75547_1_, p_75547_2_, p_75547_3_);
					}
					while(var6 == null);

					return var6;
				}

				var5 = (VillageDoorInfo)var4.Current;
			}
			while(var5.posX != p_75547_1_ || var5.posZ != p_75547_3_ || Math.Abs(var5.posY - p_75547_2_) > 1);

			return var5;
		}

		private void addDoorToNewListIfAppropriate(int p_75542_1_, int p_75542_2_, int p_75542_3_)
		{
			int var4 = ((BlockDoor)Blocks.wooden_door).func_150013_e(this.worldObj, p_75542_1_, p_75542_2_, p_75542_3_);
			int var5;
			int var6;

			if(var4 != 0 && var4 != 2)
			{
				var5 = 0;

				for(var6 = -5; var6 < 0; ++var6)
				{
					if(this.worldObj.canBlockSeeTheSky(p_75542_1_, p_75542_2_, p_75542_3_ + var6))
					{
						--var5;
					}
				}

				for(var6 = 1; var6 <= 5; ++var6)
				{
					if(this.worldObj.canBlockSeeTheSky(p_75542_1_, p_75542_2_, p_75542_3_ + var6))
					{
						++var5;
					}
				}

				if(var5 != 0)
				{
					this.newDoors.Add(new VillageDoorInfo(p_75542_1_, p_75542_2_, p_75542_3_, 0, var5 > 0 ? -2 : 2, this.tickCounter));
				}
			}
			else
			{
				var5 = 0;

				for(var6 = -5; var6 < 0; ++var6)
				{
					if(this.worldObj.canBlockSeeTheSky(p_75542_1_ + var6, p_75542_2_, p_75542_3_))
					{
						--var5;
					}
				}

				for(var6 = 1; var6 <= 5; ++var6)
				{
					if(this.worldObj.canBlockSeeTheSky(p_75542_1_ + var6, p_75542_2_, p_75542_3_))
					{
						++var5;
					}
				}

				if(var5 != 0)
				{
					this.newDoors.Add(new VillageDoorInfo(p_75542_1_, p_75542_2_, p_75542_3_, var5 > 0 ? -2 : 2, 0, this.tickCounter));
				}
			}
		}

		private bool isVillagerPositionPresent(int p_75548_1_, int p_75548_2_, int p_75548_3_)
		{
			IEnumerator var4 = this.villagerPositionsList.GetEnumerator();
			ChunkCoordinates var5;

			do
			{
				if(!var4.MoveNext())
				{
					return false;
				}

				var5 = (ChunkCoordinates)var4.Current;
			}
			while(var5.posX != p_75548_1_ || var5.posY != p_75548_2_ || var5.posZ != p_75548_3_);

			return true;
		}

		private bool isWoodenDoorAt(int p_75541_1_, int p_75541_2_, int p_75541_3_)
		{
			return this.worldObj.getBlock(p_75541_1_, p_75541_2_, p_75541_3_) == Blocks.wooden_door;
		}

///    
///     <summary> * reads in data from the NBTTagCompound into this MapDataBase </summary>
///     
		public virtual void readFromNBT(NBTTagCompound p_76184_1_)
		{
			this.tickCounter = p_76184_1_.getInteger("Tick");
			NBTTagList var2 = p_76184_1_.getTagList("Villages", 10);

			for(int var3 = 0; var3 < var2.tagCount(); ++var3)
			{
				NBTTagCompound var4 = var2.getCompoundTagAt(var3);
				Village var5 = new Village();
				var5.readVillageDataFromNBT(var4);
				this.villageList.Add(var5);
			}
		}

///    
///     <summary> * write data to NBTTagCompound from this MapDataBase, similar to Entities and TileEntities </summary>
///     
		public virtual void writeToNBT(NBTTagCompound p_76187_1_)
		{
			p_76187_1_.setInteger("Tick", this.tickCounter);
			NBTTagList var2 = new NBTTagList();
			IEnumerator var3 = this.villageList.GetEnumerator();

			while(var3.MoveNext())
			{
				Village var4 = (Village)var3.Current;
				NBTTagCompound var5 = new NBTTagCompound();
				var4.writeVillageDataToNBT(var5);
				var2.appendTag(var5);
			}

			p_76187_1_.setTag("Villages", var2);
		}
	}

}