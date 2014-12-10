using System;
using System.Collections;

namespace DotCraftCore.World.Gen.Structure
{

	using Block = DotCraftCore.block.Block;
	using CrashReport = DotCraftCore.crash.CrashReport;
	using CrashReportCategory = DotCraftCore.crash.CrashReportCategory;
	using NBTBase = DotCraftCore.nbt.NBTBase;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using ReportedException = DotCraftCore.Util.ReportedException;
	using ChunkCoordIntPair = DotCraftCore.World.ChunkCoordIntPair;
	using ChunkPosition = DotCraftCore.World.ChunkPosition;
	using World = DotCraftCore.World.World;
	using MapGenBase = DotCraftCore.World.Gen.MapGenBase;

	public abstract class MapGenStructure : MapGenBase
	{
		private MapGenStructureData field_143029_e;

///    
///     <summary> * Used to store a list of all structures that have been recursively generated. Used so that during recursive
///     * generation, the structure generator can avoid generating structures that intersect ones that have already been
///     * placed. </summary>
///     
		protected internal IDictionary structureMap = new Hashtable();
		private const string __OBFID = "CL_00000505";

		public abstract string func_143025_a();

//JAVA TO VB & C# CONVERTER WARNING: 'final' parameters are not allowed in .NET:
//ORIGINAL LINE: protected final void func_151538_a(World p_151538_1_, final int p_151538_2_, final int p_151538_3_, int p_151538_4_, int p_151538_5_, Block[] p_151538_6_)
		protected internal void func_151538_a(World p_151538_1_, int p_151538_2_, int p_151538_3_, int p_151538_4_, int p_151538_5_, Block[] p_151538_6_)
		{
			this.func_143027_a(p_151538_1_);

			if (!this.structureMap.ContainsKey(Convert.ToInt64(ChunkCoordIntPair.chunkXZ2Int(p_151538_2_, p_151538_3_))))
			{
				this.rand.Next();

				try
				{
					if (this.canSpawnStructureAtCoords(p_151538_2_, p_151538_3_))
					{
						StructureStart var7 = this.getStructureStart(p_151538_2_, p_151538_3_);
						this.structureMap.Add(Convert.ToInt64(ChunkCoordIntPair.chunkXZ2Int(p_151538_2_, p_151538_3_)), var7);
						this.func_143026_a(p_151538_2_, p_151538_3_, var7);
					}
				}
				catch (Exception var10)
				{
					CrashReport var8 = CrashReport.makeCrashReport(var10, "Exception preparing structure feature");
					CrashReportCategory var9 = var8.makeCategory("Feature being prepared");
					var9.addCrashSectionCallable("Is feature chunk", new Callable() { private static final string __OBFID = "CL_00000506"; public string call() { return MapGenStructure.canSpawnStructureAtCoords(p_151538_2_, p_151538_3_) ? "True" : "False"; } });
					var9.addCrashSection("Chunk location", string.Format("{0:D},{1:D}", new object[] {Convert.ToInt32(p_151538_2_), Convert.ToInt32(p_151538_3_)}));
					var9.addCrashSectionCallable("Chunk pos hash", new Callable() { private static final string __OBFID = "CL_00000507"; public string call() { return Convert.ToString(ChunkCoordIntPair.chunkXZ2Int(p_151538_2_, p_151538_3_)); } });
					var9.addCrashSectionCallable("Structure type", new Callable() { private static final string __OBFID = "CL_00000508"; public string call() { return MapGenStructure.GetType().CanonicalName; } });
					throw new ReportedException(var8);
				}
			}
		}

///    
///     <summary> * Generates structures in specified chunk next to existing structures. Does *not* generate StructureStarts. </summary>
///     
		public virtual bool generateStructuresInChunk(World p_75051_1_, Random p_75051_2_, int p_75051_3_, int p_75051_4_)
		{
			this.func_143027_a(p_75051_1_);
			int var5 = (p_75051_3_ << 4) + 8;
			int var6 = (p_75051_4_ << 4) + 8;
			bool var7 = false;
			IEnumerator var8 = this.structureMap.Values.GetEnumerator();

			while (var8.MoveNext())
			{
				StructureStart var9 = (StructureStart)var8.Current;

				if (var9.SizeableStructure && var9.BoundingBox.intersectsWith(var5, var6, var5 + 15, var6 + 15))
				{
					var9.generateStructure(p_75051_1_, p_75051_2_, new StructureBoundingBox(var5, var6, var5 + 15, var6 + 15));
					var7 = true;
					this.func_143026_a(var9.func_143019_e(), var9.func_143018_f(), var9);
				}
			}

			return var7;
		}

///    
///     <summary> * Returns true if the structure generator has generated a structure located at the given position tuple. </summary>
///     
		public virtual bool hasStructureAt(int p_75048_1_, int p_75048_2_, int p_75048_3_)
		{
			this.func_143027_a(this.worldObj);
			return this.func_143028_c(p_75048_1_, p_75048_2_, p_75048_3_) != null;
		}

		protected internal virtual StructureStart func_143028_c(int p_143028_1_, int p_143028_2_, int p_143028_3_)
		{
			IEnumerator var4 = this.structureMap.Values.GetEnumerator();

			while (var4.MoveNext())
			{
				StructureStart var5 = (StructureStart)var4.Current;

				if (var5.SizeableStructure && var5.BoundingBox.intersectsWith(p_143028_1_, p_143028_3_, p_143028_1_, p_143028_3_))
				{
					IEnumerator var6 = var5.Components.GetEnumerator();

					while (var6.MoveNext())
					{
						StructureComponent var7 = (StructureComponent)var6.Current;

						if (var7.BoundingBox.isVecInside(p_143028_1_, p_143028_2_, p_143028_3_))
						{
							return var5;
						}
					}
				}
			}

			return null;
		}

		public virtual bool func_142038_b(int p_142038_1_, int p_142038_2_, int p_142038_3_)
		{
			this.func_143027_a(this.worldObj);
			IEnumerator var4 = this.structureMap.Values.GetEnumerator();
			StructureStart var5;

			do
			{
				if (!var4.MoveNext())
				{
					return false;
				}

				var5 = (StructureStart)var4.Current;
			}
			while (!var5.SizeableStructure);

			return var5.BoundingBox.intersectsWith(p_142038_1_, p_142038_3_, p_142038_1_, p_142038_3_);
		}

		public virtual ChunkPosition func_151545_a(World p_151545_1_, int p_151545_2_, int p_151545_3_, int p_151545_4_)
		{
			this.worldObj = p_151545_1_;
			this.func_143027_a(p_151545_1_);
			this.rand.Seed = p_151545_1_.Seed;
			long var5 = this.rand.nextLong();
			long var7 = this.rand.nextLong();
			long var9 = (long)(p_151545_2_ >> 4) * var5;
			long var11 = (long)(p_151545_4_ >> 4) * var7;
			this.rand.Seed = var9 ^ var11 ^ p_151545_1_.Seed;
			this.func_151538_a(p_151545_1_, p_151545_2_ >> 4, p_151545_4_ >> 4, 0, 0, (Block[])null);
			double var13 = double.MaxValue;
			ChunkPosition var15 = null;
			IEnumerator var16 = this.structureMap.Values.GetEnumerator();
			ChunkPosition var19;
			int var20;
			int var21;
			int var22;
			double var23;

			while (var16.MoveNext())
			{
				StructureStart var17 = (StructureStart)var16.Current;

				if (var17.SizeableStructure)
				{
					StructureComponent var18 = (StructureComponent)var17.Components.get(0);
					var19 = var18.func_151553_a();
					var20 = var19.field_151329_a - p_151545_2_;
					var21 = var19.field_151327_b - p_151545_3_;
					var22 = var19.field_151328_c - p_151545_4_;
					var23 = (double)(var20 * var20 + var21 * var21 + var22 * var22);

					if (var23 < var13)
					{
						var13 = var23;
						var15 = var19;
					}
				}
			}

			if (var15 != null)
			{
				return var15;
			}
			else
			{
				IList var25 = this.CoordList;

				if (var25 != null)
				{
					ChunkPosition var26 = null;
					IEnumerator var27 = var25.GetEnumerator();

					while (var27.MoveNext())
					{
						var19 = (ChunkPosition)var27.Current;
						var20 = var19.field_151329_a - p_151545_2_;
						var21 = var19.field_151327_b - p_151545_3_;
						var22 = var19.field_151328_c - p_151545_4_;
						var23 = (double)(var20 * var20 + var21 * var21 + var22 * var22);

						if (var23 < var13)
						{
							var13 = var23;
							var26 = var19;
						}
					}

					return var26;
				}
				else
				{
					return null;
				}
			}
		}

///    
///     <summary> * Returns a list of other locations at which the structure generation has been run, or null if not relevant to this
///     * structure generator. </summary>
///     
		protected internal virtual IList CoordList
		{
			get
			{
				return null;
			}
		}

		private void func_143027_a(World p_143027_1_)
		{
			if (this.field_143029_e == null)
			{
				this.field_143029_e = (MapGenStructureData)p_143027_1_.loadItemData(typeof(MapGenStructureData), this.func_143025_a());

				if (this.field_143029_e == null)
				{
					this.field_143029_e = new MapGenStructureData(this.func_143025_a());
					p_143027_1_.setItemData(this.func_143025_a(), this.field_143029_e);
				}
				else
				{
					NBTTagCompound var2 = this.field_143029_e.func_143041_a();
					IEnumerator var3 = var2.func_150296_c().GetEnumerator();

					while (var3.MoveNext())
					{
						string var4 = (string)var3.Current;
						NBTBase var5 = var2.getTag(var4);

						if (var5.Id == 10)
						{
							NBTTagCompound var6 = (NBTTagCompound)var5;

							if (var6.hasKey("ChunkX") && var6.hasKey("ChunkZ"))
							{
								int var7 = var6.getInteger("ChunkX");
								int var8 = var6.getInteger("ChunkZ");
								StructureStart var9 = MapGenStructureIO.func_143035_a(var6, p_143027_1_);

								if (var9 != null)
								{
									this.structureMap.Add(Convert.ToInt64(ChunkCoordIntPair.chunkXZ2Int(var7, var8)), var9);
								}
							}
						}
					}
				}
			}
		}

		private void func_143026_a(int p_143026_1_, int p_143026_2_, StructureStart p_143026_3_)
		{
			this.field_143029_e.func_143043_a(p_143026_3_.func_143021_a(p_143026_1_, p_143026_2_), p_143026_1_, p_143026_2_);
			this.field_143029_e.markDirty();
		}

		protected internal abstract bool canSpawnStructureAtCoords(int p_75047_1_, int p_75047_2_);

		protected internal abstract StructureStart getStructureStart(int p_75049_1_, int p_75049_2_);
	}

}