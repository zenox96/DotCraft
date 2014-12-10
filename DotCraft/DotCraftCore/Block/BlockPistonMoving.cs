using System;

namespace DotCraftCore.Block
{

	
	using IIconRegister = DotCraftCore.client.renderer.texture.IIconRegister;
	using EntityPlayer = DotCraftCore.Entity.Player.EntityPlayer;
	using Item = DotCraftCore.item.Item;
	using TileEntity = DotCraftCore.tileentity.TileEntity;
	using TileEntityPiston = DotCraftCore.tileentity.TileEntityPiston;
	using AxisAlignedBB = DotCraftCore.util.AxisAlignedBB;
	using Facing = DotCraftCore.util.Facing;
	using IBlockAccess = DotCraftCore.world.IBlockAccess;
	using World = DotCraftCore.world.World;

	public class BlockPistonMoving : BlockContainer
	{
		private const string __OBFID = "CL_00000368";

		public BlockPistonMoving() : base(Material.piston)
		{
			this.Hardness = -1.0F;
		}

///    
///     <summary> * Returns a new instance of a block's tile entity class. Called on placing the block. </summary>
///     
		public virtual TileEntity createNewTileEntity(World p_149915_1_, int p_149915_2_)
		{
			return null;
		}

		public override void onBlockAdded(World p_149726_1_, int p_149726_2_, int p_149726_3_, int p_149726_4_)
		{
		}

		public override void breakBlock(World p_149749_1_, int p_149749_2_, int p_149749_3_, int p_149749_4_, Block p_149749_5_, int p_149749_6_)
		{
			TileEntity var7 = p_149749_1_.getTileEntity(p_149749_2_, p_149749_3_, p_149749_4_);

			if (var7 is TileEntityPiston)
			{
				((TileEntityPiston)var7).func_145866_f();
			}
			else
			{
				base.breakBlock(p_149749_1_, p_149749_2_, p_149749_3_, p_149749_4_, p_149749_5_, p_149749_6_);
			}
		}

		public virtual bool canPlaceBlockAt(World p_149742_1_, int p_149742_2_, int p_149742_3_, int p_149742_4_)
		{
			return false;
		}

///    
///     <summary> * checks to see if you can place this block can be placed on that side of a block: BlockLever overrides </summary>
///     
		public virtual bool canPlaceBlockOnSide(World p_149707_1_, int p_149707_2_, int p_149707_3_, int p_149707_4_, int p_149707_5_)
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
				return -1;
			}
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
///     <summary> * Called upon block activation (right click on the block.) </summary>
///     
		public virtual bool onBlockActivated(World p_149727_1_, int p_149727_2_, int p_149727_3_, int p_149727_4_, EntityPlayer p_149727_5_, int p_149727_6_, float p_149727_7_, float p_149727_8_, float p_149727_9_)
		{
			if (!p_149727_1_.isClient && p_149727_1_.getTileEntity(p_149727_2_, p_149727_3_, p_149727_4_) == null)
			{
				p_149727_1_.setBlockToAir(p_149727_2_, p_149727_3_, p_149727_4_);
				return true;
			}
			else
			{
				return false;
			}
		}

		public virtual Item getItemDropped(int p_149650_1_, Random p_149650_2_, int p_149650_3_)
		{
			return null;
		}

///    
///     <summary> * Drops the block items with a specified chance of dropping the specified items </summary>
///     
		public virtual void dropBlockAsItemWithChance(World p_149690_1_, int p_149690_2_, int p_149690_3_, int p_149690_4_, int p_149690_5_, float p_149690_6_, int p_149690_7_)
		{
			if (!p_149690_1_.isClient)
			{
				TileEntityPiston var8 = this.func_149963_e(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_);

				if (var8 != null)
				{
					var8.func_145861_a().dropBlockAsItem(p_149690_1_, p_149690_2_, p_149690_3_, p_149690_4_, var8.BlockMetadata, 0);
				}
			}
		}

		public virtual void onNeighborBlockChange(World p_149695_1_, int p_149695_2_, int p_149695_3_, int p_149695_4_, Block p_149695_5_)
		{
			if (!p_149695_1_.isClient)
			{
				p_149695_1_.getTileEntity(p_149695_2_, p_149695_3_, p_149695_4_);
			}
		}

		public static TileEntity func_149962_a(Block p_149962_0_, int p_149962_1_, int p_149962_2_, bool p_149962_3_, bool p_149962_4_)
		{
			return new TileEntityPiston(p_149962_0_, p_149962_1_, p_149962_2_, p_149962_3_, p_149962_4_);
		}

///    
///     <summary> * Returns a bounding box from the pool of bounding boxes (this means this box can change after the pool has been
///     * cleared to be reused) </summary>
///     
		public virtual AxisAlignedBB getCollisionBoundingBoxFromPool(World p_149668_1_, int p_149668_2_, int p_149668_3_, int p_149668_4_)
		{
			TileEntityPiston var5 = this.func_149963_e(p_149668_1_, p_149668_2_, p_149668_3_, p_149668_4_);

			if (var5 == null)
			{
				return null;
			}
			else
			{
				float var6 = var5.func_145860_a(0.0F);

				if (var5.func_145868_b())
				{
					var6 = 1.0F - var6;
				}

				return this.func_149964_a(p_149668_1_, p_149668_2_, p_149668_3_, p_149668_4_, var5.func_145861_a(), var6, var5.func_145864_c());
			}
		}

		public virtual void setBlockBoundsBasedOnState(IBlockAccess p_149719_1_, int p_149719_2_, int p_149719_3_, int p_149719_4_)
		{
			TileEntityPiston var5 = this.func_149963_e(p_149719_1_, p_149719_2_, p_149719_3_, p_149719_4_);

			if (var5 != null)
			{
				Block var6 = var5.func_145861_a();

				if (var6 == this || var6.Material == Material.air)
				{
					return;
				}

				var6.setBlockBoundsBasedOnState(p_149719_1_, p_149719_2_, p_149719_3_, p_149719_4_);
				float var7 = var5.func_145860_a(0.0F);

				if (var5.func_145868_b())
				{
					var7 = 1.0F - var7;
				}

				int var8 = var5.func_145864_c();
				this.field_149759_B = var6.BlockBoundsMinX - (double)((float)Facing.offsetsXForSide[var8] * var7);
				this.field_149760_C = var6.BlockBoundsMinY - (double)((float)Facing.offsetsYForSide[var8] * var7);
				this.field_149754_D = var6.BlockBoundsMinZ - (double)((float)Facing.offsetsZForSide[var8] * var7);
				this.field_149755_E = var6.BlockBoundsMaxX - (double)((float)Facing.offsetsXForSide[var8] * var7);
				this.field_149756_F = var6.BlockBoundsMaxY - (double)((float)Facing.offsetsYForSide[var8] * var7);
				this.field_149757_G = var6.BlockBoundsMaxZ - (double)((float)Facing.offsetsZForSide[var8] * var7);
			}
		}

		public virtual AxisAlignedBB func_149964_a(World p_149964_1_, int p_149964_2_, int p_149964_3_, int p_149964_4_, Block p_149964_5_, float p_149964_6_, int p_149964_7_)
		{
			if (p_149964_5_ != this && p_149964_5_.Material != Material.air)
			{
				AxisAlignedBB var8 = p_149964_5_.getCollisionBoundingBoxFromPool(p_149964_1_, p_149964_2_, p_149964_3_, p_149964_4_);

				if (var8 == null)
				{
					return null;
				}
				else
				{
					if (Facing.offsetsXForSide[p_149964_7_] < 0)
					{
						var8.minX -= (double)((float)Facing.offsetsXForSide[p_149964_7_] * p_149964_6_);
					}
					else
					{
						var8.maxX -= (double)((float)Facing.offsetsXForSide[p_149964_7_] * p_149964_6_);
					}

					if (Facing.offsetsYForSide[p_149964_7_] < 0)
					{
						var8.minY -= (double)((float)Facing.offsetsYForSide[p_149964_7_] * p_149964_6_);
					}
					else
					{
						var8.maxY -= (double)((float)Facing.offsetsYForSide[p_149964_7_] * p_149964_6_);
					}

					if (Facing.offsetsZForSide[p_149964_7_] < 0)
					{
						var8.minZ -= (double)((float)Facing.offsetsZForSide[p_149964_7_] * p_149964_6_);
					}
					else
					{
						var8.maxZ -= (double)((float)Facing.offsetsZForSide[p_149964_7_] * p_149964_6_);
					}

					return var8;
				}
			}
			else
			{
				return null;
			}
		}

		private TileEntityPiston func_149963_e(IBlockAccess p_149963_1_, int p_149963_2_, int p_149963_3_, int p_149963_4_)
		{
			TileEntity var5 = p_149963_1_.getTileEntity(p_149963_2_, p_149963_3_, p_149963_4_);
			return var5 is TileEntityPiston ? (TileEntityPiston)var5 : null;
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
			this.blockIcon = p_149651_1_.registerIcon("piston_top_normal");
		}
	}

}