using System;
using System.Collections;

namespace DotCraftCore.World.Gen.Structure
{

	using MathHelper = DotCraftCore.Util.MathHelper;
	using ChunkCoordIntPair = DotCraftCore.World.ChunkCoordIntPair;
	using ChunkPosition = DotCraftCore.World.ChunkPosition;
	using World = DotCraftCore.World.World;
	using BiomeGenBase = DotCraftCore.World.Biome.BiomeGenBase;

	public class MapGenStronghold : MapGenStructure
	{
		private IList field_151546_e;

///    
///     <summary> * is spawned false and set true once the defined BiomeGenBases were compared with the present ones </summary>
///     
		private bool ranBiomeCheck;
		private ChunkCoordIntPair[] structureCoords;
		private double field_82671_h;
		private int field_82672_i;
		private const string __OBFID = "CL_00000481";

		public MapGenStronghold()
		{
			this.structureCoords = new ChunkCoordIntPair[3];
			this.field_82671_h = 32.0D;
			this.field_82672_i = 3;
			this.field_151546_e = new ArrayList();
			BiomeGenBase[] var1 = BiomeGenBase.BiomeGenArray;
			int var2 = var1.Length;

			for (int var3 = 0; var3 < var2; ++var3)
			{
				BiomeGenBase var4 = var1[var3];

				if (var4 != null && var4.minHeight > 0.0F)
				{
					this.field_151546_e.Add(var4);
				}
			}
		}

		public MapGenStronghold(IDictionary p_i2068_1_) : this()
		{
			IEnumerator var2 = p_i2068_1_.GetEnumerator();

			while (var2.MoveNext())
			{
				Entry var3 = (Entry)var2.Current;

				if (((string)var3.Key).Equals("distance"))
				{
					this.field_82671_h = MathHelper.parseDoubleWithDefaultAndMax((string)var3.Value, this.field_82671_h, 1.0D);
				}
				else if (((string)var3.Key).Equals("count"))
				{
					this.structureCoords = new ChunkCoordIntPair[MathHelper.parseIntWithDefaultAndMax((string)var3.Value, this.structureCoords.Length, 1)];
				}
				else if (((string)var3.Key).Equals("spread"))
				{
					this.field_82672_i = MathHelper.parseIntWithDefaultAndMax((string)var3.Value, this.field_82672_i, 1);
				}
			}
		}

		public override string func_143025_a()
		{
			return "Stronghold";
		}

		protected internal override bool canSpawnStructureAtCoords(int p_75047_1_, int p_75047_2_)
		{
			if (!this.ranBiomeCheck)
			{
				Random var3 = new Random();
				var3.Seed = this.worldObj.Seed;
				double var4 = var3.NextDouble() * Math.PI * 2.0D;
				int var6 = 1;

				for (int var7 = 0; var7 < this.structureCoords.Length; ++var7)
				{
					double var8 = (1.25D * (double)var6 + var3.NextDouble()) * this.field_82671_h * (double)var6;
					int var10 = (int)Math.Round(Math.Cos(var4) * var8);
					int var11 = (int)Math.Round(Math.Sin(var4) * var8);
					ChunkPosition var12 = this.worldObj.WorldChunkManager.func_150795_a((var10 << 4) + 8, (var11 << 4) + 8, 112, this.field_151546_e, var3);

					if (var12 != null)
					{
						var10 = var12.field_151329_a >> 4;
						var11 = var12.field_151328_c >> 4;
					}

					this.structureCoords[var7] = new ChunkCoordIntPair(var10, var11);
					var4 += (Math.PI * 2D) * (double)var6 / (double)this.field_82672_i;

					if (var7 == this.field_82672_i)
					{
						var6 += 2 + var3.Next(5);
						this.field_82672_i += 1 + var3.Next(2);
					}
				}

				this.ranBiomeCheck = true;
			}

			ChunkCoordIntPair[] var13 = this.structureCoords;
			int var14 = var13.Length;

			for (int var5 = 0; var5 < var14; ++var5)
			{
				ChunkCoordIntPair var15 = var13[var5];

				if (p_75047_1_ == var15.chunkXPos && p_75047_2_ == var15.chunkZPos)
				{
					return true;
				}
			}

			return false;
		}

///    
///     <summary> * Returns a list of other locations at which the structure generation has been run, or null if not relevant to this
///     * structure generator. </summary>
///     
		protected internal override IList CoordList
		{
			get
			{
				ArrayList var1 = new ArrayList();
				ChunkCoordIntPair[] var2 = this.structureCoords;
				int var3 = var2.Length;
	
				for (int var4 = 0; var4 < var3; ++var4)
				{
					ChunkCoordIntPair var5 = var2[var4];
	
					if (var5 != null)
					{
						var1.Add(var5.func_151349_a(64));
					}
				}
	
				return var1;
			}
		}

		protected internal override StructureStart getStructureStart(int p_75049_1_, int p_75049_2_)
		{
			MapGenStronghold.Start var3;

			for (var3 = new MapGenStronghold.Start(this.worldObj, this.rand, p_75049_1_, p_75049_2_); var3.Components.Empty || ((StructureStrongholdPieces.Stairs2)var3.Components.get(0)).strongholdPortalRoom == null; var3 = new MapGenStronghold.Start(this.worldObj, this.rand, p_75049_1_, p_75049_2_))
			{
				;
			}

			return var3;
		}

		public class Start : StructureStart
		{
			private const string __OBFID = "CL_00000482";

			public Start()
			{
			}

			public Start(World p_i2067_1_, Random p_i2067_2_, int p_i2067_3_, int p_i2067_4_) : base(p_i2067_3_, p_i2067_4_)
			{
				StructureStrongholdPieces.prepareStructurePieces();
				StructureStrongholdPieces.Stairs2 var5 = new StructureStrongholdPieces.Stairs2(0, p_i2067_2_, (p_i2067_3_ << 4) + 2, (p_i2067_4_ << 4) + 2);
				this.components.add(var5);
				var5.buildComponent(var5, this.components, p_i2067_2_);
				IList var6 = var5.field_75026_c;

				while (!var6.Count == 0)
				{
					int var7 = p_i2067_2_.Next(var6.Count);
					StructureComponent var8 = (StructureComponent)var6.Remove(var7);
					var8.buildComponent(var5, this.components, p_i2067_2_);
				}

				this.updateBoundingBox();
				this.markAvailableHeight(p_i2067_1_, p_i2067_2_, 10);
			}
		}
	}

}