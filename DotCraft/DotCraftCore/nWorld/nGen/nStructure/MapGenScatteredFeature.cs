using System;
using System.Collections;

namespace DotCraftCore.nWorld.nGen.nStructure
{

	using EntityWitch = DotCraftCore.entity.monster.EntityWitch;
	using MathHelper = DotCraftCore.nUtil.MathHelper;
	using World = DotCraftCore.nWorld.World;
	using BiomeGenBase = DotCraftCore.nWorld.nBiome.BiomeGenBase;

	public class MapGenScatteredFeature : MapGenStructure
	{
		private static IList biomelist = Arrays.asList(new BiomeGenBase[] {BiomeGenBase.desert, BiomeGenBase.desertHills, BiomeGenBase.jungle, BiomeGenBase.jungleHills, BiomeGenBase.swampland});

	/// <summary> contains possible spawns for scattered features  </summary>
		private IList scatteredFeatureSpawnList;

	/// <summary> the maximum distance between scattered features  </summary>
		private int maxDistanceBetweenScatteredFeatures;

	/// <summary> the minimum distance between scattered features  </summary>
		private int minDistanceBetweenScatteredFeatures;
		

		public MapGenScatteredFeature()
		{
			this.scatteredFeatureSpawnList = new ArrayList();
			this.maxDistanceBetweenScatteredFeatures = 32;
			this.minDistanceBetweenScatteredFeatures = 8;
			this.scatteredFeatureSpawnList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntityWitch), 1, 1, 1));
		}

		public MapGenScatteredFeature(IDictionary p_i2061_1_) : this()
		{
			IEnumerator var2 = p_i2061_1_.GetEnumerator();

			while (var2.MoveNext())
			{
				Entry var3 = (Entry)var2.Current;

				if (((string)var3.Key).Equals("distance"))
				{
					this.maxDistanceBetweenScatteredFeatures = MathHelper.parseIntWithDefaultAndMax((string)var3.Value, this.maxDistanceBetweenScatteredFeatures, this.minDistanceBetweenScatteredFeatures + 1);
				}
			}
		}

		public override string func_143025_a()
		{
			return "Temple";
		}

		protected internal override bool canSpawnStructureAtCoords(int p_75047_1_, int p_75047_2_)
		{
			int var3 = p_75047_1_;
			int var4 = p_75047_2_;

			if (p_75047_1_ < 0)
			{
				p_75047_1_ -= this.maxDistanceBetweenScatteredFeatures - 1;
			}

			if (p_75047_2_ < 0)
			{
				p_75047_2_ -= this.maxDistanceBetweenScatteredFeatures - 1;
			}

			int var5 = p_75047_1_ / this.maxDistanceBetweenScatteredFeatures;
			int var6 = p_75047_2_ / this.maxDistanceBetweenScatteredFeatures;
			Random var7 = this.worldObj.setRandomSeed(var5, var6, 14357617);
			var5 *= this.maxDistanceBetweenScatteredFeatures;
			var6 *= this.maxDistanceBetweenScatteredFeatures;
			var5 += var7.Next(this.maxDistanceBetweenScatteredFeatures - this.minDistanceBetweenScatteredFeatures);
			var6 += var7.Next(this.maxDistanceBetweenScatteredFeatures - this.minDistanceBetweenScatteredFeatures);

			if (var3 == var5 && var4 == var6)
			{
				BiomeGenBase var8 = this.worldObj.WorldChunkManager.getBiomeGenAt(var3 * 16 + 8, var4 * 16 + 8);
				IEnumerator var9 = biomelist.GetEnumerator();

				while (var9.MoveNext())
				{
					BiomeGenBase var10 = (BiomeGenBase)var9.Current;

					if (var8 == var10)
					{
						return true;
					}
				}
			}

			return false;
		}

		protected internal override StructureStart getStructureStart(int p_75049_1_, int p_75049_2_)
		{
			return new MapGenScatteredFeature.Start(this.worldObj, this.rand, p_75049_1_, p_75049_2_);
		}

		public virtual bool func_143030_a(int p_143030_1_, int p_143030_2_, int p_143030_3_)
		{
			StructureStart var4 = this.func_143028_c(p_143030_1_, p_143030_2_, p_143030_3_);

			if (var4 != null && var4 is MapGenScatteredFeature.Start && !var4.components.Count == 0)
			{
				StructureComponent var5 = (StructureComponent)var4.components.First.Value;
				return var5 is ComponentScatteredFeaturePieces.SwampHut;
			}
			else
			{
				return false;
			}
		}

///    
///     <summary> * returns possible spawns for scattered features </summary>
///     
		public virtual IList ScatteredFeatureSpawnList
		{
			get
			{
				return this.scatteredFeatureSpawnList;
			}
		}

		public class Start : StructureStart
		{
			

			public Start()
			{
			}

			public Start(World p_i2060_1_, Random p_i2060_2_, int p_i2060_3_, int p_i2060_4_) : base(p_i2060_3_, p_i2060_4_)
			{
				BiomeGenBase var5 = p_i2060_1_.getBiomeGenForCoords(p_i2060_3_ * 16 + 8, p_i2060_4_ * 16 + 8);

				if (var5 != BiomeGenBase.jungle && var5 != BiomeGenBase.jungleHills)
				{
					if (var5 == BiomeGenBase.swampland)
					{
						ComponentScatteredFeaturePieces.SwampHut var7 = new ComponentScatteredFeaturePieces.SwampHut(p_i2060_2_, p_i2060_3_ * 16, p_i2060_4_ * 16);
						this.components.add(var7);
					}
					else
					{
						ComponentScatteredFeaturePieces.DesertPyramid var8 = new ComponentScatteredFeaturePieces.DesertPyramid(p_i2060_2_, p_i2060_3_ * 16, p_i2060_4_ * 16);
						this.components.add(var8);
					}
				}
				else
				{
					ComponentScatteredFeaturePieces.JunglePyramid var6 = new ComponentScatteredFeaturePieces.JunglePyramid(p_i2060_2_, p_i2060_3_ * 16, p_i2060_4_ * 16);
					this.components.add(var6);
				}

				this.updateBoundingBox();
			}
		}
	}

}