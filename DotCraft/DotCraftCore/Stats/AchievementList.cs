using System.Collections;

namespace DotCraftCore.Stats
{

	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using ItemStack = DotCraftCore.item.ItemStack;
	using JsonSerializableSet = DotCraftCore.Util.JsonSerializableSet;

	public class AchievementList
	{
	/// <summary> Is the smallest column used to display a achievement on the GUI.  </summary>
		public static int minDisplayColumn;

	/// <summary> Is the smallest row used to display a achievement on the GUI.  </summary>
		public static int minDisplayRow;

	/// <summary> Is the biggest column used to display a achievement on the GUI.  </summary>
		public static int maxDisplayColumn;

	/// <summary> Is the biggest row used to display a achievement on the GUI.  </summary>
		public static int maxDisplayRow;

	/// <summary> Holds a list of all registered achievements.  </summary>
		public static IList achievementList = new ArrayList();

	/// <summary> Is the 'open inventory' achievement.  </summary>
		public static Achievement openInventory = (new Achievement("achievement.openInventory", "openInventory", 0, 0, Items.book, (Achievement)null)).initIndependentStat().registerStat();

	/// <summary> Is the 'getting wood' achievement.  </summary>
		public static Achievement mineWood = (new Achievement("achievement.mineWood", "mineWood", 2, 1, Blocks.log, openInventory)).registerStat();

	/// <summary> Is the 'benchmarking' achievement.  </summary>
		public static Achievement buildWorkBench = (new Achievement("achievement.buildWorkBench", "buildWorkBench", 4, -1, Blocks.crafting_table, mineWood)).registerStat();

	/// <summary> Is the 'time to mine' achievement.  </summary>
		public static Achievement buildPickaxe = (new Achievement("achievement.buildPickaxe", "buildPickaxe", 4, 2, Items.wooden_pickaxe, buildWorkBench)).registerStat();

	/// <summary> Is the 'hot topic' achievement.  </summary>
		public static Achievement buildFurnace = (new Achievement("achievement.buildFurnace", "buildFurnace", 3, 4, Blocks.furnace, buildPickaxe)).registerStat();

	/// <summary> Is the 'acquire hardware' achievement.  </summary>
		public static Achievement acquireIron = (new Achievement("achievement.acquireIron", "acquireIron", 1, 4, Items.iron_ingot, buildFurnace)).registerStat();

	/// <summary> Is the 'time to farm' achievement.  </summary>
		public static Achievement buildHoe = (new Achievement("achievement.buildHoe", "buildHoe", 2, -3, Items.wooden_hoe, buildWorkBench)).registerStat();

	/// <summary> Is the 'bake bread' achievement.  </summary>
		public static Achievement makeBread = (new Achievement("achievement.makeBread", "makeBread", -1, -3, Items.bread, buildHoe)).registerStat();

	/// <summary> Is the 'the lie' achievement.  </summary>
		public static Achievement bakeCake = (new Achievement("achievement.bakeCake", "bakeCake", 0, -5, Items.cake, buildHoe)).registerStat();

	/// <summary> Is the 'getting a upgrade' achievement.  </summary>
		public static Achievement buildBetterPickaxe = (new Achievement("achievement.buildBetterPickaxe", "buildBetterPickaxe", 6, 2, Items.stone_pickaxe, buildPickaxe)).registerStat();

	/// <summary> Is the 'delicious fish' achievement.  </summary>
		public static Achievement cookFish = (new Achievement("achievement.cookFish", "cookFish", 2, 6, Items.cooked_fished, buildFurnace)).registerStat();

	/// <summary> Is the 'on a rail' achievement  </summary>
		public static Achievement onARail = (new Achievement("achievement.onARail", "onARail", 2, 3, Blocks.rail, acquireIron)).setSpecial().registerStat();

	/// <summary> Is the 'time to strike' achievement.  </summary>
		public static Achievement buildSword = (new Achievement("achievement.buildSword", "buildSword", 6, -1, Items.wooden_sword, buildWorkBench)).registerStat();

	/// <summary> Is the 'monster hunter' achievement.  </summary>
		public static Achievement killEnemy = (new Achievement("achievement.killEnemy", "killEnemy", 8, -1, Items.bone, buildSword)).registerStat();

	/// <summary> is the 'cow tipper' achievement.  </summary>
		public static Achievement killCow = (new Achievement("achievement.killCow", "killCow", 7, -3, Items.leather, buildSword)).registerStat();

	/// <summary> Is the 'when pig fly' achievement.  </summary>
		public static Achievement flyPig = (new Achievement("achievement.flyPig", "flyPig", 9, -3, Items.saddle, killCow)).setSpecial().registerStat();

	/// <summary> The achievement for killing a Skeleton from 50 meters aways.  </summary>
		public static Achievement snipeSkeleton = (new Achievement("achievement.snipeSkeleton", "snipeSkeleton", 7, 0, Items.bow, killEnemy)).setSpecial().registerStat();

	/// <summary> Is the 'DIAMONDS!' achievement  </summary>
		public static Achievement diamonds = (new Achievement("achievement.diamonds", "diamonds", -1, 5, Blocks.diamond_ore, acquireIron)).registerStat();
		public static Achievement field_150966_x = (new Achievement("achievement.diamondsToYou", "diamondsToYou", -1, 2, Items.diamond, diamonds)).registerStat();

	/// <summary> Is the 'We Need to Go Deeper' achievement  </summary>
		public static Achievement portal = (new Achievement("achievement.portal", "portal", -1, 7, Blocks.obsidian, diamonds)).registerStat();

	/// <summary> Is the 'Return to Sender' achievement  </summary>
		public static Achievement ghast = (new Achievement("achievement.ghast", "ghast", -4, 8, Items.ghast_tear, portal)).setSpecial().registerStat();

	/// <summary> Is the 'Into Fire' achievement  </summary>
		public static Achievement blazeRod = (new Achievement("achievement.blazeRod", "blazeRod", 0, 9, Items.blaze_rod, portal)).registerStat();

	/// <summary> Is the 'Local Brewery' achievement  </summary>
		public static Achievement potion = (new Achievement("achievement.potion", "potion", 2, 8, Items.potionitem, blazeRod)).registerStat();

	/// <summary> Is the 'The End?' achievement  </summary>
		public static Achievement theEnd = (new Achievement("achievement.theEnd", "theEnd", 3, 10, Items.ender_eye, blazeRod)).setSpecial().registerStat();

	/// <summary> Is the 'The End.' achievement  </summary>
		public static Achievement theEnd2 = (new Achievement("achievement.theEnd2", "theEnd2", 4, 13, Blocks.dragon_egg, theEnd)).setSpecial().registerStat();

	/// <summary> Is the 'Enchanter' achievement  </summary>
		public static Achievement enchantments = (new Achievement("achievement.enchantments", "enchantments", -4, 4, Blocks.enchanting_table, diamonds)).registerStat();
		public static Achievement overkill = (new Achievement("achievement.overkill", "overkill", -4, 1, Items.diamond_sword, enchantments)).setSpecial().registerStat();

	/// <summary> Is the 'Librarian' achievement  </summary>
		public static Achievement bookcase = (new Achievement("achievement.bookcase", "bookcase", -3, 6, Blocks.bookshelf, enchantments)).registerStat();
		public static Achievement field_150962_H = (new Achievement("achievement.breedCow", "breedCow", 7, -5, Items.wheat, killCow)).registerStat();
		public static Achievement field_150963_I = (new Achievement("achievement.spawnWither", "spawnWither", 7, 12, new ItemStack(Items.skull, 1, 1), theEnd2)).registerStat();
		public static Achievement field_150964_J = (new Achievement("achievement.killWither", "killWither", 7, 10, Items.nether_star, field_150963_I)).registerStat();
		public static Achievement field_150965_K = (new Achievement("achievement.fullBeacon", "fullBeacon", 7, 8, Blocks.beacon, field_150964_J)).setSpecial().registerStat();
		public static Achievement field_150961_L = (new Achievement("achievement.exploreAllBiomes", "exploreAllBiomes", 4, 8, Items.diamond_boots, theEnd)).func_150953_b(typeof(JsonSerializableSet)).setSpecial().registerStat();
		

///    
///     <summary> * A stub functions called to make the static initializer for this class run. </summary>
///     
		public static void init()
		{
		}
	}

}