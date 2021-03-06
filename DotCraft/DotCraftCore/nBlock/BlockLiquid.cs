using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity;
using DotCraftCore.nInit;
using DotCraftCore.nItem;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
using DotCraftUtil;
using System;

namespace DotCraftCore.nBlock
{
	public abstract class BlockLiquid : Block
	{
		protected internal BlockLiquid(Material p_i45413_1_) : base(p_i45413_1_)
		{
			float var2 = 0.0F;
			float var3 = 0.0F;
			this.setBlockBounds(0.0F + var3, 0.0F + var2, 0.0F + var3, 1.0F + var3, 1.0F + var2, 1.0F + var3);
			this.TickRandomly = true;
		}

		public override bool getBlocksMovement(IBlockAccess p_149655_1_, int p_149655_2_, int p_149655_3_, int p_149655_4_)
		{
			return this.BlockMaterial != Material.lava;
		}

		public override int BlockColor
		{
			get
			{
				return 16777215;
			}
		}

///    
///     <summary> * Returns a integer with hex for 0xrrggbb with this color multiplied against the blocks color. Note only called
///     * when first determining what to render. </summary>
///     
		public override int colorMultiplier(IBlockAccess p_149720_1_, int p_149720_2_, int p_149720_3_, int p_149720_4_)
		{
			if (this.BlockMaterial != Material.water)
			{
				return 16777215;
			}
			else
			{
				int var5 = 0;
				int var6 = 0;
				int var7 = 0;

				for (int var8 = -1; var8 <= 1; ++var8)
				{
					for (int var9 = -1; var9 <= 1; ++var9)
					{
						int var10 = p_149720_1_.getBiomeGenForCoords(p_149720_2_ + var9, p_149720_4_ + var8).waterColorMultiplier;
						var5 += (var10 & 16711680) >> 16;
						var6 += (var10 & 65280) >> 8;
						var7 += var10 & 255;
					}
				}

				return (var5 / 9 & 255) << 16 | (var6 / 9 & 255) << 8 | var7 / 9 & 255;
			}
		}

		public static float func_149801_b(int p_149801_0_)
		{
			if (p_149801_0_ >= 8)
			{
				p_149801_0_ = 0;
			}

			return (float)(p_149801_0_ + 1) / 9.0F;
		}

		protected internal override int func_149804_e(World p_149804_1_, int p_149804_2_, int p_149804_3_, int p_149804_4_)
		{
			return p_149804_1_.getBlock(p_149804_2_, p_149804_3_, p_149804_4_).BlockMaterial == this.BlockMaterial ? p_149804_1_.getBlockMetadata(p_149804_2_, p_149804_3_, p_149804_4_) : -1;
		}

		protected internal override int func_149798_e(IBlockAccess p_149798_1_, int p_149798_2_, int p_149798_3_, int p_149798_4_)
		{
			if (p_149798_1_.getBlock(p_149798_2_, p_149798_3_, p_149798_4_).BlockMaterial != this.BlockMaterial)
			{
				return -1;
			}
			else
			{
				int var5 = p_149798_1_.getBlockMetadata(p_149798_2_, p_149798_3_, p_149798_4_);

				if (var5 >= 8)
				{
					var5 = 0;
				}

				return var5;
			}
		}

		public override bool renderAsNormalBlock()
		{
			return false;
		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

///    
///     * Returns whether this block is collideable based on the arguments passed in \n<param name="par1"> block metaData \n@param
///     * par2 whether the player right-clicked while holding a boat </param>
///     
		public override bool canCollideCheck(int p_149678_1_, bool p_149678_2_)
		{
			return p_149678_2_ && p_149678_1_ == 0;
		}

		public override bool isBlockSolid(IBlockAccess p_149747_1_, int p_149747_2_, int p_149747_3_, int p_149747_4_, int p_149747_5_)
		{
			Material var6 = p_149747_1_.getBlock(p_149747_2_, p_149747_3_, p_149747_4_).BlockMaterial;
			return var6 == this.BlockMaterial ? false : (p_149747_5_ == 1 ? true : (var6 == Material.ice ? false : base.isBlockSolid(p_149747_1_, p_149747_2_, p_149747_3_, p_149747_4_, p_149747_5_)));
		}

		public override bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
		{
			Material var6 = p_149646_1_.getBlock(p_149646_2_, p_149646_3_, p_149646_4_).BlockMaterial;
			return var6 == this.BlockMaterial ? false : (p_149646_5_ == 1 ? true : base.shouldSideBeRendered(p_149646_1_, p_149646_2_, p_149646_3_, p_149646_4_, p_149646_5_));
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			return null;
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public override int RenderType
		{
			get
			{
				return 4;
			}
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return null;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public override int quantityDropped(Random p_149745_1_)
		{
			return 0;
		}

		private Vec3 func_149800_f(IBlockAccess p_149800_1_, int p_149800_2_, int p_149800_3_, int p_149800_4_)
		{
			Vec3 var5 = Vec3.createVectorHelper(0.0D, 0.0D, 0.0D);
			int var6 = this.func_149798_e(p_149800_1_, p_149800_2_, p_149800_3_, p_149800_4_);

			for (int var7 = 0; var7 < 4; ++var7)
			{
				int var8 = p_149800_2_;
				int var10 = p_149800_4_;

				if (var7 == 0)
				{
					var8 = p_149800_2_ - 1;
				}

				if (var7 == 1)
				{
					var10 = p_149800_4_ - 1;
				}

				if (var7 == 2)
				{
					++var8;
				}

				if (var7 == 3)
				{
					++var10;
				}

				int var11 = this.func_149798_e(p_149800_1_, var8, p_149800_3_, var10);
				int var12;

				if (var11 < 0)
				{
					if (!p_149800_1_.getBlock(var8, p_149800_3_, var10).BlockMaterial.BlocksMovement)
					{
						var11 = this.func_149798_e(p_149800_1_, var8, p_149800_3_ - 1, var10);

						if (var11 >= 0)
						{
							var12 = var11 - (var6 - 8);
							var5 = var5.addVector((double)((var8 - p_149800_2_) * var12), (double)((p_149800_3_ - p_149800_3_) * var12), (double)((var10 - p_149800_4_) * var12));
						}
					}
				}
				else if (var11 >= 0)
				{
					var12 = var11 - var6;
					var5 = var5.addVector((double)((var8 - p_149800_2_) * var12), (double)((p_149800_3_ - p_149800_3_) * var12), (double)((var10 - p_149800_4_) * var12));
				}
			}

			if (p_149800_1_.getBlockMetadata(p_149800_2_, p_149800_3_, p_149800_4_) >= 8)
			{
				bool var13 = false;

				if (var13 || this.isBlockSolid(p_149800_1_, p_149800_2_, p_149800_3_, p_149800_4_ - 1, 2))
				{
					var13 = true;
				}

				if (var13 || this.isBlockSolid(p_149800_1_, p_149800_2_, p_149800_3_, p_149800_4_ + 1, 3))
				{
					var13 = true;
				}

				if (var13 || this.isBlockSolid(p_149800_1_, p_149800_2_ - 1, p_149800_3_, p_149800_4_, 4))
				{
					var13 = true;
				}

				if (var13 || this.isBlockSolid(p_149800_1_, p_149800_2_ + 1, p_149800_3_, p_149800_4_, 5))
				{
					var13 = true;
				}

				if (var13 || this.isBlockSolid(p_149800_1_, p_149800_2_, p_149800_3_ + 1, p_149800_4_ - 1, 2))
				{
					var13 = true;
				}

				if (var13 || this.isBlockSolid(p_149800_1_, p_149800_2_, p_149800_3_ + 1, p_149800_4_ + 1, 3))
				{
					var13 = true;
				}

				if (var13 || this.isBlockSolid(p_149800_1_, p_149800_2_ - 1, p_149800_3_ + 1, p_149800_4_, 4))
				{
					var13 = true;
				}

				if (var13 || this.isBlockSolid(p_149800_1_, p_149800_2_ + 1, p_149800_3_ + 1, p_149800_4_, 5))
				{
					var13 = true;
				}

				if (var13)
				{
					var5 = var5.normalize().addVector(0.0D, -6.0D, 0.0D);
				}
			}

			var5 = var5.normalize();
			return var5;
		}

		public override void velocityToAddToEntity(World p_149640_1_, int p_149640_2_, int p_149640_3_, int p_149640_4_, Entity p_149640_5_, Vec3 p_149640_6_)
		{
			Vec3 var7 = this.func_149800_f(p_149640_1_, p_149640_2_, p_149640_3_, p_149640_4_);
			p_149640_6_.xCoord += var7.xCoord;
			p_149640_6_.yCoord += var7.yCoord;
			p_149640_6_.zCoord += var7.zCoord;
		}

		public override int func_149738_a(World p_149738_1_)
		{
			return this.BlockMaterial == Material.water ? 5 : (this.BlockMaterial == Material.lava ? (p_149738_1_.provider.hasNoSky ? 10 : 30) : 0);
		}

		public override int getBlockBrightness(IBlockAccess p_149677_1_, int p_149677_2_, int p_149677_3_, int p_149677_4_)
		{
			int var5 = p_149677_1_.getLightBrightnessForSkyBlocks(p_149677_2_, p_149677_3_, p_149677_4_, 0);
			int var6 = p_149677_1_.getLightBrightnessForSkyBlocks(p_149677_2_, p_149677_3_ + 1, p_149677_4_, 0);
			int var7 = var5 & 255;
			int var8 = var6 & 255;
			int var9 = var5 >> 16 & 255;
			int var10 = var6 >> 16 & 255;
			return (var7 > var8 ? var7 : var8) | (var9 > var10 ? var9 : var10) << 16;
		}

///    
///     <summary> * Returns which pass should this block be rendered on. 0 for solids and 1 for alpha </summary>
///     
		public override int RenderBlockPass
		{
			get
			{
				return this.BlockMaterial == Material.water ? 1 : 0;
			}
		}

///    
///     <summary> * A randomly called display update to be able to add particles or other items for display </summary>
///     
		public override void randomDisplayTick(World p_149734_1_, int p_149734_2_, int p_149734_3_, int p_149734_4_, Random p_149734_5_)
		{
			int var6;

			if (this.BlockMaterial == Material.water)
			{
				if (p_149734_5_.Next(10) == 0)
				{
					var6 = p_149734_1_.getBlockMetadata(p_149734_2_, p_149734_3_, p_149734_4_);

					if (var6 <= 0 || var6 >= 8)
					{
						p_149734_1_.spawnParticle("suspended", ((double)p_149734_2_ + p_149734_5_.NextDouble()), ((double)p_149734_3_ + p_149734_5_.NextDouble()), ((double)p_149734_4_ + p_149734_5_.NextDouble()), 0.0D, 0.0D, 0.0D);
					}
				}

				for (var6 = 0; var6 < 0; ++var6)
				{
					int var7 = p_149734_5_.Next(4);
					int var8 = p_149734_2_;
					int var9 = p_149734_4_;

					if (var7 == 0)
					{
						var8 = p_149734_2_ - 1;
					}

					if (var7 == 1)
					{
						++var8;
					}

					if (var7 == 2)
					{
						var9 = p_149734_4_ - 1;
					}

					if (var7 == 3)
					{
						++var9;
					}

					if (p_149734_1_.getBlock(var8, p_149734_3_, var9).BlockMaterial == Material.air && (p_149734_1_.getBlock(var8, p_149734_3_ - 1, var9).BlockMaterial.BlocksMovement || p_149734_1_.getBlock(var8, p_149734_3_ - 1, var9).BlockMaterial.Liquid))
					{
						double var10 = 0.0625D;
						double var11 = ((double)p_149734_2_ + p_149734_5_.NextDouble());
						double var13 = ((double)p_149734_3_ + p_149734_5_.NextDouble());
						double var15 = ((double)p_149734_4_ + p_149734_5_.NextDouble());

						if (var7 == 0)
						{
							var11 = ((double)p_149734_2_ - var10);
						}

						if (var7 == 1)
						{
							var11 = ((double)(p_149734_2_ + 1) + var10);
						}

						if (var7 == 2)
						{
							var15 = ((double)p_149734_4_ - var10);
						}

						if (var7 == 3)
						{
							var15 = ((double)(p_149734_4_ + 1) + var10);
						}

						double var17 = 0.0D;
						double var19 = 0.0D;

						if (var7 == 0)
						{
							var17 = (double)(-var10);
						}

						if (var7 == 1)
						{
							var17 = (double)var10;
						}

						if (var7 == 2)
						{
							var19 = (double)(-var10);
						}

						if (var7 == 3)
						{
							var19 = (double)var10;
						}

						p_149734_1_.spawnParticle("splash", var11, var13, var15, var17, 0.0D, var19);
					}
				}
			}

			if (this.BlockMaterial == Material.water && p_149734_5_.Next(64) == 0)
			{
				var6 = p_149734_1_.getBlockMetadata(p_149734_2_, p_149734_3_, p_149734_4_);

				if (var6 > 0 && var6 < 8)
				{
					p_149734_1_.playSound((double)p_149734_2_ + 0.5D, (double)p_149734_3_ + 0.5D, (double)p_149734_4_ + 0.5D, "liquid.water", p_149734_5_.NextFloat() * 0.25F + 0.75F, p_149734_5_.NextFloat() * 1.0F + 0.5F, false);
				}
			}

			double var21;
			double var22;
			double var23;

			if (this.BlockMaterial == Material.lava && p_149734_1_.getBlock(p_149734_2_, p_149734_3_ + 1, p_149734_4_).BlockMaterial == Material.air && !p_149734_1_.getBlock(p_149734_2_, p_149734_3_ + 1, p_149734_4_).OpaqueCube)
			{
				if (p_149734_5_.Next(100) == 0)
				{
					var21 = (double)p_149734_2_ + p_149734_5_.NextDouble();
					var22 = (double)p_149734_3_ + this.field_149756_F;
					var23 = (double)p_149734_4_ + p_149734_5_.NextFloat();
					p_149734_1_.spawnParticle("lava", var21, var22, var23, 0.0D, 0.0D, 0.0D);
					p_149734_1_.playSound(var21, var22, var23, "liquid.lavapop", 0.2F + p_149734_5_.NextFloat() * 0.2F, 0.9F + p_149734_5_.NextFloat() * 0.15F, false);
				}

				if (p_149734_5_.Next(200) == 0)
				{
					p_149734_1_.playSound((double)p_149734_2_, (double)p_149734_3_, (double)p_149734_4_, "liquid.lava", 0.2F + p_149734_5_.NextFloat() * 0.2F, 0.9F + p_149734_5_.NextFloat() * 0.15F, false);
				}
			}

			if (p_149734_5_.Next(10) == 0 && World.doesBlockHaveSolidTopSurface(p_149734_1_, p_149734_2_, p_149734_3_ - 1, p_149734_4_) && !p_149734_1_.getBlock(p_149734_2_, p_149734_3_ - 2, p_149734_4_).BlockMaterial.BlocksMovement)
			{
				var21 = (double)p_149734_2_ + p_149734_5_.NextDouble();
				var22 = (double)p_149734_3_ - 1.05D;
				var23 = (double)p_149734_4_ + p_149734_5_.NextDouble();

				if (this.BlockMaterial == Material.water)
				{
					p_149734_1_.spawnParticle("dripWater", var21, var22, var23, 0.0D, 0.0D, 0.0D);
				}
				else
				{
					p_149734_1_.spawnParticle("dripLava", var21, var22, var23, 0.0D, 0.0D, 0.0D);
				}
			}
		}

		public static double func_149802_a(IBlockAccess p_149802_0_, int p_149802_1_, int p_149802_2_, int p_149802_3_, Material p_149802_4_)
		{
			Vec3 var5 = null;

			if (p_149802_4_ == Material.water)
			{
				var5 = Blocks.flowing_water.func_149800_f(p_149802_0_, p_149802_1_, p_149802_2_, p_149802_3_);
			}

			if (p_149802_4_ == Material.lava)
			{
				var5 = Blocks.flowing_lava.func_149800_f(p_149802_0_, p_149802_1_, p_149802_2_, p_149802_3_);
			}

			return var5.xCoord == 0.0D && var5.zCoord == 0.0D ? -1000.0D : Math.Atan2(var5.zCoord, var5.xCoord) - (Math.PI / 2D);
		}

		public override void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
			this.func_149805_n(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);
		}

		public override void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			this.func_149805_n(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_);
		}

		private void func_149805_n(World p_149805_1_, int p_149805_2_, int p_149805_3_, int p_149805_4_)
		{
			if (p_149805_1_.getBlock(p_149805_2_, p_149805_3_, p_149805_4_) == this)
			{
				if (this.BlockMaterial == Material.lava)
				{
					bool var5 = false;

					if (var5 || p_149805_1_.getBlock(p_149805_2_, p_149805_3_, p_149805_4_ - 1).BlockMaterial == Material.water)
					{
						var5 = true;
					}

					if (var5 || p_149805_1_.getBlock(p_149805_2_, p_149805_3_, p_149805_4_ + 1).BlockMaterial == Material.water)
					{
						var5 = true;
					}

					if (var5 || p_149805_1_.getBlock(p_149805_2_ - 1, p_149805_3_, p_149805_4_).BlockMaterial == Material.water)
					{
						var5 = true;
					}

					if (var5 || p_149805_1_.getBlock(p_149805_2_ + 1, p_149805_3_, p_149805_4_).BlockMaterial == Material.water)
					{
						var5 = true;
					}

					if (var5 || p_149805_1_.getBlock(p_149805_2_, p_149805_3_ + 1, p_149805_4_).BlockMaterial == Material.water)
					{
						var5 = true;
					}

					if (var5)
					{
						int var6 = p_149805_1_.getBlockMetadata(p_149805_2_, p_149805_3_, p_149805_4_);

						if (var6 == 0)
						{
							p_149805_1_.setBlock(p_149805_2_, p_149805_3_, p_149805_4_, Blocks.obsidian);
						}
						else if (var6 <= 4)
						{
							p_149805_1_.setBlock(p_149805_2_, p_149805_3_, p_149805_4_, Blocks.cobblestone);
						}

						this.func_149799_m(p_149805_1_, p_149805_2_, p_149805_3_, p_149805_4_);
					}
				}
			}
		}

		protected internal override void func_149799_m(World p_149799_1_, int p_149799_2_, int p_149799_3_, int p_149799_4_)
		{
			p_149799_1_.playSoundEffect((double)p_149799_2_ + 0.5D, (double)p_149799_3_ + 0.5D, (double)p_149799_4_ + 0.5D, "random.fizz", 0.5F, 2.6F + (p_149799_1_.rand.NextFloat() - p_149799_1_.rand.NextFloat()) * 0.8F);

			for (int var5 = 0; var5 < 8; ++var5)
			{
				p_149799_1_.spawnParticle("largesmoke", (double)p_149799_2_ + new Random(1).NextDouble(), (double)p_149799_3_ + 1.2D, (double)p_149799_4_ + new Random(2).NextDouble(), 0.0D, 0.0D, 0.0D);
			}
		}
	}
}