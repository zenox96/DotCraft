using System;
using System.Collections;

namespace DotCraftCore.nWorld.nBiome
{

	using Sets = com.google.common.collect.Sets;
	using Block = DotCraftCore.nBlock.Block;
	using BlockFlower = DotCraftCore.nBlock.BlockFlower;
	using Material = DotCraftCore.nBlock.nMaterial.Material;
	using EnumCreatureType = DotCraftCore.entity.EnumCreatureType;
	using EntityCreeper = DotCraftCore.entity.monster.EntityCreeper;
	using EntityEnderman = DotCraftCore.entity.monster.EntityEnderman;
	using EntitySkeleton = DotCraftCore.entity.monster.EntitySkeleton;
	using EntitySlime = DotCraftCore.entity.monster.EntitySlime;
	using EntitySpider = DotCraftCore.entity.monster.EntitySpider;
	using EntityWitch = DotCraftCore.entity.monster.EntityWitch;
	using EntityZombie = DotCraftCore.entity.monster.EntityZombie;
	using EntityBat = DotCraftCore.entity.passive.EntityBat;
	using EntityChicken = DotCraftCore.entity.passive.EntityChicken;
	using EntityCow = DotCraftCore.entity.passive.EntityCow;
	using EntityPig = DotCraftCore.entity.passive.EntityPig;
	using EntitySheep = DotCraftCore.entity.passive.EntitySheep;
	using EntitySquid = DotCraftCore.entity.passive.EntitySquid;
	using Blocks = DotCraftCore.init.Blocks;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using WeightedRandom = DotCraftCore.nUtil.WeightedRandom;
	using ColorizerFoliage = DotCraftCore.nWorld.ColorizerFoliage;
	using ColorizerGrass = DotCraftCore.nWorld.ColorizerGrass;
	using World = DotCraftCore.nWorld.World;
	using NoiseGeneratorPerlin = DotCraftCore.nWorld.nGen.NoiseGeneratorPerlin;
	using WorldGenAbstractTree = DotCraftCore.nWorld.nGen.nFeature.WorldGenAbstractTree;
	using WorldGenBigTree = DotCraftCore.nWorld.nGen.nFeature.WorldGenBigTree;
	using WorldGenDoublePlant = DotCraftCore.nWorld.nGen.nFeature.WorldGenDoublePlant;
	using WorldGenSwamp = DotCraftCore.nWorld.nGen.nFeature.WorldGenSwamp;
	using WorldGenTallGrass = DotCraftCore.nWorld.nGen.nFeature.WorldGenTallGrass;
	using WorldGenTrees = DotCraftCore.nWorld.nGen.nFeature.WorldGenTrees;
	using WorldGenerator = DotCraftCore.nWorld.nGen.nFeature.WorldGenerator;
	using LogManager = org.apache.logging.log4j.LogManager;
	using Logger = org.apache.logging.log4j.Logger;

	public abstract class BiomeGenBase
	{
		private static readonly Logger logger = LogManager.Logger;
		protected internal static readonly BiomeGenBase.Height field_150596_a = new BiomeGenBase.Height(0.1F, 0.2F);
		protected internal static readonly BiomeGenBase.Height field_150594_b = new BiomeGenBase.Height(-0.5F, 0.0F);
		protected internal static readonly BiomeGenBase.Height field_150595_c = new BiomeGenBase.Height(-1.0F, 0.1F);
		protected internal static readonly BiomeGenBase.Height field_150592_d = new BiomeGenBase.Height(-1.8F, 0.1F);
		protected internal static readonly BiomeGenBase.Height field_150593_e = new BiomeGenBase.Height(0.125F, 0.05F);
		protected internal static readonly BiomeGenBase.Height field_150590_f = new BiomeGenBase.Height(0.2F, 0.2F);
		protected internal static readonly BiomeGenBase.Height field_150591_g = new BiomeGenBase.Height(0.45F, 0.3F);
		protected internal static readonly BiomeGenBase.Height field_150602_h = new BiomeGenBase.Height(1.5F, 0.025F);
		protected internal static readonly BiomeGenBase.Height field_150603_i = new BiomeGenBase.Height(1.0F, 0.5F);
		protected internal static readonly BiomeGenBase.Height field_150600_j = new BiomeGenBase.Height(0.0F, 0.025F);
		protected internal static readonly BiomeGenBase.Height field_150601_k = new BiomeGenBase.Height(0.1F, 0.8F);
		protected internal static readonly BiomeGenBase.Height field_150598_l = new BiomeGenBase.Height(0.2F, 0.3F);
		protected internal static readonly BiomeGenBase.Height field_150599_m = new BiomeGenBase.Height(-0.2F, 0.1F);

	/// <summary> An array of all the biomes, indexed by biome id.  </summary>
		private static readonly BiomeGenBase[] biomeList = new BiomeGenBase[256];
		public static readonly Set field_150597_n = Sets.newHashSet();
		public static readonly BiomeGenBase ocean = (new BiomeGenOcean(0)).setColor(112).setBiomeName("Ocean").func_150570_a(field_150595_c);
		public static readonly BiomeGenBase plains = (new BiomeGenPlains(1)).setColor(9286496).setBiomeName("Plains");
		public static readonly BiomeGenBase desert = (new BiomeGenDesert(2)).setColor(16421912).setBiomeName("Desert").setDisableRain().setTemperatureRainfall(2.0F, 0.0F).func_150570_a(field_150593_e);
		public static readonly BiomeGenBase extremeHills = (new BiomeGenHills(3, false)).setColor(6316128).setBiomeName("Extreme Hills").func_150570_a(field_150603_i).setTemperatureRainfall(0.2F, 0.3F);
		public static readonly BiomeGenBase forest = (new BiomeGenForest(4, 0)).setColor(353825).setBiomeName("Forest");
		public static readonly BiomeGenBase taiga = (new BiomeGenTaiga(5, 0)).setColor(747097).setBiomeName("Taiga").func_76733_a(5159473).setTemperatureRainfall(0.25F, 0.8F).func_150570_a(field_150590_f);
		public static readonly BiomeGenBase swampland = (new BiomeGenSwamp(6)).setColor(522674).setBiomeName("Swampland").func_76733_a(9154376).func_150570_a(field_150599_m).setTemperatureRainfall(0.8F, 0.9F);
		public static readonly BiomeGenBase river = (new BiomeGenRiver(7)).setColor(255).setBiomeName("River").func_150570_a(field_150594_b);
		public static readonly BiomeGenBase hell = (new BiomeGenHell(8)).setColor(16711680).setBiomeName("Hell").setDisableRain().setTemperatureRainfall(2.0F, 0.0F);

	/// <summary> Is the biome used for sky world.  </summary>
		public static readonly BiomeGenBase sky = (new BiomeGenEnd(9)).setColor(8421631).setBiomeName("Sky").setDisableRain();
		public static readonly BiomeGenBase frozenOcean = (new BiomeGenOcean(10)).setColor(9474208).setBiomeName("FrozenOcean").setEnableSnow().func_150570_a(field_150595_c).setTemperatureRainfall(0.0F, 0.5F);
		public static readonly BiomeGenBase frozenRiver = (new BiomeGenRiver(11)).setColor(10526975).setBiomeName("FrozenRiver").setEnableSnow().func_150570_a(field_150594_b).setTemperatureRainfall(0.0F, 0.5F);
		public static readonly BiomeGenBase icePlains = (new BiomeGenSnow(12, false)).setColor(16777215).setBiomeName("Ice Plains").setEnableSnow().setTemperatureRainfall(0.0F, 0.5F).func_150570_a(field_150593_e);
		public static readonly BiomeGenBase iceMountains = (new BiomeGenSnow(13, false)).setColor(10526880).setBiomeName("Ice Mountains").setEnableSnow().func_150570_a(field_150591_g).setTemperatureRainfall(0.0F, 0.5F);
		public static readonly BiomeGenBase mushroomIsland = (new BiomeGenMushroomIsland(14)).setColor(16711935).setBiomeName("MushroomIsland").setTemperatureRainfall(0.9F, 1.0F).func_150570_a(field_150598_l);
		public static readonly BiomeGenBase mushroomIslandShore = (new BiomeGenMushroomIsland(15)).setColor(10486015).setBiomeName("MushroomIslandShore").setTemperatureRainfall(0.9F, 1.0F).func_150570_a(field_150600_j);

	/// <summary> Beach biome.  </summary>
		public static readonly BiomeGenBase beach = (new BiomeGenBeach(16)).setColor(16440917).setBiomeName("Beach").setTemperatureRainfall(0.8F, 0.4F).func_150570_a(field_150600_j);

	/// <summary> Desert Hills biome.  </summary>
		public static readonly BiomeGenBase desertHills = (new BiomeGenDesert(17)).setColor(13786898).setBiomeName("DesertHills").setDisableRain().setTemperatureRainfall(2.0F, 0.0F).func_150570_a(field_150591_g);

	/// <summary> Forest Hills biome.  </summary>
		public static readonly BiomeGenBase forestHills = (new BiomeGenForest(18, 0)).setColor(2250012).setBiomeName("ForestHills").func_150570_a(field_150591_g);

	/// <summary> Taiga Hills biome.  </summary>
		public static readonly BiomeGenBase taigaHills = (new BiomeGenTaiga(19, 0)).setColor(1456435).setBiomeName("TaigaHills").func_76733_a(5159473).setTemperatureRainfall(0.25F, 0.8F).func_150570_a(field_150591_g);

	/// <summary> Extreme Hills Edge biome.  </summary>
		public static readonly BiomeGenBase extremeHillsEdge = (new BiomeGenHills(20, true)).setColor(7501978).setBiomeName("Extreme Hills Edge").func_150570_a(field_150603_i.func_150775_a()).setTemperatureRainfall(0.2F, 0.3F);

	/// <summary> Jungle biome identifier  </summary>
		public static readonly BiomeGenBase jungle = (new BiomeGenJungle(21, false)).setColor(5470985).setBiomeName("Jungle").func_76733_a(5470985).setTemperatureRainfall(0.95F, 0.9F);
		public static readonly BiomeGenBase jungleHills = (new BiomeGenJungle(22, false)).setColor(2900485).setBiomeName("JungleHills").func_76733_a(5470985).setTemperatureRainfall(0.95F, 0.9F).func_150570_a(field_150591_g);
		public static readonly BiomeGenBase field_150574_L = (new BiomeGenJungle(23, true)).setColor(6458135).setBiomeName("JungleEdge").func_76733_a(5470985).setTemperatureRainfall(0.95F, 0.8F);
		public static readonly BiomeGenBase field_150575_M = (new BiomeGenOcean(24)).setColor(48).setBiomeName("Deep Ocean").func_150570_a(field_150592_d);
		public static readonly BiomeGenBase field_150576_N = (new BiomeGenStoneBeach(25)).setColor(10658436).setBiomeName("Stone Beach").setTemperatureRainfall(0.2F, 0.3F).func_150570_a(field_150601_k);
		public static readonly BiomeGenBase field_150577_O = (new BiomeGenBeach(26)).setColor(16445632).setBiomeName("Cold Beach").setTemperatureRainfall(0.05F, 0.3F).func_150570_a(field_150600_j).setEnableSnow();
		public static readonly BiomeGenBase field_150583_P = (new BiomeGenForest(27, 2)).setBiomeName("Birch Forest").setColor(3175492);
		public static readonly BiomeGenBase field_150582_Q = (new BiomeGenForest(28, 2)).setBiomeName("Birch Forest Hills").setColor(2055986).func_150570_a(field_150591_g);
		public static readonly BiomeGenBase field_150585_R = (new BiomeGenForest(29, 3)).setColor(4215066).setBiomeName("Roofed Forest");
		public static readonly BiomeGenBase field_150584_S = (new BiomeGenTaiga(30, 0)).setColor(3233098).setBiomeName("Cold Taiga").func_76733_a(5159473).setEnableSnow().setTemperatureRainfall(-0.5F, 0.4F).func_150570_a(field_150590_f).func_150563_c(16777215);
		public static readonly BiomeGenBase field_150579_T = (new BiomeGenTaiga(31, 0)).setColor(2375478).setBiomeName("Cold Taiga Hills").func_76733_a(5159473).setEnableSnow().setTemperatureRainfall(-0.5F, 0.4F).func_150570_a(field_150591_g).func_150563_c(16777215);
		public static readonly BiomeGenBase field_150578_U = (new BiomeGenTaiga(32, 1)).setColor(5858897).setBiomeName("Mega Taiga").func_76733_a(5159473).setTemperatureRainfall(0.3F, 0.8F).func_150570_a(field_150590_f);
		public static readonly BiomeGenBase field_150581_V = (new BiomeGenTaiga(33, 1)).setColor(4542270).setBiomeName("Mega Taiga Hills").func_76733_a(5159473).setTemperatureRainfall(0.3F, 0.8F).func_150570_a(field_150591_g);
		public static readonly BiomeGenBase field_150580_W = (new BiomeGenHills(34, true)).setColor(5271632).setBiomeName("Extreme Hills+").func_150570_a(field_150603_i).setTemperatureRainfall(0.2F, 0.3F);
		public static readonly BiomeGenBase field_150588_X = (new BiomeGenSavanna(35)).setColor(12431967).setBiomeName("Savanna").setTemperatureRainfall(1.2F, 0.0F).setDisableRain().func_150570_a(field_150593_e);
		public static readonly BiomeGenBase field_150587_Y = (new BiomeGenSavanna(36)).setColor(10984804).setBiomeName("Savanna Plateau").setTemperatureRainfall(1.0F, 0.0F).setDisableRain().func_150570_a(field_150602_h);
		public static readonly BiomeGenBase field_150589_Z = (new BiomeGenMesa(37, false, false)).setColor(14238997).setBiomeName("Mesa");
		public static readonly BiomeGenBase field_150607_aa = (new BiomeGenMesa(38, false, true)).setColor(11573093).setBiomeName("Mesa Plateau F").func_150570_a(field_150602_h);
		public static readonly BiomeGenBase field_150608_ab = (new BiomeGenMesa(39, false, false)).setColor(13274213).setBiomeName("Mesa Plateau").func_150570_a(field_150602_h);
		protected internal static readonly NoiseGeneratorPerlin field_150605_ac;
		protected internal static readonly NoiseGeneratorPerlin field_150606_ad;
		protected internal static readonly WorldGenDoublePlant field_150610_ae;
		public string biomeName;
		public int color;
		public int field_150609_ah;

	/// <summary> The block expected to be on the top of this biome  </summary>
		public Block topBlock;
		public int field_150604_aj;

	/// <summary> The block to fill spots in when not on the top  </summary>
		public Block fillerBlock;
		public int field_76754_C;

	/// <summary> The minimum height of this biome. Default 0.1.  </summary>
		public float minHeight;

	/// <summary> The maximum height of this biome. Default 0.3.  </summary>
		public float maxHeight;

	/// <summary> The temperature of this biome.  </summary>
		public float temperature;

	/// <summary> The rainfall in this biome.  </summary>
		public float rainfall;

	/// <summary> Color tint applied to water depending on biome  </summary>
		public int waterColorMultiplier;

	/// <summary> The biome decorator.  </summary>
		public BiomeDecorator theBiomeDecorator;

///    
///     <summary> * Holds the classes of IMobs (hostile mobs) that can be spawned in the biome. </summary>
///     
		protected internal IList spawnableMonsterList;

///    
///     <summary> * Holds the classes of any creature that can be spawned in the biome as friendly creature. </summary>
///     
		protected internal IList spawnableCreatureList;

///    
///     <summary> * Holds the classes of any aquatic creature that can be spawned in the water of the biome. </summary>
///     
		protected internal IList spawnableWaterCreatureList;
		protected internal IList spawnableCaveCreatureList;

	/// <summary> Set to true if snow is enabled for this biome.  </summary>
		protected internal bool enableSnow;

///    
///     <summary> * Is true (default) if the biome support rain (desert and nether can't have rain) </summary>
///     
		protected internal bool enableRain;

	/// <summary> The id number to this biome, and its index in the biomeList array.  </summary>
		public readonly int biomeID;

	/// <summary> The tree generator.  </summary>
		protected internal WorldGenTrees worldGeneratorTrees;

	/// <summary> The big tree generator.  </summary>
		protected internal WorldGenBigTree worldGeneratorBigTree;

	/// <summary> The swamp tree generator.  </summary>
		protected internal WorldGenSwamp worldGeneratorSwamp;
		

		protected internal BiomeGenBase(int p_i1971_1_)
		{
			this.topBlock = Blocks.grass;
			this.field_150604_aj = 0;
			this.fillerBlock = Blocks.dirt;
			this.field_76754_C = 5169201;
			this.minHeight = field_150596_a.field_150777_a;
			this.maxHeight = field_150596_a.field_150776_b;
			this.temperature = 0.5F;
			this.rainfall = 0.5F;
			this.waterColorMultiplier = 16777215;
			this.spawnableMonsterList = new ArrayList();
			this.spawnableCreatureList = new ArrayList();
			this.spawnableWaterCreatureList = new ArrayList();
			this.spawnableCaveCreatureList = new ArrayList();
			this.enableRain = true;
			this.worldGeneratorTrees = new WorldGenTrees(false);
			this.worldGeneratorBigTree = new WorldGenBigTree(false);
			this.worldGeneratorSwamp = new WorldGenSwamp();
			this.biomeID = p_i1971_1_;
			biomeList[p_i1971_1_] = this;
			this.theBiomeDecorator = this.createBiomeDecorator();
			this.spawnableCreatureList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntitySheep), 12, 4, 4));
			this.spawnableCreatureList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntityPig), 10, 4, 4));
			this.spawnableCreatureList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntityChicken), 10, 4, 4));
			this.spawnableCreatureList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntityCow), 8, 4, 4));
			this.spawnableMonsterList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntitySpider), 100, 4, 4));
			this.spawnableMonsterList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntityZombie), 100, 4, 4));
			this.spawnableMonsterList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntitySkeleton), 100, 4, 4));
			this.spawnableMonsterList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntityCreeper), 100, 4, 4));
			this.spawnableMonsterList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntitySlime), 100, 4, 4));
			this.spawnableMonsterList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntityEnderman), 10, 1, 4));
			this.spawnableMonsterList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntityWitch), 5, 1, 1));
			this.spawnableWaterCreatureList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntitySquid), 10, 4, 4));
			this.spawnableCaveCreatureList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntityBat), 10, 8, 8));
		}

///    
///     <summary> * Allocate a new BiomeDecorator for this BiomeGenBase </summary>
///     
		protected internal virtual BiomeDecorator createBiomeDecorator()
		{
			return new BiomeDecorator();
		}

///    
///     <summary> * Sets the temperature and rainfall of this biome. </summary>
///     
		protected internal virtual BiomeGenBase setTemperatureRainfall(float p_76732_1_, float p_76732_2_)
		{
			if (p_76732_1_ > 0.1F && p_76732_1_ < 0.2F)
			{
				throw new System.ArgumentException("Please avoid temperatures in the range 0.1 - 0.2 because of snow");
			}
			else
			{
				this.temperature = p_76732_1_;
				this.rainfall = p_76732_2_;
				return this;
			}
		}

		protected internal BiomeGenBase func_150570_a(BiomeGenBase.Height p_150570_1_)
		{
			this.minHeight = p_150570_1_.field_150777_a;
			this.maxHeight = p_150570_1_.field_150776_b;
			return this;
		}

///    
///     <summary> * Disable the rain for the biome. </summary>
///     
		protected internal virtual BiomeGenBase setDisableRain()
		{
			this.enableRain = false;
			return this;
		}

		public virtual WorldGenAbstractTree func_150567_a(Random p_150567_1_)
		{
			return (WorldGenAbstractTree)(p_150567_1_.Next(10) == 0 ? this.worldGeneratorBigTree : this.worldGeneratorTrees);
		}

///    
///     <summary> * Gets a WorldGen appropriate for this biome. </summary>
///     
		public virtual WorldGenerator getRandomWorldGenForGrass(Random p_76730_1_)
		{
			return new WorldGenTallGrass(Blocks.tallgrass, 1);
		}

		public virtual string func_150572_a(Random p_150572_1_, int p_150572_2_, int p_150572_3_, int p_150572_4_)
		{
			return p_150572_1_.Next(3) > 0 ? BlockFlower.field_149858_b[0] : BlockFlower.field_149859_a[0];
		}

///    
///     <summary> * sets enableSnow to true during biome initialization. returns BiomeGenBase. </summary>
///     
		protected internal virtual BiomeGenBase setEnableSnow()
		{
			this.enableSnow = true;
			return this;
		}

		protected internal virtual BiomeGenBase BiomeName
		{
			set
			{
				this.biomeName = value;
				return this;
			}
		}

		protected internal virtual BiomeGenBase func_76733_a(int p_76733_1_)
		{
			this.field_76754_C = p_76733_1_;
			return this;
		}

		protected internal virtual BiomeGenBase Color
		{
			set
			{
				this.func_150557_a(value, false);
				return this;
			}
		}

		protected internal virtual BiomeGenBase func_150563_c(int p_150563_1_)
		{
			this.field_150609_ah = p_150563_1_;
			return this;
		}

		protected internal virtual BiomeGenBase func_150557_a(int p_150557_1_, bool p_150557_2_)
		{
			this.color = p_150557_1_;

			if (p_150557_2_)
			{
				this.field_150609_ah = (p_150557_1_ & 16711422) >> 1;
			}
			else
			{
				this.field_150609_ah = p_150557_1_;
			}

			return this;
		}

///    
///     <summary> * takes temperature, returns color </summary>
///     
		public virtual int getSkyColorByTemp(float p_76731_1_)
		{
			p_76731_1_ /= 3.0F;

			if (p_76731_1_ < -1.0F)
			{
				p_76731_1_ = -1.0F;
			}

			if (p_76731_1_ > 1.0F)
			{
				p_76731_1_ = 1.0F;
			}

			return Color.getHSBColor(0.62222224F - p_76731_1_ * 0.05F, 0.5F + p_76731_1_ * 0.1F, 1.0F).RGB;
		}

///    
///     <summary> * Returns the correspondent list of the EnumCreatureType informed. </summary>
///     
		public virtual IList getSpawnableList(EnumCreatureType p_76747_1_)
		{
			return p_76747_1_ == EnumCreatureType.monster ? this.spawnableMonsterList : (p_76747_1_ == EnumCreatureType.creature ? this.spawnableCreatureList : (p_76747_1_ == EnumCreatureType.waterCreature ? this.spawnableWaterCreatureList : (p_76747_1_ == EnumCreatureType.ambient ? this.spawnableCaveCreatureList : null)));
		}

///    
///     <summary> * Returns true if the biome have snowfall instead a normal rain. </summary>
///     
		public virtual bool EnableSnow
		{
			get
			{
				return this.func_150559_j();
			}
		}

///    
///     <summary> * Return true if the biome supports lightning bolt spawn, either by have the bolts enabled and have rain enabled. </summary>
///     
		public virtual bool canSpawnLightningBolt()
		{
			return this.func_150559_j() ? false : this.enableRain;
		}

///    
///     <summary> * Checks to see if the rainfall level of the biome is extremely high </summary>
///     
		public virtual bool isHighHumidity()
		{
			get
			{
				return this.rainfall > 0.85F;
			}
		}

///    
///     <summary> * returns the chance a creature has to spawn. </summary>
///     
		public virtual float SpawningChance
		{
			get
			{
				return 0.1F;
			}
		}

///    
///     <summary> * Gets an integer representation of this biome's rainfall </summary>
///     
		public int IntRainfall
		{
			get
			{
				return (int)(this.rainfall * 65536.0F);
			}
		}

///    
///     <summary> * Gets a floating point representation of this biome's rainfall </summary>
///     
		public float FloatRainfall
		{
			get
			{
				return this.rainfall;
			}
		}

///    
///     <summary> * Gets a floating point representation of this biome's temperature </summary>
///     
		public float getFloatTemperature(int p_150564_1_, int p_150564_2_, int p_150564_3_)
		{
			if (p_150564_2_ > 64)
			{
				float var4 = (float)field_150605_ac.func_151601_a((double)p_150564_1_ * 1.0D / 8.0D, (double)p_150564_3_ * 1.0D / 8.0D) * 4.0F;
				return this.temperature - (var4 + (float)p_150564_2_ - 64.0F) * 0.05F / 30.0F;
			}
			else
			{
				return this.temperature;
			}
		}

		public virtual void decorate(World p_76728_1_, Random p_76728_2_, int p_76728_3_, int p_76728_4_)
		{
			this.theBiomeDecorator.func_150512_a(p_76728_1_, p_76728_2_, this, p_76728_3_, p_76728_4_);
		}

///    
///     <summary> * Provides the basic grass color based on the biome temperature and rainfall </summary>
///     
		public virtual int getBiomeGrassColor(int p_150558_1_, int p_150558_2_, int p_150558_3_)
		{
			double var4 = (double)MathHelper.clamp_float(this.getFloatTemperature(p_150558_1_, p_150558_2_, p_150558_3_), 0.0F, 1.0F);
			double var6 = (double)MathHelper.clamp_float(this.FloatRainfall, 0.0F, 1.0F);
			return ColorizerGrass.getGrassColor(var4, var6);
		}

///    
///     <summary> * Provides the basic foliage color based on the biome temperature and rainfall </summary>
///     
		public virtual int getBiomeFoliageColor(int p_150571_1_, int p_150571_2_, int p_150571_3_)
		{
			double var4 = (double)MathHelper.clamp_float(this.getFloatTemperature(p_150571_1_, p_150571_2_, p_150571_3_), 0.0F, 1.0F);
			double var6 = (double)MathHelper.clamp_float(this.FloatRainfall, 0.0F, 1.0F);
			return ColorizerFoliage.getFoliageColor(var4, var6);
		}

		public virtual bool func_150559_j()
		{
			return this.enableSnow;
		}

		public virtual void func_150573_a(World p_150573_1_, Random p_150573_2_, Block[] p_150573_3_, sbyte[] p_150573_4_, int p_150573_5_, int p_150573_6_, double p_150573_7_)
		{
			this.func_150560_b(p_150573_1_, p_150573_2_, p_150573_3_, p_150573_4_, p_150573_5_, p_150573_6_, p_150573_7_);
		}

		public void func_150560_b(World p_150560_1_, Random p_150560_2_, Block[] p_150560_3_, sbyte[] p_150560_4_, int p_150560_5_, int p_150560_6_, double p_150560_7_)
		{
			bool var9 = true;
			Block var10 = this.topBlock;
			sbyte var11 = (sbyte)(this.field_150604_aj & 255);
			Block var12 = this.fillerBlock;
			int var13 = -1;
			int var14 = (int)(p_150560_7_ / 3.0D + 3.0D + p_150560_2_.NextDouble() * 0.25D);
			int var15 = p_150560_5_ & 15;
			int var16 = p_150560_6_ & 15;
			int var17 = p_150560_3_.Length / 256;

			for (int var18 = 255; var18 >= 0; --var18)
			{
				int var19 = (var16 * 16 + var15) * var17 + var18;

				if (var18 <= 0 + p_150560_2_.Next(5))
				{
					p_150560_3_[var19] = Blocks.bedrock;
				}
				else
				{
					Block var20 = p_150560_3_[var19];

					if (var20 != null && var20.Material != Material.air)
					{
						if (var20 == Blocks.stone)
						{
							if (var13 == -1)
							{
								if (var14 <= 0)
								{
									var10 = null;
									var11 = 0;
									var12 = Blocks.stone;
								}
								else if (var18 >= 59 && var18 <= 64)
								{
									var10 = this.topBlock;
									var11 = (sbyte)(this.field_150604_aj & 255);
									var12 = this.fillerBlock;
								}

								if (var18 < 63 && (var10 == null || var10.Material == Material.air))
								{
									if (this.getFloatTemperature(p_150560_5_, var18, p_150560_6_) < 0.15F)
									{
										var10 = Blocks.ice;
										var11 = 0;
									}
									else
									{
										var10 = Blocks.water;
										var11 = 0;
									}
								}

								var13 = var14;

								if (var18 >= 62)
								{
									p_150560_3_[var19] = var10;
									p_150560_4_[var19] = var11;
								}
								else if (var18 < 56 - var14)
								{
									var10 = null;
									var12 = Blocks.stone;
									p_150560_3_[var19] = Blocks.gravel;
								}
								else
								{
									p_150560_3_[var19] = var12;
								}
							}
							else if (var13 > 0)
							{
								--var13;
								p_150560_3_[var19] = var12;

								if (var13 == 0 && var12 == Blocks.sand)
								{
									var13 = p_150560_2_.Next(4) + Math.Max(0, var18 - 63);
									var12 = Blocks.sandstone;
								}
							}
						}
					}
					else
					{
						var13 = -1;
					}
				}
			}
		}

		protected internal virtual BiomeGenBase func_150566_k()
		{
			return new BiomeGenMutated(this.biomeID + 128, this);
		}

		public virtual Type func_150562_l()
		{
			return this.GetType();
		}

		public virtual bool func_150569_a(BiomeGenBase p_150569_1_)
		{
			return p_150569_1_ == this ? true : (p_150569_1_ == null ? false : this.func_150562_l() == p_150569_1_.func_150562_l());
		}

		public virtual BiomeGenBase.TempCategory func_150561_m()
		{
			return (double)this.temperature < 0.2D ? BiomeGenBase.TempCategory.COLD : ((double)this.temperature < 1.0D ? BiomeGenBase.TempCategory.MEDIUM : BiomeGenBase.TempCategory.WARM);
		}

		public static BiomeGenBase[] BiomeGenArray
		{
			get
			{
				return biomeList;
			}
		}

		public static BiomeGenBase func_150568_d(int p_150568_0_)
		{
			if (p_150568_0_ >= 0 && p_150568_0_ <= biomeList.Length)
			{
				return biomeList[p_150568_0_];
			}
			else
			{
				logger.warn("Biome ID is out of bounds: " + p_150568_0_ + ", defaulting to 0 (Ocean)");
				return ocean;
			}
		}

		static BiomeGenBase()
		{
			plains.func_150566_k();
			desert.func_150566_k();
			forest.func_150566_k();
			taiga.func_150566_k();
			swampland.func_150566_k();
			icePlains.func_150566_k();
			jungle.func_150566_k();
			field_150574_L.func_150566_k();
			field_150584_S.func_150566_k();
			field_150588_X.func_150566_k();
			field_150587_Y.func_150566_k();
			field_150589_Z.func_150566_k();
			field_150607_aa.func_150566_k();
			field_150608_ab.func_150566_k();
			field_150583_P.func_150566_k();
			field_150582_Q.func_150566_k();
			field_150585_R.func_150566_k();
			field_150578_U.func_150566_k();
			extremeHills.func_150566_k();
			field_150580_W.func_150566_k();
			biomeList[field_150581_V.biomeID + 128] = biomeList[field_150578_U.biomeID + 128];
			BiomeGenBase[] var0 = biomeList;
			int var1 = var0.Length;

			for (int var2 = 0; var2 < var1; ++var2)
			{
				BiomeGenBase var3 = var0[var2];

				if (var3 != null && var3.biomeID < 128)
				{
					field_150597_n.add(var3);
				}
			}

			field_150597_n.remove(hell);
			field_150597_n.remove(sky);
			field_150597_n.remove(frozenOcean);
			field_150597_n.remove(extremeHillsEdge);
			field_150605_ac = new NoiseGeneratorPerlin(new Random(1234L), 1);
			field_150606_ad = new NoiseGeneratorPerlin(new Random(2345L), 1);
			field_150610_ae = new WorldGenDoublePlant();
		}

		public class Height
		{
			public float field_150777_a;
			public float field_150776_b;
			

			public Height(float p_i45371_1_, float p_i45371_2_)
			{
				this.field_150777_a = p_i45371_1_;
				this.field_150776_b = p_i45371_2_;
			}

			public virtual BiomeGenBase.Height func_150775_a()
			{
				return new BiomeGenBase.Height(this.field_150777_a * 0.8F, this.field_150776_b * 0.6F);
			}
		}

		public class SpawnListEntry : WeightedRandom.Item
		{
			public Type entityClass;
			public int minGroupCount;
			public int maxGroupCount;
			

			public SpawnListEntry(Type p_i1970_1_, int p_i1970_2_, int p_i1970_3_, int p_i1970_4_) : base(p_i1970_2_)
			{
				this.entityClass = p_i1970_1_;
				this.minGroupCount = p_i1970_3_;
				this.maxGroupCount = p_i1970_4_;
			}

			public override string ToString()
			{
				return this.entityClass.SimpleName + "*(" + this.minGroupCount + "-" + this.maxGroupCount + "):" + this.itemWeight;
			}
		}

		public enum TempCategory
		{
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			OCEAN("OCEAN", 0),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			COLD("COLD", 1),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			MEDIUM("MEDIUM", 2),
//JAVA TO VB & C# CONVERTER TODO TASK: Enum values must be single integer values in .NET:
			WARM("WARM", 3);

			@private static final BiomeGenBase.TempCategory[] $VALUES = new BiomeGenBase.TempCategory[]{OCEAN, COLD, MEDIUM, WARM
		}
			

			private TempCategory(string p_i45372_1_, int p_i45372_2_)
			{
			}
		}
	}

}