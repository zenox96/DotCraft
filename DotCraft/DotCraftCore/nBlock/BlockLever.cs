using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
namespace DotCraftCore.nBlock
{
	public class BlockLever : Block
	{
		protected internal BlockLever() : base(Material.circuits)
		{
			this.CreativeTab = CreativeTabs.tabRedstone;
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			return null;
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
				return 12;
			}
		}

///    
///     <summary> * checks to see if you can place this block can be placed on that side of a block: BlockLever overrides </summary>
///     
		public override bool canPlaceBlockOnSide(World p_149707_1_, int p_149707_2_, int p_149707_3_, int p_149707_4_, int p_149707_5_)
		{
			return p_149707_5_ == 0 && p_149707_1_.getBlock(p_149707_2_, p_149707_3_ + 1, p_149707_4_).isBlockNormalCube() ? true : (p_149707_5_ == 1 && World.doesBlockHaveSolidTopSurface(p_149707_1_, p_149707_2_, p_149707_3_ - 1, p_149707_4_) ? true : (p_149707_5_ == 2 && p_149707_1_.getBlock(p_149707_2_, p_149707_3_, p_149707_4_ + 1).isBlockNormalCube() ? true : (p_149707_5_ == 3 && p_149707_1_.getBlock(p_149707_2_, p_149707_3_, p_149707_4_ - 1).isBlockNormalCube() ? true : (p_149707_5_ == 4 && p_149707_1_.getBlock(p_149707_2_ + 1, p_149707_3_, p_149707_4_).isBlockNormalCube() ? true : p_149707_5_ == 5 && p_149707_1_.getBlock(p_149707_2_ - 1, p_149707_3_, p_149707_4_).isBlockNormalCube()))));
		}

		public override bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			return p_149742_1_.getBlock(p_149742_2_ - 1, p_149742_3_, p_149742_4_).isBlockNormalCube() ? true : (p_149742_1_.getBlock(p_149742_2_ + 1, p_149742_3_, p_149742_4_).isBlockNormalCube() ? true : (p_149742_1_.getBlock(p_149742_2_, p_149742_3_, p_149742_4_ - 1).isBlockNormalCube() ? true : (p_149742_1_.getBlock(p_149742_2_, p_149742_3_, p_149742_4_ + 1).isBlockNormalCube() ? true : (World.doesBlockHaveSolidTopSurface(p_149742_1_, p_149742_2_, p_149742_3_ - 1, p_149742_4_) ? true : p_149742_1_.getBlock(p_149742_2_, p_149742_3_ + 1, p_149742_4_).isBlockNormalCube()))));
		}

		public override int onBlockPlaced(World p_149660_1_, int p_149660_2_, int p_149660_3_, int p_149660_4_, int p_149660_5_, float p_149660_6_, float p_149660_7_, float p_149660_8_, int p_149660_9_)
		{
			int var11 = p_149660_9_ & 8;
			int var10 = p_149660_9_ & 7;
			sbyte var12 = -1;

			if (p_149660_5_ == 0 && p_149660_1_.getBlock(p_149660_2_, p_149660_3_ + 1, p_149660_4_).isBlockNormalCube())
			{
				var12 = 0;
			}

			if (p_149660_5_ == 1 && World.doesBlockHaveSolidTopSurface(p_149660_1_, p_149660_2_, p_149660_3_ - 1, p_149660_4_))
			{
				var12 = 5;
			}

			if (p_149660_5_ == 2 && p_149660_1_.getBlock(p_149660_2_, p_149660_3_, p_149660_4_ + 1).isBlockNormalCube())
			{
				var12 = 4;
			}

			if (p_149660_5_ == 3 && p_149660_1_.getBlock(p_149660_2_, p_149660_3_, p_149660_4_ - 1).isBlockNormalCube())
			{
				var12 = 3;
			}

			if (p_149660_5_ == 4 && p_149660_1_.getBlock(p_149660_2_ + 1, p_149660_3_, p_149660_4_).isBlockNormalCube())
			{
				var12 = 2;
			}

			if (p_149660_5_ == 5 && p_149660_1_.getBlock(p_149660_2_ - 1, p_149660_3_, p_149660_4_).isBlockNormalCube())
			{
				var12 = 1;
			}

			return var12 + var11;
		}

///    
///     <summary> * Called when the block is placed in the world. </summary>
///     
		public override void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
		{
			int var7 = p_149689_1_.getBlockMetadata(p_149689_2_, p_149689_3_, p_149689_4_);
			int var8 = var7 & 7;
			int var9 = var7 & 8;

			if (var8 == func_149819_b(1))
			{
				if ((MathHelper.floor_double((double)(p_149689_5_.rotationYaw * 4.0F / 360.0F) + 0.5D) & 1) == 0)
				{
					p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 5 | var9, 2);
				}
				else
				{
					p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 6 | var9, 2);
				}
			}
			else if (var8 == func_149819_b(0))
			{
				if ((MathHelper.floor_double((double)(p_149689_5_.rotationYaw * 4.0F / 360.0F) + 0.5D) & 1) == 0)
				{
					p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 7 | var9, 2);
				}
				else
				{
					p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, 0 | var9, 2);
				}
			}
		}

		public static int func_149819_b(int p_149819_0_)
		{
			switch (p_149819_0_)
			{
				case 0:
					return 0;

				case 1:
					return 5;

				case 2:
					return 4;

				case 3:
					return 3;

				case 4:
					return 2;

				case 5:
					return 1;

				default:
					return -1;
			}
		}

		public override void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			if (this.func_149820_e(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_))
			{
				int var6 = p_149695_1_.getBlockMetadata(p_149695_2_, p_149695_3_, p_149695_4_) & 7;
				bool var7 = false;

				if (!p_149695_1_.getBlock(p_149695_2_ - 1, p_149695_3_, p_149695_4_).isBlockNormalCube() && var6 == 1)
				{
					var7 = true;
				}

				if (!p_149695_1_.getBlock(p_149695_2_ + 1, p_149695_3_, p_149695_4_).isBlockNormalCube() && var6 == 2)
				{
					var7 = true;
				}

				if (!p_149695_1_.getBlock(p_149695_2_, p_149695_3_, p_149695_4_ - 1).isBlockNormalCube() && var6 == 3)
				{
					var7 = true;
				}

				if (!p_149695_1_.getBlock(p_149695_2_, p_149695_3_, p_149695_4_ + 1).isBlockNormalCube() && var6 == 4)
				{
					var7 = true;
				}

				if (!World.doesBlockHaveSolidTopSurface(p_149695_1_, p_149695_2_, p_149695_3_ - 1, p_149695_4_) && var6 == 5)
				{
					var7 = true;
				}

				if (!World.doesBlockHaveSolidTopSurface(p_149695_1_, p_149695_2_, p_149695_3_ - 1, p_149695_4_) && var6 == 6)
				{
					var7 = true;
				}

				if (!p_149695_1_.getBlock(p_149695_2_, p_149695_3_ + 1, p_149695_4_).isBlockNormalCube() && var6 == 0)
				{
					var7 = true;
				}

				if (!p_149695_1_.getBlock(p_149695_2_, p_149695_3_ + 1, p_149695_4_).isBlockNormalCube() && var6 == 7)
				{
					var7 = true;
				}

				if (var7)
				{
					this.dropBlockAsItem(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_, p_149695_1_.getBlockMetadata(p_149695_2_, p_149695_3_, p_149695_4_), 0);
					p_149695_1_.setBlockToAir(p_149695_2_, p_149695_3_, p_149695_4_);
				}
			}
		}

		private bool func_149820_e(World p_149820_1_, int p_149820_2_, int p_149820_3_, int p_149820_4_)
		{
			if (!this.canPlaceBlockAt(p_149820_1_, p_149820_2_, p_149820_3_, p_149820_4_))
			{
				this.dropBlockAsItem(p_149820_1_, p_149820_2_, p_149820_3_, p_149820_4_, p_149820_1_.getBlockMetadata(p_149820_2_, p_149820_3_, p_149820_4_), 0);
				p_149820_1_.setBlockToAir(p_149820_2_, p_149820_3_, p_149820_4_);
				return false;
			}
			else
			{
				return true;
			}
		}

		public override void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			int var5 = p_149719_1_.getBlockMetadata(p_149719_2_, p_149719_3_, p_149719_4_) & 7;
			float var6 = 0.1875F;

			if (var5 == 1)
			{
				this.setBlockBounds(0.0F, 0.2F, 0.5F - var6, var6 * 2.0F, 0.8F, 0.5F + var6);
			}
			else if (var5 == 2)
			{
				this.setBlockBounds(1.0F - var6 * 2.0F, 0.2F, 0.5F - var6, 1.0F, 0.8F, 0.5F + var6);
			}
			else if (var5 == 3)
			{
				this.setBlockBounds(0.5F - var6, 0.2F, 0.0F, 0.5F + var6, 0.8F, var6 * 2.0F);
			}
			else if (var5 == 4)
			{
				this.setBlockBounds(0.5F - var6, 0.2F, 1.0F - var6 * 2.0F, 0.5F + var6, 0.8F, 1.0F);
			}
			else if (var5 != 5 && var5 != 6)
			{
				if (var5 == 0 || var5 == 7)
				{
					var6 = 0.25F;
					this.setBlockBounds(0.5F - var6, 0.4F, 0.5F - var6, 0.5F + var6, 1.0F, 0.5F + var6);
				}
			}
			else
			{
				var6 = 0.25F;
				this.setBlockBounds(0.5F - var6, 0.0F, 0.5F - var6, 0.5F + var6, 0.6F, 0.5F + var6);
			}
		}

///    
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public override bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			if (p_149727_1_.isClient)
			{
				return true;
			}
			else
			{
				int var10 = p_149727_1_.getBlockMetadata(p_149727_2_, p_149727_3_, p_149727_4_);
				int var11 = var10 & 7;
				int var12 = 8 - (var10 & 8);
				p_149727_1_.setBlockMetadataWithNotify(p_149727_2_, p_149727_3_, p_149727_4_, var11 + var12, 3);
				p_149727_1_.playSoundEffect((double)p_149727_2_ + 0.5D, (double)p_149727_3_ + 0.5D, (double)p_149727_4_ + 0.5D, "random.click", 0.3F, var12 > 0 ? 0.6F : 0.5F);
				p_149727_1_.notifyBlocksOfNeighborChange(p_149727_2_, p_149727_3_, p_149727_4_, this);

				if (var11 == 1)
				{
					p_149727_1_.notifyBlocksOfNeighborChange(p_149727_2_ - 1, p_149727_3_, p_149727_4_, this);
				}
				else if (var11 == 2)
				{
					p_149727_1_.notifyBlocksOfNeighborChange(p_149727_2_ + 1, p_149727_3_, p_149727_4_, this);
				}
				else if (var11 == 3)
				{
					p_149727_1_.notifyBlocksOfNeighborChange(p_149727_2_, p_149727_3_, p_149727_4_ - 1, this);
				}
				else if (var11 == 4)
				{
					p_149727_1_.notifyBlocksOfNeighborChange(p_149727_2_, p_149727_3_, p_149727_4_ + 1, this);
				}
				else if (var11 != 5 && var11 != 6)
				{
					if (var11 == 0 || var11 == 7)
					{
						p_149727_1_.notifyBlocksOfNeighborChange(p_149727_2_, p_149727_3_ + 1, p_149727_4_, this);
					}
				}
				else
				{
					p_149727_1_.notifyBlocksOfNeighborChange(p_149727_2_, p_149727_3_ - 1, p_149727_4_, this);
				}

				return true;
			}
		}

		public override void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			if ((p_149749_6_ & 8) > 0)
			{
				p_149749_1_.notifyBlocksOfNeighborChange(p_149749_2_, p_149749_3_, p_149749_4_, this);
				int var7 = p_149749_6_ & 7;

				if (var7 == 1)
				{
					p_149749_1_.notifyBlocksOfNeighborChange(p_149749_2_ - 1, p_149749_3_, p_149749_4_, this);
				}
				else if (var7 == 2)
				{
					p_149749_1_.notifyBlocksOfNeighborChange(p_149749_2_ + 1, p_149749_3_, p_149749_4_, this);
				}
				else if (var7 == 3)
				{
					p_149749_1_.notifyBlocksOfNeighborChange(p_149749_2_, p_149749_3_, p_149749_4_ - 1, this);
				}
				else if (var7 == 4)
				{
					p_149749_1_.notifyBlocksOfNeighborChange(p_149749_2_, p_149749_3_, p_149749_4_ + 1, this);
				}
				else if (var7 != 5 && var7 != 6)
				{
					if (var7 == 0 || var7 == 7)
					{
						p_149749_1_.notifyBlocksOfNeighborChange(p_149749_2_, p_149749_3_ + 1, p_149749_4_, this);
					}
				}
				else
				{
					p_149749_1_.notifyBlocksOfNeighborChange(p_149749_2_, p_149749_3_ - 1, p_149749_4_, this);
				}
			}

			base.breakBlock(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_, p_149749_6_);
		}

		public override int isProvidingWeakPower(IBlockAccess p_149709_1_, int p_149709_2_, int p_149709_3_, int p_149709_4_, int p_149709_5_)
		{
			return (p_149709_1_.getBlockMetadata(p_149709_2_, p_149709_3_, p_149709_4_) & 8) > 0 ? 15 : 0;
		}

		public override int isProvidingStrongPower(IBlockAccess p_149748_1_, int p_149748_2_, int p_149748_3_, int p_149748_4_, int p_149748_5_)
		{
			int var6 = p_149748_1_.getBlockMetadata(p_149748_2_, p_149748_3_, p_149748_4_);

			if ((var6 & 8) == 0)
			{
				return 0;
			}
			else
			{
				int var7 = var6 & 7;
				return var7 == 0 && p_149748_5_ == 0 ? 15 : (var7 == 7 && p_149748_5_ == 0 ? 15 : (var7 == 6 && p_149748_5_ == 1 ? 15 : (var7 == 5 && p_149748_5_ == 1 ? 15 : (var7 == 4 && p_149748_5_ == 2 ? 15 : (var7 == 3 && p_149748_5_ == 3 ? 15 : (var7 == 2 && p_149748_5_ == 4 ? 15 : (var7 == 1 && p_149748_5_ == 5 ? 15 : 0)))))));
			}
		}

///    
///     <summary> * Can this block provide power. Only wire currently seems to have this change based on its state. </summary>
///     
		public override bool canProvidePower()
		{
			return true;
		}
	}

}