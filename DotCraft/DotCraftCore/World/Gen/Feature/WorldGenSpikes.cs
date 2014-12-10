using System;

namespace DotCraftCore.World.Gen.Feature
{

	using Block = DotCraftCore.block.Block;
	using EntityEnderCrystal = DotCraftCore.entity.item.EntityEnderCrystal;
	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.World.World;

	public class WorldGenSpikes : WorldGenerator
	{
		private Block field_150520_a;
		private const string __OBFID = "CL_00000433";

		public WorldGenSpikes(Block p_i45464_1_)
		{
			this.field_150520_a = p_i45464_1_;
		}

		public override bool generate(World p_76484_1_, Random p_76484_2_, int p_76484_3_, int p_76484_4_, int p_76484_5_)
		{
			if (p_76484_1_.isAirBlock(p_76484_3_, p_76484_4_, p_76484_5_) && p_76484_1_.getBlock(p_76484_3_, p_76484_4_ - 1, p_76484_5_) == this.field_150520_a)
			{
				int var6 = p_76484_2_.Next(32) + 6;
				int var7 = p_76484_2_.Next(4) + 1;
				int var8;
				int var9;
				int var10;
				int var11;

				for (var8 = p_76484_3_ - var7; var8 <= p_76484_3_ + var7; ++var8)
				{
					for (var9 = p_76484_5_ - var7; var9 <= p_76484_5_ + var7; ++var9)
					{
						var10 = var8 - p_76484_3_;
						var11 = var9 - p_76484_5_;

						if (var10 * var10 + var11 * var11 <= var7 * var7 + 1 && p_76484_1_.getBlock(var8, p_76484_4_ - 1, var9) != this.field_150520_a)
						{
							return false;
						}
					}
				}

				for (var8 = p_76484_4_; var8 < p_76484_4_ + var6 && var8 < 256; ++var8)
				{
					for (var9 = p_76484_3_ - var7; var9 <= p_76484_3_ + var7; ++var9)
					{
						for (var10 = p_76484_5_ - var7; var10 <= p_76484_5_ + var7; ++var10)
						{
							var11 = var9 - p_76484_3_;
							int var12 = var10 - p_76484_5_;

							if (var11 * var11 + var12 * var12 <= var7 * var7 + 1)
							{
								p_76484_1_.setBlock(var9, var8, var10, Blocks.obsidian, 0, 2);
							}
						}
					}
				}

				EntityEnderCrystal var13 = new EntityEnderCrystal(p_76484_1_);
				var13.setLocationAndAngles((double)((float)p_76484_3_ + 0.5F), (double)(p_76484_4_ + var6), (double)((float)p_76484_5_ + 0.5F), p_76484_2_.nextFloat() * 360.0F, 0.0F);
				p_76484_1_.spawnEntityInWorld(var13);
				p_76484_1_.setBlock(p_76484_3_, p_76484_4_ + var6, p_76484_5_, Blocks.bedrock, 0, 2);
				return true;
			}
			else
			{
				return false;
			}
		}
	}

}