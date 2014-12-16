using System.Collections;

namespace DotCraftCore.Block
{

	
	using CreativeTabs = DotCraftCore.CreativeTab.CreativeTabs;
	using Entity = DotCraftCore.Entity.Entity;
	using EntityBoat = DotCraftCore.Entity.Item.EntityBoat;
	using Blocks = DotCraftCore.Init.Blocks;
	using AxisAlignedBB = DotCraftCore.Util.AxisAlignedBB;
	using IBlockAccess = DotCraftCore.World.IBlockAccess;
	using World = DotCraftCore.World.World;

	public class BlockLilyPad : BlockBush
	{
		

		protected internal BlockLilyPad()
		{
			float var1 = 0.5F;
			float var2 = 0.015625F;
			this.setBlockBounds(0.5F - var1, 0.0F, 0.5F - var1, 0.5F + var1, var2, 0.5F + var1);
			this.CreativeTab = CreativeTabs.tabDecorations;
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public override int RenderType
		{
			get
			{
				return 23;
			}
		}

		public virtual void addCollisionBoxesToList(World p_149743_1_, int p_149743_2_, int p_149743_3_, int p_149743_4_, AxisAlignedBB p_149743_5_, IList p_149743_6_, Entity p_149743_7_)
		{
			if (p_149743_7_ == null || !(p_149743_7_ is EntityBoat))
			{
				base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
			}
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public override AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			return AxisAlignedBB.getBoundingBox((double)p_149668_2_ + this.field_149759_B, (double)p_149668_3_ + this.field_149760_C, (double)p_149668_4_ + this.field_149754_D, (double)p_149668_2_ + this.field_149755_E, (double)p_149668_3_ + this.field_149756_F, (double)p_149668_4_ + this.field_149757_G);
		}

		public virtual int BlockColor
		{
			get
			{
				return 2129968;
			}
		}

///    
///     <summary> * Returns the color this block should be rendered. Used by leaves. </summary>
///     
		public virtual int getRenderColor(int p_149741_1_)
		{
			return 2129968;
		}

///    
///     <summary> * Returns a integer with hex for 0xrrggbb with this color multiplied against the blocks color. Note only called
///     * when first determining what to render. </summary>
///     
		public virtual int colorMultiplier(IBlockAccess p_149720_1_, int p_149720_2_, int p_149720_3_, int p_149720_4_)
		{
			return 2129968;
		}

		protected internal override bool func_149854_a(Block p_149854_1_)
		{
			return p_149854_1_ == Blocks.water;
		}

///    
///     <summary> * Can this block stay at this position.  Similar to canPlaceBlockAt except gets checked often with plants. </summary>
///     
		public override bool canBlockStay(World p_149718_1_, int p_149718_2_, int p_149718_3_, int p_149718_4_)
		{
			return p_149718_3_ >= 0 && p_149718_3_ < 256 ? p_149718_1_.getBlock(p_149718_2_, p_149718_3_ - 1, p_149718_4_).Material == Material.water && p_149718_1_.getBlockMetadata(p_149718_2_, p_149718_3_ - 1, p_149718_4_) == 0 : false;
		}
	}

}