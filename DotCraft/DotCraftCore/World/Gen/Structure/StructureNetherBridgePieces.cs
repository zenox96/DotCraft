using System;
using System.Collections;

namespace DotCraftCore.World.Gen.Structure
{

	using Blocks = DotCraftCore.init.Blocks;
	using Items = DotCraftCore.init.Items;
	using NBTTagCompound = DotCraftCore.nbt.NBTTagCompound;
	using TileEntityMobSpawner = DotCraftCore.TileEntity.TileEntityMobSpawner;
	using WeightedRandomChestContent = DotCraftCore.Util.WeightedRandomChestContent;
	using World = DotCraftCore.World.World;

	public class StructureNetherBridgePieces
	{
		private static readonly StructureNetherBridgePieces.PieceWeight[] primaryComponents = new StructureNetherBridgePieces.PieceWeight[] {new StructureNetherBridgePieces.PieceWeight(typeof(StructureNetherBridgePieces.Straight), 30, 0, true), new StructureNetherBridgePieces.PieceWeight(typeof(StructureNetherBridgePieces.Crossing3), 10, 4), new StructureNetherBridgePieces.PieceWeight(typeof(StructureNetherBridgePieces.Crossing), 10, 4), new StructureNetherBridgePieces.PieceWeight(typeof(StructureNetherBridgePieces.Stairs), 10, 3), new StructureNetherBridgePieces.PieceWeight(typeof(StructureNetherBridgePieces.Throne), 5, 2), new StructureNetherBridgePieces.PieceWeight(typeof(StructureNetherBridgePieces.Entrance), 5, 1)};
		private static readonly StructureNetherBridgePieces.PieceWeight[] secondaryComponents = new StructureNetherBridgePieces.PieceWeight[] {new StructureNetherBridgePieces.PieceWeight(typeof(StructureNetherBridgePieces.Corridor5), 25, 0, true), new StructureNetherBridgePieces.PieceWeight(typeof(StructureNetherBridgePieces.Crossing2), 15, 5), new StructureNetherBridgePieces.PieceWeight(typeof(StructureNetherBridgePieces.Corridor2), 5, 10), new StructureNetherBridgePieces.PieceWeight(typeof(StructureNetherBridgePieces.Corridor), 5, 10), new StructureNetherBridgePieces.PieceWeight(typeof(StructureNetherBridgePieces.Corridor3), 10, 3, true), new StructureNetherBridgePieces.PieceWeight(typeof(StructureNetherBridgePieces.Corridor4), 7, 2), new StructureNetherBridgePieces.PieceWeight(typeof(StructureNetherBridgePieces.NetherStalkRoom), 5, 2)};
		private const string __OBFID = "CL_00000453";

		public static void func_143049_a()
		{
			MapGenStructureIO.func_143031_a(typeof(StructureNetherBridgePieces.Crossing3), "NeBCr");
			MapGenStructureIO.func_143031_a(typeof(StructureNetherBridgePieces.End), "NeBEF");
			MapGenStructureIO.func_143031_a(typeof(StructureNetherBridgePieces.Straight), "NeBS");
			MapGenStructureIO.func_143031_a(typeof(StructureNetherBridgePieces.Corridor3), "NeCCS");
			MapGenStructureIO.func_143031_a(typeof(StructureNetherBridgePieces.Corridor4), "NeCTB");
			MapGenStructureIO.func_143031_a(typeof(StructureNetherBridgePieces.Entrance), "NeCE");
			MapGenStructureIO.func_143031_a(typeof(StructureNetherBridgePieces.Crossing2), "NeSCSC");
			MapGenStructureIO.func_143031_a(typeof(StructureNetherBridgePieces.Corridor), "NeSCLT");
			MapGenStructureIO.func_143031_a(typeof(StructureNetherBridgePieces.Corridor5), "NeSC");
			MapGenStructureIO.func_143031_a(typeof(StructureNetherBridgePieces.Corridor2), "NeSCRT");
			MapGenStructureIO.func_143031_a(typeof(StructureNetherBridgePieces.NetherStalkRoom), "NeCSR");
			MapGenStructureIO.func_143031_a(typeof(StructureNetherBridgePieces.Throne), "NeMT");
			MapGenStructureIO.func_143031_a(typeof(StructureNetherBridgePieces.Crossing), "NeRC");
			MapGenStructureIO.func_143031_a(typeof(StructureNetherBridgePieces.Stairs), "NeSR");
			MapGenStructureIO.func_143031_a(typeof(StructureNetherBridgePieces.Start), "NeStart");
		}

		private static StructureNetherBridgePieces.Piece createNextComponentRandom(StructureNetherBridgePieces.PieceWeight p_78738_0_, IList p_78738_1_, Random p_78738_2_, int p_78738_3_, int p_78738_4_, int p_78738_5_, int p_78738_6_, int p_78738_7_)
		{
			Type var8 = p_78738_0_.weightClass;
			object var9 = null;

			if (var8 == typeof(StructureNetherBridgePieces.Straight))
			{
				var9 = StructureNetherBridgePieces.Straight.createValidComponent(p_78738_1_, p_78738_2_, p_78738_3_, p_78738_4_, p_78738_5_, p_78738_6_, p_78738_7_);
			}
			else if (var8 == typeof(StructureNetherBridgePieces.Crossing3))
			{
				var9 = StructureNetherBridgePieces.Crossing3.createValidComponent(p_78738_1_, p_78738_2_, p_78738_3_, p_78738_4_, p_78738_5_, p_78738_6_, p_78738_7_);
			}
			else if (var8 == typeof(StructureNetherBridgePieces.Crossing))
			{
				var9 = StructureNetherBridgePieces.Crossing.createValidComponent(p_78738_1_, p_78738_2_, p_78738_3_, p_78738_4_, p_78738_5_, p_78738_6_, p_78738_7_);
			}
			else if (var8 == typeof(StructureNetherBridgePieces.Stairs))
			{
				var9 = StructureNetherBridgePieces.Stairs.createValidComponent(p_78738_1_, p_78738_2_, p_78738_3_, p_78738_4_, p_78738_5_, p_78738_6_, p_78738_7_);
			}
			else if (var8 == typeof(StructureNetherBridgePieces.Throne))
			{
				var9 = StructureNetherBridgePieces.Throne.createValidComponent(p_78738_1_, p_78738_2_, p_78738_3_, p_78738_4_, p_78738_5_, p_78738_6_, p_78738_7_);
			}
			else if (var8 == typeof(StructureNetherBridgePieces.Entrance))
			{
				var9 = StructureNetherBridgePieces.Entrance.createValidComponent(p_78738_1_, p_78738_2_, p_78738_3_, p_78738_4_, p_78738_5_, p_78738_6_, p_78738_7_);
			}
			else if (var8 == typeof(StructureNetherBridgePieces.Corridor5))
			{
				var9 = StructureNetherBridgePieces.Corridor5.createValidComponent(p_78738_1_, p_78738_2_, p_78738_3_, p_78738_4_, p_78738_5_, p_78738_6_, p_78738_7_);
			}
			else if (var8 == typeof(StructureNetherBridgePieces.Corridor2))
			{
				var9 = StructureNetherBridgePieces.Corridor2.createValidComponent(p_78738_1_, p_78738_2_, p_78738_3_, p_78738_4_, p_78738_5_, p_78738_6_, p_78738_7_);
			}
			else if (var8 == typeof(StructureNetherBridgePieces.Corridor))
			{
				var9 = StructureNetherBridgePieces.Corridor.createValidComponent(p_78738_1_, p_78738_2_, p_78738_3_, p_78738_4_, p_78738_5_, p_78738_6_, p_78738_7_);
			}
			else if (var8 == typeof(StructureNetherBridgePieces.Corridor3))
			{
				var9 = StructureNetherBridgePieces.Corridor3.createValidComponent(p_78738_1_, p_78738_2_, p_78738_3_, p_78738_4_, p_78738_5_, p_78738_6_, p_78738_7_);
			}
			else if (var8 == typeof(StructureNetherBridgePieces.Corridor4))
			{
				var9 = StructureNetherBridgePieces.Corridor4.createValidComponent(p_78738_1_, p_78738_2_, p_78738_3_, p_78738_4_, p_78738_5_, p_78738_6_, p_78738_7_);
			}
			else if (var8 == typeof(StructureNetherBridgePieces.Crossing2))
			{
				var9 = StructureNetherBridgePieces.Crossing2.createValidComponent(p_78738_1_, p_78738_2_, p_78738_3_, p_78738_4_, p_78738_5_, p_78738_6_, p_78738_7_);
			}
			else if (var8 == typeof(StructureNetherBridgePieces.NetherStalkRoom))
			{
				var9 = StructureNetherBridgePieces.NetherStalkRoom.createValidComponent(p_78738_1_, p_78738_2_, p_78738_3_, p_78738_4_, p_78738_5_, p_78738_6_, p_78738_7_);
			}

			return (StructureNetherBridgePieces.Piece)var9;
		}

		public class Corridor : StructureNetherBridgePieces.Piece
		{
			private bool field_111021_b;
			private const string __OBFID = "CL_00000461";

			public Corridor()
			{
			}

			public Corridor(int p_i2049_1_, Random p_i2049_2_, StructureBoundingBox p_i2049_3_, int p_i2049_4_) : base(p_i2049_1_)
			{
				this.coordBaseMode = p_i2049_4_;
				this.boundingBox = p_i2049_3_;
				this.field_111021_b = p_i2049_2_.Next(3) == 0;
			}

			protected internal virtual void func_143011_b(NBTTagCompound p_143011_1_)
			{
				base.func_143011_b(p_143011_1_);
				this.field_111021_b = p_143011_1_.getBoolean("Chest");
			}

			protected internal virtual void func_143012_a(NBTTagCompound p_143012_1_)
			{
				base.func_143012_a(p_143012_1_);
				p_143012_1_.setBoolean("Chest", this.field_111021_b);
			}

			public virtual void buildComponent(StructureComponent p_74861_1_, IList p_74861_2_, Random p_74861_3_)
			{
				this.getNextComponentX((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 0, 1, true);
			}

			public static StructureNetherBridgePieces.Corridor createValidComponent(IList p_74978_0_, Random p_74978_1_, int p_74978_2_, int p_74978_3_, int p_74978_4_, int p_74978_5_, int p_74978_6_)
			{
				StructureBoundingBox var7 = StructureBoundingBox.getComponentToAddBoundingBox(p_74978_2_, p_74978_3_, p_74978_4_, -1, 0, 0, 5, 7, 5, p_74978_5_);
				return isAboveGround(var7) && StructureComponent.findIntersecting(p_74978_0_, var7) == null ? new StructureNetherBridgePieces.Corridor(p_74978_6_, p_74978_1_, var7, p_74978_5_) : null;
			}

			public virtual bool addComponentParts(World p_74875_1_, Random p_74875_2_, StructureBoundingBox p_74875_3_)
			{
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 0, 0, 4, 1, 4, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 4, 5, 4, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 2, 0, 4, 5, 4, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 3, 1, 4, 4, 1, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 3, 3, 4, 4, 3, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 0, 5, 0, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 4, 3, 5, 4, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 3, 4, 1, 4, 4, Blocks.nether_brick_fence, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 3, 3, 4, 3, 4, 4, Blocks.nether_brick_fence, Blocks.nether_brick, false);
				int var4;
				int var5;

				if (this.field_111021_b)
				{
					var4 = this.getYWithOffset(2);
					var5 = this.getXWithOffset(3, 3);
					int var6 = this.getZWithOffset(3, 3);

					if (p_74875_3_.isVecInside(var5, var4, var6))
					{
						this.field_111021_b = false;
						this.generateStructureChestContents(p_74875_1_, p_74875_3_, p_74875_2_, 3, 2, 3, field_111019_a, 2 + p_74875_2_.Next(4));
					}
				}

				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 6, 0, 4, 6, 4, Blocks.nether_brick, Blocks.nether_brick, false);

				for (var4 = 0; var4 <= 4; ++var4)
				{
					for (var5 = 0; var5 <= 4; ++var5)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var4, -1, var5, p_74875_3_);
					}
				}

				return true;
			}
		}

		public class Corridor2 : StructureNetherBridgePieces.Piece
		{
			private bool field_111020_b;
			private const string __OBFID = "CL_00000463";

			public Corridor2()
			{
			}

			public Corridor2(int p_i2051_1_, Random p_i2051_2_, StructureBoundingBox p_i2051_3_, int p_i2051_4_) : base(p_i2051_1_)
			{
				this.coordBaseMode = p_i2051_4_;
				this.boundingBox = p_i2051_3_;
				this.field_111020_b = p_i2051_2_.Next(3) == 0;
			}

			protected internal virtual void func_143011_b(NBTTagCompound p_143011_1_)
			{
				base.func_143011_b(p_143011_1_);
				this.field_111020_b = p_143011_1_.getBoolean("Chest");
			}

			protected internal virtual void func_143012_a(NBTTagCompound p_143012_1_)
			{
				base.func_143012_a(p_143012_1_);
				p_143012_1_.setBoolean("Chest", this.field_111020_b);
			}

			public virtual void buildComponent(StructureComponent p_74861_1_, IList p_74861_2_, Random p_74861_3_)
			{
				this.getNextComponentZ((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 0, 1, true);
			}

			public static StructureNetherBridgePieces.Corridor2 createValidComponent(IList p_74980_0_, Random p_74980_1_, int p_74980_2_, int p_74980_3_, int p_74980_4_, int p_74980_5_, int p_74980_6_)
			{
				StructureBoundingBox var7 = StructureBoundingBox.getComponentToAddBoundingBox(p_74980_2_, p_74980_3_, p_74980_4_, -1, 0, 0, 5, 7, 5, p_74980_5_);
				return isAboveGround(var7) && StructureComponent.findIntersecting(p_74980_0_, var7) == null ? new StructureNetherBridgePieces.Corridor2(p_74980_6_, p_74980_1_, var7, p_74980_5_) : null;
			}

			public virtual bool addComponentParts(World p_74875_1_, Random p_74875_2_, StructureBoundingBox p_74875_3_)
			{
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 0, 0, 4, 1, 4, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 4, 5, 4, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 0, 5, 4, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 3, 1, 0, 4, 1, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 3, 3, 0, 4, 3, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 2, 0, 4, 5, 0, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 2, 4, 4, 5, 4, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 3, 4, 1, 4, 4, Blocks.nether_brick_fence, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 3, 3, 4, 3, 4, 4, Blocks.nether_brick_fence, Blocks.nether_brick, false);
				int var4;
				int var5;

				if (this.field_111020_b)
				{
					var4 = this.getYWithOffset(2);
					var5 = this.getXWithOffset(1, 3);
					int var6 = this.getZWithOffset(1, 3);

					if (p_74875_3_.isVecInside(var5, var4, var6))
					{
						this.field_111020_b = false;
						this.generateStructureChestContents(p_74875_1_, p_74875_3_, p_74875_2_, 1, 2, 3, field_111019_a, 2 + p_74875_2_.Next(4));
					}
				}

				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 6, 0, 4, 6, 4, Blocks.nether_brick, Blocks.nether_brick, false);

				for (var4 = 0; var4 <= 4; ++var4)
				{
					for (var5 = 0; var5 <= 4; ++var5)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var4, -1, var5, p_74875_3_);
					}
				}

				return true;
			}
		}

		public class Corridor3 : StructureNetherBridgePieces.Piece
		{
			private const string __OBFID = "CL_00000457";

			public Corridor3()
			{
			}

			public Corridor3(int p_i2045_1_, Random p_i2045_2_, StructureBoundingBox p_i2045_3_, int p_i2045_4_) : base(p_i2045_1_)
			{
				this.coordBaseMode = p_i2045_4_;
				this.boundingBox = p_i2045_3_;
			}

			public virtual void buildComponent(StructureComponent p_74861_1_, IList p_74861_2_, Random p_74861_3_)
			{
				this.getNextComponentNormal((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 1, 0, true);
			}

			public static StructureNetherBridgePieces.Corridor3 createValidComponent(IList p_74982_0_, Random p_74982_1_, int p_74982_2_, int p_74982_3_, int p_74982_4_, int p_74982_5_, int p_74982_6_)
			{
				StructureBoundingBox var7 = StructureBoundingBox.getComponentToAddBoundingBox(p_74982_2_, p_74982_3_, p_74982_4_, -1, -7, 0, 5, 14, 10, p_74982_5_);
				return isAboveGround(var7) && StructureComponent.findIntersecting(p_74982_0_, var7) == null ? new StructureNetherBridgePieces.Corridor3(p_74982_6_, p_74982_1_, var7, p_74982_5_) : null;
			}

			public virtual bool addComponentParts(World p_74875_1_, Random p_74875_2_, StructureBoundingBox p_74875_3_)
			{
				int var4 = this.func_151555_a(Blocks.nether_brick_stairs, 2);

				for (int var5 = 0; var5 <= 9; ++var5)
				{
					int var6 = Math.Max(1, 7 - var5);
					int var7 = Math.Min(Math.Max(var6 + 5, 14 - var5), 13);
					int var8 = var5;
					this.func_151549_a(p_74875_1_, p_74875_3_, 0, 0, var5, 4, var6, var5, Blocks.nether_brick, Blocks.nether_brick, false);
					this.func_151549_a(p_74875_1_, p_74875_3_, 1, var6 + 1, var5, 3, var7 - 1, var5, Blocks.air, Blocks.air, false);

					if (var5 <= 6)
					{
						this.func_151550_a(p_74875_1_, Blocks.nether_brick_stairs, var4, 1, var6 + 1, var5, p_74875_3_);
						this.func_151550_a(p_74875_1_, Blocks.nether_brick_stairs, var4, 2, var6 + 1, var5, p_74875_3_);
						this.func_151550_a(p_74875_1_, Blocks.nether_brick_stairs, var4, 3, var6 + 1, var5, p_74875_3_);
					}

					this.func_151549_a(p_74875_1_, p_74875_3_, 0, var7, var5, 4, var7, var5, Blocks.nether_brick, Blocks.nether_brick, false);
					this.func_151549_a(p_74875_1_, p_74875_3_, 0, var6 + 1, var5, 0, var7 - 1, var5, Blocks.nether_brick, Blocks.nether_brick, false);
					this.func_151549_a(p_74875_1_, p_74875_3_, 4, var6 + 1, var5, 4, var7 - 1, var5, Blocks.nether_brick, Blocks.nether_brick, false);

					if ((var5 & 1) == 0)
					{
						this.func_151549_a(p_74875_1_, p_74875_3_, 0, var6 + 2, var5, 0, var6 + 3, var5, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
						this.func_151549_a(p_74875_1_, p_74875_3_, 4, var6 + 2, var5, 4, var6 + 3, var5, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
					}

					for (int var9 = 0; var9 <= 4; ++var9)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var9, -1, var8, p_74875_3_);
					}
				}

				return true;
			}
		}

		public class Corridor4 : StructureNetherBridgePieces.Piece
		{
			private const string __OBFID = "CL_00000458";

			public Corridor4()
			{
			}

			public Corridor4(int p_i2046_1_, Random p_i2046_2_, StructureBoundingBox p_i2046_3_, int p_i2046_4_) : base(p_i2046_1_)
			{
				this.coordBaseMode = p_i2046_4_;
				this.boundingBox = p_i2046_3_;
			}

			public virtual void buildComponent(StructureComponent p_74861_1_, IList p_74861_2_, Random p_74861_3_)
			{
				sbyte var4 = 1;

				if (this.coordBaseMode == 1 || this.coordBaseMode == 2)
				{
					var4 = 5;
				}

				this.getNextComponentX((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 0, var4, p_74861_3_.Next(8) > 0);
				this.getNextComponentZ((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 0, var4, p_74861_3_.Next(8) > 0);
			}

			public static StructureNetherBridgePieces.Corridor4 createValidComponent(IList p_74985_0_, Random p_74985_1_, int p_74985_2_, int p_74985_3_, int p_74985_4_, int p_74985_5_, int p_74985_6_)
			{
				StructureBoundingBox var7 = StructureBoundingBox.getComponentToAddBoundingBox(p_74985_2_, p_74985_3_, p_74985_4_, -3, 0, 0, 9, 7, 9, p_74985_5_);
				return isAboveGround(var7) && StructureComponent.findIntersecting(p_74985_0_, var7) == null ? new StructureNetherBridgePieces.Corridor4(p_74985_6_, p_74985_1_, var7, p_74985_5_) : null;
			}

			public virtual bool addComponentParts(World p_74875_1_, Random p_74875_2_, StructureBoundingBox p_74875_3_)
			{
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 0, 0, 8, 1, 8, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 8, 5, 8, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 6, 0, 8, 6, 5, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 2, 5, 0, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 6, 2, 0, 8, 5, 0, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 3, 0, 1, 4, 0, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 7, 3, 0, 7, 4, 0, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 4, 8, 2, 8, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 1, 4, 2, 2, 4, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 6, 1, 4, 7, 2, 4, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 3, 8, 8, 3, 8, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 3, 6, 0, 3, 7, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 8, 3, 6, 8, 3, 7, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 3, 4, 0, 5, 5, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 8, 3, 4, 8, 5, 5, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 3, 5, 2, 5, 5, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 6, 3, 5, 7, 5, 5, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 4, 5, 1, 5, 5, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 7, 4, 5, 7, 5, 5, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);

				for (int var4 = 0; var4 <= 5; ++var4)
				{
					for (int var5 = 0; var5 <= 8; ++var5)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var5, -1, var4, p_74875_3_);
					}
				}

				return true;
			}
		}

		public class Corridor5 : StructureNetherBridgePieces.Piece
		{
			private const string __OBFID = "CL_00000462";

			public Corridor5()
			{
			}

			public Corridor5(int p_i2050_1_, Random p_i2050_2_, StructureBoundingBox p_i2050_3_, int p_i2050_4_) : base(p_i2050_1_)
			{
				this.coordBaseMode = p_i2050_4_;
				this.boundingBox = p_i2050_3_;
			}

			public virtual void buildComponent(StructureComponent p_74861_1_, IList p_74861_2_, Random p_74861_3_)
			{
				this.getNextComponentNormal((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 1, 0, true);
			}

			public static StructureNetherBridgePieces.Corridor5 createValidComponent(IList p_74981_0_, Random p_74981_1_, int p_74981_2_, int p_74981_3_, int p_74981_4_, int p_74981_5_, int p_74981_6_)
			{
				StructureBoundingBox var7 = StructureBoundingBox.getComponentToAddBoundingBox(p_74981_2_, p_74981_3_, p_74981_4_, -1, 0, 0, 5, 7, 5, p_74981_5_);
				return isAboveGround(var7) && StructureComponent.findIntersecting(p_74981_0_, var7) == null ? new StructureNetherBridgePieces.Corridor5(p_74981_6_, p_74981_1_, var7, p_74981_5_) : null;
			}

			public virtual bool addComponentParts(World p_74875_1_, Random p_74875_2_, StructureBoundingBox p_74875_3_)
			{
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 0, 0, 4, 1, 4, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 4, 5, 4, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 0, 5, 4, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 2, 0, 4, 5, 4, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 3, 1, 0, 4, 1, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 3, 3, 0, 4, 3, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 3, 1, 4, 4, 1, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 3, 3, 4, 4, 3, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 6, 0, 4, 6, 4, Blocks.nether_brick, Blocks.nether_brick, false);

				for (int var4 = 0; var4 <= 4; ++var4)
				{
					for (int var5 = 0; var5 <= 4; ++var5)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var4, -1, var5, p_74875_3_);
					}
				}

				return true;
			}
		}

		public class Crossing : StructureNetherBridgePieces.Piece
		{
			private const string __OBFID = "CL_00000468";

			public Crossing()
			{
			}

			public Crossing(int p_i2057_1_, Random p_i2057_2_, StructureBoundingBox p_i2057_3_, int p_i2057_4_) : base(p_i2057_1_)
			{
				this.coordBaseMode = p_i2057_4_;
				this.boundingBox = p_i2057_3_;
			}

			public virtual void buildComponent(StructureComponent p_74861_1_, IList p_74861_2_, Random p_74861_3_)
			{
				this.getNextComponentNormal((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 2, 0, false);
				this.getNextComponentX((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 0, 2, false);
				this.getNextComponentZ((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 0, 2, false);
			}

			public static StructureNetherBridgePieces.Crossing createValidComponent(IList p_74974_0_, Random p_74974_1_, int p_74974_2_, int p_74974_3_, int p_74974_4_, int p_74974_5_, int p_74974_6_)
			{
				StructureBoundingBox var7 = StructureBoundingBox.getComponentToAddBoundingBox(p_74974_2_, p_74974_3_, p_74974_4_, -2, 0, 0, 7, 9, 7, p_74974_5_);
				return isAboveGround(var7) && StructureComponent.findIntersecting(p_74974_0_, var7) == null ? new StructureNetherBridgePieces.Crossing(p_74974_6_, p_74974_1_, var7, p_74974_5_) : null;
			}

			public virtual bool addComponentParts(World p_74875_1_, Random p_74875_2_, StructureBoundingBox p_74875_3_)
			{
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 0, 0, 6, 1, 6, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 6, 7, 6, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 1, 6, 0, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 6, 1, 6, 6, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 5, 2, 0, 6, 6, 0, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 5, 2, 6, 6, 6, 6, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 0, 6, 1, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 5, 0, 6, 6, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 6, 2, 0, 6, 6, 1, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 6, 2, 5, 6, 6, 6, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 6, 0, 4, 6, 0, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 5, 0, 4, 5, 0, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 6, 6, 4, 6, 6, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 5, 6, 4, 5, 6, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 6, 2, 0, 6, 4, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 5, 2, 0, 5, 4, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 6, 6, 2, 6, 6, 4, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 6, 5, 2, 6, 5, 4, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);

				for (int var4 = 0; var4 <= 6; ++var4)
				{
					for (int var5 = 0; var5 <= 6; ++var5)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var4, -1, var5, p_74875_3_);
					}
				}

				return true;
			}
		}

		public class Crossing2 : StructureNetherBridgePieces.Piece
		{
			private const string __OBFID = "CL_00000460";

			public Crossing2()
			{
			}

			public Crossing2(int p_i2048_1_, Random p_i2048_2_, StructureBoundingBox p_i2048_3_, int p_i2048_4_) : base(p_i2048_1_)
			{
				this.coordBaseMode = p_i2048_4_;
				this.boundingBox = p_i2048_3_;
			}

			public virtual void buildComponent(StructureComponent p_74861_1_, IList p_74861_2_, Random p_74861_3_)
			{
				this.getNextComponentNormal((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 1, 0, true);
				this.getNextComponentX((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 0, 1, true);
				this.getNextComponentZ((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 0, 1, true);
			}

			public static StructureNetherBridgePieces.Crossing2 createValidComponent(IList p_74979_0_, Random p_74979_1_, int p_74979_2_, int p_74979_3_, int p_74979_4_, int p_74979_5_, int p_74979_6_)
			{
				StructureBoundingBox var7 = StructureBoundingBox.getComponentToAddBoundingBox(p_74979_2_, p_74979_3_, p_74979_4_, -1, 0, 0, 5, 7, 5, p_74979_5_);
				return isAboveGround(var7) && StructureComponent.findIntersecting(p_74979_0_, var7) == null ? new StructureNetherBridgePieces.Crossing2(p_74979_6_, p_74979_1_, var7, p_74979_5_) : null;
			}

			public virtual bool addComponentParts(World p_74875_1_, Random p_74875_2_, StructureBoundingBox p_74875_3_)
			{
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 0, 0, 4, 1, 4, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 4, 5, 4, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 0, 5, 0, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 2, 0, 4, 5, 0, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 4, 0, 5, 4, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 2, 4, 4, 5, 4, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 6, 0, 4, 6, 4, Blocks.nether_brick, Blocks.nether_brick, false);

				for (int var4 = 0; var4 <= 4; ++var4)
				{
					for (int var5 = 0; var5 <= 4; ++var5)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var4, -1, var5, p_74875_3_);
					}
				}

				return true;
			}
		}

		public class Crossing3 : StructureNetherBridgePieces.Piece
		{
			private const string __OBFID = "CL_00000454";

			public Crossing3()
			{
			}

			public Crossing3(int p_i2041_1_, Random p_i2041_2_, StructureBoundingBox p_i2041_3_, int p_i2041_4_) : base(p_i2041_1_)
			{
				this.coordBaseMode = p_i2041_4_;
				this.boundingBox = p_i2041_3_;
			}

			protected internal Crossing3(Random p_i2042_1_, int p_i2042_2_, int p_i2042_3_) : base(0)
			{
				this.coordBaseMode = p_i2042_1_.Next(4);

				switch (this.coordBaseMode)
				{
					case 0:
					case 2:
						this.boundingBox = new StructureBoundingBox(p_i2042_2_, 64, p_i2042_3_, p_i2042_2_ + 19 - 1, 73, p_i2042_3_ + 19 - 1);
						break;

					default:
						this.boundingBox = new StructureBoundingBox(p_i2042_2_, 64, p_i2042_3_, p_i2042_2_ + 19 - 1, 73, p_i2042_3_ + 19 - 1);
					break;
				}
			}

			public virtual void buildComponent(StructureComponent p_74861_1_, IList p_74861_2_, Random p_74861_3_)
			{
				this.getNextComponentNormal((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 8, 3, false);
				this.getNextComponentX((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 3, 8, false);
				this.getNextComponentZ((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 3, 8, false);
			}

			public static StructureNetherBridgePieces.Crossing3 createValidComponent(IList p_74966_0_, Random p_74966_1_, int p_74966_2_, int p_74966_3_, int p_74966_4_, int p_74966_5_, int p_74966_6_)
			{
				StructureBoundingBox var7 = StructureBoundingBox.getComponentToAddBoundingBox(p_74966_2_, p_74966_3_, p_74966_4_, -8, -3, 0, 19, 10, 19, p_74966_5_);
				return isAboveGround(var7) && StructureComponent.findIntersecting(p_74966_0_, var7) == null ? new StructureNetherBridgePieces.Crossing3(p_74966_6_, p_74966_1_, var7, p_74966_5_) : null;
			}

			public virtual bool addComponentParts(World p_74875_1_, Random p_74875_2_, StructureBoundingBox p_74875_3_)
			{
				this.func_151549_a(p_74875_1_, p_74875_3_, 7, 3, 0, 11, 4, 18, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 3, 7, 18, 4, 11, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 8, 5, 0, 10, 7, 18, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 5, 8, 18, 7, 10, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 7, 5, 0, 7, 5, 7, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 7, 5, 11, 7, 5, 18, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 11, 5, 0, 11, 5, 7, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 11, 5, 11, 11, 5, 18, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 5, 7, 7, 5, 7, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 11, 5, 7, 18, 5, 7, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 5, 11, 7, 5, 11, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 11, 5, 11, 18, 5, 11, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 7, 2, 0, 11, 2, 5, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 7, 2, 13, 11, 2, 18, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 7, 0, 0, 11, 1, 3, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 7, 0, 15, 11, 1, 18, Blocks.nether_brick, Blocks.nether_brick, false);
				int var4;
				int var5;

				for (var4 = 7; var4 <= 11; ++var4)
				{
					for (var5 = 0; var5 <= 2; ++var5)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var4, -1, var5, p_74875_3_);
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var4, -1, 18 - var5, p_74875_3_);
					}
				}

				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 7, 5, 2, 11, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 13, 2, 7, 18, 2, 11, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 0, 7, 3, 1, 11, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 15, 0, 7, 18, 1, 11, Blocks.nether_brick, Blocks.nether_brick, false);

				for (var4 = 0; var4 <= 2; ++var4)
				{
					for (var5 = 7; var5 <= 11; ++var5)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var4, -1, var5, p_74875_3_);
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, 18 - var4, -1, var5, p_74875_3_);
					}
				}

				return true;
			}
		}

		public class End : StructureNetherBridgePieces.Piece
		{
			private int fillSeed;
			private const string __OBFID = "CL_00000455";

			public End()
			{
			}

			public End(int p_i2043_1_, Random p_i2043_2_, StructureBoundingBox p_i2043_3_, int p_i2043_4_) : base(p_i2043_1_)
			{
				this.coordBaseMode = p_i2043_4_;
				this.boundingBox = p_i2043_3_;
				this.fillSeed = p_i2043_2_.Next();
			}

			public static StructureNetherBridgePieces.End func_74971_a(IList p_74971_0_, Random p_74971_1_, int p_74971_2_, int p_74971_3_, int p_74971_4_, int p_74971_5_, int p_74971_6_)
			{
				StructureBoundingBox var7 = StructureBoundingBox.getComponentToAddBoundingBox(p_74971_2_, p_74971_3_, p_74971_4_, -1, -3, 0, 5, 10, 8, p_74971_5_);
				return isAboveGround(var7) && StructureComponent.findIntersecting(p_74971_0_, var7) == null ? new StructureNetherBridgePieces.End(p_74971_6_, p_74971_1_, var7, p_74971_5_) : null;
			}

			protected internal virtual void func_143011_b(NBTTagCompound p_143011_1_)
			{
				base.func_143011_b(p_143011_1_);
				this.fillSeed = p_143011_1_.getInteger("Seed");
			}

			protected internal virtual void func_143012_a(NBTTagCompound p_143012_1_)
			{
				base.func_143012_a(p_143012_1_);
				p_143012_1_.setInteger("Seed", this.fillSeed);
			}

			public virtual bool addComponentParts(World p_74875_1_, Random p_74875_2_, StructureBoundingBox p_74875_3_)
			{
				Random var4 = new Random((long)this.fillSeed);
				int var5;
				int var6;
				int var7;

				for (var5 = 0; var5 <= 4; ++var5)
				{
					for (var6 = 3; var6 <= 4; ++var6)
					{
						var7 = var4.Next(8);
						this.func_151549_a(p_74875_1_, p_74875_3_, var5, var6, 0, var5, var6, var7, Blocks.nether_brick, Blocks.nether_brick, false);
					}
				}

				var5 = var4.Next(8);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 5, 0, 0, 5, var5, Blocks.nether_brick, Blocks.nether_brick, false);
				var5 = var4.Next(8);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 5, 0, 4, 5, var5, Blocks.nether_brick, Blocks.nether_brick, false);

				for (var5 = 0; var5 <= 4; ++var5)
				{
					var6 = var4.Next(5);
					this.func_151549_a(p_74875_1_, p_74875_3_, var5, 2, 0, var5, 2, var6, Blocks.nether_brick, Blocks.nether_brick, false);
				}

				for (var5 = 0; var5 <= 4; ++var5)
				{
					for (var6 = 0; var6 <= 1; ++var6)
					{
						var7 = var4.Next(3);
						this.func_151549_a(p_74875_1_, p_74875_3_, var5, var6, 0, var5, var6, var7, Blocks.nether_brick, Blocks.nether_brick, false);
					}
				}

				return true;
			}
		}

		public class Entrance : StructureNetherBridgePieces.Piece
		{
			private const string __OBFID = "CL_00000459";

			public Entrance()
			{
			}

			public Entrance(int p_i2047_1_, Random p_i2047_2_, StructureBoundingBox p_i2047_3_, int p_i2047_4_) : base(p_i2047_1_)
			{
				this.coordBaseMode = p_i2047_4_;
				this.boundingBox = p_i2047_3_;
			}

			public virtual void buildComponent(StructureComponent p_74861_1_, IList p_74861_2_, Random p_74861_3_)
			{
				this.getNextComponentNormal((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 5, 3, true);
			}

			public static StructureNetherBridgePieces.Entrance createValidComponent(IList p_74984_0_, Random p_74984_1_, int p_74984_2_, int p_74984_3_, int p_74984_4_, int p_74984_5_, int p_74984_6_)
			{
				StructureBoundingBox var7 = StructureBoundingBox.getComponentToAddBoundingBox(p_74984_2_, p_74984_3_, p_74984_4_, -5, -3, 0, 13, 14, 13, p_74984_5_);
				return isAboveGround(var7) && StructureComponent.findIntersecting(p_74984_0_, var7) == null ? new StructureNetherBridgePieces.Entrance(p_74984_6_, p_74984_1_, var7, p_74984_5_) : null;
			}

			public virtual bool addComponentParts(World p_74875_1_, Random p_74875_2_, StructureBoundingBox p_74875_3_)
			{
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 3, 0, 12, 4, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 5, 0, 12, 13, 12, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 5, 0, 1, 12, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 11, 5, 0, 12, 12, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 5, 11, 4, 12, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 8, 5, 11, 10, 12, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 5, 9, 11, 7, 12, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 5, 0, 4, 12, 1, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 8, 5, 0, 10, 12, 1, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 5, 9, 0, 7, 12, 1, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 11, 2, 10, 12, 10, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 5, 8, 0, 7, 8, 0, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				int var4;

				for (var4 = 1; var4 <= 11; var4 += 2)
				{
					this.func_151549_a(p_74875_1_, p_74875_3_, var4, 10, 0, var4, 11, 0, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
					this.func_151549_a(p_74875_1_, p_74875_3_, var4, 10, 12, var4, 11, 12, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
					this.func_151549_a(p_74875_1_, p_74875_3_, 0, 10, var4, 0, 11, var4, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
					this.func_151549_a(p_74875_1_, p_74875_3_, 12, 10, var4, 12, 11, var4, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick, 0, var4, 13, 0, p_74875_3_);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick, 0, var4, 13, 12, p_74875_3_);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick, 0, 0, 13, var4, p_74875_3_);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick, 0, 12, 13, var4, p_74875_3_);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, var4 + 1, 13, 0, p_74875_3_);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, var4 + 1, 13, 12, p_74875_3_);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, 0, 13, var4 + 1, p_74875_3_);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, 12, 13, var4 + 1, p_74875_3_);
				}

				this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, 0, 13, 0, p_74875_3_);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, 0, 13, 12, p_74875_3_);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, 0, 13, 0, p_74875_3_);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, 12, 13, 0, p_74875_3_);

				for (var4 = 3; var4 <= 9; var4 += 2)
				{
					this.func_151549_a(p_74875_1_, p_74875_3_, 1, 7, var4, 1, 8, var4, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
					this.func_151549_a(p_74875_1_, p_74875_3_, 11, 7, var4, 11, 8, var4, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				}

				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 2, 0, 8, 2, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 4, 12, 2, 8, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 0, 0, 8, 1, 3, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 0, 9, 8, 1, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 0, 4, 3, 1, 8, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 9, 0, 4, 12, 1, 8, Blocks.nether_brick, Blocks.nether_brick, false);
				int var5;

				for (var4 = 4; var4 <= 8; ++var4)
				{
					for (var5 = 0; var5 <= 2; ++var5)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var4, -1, var5, p_74875_3_);
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var4, -1, 12 - var5, p_74875_3_);
					}
				}

				for (var4 = 0; var4 <= 2; ++var4)
				{
					for (var5 = 4; var5 <= 8; ++var5)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var4, -1, var5, p_74875_3_);
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, 12 - var4, -1, var5, p_74875_3_);
					}
				}

				this.func_151549_a(p_74875_1_, p_74875_3_, 5, 5, 5, 7, 5, 7, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 6, 1, 6, 6, 4, 6, Blocks.air, Blocks.air, false);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick, 0, 6, 0, 6, p_74875_3_);
				this.func_151550_a(p_74875_1_, Blocks.flowing_lava, 0, 6, 5, 6, p_74875_3_);
				var4 = this.getXWithOffset(6, 6);
				var5 = this.getYWithOffset(5);
				int var6 = this.getZWithOffset(6, 6);

				if (p_74875_3_.isVecInside(var4, var5, var6))
				{
					p_74875_1_.scheduledUpdatesAreImmediate = true;
					Blocks.flowing_lava.updateTick(p_74875_1_, var4, var5, var6, p_74875_2_);
					p_74875_1_.scheduledUpdatesAreImmediate = false;
				}

				return true;
			}
		}

		public class NetherStalkRoom : StructureNetherBridgePieces.Piece
		{
			private const string __OBFID = "CL_00000464";

			public NetherStalkRoom()
			{
			}

			public NetherStalkRoom(int p_i2052_1_, Random p_i2052_2_, StructureBoundingBox p_i2052_3_, int p_i2052_4_) : base(p_i2052_1_)
			{
				this.coordBaseMode = p_i2052_4_;
				this.boundingBox = p_i2052_3_;
			}

			public virtual void buildComponent(StructureComponent p_74861_1_, IList p_74861_2_, Random p_74861_3_)
			{
				this.getNextComponentNormal((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 5, 3, true);
				this.getNextComponentNormal((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 5, 11, true);
			}

			public static StructureNetherBridgePieces.NetherStalkRoom createValidComponent(IList p_74977_0_, Random p_74977_1_, int p_74977_2_, int p_74977_3_, int p_74977_4_, int p_74977_5_, int p_74977_6_)
			{
				StructureBoundingBox var7 = StructureBoundingBox.getComponentToAddBoundingBox(p_74977_2_, p_74977_3_, p_74977_4_, -5, -3, 0, 13, 14, 13, p_74977_5_);
				return isAboveGround(var7) && StructureComponent.findIntersecting(p_74977_0_, var7) == null ? new StructureNetherBridgePieces.NetherStalkRoom(p_74977_6_, p_74977_1_, var7, p_74977_5_) : null;
			}

			public virtual bool addComponentParts(World p_74875_1_, Random p_74875_2_, StructureBoundingBox p_74875_3_)
			{
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 3, 0, 12, 4, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 5, 0, 12, 13, 12, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 5, 0, 1, 12, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 11, 5, 0, 12, 12, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 5, 11, 4, 12, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 8, 5, 11, 10, 12, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 5, 9, 11, 7, 12, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 5, 0, 4, 12, 1, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 8, 5, 0, 10, 12, 1, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 5, 9, 0, 7, 12, 1, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 11, 2, 10, 12, 10, Blocks.nether_brick, Blocks.nether_brick, false);
				int var4;

				for (var4 = 1; var4 <= 11; var4 += 2)
				{
					this.func_151549_a(p_74875_1_, p_74875_3_, var4, 10, 0, var4, 11, 0, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
					this.func_151549_a(p_74875_1_, p_74875_3_, var4, 10, 12, var4, 11, 12, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
					this.func_151549_a(p_74875_1_, p_74875_3_, 0, 10, var4, 0, 11, var4, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
					this.func_151549_a(p_74875_1_, p_74875_3_, 12, 10, var4, 12, 11, var4, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick, 0, var4, 13, 0, p_74875_3_);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick, 0, var4, 13, 12, p_74875_3_);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick, 0, 0, 13, var4, p_74875_3_);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick, 0, 12, 13, var4, p_74875_3_);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, var4 + 1, 13, 0, p_74875_3_);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, var4 + 1, 13, 12, p_74875_3_);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, 0, 13, var4 + 1, p_74875_3_);
					this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, 12, 13, var4 + 1, p_74875_3_);
				}

				this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, 0, 13, 0, p_74875_3_);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, 0, 13, 12, p_74875_3_);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, 0, 13, 0, p_74875_3_);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, 12, 13, 0, p_74875_3_);

				for (var4 = 3; var4 <= 9; var4 += 2)
				{
					this.func_151549_a(p_74875_1_, p_74875_3_, 1, 7, var4, 1, 8, var4, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
					this.func_151549_a(p_74875_1_, p_74875_3_, 11, 7, var4, 11, 8, var4, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				}

				var4 = this.func_151555_a(Blocks.nether_brick_stairs, 3);
				int var5;
				int var6;
				int var7;

				for (var5 = 0; var5 <= 6; ++var5)
				{
					var6 = var5 + 4;

					for (var7 = 5; var7 <= 7; ++var7)
					{
						this.func_151550_a(p_74875_1_, Blocks.nether_brick_stairs, var4, var7, 5 + var5, var6, p_74875_3_);
					}

					if (var6 >= 5 && var6 <= 8)
					{
						this.func_151549_a(p_74875_1_, p_74875_3_, 5, 5, var6, 7, var5 + 4, var6, Blocks.nether_brick, Blocks.nether_brick, false);
					}
					else if (var6 >= 9 && var6 <= 10)
					{
						this.func_151549_a(p_74875_1_, p_74875_3_, 5, 8, var6, 7, var5 + 4, var6, Blocks.nether_brick, Blocks.nether_brick, false);
					}

					if (var5 >= 1)
					{
						this.func_151549_a(p_74875_1_, p_74875_3_, 5, 6 + var5, var6, 7, 9 + var5, var6, Blocks.air, Blocks.air, false);
					}
				}

				for (var5 = 5; var5 <= 7; ++var5)
				{
					this.func_151550_a(p_74875_1_, Blocks.nether_brick_stairs, var4, var5, 12, 11, p_74875_3_);
				}

				this.func_151549_a(p_74875_1_, p_74875_3_, 5, 6, 7, 5, 7, 7, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 7, 6, 7, 7, 7, 7, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 5, 13, 12, 7, 13, 12, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 5, 2, 3, 5, 3, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 5, 9, 3, 5, 10, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 5, 4, 2, 5, 8, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 9, 5, 2, 10, 5, 3, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 9, 5, 9, 10, 5, 10, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 10, 5, 4, 10, 5, 8, Blocks.nether_brick, Blocks.nether_brick, false);
				var5 = this.func_151555_a(Blocks.nether_brick_stairs, 0);
				var6 = this.func_151555_a(Blocks.nether_brick_stairs, 1);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_stairs, var6, 4, 5, 2, p_74875_3_);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_stairs, var6, 4, 5, 3, p_74875_3_);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_stairs, var6, 4, 5, 9, p_74875_3_);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_stairs, var6, 4, 5, 10, p_74875_3_);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_stairs, var5, 8, 5, 2, p_74875_3_);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_stairs, var5, 8, 5, 3, p_74875_3_);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_stairs, var5, 8, 5, 9, p_74875_3_);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_stairs, var5, 8, 5, 10, p_74875_3_);
				this.func_151549_a(p_74875_1_, p_74875_3_, 3, 4, 4, 4, 4, 8, Blocks.soul_sand, Blocks.soul_sand, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 8, 4, 4, 9, 4, 8, Blocks.soul_sand, Blocks.soul_sand, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 3, 5, 4, 4, 5, 8, Blocks.nether_wart, Blocks.nether_wart, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 8, 5, 4, 9, 5, 8, Blocks.nether_wart, Blocks.nether_wart, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 2, 0, 8, 2, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 4, 12, 2, 8, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 0, 0, 8, 1, 3, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 0, 9, 8, 1, 12, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 0, 4, 3, 1, 8, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 9, 0, 4, 12, 1, 8, Blocks.nether_brick, Blocks.nether_brick, false);
				int var8;

				for (var7 = 4; var7 <= 8; ++var7)
				{
					for (var8 = 0; var8 <= 2; ++var8)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var7, -1, var8, p_74875_3_);
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var7, -1, 12 - var8, p_74875_3_);
					}
				}

				for (var7 = 0; var7 <= 2; ++var7)
				{
					for (var8 = 4; var8 <= 8; ++var8)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var7, -1, var8, p_74875_3_);
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, 12 - var7, -1, var8, p_74875_3_);
					}
				}

				return true;
			}
		}

		internal abstract class Piece : StructureComponent
		{
			protected internal static readonly WeightedRandomChestContent[] field_111019_a = new WeightedRandomChestContent[] {new WeightedRandomChestContent(Items.diamond, 0, 1, 3, 5), new WeightedRandomChestContent(Items.iron_ingot, 0, 1, 5, 5), new WeightedRandomChestContent(Items.gold_ingot, 0, 1, 3, 15), new WeightedRandomChestContent(Items.golden_sword, 0, 1, 1, 5), new WeightedRandomChestContent(Items.golden_chestplate, 0, 1, 1, 5), new WeightedRandomChestContent(Items.flint_and_steel, 0, 1, 1, 5), new WeightedRandomChestContent(Items.nether_wart, 0, 3, 7, 5), new WeightedRandomChestContent(Items.saddle, 0, 1, 1, 10), new WeightedRandomChestContent(Items.golden_horse_armor, 0, 1, 1, 8), new WeightedRandomChestContent(Items.iron_horse_armor, 0, 1, 1, 5), new WeightedRandomChestContent(Items.diamond_horse_armor, 0, 1, 1, 3)};
			private const string __OBFID = "CL_00000466";

			public Piece()
			{
			}

			protected internal Piece(int p_i2054_1_) : base(p_i2054_1_)
			{
			}

			protected internal override void func_143011_b(NBTTagCompound p_143011_1_)
			{
			}

			protected internal override void func_143012_a(NBTTagCompound p_143012_1_)
			{
			}

			private int getTotalWeight(IList p_74960_1_)
			{
				bool var2 = false;
				int var3 = 0;
				StructureNetherBridgePieces.PieceWeight var5;

				for (IEnumerator var4 = p_74960_1_.GetEnumerator(); var4.MoveNext(); var3 += var5.field_78826_b)
				{
					var5 = (StructureNetherBridgePieces.PieceWeight)var4.Current;

					if (var5.field_78824_d > 0 && var5.field_78827_c < var5.field_78824_d)
					{
						var2 = true;
					}
				}

				return var2 ? var3 : -1;
			}

			private StructureNetherBridgePieces.Piece getNextComponent(StructureNetherBridgePieces.Start p_74959_1_, IList p_74959_2_, IList p_74959_3_, Random p_74959_4_, int p_74959_5_, int p_74959_6_, int p_74959_7_, int p_74959_8_, int p_74959_9_)
			{
				int var10 = this.getTotalWeight(p_74959_2_);
				bool var11 = var10 > 0 && p_74959_9_ <= 30;
				int var12 = 0;

				while (var12 < 5 && var11)
				{
					++var12;
					int var13 = p_74959_4_.Next(var10);
					IEnumerator var14 = p_74959_2_.GetEnumerator();

					while (var14.MoveNext())
					{
						StructureNetherBridgePieces.PieceWeight var15 = (StructureNetherBridgePieces.PieceWeight)var14.Current;
						var13 -= var15.field_78826_b;

						if (var13 < 0)
						{
							if (!var15.func_78822_a(p_74959_9_) || var15 == p_74959_1_.theNetherBridgePieceWeight && !var15.field_78825_e)
							{
								break;
							}

							StructureNetherBridgePieces.Piece var16 = StructureNetherBridgePieces.createNextComponentRandom(var15, p_74959_3_, p_74959_4_, p_74959_5_, p_74959_6_, p_74959_7_, p_74959_8_, p_74959_9_);

							if (var16 != null)
							{
								++var15.field_78827_c;
								p_74959_1_.theNetherBridgePieceWeight = var15;

								if (!var15.func_78823_a())
								{
									p_74959_2_.Remove(var15);
								}

								return var16;
							}
						}
					}
				}

				return StructureNetherBridgePieces.End.func_74971_a(p_74959_3_, p_74959_4_, p_74959_5_, p_74959_6_, p_74959_7_, p_74959_8_, p_74959_9_);
			}

			private StructureComponent getNextComponent(StructureNetherBridgePieces.Start p_74962_1_, IList p_74962_2_, Random p_74962_3_, int p_74962_4_, int p_74962_5_, int p_74962_6_, int p_74962_7_, int p_74962_8_, bool p_74962_9_)
			{
				if (Math.Abs(p_74962_4_ - p_74962_1_.BoundingBox.minX) <= 112 && Math.Abs(p_74962_6_ - p_74962_1_.BoundingBox.minZ) <= 112)
				{
					IList var10 = p_74962_1_.primaryWeights;

					if (p_74962_9_)
					{
						var10 = p_74962_1_.secondaryWeights;
					}

					StructureNetherBridgePieces.Piece var11 = this.getNextComponent(p_74962_1_, var10, p_74962_2_, p_74962_3_, p_74962_4_, p_74962_5_, p_74962_6_, p_74962_7_, p_74962_8_ + 1);

					if (var11 != null)
					{
						p_74962_2_.Add(var11);
						p_74962_1_.field_74967_d.Add(var11);
					}

					return var11;
				}
				else
				{
					return StructureNetherBridgePieces.End.func_74971_a(p_74962_2_, p_74962_3_, p_74962_4_, p_74962_5_, p_74962_6_, p_74962_7_, p_74962_8_);
				}
			}

			protected internal virtual StructureComponent getNextComponentNormal(StructureNetherBridgePieces.Start p_74963_1_, IList p_74963_2_, Random p_74963_3_, int p_74963_4_, int p_74963_5_, bool p_74963_6_)
			{
				switch (this.coordBaseMode)
				{
					case 0:
						return this.getNextComponent(p_74963_1_, p_74963_2_, p_74963_3_, this.boundingBox.minX + p_74963_4_, this.boundingBox.minY + p_74963_5_, this.boundingBox.maxZ + 1, this.coordBaseMode, this.ComponentType, p_74963_6_);

					case 1:
						return this.getNextComponent(p_74963_1_, p_74963_2_, p_74963_3_, this.boundingBox.minX - 1, this.boundingBox.minY + p_74963_5_, this.boundingBox.minZ + p_74963_4_, this.coordBaseMode, this.ComponentType, p_74963_6_);

					case 2:
						return this.getNextComponent(p_74963_1_, p_74963_2_, p_74963_3_, this.boundingBox.minX + p_74963_4_, this.boundingBox.minY + p_74963_5_, this.boundingBox.minZ - 1, this.coordBaseMode, this.ComponentType, p_74963_6_);

					case 3:
						return this.getNextComponent(p_74963_1_, p_74963_2_, p_74963_3_, this.boundingBox.maxX + 1, this.boundingBox.minY + p_74963_5_, this.boundingBox.minZ + p_74963_4_, this.coordBaseMode, this.ComponentType, p_74963_6_);

					default:
						return null;
				}
			}

			protected internal virtual StructureComponent getNextComponentX(StructureNetherBridgePieces.Start p_74961_1_, IList p_74961_2_, Random p_74961_3_, int p_74961_4_, int p_74961_5_, bool p_74961_6_)
			{
				switch (this.coordBaseMode)
				{
					case 0:
						return this.getNextComponent(p_74961_1_, p_74961_2_, p_74961_3_, this.boundingBox.minX - 1, this.boundingBox.minY + p_74961_4_, this.boundingBox.minZ + p_74961_5_, 1, this.ComponentType, p_74961_6_);

					case 1:
						return this.getNextComponent(p_74961_1_, p_74961_2_, p_74961_3_, this.boundingBox.minX + p_74961_5_, this.boundingBox.minY + p_74961_4_, this.boundingBox.minZ - 1, 2, this.ComponentType, p_74961_6_);

					case 2:
						return this.getNextComponent(p_74961_1_, p_74961_2_, p_74961_3_, this.boundingBox.minX - 1, this.boundingBox.minY + p_74961_4_, this.boundingBox.minZ + p_74961_5_, 1, this.ComponentType, p_74961_6_);

					case 3:
						return this.getNextComponent(p_74961_1_, p_74961_2_, p_74961_3_, this.boundingBox.minX + p_74961_5_, this.boundingBox.minY + p_74961_4_, this.boundingBox.minZ - 1, 2, this.ComponentType, p_74961_6_);

					default:
						return null;
				}
			}

			protected internal virtual StructureComponent getNextComponentZ(StructureNetherBridgePieces.Start p_74965_1_, IList p_74965_2_, Random p_74965_3_, int p_74965_4_, int p_74965_5_, bool p_74965_6_)
			{
				switch (this.coordBaseMode)
				{
					case 0:
						return this.getNextComponent(p_74965_1_, p_74965_2_, p_74965_3_, this.boundingBox.maxX + 1, this.boundingBox.minY + p_74965_4_, this.boundingBox.minZ + p_74965_5_, 3, this.ComponentType, p_74965_6_);

					case 1:
						return this.getNextComponent(p_74965_1_, p_74965_2_, p_74965_3_, this.boundingBox.minX + p_74965_5_, this.boundingBox.minY + p_74965_4_, this.boundingBox.maxZ + 1, 0, this.ComponentType, p_74965_6_);

					case 2:
						return this.getNextComponent(p_74965_1_, p_74965_2_, p_74965_3_, this.boundingBox.maxX + 1, this.boundingBox.minY + p_74965_4_, this.boundingBox.minZ + p_74965_5_, 3, this.ComponentType, p_74965_6_);

					case 3:
						return this.getNextComponent(p_74965_1_, p_74965_2_, p_74965_3_, this.boundingBox.minX + p_74965_5_, this.boundingBox.minY + p_74965_4_, this.boundingBox.maxZ + 1, 0, this.ComponentType, p_74965_6_);

					default:
						return null;
				}
			}

			protected internal static bool isAboveGround(StructureBoundingBox p_74964_0_)
			{
				return p_74964_0_ != null && p_74964_0_.minY > 10;
			}
		}

		internal class PieceWeight
		{
			public Type weightClass;
			public readonly int field_78826_b;
			public int field_78827_c;
			public int field_78824_d;
			public bool field_78825_e;
			private const string __OBFID = "CL_00000467";

			public PieceWeight(Type p_i2055_1_, int p_i2055_2_, int p_i2055_3_, bool p_i2055_4_)
			{
				this.weightClass = p_i2055_1_;
				this.field_78826_b = p_i2055_2_;
				this.field_78824_d = p_i2055_3_;
				this.field_78825_e = p_i2055_4_;
			}

			public PieceWeight(Type p_i2056_1_, int p_i2056_2_, int p_i2056_3_) : this(p_i2056_1_, p_i2056_2_, p_i2056_3_, false)
			{
			}

			public virtual bool func_78822_a(int p_78822_1_)
			{
				return this.field_78824_d == 0 || this.field_78827_c < this.field_78824_d;
			}

			public virtual bool func_78823_a()
			{
				return this.field_78824_d == 0 || this.field_78827_c < this.field_78824_d;
			}
		}

		public class Stairs : StructureNetherBridgePieces.Piece
		{
			private const string __OBFID = "CL_00000469";

			public Stairs()
			{
			}

			public Stairs(int p_i2058_1_, Random p_i2058_2_, StructureBoundingBox p_i2058_3_, int p_i2058_4_) : base(p_i2058_1_)
			{
				this.coordBaseMode = p_i2058_4_;
				this.boundingBox = p_i2058_3_;
			}

			public virtual void buildComponent(StructureComponent p_74861_1_, IList p_74861_2_, Random p_74861_3_)
			{
				this.getNextComponentZ((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 6, 2, false);
			}

			public static StructureNetherBridgePieces.Stairs createValidComponent(IList p_74973_0_, Random p_74973_1_, int p_74973_2_, int p_74973_3_, int p_74973_4_, int p_74973_5_, int p_74973_6_)
			{
				StructureBoundingBox var7 = StructureBoundingBox.getComponentToAddBoundingBox(p_74973_2_, p_74973_3_, p_74973_4_, -2, 0, 0, 7, 11, 7, p_74973_5_);
				return isAboveGround(var7) && StructureComponent.findIntersecting(p_74973_0_, var7) == null ? new StructureNetherBridgePieces.Stairs(p_74973_6_, p_74973_1_, var7, p_74973_5_) : null;
			}

			public virtual bool addComponentParts(World p_74875_1_, Random p_74875_2_, StructureBoundingBox p_74875_3_)
			{
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 0, 0, 6, 1, 6, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 6, 10, 6, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 1, 8, 0, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 5, 2, 0, 6, 8, 0, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 1, 0, 8, 6, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 6, 2, 1, 6, 8, 6, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 2, 6, 5, 8, 6, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 3, 2, 0, 5, 4, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 6, 3, 2, 6, 5, 2, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 6, 3, 4, 6, 5, 4, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick, 0, 5, 2, 5, p_74875_3_);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 2, 5, 4, 3, 5, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 3, 2, 5, 3, 4, 5, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 2, 5, 2, 5, 5, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 2, 5, 1, 6, 5, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 7, 1, 5, 7, 4, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 6, 8, 2, 6, 8, 4, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 6, 0, 4, 8, 0, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 5, 0, 4, 5, 0, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);

				for (int var4 = 0; var4 <= 6; ++var4)
				{
					for (int var5 = 0; var5 <= 6; ++var5)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var4, -1, var5, p_74875_3_);
					}
				}

				return true;
			}
		}

		public class Start : StructureNetherBridgePieces.Crossing3
		{
			public StructureNetherBridgePieces.PieceWeight theNetherBridgePieceWeight;
			public IList primaryWeights;
			public IList secondaryWeights;
			public ArrayList field_74967_d = new ArrayList();
			private const string __OBFID = "CL_00000470";

			public Start()
			{
			}

			public Start(Random p_i2059_1_, int p_i2059_2_, int p_i2059_3_) : base(p_i2059_1_, p_i2059_2_, p_i2059_3_)
			{
				this.primaryWeights = new ArrayList();
				StructureNetherBridgePieces.PieceWeight[] var4 = StructureNetherBridgePieces.primaryComponents;
				int var5 = var4.Length;
				int var6;
				StructureNetherBridgePieces.PieceWeight var7;

				for (var6 = 0; var6 < var5; ++var6)
				{
					var7 = var4[var6];
					var7.field_78827_c = 0;
					this.primaryWeights.Add(var7);
				}

				this.secondaryWeights = new ArrayList();
				var4 = StructureNetherBridgePieces.secondaryComponents;
				var5 = var4.Length;

				for (var6 = 0; var6 < var5; ++var6)
				{
					var7 = var4[var6];
					var7.field_78827_c = 0;
					this.secondaryWeights.Add(var7);
				}
			}

			protected internal virtual void func_143011_b(NBTTagCompound p_143011_1_)
			{
				base.func_143011_b(p_143011_1_);
			}

			protected internal virtual void func_143012_a(NBTTagCompound p_143012_1_)
			{
				base.func_143012_a(p_143012_1_);
			}
		}

		public class Straight : StructureNetherBridgePieces.Piece
		{
			private const string __OBFID = "CL_00000456";

			public Straight()
			{
			}

			public Straight(int p_i2044_1_, Random p_i2044_2_, StructureBoundingBox p_i2044_3_, int p_i2044_4_) : base(p_i2044_1_)
			{
				this.coordBaseMode = p_i2044_4_;
				this.boundingBox = p_i2044_3_;
			}

			public virtual void buildComponent(StructureComponent p_74861_1_, IList p_74861_2_, Random p_74861_3_)
			{
				this.getNextComponentNormal((StructureNetherBridgePieces.Start)p_74861_1_, p_74861_2_, p_74861_3_, 1, 3, false);
			}

			public static StructureNetherBridgePieces.Straight createValidComponent(IList p_74983_0_, Random p_74983_1_, int p_74983_2_, int p_74983_3_, int p_74983_4_, int p_74983_5_, int p_74983_6_)
			{
				StructureBoundingBox var7 = StructureBoundingBox.getComponentToAddBoundingBox(p_74983_2_, p_74983_3_, p_74983_4_, -1, -3, 0, 5, 10, 19, p_74983_5_);
				return isAboveGround(var7) && StructureComponent.findIntersecting(p_74983_0_, var7) == null ? new StructureNetherBridgePieces.Straight(p_74983_6_, p_74983_1_, var7, p_74983_5_) : null;
			}

			public virtual bool addComponentParts(World p_74875_1_, Random p_74875_2_, StructureBoundingBox p_74875_3_)
			{
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 3, 0, 4, 4, 18, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 5, 0, 3, 7, 18, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 5, 0, 0, 5, 18, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 5, 0, 4, 5, 18, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 4, 2, 5, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 13, 4, 2, 18, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 0, 0, 4, 1, 3, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 0, 15, 4, 1, 18, Blocks.nether_brick, Blocks.nether_brick, false);

				for (int var4 = 0; var4 <= 4; ++var4)
				{
					for (int var5 = 0; var5 <= 2; ++var5)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var4, -1, var5, p_74875_3_);
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var4, -1, 18 - var5, p_74875_3_);
					}
				}

				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 1, 1, 0, 4, 1, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 3, 4, 0, 4, 4, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 3, 14, 0, 4, 14, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 1, 17, 0, 4, 17, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 1, 1, 4, 4, 1, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 3, 4, 4, 4, 4, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 3, 14, 4, 4, 14, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 4, 1, 17, 4, 4, 17, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				return true;
			}
		}

		public class Throne : StructureNetherBridgePieces.Piece
		{
			private bool hasSpawner;
			private const string __OBFID = "CL_00000465";

			public Throne()
			{
			}

			public Throne(int p_i2053_1_, Random p_i2053_2_, StructureBoundingBox p_i2053_3_, int p_i2053_4_) : base(p_i2053_1_)
			{
				this.coordBaseMode = p_i2053_4_;
				this.boundingBox = p_i2053_3_;
			}

			protected internal virtual void func_143011_b(NBTTagCompound p_143011_1_)
			{
				base.func_143011_b(p_143011_1_);
				this.hasSpawner = p_143011_1_.getBoolean("Mob");
			}

			protected internal virtual void func_143012_a(NBTTagCompound p_143012_1_)
			{
				base.func_143012_a(p_143012_1_);
				p_143012_1_.setBoolean("Mob", this.hasSpawner);
			}

			public static StructureNetherBridgePieces.Throne createValidComponent(IList p_74975_0_, Random p_74975_1_, int p_74975_2_, int p_74975_3_, int p_74975_4_, int p_74975_5_, int p_74975_6_)
			{
				StructureBoundingBox var7 = StructureBoundingBox.getComponentToAddBoundingBox(p_74975_2_, p_74975_3_, p_74975_4_, -2, 0, 0, 7, 8, 9, p_74975_5_);
				return isAboveGround(var7) && StructureComponent.findIntersecting(p_74975_0_, var7) == null ? new StructureNetherBridgePieces.Throne(p_74975_6_, p_74975_1_, var7, p_74975_5_) : null;
			}

			public virtual bool addComponentParts(World p_74875_1_, Random p_74875_2_, StructureBoundingBox p_74875_3_)
			{
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 2, 0, 6, 7, 7, Blocks.air, Blocks.air, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 0, 0, 5, 1, 7, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 2, 1, 5, 2, 7, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 3, 2, 5, 3, 7, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 4, 3, 5, 4, 7, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 2, 0, 1, 4, 2, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 5, 2, 0, 5, 4, 2, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 5, 2, 1, 5, 3, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 5, 5, 2, 5, 5, 3, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 5, 3, 0, 5, 8, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 6, 5, 3, 6, 5, 8, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 5, 8, 5, 5, 8, Blocks.nether_brick, Blocks.nether_brick, false);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, 1, 6, 3, p_74875_3_);
				this.func_151550_a(p_74875_1_, Blocks.nether_brick_fence, 0, 5, 6, 3, p_74875_3_);
				this.func_151549_a(p_74875_1_, p_74875_3_, 0, 6, 3, 0, 6, 8, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 6, 6, 3, 6, 6, 8, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 1, 6, 8, 5, 7, 8, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				this.func_151549_a(p_74875_1_, p_74875_3_, 2, 8, 8, 4, 8, 8, Blocks.nether_brick_fence, Blocks.nether_brick_fence, false);
				int var4;
				int var5;

				if (!this.hasSpawner)
				{
					var4 = this.getYWithOffset(5);
					var5 = this.getXWithOffset(3, 5);
					int var6 = this.getZWithOffset(3, 5);

					if (p_74875_3_.isVecInside(var5, var4, var6))
					{
						this.hasSpawner = true;
						p_74875_1_.setBlock(var5, var4, var6, Blocks.mob_spawner, 0, 2);
						TileEntityMobSpawner var7 = (TileEntityMobSpawner)p_74875_1_.getTileEntity(var5, var4, var6);

						if (var7 != null)
						{
							var7.func_145881_a().MobID = "Blaze";
						}
					}
				}

				for (var4 = 0; var4 <= 6; ++var4)
				{
					for (var5 = 0; var5 <= 6; ++var5)
					{
						this.func_151554_b(p_74875_1_, Blocks.nether_brick, 0, var4, -1, var5, p_74875_3_);
					}
				}

				return true;
			}
		}
	}

}