using System.Collections;

namespace DotCraftCore.nTileEntity
{

	using Entity = DotCraftCore.entity.Entity;
	using EntityList = DotCraftCore.entity.EntityList;
	using EntityLiving = DotCraftCore.entity.EntityLiving;
	using EntityLivingBase = DotCraftCore.entity.EntityLivingBase;
	using IEntityLivingData = DotCraftCore.entity.IEntityLivingData;
	using NBTBase = DotCraftCore.nbt.NBTBase;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using NBTTagList = DotCraftCore.nbt.NBTTagList;
	using AxisAlignedBB = DotCraftCore.nUtil.AxisAlignedBB;
	using WeightedRandom = DotCraftCore.nUtil.WeightedRandom;
	using World = DotCraftCore.nWorld.World;

	public abstract class MobSpawnerBaseLogic
	{
	/// <summary> The delay to spawn.  </summary>
		public int spawnDelay = 20;
		private string mobID = "Pig";

	/// <summary> List of minecart to spawn.  </summary>
		private IList minecartToSpawn;
		private MobSpawnerBaseLogic.WeightedRandomMinecart randomMinecart;
		public double field_98287_c;
		public double field_98284_d;
		private int minSpawnDelay = 200;
		private int maxSpawnDelay = 800;

	/// <summary> A counter for spawn tries.  </summary>
		private int spawnCount = 4;
		private Entity field_98291_j;
		private int maxNearbyEntities = 6;

	/// <summary> The distance from which a player activates the spawner.  </summary>
		private int activatingRangeFromPlayer = 16;

	/// <summary> The range coefficient for spawning entities around.  </summary>
		private int spawnRange = 4;
		

///    
///     <summary> * Gets the entity name that should be spawned. </summary>
///     
		public virtual string EntityNameToSpawn
		{
			get
			{
				if(this.RandomMinecart == null)
				{
					if(this.mobID.Equals("Minecart"))
					{
						this.mobID = "MinecartRideable";
					}
	
					return this.mobID;
				}
				else
				{
					return this.RandomMinecart.minecartName;
				}
			}
		}

		public virtual string MobID
		{
			set
			{
				this.mobID = value;
			}
		}

///    
///     <summary> * Returns true if there's a player close enough to this mob spawner to activate it. </summary>
///     
		public virtual bool canRun()
		{
			return this.SpawnerWorld.getClosestPlayer((double)this.SpawnerX + 0.5D, (double)this.SpawnerY + 0.5D, (double)this.SpawnerZ + 0.5D, (double)this.activatingRangeFromPlayer) != null;
		}

		public virtual void updateSpawner()
		{
			if(this.canRun())
			{
				double var5;

				if(this.SpawnerWorld.isClient)
				{
					double var1 = (double)((float)this.SpawnerX + this.SpawnerWorld.rand.nextFloat());
					double var3 = (double)((float)this.SpawnerY + this.SpawnerWorld.rand.nextFloat());
					var5 = (double)((float)this.SpawnerZ + this.SpawnerWorld.rand.nextFloat());
					this.SpawnerWorld.spawnParticle("smoke", var1, var3, var5, 0.0D, 0.0D, 0.0D);
					this.SpawnerWorld.spawnParticle("flame", var1, var3, var5, 0.0D, 0.0D, 0.0D);

					if(this.spawnDelay > 0)
					{
						--this.spawnDelay;
					}

					this.field_98284_d = this.field_98287_c;
					this.field_98287_c = (this.field_98287_c + (double)(1000.0F / ((float)this.spawnDelay + 200.0F))) % 360.0D;
				}
				else
				{
					if(this.spawnDelay == -1)
					{
						this.resetTimer();
					}

					if(this.spawnDelay > 0)
					{
						--this.spawnDelay;
						return;
					}

					bool var12 = false;

					for(int var2 = 0; var2 < this.spawnCount; ++var2)
					{
						Entity var13 = EntityList.createEntityByName(this.EntityNameToSpawn, this.SpawnerWorld);

						if(var13 == null)
						{
							return;
						}

						int var4 = this.SpawnerWorld.getEntitiesWithinAABB(var13.GetType(), AxisAlignedBB.getBoundingBox((double)this.SpawnerX, (double)this.SpawnerY, (double)this.SpawnerZ, (double)(this.SpawnerX + 1), (double)(this.SpawnerY + 1), (double)(this.SpawnerZ + 1)).expand((double)(this.spawnRange * 2), 4.0D, (double)(this.spawnRange * 2))).size();

						if(var4 >= this.maxNearbyEntities)
						{
							this.resetTimer();
							return;
						}

						var5 = (double)this.SpawnerX + (this.SpawnerWorld.rand.NextDouble() - this.SpawnerWorld.rand.NextDouble()) * (double)this.spawnRange;
						double var7 = (double)(this.SpawnerY + this.SpawnerWorld.rand.Next(3) - 1);
						double var9 = (double)this.SpawnerZ + (this.SpawnerWorld.rand.NextDouble() - this.SpawnerWorld.rand.NextDouble()) * (double)this.spawnRange;
						EntityLiving var11 = var13 is EntityLiving ? (EntityLiving)var13 : null;
						var13.setLocationAndAngles(var5, var7, var9, this.SpawnerWorld.rand.nextFloat() * 360.0F, 0.0F);

						if(var11 == null || var11.CanSpawnHere)
						{
							this.func_98265_a(var13);
							this.SpawnerWorld.playAuxSFX(2004, this.SpawnerX, this.SpawnerY, this.SpawnerZ, 0);

							if(var11 != null)
							{
								var11.spawnExplosionParticle();
							}

							var12 = true;
						}
					}

					if(var12)
					{
						this.resetTimer();
					}
				}
			}
		}

		public virtual Entity func_98265_a(Entity p_98265_1_)
		{
			if(this.RandomMinecart != null)
			{
				NBTTagCompound var2 = new NBTTagCompound();
				p_98265_1_.writeToNBTOptional(var2);
				IEnumerator var3 = this.RandomMinecart.field_98222_b.func_150296_c().GetEnumerator();

				while(var3.MoveNext())
				{
					string var4 = (string)var3.Current;
					NBTBase var5 = this.RandomMinecart.field_98222_b.getTag(var4);
					var2.setTag(var4, var5.copy());
				}

				p_98265_1_.readFromNBT(var2);

				if(p_98265_1_.worldObj != null)
				{
					p_98265_1_.worldObj.spawnEntityInWorld(p_98265_1_);
				}

				NBTTagCompound var11;

				for(Entity var10 = p_98265_1_; var2.func_150297_b("Riding", 10); var2 = var11)
				{
					var11 = var2.getCompoundTag("Riding");
					Entity var12 = EntityList.createEntityByName(var11.getString("id"), p_98265_1_.worldObj);

					if(var12 != null)
					{
						NBTTagCompound var6 = new NBTTagCompound();
						var12.writeToNBTOptional(var6);
						IEnumerator var7 = var11.func_150296_c().GetEnumerator();

						while(var7.MoveNext())
						{
							string var8 = (string)var7.Current;
							NBTBase var9 = var11.getTag(var8);
							var6.setTag(var8, var9.copy());
						}

						var12.readFromNBT(var6);
						var12.setLocationAndAngles(var10.posX, var10.posY, var10.posZ, var10.rotationYaw, var10.rotationPitch);

						if(p_98265_1_.worldObj != null)
						{
							p_98265_1_.worldObj.spawnEntityInWorld(var12);
						}

						var10.mountEntity(var12);
					}

					var10 = var12;
				}
			}
			else if(p_98265_1_ is EntityLivingBase && p_98265_1_.worldObj != null)
			{
				((EntityLiving)p_98265_1_).onSpawnWithEgg((IEntityLivingData)null);
				this.SpawnerWorld.spawnEntityInWorld(p_98265_1_);
			}

			return p_98265_1_;
		}

		private void resetTimer()
		{
			if(this.maxSpawnDelay <= this.minSpawnDelay)
			{
				this.spawnDelay = this.minSpawnDelay;
			}
			else
			{
				int var10003 = this.maxSpawnDelay - this.minSpawnDelay;
				this.spawnDelay = this.minSpawnDelay + this.SpawnerWorld.rand.Next(var10003);
			}

			if(this.minecartToSpawn != null && this.minecartToSpawn.Count > 0)
			{
				this.RandomMinecart = (MobSpawnerBaseLogic.WeightedRandomMinecart)WeightedRandom.getRandomItem(this.SpawnerWorld.rand, this.minecartToSpawn);
			}

			this.func_98267_a(1);
		}

		public virtual void readFromNBT(NBTTagCompound p_98270_1_)
		{
			this.mobID = p_98270_1_.getString("EntityId");
			this.spawnDelay = p_98270_1_.getShort("Delay");

			if(p_98270_1_.func_150297_b("SpawnPotentials", 9))
			{
				this.minecartToSpawn = new ArrayList();
				NBTTagList var2 = p_98270_1_.getTagList("SpawnPotentials", 10);

				for(int var3 = 0; var3 < var2.tagCount(); ++var3)
				{
					this.minecartToSpawn.Add(new MobSpawnerBaseLogic.WeightedRandomMinecart(var2.getCompoundTagAt(var3)));
				}
			}
			else
			{
				this.minecartToSpawn = null;
			}

			if(p_98270_1_.func_150297_b("SpawnData", 10))
			{
				this.RandomMinecart = new MobSpawnerBaseLogic.WeightedRandomMinecart(p_98270_1_.getCompoundTag("SpawnData"), this.mobID);
			}
			else
			{
				this.RandomMinecart = (MobSpawnerBaseLogic.WeightedRandomMinecart)null;
			}

			if(p_98270_1_.func_150297_b("MinSpawnDelay", 99))
			{
				this.minSpawnDelay = p_98270_1_.getShort("MinSpawnDelay");
				this.maxSpawnDelay = p_98270_1_.getShort("MaxSpawnDelay");
				this.spawnCount = p_98270_1_.getShort("SpawnCount");
			}

			if(p_98270_1_.func_150297_b("MaxNearbyEntities", 99))
			{
				this.maxNearbyEntities = p_98270_1_.getShort("MaxNearbyEntities");
				this.activatingRangeFromPlayer = p_98270_1_.getShort("RequiredPlayerRange");
			}

			if(p_98270_1_.func_150297_b("SpawnRange", 99))
			{
				this.spawnRange = p_98270_1_.getShort("SpawnRange");
			}

			if(this.SpawnerWorld != null && this.SpawnerWorld.isClient)
			{
				this.field_98291_j = null;
			}
		}

		public virtual void writeToNBT(NBTTagCompound p_98280_1_)
		{
			p_98280_1_.setString("EntityId", this.EntityNameToSpawn);
			p_98280_1_.setShort("Delay", (short)this.spawnDelay);
			p_98280_1_.setShort("MinSpawnDelay", (short)this.minSpawnDelay);
			p_98280_1_.setShort("MaxSpawnDelay", (short)this.maxSpawnDelay);
			p_98280_1_.setShort("SpawnCount", (short)this.spawnCount);
			p_98280_1_.setShort("MaxNearbyEntities", (short)this.maxNearbyEntities);
			p_98280_1_.setShort("RequiredPlayerRange", (short)this.activatingRangeFromPlayer);
			p_98280_1_.setShort("SpawnRange", (short)this.spawnRange);

			if(this.RandomMinecart != null)
			{
				p_98280_1_.setTag("SpawnData", this.RandomMinecart.field_98222_b.copy());
			}

			if(this.RandomMinecart != null || this.minecartToSpawn != null && this.minecartToSpawn.Count > 0)
			{
				NBTTagList var2 = new NBTTagList();

				if(this.minecartToSpawn != null && this.minecartToSpawn.Count > 0)
				{
					IEnumerator var3 = this.minecartToSpawn.GetEnumerator();

					while(var3.MoveNext())
					{
						MobSpawnerBaseLogic.WeightedRandomMinecart var4 = (MobSpawnerBaseLogic.WeightedRandomMinecart)var3.Current;
						var2.appendTag(var4.func_98220_a());
					}
				}
				else
				{
					var2.appendTag(this.RandomMinecart.func_98220_a());
				}

				p_98280_1_.setTag("SpawnPotentials", var2);
			}
		}

		public virtual Entity func_98281_h()
		{
			if(this.field_98291_j == null)
			{
				Entity var1 = EntityList.createEntityByName(this.EntityNameToSpawn, (World)null);
				var1 = this.func_98265_a(var1);
				this.field_98291_j = var1;
			}

			return this.field_98291_j;
		}

///    
///     <summary> * Sets the delay to minDelay if parameter given is 1, else return false. </summary>
///     
		public virtual bool DelayToMin
		{
			set
			{
				if(value == 1 && this.SpawnerWorld.isClient)
				{
					this.spawnDelay = this.minSpawnDelay;
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		public virtual MobSpawnerBaseLogic.WeightedRandomMinecart RandomMinecart
		{
			get
			{
				return this.randomMinecart;
			}
			set
			{
				this.randomMinecart = value;
			}
		}


		public abstract void func_98267_a(int p_98267_1_);

		public abstract World SpawnerWorld {get;}

		public abstract int SpawnerX {get;}

		public abstract int SpawnerY {get;}

		public abstract int SpawnerZ {get;}

		public class WeightedRandomMinecart : WeightedRandom.Item
		{
			public readonly NBTTagCompound field_98222_b;
			public readonly string minecartName;
			

			public WeightedRandomMinecart(NBTTagCompound p_i1945_2_) : base(p_i1945_2_.getInteger("Weight"))
			{
				NBTTagCompound var3 = p_i1945_2_.getCompoundTag("Properties");
				string var4 = p_i1945_2_.getString("Type");

				if(var4.Equals("Minecart"))
				{
					if(var3 != null)
					{
						switch (var3.getInteger("Type"))
						{
							case 0:
								var4 = "MinecartRideable";
								break;

							case 1:
								var4 = "MinecartChest";
								break;

							case 2:
								var4 = "MinecartFurnace";
							break;
						}
					}
					else
					{
						var4 = "MinecartRideable";
					}
				}

				this.field_98222_b = var3;
				this.minecartName = var4;
			}

			public WeightedRandomMinecart(NBTTagCompound p_i1946_2_, string p_i1946_3_) : base(1)
			{

				if(p_i1946_3_.Equals("Minecart"))
				{
					if(p_i1946_2_ != null)
					{
						switch (p_i1946_2_.getInteger("Type"))
						{
							case 0:
								p_i1946_3_ = "MinecartRideable";
								break;

							case 1:
								p_i1946_3_ = "MinecartChest";
								break;

							case 2:
								p_i1946_3_ = "MinecartFurnace";
							break;
						}
					}
					else
					{
						p_i1946_3_ = "MinecartRideable";
					}
				}

				this.field_98222_b = p_i1946_2_;
				this.minecartName = p_i1946_3_;
			}

			public virtual NBTTagCompound func_98220_a()
			{
				NBTTagCompound var1 = new NBTTagCompound();
				var1.setTag("Properties", this.field_98222_b);
				var1.setString("Type", this.minecartName);
				var1.setInteger("Weight", this.itemWeight);
				return var1;
			}
		}
	}

}