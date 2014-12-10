using System;

namespace DotCraftCore.World.Biome
{

	using Block = DotCraftCore.block.Block;
	using BlockFlower = DotCraftCore.block.BlockFlower;
	using Material = DotCraftCore.block.material.Material;
	using EntitySlime = DotCraftCore.entity.monster.EntitySlime;
	using Blocks = DotCraftCore.init.Blocks;
	using World = DotCraftCore.World.World;
	using WorldGenAbstractTree = DotCraftCore.World.Gen.Feature.WorldGenAbstractTree;

	public class BiomeGenSwamp : BiomeGenBase
	{
		private const string __OBFID = "CL_00000185";

		protected internal BiomeGenSwamp(int p_i1988_1_) : base(p_i1988_1_)
		{
			this.theBiomeDecorator.treesPerChunk = 2;
			this.theBiomeDecorator.flowersPerChunk = 1;
			this.theBiomeDecorator.deadBushPerChunk = 1;
			this.theBiomeDecorator.mushroomsPerChunk = 8;
			this.theBiomeDecorator.reedsPerChunk = 10;
			this.theBiomeDecorator.clayPerChunk = 1;
			this.theBiomeDecorator.waterlilyPerChunk = 4;
			this.theBiomeDecorator.sandPerChunk2 = 0;
			this.theBiomeDecorator.sandPerChunk = 0;
			this.theBiomeDecorator.grassPerChunk = 5;
			this.waterColorMultiplier = 14745518;
			this.spawnableMonsterList.add(new BiomeGenBase.SpawnListEntry(typeof(EntitySlime), 1, 1, 1));
		}

		public virtual WorldGenAbstractTree func_150567_a(Random p_150567_1_)
		{
			return this.worldGeneratorSwamp;
		}

///    
///     <summary> * Provides the basic grass color based on the biome temperature and rainfall </summary>
///     
		public virtual int getBiomeGrassColor(int p_150558_1_, int p_150558_2_, int p_150558_3_)
		{
			double var4 = field_150606_ad.func_151601_a((double)p_150558_1_ * 0.0225D, (double)p_150558_3_ * 0.0225D);
			return var4 < -0.1D ? 5011004 : 6975545;
		}

///    
///     <summary> * Provides the basic foliage color based on the biome temperature and rainfall </summary>
///     
		public virtual int getBiomeFoliageColor(int p_150571_1_, int p_150571_2_, int p_150571_3_)
		{
			return 6975545;
		}

		public virtual string func_150572_a(Random p_150572_1_, int p_150572_2_, int p_150572_3_, int p_150572_4_)
		{
			return BlockFlower.field_149859_a[1];
		}

		public virtual void func_150573_a(World p_150573_1_, Random p_150573_2_, Block[] p_150573_3_, sbyte[] p_150573_4_, int p_150573_5_, int p_150573_6_, double p_150573_7_)
		{
			double var9 = field_150606_ad.func_151601_a((double)p_150573_5_ * 0.25D, (double)p_150573_6_ * 0.25D);

			if (var9 > 0.0D)
			{
				int var11 = p_150573_5_ & 15;
				int var12 = p_150573_6_ & 15;
				int var13 = p_150573_3_.Length / 256;

				for (int var14 = 255; var14 >= 0; --var14)
				{
					int var15 = (var12 * 16 + var11) * var13 + var14;

					if (p_150573_3_[var15] == null || p_150573_3_[var15].Material != Material.air)
					{
						if (var14 == 62 && p_150573_3_[var15] != Blocks.water)
						{
							p_150573_3_[var15] = Blocks.water;

							if (var9 < 0.12D)
							{
								p_150573_3_[var15 + 1] = Blocks.waterlily;
							}
						}

						break;
					}
				}
			}

			this.func_150560_b(p_150573_1_, p_150573_2_, p_150573_3_, p_150573_4_, p_150573_5_, p_150573_6_, p_150573_7_);
		}
	}

}