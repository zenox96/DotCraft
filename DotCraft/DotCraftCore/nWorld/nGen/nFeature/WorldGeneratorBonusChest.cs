using System;

namespace DotCraftCore.nWorld.nGen.nFeature
{

	using Block = DotCraftCore.nBlock.Block;
	using Material = DotCraftCore.nBlock.nMaterial.Material;
	using Blocks = DotCraftCore.init.Blocks;
	using TileEntityChest = DotCraftCore.nTileEntity.TileEntityChest;
	using WeightedRandomChestContent = DotCraftCore.nUtil.WeightedRandomChestContent;
	using World = DotCraftCore.nWorld.World;

	public class WorldGeneratorBonusChest : WorldGenerator
	{
///    
///     <summary> * Instance of WeightedRandomChestContent what will randomly generate items into the Bonus Chest. </summary>
///     
		private readonly WeightedRandomChestContent[] theBonusChestGenerator;

///    
///     <summary> * Value of this int will determine how much items gonna generate in Bonus Chest. </summary>
///     
		private readonly int itemsToGenerateInBonusChest;
		

		public WorldGeneratorBonusChest(WeightedRandomChestContent[] p_i2010_1_, int p_i2010_2_)
		{
			this.theBonusChestGenerator = p_i2010_1_;
			this.itemsToGenerateInBonusChest = p_i2010_2_;
		}

		public override bool generate(World p_76484_1_, Random p_76484_2_, int p_76484_3_, int p_76484_4_, int p_76484_5_)
		{
			Block var6;

			while (((var6 = p_76484_1_.getBlock(p_76484_3_, p_76484_4_, p_76484_5_)).Material == Material.air || var6.Material == Material.leaves) && p_76484_4_ > 1)
			{
				--p_76484_4_;
			}

			if (p_76484_4_ < 1)
			{
				return false;
			}
			else
			{
				++p_76484_4_;

				for (int var7 = 0; var7 < 4; ++var7)
				{
					int var8 = p_76484_3_ + p_76484_2_.Next(4) - p_76484_2_.Next(4);
					int var9 = p_76484_4_ + p_76484_2_.Next(3) - p_76484_2_.Next(3);
					int var10 = p_76484_5_ + p_76484_2_.Next(4) - p_76484_2_.Next(4);

					if (p_76484_1_.isAirBlock(var8, var9, var10) && World.doesBlockHaveSolidTopSurface(p_76484_1_, var8, var9 - 1, var10))
					{
						p_76484_1_.setBlock(var8, var9, var10, Blocks.chest, 0, 2);
						TileEntityChest var11 = (TileEntityChest)p_76484_1_.getTileEntity(var8, var9, var10);

						if (var11 != null && var11 != null)
						{
							WeightedRandomChestContent.generateChestContents(p_76484_2_, this.theBonusChestGenerator, var11, this.itemsToGenerateInBonusChest);
						}

						if (p_76484_1_.isAirBlock(var8 - 1, var9, var10) && World.doesBlockHaveSolidTopSurface(p_76484_1_, var8 - 1, var9 - 1, var10))
						{
							p_76484_1_.setBlock(var8 - 1, var9, var10, Blocks.torch, 0, 2);
						}

						if (p_76484_1_.isAirBlock(var8 + 1, var9, var10) && World.doesBlockHaveSolidTopSurface(p_76484_1_, var8 - 1, var9 - 1, var10))
						{
							p_76484_1_.setBlock(var8 + 1, var9, var10, Blocks.torch, 0, 2);
						}

						if (p_76484_1_.isAirBlock(var8, var9, var10 - 1) && World.doesBlockHaveSolidTopSurface(p_76484_1_, var8 - 1, var9 - 1, var10))
						{
							p_76484_1_.setBlock(var8, var9, var10 - 1, Blocks.torch, 0, 2);
						}

						if (p_76484_1_.isAirBlock(var8, var9, var10 + 1) && World.doesBlockHaveSolidTopSurface(p_76484_1_, var8 - 1, var9 - 1, var10))
						{
							p_76484_1_.setBlock(var8, var9, var10 + 1, Blocks.torch, 0, 2);
						}

						return true;
					}
				}

				return false;
			}
		}
	}

}