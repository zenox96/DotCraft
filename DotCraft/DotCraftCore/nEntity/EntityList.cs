using System;
using System.Collections;

namespace DotCraftCore.nEntity
{

	using EntityMinecartMobSpawner = DotCraftCore.nEntity.nAI.EntityMinecartMobSpawner;
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
	using EntityItemFrame = DotCraftCore.nEntity.nItem.EntityItemFrame;
	using EntityMinecartChest = DotCraftCore.nEntity.nItem.EntityMinecartChest;
	using EntityMinecartEmpty = DotCraftCore.nEntity.nItem.EntityMinecartEmpty;
	using EntityMinecartFurnace = DotCraftCore.nEntity.nItem.EntityMinecartFurnace;
	using EntityMinecartHopper = DotCraftCore.nEntity.nItem.EntityMinecartHopper;
	using EntityMinecartTNT = DotCraftCore.nEntity.nItem.EntityMinecartTNT;
	using EntityPainting = DotCraftCore.nEntity.nItem.EntityPainting;
	using EntityTNTPrimed = DotCraftCore.nEntity.nItem.EntityTNTPrimed;
	using EntityXPOrb = DotCraftCore.nEntity.nItem.EntityXPOrb;
	using EntityBlaze = DotCraftCore.nEntity.nMonster.EntityBlaze;
	using EntityCaveSpider = DotCraftCore.nEntity.nMonster.EntityCaveSpider;
	using EntityCreeper = DotCraftCore.nEntity.nMonster.EntityCreeper;
	using EntityEnderman = DotCraftCore.nEntity.nMonster.EntityEnderman;
	using EntityGhast = DotCraftCore.nEntity.nMonster.EntityGhast;
	using EntityGiantZombie = DotCraftCore.nEntity.nMonster.EntityGiantZombie;
	using EntityIronGolem = DotCraftCore.nEntity.nMonster.EntityIronGolem;
	using EntityMagmaCube = DotCraftCore.nEntity.nMonster.EntityMagmaCube;
	using EntityMob = DotCraftCore.nEntity.nMonster.EntityMob;
	using EntityPigZombie = DotCraftCore.nEntity.nMonster.EntityPigZombie;
	using EntitySilverfish = DotCraftCore.nEntity.nMonster.EntitySilverfish;
	using EntitySkeleton = DotCraftCore.nEntity.nMonster.EntitySkeleton;
	using EntitySlime = DotCraftCore.nEntity.nMonster.EntitySlime;
	using EntitySnowman = DotCraftCore.nEntity.nMonster.EntitySnowman;
	using EntitySpider = DotCraftCore.nEntity.nMonster.EntitySpider;
	using EntityWitch = DotCraftCore.nEntity.nMonster.EntityWitch;
	using EntityZombie = DotCraftCore.nEntity.nMonster.EntityZombie;
	using EntityBat = DotCraftCore.nEntity.nPassive.EntityBat;
	using EntityChicken = DotCraftCore.nEntity.nPassive.EntityChicken;
	using EntityCow = DotCraftCore.nEntity.nPassive.EntityCow;
	using EntityHorse = DotCraftCore.nEntity.nPassive.EntityHorse;
	using EntityMooshroom = DotCraftCore.nEntity.nPassive.EntityMooshroom;
	using EntityOcelot = DotCraftCore.nEntity.nPassive.EntityOcelot;
	using EntityPig = DotCraftCore.nEntity.nPassive.EntityPig;
	using EntitySheep = DotCraftCore.nEntity.nPassive.EntitySheep;
	using EntitySquid = DotCraftCore.nEntity.nPassive.EntitySquid;
	using EntityVillager = DotCraftCore.nEntity.nPassive.EntityVillager;
	using EntityWolf = DotCraftCore.nEntity.nPassive.EntityWolf;
	using EntityArrow = DotCraftCore.nEntity.nProjectile.EntityArrow;
	using EntityLargeFireball = DotCraftCore.nEntity.nProjectile.EntityLargeFireball;
	using EntityPotion = DotCraftCore.nEntity.nProjectile.EntityPotion;
	using EntitySmallFireball = DotCraftCore.nEntity.nProjectile.EntitySmallFireball;
	using EntitySnowball = DotCraftCore.nEntity.nProjectile.EntitySnowball;
	using EntityWitherSkull = DotCraftCore.nEntity.nProjectile.EntityWitherSkull;
	using NBTTagCompound = DotCraftCore.nNBT.NBTTagCompound;
	using StatBase = DotCraftCore.nStats.StatBase;
	using StatList = DotCraftCore.nStats.StatList;
	using World = DotCraftCore.nWorld.World;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public class EntityList
	{
		private static readonly Logger logger = LogManager.Logger;

	/// <summary> Provides a mapping between entity classes and a string  </summary>
		private static IDictionary stringToClassMapping = new Hashtable();

	/// <summary> Provides a mapping between a string and an entity classes  </summary>
		private static IDictionary classToStringMapping = new Hashtable();

	/// <summary> provides a mapping between an entityID and an Entity Class  </summary>
		private static IDictionary IDtoClassMapping = new Hashtable();

	/// <summary> provides a mapping between an Entity Class and an entity ID  </summary>
		private static IDictionary classToIDMapping = new Hashtable();

	/// <summary> Maps entity names to their numeric identifiers  </summary>
		private static IDictionary stringToIDMapping = new Hashtable();

	/// <summary> This is a HashMap of the Creative Entity Eggs/Spawners.  </summary>
		public static Hashtable entityEggs = new LinkedHashMap();
		

///    
///     <summary> * adds a mapping between Entity classes and both a string representation and an ID </summary>
///     
		private static void addMapping(Type p_75618_0_, string p_75618_1_, int p_75618_2_)
		{
			if (stringToClassMapping.ContainsKey(p_75618_1_))
			{
				throw new System.ArgumentException("ID is already registered: " + p_75618_1_);
			}
			else if (IDtoClassMapping.ContainsKey(Convert.ToInt32(p_75618_2_)))
			{
				throw new System.ArgumentException("ID is already registered: " + p_75618_2_);
			}
			else
			{
				stringToClassMapping.Add(p_75618_1_, p_75618_0_);
				classToStringMapping.Add(p_75618_0_, p_75618_1_);
				IDtoClassMapping.Add(Convert.ToInt32(p_75618_2_), p_75618_0_);
				classToIDMapping.Add(p_75618_0_, Convert.ToInt32(p_75618_2_));
				stringToIDMapping.Add(p_75618_1_, Convert.ToInt32(p_75618_2_));
			}
		}

///    
///     <summary> * Adds a entity mapping with egg info. </summary>
///     
		private static void addMapping(Type p_75614_0_, string p_75614_1_, int p_75614_2_, int p_75614_3_, int p_75614_4_)
		{
			addMapping(p_75614_0_, p_75614_1_, p_75614_2_);
			entityEggs.Add(Convert.ToInt32(p_75614_2_), new EntityList.EntityEggInfo(p_75614_2_, p_75614_3_, p_75614_4_));
		}

///    
///     <summary> * Create a new instance of an entity in the world by using the entity name. </summary>
///     
		public static Entity createEntityByName(string p_75620_0_, World p_75620_1_)
		{
			Entity var2 = null;

			try
			{
				Type var3 = (Class)stringToClassMapping[p_75620_0_];

				if (var3 != null)
				{
					var2 = (Entity)var3.GetConstructor(new Class[] {typeof(World)}).newInstance(new object[] {p_75620_1_});
				}
			}
			catch (Exception var4)
			{
				var4.printStackTrace();
			}

			return var2;
		}

///    
///     <summary> * create a new instance of an entity from NBT store </summary>
///     
		public static Entity createEntityFromNBT(NBTTagCompound p_75615_0_, World p_75615_1_)
		{
			Entity var2 = null;

			if ("Minecart".Equals(p_75615_0_.getString("id")))
			{
				switch (p_75615_0_.getInteger("Type"))
				{
					case 0:
						p_75615_0_.setString("id", "MinecartRideable");
						break;

					case 1:
						p_75615_0_.setString("id", "MinecartChest");
						break;

					case 2:
						p_75615_0_.setString("id", "MinecartFurnace");
					break;
				}

				p_75615_0_.removeTag("Type");
			}

			try
			{
				Type var3 = (Class)stringToClassMapping[p_75615_0_.getString("id")];

				if (var3 != null)
				{
					var2 = (Entity)var3.GetConstructor(new Class[] {typeof(World)}).newInstance(new object[] {p_75615_1_});
				}
			}
			catch (Exception var4)
			{
				var4.printStackTrace();
			}

			if (var2 != null)
			{
				var2.readFromNBT(p_75615_0_);
			}
			else
			{
				logger.warn("Skipping Entity with id " + p_75615_0_.getString("id"));
			}

			return var2;
		}

///    
///     <summary> * Create a new instance of an entity in the world by using an entity ID. </summary>
///     
		public static Entity createEntityByID(int p_75616_0_, World p_75616_1_)
		{
			Entity var2 = null;

			try
			{
				Type var3 = getClassFromID(p_75616_0_);

				if (var3 != null)
				{
					var2 = (Entity)var3.GetConstructor(new Class[] {typeof(World)}).newInstance(new object[] {p_75616_1_});
				}
			}
			catch (Exception var4)
			{
				var4.printStackTrace();
			}

			if (var2 == null)
			{
				logger.warn("Skipping Entity with id " + p_75616_0_);
			}

			return var2;
		}

///    
///     <summary> * gets the entityID of a specific entity </summary>
///     
		public static int getEntityID(Entity p_75619_0_)
		{
			Type var1 = p_75619_0_.GetType();
			return classToIDMapping.ContainsKey(var1) ? (int)((int?)classToIDMapping[var1]) : 0;
		}

///    
///     <summary> * Return the class assigned to this entity ID. </summary>
///     
		public static Type getClassFromID(int p_90035_0_)
		{
			return (Class)IDtoClassMapping[Convert.ToInt32(p_90035_0_)];
		}

///    
///     <summary> * Gets the string representation of a specific entity. </summary>
///     
		public static string getEntityString(Entity p_75621_0_)
		{
			return (string)classToStringMapping[p_75621_0_.GetType()];
		}

///    
///     <summary> * Finds the class using IDtoClassMapping and classToStringMapping </summary>
///     
		public static string getStringFromID(int p_75617_0_)
		{
			Type var1 = getClassFromID(p_75617_0_);
			return var1 != null ? (string)classToStringMapping[var1] : null;
		}

		public static void func_151514_a()
		{
		}

		public static Set func_151515_b()
		{
			return Collections.unmodifiableSet(stringToIDMapping.Keys);
		}

		static EntityList()
		{
			addMapping(typeof(EntityItem), "Item", 1);
			addMapping(typeof(EntityXPOrb), "XPOrb", 2);
			addMapping(typeof(EntityLeashKnot), "LeashKnot", 8);
			addMapping(typeof(EntityPainting), "Painting", 9);
			addMapping(typeof(EntityArrow), "Arrow", 10);
			addMapping(typeof(EntitySnowball), "Snowball", 11);
			addMapping(typeof(EntityLargeFireball), "Fireball", 12);
			addMapping(typeof(EntitySmallFireball), "SmallFireball", 13);
			addMapping(typeof(EntityEnderPearl), "ThrownEnderpearl", 14);
			addMapping(typeof(EntityEnderEye), "EyeOfEnderSignal", 15);
			addMapping(typeof(EntityPotion), "ThrownPotion", 16);
			addMapping(typeof(EntityExpBottle), "ThrownExpBottle", 17);
			addMapping(typeof(EntityItemFrame), "ItemFrame", 18);
			addMapping(typeof(EntityWitherSkull), "WitherSkull", 19);
			addMapping(typeof(EntityTNTPrimed), "PrimedTnt", 20);
			addMapping(typeof(EntityFallingBlock), "FallingSand", 21);
			addMapping(typeof(EntityFireworkRocket), "FireworksRocketEntity", 22);
			addMapping(typeof(EntityBoat), "Boat", 41);
			addMapping(typeof(EntityMinecartEmpty), "MinecartRideable", 42);
			addMapping(typeof(EntityMinecartChest), "MinecartChest", 43);
			addMapping(typeof(EntityMinecartFurnace), "MinecartFurnace", 44);
			addMapping(typeof(EntityMinecartTNT), "MinecartTNT", 45);
			addMapping(typeof(EntityMinecartHopper), "MinecartHopper", 46);
			addMapping(typeof(EntityMinecartMobSpawner), "MinecartSpawner", 47);
			addMapping(typeof(EntityMinecartCommandBlock), "MinecartCommandBlock", 40);
			addMapping(typeof(EntityLiving), "Mob", 48);
			addMapping(typeof(EntityMob), "Monster", 49);
			addMapping(typeof(EntityCreeper), "Creeper", 50, 894731, 0);
			addMapping(typeof(EntitySkeleton), "Skeleton", 51, 12698049, 4802889);
			addMapping(typeof(EntitySpider), "Spider", 52, 3419431, 11013646);
			addMapping(typeof(EntityGiantZombie), "Giant", 53);
			addMapping(typeof(EntityZombie), "Zombie", 54, 44975, 7969893);
			addMapping(typeof(EntitySlime), "Slime", 55, 5349438, 8306542);
			addMapping(typeof(EntityGhast), "Ghast", 56, 16382457, 12369084);
			addMapping(typeof(EntityPigZombie), "PigZombie", 57, 15373203, 5009705);
			addMapping(typeof(EntityEnderman), "Enderman", 58, 1447446, 0);
			addMapping(typeof(EntityCaveSpider), "CaveSpider", 59, 803406, 11013646);
			addMapping(typeof(EntitySilverfish), "Silverfish", 60, 7237230, 3158064);
			addMapping(typeof(EntityBlaze), "Blaze", 61, 16167425, 16775294);
			addMapping(typeof(EntityMagmaCube), "LavaSlime", 62, 3407872, 16579584);
			addMapping(typeof(EntityDragon), "EnderDragon", 63);
			addMapping(typeof(EntityWither), "WitherBoss", 64);
			addMapping(typeof(EntityBat), "Bat", 65, 4996656, 986895);
			addMapping(typeof(EntityWitch), "Witch", 66, 3407872, 5349438);
			addMapping(typeof(EntityPig), "Pig", 90, 15771042, 14377823);
			addMapping(typeof(EntitySheep), "Sheep", 91, 15198183, 16758197);
			addMapping(typeof(EntityCow), "Cow", 92, 4470310, 10592673);
			addMapping(typeof(EntityChicken), "Chicken", 93, 10592673, 16711680);
			addMapping(typeof(EntitySquid), "Squid", 94, 2243405, 7375001);
			addMapping(typeof(EntityWolf), "Wolf", 95, 14144467, 13545366);
			addMapping(typeof(EntityMooshroom), "MushroomCow", 96, 10489616, 12040119);
			addMapping(typeof(EntitySnowman), "SnowMan", 97);
			addMapping(typeof(EntityOcelot), "Ozelot", 98, 15720061, 5653556);
			addMapping(typeof(EntityIronGolem), "VillagerGolem", 99);
			addMapping(typeof(EntityHorse), "EntityHorse", 100, 12623485, 15656192);
			addMapping(typeof(EntityVillager), "Villager", 120, 5651507, 12422002);
			addMapping(typeof(EntityEnderCrystal), "EnderCrystal", 200);
		}

		public class EntityEggInfo
		{
			public readonly int spawnedID;
			public readonly int primaryColor;
			public readonly int secondaryColor;
			public readonly StatBase field_151512_d;
			public readonly StatBase field_151513_e;
			

			public EntityEggInfo(int p_i1583_1_, int p_i1583_2_, int p_i1583_3_)
			{
				this.spawnedID = p_i1583_1_;
				this.primaryColor = p_i1583_2_;
				this.secondaryColor = p_i1583_3_;
				this.field_151512_d = StatList.func_151182_a(this);
				this.field_151513_e = StatList.func_151176_b(this);
			}
		}
	}

}