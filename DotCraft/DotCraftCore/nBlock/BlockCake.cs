using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity.nPlayer;
using DotCraftCore.nInit;
using DotCraftCore.nItem;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockCake : Block
	{
		protected internal BlockCake() : base(Material.field_151568_F)
		{
			this.TickRandomly = true;
		}

		public override void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			int var5 = p_149719_1_.getBlockMetadata(p_149719_2_, p_149719_3_, p_149719_4_);
			float var6 = 0.0625F;
			float var7 = (float)(1 + var5 * 2) / 16.0F;
			float var8 = 0.5F;
			this.setBlockBounds(var7, 0.0F, var6, 1.0F - var6, var8, 1.0F - var6);
		}

///    
///     <summary> * Sets the block's bounds for rendering it as an item </summary>
///     
		public override void setBlockBoundsForItemRender()
		{
			float var1 = 0.0625F;
			float var2 = 0.5F;
			this.setBlockBounds(var1, 0.0F, var1, 1.0F - var1, var2, 1.0F - var1);
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			int var5 = p_149668_1_.getBlockMetadata(p_149668_2_, p_149668_3_, p_149668_4_);
			float var6 = 0.0625F;
			float var7 = (float)(1 + var5 * 2) / 16.0F;
			float var8 = 0.5F;
			return AxisAlignedBB.getBoundingBox((double)((float)p_149668_2_ + var7), (double)p_149668_3_, (double)((float)p_149668_4_ + var6), (double)((float)(p_149668_2_ + 1) - var6), (double)((float)p_149668_3_ + var8 - var6), (double)((float)(p_149668_4_ + 1) - var6));
		}

///    
///     <summary> * Returns the bounding box of the wired rectangular prism to render. </summary>
///     
		public override AxisAlignedBB getSelectedBoundingBoxFromPool(World p_149633_1_, int p_149633_2_, int p_149633_3_, int p_149633_4_)
		{
			int var5 = p_149633_1_.getBlockMetadata(p_149633_2_, p_149633_3_, p_149633_4_);
			float var6 = 0.0625F;
			float var7 = (float)(1 + var5 * 2) / 16.0F;
			float var8 = 0.5F;
			return AxisAlignedBB.getBoundingBox((double)((float)p_149633_2_ + var7), (double)p_149633_3_, (double)((float)p_149633_4_ + var6), (double)((float)(p_149633_2_ + 1) - var6), (double)((float)p_149633_3_ + var8), (double)((float)(p_149633_4_ + 1) - var6));
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
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public override bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			this.func_150036_b(p_149727_1_, p_149727_2_, p_149727_3_, p_149727_4_, p_149727_5_);
			return true;
		}

///    
///     <summary> * Called when a player hits the block. Args: world, x, y, z, player </summary>
///     
		public override void onBlockClicked(World p_149699_1_, int p_149699_2_, int p_149699_3_, int p_149699_4_, EntityPlayer p_149699_5_)
		{
			this.func_150036_b(p_149699_1_, p_149699_2_, p_149699_3_, p_149699_4_, p_149699_5_);
		}

		private void func_150036_b(World p_150036_1_, int p_150036_2_, int p_150036_3_, int p_150036_4_, EntityPlayer p_150036_5_)
		{
			if (p_150036_5_.canEat(false))
			{
				p_150036_5_.FoodStats.addStats(2, 0.1F);
				int var6 = p_150036_1_.getBlockMetadata(p_150036_2_, p_150036_3_, p_150036_4_) + 1;

				if (var6 >= 6)
				{
					p_150036_1_.setBlockToAir(p_150036_2_, p_150036_3_, p_150036_4_);
				}
				else
				{
					p_150036_1_.setBlockMetadataWithNotify(p_150036_2_, p_150036_3_, p_150036_4_, var6, 2);
				}
			}
		}

		public override bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			return !base.canPlaceBlockAt(p_149742_1_, p_149742_2_, p_149742_3_, p_149742_4_) ? false : this.canBlockStay(p_149742_1_, p_149742_2_, p_149742_3_, p_149742_4_);
		}

		public override void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			if (!this.canBlockStay(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_))
			{
				p_149695_1_.setBlockToAir(p_149695_2_, p_149695_3_, p_149695_4_);
			}
		}

///    
///     <summary> * Can this block stay at this position.  Similar to canPlaceBlockAt except gets checked often with plants. </summary>
///     
		public override bool canBlockStay(World p_149718_1_, int p_149718_2_, int p_149718_3_, int p_149718_4_)
		{
			return p_149718_1_.getBlock(p_149718_2_, p_149718_3_ - 1, p_149718_4_).BlockMaterial.Solid;
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public override int quantityDropped(Random p_149745_1_)
		{
			return 0;
		}

		public override Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return null;
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public override Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Items.cake;
		}
	}

}