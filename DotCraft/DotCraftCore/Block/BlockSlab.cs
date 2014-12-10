using System;
using System.Collections;

namespace DotCraftCore.Block
{

	
	using Entity = DotCraftCore.entity.Entity;
	using Blocks = DotCraftCore.init.Blocks;
	using Item = DotCraftCore.item.Item;
	using AxisAlignedBB = DotCraftCore.util.AxisAlignedBB;
	using Facing = DotCraftCore.util.Facing;
	using IBlockAccess = DotCraftCore.world.IBlockAccess;
	using World = DotCraftCore.world.World;

	public abstract class BlockSlab : Block
	{
		protected internal readonly bool field_150004_a;
		private const string __OBFID = "CL_00000253";

		public BlockSlab(bool p_i45410_1_, Material p_i45410_2_) : base(p_i45410_2_)
		{
			this.field_150004_a = p_i45410_1_;

			if (p_i45410_1_)
			{
				this.opaque = true;
			}
			else
			{
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.5F, 1.0F);
			}

			this.LightOpacity = 255;
		}

		public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			if (this.field_150004_a)
			{
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
			}
			else
			{
				bool var5 = (p_149719_1_.getBlockMetadata(p_149719_2_, p_149719_3_, p_149719_4_) & 8) != 0;

				if (var5)
				{
					this.setBlockBounds(0.0F, 0.5F, 0.0F, 1.0F, 1.0F, 1.0F);
				}
				else
				{
					this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.5F, 1.0F);
				}
			}
		}

///    
///     <summary> * Sets the block's bounds for rendering it as an item </summary>
///     
		public virtual void setBlockBoundsForItemRender()
		{
			if (this.field_150004_a)
			{
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 1.0F);
			}
			else
			{
				this.setBlockBounds(0.0F, 0.0F, 0.0F, 1.0F, 0.5F, 1.0F);
			}
		}

		public virtual void addCollisionBoxesToList(World p_149743_1_, int p_149743_2_, int p_149743_3_, int p_149743_4_, AxisAlignedBB p_149743_5_, IList p_149743_6_, Entity p_149743_7_)
		{
			this.setBlockBoundsBasedOnState(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_);
			base.addCollisionBoxesToList(p_149743_1_, p_149743_2_, p_149743_3_, p_149743_4_, p_149743_5_, p_149743_6_, p_149743_7_);
		}

		public virtual bool isOpaqueCube()
		{
			get
			{
				return this.field_150004_a;
			}
		}

		public virtual int onBlockPlaced(World p_149660_1_, int p_149660_2_, int p_149660_3_, int p_149660_4_, int p_149660_5_, float p_149660_6_, float p_149660_7_, float p_149660_8_, int p_149660_9_)
		{
			return this.field_150004_a ? p_149660_9_ : (p_149660_5_ != 0 && (p_149660_5_ == 1 || (double)p_149660_7_ <= 0.5D) ? p_149660_9_ : p_149660_9_ | 8);
		}

///    
///     <summary> * Returns the quantity of items to drop on block destruction. </summary>
///     
		public virtual int quantityDropped(Random p_149745_1_)
		{
			return this.field_150004_a ? 2 : 1;
		}

///    
///     <summary> * Determines the damage on the item the block drops. Used in cloth and wood. </summary>
///     
		public virtual int damageDropped(int p_149692_1_)
		{
			return p_149692_1_ & 7;
		}

		public virtual bool renderAsNormalBlock()
		{
			return this.field_150004_a;
		}

		public virtual bool shouldSideBeRendered(IBlockAccess p_149646_1_, int p_149646_2_, int p_149646_3_, int p_149646_4_, int p_149646_5_)
		{
			if (this.field_150004_a)
			{
				return base.shouldSideBeRendered(p_149646_1_, p_149646_2_, p_149646_3_, p_149646_4_, p_149646_5_);
			}
			else if (p_149646_5_ != 1 && p_149646_5_ != 0 && !base.shouldSideBeRendered(p_149646_1_, p_149646_2_, p_149646_3_, p_149646_4_, p_149646_5_))
			{
				return false;
			}
			else
			{
				int var6 = p_149646_2_ + Facing.offsetsXForSide[Facing.oppositeSide[p_149646_5_]];
				int var7 = p_149646_3_ + Facing.offsetsYForSide[Facing.oppositeSide[p_149646_5_]];
				int var8 = p_149646_4_ + Facing.offsetsZForSide[Facing.oppositeSide[p_149646_5_]];
				bool var9 = (p_149646_1_.getBlockMetadata(var6, var7, var8) & 8) != 0;
				return var9 ? (p_149646_5_ == 0 ? true : (p_149646_5_ == 1 && base.shouldSideBeRendered(p_149646_1_, p_149646_2_, p_149646_3_, p_149646_4_, p_149646_5_) ? true : !func_150003_a(p_149646_1_.getBlock(p_149646_2_, p_149646_3_, p_149646_4_)) || (p_149646_1_.getBlockMetadata(p_149646_2_, p_149646_3_, p_149646_4_) & 8) == 0)) : (p_149646_5_ == 1 ? true : (p_149646_5_ == 0 && base.shouldSideBeRendered(p_149646_1_, p_149646_2_, p_149646_3_, p_149646_4_, p_149646_5_) ? true : !func_150003_a(p_149646_1_.getBlock(p_149646_2_, p_149646_3_, p_149646_4_)) || (p_149646_1_.getBlockMetadata(p_149646_2_, p_149646_3_, p_149646_4_) & 8) != 0));
			}
		}

		private static bool func_150003_a(Block p_150003_0_)
		{
			return p_150003_0_ == Blocks.stone_slab || p_150003_0_ == Blocks.wooden_slab;
		}

		public abstract string func_150002_b(int p_150002_1_);

///    
///     <summary> * Get the block's damage value (for use with pick block). </summary>
///     
		public virtual int getDamageValue(World p_149643_1_, int p_149643_2_, int p_149643_3_, int p_149643_4_)
		{
			return base.getDamageValue(p_149643_1_, p_149643_2_, p_149643_3_, p_149643_4_) & 7;
		}

///    
///     <summary> * Gets an item for the block being called on. Args: world, x, y, z </summary>
///     
		public virtual Item getItem(World p_149694_1_, int p_149694_2_, int p_149694_3_, int p_149694_4_)
		{
			return func_150003_a(this) ? Item.getItemFromBlock(this) : (this == Blocks.double_stone_slab ? Item.getItemFromBlock(Blocks.stone_slab) : (this == Blocks.double_wooden_slab ? Item.getItemFromBlock(Blocks.wooden_slab) : Item.getItemFromBlock(Blocks.stone_slab)));
		}
	}

}