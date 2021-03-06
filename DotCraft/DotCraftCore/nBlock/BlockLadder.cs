using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockLadder : Block
	{
		protected internal BlockLadder() : base(Material.circuits)
		{
			this.CreativeTab = CreativeTabs.tabDecorations;
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			this.setBlockBoundsBasedOnState(p_149668_1_, p_149668_2_, p_149668_3_, p_149668_4_);
			return base.getCollisionBoundingBoxFromPool(p_149668_1_, p_149668_2_, p_149668_3_, p_149668_4_);
		}

///    
///     <summary> * Returns the bounding box of the wired rectangular prism to render. </summary>
///     
		public override AxisAlignedBB getSelectedBoundingBoxFromPool(World p_149633_1_, int p_149633_2_, int p_149633_3_, int p_149633_4_)
		{
			this.setBlockBoundsBasedOnState(p_149633_1_, p_149633_2_, p_149633_3_, p_149633_4_);
			return base.getSelectedBoundingBoxFromPool(p_149633_1_, p_149633_2_, p_149633_3_, p_149633_4_);
		}

		public override void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			this.func_149797_b(p_149719_1_.getBlockMetadata(p_149719_2_, p_149719_3_, p_149719_4_));
		}

		public override void func_149797_b(int p_149797_1_)
		{
			float var3 = 0.125F;

			if (p_149797_1_ == 2)
			{
				this.setBlockBounds(0.0F, 0.0F, 1.0F - var3, 1.0F, 1.0F, 1.0F);
			}

			if (p_149797_1_ == 3)
			{
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, var3);
			}

			if (p_149797_1_ == 4)
			{
				this.setBlockBounds(1.0F - var3, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
			}

			if (p_149797_1_ == 5)
			{
				this.setBlockBounds(0.0F, 0.0F, 0.0F, var3, 1.0F, 1.0F);
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
				return 8;
			}
		}

		public override bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			return p_149742_1_.getBlock(p_149742_2_ - 1, p_149742_3_, p_149742_4_).isBlockNormalCube() ? true : (p_149742_1_.getBlock(p_149742_2_ + 1, p_149742_3_, p_149742_4_).isBlockNormalCube() ? true : (p_149742_1_.getBlock(p_149742_2_, p_149742_3_, p_149742_4_ - 1).isBlockNormalCube() ? true : p_149742_1_.getBlock(p_149742_2_, p_149742_3_, p_149742_4_ + 1).isBlockNormalCube()));
		}

		public override int onBlockPlaced(World p_149660_1_, int p_149660_2_, int p_149660_3_, int p_149660_4_, int p_149660_5_, float p_149660_6_, float p_149660_7_, float p_149660_8_, int p_149660_9_)
		{
			int var10 = p_149660_9_;

			if ((p_149660_9_ == 0 || p_149660_5_ == 2) && p_149660_1_.getBlock(p_149660_2_, p_149660_3_, p_149660_4_ + 1).isBlockNormalCube())
			{
				var10 = 2;
			}

			if ((var10 == 0 || p_149660_5_ == 3) && p_149660_1_.getBlock(p_149660_2_, p_149660_3_, p_149660_4_ - 1).isBlockNormalCube())
			{
				var10 = 3;
			}

			if ((var10 == 0 || p_149660_5_ == 4) && p_149660_1_.getBlock(p_149660_2_ + 1, p_149660_3_, p_149660_4_).isBlockNormalCube())
			{
				var10 = 4;
			}

			if ((var10 == 0 || p_149660_5_ == 5) && p_149660_1_.getBlock(p_149660_2_ - 1, p_149660_3_, p_149660_4_).isBlockNormalCube())
			{
				var10 = 5;
			}

			return var10;
		}

		public override void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			int var6 = p_149695_1_.getBlockMetadata(p_149695_2_, p_149695_3_, p_149695_4_);
			bool var7 = false;

			if (var6 == 2 && p_149695_1_.getBlock(p_149695_2_, p_149695_3_, p_149695_4_ + 1).isBlockNormalCube())
			{
				var7 = true;
			}

			if (var6 == 3 && p_149695_1_.getBlock(p_149695_2_, p_149695_3_, p_149695_4_ - 1).isBlockNormalCube())
			{
				var7 = true;
			}

			if (var6 == 4 && p_149695_1_.getBlock(p_149695_2_ + 1, p_149695_3_, p_149695_4_).isBlockNormalCube())
			{
				var7 = true;
			}

			if (var6 == 5 && p_149695_1_.getBlock(p_149695_2_ - 1, p_149695_3_, p_149695_4_).isBlockNormalCube())
			{
				var7 = true;
			}

			if (!var7)
			{
				this.dropBlockAsItem(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_, var6, 0);
				p_149695_1_.setBlockToAir(p_149695_2_, p_149695_3_, p_149695_4_);
			}

			base.onNeighborBlockChange(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_, p_149695_5_);
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public override int quantityDropped(Random p_149745_1_)
		{
			return 1;
		}
	}

}