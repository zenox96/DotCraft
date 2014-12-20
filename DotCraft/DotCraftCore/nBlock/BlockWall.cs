using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nInit;
using DotCraftCore.nInventory;
using DotCraftCore.nItem;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockWall : Block
	{
		public static readonly string[] field_150092_a = new string[] {"normal", "mossy"};
		

		public BlockWall(Block p_i45435_1_) : base(p_i45435_1_.BlockMaterial)
		{
			this.Hardness = p_i45435_1_.blockHardness;
			this.Resistance = p_i45435_1_.blockResistance / 3.0F;
			this.StepSound = p_i45435_1_.stepSound;
			this.CreativeTab = CreativeTabs.tabBlock;
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public virtual int RenderType
		{
			get
			{
				return 32;
			}
		}

		public virtual bool renderAsNormalBlock()
		{
			return false;
		}

		public virtual bool getBlocksMovement(IBlockAccess p_149655_1_, int p_149655_2_, int p_149655_3_, int p_149655_4_)
		{
			return false;
		}

		public virtual bool isOpaqueCube()
		{
			get
			{
				return false;
			}
		}

		public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			bool var5 = this.func_150091_e(p_149719_1_, p_149719_2_, p_149719_3_, p_149719_4_ - 1);
			bool var6 = this.func_150091_e(p_149719_1_, p_149719_2_, p_149719_3_, p_149719_4_ + 1);
			bool var7 = this.func_150091_e(p_149719_1_, p_149719_2_ - 1, p_149719_3_, p_149719_4_);
			bool var8 = this.func_150091_e(p_149719_1_, p_149719_2_ + 1, p_149719_3_, p_149719_4_);
			float var9 = 0.25F;
			float var10 = 0.75F;
			float var11 = 0.25F;
			float var12 = 0.75F;
			float var13 = 1.0F;

			if (var5)
			{
				var11 = 0.0F;
			}

			if (var6)
			{
				var12 = 1.0F;
			}

			if (var7)
			{
				var9 = 0.0F;
			}

			if (var8)
			{
				var10 = 1.0F;
			}

			if (var5 && var6 && !var7 && !var8)
			{
				var13 = 0.8125F;
				var9 = 0.3125F;
				var10 = 0.6875F;
			}
			else if (!var5 && !var6 && var7 && var8)
			{
				var13 = 0.8125F;
				var11 = 0.3125F;
				var12 = 0.6875F;
			}

			this.setBlockBounds(var9, 0.0F, var11, var10, var13, var12);
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public virtual AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			this.setBlockBoundsBasedOnState(p_149668_1_, p_149668_2_, p_149668_3_, p_149668_4_);
			this.field_149756_F = 1.5D;
			return base.getCollisionBoundingBoxFromPool(p_149668_1_, p_149668_2_, p_149668_3_, p_149668_4_);
		}

		public virtual bool func_150091_e(IBlockAccess p_150091_1_, int p_150091_2_, int p_150091_3_, int p_150091_4_)
		{
			Block var5 = p_150091_1_.getBlock(p_150091_2_, p_150091_3_, p_150091_4_);
			return var5 != this && var5 != Blocks.fence_gate ? (var5.BlockMaterial.Opaque && var5.renderAsNormalBlock() ? var5.BlockMaterial != Material.field_151572_C : false) : true;
		}

		public virtual void getSubBlocks(Item p_149666_1_, CreativeTabs p_149666_2_, IList p_149666_3_)
		{
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 0));
			p_149666_3_.Add(new ItemStack(p_149666_1_, 1, 1));
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public virtual int damageDropped(int p_149692_1_)
		{
			return p_149692_1_;
		}

		public virtual bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
		{
			return p_149646_5_ == 0 ? base.shouldSideBeRendered(p_149646_1_, p_149646_2_, p_149646_3_, p_149646_4_, p_149646_5_) : true;
		}
	}
}