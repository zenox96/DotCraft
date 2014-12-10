using System;

namespace DotCraftCore.World.Gen.Feature
{

	using Block = DotCraftCore.block.Block;
	using Material = DotCraftCore.block.material.Material;
	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.World.World;

	public class WorldGenForest : WorldGenAbstractTree
	{
		private bool field_150531_a;
		private const string __OBFID = "CL_00000401";

		public WorldGenForest(bool p_i45449_1_, bool p_i45449_2_) : base(p_i45449_1_)
		{
			this.field_150531_a = p_i45449_2_;
		}

		public virtual bool generate(World p_76484_1_, Random p_76484_2_, int p_76484_3_, int p_76484_4_, int p_76484_5_)
		{
			int var6 = p_76484_2_.Next(3) + 5;

			if (this.field_150531_a)
			{
				var6 += p_76484_2_.Next(7);
			}

			bool var7 = true;

			if (p_76484_4_ >= 1 && p_76484_4_ + var6 + 1 <= 256)
			{
				int var10;
				int var11;

				for (int var8 = p_76484_4_; var8 <= p_76484_4_ + 1 + var6; ++var8)
				{
					sbyte var9 = 1;

					if (var8 == p_76484_4_)
					{
						var9 = 0;
					}

					if (var8 >= p_76484_4_ + 1 + var6 - 2)
					{
						var9 = 2;
					}

					for (var10 = p_76484_3_ - var9; var10 <= p_76484_3_ + var9 && var7; ++var10)
					{
						for (var11 = p_76484_5_ - var9; var11 <= p_76484_5_ + var9 && var7; ++var11)
						{
							if (var8 >= 0 && var8 < 256)
							{
								Block var12 = p_76484_1_.getBlock(var10, var8, var11);

								if (!this.func_150523_a(var12))
								{
									var7 = false;
								}
							}
							else
							{
								var7 = false;
							}
						}
					}
				}

				if (!var7)
				{
					return false;
				}
				else
				{
					Block var17 = p_76484_1_.getBlock(p_76484_3_, p_76484_4_ - 1, p_76484_5_);

					if ((var17 == Blocks.grass || var17 == Blocks.dirt || var17 == Blocks.farmland) && p_76484_4_ < 256 - var6 - 1)
					{
						this.func_150515_a(p_76484_1_, p_76484_3_, p_76484_4_ - 1, p_76484_5_, Blocks.dirt);
						int var18;

						for (var18 = p_76484_4_ - 3 + var6; var18 <= p_76484_4_ + var6; ++var18)
						{
							var10 = var18 - (p_76484_4_ + var6);
							var11 = 1 - var10 / 2;

							for (int var20 = p_76484_3_ - var11; var20 <= p_76484_3_ + var11; ++var20)
							{
								int var13 = var20 - p_76484_3_;

								for (int var14 = p_76484_5_ - var11; var14 <= p_76484_5_ + var11; ++var14)
								{
									int var15 = var14 - p_76484_5_;

									if (Math.Abs(var13) != var11 || Math.Abs(var15) != var11 || p_76484_2_.Next(2) != 0 && var10 != 0)
									{
										Block var16 = p_76484_1_.getBlock(var20, var18, var14);

										if (var16.Material == Material.air || var16.Material == Material.leaves)
										{
											this.func_150516_a(p_76484_1_, var20, var18, var14, Blocks.leaves, 2);
										}
									}
								}
							}
						}

						for (var18 = 0; var18 < var6; ++var18)
						{
							Block var19 = p_76484_1_.getBlock(p_76484_3_, p_76484_4_ + var18, p_76484_5_);

							if (var19.Material == Material.air || var19.Material == Material.leaves)
							{
								this.func_150516_a(p_76484_1_, p_76484_3_, p_76484_4_ + var18, p_76484_5_, Blocks.log, 2);
							}
						}

						return true;
					}
					else
					{
						return false;
					}
				}
			}
			else
			{
				return false;
			}
		}
	}

}