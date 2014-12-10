using System;
using System.Collections;

namespace DotCraftCore.Block
{

	using MapColor = DotCraftCore.Block.material.MapColor;
	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using Entity = DotCraftCore.entity.Entity;
	using Item = DotCraftCore.item.Item;
	using TileEntity = DotCraftCore.tileentity.TileEntity;
	using TileEntityEndPortal = DotCraftCore.tileentity.TileEntityEndPortal;
	using AxisAlignedBB = DotCraftCore.util.AxisAlignedBB;
	using IBlockAccess = DotCraftCore.world.IBlockAccess;
	using World = DotCraftCore.world.World;

	public class BlockEndPortal : BlockContainer
	{
		public static bool field_149948_a;
		private const string __OBFID = "CL_00000236";

		protected internal BlockEndPortal(Material p_i45404_1_) : base(p_i45404_1_)
		{
			this.LightLevel = 1.0F;
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public virtual TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return new TileEntityEndPortal();
		}

		public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			float var5 = 0.0625F;
			this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, var5, 1.0F);
		}

		public virtual bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
		{
			return p_149646_5_ != 0 ? false : base.shouldSideBeRendered(p_149646_1_, p_149646_2_, p_149646_3_, p_149646_4_, p_149646_5_);
		}

		public virtual void addCollisionBoxesToList(World p_149743_1_, int p_149743_2_, int p_149743_3_, int p_149743_4_, AxisAlignedBB p_149743_5_, IList p_149743_6_, Entity p_149743_7_)
		{
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

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return 0;
		}

		public virtual void onEntityCollidedWithBlock(World p_149670_1_, int p_149670_2_, int p_149670_3_, int p_149670_4_, Entity p_149670_5_)
		{
			if (p_149670_5_.ridingEntity == null && p_149670_5_.riddenByEntity == null && !p_149670_1_.isClient)
			{
				p_149670_5_.travelToDimension(1);
			}
		}

///    
///     <summary> * A randomly called display update to be able to add particles or other items for display </summary>
///     
		public virtual void randomDisplayTick(World p_149734_1_, int p_149734_2_, int p_149734_3_, int p_149734_4_, Random p_149734_5_)
		{
			double var6 = (double)((float)p_149734_2_ + p_149734_5_.nextFloat());
			double var8 = (double)((float)p_149734_3_ + 0.8F);
			double var10 = (double)((float)p_149734_4_ + p_149734_5_.nextFloat());
			double var12 = 0.0D;
			double var14 = 0.0D;
			double var16 = 0.0D;
			p_149734_1_.spawnParticle("smoke", var6, var8, var10, var12, var14, var16);
		}

///    
///     <summary> * The type of render function that is called for this block </summary>
///     
		public virtual int RenderType
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
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return Item.getItemById(0);
		}

		public virtual void registerBlockIcons(IIconRegister p_149651_1_)
		{
			this.blockIcon = p_149651_1_.registerIcon("portal");
		}

		public virtual MapColor getMapColor(int p_149728_1_)
		{
			return MapColor.field_151654_J;
		}
	}

}