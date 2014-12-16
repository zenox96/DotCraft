using System;
using System.Collections;

namespace DotCraftCore.World.Gen.Structure
{

	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using MathHelper = DotCraftCore.Util.MathHelper;
	using World = DotCraftCore.World.World;
	using BiomeGenBase = DotCraftCore.World.Biome.BiomeGenBase;

	public class MapGenVillage : MapGenStructure
	{
	/// <summary> A list of all the biomes villages can spawn in.  </summary>
		public static readonly IList villageSpawnBiomes = Arrays.asList(new BiomeGenBase[] {BiomeGenBase.plains, BiomeGenBase.desert, BiomeGenBase.field_150588_X});

	/// <summary> World terrain type, 0 for normal, 1 for flat map  </summary>
		private int terrainType;
		private int field_82665_g;
		private int field_82666_h;
		

		public MapGenVillage()
		{
			this.field_82665_g = 32;
			this.field_82666_h = 8;
		}

		public MapGenVillage(IDictionary p_i2093_1_) : this()
		{
			IEnumerator var2 = p_i2093_1_.GetEnumerator();

			while (var2.MoveNext())
			{
				Entry var3 = (Entry)var2.Current;

				if (((string)var3.Key).Equals("size"))
				{
					this.terrainType = MathHelper.parseIntWithDefaultAndMax((string)var3.Value, this.terrainType, 0);
				}
				else if (((string)var3.Key).Equals("distance"))
				{
					this.field_82665_g = MathHelper.parseIntWithDefaultAndMax((string)var3.Value, this.field_82665_g, this.field_82666_h + 1);
				}
			}
		}

		public override string func_143025_a()
		{
			return "Village";
		}

		protected internal override bool canSpawnStructureAtCoords(int p_75047_1_, int p_75047_2_)
		{
			int var3 = p_75047_1_;
			int var4 = p_75047_2_;

			if (p_75047_1_ < 0)
			{
				p_75047_1_ -= this.field_82665_g - 1;
			}

			if (p_75047_2_ < 0)
			{
				p_75047_2_ -= this.field_82665_g - 1;
			}

			int var5 = p_75047_1_ / this.field_82665_g;
			int var6 = p_75047_2_ / this.field_82665_g;
			Random var7 = this.worldObj.setRandomSeed(var5, var6, 10387312);
			var5 *= this.field_82665_g;
			var6 *= this.field_82665_g;
			var5 += var7.Next(this.field_82665_g - this.field_82666_h);
			var6 += var7.Next(this.field_82665_g - this.field_82666_h);

			if (var3 == var5 && var4 == var6)
			{
				bool var8 = this.worldObj.WorldChunkManager.areBiomesViable(var3 * 16 + 8, var4 * 16 + 8, 0, villageSpawnBiomes);

				if (var8)
				{
					return true;
				}
			}

			return false;
		}

		protected internal override StructureStart getStructureStart(int p_75049_1_, int p_75049_2_)
		{
			return new MapGenVillage.Start(this.worldObj, this.rand, p_75049_1_, p_75049_2_, this.terrainType);
		}

		public class Start : StructureStart
		{
			private bool hasMoreThanTwoComponents;
			

			public Start()
			{
			}

			public Start(World p_i2092_1_, Random p_i2092_2_, int p_i2092_3_, int p_i2092_4_, int p_i2092_5_) : base(p_i2092_3_, p_i2092_4_)
			{
				IList var6 = StructureVillagePieces.getStructureVillageWeightedPieceList(p_i2092_2_, p_i2092_5_);
				StructureVillagePieces.Start var7 = new StructureVillagePieces.Start(p_i2092_1_.WorldChunkManager, 0, p_i2092_2_, (p_i2092_3_ << 4) + 2, (p_i2092_4_ << 4) + 2, var6, p_i2092_5_);
				this.components.add(var7);
				var7.buildComponent(var7, this.components, p_i2092_2_);
				IList var8 = var7.field_74930_j;
				IList var9 = var7.field_74932_i;
				int var10;

				while (!var8.Count == 0 || !var9.Count == 0)
				{
					StructureComponent var11;

					if (var8.Count == 0)
					{
						var10 = p_i2092_2_.Next(var9.Count);
						var11 = (StructureComponent)var9.Remove(var10);
						var11.buildComponent(var7, this.components, p_i2092_2_);
					}
					else
					{
						var10 = p_i2092_2_.Next(var8.Count);
						var11 = (StructureComponent)var8.Remove(var10);
						var11.buildComponent(var7, this.components, p_i2092_2_);
					}
				}

				this.updateBoundingBox();
				var10 = 0;
				IEnumerator var13 = this.components.GetEnumerator();

				while (var13.MoveNext())
				{
					StructureComponent var12 = (StructureComponent)var13.Current;

					if (!(var12 is StructureVillagePieces.Road))
					{
						++var10;
					}
				}

				this.hasMoreThanTwoComponents = var10 > 2;
			}

			public override bool isSizeableStructure()
			{
				get
				{
					return this.hasMoreThanTwoComponents;
				}
			}

			public override void func_143022_a(NBTTagCompound p_143022_1_)
			{
				base.func_143022_a(p_143022_1_);
				p_143022_1_.setBoolean("Valid", this.hasMoreThanTwoComponents);
			}

			public override void func_143017_b(NBTTagCompound p_143017_1_)
			{
				base.func_143017_b(p_143017_1_);
				this.hasMoreThanTwoComponents = p_143017_1_.getBoolean("Valid");
			}
		}
	}

}