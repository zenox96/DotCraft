using System;
using System.Collections;

namespace DotCraftCore.nWorld.nGen.nStructure
{

	using EntityBlaze = DotCraftCore.entity.monster.EntityBlaze;
	using EntityMagmaCube = DotCraftCore.entity.monster.EntityMagmaCube;
	using EntityPigZombie = DotCraftCore.entity.monster.EntityPigZombie;
	using EntitySkeleton = DotCraftCore.entity.monster.EntitySkeleton;
	using World = DotCraftCore.nWorld.World;
	using BiomeGenBase = DotCraftCore.nWorld.nBiome.BiomeGenBase;

	public class MapGenNetherBridge : MapGenStructure
	{
		private IList spawnList = new ArrayList();
		

		public MapGenNetherBridge()
		{
			this.spawnList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntityBlaze), 10, 2, 3));
			this.spawnList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntityPigZombie), 5, 4, 4));
			this.spawnList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntitySkeleton), 10, 4, 4));
			this.spawnList.Add(new BiomeGenBase.SpawnListEntry(typeof(EntityMagmaCube), 3, 4, 4));
		}

		public override string func_143025_a()
		{
			return "Fortress";
		}

		public virtual IList SpawnList
		{
			get
			{
				return this.spawnList;
			}
		}

		protected internal override bool canSpawnStructureAtCoords(int p_75047_1_, int p_75047_2_)
		{
			int var3 = p_75047_1_ >> 4;
			int var4 = p_75047_2_ >> 4;
			this.rand.Seed = (long)(var3 ^ var4 << 4) ^ this.worldObj.Seed;
			this.rand.Next();
			return this.rand.Next(3) != 0 ? false : (p_75047_1_ != (var3 << 4) + 4 + this.rand.Next(8) ? false : p_75047_2_ == (var4 << 4) + 4 + this.rand.Next(8));
		}

		protected internal override StructureStart getStructureStart(int p_75049_1_, int p_75049_2_)
		{
			return new MapGenNetherBridge.Start(this.worldObj, this.rand, p_75049_1_, p_75049_2_);
		}

		public class Start : StructureStart
		{
			

			public Start()
			{
			}

			public Start(World p_i2040_1_, Random p_i2040_2_, int p_i2040_3_, int p_i2040_4_) : base(p_i2040_3_, p_i2040_4_)
			{
				StructureNetherBridgePieces.Start var5 = new StructureNetherBridgePieces.Start(p_i2040_2_, (p_i2040_3_ << 4) + 2, (p_i2040_4_ << 4) + 2);
				this.components.add(var5);
				var5.buildComponent(var5, this.components, p_i2040_2_);
				ArrayList var6 = var5.field_74967_d;

				while (!var6.Count == 0)
				{
					int var7 = p_i2040_2_.Next(var6.Count);
					StructureComponent var8 = (StructureComponent)var6.Remove(var7);
					var8.buildComponent(var5, this.components, p_i2040_2_);
				}

				this.updateBoundingBox();
				this.setRandomHeight(p_i2040_1_, p_i2040_2_, 48, 70);
			}
		}
	}

}