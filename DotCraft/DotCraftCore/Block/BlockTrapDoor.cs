namespace DotCraftCore.Block
{

	
	using CreativeTabs = DotCraftCore.creativetab.CreativeTabs;
	using EntityPlayer = DotCraftCore.entity.player.EntityPlayer;
	using Blocks = DotCraftCore.init.Blocks;
	using AxisAlignedBB = DotCraftCore.util.AxisAlignedBB;
	using MovingObjectPosition = DotCraftCore.util.MovingObjectPosition;
	using Vec3 = DotCraftCore.util.Vec3;
	using IBlockAccess = DotCraftCore.world.IBlockAccess;
	using World = DotCraftCore.world.World;

	public class BlockTrapDoor : Block
	{
		private const string __OBFID = "CL_00000327";

		protected internal BlockTrapDoor(Material p_i45434_1_) : base(p_i45434_1_)
		{
			float var2 = 0.5F;
			float var3 = 1.0F;
			this.setBlockBounds(0.5F - var2, 0.0F, 0.5F - var2, 0.5F + var2, var3, 0.5F + var2);
			this.CreativeTab = CreativeTabs.tabRedstone;
		}

		public virtual bool isOpaqueCube()
		{
			get
			{
				return false;
			}
		}

		public virtual bool renderAsNormalBlock()
		{
			return false;
		}

		public virtual bool getBlocksMovement(IBlockAccess p_149655_1_, int p_149655_2_, int p_149655_3_, int p_149655_4_)
		{
			return !func_150118_d(p_149655_1_.getBlockMetadata(p_149655_2_, p_149655_3_, p_149655_4_));
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public virtual int RenderType
		{
			get
			{
				return 0;
			}
		}

///    
///     <summary> * Returns the bounding box of the wired rectangular prism to render. </summary>
///     
		public virtual AxisAlignedBB getSelectedBoundingBoxFromPool(World p_149633_1_, int p_149633_2_, int p_149633_3_, int p_149633_4_)
		{
			this.setBlockBoundsBasedOnState(p_149633_1_, p_149633_2_, p_149633_3_, p_149633_4_);
			return base.getSelectedBoundingBoxFromPool(p_149633_1_, p_149633_2_, p_149633_3_, p_149633_4_);
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public virtual AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			this.setBlockBoundsBasedOnState(p_149668_1_, p_149668_2_, p_149668_3_, p_149668_4_);
			return base.getCollisionBoundingBoxFromPool(p_149668_1_, p_149668_2_, p_149668_3_, p_149668_4_);
		}

		public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			this.func_150117_b(p_149719_1_.getBlockMetadata(p_149719_2_, p_149719_3_, p_149719_4_));
		}

///    
///     <summary> * Sets the block's bounds for rendering it as an item </summary>
///     
		public virtual void setBlockBoundsForItemRender()
		{
			float var1 = 0.1875F;
			this.setBlockBounds(0.0F, 0.5F - var1 / 2.0F, 0.0F, 1.0F, 0.5F + var1 / 2.0F, 1.0F);
		}

		public virtual void func_150117_b(int p_150117_1_)
		{
			float var2 = 0.1875F;

			if ((p_150117_1_ & 8) != 0)
			{
				this.setBlockBounds(0.0F, 1.0F - var2, 0.0F, 1.0F, 1.0F, 1.0F);
			}
			else
			{
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, var2, 1.0F);
			}

			if (func_150118_d(p_150117_1_))
			{
				if ((p_150117_1_ & 3) == 0)
				{
					this.setBlockBounds(0.0F, 0.0F, 1.0F - var2, 1.0F, 1.0F, 1.0F);
				}

				if ((p_150117_1_ & 3) == 1)
				{
					this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, var2);
				}

				if ((p_150117_1_ & 3) == 2)
				{
					this.setBlockBounds(1.0F - var2, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
				}

				if ((p_150117_1_ & 3) == 3)
				{
					this.setBlockBounds(0.0F, 0.0F, 0.0F, var2, 1.0F, 1.0F);
				}
			}
		}

///    
///     <summary> * Called when a player hits the block. Args: world, x, y, z, player </summary>
///     
		public virtual void onBlockClicked(World p_149699_1_, int p_149699_2_, int p_149699_3_, int p_149699_4_, EntityPlayer p_149699_5_)
		{
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public virtual bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			if (this.blockMaterial == Material.iron)
			{
				return true;
			}
			else
			{
				int var10 = p_149727_1_.getBlockMetadata(p_149727_2_, p_149727_3_, p_149727_4_);
				p_149727_1_.setBlockMetadataWithNotify(p_149727_2_, p_149727_3_, p_149727_4_, var10 ^ 4, 2);
				p_149727_1_.playAuxSFXAtEntity(p_149727_5_, 1003, p_149727_2_, p_149727_3_, p_149727_4_, 0);
				return true;
			}
		}

		public virtual void func_150120_a(World p_150120_1_, int p_150120_2_, int p_150120_3_, int p_150120_4_, bool p_150120_5_)
		{
			int var6 = p_150120_1_.getBlockMetadata(p_150120_2_, p_150120_3_, p_150120_4_);
			bool var7 = (var6 & 4) > 0;

			if (var7 != p_150120_5_)
			{
				p_150120_1_.setBlockMetadataWithNotify(p_150120_2_, p_150120_3_, p_150120_4_, var6 ^ 4, 2);
				p_150120_1_.playAuxSFXAtEntity((EntityPlayer)null, 1003, p_150120_2_, p_150120_3_, p_150120_4_, 0);
			}
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			if (!p_149695_1_.isClient)
			{
				int var6 = p_149695_1_.getBlockMetadata(p_149695_2_, p_149695_3_, p_149695_4_);
				int var7 = p_149695_2_;
				int var8 = p_149695_4_;

				if ((var6 & 3) == 0)
				{
					var8 = p_149695_4_ + 1;
				}

				if ((var6 & 3) == 1)
				{
					--var8;
				}

				if ((var6 & 3) == 2)
				{
					var7 = p_149695_2_ + 1;
				}

				if ((var6 & 3) == 3)
				{
					--var7;
				}

				if (!func_150119_a(p_149695_1_.getBlock(var7, p_149695_3_, var8)))
				{
					p_149695_1_.setBlockToAir(p_149695_2_, p_149695_3_, p_149695_4_);
					this.dropBlockAsItem(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_, var6, 0);
				}

				bool var9 = p_149695_1_.isBlockIndirectlyGettingPowered(p_149695_2_, p_149695_3_, p_149695_4_);

				if (var9 || p_149695_5_.canProvidePower())
				{
					this.func_150120_a(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_, var9);
				}
			}
		}

		public virtual MovingObjectPosition collisionRayTrace(World p_149731_1_, int p_149731_2_, int p_149731_3_, int p_149731_4_, Vec3 p_149731_5_, Vec3 p_149731_6_)
		{
			this.setBlockBoundsBasedOnState(p_149731_1_, p_149731_2_, p_149731_3_, p_149731_4_);
			return base.collisionRayTrace(p_149731_1_, p_149731_2_, p_149731_3_, p_149731_4_, p_149731_5_, p_149731_6_);
		}

		public virtual int onBlockPlaced(World p_149660_1_, int p_149660_2_, int p_149660_3_, int p_149660_4_, int p_149660_5_, float p_149660_6_, float p_149660_7_, float p_149660_8_, int p_149660_9_)
		{
			int var10 = 0;

			if (p_149660_5_ == 2)
			{
				var10 = 0;
			}

			if (p_149660_5_ == 3)
			{
				var10 = 1;
			}

			if (p_149660_5_ == 4)
			{
				var10 = 2;
			}

			if (p_149660_5_ == 5)
			{
				var10 = 3;
			}

			if (p_149660_5_ != 1 && p_149660_5_ != 0 && p_149660_7_ > 0.5F)
			{
				var10 |= 8;
			}

			return var10;
		}

///    
///     <summary> * checks to see if you can place this block can be placed on that side of a block: BlockLever overrides </summary>
///     
		public virtual bool canPlaceBlockOnSide(World p_149707_1_, int p_149707_2_, int p_149707_3_, int p_149707_4_, int p_149707_5_)
		{
			if (p_149707_5_ == 0)
			{
				return false;
			}
			else if (p_149707_5_ == 1)
			{
				return false;
			}
			else
			{
				if (p_149707_5_ == 2)
				{
					++p_149707_4_;
				}

				if (p_149707_5_ == 3)
				{
					--p_149707_4_;
				}

				if (p_149707_5_ == 4)
				{
					++p_149707_2_;
				}

				if (p_149707_5_ == 5)
				{
					--p_149707_2_;
				}

				return func_150119_a(p_149707_1_.getBlock(p_149707_2_, p_149707_3_, p_149707_4_));
			}
		}

		public static bool func_150118_d(int p_150118_0_)
		{
			return (p_150118_0_ & 4) != 0;
		}

		private static bool func_150119_a(Block p_150119_0_)
		{
			return p_150119_0_.blockMaterial.Opaque && p_150119_0_.renderAsNormalBlock() || p_150119_0_ == Blocks.glowstone || p_150119_0_ is BlockSlab || p_150119_0_ is BlockStairs;
		}
	}

}