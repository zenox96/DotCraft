using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockCarpet : Block
	{
		protected internal BlockCarpet() : base(Material.carpet)
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.0625F, 1.0F);
			this.TickRandomly = true;
			this.CreativeTab = CreativeTabs.tabDecorations;
			this.func_150089_b(0);
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			sbyte var5 = 0;
			float var6 = 0.0625F;
			return AxisAlignedBB.getBoundingBox((double)p_149668_2_ + this.field_149759_B, (double)p_149668_3_ + this.field_149760_C, (double)p_149668_4_ + this.field_149754_D, (double)p_149668_2_ + this.field_149755_E, (double)((float)p_149668_3_ + (float)var5 * var6), (double)p_149668_4_ + this.field_149757_G);
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
///     <summary> * Sets the block's bounds for rendering it as an item </summary>
///     
		public override void setBlockBoundsForItemRender()
		{
			this.func_150089_b(0);
		}

		public override void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			this.func_150089_b(p_149719_1_.getBlockMetadata(p_149719_2_, p_149719_3_, p_149719_4_));
		}

		protected internal override void func_150089_b(int p_150089_1_)
		{
			sbyte var2 = 0;
			float var3 = (float)(1 * (1 + var2)) / 16.0F;
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, var3, 1.0F);
		}

		public override bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			return base.canPlaceBlockAt(p_149742_1_, p_149742_2_, p_149742_3_, p_149742_4_) && this.canBlockStay(p_149742_1_, p_149742_2_, p_149742_3_, p_149742_4_);
		}

		public override void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			this.func_150090_e(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_);
		}

		private bool func_150090_e(World p_150090_1_, int p_150090_2_, int p_150090_3_, int p_150090_4_)
		{
			if (!this.canBlockStay(p_150090_1_, p_150090_2_, p_150090_3_, p_150090_4_))
			{
				this.dropBlockAsItem(p_150090_1_, p_150090_2_, p_150090_3_, p_150090_4_, p_150090_1_.getBlockMetadata(p_150090_2_, p_150090_3_, p_150090_4_), 0);
				p_150090_1_.setBlockToAir(p_150090_2_, p_150090_3_, p_150090_4_);
				return false;
			}
			else
			{
				return true;
			}
		}

///    
///     <summary> * Can this block stay at this position.  Similar to canPlaceBlockAt except gets checked often with plants. </summary>
///     
		public override bool canBlockStay(World p_149718_1_, int p_149718_2_, int p_149718_3_, int p_149718_4_)
		{
			return !p_149718_1_.isAirBlock(p_149718_2_, p_149718_3_ - 1, p_149718_4_);
		}

		public override bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
		{
			return p_149646_5_ == 1 ? true : base.shouldSideBeRendered(p_149646_1_, p_149646_2_, p_149646_3_, p_149646_4_, p_149646_5_);
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public override int damageDropped(int p_149692_1_)
		{
			return p_149692_1_;
		}

		public override void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			for (int var4 = 0; var4 < 16; ++var4)
			{
				p_149666_3_.Add(new ItemStack(p_149666_1_, 1, var4));
			}
		}
	}
}