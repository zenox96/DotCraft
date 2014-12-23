using DotCraftCore.nBlock.nMaterial;
using DotCraftCore.nEntity;
using DotCraftCore.nItem;
using DotCraftCore.nTileEntity;
using DotCraftCore.nUtil;
using DotCraftCore.nWorld;
using System;
using System.Collections;

namespace DotCraftCore.nBlock
{
	public class BlockEndPortal : BlockContainer
	{
		public static bool field_149948_a;

		protected internal BlockEndPortal(Material p_i45404_1_) : base(p_i45404_1_)
		{
			this.LightLevel = 1.0F;
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public override TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return new TileEntityEndPortal();
		}

		public override void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			float var5 = 0.0625F;
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, var5, 1.0F);
		}

		public override bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
		{
			return p_149646_5_ != 0 ? false : base.shouldSideBeRendered(p_149646_1_, p_149646_2_, p_149646_3_, p_149646_4_, p_149646_5_);
		}

		public override void addCollisionBoxesToList(World p_149743_1_, int p_149743_2_, int p_149743_3_, int p_149743_4_, AxisAlignedBB p_149743_5_, IList p_149743_6_, Entity p_149743_7_)
		{
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
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public override int quantityDropped(Random p_149745_1_)
		{
			return 0;
		}

		public override void onEntityCollidedWithBlock(World p_149670_1_, int p_149670_2_, int p_149670_3_, int p_149670_4_, Entity p_149670_5_)
		{
			if (p_149670_5_.ridingEntity == null && p_149670_5_.riddenByEntity == null && !p_149670_1_.isClient)
			{
				p_149670_5_.travelToDimension(1);
			}
		}

///    
///     <summary> * A randomly called display update to be able to add particles or other items for display </summary>
///     
		public override void randomDisplayTick(World p_149734_1_, int p_149734_2_, int p_149734_3_, int p_149734_4_, Random p_149734_5_)
		{
			double var6 = (double)p_149734_2_ + p_149734_5_.NextDouble();
			double var8 = (double)p_149734_3_ + 0.8D;
			double var10 = (double)p_149734_4_ + p_149734_5_.NextDouble();
			double var12 = 0.0D;
			double var14 = 0.0D;
			double var16 = 0.0D;
			p_149734_1_.spawnParticle("smoke", var6, var8, var10, var12, var14, var16);
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public override int RenderType
		{
			get
			{
				return -1;
			}
		}

		public override void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
			if (!field_149948_a)
			{
				if (p_149726_1_.provider.dimensionId != 0)
				{
					p_149726_1_.setBlockToAir(p_149726_2_, p_149726_3_, p_149726_4_);
				}
			}
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public override Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Item.getItemById(0);
		}

		public override MapColor getMapColor(int p_149728_1_)
		{
			return MapColor.field_151654_J;
		}
	}
}