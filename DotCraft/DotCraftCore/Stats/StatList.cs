using System.Collections;

namespace DotCraftCore.Stats
{

	using Block = DotCraftCore.block.Block;
	using EntityList = DotCraftCore.entity.EntityList;
	using Blocks = DotCraftCore.init.Blocks;
	using Item = DotCraftCore.item.Item;
	using ItemBlock = DotCraftCore.item.ItemBlock;
	using ItemStack = DotCraftCore.item.ItemStack;
	using CraftingManager = DotCraftCore.item.crafting.CraftingManager;
	using FurnaceRecipes = DotCraftCore.item.crafting.FurnaceRecipes;
	using IRecipe = DotCraftCore.item.crafting.IRecipe;
	using ChatComponentTranslation = DotCraftCore.Util.ChatComponentTranslation;

	public class StatList
	{
	/// <summary> Tracks one-off stats.  </summary>
		protected internal static IDictionary oneShotStats = new Hashtable();
		public static IList allStats = new ArrayList();
		public static IList generalStats = new ArrayList();
		public static IList itemStats = new ArrayList();

	/// <summary> Tracks the number of times a given block or item has been mined.  </summary>
		public static IList objectMineStats = new ArrayList();

	/// <summary> number of times you've left a game  </summary>
		public static StatBase leaveGameStat = (new StatBasic("stat.leaveGame", new ChatComponentTranslation("stat.leaveGame", new object[0]))).initIndependentStat().registerStat();

	/// <summary> number of minutes you have played  </summary>
		public static StatBase minutesPlayedStat = (new StatBasic("stat.playOneMinute", new ChatComponentTranslation("stat.playOneMinute", new object[0]), StatBase.timeStatType)).initIndependentStat().registerStat();

	/// <summary> distance you've walked  </summary>
		public static StatBase distanceWalkedStat = (new StatBasic("stat.walkOneCm", new ChatComponentTranslation("stat.walkOneCm", new object[0]), StatBase.distanceStatType)).initIndependentStat().registerStat();

	/// <summary> distance you have swam  </summary>
		public static StatBase distanceSwumStat = (new StatBasic("stat.swimOneCm", new ChatComponentTranslation("stat.swimOneCm", new object[0]), StatBase.distanceStatType)).initIndependentStat().registerStat();

	/// <summary> the distance you have fallen  </summary>
		public static StatBase distanceFallenStat = (new StatBasic("stat.fallOneCm", new ChatComponentTranslation("stat.fallOneCm", new object[0]), StatBase.distanceStatType)).initIndependentStat().registerStat();

	/// <summary> the distance you've climbed  </summary>
		public static StatBase distanceClimbedStat = (new StatBasic("stat.climbOneCm", new ChatComponentTranslation("stat.climbOneCm", new object[0]), StatBase.distanceStatType)).initIndependentStat().registerStat();

	/// <summary> the distance you've flown  </summary>
		public static StatBase distanceFlownStat = (new StatBasic("stat.flyOneCm", new ChatComponentTranslation("stat.flyOneCm", new object[0]), StatBase.distanceStatType)).initIndependentStat().registerStat();

	/// <summary> the distance you've dived  </summary>
		public static StatBase distanceDoveStat = (new StatBasic("stat.diveOneCm", new ChatComponentTranslation("stat.diveOneCm", new object[0]), StatBase.distanceStatType)).initIndependentStat().registerStat();

	/// <summary> the distance you've traveled by minecart  </summary>
		public static StatBase distanceByMinecartStat = (new StatBasic("stat.minecartOneCm", new ChatComponentTranslation("stat.minecartOneCm", new object[0]), StatBase.distanceStatType)).initIndependentStat().registerStat();

	/// <summary> the distance you've traveled by boat  </summary>
		public static StatBase distanceByBoatStat = (new StatBasic("stat.boatOneCm", new ChatComponentTranslation("stat.boatOneCm", new object[0]), StatBase.distanceStatType)).initIndependentStat().registerStat();

	/// <summary> the distance you've traveled by pig  </summary>
		public static StatBase distanceByPigStat = (new StatBasic("stat.pigOneCm", new ChatComponentTranslation("stat.pigOneCm", new object[0]), StatBase.distanceStatType)).initIndependentStat().registerStat();
		public static StatBase field_151185_q = (new StatBasic("stat.horseOneCm", new ChatComponentTranslation("stat.horseOneCm", new object[0]), StatBase.distanceStatType)).initIndependentStat().registerStat();

	/// <summary> the times you've jumped  </summary>
		public static StatBase jumpStat = (new StatBasic("stat.jump", new ChatComponentTranslation("stat.jump", new object[0]))).initIndependentStat().registerStat();

	/// <summary> the distance you've dropped (or times you've fallen?)  </summary>
		public static StatBase dropStat = (new StatBasic("stat.drop", new ChatComponentTranslation("stat.drop", new object[0]))).initIndependentStat().registerStat();

	/// <summary> the amount of damage you've dealt  </summary>
		public static StatBase damageDealtStat = (new StatBasic("stat.damageDealt", new ChatComponentTranslation("stat.damageDealt", new object[0]), StatBase.field_111202_k)).registerStat();

	/// <summary> the amount of damage you have taken  </summary>
		public static StatBase damageTakenStat = (new StatBasic("stat.damageTaken", new ChatComponentTranslation("stat.damageTaken", new object[0]), StatBase.field_111202_k)).registerStat();

	/// <summary> the number of times you have died  </summary>
		public static StatBase deathsStat = (new StatBasic("stat.deaths", new ChatComponentTranslation("stat.deaths", new object[0]))).registerStat();

	/// <summary> the number of mobs you have killed  </summary>
		public static StatBase mobKillsStat = (new StatBasic("stat.mobKills", new ChatComponentTranslation("stat.mobKills", new object[0]))).registerStat();
		public static StatBase field_151186_x = (new StatBasic("stat.animalsBred", new ChatComponentTranslation("stat.animalsBred", new object[0]))).registerStat();

	/// <summary> counts the number of times you've killed a player  </summary>
		public static StatBase playerKillsStat = (new StatBasic("stat.playerKills", new ChatComponentTranslation("stat.playerKills", new object[0]))).registerStat();
		public static StatBase fishCaughtStat = (new StatBasic("stat.fishCaught", new ChatComponentTranslation("stat.fishCaught", new object[0]))).registerStat();
		public static StatBase field_151183_A = (new StatBasic("stat.junkFished", new ChatComponentTranslation("stat.junkFished", new object[0]))).registerStat();
		public static StatBase field_151184_B = (new StatBasic("stat.treasureFished", new ChatComponentTranslation("stat.treasureFished", new object[0]))).registerStat();
		public static readonly StatBase[] mineBlockStatArray = new StatBase[4096];

	/// <summary> Tracks the number of items a given block or item has been crafted.  </summary>
		public static readonly StatBase[] objectCraftStats = new StatBase[32000];

	/// <summary> Tracks the number of times a given block or item has been used.  </summary>
		public static readonly StatBase[] objectUseStats = new StatBase[32000];

	/// <summary> Tracks the number of times a given block or item has been broken.  </summary>
		public static readonly StatBase[] objectBreakStats = new StatBase[32000];
		

		public static void func_151178_a()
		{
			func_151181_c();
			initStats();
			func_151179_e();
			initCraftableStats();
			AchievementList.init();
			EntityList.func_151514_a();
		}

///    
///     <summary> * Initializes statistics related to craftable items. Is only called after both block and item stats have been
///     * initialized. </summary>
///     
		private static void initCraftableStats()
		{
			HashSet var0 = new HashSet();
			IEnumerator var1 = CraftingManager.Instance.RecipeList.GetEnumerator();

			while(var1.MoveNext())
			{
				IRecipe var2 = (IRecipe)var1.Current;

				if(var2.RecipeOutput != null)
				{
					var0.Add(var2.RecipeOutput.Item);
				}
			}

			var1 = FurnaceRecipes.smelting().SmeltingList.values().GetEnumerator();

			while(var1.MoveNext())
			{
				ItemStack var4 = (ItemStack)var1.Current;
				var0.Add(var4.Item);
			}

			var1 = var0.GetEnumerator();

			while(var1.MoveNext())
			{
				Item var5 = (Item)var1.Current;

				if(var5 != null)
				{
					int var3 = Item.getIdFromItem(var5);
					objectCraftStats[var3] = (new StatCrafting("stat.craftItem." + var3, new ChatComponentTranslation("stat.craftItem", new object[] {(new ItemStack(var5)).func_151000_E()}), var5)).registerStat();
				}
			}

			replaceAllSimilarBlocks(objectCraftStats);
		}

		private static void func_151181_c()
		{
			IEnumerator var0 = Block.blockRegistry.GetEnumerator();

			while(var0.MoveNext())
			{
				Block var1 = (Block)var0.Current;

				if(Item.getItemFromBlock(var1) != null)
				{
					int var2 = Block.getIdFromBlock(var1);

					if(var1.EnableStats)
					{
						mineBlockStatArray[var2] = (new StatCrafting("stat.mineBlock." + var2, new ChatComponentTranslation("stat.mineBlock", new object[] {(new ItemStack(var1)).func_151000_E()}), Item.getItemFromBlock(var1))).registerStat();
						objectMineStats.Add((StatCrafting)mineBlockStatArray[var2]);
					}
				}
			}

			replaceAllSimilarBlocks(mineBlockStatArray);
		}

		private static void initStats()
		{
			IEnumerator var0 = Item.itemRegistry.GetEnumerator();

			while(var0.MoveNext())
			{
				Item var1 = (Item)var0.Current;

				if(var1 != null)
				{
					int var2 = Item.getIdFromItem(var1);
					objectUseStats[var2] = (new StatCrafting("stat.useItem." + var2, new ChatComponentTranslation("stat.useItem", new object[] {(new ItemStack(var1)).func_151000_E()}), var1)).registerStat();

					if(!(var1 is ItemBlock))
					{
						itemStats.Add((StatCrafting)objectUseStats[var2]);
					}
				}
			}

			replaceAllSimilarBlocks(objectUseStats);
		}

		private static void func_151179_e()
		{
			IEnumerator var0 = Item.itemRegistry.GetEnumerator();

			while(var0.MoveNext())
			{
				Item var1 = (Item)var0.Current;

				if(var1 != null)
				{
					int var2 = Item.getIdFromItem(var1);

					if(var1.Damageable)
					{
						objectBreakStats[var2] = (new StatCrafting("stat.breakItem." + var2, new ChatComponentTranslation("stat.breakItem", new object[] {(new ItemStack(var1)).func_151000_E()}), var1)).registerStat();
					}
				}
			}

			replaceAllSimilarBlocks(objectBreakStats);
		}

///    
///     <summary> * Forces all dual blocks to count for each other on the stats list </summary>
///     
		private static void replaceAllSimilarBlocks(StatBase[] p_75924_0_)
		{
			func_151180_a(p_75924_0_, Blocks.water, Blocks.flowing_water);
			func_151180_a(p_75924_0_, Blocks.lava, Blocks.flowing_lava);
			func_151180_a(p_75924_0_, Blocks.lit_pumpkin, Blocks.pumpkin);
			func_151180_a(p_75924_0_, Blocks.lit_furnace, Blocks.furnace);
			func_151180_a(p_75924_0_, Blocks.lit_redstone_ore, Blocks.redstone_ore);
			func_151180_a(p_75924_0_, Blocks.powered_repeater, Blocks.unpowered_repeater);
			func_151180_a(p_75924_0_, Blocks.powered_comparator, Blocks.unpowered_comparator);
			func_151180_a(p_75924_0_, Blocks.redstone_torch, Blocks.unlit_redstone_torch);
			func_151180_a(p_75924_0_, Blocks.lit_redstone_lamp, Blocks.redstone_lamp);
			func_151180_a(p_75924_0_, Blocks.red_mushroom, Blocks.brown_mushroom);
			func_151180_a(p_75924_0_, Blocks.double_stone_slab, Blocks.stone_slab);
			func_151180_a(p_75924_0_, Blocks.double_wooden_slab, Blocks.wooden_slab);
			func_151180_a(p_75924_0_, Blocks.grass, Blocks.dirt);
			func_151180_a(p_75924_0_, Blocks.farmland, Blocks.dirt);
		}

		private static void func_151180_a(StatBase[] p_151180_0_, Block p_151180_1_, Block p_151180_2_)
		{
			int var3 = Block.getIdFromBlock(p_151180_1_);
			int var4 = Block.getIdFromBlock(p_151180_2_);

			if(p_151180_0_[var3] != null && p_151180_0_[var4] == null)
			{
				p_151180_0_[var4] = p_151180_0_[var3];
			}
			else
			{
				allStats.Remove(p_151180_0_[var3]);
				objectMineStats.Remove(p_151180_0_[var3]);
				generalStats.Remove(p_151180_0_[var3]);
				p_151180_0_[var3] = p_151180_0_[var4];
			}
		}

		public static StatBase func_151182_a(EntityList.EntityEggInfo p_151182_0_)
		{
			string var1 = EntityList.getStringFromID(p_151182_0_.spawnedID);
			return var1 == null ? null : (new StatBase("stat.killEntity." + var1, new ChatComponentTranslation("stat.entityKill", new object[] {new ChatComponentTranslation("entity." + var1 + ".name", new object[0])}))).registerStat();
		}

		public static StatBase func_151176_b(EntityList.EntityEggInfo p_151176_0_)
		{
			string var1 = EntityList.getStringFromID(p_151176_0_.spawnedID);
			return var1 == null ? null : (new StatBase("stat.entityKilledBy." + var1, new ChatComponentTranslation("stat.entityKilledBy", new object[] {new ChatComponentTranslation("entity." + var1 + ".name", new object[0])}))).registerStat();
		}

		public static StatBase func_151177_a(string p_151177_0_)
		{
			return(StatBase)oneShotStats[p_151177_0_];
		}
	}

}