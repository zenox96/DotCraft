using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
using System;

namespace DotCraftCore.nBlock
{
	public class BlockBush : Block
	{
		protected internal BlockBush(Material p_i45395_1_) : base(p_i45395_1_)
		{
			this.TickRandomly = true;
			float var2 = 0.2F;
			this.setBlockBounds(0.5F - var2, 0.0F, 0.5F - var2, 0.5F + var2, var2 * 3.0F, 0.5F + var2);
			this.CreativeTab = CreativeTabs.tabDecorations;
		}

		protected internal BlockBush() : this(Material.plants)
		{
		}

		public virtual bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			return base.canPlaceBlockAt(p_149742_1_, p_149742_2_, p_149742_3_, p_149742_4_) && this.func_149854_a(p_149742_1_.getBlock(p_149742_2_, p_149742_3_ - 1, p_149742_4_));
		}

		protected internal virtual bool func_149854_a(Block p_149854_1_)
		{
			return p_149854_1_ == Blocks.grass || p_149854_1_ == Blocks.dirt || p_149854_1_ == Blocks.farmland;
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			base.onNeighborBlockChange(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_, p_149695_5_);
			this.func_149855_e(p_149695_1_, p_149695_2_, p_149695_3_, p_149695_4_);
		}

///    
///     <summary> * Ticks the block if it's been scheduled </summary>
///     
		public virtual void updateTick(World p_149674_1_, int p_149674_2_, int p_149674_3_, int p_149674_4_, Random p_149674_5_)
		{
			this.func_149855_e(p_149674_1_, p_149674_2_, p_149674_3_, p_149674_4_);
		}

		protected internal virtual void func_149855_e(World p_149855_1_, int p_149855_2_, int p_149855_3_, int p_149855_4_)
		{
			if (!this.canBlockStay(p_149855_1_, p_149855_2_, p_149855_3_, p_149855_4_))
			{
				this.dropBlockAsItem(p_149855_1_, p_149855_2_, p_149855_3_, p_149855_4_, p_149855_1_.getBlockMetadata(p_149855_2_, p_149855_3_, p_149855_4_), 0);
				p_149855_1_.setBlock(p_149855_2_, p_149855_3_, p_149855_4_, getBlockById(0), 0, 2);
			}
		}

///    
///     <summary> * Can this block stay at this position.  Similar to canPlaceBlockAt except gets checked often with plants. </summary>
///     
		public virtual bool canBlockStay(World p_149718_1_, int p_149718_2_, int p_149718_3_, int p_149718_4_)
		{
			return this.func_149854_a(p_149718_1_.getBlock(p_149718_2_, p_149718_3_ - 1, p_149718_4_));
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public virtual AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
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

		public virtual bool renderAsNormalBlock()
		{
			return false;
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public virtual int RenderType
		{
			get
			{
				return 1;
			}
		}
	}

}