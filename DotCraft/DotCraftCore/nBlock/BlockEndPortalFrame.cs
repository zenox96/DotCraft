using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity;
using DotCraftCore.nItem;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
using System;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockEndPortalFrame : Block
	{
		public BlockEndPortalFrame() : base(Material.rock)
		{
		}

		public override bool OpaqueCube
		{
			get
			{
				return false;
			}
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public virtual int RenderType
		{
			get
			{
				return 26;
			}
		}

///    
///     <summary> * Sets the block's bounds for rendering it as an item </summary>
///     
		public virtual void setBlockBoundsForItemRender()
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.8125F, 1.0F);
		}

		public virtual void addCollisionBoxesToList(World p_149743_1_, int p_149743_2_, int p_149743_3_, int p_149743_4_, AxisAlignedBB p_149743_5_, IList p_149743_6_, Entity p_149743_7_)
		{
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.8125F, 1.0F);
			base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
			int var8 = p_149743_1_.getBlockMetadata(p_149743_2_, p_149743_3_, p_149743_4_);

			if (func_150020_b(var8))
			{
				this.setBlockBounds(0.3125F, 0.8125F, 0.3125F, 0.6875F, 1.0F, 0.6875F);
				base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
			}

			this.setBlockBoundsForItemRender();
		}

		public static bool func_150020_b(int p_150020_0_)
		{
			return (p_150020_0_ & 4) != 0;
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return null;
		}

///    
///     <summary> * Called when the block is placed in the world. </summary>
///     
		public virtual void onBlockPlacedBy(World p_149689_1_, int p_149689_2_, int p_149689_3_, int p_149689_4_, EntityLivingBase p_149689_5_, ItemStack p_149689_6_)
		{
			int var7 = ((MathHelper.floor_double((double)(p_149689_5_.rotationYaw * 4.0F / 360.0F) + 0.5D) & 3) + 2) % 4;
			p_149689_1_.setBlockMetadataWithNotify(p_149689_2_, p_149689_3_, p_149689_4_, var7, 2);
		}

		public virtual bool hasComparatorInputOverride()
		{
			return true;
		}

		public virtual int getComparatorInputOverride(World p_149736_1_, int p_149736_2_, int p_149736_3_, int p_149736_4_, int p_149736_5_)
		{
			int var6 = p_149736_1_.getBlockMetadata(p_149736_2_, p_149736_3_, p_149736_4_);
			return func_150020_b(var6) ? 15 : 0;
		}
	}

}