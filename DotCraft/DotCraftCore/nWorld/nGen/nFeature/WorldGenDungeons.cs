using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInit;
using DotCraftCore.nTileEntity;
using DotCraftCore.nUtil;
using System;

namespace DotCraftCore.nWorld.nGen.nFeature
{
	public class WorldGenDungeons : WorldGenerator
	{
		private static readonly WeightedRandomChestContent[] field_111189_a = new WeightedRandomChestContent[] {new WeightedRandomChestContent(Items.saddle, 0, 1, 1, 10), new WeightedRandomChestContent(Items.iron_ingot, 0, 1, 4, 10), new WeightedRandomChestContent(Items.bread, 0, 1, 1, 10), new WeightedRandomChestContent(Items.wheat, 0, 1, 4, 10), new WeightedRandomChestContent(Items.gunpowder, 0, 1, 4, 10), new WeightedRandomChestContent(Items.@string, 0, 1, 4, 10), new WeightedRandomChestContent(Items.bucket, 0, 1, 1, 10), new WeightedRandomChestContent(Items.golden_apple, 0, 1, 1, 1), new WeightedRandomChestContent(Items.redstone, 0, 1, 4, 10), new WeightedRandomChestContent(Items.record_13, 0, 1, 1, 10), new WeightedRandomChestContent(Items.record_cat, 0, 1, 1, 10), new WeightedRandomChestContent(Items.name_tag, 0, 1, 1, 10), new WeightedRandomChestContent(Items.golden_horse_armor, 0, 1, 1, 2), new WeightedRandomChestContent(Items.iron_horse_armor, 0, 1, 1, 5), new WeightedRandomChestContent(Items.diamond_horse_armor, 0, 1, 1, 1)};
		

		public override bool generate(World p_76484_1_, Random p_76484_2_, int p_76484_3_, int p_76484_4_, int p_76484_5_)
		{
			sbyte var6 = 3;
			int var7 = p_76484_2_.Next(2) + 2;
			int var8 = p_76484_2_.Next(2) + 2;
			int var9 = 0;
			int var10;
			int var11;
			int var12;

			for (var10 = p_76484_3_ - var7 - 1; var10 <= p_76484_3_ + var7 + 1; ++var10)
			{
				for (var11 = p_76484_4_ - 1; var11 <= p_76484_4_ + var6 + 1; ++var11)
				{
					for (var12 = p_76484_5_ - var8 - 1; var12 <= p_76484_5_ + var8 + 1; ++var12)
					{
						Material var13 = p_76484_1_.getBlock(var10, var11, var12).BlockMaterial;

						if (var11 == p_76484_4_ - 1 && !var13.Solid)
						{
							return false;
						}

						if (var11 == p_76484_4_ + var6 + 1 && !var13.Solid)
						{
							return false;
						}

						if ((var10 == p_76484_3_ - var7 - 1 || var10 == p_76484_3_ + var7 + 1 || var12 == p_76484_5_ - var8 - 1 || var12 == p_76484_5_ + var8 + 1) && var11 == p_76484_4_ && p_76484_1_.isAirBlock(var10, var11, var12) && p_76484_1_.isAirBlock(var10, var11 + 1, var12))
						{
							++var9;
						}
					}
				}
			}

			if (var9 >= 1 && var9 <= 5)
			{
				for (var10 = p_76484_3_ - var7 - 1; var10 <= p_76484_3_ + var7 + 1; ++var10)
				{
					for (var11 = p_76484_4_ + var6; var11 >= p_76484_4_ - 1; --var11)
					{
						for (var12 = p_76484_5_ - var8 - 1; var12 <= p_76484_5_ + var8 + 1; ++var12)
						{
							if (var10 != p_76484_3_ - var7 - 1 && var11 != p_76484_4_ - 1 && var12 != p_76484_5_ - var8 - 1 && var10 != p_76484_3_ + var7 + 1 && var11 != p_76484_4_ + var6 + 1 && var12 != p_76484_5_ + var8 + 1)
							{
								p_76484_1_.setBlockToAir(var10, var11, var12);
							}
							else if (var11 >= 0 && !p_76484_1_.getBlock(var10, var11 - 1, var12).BlockMaterial.Solid)
							{
								p_76484_1_.setBlockToAir(var10, var11, var12);
							}
							else if (p_76484_1_.getBlock(var10, var11, var12).BlockMaterial.Solid)
							{
								if (var11 == p_76484_4_ - 1 && p_76484_2_.Next(4) != 0)
								{
									p_76484_1_.setBlock(var10, var11, var12, Blocks.mossy_cobblestone, 0, 2);
								}
								else
								{
									p_76484_1_.setBlock(var10, var11, var12, Blocks.cobblestone, 0, 2);
								}
							}
						}
					}
				}

				var10 = 0;

				while (var10 < 2)
				{
					var11 = 0;

					while (true)
					{
						if (var11 < 3)
						{
							label101:
							{
								var12 = p_76484_3_ + p_76484_2_.Next(var7 * 2 + 1) - var7;
								int var14 = p_76484_5_ + p_76484_2_.Next(var8 * 2 + 1) - var8;

								if (p_76484_1_.isAirBlock(var12, p_76484_4_, var14))
								{
									int var15 = 0;

									if (p_76484_1_.getBlock(var12 - 1, p_76484_4_, var14).BlockMaterial.Solid)
									{
										++var15;
									}

									if (p_76484_1_.getBlock(var12 + 1, p_76484_4_, var14).BlockMaterial.Solid)
									{
										++var15;
									}

									if (p_76484_1_.getBlock(var12, p_76484_4_, var14 - 1).BlockMaterial.Solid)
									{
										++var15;
									}

									if (p_76484_1_.getBlock(var12, p_76484_4_, var14 + 1).BlockMaterial.Solid)
									{
										++var15;
									}

									if (var15 == 1)
									{
										p_76484_1_.setBlock(var12, p_76484_4_, var14, Blocks.chest, 0, 2);
										WeightedRandomChestContent[] var16 = WeightedRandomChestContent.func_92080_a(field_111189_a, new WeightedRandomChestContent[] {Items.enchanted_book.func_92114_b(p_76484_2_)});
										TileEntityChest var17 = (TileEntityChest)p_76484_1_.getTileEntity(var12, p_76484_4_, var14);

										if (var17 != null)
										{
											WeightedRandomChestContent.generateChestContents(p_76484_2_, var16, var17, 8);
										}

										goto label101;
									}
								}

								++var11;
								continue;
							}
						}

						++var10;
						break;
					}
				}

				p_76484_1_.setBlock(p_76484_3_, p_76484_4_, p_76484_5_, Blocks.mob_spawner, 0, 2);
				TileEntityMobSpawner var18 = (TileEntityMobSpawner)p_76484_1_.getTileEntity(p_76484_3_, p_76484_4_, p_76484_5_);

				if (var18 != null)
				{
					var18.func_145881_a().MobID = this.pickMobSpawner(p_76484_2_);
				}
				else
				{
					System.err.println("Failed to fetch mob spawner entity at (" + p_76484_3_ + ", " + p_76484_4_ + ", " + p_76484_5_ + ")");
				}

				return true;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * Randomly decides which spawner to use in a dungeon </summary>
///     
		private string pickMobSpawner(Random p_76543_1_)
		{
			int var2 = p_76543_1_.Next(4);
			return var2 == 0 ? "Skeleton" : (var2 == 1 ? "Zombie" : (var2 == 2 ? "Zombie" : (var2 == 3 ? "Spider" : "")));
		}
	}

}