using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
using System;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockStairs : Block
	{
        private static readonly int[][] field_150150_a =
        {   new int[] { 2, 6 },
            new int[] { 3, 7 },
            new int[] { 2, 3 },
            new int[] { 6, 7 },
            new int[] { 0, 4 },
            new int[] { 1, 5 },
            new int[] { 0, 1 },
            new int[] { 4, 5 } };

		private readonly Block field_150149_b;
		private readonly int field_150151_M;
		private bool field_150152_N;
		private int field_150153_O;
		

		protected internal BlockStairs(Block p_i45428_1_, int p_i45428_2_) : base(p_i45428_1_.BlockMaterial)
		{
			this.field_150149_b = p_i45428_1_;
			this.field_150151_M = p_i45428_2_;
			this.Hardness = p_i45428_1_.blockHardness;
			this.Resistance = p_i45428_1_.blockResistance / 3.0F;
			this.StepSound = p_i45428_1_.StepSound;
			this.LightOpacity = 255;
			this.CreativeTab = CreativeTabs.tabBlock;
		}

		public override void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			if (this.field_150152_N)
			{
				this.setBlockBounds(0.5F * (float)(this.field_150153_O % 2), 0.5F * (float)(this.field_150153_O / 2 % 2), 0.5F * (float)(this.field_150153_O / 4 % 2), 0.5F + 0.5F * (float)(this.field_150153_O % 2), 0.5F + 0.5F * (float)(this.field_150153_O / 2 % 2), 0.5F + 0.5F * (float)(this.field_150153_O / 4 % 2));
			}
			else
			{
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
			}
		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

		public override bool renderAsNormalBlock()
		{
			return false;
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public override int RenderType
		{
			get
			{
				return 10;
			}
		}

		public override void func_150147_e(IBlockAccess p_150147_1_, int p_150147_2_, int p_150147_3_, int p_150147_4_)
		{
			int var5 = p_150147_1_.getBlockMetadata(p_150147_2_, p_150147_3_, p_150147_4_);

			if ((var5 & 4) != 0)
			{
				this.setBlockBounds(0.0F, 0.5F, 0.0F, 1.0F, 1.0F, 1.0F);
			}
			else
			{
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.5F, 1.0F);
			}
		}

		public static bool func_150148_a(Block p_150148_0_)
		{
			return p_150148_0_ is BlockStairs;
		}

		private bool func_150146_f(IBlockAccess p_150146_1_, int p_150146_2_, int p_150146_3_, int p_150146_4_, int p_150146_5_)
		{
			Block var6 = p_150146_1_.getBlock(p_150146_2_, p_150146_3_, p_150146_4_);
			return func_150148_a(var6) && p_150146_1_.getBlockMetadata(p_150146_2_, p_150146_3_, p_150146_4_) == p_150146_5_;
		}

		public override bool func_150145_f(IBlockAccess p_150145_1_, int p_150145_2_, int p_150145_3_, int p_150145_4_)
		{
			int var5 = p_150145_1_.getBlockMetadata(p_150145_2_, p_150145_3_, p_150145_4_);
			int var6 = var5 & 3;
			float var7 = 0.5F;
			float var8 = 1.0F;

			if ((var5 & 4) != 0)
			{
				var7 = 0.0F;
				var8 = 0.5F;
			}

			float var9 = 0.0F;
			float var10 = 1.0F;
			float var11 = 0.0F;
			float var12 = 0.5F;
			bool var13 = true;
			Block var14;
			int var15;
			int var16;

			if (var6 == 0)
			{
				var9 = 0.5F;
				var12 = 1.0F;
				var14 = p_150145_1_.getBlock(p_150145_2_ + 1, p_150145_3_, p_150145_4_);
				var15 = p_150145_1_.getBlockMetadata(p_150145_2_ + 1, p_150145_3_, p_150145_4_);

				if (func_150148_a(var14) && (var5 & 4) == (var15 & 4))
				{
					var16 = var15 & 3;

					if (var16 == 3 && !this.func_150146_f(p_150145_1_, p_150145_2_, p_150145_3_, p_150145_4_ + 1, var5))
					{
						var12 = 0.5F;
						var13 = false;
					}
					else if (var16 == 2 && !this.func_150146_f(p_150145_1_, p_150145_2_, p_150145_3_, p_150145_4_ - 1, var5))
					{
						var11 = 0.5F;
						var13 = false;
					}
				}
			}
			else if (var6 == 1)
			{
				var10 = 0.5F;
				var12 = 1.0F;
				var14 = p_150145_1_.getBlock(p_150145_2_ - 1, p_150145_3_, p_150145_4_);
				var15 = p_150145_1_.getBlockMetadata(p_150145_2_ - 1, p_150145_3_, p_150145_4_);

				if (func_150148_a(var14) && (var5 & 4) == (var15 & 4))
				{
					var16 = var15 & 3;

					if (var16 == 3 && !this.func_150146_f(p_150145_1_, p_150145_2_, p_150145_3_, p_150145_4_ + 1, var5))
					{
						var12 = 0.5F;
						var13 = false;
					}
					else if (var16 == 2 && !this.func_150146_f(p_150145_1_, p_150145_2_, p_150145_3_, p_150145_4_ - 1, var5))
					{
						var11 = 0.5F;
						var13 = false;
					}
				}
			}
			else if (var6 == 2)
			{
				var11 = 0.5F;
				var12 = 1.0F;
				var14 = p_150145_1_.getBlock(p_150145_2_, p_150145_3_, p_150145_4_ + 1);
				var15 = p_150145_1_.getBlockMetadata(p_150145_2_, p_150145_3_, p_150145_4_ + 1);

				if (func_150148_a(var14) && (var5 & 4) == (var15 & 4))
				{
					var16 = var15 & 3;

					if (var16 == 1 && !this.func_150146_f(p_150145_1_, p_150145_2_ + 1, p_150145_3_, p_150145_4_, var5))
					{
						var10 = 0.5F;
						var13 = false;
					}
					else if (var16 == 0 && !this.func_150146_f(p_150145_1_, p_150145_2_ - 1, p_150145_3_, p_150145_4_, var5))
					{
						var9 = 0.5F;
						var13 = false;
					}
				}
			}
			else if (var6 == 3)
			{
				var14 = p_150145_1_.getBlock(p_150145_2_, p_150145_3_, p_150145_4_ - 1);
				var15 = p_150145_1_.getBlockMetadata(p_150145_2_, p_150145_3_, p_150145_4_ - 1);

				if (func_150148_a(var14) && (var5 & 4) == (var15 & 4))
				{
					var16 = var15 & 3;

					if (var16 == 1 && !this.func_150146_f(p_150145_1_, p_150145_2_ + 1, p_150145_3_, p_150145_4_, var5))
					{
						var10 = 0.5F;
						var13 = false;
					}
					else if (var16 == 0 && !this.func_150146_f(p_150145_1_, p_150145_2_ - 1, p_150145_3_, p_150145_4_, var5))
					{
						var9 = 0.5F;
						var13 = false;
					}
				}
			}

			this.setBlockBounds(var9, var7, var11, var10, var8, var12);
			return var13;
		}

		public override bool func_150144_g(IBlockAccess p_150144_1_, int p_150144_2_, int p_150144_3_, int p_150144_4_)
		{
			int var5 = p_150144_1_.getBlockMetadata(p_150144_2_, p_150144_3_, p_150144_4_);
			int var6 = var5 & 3;
			float var7 = 0.5F;
			float var8 = 1.0F;

			if ((var5 & 4) != 0)
			{
				var7 = 0.0F;
				var8 = 0.5F;
			}

			float var9 = 0.0F;
			float var10 = 0.5F;
			float var11 = 0.5F;
			float var12 = 1.0F;
			bool var13 = false;
			Block var14;
			int var15;
			int var16;

			if (var6 == 0)
			{
				var14 = p_150144_1_.getBlock(p_150144_2_ - 1, p_150144_3_, p_150144_4_);
				var15 = p_150144_1_.getBlockMetadata(p_150144_2_ - 1, p_150144_3_, p_150144_4_);

				if (func_150148_a(var14) && (var5 & 4) == (var15 & 4))
				{
					var16 = var15 & 3;

					if (var16 == 3 && !this.func_150146_f(p_150144_1_, p_150144_2_, p_150144_3_, p_150144_4_ - 1, var5))
					{
						var11 = 0.0F;
						var12 = 0.5F;
						var13 = true;
					}
					else if (var16 == 2 && !this.func_150146_f(p_150144_1_, p_150144_2_, p_150144_3_, p_150144_4_ + 1, var5))
					{
						var11 = 0.5F;
						var12 = 1.0F;
						var13 = true;
					}
				}
			}
			else if (var6 == 1)
			{
				var14 = p_150144_1_.getBlock(p_150144_2_ + 1, p_150144_3_, p_150144_4_);
				var15 = p_150144_1_.getBlockMetadata(p_150144_2_ + 1, p_150144_3_, p_150144_4_);

				if (func_150148_a(var14) && (var5 & 4) == (var15 & 4))
				{
					var9 = 0.5F;
					var10 = 1.0F;
					var16 = var15 & 3;

					if (var16 == 3 && !this.func_150146_f(p_150144_1_, p_150144_2_, p_150144_3_, p_150144_4_ - 1, var5))
					{
						var11 = 0.0F;
						var12 = 0.5F;
						var13 = true;
					}
					else if (var16 == 2 && !this.func_150146_f(p_150144_1_, p_150144_2_, p_150144_3_, p_150144_4_ + 1, var5))
					{
						var11 = 0.5F;
						var12 = 1.0F;
						var13 = true;
					}
				}
			}
			else if (var6 == 2)
			{
				var14 = p_150144_1_.getBlock(p_150144_2_, p_150144_3_, p_150144_4_ - 1);
				var15 = p_150144_1_.getBlockMetadata(p_150144_2_, p_150144_3_, p_150144_4_ - 1);

				if (func_150148_a(var14) && (var5 & 4) == (var15 & 4))
				{
					var11 = 0.0F;
					var12 = 0.5F;
					var16 = var15 & 3;

					if (var16 == 1 && !this.func_150146_f(p_150144_1_, p_150144_2_ - 1, p_150144_3_, p_150144_4_, var5))
					{
						var13 = true;
					}
					else if (var16 == 0 && !this.func_150146_f(p_150144_1_, p_150144_2_ + 1, p_150144_3_, p_150144_4_, var5))
					{
						var9 = 0.5F;
						var10 = 1.0F;
						var13 = true;
					}
				}
			}
			else if (var6 == 3)
			{
				var14 = p_150144_1_.getBlock(p_150144_2_, p_150144_3_, p_150144_4_ + 1);
				var15 = p_150144_1_.getBlockMetadata(p_150144_2_, p_150144_3_, p_150144_4_ + 1);

				if (func_150148_a(var14) && (var5 & 4) == (var15 & 4))
				{
					var16 = var15 & 3;

					if (var16 == 1 && !this.func_150146_f(p_150144_1_, p_150144_2_ - 1, p_150144_3_, p_150144_4_, var5))
					{
						var13 = true;
					}
					else if (var16 == 0 && !this.func_150146_f(p_150144_1_, p_150144_2_ + 1, p_150144_3_, p_150144_4_, var5))
					{
						var9 = 0.5F;
						var10 = 1.0F;
						var13 = true;
					}
				}
			}

			if (var13)
			{
				this.setBlockBounds(var9, var7, var11, var10, var8, var12);
			}

			return var13;
		}

		public override void addCollisionBoxesToList(World p_149743_1_, int p_149743_2_, int p_149743_3_, int p_149743_4_, AxisAlignedBB p_149743_5_, IList p_149743_6_, Entity p_149743_7_)
		{
			this.func_150147_e(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_);
			base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
			bool var8 = this.func_150145_f(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_);
			base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);

			if (var8 && this.func_150144_g(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_))
			{
				base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
			}

			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
		}

///    
///     <summary> * A randomly called display update to be able to add particles or other items for display </summary>
///     
		public override void randomDisplayTick(World p_149734_1_, int p_149734_2_, int p_149734_3_, int p_149734_4_, Random p_149734_5_)
		{
			this.field_150149_b.randomDisplayTick(p_149734_1_, p_149734_2_, p_149734_3_, p_149734_4_, p_149734_5_);
		}

///    
///     <summary> * Called when a player hits the block. Args: world, x, y, z, player </summary>
///     
		public override void onBlockClicked(World p_149699_1_, int p_149699_2_, int p_149699_3_, int p_149699_4_, EntityPlayer p_149699_5_)
		{
			this.field_150149_b.onBlockClicked(p_149699_1_, p_149699_2_, p_149699_3_, p_149699_4_, p_149699_5_);
		}

		public override void onBlockDestroyedByPlayer(World p_149664_1_, int p_149664_2_, int p_149664_3_, int p_149664_4_, int p_149664_5_)
		{
			this.field_150149_b.onBlockDestroyedByPlayer(p_149664_1_, p_149664_2_, p_149664_3_, p_149664_4_, p_149664_5_);
		}

		public override int getBlockBrightness(IBlockAccess p_149677_1_, int p_149677_2_, int p_149677_3_, int p_149677_4_)
		{
			return this.field_150149_b.getBlockBrightness(p_149677_1_, p_149677_2_, p_149677_3_, p_149677_4_);
		}

///    
///     <summary> * Returns how much this block can resist explosions from the passed in entity. </summary>
///     
		public override float getExplosionResistance(Entity p_149638_1_)
		{
			return this.field_150149_b.getExplosionResistance(p_149638_1_);
		}

///    
///     <summary> * Returns which pass should this block be rendered on. 0 for solids and 1 for alpha </summary>
///     
		public override int RenderBlockPass
		{
			get
			{
				return this.field_150149_b.RenderBlockPass;
			}
		}

		public override int func_149738_a(World p_149738_1_)
		{
			return this.field_150149_b.func_149738_a(p_149738_1_);
		}

///    
///     <summary> * Returns the bounding box of the wired rectangular prism to render. </summary>
///     
		public override AxisAlignedBB getSelectedBoundingBoxFromPool(World p_149633_1_, int p_149633_2_, int p_149633_3_, int p_149633_4_)
		{
			return this.field_150149_b.getSelectedBoundingBoxFromPool(p_149633_1_, p_149633_2_, p_149633_3_, p_149633_4_);
		}

		public override void velocityToAddToEntity(World p_149640_1_, int p_149640_2_, int p_149640_3_, int p_149640_4_, Entity p_149640_5_, Vec3 p_149640_6_)
		{
			this.field_150149_b.velocityToAddToEntity(p_149640_1_, p_149640_2_, p_149640_3_, p_149640_4_, p_149640_5_, p_149640_6_);
		}

		public override bool isCollidable()
		{
			return this.field_150149_b.isCollidable();
		}

///    
///     * Returns whether this block is collideable based on the arguments passed in \n<param name="par1"> block metaData \n@param
///     * par2 whether the player right-clicked while holding a boat </param>
///     
		public override bool canCollideCheck(int p_149678_1_, bool p_149678_2_)
		{
			return this.field_150149_b.canCollideCheck(p_149678_1_, p_149678_2_);
		}

		public override bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			return this.field_150149_b.canPlaceBlockAt(p_149742_1_, p_149742_2_, p_149742_3_, p_149742_4_);
		}

		public override void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
			this.onNeighborBlockChange(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_, Blocks.air);
			this.field_150149_b.onBlockAdded(p_149726_1_, p_149726_2_, p_149726_3_, p_149726_4_);
		}

		public override void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			this.field_150149_b.breakBlock(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_, p_149749_6_);
		}

		public override void onEntityWalking(World p_149724_1_, int p_149724_2_, int p_149724_3_, int p_149724_4_, Entity p_149724_5_)
		{
			this.field_150149_b.onEntityWalking(p_149724_1_, p_149724_2_, p_149724_3_, p_149724_4_, p_149724_5_);
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public override void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			this.field_150149_b.updateTick(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_, p_149674_5_);
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public override bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			return this.field_150149_b.onBlockActivated(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_, p_149727_5_, 0, 0.0F, 0.0F, 0.0F);
		}

///    
///     <summary> * Called upon the block being destroyed by an explosion </summary>
///     
		public override void onBlockDestroyedByExplosion(World p_149723_1_, int p_149723_2_, int p_149723_3_, int p_149723_4_, Explosion p_149723_5_)
		{
			this.field_150149_b.onBlockDestroyedByExplosion(p_149723_1_, p_149723_2_, p_149723_3_, p_149723_4_, p_149723_5_);
		}

		public override MapColor getMapColor(int p_149728_1_)
		{
			return this.field_150149_b.getMapColor(this.field_150151_M);
		}

///    
///     <summary> * Called when the block is placed in the world. </summary>
///     
		public override void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
		{
			int var7 = MathHelper.floor_double((double)(p_149689_5_.rotationYaw * 4.0F / 360.0F) + 0.5D) & 3;
			int var8 = p_149689_1_.getBlockMetadata(p_149689_2_, p_149689_3_, p_149689_4_) & 4;

			if (var7 == 0)
			{
				p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 2 | var8, 2);
			}

			if (var7 == 1)
			{
				p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 1 | var8, 2);
			}

			if (var7 == 2)
			{
				p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 3 | var8, 2);
			}

			if (var7 == 3)
			{
				p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 0 | var8, 2);
			}
		}

		public override int onBlockPlaced(World p_149660_1_, int p_149660_2_, int p_149660_3_, int p_149660_4_, int p_149660_5_, float p_149660_6_, float p_149660_7_, float p_149660_8_, int p_149660_9_)
		{
			return p_149660_5_ != 0 && (p_149660_5_ == 1 || (double)p_149660_7_ <= 0.5D) ? p_149660_9_ : p_149660_9_ | 4;
		}

		public override MovingObjectPosition collisionRayTrace(World pWorld, int pX, int pY, int pZ, Vec3 p_149731_5_, Vec3 p_149731_6_)
		{
			MovingObjectPosition[] mopArray = new MovingObjectPosition[8];
			int blockMetaData = pWorld.getBlockMetadata(pX, pY, pZ);
            int dirFlag = blockMetaData & 3; //is byte flag 0x00000010 set? or is byte flag 0x00000001 set? This probably relates to direction
			bool var10 = (blockMetaData & 4) == 4; //is byte flag 0x00000100 set?
			int[] var11 = field_150150_a[dirFlag + (var10 ? 4 : 0)];
			this.field_150152_N = true;
			int arraywidth1;
			int var15;
			int var16;

			for (int var12 = 0; var12 < 8; ++var12)
			{
				this.field_150153_O = var12;
				int[] var13 = var11;
				arraywidth1 = var11.Length;

				for (var15 = 0; var15 < arraywidth1; ++var15)
				{
					var16 = var13[var15];
				}

				mopArray[var12] = base.collisionRayTrace(pWorld, pX, pY, pZ, p_149731_5_, p_149731_6_);
			}

			int[] var21 = var11;
			int var23 = var11.Length;

			for (arraywidth1 = 0; arraywidth1 < var23; ++arraywidth1)
			{
				var15 = var21[arraywidth1];
				mopArray[var15] = null;
			}

			MovingObjectPosition var22 = null;
			double var24 = 0.0D;
			MovingObjectPosition[] var25 = mopArray;
			var16 = mopArray.Length;

			for (int var17 = 0; var17 < var16; ++var17)
			{
				MovingObjectPosition var18 = var25[var17];

				if (var18 != null)
				{
					double var19 = var18.hitVec.squareDistanceTo(p_149731_6_);

					if (var19 > var24)
					{
						var22 = var18;
						var24 = var19;
					}
				}
			}

			return var22;
		}
	}
}